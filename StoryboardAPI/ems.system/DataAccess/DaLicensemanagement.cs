using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.system.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
using System.Web;
using OfficeOpenXml;
using System.Configuration;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
namespace ems.system.DataAccess
{
    public class DaLicensemanagement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsbranchgroup_code, lsCode, msGetGid, msGetGid1, msGetPass, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        int mnResult6;


        public void DaPostLicensemanagement(string user_gid, licensemanagement_list values)
        {

         
   
            msSQL = " insert into adm_mst_tlicense(" +
                    " host_server," +
                    " machine_id," +
                    " server_name," +
                    " port," +
                    " user_name," +
                    " password," +
                    " message," +
                    " active_flag," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + values.host_server + "'," +
                    " '" + "MCHE" + "'," +
                    " '" + values.server_name + "'," +
                    " '" + "7018" + "'," +
                    " '" + "sb_erp" + "'," +
                    " '" + "vision!8" + "'," +
                    " '" + values.message_lic + "',";

            if (values.active_flag == "Yes")
            {
                msSQL += " 'Y',";

            }

            else 
            {
                msSQL += " 'N',";
            }

            msSQL += "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "License Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding License";
            }

        }

       
    }
}
