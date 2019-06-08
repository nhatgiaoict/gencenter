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

public partial class advert_ViewAdvert : System.Web.UI.Page
{
    private UssUrl sUrl = null;
    private Advert sAdvert = null;
    private string Advertid
    {
        get { sUrl = new UssUrl(); return sUrl.GetParam("Advertid"); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Advertid == string.Empty || Advertid == null) return;
        sAdvert = new Advert();
        if (!this.IsPostBack)
        {
            DataRow dr = sAdvert.GetInfo(Advertid);
            if (dr != null)
            {
                ltlQuangCao.Text = dr["filename"].ToString().Trim();
            }
        }
    }
}
