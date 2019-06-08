using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for Images
/// </summary>
public class Images
{
    public static string TableName
    {
        get { return "tbl_product_images_" + Globals.CurrentLang; }
    }
    public DataTable Search_Images(string productid)
    {
        DbTask db = new DbTask();
        DataTable dt = null;

        string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        string Sql = "SELECT  distinct a.* FROM " + TableName + " as a ";
        Sql += " WHERE a.status = 1 ";
        Sql += " AND a.created <= '" + dtNow + "' ";
        if (productid != null && productid != string.Empty && productid != "")
        {
            Sql += " AND a.productid = '" + productid + "'";
        }
        Sql += " ORDER BY a.created DESC ";

        dt = db.GetData(Sql);
        return dt;
    }
    public DataTable Get_Images(string id,string productid)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        string Sql = "SELECT  distinct a.* FROM " + TableName + " as a ";
        Sql += " WHERE a.status = 1 ";
        Sql += " AND a.created <= '" + dtNow + "' ";
        if (id != null && id != string.Empty && id != "")
        {
            Sql += " AND a.id = '" + id + "'";
        }
        if (productid != null && productid != string.Empty && productid != "")
        {
            Sql += " AND a.productid= '" + productid + "'";
        }
        Sql += " ORDER BY a.created DESC ";
        dt = db.GetData(Sql);

        if (id.Length > 0 || id != string.Empty)
        {
            return dt;
        }
        else
        {
            return null;
        }
    }
    public DataRow GetInfo(string id)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string Sql = "SELECT * FROM " + TableName + " WHERE id = '" + id + "' ";
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0) return null;

        return dt.Rows[0];
    }
    public DataTable Searching(string id, string productid, int Status, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
       
        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        Sql += "WHERE 1=1 ";
        if (Status < 2)
            Sql += "AND status = " + Status + " ";
        if (id != string.Empty && id != "" && id != null)
        {            
            Sql += " AND id  = '" + id + "' ";
        }
        if (productid != string.Empty && productid != "" && productid != null)
        {
            Sql += " AND productid  = '" + productid + "' ";
        }
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;

        int nS = RecordPerPages * (CurrentPage - 1);

        Sql = "SELECT * FROM " + TableName + " ";
        Sql += "WHERE 1=1 ";
        if (Status < 2)
            Sql += "AND status = " + Status + " ";
        
        if (id != string.Empty && id != "" && id != null)
        {
            Sql += " AND id  = '" + id + "' ";
        }
        if (productid != string.Empty && productid != "" && productid != null)
        {
            Sql += " AND productid  = '" + productid + "' ";
        }
        Sql += "ORDER BY created DESC, idx ASC ";
        Sql += "LIMIT " + nS + ", " + RecordPerPages + " ";
        dt = db.GetData(Sql);
        return dt;
    }
    public static void GetImageSize(string strAbsolutePath, out int w, out int h)
    {
        try
        {
            System.Drawing.Image img2Scale = System.Drawing.Image.FromFile(strAbsolutePath);
            w = Convert.ToInt32(img2Scale.Width);
            h = Convert.ToInt32(img2Scale.Height);
            img2Scale.Dispose();
        }
        catch (ArgumentException)
        {
            w = 0; h = 0;
        }
    }
}
