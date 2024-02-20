using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.crm.Models;
using static ems.crm.Models.product_list;

namespace ems.crm.Models
{
    public class MdlProducts : result
    {
       // internal List<product_image> product_image;

        public List<product_list> product_list { get; set; }
        public List<productexport_list> productexport_list { get; set; }

        public List<Getproducttypedropdowns> Getproducttypedropdowns { get; set; }
        public List<Getproductgroupdropdowns> Getproductgroupdropdowns { get; set; }
        public List<Getproductunitclassdropdowns> Getproductunitclassdropdowns { get; set; }
        public List<Getproductunitdropdowns> Getproductunitdropdowns { get; set; }
        public List<Getcurrencydropdowns> Getcurrencydropdowns { get; set; }

        public List<editproductsummary_list> editproductsummary_list { get; set; }
        public List<GetProductattributes_list> GetProductattributes_list { get; set; }
        public List<breadcrumb_list> breadcrumb_list { get; set; }
        public List<product_images> product_images  { get; set; }
    }
  
    public class GetProductattributes_list : result
    {

        public string attribute_code { get; set; }
        public string attribute_name { get; set; }
        public string attribute_make { get; set; }
        public string attribute_value { get; set; }
        public string product_gid { get; set; }



    }
    public class editproductsummary_list : result
    {

        public string currency_code { get; set; }
        public string batch_flag { get; set; }
        public string serial_flag { get; set; }
        public string purchasewarrenty_flag { get; set; }
        public string expirytracking_flag { get; set; }
        public string product_desc { get; set; }
        public string avg_lead_time { get; set; }
        public string product_gid { get; set; }
        public string productgroup_name { get; set; }
        public string product_name { get; set; }
        public string product_code { get; set; }
        public string productuom_name { get; set; }


    }
    public class Getcurrencydropdowns : result
    {
        public string currency_code { get; set; }
        public string currencyexchange_gid { get; set; }


    }
    public class Getproductunitdropdowns : result
    {
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }


    }
    public class Getproductunitclassdropdowns : result
    {
        public string productuomclass_gid { get; set; }
        public string productuomclass_code { get; set; }
        public string productuomclass_name { get; set; }

    }
    public class Getproducttypedropdowns : result
    {
        public string producttype_name { get; set; }
        public string producttype_gid { get; set; }

    }
    public class Getproductgroupdropdowns : result
    {
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }

    }

    public class productexport_list : result
    {


        public string lspath1 { get; set; }


        public string lsname { get; set; }






    }
    public class product_list : result
    {


        public string producttype_name { get; set; }
        public string productgroup_name { get; set; }
        public string productgroup_code { get; set; }
        public string product_price { get; set; }
        public string cost_price { get; set; }
        public string productuomclass_code { get; set; }
        public string productuom_code { get; set; }
        public string productuomclass_name { get; set; }
        public string stockable { get; set; }
        public string productuom_name { get; set; }
        public string product_type { get; set; }
        public string Status { get; set; }
        public string serial_flag { get; set; }
        public string lead_time { get; set; }
      //  public string product_image { get; set; }
        //public string name { get; set; }

        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string product_code { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

        public string product_desc { get; set; }
        public string currency_code { get; set; }
        public string avg_lead_time { get; set; }
        public string purchasewarrenty_flag { get; set; }
        public string expirytracking_flag { get; set; }
        public string batch_flag { get; set; }


        public class mdlProducts
        {
            public string Name { get; set; }
            public string ContentType { get; set; }
            public byte[] Data { get; set; }
        }

        public class product_images : result
        {
            public string product_gid { get; set; }
            public string product_image { get; set; }
            public string name { get; set; }
        }



    }
}