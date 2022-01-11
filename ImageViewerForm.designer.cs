namespace ImageStack
{
    partial class ImageViewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageViewerForm));
            this.ExportPNGBT = new System.Windows.Forms.Button();
            this.ExportJPEGBT = new System.Windows.Forms.Button();
            this.JPEGNUD = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.MainImageIVB = new ImageStack.ImageViewer();
            ((System.ComponentModel.ISupportInitialize)(this.JPEGNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // ExportPNGBT
            // 
            this.ExportPNGBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportPNGBT.Location = new System.Drawing.Point(591, 496);
            this.ExportPNGBT.Name = "ExportPNGBT";
            this.ExportPNGBT.Size = new System.Drawing.Size(99, 47);
            this.ExportPNGBT.TabIndex = 1;
            this.ExportPNGBT.Text = "Export PNG";
            this.ExportPNGBT.UseVisualStyleBackColor = true;
            this.ExportPNGBT.Click += new System.EventHandler(this.ExportPNGBT_Click);
            // 
            // ExportJPEGBT
            // 
            this.ExportJPEGBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportJPEGBT.Location = new System.Drawing.Point(463, 496);
            this.ExportJPEGBT.Name = "ExportJPEGBT";
            this.ExportJPEGBT.Size = new System.Drawing.Size(115, 47);
            this.ExportJPEGBT.TabIndex = 2;
            this.ExportJPEGBT.Text = "Export JPEG";
            this.ExportJPEGBT.UseVisualStyleBackColor = true;
            this.ExportJPEGBT.Click += new System.EventHandler(this.ExportJPEGBT_Click);
            // 
            // JPEGNUD
            // 
            this.JPEGNUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.JPEGNUD.Location = new System.Drawing.Point(383, 512);
            this.JPEGNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.JPEGNUD.Name = "JPEGNUD";
            this.JPEGNUD.Size = new System.Drawing.Size(64, 23);
            this.JPEGNUD.TabIndex = 3;
            this.JPEGNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.JPEGNUD.Value = new decimal(new int[] {
            95,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(279, 512);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "JPEG Quality";
            // 
            // MainImageIVB
            // 
            this.MainImageIVB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainImageIVB.BackColor = System.Drawing.Color.Black;
            this.MainImageIVB.Image = null;
            this.MainImageIVB.Location = new System.Drawing.Point(16, 16);
            this.MainImageIVB.Name = "MainImageIVB";
            this.MainImageIVB.Size = new System.Drawing.Size(671, 464);
            this.MainImageIVB.TabIndex = 0;
            this.MainImageIVB.TabStop = false;
            this.MainImageIVB.Zoom = 1F;
            // 
            // ImageViewerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(703, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.JPEGNUD);
            this.Controls.Add(this.ExportJPEGBT);
            this.Controls.Add(this.ExportPNGBT);
            this.Controls.Add(this.MainImageIVB);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "ImageViewerForm";
            this.Text = "Image Viewer";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.JPEGNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ImageViewer MainImageIVB;
        private System.Windows.Forms.Button ExportPNGBT;
        private System.Windows.Forms.Button ExportJPEGBT;
        private System.Windows.Forms.NumericUpDown JPEGNUD;
        private System.Windows.Forms.Label label1;
    }
}