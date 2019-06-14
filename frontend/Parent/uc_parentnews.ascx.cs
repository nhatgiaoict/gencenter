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

public partial class control_uc_parentnews : System.Web.UI.UserControl
{
    private Groups sGroup = new Groups();
    private News sNew = null;
    public string tesst = "";
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
                }
            }
            return _groupid;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            sNew = new News();
            string PageQuery = "page";
            string strcPage = Request.QueryString[PageQuery];
            if (strcPage == null)
                strcPage = "1";
            int cPage = Convert.ToInt32(strcPage);
            int TotalRecords, TotalPages, Total;
            DataTable dt = new DataTable();
            dt = sNew.SearchingDeTail(groupid, cPage, Convert.ToInt32(Globals.TotalNew), out TotalRecords, out TotalPages, out Total);
            rptN.DataSource = dt;
            rptN.DataBind();
            Paging paging = new Paging();
            ltlPage.Text = paging.PhanTrang(cPage, Convert.ToInt32(Globals.TotalNew), TotalRecords);
        }
    }
}
