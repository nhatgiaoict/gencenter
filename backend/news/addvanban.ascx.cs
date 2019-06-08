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

public partial class news_addvanban : System.Web.UI.UserControl
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
        group = new Groups(1);
        if (!this.IsPostBack)
        {
            Globals.CKEditor(txtContent);
            ltlGroup.Text = Language.GetTextByID(91);
            ltlNote.Text = Language.GetTextByID(28);
            ltlRequireTitle.Text = "Nhập đầy đủ các trường bắt buộc";
            ltlGroup.Text = Language.GetTextByID(91);
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
            treeview1.ExpandDepth = 1;
            BildRoot(treeview1);
            
            // Hoivien
            //BuildHoivien(group.GetHoiVien());

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
        group = new Groups(1);
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
        string sShortLink = txtShotlink.Value.Trim();
        string sTitleMeta = txtTitleMeta.Text.Trim();
        string sKeyword = txtkeyword.Text.Trim();
        string sDescription = txtDescripton.Text.Trim();
        string sTomtat = tbxSummary.Text.Trim();
        string sImage = txtFileImages.Value.Trim();
        string sImages = txtMultiIMG.Value.Trim();
        string created = rdpPublishDate.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss");
        string sContent = txtContent.Text.Trim();
        if (sTitle == "" || sTitle == string.Empty || sShortLink == string.Empty || sShortLink == "" || sTitleMeta == "" || sTitleMeta == string.Empty)
        {
            ltlRequireTitle.Visible = true;
            return;
        }
        else
        {
            ltlRequireTitle.Visible = false;
        }
        string sSummary = tbxSummary.Text.Trim();
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
            sGroup = node.Value; //Lay da goc 

            News news = new News();
            int status = 0; //Tao moi
            //save + lay id ra
            string IDNew = news.InsertProduct(sGroup, sTitle, sTomtat, sContent, created, status, 1, sImage, sImages, sShortLink, sTitleMeta, sKeyword, sDescription);
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
        Response.Redirect(Globals.URLCurrent + "listvanban.aspx");
    }

    protected void BtPost_Click(object sender, EventArgs e)
    {
        string sTitle = tbxTitle.Text.Trim();
        string sShortLink = txtShotlink.Value.Trim();
        string sTitleMeta = txtTitleMeta.Text.Trim();
        string sKeyword = txtkeyword.Text.Trim();
        string sDescription =  txtDescripton.Text.Trim();
        string sTomtat = tbxSummary.Text.Trim();
        string sImage = txtFileImages.Value.Trim();
        string sImages = txtMultiIMG.Value.Trim();
        string created = rdpPublishDate.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss");
        string sContent = txtContent.Text.Trim();

        if (sTitle == "" || sTitle == string.Empty || sShortLink == string.Empty || sShortLink == "" || sTitleMeta == "" || 
            sTitleMeta == string.Empty)
        {
            ltlRequireTitle.Visible = true;
            return;
        }
        else
        {
            ltlRequireTitle.Visible = false;
        }
        string sSummary = tbxSummary.Text.Trim();
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
            //save + lay id ra
            string IDNew = news.InsertProduct(sGroup, sTitle, sTomtat, sContent, created, status, 1, sImage, sImages, sShortLink, sTitleMeta, sKeyword, sDescription);
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
        Response.Redirect(Globals.URLCurrent + "listvanban.aspx");
    }
}
