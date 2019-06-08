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
    public DataRow Getforfooter()
    {
        DbTask db = new DbTask();
        DataTable dt = null;
        if (clsCache.Get("FooterNHS" + Globals.CurrentLang) != null)
        {
            dt = (DataTable)clsCache.Get("FooterNHS" + Globals.CurrentLang);
        }
        else
        {
            string Sql = string.Empty;
            Sql = "Select * from " + TableName + " ";
            Sql += "Where status = 1 ";
            Sql += "Order by created DESC ";
            dt = db.GetData(Sql);
            clsCache.Max("FooterNHS" + Globals.CurrentLang, (object)dt);
        }
        if (dt == null) return null;
        return dt.Rows[0]; // lay mot dong moi nhat;
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
        int vid = Convert.ToInt32(id);
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " status = " + status + " ";
        Sql += " WHERE id = " + vid + " ";
        db.ExecuteNonQuery(Sql);
    }

    public DataTable GetDoitac()
    {
        DbTask db = new DbTask();
        string SQL = "Select * from tbl_partner_vn where status = 1";
        return db.GetData(SQL);
    }
    public string sAddFooter()
    {
       DbTask db = new DbTask();
       string SQL = "Select diachichantrang from tblinforweb_vn where id = 1";
       return db.GetData(SQL).Rows[0]["diachichantrang"].ToString().Trim();
    }
}
