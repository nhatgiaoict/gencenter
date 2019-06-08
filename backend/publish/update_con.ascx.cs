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
using uss.utils;

public partial class publish_update_con : System.Web.UI.UserControl
{
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    private UssUrl sUrl = null;
    private string id
    {
        get
        {
            sUrl = new UssUrl();
            return sUrl.GetParam("id");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif"; ;
            ltlHeader.Text = Language.GetTextByID(152);

            txtFileImages.Attributes.Add("style", "Width:250px");
            txtName.Attributes.Add("style", "Width:250px");
            ltlViewSize.Text = Language.GetTextByID(230);
            ltlAddImages.Text = Language.GetTextByID(151);
            tbxWidth.Attributes.Add("style", "Width:50px");
            tbxHeight.Attributes.Add("style", "Width:50px");

            ltlRequireTitle.Text = Language.GetTextByID(34);
            BtAdd.Text = Language.GetTextByID(37);

            imgSelect.Attributes.Add("style", "cursor:hand; cursor:pointer");
            imgSelect.Src = Globals.UrlImages + "folder.gif";
            imgSelect.Attributes.Add("onclick", "SelectFile('" + txtFileImages.ClientID + "')");
           
            ImageLogo objImage = new ImageLogo();
            //Update
            if (id == "" || id == null || id == string.Empty)
                Response.Redirect(Globals.URLCurrent + "images_home.aspx", true);
            DataRow dr = objImage.Getdata(id);
            if (dr == null)
                Response.Redirect(Globals.URLCurrent + "images_home.aspx", true);
            
                txtName.Text = dr["name"].ToString();
                txtFileImages.Text = dr["fimage"].ToString();
                tbxWidth.Text = dr["width"].ToString();
                tbxHeight.Text = dr["height"].ToString();
        }

    }
    protected void BtAdd_Click(object sender, EventArgs e)
    {
        string sFileImages = txtFileImages.Text.Trim();
        string sName = txtName.Text.Trim();
        if (sFileImages == "" || sFileImages == string.Empty || sName == "" || sName == string.Empty)
        {
            ltlRequireTitle.Visible = true;
            return;
        }
        else
        {
            ltlRequireTitle.Visible = false;
        }
        string sFilename = txtFileImages.Text.Trim();
        string fwidht = tbxWidth.Text.Trim();
        string fheight = tbxHeight.Text.Trim();
        ImageLogo objImage = new ImageLogo();
        objImage.Update(id ,sName, sFilename,fwidht,fheight);
        Response.Redirect(Globals.URLCurrent + "images_home.aspx", true);
    }
}
