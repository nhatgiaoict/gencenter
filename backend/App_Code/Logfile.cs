using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Logfile
/// </summary>
public class Logfile
{
    public static string TableName
    {
        get { return "logfile_" + Globals.CurrentLang; }
    }

    public void insertDatalog(string LogId, string Ngaythang, int TrangthaiID, string GhiChu)
    {
        try
        {
            DbTask db = new DbTask();
            DataTable dt = null;
            string Sql = "INSERT INTO " + TableName + "(LogId,userId,Ngaythang,TrangthaiID,GhiChu)";
            Sql += " VALUES (?LogId, ?userId, ?Ngaythang, ?TrangthaiID, ?GhiChu)";
            db.AddParameters(ref dt, "LogId", DbType.NVarChar, LogId);
            db.AddParameters(ref dt, "userId", DbType.NVarChar, Membertask.Name);
            db.AddParameters(ref dt, "Ngaythang", DbType.Datetime, Ngaythang);
            db.AddParameters(ref dt, "TrangthaiID", DbType.Int32, TrangthaiID);
            db.AddParameters(ref dt, "GhiChu", DbType.NVarChar, GhiChu);
            db.ExecuteNonQuery(Sql, dt);               
        }
        catch (Exception e)
        {
            string messasge = e.Message;
        }
    }
    public void DeleteLog(string vLogId)
    {
        DbTask db = new DbTask();
        string Sql = "DELETE FROM " + TableName + " WHERE LogId = '" + vLogId.Trim() + "' ";
        db.ExecuteNonQuery(Sql);
    }

    //Log file
    public DataTable SearchingLog(string LogID)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string Sql = string.Empty;
        Sql = "SELECT * FROM " + TableName + " ";
        Sql += "WHERE  LogId ='" + LogID + "' ";
        Sql += "ORDER BY Ngaythang DESC ";
        dt = db.GetData(Sql); 
        return dt;
    }

}