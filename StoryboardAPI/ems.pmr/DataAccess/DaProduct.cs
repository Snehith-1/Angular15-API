﻿using System;
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
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ems.pmr.DataAccess
{
    public class DaProduct
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetProductSummary(MdlProduct values)
        {
            msSQL = " SELECT d.producttype_name,b.productgroup_name,b.productgroup_code,a.product_gid, a.product_price, a.cost_price, a.product_code, CONCAT_WS('|',a.product_name,a.size, a.width, a.length) as product_name,  CONCAT(f.user_firstname,' ',f.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date, " +
                    " c.productuomclass_code,e.productuom_code,c.productuomclass_name,(case when a.stockable ='Y' then 'Yes' else 'No ' end) as stockable,e.productuom_name,d.producttype_name as product_type,(case when a.status ='1' then 'Active' else 'Inactive' end) as Status," +
                    " (case when a.serial_flag ='Y' then 'Yes' else 'No' end)as serial_flag,(case when a.avg_lead_time is null then '0 days' else concat(a.avg_lead_time,'  ', 'days') end)as lead_time  from pmr_mst_tproduct a " +
                    " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid " +
                    " left join pmr_mst_tproductuomclass c on a.productuomclass_gid = c.productuomclass_gid " +
                    " left join pmr_mst_tproducttype d on a.producttype_gid = d.producttype_gid " +
                    " left join pmr_mst_tproductuom e on a.productuom_gid = e.productuom_gid " +
                    " left join adm_mst_tuser f on f.user_gid=a.created_by order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<product_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new product_list
                    {
                        product_gid = dt["product_gid"].ToString(),
                        product_name = dt["product_name"].ToString(),
                        product_code = dt["product_code"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),

                        producttype_name = dt["producttype_name"].ToString(),
                        productgroup_name = dt["productgroup_name"].ToString(),
                        productgroup_code = dt["productgroup_code"].ToString(),
                        product_price = dt["product_price"].ToString(),
                        cost_price = dt["cost_price"].ToString(),
                        productuomclass_code = dt["productuomclass_code"].ToString(),
                        productuom_code = dt["productuom_code"].ToString(),
                        productuomclass_name = dt["productuomclass_name"].ToString(),
                        stockable = dt["stockable"].ToString(),

                        productuom_name = dt["productuom_name"].ToString(),
                        product_type = dt["product_type"].ToString(),
                        Status = dt["Status"].ToString(),
                        serial_flag = dt["serial_flag"].ToString(),
                        lead_time = dt["lead_time"].ToString(),


                    });
                    values.product_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetProductReportExport(MdlProduct values)
        {
            msSQL = " SELECT d.producttype_name as 'Product Type',b.productgroup_name as 'Product Group', a.product_code as 'Product Code', CONCAT_WS('|',a.product_name,a.size, a.width, a.length) as 'Product',c.productuomclass_name as 'Unit', a.cost_price as 'Cost Price', " +
                    " (case when a.avg_lead_time is null then '0 days' else concat(a.avg_lead_time,'  ', 'days') end)as 'Avg Lead Time',(case when a.status ='1' then 'Active' else 'Inactive' end) as 'Product Status'" +
                    "   from pmr_mst_tproduct a " +
                    " left join pmr_mst_tproductgroup b on a.productgroup_gid = b.productgroup_gid " +
                    " left join pmr_mst_tproductuomclass c on a.productuomclass_gid = c.productuomclass_gid " +
                    " left join pmr_mst_tproducttype d on a.producttype_gid = d.producttype_gid " +
                    " left join pmr_mst_tproductuom e on a.productuom_gid = e.productuom_gid " +
                    " left join adm_mst_tuser f on f.user_gid=a.created_by order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Product Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
             string lspath = ConfigurationManager.AppSettings["exportexcelfile"] + "/assets/export" + "/" + lscompany_code + "/" + "Purchase/Product/Export/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/TestReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(lspath)))
                        System.IO.Directory.CreateDirectory(lspath);
                }

                string lsname = "Product_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                string lspath1 = ConfigurationManager.AppSettings["exportexcelfile"] + "/assets/export" + "/" + lscompany_code + "/" + "Purchase/Product/Export/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsname;

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(lspath1);
                using (var range = workSheet.Cells[1, 1, 1, 8])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                  range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(file);

                var getModuleList = new List<productexport_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    
                        getModuleList.Add(new productexport_list
                        {
  
                            lsname = lsname,
                            lspath1 = lspath1,



                        });
                        values.productexport_list = getModuleList;
                    
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }

        public void DaGetproducttypedropdown(MdlProduct values)
        {
            msSQL = " Select producttype_name,producttype_gid  " +
                    " from pmr_mst_tproducttype ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getproducttypedropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getproducttypedropdown
                    {
                        producttype_name = dt["producttype_name"].ToString(),
                        producttype_gid = dt["producttype_gid"].ToString(),
                    });
                    values.Getproducttypedropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetproductgroupdropdown(MdlProduct values)
        {
            msSQL = " Select productgroup_gid, productgroup_name from pmr_mst_tproductgroup  " +
                    " order by productgroup_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getproductgroupdropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getproductgroupdropdown
                    {
                        productgroup_gid = dt["productgroup_gid"].ToString(),
                        productgroup_name = dt["productgroup_name"].ToString(),
                    });
                    values.Getproductgroupdropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetproductunitclassdropdown(MdlProduct values)
        {
            msSQL = " Select productuomclass_gid, productuomclass_code, productuomclass_name  " +
                    " from pmr_mst_tproductuomclass order by productuomclass_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getproductunitclassdropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getproductunitclassdropdown
                    {
                        productuomclass_gid = dt["productuomclass_gid"].ToString(),
                        productuomclass_code = dt["productuomclass_code"].ToString(),
                        productuomclass_name = dt["productuomclass_name"].ToString(),
                    });
                    values.Getproductunitclassdropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetproductunitdropdown(MdlProduct values)
        {
            msSQL = " select productuom_name,productuom_gid from pmr_mst_tproductuom a left join pmr_mst_tproductuomclass b on b.productuomclass_gid= a.productuomclass_gid  " +
                    " order by a.sequence_level ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getproductunitdropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getproductunitdropdown
                    {
                        productuom_name = dt["productuom_name"].ToString(),
                        productuom_gid = dt["productuom_gid"].ToString(),

                    });
                    values.Getproductunitdropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetcurrencydropdown(MdlProduct values)
        {
            msSQL = " select currency_code,currencyexchange_gid  " +
                    " from crm_trn_tcurrencyexchange ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getcurrencydropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getcurrencydropdown
                    {
                        currency_code = dt["currency_code"].ToString(),
                        currencyexchange_gid = dt["currencyexchange_gid"].ToString(),

                    });
                    values.Getcurrencydropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaPostProduct(string user_gid, product_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("PPTM");
            msSQL = " SELECT currency_code FROM crm_trn_tcurrencyexchange WHERE currencyexchange_gid='" + values.currency_code + "' ";
            string lscurrency_code = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " insert into pmr_mst_tproduct (" +
                    " product_gid," +
                    " product_code," +
                    " product_name," +
                " product_desc, "  +
                " productgroup_gid, "  +
                " productuomclass_gid, "  +
                " productuom_gid, "  +
                " currency_code,"  +
                " cost_price, "  +
                " avg_lead_time, "  +
                " stockable, "  +
                " status, "  +
                " producttype_gid, "  +
                " purchasewarrenty_flag, " +
                " expirytracking_flag, " +
                " batch_flag,"  +
                " serial_flag,"  +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    "'" + values.product_code + "',";
            if (values.product_name == null || values.product_name == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.product_name.Replace("'", "") + "',";
            }
            msSQL += "'" + values.product_desc + "'," +
                     "'" + values.productgroup_name + "'," +
                     "'" + values.productuomclass_name + "'," +
                     "'" + values.productuom_name + "'," +
                     "'" + lscurrency_code + "'," +
                     "'" + values.cost_price + "'," +
                     "'" + values.avg_lead_time + "'," +
                     "'" + "Y" + "'," +
                     "'" + "1" + "'," +
                     "'" + values.producttype_name + "'," +
                     "'" + values.purchasewarrenty_flag + "'," +
                     "'" + values.expirytracking_flag + "'," +
                     "'" + values.batch_flag + "'," +
                     "'" + values.serial_flag + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Product";
            }

        }
        public void DaeditProductSummary(string user_gid, string product_gid, MdlProduct values)
        {
            msSQL = "   select batch_flag,serial_flag,purchasewarrenty_flag,expirytracking_flag,product_desc,avg_lead_time,b.currency_code,a.product_gid,c.productgroup_name,a.product_name,a.product_code,d.productuom_name " +
                " from pmr_mst_tproduct a " +
                        " left join crm_trn_tcurrencyexchange b on b.currency_code=a.currency_code left  join pmr_mst_tproductgroup c on a.productgroup_gid=c.productgroup_gid" +
                        "  left join pmr_mst_tproductuom d on a.productuom_gid=d.productuom_gid" +
                        " where a.product_gid='" + product_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<editproductsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new editproductsummary_list
                    {


                        productgroup_name = dt["productgroup_name"].ToString(),
                        product_name = dt["product_name"].ToString(),
                        product_code = dt["product_code"].ToString(),
                        productuom_name = dt["productuom_name"].ToString(),
                        batch_flag = dt["batch_flag"].ToString(),
                        serial_flag = dt["serial_flag"].ToString(),
                        expirytracking_flag = dt["expirytracking_flag"].ToString(),
                        purchasewarrenty_flag = dt["purchasewarrenty_flag"].ToString(),
                        product_desc = dt["product_desc"].ToString(),
                        avg_lead_time = dt["avg_lead_time"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
                        currency_code = dt["currency_code"].ToString(),

                    });
                    values.editproductsummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetProductattributesSummary(string user_gid, string product_gid, MdlProduct values)
        {
            msSQL = " select c.product_gid,a.attribute_code, a.attribute_name,b.attribute_make,b.attribute_value from pmr_mst_tproductattributes a " +
                " left join pmr_mst_tattribute2product b on a.productattribute_gid=b.attribute_gid" +
                " left join pmr_mst_tproduct c on b.product_gid=c.product_gid" +
                        " where c.product_gid='" + product_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetProductattributes_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetProductattributes_list
                    {


                        attribute_code = dt["attribute_code"].ToString(),
                        attribute_name = dt["attribute_name"].ToString(),
                        attribute_make = dt["attribute_make"].ToString(),
                        attribute_value = dt["attribute_value"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
 

                    });
                    values.GetProductattributes_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetproductunitclassdropdownonchange(string user_gid, string productuomclass_gid, MdlProduct values)
        {
            msSQL = " select productuom_name,productuom_gid from pmr_mst_tproductuom a left join pmr_mst_tproductuomclass b on b.productuomclass_gid= a.productuomclass_gid  " +
                     " where b.productuomclass_gid ='" + productuomclass_gid + "' order by a.sequence_level  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getproductunitdropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getproductunitdropdown
                    {
                        productuom_name = dt["productuom_name"].ToString(),
                        productuom_gid = dt["productuom_gid"].ToString(),

                    });
                    values.Getproductunitdropdown = getModuleList;
                }
            }
        }

        public void DaGetproductunitclassdropdownonchangename(string user_gid, string productuomclass_name, MdlProduct values)
        {
            msSQL = " select productuom_name,productuom_gid from pmr_mst_tproductuom a left join pmr_mst_tproductuomclass b on b.productuomclass_gid= a.productuomclass_gid  " +
                     " where b.productuomclass_name ='" + productuomclass_name + "' order by a.sequence_level  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getproductunitdropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getproductunitdropdown
                    {
                        productuom_name = dt["productuom_name"].ToString(),
                        productuom_gid = dt["productuom_gid"].ToString(),

                    });
                    values.Getproductunitdropdown = getModuleList;
                }
            }
        }
        public void DaUpdatedProduct(string user_gid, product_list values)
        {
            msSQL = " SELECT productgroup_gid FROM pmr_mst_tproductgroup WHERE productgroup_name='" + values.productgroup_name + "' ";
            string productgroup_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT producttype_gid FROM pmr_mst_tproducttype WHERE producttype_name='" + values.producttype_name + "' ";
            string producttype_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT productuomclass_gid FROM pmr_mst_tproductuomclass WHERE productuomclass_name='" + values.productuomclass_name + "' ";
            string productuomclass_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT productuom_gid FROM pmr_mst_tproductuom WHERE productuom_name='" + values.productuom_name + "' ";
            string productuom_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update  pmr_mst_tproduct  set " +
          " product_name = '" + values.product_name + "'," +
          " product_code = '" + values.product_code + "'," +
          " product_desc = '" + values.product_desc + "'," +
          " currency_code = '" + values.currency_code + "'," +
          " productgroup_gid = '" + productgroup_gid + "'," +
          " producttype_gid = '" + producttype_gid + "'," +
          " productuomclass_gid = '" + productuomclass_gid + "'," +
          " productuom_gid = '" + productuom_gid + "'," +
          " cost_price = '" + values.cost_price + "'," +
          " avg_lead_time = '" + values.avg_lead_time + "'," +
          " purchasewarrenty_flag = '" + values.purchasewarrenty_flag + "'," +
          " expirytracking_flag = '" + values.expirytracking_flag + "'," +
          " batch_flag = '" + values.batch_flag + "'," +
          " serial_flag = '" + values.serial_flag + "'," +
          " updated_by = '" + user_gid + "'," +
          " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where product_gid='" + values.product_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Product Updated Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Product";
            }




        }
        public void DaUpdatedProductcost(string user_gid, product_list values)
        {


            msSQL = " update  pmr_mst_tproduct  set " +
          " cost_price = '" + values.cost_price + "'" +
          "  where product_gid='" + values.product_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Product Cost Updated Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Product Cost";
            }




        }

        public void DadeleteProductSummary(string product_gid, product_list values)
        {
            msSQL = "  delete from pmr_mst_tproduct where product_gid='" + product_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Product";
            }
        }
        public void DaenquiryProductSummary(string user_gid,string product_gid, product_list values)
        {


           
            msSQL = " Select product_gid from crm_trn_tcostpricerequest WHERE product_gid='" + product_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Already Request Raised For Enquiry";
            }
            else {
                msGetGid = objcmnfunctions.GetMasterGID("CSPR");

         

            msSQL = " insert into crm_trn_tcostpricerequest (" +
          " costpricerequest_gid, " +
          " product_gid, " +
          " request_flag, " +
          " requested_by, " +
          " requested_date " +
          " )values ( " +
          "'" + msGetGid + "'," +
          "'" + product_gid + "'," +
          "' Requested '," +
          "'" + user_gid + "'," +
          "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Request Raised For Enquiry";
            }
            else
            {
                values.status = false;
                values.message = "Error While Request Raised For Enquiry";
            }
            }
        }
        public void DaProductUploadExcel(HttpRequest httpRequest, string user_gid, result objResult, product_list values)
        {
            string lscompany_code;
            string excelRange, endRange;
            int rowCount, columnCount;


            try
            {
                int insertCount = 0;
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                //  string lsaudit_gid = httpRequest.Form["auditcreation_gid"];

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Purchase/Product/Import/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

                //lsfilePath = HttpContext.Current.Server.MapPath("/erp_documents" + "/" + lscompany_code + "/Purchase/Product/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

                if ((!System.IO.Directory.Exists(lsfilePath)))
                    System.IO.Directory.CreateDirectory(lsfilePath);


                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;
                FileExtension = Path.GetExtension(FileExtension).ToLower();
                lsfile_gid = lsfile_gid + FileExtension;

                Stream ls_readStream;
                ls_readStream = httpPostedFile.InputStream;
                MemoryStream ms = new MemoryStream();
                ls_readStream.CopyTo(ms);

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                try
                {
                    using (ExcelPackage xlPackage = new ExcelPackage(ms))
                    {
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets["Sheet1"];
                        rowCount = worksheet.Dimension.End.Row;
                        columnCount = worksheet.Dimension.End.Column;
                        endRange = worksheet.Dimension.End.Address;
                    }
                    string status;
                    status = objcmnfunctions.uploadFile(lsfilePath, FileExtension);
                    file.Close();
                    ms.Close();

                    objcmnfunctions.uploadFile(lspath, lsfile_gid);
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = ex.ToString();
                    return;
                }


                //Excel To DataTable


                try
                {
                    lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                    excelRange = "A1:" + endRange + rowCount.ToString();
                    dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);
                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = ex.ToString();
                    return;
                }
                //  Nullable<DateTime> ldcodecreation_date;

                string[] columnNames = dt.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
                string Header_name = "", Header_value = "";
                foreach (DataRow row in dt.Rows)
                {


                    foreach (var i in columnNames)
                    {
                        Header_name = i.Trim();
                        Header_name = Header_name.Replace("*", "");
                        Header_name = Header_name.Replace(" ", "_");

                        Header_value = row[i].ToString();


                    }

                }
                for (int r = 0; r <= dt.Rows.Count - 1; r++)
                {

                    //string unitprefix = String.Concat(Convert.ToString(dt.Rows[r][0]).Where(c => !Char.IsWhiteSpace(c)));
                    //string unitname = String.Concat(Convert.ToString(dt.Rows[r][1]).Where(c => !Char.IsWhiteSpace(c)));
                    //string unitaddress = String.Concat(Convert.ToString(dt.Rows[r][2]).Where(c => !Char.IsWhiteSpace(c)));
                    //string blockprefix = String.Concat(Convert.ToString(dt.Rows[r][3]).Where(c => !Char.IsWhiteSpace(c)));
                    //string blockname = String.Concat(Convert.ToString(dt.Rows[r][4]).Where(c => !Char.IsWhiteSpace(c)));
                    //string sectionprefix = String.Concat(Convert.ToString(dt.Rows[r][5]).Where(c => !Char.IsWhiteSpace(c)));
                    //string sectionname = String.Concat(Convert.ToString(dt.Rows[r][6]).Where(c => !Char.IsWhiteSpace(c)));
                    //string department = String.Concat(Convert.ToString(dt.Rows[r][7]).Where(c => !Char.IsWhiteSpace(c)));
                    //string lsunit_gid, lsunit_code, lsblock_gid, lsblock_code, lssection_gid, lsdepartment_gid, lssection_code, lsunit_name, lsblock_name, lssection_name, lsdepartment_prefix;
                    //msSQL = " select locationunit_name from ams_mst_tlocationunit where locationunit_name='" + unitname + "' ";
                    //lsunit_name = objdbconn.GetExecuteScalar(msSQL);
                    //msSQL = " select locationblock_name from ams_mst_tlocationblock where locationblock_name='" + blockname + "' ";
                    //lsblock_name = objdbconn.GetExecuteScalar(msSQL);
                    //msSQL = " select locationsection_name from ams_mst_tlocationsection where locationsection_name='" + sectionname + "' ";
                    //lssection_name = objdbconn.GetExecuteScalar(msSQL);
                    //if (unitname != lsunit_name && blockname != lsblock_name && sectionname != lssection_name)
                    //{

                    //    lsunit_gid = objcmnfunctions.GetMasterGID("LUNT");
                    //    lsunit_code = objcmnfunctions.GetMasterGID("UC");

                    //    msSQL = " insert into ams_mst_tlocationunit(" +
                    //            " locationunit_gid," +
                    //            " locationunit_code," +
                    //            " locationunit_name," +
                    //            " unit_prefix," +
                    //            " locationunit_address," +
                    //            " created_by, " +
                    //            " created_date)" +
                    //            " values(" +
                    //            " '" + lsunit_gid + "'," +
                    //            " '" + lsunit_code + "'," +
                    //            " '" + unitname + "'," +
                    //            " '" + unitprefix + "',";
                    //    if (unitaddress == null || unitaddress == "")
                    //    {
                    //        msSQL += "'',";
                    //    }
                    //    else
                    //    {
                    //        msSQL += "'" + unitaddress.Replace("'", "") + "',";
                    //    }
                    //    msSQL += "'" + user_gid + "'," +
                    //             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //    if (mnResult != 0)
                    //    {
                    //        lsblock_gid = objcmnfunctions.GetMasterGID("LBLK");
                    //        lsblock_code = objcmnfunctions.GetMasterGID("BC");
                    //        msSQL = " insert into ams_mst_tlocationblock(" +
                    //         " locationblock_gid," +
                    //         " locationblock_code," +
                    //         " locationblock_name," +
                    //         " block_prefix," +
                    //         " locationunit_gid," +
                    //         " created_by, " +
                    //         " created_date)" +
                    //         " values(" +
                    //         " '" + lsblock_gid + "'," +
                    //         " '" + lsblock_code + "'," +
                    //         " '" + blockname + "'," +
                    //         " '" + blockprefix + "'," +
                    //         " '" + lsunit_gid + "',";
                    //        msSQL += "'" + user_gid + "'," +
                    //                 "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //        if (mnResult != 0)
                    //        {

                    //            lssection_gid = objcmnfunctions.GetMasterGID("LSEC");
                    //            lssection_code = objcmnfunctions.GetMasterGID("SC");

                    //            msSQL = " select department_prefix from hrm_mst_tdepartment where department_name='" + department + "' ";
                    //            lsdepartment_prefix = objdbconn.GetExecuteScalar(msSQL);
                    //            msSQL = " select department_gid from hrm_mst_tdepartment where department_name='" + department + "' ";
                    //            lsdepartment_gid = objdbconn.GetExecuteScalar(msSQL);

                    //            msSQL = " insert into ams_mst_tlocationsection(" +
                    //        " locationsection_gid," +
                    //        " locationsection_code," +
                    //        " locationsection_name," +
                    //        " section_prefix," +
                    //        " department_gid," +
                    //        " dept_prefix," +
                    //        " locationunit_gid," +
                    //        " locationblock_gid," +
                    //        " created_by, " +
                    //        " created_date)" +
                    //        " values(" +
                    //        " '" + lssection_gid + "'," +
                    //        " '" + lssection_code + "'," +
                    //        " '" + sectionname + "'," +
                    //        " '" + sectionprefix + "'," +
                    //        " '" + lsdepartment_gid + "'," +
                    //        " '" + lsdepartment_prefix + "'," +
                    //        " '" + lsunit_gid + "'," +
                    //        " '" + lsblock_gid + "',";
                    //            msSQL += "'" + user_gid + "'," +
                    //                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //            if (mnResult != 0)
                    //            {
                    //                values.status = true;
                    //                values.message = "Asset Location Added Successfully";
                    //            }
                    //            else
                    //            {
                    //                values.status = false;
                    //                values.message = "Error While Adding Asset Location";
                    //            }
                    //        }

                    //    }
                    //}
                    //else if (values.locationblock_name != lsblock_name && values.locationsection_name != lssection_name)
                    //{
                    //    lsblock_gid = objcmnfunctions.GetMasterGID("LBLK");
                    //    lsblock_code = objcmnfunctions.GetMasterGID("BC");
                    //    msSQL = " select locationunit_gid from ams_mst_tlocationunit where locationunit_name='" + unitname + "' ";
                    //    lsunit_gid = objdbconn.GetExecuteScalar(msSQL);
                    //    msSQL = " insert into ams_mst_tlocationblock(" +
                    //     " locationblock_gid," +
                    //     " locationblock_code," +
                    //     " locationblock_name," +
                    //     " block_prefix," +
                    //     " locationunit_gid," +
                    //     " created_by, " +
                    //     " created_date)" +
                    //     " values(" +
                    //     " '" + lsblock_gid + "'," +
                    //     " '" + lsblock_code + "'," +
                    //     " '" + blockname + "'," +
                    //     " '" + blockprefix + "'," +
                    //     " '" + lsunit_gid + "',";
                    //    msSQL += "'" + user_gid + "'," +
                    //             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //    if (mnResult != 0)
                    //    {

                    //        lssection_gid = objcmnfunctions.GetMasterGID("LSEC");
                    //        lssection_code = objcmnfunctions.GetMasterGID("SC");
                    //        msSQL = " select department_prefix from hrm_mst_tdepartment where department_name='" + department + "' ";
                    //        lsdepartment_prefix = objdbconn.GetExecuteScalar(msSQL);
                    //        msSQL = " select department_gid from hrm_mst_tdepartment where department_name='" + department + "' ";
                    //        lsdepartment_gid = objdbconn.GetExecuteScalar(msSQL);

                    //        msSQL = " insert into ams_mst_tlocationsection(" +
                    //    " locationsection_gid," +
                    //    " locationsection_code," +
                    //    " locationsection_name," +
                    //    " section_prefix," +
                    //    " department_gid," +
                    //    " dept_prefix," +
                    //    " locationunit_gid," +
                    //    " locationblock_gid," +
                    //    " created_by, " +
                    //    " created_date)" +
                    //    " values(" +
                    //    " '" + lssection_gid + "'," +
                    //    " '" + lssection_code + "'," +
                    //    " '" + sectionname + "'," +
                    //    " '" + sectionprefix + "'," +
                    //    " '" + lsdepartment_gid + "'," +
                    //    " '" + lsdepartment_prefix + "'," +
                    //    " '" + lsunit_gid + "'," +
                    //    " '" + lsblock_gid + "',";
                    //        msSQL += "'" + user_gid + "'," +
                    //                 "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //        if (mnResult != 0)
                    //        {
                    //            values.status = true;
                    //            values.message = "Asset Location Added Successfully";
                    //        }
                    //        else
                    //        {
                    //            values.status = false;
                    //            values.message = "Error While Adding Asset Location";
                    //        }
                    //    }

                    //}
                    //else if (values.locationsection_name != lssection_name)
                    //{
                    //    lssection_gid = objcmnfunctions.GetMasterGID("LSEC");
                    //    lssection_code = objcmnfunctions.GetMasterGID("SC");

                    //    msSQL = " select locationunit_gid from ams_mst_tlocationunit where locationunit_name='" + unitname + "' ";
                    //    lsunit_gid = objdbconn.GetExecuteScalar(msSQL);
                    //    msSQL = " select locationblock_gid from ams_mst_tlocationblock where locationblock_name='" + blockname + "' ";
                    //    lsblock_gid = objdbconn.GetExecuteScalar(msSQL);

                    //    msSQL = " select department_prefix from hrm_mst_tdepartment where department_name='" + department + "' ";
                    //    lsdepartment_prefix = objdbconn.GetExecuteScalar(msSQL);
                    //    msSQL = " select department_gid from hrm_mst_tdepartment where department_name='" + department + "' ";
                    //    lsdepartment_gid = objdbconn.GetExecuteScalar(msSQL);

                    //    msSQL = " insert into ams_mst_tlocationsection(" +
                    //" locationsection_gid," +
                    //" locationsection_code," +
                    //" locationsection_name," +
                    //" section_prefix," +
                    //" department_gid," +
                    //" dept_prefix," +
                    //" locationunit_gid," +
                    //" locationblock_gid," +
                    //" created_by, " +
                    //" created_date)" +
                    //" values(" +
                    //" '" + lssection_gid + "'," +
                    //" '" + lssection_code + "'," +
                    //" '" + sectionname + "'," +
                    //" '" + sectionprefix + "'," +
                    //" '" + lsdepartment_gid + "'," +
                    //" '" + lsdepartment_prefix + "'," +
                    //" '" + lsunit_gid + "'," +
                    //" '" + lsblock_gid + "',";
                    //    msSQL += "'" + user_gid + "'," +
                    //             "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //    if (mnResult != 0)
                    //    {
                    //        values.status = true;
                    //        //values.message = "Asset Location Added Successfully";
                    //        objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                    //    }
                    //    else
                    //    {
                    //        values.status = false;
                    //        objResult.message = "Error occured in uploading Excel Sheet Details";
                    //    }
                    //}


                }

                //if (mnResult != 0)
                //{

                //    objResult.status = true;
                //    objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                //    // values.message= insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                //    dt.Dispose();
                //}


                //else
                //{
                //    objResult.status = false;
                //    objResult.message = "Error occured in uploading Excel Sheet Details";
                //    // values.message= "Error occured in uploading Excel Sheet Details";
                //}


            }


            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }

        }
    }
}