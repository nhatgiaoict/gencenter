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
    public string sBg = "background:url(/data/data/img_slide/bg-project.jpg)";
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

                rptOld.DataSource = dtOld;
                rptOld.DataBind();
                rptNew.DataSource = dtnew;
                rptNew.DataBind();

                ltldate.Text = DateTime.Parse(dr1["created"].ToString()).ToString("dd/MM/yyyy");
                rptNew.DataSource = dtnew;
                rptNew.DataBind();
      
                if (Session["view_" + dr["id"].ToString()] == null)
                {
                    sNew.Inceatncount(dr["id"].ToString().Trim());
                }

                sDuannoibat(sNew.GetDuannoibat(8));

                DataRow drg = sNew.GetInfoGroup(dr["groupid"].ToString().Trim());
                if (drg != null)
                {
                    ltltitlegroup.Text = drg["title"].ToString().Trim();
                    if (drg["fimages"].ToString().Trim().Length > 0)
                    {
                        sBg = "background:url(/" + drg["fimages"].ToString().Trim() + ");";
                    }
                }
            }
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
    //            sTemp += "<li><a class=\"active\" href=\"" + CMDU.CommanUrl.UrlGroup(dt.Rows[i]["link"].ToString(), dt.Rows[i]["shortlink"].ToString()) + "\">" + dt.Rows[i]["title"].ToString().Trim() + "</a></li>";
    //        }
    //        else
    //        {
    //            sTemp += "<li><a href=\"" + CMDU.CommanUrl.UrlGroup(dt.Rows[i]["link"].ToString(), dt.Rows[i]["shortlink"].ToString()) + "\">" + dt.Rows[i]["title"].ToString().Trim() + "</a></li>";
    //        }
    //    }
    //    return sTemp;
    //}
}
