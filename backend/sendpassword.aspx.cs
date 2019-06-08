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
using System.Web.Mail;
public partial class sendpassword : System.Web.UI.Page
{
    public string UrlImages
    {
        get { return Globals.UrlImages; }
    }	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Lab_message.Text = "";
            txtUsername.Attributes.Add("style", "Width:200px");
            txtEmail.Attributes.Add("style", "Width:300px");

            ltlUsername.Text = Language.GetTextByID(201);
            ltlEmail.Text = Language.GetTextByID(202);
        }

    }
    protected void btSend_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (txtUsername.Text.Trim() == "")
            {
                Lab_message.Text = "Đăng nhập tên người dùng !";
                return;
            }
            if (txtEmail.Text.Trim() == "")
            {
                Lab_message.Text = " Nhập địa chỉ thư điện tử!";
                return;
            }
            Member member = new Member();
            DataRow vDr = member.getUsers_Login(txtUsername.Text.Trim());

            if (vDr == null)
            {
                Lab_message.Text = "Tên người dùng không tồn tại !";
                return;
            }
            string vMail = vDr["email"].ToString();
            if (vMail != txtEmail.Text.Trim())
            {
                Lab_message.Text = "Email nhập không đúng !";
                return;
            }
            if (vDr != null)
            {
                // Lay Radom Password qua System
                string vnewpass = System.Guid.NewGuid().ToString();
                vnewpass = vnewpass.Replace("-", string.Empty);
                vnewpass = vnewpass.Substring(0, 6);
                Sendmail sMail = null;
                string NewPass = FormsAuthentication.HashPasswordForStoringInConfigFile(vnewpass.Trim(), "MD5");

                try
                {
                    string sMailContact = string.Empty;
                    sMail = new Sendmail();
                    sMailContact = Globals.MailMaster;
                    sMail.SmtpServer = Globals.MailServer;
                    sMail.SmtpMailFrom = Globals.Gmail;
                    sMail.SmtpUser = Globals.Gmail.Replace("@gmail.com", "");
                    sMail.SmtpPassword = Globals.GmailPassword; ;
                    sMail.SmtpPort = Convert.ToInt32(Globals.MailPort);

                    string sContent = "<font color=#000099>Dùng thông tin sau để đăng nhập vào hệ thống: <B>Administrator</B>." +
                        "<br> Tên người dùng (Username):<font color=red> " + txtUsername.Text.Trim() + "</font>" +
                        "<br> Mật khẩu mới (New password):<font color=red>  " + vnewpass + "</font>" +
                        "<br> Sau khi đăng nhập hãy <B>đổi mật khẩu mới</B> để đảm bảo tính bảo mật của hệ thống ." +
                        "<br> Xin chân thành cảm ơn ! </font>";

                    sMail.SendMail(sMailContact, "Bạn đã thay đổi mật khẩu thành công", sContent, true);
                    Lab_message.Text = "Gửi thành công. Hãy đọc email để lấy thông tin ! ";

                }
                catch (Exception ex)
                {
                    string s = ex.Message.ToString();
                    Lab_message.Text = "Không gửi được. Xem lại địa chỉ Email ! ";
                    return;
                }

                // Update PassNew to database
                string vID = vDr["ID"].ToString();
                member.update_Pass(vID, NewPass.Trim());

                txtUsername.Text = "";
                txtEmail.Text = "";

            }
        }
    }
    protected void btBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");

    }
}
