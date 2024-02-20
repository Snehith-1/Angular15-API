using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.utilities.Functions
{
    public class dbconn
    {
        private string lsConnectionString = string.Empty;

        // Get Connection String 

        public string GetConnectionString()
        {
            try
            {
                if(HttpContext.Current.Request.Headers["Authorization"] == null || HttpContext.Current.Request.Headers["Authorization"] == "null")
                {
                    lsConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();
                }
                else
                {
                    using(OdbcConnection conn=new OdbcConnection(ConfigurationManager.ConnectionStrings["AuthConn"].ToString()))
                    {
                        using(OdbcCommand cmd=new OdbcCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = " CALL adm_mst_spgetconnectionstring('" + HttpContext.Current.Request.Headers["Authorization"].ToString() + "')";
                            cmd.Connection = conn;
                            conn.Open();
                            lsConnectionString = cmd.ExecuteScalar().ToString();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                lsConnectionString = "error";
            }
            return lsConnectionString;
        }

        // Open Connection 

        public OdbcConnection OpenConn()
        {
            try
            {
                OdbcConnection gs_ConnDB;
                gs_ConnDB = new OdbcConnection(GetConnectionString());
                if (gs_ConnDB.State != ConnectionState.Open)
                {
                    gs_ConnDB.Open();
                }
                return gs_ConnDB;
            }
            catch (Exception e)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "UnAuthorized" };
                throw new HttpResponseException(msg);
            }

        }

        // Close Connection

        public void CloseConn()
        {
            if (OpenConn().State != ConnectionState.Closed)
            {
                OpenConn().Dispose();
                OpenConn().Close();
            }
        }

        // Execute a Query

        public int ExecuteNonQuerySQL(string query)
        {
            int mnResult = 0;
            OdbcConnection ObjODBCConnection = OpenConn();
            try
            {
                OdbcCommand cmd = new OdbcCommand(query, ObjODBCConnection);
                mnResult = cmd.ExecuteNonQuery();
                mnResult = 1;
            }
            catch
            {
            }
            ObjODBCConnection.Close();
            return mnResult;
        }

        // Get Scalar Value

        public string GetExecuteScalar(string query)
        {
            string val;
            OdbcConnection ObjODBCConnection = OpenConn();
            try
            {
                OdbcCommand cmd = new OdbcCommand(query, ObjODBCConnection);
                 val = cmd.ExecuteScalar().ToString();
               
            }
            catch
            {
                val = "";
            }
            ObjODBCConnection.Close();
            return val;

        }

        // Get Data Reader

        public OdbcDataReader GetDataReader(string query)
        {
            OdbcCommand cmd = new OdbcCommand(query, OpenConn());
            OdbcDataReader rdr;
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection );
            return rdr;
        }

        // Get Data Table

        public DataTable GetDataTable(string query)
        {
            OdbcConnection ObjODBCConnection = OpenConn();
            DataTable dt = new DataTable();
            OdbcDataAdapter da = new OdbcDataAdapter(query, ObjODBCConnection);
            da.Fill(dt);
            ObjODBCConnection.Close();
            return dt;
        }

        // Get Data Set

        public DataSet GetDataSet(string query, string table)
        {
            OdbcConnection ObjODBCConnection = OpenConn();
            DataSet ds = new DataSet();
            OdbcDataAdapter da = new OdbcDataAdapter(query, ObjODBCConnection);
            da.Fill(ds, table);
            ObjODBCConnection.Close ();
            return ds;
        }

    }
}