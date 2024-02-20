using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlMarketingTax : result
    {
        public List<tax_list> tax_list { get; set; }

        public List<breadcrumb_list> breadcrumb_list { get; set; }
        public List<splittax_list> splittax_list { get; set; }  


    }
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }


    }
    public class tax_list : result
    {

        public string tax_gid { get; set; }
        public string tax_name { get; set; }
        public string percentage { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string split_flag { get; set; }
        public string active_flag { get; set; }
        public string taxsplit_name1 { get; set; }
        public string taxsplit_per1 { get; set; }
        public string taxsplit_name2 { get; set; }
        public string taxsplit_per2 { get; set; }
        public string taxsplit_name3 { get; set; }
        public string taxsplit_per3 { get; set; }




    }

    public class splittax_list : result
    {
        public string taxdtl_gid { get; set; }  

        public string tax_gid { get; set; }
        public string taxsplit_name1 { get; set; }
        public string taxsplit_per1 { get; set; }
        public string taxsplit_name2 { get; set; }
        public string taxsplit_per2 { get; set; }
        public string taxsplit_name3 { get; set; }
        public string taxsplit_per3 { get; set; }




    }

    public class breadcrumb_list : result
    {

        public string module_name1 { get; set; }
        public string sref1 { get; set; }
        public string module_name2 { get; set; }
        public string sref2 { get; set; }
        public string module_name3 { get; set; }
        public string sref3 { get; set; }


    }

}