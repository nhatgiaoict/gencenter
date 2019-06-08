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

public partial class product_uc_product : System.Web.UI.UserControl
{
    private Groups sGroup = new Groups();
    private News snew = new News();
    public string sBg = "background:url(/images/bg-project.jpg)";
    private string groupid
    {
        get
        {
            string _groupid = string.Empty;
            if (Request.QueryString["shortlink"] != null)
            {
                string shortlink = Request.QueryString["shortlink"].ToString();
                DataRow dr = sGroup.GetInfoByShortLink(shortlink.Trim());
                if (dr != null)
                {
                    _groupid = dr["id"].ToString().Trim();
                    ltltitleG.Text = dr["title"].ToString().Trim();
                    if (dr["fimages"].ToString().Trim().Length > 0)
                    {
                        sBg = "background:url(/" + dr["fimages"].ToString().Trim() + ");";
                    }
                }
            }
            return _groupid;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (groupid == null) return;
        if (!this.IsPostBack)
        {
            string PageQuery = "page";
            string strcPage = Request.QueryString[PageQuery];
            if (strcPage == null)
                strcPage = "1";
            int cPage = Convert.ToInt32(strcPage);
            int TotalRecords, TotalPages, Total;
            DataTable dt = new DataTable();
            News sNew = new News();
            dt = sNew.SearchingDeTail(groupid, cPage, Convert.ToInt32(Globals.TotalSP), out TotalRecords, out TotalPages, out Total);
            rptProduct.DataSource = dt;
            rptProduct.DataBind();
            if (Total > Convert.ToInt32(Globals.TotalSP))
            {
                Paging paging = new Paging();
                ltlPage.Text = paging.PhanTrang(cPage, Convert.ToInt32(Globals.TotalSP), TotalRecords);
            }
            else
            {
                ulPage.Visible = false;
            }
        }
    }
}
