using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlMarketingCurrency 
    {
        public List<currency_list> currency_list { get; set; }
        public List<Getcountrydropdown> Getcountrydropdown { get; set; }
        public List<breadcrumb_list1> breadcrumb_list { get; set; }


    }
    

    public class currency_list : result
    {
        public string currencyexchange_gid { get; set; }
        public string currency_code { get; set; }
        public string exchange_rate { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        ////public string country { get; set; }
        public string country_name { get; set; }

        //public string termsconditions_gid { get; set; }
        //public string template_name { get; set; }
        //public string payment_terms { get; set; }
        //public string template_content { get; set; }


        public bool status { get; set; }
        public string message { get; set; }
    }


    public class Getcountrydropdown : result
    {
        public string country_gid { get; set; }
        public string country_name { get; set; }

    }

    public class breadcrumb_list1 : result
    {

        public string module_name1 { get; set; }
        public string sref1 { get; set; }
        public string module_name2 { get; set; }
        public string sref2 { get; set; }
        public string module_name3 { get; set; }
        public string sref3 { get; set; }


    }



}