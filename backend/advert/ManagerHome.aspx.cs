using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class advert_ManagerHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Membertask.IsAdministrator() && Membertask.IsAdvert() == string.Empty)
            Response.Redirect(Globals.UrlRoot);
    }
}
