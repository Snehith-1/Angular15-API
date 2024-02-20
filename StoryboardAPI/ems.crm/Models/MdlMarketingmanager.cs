using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlMarketingmanager
    {
        public List<marketing_list> marketing_list { get; set; }
        public List<transfer_list> transfer_list { get; set; }
        public List<transfername_list> transfername_list { get; set; }
        public List<transfernameonchange_list> transfernameonchange_list { get; set; }

        public List<marketingmanagergrid_list> marketingmanagergrid_list { get; set; }
        public List<Getassignproductattribute_list> Getassignproductattribute_list { get; set; }

    }

    public class marketing_list : result
    {
        public string campaign_gid { get; set; }

        public string campaign_location { get; set; }

        public string assign_to { get; set; }
        public string branch_name { get; set; }
        public string employeecount { get; set; }
        public string assigned_leads { get; set; }
        public string newleads { get; set; }
        public string followup { get; set; }
        public string visit { get; set; }
        public string prospect { get; set; }
        public string drop_status { get; set; }
        public string customer { get; set; }

        public string lead2campaign_gid { get; set; }

        public string total { get; set; }

      

        public string campaign_title { get; set; }

        public string employee_gid { get; set; }

        public string user { get; set; }

        public string leadstage_gid { get; set; }

        
        public string productuomclass_gid { get; set; }

        public string productuomclass_name { get; set; }

        public string created_by { get; set; }

        public string created_date { get; set; }




    }
    public class marketingmanagergrid_list : result
    {

        public string campaign_gid { get; set; }
        public string user { get; set; }
        public string department_name { get; set; }
        public string total { get; set; }
        public string newleads { get; set; }
        public string followup { get; set; }
        public string visit { get; set; }
        public string prospect { get; set; }
        public string drop_status { get; set; }
        public string customer { get; set; }
        public string created_date { get; set; }


    }
    public class assign_list : result
    {
        public string attribute_make { get; set; }
        public string attribute_value { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string message { get; set; }
        public string product_code { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string productattribute_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
        public string status { get; set; }


    }
    public class Getassignproductattribute_list : result
    {

        public string leadbankcontact_gid { get; set; }
        public string campaign_gid { get; set; }
        public string leadstage_name { get; set; }
        public string leadbank_gid { get; set; }
        public string lead2campaign_gid { get; set; }
        public string created_by { get; set; }
        public string internal_notes { get; set; }
        public string region_name { get; set; }
        public string contact_details { get; set; }
        public string call_response { get; set; }
        public string leadbank_name { get; set; }
        public string employee_gid { get; set; }
        




    }
    public class productattributes_list : result
    {

        
        public string productattribute_gid { get; set; }
        public string attribute_code { get; set; }
        public string attribute_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

        public assign_list[] assign_list;


    }

    public class schedule_list : result
    { 
        public string schedule_remarks { get;set; }
        public string schedule_time { get;set; }
        public string schedule_type { get;set; }
        public string schedule_date { get;set; }
        public string leadbank_gid { get;set; }
        public string reference_gid { get;set; }
        public string employee_gid { get;set; }

        public schedule_list[] schedules_list;

    }

    public class transfer_list : result 
    {
        public string campaign_gid { get; set; }

        public string campaign_title { get; set;}

        public string   employee_gid {  get; set; } 

        public string user_name { get; set; }   
        public string leadbank_gid { get; set; }   
        public string reference_gid { get; set; }

        public transfer_list[] trransfer_list;


    }
    

    public class transfername_list : result
    {
        public string employee_gid { get; set; }

        public string user_gid { get; set; }

        public string user_name { get; set; }
    }

    public class transfernameonchange_list : result
    {
        public string employee_gid { get; set; }

        public string user_gid { get; set; }

        public string user_name { get; set; }
    }


}