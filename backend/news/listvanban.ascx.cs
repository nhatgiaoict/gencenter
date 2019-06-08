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

public partial class news_listvanban : System.Web.UI.UserControl
{
    private static UssUrl sUrl { get { return new UssUrl(); } }
    private string status
    {
        get
        {
            string status = sUrl.GetParam("Status"); ;
            if (status == "" || status == string.Empty || status == null)
                status = "2";
            return status;
        }
    }
    private string groupid
    {
        get
        {
            string groupid = sUrl.GetParam("groupid");
            if (groupid == null || groupid == "")
                groupid = string.Empty;
            return groupid;
        }
    }
    private string ParentID
    {
        get
        {
            if (groupid == "" || groupid == string.Empty || groupid == "")
                return string.Empty;
            else if (groupid.Length > Groups._nLength)
                return groupid.Substring(0, Groups._nLength);
            else
                return string.Empty;
        }
    }
    private string keyword
    {
        get { return Server.UrlDecode(sUrl.GetParam("Keyword")); }
    }
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    private static int test_lang = 0;
    private Groups group = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        group = new Groups(1);
        string sLang = Globals.CurrentLang;
        if (sLang == "vn")
        {
            sLang = "vi";
            test_lang = 1;
        }
        else
        {
            test_lang = 2;
        }
        if (!this.IsPostBack)
        {
            //Trees view           
            treeview1.ShowCheckBoxes = TreeNodeTypes.All;
            treeview1.ShowLines = true;
            if (ParentID == "" || ParentID == string.Empty || ParentID == "")
            {
                treeview1.ExpandDepth = 0;
            }
            else
            {
                treeview1.ExpandDepth = Convert.ToInt32(ParentID);
            }
            BildRoot(treeview1);
            BtDelete.Text = Language.GetTextByID(43);
            txtKeyword.Attributes.Add("style", "width:250px");

            imgBtn.ImageUrl = Globals.UrlImages + "search_new.gif";
            ddlStatus.Attributes.Add("style", "border:1 solid #ff0000; font-family:Verdana, Arial, Helvetica, sans-serif; font-size:11px; width:170px");
            ddlStatus.DataSource = Language.AllStatus();
            ddlStatus.DataValueField = "id";
            ddlStatus.DataTextField = Globals.CurrentLang;
            ddlStatus.DataBind();
            ddlStatus.SelectedValue = status;

            this.txtKeyword.Text = keyword;
            UssUrl sUrl1 = new UssUrl(Request.CurrentExecutionFilePath);
            sUrl1.SetParam("Keyword", Server.UrlEncode(txtKeyword.Text.ToString()));
            sUrl1.SetParam("Status", ddlStatus.SelectedValue.ToString());
            sUrl1.SetParam("groupid", groupid);

            string PageQuery = "page";
            string strcPage = Request.QueryString[PageQuery];
            if (strcPage == null)
                strcPage = "1";
            int cPage = Convert.ToInt32(strcPage);
            int TotalRecords, TotalPages;

            News news = new News();
            rptNews.DataSource = news.Searching_ofUsers(keyword, groupid, 1, Convert.ToInt32(status), cPage, 20, out TotalRecords, out TotalPages);
            rptNews.DataBind();
            
            PageList1.m_pTotalPage = TotalPages;
            PageList1.m_pPageQuery = PageQuery;
            PageList1.m_pCurrentPage = cPage;
            PageList1.m_pPageUrl = sUrl1.Url;
            PageList1.m_pIconPath = Globals.UrlImages;

            PageList2.m_pTotalPage = TotalPages;
            PageList2.m_pPageQuery = PageQuery;
            PageList2.m_pCurrentPage = cPage;
            PageList2.m_pPageUrl = sUrl1.Url;
            PageList2.m_pIconPath = Globals.UrlImages;

            if (TotalRecords == 0)
            {
                //ltlTotal.Visible = false;
                ltlTotal1.Visible = false;
            }
            ltlTotal.Text = ltlTotal1.Text = string.Format(Language.GetTextByID(45), TotalRecords);
            
        }

    }
    protected void BildRoot(TreeView treeview)
    {
        string sgroup = "," + groupid + ",";
        DataTable dt = group.GetChild(string.Empty);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string sid = dr["pid"].ToString();
                TreeNode newNode = new TreeNode();
                newNode.Text = dr["ptitle"].ToString();
                newNode.Value = sid;
                if (sgroup.IndexOf("," + sid + ",") > -1)
                {
                    newNode.Checked = true;
                }
                bool kiemtra = group.CheckChild(dr["pid"].ToString());
                if (kiemtra)
                {
                    newNode.PopulateOnDemand = true;
                    newNode.SelectAction = TreeNodeSelectAction.Expand;
                }
                else
                {
                    newNode.PopulateOnDemand = false;
                    newNode.SelectAction = TreeNodeSelectAction.None;
                }
                treeview.Nodes.Add(newNode);
            }
        }
        else
        {
            treeview1.Visible = false;
        }

    }
    protected void PopulateCategories(TreeNode node)
    {
        string sgroup = "," + groupid + ",";
        group = new Groups(1);
        DataTable dt = group.GetChild(node.Value);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode newNode = new TreeNode();
                string sid = dr["pid"].ToString();
                newNode.Text = dr["ptitle"].ToString();
                newNode.Value = sid;
                if (sgroup.IndexOf("," + sid + ",") > -1)
                {
                    newNode.Checked = true;
                }
                bool kiemtra = group.CheckChild(dr["pid"].ToString());
                if (kiemtra)
                {
                    newNode.PopulateOnDemand = true;
                    newNode.SelectAction = TreeNodeSelectAction.Expand;
                }
                else
                {
                    newNode.PopulateOnDemand = false;
                    newNode.SelectAction = TreeNodeSelectAction.None;
                }
                node.ChildNodes.Add(newNode);

            }
        }

    }
    protected void treeview1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        PopulateCategories(e.Node);
    }
    protected void BtDelete_Click(object sender, EventArgs e)
    {
        News news = new News();
        string id = string.Empty;
        foreach (RepeaterItem items in rptNews.Items)
        {
            CheckBox checkboxID = (CheckBox)items.FindControl("checkboxID");
            if (checkboxID.Checked)
            {
                Literal ltlIDNews = (Literal)items.FindControl("ltlIDNews");
                id = ltlIDNews.Text.Trim();
                //Xoa 
                news.Delete(id);
                //Xoa log
                Logfile log = new Logfile();
                log.DeleteLog(id);
            }
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);

    }
    private void lbtSend_Click(object sender, System.EventArgs e)
    {
        LinkButton lbtSend = (LinkButton)sender;
        if (lbtSend.CommandArgument != String.Empty)
        {
            string id = lbtSend.CommandArgument.ToString();
            News news = new News();
            news.SetStatus(id);

            //Save log
            string created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string Ghichu = "";
            if (test_lang != 2)
            {
                Ghichu = "Cho phép bài hiển thị";
            }
            else
            {
                Ghichu = "Online";
            }
            Logfile log = new Logfile();
            int status = 1;
            log.insertDatalog(id, created, status, Ghichu.Trim());
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }
    protected void BtSend_Click(object sender, EventArgs e)
    {
        News news = new News();
        foreach (RepeaterItem items in rptNews.Items)
        {
            Literal ltlIDNews = (Literal)items.FindControl("ltlIDNews");
            TextBox txtSTT = (TextBox)items.FindControl("txtSTT");
            string id = ltlIDNews.Text.Trim();
            int ncount = Convert.ToInt32(txtSTT.Text.Trim());
            news.UpdateSTT(id, ncount);
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);

    }

    protected void rptNews_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtSend = (LinkButton)e.Item.FindControl("lbtSend");
            lbtSend.Attributes.Add("onclick", "javascript:{ return ConfirmWarning('" + Language.GetTextByID(165) + "'); }");
            lbtSend.CausesValidation = false;
            lbtSend.Click += new System.EventHandler(lbtSend_Click);
            //sp Moi
            LinkButton ltbMoi = (LinkButton)e.Item.FindControl("ltbMoi");
            ltbMoi.CausesValidation = false;
            ltbMoi.Click += new EventHandler(ltbMoi_Click);

            //sp Moi
            LinkButton ltbHoanthanh = (LinkButton)e.Item.FindControl("ltbHoanthanh");
            ltbHoanthanh.CausesValidation = false;
            ltbHoanthanh.Click += new EventHandler(ltbHoanthanh_Click);

        }
    }

    protected void ltbHoanthanh_Click(object sender, EventArgs e)
    {
        LinkButton ltbHoanthanh = (LinkButton)sender;
        if (ltbHoanthanh.CommandArgument != String.Empty)
        {
            string id = ltbHoanthanh.CommandArgument.ToString();
            News news = new News();
            news.SetHotlike(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }

    protected void ltbMoi_Click(object sender, EventArgs e)
    {
        LinkButton ltbMoi = (LinkButton)sender;
        if (ltbMoi.CommandArgument != String.Empty)
        {
            string id = ltbMoi.CommandArgument.ToString();
            News news = new News();
            news.SetHotNew(id);
            Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }
    protected void rptNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems = (HtmlTableRow)e.Item.FindControl("trItems");
            HyperLink ltlTitle = (HyperLink)e.Item.FindControl("ltlTitle");
            Literal ltlCreated = (Literal)e.Item.FindControl("ltlCreated");
            TextBox txtSTT = (TextBox)e.Item.FindControl("txtSTT");
            HtmlAnchor hrEdit = (HtmlAnchor)e.Item.FindControl("hrEdit");
            LinkButton lbtSend = (LinkButton)e.Item.FindControl("lbtSend");
            HtmlImage imgHistory = (HtmlImage)e.Item.FindControl("imgHistory");
            Literal ltlIDNews = (Literal)e.Item.FindControl("ltlIDNews");
            CheckBox checkboxID = (CheckBox)e.Item.FindControl("checkboxID");
            LinkButton ltbMoi = (LinkButton)e.Item.FindControl("ltbMoi");
            LinkButton ltbHoanthanh = (LinkButton)e.Item.FindControl("ltbHoanthanh");
            DataRowView dr = (DataRowView)e.Item.DataItem;
            UssUrl sUrl = null;

            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems.Attributes.Add("class", "alter");
                else
                    trItems.Attributes.Add("class", "item");

                ltlIDNews.Text = dr["id"].ToString();

                txtSTT.Text = (e.Item.ItemIndex + 1).ToString();
                ltlCreated.Text = Globals.FullDateTime(Convert.ToDateTime(dr["created"]));
                ltlTitle.Text = dr["title"].ToString();
                sUrl = new UssUrl(Globals.URLCurrent + "detail.aspx");
                sUrl.SetParam("newsid", dr["id"].ToString());
                ltlTitle.Attributes.Add("style", "cursor:hand; cursor:pointer;");
                ltlTitle.Attributes.Add("onclick", "javascript:ShowPopup('" + sUrl.Url + "','670','800');");
                
                sUrl = new UssUrl(Globals.URLCurrent + "updatevanban.aspx");
                sUrl.SetParam("id", dr["id"].ToString());
                hrEdit.InnerText = Language.GetTextByID(42);
                //hrEdit.Visible = false;
                hrEdit.HRef = sUrl.Url;
                lbtSend.Text = (Convert.ToInt32(dr["status"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                lbtSend.CommandArgument = dr["id"].ToString();

                ltbMoi.Text = (Convert.ToInt32(dr["hotnew"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                ltbMoi.CommandArgument = dr["id"].ToString();

                // 
                ltbHoanthanh.Text = (Convert.ToInt32(dr["hotlike"]) == 1) ? "<font color='#0000FF'>" + Language.GetTextByID(46) + "</font>" : "<font color='#FF0000'>" + Language.GetTextByID(47) + "</font>";
                ltbHoanthanh.CommandArgument = dr["id"].ToString();

                checkboxID.Visible = false;
                if (Convert.ToInt32(dr["status"].ToString()) == 0)
                {
                    checkboxID.Visible = true;
                }
                sUrl = new UssUrl(Globals.UrlRoot + "news/logfile.aspx");
                sUrl.SetParam("ID", dr["ID"].ToString());
                imgHistory.Attributes.Add("style", "cursor:hand;cursor:pointer");
                imgHistory.Attributes.Add("onclick", "javascript:ShowPopup('" + sUrl.Url + "','800','400');");
                imgHistory.Src = Globals.UrlImages + "history.gif";
            }
        }
    }
    protected void imgBtn_Click(object sender, ImageClickEventArgs e)
    {
        UssUrl sUrl = new UssUrl(Request.CurrentExecutionFilePath);
        sUrl.SetParam("Keyword", Server.UrlEncode(txtKeyword.Text.ToString()));
        sUrl.SetParam("Status", ddlStatus.SelectedValue.ToString());
       
        group = new Groups();
        string sGroup = string.Empty;        
        foreach (TreeNode node in treeview1.CheckedNodes)
        {
            sGroup += node.Value+",";
        }
        if (sGroup != string.Empty)
        {
            sGroup = sGroup.Substring(0, sGroup.Length - 1);
            sUrl.SetParam("groupid", sGroup);
        }
        Response.Redirect(sUrl.Url);
    }
}
