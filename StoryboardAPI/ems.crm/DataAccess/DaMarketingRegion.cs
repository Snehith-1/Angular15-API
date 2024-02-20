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
using static ems.crm.Models.region_list;

namespace ems.crm.DataAccess
{
    public class DaMarketingRegion
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetMarketingRegionSummary(MdlMarketingRegion values)
        {
            msSQL = " select a.region_gid,a.region_code,a.region_name,a.created_by,a.created_date,a.city, CONCAT(b.user_firstname,' ',b.user_lastname)  as username from crm_mst_tregion a" +

                    "  left join adm_mst_tuser b on b.user_gid=a.created_by order by a.created_by desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<region_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new region_list
                    {
                        region_gid = dt["region_gid"].ToString(),
                        region_code = dt["region_code"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        created_by = dt["username"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        city = dt["City"].ToString(),


                        //created_date = dt["created_date"].ToString(),
                    });
                    values.regionlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }




        public void DaPostMarketingRegion(string user_gid, region_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("BRNM");
           

            msSQL = " insert into crm_mst_tregion(" +
                   " region_gid," +
                   " region_code," +
                   " region_name," +
                   " city," +
                   " created_by, " +
                  " created_date)" +
                   " values(" +
                   " '" + msGetGid + "'," +
                   "'" + values.region_code + "'," +
                    "'" + values.region_name + "'," +
                      "'" + values.city + "'," +
                      "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Region Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Region";
            }

        }

        public void DaUpdatedMarketingRegion(string user_gid, region_list values)
        {


            msSQL = " update  crm_mst_tregion set " +
          " region_gid = '" + values.region_gid + "'," +
          " region_code = '" + values.region_code+ "'," +
          " region_name = '" + values.region_name + "'," +
          " city = '" + values.city + "',"+
          " updated_by = '" + user_gid + "'," +
          " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where region_gid='" + values.region_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "region Updated Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error While Updating region";
            }




        }

        public void DadeleteMarketingRegionSummary(string region_gid, region_list values)
        {
            msSQL = "  delete from  crm_mst_tregion where region_gid='" + region_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "region Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting region";
            }
        }
        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlMarketingRegion values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumblist4>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumblist4
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