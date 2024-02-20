using System;
using System.Collections.Generic;

namespace ems.system.Models
{

    public class MdlEmployeeProfile : result
    {
        public List<EmployeeProfile> EmployeeProfile { get; set; }
    }

    //Other Application  List
    public class EmployeeProfile : result
    {



        public string user_firstname { get; set; }
        public string user_lastname { get; set; }
        public string employee_dob { get; set; }
        public string employee_mobileno { get; set; }
        public string employee_emailid { get; set; }
        public string employee_qualification { get; set; }
        public string bloodgroup_name { get; set; }

        public string current_password { get; set; }
        public string new_password { get; set; }
        public string conf_password { get; set; }


        public string employee_previous_company { get; set; }
        public string employee_code { get; set; }
        public string previous_occupation { get; set; }
        public string department { get; set; }
        public string date_ofjoining { get; set; }
        public string date_ofrelieving { get; set; }
        public string working_period { get; set; }
        public string HR_namecontactinformation { get; set; }
        public string reason_toleavethepreviouscompany { get; set; }
        public string reporting_to { get; set; }
        public string re_marks { get; set; }

        public string user_name { get; set; }
        public string age { get; set; }
        public string date_ofbirth { get; set; }
        public string relationship_withtheemployee { get; set; }
        public string whether_residingwiththeemployee { get; set; }
        public string put_asnomineefor { get; set; }

        public string provident_fund_account_no { get; set; }
        public string employee_state_insurance_no { get; set; }
        public string date_of_join_of_PF { get; set; }



        public string contact_person { get; set; }
        public string contact_address { get; set; }
        public string contact_no { get; set; }
        public string contact_emailid { get; set; }
        public string remarks { get; set; }

        public string institution_name { get; set; }
        public string degree_diploma { get; set; }
        public string field_ofstudy { get; set; }
        public string date_ofcompletion { get; set; }
        public string additional_notes { get; set; }

        public string name { get; set; }
        public string relationship { get; set; }
        public string date_of_birth { get; set; }

    }



}