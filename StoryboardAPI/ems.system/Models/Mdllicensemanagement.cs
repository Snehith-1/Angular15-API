using System;
using System.Collections.Generic;

namespace ems.system.Models
{

    public class MdlLicensemanagement : result
    {
        public List<licensemanagement_list> licensemanagementlist { get; set; }
    }

    //Other Application  List
    public class licensemanagement_list : result
    {
        public string host_server { get; set; }
        public string server_name { get; set; }
        public  string message_lic { get; set; }
        public string active_flag { get; set; }
        
    }



}