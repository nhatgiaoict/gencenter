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

public partial class Detail_uc_detailkind : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["iddetail"]))
        {
            News sNew = new News();
            string shortlink = Request.QueryString["iddetail"].ToString();
            DataRow dr = sNew.GetInfoAllByShortLink(shortlink.Trim());
            if (dr["kind"].ToString() == "1")
            {
                panelcontel.Controls.Add(Page.LoadControl("~/Detail/uc_detailproduct.ascx"));
            }
            else
            {
                panelcontel.Controls.Add(Page.LoadControl("~/Detail/uc_detailnews.ascx"));
            }
        }
    }
}
