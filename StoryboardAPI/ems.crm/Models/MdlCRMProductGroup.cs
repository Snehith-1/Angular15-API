using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{

    public class MdlCRMProductGroup : result
    {
        public List<productgroup_list> productgroup_list { get; set; }
        public List<breadcrumb_list> breadcrumb_list { get; set; }

    }

    public class productgroup_list : result
    {

        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }
        public string productgroup_code { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

        public source_list[] source_list;


    }
   
}
