using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageStack
{
    /// <summary>
    /// A Flowlayoutpanel with selectable components that can reordered with a command
    /// </summary>
    public class SelectectableFLP : FlowLayoutPanel
    {
        private Control _SelectedControl = null;

        public Color SelectColor = Color.Blue;

        public int SelectThickness = 1;

        public Control SelectedControl 
        {
            get => _SelectedControl;
            set 
            {
                _SelectedControl = value; 
                Invalidate();        
            }
        }

        public SelectectableFLP()
        {
            // set rendering parameters
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void RemoveSelected()
        {
            if (SelectedControl == null) { return; }

            // get index of selected control first
            int index = Controls.GetChildIndex(SelectedControl);

            // remove selected control
            Controls.Remove(SelectedControl);

            // if there are any controls left, select the one in the same slot as the deleted one
            if (Controls.Count > 0)
            {
                SelectedControl = Controls[Math.Min(index, Controls.Count -1)];
            }
        }

        #region moving items

        public void MoveSelectedForwards()
        {
            int currentindex = Controls.GetChildIndex(SelectedControl);

            MoveForwards(currentindex);
        }

        /// <summary>
        /// Move item with the given index back in the list, i.e. decrease it's position index
        /// </summary>
        public void MoveForwards(int index)
        {
            // if last item already, return
            if (index == Controls.Count - 1) { return; }

            // if not, move one backwards
            Controls.SetChildIndex(SelectedControl, index + 1);

            Invalidate();
        }

        /// <summary>
        /// Move selected item back in the list, i.e. decrease it's position index
        /// </summary>
        public void MoveSelectedBackwards()
        {
            int currentindex = Controls.GetChildIndex(SelectedControl);

            MoveBackwards(currentindex);
        }

        /// <summary>
        /// Move item with the given index back in the list, i.e. decrease it's position index
        /// </summary>
        public void MoveBackwards(int index)
        {
            // if first item already, return
            if (index == 0) { return; }

            // if not, move one backwards
            Controls.SetChildIndex(SelectedControl, index - 1);

            Invalidate();
        }

        #endregion

        #region Event Handlers

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (SelectedControl != null && Controls.Contains(SelectedControl))
            {
                Rectangle highlight = new Rectangle()
                {
                    X = SelectedControl.Bounds.X - SelectThickness,
                    Y = SelectedControl.Bounds.Y - SelectThickness,
                    Width = SelectedControl.Width + 2 * SelectThickness,
                    Height = SelectedControl.Height + 2 * SelectThickness,
                };

                e.Graphics.DrawRectangle(new Pen(SelectColor), highlight);
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            Invalidate();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            Select();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // if the user pressed the delete key
            if (keyData == Keys.Delete)
            {
                // remove the selected item
                RemoveSelected();
                // return true as keypress handled
                return true;
            }

            // unhgandled, so pass on
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion Event Handlers


    }

}
