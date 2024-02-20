using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.pmr.Models
{
    public class MdlProductattributes : result
    {
        public List<productattributes_list> productattributes_list { get; set; }
        public List<Getassignproductattribute_list> Getassignproductattribute_list { get; set; }
        
    }
    public class productattributes_list : result
    {

      
        public string productattribute_gid { get; set; }
        public string attribute_code { get; set; }
        public string attribute_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

        public assign_list [] assign_list;

    }
    public class assign_list : result
    {
        public string attribute_make { get; set; }
        public string attribute_value { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string message { get; set; }
        public string product_code { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string productattribute_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
        public string status { get; set; }


    }
    public class Getassignproductattribute_list : result
    {

        public string productgroup_gid { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string productuom_gid { get; set; }
        public string product_gid { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string attribute_make { get; set; }
        public string attribute_value { get; set; }
        public string productattribute_gid { get; set; }
        public string attribute2product_gid { get; set; }
        



    }
}