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

    public class DaProductgroup
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetProductgroupSummary(MdlProductgroup values)
        {
            msSQL = " select  productgroup_gid, productgroup_name,productgroup_code, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                    " from pmr_mst_tproductgroup a " +
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


            msSQL = " insert into pmr_mst_tproductgroup (" +
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


            msSQL = " update  pmr_mst_tproductgroup  set " +
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
            msSQL = "  delete from pmr_mst_tproductgroup where productgroup_gid='" + productgroup_gid + "'  ";
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

        public void DaUnmappingvendor(string user_gid, string productgroup_gid, MdlProductgroup values)
        {
            msSQL = "   select a.vendor_gid,a.vendor_companyname " +
                        " from acp_mst_tvendor a " +
                        " where vendor_gid not in (select vendor_gid from acp_mst_tvendor2group b where b.productgroup_gid='" + productgroup_gid + "') " +
                        " order by vendor_companyname ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<unmappingvendor_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new unmappingvendor_list
                    {




                        vendor_gid = dt["vendor_gid"].ToString(),
                        vendor_companyname = dt["vendor_companyname"].ToString(),
                        
                    });
                    values.unmappingvendor_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaMappingvendor(string user_gid, string productgroup_gid, MdlProductgroup values)
        {
            msSQL = "   select a.vendor_gid,a.vendor_companyname " +
                        " from acp_mst_tvendor a " +
                        " inner join acp_mst_tvendor2group b on a.vendor_gid = b.vendor_gid where b.Productgroup_gid ='" + productgroup_gid + "' " +
                        " order by vendor_companyname ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<mappingvendor_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new mappingvendor_list
                    {




                        vendor_gid = dt["vendor_gid"].ToString(),
                        vendor_companyname = dt["vendor_companyname"].ToString(),

                    });
                    values.mappingvendor_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostMappingvendor(string user_gid, productgroup_list values)
        {
            for (int i = 0; i < values.source_list.ToArray().Length; i++)
            {
                string msGetGid = objcmnfunctions.GetMasterGID("PVRG");

                msSQL = " insert into acp_mst_tvendor2group(" +
                    "  vendor2group_gid, " +
                    " productgroup_gid, " +
                    " vendor_gid )" +
                    " values(" +
                " '" + msGetGid + "'," +
                " '" + values.productgroup_gid + "'," +
                "'" + values.source_list[i]._id + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Vendor Mapping Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Vendor Mapping";
                }

            }

        




        }

        public void DaPostUnmappingvendor(string user_gid, productgroup_list values)
        {
            for (int i = 0; i < values.source_list.ToArray().Length; i++)
            {
               
                msSQL = " delete  from acp_mst_tvendor2group " +
         " where vendor_gid ='" + values.source_list[i]._id + "' and Productgroup_gid='" + values.productgroup_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Vendor UnMapping Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Vendor UnMapping";
                }

            }






        }


    }

}