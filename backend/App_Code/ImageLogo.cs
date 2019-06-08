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
/// Summary description for ImageLogo
/// </summary>
public class ImageLogo
{
    public static string TableName
    {
        get { return "tbl_imageslogo"; }
    }
    public static string TableNameDefault
    {
        get { return "tbl_imagesdefault"; }
    }
    public void Insert(string name,string fimage,string width,string height)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string Sql = "INSERT INTO " + TableName + "(name,fimage,width,height) ";
        Sql += " VALUES(?name,?fimage,?width,?height)";
        db.AddParameters(ref dt, "name", DbType.NVarChar, name);
        db.AddParameters(ref dt, "fimage", DbType.NVarChar, fimage);
        db.AddParameters(ref dt, "width", DbType.NVarChar, width);
        db.AddParameters(ref dt, "height", DbType.NVarChar, height);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void Update(string vid, string name, string fimage, string width, string height)
    {
        int id = Convert.ToInt32(vid);
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " name = ?name,  ";
        Sql += " fimage = ?fimage,  ";
        Sql += " width = ?width,  ";
        Sql += " height = ?height  ";
        Sql += "WHERE ?id = id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.Int32, id);
        db.AddParameters(ref dt, "name", DbType.NVarChar, name);
        db.AddParameters(ref dt, "fimage", DbType.NVarChar, fimage);
        db.AddParameters(ref dt, "width", DbType.NVarChar, width);
        db.AddParameters(ref dt, "height", DbType.NVarChar, height);
        db.ExecuteNonQuery(Sql, dt);

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
    public void Delete(string id)
    {
        string Sql = "DELETE FROM " + TableName + " WHERE id = " + Convert.ToInt32(id) + " ";
        DbTask db = new DbTask();
        db.ExecuteNonQuery(Sql);
    }
    public DataRow GetInfoStatus(string id)
    {
        int vid = Convert.ToInt32(id);
        string Sql = "SELECT * FROM " + TableName + " WHERE id = " + vid + " ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public void SetStatus(string id)
    {
        DbTask db = new DbTask();
        DataRow dr = this.GetInfoStatus(id);
        if (dr == null)
            return;
        int status;
        if (Convert.ToInt32(dr["status"].ToString()) == 0)
            status = 1;
        else
            status = 0;
        int vid = Convert.ToInt32(id);
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = " + status + " ";
        Sql += " WHERE id = " + vid + " ";
        db.ExecuteNonQuery(Sql);
    }
    public void SetSTT(string id,int idx)
    {
        DbTask db = new DbTask();
        string Sql = "Update " + TableName + " SET ";
        Sql += " idx='" + idx + "' WHERE ";
        Sql += " id='" + id + "'";
        db.ExecuteNonQuery(Sql);
    }
    public DataTable Searching(int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;

        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        Sql += "WHERE 1=1 ";

        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        Sql = "Select " + TableName + ".*, row_number() over (order by idx ASC) as row_index INTO #Temp_Table ";
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
    //imagesdefault

    public void InsertDefault(string name, string fimage, string width, string height)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string Sql = "INSERT INTO " + TableNameDefault + "(name,fimage,width,height) ";
        Sql += " VALUES(?name,?fimage,?width,?height)";
        db.AddParameters(ref dt, "name", DbType.NVarChar, name);
        db.AddParameters(ref dt, "fimage", DbType.NVarChar, fimage);
        db.AddParameters(ref dt, "width", DbType.NVarChar, width);
        db.AddParameters(ref dt, "height", DbType.NVarChar, height);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void UpdateDefault(string vid, string name, string fimage, string width, string height)
    {
        int id = Convert.ToInt32(vid);
        string Sql = "UPDATE " + TableNameDefault + " SET ";
        Sql += " name = ?name,  ";
        Sql += " fimage = ?fimage,  ";
        Sql += " width = ?width,  ";
        Sql += " height = ?height  ";
        Sql += "WHERE ?id = id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.Int32, id);
        db.AddParameters(ref dt, "name", DbType.NVarChar, name);
        db.AddParameters(ref dt, "fimage", DbType.NVarChar, fimage);
        db.AddParameters(ref dt, "width", DbType.NVarChar, width);
        db.AddParameters(ref dt, "height", DbType.NVarChar, height);
        db.ExecuteNonQuery(Sql, dt);

    }
    public DataRow GetdataDefault(string id)
    {
        int vID = Convert.ToInt32(id);
        string Sql = "SELECT * FROM " + TableNameDefault + " WHERE id = " + vID + " ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public DataRow GetInfoDefault()
    {
        string Sql = "SELECT * FROM " + TableNameDefault + " ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public void DeleteDefault(string id)
    {
        string Sql = "DELETE FROM " + TableNameDefault + " WHERE id = " + Convert.ToInt32(id) + " ";
        DbTask db = new DbTask();
        db.ExecuteNonQuery(Sql);
    }
    public DataRow GetInfoStatusDefault(string id)
    {
        int vid = Convert.ToInt32(id);
        string Sql = "SELECT * FROM " + TableNameDefault + " WHERE id = " + vid + " ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }
    public void SetStatusDefault(string id)
    {
        DbTask db = new DbTask();
        DataRow dr = this.GetInfoStatusDefault(id);
        if (dr == null)
            return;
        int status;
        if (Convert.ToInt32(dr["status"].ToString()) == 0)
            status = 1;
        else
            status = 0;
        int vid = Convert.ToInt32(id);
        string Sql = "UPDATE " + TableNameDefault + " SET ";
        Sql += " status = " + status + " ";
        Sql += " WHERE id = " + vid + " ";
        db.ExecuteNonQuery(Sql);
    }
    public DataTable SearchingDefault(int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;

        string Sql = string.Empty;
        Sql = "SELECT COUNT(ID) AS Total FROM " + TableNameDefault + "  ";
        Sql += "WHERE 1=1 ";

        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;

        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        Sql = "Select " + TableNameDefault + ".*, row_number() over (order by id ASC) as row_index INTO #Temp_Table ";
        Sql += "From " + TableNameDefault + " ";
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
}
