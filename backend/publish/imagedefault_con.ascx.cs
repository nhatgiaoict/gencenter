using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using uss.utils;
public partial class publish_imagedefault_con : System.Web.UI.UserControl
{
    private static UssUrl sUrl
    {
        get
        {
            return new UssUrl();
        }
    }
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
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
            //
            string PageQuery = "page";
            string strcPage = Request.QueryString[PageQuery];
            if (strcPage == null)
                strcPage = "1";
            int cPage = Convert.ToInt32(strcPage);
            int TotalRecords, TotalPages;

            ImageLogo objImage = new ImageLogo();
            rptNews.DataSource = objImage.SearchingDefault(cPage, 15, out TotalRecords, out TotalPages);
            rptNews.DataBind();

            PageList2.m_pTotalPage = TotalPages;
            PageList2.m_pPageQuery = PageQuery;
            PageList2.m_pCurrentPage = cPage;
            PageList2.m_pPageUrl = Request.RawUrl;
            PageList2.m_pIconPath = Globals.UrlImages;

            ltlTotal1.Text = string.Format(Language.GetTextByID(205), TotalRecords);

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
        objImage.InsertDefault(sName, sFilename, fwidht, fheight);
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }
    protected void rptNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Literal ltlIndexHeader = (Literal)e.Item.FindControl("ltlIndexHeader");
            ltlIndexHeader.Text = Language.GetTextByID(40);
            Literal ltlTitleHeader = (Literal)e.Item.FindControl("ltlTitleHeader");
            ltlTitleHeader.Text = Language.GetTextByID(33);
            Literal ltlEditHeader = (Literal)e.Item.FindControl("ltlEditHeader");
            ltlEditHeader.Text = Language.GetTextByID(42);
            Literal ltlStatusHeader = (Literal)e.Item.FindControl("ltlStatusHeader");
            ltlStatusHeader.Text = Language.GetTextByID(39);
            Literal ltlTitleName = (Literal)e.Item.FindControl("ltlTitleName");
            ltlTitleName.Text = Language.GetTextByID(151);
        }
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems = (HtmlTableRow)e.Item.FindControl("trItems");
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            HyperLink hlName = (HyperLink)e.Item.FindControl("hlName");
            Literal ltlTitle = (Literal)e.Item.FindControl("ltlTitle");
            LinkButton lbtOnline = (LinkButton)e.Item.FindControl("lbtOnline");
            HtmlAnchor hrEdit = (HtmlAnchor)e.Item.FindControl("hrEdit");
            Literal ltlIDNews = (Literal)e.Item.FindControl("ltlIDNews");
            CheckBox checkboxID = (CheckBox)e.Item.FindControl("checkboxID");

            DataRowView dr = (DataRowView)e.Item.DataItem;
            UssUrl sUrl = null;

            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems.Attributes.Add("class", "alter");
                else
                    trItems.Attributes.Add("class", "item");

                ltlID.Text = (e.Item.ItemIndex + 1).ToString();
                ltlIDNews.Text = dr["id"].ToString();
                ltlTitle.Text = dr["fimage"].ToString();
                hlName.Text = dr["name"].ToString();


                sUrl = new UssUrl(Globals.URLCurrent + "DetailDefault.aspx");
                sUrl.SetParam("newsid", dr["id"].ToString());
                hlName.Attributes.Add("style", "cursor:hand; cursor:pointer;");
                hlName.Attributes.Add("onclick", "javascript:ShowPopup('" + sUrl.Url + "','700','400');");


                lbtOnline.Text = (Convert.ToInt32(dr["status"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                lbtOnline.CommandArgument = dr["id"].ToString();

                sUrl = new UssUrl(Globals.URLCurrent + "updatedefault.aspx");
                sUrl.SetParam("id", dr["id"].ToString());
                hrEdit.InnerText = Language.GetTextByID(42);
                hrEdit.HRef = sUrl.Url;
            }
        }

    }
    protected void rptNews_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtOnline = (LinkButton)e.Item.FindControl("lbtOnline");
            lbtOnline.CausesValidation = false;
            lbtOnline.Click += new System.EventHandler(lbtOnline_Click);
        }
    }
    private void lbtOnline_Click(object sender, System.EventArgs e)
    {
        LinkButton lbtOnline = (LinkButton)sender;
        if (lbtOnline.CommandArgument != String.Empty)
        {
            string id = lbtOnline.CommandArgument.ToString();
            ImageLogo objImage = new ImageLogo();
            objImage.SetStatusDefault(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }
    protected void BtDelete_Click(object sender, EventArgs e)
    {
        ImageLogo objImages = new ImageLogo();
        string id = string.Empty;
        foreach (RepeaterItem items in rptNews.Items)
        {
            CheckBox checkboxID = (CheckBox)items.FindControl("checkboxID");
            if (checkboxID.Checked)
            {
                Literal ltlIDNews = (Literal)items.FindControl("ltlIDNews");
                id = ltlIDNews.Text.Trim();
                objImages.DeleteDefault(id);
            }
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }
}
