using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.sdc.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class moduleadd : result
    {
        public string module_name { get; set; }
        public string module_prefix { get; set; }
        public string availability { get; set; }
        public string module_code { get; set; }
        public string module_gid { get; set; }
    }

    public class moduledtllist
    {
        public List<moduledtl> moduledtl { get; set; }
    }
    public class moduledtl
    {
        public string module_gid { get; set; }
        public string module_code { get; set; }
        public string module_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string module_prefix { get; set; }
        public string availability { get; set; }
        public string module2customer_gid { get; set; }
        public string customer_name { get; set; }
        public string customer_gid { get; set; }
    }

    public class customer : result
    {
        public string customer_gid { get; set; }
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string customer_city { get; set; }
        public string customer_state { get; set; }
        public string module_gid { get; set; }
        public List<customerlist> customerlist { get; set; }
    }
    public class customerlist
    {
        public string customer_gid { get; set; }
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string customer_city { get; set; }
        public string customer_state { get; set; }
    }

    public class MdlcustomerAssign : result
    {
        public string[] customer_gid { get; set; }
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string customer_city { get; set; }
        public string customer_state { get; set; }
        public string module_gid { get; set; }

    }
}