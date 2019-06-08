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
using uss.utils;
/// <summary>
/// Summary description for Album
/// </summary>
public class Album
{
    public static string TableName
    {
        get { return "tbl_Album_" + Globals.CurrentLang; }
    }

    public Album()
    {
        // TODO: Add constructor logic here
    }

    public DataRow GetInfo(string id)
    {
        string Sql = "SELECT * FROM " + TableName + " WHERE id = '" + id.Trim() + "' ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
    }

    public void InSert(string title, string fimages, string fwidth, string fheight, string summary, out string FileXML)
    {
        DbTask db = new DbTask();
        string Sql = "Insert into " + TableName + " ";
        Sql += "(id, Name, fimages, fheight, fwidth, summary, fXML, status) ";
        Sql += " VALUES(?id, ?Name, ?fimages, ?fheight, ?fwidth, ?summary, ?fXML, ?status )";
        string id = db.GetNewKey(TableName, "id", string.Empty);
        FileXML = id + Globals.CurrentLang + ".xml";
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "Name", DbType.NVarChar, title);
        db.AddParameters(ref dt, "fimages", DbType.NVarChar, fimages);
        db.AddParameters(ref dt, "fheight", DbType.NVarChar, fheight);
        db.AddParameters(ref dt, "fwidth", DbType.NVarChar, fwidth);
        db.AddParameters(ref dt, "fXML", DbType.NVarChar, FileXML);
        db.AddParameters(ref dt, "Summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "status", DbType.Int32, 0);
        db.ExecuteNonQuery(Sql, dt);
        // nen tao temp file XML op day luon cung duoc; 
    }

    public void Update(string id, string title, string fimages, string fwidth, string fheight, string summary)
    {
        DbTask db = new DbTask();
        string Sql = "Update " + TableName + " Set ";
        Sql += "Name = ?Name,  ";
        Sql += "fimages = ?fimages,  ";
        Sql += "fwidth = ?fwidth,  ";
        Sql += "fheight = ?fheight,  ";
        Sql += "summary = ?summary  ";
        Sql += " Where id = ?id ";
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "Name", DbType.NVarChar, title);
        db.AddParameters(ref dt, "fimages", DbType.NVarChar, fimages);
        db.AddParameters(ref dt, "fwidth", DbType.NVarChar, fwidth);
        db.AddParameters(ref dt, "fheight", DbType.NVarChar, fheight);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.ExecuteNonQuery(Sql, dt);
    }

    public bool Kiemtratrungten(string Name)
    {
        DbTask db = new DbTask();
        string Sql = "Select * from " + TableName + " ";
        Sql += "where Name = N'" + Name + "'";
        DataTable dt = db.GetData(Sql);
        if (dt.Rows.Count > 0) return false;
        else return true;
    }

    public DataTable GetData()
    {
        DbTask db = new DbTask();
        string Sql = "Select * from " + TableName + " ";
        DataTable dt = db.GetData(Sql);
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
        Sql += " WHERE id = '" + id.Trim() + "' ";
        db.ExecuteNonQuery(Sql);
    }
    public void Delete(string id)
    {
        DbTask db = new DbTask();
        string Sql = "Delete From " + TableName + " ";
        Sql += "Where id = " + id + " ";
        db.ExecuteNonQuery(Sql);
    }

    /// <summary>
    /// Lấy dữ liệu từ 1 file XML
    /// </summary>
    /// <param name="FileName">Đường dẫn tuyệt đối tới File XML</param>
    /// <returns></returns>
    public DataSet GetXMLData(string FileName)
    {
        try
        {
            System.Xml.XmlTextReader mReader = new System.Xml.XmlTextReader(FileName);
            DataSet mDataSet = new DataSet();
            mDataSet.ReadXml(mReader);
            mReader.Close();
            return mDataSet;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Doc du lieutu file xml va tra ve 2 gia tri lay ra ;
    /// </summary>
    /// <param name="fileXml"></param>
    /// <param name="Thumb_img">Values anh thumb</param>
    /// <param name="Img">Values anh chinh</param>
    public DataTable ReadXmlToDatatale(string fileXml)
    {
        DataSet mSet = GetXMLData(fileXml);// no se co nhieu datatable;
        DataTable dtc = mSet.Tables["item"];  
        if (dtc == null) return null;
        return dtc;
    }
}
