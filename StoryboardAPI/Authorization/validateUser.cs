using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using System.Data;
using System.Data.Odbc;
using System.Configuration;

namespace StoryboardAPI.Authorization
{

    public class validateUser
    {
        dbconn objdbconn = new dbconn();
        OdbcDataReader objODBCDataReader;
        string mssql;
        public bool isvalid(string username, string password, string companycode = null)
        {
            if (companycode == "")
            {
                string lscompany_code= ConfigurationManager.AppSettings["externalportal"].ToString();
                mssql = " select * from " + lscompany_code + ".adm_mst_tuser where user_code='" + username + "' and user_password='" + password + "'";
                objODBCDataReader = objdbconn.GetDataReader(mssql);
                if (objODBCDataReader.HasRows)
                {
                    objODBCDataReader.Close();
                    return true;
                }
                else
                {
                    objODBCDataReader.Close();

                    // Application Vendor Login....//

                    mssql = "select * from " + lscompany_code + ".adm_mst_tvendoruser where vendoruser_code ='" + username + "' and vendoruser_password='" + password + "'";
                    objODBCDataReader = objdbconn.GetDataReader(mssql);
                    if (objODBCDataReader.HasRows)
                    {
                        objODBCDataReader.Close();
                        return true;
                    }
                    else
                    {
                        objODBCDataReader.Close();
                        // Lawyer Login...//
                        mssql = "select * from " + lscompany_code + ".lgl_mst_tlawyeruser where lawyeruser_code ='" + username + "' and lawyeruser_password='" + password + "'";
                        objODBCDataReader = objdbconn.GetDataReader(mssql);
                        if (objODBCDataReader.HasRows)
                        {
                            objODBCDataReader.Close();
                            return true;
                        }
                        else
                        {
                            objODBCDataReader.Close();
                            objdbconn.CloseConn();
                            mssql = "select * from " + lscompany_code + ".rsk_mst_texternaluser where external_usercode ='" + username + "' and external_userpassword='" + password + "'";
                            objODBCDataReader = objdbconn.GetDataReader(mssql);
                            if (objODBCDataReader.HasRows)
                            {
                                objODBCDataReader.Close();
                                return true;
                            }
                            else
                            {
                                objODBCDataReader.Close();
                                return false;
                            }

                        }
                    }
                }
            }
            else
            {
                mssql = "SELECT user_gid FROM " + companycode + ".adm_mst_tuser WHERE user_code='" + username + "' AND user_password='" + password + "'";
                objODBCDataReader = objdbconn.GetDataReader(mssql);
                if(objODBCDataReader.HasRows) 
                {
                    objODBCDataReader.Close();
                    return true;
                }   
                else
                {
                    objODBCDataReader.Close();
                    return false;
                }
            }

        }
    }
}