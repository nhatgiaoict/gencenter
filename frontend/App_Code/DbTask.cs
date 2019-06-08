using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Db
/// </summary>
public class DbTask
{
    
    private IsolationLevel m_isoLevel = IsolationLevel.ReadUncommitted;
    private string strCon = Globals.ConectionString;
    public DbTask()
    {
        //strCon = System.Configuration.ConfigurationManager.AppSettings["conectionString"];   
    }
    public DbTask(string Server, string UserID, string Password)
    {
        strCon = "server = " + Server + "; uid = " + UserID + "; pwd = " + Password + "; charset = utf8;";
    }

    public DbTask(string Server, string UserID, string Password, string Database)
    {
        strCon = "server = " + Server + "; uid = " + UserID + "; pwd = " + Password + "; Database = " + Database + "; charset = utf8;";
    }

    //====
    public SqlConnection GetConnection()
    {
        SqlConnection conn = new SqlConnection(strCon);
        conn.Open();
        return conn;
    }
    //
    public DataTable GetData(SqlCommand cmd)
    {
        if (cmd.Connection != null)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }
        else
        {
            using (SqlConnection conn = GetConnection())
            {

                using (SqlTransaction trans = conn.BeginTransaction(m_isoLevel))
                {
                    try
                    {
                        cmd.Transaction = trans;
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {
                                da.SelectCommand = cmd;
                                da.SelectCommand.Connection = conn;
                                da.Fill(ds);
                                return ds.Tables[0];
                            }
                        }
                    }
                    finally
                    {
                        trans.Commit();
                    }
                }
            }
        }

    }
    public DataTable GetData(string sql)
    {
        using (SqlConnection conn = GetConnection())
        {
            using (SqlTransaction trans = conn.BeginTransaction(m_isoLevel))
            {
                try
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = trans;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {
                                da.SelectCommand = cmd;
                                da.SelectCommand.Connection = conn;
                                da.Fill(ds);
                                return ds.Tables[0];
                            }
                        }
                    }
                }
                finally
                {
                    trans.Commit();
                }
            }
        }

    }
    //====
    public DataTable GetData(string sql, DataTable dtParameters)
    {
        return GetData(MakeCommand(sql, dtParameters));
    }
    //
    public void ExecuteNonQuery(SqlCommand cmd)
    {

        using (SqlConnection conn = GetConnection())
        {
            using (SqlTransaction trans = conn.BeginTransaction(m_isoLevel))
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    trans.Commit();
                }
            }
        }

    }
    public void ExecuteNonQuery(string sql)
    {

        using (SqlConnection conn = GetConnection())
        {
            using (SqlTransaction trans = conn.BeginTransaction(m_isoLevel))
            {
                try
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = trans;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    trans.Commit();
                }
            }
        }

    }
    public void ExecuteNonQuery(string sql, DataTable dtParameters)
    {
        ExecuteNonQuery(MakeCommand(sql, dtParameters));
    }
    //
    public object ExecuteScalar(SqlCommand cmd)
    {
        using (SqlConnection conn = GetConnection())
        {
            using (SqlTransaction trans = conn.BeginTransaction(m_isoLevel))
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    return cmd.ExecuteScalar();
                }
                finally
                {
                    trans.Commit();
                }
            }
        }

    }
    public object ExecuteScalar(string sql)
    {
        using (SqlConnection conn = GetConnection())
        {
            using (SqlTransaction trans = conn.BeginTransaction(m_isoLevel))
            {
                try
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = trans;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        return cmd.ExecuteScalar();
                    }
                }
                finally
                {
                    trans.Commit();

                }
            }
        }

    }
    public object ExecuteScalar(string sql, DataTable dtParameters)
    {
        return ExecuteScalar(MakeCommand(sql, dtParameters));
    }
    //

    private SqlCommand MakeCommand(string sql, DataTable dtParameters)
    {
        string prepareSql = sql.Replace("?", "@"); // sql.Replace("@", "?");
        SqlCommand oCommand = new SqlCommand(prepareSql);
        if (dtParameters == null || dtParameters.Rows.Count == 0)
            return oCommand;
        string para_name = string.Empty;
        DbType para_type;
        foreach (DataRow dr in dtParameters.Rows)
        {
            try
            {
                para_name = dr["para_name"].ToString();
                para_type = (DbType)dr["para_type"];
                switch (para_type)
                {
                    case DbType.Date:
                        {
                            oCommand.Parameters.Add("@" + para_name, SqlDbType.Date).Value = Convert.ToDateTime(dr["para_value"]);
                            break;
                        }
                    case DbType.Datetime:
                        {
                            oCommand.Parameters.Add("@" + para_name, SqlDbType.DateTime).Value = Convert.ToDateTime(dr["para_value"]);
                            break;
                        }
                    case DbType.Decimal:
                        {
                            oCommand.Parameters.Add("@" + para_name, SqlDbType.Decimal).Value = Convert.ToDecimal(dr["para_value"]);
                            break;
                        }
                    case DbType.Float:
                        {
                            oCommand.Parameters.Add("@" + para_name, SqlDbType.Float).Value = Convert.ToDouble(dr["para_value"]);
                            break;
                        }
                    case DbType.Int32:
                        {
                            oCommand.Parameters.Add("@" + para_name, SqlDbType.Int).Value = Convert.ToInt32(dr["para_value"]);
                            break;
                        }
                    case DbType.NVarChar:
                        {
                            oCommand.Parameters.Add("@" + para_name, SqlDbType.NVarChar).Value = dr["para_value"].ToString();
                            break;
                        }
                    case DbType.Text:
                        {
                            oCommand.Parameters.Add("@" + para_name, SqlDbType.Text).Value = dr["para_value"].ToString();
                            break;
                        }
                    case DbType.VarChar:
                        {
                            oCommand.Parameters.Add("@" + para_name, SqlDbType.VarChar).Value = dr["para_value"].ToString();
                            break;
                        }
                    default:
                        {
                            oCommand.Parameters.Add("@" + para_name, SqlDbType.NVarChar).Value = dr["para_value"].ToString();
                            break;
                        }
                }
            }
            catch { }
        }
        dtParameters.Dispose();
        return oCommand;
    }
    private DataTable dtParameters()
    {
        DataTable dt = new DataTable("Parameters");
        dt.Columns.Add("para_name", typeof(string));
        dt.Columns.Add("para_type", typeof(DbType));
        dt.Columns.Add("para_value", typeof(object));
        dt.Columns["para_name"].Unique = true;
        dt.PrimaryKey = new DataColumn[] { dt.Columns["para_name"] };
        return dt;
    }
    public void AddParameters(ref DataTable dtParameters, string para_name, DbType para_type, object para_value)
    {
        try
        {
            DataRow dr = dtParameters.NewRow();
            dr["para_name"] = para_name;
            dr["para_type"] = para_type;
            dr["para_value"] = para_value;
            dtParameters.Rows.Add(dr);
            dtParameters.AcceptChanges();
        }
        catch
        {
            dtParameters = this.dtParameters();
            DataRow dr = dtParameters.NewRow();
            dr["para_name"] = para_name;
            dr["para_type"] = para_type;
            dr["para_value"] = para_value;
            dtParameters.Rows.Add(dr);
            dtParameters.AcceptChanges();
        }
    }
    public string GetNewKey(string tbl_name, string primary_field, string parentid, int levelLength)
    {
        string Sql = "SELECT " + primary_field + " FROM " + tbl_name + " WHERE 1 = 1 ";
        string parent = (parentid == null || parentid == string.Empty) ? "" : parentid;
        if (parent != string.Empty)
        {
            Sql += "AND " + primary_field + " LIKE '" + parent + "%' ";
        }
        Sql += "AND CHAR_LENGTH(" + primary_field + ") = " + (parent.Length + levelLength) + " ";


        DataTable dt = GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return string.Format("{0}{1:D2}", parent, 1);

        int n = dt.Rows.Count;

        int lastid = Convert.ToInt32(dt.Rows[n - 1][primary_field]);
        if (lastid == n)
            return string.Format("{0}{1:D2}", parent, n + 1);
        dt.PrimaryKey = new DataColumn[] { dt.Columns[primary_field] };
        dt.AcceptChanges();
        int i = 1;
        string sRet = string.Format("{0}{1:D2}", parent, i);
        while (dt.Rows.Find(sRet) != null)
        {
            i++;
            sRet = string.Format("{0}{1:D2}", parent, i);
        }
        return sRet;

    }

    public string GetNewKey(string tbl_name, string primary_field)
    {
        string Sql = "SELECT " + primary_field + " FROM " + tbl_name + "";
        DataTable dt = GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return "01";

        int n = dt.Rows.Count;
        string parent = string.Empty;
        int lastid = Convert.ToInt32(dt.Rows[n - 1]["id"]);
        if (lastid == n)
            return string.Format("{0}{1:D2}", parent, n + 1);
        dt.PrimaryKey = new DataColumn[] { dt.Columns[primary_field] };
        dt.AcceptChanges();
        int i = 1;
        string sRet = string.Format("{0}{1:D2}", parent, i);
        while (dt.Rows.Find(sRet) != null)
        {
            i++;
            sRet = string.Format("{0}{1:D2}", parent, i);
        }
        return sRet;
    }

    public string GetNewKey(string tbl_name, string primary_field, string parentid)
    {
        string Sql = "SELECT " + primary_field + " AS id FROM " + tbl_name + "";
        string parent = (parentid == null || parentid == string.Empty) ? "" : parentid;
        if (parent != string.Empty)
        {
            Sql += " WHERE id LIKE '" + parent + "%' AND len (id) >" + parent.Length + " ";
        }
        DataTable dt = GetData(Sql);
        if (dt == null || dt.Rows.Count == 0)
            return parentid + "01";

        int n = dt.Rows.Count;

        int lastid = Convert.ToInt32(dt.Rows[n - 1]["id"]);
        if (lastid == n)
            return string.Format("{0}{1:D2}", parent, n + 1);
        dt.PrimaryKey = new DataColumn[] { dt.Columns[primary_field] };
        dt.AcceptChanges();
        int i = 1;
        string sRet = string.Format("{0}{1:D2}", parent, i);
        while (dt.Rows.Find(sRet) != null)
        {
            i++;
            sRet = string.Format("{0}{1:D2}", parent, i);
        }
        return sRet;
    }   
}
