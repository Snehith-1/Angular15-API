using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.system.Models
{
    public class MdlTriggerUser :result
    {
        public List<trigger_list> trigger_list { get; set; }
        //public List<employee_list> employee_list { get; set; }
    }
    public class trigger_list
    {
        public string remarks { get; set; }
        public string triggeruser_gid { get; set; }
        public string trigger_gid { get; set; }
        public string triggeruser_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string deleted_date { get; set; }
        public string deleted_by { get; set; }
    }
    public class triggeruser : result
    {
        public string remarks { get; set; }
        public string triggeruser_gid { get; set; }
        public string trigger_gid { get; set; }
        public string triggeruser_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public char rbo_status { get; set; }
        public char delete_flag { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string deleted_date { get; set; }
        public string deleted_by { get; set; }
        public List<trigger_list> trigger_list { get; set; }
        public List<employee_list> employee_list { get; set; }
    }
    //public class MdlEmployee : result
    //{
    //    public List<employee_list> employee_list { get; set; }
    //}
    //public class employee_list
    //{
    //    public string employee_gid { get; set; }
    //    public string employee_name { get; set; }
    //}
}