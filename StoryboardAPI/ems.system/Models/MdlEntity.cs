using System;
using System.Collections.Generic;

namespace ems.system.Models
{

    public class MdlEntity : result
    {
        public List<entity_list> entitylist { get; set; }
    }

    //Other Application  List
    public class entity_list : result
    {
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string entity_description { get; set; }
        public string designation_gid { get; set; }
        public string designation_name { get; set; }
        public string designation_description { get; set; }

    }



}