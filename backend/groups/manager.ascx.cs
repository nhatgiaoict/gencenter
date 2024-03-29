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

public partial class groups_manager : System.Web.UI.UserControl
{
    private UssUrl sUrl = null;
    private Groups group = null;
    private string ParentID
    {
        get
        {
            sUrl = new UssUrl();
            return sUrl.GetParam("ParentID");
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
            ltlHeader.Text = Language.GetTextByID(93);
            if (ParentID == null || ParentID == string.Empty || ParentID == "")
            {
                hlNavigator.Visible = false;
            }
            else
            {
                sUrl = new UssUrl(Request.CurrentExecutionFilePath);
                hlNavigator.Text = Language.GetTextByID(93).ToUpper();
                hlNavigator.NavigateUrl = sUrl.Url;
            }
            sUrl = new UssUrl();
            string status = sUrl.GetParam("Status");
            if (status == "" || status == string.Empty || status == null)
                status = "2";

            UssUrl sUrl1 = new UssUrl(Request.CurrentExecutionFilePath);
            sUrl1.SetParam("ParentID", ParentID);

            string PageQuery = "page";
            string strcPage = Request.QueryString[PageQuery];
            if (strcPage == null)
                strcPage = "1";
            int cPage = Convert.ToInt32(strcPage);
            int TotalRecords, TotalPages;

            group = new Groups();
            rptGroup.DataSource = group.Searching(ParentID, Convert.ToInt32(status), cPage,25, out TotalRecords, out TotalPages);
            rptGroup.DataBind();

            PageList.m_pTotalPage = TotalPages;
            PageList.m_pPageQuery = PageQuery;
            PageList.m_pCurrentPage = cPage;
            PageList.m_pPageUrl = sUrl1.Url;
            PageList.m_pIconPath = Globals.UrlImages;

            if (TotalRecords == 0)
            {
                ltlTotal.Visible = false;
            }
            else
            {
                ltlTotal.Text = "";//string.Format(Language.GetTextByID(240), TotalRecords);

            }
            BtDelete.Text = Language.GetTextByID(43);
            BtSTT.Text = Language.GetTextByID(48);
            
        }

    }
    protected void BtDelete_Click(object sender, EventArgs e)
    {
        group = new Groups();
        string id = string.Empty;
        foreach (RepeaterItem items in rptGroup.Items)
        {
            CheckBox checkboxID = (CheckBox)items.FindControl("checkboxID");
            if (checkboxID.Checked)
            {
                Literal ltlIDChuyenmuc = (Literal)items.FindControl("ltlIDChuyenmuc");
                id = ltlIDChuyenmuc.Text.Trim();
                group.Delete(id);
            }
        }
        try
        {
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
        catch (Exception ae)
        {
            string ad = ae.ToString();
        }

    }
    protected void rptGroup_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtOnline = (LinkButton)e.Item.FindControl("lbtOnline");
            lbtOnline.CausesValidation = false;
            lbtOnline.Click += new System.EventHandler(lbtOnline_Click);

            LinkButton lbtHome = (LinkButton)e.Item.FindControl("lbtHome");
            lbtHome.CausesValidation = false;
            lbtHome.Click += new System.EventHandler(lbtHome_Click);

            LinkButton lbtInquiry = (LinkButton)e.Item.FindControl("lbtInquiry");
            lbtInquiry.CausesValidation = false;
            lbtInquiry.Click += new System.EventHandler(lbtInquiry_Click);

         }

    }
    private string ConvertKind(int ikind)
    {
        if (ikind == 1)
        {
            return "Sản phẩm";
        }
        else
        {
            return "Tin tức";
        }
    }
    protected void rptGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Literal ltlIndexHeader = (Literal)e.Item.FindControl("ltlIndexHeader");
            ltlIndexHeader.Text = Language.GetTextByID(40);

            Literal ltlTitleHeader = (Literal)e.Item.FindControl("ltlTitleHeader");
            ltlTitleHeader.Text = Language.GetTextByID(29);

            Literal ltlStatushome = (Literal)e.Item.FindControl("ltlStatushome");
            ltlStatushome.Text = Language.GetTextByID(96);

            Literal ltlStatusHeader = (Literal)e.Item.FindControl("ltlStatusHeader");
            ltlStatusHeader.Text = Language.GetTextByID(39);

            Literal ltlInquiryHeader = (Literal)e.Item.FindControl("ltlInquiryHeader");
            ltlInquiryHeader.Text = Language.GetTextByID(142);

            //Literal ltlKind = (Literal)e.Item.FindControl("ltlKind");
            //ltlKind.Text = Language.GetTextByID(415);
            
            Literal ltlEditHeader = (Literal)e.Item.FindControl("ltlEditHeader");
            ltlEditHeader.Text = Language.GetTextByID(42);

            Literal ltlDeleteHeader = (Literal)e.Item.FindControl("ltlDeleteHeader");
            ltlDeleteHeader.Text = Language.GetTextByID(43);

        }
        group = new Groups();
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems = (HtmlTableRow)e.Item.FindControl("trItems");
            
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            TextBox ltlID_Index = (TextBox)e.Item.FindControl("ltlID_Index");
            Literal ltlKind = (Literal)e.Item.FindControl("ltlKind");
            TextBox txtIdx = (TextBox)e.Item.FindControl("txtIdx");
            HyperLink hlTitle = (HyperLink)e.Item.FindControl("hlTitle");
            LinkButton lbtOnline = (LinkButton)e.Item.FindControl("lbtOnline");
            LinkButton lbtHome = (LinkButton)e.Item.FindControl("lbtHome");
            LinkButton lbtInquiry = (LinkButton)e.Item.FindControl("lbtInquiry");
            //LinkButton lbtKind = (LinkButton)e.Item.FindControl("lbtKind");

            Literal ltlIDChuyenmuc = (Literal)e.Item.FindControl("ltlIDChuyenmuc");
            CheckBox checkboxID = (CheckBox)e.Item.FindControl("checkboxID");            
            HtmlAnchor hrEdit = (HtmlAnchor)e.Item.FindControl("hrEdit");
            HtmlAnchor hlinkthuoctinh = (HtmlAnchor)e.Item.FindControl("hlinkthuoctinh");
            DataRowView dr = (DataRowView)e.Item.DataItem;
            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems.Attributes.Add("class", "alter");
                else
                    trItems.Attributes.Add("class", "item");

                string sid = dr["id"].ToString();
                ltlID.Text = sid;
                ltlIDChuyenmuc.Text = sid;
                ltlID_Index.Attributes.Add("style", "width:30px");
                ltlID_Index.Text = (e.Item.ItemIndex + 1).ToString();	//STT
                hlTitle.Text = dr["title"].ToString();

                if (group.NextChild(sid))
                {
                    sUrl = new UssUrl();
                    sUrl.SetParam("ParentID", sid);
                    hlTitle.NavigateUrl = sUrl.Url;
                }

                if (dr["kind"].ToString() == "1")
                {
                    hlinkthuoctinh.InnerText = "Thuộc tính";
                    sUrl = new UssUrl(Globals.URLCurrent + "thuoctinhgroup.aspx");
                    sUrl.SetParam("id", sid);
                    hlinkthuoctinh.HRef = sUrl.Url;
                }
                lbtOnline.Text = (Convert.ToInt32(dr["status"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                lbtOnline.CommandArgument = dr["id"].ToString();

                lbtHome.Text = (Convert.ToInt32(dr["home"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                lbtHome.CommandArgument = dr["id"].ToString();

                lbtInquiry.Text = (Convert.ToInt32(dr["inquiry"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(21) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(22) + "</font>";
                lbtInquiry.CommandArgument = dr["id"].ToString();

                ltlKind.Text = ConvertKind(Convert.ToInt32(dr["kind"].ToString()));
                
                sUrl = new UssUrl(Globals.URLCurrent + "update.aspx");
                sUrl.SetParam("id", sid);
                hrEdit.InnerText = Language.GetTextByID(42);
                hrEdit.HRef = sUrl.Url;
            }
        }
    }
    private void lbtOnline_Click(object sender , System.EventArgs e)
	{
		LinkButton lbtOnline = (LinkButton)sender;
		if(lbtOnline.CommandArgument != String.Empty)
		{	
			string id = lbtOnline.CommandArgument.ToString();
			group = new Groups();
			group.SetStatus(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
		}
	}
	private void lbtHome_Click(object sender , System.EventArgs e)
	{
		LinkButton lbtHome = (LinkButton)sender;
		if(lbtHome.CommandArgument != String.Empty)
		{	
			string id = lbtHome.CommandArgument.ToString();
			group = new Groups();
			group.SetStatusHome(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
		}
	}
    private void lbtInquiry_Click(object sender, System.EventArgs e)
    {
        LinkButton lbtOnline = (LinkButton)sender;
        if (lbtOnline.CommandArgument != String.Empty)
        {
            string id = lbtOnline.CommandArgument.ToString();
            group = new Groups();
            group.SetInquiry(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }

    protected void BtSTT_Click(object sender, EventArgs e)
    {
        group = new Groups();
        foreach (RepeaterItem rptItem in rptGroup.Items)
        {
            Literal ltlID = (Literal)rptItem.FindControl("ltlID");
            TextBox ltlID_Index = (TextBox)rptItem.FindControl("ltlID_Index");
            string id = ltlID.Text.ToString().Trim();
            group.UpdateIndex(id, Convert.ToInt32(ltlID_Index.Text.ToString()));
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);

    }
}
