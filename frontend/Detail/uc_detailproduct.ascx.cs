using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Detail_uc_detailproduct : System.Web.UI.UserControl
{
    private News sNew = null;
    public string IDP = string.Empty;
    private DataTable dtnew = null;
    private DataTable dtOld = null;
    private DataRow dr1 = null;
    public string sBg = "background:url(/images/bg-project.jpg)";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["iddetail"]))
        {
            sNew = new News();
            string shortlink = Request.QueryString["iddetail"].ToString();
            DataRow dr = sNew.GetInfoAllByShortLink(shortlink.Trim());
            sNew.DetailProduct(dr["groupid"].ToString().Trim(), dr["id"].ToString().Trim(), out dtnew, out dtOld, out dr1);
            if (dr != null)
            {
                IDP = dr["id"].ToString().Trim();
                ltltitle.Text = dr["title"].ToString().Trim();
                ltlContent.Text = sNew.GetContent(dr["id"].ToString().Trim());
                ltlSummary.Text = dr["summary"].ToString().Trim();
                ltlData.Text = DateTime.Parse(dr["created"].ToString().Trim()).ToString("dd/MM/yyyy");
                if (dr["fimagekhac"].ToString().Length > 0)
                {
                    ltlImage.Text = BuilImage(dr["fimagekhac"].ToString().Trim());
                }
                //else
                //{
                //    ltlImage.Text = "<img src=\"" + dr["fimage"].ToString().Trim() + "\" alt=" + dr["title"].ToString().Trim() + ">";
                //}
                rptNew.DataSource = dtnew;
                rptNew.DataBind();
                rptSpK.DataSource = dtOld;
                rptSpK.DataBind();
                // Tang dem
                if (Session["view_" + dr["id"].ToString()] == null)
                {
                    sNew.Inceatncount(dr["id"].ToString().Trim());
                }

                sDuannoibat(sNew.GetDuannoibat(10));

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
        ltlNoidungnb.Text = sTemp;
    }

    public string BuilImage(string Fimages)
    {
        string sTemp = string.Empty;
        if (Fimages.Length > 0)
        {
            string[] arrImage = new string[250];
            arrImage = Fimages.Split(',');
            for (int i = 0; i < arrImage.Length - 1; i++)
            {
                sTemp += "<img src=\"" + arrImage[i].ToString() + "\">";
            }
        }
        return sTemp;
    }
}
