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

public partial class news_update : System.Web.UI.UserControl
{
    private UssUrl sUrl = null;
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    private string id
    {
        get
        {
            sUrl = new UssUrl();
            return sUrl.GetParam("id");
        }
    }
    
    private static int test_lang = 0;
    private Groups group = null;
    private  static int status ;
    private string sGroupID = string.Empty;
    private string ParentID
    {
        get
        {
            if (sGroupID == "" || sGroupID == string.Empty || sGroupID == "")
                return string.Empty;
            else if (sGroupID.Length > Groups._nLength)
                return sGroupID.Substring(0, Groups._nLength);
            else
                return string.Empty;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (id == "" || id == null || id == string.Empty)
            Response.Redirect(Globals.URLCurrent + "listbaiviet.aspx");
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
            Globals.CKEditor(txtContent);
            txtFileImages.Attributes.Add("style", "Width:250px");
            ltlNote.Text = Language.GetTextByID(28);
            ltlTitle.Text = "(*) " + Language.GetTextByID(29);
            ltlRequireTitle.Text = Language.GetTextByID(30);
            ltlGroup.Text = Language.GetTextByID(91);
            ltlRequireGroup.Text = Language.GetTextByID(94);
            ltlSummary.Text = Language.GetTextByID(31);
            ltlRequireSummary.Text = Language.GetTextByID(32);
            tbxSummary.Attributes.Add("style", "width:330px;height:160px");
            ltlContent.Text = "(*) " + Language.GetTextByID(35);
            ltlRequireContent.Text = Language.GetTextByID(36);
            btnRegister.Text = Language.GetTextByID(37);

            ltlGhichu.Text = Language.GetTextByID(169);
            txtGhichu.Attributes.Add("style", "width:350px;height:50px");

            News news = new News();
            DataRow dr = news.GetInfo(id);
            if (dr == null)
                Response.Redirect(Globals.URLCurrent + "listbaiviet.aspx");
            tbxTitle.Text = dr["title"].ToString();
            tbxSummary.Text = dr["summary"].ToString();

            sGroupID = dr["groupid"].ToString();
            string sGetPath = dr["fimage"].ToString();
            if (sGetPath.Trim() != string.Empty)
            {
                string cImages = sGetPath.Substring(0, 4);
                if (cImages.Trim() != "data")
                {
                    imgLogo.ImageUrl = sGetPath;
                }
                else
                {
                    imgLogo.ImageUrl = Globals.UrlRootImages + sGetPath;
                }
                txtFileImages.Value = sGetPath;
            }
            else
            {
                imgLogo.Visible = false;
            }
            //tbxWidth.Text = dr["fwidth"].ToString();
            //tbxHeight.Text = dr["fheight"].ToString();
            txtShotlink.Value = dr["shortlink"].ToString();
            txtTitleMeta.Text = dr["titlemeta"].ToString();
            txtDescripton.Text = dr["Description"].ToString();
            txtTitleMeta.Text = dr["titlemeta"].ToString();
            txtShotlink.Value = dr["shortlink"].ToString();
            txtkeyword.Text = dr["keywords"].ToString();
            status = Convert.ToInt32(dr["status"].ToString());
            txtContent.Text = news.GetContent(id);

            //Trees view           
            treeview1.ShowCheckBoxes = TreeNodeTypes.All;
            treeview1.ShowLines = true;
            treeview1.ExpandAll();
            BildRoot(treeview1);

        }
    }
    protected void BildRoot(TreeView treeview)
    {
        string sgroup = "," + sGroupID + ",";
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
        string sgroup = "," + sGroupID + ",";
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
        string sTitle = tbxTitle.Text.Trim();
        if (sTitle == "" || sTitle == string.Empty)
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
            sGroup = node.Value;
        }
        if (sGroup == "" || sGroup == string.Empty || sGroup == null)
        {
            ltlRequireGroup.Visible = true;
            return;
        }
        else
            ltlRequireGroup.Visible = false;

        string sFilename = txtFileImages.Value;
        string vwidth = "200";// tbxWidth.Text.Trim();
        string vheigth = "200";// tbxHeight.Text.Trim();
        string sShortLink = txtShotlink.Value.Trim();
        string sTitleMeta = txtTitleMeta.Text.Trim();
        string skeyword = txtkeyword.Text.Trim();
        string sDescription = txtDescripton.Text.Trim();


        News news = new News();
        news.Update(id, sTitle, sGroup, sSummary, sContent, sFilename, sTitleMeta, skeyword, sDescription, sShortLink);

        //Save log
        string datenow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Ghichu = "";
        if (test_lang != 2)
        {
            Ghichu = "Đã sửa: " + txtGhichu.Text.Trim();
        }
        else
        {
            Ghichu = "Editted: " + txtGhichu.Text.Trim();
        }
        if (status == 0 || status == 1)
        {
            Logfile log = new Logfile();
            log.insertDatalog(id, datenow, status, Ghichu.Trim());
            Response.Redirect(Globals.URLCurrent + "listbaiviet.aspx");
        }
    }
    protected void treeview1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        PopulateCategories(e.Node);
    }
}
