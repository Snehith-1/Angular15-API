using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.crm.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;
namespace ems.crm.DataAccess
{
    public class DaCRMProductGroup
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetProductgroupSummary(MdlCRMProductGroup values)
        {
            msSQL = " select  productgroup_gid, productgroup_name,productgroup_code, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                    " from crm_mst_tproductgroup a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<productgroup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new productgroup_list
                    {
                        productgroup_gid = dt["productgroup_gid"].ToString(),
                        productgroup_name = dt["productgroup_name"].ToString(),
                        productgroup_code = dt["productgroup_code"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.productgroup_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }




        public void DaPostProductgroup(string user_gid, productgroup_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("PPGM");


            msSQL = " insert into crm_mst_tproductgroup (" +
                    " productgroup_gid," +
                    " productgroup_code," +
                    " productgroup_name," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    "'" + values.productgroup_code + "',";
            if (values.productgroup_name == null || values.productgroup_name == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.productgroup_name.Replace("'", "") + "',";
            }
            msSQL += "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Productgroup Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Productgroup";
            }

        }

        public void DaUpdatedProductgroup(string user_gid, productgroup_list values)
        {


            msSQL = " update  crm_mst_tproductgroup  set " +
                 " productgroup_code = '" + values.productgroup_code + "',"+
          " productgroup_name = '" + values.productgroup_name + "'," +
          " updated_by = '" + user_gid + "'," +
          " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where productgroup_gid='" + values.productgroup_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Productgroup Updated Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Productgroup";
            }




        }

        public void DadeleteProductgroupSummary(string productgroup_gid, productgroup_list values)
        {
            msSQL = "  delete from crm_mst_tproductgroup where productgroup_gid='" + productgroup_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Productgroup Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Productgroup";
            }
       
        }
        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlCRMProductGroup values)
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
