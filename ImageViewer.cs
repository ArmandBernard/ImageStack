using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ImageStack
{
    /// <summary>
    /// Image Viewer with Pan and Zoom functionality
    /// </summary>
    public class ImageViewer : Control
    {
        #region Properties

        #region Options

        [DefaultValue(typeof(Color), "255,255,255")]
        [Browsable(true)]
        private int _BorderThickness = 1;
        /// <summary>
        /// The thickness of the border around the control
        /// </summary>
        public int BorderThickness
        {
            get { return _BorderThickness; }
            set { _BorderThickness = value; Invalidate(); }
        }

        [DefaultValue(typeof(Color), "255,255,255")]
        [Browsable(true)]
        private Color _BorderColor = Color.White;
        /// <summary>
        /// Colour of the control's border
        /// </summary>
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; Invalidate(); }
        }


        [DefaultValue(1.25F)]
        [Browsable(true)]
        private float _ZoomStepFactor = 1.25F;
        /// <summary>
        /// The factor by which to zoom per scrollwheel step
        /// </summary>
        public float ZoomStepFactor
        {
            get { return _ZoomStepFactor; }
            set { _ZoomStepFactor = value; }
        }

        [DefaultValue(0.01F)]
        [Browsable(true)]
        /// <summary>
        /// The minimum zoom level
        /// </summary>
        private float _MinZoom = 0.01F;
        public float MinZoom
        {
            get { return _MinZoom; }
            set { _MinZoom = value; }
        }

        [DefaultValue(InterpolationMode.HighQualityBicubic)]
        [Browsable(true)]
        /// <summary>
        /// The interpolation mode for drawing the image. This is a tradeoff of quality and 
        /// responsiveness
        /// </summary>
        private InterpolationMode _InterpolationMode = InterpolationMode.HighQualityBicubic;
        public InterpolationMode InterpolationMode
        {
            get { return _InterpolationMode; }
            set { _InterpolationMode = value; }
        }

        #region MiniMap

        [Browsable(true)]
        /// <summary>
        /// Show a small minimap in the corner which shows where the viewable area relative to
        /// the image. Only active when not in zoom-to-fit mode
        /// </summary>
        private bool _ShowMiniMap = true;
        public bool ShowMiniMap
        {
            get { return _ShowMiniMap; }
            set { _ShowMiniMap = value; }
        }

        [Browsable(true)]
        /// <summary>
        /// The width of the minimap
        /// </summary>
        private int _MiniMapWidth = 100;
        public int MiniMapWidth
        {
            get { return _MiniMapWidth; }
            set { _MiniMapWidth = value; }
        }

        [Browsable(true)]
        /// <summary>
        /// The margin around the mini maps
        /// </summary>
        private int _MiniMapMargin = 10;
        public int MiniMapMargin
        {
            get { return _MiniMapMargin; }
            set { _MiniMapMargin = value; }
        }

        #endregion MiniMap

        #region ZoomButtons

        [Browsable(true)]
        /// <summary>
        /// Show the Zoom Buttons or not
        /// </summary>
        public bool ShowZoomButtons = true;

        [Browsable(true)]
        /// <summary>
        /// The size of the Zoom buttons
        /// </summary>
        public Size ZoomButtonSize = new Size(60, 40);

        [Browsable(true)]
        /// <summary>
        /// The margin around the Zoom buttons
        /// </summary>
        public int ZoomButtonMargin = 10;

        [Browsable(true)]
        /// <summary>
        /// The background colour of the Zoom buttons
        /// </summary>
        public Color ButtonColour = Color.FromArgb(128, 64, 64, 64);

        [Browsable(true)]
        /// <summary>
        /// The space between Zoom buttons
        /// </summary>
        public int ZoomButtonSpacing = 10;

        #endregion ZoomButtons

        #endregion Options

        private Point MouseDownLoc;
        private bool MouseIsDown;

        private Point StartOffset = new Point(0, 0);
        private Point Offset = new Point(0, 0);

        [DefaultValue(true)]
        [Browsable(true)]
        private bool _ZoomToFit = true;
        /// <summary>
        /// Enable Zoom-To-Fit mode
        /// </summary>
        public bool ZoomToFit
        {
            get { return _ZoomToFit; }

            set
            {
                // reset offset if setting ZoomToFit to true
                if (value) { Offset = Point.Empty; }

                _ZoomToFit = value;

                // force full refresh
                Invalidate();
            }
        }

        [DefaultValue(1)]
        [Browsable(true)]
        private float _Zoom = 1;
        /// <summary>
        /// The Zoom Level (1 = 100% zoom i.e. actual size)
        /// </summary>
        public float Zoom
        {
            get
            {
                // if Zoom-to-fit enabled
                if (ZoomToFit && Image != null)
                {
                    // get both aspect ratios
                    float ClientAR = (float)ClientRectangle.Width / ClientRectangle.Height;
                    float ImageAR = (float)Image.Width / Image.Height;

                    // depending on which aspect ratio is larger, zoom such that the image is 
                    // always fully visible, but with "black bars" if necessary
                    if (ClientAR >= ImageAR)
                    {
                        _Zoom = ((float)ClientRectangle.Height / Image.Height);
                    }
                    else
                    {
                        _Zoom = ((float)ClientRectangle.Width / Image.Width);
                    }
                }

                return _Zoom;

            }
            set
            {
                if (Image == null) { return; }

                // deactivate zoom to fit
                ZoomToFit = false;

                // invalidate the old image area
                Invalidate(ImageArea);

                _Zoom = value;

                // cap position of image to within bounds
                CapPosition();

                // invalidate the new image area
                Invalidate(ImageArea);

                // Invalidate Minimap area if it is in use
                if (ShowMiniMap)
                {
                    Invalidate(
                        new Rectangle(
                            MiniMapRectangle.X,
                            MiniMapRectangle.Y,
                            MiniMapRectangle.Width + 1,
                            MiniMapRectangle.Height + 1
                        )
                        );
                }
            }
        }

        [Browsable(true)]
        private Image _Image;
        /// <summary>
        /// The image to display
        /// </summary>
        public Image Image
        {
            get { return _Image; }

            set
            {
                _Image = value;

                Invalidate();
                Reset();
            }
        }

        #endregion Properties

        #region Rectangles

        /// <summary>
        /// Rectangle representing the image area relative to the client area 
        /// </summary>
        private Rectangle ImageArea
        {
            get
            {
                Rectangle imageRect = Rectangle.Round(new RectangleF(
                        Zoom * Offset.X,
                        Zoom * Offset.Y,
                        Zoom * Image.Width,
                        Zoom * Image.Height
                    ));

                // centering offset (keeps image and drawing rectangle centred when their 
                // sizes change)
                imageRect.Offset(
                    Convert.ToInt32(Zoom * (ClientRectangle.Width / Zoom - Image.Width) / 2),
                    Convert.ToInt32(Zoom * (ClientRectangle.Height / Zoom - Image.Height) / 2)
                    );

                return imageRect;
            }
        }

        /// <summary>
        /// Rectangle representing the client area relative to the image
        /// </summary>
        private Rectangle ClientArea
        {
            get
            {
                Rectangle drawRectangle = Rectangle.Round(new RectangleF(
                    -Offset.X,
                    -Offset.Y,
                    ClientRectangle.Width / Zoom,
                    ClientRectangle.Height / Zoom
                    ));

                // centering offset (keeps image and drawing rectangle centred when their 
                // sizes change)
                drawRectangle.Offset(
                    Convert.ToInt32((Image.Width - ClientRectangle.Width / Zoom) / 2),
                    Convert.ToInt32((Image.Height - ClientRectangle.Height / Zoom) / 2)
                    );

                return drawRectangle;
            }
        }

        // rectangle where the MiniMap will be drawn
        private Rectangle MiniMapRectangle
        {
            get
            {
                Size previewSize =
                    new Size(MiniMapWidth, MiniMapWidth * Image.Height / Image.Width);

                return new Rectangle(
                    Width - previewSize.Width - 1 - MiniMapMargin,
                    Height - previewSize.Height - 1 - MiniMapMargin,
                    previewSize.Width,
                    previewSize.Height
                    );
            }
        }

        #region Buttons

        // rectangle for zoom-out button
        private Rectangle ZoomOutBTRectangle
        {
            get
            {
                return new Rectangle(
                    Width - ZoomButtonSize.Width - ZoomButtonMargin,
                    ZoomButtonMargin,
                    ZoomButtonSize.Width,
                    ZoomButtonSize.Height
                    );
            }
        }

        // rectangle for zoom-in button
        private Rectangle ZoomInBTRectangle
        {
            get
            {
                return new Rectangle(
                    ZoomOutBTRectangle.X - ZoomButtonSize.Width - ZoomButtonSpacing,
                    ZoomOutBTRectangle.Y,
                    ZoomButtonSize.Width,
                    ZoomButtonSize.Height
                    );
            }
        }

        // rectangle for reset-zoom button
        private Rectangle ResetZoomBTRectangle
        {
            get
            {
                return new Rectangle(
                    ZoomInBTRectangle.X - ZoomButtonSize.Width - ZoomButtonSpacing,
                    ZoomInBTRectangle.Y,
                    ZoomButtonSize.Width,
                    ZoomButtonSize.Height
                    );
            }
        }

        // rectangle for reset-zoom button
        private Rectangle ZoomToFitBTRectangle
        {
            get
            {
                return new Rectangle(
                    ResetZoomBTRectangle.X - ZoomButtonSize.Width - ZoomButtonSpacing,
                    ResetZoomBTRectangle.Y,
                    ZoomButtonSize.Width,
                    ZoomButtonSize.Height
                    );
            }
        }

        #endregion Buttons

        #endregion Rectangles

        public ImageViewer()
        {
            SetStyle(
                ControlStyles.DoubleBuffer
                | ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw, true);
        }

        /// <summary>
        /// Keep the image within client bounds
        /// </summary>
        private void CapPosition()
        {
            if (Image == null) { return; }

            int margin = 5;

            // cache rectangle for speed
            Rectangle imageArea = ImageArea;

            if (imageArea.Right < margin)
            {
                Offset.X = Offset.X - Convert.ToInt32(imageArea.Right / Zoom - margin / Zoom);
            }
            else if (imageArea.Left > ClientRectangle.Width - margin)
            {
                Offset.X = Offset.X
                    - Convert.ToInt32((imageArea.Left - ClientRectangle.Width + margin) / Zoom);
            }

            if (imageArea.Bottom < margin)
            {
                Offset.Y = Offset.Y - Convert.ToInt32(imageArea.Bottom / Zoom - margin / Zoom);
            }
            else if (imageArea.Top > ClientRectangle.Height - margin)
            {
                Offset.Y = Offset.Y
                    - Convert.ToInt32((imageArea.Top - ClientRectangle.Height + margin) / Zoom);
            }
        }

        #region Public Methods

        public void ZoomIn()
        {
            // increase zoom level by zoom step
            Zoom = Zoom * ZoomStepFactor;
        }

        public void ZoomOut()
        {
            // decrease zoom level by zoom step unless at limit
            Zoom = Math.Max(Zoom / ZoomStepFactor, MinZoom);
        }

        /// <summary>
        /// Reset to 100% zoom and centred
        /// </summary>
        public void Reset()
        {
            if (ZoomToFit) { return; }

            Offset = Point.Empty;
            Zoom = 1;
            Invalidate();
        }

        #endregion Public Methods

        #region Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.InterpolationMode = InterpolationMode;

            // fill background
            g.Clear(BackColor);

            // Draw Image into client area
            DrawImage(g);

            // if minimap enabled and not in ZoomToFit mode, draw minimap
            if (ShowMiniMap && !ZoomToFit) { DrawMiniMap(g); }

            if (ShowZoomButtons) { DrawZoomButtons(g); }

            // draw border
            g.DrawRectangle(
                new Pen(BorderColor, BorderThickness),
                new Rectangle(
                    new Point(0, 0),
                    new Size(ClientSize.Width - BorderThickness, ClientSize.Height - BorderThickness))
                );

        }


        /// <summary>
        /// Draw the image in the client area
        /// </summary>
        /// <param name="g"></param>
        private void DrawImage(Graphics g)
        {
            if (Image == null) { return; }

            Rectangle srcrect = ClientArea;
            Rectangle destrect = ImageArea;

            // intersect the image-scaled client area with the image to get source rectangle.
            srcrect.Intersect(new Rectangle(Point.Empty, Image.Size));
            // intersect the client-scaled image area with the image to get the destination 
            // rectangle.
            destrect.Intersect(ClientRectangle);

            // Draw the part of the image defined by srcrect in rectangle destrect.
            // Whilst using src = ClientArea and destrect = ClientRectangle would work, it would
            // render offscreen or empty areas needlessly. This way, the performance is proportional 
            // to the number of onscreen pixels in the image only, making it the most efficient
            // render-area solution possible
            g.DrawImage(Image, destrect, srcrect, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Draw a small preview of the viewport's location relative to the image
        /// </summary>
        /// <param name="g"></param>
        private void DrawMiniMap(Graphics g)
        {
            if (Image == null) { return; }

            float scalefactor = MiniMapWidth / (float)Image.Width;

            // Rectangle used to show client area
            Rectangle HighlightRect = Rectangle.Round(new RectangleF(
                ClientArea.X * scalefactor + MiniMapRectangle.X,
                ClientArea.Y * scalefactor + MiniMapRectangle.Y,
                ClientArea.Width * scalefactor,
                ClientArea.Height * scalefactor
                ));

            HighlightRect.Intersect(MiniMapRectangle);

            g.DrawImage(Image, MiniMapRectangle);
            g.DrawRectangle(new Pen(Color.Red), HighlightRect);
        }

        /// <summary>
        /// Draw buttons onscreen
        /// </summary>
        /// <param name="g"></param>
        private void DrawZoomButtons(Graphics g)
        {
            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            SolidBrush textbrush = new SolidBrush(Color.White);
            Font font = new Font(Font.FontFamily, 12);

            g.FillRectangle(new SolidBrush(ButtonColour), ZoomOutBTRectangle);
            g.FillRectangle(new SolidBrush(ButtonColour), ZoomInBTRectangle);
            g.FillRectangle(new SolidBrush(ButtonColour), ResetZoomBTRectangle);
            g.FillRectangle(new SolidBrush(ButtonColour), ZoomToFitBTRectangle);

            g.DrawString("-", font, textbrush, ZoomOutBTRectangle, format);
            g.DrawString(
                "+", new Font(Font.FontFamily, 12), textbrush, ZoomInBTRectangle, format);
            g.DrawString(
                "100%", new Font(Font.FontFamily, 12), textbrush, ResetZoomBTRectangle, format);
            g.DrawString(
                "Fit", new Font(Font.FontFamily, 12), textbrush, ZoomToFitBTRectangle, format);
        }

        #endregion Paint

        #region Mouse Events

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            JointClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            JointClick(e);
        }

        private void JointClick(MouseEventArgs e)
        {
            // left click
            if (e.Button == MouseButtons.Left)
            {
                // if clicking zoom in and button visible
                if (ShowZoomButtons && ZoomInBTRectangle.Contains(e.Location))
                {
                    ZoomIn();
                }
                // if clicking zoom out and button visible
                else if (ShowZoomButtons && ZoomOutBTRectangle.Contains(e.Location))
                {
                    ZoomOut();
                }
                else if (ShowZoomButtons && ResetZoomBTRectangle.Contains(e.Location))
                {
                    ZoomToFit = false;
                    Reset();
                }
                else if (ShowZoomButtons && ZoomToFitBTRectangle.Contains(e.Location))
                {
                    ZoomToFit = true;
                }
            }
        }

        /// <summary>
        /// Happens when mouse is moved across the control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if the user is holding down the left mouse button and they haven't move out of the
            // client area
            if (MouseIsDown && Image != null)
            {

                // record the distance moved by the mouse
                int dx = (MouseDownLoc.X - e.Location.X);
                int dy = (MouseDownLoc.Y - e.Location.Y);

                // mark the old onscreen image area for redraw
                Invalidate(ImageArea);

                // Recalculate the offset for the viewport rectangle based on user movement.
                // Changes proportionally to dragged mouse movement, but a zoom scale factor
                // is needed to compensate for the image in the viewport appearing larger.
                Offset = new Point(
                    Convert.ToInt32(StartOffset.X - dx / Zoom),
                    Convert.ToInt32(StartOffset.Y - dy / Zoom)
                    );

                // deactivate zoom-to-fit mode
                ZoomToFit = false;

                CapPosition();

                // mark the new onscreen image area for redraw
                Invalidate(ImageArea);

                // Invalidate Minimap area if it is in use
                if (ShowMiniMap)
                {
                    Invalidate(
                        new Rectangle(
                            MiniMapRectangle.X,
                            MiniMapRectangle.Y,
                            MiniMapRectangle.Width + 1,
                            MiniMapRectangle.Height + 1
                        )
                        );
                }
            }
        }

        /// <summary>
        /// Triggered when a mouse button is pressed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // if the user left-clicks on the image
            if (e.Button == MouseButtons.Left)
            {
                MouseIsDown = true;

                // record mouse location
                MouseDownLoc = e.Location;
                // record the offset when clicked down
                StartOffset = Offset;
            }
        }

        /// <summary>
        /// Triggered when a mouse button is released
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                MouseIsDown = false;
            }
        }

        /// <summary>
        /// Triggered when the mouse wheel is scrolled
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // scroll up / zoom in
            if (e.Delta > 0)
            {
                ZoomIn();
            }
            // scroll down / zoom out
            else if (e.Delta < 0)
            {
                ZoomOut();
            }
        }

        #endregion Mouse Event Handlers

    }
}
