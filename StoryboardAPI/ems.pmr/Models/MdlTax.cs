using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.pmr.Models
{
    public class MdlTax :result
    {
        public List<tax_list> tax_list { get; set; }
    }

    public class tax_list : result
    {
        
        public string tax_gid { get; set; }
        public string tax_name { get; set; }
        public string percentage { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string split_flag { get; set; }
        public string active_flag { get; set; }

    }

}