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
public partial class Ykienkhachhang_con : System.Web.UI.UserControl
{
    public string UrlImages { get { return Globals.UrlImages; } }
    private static UssUrl sUrl
    {
        get
        {
            return new UssUrl();
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
    private Publish publish = null;
    private Groups group = null;
    // private string groupid = null;  
    protected void Page_Load(object sender, EventArgs e)
    {
        group = new Groups(0);
        if (!this.IsPostBack)
        {
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif";
            ltlHeader1.Text = Language.GetTextByID(124);

            ltlGroup.Text = Language.GetTextByID(91);
            ltlKeyword.Text = Language.GetTextByID(38);
            txtKeyword.Attributes.Add("style", "width:250px");
            imgBtn.ImageUrl = Globals.UrlImages + "search_new.gif";

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
            GetData();
        }

    }
    protected void GetData()
    {
        //DataRow dr = group.GetInfo(groupid);        
        publish = new Publish(groupid);
        rptGroup.DataSource = publish.GetPublish_Ykienkhachhang();
        rptGroup.DataBind();

        this.txtKeyword.Text = keyword;

        string PageQuery = "page";
        string strcPage = Request.QueryString[PageQuery];
        if (strcPage == null)
            strcPage = "1";
        int cPage = Convert.ToInt32(strcPage);
        int TotalRecords, TotalPages;

        rptFree.DataSource = publish.GetNotInPublish_Ykienkhachhang(keyword, groupid, cPage, 25, out TotalRecords, out TotalPages);
        rptFree.DataBind();

        UssUrl sUrl = new UssUrl();
        PageList1.m_pTotalPage = TotalPages;
        PageList1.m_pPageQuery = PageQuery;
        PageList1.m_pCurrentPage = cPage;
        PageList1.m_pPageUrl = sUrl.Url;
        PageList1.m_pIconPath = Globals.UrlImages;

        ltlTotal1.Text = string.Format(Language.GetTextByID(45), TotalRecords);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        Publish publish = new Publish(groupid);
        foreach (RepeaterItem rptItem in rptGroup.Items)
        {
            Literal ltlID = (Literal)rptItem.FindControl("ltlID");
            TextBox txtIdx = (TextBox)rptItem.FindControl("txtIdx");
            string id = ltlID.Text.ToString();
            publish.UpdateIndex_Ykienkhachhang(id, Convert.ToInt32(txtIdx.Text.ToString()));
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
            publish.RemoveFromPublish_Ykienkhachhang(id);
            GetData();
            //Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
        }
    }
    private void lbtSelect_Click(object sender, System.EventArgs e)
    {
        LinkButton lbtSelect1 = (LinkButton)sender;
        if (lbtSelect1.CommandArgument != String.Empty)
        {
            string id = lbtSelect1.CommandArgument.ToString();
            publish = new Publish(groupid);
            publish.AddToPublish_Ykienkhachhang(id);
            GetData();
            //Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
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
                if (Convert.ToInt32(dr["nextchild"].ToString()) > 0)
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
        group = new Groups();
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
                if (Convert.ToInt32(dr["nextchild"].ToString()) > 0)
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
    protected void imgBtn_Click(object sender, ImageClickEventArgs e)
    {
        UssUrl sUrl = new UssUrl(Request.CurrentExecutionFilePath);
        sUrl.SetParam("Keyword", Server.UrlEncode(txtKeyword.Text.ToString()));
        group = new Groups();
        string sGroup = string.Empty;
        foreach (TreeNode node in treeview1.CheckedNodes)
        {
            sGroup += node.Value + ",";
        }
        if (sGroup != string.Empty)
        {
            sGroup = sGroup.Substring(0, sGroup.Length - 1);
            string total_node = sGroup + "," + GetMutilTree(sGroup);
            string TotalGroupid = total_node.Substring(0, total_node.Trim().Length - 1);
            sUrl.SetParam("groupid", TotalGroupid);
        }
        Response.Redirect(sUrl.Url);
    }
    public string GetMutilTree(string s)
    {
        string Kq = string.Empty;
        string[] arr_s = s.Split(',');
        int count = Convert.ToInt32(arr_s.Length);
        if (count > 1)
        {
            string k = string.Empty;
            for (int j = 0; j < count; j++)
            {
                k = arr_s[j].ToString();
                Kq += GetTree(k);
            }
            //Response.Write("<br> Kq = " + Kq);
        }
        else
        {
            Kq = GetTree(s);
            //Response.Write("<br> Kq = " + Kq);
        }
        return Kq;
    }
    public string GetTree(string s)
    {
        Groups objgrouid = new Groups();
        string[] arr = s.Split(',');
        int n = Convert.ToInt32(arr.Length);
        string m = string.Empty;
        string s_con = string.Empty;
        for (int i = 0; i < n; i++)
        {
            m = arr[i].ToString();
            s_con += objgrouid.GetIDChild(m);
        }
        DataTable dt = objgrouid.GetChild(s);
        string con = string.Empty;
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                con += dr["pid"].ToString() + ",";
            }
            return s_con;
        }
        else
        {
            return s_con;
        }
        //Response.Write("<br> goc = " + s + " <br> s_con= " + s_con);
    }
}
