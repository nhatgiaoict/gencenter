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
/// Summary description for WenInfor
/// </summary>
public class WebInfor
{
    public static string TableName
    {
        get { return "tblInforweb_" + Globals.CurrentLang; }
    }
    public static DataTable GetDatabaseXml()
    {
        Globals.xmltask.FileName = Globals.AbsData + "xmls/database.xml";
        return Globals.xmltask.Read();
    }
    public void Update(string vid, string mailcontact, string backend, string frontend, string keywords, string Description, string diachi, string contact, string SEOH1, string tencongty, string slogan)
    {
        int id = Convert.ToInt32(vid);
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " mailcontact = ?mailcontact,  ";
        Sql += " backend = ?backend,  ";
        Sql += " frontend = ?frontend,  ";
        Sql += " keywords = ?keywords,  ";
        Sql += " Diachi = ?Diachi, ";
        Sql += " contact = ?contact, ";
        Sql += " SEOH1 = ?SEOH1, ";
        Sql += " tencongty = ?tencongty, ";
        Sql += " slogan = ?slogan, ";
        Sql += " Description = ?Description  ";
        Sql += "WHERE ?id = id ";

        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.Int32, id);
        db.AddParameters(ref dt, "mailcontact", DbType.NVarChar, mailcontact);
        db.AddParameters(ref dt, "backend", DbType.NVarChar, backend);
        db.AddParameters(ref dt, "frontend", DbType.NVarChar, frontend);
        db.AddParameters(ref dt, "keywords", DbType.NVarChar, keywords);
        db.AddParameters(ref dt, "Diachi", DbType.Ntext, diachi);
        db.AddParameters(ref dt, "contact", DbType.Ntext, contact);
        db.AddParameters(ref dt, "SEOH1", DbType.Ntext, SEOH1);
        db.AddParameters(ref dt, "tencongty", DbType.Ntext, tencongty);
        db.AddParameters(ref dt, "slogan", DbType.Ntext, slogan);
        db.AddParameters(ref dt, "Description", DbType.NVarChar, Description);
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

}
