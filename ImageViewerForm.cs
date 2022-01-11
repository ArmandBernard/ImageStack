using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace ImageStack
{
    public partial class ImageViewerForm : Form
    {
        public ImageViewerForm(Image image, Color? backColor = null)
        {
            InitializeComponent();

            if (backColor != null)
            {
                this.BackColor = (Color)backColor;
            }

            MainImageIVB.Image = image;

            // load saved jpeg quality setting
            if (int.TryParse(RegistryWF.GetValue("JPEGQuality"), out int quality))
            {
                JPEGNUD.Value = quality;
            }

        }

        private void Export(ImageFormat format)
        {
            // create a savedialogue object that will ask the user where to save the
            // file and under what type
            SaveFileDialog sfd = new SaveFileDialog()
            {
                InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString(),
                Title = "Export Image to File",
                FileName = 
                    "Stitch_" + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss") + "." + format.ToString(),
                Filter = 
                    format.ToString() + " files|*." + format.ToString() + "|All files|*.*",
                ValidateNames = true
            };

            // show the save dialogue
            DialogResult result = sfd.ShowDialog();

            EncoderParameters parameters = new EncoderParameters();
            parameters.Param = new EncoderParameter[]
            {
                new EncoderParameter(Encoder.Quality, Convert.ToInt64(JPEGNUD.Value))
            };

            // save jpeg quality setting
            RegistryWF.SetValue("JPEGQuality", JPEGNUD.Value);

            // If the file name is not an empty string and the user did not cancel  
            if (!string.IsNullOrEmpty(sfd.FileName) && result == DialogResult.OK)
            {
                try
                {
                    // try to save
                    if (format == ImageFormat.Jpeg)
                    {
                        MainImageIVB.Image.Save(sfd.FileName, GetEncoder(format), parameters);
                    }
                    else
                    {
                        MainImageIVB.Image.Save(sfd.FileName, format);
                    }
                    MessageBox.Show(
                        "Export successful.",
                        "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information
                        );
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);

                    MessageBox.Show(
                        "File save failed. File may be locked for editing by another " +
                        "program or in a directory that requires special access privileges.\nDetails:\n" + 
                        ex.Message,
                        "File Save Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
            }
        }

        /// <summary>
        /// Find an encoder that handles a format
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().Where(x => x.FormatID == format.Guid).First();
        }

        private void ExportPNGBT_Click(object sender, EventArgs e)
        {
            Export(ImageFormat.Png);

        }

        private void ExportJPEGBT_Click(object sender, EventArgs e)
        {
            Export(ImageFormat.Jpeg);
        }
    }
}
