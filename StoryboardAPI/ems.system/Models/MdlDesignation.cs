using System;
using System.Collections.Generic;

namespace ems.system.Models
{

    public class MdlDesignation : result
    {
        public List<designation_list> designationlist { get; set; }
    }

    //Other Application  List
    public class designation_list : result
    {
        public string designation_gid { get; set; }
        public string designation_name { get; set; }
        public string designation_description { get; set; }

    }



}