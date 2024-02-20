using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.system.Models
{
    public class MdlSystemMaster : result
    {
        public List<master_list> master_list { get; set; }
        public List <location_list> location_list { get; set; }
        //public List<employee_list> employee_list { get; set; }
    }
    public class location_list
    {
        public string baselocation_gid { get; set; }
        public string baselocation_name { get; set; }
    }
    public class master_list
    {
        public string remarks { get; set; }
        public string bloodgroup_gid { get; set; }
        public string bloodgroup_name { get; set; }
        public string physicalstatus_gid { get; set; }
        public string physicalstatus_name { get; set; }
        public string baselocation_gid { get; set; }
        public string baselocation_name { get; set; }
        public string calendargroup_gid { get; set; }
        public string calendargroup_name { get; set; }
        public string clientrole_gid { get; set; }
        public string clientrole_name { get; set; }
        public string salutation_gid { get; set; }
        public string salutation_name { get; set; }
        public string project_gid { get; set; }
        public string project_name { get; set; }
        public string task_gid { get; set; }
        public string task_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string deleted_date { get; set; }
        public string deleted_by { get; set; }

        public string cluster_gid { get; set; }
        public string region_gid { get; set; }
        public string zone_gid { get; set; }
        public string regionhead_gid { get; set; }
        public string businesshead_gid { get; set; }
        public string groupbusinesshead_gid { get; set; }
        public string clusterhead_gid { get; set; }
        public string zonalhead_gid { get; set; }
        public string producthead_gid { get; set; }

    }
    public class master : result
    {
        public string remarks { get; set; }
        public string bloodgroup_gid { get; set; }
        public string bloodgroup_name { get; set; }
        public string baselocation_gid { get; set; }
        public string baselocation_name { get; set; }
        public string physicalstatus_gid { get; set; }
        public string physicalstatus_name { get; set; }
        public string calendargroup_gid { get; set; }
        public string calendargroup_name { get; set; }
        public string clientrole_gid { get; set; }
        public string clientrole_name { get; set; }
        public string salutation_gid { get; set; }
        public string salutation_name { get; set; }
        public string project_gid { get; set; }
        public string project_name { get; set; }
        public string task_gid { get; set; }
        public string task_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
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
        public List<master_list> master_list { get; set; }
        public List<employee_list> employee_list { get; set; }
        public string status_bloodgroup { get; set; }
        public string status_physicalstatus { get; set; }
        public string status_baselocation { get; set; }
        public string status_calendargroup { get; set; }
        public string status_clientrole { get; set; }
        public string status_salutation{ get; set; }
        public string status_project { get; set; }

    }

    public class cluster : result
    {
        public string cluster_gid { get; set; }
        public string cluster_name { get; set; }
        public string cluster_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public List<locationlist> locationlist { get; set; }
        public List<cluster_list> cluster_list { get; set; }
        public List<clusterhead_list> clusterhead_list { get; set; }
        public List<zonalhead_list> zonalhead_list { get; set; }
    }
    public class locationlist
    {
        public string baselocation_gid { get; set; }
        public string baselocation_name { get; set; }
        public string cluster2baselocation_gid { get; set; }
    }
    public class cluster_list
    {
        public string cluster_gid { get; set; }
        public string cluster_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string region2cluster_gid { get; set; }
    }
    public class region : result
    {
        public string region_gid { get; set; }
        public string region_name { get; set; }
        public string region_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public List<region_list> region_list { get; set; }
        public List<cluster_list> cluster_list { get; set; }
        public List<regionhead_list> regionhead_list { get; set; }
        public List<clusterhead_list> clusterhead_list { get; set; }
        public List<zonalhead_list> zonalhead_list { get; set; }
    }

    public class region_list
    {
        public string region_gid { get; set; }
        public string region_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string zone2region_gid { get; set; }
    }

    public class zone : result
    {
        public string zone_gid { get; set; }
        public string zone_name { get; set; }
        public string zone_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public List<zone_list> zone_list { get; set; }
        public List<region_list> region_list { get; set; }
        public List<zonalhead_list> zonalhead_list { get; set; }
        public List<producthead_list> producthead_list { get; set; }
    }

    public class zone_list
    {
        public string zone_gid { get; set; }
        public string zone_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
    }
    public class mdlvertical : result
    {
        public List<vertical_list> vertical_list { get; set; }
    }
    public class vertical_list
    {
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
    }
    public class mdlemployee : result
    {
        public List<employeelist> employeelist { get; set; }
    }
    public class employeelist
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class mdlregionhead : result
    {
        public string regionhead_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string region_gid { get; set; }
        public string region_name { get; set; }
        public string region_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public string regionhead_status { get; set; }
        public List<employeelist> employeelist { get; set; }
        public List<vertical_list> vertical_list { get; set; }
        public List<region_list> region_list { get; set; }
    }

    public class regionhead_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string regionhead_gid { get; set; }
        public string vertical_name { get; set; }
        public string region_name { get; set; }
    }
    public class mdlbusinesshead : result
    {
        public string groupbusinesshead_gid { get; set; }
        public string businesshead_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string zone_gid { get; set; }
        public string zone_name { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public string businesshead_status { get; set; }
        public List<businesshead_list> businesshead_list { get; set; }
        public List<employeelist> employeelist { get; set; }
        public List<vertical_list> vertical_list { get; set; }
        public List<zone_list> zone_list { get; set; }
    }
    public class businesshead_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string businesshead_gid { get; set; }
        public string vertical_name { get; set; }
        public string zone_name { get; set; }
        public string groupbusinesshead_gid { get; set; }
    }
    //Cluster Head codes
    public class clusterhead_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string vertical_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string cluster_name { get; set; }
        public string clusterhead_gid { get; set; }
    }

    public class mdlclusterhead : result
    {
        public string clusterhead_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string cluster_gid { get; set; }
        public string cluster_name { get; set; }
        public string clusterhead_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public List<cluster_list> cluster_list { get; set; }
        public List<employeelist> employeelist { get; set; }
        public List<vertical_list> vertical_list { get; set; }
    }
    // Zonal Head Codes
    public class mdlzonalhead : result
    {
        public string zonalhead_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string zonal_gid { get; set; }
        public string zonal_name { get; set; }
        public string zonalhead_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public List<employeelist> employeelist { get; set; }
        public List<vertical_list> vertical_list { get; set; }
        public List<zone_list> zone_list { get; set; }
    }

    
    public class zonalhead_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string zonal_name { get; set; }
        public string zonalhead_gid { get; set; }
        public string vertical_name { get; set; }
    }
    // Product Head Codes
    public class mdlproducthead : result
    {
        public string producthead_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string producthead_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public List<employeelist> employeelist { get; set; }
        public List<zone_list> zone_list { get; set; }
    }
    public class producthead_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string product_name { get; set; }
        public string producthead_gid { get; set; }
    }
    //Task
    public class MdlTask : result
    {
        public string task_gid { get; set; }
        public string task_code { get; set; }
        public string task_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string task_description { get; set; }
        public string tat { get; set; }
        public string Status { get; set; }
        public string assignedto_name { get; set; }
        public string escalationmailto_name { get; set; }
       

        public List<taskassigned_to> taskassigned_to { get; set; }
        public List<assignedto_list> assigned_to { get; set; }
        public List<assignedto_list> assignedto_general { get; set; }

        public List<escalationmailto_list> escalationmail_to { get; set; }
        public List<escalationmailto_list> escalationmailto_general { get; set; }

    }

    public class assignedto_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class escalationmailto_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }


    public class taskassigned_to
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string supportteam2member_gid { get; set; }
    }
}