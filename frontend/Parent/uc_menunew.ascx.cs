using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Parent_uc_menunew : System.Web.UI.UserControl
{
    private StringBuilder sb1 = new StringBuilder();
    private Menus menu = new Menus(4);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sKetQua = "";
            if (clsCache.Get("CacheDeQuy3" + Globals.CurrentLang) != null)
            {
                sKetQua = (string)clsCache.Get("CacheDeQuy3" + Globals.CurrentLang);
            }
            else
            {
                DataTable dt = menu.GetMenu();
                sKetQua = BuidMenu(dt);
                clsCache.Max("CacheDeQuy3" + Globals.CurrentLang, (object)sKetQua);
            }
            ltlMenunew.Text = sKetQua;
        }
    }

    private string BuidMenu(DataTable dt)
    {
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["nextchild"].ToString()) > 0)
                {
                    sb1.Append("<li class=\"arrow-down\"><a href=\"javascript:void(0)\"><span class=\"gw-menu-text\">" + dt.Rows[i]["ptitle"].ToString() + "</span> <b class=\"icon-arrow-up8\"></b> </a>");
                    BuilMenuSub(menu.GetChild(dt.Rows[i]["pid"].ToString().Trim()));
                }
                else
                {
                    sb1.Append("<li class=\"init-un-active\"><a href=\"" + CMDU.CommanUrl.UrlGroup(dt.Rows[i]["plink"].ToString(), dt.Rows[i]["shortlink"].ToString()) + "\"><span class=\"gw-menu-text\">" + dt.Rows[i]["ptitle"].ToString() + "</span></a>");
                }
                sb1.Append("</li>");
            }
        }
        return sb1.ToString();
    }

    private string BuilMenuSub(DataTable dt)
    {
        if (dt != null)
        {
            sb1.Append("<ul class=\"gw-submenu\" style=\"display: block;\">");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb1.Append("<li><a href=\"" + CMDU.CommanUrl.UrlGroup(dt.Rows[i]["plink"].ToString(), dt.Rows[i]["shortlink"].ToString()) + "\">" + dt.Rows[i]["ptitle"].ToString() + "</a></li>");
            }
            sb1.Append("</ul>");
        }
        return sb1.ToString();
    }
}
