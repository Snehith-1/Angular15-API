using System;
using System.Collections.Generic;

namespace ems.system.Models
{
    
    public class MdlOtherApplication : result
    {
        public List<otherapplication> otherapplication_list { get; set; }
    }

    //Other Application  List
    public class otherapplication : result
    {
        public string otherapplication_gid { get; set; }
        public string otherapplication_name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
        public string assign_status { get; set; }
        public char rbo_status { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }

    //Employee 
    public class MdlEmployeeassign : result
    {
        public List<employeeasssign_list> employeeasssign_list { get; set; }
        public List<membereasssigned_list> membereasssigned_list { get; set; }
    }
    public class employeeasssign_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string assignotherapplication_gid { get; set; }
    }

    public class Mdlassignmember : result
    {
        public string[] employeelist_gid { get; set; }
        public string otherapplication_gid { get; set; }
    }
    public class membereasssigned_list
    {

        public string activedepartment2member_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string department_name { get; set; }
    }

}