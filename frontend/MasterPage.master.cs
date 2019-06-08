using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //
        }
    }
    //khi sử dụng thì các button,các control thao tác với dữ liệu trên web ko thực hiện được
    /*
    protected override void Render(HtmlTextWriter writer)
    {
        System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
        base.Render(htmlWriter);
        string html = stringWriter.ToString();
        int startPoint = html.IndexOf("<input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\"");
        if (startPoint >= 0)
        {
            int endPoint = html.IndexOf("/>", startPoint) + 2;
            string viewstateInput = html.Substring(startPoint, endPoint - startPoint);
            html = html.Remove(startPoint, endPoint - startPoint);
            int formEndStart = html.IndexOf("</form>") - 1;
        }
        writer.Write(html);
    } 
     */
}
