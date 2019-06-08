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
/// Summary description for MemberCompany
/// </summary>
public class MemberCompany
{
    public static string TableName
    {
        get { return "tbl_dbmanager"; }
    }
    public DataTable Search_Mem(int start, int Total, out int TotalRecords)
    {
        DbTask db = new DbTask();
        DataTable dt = null;

        TotalRecords = 0;
        string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName ;
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);

        Sql = "SELECT  * FROM " + TableName ;
        Sql += " WHERE 1 = 1 ";
        Sql += " LIMIT " + start + ", " + Total ;
        dt = db.GetData(Sql);
        return dt;
    }
    public DataRow GetInfo(int id)
    {
        DbTask db = new DbTask();
        string Sql = string.Empty;
        Sql += "SELECT * FROM " + TableName + " WHERE id = " + id + "";
        return db.GetData(Sql).Rows[0];
    }
    public DataTable GetData()
    {
        DbTask db = new DbTask();
        string Sql = string.Empty;
        Sql += "SELECT * FROM " + TableName + "";
        return db.GetData(Sql);
    }
}
