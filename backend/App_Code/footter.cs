using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for footter
/// </summary>
public class footter
{
    public static string TableName
    {
        get
        {
            return "tbl_footer_" + Globals.CurrentLang;
        }
    }
	public footter()
	{
		
	}

    public void Inser(string title, string  sContent, string Created)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        // tao Id moi ; 
        string id = db.GetNewKey(TableName, "id", string.Empty);
        string Sql = "INSERT INTO " + TableName + "(id,title,footertext,created) ";
        Sql += " VALUES(?id,?title,?footertext,?created )";
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "footertext", DbType.Ntext, sContent);
        db.AddParameters(ref dt, "created", DbType.NVarChar, Created);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void UpDate(string id, string title, string sContent)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " title = ?title,  ";
        Sql += " footertext = ?footertext  ";
        Sql += " WHERE ?id = id ";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "footertext", DbType.Ntext, sContent);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void Delete(string id)
    {
        string Sql = "DELETE FROM " + TableName + " WHERE id = " + id + " ";
        DbTask db = new DbTask();
        db.ExecuteNonQuery(Sql);
    }
    public DataRow GetInfo( string ID)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string Sql = string.Empty;
        Sql = "Select * from " + TableName + " ";
        Sql += "where id = " + ID + " ";
        dt = db.GetData(Sql);
        if (dt == null) return null;
        return dt.Rows[0]; 
    }
    public DataTable Searchinh(int status)
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        string Sql = string.Empty;
        Sql = "Select * from " + TableName + " ";
        Sql += "where 1 = 1 ";
        if (status < 2)
        {
            Sql += "And status = " + status + " "; 
        }
        dt = db.GetData(Sql);
        if (dt == null) return null;
        return dt; 
    }
    public void SetStatus(string id)
    {
        DbTask db = new DbTask();
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status;
        if (Convert.ToInt32(dr["status"].ToString()) == 0)
            status = 1;
        else
            status = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = " + status + " ";
        Sql += " WHERE id = " + id + " ";
        db.ExecuteNonQuery(Sql);
    }
}
