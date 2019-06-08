<%@ WebHandler Language="C#" Class="Datmon_hangmoi" %>
using System;
using System.Web;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Web.UI;
using System.Data;
using CMDU;
public class Datmon_hangmoi : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Email = context.Request.Params["Email"];
        string CapCha = context.Request.Params["Code"];
        string nPhone = context.Request.Params["Phone"];
        string Hoten = context.Request.Params["Hoten"];
        string Diachi = context.Request.Params["Diachi"];
        string Ac = SendMailJquery(CapCha, Email, nPhone, Hoten, Diachi);
        context.Response.Write(Ac);
    }
    public bool IsReusable { get { return false; } }
    public string SendMailJquery(string Code, string Email,string Phone, string Hoten, string Diachi)
    {
        News sNew = new News();
        //clsComment clsCom = new clsComment();
        string f = string.Empty;
        if (HttpContext.Current.Session["RadomCode"].ToString().ToLower() == Code.ToLower())
        {
            // Send mail cho người mua:
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Data.DataTable dt = (DataTable)HttpContext.Current.Session["cardluu"]; // sNew.GetCart();
            //System.Data.DataTable dtSL = (DataTable)HttpContext.Current.Session["DLSL"];
            System.Net.Configuration.SmtpSection cfg = (System.Net.Configuration.SmtpSection)System.Configuration.ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            clsSendmail sMail = new clsSendmail();
            sMail.SmtpServer = cfg.Network.Host;
            sMail.SmtpMailFrom = cfg.From;
            sMail.SmtpUser = cfg.Network.UserName;
            sMail.SmtpPassword = cfg.Network.Password;
            sMail.SmtpPort = cfg.Network.Port;
            sb.Append("<div>");
            sb.Append("<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"1\" style=\"background: none repeat scroll 0 0 #FFFFFF;border: 1px solid #666666;width: 96%;\">");
            sb.Append("<tbody><tr style=\"background: none repeat scroll 0 0 #AADC7D;\" >");
            sb.Append("<td width=\"6%\" class=\"td1\" style=\"text-align:center; font-weight:bold;border-top: 1px solid #666666; padding: 8px;\">STT</td>");
            sb.Append("<td width=\"35%\" class=\"td1\" style=\"text-align:center; font-weight:bold;border-top: 1px solid #666666; padding: 8px;\">Tên sản phâm</td>");
            sb.Append("<td width=\"15%\" class=\"td1\" style=\"text-align:center; font-weight:bold;border-top: 1px solid #666666; padding: 8px;\">Số lượng</td>");
            sb.Append("<td width=\"9%\" class=\"td1\" style=\"text-align:center; font-weight:bold;border-top: 1px solid #666666; padding: 8px;\">Đơn giá</td>");
            sb.Append("<td width=\"15%\" class=\"td1\" style=\"text-align:center; font-weight:bold;border-top: 1px solid #666666; padding: 8px;\">Thành tiền</td>");
            sb.Append("</tr>");
            
            if (dt != null && dt.Rows.Count > 0)
            {
                double sTong = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align:center;\">" + Convert.ToInt32(i + 1) + "</td>");
                    sb.Append("<td style=\"text-align:center;\"><a href=\"http://danamthanh.com/" + dt.Rows[i]["shortlink"].ToString() + "\">" + dt.Rows[i]["title"].ToString() + "</a></td>");
                    sb.Append("<td style=\"text-align:center;\">" + dt.Rows[i]["soluong"].ToString() + "</td>");
                    sb.Append("<td style=\"text-align:center;\">" + dt.Rows[i]["giaban"].ToString() + "</td>");
                    if (dt.Rows[i]["giaban"].ToString() != "call")
                    {
                        double gia = Convert.ToDouble(dt.Rows[i]["giaban"].ToString());
                        double soluong = Convert.ToDouble(dt.Rows[i]["soluong"].ToString());
                        sb.Append("<td style=\"text-align:center;\">" + (gia * soluong).ToString("c0").Replace("$", "") + "</td>");
                        sb.Append("</tr>");
                        sTong += (gia * soluong);
                    }
                    else
                    {
                        sb.Append("<td style=\"text-align:center;\">Liên hệ</td>");
                        sb.Append("</tr>");
                    }
                }
                sb.Append("<tr><td colspan='3' style=\"text-align:center\">Tổng tiền</td><td colspan='2' align=\"center\">" + sTong.ToString("c0").Replace("$", "") + "</td>VNĐ</tr></tbody></table>");
                sb.Append("</div>");
            }

            string BodyNba = string.Empty;
            BodyNba += "<b>" + Language.GetTextByID(535) + "</b><br />";
            BodyNba += "<b>Email</b>:" + Email + "<br />";
            BodyNba += "<b>Họ tên</b>:" + Hoten + "<br />";
            BodyNba += "<b>Địa chỉ</b>:" + Diachi + "<br />";
            BodyNba += "<b>Điện thoại</b>:" + Phone + "<br /><br />";
            
            BodyNba += sb.ToString();                        
            // Gui cho người mua
            sMail.SendMail(Email, Language.GetTextByID(534), BodyNba, true);
            string BodyAdmin = string.Empty; ;
            BodyAdmin += "<b>Họ tên</b>:" + Hoten + "<br />";
            BodyAdmin += "<b>Email</b>:" + Email + "<br />";
            BodyAdmin += "<b>Địa chỉ</b>:" + Diachi + "<br />";
            BodyAdmin += "<b>Điện thoại</b>:" + Phone + "<br /><br />";
            BodyAdmin += sb.ToString();
            // gui cho admin
            sMail.SendMail(Globals.MailContact.ToString(), "Thư đặt hàng từ hệ thống website danamthanh.com", BodyAdmin, true);
            f = "0";
            HttpContext.Current.Session.Remove("cardluu");
        }
        else
        {
            f = "1";
        }
        return f;
    }
    public string GetDonViV(string donvi)
    {
        string _result = "";
        string[] ArrayDV = donvi.Split(new Char[] { '/' });
        _result = ArrayDV[0].ToString();
        if (_result == "")
        {
            _result = "&nbsp;";
        }
        return _result;
    }
    public string GetDonViE(string donvi)
    {
        string _result = "";
        string[] ArrayDV = donvi.Split(new Char[] { '/' });
        if (ArrayDV.Length > 1)
        {
            _result = ArrayDV[1].ToString();
        }
        else
        {
            _result = "&nbsp;";
        }
        return _result;
    }
}

