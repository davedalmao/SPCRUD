using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPCRUD.Classes.Utility
{
    class ImageOperations
    {
        public static byte[] ImageToBytes(Image userImage)//Get bytes of the image
        {
            using (MemoryStream ms = new MemoryStream())
            using (Bitmap tempImage = new Bitmap(userImage))
            {
                /*copy the object (userImage) into a new object (tempImage), 
                  then use that object(tempImage) to "Write" */
                tempImage.Save(ms, userImage.RawFormat); //error here, maybe exif error 
                return ms.ToArray();
            }
        }

        public static Image BytesToImage(byte[] buffer) //Get image from database
        {
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                return Image.FromStream(ms);
            }
        }

        public static RotateFlipType Rotate(Image bmp) //Change Image to Correct Orientation When displaying to PictureBox
        {
            const int OrientationId = 0x0112;
            PropertyItem pi = bmp.PropertyItems.Select(x => x).FirstOrDefault(x => x.Id == OrientationId);

            if (pi == null)
                return RotateFlipType.RotateNoneFlipNone;

            byte o = pi.Value[0];

            //Orientations
            if (o == 2) //TopRight
                return RotateFlipType.RotateNoneFlipX;
            if (o == 3) //BottomRight
                return RotateFlipType.RotateNoneFlipXY;
            if (o == 4) //BottomLeft
                return RotateFlipType.RotateNoneFlipY;
            if (o == 5) //LeftTop
                return RotateFlipType.Rotate90FlipX;
            if (o == 6) //RightTop
                return RotateFlipType.Rotate90FlipNone;
            if (o == 7) //RightBottom
                return RotateFlipType.Rotate90FlipY;
            if (o == 8) //LeftBottom
                return RotateFlipType.Rotate90FlipXY;

            //TopLeft (what the image looks by default) [or] Unknown
            return RotateFlipType.RotateNoneFlipNone;
        }

        public static bool AreImagesEqual(Image img1, Image img2)
        {
            ImageConverter converter = new ImageConverter();
            byte[] bytes1 = (byte[])converter.ConvertTo(img1, typeof(byte[]));
            byte[] bytes2 = (byte[])converter.ConvertTo(img2, typeof(byte[]));
            return Enumerable.SequenceEqual(bytes1, bytes2);
        }

        private ImageFormat CheckFileExtension(string extension)
        {
            //We need to check file extension to aviod: System.ArgumentNullException on some images
            switch (extension.ToLower())
            {
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".emf":
                    return ImageFormat.Emf;
                case ".exif":
                    return ImageFormat.Exif;
                case ".jpg":
                    return ImageFormat.Jpeg;
                case ".memorybmp":
                    return ImageFormat.MemoryBmp;
                case ".png":
                    return ImageFormat.Png;
                case ".tiff":
                    return ImageFormat.Tiff;
                case ".wmf":
                    return ImageFormat.Wmf;
                default:
                    return ImageFormat.Jpeg;
            }
        }
    }
}
