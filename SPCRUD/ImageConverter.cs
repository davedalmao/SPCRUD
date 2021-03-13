using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPCRUD
{
    class ImageConverter
    {
        public static byte[] ConvertImageToByteArray(Image userImage)//Get bytes of the image
        {
            using (MemoryStream ms = new MemoryStream())
            using (Bitmap tempImage = new Bitmap(userImage))
            {
                /*copy the object (userImage) into a new object (tempImage), 
                  then use that object(tempImage) to "Write" */
                tempImage.Save(ms, userImage.RawFormat);
                return ms.ToArray();
            }
        }

        //public static byte[] ConvertImageToByteArray(string path) //Get bytes of the image
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        Image img = Image.FromFile(path);
        //        img.Save(ms, ImageFormat.Png);
        //        return ms.ToArray();
        //    }
        //}

        public static Image ConvertByteArrayToImage(byte[] buffer) //Get image from database
        {
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                return Image.FromStream(ms);
            }
        }

        private ImageFormat CheckFileExtension(string extension)
        {
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
