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

public partial class groups_register : System.Web.UI.UserControl
{
    private Groups group = null;
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        group = new Groups();
        if (!this.IsPostBack)
        {
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif";
            ltlHeader.Text = Language.GetTextByID(92);
            ltlNote.Text = Language.GetTextByID(28);
            ltlRequireTitle.Text = Language.GetTextByID(30);
            btnRegister.Text = Language.GetTextByID(37);
            tbxLink.Text = "http://";

            txtFileImages.Attributes.Add("style", "Width:250px");
            tbxWidth.Attributes.Add("style", "Width:50px");
            tbxHeight.Attributes.Add("style", "Width:50px");

            ltlWidth.Text = Language.GetTextByID(231);
            ltlHeight.Text = Language.GetTextByID(232);
            ltlViewSize.Text = Language.GetTextByID(230);
            imgSelect.Attributes.Add("style", "cursor:hand; cursor:pointer");
            imgSelect.Src = Globals.UrlImages + "folder.gif";
            imgSelect.Attributes.Add("onclick", "SelectFile('" + txtFileImages.ClientID + "')");
            //Trees view           
            treeview1.ShowCheckBoxes = TreeNodeTypes.All;
            treeview1.ShowLines = true;
            //treeview1.ShowExpandCollapse = false;
            treeview1.ExpandDepth = 0;
            BildRoot(treeview1);
        }
    }
    protected void BildRoot(TreeView treeview)
    {
        DataTable dt = group.GetChildGroup(string.Empty);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = dr["ptitle"].ToString();
                newNode.Value = dr["pid"].ToString();
                bool KiemTra = group.CheckChild(dr["pid"].ToString());
                if (KiemTra)
                {
                    newNode.PopulateOnDemand = true;
                    newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                }
                else
                {
                    newNode.PopulateOnDemand = false;
                    newNode.SelectAction = TreeNodeSelectAction.Select;
                }
                // them tiep node con vao;
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
        DataTable dt = group.GetChildGroup(node.Value);
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
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        int i = 0;
        string sTitle = tbxTitle.Text.Trim();
        if (sTitle == "")
        {
            i++;
            ltlRequireTitle.Visible = true;
        }
        else
        {
            ltlRequireTitle.Visible = false;
        }
        if (i > 0)
            return;
        string sLink = tbxLink.Text.Trim();
        if (sLink == "http://")
            sLink = string.Empty;

        string sFilename = txtFileImages.Text;
        string vwidth = tbxWidth.Text.Trim();
        string vheight = tbxHeight.Text.Trim();
        int ikind = Convert.ToInt32(dropkind.SelectedItem.Value.Trim());
        string sShortlink = txtShotlink.Value.Trim();
        string sTitleMeta = txttitlemeta.Text.Trim();
        string sKetwords = txtkeywords.Text.Trim();
        string sDescription = txtDescription.Text.Trim();
        int sluongtin = Convert.ToInt32(dropSL.SelectedItem.Value.Trim());
        //Add Trees
        //string parentid = ParentGroup.Selection;
        string sRet = string.Empty;
        group = new Groups();
        foreach (TreeNode node in treeview1.CheckedNodes)
        {
            sRet = node.Value;
            group.Insert(sRet, sTitle, sLink, tbxSummary.Text.Trim(), sFilename.Trim(), vwidth.Trim(), vheight.Trim(), sKetwords, sDescription, ikind, sShortlink, sTitleMeta, sluongtin);
        }
        if (sRet == string.Empty || sRet == null || sRet == "")
        {
            group.Insert(sRet, sTitle, sLink, tbxSummary.Text.Trim(), sFilename.Trim(), vwidth.Trim(), vheight.Trim(), sKetwords, sDescription, ikind, sShortlink, sTitleMeta, sluongtin);
        }
        Response.Redirect(Globals.URLCurrent + "manager.aspx");
    }
    protected void treeview1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        PopulateCategories(e.Node);
    }
}
