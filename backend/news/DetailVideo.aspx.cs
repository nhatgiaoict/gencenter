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
using uss.utils;

public partial class news_DetailVideo : System.Web.UI.Page
{
    private UssUrl sUrl = null;
    private string id
    {
        get
        {
            sUrl = new UssUrl();
            return sUrl.GetParam("id");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            News news = new News();
            DataRow dr = news.GetInfo(id);
            if (dr != null)
            {
                string sFilename = dr["fimage"].ToString();
                string fileFLV = "../" + Globals.UrlRoot + "data/images/flvplayer.swf?autostart=true&repeat=true&file=" + Globals.UrlRoot + "/" + sFilename;
                VTCVN.Attributes.Add("src", fileFLV);
            }
        }
    }
}
