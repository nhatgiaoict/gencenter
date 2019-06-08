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

public partial class Boloc_uc_managerthuoctinh : System.Web.UI.UserControl
{
    private Groups group = null;
    private cls_thuoctinh clsThuoctinh = null;
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
        group = new Groups(1);
        clsThuoctinh = new cls_thuoctinh();
        if (!this.IsPostBack)
        {
            //Trees view  
            treeview1.ShowCheckBoxes = TreeNodeTypes.All;
            treeview1.ShowLines = true;
            treeview1.ExpandDepth = 1;
            BildRoot(treeview1);
           // Setcontrol
            SetControl(false);
        }
    }
    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // Itemdata
    }
    protected void rptList_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        // created 
    }
    protected void treeview1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        PopulateCategories(e.Node);
    }
    protected void SetControl(bool status)
    {
        txtTitle.Enabled = status;
        treeview1.Enabled = status;
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        txtTitle.Text = "";
        SetControl(true);
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        //Save
        string sTitle = txtTitle.Text.Trim();
        // treeview
        string sGroup = string.Empty;
        foreach (TreeNode node in treeview1.CheckedNodes)
        {
            sGroup += node.Value + ",";
        }
        if (sGroup == "" || sGroup == string.Empty || sGroup == null)
        {
            ltlRequireGroup.Visible = true;
            return;
        }
        else ltlRequireGroup.Visible = false;
        // insert nhóm thuộc tính vào các chuyên mục
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
}
