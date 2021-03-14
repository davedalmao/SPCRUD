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

namespace SPCRUD
{
    class ImageConverter
    {
        public static byte[] ImageToBytes(Image userImage)//Get bytes of the image
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

        public static byte[] ImageToBytes(string path) //Get bytes of the image
        {
            using (var ms = new MemoryStream())
            {
                Image img = Image.FromFile(path);
                img.Save(ms, ImageFormat.Png);
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
    }
}
