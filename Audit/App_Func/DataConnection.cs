using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Audit.App_Func
{
    public class DataConnection
    {
        static string _connName = "DataConnection";

        static int _connTimeOut = 180000;

        static SqlConnection GetConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings[_connName].ConnectionString;

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            return conn;
        }

        public static void ExecuteNonQuery(SqlCommand cmd)
        {
            cmd.Connection = GetConnection();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = _connTimeOut;

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public static void ExecuteProcedure(SqlCommand cmd)
        {
            cmd.Connection = GetConnection();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = _connTimeOut;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static DataSet GetDataSet(SqlCommand cmd)
        {
            cmd.Connection = GetConnection();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = _connTimeOut;

            DataSet dsDataset = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsDataset);
            da.Dispose();

            cmd.Connection.Close();

            return dsDataset;
        }

        public static DataTable GetDataTable(SqlCommand cmd)
        {
            cmd.Connection = GetConnection();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = _connTimeOut;

            DataTable dtTable = new DataTable();
            dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

            cmd.Connection.Close();

            return dtTable;
        }

        public static DataTable GetDataTableFromText(SqlCommand cmd)
        {
            cmd.Connection = GetConnection();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = _connTimeOut;

            DataTable dtTable = new DataTable();
            dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

            cmd.Connection.Close();

            return dtTable;
        }

        public static object ExecuteScalar(SqlCommand cmd)
        {
            cmd.Connection = GetConnection();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = _connTimeOut;

            var obj = cmd.ExecuteScalar();

            cmd.Connection.Close();

            return obj;
        }
    }
}