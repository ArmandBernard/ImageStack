using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace ImageStack
{
    public class ImageProcessing
    {
        /// <summary>
        /// Fix image orientation using Exif Data
        /// </summary>
        /// <param name="image"></param>
        public static void FixOrientation(Image image)
        {
            // get the property id associated with orientation
            PropertyItem pi =
                image.PropertyItems.Select(x => x).FirstOrDefault(x => x.Id == 0x0112);
            // if not found, return
            if (pi == null) return;

            // get the orientation value byte
            byte orientation = pi.Value[0];

            // fix the orientation based on the orientation value found
            switch (orientation)
            {
                case 2:
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case 3:
                    image.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    break;
                case 4:
                    image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    break;
                case 5:
                    image.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 6:
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 7:
                    image.RotateFlip(RotateFlipType.Rotate90FlipY);
                    break;
                case 8:
                    image.RotateFlip(RotateFlipType.Rotate90FlipXY);
                    break;
                default:
                    break;
            }

        }

    }
}
