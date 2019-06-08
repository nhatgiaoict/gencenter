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

public partial class uchoivien_register : System.Web.UI.UserControl
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
            // Set Editor
            Globals.CKEditor(txtContent);

            ltlGroup.Text = Language.GetTextByID(424);
            ltlNote.Text = Language.GetTextByID(28);
            ltlRequireGroup.Text = Language.GetTextByID(94);
            ltlRequireTitle.Text = "Nhập đầy đủ các trường bắt buộc";
            rdpPublishDate.SelectedDate = DateTime.Now;
            BtPost.Text = Language.GetTextByID(427);

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
            // Thuộc tính
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
    protected void BtPost_Click(object sender, EventArgs e)
    {
        string sTitle = tbxTitle.Text.Trim();
        string sTomtat = tbxSummary.Text.Trim();
        string sImage = txtFileImages.Value.Trim();
        string scapbac = txtCapbac.Text.Trim();
        string sContent = txtContent.Text.Trim();
        string created = rdpPublishDate.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss");
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
            clsHoivien hoivien = new clsHoivien();
            int status = 1; //Online
            string IDNew = hoivien.InsertHoiVien(sGroup, sTitle, sTomtat, created, status, sImage, scapbac, sContent);
        }
        Response.Redirect(Globals.URLCurrent + "manager.aspx");
    }
}
