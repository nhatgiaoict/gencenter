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

public partial class temp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["url"] != null)
        {
            if (Request["type"] != null)
            {
                string url = Server.UrlDecode(Request["url"].ToString());
                Response.Redirect(url, true);
            }
            else
            {
                string url = Request["url"].ToString();
                Response.Redirect(url, true);
            }
            
        }
    }
}
