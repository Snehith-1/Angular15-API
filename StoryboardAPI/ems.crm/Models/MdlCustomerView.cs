using System;
using System.Collections.Generic;

namespace ems.crm.Models
{

    public class MdlCustomerView : result
    {
        public List<from_list> mailidlist { get; set; }

        public List<cont_list> contlist { get; set; }
        public List<enquiry_list> enquirydtl { get; set; }
    }
    public class from_list
    {
        public string mail { get; set; }
       public string company_gid { get; set; }
    }

    public class cont_list
    {
        public string approvecontact_gid { get; set; }
        public string contact_person { get; set; }
        public string source { get; set; }
        public string region { get; set; }
        public string company_code { get; set; }
        public string fax { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string designation { get; set; }
        public string branch_name { get; set; }
        public string email { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string state { get; set; }
        public string company_name { get; set; }
        public string tele1 { get; set; }
        public string tele2 { get; set; }
        public string website { get; set; }
        public string approve_flag { get; set; }
        public string mobile { get; set; }
        public string leadbank_gid { get; set; }
        public string employee_gid { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public string request_from { get; set; }
        public string updated_flag { get; set; }
        public string pincode { get; set; }




    }

    public class enquiry_list : result
    {
        public string enquiry_gid { get; set; }
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string enquiry_date { get; set; }
        //public string leadbank_gid { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string contact_number { get; set; }
        public string enquiry_referencenumber { get; set; }
        public string customer_rating { get; set; }
        public string lead_status { get; set; }
        public string enquiry_status { get; set; }

        public string contact_details { get; set; }
        public string contact_person { get; set; }
        public string contact_email { get; set; }
        public string contact_address { get; set; }
        public string enquiry_type { get; set; }

        //public string team_name { get; set; }
        public string potorder_value { get; set; }
        //public string created_by { get; set; }
        //public string created_date { get; set; }
        //public string teammanager_name { get; set; }
        //public string txtteammail { get; set; }
        //public string assign_employee { get; set; }
        //public string assign_manager { get; set; }
        //public string assign_total { get; set; }
        //public string campaign_mailid { get; set; }
        //public string campaign_team { get; set; }





        //public string termsconditions_gid { get; set; }
        //public string template_name { get; set; }
        //public string payment_terms { get; set; }
        //public string template_content { get; set; }





        public bool status { get; set; }
        public string message { get; set; }
    }

}
