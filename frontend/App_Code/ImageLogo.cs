using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ImageLogo
/// </summary>
public class ImageLogo
{
    public static string TableName
    {
        //get { return "tbl_imageslogo_" + Globals.CurrentLang; }
        get { return "tbl_imageslogo"; }
    }
    public static string TableNameDefault
    {
        get { return "tbl_imagesdefault"; }
    }
    public DataRow GetInfo()
    {
        DataTable dt = new DataTable();
        if (clsCache.Get("BannerNSH") != null)
        {
            dt = (DataTable)clsCache.Get("BannerNSH");
        }
        else
        {
            string Sql = "SELECT * FROM " + TableName + " ";
            Sql += " Where status = 1";
            DbTask db = new DbTask();
            dt = db.GetData(Sql);
            clsCache.Max("BannerNSH", (object)dt);
        }
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataRow GetInfoBanNer()
    {
        string Sql = "SELECT * FROM " + TableName + " ";
        Sql += " Where status = 1";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt.Rows.Count > 1)
        {
            return dt.Rows[1];
        }
        else
        {
            return null;
        }
    }

    // lay thong tin bang nay theo ID 
    public DataRow GetInfoOnID(string ID)
    {
        DbTask  db =  new DbTask();
        string Sql = string.Empty;
        Sql = "Select * from " + TableName + "";
        Sql += "Where id =" + ID + "";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataRow GetflashDefault()
    {
        DbTask db=new DbTask();
        string Sql = "Select * from " + TableNameDefault + " ";
        Sql += "Where status = 1 ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataRow GetInfo1(int idx)
    {
        string Sql = "SELECT * FROM " + TableName + " ";
        Sql += " Where status = 1 AND idx= '" + idx + "' ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataRow GetInfo2(int idx)
    {
        string Sql = "SELECT * FROM " + TableName + " ";
        Sql += " Where status = 1 AND idx= '" + idx + "' ";
        DbTask db = new DbTask(); 
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
}
