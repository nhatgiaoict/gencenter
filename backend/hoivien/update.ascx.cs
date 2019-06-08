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

public partial class uchoivien_update : System.Web.UI.UserControl
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
            Response.Redirect(Globals.URLCurrent + "manager.aspx");
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
            //txtFileImages.Attributes.Add("style", "Width:250px");
            ltlNote.Text = Language.GetTextByID(28);
            ltlRequireTitle.Text = "Nhập đầy đủ các trường bắt buộc !"; //Language.GetTextByID(30);
            ltlGroup.Text = Language.GetTextByID(91);
            ltlRequireGroup.Text = Language.GetTextByID(94);
            btnRegister.Text = Language.GetTextByID(37);

            clsHoivien hoivien = new clsHoivien();
            DataRow dr = hoivien.GetInfo(id);

            if (dr == null) Response.Redirect(Globals.URLCurrent + "manager.aspx");

            tbxTitle.Text = dr["title"].ToString();
            tbxSummary.Text = dr["summary"].ToString();
            
            sGroupID = dr["groupid"].ToString();

            tbxSummary.Text = dr["summary"].ToString();
            txtFileImages.Value = dr["logo"].ToString();
            txtCapbac.Text = dr["capbac"].ToString();
            txtContent.Text = dr["Content"].ToString().Trim();
            rdpPublishDate.SelectedDate = Convert.ToDateTime(dr["created"].ToString().Trim());

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
        string sTomtat = tbxSummary.Text.Trim();
        string sImage = txtFileImages.Value.Trim();
        string sCapbac = txtCapbac.Text.Trim();
        string sContent = txtContent.Text.Trim();
        string created = rdpPublishDate.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss");
        //Treeview
        group = new Groups();
        string sGroup = string.Empty;
        foreach (TreeNode node in treeview1.CheckedNodes)
        {
            sGroup = node.Value;
        }
        if (sGroup == "" || sGroup == string.Empty || sGroup == null)
        { ltlRequireGroup.Visible = true; return; }
        else ltlRequireGroup.Visible = false;

        clsHoivien hoivien = new clsHoivien();
        hoivien.UpdateHoivien(id, sGroup, sTitle, sTomtat, created, sImage, sCapbac, sContent);
        
        Response.Redirect(Globals.URLCurrent + "manager.aspx");
    }

    protected void treeview1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        PopulateCategories(e.Node);
    }
}







//using System;
//using System.Data;
//using System.Configuration;
//using System.Collections;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using uss.utils;

//public partial class hoivien_update : System.Web.UI.UserControl
//{
//    private UssUrl sUrl = null;
//    private string id
//    {
//        get
//        {
//            sUrl = new UssUrl();
//            return sUrl.GetParam("id");
//        }
//    }
//    private DataRow dr
//    {
//        get
//        {
//            Member member = new Member();
//            return member.GetInfo(id);

//        }
//    }
//    private static string _Lang
//    {
//        get { return Globals.CurrentLang; }
//    }
//    public string UrlImages
//    {
//        get
//        {
//            return Globals.UrlImages;
//        }
//    }
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (id == "" || id == string.Empty || id == null)
//            Response.Redirect(Globals.URLCurrent + "manager.aspx");

//        if (!this.IsPostBack)
//        {
//            if (dr == null)
//                Response.Redirect(Globals.URLCurrent + "manager.aspx");

//            ltlMember.Text = dr["fullname"].ToString() + " (" + dr["username"].ToString() + ")";
//            string sUserName = dr["username"].ToString();

//            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif"; ;
//            ltlHeader.Text = Language.GetTextByID(90);
//            ltlNote.Text = Language.GetTextByID(28);

//            ltlRole.Text = "(*) " + Language.GetTextByID(69);
//            ltlRequireRole.Text = Language.GetTextByID(70);
//            btnRegister.Text = Language.GetTextByID(37);

//            cbxAdministrator.Text = Language.GetTextByID(68);
//            cbxChuyenmuc.Text = Language.GetTextByID(93);
//            cbxThanhvien.Text = Language.GetTextByID(3);
//            cbxQTNoidung.Text = Language.GetTextByID(8);
//            cbxPostOn.Text = Language.GetTextByID(243);
         
//            if (Membertask.IsAdministrator(sUserName))
//                cbxAdministrator.Checked = true;
//            if (Membertask.IsThanhvien(sUserName) != string.Empty)
//                cbxThanhvien.Checked = true;
//            if (Membertask.IsChuyenmuc(sUserName) != string.Empty)
//                cbxChuyenmuc.Checked = true;
//            if (Membertask.IsNewsTask(sUserName) != string.Empty)
//                cbxQTNoidung.Checked = true;
//            if (Membertask.IsPostOn(sUserName) != string.Empty)
//                cbxPostOn.Checked = true;
//            cbxAdministrator.Attributes.Add("onclick", "AdminCheck('Form1', this)");
//        }
//    }
//    protected void btnRegister_Click(object sender, EventArgs e)
//    {
//        int i = 0;
//        //Not check
//        if (!cbxAdministrator.Checked && !cbxChuyenmuc.Checked && !cbxThanhvien.Checked && !cbxQTNoidung.Checked && !cbxPostOn.Checked)
//        {
//            i++;
//            ltlRequireRole.Visible = true;
//        }
//        else
//            ltlRequireRole.Visible = false;

//        if (i > 0)
//            return;
//        Memberinfo memberinfo = new Memberinfo();
//        memberinfo.Name = dr["username"].ToString();
//            //Add property
//            if(cbxAdministrator.Checked && cbxAdministrator.Enabled)
//                memberinfo.SetConfigItem("Administrator","IsAdministrator_"+_Lang,"true");
//            else
//                memberinfo.SetConfigItem("Administrator", "IsAdministrator_" + _Lang, "false");

//            if (cbxChuyenmuc.Checked && cbxChuyenmuc.Enabled)
//                memberinfo.SetConfigItem("Chuyenmuc", "IsChuyenmuc_" + _Lang, "true");
//            else
//                memberinfo.SetConfigItem("Chuyenmuc", "IsChuyenmuc_" + _Lang, string.Empty);

//            if (cbxThanhvien.Checked && cbxThanhvien.Enabled)
//                memberinfo.SetConfigItem("Thanhvien", "IsThanhvien_" + _Lang, "true");
//            else
//                memberinfo.SetConfigItem("Thanhvien", "IsThanhvien_" + _Lang, string.Empty);

//            if (cbxQTNoidung.Checked && cbxQTNoidung.Enabled)
//                memberinfo.SetConfigItem("News", "IsNews_" + _Lang, "true");
//            else
//                memberinfo.SetConfigItem("News", "IsNews_" + _Lang, string.Empty);
//            if (cbxPostOn.Checked && cbxPostOn.Enabled)
//                    memberinfo.SetConfigItem("PostOn", "IsPostOn_" + _Lang, "true");
//                else
//                    memberinfo.SetConfigItem("PostOn", "IsPostOn_" + _Lang, string.Empty);

//            Response.Redirect(Globals.URLCurrent+"manager.aspx");
//        }
//    protected void RegisterAllScript()
//    {
//        string strScript = String.Empty;
//        strScript = @"<script language=""javascript"">
//			<!--
//				function AdminCheck(formName, obj) {
//                    
//					if (obj.checked) {
//                        
//                       	CheckDisabled(formName, ""cbxChuyenmuc"",""true"");
//						CheckDisabled(formName, ""cbxThanhvien"",""true"");
//                        CheckDisabled(formName, ""cbxQTNoidung"",""true"");
//                        CheckDisabled(formName, ""cbxTochuc"",""true"");
//                        CheckDisabled(formName, ""cbxPostOn"",""true"");
//
//					} else {
//						CheckDisabled(formName, ""cbxChuyenmuc"",""false"");
//						CheckDisabled(formName, ""cbxThanhvien"",""false"");
//                        CheckDisabled(formName, ""cbxQTNoidung"",""false"");
//                        CheckDisabled(formName, ""cbxTochuc"",""false"");	
//                        CheckDisabled(formName, ""cbxPostOn"",""false""); 
//					}
//					
//				}
//				
//				function OnLoaded(formName, obj1){
//					//onBodyLoad();
//					var b = false;
//					eval(""var theform = document."" + formName + "";"");
//					var len = theform.elements.length;
//					for (var j = 0; j < len; j++) {
//						var e = theform.elements[j];
//						if (e.name.indexOf(obj1)>=0) {
//							if(e.checked == true && e.disabled == false){
//							b = true;break;
//							}
//						}
//					}
//					if (b==true) {
//						
//						CheckDisabled(formName, ""cbxChuyenmuc"",""true"");
//						CheckDisabled(formName, ""cbxThanhvien"",""true"");
//                        CheckDisabled(formName, ""cbxQTNoidung"",""true"");
//                        CheckDisabled(formName, ""cbxTochuc"",""true"");
//                        CheckDisabled(formName, ""cbxPostOn"",""true""); 
//					
//					} else {
//						
//						CheckDisabled(formName, ""cbxChuyenmuc"",""false"");
//						CheckDisabled(formName, ""cbxThanhvien"",""false"");
//                        CheckDisabled(formName, ""cbxQTNoidung"",""false"");
//                        CheckDisabled(formName, ""cbxTochuc"",""false"");
//                        CheckDisabled(formName, ""cbxPostOn"",""false""); 
//						
//					}
//			}
//			//-->
//			</script>";
//        if (!this.Page.IsClientScriptBlockRegistered("EnabledJavaScript"))
//        {
//            this.Page.RegisterClientScriptBlock("EnabledJavaScript", strScript);
//        }
//    }
//    protected override void CreateChildControls()
//    {
//        base.CreateChildControls();
//        RegisterAllScript();
//    }

//}