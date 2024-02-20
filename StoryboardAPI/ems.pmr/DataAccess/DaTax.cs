using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.pmr.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
//using System.Web;
//using OfficeOpenXml;
using System.Configuration;
using System.IO;
//using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;

namespace ems.pmr.DataAccess
{
    public class DaTax
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetTaxSummary(MdlTax values)
        {
            msSQL = " select  tax_gid, tax_name, percentage,  split_flag, active_flag , CONCAT(b.user_firstname,' ',b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                    " from acp_mst_ttax a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<tax_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new tax_list
                    {
                        tax_gid = dt["tax_gid"].ToString(),
                        tax_name = dt["tax_name"].ToString(),
                        percentage = dt["percentage"].ToString(),
                        split_flag = dt["split_flag"].ToString(),
                        active_flag = dt["active_flag"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.tax_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

      


        public void DaPostTax(string user_gid, tax_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("STXM");
            //msSQL = " Select country_name from adm_mst_tcountry where country_gid= '" + values.country_name + "'";
            //string lscountry_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " insert into acp_mst_ttax(" +
                    " tax_gid," +
                    " tax_type," +
                    " split_flag," +
                    " percentage," +
                    " tax_name," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    " 'Tax'," +
                    " '" + values.split_flag + "'," +
                    "'" + values.percentage + "',";
            if (values.tax_name == null || values.tax_name == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.tax_name.Replace("'", "") + "',";
            }
            msSQL += "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tax Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Tax";
            }

        }

        public void DaUpdatedTax(string user_gid, tax_list values)
        {
            

            msSQL = " update  acp_mst_ttax set " +
          " tax_name = '" + values.tax_name + "'," +
          " percentage = '" + values.percentage + "'," +
          " split_flag = '" + values.split_flag + "'," +
          " active_flag = '" + values.active_flag + "'," +
          " updated_by = '" + user_gid + "'," +
          " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where tax_gid='" + values.tax_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
             
                    values.status = true;
                    values.message = "Tax Updated Successfully";
             
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Tax";
            }




        }

        public void DadeleteTaxSummary(string tax_gid, tax_list values)
        {
            msSQL = "  delete from acp_mst_ttax where tax_gid='" + tax_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tax Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Tax";
            }
        }
    }
}