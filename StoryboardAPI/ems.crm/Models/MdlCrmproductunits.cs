using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlCrmproductunits
    {

        public List<summaryproductunit_list> summaryproductunit_list { get; set; }

        public List<summaryproductunitgrid_list> summaryproductunitgrid_list { get; set; }
        public List<breadcrumb_list1> breadcrumb_list1 { get; set; }

    }
    public class summaryproductunit_list : result
    {
        public string productuomclass_gid { get; set; }
        public string productuomclass_code { get; set; }
        public string productuomclass_name { get; set; }
    }
    public class summaryproductunitgrid_list : result
    {
        public string productuom_gid { get; set; }
        public string productuomclass_gid { get; set; }
        public string productuomclass_code { get; set; }
        public string productuomclass_name { get; set; }
        public string productuom_code { get; set; }
        public string productuom_name { get; set; }
        public string sequence_level { get; set; }
        public string convertion_rate { get; set; }
        public string baseuom_flag { get; set; }

    }






}