using System;
using System.Collections.Generic;

namespace ems.crm.Models
{

    public class MdlSource : result
    {
        public List<source_list> sourcelist { get; set; }
       public List<breadcrumblist2> breadcrumblist { get; set; }


    }


    public class source_list : result
    {
        public string source_gid { get; set; }
        public string source_name { get; set; }
        public string source_description { get; set; }
        

    }
    public class sourcelist
    {
        public List<sourcedtl> sourcedtl { get; set; }
    }
    public class sourcedtl
    {

        public string source_gid { get; set; }
        public string source_code { get; set; }
        public string source_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string source_description { get; set; }

    }

    //public class breadcrumblist : result
    //{

    //    public string module_name1 { get; set; }
    //    public string sref1 { get; set; }
    //    public string module_name2 { get; set; }
    //    public string sref2 { get; set; }
    //    public string module_name3 { get; set; }
    //    public string sref3 { get; set; }


    //}


}