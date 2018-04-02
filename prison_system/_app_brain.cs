using System;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prison_system
{
    public class _app_brain
    {
        public string _to_string(Image value)
        {
            String szResult = "";
            MemoryStream ms = new MemoryStream();
            value.Save(ms, ImageFormat.Jpeg);
            ms.Flush();
            ms.Position = 0;
            Byte[] buffer = ms.ToArray();
            szResult = Convert.ToBase64String(buffer);
            return szResult;
        }

        public Image _fun(String str)
        {
            try
            {
                System.Drawing.Image img = null;
                MemoryStream mem = new MemoryStream();
                Byte[] buffer = Convert.FromBase64String(str);
                mem.Position = 0;
                mem.Write(buffer, 0, buffer.Count());
                if (mem != null)
                {
                    img = Image.FromStream(mem);
                    img.Save("C:\\wo.png");
                    return img;
                }
                return img;
            }
            catch (Exception f)
            {
                System.Windows.Forms.MessageBox.Show(f.Message);
                return null;
            }
        }

        public Image _to_image(String imageEncodedString)
        {
            try
            {
                System.Drawing.Image img = null;
                MemoryStream mem = new MemoryStream();
                Byte[] buffer = Convert.FromBase64String(imageEncodedString);
                mem.Position = 0;
                mem.Write(buffer,0,buffer.Count());
                if (mem != null)
                {
                    img = Image.FromStream(mem);
                    return img;
                }
                return img;
            }
            catch (Exception f)
            {
                System.Windows.Forms.MessageBox.Show(f.Message);
                return null;
            }
          
          
        }

    }
}
