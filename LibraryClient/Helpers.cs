using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClient
{
    class Helpers
    {
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                return Image.FromStream(ms, true);
            }
            catch
            {
                return null;
            }
        }

        public static Byte[] ImageToBytes(Image img)
        {
            var ms = new MemoryStream();
            img.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
