using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.pmr.Models
{
    public class MdlProductgroup:result
    {
        public List<productgroup_list> productgroup_list { get; set; }
        public List<mappingvendor_list> mappingvendor_list { get; set; }
        public List<unmappingvendor_list> unmappingvendor_list { get; set; }
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
    public class source_list : result
    {
        public string _id { get; set; }
        public string _name { get; set; }
       
    }
    public class unmappingvendor_list : result
    {

        public string vendor_gid { get; set; }
        public string vendor_companyname { get; set; }
       


    }
    public class mappingvendor_list : result
    {

        public string vendor_gid { get; set; }
        public string vendor_companyname { get; set; }



    }
}