using System;
using System.Collections.Generic;
using System.Linq;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
//using System.Web;
//using OfficeOpenXml;
using System.Configuration;
using System.IO;
//using OfficeOpenXml.Style;
using System.Drawing;
using System.Web;
using ems.crm.Models;
//using ems.foundation.Models;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using result = ems.crm.Models.result;
//using MySql.Data.MySqlClient;
//using MySqlX.XDevAPI;
using System.Data.SqlClient;
using static ems.crm.Models.product_list;


namespace ems.crm.DataAccess
{
    public class objDacrm
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetProductSummarys(MdlProducts values)
        {
            msSQL = " SELECT d.producttype_name,b.productgroup_name,b.productgroup_code,a.product_gid, a.product_price, a.cost_price, a.product_code, CONCAT_WS('|',a.product_name,a.size, a.width, a.length) as product_name,  CONCAT(f.user_firstname,' ',f.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date, " +
                    " c.productuomclass_code,e.productuom_code,c.productuomclass_name,(case when a.stockable ='Y' then 'Yes' else 'No ' end) as stockable,e.productuom_name,d.producttype_name as product_type,(case when a.status ='1' then 'Active' else 'Inactive' end) as Status," +
                    " (case when a.serial_flag ='Y' then 'Yes' else 'No' end)as serial_flag,(case when a.avg_lead_time is null then '0 days' else concat(a.avg_lead_time,'  ', 'days') end)as lead_time,a.product_image,a.name  from pmr_mst_tproduct a " +
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
                       // product_image = dt["product_image"].ToString(),
                       // name = dt["name"].ToString(),


                    });
                    values.product_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetProductReportExports(MdlProducts values)
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
                string lspath = ConfigurationManager.AppSettings["exportexcelfile1"] + "/assets/export" + "/" + lscompany_code + "/" + "Purchase/Product/Export/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                

                //values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/TestReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(lspath)))
                        System.IO.Directory.CreateDirectory(lspath);
                }

                string lsname = "Product_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                string lspath1 = ConfigurationManager.AppSettings["exportexcelfile1"] + "/assets/export" + "/" + lscompany_code + "/" + "Purchase/Product/Export/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsname;


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

        public void DaGetproducttypedropdowns(MdlProducts values)
        {
            msSQL = " Select producttype_name,producttype_gid  " +
                    " from pmr_mst_tproducttype ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getproducttypedropdowns>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getproducttypedropdowns
                    {
                        producttype_name = dt["producttype_name"].ToString(),
                        producttype_gid = dt["producttype_gid"].ToString(),
                    });
                    values.Getproducttypedropdowns = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetproductgroupdropdowns(MdlProducts values)
        {
            msSQL = " Select productgroup_gid, productgroup_name from pmr_mst_tproductgroup  " +
                    " order by productgroup_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getproductgroupdropdowns>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getproductgroupdropdowns
                    {
                        productgroup_gid = dt["productgroup_gid"].ToString(),
                        productgroup_name = dt["productgroup_name"].ToString(),
                    });
                    values.Getproductgroupdropdowns = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetproductunitclassdropdowns(MdlProducts values)
        {
            msSQL = " Select productuomclass_gid, productuomclass_code, productuomclass_name  " +
                    " from pmr_mst_tproductuomclass order by productuomclass_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getproductunitclassdropdowns>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getproductunitclassdropdowns
                    {
                        productuomclass_gid = dt["productuomclass_gid"].ToString(),
                        productuomclass_code = dt["productuomclass_code"].ToString(),
                        productuomclass_name = dt["productuomclass_name"].ToString(),
                    });
                    values.Getproductunitclassdropdowns = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetproductunitdropdowns(MdlProducts values)
        {
            msSQL = " select productuom_name,productuom_gid from pmr_mst_tproductuom a left join pmr_mst_tproductuomclass b on b.productuomclass_gid= a.productuomclass_gid  " +
                    " order by a.sequence_level ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getproductunitdropdowns>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getproductunitdropdowns
                    {
                        productuom_name = dt["productuom_name"].ToString(),
                        productuom_gid = dt["productuom_gid"].ToString(),

                    });
                    values.Getproductunitdropdowns = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetcurrencydropdowns(MdlProducts values)
        {
            msSQL = " select currency_code,currencyexchange_gid  " +
                    " from crm_trn_tcurrencyexchange ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getcurrencydropdowns>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getcurrencydropdowns
                    {
                        currency_code = dt["currency_code"].ToString(),
                        currencyexchange_gid = dt["currencyexchange_gid"].ToString(),

                    });
                    values.Getcurrencydropdowns = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaPostProducts(string user_gid, product_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("PPTM");
            msSQL = " SELECT currency_code FROM crm_trn_tcurrencyexchange WHERE currencyexchange_gid='" + values.currency_code + "' ";
            string lscurrency_code = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " insert into pmr_mst_tproduct (" +
                    " product_gid," +
                    " product_code," +
                    " product_name," +
                " product_desc, " +
                " productgroup_gid, " +
                " productuomclass_gid, " +
                " productuom_gid, " +
                " currency_code," +
                " cost_price, " +
                " avg_lead_time, " +
                " stockable, " +
                " status, " +
                " producttype_gid, " +
                " purchasewarrenty_flag, " +
                " expirytracking_flag, " +
                " batch_flag," +
                " serial_flag," +
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
        public void DaeditProductSummarys(string user_gid, string product_gid, MdlProducts values)
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
        public void DaGetProductattributesSummarys(string user_gid, string product_gid, MdlProducts values)
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

        public void DaGetproductunitclassdropdownonchanges(string user_gid, string productuomclass_gid, MdlProducts values)
        {
            msSQL = " select productuom_name,productuom_gid from pmr_mst_tproductuom a left join pmr_mst_tproductuomclass b on b.productuomclass_gid= a.productuomclass_gid  " +
                     " where b.productuomclass_gid ='" + productuomclass_gid + "' order by a.sequence_level  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getproductunitdropdowns>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getproductunitdropdowns
                    {
                        productuom_name = dt["productuom_name"].ToString(),
                        productuom_gid = dt["productuom_gid"].ToString(),

                    });
                    values.Getproductunitdropdowns = getModuleList;
                }
            }
        }

        public void DaGetproductunitclassdropdownonchangenames(string user_gid, string productuomclass_name, MdlProducts values)
        {
            msSQL = " select productuom_name,productuom_gid from pmr_mst_tproductuom a left join pmr_mst_tproductuomclass b on b.productuomclass_gid= a.productuomclass_gid  " +
                     " where b.productuomclass_name ='" + productuomclass_name + "' order by a.sequence_level  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getproductunitdropdowns>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getproductunitdropdowns
                    {
                        productuom_name = dt["productuom_name"].ToString(),
                        productuom_gid = dt["productuom_gid"].ToString(),

                    });
                    values.Getproductunitdropdowns = getModuleList;
                }
            }
        }
        public void DaUpdatedProducts(string user_gid, product_list values)
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
        public void DaUpdatedProductcosts(string user_gid, product_list values)
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

        public void DadeleteProductSummarys(string product_gid, product_list values)
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
        public void DaenquiryProductSummarys(string user_gid, string product_gid, product_list values)
        {



            msSQL = " Select product_gid from crm_trn_tcostpricerequest WHERE product_gid='" + product_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Already Request Raised For Enquiry";
            }
            else
            {
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
        public void DaProductUploadExcels(HttpRequest httpRequest, string user_gid, result objResult, product_list values)
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

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = ConfigurationManager.AppSettings["importexcelfile1"];

                if (!Directory.Exists(lsfilePath))
                    Directory.CreateDirectory(lsfilePath);

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
                    // Your code logic here
                }
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }



        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlProducts values)
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



        

        public void UpdateProductImagePath(string imagePath, string fileName, string fileExtension, string productGid)
        {
            try
            { 


                //msSQL = "UPDATE pmr_mst_tproduct " +
                //                     "SET product_image = @productImage, " +
                //                     "name = @fileName, " +
                //                     "content_type = @fileExtension " +
                //                     "WHERE product_gid = @productGid";

                msSQL = "Update  pmr_mst_tproduct  set product_image = '" + imagePath + "'," +
                       "name ='" + fileName + "', " +
                       "content_type='" + fileExtension + "'" +
                       "WHERE product_gid = '" + productGid + "'";
                      mnResult =objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

            }

               
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the data update process
                throw ex;
            }
        }

       

      



       

        public void DadownloadImages(string product_gid, MdlProducts values)
        {
            msSQL = "SELECT product_image, name FROM pmr_mst_tproduct WHERE product_gid = '" + product_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<product_images>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new product_images
                    {
                        product_image = dt["product_image"].ToString(),
                        name = dt["name"].ToString()
                    });
                }
                values.product_images = getModuleList;
            }
            dt_datatable.Dispose();
        }







    }
}
