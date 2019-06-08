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


public partial class member_register : System.Web.UI.UserControl
{
    private static string _Lang
    {
        get { return Globals.CurrentLang; }
    }
    public string UrlImages
    {
        get
        {
            return Globals.UrlImages;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!this.IsPostBack)
        {
            imgIconHeader.ImageUrl = Globals.UrlImages + "icon_menu1.gif"; ;
            ltlHeader.Text = Language.GetTextByID(4);
            ltlNote.Text = Language.GetTextByID(28);

            ltlUsername.Text = "(*) " + Language.GetTextByID(57);
            tbxUsername.Attributes.Add("style", "width:250px");
            ltlRequireUsername.Text = Language.GetTextByID(58);

            ltlPassword.Text = "(*) " + Language.GetTextByID(59);
            tbxPassword.Attributes.Add("style", "width:250px");
            ltlRequirePassword.Text = Language.GetTextByID(60);

            ltlFullname.Text = "(*) " + Language.GetTextByID(61);
            tbxFullname.Attributes.Add("style", "width:250px");
            ltlRequireFullname.Text = Language.GetTextByID(62);

            ltlEmail.Text = "(*) " + Language.GetTextByID(63);
            tbxEmail.Attributes.Add("style", "width:250px");
            ltlRequireEmail.Text = Language.GetTextByID(64);

            ltlTel.Text = Language.GetTextByID(65);
            tbxTel.Attributes.Add("style", "width:250px");

            ltlAddress.Text = Language.GetTextByID(66);
            tbxAddress.Attributes.Add("style", "width:250px");

            ltlJobtitle.Text = Language.GetTextByID(67);
            tbxJobtitle.Attributes.Add("style", "width:250px");

            ltlRole.Text = "(*) " + Language.GetTextByID(69);
            cbxAdministrator.Text = Language.GetTextByID(68);
            ltlRequireRole.Text = Language.GetTextByID(70);

            cbxThanhvien.Text = Language.GetTextByID(3);
            cbxChuyenmuc.Text = Language.GetTextByID(93);
            cbxQTNoidung.Text = Language.GetTextByID(8);
            cbxTochuc.Text = Language.GetTextByID(170);
            //cbxDvThanhvien.Text = Language.GetTextByID(182);
            cbxPostOn.Text = Language.GetTextByID(243);
            btnRegister.Text = Language.GetTextByID(37);
            cbxAdministrator.Attributes.Add("onclick", "AdminCheck('Form1', this)");
            //cbxNews.Attributes.Add("onclick", "ShowTR('" + cbxNews.ClientID + "'," + TRNewsGroup.ClientID + ")");
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        
        int i = 0;
			string sUsername = tbxUsername.Text.Trim();
			if(sUsername == "")
			{
				i ++;
				ltlRequireUsername.Visible = true;
			}
			else
			{
				ltlRequireUsername.Visible = false;
			}
			string sPassword = tbxPassword.Text.Trim();
			if(sPassword == "")
			{
				i ++;
				ltlRequirePassword.Visible = true;
			}
			else
			{
				ltlRequirePassword.Visible = false;
			}
			string sFullname = tbxFullname.Text.Trim();
			if(sFullname == "")
			{
				i ++;
				ltlRequireFullname.Visible = true;
			}
			else
			{
				ltlRequireFullname.Visible = false;
			}
			string sEmail = tbxEmail.Text.Trim();
			if(sEmail == "")
			{
				i ++;
				ltlRequireEmail.Visible = true;
			}
			else
			{
				ltlRequireEmail.Visible = false;
			}
			//Not check
            if (!cbxAdministrator.Checked && !cbxChuyenmuc.Checked && !cbxThanhvien.Checked && !cbxQTNoidung.Checked && !cbxTochuc.Checked && !cbxPostOn.Checked)
			{
				i ++;
				ltlRequireRole.Visible = true;
			}
			else
				ltlRequireRole.Visible = false;

			if(i>0)
				return;
			Member member = new Member();
			if(member.CheckExist(sUsername))
			{
				ltlRequireUsername.Visible = true;
				ltlRequireUsername.Text = Language.GetTextByID(72);
				return;
			}
			string vPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(sPassword,"MD5");
			member.Insert(sUsername, vPassword, sFullname, tbxTel.Text.Trim(), sEmail, tbxAddress.Text.Trim(),tbxJobtitle.Text.Trim());
			
           	Memberinfo memberinfo = new Memberinfo();
			memberinfo.Name = sUsername;
			//Add property
            if (cbxAdministrator.Checked && cbxAdministrator.Enabled)
            {
                memberinfo.SetConfigItem("Administrator", "IsAdministrator_" + _Lang, "true");
                memberinfo.SetConfigItem("Administrator", "IsAdministrator_en", "true");
            }
            if (cbxChuyenmuc.Checked && cbxChuyenmuc.Enabled)
                memberinfo.SetConfigItem("Chuyenmuc", "IsChuyenmuc_" + _Lang, "true");

            if (cbxThanhvien.Checked && cbxThanhvien.Enabled)
                memberinfo.SetConfigItem("Thanhvien", "IsThanhvien_" + _Lang, "true");

            if (cbxQTNoidung.Checked && cbxQTNoidung.Enabled)
                memberinfo.SetConfigItem("News", "IsNews_" + _Lang, "true");

            if(cbxTochuc.Checked && cbxTochuc.Enabled)
                memberinfo.SetConfigItem("Tochuc", "IsTochuc_" + _Lang, "true");

            //if (cbxDvThanhvien.Checked && cbxDvThanhvien.Enabled)
                //memberinfo.SetConfigItem("DonViThanhVien", "IsDonViThanhVien_" + _Lang, "true");

            if (cbxPostOn.Checked && cbxPostOn.Enabled)
                memberinfo.SetConfigItem("PostOn", "IsPostOn_" + _Lang, "true");

			Response.Redirect(Globals.URLCurrent+"manager.aspx");            

		}
		protected void RegisterAllScript()
		{
			string strScript = String.Empty;
			strScript = @"<script language=""javascript"">
			<!--
				function AdminCheck(formName, obj) {
					if (obj.checked) {
                        
                       	CheckDisabled(formName, ""cbxChuyenmuc"",""true"");
						CheckDisabled(formName, ""cbxThanhvien"",""true"");
                        CheckDisabled(formName, ""cbxQTNoidung"",""true"");
                        CheckDisabled(formName, ""cbxTochuc"",""true"");
                        CheckDisabled(formName, ""cbxPostOn"",""true"");

					} else {
						CheckDisabled(formName, ""cbxChuyenmuc"",""false"");
						CheckDisabled(formName, ""cbxThanhvien"",""false"");
                        CheckDisabled(formName, ""cbxQTNoidung"",""false"");
                        CheckDisabled(formName, ""cbxTochuc"",""false"");	
                        CheckDisabled(formName, ""cbxPostOn"",""false"");
					}
				}
				
				function OnLoaded(formName, obj1){
					onBodyLoad();
					var b = false;
					eval(""var theform = document."" + formName + "";"");
					var len = theform.elements.length;
					for (var j = 0; j < len; j++) {
						var e = theform.elements[j];
						if (e.name.indexOf(obj1)>=0) {
							if(e.checked == true && e.disabled == false){
							b = true;break;
							}
						}
					}
					if (b==true) {
						CheckDisabled(formName, ""cbxChuyenmuc"",""true"");
						CheckDisabled(formName, ""cbxThanhvien"",""true"");
                        CheckDisabled(formName, ""cbxQTNoidung"",""true"");
                        CheckDisabled(formName, ""cbxTochuc"",""true"");
                        CheckDisabled(formName, ""cbxPostOn"",""true"");	
						
					} else {
						CheckDisabled(formName, ""cbxChuyenmuc"",""false"");
						CheckDisabled(formName, ""cbxThanhvien"",""false"");
                        CheckDisabled(formName, ""cbxQTNoidung"",""false"");
                        CheckDisabled(formName, ""cbxTochuc"",""false"");
                        CheckDisabled(formName, ""cbxPostOn"",""false"");
					}
				}
			//-->
			</script>";
			if(!this.Page.IsClientScriptBlockRegistered("EnabledJavaScript"))
			{
				this.Page.RegisterClientScriptBlock("EnabledJavaScript" , strScript);
			}
		}
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			RegisterAllScript();
		}
	

    }