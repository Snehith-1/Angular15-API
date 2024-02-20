using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace ems.crm.Models
{

    public class MdlMyCalls : result
    {
        public List<breadcrumblist2> breadcrumb2_list { get; set; }

        public List<mycalls_list> mycallslist { get; set; }
        public List<pending_list> pendinglist { get; set; }

        public List<followup_list> followuplist { get; set; }
        public List<closed_list> closedlist { get; set; }
        public List<drop_list> droplist { get; set; }
        public List<product_list1> productlist { get; set; }
        public List<view_list> viewlist { get; set; }
        public List<log_list> loglist { get; set; }

        public List<productgroup_list2> productgrouplist { get; set; }
        public List<flog_list> floglist { get; set; }
        public List<plog_list> ploglist { get; set; }
        public List<slog_list> sloglist { get; set; }

    }
    public class mycalls_list : result
    {

        public string leadbank_gid { get; set; }
        public string lead2campaign_gid { get; set; }
        public string campaign_title { get; set; }
        public string leadbank_name { get; set; }
        public string contact_details { get; set; }
        public string regionname { get; set; }

        public string call_response { get; set; }
        public string schedule_type { get; set; }
        public string schedule { get; set; }
        public string lead_base { get; set; }
        public string user_gid { get; set; }

        public string prospective_percentage { get; set; }
        public string schedule_remarks { get; set; }
        

    }



    public class pending_list : result
    {

        public string leadbank_gid { get; set; }
        public string lead2campaign_gid { get; set; }
        public string campaign_title { get; set; }
        public string leadbank_name { get; set; }
        public string contact_details { get; set; }
        public string regionname { get; set; }

        public string call_response { get; set; }
        public string schedule_type { get; set; }
        public string schedule { get; set; }
        public string lead_base { get; set; }
        public string user_gid { get; set; }
        public string prospective_percentage { get; set; }
        public string schedule_remarks { get; set; }
        



    }

    public class followup_list : result
    {

        public string leadbank_gid { get; set; }
        public string lead2campaign_gid { get; set; }
        public string campaign_title { get; set; }
        public string leadbank_name { get; set; }
        public string contact_details { get; set; }
        public string regionname { get; set; }
        public string call_response { get; set; }
        public string schedule_type { get; set; }
        public string schedule { get; set; }
        public string lead_base { get; set; }
        public string user_gid { get; set; }
        public string prospective_percentage { get; set; }
        public string schedule_remarks { get; set; }
        



    }

    public class closed_list : result
    {

        public string leadbank_gid { get; set; }
        public string lead2campaign_gid { get; set; }
        public string campaign_title { get; set; }
        public string leadbank_name { get; set; }
        public string contact_details { get; set; }
        public string regionname { get; set; }
        public string schedule_type { get; set; }
        public string schedule { get; set; }
        public string call_response { get; set; }
        public string lead_base { get; set; }
        public string user_gid { get; set; }
        public string prospective_percentage { get; set; }
        public string schedule_remarks { get; set; }
        


    }

    public class drop_list : result
    {

        public string leadbank_gid { get; set; }
        public string lead2campaign_gid { get; set; }
        public string campaign_title { get; set; }
        public string leadbank_name { get; set; }
        public string contact_details { get; set; }
        public string regionname { get; set; }
        public string lead_base { get; set; }
        public string call_response { get; set; }
        public string schedule_type { get; set; }
        public string schedule { get; set; }
        public string user_gid { get; set; }
        public string prospective_percentage { get; set; }
        public string schedule_remarks { get; set; }
       


    }

    public class view_list : result
    {

        public string leadbank_gid { get; set; }
        public string lead2campaign_gid { get; set; }
        public string campaign_title { get; set; }
        public string leadbank_name { get; set; }
        public string contact_details { get; set; }
        public string regionname { get; set; }
        public string lead_base { get; set; }
        public string call_response { get; set; }
        public string schedule_type { get; set; }
        public string schedule { get; set; }
        public string user_gid { get; set; }
        public string prospective_percentage { get; set; }
        public string schedule_remarks { get; set; }
      


    }
    public class product_list1 : result
    {

        public string product_gid { get; set; }
        public string product_name { get; set; }



    }
    public class log_list : result
    {
        public string leadbank_gid { get; set; }
        public string contact_details { get; set; } 

        public string call_response { get; set; }
        public string prospective_percentage { get; set; }
        public string remarks { get; set; }
    }

    public class productgroup_list2: result
    {

        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }
    }
    public class flog_list : result
    {
        public string leadbank_gid { get; set; }
        public string contact_details { get; set; }

        public string call_response { get; set; }
        
        public string prospective_percentage { get; set ; }
        public string remarks { get; set; }
    }
    public class plog_list : result
    {
        public string leadbank_gid { get; set; }
        public string contact_details { get; set; }

        public string call_response { get; set; }
       
        public string prospective_percentage { get; set ; }
        public string remarks { get; set; }
    }

    public class slog_list: result

    {
                public string leadbank_gid { get; set; }
        public string schedule_date { get; set; }
        public string schedule_time { get; set; }
        public string schedule_type { get; set; }
        public string schedule_remarks { get; set; }
    }

    public class breadcrumblist2 : result
    {



        public string module_name1 { get; set; }
        public string sref1 { get; set; }
        public string module_name2 { get; set; }
        public string sref2 { get; set; }
        public string module_name3 { get; set; }
        public string sref3 { get; set; }




    }
}