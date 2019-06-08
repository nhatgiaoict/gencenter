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
using System.IO;

/// <summary>
/// Summary description for DynamicFile
/// </summary>
public class DynamicFile
{
    public DynamicFile()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// Hàm tạo một file mới (Kiếm tra nếu chưa có fileName thì tạo mới)
    /// </summary>
    public void CreateFile(string fileName, string strSTR)
    {
        try
        {
            StreamWriter write;
            StreamReader s;
            if (System.IO.File.Exists(fileName) == false)
            {
                write = new StreamWriter(fileName);
                write.WriteLine(strSTR);
                write.Close();
            }
            else
            {
                s = File.OpenText(fileName);
                string line = null;
                while ((line = s.ReadLine()) != null)
                {
                    strSTR += line;
                }
                s.Close();
                write = new StreamWriter(fileName);
                write.WriteLine(strSTR);
                write.Close();
            }
        }
        catch (Exception e) { e.Message.ToString(); }
    }
    /// <summary>
    /// Hàm đọc nội dung 1 file đã tồn tại. Nếu không tìm thấy file sẽ tra về ""
    /// </summary>
    public string ReadFile(string fileName)
    {
        string Content = "";
        StreamReader s;
        if (System.IO.File.Exists(fileName) == false)
        {
            return "";
        }
        else
        {
            s = File.OpenText(fileName);
            string line = null;
            while ((line = s.ReadLine()) != null)
            {
                Content += line;
            }
            s.Close();
            return Content;
        }
    }
    /// <summary>
    /// Cập nhật nhật nôi dung của file
    /// </summary>
    public void UpDateFile(string fileName, string newConTent)
    {
        try
        {
            StreamWriter write;
            StreamReader s;
            if (System.IO.File.Exists(fileName) == false)
            {
                System.Console.WriteLine("No Have fileName");
            }
            else
            {
                write = new StreamWriter(fileName);
                write.WriteLine(newConTent);
                write.Close();
            }
        }
        catch (Exception ex) { ex.Message.ToString(); }

    }
    /// <summary>
    /// Xóa một file được chọn
    /// </summary>
    /// <param name="fileName"></param>
    public void DeleteFile(string fileName)
    {
        try
        {
            FileInfo fi;
            if (System.IO.File.Exists(fileName) == true)
            {
                fi = new FileInfo(fileName);
                fi.Delete();
            }
        }
        catch (Exception ex) { ex.Message.ToString(); }
    }
}

