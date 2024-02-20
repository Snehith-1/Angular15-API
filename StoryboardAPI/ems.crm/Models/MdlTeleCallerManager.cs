using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlTeleCallerManager
    {
        public List<telecaller_list> telecallerlist { get; set; }
        public List<Telecallermanager_list> Telecallermanagerlist { get; set; }
    }

    public class telecaller_list : result
    {
        public string campaign_gid { get; set; }

        public string campaign_location { get; set; }

        public string assign_to { get; set; }

        public string branch_name { get; set; }

        public string employeecount { get; set; }
        public string assigned_leads { get; set; }
        
        public string followup { get; set; }
       
        public string prospect { get; set; }
        public string drop_status { get; set; }
        public string lead2campaign_gid { get; set; }

        public string total { get; set; }
        public string campaign_title { get; set; }
         public string employee_gid { get; set; }

        public string user { get; set; }



        

        public string newleads { get; set; }

        public string created_by { get; set; }

        public string created_date { get; set; }

 }
    public class Telecallermanager_list : result
    {

        public string campaign_gid { get; set; }
        public string user { get; set; }
        public string total { get; set; }
        public string followup { get; set; }
        public string prospect { get; set; }
        public string drop_status { get; set; }
        public string created_date { get; set; }
       

    }

}
