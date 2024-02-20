using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.pmr.Models
{
    public class MdlProduct : result
    {
        public List<product_list> product_list { get; set; }
        public List<productexport_list> productexport_list { get; set; }

        public List<Getproducttypedropdown> Getproducttypedropdown { get; set; }
        public List<Getproductgroupdropdown> Getproductgroupdropdown { get; set; }
        public List<Getproductunitclassdropdown> Getproductunitclassdropdown { get; set; }
        public List<Getproductunitdropdown> Getproductunitdropdown { get; set; }
        public List<Getcurrencydropdown> Getcurrencydropdown { get; set; }

        public List<editproductsummary_list> editproductsummary_list { get; set; }
        public List<GetProductattributes_list> GetProductattributes_list { get; set; }





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
    public class Getcurrencydropdown : result
    {
        public string currency_code { get; set; }
        public string currencyexchange_gid { get; set; }


    }
    public class Getproductunitdropdown : result
    {
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }


    }
    public class Getproductunitclassdropdown : result
    {
        public string productuomclass_gid { get; set; }
        public string productuomclass_code { get; set; }
        public string productuomclass_name { get; set; }

    }
    public class Getproducttypedropdown : result
    {
        public string producttype_name { get; set; }
        public string producttype_gid { get; set; }

    }
    public class Getproductgroupdropdown : result
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

        //API Models


        public class Rootobject
        {
            public string id { get; set; }
            public string name { get; set; }
            public Age_Range age_range { get; set; }
            public string birthday { get; set; }
            public string email { get; set; }
            public string first_name { get; set; }
            public string gender { get; set; }
            public Hometown hometown { get; set; }
            public string last_name { get; set; }
            public Location location { get; set; }
            public Friends friends { get; set; }
            public Likes likes { get; set; }
            public Picture picture { get; set; }
            public Posts posts { get; set; }
        }

        public class Age_Range
        {
            public int min { get; set; }
        }

        public class Hometown
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Location
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Friends
        {
            public object[] data { get; set; }
            public Summary summary { get; set; }
        }

        public class Summary
        {
            public int total_count { get; set; }
        }

        public class Likes
        {
            public Datum[] data { get; set; }
            public Paging paging { get; set; }
        }

        public class Paging
        {
            public Cursors cursors { get; set; }
            public string next { get; set; }
        }

        public class Cursors
        {
            public string before { get; set; }
            public string after { get; set; }
        }

        public class Datum
        {
            public string[] emails { get; set; }
            public string link { get; set; }
            public string id { get; set; }
            public DateTime created_time { get; set; }
        }

        public class Picture
        {
            public Data data { get; set; }
        }

        public class Data
        {
            public int height { get; set; }
            public bool is_silhouette { get; set; }
            public string url { get; set; }
            public int width { get; set; }
        }

        public class Posts
        {
            public Datum1[] data { get; set; }
            public Paging1 paging { get; set; }
        }

        public class Paging1
        {
            public string previous { get; set; }
            public string next { get; set; }
        }

        public class Datum1
        {
            public string link { get; set; }
            public string full_picture { get; set; }
            public DateTime created_time { get; set; }
            public string id { get; set; }
        }



    }
}