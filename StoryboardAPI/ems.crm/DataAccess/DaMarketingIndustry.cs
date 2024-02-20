using ems.crm.Controllers;
using ems.crm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace ems.crm.DataAccess
{
    public class DaMarketingIndustry
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader odbcDataReader;
        DataTable dt_datatable;
        string msGetGid, lsCode;
        int mnResult;


        public void DaPostIndustry(string user_gid, industry_list values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("BCIM");

                msSQL = " insert into crm_mst_tcategoryindustry  (" +
                 " categoryindustry_gid, " +
                  " categoryindustry_code, " +
                 " categoryindustry_name, " +
                 " category_desc ) " +
                 " values (" +
                " '" + msGetGid + "', " +
                " '" + values.categoryindustry_code + "'," +
                " '" + values.categoryindustry_name + "'," +
                " '" + values.category_desc + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult !=0)
            {
                values.status = true;
                values.message = " Industry Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = " Error Occurs ";

            }


        }
        public void DaGetIndustrySummary(MdlMarketingIndustry values)
        {
                msSQL = "select categoryindustry_gid,categoryindustry_code,categoryindustry_name,category_desc from crm_mst_tcategoryindustry  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<industrydtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new industrydtl
                    {
                        categoryindustry_gid  = dt["categoryindustry_gid"].ToString(),
                        categoryindustry_code = dt["categoryindustry_code"].ToString(),
                        categoryindustry_name = dt["categoryindustry_name"].ToString(),
                        category_desc = dt["category_desc"].ToString(),
                    });
                    values.industrydtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetupdateIndustrytdetails(string user_gid, industry_list values)
        {
            msSQL = " update  crm_mst_tcategoryindustry set " +
                 " categoryindustry_name = '" + values.categoryindustry_name + "'," +
                 " category_desc = '" + values.category_desc + "'," +
                 " updated_by = '" + user_gid + "'," +
                 " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                 "' where categoryindustry_gid='" + values.categoryindustry_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "industry Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating industry";
            }
        }
        public void DaGetdeleteindustrydetails(string categoryindustry_gid, industry_list values)
        {
            msSQL = "  delete from crm_mst_tcategoryindustry where categoryindustry_gid='" + categoryindustry_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Industry Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Industry";
            }
        }

        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlMarketingIndustry values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";



            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumblist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumblist
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