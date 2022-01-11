namespace ImageStack
{
    partial class SelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CreateBT = new System.Windows.Forms.Button();
            this.MoveLeftBT = new System.Windows.Forms.Button();
            this.MoveRightBT = new System.Windows.Forms.Button();
            this.DeleteBT = new System.Windows.Forms.Button();
            this.ImagesFLP = new ImageStack.SelectectableFLP();
            this.topPB = new System.Windows.Forms.PictureBox();
            this.downPB = new System.Windows.Forms.PictureBox();
            this.leftPB = new System.Windows.Forms.PictureBox();
            this.rightPB = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.topPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.downPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPB)).BeginInit();
            this.SuspendLayout();
            // 
            // CreateBT
            // 
            this.CreateBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateBT.Location = new System.Drawing.Point(599, 387);
            this.CreateBT.Margin = new System.Windows.Forms.Padding(4);
            this.CreateBT.Name = "CreateBT";
            this.CreateBT.Size = new System.Drawing.Size(96, 56);
            this.CreateBT.TabIndex = 1;
            this.CreateBT.Text = "Create";
            this.CreateBT.UseVisualStyleBackColor = true;
            this.CreateBT.Click += new System.EventHandler(this.CreateBT_Click);
            // 
            // MoveLeftBT
            // 
            this.MoveLeftBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveLeftBT.Location = new System.Drawing.Point(375, 387);
            this.MoveLeftBT.Margin = new System.Windows.Forms.Padding(4);
            this.MoveLeftBT.Name = "MoveLeftBT";
            this.MoveLeftBT.Size = new System.Drawing.Size(96, 56);
            this.MoveLeftBT.TabIndex = 2;
            this.MoveLeftBT.Text = "Move Left";
            this.MoveLeftBT.UseVisualStyleBackColor = true;
            this.MoveLeftBT.Click += new System.EventHandler(this.MoveLeftBT_Click);
            // 
            // MoveRightBT
            // 
            this.MoveRightBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveRightBT.Location = new System.Drawing.Point(487, 387);
            this.MoveRightBT.Margin = new System.Windows.Forms.Padding(4);
            this.MoveRightBT.Name = "MoveRightBT";
            this.MoveRightBT.Size = new System.Drawing.Size(96, 56);
            this.MoveRightBT.TabIndex = 3;
            this.MoveRightBT.Text = "Move Right";
            this.MoveRightBT.UseVisualStyleBackColor = true;
            this.MoveRightBT.Click += new System.EventHandler(this.MoveRightBT_Click);
            // 
            // DeleteBT
            // 
            this.DeleteBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteBT.Location = new System.Drawing.Point(263, 387);
            this.DeleteBT.Margin = new System.Windows.Forms.Padding(4);
            this.DeleteBT.Name = "DeleteBT";
            this.DeleteBT.Size = new System.Drawing.Size(96, 56);
            this.DeleteBT.TabIndex = 4;
            this.DeleteBT.Text = "Delete";
            this.DeleteBT.UseVisualStyleBackColor = true;
            this.DeleteBT.Click += new System.EventHandler(this.DeleteBT_Click);
            // 
            // ImagesFLP
            // 
            this.ImagesFLP.AllowDrop = true;
            this.ImagesFLP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagesFLP.AutoScroll = true;
            this.ImagesFLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImagesFLP.Location = new System.Drawing.Point(16, 56);
            this.ImagesFLP.Margin = new System.Windows.Forms.Padding(4);
            this.ImagesFLP.Name = "ImagesFLP";
            this.ImagesFLP.SelectedControl = null;
            this.ImagesFLP.Size = new System.Drawing.Size(679, 315);
            this.ImagesFLP.TabIndex = 0;
            this.ImagesFLP.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImagesFLP_DragDrop);
            this.ImagesFLP.DragEnter += new System.Windows.Forms.DragEventHandler(this.ImagesFLP_DragEnter);
            // 
            // topPB
            // 
            this.topPB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.topPB.Image = global::ImageStack.Properties.Resources.up_32px;
            this.topPB.Location = new System.Drawing.Point(192, 379);
            this.topPB.Name = "topPB";
            this.topPB.Size = new System.Drawing.Size(24, 24);
            this.topPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.topPB.TabIndex = 5;
            this.topPB.TabStop = false;
            this.topPB.Click += new System.EventHandler(this.DirectionPB_Click);
            // 
            // downPB
            // 
            this.downPB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.downPB.Image = global::ImageStack.Properties.Resources.down_32px;
            this.downPB.Location = new System.Drawing.Point(192, 427);
            this.downPB.Name = "downPB";
            this.downPB.Size = new System.Drawing.Size(24, 24);
            this.downPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.downPB.TabIndex = 6;
            this.downPB.TabStop = false;
            this.downPB.Click += new System.EventHandler(this.DirectionPB_Click);
            // 
            // leftPB
            // 
            this.leftPB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.leftPB.Image = global::ImageStack.Properties.Resources.left_32px;
            this.leftPB.Location = new System.Drawing.Point(168, 403);
            this.leftPB.Name = "leftPB";
            this.leftPB.Size = new System.Drawing.Size(24, 24);
            this.leftPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.leftPB.TabIndex = 7;
            this.leftPB.TabStop = false;
            this.leftPB.Click += new System.EventHandler(this.DirectionPB_Click);
            // 
            // rightPB
            // 
            this.rightPB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rightPB.Image = global::ImageStack.Properties.Resources.right_32px;
            this.rightPB.Location = new System.Drawing.Point(216, 403);
            this.rightPB.Name = "rightPB";
            this.rightPB.Size = new System.Drawing.Size(24, 24);
            this.rightPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.rightPB.TabIndex = 8;
            this.rightPB.TabStop = false;
            this.rightPB.Click += new System.EventHandler(this.DirectionPB_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 403);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Change Stack Direction";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Drag images into the list below to start";
            // 
            // SelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 461);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rightPB);
            this.Controls.Add(this.leftPB);
            this.Controls.Add(this.downPB);
            this.Controls.Add(this.topPB);
            this.Controls.Add(this.DeleteBT);
            this.Controls.Add(this.MoveRightBT);
            this.Controls.Add(this.MoveLeftBT);
            this.Controls.Add(this.CreateBT);
            this.Controls.Add(this.ImagesFLP);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(728, 500);
            this.Name = "SelectionForm";
            this.Text = "ImageStack";
            ((System.ComponentModel.ISupportInitialize)(this.topPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.downPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ImageStack.SelectectableFLP ImagesFLP;
        private System.Windows.Forms.Button CreateBT;
        private System.Windows.Forms.Button MoveLeftBT;
        private System.Windows.Forms.Button MoveRightBT;
        private System.Windows.Forms.Button DeleteBT;
        private System.Windows.Forms.PictureBox topPB;
        private System.Windows.Forms.PictureBox downPB;
        private System.Windows.Forms.PictureBox leftPB;
        private System.Windows.Forms.PictureBox rightPB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

