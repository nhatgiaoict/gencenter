using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _news_addvanban : System.Web.UI.Page
{
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Membertask.IsAdministrator() && Membertask.IsNewsTask() == string.Empty)
            Response.Redirect(Globals.UrlRoot);
    }
    [System.Web.Services.WebMethod]
    public static bool KiemTraShortlink(string Title)
    {
        bool check = false;
        if (Title.Length > 0 && Title != string.Empty)
        {
            News snew = new News();
            DataTable dtb = snew.CheckShortlinkUrl(Title);
            if (dtb == null)
            {
                check = true;
            }
            else
            {
                check = false;
            }
            //return check;
        }
        else
        {
            check = false;
        }
        return check;
    }
}
