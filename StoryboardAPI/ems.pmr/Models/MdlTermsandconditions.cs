using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ems.pmr.Models
{
    //public class result
    //{
    //    public bool status { get; set; }
    //    public string message { get; set; }
    //}
    public class MdlTermsandconditions : result
    {
        public List<terms_list> terms_list { get; set; }
    }
    public class terms_list : result
    {
        public string template_content { get; set; }
        public string payment_terms { get; set; }
        public string template_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string termsconditions_gid { get; set; }

    }
}
