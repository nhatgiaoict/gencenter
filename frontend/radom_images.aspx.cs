using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;

public partial class radom_images : System.Web.UI.Page
{
    private int width = 70;
    private int height = 23;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //RequestUrl sUrl = new RequestUrl();
        RandomCodes objrad = new RandomCodes();
        if (!this.IsPostBack)
        {
            string checkCode = objrad.CreateRandomCode(4);
            Session["RadomCode"] = checkCode;
            CreateImage(checkCode);
        }
    }    
    private void CreateImage(string checkCode)
    {
        //checkCode.Length * 14
        System.Drawing.Bitmap image = new System.Drawing.Bitmap(Convert.ToInt32(Math.Ceiling((decimal)(width))), height);
        Graphics g = Graphics.FromImage(image);
        try
        {
            g.Clear(Color.AliceBlue);
            Font font = new System.Drawing.Font("Comic Sans MS", 15, System.Drawing.FontStyle.Bold);
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
            SizeF fSize = g.MeasureString(checkCode, font);
            g.DrawString(checkCode, font, brush, (width - Convert.ToInt32(fSize.Width)) / 2, (height - Convert.ToInt32(fSize.Height)) / 2);
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
        }
        finally
        {
            g.Dispose();
            image.Dispose();
        }
    }
    
}
