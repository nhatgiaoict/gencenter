<%@ WebHandler Language="C#" Class="CaptchaHandler" %>
using System;
using System.Web;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

public class CaptchaHandler : IHttpHandler , IRequiresSessionState 
{
    MemoryStream myMemoryStream = new MemoryStream();
    public void ProcessRequest (HttpContext context) 
    {
        string salt =  MakeCaptchaImage();
        context.Session.Add("CAPCHA", salt.ToLower());
        context.Response.ContentType = "image/jpeg";
        context.Response.Cache.SetCacheability(HttpCacheability.Public);
        context.Response.BufferOutput = false;
        context.Response.OutputStream.Write(myMemoryStream.GetBuffer(), 0, myMemoryStream.GetBuffer().Length);
        //context.Response.Write(salt);
    }
    public bool IsReusable {get {return true ;}}
    
    public string MakeCaptchaImage()
    {
        string s = "";
        char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        int index;
        Random r = new Random();
        int lenght = r.Next(4, 5);
        for (int i = 0; i < lenght; i++)
        {
            index = r.Next(chars.Length - 1);
            s += chars[index].ToString();
        }
        int hight = 30;
        int width = 100;
        string fontFamilyName = "Arial";
        //make the bitmap and the associated Graphics object
        Bitmap bm = new Bitmap(width, hight);
        Graphics gr = Graphics.FromImage(bm);
        gr.SmoothingMode = SmoothingMode.HighQuality;

        RectangleF recF = new RectangleF(0, 0, width, hight);
        Brush br;
        br = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
        gr.FillRectangle(br, recF);

        SizeF text_size;
        Font the_font;
        float font_size = 65 + 1;
        do
        {
            font_size -= 1;
            the_font = new Font(fontFamilyName, font_size, FontStyle.Bold, GraphicsUnit.Pixel);
            text_size = gr.MeasureString(s, the_font);
        }
        while ((text_size.Width > width) || (text_size.Height > 40));

        // Center the text.
        StringFormat string_format = new StringFormat();
        string_format.Alignment = StringAlignment.Center;
        string_format.LineAlignment = StringAlignment.Center;

        // Convert the text into a path.
        GraphicsPath graphics_path = new GraphicsPath();
        //graphics_path.AddString(txt, the_font.FontFamily, CInt(Font.Style), the_font.Size, recF, string_format)
        graphics_path.AddString(s, the_font.FontFamily, 1, the_font.Size, recF, string_format);

        // Make random warping parameters.
        Random rnd = new Random();
        PointF[] pts = { new PointF((float)rnd.Next(width) / 4, (float)rnd.Next(hight) / 4), new PointF(width - (float)rnd.Next(width) / 4, (float)rnd.Next(hight) / 4), new PointF((float)rnd.Next(width) / 4, hight - (float)rnd.Next(hight) / 4), new PointF(width - (float)rnd.Next(width) / 4, hight - (float)rnd.Next(hight) / 4) };
        Matrix mat = new Matrix();
        graphics_path.Warp(pts, recF, mat, WarpMode.Perspective, 0);

        // Draw the text.
        br = new HatchBrush(HatchStyle.LargeConfetti, Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)), Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
        gr.FillPath(br, graphics_path);

        // Mess things up a bit.
        int max_dimension = System.Math.Max(width, hight);
        for (int i = 0; i <= (int)width * hight / 30; i++)
        {
            int X = rnd.Next(width);
            int Y = rnd.Next(hight);
            int W = (int)rnd.Next(max_dimension) / 50;
            int H = (int)rnd.Next(max_dimension) / 50;
            gr.FillEllipse(br, X, Y, W, H);
        }
        for (int i = 1; i <= 5; i++)
        {
            int x1 = rnd.Next(width);
            int y1 = rnd.Next(hight);
            int x2 = rnd.Next(width);
            int y2 = rnd.Next(hight);
            gr.DrawLine(Pens.DarkGray, x1, y1, x2, y2);
        }
        for (int i = 1; i <= 5; i++)
        {
            int x1 = rnd.Next(width);
            int y1 = rnd.Next(hight);
            int x2 = rnd.Next(width);
            int y2 = rnd.Next(hight);
            gr.DrawLine(Pens.LightGray, x1, y1, x2, y2);
        }
        bm.Save(myMemoryStream,ImageFormat.Jpeg);
        graphics_path.Dispose();
        br.Dispose();
        the_font.Dispose();
        gr.Dispose();
        return s;
    }
}
