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

public partial class controls_style : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ltlStyle.Text = string.Empty;
        ltlStyle.Text += "<link href='" + Globals.UrlCss + "backend.css' rel='stylesheet' type='text/css' >\n";
        ltlStyle.Text += "<link href='" + Globals.UrlCss + "tree.css' rel='stylesheet' type='text/css' >\n";
        ltlStyle.Text += "<script language='JavaScript' type='text/JavaScript' src='" + Globals.UrlJavascript + "common.js'></script> \n";
        ltlStyle.Text += "<script language='JavaScript' type='text/JavaScript' src='" + Globals.UrlJavascript + "common_new.js'></script> \n";
        ltlStyle.Text += "<script language='JavaScript' type='text/JavaScript' src='" + Globals.UrlJavascript + "common_images.js'></script> \n";

        //ltlStyle.Text += "<script language='JavaScript' type='text/JavaScript' src='" + Globals.UrlJavascript + "tree.js'></script> \n";
        //ltlStyle.Text += "<script language='JavaScript' type='text/JavaScript' src='" + Globals.UrlJavascript + "tree_select.js'></script> \n";
        //ltlStyle.Text += "<script language='JavaScript' type='text/JavaScript' src='" + Globals.UrlJavascript + "xmlextras.js'></script> \n";

        //ltlStyle.Text += "<link href='" + Globals.UrlImages + "icon_VIGLACERA.ico' rel='shortcut icon'>";

        //======
        WebInfor objweb = new WebInfor();
        string id = "1";
        DataRow dr = objweb.Getdata(id);
        ltlTitle.Text = dr["backend"].ToString().Trim();
    }
}
