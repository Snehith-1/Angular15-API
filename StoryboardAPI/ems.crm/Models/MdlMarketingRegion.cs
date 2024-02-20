using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ems.crm.Models.region_list;

namespace ems.crm.Models
{
    public class MdlMarketingRegion : result
    {
        public List<region_list> regionlist { get; set; }
        public List<breadcrumblist4> breadcrumb_list { get; set; }

    }
    public class region_list : result
    {

        public string region_gid { get; set; }
        public string region_code { get; set; }

        public string region_name { get; set; }
        public string city { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }


        public class breadcrumblist4 : result
        {

            public string module_name1 { get; set; }
            public string sref1 { get; set; }
            public string module_name2 { get; set; }
            public string sref2 { get; set; }
            public string module_name3 { get; set; }
            public string sref3 { get; set; }


        }

    }
}