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

public partial class news_uc_lienket : System.Web.UI.UserControl
{
    private static UssUrl sUrl { get { return new UssUrl(); } }
    private string status { get { string status = sUrl.GetParam("Status"); ;if (status == "" || status == string.Empty || status == null)status = "0"; return status; } }
    private string groupid { get { string groupid = sUrl.GetParam("groupid"); if (groupid == null || groupid == "")groupid = string.Empty; return groupid; } }
    private string ParentID { get { if (groupid == "" || groupid == string.Empty || groupid == "") return string.Empty; else if (groupid.Length > Groups._nLength) return groupid.Substring(0, Groups._nLength); else return string.Empty; } }
    private string keyword { get { return Server.UrlDecode(sUrl.GetParam("Keyword")); } }
    public string UrlImages { get { return Globals.UrlImages; } }
    private string id { get { string id = sUrl.GetParam("id"); if (id == null || id == "")id = string.Empty; return id; } }
    private Groups group = null;
    private News sNew = null;
    public string IdLienket = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        group = new Groups();
        if (!this.IsPostBack)
        {
            //Trees view           
            treeview1.ShowCheckBoxes = TreeNodeTypes.All;
            treeview1.ShowLines = true;
            if (ParentID == "" || ParentID == string.Empty || ParentID == "") { treeview1.ExpandDepth = 0; }
            else { treeview1.ExpandDepth = Convert.ToInt32(ParentID); }
            BildRoot(treeview1);
            // Nội dung tiêu đề bài viết
            sNew = new News();
            DataRow dr = sNew.GetInfo(id);
            if (dr != null)
            {
                ltltieude.Text = dr["title"].ToString().Trim();
                IdLienket = dr["idlienket"].ToString().Trim();
                if (IdLienket.Length > 2)
                {
                    IdLienket = IdLienket.Remove(IdLienket.Length - 1);
                }
            }
            GetData(IdLienket);
        }
    }
    private void GetData(string IdLienketC)
    {
        News news = new News();
        RptIN.DataSource = news.GetAllDataSelect(IdLienketC);
        RptIN.DataBind();
        //

        string vusername = "";
        if (!Membertask.IsAdministrator())
        {
            Member mem = new Member();
            DataRow dr = mem.GetInfoName(Membertask.Name);
            vusername = dr["username"].ToString().Trim();
        }
        else
        {
            vusername = string.Empty;
        }


        this.txtKeyword.Text = keyword;
        UssUrl sUrl1 = new UssUrl(Request.CurrentExecutionFilePath);
        sUrl1.SetParam("Keyword", Server.UrlEncode(txtKeyword.Text.ToString()));
        sUrl1.SetParam("groupid", groupid);
        sUrl1.SetParam("id", id);
        string PageQuery = "page";
        string strcPage = Request.QueryString[PageQuery];
        if (strcPage == null)
            strcPage = "1";
        int cPage = Convert.ToInt32(strcPage);
        int TotalRecords, TotalPages;
        rptNews.DataSource = news.Searching_ForLienKet(vusername,keyword, groupid, cPage, 20, out TotalRecords, out TotalPages, IdLienketC);
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
            ltlTotal1.Visible = false;
        }
        ltlTotal.Text = ltlTotal1.Text = string.Format(Language.GetTextByID(45), TotalRecords);
    }
    protected void imgBtn_Click(object sender, ImageClickEventArgs e)
    {
        UssUrl sUrl = new UssUrl(Request.CurrentExecutionFilePath);
        sUrl.SetParam("Keyword", Server.UrlEncode(txtKeyword.Text.ToString()));
        sUrl.SetParam("id", id);
        string total_node = string.Empty;
        string TotalGroupid = string.Empty;
        group = new Groups();
        string sGroup = string.Empty;
        foreach (TreeNode node in treeview1.CheckedNodes)
        {
            sGroup += node.Value + ",";
        }
        if (sGroup != string.Empty)//groupid here
        {
            sGroup = sGroup.Substring(0, sGroup.Length - 1);
            total_node = sGroup + "," + GetMutilTree(sGroup);
            TotalGroupid = total_node.Substring(0, total_node.Trim().Length - 1);
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
    protected void rptNews_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtSelect = (LinkButton)e.Item.FindControl("lbtSelect");
            lbtSelect.CausesValidation = false;
            lbtSelect.Click += new EventHandler(lbtSelect_Click);
        }
    }

    protected void lbtSelect_Click(object sender, EventArgs e)
    {
        LinkButton lbtSelect = (LinkButton)sender;
        if (lbtSelect.CommandArgument != String.Empty)
        {
            string idSelect = lbtSelect.CommandArgument.ToString() + ",";
            News news = new News();
            news.UpdateIdLienket(id, idSelect);
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }
    protected void rptNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems = (HtmlTableRow)e.Item.FindControl("trItems");
            Literal ltlID = (Literal)e.Item.FindControl("ltlID");
            Literal ltlIDNews = (Literal)e.Item.FindControl("ltlIDNews");
            HtmlAnchor ltlTitle = (HtmlAnchor)e.Item.FindControl("ltlTitle");

            Literal ltlCreated = (Literal)e.Item.FindControl("ltlCreated");
            LinkButton lbtSelect = (LinkButton)e.Item.FindControl("lbtSelect");
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
                ltlTitle.InnerText = dr["title"].ToString().Trim();
                //
                ltlCreated.Text = Globals.FullDateTime(Convert.ToDateTime(dr["created"]));
                lbtSelect.CommandArgument = dr["id"].ToString();
                // xem chi tiết
                sUrl = new UssUrl(Globals.URLCurrent + "detail.aspx");
                sUrl.SetParam("newsid", dr["id"].ToString());
                ltlTitle.Attributes.Add("style", "cursor:pointer;");
                ltlTitle.Attributes.Add("onclick", "javascript:ShowPopup('" + sUrl.Url + "','670','800');");
            }
        }
    }

    protected void treeview1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        PopulateCategories(e.Node);
    }
    protected void BildRoot(TreeView treeview)
    {
        string sgroup = "," + groupid + ",";
        DataTable dt = null;
        dt = group.GetChildNews(string.Empty);
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
                //if (Membertask.IsAdministrator())
                //{
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
                //}
                //else
                //{
                //    int Checkp = group.CheckP(dr["pid"].ToString(), Session["ID"].ToString());
                //    if (Checkp == 1)
                //    {
                //        bool kiemtra = group.CheckChild(dr["pid"].ToString());
                //        if (kiemtra)
                //        {
                //            newNode.PopulateOnDemand = true;
                //            newNode.SelectAction = TreeNodeSelectAction.SelectExpand;

                //        }
                //        else
                //        {
                //            newNode.PopulateOnDemand = false;
                //            newNode.SelectAction = TreeNodeSelectAction.Select;
                //        }

                //        treeview.Nodes.Add(newNode);
                //    }
                //    else
                //    {

                //    }
                //}
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
        DataTable dt = null;
        dt = group.GetChildNews(node.Value);
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
                    newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                }
                else
                {
                    newNode.PopulateOnDemand = false;
                    newNode.SelectAction = TreeNodeSelectAction.Select;
                } if (Membertask.IsAdministrator())
                {
                    node.ChildNodes.Add(newNode);
                }
                else
                {
                    int Checkp = group.CheckP(dr["pid"].ToString(), Session["ID"].ToString());
                    if (Checkp == 1)
                    {
                        node.ChildNodes.Add(newNode);
                    }
                    else
                    {
                    }
                }
            }
        }
    }
    protected void RptIN_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            LinkButton lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
            lbtDelete.CausesValidation = false;
            lbtDelete.Click += new EventHandler(lbtDelete_Click);
        }
    }
    protected void lbtDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtDelete = (LinkButton)sender;
        if (lbtDelete.CommandArgument != String.Empty)
        {
            // Remove trong đối tượng A
            string idSelect = lbtDelete.CommandArgument.ToString(); // ID bài viết đã lựa chọn + thêm dấu phẩy
            News news = new News();
            DataRow dr = news.GetInfo(id);
            string sidLienket = dr["idlienket"].ToString();
            string TempIDselect = idSelect + ",";
            sidLienket = sidLienket.Replace(TempIDselect, ""); // idlienket da delete id cua bài viết lựa chọn xóa
            news.RemoveIDlienket(id, sidLienket); // Update lại IDLienket cho đối tượng A
            
            // Remove A trong đối tượng được lựa chọn là Idselect 
            DataRow drselect = news.GetInfo(idSelect);
            string sIDLKSelect = drselect["idlienket"].ToString();
            string TempID = id + ",";
            sIDLKSelect = sIDLKSelect.Replace(TempID, "");
            news.RemoveIDlienket(idSelect, sIDLKSelect);
        }
        Response.Redirect(Globals.UrlRoot + "temp.aspx?url=" + Request.Url);
    }
    protected void RptIN_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
        {
            HtmlTableRow trItems = (HtmlTableRow)e.Item.FindControl("trItems");
            Literal ltlIDN = (Literal)e.Item.FindControl("ltlIDN");
            Literal ltlIDNewsN = (Literal)e.Item.FindControl("ltlIDNewsN");
            HtmlAnchor ltlTitleN = (HtmlAnchor)e.Item.FindControl("ltlTitleN");
            LinkButton lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
            DataRowView dr = (DataRowView)e.Item.DataItem;
            UssUrl sUrl = null;
            if (dr != null)
            {
                if (e.Item.ItemIndex % 2 == 1)
                    trItems.Attributes.Add("class", "alter");
                else
                    trItems.Attributes.Add("class", "item");
                ltlIDN.Text = (e.Item.ItemIndex + 1).ToString();
                ltlIDNewsN.Text = dr["id"].ToString();
                ltlTitleN.InnerText = dr["title"].ToString().Trim();
                lbtDelete.CommandArgument = dr["id"].ToString();
                //
                // xem chi tiết
                sUrl = new UssUrl(Globals.URLCurrent + "detail.aspx");
                sUrl.SetParam("newsid", dr["id"].ToString());
                ltlTitleN.Attributes.Add("style", "cursor:pointer;");
                ltlTitleN.Attributes.Add("onclick", "javascript:ShowPopup('" + sUrl.Url + "','670','800');");
            }
        }
    }
    protected void btnlkcheo_Click(object sender, EventArgs e)
    {
        sNew = new News();
        DataRow dr = sNew.GetInfo(id);
        if (dr != null)
        {
            ltltieude.Text = dr["title"].ToString().Trim();
            IdLienket = id + "," + dr["idlienket"].ToString().Trim();
            //if (IdLienket.Length > 2)
            //{
            //    IdLienket = IdLienket.Remove(IdLienket.Length - 1);
            //}
        }
        foreach (RepeaterItem rptItem in RptIN.Items)
        {
            Literal ltlIDNews = (Literal)rptItem.FindControl("ltlIDNewsN");
            string Temp1 = ltlIDNews.Text.Trim() + ",";
            //string Temp2 = "," + ltlIDNews.Text.Trim();
            if (IdLienket.Contains(Temp1))
            {
                string IDLKCL = IdLienket.Replace(Temp1, "");
                sNew.UpdateIdLienketCheo(ltlIDNews.Text.Trim(), IDLKCL);
            }
        }
        // Giờ là tích hợp thêm chức năng dở hơi, nhưng không hơi dở
    }
}
