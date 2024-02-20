﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlMyleads : result
    {
        public List<myleads_list> myleadslist { get; set; }
        public List<currency_codelist> currencycodelist { get; set; }


        public List<inprogress_list> inprogresslist { get; set; }

        public List<customer_list> customerlist { get; set; }

        public List<drop_list1> droplist { get; set; }

        public List<all_list> alllist { get; set; }


        public List<leadbank_list> leadbank_list { get; set; }
        public List<Source_list> Source_list { get; set; }
        public List<regionname_list> regionname_list { get; set; }
        public List<industryname_list> industryname_list { get; set; }
        public List<company_list> company_list { get; set; }
        public List<country_list> country_list { get; set; }
        public List<leadbankedit_list> leadbankedit_list { get; set; }
        public List<leadbankbranch_list> leadbankbranch_list { get; set; }
        public List<leadbankcontact_list> leadbankcontact_list { get; set; }

        public List<breadcrumb_list> breadcrumb_list { get; set; }

    }

    //public class result
    //{
    //    public bool status { get; set; }
    //    public string message { get; set; }
    //}

    public class myleads_list : result
    {

        public string leadbank_gid { get; set; }
        public string leadbank_name { get; set; }
        public string campaign_title { get; set; }
        public string lead2campaign_gid { get; set; }
        public string schedule_type { get; set; }
        
        public string contact_details { get; set; }
        public string schedule_remarks { get; set; }
        public string region_name { get; set; }

    }
    public class currency_codelist : result
    {
        public string currencyexchange_gid { get; set; }
        public string currency_code { get; set; }



    }

    public class inprogress_list : result
    {

        public string leadbank_gid { get; set; }
        public string leadbank_name { get; set; }
        public string campaign_title { get; set; }
        public string lead2campaign_gid { get; set; }
        public string contact_details { get; set; }
        public string region_name { get; set; }

    }

    public class customer_list : result
    {

        public string leadbank_gid { get; set; }
        public string leadbank_name { get; set; }
        public string campaign_title { get; set; }
        public string internal_notes { get; set; }
        public string contact_details { get; set; }
        public string region_name { get; set; }

    }

    public class drop_list1 : result
    {

        public string leadbank_gid { get; set; }
        public string leadbank_name { get; set; }
        public string campaign_title { get; set; }
        public string internal_notes { get; set; }
        public string contact_details { get; set; }
        public string region_name { get; set; }
        public string leadstage_name { get; set; }

    }

    public class all_list : result
    {

        public string leadbank_gid { get; set; }
        public string leadbank_name { get; set; }
        public string campaign_title { get; set; }
        public string internal_notes { get; set; }
        public string contact_details { get; set; }
        public string region_name { get; set; }
        public string leadstage_name { get; set; }

    }

    //public class leadbank_list : result
    //{
    //    public string customer_code { get; set; }
    //    public string leadbank_gid { get; set; }
    //    public string leadbankcontact_name { get; set; }
    //    public string contact_details { get; set; }
    //    public string leadbank_state { get; set; }
    //    public string region_name { get; set; }
    //    public string source_name { get; set; }
    //    public string created_by { get; set; }
    //    public string created_date { get; set; }
    //    public string lead_status { get; set; }
    //    public string assign_to { get; set; }


    //    public string source_gid { get; set; }
    //    public string leadbank_name { get; set; }
    //    public string status { get; set; }
    //    public string designation { get; set; }
    //    public string mobile { get; set; }
    //    public string email { get; set; }
    //    public string country_code1 { get; set; }
    //    public string country_code2 { get; set; }

    //    public string area_code1 { get; set; }
    //    public string area_code2 { get; set; }
    //    public string company_website { get; set; }
    //    public string fax_country_code { get; set; }
    //    public string fax_area_code { get; set; }
    //    public string fax { get; set; }
    //    public string approval_flag { get; set; }
    //    public string phone1 { get; set; }
    //    public string phone2 { get; set; }
    //    public string leadbank_address2 { get; set; }
    //    public string leadbank_address1 { get; set; }
    //    public string leadbank_city { get; set; }
    //    public string leadbank_region { get; set; }
    //    public string leadbank_country { get; set; }
    //    public string leadbank_pin { get; set; }
    //    public string categoryindustry_gid { get; set; }
    //    public string referred_by { get; set; }
    //    public string remarks { get; set; }

    //    public string region_gid { get; set; }
    //    public string leadbank_code { get; set; }


    //    public string country_name { get; set; }
    //    public string country_gid { get; set; }
    //    public string leadbankbranch_name { get; set; }


    //    public string categoryindustry_code { get; set; }
    //    public string category_desc { get; set; }
    //    public string categoryindustry_name { get; set; }
    //    public bool Status { get; set; }
    //    public string message { get; set; }
    //    public string customerbranch_name { get; set; }
    //    public string customercontact_name { get; set; }

    //    public string address1 { get; set; }

    //    public string address2 { get; set; }

    //    public string state { get; set; }

    //    public string country_code { get; set; }

    //    public string main_contact { get; set; }

    //    public string area_code { get; set; }




    //}
    public class customeradd_list : result
    {
        public string leadbank_gid { get; set; }
        public string customername { get; set; }


        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string leadbank_name { get; set; }
        public string contactpersonname { get; set; }
        public string leadbankcontact_name { get; set; }
        public string designation { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string company_website { get; set; }
        public string area_code1 { get; set; }
        public string fax_country_code { get; set; }


        public string fax { get; set; }
        public string credit_days { get; set; }
        public string credit_limit { get; set; }
        public string currency_code { get; set; }
        public string country_code1 { get; set; }
        public string phone2 { get; set; }
        public string customer_address { get; set; }
        public string pin { get; set; }
        public string city { get; set; }
        public string region_name { get; set; }



        public string customer_address1 { get; set; }

        public string customer_city { get; set; }
        public string countrycode { get; set; }
        public string areacode { get; set; }
        public string countrycode1 { get; set; }
        public string areacode1 { get; set; }
        public string country_name { get; set; }
        public string customer_pin { get; set; }
        public string approval_flag { get; set; }
        public string customer_state { get; set; }
        public string phone1 { get; set; }
        public string country_code2 { get; set; }
        public string fax_area_code { get; set; }
        public string area_code2 { get; set; }
        public string country_gid { get; set; }


        public string customer_region { get; set; }

        public string customerbranch_name { get; set; }
        public string customercontact_name { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string main_contact { get; set; }
        public string country_code { get; set; }
        public string area_code { get; set; }

        public string state { get; set; }


}

    //public class Source_list : result
    //{
    //    public string source_gid { get; set; }
    //    public string source_name { get; set; }
    //}


    //public class industryname_list : result
    //{
    //    public string categoryindustry_gid { get; set; }
    //    public string categoryindustry_name { get; set; }
    //}

    //public class company_list : result
    //{
    //    public string leadbank_gid { get; set; }
    //    public string leadbank_name { get; set; }
    //}

    //public class country_list : result
    //{
    //    public string country_name { get; set; }
    //    public string country_gid { get; set; }
    //}

    //public class regionname_list1: result
    //{
    //    public string region_name { get; set; }
    //    public string region_gid { get; set; }
    //}

    //public class leadbankedit_list1 : result
    //{
    //    public string leadbank_gid { get; set; }
    //    public string source_name { get; set; }
    //    public string region_name { get; set; }
    //    public string categoryindustry_name { get; set; }
    //    public string referred_by { get; set; }
    //    public string leadbankbranch_name { get; set; }

    //    public string remarks { get; set; }
    //    public string leadbank_name { get; set; }
    //    public string leadbankcontact_name { get; set; }
    //    public string designation { get; set; }
    //    public string email { get; set; }

    //    public string company_website { get; set; }
    //    public string mobile { get; set; }

    //    public string fax_area_code { get; set; }
    //    public string fax_country_code { get; set; }
    //    public string fax { get; set; }

    //    public string country_code1 { get; set; }
    //    public string area_code1 { get; set; }
    //    public string phone1 { get; set; }
    //    public string country_code2 { get; set; }
    //    public string area_code2 { get; set; }

    //    public string phone2 { get; set; }
    //    public string leadbank_code { get; set; }
    //    public string leadbank_address1 { get; set; }
    //    public string leadbank_address2 { get; set; }
    //    public string leadbank_city { get; set; }
    //    public string leadbank_state { get; set; }

    //    public string country_name { get; set; }
    //    public string approval_flag { get; set; }
    //    public string categoryindustry_gid { get; set; }
    //    public string leadbank_pin { get; set; }

    //}

    public class leadbankbranch_list1 : result
    {
        public string leadbankbranch_name { get; set; }
        public string leadbank_name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string designation { get; set; }
        public string leadbankcontact_name { get; set; }
        public string Address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country_name { get; set; }
        public string pincode { get; set; }
    }

    //public class leadbankcontact_list : result
    //{
    //    public string leadbankbranch_name { get; set; }
    //    public string Address { get; set; }
    //    public string city { get; set; }
    //    public string state { get; set; }
    //    public string country_name { get; set; }
    //    public string pincode { get; set; }

    //}

    //public class breadcrumb_list3 : result
    //{

    //    public string module_name1 { get; set; }
    //    public string sref1 { get; set; }
    //    public string module_name2 { get; set; }
    //    public string sref2 { get; set; }
    //    public string module_name3 { get; set; }
    //    public string sref3 { get; set; }


    //}





}