using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.system.Models
{
    public class employee_list : employee
    {
        public List<employee> employee { get; set; }
    }
    public class employee : result
    {
        public string user_gid { get; set; }
        public string user_code { get; set; }
        public string employee_qualification { get; set; }
        public string user_firstname { get; set; }
        public string user_lastname { get; set; }
        public string user_password { get; set; }
        public string user_status { get; set; }
        public string employee_gid { get; set; }
        public string designation_gid { get; set; }
        public string employee_mobileno { get; set; }
        public string employee_dob { get; set; }
        public string employee_emailid { get; set; }
        public string employee_gender { get; set; }
        public string department_gid { get; set; }
        public string employee_photo { get; set; }
        public string useraccess { get; set; }
        public string engagement_type { get; set; }
        public string biometric_id { get; set; }
        public string attendance_flag { get; set; }
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string department_name { get; set; }
        public string designation_name { get; set; }
        public string employee_name { get; set; }
        public string company_name { get; set; }
        public string gender { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string entity_flag { get; set; }
        public string per_address1 { get; set; }
        public string per_address2 { get; set; }
        public string per_country_gid { get; set; }
        public string per_country_name { get; set; }
        public string per_state { get; set; }
        public string per_city { get; set; }
        public string per_postal_code { get; set; }
        public string temp_address1 { get; set; }
        public string temp_address2 { get; set; }
        public string temp_country_gid { get; set; }
        public string temp_country_name { get; set; }
        public string temp_state { get; set; }
        public string temp_city { get; set; }
        public string temp_postal_code { get; set; }
        public string role_name { get; set; }
        public string role_gid { get; set; }
        public string employee_reportingto_name { get; set; }
        public string employee_reportingto { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string baselocation_gid { get; set; }
        public string baselocation_name { get; set; }
        public string user_access { get; set; }
        public string employeereporting_to { get; set; }
        public string marital_status { get; set; }
        public string marital_status_gid { get; set; }
        public string bloodgroup_name { get; set; }
        public string bloodgroup_gid { get; set; }
        public string joining_date { get; set; }
        public DateTime joiningdate { get; set; }
        public string personal_phone_no { get; set; }
        public string personal_emailid { get; set; }
        public string remarks { get; set; }
        public string relive_date { get; set; }
        public string employee_status { get; set; }
    }

    public class role_list : rolemaster
    {
        public List<rolemaster> rolemaster { get; set; }

    }
    public class rolemaster : result
    {

        public string role_gid { get; set; }
        public string role_name { get; set; }

    }
    public class reportingto_list : reportingto
    {
        public List<reportingto> reportingto { get; set; }
    }
    public class reportingto : result
    {
        public string employee_gid { get; set; }
        public string user_gid { get; set; }
        public string user_code { get; set; }
        public string employee_name { get; set; }
    }
    public class country_list : country
    {
        public List<country> country { get; set; }
    }
    public class country : result
    {
        public string country_gid { get; set; }
        public string country_name { get; set; }
    }
    //public class entity_list : entity
    //{
    //    public List<entity> entity { get; set; }
    //}
    public class entity : result
    {
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string entity_description { get; set; }
    }
    public class export : result
    {
        public string lspath { get; set; }
        public string lsname { get; set; }
    }
    public class tasklist : result
    {

        public string user_gid { get; set; }
        public string employee_gid { get; set; }
        public string approval_remarks { get; set; }
        public string approval_type { get; set; }
        public string task_remarks { get; set; }
        public string task_name { get; set; }
        public string task_gid { get; set; }
        public string taskinitiate_gid { get; set; }
        public string task_completeremarks { get; set; }
        public List<tasklists> tasklists { get; set; }
        public List<tasksummarylists> tasksummarylists { get; set; }
        public List<tasksummarylist> tasksummarylist { get; set; }
    }

    public class tasklists
    {
        public string task_gid { get; set; }
        public string task_name { get; set; }
        public string task_remark { get; set; }
        public string employee_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string task_remarks { get; set; }
        public string temptaskinitiate_gid { get; set; }

    }

    public class tasksummarylists
    {
        public string task_gid { get; set; }
        public string task_name { get; set; }
        public string task_remark { get; set; }
        public string employee_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string task_remarks { get; set; }
        public string temptaskinitiate_gid { get; set; }
        public string employee { get; set; }
        public string task_status { get; set; }
        public string taskinitiate_flag { get; set; }
        public string overallinitiate_flag { get; set; }

    }


    public class tasksummarylist
    {
        public string task_gid { get; set; }
        public string task_name { get; set; }
        public string task_remark { get; set; }
        public string employee_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string task_remarks { get; set; }
        public string temptaskinitiate_gid { get; set; }
        public string taskinitiate_gid { get; set; }
        public string taskinitiate_flag { get; set; }
        public string employee_name { get; set; }
        public string complete_flag { get; set; }
        public string completed_date { get; set; }
        public string completed_by { get; set; }
        public string lsemployee_name { get; set; }
        public string lsemployee_gid { get; set; }
        public string task_completeremarks { get; set; }
    }
    public class MdlTaskList : result
    {
        public string taskinitiate_gid { get; set; }
        public string initiate_flag { get; set; }
        public string employee_gid { get; set; }
        public string complete_flag { get; set; }
        public List<tasklists> tasklists { get; set; }
        public List<tasksummarylists> tasksummarylists { get; set; }
        public List<tasksummarylist> tasksummarylist { get; set; }
    }
    public class countlist : result
    {
        public string pending_count { get; set; }
        public string completed_count { get; set; }
    }


}
