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

public partial class control_style : System.Web.UI.UserControl
{
    private Groups sgroup = null;
    private News sNew = null;
    private WebInfor sWebinfo = null;
    private string sKeyWord = string.Empty;
    private string sDescription = string.Empty;
    private string stitle = string.Empty;
    private string groupid
    {
        get
        {
            string _groupid = string.Empty;
            if (Request.QueryString["shortlink"] != null)
            {
                _groupid = Request.QueryString["shortlink"].ToString();
            }
            return _groupid;
        }
    }
    private string id
    {
        get
        {
            string _id = string.Empty;
            if (Request.QueryString["iddetail"] != null)
            {
                _id = Request.QueryString["iddetail"].ToString();
            }
            return _id;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (id != string.Empty)
            {
                sNew = new News();
                DataRow dr = sNew.GetInfoAllByShortLink(id);
                if (dr != null)
                {
                    ltlTitle.Text = dr["titlemeta"].ToString().Trim();
                    sKeyWord = dr["keywords"].ToString().Trim();
                    sDescription = dr["Description"].ToString().Trim();
                    stitle = dr["title"].ToString().Trim();

                    string savatafb = Globals.UrlHot + dr["fimage"].ToString().Trim();
                    string sTitlefb = Server.HtmlDecode(dr["title"].ToString().Trim());
                    string sDescriptionfb = Server.HtmlDecode(HtmlRemoval.StripTagsRegex(dr["Summary"].ToString()));

                    HtmlMeta objMetaimg = new HtmlMeta();
                    objMetaimg.Attributes.Add("property", "og:image");
                    objMetaimg.Content = savatafb;
                    this.Page.Header.Controls.Add(objMetaimg);

                    HtmlMeta objMetaTitleFb = new HtmlMeta();
                    objMetaTitleFb.Attributes.Add("property", "og:title");
                    objMetaTitleFb.Content = sTitlefb;
                    this.Page.Header.Controls.Add(objMetaTitleFb);

                    HtmlMeta objMetaDesFb = new HtmlMeta();
                    objMetaDesFb.Attributes.Add("property", "og:description");
                    objMetaDesFb.Content = sDescriptionfb;
                    this.Page.Header.Controls.Add(objMetaDesFb);
                }
            }
            else
            {
                if (groupid != string.Empty)
                {
                    sgroup = new Groups();
                    DataRow dr = sgroup.GetInfoByShortLink(groupid);
                    if (dr != null)
                    {
                        ltlTitle.Text = dr["titlemeta"].ToString().Trim();
                        sKeyWord = dr["keywords"].ToString().Trim();
                        sDescription = dr["Description"].ToString().Trim();
                        stitle = dr["titlemeta"].ToString().Trim(); ;
                    }
                }
                else
                {
                    sWebinfo = new WebInfor();
                    DataRow dr = sWebinfo.GetInfo();
                    if (dr != null)
                    {
                        ltlTitle.Text = dr["frontend"].ToString().Trim();
                        sKeyWord = dr["keywords"].ToString().Trim();
                        sDescription = dr["Description"].ToString().Trim();
                        stitle = dr["frontend"].ToString().Trim();
                    }
                }
            }
            HtmlMeta objMeta = new HtmlMeta(); 
            objMeta.Name = "description"; 
            objMeta.Content = sDescription; this.Page.Header.Controls.Add(objMeta);
            objMeta = new HtmlMeta(); objMeta.Name = "keywords"; objMeta.Content = sKeyWord; ; this.Page.Header.Controls.Add(objMeta);
            objMeta = new HtmlMeta(); objMeta.Name = "title"; objMeta.Content = stitle; this.Page.Header.Controls.Add(objMeta);
            
            GetStyle();
        }
    }
    private void GetStyle()
    {
        string Style = "";
        string StyleResource = "";
        ltlStyle.Text = string.Empty;
        Style += "<link href='" + Globals.UrlCss + "style.css' rel='stylesheet' type='text/css' >\n";
        Style += "<link href='" + Globals.UrlCss + "responsive.css' rel='stylesheet' type='text/css' >\n";
        Style += "<link href='" + Globals.UrlCss + "carousel.css' rel='stylesheet' type='text/css' >\n";
        Style += "<link href='" + Globals.UrlCss + "font-awesome.css' rel='stylesheet' type='text/css' >\n";

        Style += "<script language='JavaScript' type='text/JavaScript' src='" + Globals.UrlJavascript + "jquery.min.js'></script> \n";
        Style += "<script language='JavaScript' type='text/JavaScript' src='" + Globals.UrlJavascript + "bootstrap.min.js'></script> \n";
        Style += "<script language='JavaScript' type='text/JavaScript' src='" + Globals.UrlJavascript + "jquery.smartmenus.js'></script> \n";
        Style += "<script language='JavaScript' type='text/JavaScript' src='" + Globals.UrlJavascript + "docs.min.js'></script> \n";
        Style += "<script language='JavaScript' type='text/JavaScript' src='" + Globals.UrlJavascript + "common.js'></script> \n";

        StyleResource += " <link type=\"text/css\" href=\"" + Globals.UrlHot + "/ResourceHandler.ashx?fileSet=CSS&type=text/css&v=" + Globals.FileSetVession + "\" rel=\"Stylesheet\"/>";
        StyleResource += " <script type=\"text/javascript\" src=\"" + Globals.UrlHot + "/ResourceHandler.ashx?fileSet=JS&type=application/x-javascript&v=" + Globals.FileSetVession + "\"></script>";
        if (Globals.UseFileSet == true)
        {
            ltlStyle.Text = StyleResource;
        }
        else
        {
            ltlStyle.Text = Style;
        }
    }
}
