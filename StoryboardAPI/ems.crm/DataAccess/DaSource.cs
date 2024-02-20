using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.crm.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
using System.Web;
//using OfficeOpenXml;
using System.Configuration;
using System.IO;
//using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;


namespace ems.crm.DataAccess
{
    public class DaSource
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lssource_code, lssource, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        int mnResult6;


        public void DaPostSource(string source_gid, source_list values)
        {
            //msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            //lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
            msGetGid = objcmnfunctions.GetMasterGID("MSCM");
            msGetGid1 = objcmnfunctions.GetMasterGID("SCM");
            msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='MSCM' order by finyear desc limit 0,1 ";
            lsCode = objdbconn.GetExecuteScalar(msSQL);

            lssource_code = "SCM" + "000" + lsCode;






            msSQL = " insert into crm_mst_tsource(" +
                    " source_gid," +
                    " source_code," +
                    " source_name," +
                    " source_desc," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    " '" + lssource_code + "'," +
                    "'" + values.source_name + "',";
            if (values.source_description == null || values.source_description == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.source_description.Replace("'", "") + "',";
            }
            msSQL += "'" + source_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Source Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Source";
            }
        }

        public void DaGetSourceSummary(sourcelist values)
        {
            msSQL = " select  source_gid,source_code, source_name, source_desc, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by, a.created_date " +
                    " from crm_mst_tsource a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<sourcedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new sourcedtl
                    {
                        source_gid = dt["source_gid"].ToString(),
                        source_code = dt["source_code"].ToString(),
                        source_name = dt["source_name"].ToString(),
                        source_description = dt["source_desc"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.sourcedtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetupdatesourcedetails(string user_gid, source_list values)
        {
            msSQL = " update  crm_mst_tsource set " +
                 " source_name = '" + values.source_name + "'," +
                 " source_desc = '" + values.source_description + "'," +
                 " updated_by = '" + user_gid + "'," +
                 " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where source_gid='" + values.source_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Source Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Source";
            }
        }

        public void DaGetdeletesourcedetails(string source_gid, source_list values)
        {
            msSQL = "  delete from crm_mst_tsource where source_gid='" + source_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Source Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Source";
            }
        }

        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlSource values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumblist2>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumblist2
                    {


                        module_name1 = dt["module_name1"].ToString(),
                        sref1 = dt["sref1"].ToString(),
                        module_name2 = dt["module_name2"].ToString(),
                        sref2 = dt["sref2"].ToString(),
                        module_name3 = dt["module_name3"].ToString(),
                        sref3 = dt["sref3"].ToString(),

                    });
                    values.breadcrumblist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }



    }
}