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

public partial class advert_manager_con : System.Web.UI.UserControl
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
        if (!this.IsPostBack)
        {
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif"; ;
            ltlHeader.Text = Language.GetTextByID(131);

            string PageQuery = "page";
            string strcPage = Request.QueryString[PageQuery];
            if (strcPage == null)
                strcPage = "1";
            int cPage = Convert.ToInt32(strcPage);
            int TotalRecords, TotalPages;
            Advert adv = new Advert();
            rptAdvert.DataSource = adv.Searching(2, cPage, 10, out TotalRecords, out TotalPages);
            rptAdvert.DataBind();

            PageList1.m_pTotalPage = TotalPages;
            PageList1.m_pPageQuery = PageQuery;
            PageList1.m_pCurrentPage = cPage;
            PageList1.m_pPageUrl = Request.RawUrl;
            PageList1.m_pIconPath = Globals.UrlImages;

            PageList2.m_pTotalPage = TotalPages;
            PageList2.m_pPageQuery = PageQuery;
            PageList2.m_pCurrentPage = cPage;
            PageList2.m_pPageUrl = Request.RawUrl;
            PageList2.m_pIconPath = Globals.UrlImages;
            if (TotalRecords == 0)
            {
                ltlTotal.Visible = false;
                ltlTotal1.Visible = false;

            }
            else
            {
                ltlTotal.Text = ltlTotal1.Text = string.Format(Language.GetTextByID(205), TotalRecords);

            }
        }

    }
    protected void rptAdvert_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtOnline = (LinkButton)e.Item.FindControl("lbtOnline");
            lbtOnline.CausesValidation = false;
            lbtOnline.Click += new System.EventHandler(lbtOnline_Click);

            LinkButton lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
            lbtDelete.Attributes.Add("onclick", "javascript:{ return ConfirmDelete('" + Language.GetTextByID(44) + "'); }");
            lbtDelete.CausesValidation = false;
            lbtDelete.Click += new System.EventHandler(lbtDelete_Click);
        }
    }

    protected void rptAdvert_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Literal ltlIndexHeader = (Literal)e.Item.FindControl("ltlIndexHeader");
            ltlIndexHeader.Text = Language.GetTextByID(40);
            Literal ltlTitleHeader = (Literal)e.Item.FindControl("ltlTitleHeader");
            ltlTitleHeader.Text = Language.GetTextByID(29);

            Literal ltlLogoHeader = (Literal)e.Item.FindControl("ltlLogoHeader");
            ltlLogoHeader.Text = Language.GetTextByID(33);
            Literal ltlLastUpdateHeader = (Literal)e.Item.FindControl("ltlLastUpdateHeader");
            ltlLastUpdateHeader.Text = Language.GetTextByID(41);

            Literal ltlClickCountHeader = (Literal)e.Item.FindControl("ltlClickCountHeader");
            ltlClickCountHeader.Text = Language.GetTextByID(146);

            Literal ltlStatusHeader = (Literal)e.Item.FindControl("ltlStatusHeader");
            ltlStatusHeader.Text = Language.GetTextByID(39);
            Literal ltlEditHeader = (Literal)e.Item.FindControl("ltlEditHeader");
            ltlEditHeader.Text = Language.GetTextByID(42);
            Literal ltlDeleteHeader = (Literal)e.Item.FindControl("ltlDeleteHeader");
            ltlDeleteHeader.Text = Language.GetTextByID(43);
        }
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems = (HtmlTableRow)e.Item.FindControl("trItems");
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            HtmlAnchor Hlinktitle = (HtmlAnchor)e.Item.FindControl("Hlinktitle");
            Literal ltlLogo = (Literal)e.Item.FindControl("ltlLogo");
            Literal ltlCreated = (Literal)e.Item.FindControl("ltlCreated");
            Literal ltlClickCount = (Literal)e.Item.FindControl("ltlClickCount");
            LinkButton lbtOnline = (LinkButton)e.Item.FindControl("lbtOnline");
            HtmlAnchor hrEdit = (HtmlAnchor)e.Item.FindControl("hrEdit");
            LinkButton lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
            HtmlImage imgDelete = (HtmlImage)e.Item.FindControl("imgDelete");
            string sFile = string.Empty;
            string sPath = string.Empty;
            string fExt = string.Empty;
            DataRowView dr = (DataRowView)e.Item.DataItem;
            UssUrl sUrlCT = null;
            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems.Attributes.Add("class", "alter");
                else
                    trItems.Attributes.Add("class", "item");
                ltlID.Text = (e.Item.ItemIndex + 1).ToString();
                Hlinktitle.InnerText = dr["title"].ToString();
                if (Convert.ToInt32(dr["kind"].ToString()) == 1)
                {
                    sUrlCT = new UssUrl(Globals.URLCurrent + "ViewAdvert.aspx");
                    sUrlCT.SetParam("Advertid", dr["id"].ToString());
                    Hlinktitle.Attributes.Add("style", "cursor:hand;cursor:pointer");
                    Hlinktitle.Attributes.Add("onclick", "javascript:ShowPopup('" + sUrlCT.Url + "','400','400');");
                }
                else
                {
                    string sfimage = dr["filename"].ToString();
                    string SFile = string.Empty;
                    if (sfimage.Length > 0)
                    {
                        SFile = sfimage.Substring(sfimage.Length - 4);
                        string s = Globals.UrlRoot + dr["filename"].ToString();
                        if (SFile == ".swf")
                        {
                            ltlLogo.Text = string.Format("<script language='javascript'>\n<!--\n embed_flash('{0}', '185', '','left');\n-->\n</script>", s);
                        }
                        else
                        {
                            ltlLogo.Text = string.Format("<img src='{0}' border='0' width='185' >", s);
                        }
                    }
                    else
                    {
                        ltlLogo.Text = string.Empty;
                    }
                }
                ltlCreated.Text = Convert.ToDateTime(dr["created"]).ToString("dd/MM/yyyy");
                ltlClickCount.Text = dr["nclick"].ToString();
                lbtOnline.Text = (Convert.ToInt32(dr["status"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                lbtOnline.CommandArgument = dr["id"].ToString();

                UssUrl sUrl = new UssUrl(Globals.URLCurrent + "update.aspx");
                sUrl.SetParam("id", dr["id"].ToString());

                hrEdit.InnerText = Language.GetTextByID(42);
                hrEdit.HRef = sUrl.Url;
                imgDelete.Src = Globals.UrlImages + "delete.gif";
                lbtDelete.CommandArgument = dr["id"].ToString();
                imgDelete.Visible = false;
                if (Convert.ToInt32(dr["status"].ToString()) == 0)
                {
                    //hrEdit.Visible = true;
                    imgDelete.Visible = true;
                }
            }
        }
    }
        private void lbtDelete_Click(object sender , System.EventArgs e)
		{
			LinkButton lbtDelete = (LinkButton)sender;
			if(lbtDelete.CommandArgument != String.Empty)
			{
				string id = lbtDelete.CommandArgument.ToString();
				Advert adv = new Advert();
				adv.Delete(id);
				Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
			}
		}
		private void lbtOnline_Click(object sender , System.EventArgs e)
		{
			LinkButton lbtOnline = (LinkButton)sender;
			if(lbtOnline.CommandArgument != String.Empty)
			{	
				string id = lbtOnline.CommandArgument.ToString();
				Advert adv = new Advert();
				adv.SetStatus(id);
				Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
			}
		}

    }

