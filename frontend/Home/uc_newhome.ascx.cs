using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_uc_newhome : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!this.IsPostBack)
            {
                Menus smenu = new Menus(3);
                DataTable dtg = smenu.GetMenu();
                if (dtg.Rows.Count > 0)
                {
                    hlinkg.InnerText = dtg.Rows[0]["ptitle"].ToString().Trim();
                    hlinkg.HRef = CMDU.CommanUrl.UrlGroup(dtg.Rows[0]["plink"].ToString().Trim(), dtg.Rows[0]["shortlink"].ToString().Trim());
                    News snew = new News();
                    DataTable dt = snew.GetNewLikeGroup(dtg.Rows[0]["pid"].ToString().Trim(), 4);
                    if (dt.Rows.Count > 0)
                    {
                        string sTemp = string.Empty;
                        string sTemp1 = string.Empty;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i < 2)
                            {
                                sTemp += "<div class=\"box-while-new wow animated fadeIn animated animated\" data-wow-delay=\"0.2s\"> <div class=\"row\">";
                                sTemp += "<div class=\"col-md-6 col-sm-6 col-xs-12\"><figure><img src=\"" + dt.Rows[i]["fimage"].ToString().Trim() + "\" alt=\"" + dt.Rows[i]["title"].ToString().Trim() + "\"/></figure></div>";
                                sTemp += "<div class=\"col-md-6 col-sm-6 col-xs-12 pd-0\"><h5><a href=\"/" + dt.Rows[i]["shortlink"].ToString().Trim() + ".html\">" + dt.Rows[i]["title"].ToString().Trim() + "</a></h5>";
                                sTemp += "<span class=\"date\"><i class=\"fa fa-clock-o\"></i> " + DateTime.Parse(dt.Rows[i]["created"].ToString().Trim()).ToString("dd/MM/yyyy") + "</span>";
                                sTemp += "<p class=\"sapo\">" + dt.Rows[i]["summary"].ToString().Trim() + "</p></div></div></div>";
                            }
                            else
                            {
                                sTemp1 += "<div class=\"box-while-new wow animated fadeIn animated animated\" data-wow-delay=\"0.2s\"> <div class=\"row\">";
                                sTemp1 += "<div class=\"col-md-6 col-sm-6 col-xs-12\"><figure><img src=\"" + dt.Rows[i]["fimage"].ToString().Trim() + "\" alt=\"" + dt.Rows[i]["title"].ToString().Trim() + "\"/></figure></div>";
                                sTemp1 += "<div class=\"col-md-6 col-sm-6 col-xs-12 pd-0\"><h5><a href=\"/" + dt.Rows[i]["shortlink"].ToString().Trim() + ".html\">" + dt.Rows[i]["title"].ToString().Trim() + "</a></h5>";
                                sTemp1 += "<span class=\"date\"><i class=\"fa fa-clock-o\"></i> " + DateTime.Parse(dt.Rows[i]["created"].ToString().Trim()).ToString("dd/MM/yyyy") + "</span>";
                                sTemp1 += "<p class=\"sapo\">" + dt.Rows[i]["summary"].ToString().Trim() + "</p></div></div></div>";

                                //sTemp1 += " <div class=\"intro-news wow animated zoomIn animated animated\" data-wow-delay=\"0.4s\">";
                                //sTemp1 += "<img src=\"" + dt.Rows[i]["fimage"].ToString().Trim() + "\" alt=\"" + dt.Rows[i]["title"].ToString().Trim() + "\">";
                                //sTemp1 += "<h3><a title=\"\" href=\"/" + dt.Rows[i]["shortlink"].ToString().Trim() + ".html\">" + dt.Rows[i]["title"].ToString().Trim() + "</a></h3></div>";
                            }
                        }
                        ltlTin1.Text = sTemp;
                        ltltin2.Text = sTemp1;
                    }
                }                
            }
        }
    }
}
