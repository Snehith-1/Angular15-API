using ems.pmr.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;

namespace ems.pmr.DataAccess
{
    public class DaProductattributes
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetProductattributesSummary(MdlProductattributes values)
        {
            msSQL = " select productattribute_gid, attribute_code, attribute_name " +
                    " from pmr_mst_tproductattributes ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<productattributes_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new productattributes_list
                    {
                        productattribute_gid = dt["productattribute_gid"].ToString(),
                        attribute_code = dt["attribute_code"].ToString(),
                        attribute_name = dt["attribute_name"].ToString(),
                        //created_by = dt["created_by"].ToString(),
                        //created_date = dt["created_date"].ToString(),
                    });
                    values.productattributes_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }




        public void DaPostProductattributes(string user_gid, productattributes_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("PPGM");


            msSQL = " insert into pmr_mst_tproductattributes (" +
                    " productattribute_gid," +
                    " attribute_code," +
                    " attribute_name)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    "'" + values.attribute_code + "',";
            if (values.attribute_name == null || values.attribute_name == "")
            {
                msSQL += "'')";
            }
            else
            {
                msSQL += "'" + values.attribute_name.Replace("'", "") + "')";
            }
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Attributes Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Product Attributes";
            }

        }

        public void DaUpdatedProductattributes(string user_gid, productattributes_list values)
        {


            msSQL = " update  pmr_mst_tproductattributes  set " +
          " attribute_name = '" + values.attribute_name + "'" +
          "  where productattribute_gid='" + values.productattribute_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Product Attributes Updated Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Product Attributes";
            }




        }

        public void DadeleteProductattributesSummary(string productattribute_gid, productattributes_list values)
        {
            msSQL = "  delete from pmr_mst_tproductattributes where productattribute_gid='" + productattribute_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Attributes Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Product Attributes";
            }
        }

        public void DaGetassignproductattribute(string user_gid, string productattribute_gid, MdlProductattributes values)
        {
            msSQL = " select a.productgroup_gid,a.product_gid,a.product_code,a.product_name,a.productuom_gid," +
                " b.productgroup_name,c.productuom_name,'' as attribute_make, '' as attribute_value,'" + productattribute_gid + "' as  productattribute_gid from pmr_mst_tproduct a" +
                " left join pmr_mst_tproductgroup b on a.productgroup_gid=b.productgroup_gid" +
                " left join pmr_mst_tproductuom c on a.productuom_gid=c.productuom_gid" +
                " where a.product_gid not in (select product_gid from pmr_mst_tattribute2product where attribute_gid='" + productattribute_gid + "')" +
                " order by a.product_name asc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getassignproductattribute_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getassignproductattribute_list
                    {


                        productgroup_gid = dt["productgroup_gid"].ToString(),
                        product_code = dt["product_code"].ToString(),
                        product_name = dt["product_name"].ToString(),
                        productuom_gid = dt["productuom_gid"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
                        productgroup_name = dt["productgroup_name"].ToString(),
                        productuom_name = dt["productuom_name"].ToString(),
                        attribute_make = dt["attribute_make"].ToString(),
                        attribute_value = dt["attribute_value"].ToString(),
                        productattribute_gid = dt["productattribute_gid"].ToString()


                    });
                    values.Getassignproductattribute_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaProductassign(string user_gid, productattributes_list values)
        {
            for (int i = 0; i < values.assign_list.ToArray().Length; i++)
            {
                string msGetGid = objcmnfunctions.GetMasterGID("ATPT");

                msSQL = " insert into pmr_mst_tattribute2product (" +
                        " attribute2product_gid," +
                        " attribute_gid," +
                        " productgroup_gid," +
                        " product_gid," +
                        " productuom_gid," +
                        " attribute_make," +
                        " attribute_value," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.assign_list[i].productattribute_gid + "'," +
                        "'" + values.assign_list[i].productgroup_gid + "'," +
                        "'" + values.assign_list[i].product_gid + "'," +
                        "'" + values.assign_list[i].productuom_gid + "'," +
                        "'" + values.assign_list[i].attribute_make + "'," +
                        "'" + values.assign_list[i].attribute_value + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Product Assign Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Product Assign";
                }

            }






        }

        public void DaGetunassignproductattribute(string user_gid, string productattribute_gid, MdlProductattributes values)
        {
            msSQL = " select a.attribute2product_gid, a.productgroup_gid, a.product_gid, a.productuom_gid, a.attribute_value,a.attribute_make," +
                " b.productgroup_name,c.product_code,c.product_name,d.productuom_name,'" + productattribute_gid + "' as  productattribute_gid from pmr_mst_tattribute2product a" +
                " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid" +
                " left join pmr_mst_tproduct c on a.product_gid=c.product_gid" +
                " left join pmr_mst_tproductuom d on a.productuom_gid=d.productuom_gid" +
                " where  a.attribute_gid='" + productattribute_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getassignproductattribute_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getassignproductattribute_list
                    {


                        productgroup_gid = dt["productgroup_gid"].ToString(),
                        product_code = dt["product_code"].ToString(),
                        product_name = dt["product_name"].ToString(),
                        productuom_gid = dt["productuom_gid"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
                        productgroup_name = dt["productgroup_name"].ToString(),
                        productuom_name = dt["productuom_name"].ToString(),
                        attribute_make = dt["attribute_make"].ToString(),
                        attribute_value = dt["attribute_value"].ToString(),
                        productattribute_gid = dt["productattribute_gid"].ToString(),
                        attribute2product_gid = dt["attribute2product_gid"].ToString(),


                    });
                    values.Getassignproductattribute_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaProductunassign(string user_gid, productattributes_list values)
        {
            for (int i = 0; i < values.assign_list.ToArray().Length; i++)
            {
               
                msSQL = "  delete from pmr_mst_tattribute2product where attribute_gid='" + values.assign_list[i].productattribute_gid + "' and product_gid='" + values.assign_list[i].product_gid + "'  ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Product Unassign Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Product Unassign";
                }

            }






        }
    }
}