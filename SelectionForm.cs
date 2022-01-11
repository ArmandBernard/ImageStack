using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ImageStack
{
    public partial class SelectionForm : Form
    {
        public enum Side
        {
            Left = 0,
            Top = 1,
            Right = 2, 
            Bottom = 3
        }

        PictureBox SelectedPicturebox;
        List<PictureBox> DirectionBTList = new List<PictureBox>();

        List<Rectangle> ImgRectangles = new List<Rectangle>();

        public SelectionForm()
        {
            InitializeComponent();

            DirectionBTList.Add(leftPB);
            DirectionBTList.Add(topPB);
            DirectionBTList.Add(rightPB);
            DirectionBTList.Add(downPB);

            SelectedPicturebox = downPB;
            ColourPictureBoxes();

        }

        private void ColourPictureBoxes()
        {
            foreach (PictureBox pb in DirectionBTList)
            {
                pb.BackColor = Control.DefaultBackColor;
            }

            SelectedPicturebox.BackColor = Color.White;
        }

        private Control CreateImageHolder(Image img)
        {
            PictureBox pb = new PictureBox()
            {
                Image = img,
                Size = new Size(100, 100),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            pb.Click += ImageHolder_Click;

            return pb;
        }

        private void CreateRectangles()
        {
            if (ImagesFLP.Controls.Count == 0) { return; }

            ImgRectangles.Clear();

            Side side = IdentifySide();

            for (int i = 0; i < ImagesFLP.Controls.Count; i++)
            {
                Image img = ((PictureBox)ImagesFLP.Controls[i]).Image;

                // add the first rectangle at 0,0
                if (i == 0)
                {
                    ImgRectangles.Add(new Rectangle(0, 0, img.Width, img.Height));
                    continue;
                }

                Rectangle lastRect = ImgRectangles.Last();

                Rectangle rect;
                Point newlocation;
                Size newsize;

                // determine the next image's rectangle location based on the previous ones and 
                // the join side
                switch (side)
                {
                    case Side.Left:
                        newsize = new Size(
                            Convert.ToInt32(img.Width * (double)lastRect.Height / img.Height),
                            lastRect.Height
                            );
                        newlocation = new Point(lastRect.X - newsize.Width, lastRect.Y);
                        rect = new Rectangle(newlocation, newsize);
                        break;
                    case Side.Top:
                        newsize = new Size(
                            lastRect.Width,
                            Convert.ToInt32(img.Height * (double)lastRect.Width / img.Width)
                            );
                        newlocation = new Point(lastRect.X, lastRect.Y - newsize.Height);
                        rect = new Rectangle(newlocation, newsize);
                        break;
                    case Side.Right:
                        newsize = new Size(
                            Convert.ToInt32(img.Width * (double)lastRect.Height / img.Height),
                            lastRect.Height
                            );
                        newlocation = new Point(lastRect.X + lastRect.Width, lastRect.Y);
                        rect = new Rectangle(newlocation, newsize);
                        break;
                    // bottom
                    default:
                        newsize = new Size(
                            lastRect.Width,
                            Convert.ToInt32(img.Height * (double)lastRect.Width / img.Width)
                            );
                        newlocation = new Point(lastRect.X, lastRect.Y + lastRect.Height);
                        rect = new Rectangle(newlocation, newsize);
                        break;
                }

                ImgRectangles.Add(rect);
            }
        }

        private void RenderImages()
        {
            if (ImgRectangles.Count == 0) { return; }

            int minx = ImgRectangles.Min(r => r.X);
            int miny = ImgRectangles.Min(r => r.Y);
            int maxx = ImgRectangles.Max(r => r.X + r.Width);
            int maxy = ImgRectangles.Max(r => r.Y + r.Height);

            using (Bitmap FinalImage = new Bitmap(maxx - minx, maxy - miny))
            {
                Graphics graphics = Graphics.FromImage(FinalImage);

                // translate to offset images being drawn in negative coordinates
                graphics.TranslateTransform(-minx, -miny);

                // draw each image in their respective rectangle
                for (int i = 0; i < ImgRectangles.Count; i++)
                {
                    Image img = ((PictureBox)ImagesFLP.Controls[i]).Image;

                    Rectangle rect = ImgRectangles[i];

                    graphics.DrawImage(img, rect);
                }

                (new ImageViewerForm(FinalImage)).ShowDialog();
            }
            
        }

        /// <summary>
        /// Identify the side to join on from the selected button
        /// </summary>
        /// <returns></returns>
        private Side IdentifySide()
        {
            if (SelectedPicturebox == leftPB)
            {
                return Side.Left;
            }
            else if (SelectedPicturebox == topPB)
            {
                return Side.Top;
            }
            else if (SelectedPicturebox == rightPB)
            {
                return Side.Right;
            }
            else
            {
                return Side.Bottom;
            }
        }

        #region event handlers

        private void ImageHolder_Click(object sender, EventArgs e)
        {
            ImagesFLP.SelectedControl = (Control)sender;

            ImagesFLP.Select();
        }

        private void ImagesFLP_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void ImagesFLP_DragDrop(object sender, DragEventArgs e)
        {
            // get the list of file paths the user had dropped
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                Image image;
                try
                {
                    image = Image.FromFile(file);
                }
                catch
                {
                    continue;
                }
                // fix image orientation
                ImageProcessing.FixOrientation(image);
                // add to control
                ImagesFLP.Controls.Add(CreateImageHolder(image));
            }

        }

        private void MoveRightBT_Click(object sender, EventArgs e)
        {
            ImagesFLP.MoveSelectedForwards();
        }


        private void MoveLeftBT_Click(object sender, EventArgs e)
        {
            ImagesFLP.MoveSelectedBackwards();
        }

        private void CreateBT_Click(object sender, EventArgs e)
        {
            CreateRectangles();

            RenderImages();
        }

        private void DeleteBT_Click(object sender, EventArgs e)
        {
            PictureBox selected = (PictureBox)ImagesFLP.SelectedControl;

            ImagesFLP.RemoveSelected();

            // dispose to clear memory used by control
            selected.Image.Dispose();
            selected.Dispose();
        }

        private void DirectionPB_Click(object sender, EventArgs e)
        {
            SelectedPicturebox = ((PictureBox)sender);

            ColourPictureBoxes();
        }

        #endregion eventhandlers


    }
}
