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
//using System.Web;
//using OfficeOpenXml;
using System.Configuration;
using System.IO;
//using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;

namespace ems.crm.DataAccess
{
    public class DaMarketingTax
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetMarketingTaxSummary(MdlMarketingTax values)
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




        public void DaPostMarketingTax(string user_gid, tax_list values)
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


        public void DaPostTaxsplit(string tax_gid, splittax_list values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("TXDT");


            msSQL = " insert into acp_mst_ttaxdtl(" +
                    " taxdtl_gid," +
                    " tax_gid," +
                    " taxsplit_name1," +
                    " taxsplit_per1," +
                    " taxsplit_name2," +
                    " taxsplit_per2, " +
                    " taxsplit_name3," +
                    "taxsplit_per3)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    " '" + values.tax_gid + "'," +

                    " '" + values.taxsplit_name1 + "'," +
                    " '" + values.taxsplit_per1 + "'," +
                    " '" + values.taxsplit_name2 + "'," +
                    " '" + values.taxsplit_per2 + "'," +
                    " '" + values.taxsplit_name3 + "'," +
                    "'" + values.taxsplit_per3 + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Split Percentage Updated Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Split";
            }




        }


        public void DaUpdatedMarketingTax(string user_gid, tax_list values)
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

        public void DadeleteMarketingTaxSummary(string tax_gid, tax_list values)
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

        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlMarketingTax values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumb_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumb_list
                    {


                        module_name1 = dt["module_name1"].ToString(),
                        sref1 = dt["sref1"].ToString(),
                        module_name2 = dt["module_name2"].ToString(),
                        sref2 = dt["sref2"].ToString(),
                        module_name3 = dt["module_name3"].ToString(),
                        sref3 = dt["sref3"].ToString(),

                    });
                    values.breadcrumb_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

    }
}