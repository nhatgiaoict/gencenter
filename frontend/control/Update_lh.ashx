<%@ WebHandler Language="C#" Class="Update_lh" %>
using System;
using System.Web;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

public class Update_lh : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        // var dataString = "namelh=" + nameLh + "&emaillh=" + emailLh + "&tellh=" + telLh + "&noidunglh=" + contentLh + "&code=" + captchaLh;
        context.Response.ContentType = "text/plain";
        string name = context.Request.Params["namelh"];
        string email = context.Request.Params["emaillh"];
        string tel = context.Request.Params["tellh"];
        string noidung = context.Request.Params["noidunglh"];
        string code = context.Request.Params["code"];
        string Actu = SendMailLH(name, code, email, tel, noidung);
        context.Response.Write(Actu);
    }
    public bool IsReusable {get {return false;}}
    public string SendMailLH(string name, string Code, string email, string tel, string noidung)
    {
        string f = string.Empty;
        clsSendmail sMail = new clsSendmail();
        sMail.SmtpServer = "smtp.gmail.com";
        sMail.SmtpMailFrom = "trongarc82@gmail.com";
        sMail.SmtpUser = "trongarc82";
        sMail.SmtpPassword = "trong141982";
        sMail.SmtpPort = 587;
        string sContent = "<b>Thông tin liên hệ của khách hàng qua hệ thống website maunhadep365.vn công ty Rubik Architect</b>.<br>Thư liên hệ được gửi từ: <br><b>Địa chỉ mail </b> :" + email + "<br>";
        sContent += "<b>Họ tên  </b> : " + name + "<br>";
        sContent += "<b>Điện thoại  </b> : " + tel + "<br>";
        sContent += "<b>Email  </b> : " + email + "<br>";
        sContent += "<b>Nội Dung  </b> : " + noidung + "";
        sMail.SendMail(Globals.MailContact, "Thư liên hệ tới Công ty", sContent, true);
        f = "0";
        if (HttpContext.Current.Session["RadomCode"].ToString().ToLower() == Code.ToLower())
        {
            sMail.SendMail(Globals.MailContact, "Thư liên hệ tới Công ty", sContent, true);
            f = "0";
        }
        else
        {
            f = "1";
        }
        return f;
    }
}