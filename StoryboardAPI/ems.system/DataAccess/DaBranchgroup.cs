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
    public class DaBranchgroup
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsbranchgroup_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        int mnResult6;


        public void DaPostbranchgroup(string user_gid, branchgroup_list values)
        {
            //msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            //lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
            msGetGid = objcmnfunctions.GetMasterGID("BBGM");
            msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='BBGM' order by finyear desc limit 0,1 ";
            lsCode = objdbconn.GetExecuteScalar(msSQL);

            lsbranchgroup_code = "BGC" + lsCode; 


        
            msSQL = " insert into hrm_mst_tbranchgroup(" +
                    " branchgroup_gid," +
                    " branchgroup_code," +
                    " branchgroup_name," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    " '" + lsbranchgroup_code + "'," +
                    "'" + values.branchgroup_name + "',";


            msSQL += "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Branchgroup Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Branchgroup";
            }
        }


    }
}
