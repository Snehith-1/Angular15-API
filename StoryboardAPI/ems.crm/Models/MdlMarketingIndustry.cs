using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    

    public class MdlMarketingIndustry 
    {
        public List<breadcrumblist> breadcrumb_list { get; set; }

        public List<industry_list> industry_list { get; set; }
        public List<industrydtl> industrydtl { get; set; }

    }

    public class industry_list : result
    {
        public string categoryindustry_gid { get; set; }
        public string categoryindustry_code { get; set; }
        public string categoryindustry_name { get; set; }
        public string category_desc { get; set; }
    }
    public class industryllist
    {
        public List<industrydtl> industrydtl { get; set; }
    }

    public class industrydtl : result
    {

        public string categoryindustry_gid { get; set; }
        public string categoryindustry_code { get; set; }
        public string categoryindustry_name { get; set; }
        public string category_desc { get; set; }
      

    }

    public class breadcrumblist : result
    {



        public string module_name1 { get; set; }
        public string sref1 { get; set; }
        public string module_name2 { get; set; }
        public string sref2 { get; set; }
        public string module_name3 { get; set; }
        public string sref3 { get; set; }




    }



    //public class result
    //{

    //    public string message { get; set; }

    //    public bool status {  get; set; }

    //}
}