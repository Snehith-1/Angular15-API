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
    public class DaDesignation
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        int mnResult6;


        public void DaPostDesignation(string user_gid, designation_list values)
        {
            //msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            //lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
            msGetGid = objcmnfunctions.GetMasterGID("CENT");
            msGetGid1 = objcmnfunctions.GetMasterGID("DES");
            msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='CENT' order by finyear desc limit 0,1 ";
            lsCode = objdbconn.GetExecuteScalar(msSQL);

            lsdesignation_code = "DES" + "000" + lsCode;






            msSQL = " insert into adm_mst_tdesignation(" +
                    " designation_gid," +
                    " designation_code," +
                    " designation_name," +
                    " designation_description," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    " '" + lsdesignation_code + "'," +
                    "'" + values.designation_name + "',";
            if (values.designation_description == null || values.designation_description == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.designation_description.Replace("'", "") + "',";
            }
            msSQL += "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Designation Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Designation";
            }
        }


    }
}
