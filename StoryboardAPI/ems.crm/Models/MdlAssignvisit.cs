using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlAssignvisit : result
    {
        public List<assignvisit_list> assignvisitlist { get; set; }
        public List<Getmarketingteamdropdown> Getmarketingteamdropdown { get; set; }
        public List<Getexecutedropdown> Getexecutedropdown { get; set; }
        public List<breadcrumb_list> breadcrumb_list { get; set; }
     


    }
    public class assignvisit_list : result
    {

        
        public string leadbank_gid { get; set; }
        public string employee_gid { get; set; }
        public string schedulelog_gid { get; set; }
        public string leadbank_name { get; set; }
        public string contact_details { get; set; }
        public string executive { get; set; }
        public string customer_address { get; set; }
        //public string leadbank_region { get; set; }
        public string schedule_type { get; set; }
        public string schedule_dateandtime { get; set; }
        public string schedule_remarks { get; set; }
        public string assign_to { get; set; }
        public string updated_by { get; set; }
        public string campaign_gid { get; set; }


    }
    public class Getmarketingteamdropdown : result
    {
        public string campaign_title { get; set; }
        public string campaign_gid { get; set; }

    }
    public class Getexecutedropdown : result
    {
        public string employee_gid { get; set; }
        public string executive { get; set; }

        

    }
    public class assignvisitlist : result
    {

        public assignvisitlist[] assignvisit_list;
        public string leadbank_gid { get; set; }
        public string employee_gid { get; set; }
        public string schedulelog_gid { get; set; }
        public string leadbank_name { get; set; }
        public string contact_details { get; set; }
        public string executive { get; set; }
        public string customer_address { get; set; }
        public string assignto { get; set; }
        //public string leadbank_region { get; set; }
        public string schedule_type { get; set; }
        public string schedule_dateandtime { get; set; }
        public string schedule_remarks { get; set; }
        public string assign_to { get; set; }
        public string updated_by { get; set; }
        public string campaign_gid { get; set; }




    }
}
