using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlMarketingTeam
    {
        public List<team_list> Getteammanagerdropdown { get; set; }
       
        public List<marketingteam_list> marketingdtl { get; set; }
        public List<branch_list1> Getbranchdropdown { get; set; }

        public List<breadcrumb_list> breadcrumb_list { get; set; }

        public string branch_gid { get; internal set; }

 

    }
    public class marketingteam_list : result
    {
        internal List<marketingteam_list> marketingdtl;
        public string employee_gid { get; set; }
        public string campaign_gid { get; set; }
        public string branch_gid { get; set; }
        public string campaign_title { get; set; }
        public string campaign_description { get; set; }
        public string campaign_location { get; set; }
        //public string team_name { get; set; }
        public string branch_name { get; set; }
        public string user_firstname { get; set; }
        public string txtteammail { get; set; }
        //public string assign_employee { get; set; }
        //public string assign_manager { get; set; }
        //public string assign_total { get; set; }
        //public string campaign_mailid { get; set; }
        //public string campaign_team { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
       

        //public string termsconditions_gid { get; set; }
        //public string template_name { get; set; }
        //public string payment_terms { get; set; }
        //public string template_content { get; set; }


        public bool status { get; set; }
        public string message { get; set; }
    }
    public class branch_list1 : result
    {
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string campaign_location { get; set; }

    }
    public class team_list : result
    {

        public string employee_gid { get; set; }
        public string user_firstname { get; set; }
        public string campaign_location { get; set; }

    }
    
}
