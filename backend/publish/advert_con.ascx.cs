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

public partial class publish_advert_con : System.Web.UI.UserControl
{
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    private string col
    {
        get
        {
            UssUrl sUrl = new UssUrl();
            string sRet = sUrl.GetParam("col");
            if (sRet == "" || sRet == string.Empty || sRet == null)
                sRet = "L";
            return sRet;
        }
    }
    private AdvertOnline advertonline = null;
    private Groups group = null;
    private string groupid = null;   

    protected void Page_Load(object sender, EventArgs e)
    {
        group = new Groups();        
        if (Request.QueryString["groupid"] != null)
        {
            groupid = Request.QueryString["groupid"].ToString();            
        }        
        if(!this.IsPostBack)
        {
            ltlHeader.Text = Language.GetTextByID(287);
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif";
            ltlHeader1.Text = Language.GetTextByID(140);
            ltlGroupTitle.Text = Language.GetTextByID(132);
            imgIconName.ImageUrl = Globals.UrlImages + "icon_menu1.gif";
            if (groupid != null)
            {
                ddlCol.Visible = true;
                Button1.Visible = true;
            }
            else
            {
                ltlName.Text = "";
                ddlCol.Visible = false;
                Button1.Visible = false;
            }
            //DropDownLoadList
            ddlCol.DataSource = AdvertOnline.GetAllPage();
            ddlCol.DataValueField = "id";
            ddlCol.DataTextField = "title";
            ddlCol.DataBind();
            ddlCol.Attributes.Add("style", "border:1 solid #ff0000; font-family:Verdana, Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold; color: #ff0000; width:250px");
            try
            {
                ddlCol.SelectedValue = col;
            }
            catch
            {
            }
            Button1.Text = Language.GetTextByID(37);
            //Trees view           
            //treeview1.ShowCheckBoxes = TreeNodeTypes.All;
            treeview1.ShowLines = true;
            treeview1.ExpandDepth = 0;
            BildRoot(treeview1);

            GetData();
        }
    }
    protected void GetData()
    {
        ddlCol.Visible = true;
        Button1.Visible = true;
        
        if (groupid != string.Empty)
        {
            Groups group = new Groups();
            if (groupid != null)
            {
                DataRow dr = group.GetInfo(groupid);
                if (dr != null)
                {
                    ltlName.Text = dr["title"].ToString();
                }
                else
                {
                    if (groupid == "00")
                    {
                        ltlName.Text = Language.GetTextByID(96);
                    }
                    //if (groupid == "000")
                    //{
                    //    ltlName.Text = Language.GetTextByID(241);
                    //}
                    //if (groupid == "0000")
                    //{
                    //    ltlName.Text = Language.GetTextByID(244);
                    //}
                }
            }
            
            advertonline = new AdvertOnline(groupid, col);
            rptGroup.DataSource = advertonline.GetAdvert();
            rptGroup.DataBind();
            string PageQuery = "page";
            string strcPage = Request.QueryString[PageQuery];
            if (strcPage == null)
                strcPage = "1";
            int cPage = Convert.ToInt32(strcPage);
            int TotalRecords, TotalPages;

            rptFree.DataSource = advertonline.GetNotInAdvert(cPage, 15, out TotalRecords, out TotalPages);
            rptFree.DataBind();
            UssUrl sUrl = new UssUrl();
            PageList1.m_pTotalPage = TotalPages;
            PageList1.m_pPageQuery = PageQuery;
            PageList1.m_pCurrentPage = cPage;
            PageList1.m_pPageUrl = sUrl.Url;
            PageList1.m_pIconPath = Globals.UrlImages;

            ltlTotal1.Text = string.Format(Language.GetTextByID(205), TotalRecords);
        }
    }
    protected void BildRoot(TreeView treeview)
    {
        TreeNode newNode = new TreeNode();
        //Home
        newNode.Text = Language.GetTextByID(96);
        newNode.Value = "00";
        newNode.NavigateUrl = Request.CurrentExecutionFilePath + "?groupid=00";
        treeview1.Nodes.Add(newNode);
        
        //Search
        /*
        newNode = new TreeNode();
        newNode.Text = Language.GetTextByID(241);
        newNode.Value = "000";
        newNode.NavigateUrl = Request.CurrentExecutionFilePath + "?groupid=000";
        treeview1.Nodes.Add(newNode);
        
        //Sitemap
        newNode = new TreeNode();
        newNode.Text = Language.GetTextByID(244);
        newNode.Value = "0000";
        newNode.NavigateUrl = Request.CurrentExecutionFilePath + "?groupid=0000";
        treeview1.Nodes.Add(newNode);
       
        
        DataTable dt = group.GetChild(string.Empty);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                newNode = new TreeNode();
                newNode.Text = dr["ptitle"].ToString();
                newNode.Value = dr["pid"].ToString();
                newNode.NavigateUrl = Request.CurrentExecutionFilePath + "?groupid=" + dr["pid"].ToString();
                bool kiemtra = group.CheckChild(dr["pid"].ToString());
                if (kiemtra)
                {
                    newNode.PopulateOnDemand = true;
                    newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                }
                else
                {
                    newNode.PopulateOnDemand = false;
                    newNode.SelectAction = TreeNodeSelectAction.Select;
                }
                treeview1.Nodes.Add(newNode);
                //treeview.Nodes.Add(newNode);
            }
        }
        else
        {
            treeview1.Visible = false;
        }
         */
    }
    protected void PopulateCategories(TreeNode node)
    {
        /*
        group = new Groups();
        DataTable dt = group.GetChild(node.Value);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = dr["ptitle"].ToString();
                newNode.Value = dr["pid"].ToString();
                newNode.NavigateUrl = Request.CurrentExecutionFilePath + "?groupid=" + dr["pid"].ToString();
                bool kiemtra = group.CheckChild(dr["pid"].ToString());
                if (kiemtra)
                {
                    newNode.PopulateOnDemand = true;
                    newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                }
                else
                {
                    newNode.PopulateOnDemand = false;
                    newNode.SelectAction = TreeNodeSelectAction.Select;
                }
                node.ChildNodes.Add(newNode);

            }
        }
         */

    }
    protected void treeview1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        PopulateCategories(e.Node);
    }

    protected void treeview1_SelectedNodeChanged(object sender, EventArgs e)
    {
        //groupid = treeview1.SelectedValue;
        GetData();
    }
    protected void rptGroup_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
            lbtDelete.Attributes.Add("onclick", "javascript:{ return ConfirmDelete('" + Language.GetTextByID(44) + "'); }");
            lbtDelete.CausesValidation = false;
            lbtDelete.Click += new System.EventHandler(lbtDelete_Click);
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

            Literal ltlLogoHeader = (Literal)e.Item.FindControl("ltlLogoHeader");
            ltlLogoHeader.Text = Language.GetTextByID(33);

            Literal ltlStartDateHeader = (Literal)e.Item.FindControl("ltlStartDateHeader");
            ltlStartDateHeader.Text = Language.GetTextByID(138);
            Literal ltlEndDateHeader = (Literal)e.Item.FindControl("ltlEndDateHeader");
            ltlEndDateHeader.Text = Language.GetTextByID(139);

            Literal ltlDeleteHeader = (Literal)e.Item.FindControl("ltlDeleteHeader");
            ltlDeleteHeader.Text = Language.GetTextByID(43);
        }
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems1 = (HtmlTableRow)e.Item.FindControl("trItems1");
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            TextBox tbxIdx = (TextBox)e.Item.FindControl("tbxIdx");
            Literal ltlTitle = (Literal)e.Item.FindControl("ltlTitle");
            Literal ltlLogo = (Literal)e.Item.FindControl("ltlLogo");
            TextBox tbxStartDate = (TextBox)e.Item.FindControl("tbxStartDate");
            TextBox tbxEndDate = (TextBox)e.Item.FindControl("tbxEndDate");
            LinkButton lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
            HtmlImage imgDelete = (HtmlImage)e.Item.FindControl("imgDelete");
            string sFile = string.Empty;
            string sPath = string.Empty;
            string fExt = string.Empty;

            DataRowView dr = (DataRowView)e.Item.DataItem;
            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems1.Attributes.Add("class", "alter");
                else
                    trItems1.Attributes.Add("class", "item");

                ltlID.Text = dr["id"].ToString();
                tbxIdx.Attributes.Add("style", "width:30px");
                tbxIdx.Text = (e.Item.ItemIndex + 1).ToString();
                ltlTitle.Text = dr["title"].ToString();

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

                tbxStartDate.Attributes.Add("style", "width:80px");
                tbxStartDate.Text = Convert.ToDateTime(dr["startdate"]).ToString("dd/MM/yyyy");
                tbxEndDate.Attributes.Add("style", "width:80px");
                tbxEndDate.Text = Convert.ToDateTime(dr["enddate"]).ToString("dd/MM/yyyy");
                imgDelete.Src = Globals.UrlImages + "delete.gif";
                lbtDelete.CommandArgument = dr["id"].ToString();
            }
        }

    }
    private void lbtDelete_Click(object sender, System.EventArgs e)
    {
        LinkButton lbtDelete = (LinkButton)sender;
        if (lbtDelete.CommandArgument != String.Empty)
        {
            string id = lbtDelete.CommandArgument.ToString();
            advertonline = new AdvertOnline(groupid, col);
            advertonline.RemoveFromAdvert(id);
            GetData();
        }
    }

    
    protected void rptFree_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtSelect1 = (LinkButton)e.Item.FindControl("lbtSelect1");
            lbtSelect1.Click += new System.EventHandler(lbtSelect_Click);
        }
    }
    private void lbtSelect_Click(object sender, System.EventArgs e)
    {
        LinkButton lbtSelect1 = (LinkButton)sender;
        if (lbtSelect1.CommandArgument != String.Empty)
        {
            string id = lbtSelect1.CommandArgument.ToString();
            advertonline = new AdvertOnline(groupid, col);
            advertonline.AddToAdvert(id);
            GetData();
        }
    }
    protected void rptFree_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Literal ltlIdxHeader1 = (Literal)e.Item.FindControl("ltlIdxHeader1");
            ltlIdxHeader1.Text = Language.GetTextByID(40);
            Literal ltlTitleHeader1 = (Literal)e.Item.FindControl("ltlTitleHeader1");
            ltlTitleHeader1.Text = Language.GetTextByID(29);


            Literal ltlLogoHeader1 = (Literal)e.Item.FindControl("ltlLogoHeader1");
            ltlLogoHeader1.Text = Language.GetTextByID(33);

            Literal ltlSelectHeader1 = (Literal)e.Item.FindControl("ltlSelectHeader1");
            ltlSelectHeader1.Text = Language.GetTextByID(122);
        }
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems2 = (HtmlTableRow)e.Item.FindControl("trItems2");
            Literal ltlIndex1 = (Literal)e.Item.FindControl("ltlIndex1");
            Literal ltlTitle1 = (Literal)e.Item.FindControl("ltlTitle1");
            Literal ltlLogo1 = (Literal)e.Item.FindControl("ltlLogo1");
            LinkButton lbtSelect1 = (LinkButton)e.Item.FindControl("lbtSelect1");
            string sFile = string.Empty;
            string sPath = string.Empty;
            string fExt = string.Empty;
            DataRowView dr = (DataRowView)e.Item.DataItem;
            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems2.Attributes.Add("class", "alter");
                else
                    trItems2.Attributes.Add("class", "item");
                ltlIndex1.Text = (e.Item.ItemIndex + 1).ToString();
                ltlTitle1.Text = dr["title"].ToString();

                string sfimage = dr["filename"].ToString();
                string SFile = string.Empty;
                if (sfimage.Length > 0)
                {
                    SFile = sfimage.Substring(sfimage.Length - 4);
                    string s = Globals.UrlRoot + dr["filename"].ToString();
                    if (SFile == ".swf")
                    {
                        ltlLogo1.Text = string.Format("<script language='javascript'>\n<!--\n embed_flash('{0}', '185', '','left');\n-->\n</script>", s);
                    }
                    else
                    {
                        ltlLogo1.Text = string.Format("<img src='{0}' border='0' width='185' >", s);
                    }
                }
                else
                {
                    ltlLogo1.Text = string.Empty;
                }
                lbtSelect1.Text = Language.GetTextByID(122);
                lbtSelect1.CommandArgument = dr["id"].ToString();
            }
        }

    }
    protected void ddlCol_SelectedIndexChanged(object sender, EventArgs e)
    {
        UssUrl sUrl = new UssUrl();
        sUrl.SetParam("col", ddlCol.SelectedValue.ToString());
        Response.Redirect(sUrl.Url, true);
        GetData();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        advertonline = new AdvertOnline(groupid, col);
        foreach (RepeaterItem rptItem in rptGroup.Items)
        {
            Literal ltlID = (Literal)rptItem.FindControl("ltlID");
            TextBox tbxIdx = (TextBox)rptItem.FindControl("tbxIdx");
            TextBox tbxStartDate = (TextBox)rptItem.FindControl("tbxStartDate");
            TextBox tbxEndDate = (TextBox)rptItem.FindControl("tbxEndDate");
            string id = ltlID.Text.ToString();
            string sStartDate = tbxStartDate.Text.Trim();
            string sEndDate = tbxEndDate.Text.Trim();
            int idx = Convert.ToInt32(tbxIdx.Text.ToString());
            advertonline.Update(id, Convert.ToDateTime(Globals.EnglishDate(sStartDate)), Convert.ToDateTime(Globals.EnglishDate(sEndDate)), idx);
        }
        GetData();
    }
}
