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

/// <summary>
/// Summary description for Member
/// </summary>
public class Member
{
    private static string TableName
    {
        get { return "tbl_members"; }
    }
    public Member()
    {
        if (Globals.CheckExist)
        {
            try
            {
                string Sql = "CREATE TABLE " + TableName + " (";
                Sql += "id nvarchar(10) NOT NULL default '', ";
                Sql += "username nvarchar(255) default NULL, ";
                Sql += "password nvarchar(100) default NULL, ";
                Sql += "fullname nvarchar(255) default NULL, ";
                Sql += "tel nvarchar(15) default NULL, ";
                Sql += "email nvarchar(255) default NULL, ";
                Sql += "address nvarchar(255) default NULL, ";
                Sql += "jobtitle nvarchar(255) default NULL, ";
                Sql += "status int(4) default 0, ";
                Sql += "PRIMARY KEY  (id))";
                Sql += "ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;";

                DbTask db = new DbTask();
                db.ExecuteNonQuery(Sql);
            }
            catch
            {

            }
        }
    }
    private string GetAllID()
    {
        string Sql = "SELECT id FROM " + TableName + "";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return string.Empty;
        string sRet = ",";
        foreach (DataRow dr in dt.Rows)
            sRet += dr["id"].ToString() + ",";
        return sRet;
    }
    private bool CheckExistID(string AllID, string ID)
    {

        if (AllID == string.Empty || AllID == null || AllID == "")
            return false;
        AllID = AllID.StartsWith(",") ? AllID : "," + AllID;
        AllID = AllID.EndsWith(",") ? AllID : AllID + ",";
        if (AllID.IndexOf("," + ID + ",") > -1)
        {
            return true;
        }
        return false;
    }
    private string GetNewKey(string parentid)
    {
        string AllID = GetAllID();
        string sRet = string.Empty;
        string parent = (parentid == null || parentid == string.Empty) ? "" : parentid;
        for (int i = 1; i < 65535; i++)
        {
            sRet = string.Format("{0}{1:D2}", parent, i);
            if (!CheckExistID(AllID, sRet))
                break;
        }
        return sRet;
    }
    public DataRow GetInfoName(string username)
    {
        string Sql = "SELECT * FROM  " + TableName + " ";
        Sql += "WHERE username = N'" + username + "' ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        try
        {
            string full = dt.Rows[0][2].ToString();
        }
        catch (Exception ex)
        {
            string abc = ex.ToString();
        }
        return dt.Rows[0];
    }
    public DataRow Login(string username, string password)
    {
        DbTask db = new DbTask();
        DataTable dttemp = null;
        string sql = "select * from " + TableName + " ";
        sql += "Where username = ?usename1 and password = ?password1 ";
        db.AddParameters(ref dttemp, "usename1", DbType.NVarChar, username);
        db.AddParameters(ref dttemp, "password1", DbType.NVarChar, password);
        DataTable dt = db.GetData(sql, dttemp);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public void UpdateProfile(string username, string fullname, string tel, string email, string address, string jobtitle)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += "fullname = ?fullname,  ";
        Sql += "tel = ?tel,  ";
        Sql += "email = ?email,  ";
        Sql += "address = ?address,  ";
        Sql += "jobtitle = ?jobtitle  ";
        Sql += "WHERE username=?username";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "username", DbType.NVarChar, username);
        db.AddParameters(ref dt, "fullname", DbType.NVarChar, fullname);
        db.AddParameters(ref dt, "tel", DbType.NVarChar, tel);
        db.AddParameters(ref dt, "email", DbType.NVarChar, email);
        db.AddParameters(ref dt, "address", DbType.NVarChar, address);
        db.AddParameters(ref dt, "jobtitle", DbType.NVarChar, jobtitle);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void ChangePwd(string username, string password)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += "password = ?password ";
        Sql += "WHERE username=?username";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "username", DbType.NVarChar, username);
        db.AddParameters(ref dt, "password", DbType.NVarChar, password);
        db.ExecuteNonQuery(Sql, dt);
    }

    public void Insert(string username, string password, string fullname, string tel, string email, string address, string jobtitle)
    {
        string Sql = "INSERT INTO " + TableName + "(id,username,password,fullname,tel,email,address,jobtitle,IsAdmin,status)";
        Sql += " VALUES(?id, ?username, ?password, ?fullname, ?tel, ?email, ?address, ?jobtitle,?IsAdmin, ?status)";
        DbTask db = new DbTask();
        string id = db.GetNewKey(TableName, "id", string.Empty);
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "username", DbType.NVarChar, username);
        db.AddParameters(ref dt, "password", DbType.NVarChar, password);
        db.AddParameters(ref dt, "fullname", DbType.NVarChar, fullname);
        db.AddParameters(ref dt, "tel", DbType.NVarChar, tel);
        db.AddParameters(ref dt, "email", DbType.NVarChar, email);
        db.AddParameters(ref dt, "address", DbType.NVarChar, address);
        db.AddParameters(ref dt, "jobtitle", DbType.NVarChar, jobtitle);
        db.AddParameters(ref dt, "IsAdmin", DbType.Int32, 0);
        db.AddParameters(ref dt, "status", DbType.Int32, 0);
        db.ExecuteNonQuery(Sql, dt);
    }
    public bool CheckExist(string username)
    {
        string Sql = "SELECT * FROM  " + TableName + " ";
        Sql += "WHERE username = N'" + username.Trim() + "' ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        return false;
    }
    public DataTable Searching(int Status, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;

        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        if (Status < 2)
            Sql += "WHERE status = " + Status + " ";
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;

        Sql = "Select " + TableName + ".*, row_number() over (order by id ASC) as row_index INTO #Temp_Table ";
        Sql += "From " + TableName + " ";
        Sql += "Where 1 = 1 ";
        int nS = RecordPerPages * (CurrentPage - 1);
        int record_next = nS + RecordPerPages;
        Sql += "Select * from #Temp_Table ";
        if (nS == 0)
            Sql += " WHERE (row_index >=" + nS + ") AND (row_index <=" + record_next + ")";
        else
            Sql += " WHERE (row_index >" + nS + ") AND (row_index <=" + record_next + ")";
        Sql += " ORDER BY row_index ASC ";
        dt = db.GetData(Sql);
        return dt;

    }
    public void SetStatus(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status;
        if (Convert.ToInt32(dr["status"]) == 0)
            status = 1;
        else
            status = 0;

        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = ?status ";
        Sql += " WHERE id=?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "status", DbType.Int32, status);
        db.ExecuteNonQuery(Sql, dt);
    }
    public DataRow GetInfo(string id)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * FROM  " + TableName + " ";
        Sql += " WHERE id = '" + id.Trim() + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataRow GetInfoByUsersName(string username)
    {
        DbTask db = new DbTask();
        string Sql = "SELECT * FROM  " + TableName + " ";
        Sql += " WHERE username= N'" + username.Trim() + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public void Delete(string id)
    {
        string sFileName = Globals.AbsConfs + GetUserName(id);
        if (File.Exists(sFileName))
            File.Delete(sFileName);
        string Sql = "DELETE FROM " + TableName + " ";
        Sql += "WHERE id = '" + id.Trim() + "' ";
        DbTask db = new DbTask();
        db.ExecuteNonQuery(Sql);
    }

    private string GetUserName(string id)
    {
        string sRet = string.Empty;
        string Sql = "SELECT username FROM  " + TableName + " ";
        Sql += "WHERE id = '" + id.Trim() + "' ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            try
            {
                sRet = dt.Rows[0]["username"].ToString();
            }
            catch (Exception ex)
            {
                string abc = ex.ToString();
            }
        }
        return sRet;
    }
    public DataRow getUsers_Login(string vUser)
    {
        DbTask db = new DbTask();

        string Sql = "SELECT * FROM " + TableName + " WHERE username = N'" + vUser + "'  ";
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public void update_Pass(string id, string password)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " password = ?password ";
        Sql += " WHERE id = ?id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "password", DbType.NVarChar, password);
        db.ExecuteNonQuery(Sql, dt);

    }
}