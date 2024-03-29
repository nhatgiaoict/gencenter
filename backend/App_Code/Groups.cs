using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;


/// <summary>
/// Summary description for Groups
/// </summary>
public class Groups
{
    public static int _nLength = 2;
    public static string TableName
    {
        get { return "tbl_group_" + Globals.CurrentLang; }
    }
     public static string TableHoivien
    {
        get { return "tbl_hoivien_" + Globals.CurrentLang; }
    }
    
    private static string _mIdAllow = string.Empty;
    private int iKind;
    public Groups(int _kind)
    {
        iKind = _kind;
    }
    public Groups()
    {
        if (Globals.CheckExist)
        {
            try
            {
                //Create Table
                string Sql = "CREATE TABLE IF NOT EXISTS " + TableName + "(";
                Sql += "id nvarchar(255) NOT NULL default '', ";
                Sql += "parentid nvarchar(255) NOT NULL default '', ";
                Sql += "title nvarchar(500) default NULL, ";
                Sql += "link nvarchar(500) default NULL, ";
                Sql += "summary mediumtext, ";
                Sql += "status tinyint(4) default 0, ";
                Sql += "idx tinyint(4) default 0, ";
                Sql += "kind tinyint(4) default 0, ";
                Sql += "advert tinyint(4) default 1, ";
                Sql += "inquiry tinyint(4) default 0, ";
                Sql += "PRIMARY KEY  (id)) ";
                Sql += "ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;";
                DbTask db = new DbTask();
                db.ExecuteNonQuery(Sql);
            }
            catch
            {

            }
        }
        if (!Membertask.IsAdministrator())
            _mIdAllow = Membertask.IsChuyenmuc();
        else
            _mIdAllow = string.Empty;
    }
    //==========
    //Voi Root la kieu du lieu (id:varchar 01,0101,010101)
    public DataTable GetChild(string ParentID)
    {
        DbTask dbtask = new DbTask();
        string parent = (ParentID == null || ParentID == string.Empty || ParentID == "") ? "00" : ParentID;
        string Sql = "Select c.id AS pid, c.title AS ptitle, c.parentid as pparentid, ";
        Sql += " ( SELECT COUNT(id) FROM " + TableName + " WHERE parentid = c.id) AS nextchild ";
        Sql += "FROM " + TableName + " AS c WHERE parentid = '" + parent + "' ";
        Sql += "And kind = '" + iKind + "' ";
        return dbtask.GetData(Sql);
    }
    public DataTable GetChildGroup(string ParentID)
    {
        DbTask dbtask = new DbTask();
        string parent = (ParentID == null || ParentID == string.Empty || ParentID == "") ? "00" : ParentID;
        string Sql = "Select id AS pid, title AS ptitle, parentid as pparentid ";
        Sql += "FROM " + TableName + " WHERE parentid = '" + parent + "' ";
        return dbtask.GetData(Sql);
    }
    //get child news
    public DataTable GetChildNews(string ParentID)
    {
        DbTask dbtask = new DbTask();
        string parent = (ParentID == null || ParentID == string.Empty || ParentID == "") ? "00" : ParentID;
        string Sql = "Select c.id AS pid, c.title AS ptitle, c.parentid as pparentid, ";
        Sql += "(SELECT COUNT(id) FROM " + TableName + " WHERE parentid = c.id) AS nextchild ";
        //Sql += "FROM " + TableName + " AS c WHERE parentid = '" + parent + "' and kind = '0'";
        Sql += "FROM " + TableName + " AS c WHERE parentid = '" + parent + "' ";
        return dbtask.GetData(Sql);
    }
    //get abum
    public DataTable GetAbum(string ParentID)
    {
        DbTask dbtask = new DbTask();
        string parent = (ParentID == null || ParentID == string.Empty || ParentID == "") ? "00" : ParentID;
        string Sql = "Select id AS pid, title AS ptitle, parentid as pparentid ";
        Sql += "FROM " + TableName + " WHERE parentid = '" + parent + "' AND kind = 1 ";
        return dbtask.GetData(Sql);
    }
    public bool CheckChild(string parentid)
    {
        DbTask db = new DbTask();
        string sql = "select id from " + TableName + " where parentid = '" + parentid + "'";
        DataTable dt = db.GetData(sql);
        if (dt.Rows.Count > 0)
            return true;
        else
            return false;
    }
    public DataTable GetChild_NotLink(string ParentID)
    {
        DbTask dbtask = new DbTask();
        string parent = (ParentID == null || ParentID == string.Empty || ParentID == "") ? "00" : ParentID;
        string Sql = "Select id AS pid, title AS ptitle, parentid as pparentid ";
        Sql += "FROM " + TableName + " WHERE link = '' AND  parentid = '" + parent + "'  ";
        return dbtask.GetData(Sql);
    }
    public string GetIDChild(string ID)
    {
        string sRet = string.Empty;
        DataTable dt = GetChild(ID);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                sRet += dr["pid"].ToString() + ", ";
                bool keimtra = CheckChild(dr["pid"].ToString());
                if (keimtra)
                    sRet += GetIDChild(dr["pid"].ToString());
            }
            return sRet;
        }
        else
        {
            return string.Empty;
        }
    }

    public void Insert(string parentid, string title, string link, string summary, string fimages, string fwidth, string fheight,
        string keywords, string Description, int Kind, string shortlink, string titlemeta, int sluongtin)
    {
        string parent = (parentid == null || parentid == string.Empty || parentid == "") ? "00" : parentid;

        string Sql = "INSERT INTO " + TableName + "(id, parentid, title, link, summary, status, idx, fimages, fwidth, fheight,keywords,Description,kind ,shortlink, titlemeta,sluongtin)";
        Sql += " VALUES(?id, ?parentid, ?title, ?link, ?summary, ?status, ?idx, ?fimages, ?fwidth, ?fheight,?keywords,?Description,?kind, ?shortlink, ?titlemeta, ?sluongtin)";
        DbTask db = new DbTask();
        string id = db.GetNewKey(TableName, "id", parentid);
        DataTable dt = null;
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "parentid", DbType.NVarChar, parent);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "link", DbType.NVarChar, link);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "status", DbType.Int32, 0);
        db.AddParameters(ref dt, "idx", DbType.Int32, Convert.ToInt32(id.Substring(id.Length - _nLength, _nLength)));
        db.AddParameters(ref dt, "fimages", DbType.NVarChar, fimages);
        db.AddParameters(ref dt, "fwidth", DbType.NVarChar, fwidth);
        db.AddParameters(ref dt, "fheight", DbType.NVarChar, fheight);
        db.AddParameters(ref dt, "shortlink", DbType.NVarChar, shortlink);
        db.AddParameters(ref dt, "titlemeta", DbType.NVarChar, titlemeta);
        db.AddParameters(ref dt, "keywords", DbType.NVarChar, keywords);
        db.AddParameters(ref dt, "Description", DbType.NVarChar, Description);
        db.AddParameters(ref dt, "kind", DbType.Int32, Kind);
        db.AddParameters(ref dt, "sluongtin", DbType.Int32, sluongtin);
        db.ExecuteNonQuery(Sql, dt);
    }
    public void Update(string id, string parentid, string title, string link, string summary, string fimages, string fwidth, string fheight, string keywords,
        string Description, int ikind, string shortlink, string titlemeta, int sluongtin)
    {
        string parent = (parentid == null || parentid == string.Empty || parentid == "") ? "00" : parentid;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " parentid = ?parentid,  ";
        Sql += " title = ?title,  ";
        Sql += " link = ?link,  ";
        Sql += " summary = ?summary,  ";
        Sql += " fimages = ?fimages,  ";
        Sql += " fwidth = ?fwidth, ";
        Sql += " fheight = ?fheight,  ";
        Sql += " keywords = ?keywords,  ";
        Sql += " Description = ?Description,  ";
        Sql += " shortlink = ?shortlink,  ";
        Sql += " titlemeta = ?titlemeta,  ";
        Sql += " sluongtin = ?sluongtin,  ";
        Sql += " kind = ?kind  ";
        Sql += " WHERE id=?id";
        DbTask db = new DbTask();
        DataTable dt = null;
        db.AddParameters(ref dt, "parentid", DbType.NVarChar, parent);
        db.AddParameters(ref dt, "id", DbType.NVarChar, id);
        db.AddParameters(ref dt, "title", DbType.NVarChar, title);
        db.AddParameters(ref dt, "link", DbType.NVarChar, link);
        db.AddParameters(ref dt, "summary", DbType.NVarChar, summary);
        db.AddParameters(ref dt, "fimages", DbType.NVarChar, fimages);
        db.AddParameters(ref dt, "fwidth", DbType.NVarChar, fwidth);
        db.AddParameters(ref dt, "fheight", DbType.NVarChar, fheight);
        db.AddParameters(ref dt, "keywords", DbType.NVarChar, keywords);
        db.AddParameters(ref dt, "Description", DbType.NVarChar, Description);
        db.AddParameters(ref dt, "shortlink", DbType.NVarChar, shortlink);
        db.AddParameters(ref dt, "titlemeta", DbType.NVarChar, titlemeta);
        db.AddParameters(ref dt, "kind", DbType.Int32, ikind);
        db.AddParameters(ref dt, "sluongtin", DbType.Int32, sluongtin);
        db.ExecuteNonQuery(Sql, dt);
    }

    public Hashtable[] GetChildGroup(int kind, string strParentID, bool SelectParent)
    {

        string Sql = "SELECT * FROM " + TableName + " WHERE 1=1 ";
        int len = _nLength;
        if (_mIdAllow != string.Empty)
        {
            string arrParentIdAllow = Globals.GetArrParentID(_mIdAllow, strParentID, _nLength);
            Sql += "AND id IN(" + arrParentIdAllow + ") ";
        }
        if (strParentID != null && strParentID != "" && strParentID != string.Empty)
        {
            len += strParentID.Length;
            Sql += "AND id  LIKE N'" + strParentID + "%' ";

        }
        if (kind < 2)
        {
            Sql += "AND kind = " + kind + " ";
        }

        Sql += "AND Len(link) < 7 ";
        Sql += "AND len(id) = " + len + " ";
        Sql += "ORDER BY idx ASC";

        DbTask db = new DbTask();
        DataTable tb = db.GetData(Sql);
        if (tb == null)
            return null;

        Hashtable[] hash_array = new Hashtable[tb.Rows.Count];
        int i = 0;
        foreach (DataRow dr in tb.Rows)
        {
            hash_array[i] = new Hashtable(3);
            hash_array[i].Add("text", dr["Title"].ToString());
            if (NextChild(dr["ID"].ToString()))
            {
                hash_array[i].Add("src", dr["ID"].ToString());
                if (SelectParent)
                    hash_array[i].Add("action", dr["ID"].ToString());
            }
            else
            {
                hash_array[i].Add("action", dr["ID"].ToString());
            }
            i++;
        }
        return hash_array;
    }
    public bool NextChild(string ID)
    {
        string Sql = "SELECT * FROM " + TableName + " ";
        Sql += "WHERE parentid = '" + ID + "' ";
        if (_mIdAllow != string.Empty)
        {
            Sql += "AND id IN(" + _mIdAllow + ") ";
        }
        DbTask db = new DbTask();
        DataTable tb = db.GetData(Sql);
        if (tb == null || tb.Rows.Count == 0)
            return false;
        return true;
    }
    public TreeItem[] SetValueToTreeItem(int kind, bool SelectParent)
    {
        Hashtable[] col = GetChildGroup(kind, null, SelectParent);
        TreeItem[] items = null;
        if (col != null && col.Length > 0)
        {
            items = new TreeItem[col.Length];

            for (int i = 0; i < col.Length; i++)
            {
                Hashtable hash_elem = col[i];
                if (hash_elem["src"] == null)
                    items[i] = new TreeItem(hash_elem["text"].ToString(), hash_elem["action"] == null ? "" : hash_elem["action"].ToString());
                else
                    items[i] = new TreeItem(hash_elem["text"].ToString(), hash_elem["action"] == null ? "" : hash_elem["action"].ToString(), hash_elem["src"].ToString());
            }
        }
        return items;
    }
    public DataTable Searching(string Parent, int Status, int CurrentPage, int RecordPerPages, out int TotalRecords, out int TotalPages)
    {
        string parentid = "00";
        if (Parent != string.Empty && Parent != "" && Parent != null)
        {
            parentid = Parent;
        }
        DbTask db = new DbTask();
        DataTable dt = null;
        TotalRecords = 0;
        TotalPages = 0;
        string Sql = string.Empty;
        Sql = " SELECT COUNT(ID) AS Total FROM " + TableName + "  ";
        Sql += " WHERE 1=1 ";
        if (Status < 2)
            Sql += " AND status = " + Status + " ";
        Sql += " AND parentid  = '" + parentid.Trim() + "' ";
        dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        TotalRecords = Convert.ToInt32(dt.Rows[0]["Total"]);
        TotalPages = (int)TotalRecords / RecordPerPages;
        if ((int)TotalRecords % RecordPerPages > 0)
            TotalPages = TotalPages + 1;
        Sql = "Select " + TableName + ".*, row_number() over (order by idx ASC) as row_index INTO #Temp_Table ";
        Sql += "from " + TableName + " ";
        Sql += "where 1=1 and parentid = '" + parentid + "' ";
        if (Status < 2)
        {
            Sql += "AND status = " + Status + " ";
        }
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

    public DataRow GetInfo(string id)
    {
        string Sql = "SELECT * FROM " + TableName + " WHERE id = '" + id.Trim() + "' ";
        DbTask db = new DbTask();
        DataTable dt = db.GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return null;
        return dt.Rows[0];
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
    public void SetKind(string id)
    {
        DbTask db = new DbTask();
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status;
        if (Convert.ToInt32(dr["kind"].ToString()) == 0)
            status = 1;
        else
            status = 0;
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " kind = " + status + " ";
        Sql += " WHERE id = '" + id.Trim() + "' ";
        db.ExecuteNonQuery(Sql);
    }
    public void SetStatusHome(string id)
    {
        DbTask db = new DbTask();
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status;
        if (Convert.ToInt32(dr["home"].ToString()) == 0)
            status = 1;
        else
            status = 0;

        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " home = " + status + " ";
        Sql += " WHERE id = '" + id.Trim() + "' ";
        db.ExecuteNonQuery(Sql);
    }
    public void SetInquiry(string id)
    {
        DbTask db = new DbTask();
        DataRow dr = this.GetInfo(id);
        if (dr == null)
            return;
        int status;
        if (Convert.ToInt32(dr["inquiry"].ToString()) == 0)
            status = 1;
        else
            status = 0;

        string Sql = "UPDATE " + TableName + " SET ";
        Sql += " inquiry = " + status + " ";
        Sql += " WHERE id = '" + id.Trim() + "' ";
        db.ExecuteNonQuery(Sql);

    }

    public void Delete(string id)
    {
        DbTask db = new DbTask();
        Publish publish = null;
        Menus menu = null;
        News news = null;
        string Sql = string.Empty;
        DataTable dt = GetChild(id);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string vparentid = dr["pparentid"].ToString();
                string vid = dr["pid"].ToString();
                bool kiemtra = CheckChild(id);
                if (kiemtra)
                {
                    Delete(vid);
                }
                else
                {
                    Sql = "DELETE FROM " + TableName + " WHERE parentid = '" + vparentid + "'";
                    db.ExecuteNonQuery(Sql);
                }
                //Xoa menu

                for (int i = 1; i <= 9; i++)
                {
                    menu = new Menus(i);
                    menu.RemoveFromMenu(vid);
                }
                //Xoa Tin
                news = new News();
                news.DeleteGroup(vid);

                //xoa publish
                publish = new Publish();
                publish.DeletePublish(id);
            }
        }
        Sql = "DELETE FROM " + TableName + " WHERE id = '" + id.Trim() + "'";
        db.ExecuteNonQuery(Sql);
        //Xoa menu
        for (int i = 1; i <= 9; i++)
        {
            menu = new Menus(i);
            menu.RemoveFromMenu(id);
        }
        //Xoa Tin
        news = new News();
        news.DeleteGroup(id);

        //xoa publish
        publish = new Publish();
        publish.DeletePublish(id);
    }
    public void UpdateIndex(string id, int Index)
    {
        string Sql = "UPDATE " + TableName + " SET ";
        Sql += "idx = " + Index + " ";
        Sql += "WHERE id = '" + id + "'";
        DbTask db = new DbTask();
        db.ExecuteNonQuery(Sql);
    }
    public int Ikind(string id)
    {
        DataRow dr = this.GetInfo(id);
        if (dr != null)
            return Convert.ToInt32(dr["kind"].ToString().Trim());
        else return 0;
    }
    public DataTable CheckShortlinkUrl( string sshortLink)
    {
        DbTask db = new DbTask();
        string Sql = "Select shortlink from " + TableName + " where shortlink = '" + sshortLink + "' ";
        DataTable dt = db.GetData(Sql);
        if (dt.Rows.Count > 0 && dt != null) return dt;
        return null;
    }

    public int CheckP(string IDGroup, string IDMember)
    {
        int _return = 0;
        DbTask dbtask = new DbTask();
        string Sql = "Select idmember from " + TableName + " where id='" + IDGroup + "' and kind = 0 and status = 1 ";
        DataTable dt = dbtask.GetData(Sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            string _temp = dt.Rows[0]["idmember"].ToString();
            if (_temp.Length > 1 && _temp != null)
            {
                Sql = "select id from " + TableName + " where " + IDMember + " in (" + _temp.Remove(_temp.Length - 1) + ")";
                DataTable dt1 = dbtask.GetData(Sql);
                if (dt1.Rows.Count > 0)
                {
                    _return = 1;
                }
                else
                {
                    _return = 0;
                }
            }
        }
        return _return;

    }

    public DataTable GetHoiVien()
    {
        DbTask db = new DbTask();
        string SQL = "Select * from " + TableHoivien + " where status = 1 Order by idx ";
        return db.GetData(SQL);
    }
}