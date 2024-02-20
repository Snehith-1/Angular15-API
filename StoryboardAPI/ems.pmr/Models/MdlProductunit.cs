﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.pmr.Models
{
    public class MdlProductunit :result
    {
        public List<productunit_list> productunit_list { get; set; }
        public List<productunitgrid_list> productunitgrid_list { get; set; }
    }

    public class productunit_list : result
    {

        public string productuomclass_gid { get; set; }
        public string productuomclass_code { get; set; }
        public string productuomclass_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }


    }

        public class productunitgrid_list : result
    {

        public string productuom_gid { get; set; }
        public string productuom_code { get; set; }
        public string productuom_name { get; set; }
        public string sequence_level { get; set; }
        public string convertion_rate { get; set; }
        public string baseuom_flag { get; set; }
        public string total_count { get; set; }
        public string productuomclass_gid { get; set; }
        public string productuomclass_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }


    }
}