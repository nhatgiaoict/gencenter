using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using uss.utils;

public partial class publish_DetailBaner : System.Web.UI.Page
{
    private ImageLogo imgbaner = null;
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    private static UssUrl sUrl
    {
        get
        {
            return new UssUrl();
        }
    }
    private string newsid
    {
        get
        {
            string newsid = sUrl.GetParam("newsid");
            if (newsid == null || newsid == "")
                newsid = string.Empty;
            return newsid;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        imgbaner  = new ImageLogo(); 
        if (!this.IsPostBack)
        {
            lbHeaderT.Text = Language.GetTextByID(151);
            if (newsid == "" || newsid == string.Empty || newsid == null)
                Response.Redirect(Globals.URLCurrent + "images_home.aspx");
            DataRow dr = imgbaner.GetInfoStatus(newsid); 
            if(dr!= null)
            {
                lbTenAnh.Text = "  :" + dr["name"].ToString().ToUpper(); 
                string fimages = dr["fimage"].ToString(); 
                string fwidth = dr["width"].ToString(); 
                string fheight = dr["height"].ToString();
                string Sfile = fimages.Substring(fimages.Length - 4);
                string sPath = Globals.UrlRoot + fimages;
                if(Sfile == ".swf")
                {
                    ltlImages.Text = string.Format("<script language='javascript'>\n<!--\n embed_flash('{0}', '" + fwidth + "', '" + fheight + "','aa');\n-->\n</script>", sPath);
                }
                else
                {
                    ltlImages.Text = string.Format("<img src='{0}' border='0'>", sPath);
                }
            }
        }
    }
}
