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
            sDuannoibat(sNew.GetDuannoibat(8));
        }
    }
    private void sDuannoibat(DataTable dt)
    {
        string sTemp = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == 0)
            {
                sTemp += "<figure><img alt=\"" + dt.Rows[i]["title"].ToString().Trim() + "\" src=\"" + dt.Rows[i]["fimage"].ToString().Trim() + "\"></figure>";
                sTemp += "<li><a href=\"/" + dt.Rows[i]["shortlink"].ToString().Trim() + ".html\">" + dt.Rows[i]["title"].ToString().Trim() + "</a></li>";
            }
            else
            {
                sTemp += "<li><a href=\"/" + dt.Rows[i]["shortlink"].ToString().Trim() + ".html\">" + dt.Rows[i]["title"].ToString().Trim() + "</a></li>";
            }
        }
        ltlNoidung.Text = sTemp;
    }
    //private string MenuCT(DataTable dt)
    //{
    //    string sTemp = string.Empty;
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        if (i == dt.Rows.Count - 1)
    //        {
    //            sTemp += "<li><h1><a class=\"active\" href=\"" + CMDU.CommanUrl.UrlGroup(dt.Rows[i]["link"].ToString(), dt.Rows[i]["shortlink"].ToString()) + "\">" + dt.Rows[i]["title"].ToString().Trim() + "</a></h1></li>";
    //        }
    //        else
    //        {
    //            sTemp += "<li><a href=\"" + CMDU.CommanUrl.UrlGroup(dt.Rows[i]["link"].ToString(), dt.Rows[i]["shortlink"].ToString()) + "\">" + dt.Rows[i]["title"].ToString().Trim() + "</a></li>";
    //        }
    //    }
    //    return sTemp;
    //}
}
