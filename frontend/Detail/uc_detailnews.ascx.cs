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

public partial class Detail_uc_detailnews : System.Web.UI.UserControl
{
    private News sNew = null;
    private DataTable dtnew = null;
    private DataTable dtOld = null;
    private DataRow dr1 = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["iddetail"] != null)
        {
            sNew = new News();
            string shortlink = Request.QueryString["iddetail"].ToString();
            DataRow dr = sNew.GetInfoAllByShortLink(shortlink.Trim());
            sNew.Detail(dr["groupid"].ToString().Trim(), dr["id"].ToString().Trim(), out dtnew, out dtOld, out dr1);
            if (dr1 != null)
            {
                ltltitle.Text = dr1["title"].ToString().Trim();
                ltlcontent.Text = sNew.GetContent(dr1["id"].ToString().Trim());

                ltldate.Text = DateTime.Parse(dr1["created"].ToString()).ToString("MMM dd, yyyy");
                if (Session["view_" + dr["id"].ToString()] == null)
                {
                    sNew.Inceatncount(dr["id"].ToString().Trim());
                }
            }
            rptNew.DataSource = sNew.GetNewBygroup(dr["groupid"].ToString().Trim(), 4, dr["id"].ToString().Trim());
            rptNew.DataBind();
        }
    }
}
