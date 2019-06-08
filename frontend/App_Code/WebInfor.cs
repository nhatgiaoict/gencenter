using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
/// <summary>
/// Summary description for WebInfor
/// </summary>
public class WebInfor
{
    public static string TableName
    {
        get { return "tblInforweb_" + Globals.CurrentLang; }
    }
    
    public DataRow Getdata(string id)
    {
        int vID = Convert.ToInt32(id);
        string Sql = "SELECT * FROM " + TableName + " WHERE id = " + vID + " ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataRow GetInfo()
    {
        string Sql = "SELECT * FROM " + TableName + " ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
}
