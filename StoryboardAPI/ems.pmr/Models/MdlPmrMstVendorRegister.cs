using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.pmr.Models
{
    public class MdlPmrMstVendorRegister  : result
    {

        public List<vendor_list> vendor_list { get; set; }
        //public List<productexport_list> productexport_list { get; set; }

        public List<GetCountry> GetCountry { get; set; }
        public List<GetTax> GetTax { get; set; }
        public List<GetCurrency> GetCurrency { get; set; }
        public List<editvendorregistersummary_list> editvendorregistersummary_list { get; set; }
        public List<GetProductattributes_list> GetProductattributes_list { get; set; }
        public List<breadcrumb_list> breadcrumb_list { get; set; }
        public List<GetDocumentType> GetDocumentType { get; set; }
        public List<vendorregister_document> vendorregister_document { get; set; }
    }

    public class vendorregister_document : result
    {
        public string file_path { get; set; }
        public string document_name { get; set; }
        public string document_gid { get; set; }

    }


    public class GetCountry : result
    {
        public string country_gid { get; set; }
        public string country_name { get; set; }

    }
    public class GetCurrency : result
    {
        public string currency_code { get; set; }
        public string currencyexchange_gid { get; set; }





    }
    public class GetDocumentType  : result
    {
        public string documenttype_gid { get; set; }
        public string documenttype_name { get; set; }





    }

    public class editvendorregistersummary_list : result
    {

        public string vendorregister_gid { get; set; }
        public string vendor_code { get; set; }
        public string vendor_companyname { get; set; }
        public string contactperson_name { get; set; }
        public string contact_telephonenumber { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string currency_code { get; set; }
        public string country_name { get; set; }
        public string fax { get; set; }
        public string tax_name { get; set; }
        public string vendor_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string email_id { get; set; }
        public string tin_number { get; set; }
        public string excise_details { get; set; }
        public string pan_number { get; set; }
        public string servicetax_number { get; set; }
        public string cst_number { get; set; }
        public string bank_details { get; set; }
        public string ifsc_code { get; set; }
        public string rtgs_code { get; set; }
        public string address_gid { get; set; }
        public string tax_gid { get; set; }
        public string currencyexchange_gid { get; set; }
        public string country_gid { get; set; }

        public string address1 { get; set; }
        public string address2 { get; set; }
        public string pincode { get; set; }
        public string country { get; set; }
        public string blacklist_date { get; set; }
        public string blacklist_flag { get; set; }
        public string blacklist_remarks { get; set; }

        public string blacklist_by { get; set; }

        public string document_gid { get; set; }
        public string document_type { get; set; }
        public string document_name { get; set; }
        public string user_firstname { get; set; }
        public string documenttype_name { get; set; }
       
        public string file_path { get; set; }
        public byte[] file_data { get; set; }
     




    }

    public class GetTax: result
    {
        public string tax_gid { get; set; }
        public string tax_name { get; set; }
        

    }
    //public class Getproductunitclassdropdowns : result
    //{
    //    public string productuomclass_gid { get; set; }
    //    public string productuomclass_code { get; set; }
    //    public string productuomclass_name { get; set; }

    //}
    //public class Getproducttypedropdowns : result
    //{
    //    public string producttype_name { get; set; }
    //    public string producttype_gid { get; set; }

    //}
    //public class Getproductgroupdropdowns : result
    //{
    //    public string productgroup_gid { get; set; }
    //    public string productgroup_name { get; set; }

    //}

    //public class productexport_list : result
    //{


    //    public string lspath1 { get; set; }


    //    public string lsname { get; set; }






    //}
    public class vendor_list : result
    {
        public string vendorregister_gid { get; set; }
        public string vendor_code { get; set; }
        public string vendor_companyname { get; set; }
        public string contactperson_name { get; set; }
        public string contact_telephonenumber { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string currency_code { get; set; }
        public string country_name { get; set; }
        public string tax_name { get; set; }
        public string vendor_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string email_id { get; set; }
        public string tin_number { get; set; }
        public string excise_details { get; set; }
        public string pan_number { get; set; }
        public string servicetax_number { get; set; }
        public string cst_number { get; set; }
        public string bank_details { get; set; }
        public string ifsc_code { get; set; }
        public string rtgs_code { get; set; }
        public string address_gid { get; set; }
        public string tax_gid { get; set;}
        public string currencyexchange_gid { get; set;}
        public string country_gid { get; set; }
        public string fax { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string pincode { get; set; }
        public string country { get; set; }
        public string blacklist_date { get; set; }
        public string blacklist_flag { get; set; }
        public string blacklist_remarks { get; set; }
        public string blacklist_by { get; set; }

        public string document_gid { get; set; }
        public string document_type { get; set; }
        public string document_name { get; set; }
        public string user_firstname { get; set; }
        public string file_path  { get; set; }
        public byte[] file_data { get; set; }
        public string documenttype_name { get; set; }
    }

    }
