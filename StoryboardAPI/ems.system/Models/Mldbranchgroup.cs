using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.system.Models
{
    public class Mldbranchgroup
    {
        public List<branchgroup_list> branchgrouplist { get; set; }
    }

    //Other Application  List
    public class branchgroup_list : result
    {
        public string branchgroup_gid { get; set; }
        public string branchgroup_name { get; set; }
    }



}