using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.IO;
/// <summary>
/// Summary description for Slideshow
/// </summary>
public class Slideshow
{
    private string sFile = HttpContext.Current.Server.MapPath(UrlSlideXML + "Slideshow.xml");

    public Slideshow()
    {
        if (!File.Exists(sFile))
        {
            DynamicFile file = new DynamicFile();
            string sContent = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><Slideshow></Slideshow>";
            file.CreateFile(sFile, sContent);
        }
    }
    public static string UrlSlideXML
    {
        get
        {
            return Globals.UrlData + "ListFile/xml/";
        }
    }
    public static string UrlSlideImage
    {
        get
        {
            return Globals.UrlData + "ListFile/img/";
        }
    }


    public void InsertSlide(string title, string filename, string summary)
    {
        XmlDocument xmlDoc = new XmlDocument();
        if (File.Exists(sFile))
        {
            xmlDoc.Load(sFile);

            XmlElement elmRoot = xmlDoc.DocumentElement;
            XmlElement elmNew = xmlDoc.CreateElement("Slide"); //root
            elmRoot.AppendChild(elmNew);
            elmRoot = xmlDoc.DocumentElement;

            string id = CreateRandomID(6);
            elmNew = xmlDoc.CreateElement("id");
            XmlText sXMLText = xmlDoc.CreateTextNode(id);
            elmRoot.LastChild.AppendChild(elmNew);
            elmRoot.LastChild.LastChild.AppendChild(sXMLText);

            //tạo title
            elmNew = xmlDoc.CreateElement("title");
            XmlText sXMLText1 = xmlDoc.CreateTextNode(title);
            elmRoot.LastChild.AppendChild(elmNew);
            elmRoot.LastChild.LastChild.AppendChild(sXMLText1);

            //tạo filename
            elmNew = xmlDoc.CreateElement("filename");
            XmlText sXMLText2 = xmlDoc.CreateTextNode(title);
            elmRoot.LastChild.AppendChild(elmNew);
            elmRoot.LastChild.LastChild.AppendChild(sXMLText2);
            //
            //tạo summary
            elmNew = xmlDoc.CreateElement("content");
            XmlText sXMLText3 = xmlDoc.CreateTextNode(summary);
            elmRoot.LastChild.AppendChild(elmNew);
            elmRoot.LastChild.LastChild.AppendChild(sXMLText3);

            //tạo status
            elmNew = xmlDoc.CreateElement("status");
            XmlText sXMLText4 = xmlDoc.CreateTextNode("0");
            elmRoot.LastChild.AppendChild(elmNew);
            elmRoot.LastChild.LastChild.AppendChild(sXMLText4);

            xmlDoc.Save(sFile);
        }

    }
    private void UpdateSlide(string id, string title, string filename, string summary)
    {
        XmlDocument xmldoc = new XmlDocument();
        XmlNode xmlnode = xmldoc.DocumentElement.ChildNodes.Item(Convert.ToInt32(id));
        xmlnode["id"].InnerText = id;
        xmlnode["title"].InnerText = title;
        xmlnode["filename"].InnerText = filename;
        xmlnode["summary"].InnerText = summary;
        xmldoc.Save(sFile);
    }
    public void Delete(string id)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(sFile);
        XmlNode xmlnode = doc.DocumentElement.ChildNodes.Item(Convert.ToInt32(id) - 1);
        xmlnode.ParentNode.RemoveChild(xmlnode);
        doc.Save(sFile);
    }
    public DataTable GetSlide()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds.ReadXml(sFile);
        if (ds != null && ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
        }
        if (dt != null && dt.Rows.Count > 0)
            return dt;
        return null;
    }
    public void SetStatus(string id)
    {
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(sFile);
        int status = GetStatus(id);
        if (status == 0)
        {
            XmlNode xmlnode = xmldoc.DocumentElement.ChildNodes.Item(Convert.ToInt32(id) - 1);
            xmlnode["status"].InnerText = "1";
        }
        else
        {
            XmlNode xmlnode = xmldoc.DocumentElement.ChildNodes.Item(Convert.ToInt32(id) - 1);
            xmlnode["status"].InnerText = "0";
        }
        xmldoc.Save(sFile);
    }
    private int GetStatus(string id)
    {
        int sResult = 0;
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(sFile);
        XmlNodeList xmlnodelist = xmldoc.DocumentElement.ChildNodes;
        XmlNode xmlnode = xmlnodelist.Item(Convert.ToInt32(id) - 1);
        sResult = Convert.ToInt32(xmlnode["status"].InnerText);
        return sResult;
    }
    private string CreateRandomID(int Length)
    {
        string _allowedChars = "0123456789";
        Random randNum = new Random();
        char[] chars = new char[Length];
        int allowedCharCount = _allowedChars.Length;

        for (int i = 0; i < Length; i++)
        {
            chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        }
        return new string(chars);
    }
}
