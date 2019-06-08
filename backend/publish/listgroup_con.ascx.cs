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

public partial class publish_listgroup_con : System.Web.UI.UserControl
{
    
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    private Publish publish = null;
    private Groups group = null;
    private string groupid = null;    

    protected void Page_Load(object sender, EventArgs e)
    {
        group = new Groups();
        //groupid = treeview1.SelectedValue;
        if (Request.QueryString["groupid"] != null)
        {
            groupid = Request.QueryString["groupid"].ToString();            
        }
        if (!this.IsPostBack)
        {
            ltlHeader.Text = Language.GetTextByID(12);
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif";
            ltlHeader1.Text = Language.GetTextByID(124);
            ltlGroupTitle.Text = Language.GetTextByID(12);
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
        //if (groupid != null || groupid != string.Empty)
        //{
        //    DataRow dr = group.GetInfo(groupid);
        //}

        publish = new Publish(groupid);
        rptGroup.DataSource = publish.GetPublish();
        rptGroup.DataBind();

        string PageQuery = "page";
        string strcPage = Request.QueryString[PageQuery];
        if (strcPage == null)
            strcPage = "1";
        int cPage = Convert.ToInt32(strcPage);
        int TotalRecords, TotalPages;

        rptFree.DataSource = publish.GetNotInPublish(cPage, 20, out TotalRecords, out TotalPages);
        rptFree.DataBind();

        UssUrl sUrl = new UssUrl();
        PageList1.m_pTotalPage = TotalPages;
        PageList1.m_pPageQuery = PageQuery;
        PageList1.m_pCurrentPage = cPage;
        PageList1.m_pPageUrl = sUrl.Url;
        PageList1.m_pIconPath = Globals.UrlImages;
        
        ltlTotal1.Text = string.Format(Language.GetTextByID(45), TotalRecords);       
    }
    protected void BildRoot(TreeView treeview)
    {
        DataTable dt = group.GetChild(string.Empty);
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

    }
    protected void treeview1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        PopulateCategories(e.Node);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Publish publish = new Publish(groupid);
        foreach (RepeaterItem rptItem in rptGroup.Items)
        {
            Literal ltlID = (Literal)rptItem.FindControl("ltlID");
            TextBox txtIdx = (TextBox)rptItem.FindControl("txtIdx");
            string id = ltlID.Text.ToString();
            publish.UpdateIndex(id, Convert.ToInt32(txtIdx.Text.ToString()));
        }
        GetData();
        //Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }
    private void lbtDelete_Click(object sender, System.EventArgs e)
    {
        LinkButton lbtDelete = (LinkButton)sender;
        if (lbtDelete.CommandArgument != String.Empty)
        {
            string id = lbtDelete.CommandArgument.ToString();
            publish = new Publish(groupid);
            publish.RemoveFromPublish(id);
            GetData();
            //Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
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

            Literal ltlDeleteHeader = (Literal)e.Item.FindControl("ltlDeleteHeader");
            ltlDeleteHeader.Text = Language.GetTextByID(43);
        }
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems1 = (HtmlTableRow)e.Item.FindControl("trItems1");
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            TextBox txtIdx = (TextBox)e.Item.FindControl("txtIdx");
            Literal ltlTitle = (Literal)e.Item.FindControl("ltlTitle");

            LinkButton lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
            HtmlImage imgDelete = (HtmlImage)e.Item.FindControl("imgDelete");
            DataRowView dr = (DataRowView)e.Item.DataItem;
            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems1.Attributes.Add("class", "alter");
                else
                    trItems1.Attributes.Add("class", "item");
                ltlID.Text = dr["id"].ToString();
                txtIdx.Attributes.Add("style", "width:30px");
                txtIdx.Text = (e.Item.ItemIndex + 1).ToString();
                ltlTitle.Text = dr["title"].ToString();
                imgDelete.Src = Globals.UrlImages + "delete.gif";
                lbtDelete.CommandArgument = dr["id"].ToString();
            }
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
            Literal ltlSelectHeader1 = (Literal)e.Item.FindControl("ltlSelectHeader1");
            ltlSelectHeader1.Text = Language.GetTextByID(122);
        }
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems2 = (HtmlTableRow)e.Item.FindControl("trItems2");
            Literal ltlIndex1 = (Literal)e.Item.FindControl("ltlIndex1");
            Literal ltlTitle1 = (Literal)e.Item.FindControl("ltlTitle1");

            LinkButton lbtSelect1 = (LinkButton)e.Item.FindControl("lbtSelect1");
            DataRowView dr = (DataRowView)e.Item.DataItem;
            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems2.Attributes.Add("class", "alter");
                else
                    trItems2.Attributes.Add("class", "item");
                ltlIndex1.Text = (e.Item.ItemIndex + 1).ToString();
                ltlTitle1.Text = dr["title"].ToString();
                lbtSelect1.Text = Language.GetTextByID(122);
                lbtSelect1.CommandArgument = dr["id"].ToString();
            }
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
            publish = new Publish(groupid);
            publish.AddToPublish(id);
            GetData();
            //Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }    
}
