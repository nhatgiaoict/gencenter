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

/// <summary>
/// Summary description for YhaooHelp
/// </summary>
public class YhaooHelp
{
    public static string TableName
    {
        get { return "tbl_yahoo_" + Globals.CurrentLang; }
    }
    public static string tblgroupYahoo
    {
        get { return "tbl_group_Yahoo_" + Globals.CurrentLang; }
    }
	public YhaooHelp()
	{
	}
    /// <summary>
    /// Them moi Yahoo help
    /// </summary>
    /// <param name="fullname"></param>
    /// <param name="YahooName"></param>
    /// <param name="skypyName"></param>
    /// <param name="groupid"></param>
    public void InsertYahoo(string fullname, string YahooName, string Email, string groupid, string Phone)
    {
        DbTask db = new DbTask();
        string Sql = "Insert Into " + TableName + " (id, fullName, YahooName, Email, groupid, Phone) ";
        Sql += "Values (?id, ?fullName,?YahooName, ?Email, ?groupid,?Phone)";
        string id = db.GetNewKey(TableName, "id", string.Empty);
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "fullName", DbType.NVarChar, fullname);
        db.AddParameters(ref dt, "YahooName", DbType.NVarChar, YahooName);
        db.AddParameters(ref dt, "Email", DbType.NVarChar, Email);
        db.AddParameters(ref dt, "Groupid", DbType.NVarChar, groupid);
        db.AddParameters(ref dt, "Phone", DbType.NVarChar, Phone);
        db.ExecuteNonQuery(Sql, dt);
    }

    public void UpDateYahoo(string id, string fullname, string YahooName, string sEmail, string groupid, string Phone)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " fullname = ?fullName,  ";
        Sql += " groupid = ?groupid,  ";
        Sql += " Phone = ?Phone,  ";
        Sql += " YahooName = ?YahooName, ";
        Sql += " Email = ?Email ";
        Sql += " WHERE id = ?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "fullname", DbType.NVarChar, fullname);
        db.AddParameters(ref dt, "YahooName", DbType.NVarChar, YahooName);
        db.AddParameters(ref dt, "Email", DbType.NVarChar, sEmail);
        db.AddParameters(ref dt, "groupid", DbType.NVarChar, groupid);
        db.AddParameters(ref dt, "Phone", DbType.NVarChar, Phone);
        db.ExecuteNonQuery(Sql, dt);
    }
    public DataTable GetAllYahoo()
    {
        DbTask db = new DbTask();
        string Sql = "Select * from " + TableName + " Order by idx ASC ";
        return db.GetData(Sql);
    }
    public void SetStatus(string id)
    {
        DbTask db = new DbTask();
        DataRow dr = GetInfoYhaoo(id);
        if (dr == null)
            return;
        int status;
        if (Convert.ToInt32(dr["status"].ToString()) == 0)
            status = 1;
        else
            status = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = " + status + " ";
        Sql += " WHERE id = '" + id.Trim() + "' ";
        db.ExecuteNonQuery(Sql);
    }

    public void UpdateIndexYahoo(string id, int Index)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += "idx = " + Index + " ";
        Sql += "WHERE id = '" + id + "'";
        DbTask db = new DbTask();
        db.ExecuteNonQuery(Sql);
    }

    public DataRow GetInfoYhaoo(string id)
    {
        DbTask db = new DbTask();
        string Sql = "Select * from " + TableName + " Where id = '" + id + "' ";
        DataTable dt = db.GetData(Sql);
        if(dt != null)
        {
            return dt.Rows[0];
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// them moi nhom Yahoo
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Note"></param>
    public void InsertGroupYahoo(string Name, string Note, string DiaChi, string DienThoai,string Fax)
    { 
        DbTask db = new DbTask();
        string Sql = "Insert Into " + tblgroupYahoo + " (id, Name, Note, Address, Phone, Fax) ";
        Sql += " Values (?id, ?Name,?Note, ?Address, ?Phone, ?Fax)";
        string id = db.GetNewKey(tblgroupYahoo, "id", string.Empty);
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "Name", DbType.NVarChar,Name);
        db.AddParameters(ref dt, "Note", DbType.NVarChar, Note);
        db.AddParameters(ref dt, "Address", DbType.NVarChar, Note);
        db.AddParameters(ref dt, "Phone", DbType.NVarChar, Note);
        db.AddParameters(ref dt, "Fax", DbType.NVarChar, Note);
        db.ExecuteNonQuery(Sql, dt);
    }
    public DataTable GetgroupYahoo()
    {
        DbTask db = new DbTask();
        string Sql = "Select * from " + tblgroupYahoo + "";
        return db.GetData(Sql);
    }
    public DataRow GetInfoGroup(string id)
    {
        DbTask db = new DbTask();
        string Sql = "Select * from " + tblgroupYahoo + " ";
        Sql += "Where id = '" + id + "'";
        DataTable dt = null;
        dt = db.GetData(Sql);
        if (dt != null)
        {
            return dt.Rows[0];
        }
        else
        {
            return null;
        }
    }
    public void UpdategroupYahoo(string id, string Name, string Note, string Address, string Phone, string Fax)
    {
        string Sql = "UPDATE " + tblgroupYahoo + " SET ";
        Sql += " Name = ?Name,  ";
        Sql += " Address = ?Address,  ";
        Sql += " Phone = ?Phone,  ";
        Sql += " Fax = ?Fax,  ";
        Sql += " Note = ?Note ";
        Sql += " WHERE id = ?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "name", DbType.NVarChar, Name);
        db.AddParameters(ref dt, "Note", DbType.NVarChar, Note);
        db.AddParameters(ref dt, "Address", DbType.NVarChar, Address);
        db.AddParameters(ref dt, "Phone", DbType.NVarChar, Phone);
        db.AddParameters(ref dt, "Fax", DbType.NVarChar, Fax);
        db.ExecuteNonQuery(Sql, dt);
    }

    public void DeletegroupYahoo(string id)
    {
        DbTask db = new DbTask();
        string Sql = string.Empty;
        Sql = "DELETE FROM " + tblgroupYahoo + " WHERE id = '" + id.Trim() + "'";
        db.ExecuteNonQuery(Sql);
        //Xoa cac yahoo trong bang yhoo
        DeleteYahooforGroup(id);
    }
    /// <summary>
    /// Delete nhom Yahoo
    /// </summary>
    /// <param name="groupid"></param>
    public void DeleteYahooforGroup(string groupid)
    {
        DbTask db = new DbTask();
        string sql = "Delete From " + TableName + " where groupid = '" + groupid + "'";
        db.ExecuteNonQuery(sql);
    }
    /// <summary>
    /// Delete nick Yahoo theo id
    /// </summary>
    /// <param name="id"></param>
    public void DeleteYahooforID(string id)
    {
        DbTask db = new DbTask();
        string sql = "Delete From " + TableName + " where id = '" + id + "'";
        db.ExecuteNonQuery(sql);
    }

    public void UpdateIndexGroupYahoo(string id, int Index)
    {
        string Sql = "UPDATE " + tblgroupYahoo + " SET ";
        Sql += "idx = " + Index + " ";
        Sql += "WHERE id = '" + id + "'";
        DbTask db = new DbTask();
        db.ExecuteNonQuery(Sql);
    }

    /// <summary>
    /// Lấy danh sách các Yahoo trong nhóm hỗ trợ
    /// </summary>
    /// <param name="groupid">id của nhóm</param>
    /// <returns>Datatabe</returns>
    public DataTable GetYahooFromGroup(string groupid)
    {
        DbTask db = new DbTask();
        string Sql = "Select * from " + TableName + " Where groupid = '" + groupid + "'";
        return db.GetData(Sql);
    }
}
