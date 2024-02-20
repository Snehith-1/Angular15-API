using ems.crm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ems.crm.Models.region_list;

namespace ems.crm.Models
{
    public class MdlMyvisit : result

    {
        public List<Myvisit_list> Myvisitlist { get; set; }

        public List<Todayvisit_list> Todayvisitlist { get; set; }

        public List<Upcomingvisit_list> Upcomingvisitlist { get; set; }

        public List<product_list1> productlist { get; set; }
        public List<productgroup_list1> productgrouplist { get; set; }

      public List<breadcrumb_list> breadcrumb_list { get; set; }




    }

    public class Myvisit_list : result
    {
        public string leadbank_gid { get; set; }
        public string leadbank_name { get; set; }
        public string contact_details { get; set; }

        public string customer_address { get; set; }
        public string region_name { get; set; }
        public string schedule_type { get; set; }
        public string schedule { get; set; }

        public string ScheduleRemarks { get; set; }
        public string schedule_status { get; set; }


    }

    public class Todayvisit_list : result
    {
        public string leadbank_gid { get; set; }
        public string leadbank_name { get; set; }
        public string contact_details { get; set; }

        public string customer_address { get; set; }
        public string region_name { get; set; }
        public string schedule_type { get; set; }
        public string schedule { get; set; }

        public string ScheduleRemarks { get; set; }
        public string schedule_status { get; set; }

    }

    public class Upcomingvisit_list : result
    {
        public string leadbank_gid { get; set; }
        public string leadbank_name { get; set; }
        public string contact_details { get; set; }

        public string customer_address { get; set; }
        public string region_name { get; set; }
        public string schedule_type { get; set; }
        public string schedule { get; set; }

        public string ScheduleRemarks { get; set; }
        public string schedule_status { get; set; }

    }
    //public class product_list1 : result
    //{
    //    public string product_gid { get; set; }
    //    public string product_name { get; set; }


    //}
    public class productgroup_list1 : result
    {
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }


    }
    public class online_list : result
    {
        public string dateof_demo { get; set; }
        public string leadbank_gid { get; set; }

        public string meeting_time { get; set; }
        public string technical_assist { get; set; }
        public string schedule_type { get; set; }
        public string prospective_percentage { get; set; }
        public string product_name { get; set; }
        public string product_group { get; set; }
        public string demo_remarks { get; set; }


    }

    public class offline_list : result
    {
        public string dateof_visit { get; set; }
        public string leadbank_gid { get; set; }

        public string meeting_time { get; set; }
        public string visit_by { get; set; }
        public string contact_person { get; set; }

        public string Location { get; set; }

        public string schedule_type { get; set; }
        public string prospective_percentage { get; set; }
        public string product_name { get; set; }
        public string product_group { get; set; }
        public string meeting_remarks { get; set; }


    }
    


}





    




