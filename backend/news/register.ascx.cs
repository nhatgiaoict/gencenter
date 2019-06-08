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

public partial class news_register : System.Web.UI.UserControl
{
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
        group = new Groups(0);
        if (!this.IsPostBack)
        {
            // Set Editor
            Globals.CKEditor(txtContent);
            // End Set Editor
            txtFileImages.Attributes.Add("style", "Width:250px");
            //tbxWidth.Attributes.Add("style", "Width:50px");
            //tbxHeight.Attributes.Add("style", "Width:50px");
            //ltlWidth.Text = Language.GetTextByID(231);
            //ltlHeight.Text = Language.GetTextByID(232);

            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif"; ;
            ltlHeader.Text = Language.GetTextByID(159);
            ltlNote.Text = Language.GetTextByID(28);
            ltlRequireTitle.Text = "Nhập đầy đủ các trường bắt buộc!";
            ltlRequireGroup.Text = Language.GetTextByID(94);
            rdpPublishDate.SelectedDate = DateTime.Now;
            ltlRequireContent.Text = Language.GetTextByID(36);
            BtPost.Text = Language.GetTextByID(218);

            if (Membertask.IsAdministrator() || (Membertask.IsTaonoidung() != string.Empty && Membertask.IsPostOn() != string.Empty))
            {
                BtPost.Visible = true;
            }
            else
            {
                BtPost.Visible = false;
            }

            //Trees view           
            treeview1.ShowCheckBoxes = TreeNodeTypes.All;
            treeview1.ShowLines = true;
            treeview1.ExpandDepth = 0;
            BildRoot(treeview1);
        }
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
        group = new Groups(0);
        DataTable dt = group.GetChild(node.Value);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = dr["ptitle"].ToString();
                newNode.Value = dr["pid"].ToString();
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
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string sTitle = tbxTitle.Text.Trim();
        string sFilename = txtFileImages.Value.Trim();
        string created = rdpPublishDate.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss");
        string sShortlink = txtShotlink.Value.Trim();
        string sTitlemeta = txtTitleMeta.Text.Trim();
        string skeywordmeta = txtkeyword.Text.Trim();
        string sDescriptionMeta = txtDescripton.Text.Trim();
        if (sTitle == "" || sTitle == string.Empty || sShortlink == "" || sShortlink == string.Empty || sTitlemeta == string.Empty || sTitlemeta == "")
        {
            ltlRequireTitle.Visible = true;
            return;
        }
        else
        {
            ltlRequireTitle.Visible = false;
        }

        string sSummary = tbxSummary.Text.Trim();

        string sContent = txtContent.Text.Trim();
        if (sContent == "" || sContent == string.Empty)
        {
            ltlRequireContent.Visible = true;
            return;
        }
        else
        {
            ltlRequireContent.Visible = false;
        }
       
        //ảnh minh họa Halong
        //Treeview
        group = new Groups();
        string sGroup = string.Empty;
        foreach (TreeNode node in treeview1.CheckedNodes)
        {
            sGroup = node.Value; //Lay da goc 
        }
        if (sGroup == "" || sGroup == string.Empty || sGroup == null)
        {
            ltlRequireGroup.Visible = true;
            return;
        }
        else ltlRequireGroup.Visible = false;
        foreach (TreeNode node in treeview1.CheckedNodes)
        {
            sGroup = node.Value; //Lay da goc 
            News news = new News();
            int iKind = group.Ikind(sGroup);
            int status = 0; //Tao moi
            //save + lay id ra
            string IDNew = news.Insert(sTitle, sGroup, sFilename, sSummary, sContent, created, status, sTitlemeta, skeywordmeta, sDescriptionMeta, sShortlink, iKind);
            //Save log
            string datenow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string Ghichu = "";
            if (test_lang != 2)
            {
                Ghichu = "Tạo mới";
            }
            else
            {
                Ghichu = "New Create";
            }
            Logfile log = new Logfile();
            log.insertDatalog(IDNew, datenow, 2, Ghichu.Trim());
        }
        Response.Redirect(Globals.URLCurrent + "listbaiviet.aspx");
    }

    protected void BtPost_Click(object sender, EventArgs e)
    {
        string sTitle = tbxTitle.Text.Trim();
        string sFilename = txtFileImages.Value.Trim();
        string sShortlink = txtShotlink.Value.Trim();
        string sTitlemeta = txtTitleMeta.Text.Trim();
        string skeywordmeta = txtkeyword.Text.Trim();
        string sDescriptionMeta = txtDescripton.Text.Trim();
        string created = rdpPublishDate.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss");
        if (sTitle == "" || sTitle == string.Empty || sShortlink == "" || sShortlink == string.Empty || sTitlemeta == string.Empty || sTitlemeta == "")
        {
            ltlRequireTitle.Visible = true;
            return;
        }
        else
        {
            ltlRequireTitle.Visible = false;
        }
        string sSummary = tbxSummary.Text.Trim();
        string sContent = txtContent.Text.Trim();
        if (sContent == "" || sContent == string.Empty)
        {
            ltlRequireContent.Visible = true;
            return;
        }
        else
        {
            ltlRequireContent.Visible = false;
        }
       
        //Treeview
        group = new Groups();
        string sGroup = string.Empty;
        foreach (TreeNode node in treeview1.CheckedNodes)
        {
            sGroup = node.Value; //Lay da goc 
        }
        if (sGroup == "" || sGroup == string.Empty || sGroup == null)
        {
            ltlRequireGroup.Visible = true;
            return;
        }
        else
            ltlRequireGroup.Visible = false;

        foreach (TreeNode node in treeview1.CheckedNodes)
        {
            sGroup = node.Value; // Lay da goc
            News news = new News();
            int status = 1; //Online
            //int ikind = group.Ikind(sGroup);
            //save + lay id ra
            string IDNew = news.Insert(sTitle, sGroup, sFilename, sSummary, sContent, created, status,  sTitlemeta, skeywordmeta, sDescriptionMeta, sShortlink, 0);
            //Save log
            string datenow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string Ghichu = "";
            if (test_lang != 2)
            {
                Ghichu = "Tạo mới - Đăng ngay ";
            }
            else
            {
                Ghichu = "New Create - Online ";
            }
            Logfile log = new Logfile();
            log.insertDatalog(IDNew, datenow, status, Ghichu.Trim());
        }
        Response.Redirect(Globals.URLCurrent + "listbaiviet.aspx");
    }
}
