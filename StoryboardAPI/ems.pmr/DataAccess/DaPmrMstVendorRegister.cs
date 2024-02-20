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
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Net.NetworkInformation;

namespace ems.pmr.DataAccess
{
    public class DaPmrMstVendorRegister
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetVendorRegisterSummary (MdlPmrMstVendorRegister values)
        {
            msSQL = " SELECT vendorregister_gid, vendor_code, " +
                " vendor_companyname, contactperson_name, " +
                " contact_telephonenumber,vendor_status " +
                " from acp_mst_tvendorregister " +
                " Order by  vendorregister_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<vendor_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new vendor_list
                    {
                        vendorregister_gid = dt["vendorregister_gid"].ToString(),
                        vendor_code = dt["vendor_code"].ToString(),
                        vendor_companyname = dt["vendor_companyname"].ToString(),
                      // created_by = dt["created_by"].ToString(),
                        //created_date = dt["created_date"].ToString(),

                        contactperson_name = dt["contactperson_name"].ToString(),
                        contact_telephonenumber = dt["contact_telephonenumber"].ToString(),
                        vendor_status = dt["vendor_status"].ToString(),



                    });
                    values.vendor_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetBreadCrumb (string user_gid, string module_gid, MdlPmrMstVendorRegister values)
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

        public void DaGetCurrency (MdlPmrMstVendorRegister values)
        {
            msSQL = " select currency_code,currencyexchange_gid  " +
                    " from crm_trn_tcurrencyexchange ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetCurrency>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetCurrency
                    {
                        currency_code = dt["currency_code"].ToString(),
                        currencyexchange_gid = dt["currencyexchange_gid"].ToString(),

                    });
                    values.GetCurrency  = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetCountry(MdlPmrMstVendorRegister values)
        {
            msSQL = " select  country_gid, country_code, country_name " +
                    " from adm_mst_tcountry ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetCountry>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetCountry
                    {
                        country_gid = dt["country_gid"].ToString(),
                        country_name = dt["country_name"].ToString(),
                    });
                    values.GetCountry = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetTax(MdlPmrMstVendorRegister values)
        {
            msSQL = " select  tax_gid, tax_name " +
                     " from acp_mst_ttax  ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetTax>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetTax
                    {
                        tax_gid = dt["tax_gid"].ToString(),
                        tax_name = dt["tax_name"].ToString(),
                    });
                    values.GetTax = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostVendorRegister (string user_gid, vendor_list values)
        {
            msGetGid1 = objcmnfunctions.GetMasterGID("PVRR");
            msGetGid = objcmnfunctions.GetMasterGID("SADM");
            msSQL = " SELECT currency_code FROM crm_trn_tcurrencyexchange WHERE currencyexchange_gid='" + values.currency_code + "' ";
            string lscurrency_code = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " insert into acp_mst_tvendorregister (" +
                " vendorregister_gid, " +
                " vendor_code, " +
                " vendor_companyname, " +
                " contactperson_name, " +
                " contact_telephonenumber, " +
                " email_id," +
                " tin_number," +
                " excise_details," +
                " pan_number," +
                " servicetax_number," +
                " cst_number," +
                " bank_details," +
                " ifsc_code," +
                " rtgs_code, " +
                " vendor_status, " +
                " address_gid," +
                " tax_gid," +
                " currencyexchange_gid) " +
                " values (" +
                "'" + msGetGid1 + "', " +
                 "'" + values.vendor_code + "', " +
                "'" + values.vendor_companyname + "'," +
                "'" + values.contactperson_name + "'," +
                "'" + values.contact_telephonenumber + "'," +   
                "'" + values.email_id + "'," +
                "'" + values.tin_number + "'," +
                "'" + values.excise_details + "'," +
                "'" + values.pan_number + "'," +
                "'" + values.servicetax_number + "'," +
                "'" + values.cst_number + "'," +
                "'" + values.bank_details + "'," +
                "'" + values.ifsc_code + "'," +
                "'" + values.rtgs_code + "'," +
                "'Approval Pending'," +
                  "'" + msGetGid + "', " +
                "'" + values.tax_name + "'," +
                "'" + values.currency_code + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " insert into adm_mst_taddress " +
                " (address_gid, " +
                " address1, " +
                " address2, " +
                " city, " +
                " state, " +
                " postal_code, " +
                " country_gid, " +
                " fax ) " +
                " values (" +
                 "'" + msGetGid + "', " +
                "'" + values.address1 + "'," +
                "'" + values.address2 + "'," +
                "'" + values.city + "'," +
                "'" + values.state + "'," +
                "'" + values.postal_code + "'," +
                "'" + values.country_name + "'," +
                "'" + values.fax + "')" ;

                mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);


            }
            if (mnResult1 != 0)
            {
                values.status = true;
                values.message = "Vendor Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Vendor";
            }

        }

        public void DaGetVendorRegisterDetail (string user_gid, string vendorregister_gid , MdlPmrMstVendorRegister values)
        {
            msSQL = " SELECT a.vendorregister_gid, a.currencyexchange_gid, a.vendor_code,f.tax_name,d.currency_code," +
               " a.vendor_companyname, a.contactperson_name," +
               " a.contact_telephonenumber, a.email_id, b.address1," +
               " b.address2, b.city,a.tin_number,a.excise_details,a.pan_number," +
               " a.servicetax_number,a.cst_number, a.bank_details, a.ifsc_code, a.rtgs_code," +
               " a.blacklist_remarks, a.blacklist_flag, a.blacklist_date, a.blacklist_by, " +
               " b.state, b.postal_code," +
               " b.country_gid, b.fax," +
               " c.country_name" +
               " FROM acp_mst_tvendorregister a " +
               " left join adm_mst_taddress b on  a.address_gid = b.address_gid " +
               " left join adm_mst_tcountry c on  b.country_gid = c.country_gid " +
               " left join crm_trn_tcurrencyexchange d on a.currencyexchange_gid = d.currencyexchange_gid " +
               " left join acp_mst_ttax f on a.tax_gid = f.tax_gid " +
               " where a.vendorregister_gid = '" + vendorregister_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<editvendorregistersummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new editvendorregistersummary_list
                    {


                        vendor_code = dt["vendor_code"].ToString(),
                        tax_name = dt["tax_name"].ToString(),
                        currency_code = dt["currency_code"].ToString(),
                        vendor_companyname = dt["vendor_companyname"].ToString(),
                        contactperson_name = dt["contactperson_name"].ToString(),
                        contact_telephonenumber = dt["contact_telephonenumber"].ToString(),
                        email_id = dt["email_id"].ToString(),
                        address1 = dt["address1"].ToString(),
                        address2 = dt["address2"].ToString(),
                        city = dt["city"].ToString(),
                        tin_number = dt["tin_number"].ToString(),
                        excise_details = dt["excise_details"].ToString(),
                        pan_number = dt["pan_number"].ToString(),
                        servicetax_number = dt["servicetax_number"].ToString(),
                        cst_number = dt["cst_number"].ToString(),
                        bank_details = dt["bank_details"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        rtgs_code = dt["rtgs_code"].ToString(),
                        blacklist_remarks = dt["blacklist_remarks"].ToString(),
                        blacklist_flag = dt["blacklist_flag"].ToString(),
                        blacklist_date = dt["blacklist_date"].ToString(),
                        blacklist_by = dt["blacklist_by"].ToString(),
                        state = dt["state"].ToString(),
                        postal_code = dt["postal_code"].ToString(),
                        fax = dt["fax"].ToString(),
                        country_name = dt["country_name"].ToString(),

                    });
                    values.editvendorregistersummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }



        public void DaPostVendorRegisterUpdate (string user_gid, vendor_list values)

        {
            msSQL = " SELECT country_gid FROM  adm_mst_tcountry WHERE country_name='" + values.country_name + "' ";
            string country_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT currencyexchange_gid FROM crm_trn_tcurrencyexchange WHERE currency_code='" + values.currency_code + "' ";
            string currencyexchange_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " SELECT tax_gid FROM acp_mst_ttax WHERE tax_name='" + values.tax_name + "' ";
            string tax_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " UPDATE acp_mst_tvendorregister a,adm_mst_taddress b " +
                " SET a.vendor_companyname = '" + values.vendor_companyname + "'," +
                " a.vendor_code = '" + values.vendor_code + "'," +
                " a.contactperson_name = '" + values.contactperson_name + "'," +
                " a.contact_telephonenumber = '" + values.contact_telephonenumber + "'," +
                " a.email_id = '" + values.email_id + "'," +
                " a.tin_number = '" + values.tin_number + "'," +
                " a.excise_details = '" + values.excise_details + "'," +
                " a.pan_number = '" + values.pan_number + "'," +
                " a.servicetax_number = '" + values.servicetax_number + "'," +
                " a.cst_number = '" + values.cst_number + "'," +
                " a.bank_details = '" + values.bank_details + "'," +
                " a.ifsc_code = '" + values.ifsc_code + "'," +
                " a.rtgs_code = '" + values.rtgs_code + "'," +
                " b.address1 = '" + values.address1 + "'," +
                " b.address2 = '" + values.address2 + "'," +
                " b.city = '" + values.city + "'," +
                " b.state = '" + values.state + "'," +
                " b.postal_code = '" + values.postal_code + "'," +
                " b.country_gid = '" + country_gid + "'," +
                " b.fax = '" + values.fax + "'," +
                " a.tax_gid = '" + tax_gid + "'," +
                " a.currencyexchange_gid = '" + currencyexchange_gid + "'" +
                " WHERE a.vendorregister_gid = '" + values.vendorregister_gid + "'" ;

            mnResult2 = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult2 != 0)
            {

                values.status = true;
                values.message = "Vendor Updated Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Vendor ";
            }

        }

        public void DaPostVendorRegisterAdditionalInformation(string user_gid, vendor_list values)

        {
           msSQL = " update acp_mst_tvendorregister set" +
                   " bank_details='" + values.bank_details + "', " +
                   " cst_number='" + values.cst_number + "'," +
                   " excise_details='" + values.excise_details + "'," +
                   " ifsc_code='" + values.ifsc_code + "'," +
                   " pan_number='" + values.pan_number + "'," +
                   " rtgs_code='" + values.rtgs_code + "'," +
                   " servicetax_number='" + values.servicetax_number + "'," +
                   " tin_number='" + values.tin_number + "'" +
                   " WHERE vendorregister_gid = '" + values.vendorregister_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Vendor AdditionalInformation Updated Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Vendor ";
            }

        }



        public void DaGetDocumentType(MdlPmrMstVendorRegister values)
        {
            msSQL = "select documenttype_gid,documenttype_name " +
                     " from  acp_mst_tvendordocumenttype  ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetDocumentType>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetDocumentType
                    {
                        documenttype_gid = dt["documenttype_gid"].ToString(),
                        documenttype_name = dt["documenttype_name"].ToString(),
                    });
                    values.GetDocumentType = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaVendorRegisterSummaryDelete(string vendorregister_gid, vendor_list values)
        {
            msSQL = "  Delete from acp_mst_tvendorregister where vendorregister_gid='" + vendorregister_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Vendor Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Vendor";
            }
        }



        


        public void DaGetVendorRegisterDocumentDetail(string user_gid, string vendorregister_gid, MdlPmrMstVendorRegister values)
        {
            msSQL = " select a.vendorregister_gid,a.document_gid,a.document_type,a.document_name,date_format(a.created_date,'%d-%m-%Y')as created_date," +
                    " a.created_by,b.user_firstname from" +
                    " acp_mst_tvendorregisterdocument a" +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by" +
                    " where vendorregister_gid='" + vendorregister_gid + "' " +
                    " order by DATE(a.created_date) asc, created_date desc  ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<editvendorregistersummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new editvendorregistersummary_list
                    {


                        vendorregister_gid = dt["vendorregister_gid"].ToString(),
                        document_gid = dt["document_gid"].ToString(),
                        document_type = dt["document_type"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        user_firstname = dt["user_firstname"].ToString(),
                        

                    });
                    values.editvendorregistersummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetVendorRegisterDocumentDetailDownload(string document_gid, MdlPmrMstVendorRegister values)
        {
            msSQL = "select document_gid,file_path,document_name from acp_mst_tvendorregisterdocument where document_gid = '" + document_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<vendorregister_document>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new vendorregister_document
                    {
                        file_path = dt["file_path"].ToString(),
                        document_name = dt["document_name"].ToString()
                    });
                }
                values.vendorregister_document = getModuleList;
            }
            dt_datatable.Dispose();
        }

        public void DaPostVendorRegisterAttachment(string user_gid, vendor_list values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("SVDM");
            string file_path = ConfigurationManager.AppSettings["file_path"];

            try
            {
                string lsfile_gid = objcmnfunctions.GetMasterGID("UPLF");
                string fileExtension = Path.GetExtension(values.document_name).ToLower();
                lsfile_gid = lsfile_gid + fileExtension;

                // Save the file with the calculated file name and extension
                string filePath = Path.Combine(file_path, lsfile_gid);
                File.WriteAllBytes(filePath, new byte[0]); // Write an empty byte array

                // Insert into database
                msSQL = "INSERT INTO acp_mst_tvendorregisterdocument (" +
                        "document_gid, " +
                        "vendorregister_gid, " +
                        "document_type," +
                        "document_name, " +
                        "created_date, " +
                        "file_path," +
                        "created_by) " +
                        "VALUES (" +
                        "'" + msGetGid + "', " +
                        "'" + values.vendorregister_gid + "'," +
                        "'" + values.document_type + "'," +
                        "'" + values.document_name + "', " +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                        "'" + filePath + "', " + // Store the file path
                        "'" + user_gid + "') ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Document Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Adding Vendor";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }












    }
}