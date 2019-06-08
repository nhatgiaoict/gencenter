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

public partial class _groups_register : System.Web.UI.Page
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
        if (!Membertask.IsAdministrator() && Membertask.IsChuyenmuc() == string.Empty)
            Response.Redirect(Globals.UrlRoot);
    }
    [System.Web.Services.WebMethod]
    public static bool KiemTraShortlink(string Title)
    {
        Groups group = new Groups();
        bool check = false;
        DataTable dtb = group.CheckShortlinkUrl(Title);
        if (dtb == null)
        {
            check = true;
        }
        else { check = false; }
        return check;
    }
}
