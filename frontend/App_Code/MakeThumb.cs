using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

    public class MakeThumb
    {
        public MakeThumb(HttpContext context, string path, int thumbW, int thumbH)
        {
            Image img = Image.FromFile(path);
            this.ThumbImage(context, img, thumbW, thumbH);
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
        }

        public MakeThumb(HttpContext context, byte[] bits, int thumbW, int thumbH)
        {
            MemoryStream memoryfile = new MemoryStream(bits);
            Image img = Image.FromStream(memoryfile);
            this.ThumbImage(context, img, thumbW, thumbH);
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
        }
        private void ThumbImage(HttpContext context, Image img, int thumbW, int thumbH)
        {
            int width;
            int height;
            Image orginalimg = img;
            int orginalW = orginalimg.Width;
            int orginalH = orginalimg.Height;
            if (orginalW > orginalH)
            {
                if (orginalW > thumbW)
                {
                    width = thumbW;
                    height = Convert.ToInt32((int)((orginalH * thumbW) / orginalW));
                }
                else
                {
                    width = orginalW;
                    height = orginalH;
                }
            }
            else if (orginalH > thumbH)
            {
                height = thumbH;
                width = Convert.ToInt32((int)((orginalW * thumbH) / orginalH));
            }
            else
            {
                width = orginalW;
                height = orginalH;
            }
            IntPtr inp = new IntPtr();
            Image thumb = orginalimg.GetThumbnailImage(width, height, null, inp);
            context.Response.ContentType = "image/gif";
            thumb.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            orginalimg.Dispose();
            thumb.Dispose();
        }
    }

