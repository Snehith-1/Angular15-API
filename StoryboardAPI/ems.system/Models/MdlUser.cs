using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.system.Models
{
    public class UserData : result
    {
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string user_photo { get; set; }
        public string user_designation { get; set; }
        public string user_department { get; set; }
        public string attendancelogintmp_gid { get; set; }
        

    }

    public class result
    {
        public string message { get; set; }
        public bool status { get; set; }
    }

    public class menu_response : result
    {
        public List<sys_menu> menu_list { get; set; }

    }
    public class branchgrouplist
    {
        public List<branchgroupdtl> branchgroupdtl { get; set; }
    }
    public class branchgroupdtl
    {

        public string branchgroup_gid { get; set; }
        public string branchgroup_code { get; set; }
        public string branchgroup_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

    }
    public class activateemployeelist
    {
        public List<activateemployeedtl> activateemployeedtl { get; set; }
    }
    public class activateemployeedtl
    {

        public string branch_name { get; set; }
        public string department_name { get; set; }
        public string designation_name { get; set; }
        public string user_code { get; set; }
        public string employee_name { get; set; }
        public string joining_date { get; set; }
        public string exit_date { get; set; }
        public string contact { get; set; }
        public string user_status { get; set; }

    }
    public class licensemanagementlist
    {
        public List<licensemanagementdtl> licensemanagementdtl { get; set; }
    }
    public class licensemanagementdtl
    {
        public string license_gid { get; set; }
        public string host_server { get; set; }
        public string machine_id { get; set; }
        public string server_name { get; set; }
        public string port { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string active_flag { get; set; }
        public string message { get; set; }


    }
    public class userreportlist
    {
        public List<userreportdtl> userreportdtl { get; set; }
    }
    public class userreportdtl
    {

        public string branch_name { get; set; }
        public string department_name { get; set; }
        public string designation_name { get; set; }
        public string user_code { get; set; }
        public string employee_mobileno { get; set; }
        public string user_name { get; set; }
        public string user_status { get; set; }

    }



    public class financialyearactivitieslist
    {
        public List<financialyearactivitiesdtl> financialyearactivitiesdtl { get; set; }
    }
    public class financialyearactivitiesdtl
    {

        public string fyear_start { get; set; }
        public string fyear_end { get; set; }
      

    }



    public class employeereportlist
    {
        public List<employeereportdtl> employeereportdtl { get; set; }
    }
    public class employeereportdtl
    {

        public string branch_name { get; set; }
        public string department_name { get; set; }
        public string designation_name { get; set; }
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string user_status { get; set; }
        public string passport_no { get; set; }
        public string workpermit_no { get; set; }
        public string passport_expiredate { get; set; }
        public string workpermit_expiredate { get; set; }
        public string employee_gender { get; set; }
        public string employee_joiningdate { get; set; }
        public string salary { get; set; }
        public string permonth_rate { get; set; }
        public string fin_no { get; set; }
    }


    public class myteam : result
    {
        public List<myteam_list> myteam_list { get; set; }
        //public List<companylogo_list> companylogo_list { get; set; }

        

    }
    public class createvendor : result
    {
        public string attendancelogintmp_gid { get; set; }

        
    }

        public class entityllist
    {
        public List<entitydtl> entitydtl { get; set; }
    }
    public class entitydtl
    {

        public string entity_gid { get; set; }
        public string entity_code { get; set; }
        public string entity_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string entity_description { get; set; }

    }
    public class designationlist
    {
        public List<designationdtl> designationdtl { get; set; }
    }
    public class designationdtl
    {

        public string designation_gid { get; set; }
        public string designation_code { get; set; }
        public string designation_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }


    }
    public class branchlist
    {
        public List<branchdtl> branchdtl { get; set; }
    }
    public class branchdtl
    {

        public string branch_gid { get; set; }
        public string branch_code { get; set; }
        public string branch_name { get; set; }
        public string branch_location { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string branch_prefix { get; set; }

    }
    public class monthlyAttendence : result
    {
        public List<last6MonthAttendence_list> last6MonthAttendence_list { get; set; }
        public string monthyear { get; set; }
        public string totalDays { get; set; }
        public string countPresent { get; set; }
        public string countAbsent { get; set; }
        public string countLeave { get; set; }
        public string countholiday { get; set; }
        public string countWeekOff { get; set; }
    }
    public class last6MonthAttendence_list
    {
        public string monthname { get; set; }
        public string countPresent { get; set; }
        public string countAbsent { get; set; }
    }
    public class Result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class company_policy : Result
    {
        public List<policy_list> policy_list { get; set; }



    }
    public class policy_list
    {
        public string company_policies { get; set; }
        public string policies_description { get; set; }
    }
    //public class myteam : result
    //{
    //    public List<myteam_list> myteam_list { get; set; }
    //}
    public class myteam_list
    {
        public string employee_code { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string employee_mobileno { get; set; }
        public string employee_photo { get; set; }
        public string employeereporting { get; set; }
        public string company_logo_path { get; set; }
        public string welcome_logo { get; set; }
        public string company_name { get; set; }
        




    }

    public class getlogindetails : result
    {
        public List<loginpending_list> loginpending_list { get; set; }
        public List<loginrejected_list> loginrejected_list { get; set; }
    }

    public class loginpending_list
    {
        public string employee_name { get; set; }
        public string attendancelogintmp_gid { get; set; }
        public string loginapply_date { get; set; }
        public string loginattendence_date { get; set; }
        public string login_time { get; set; }
        public string login_reason { get; set; }
        public string login_status { get; set; }
        public string apply_employeegid { get; set; }
    }

    public class loginrejected_list
    {
        public string employee_name { get; set; }
        public string attendancelogintmp_gid { get; set; }
        public string loginapply_date { get; set; }
        public string loginattendence_date { get; set; }
        public string login_time { get; set; }
        public string login_reason { get; set; }
        public string login_status { get; set; }
    }

    public class approvelogin : result
    {
        public string attendancelogintmp_gid { get; set; }
        public string loginattendence_date { get; set; }
        public string apply_employeegid { get; set; }
    }

    // Logout Approval....//

    public class getlogoutdetails : result
    {
        public List<logoutpending_list> logoutpending_list { get; set; }
        public List<logoutrejected_list> logoutrejected_list { get; set; }
    }

    public class logoutpending_list
    {
        public string employee_name { get; set; }
        public string attendancelogouttmp_gid { get; set; }
        public string logoutapply_date { get; set; }
        public string logoutattendence_date { get; set; }
        public string logout_time { get; set; }
        public string logout_reason { get; set; }
        public string logout_status { get; set; }
        public string apply_employeegid { get; set; }
    }

    public class logoutrejected_list
    {
        public string employee_name { get; set; }
        public string attendancelogouttmp_gid { get; set; }
        public string logoutapply_date { get; set; }
        public string logoutattendence_date { get; set; }
        public string logout_time { get; set; }
        public string logout_reason { get; set; }
        public string logout_status { get; set; }
    }

    public class approvelogout : result
    {
        public string attendancelogouttmp_gid { get; set; }
        public string logoutattendence_date { get; set; }
        public string apply_employeegid { get; set; }
    }

    // OD Approval......//

    public class getODdetails : result
    {
        public List<ODpending_list> ODpending_list { get; set; }
        public List<ODrejected_list> ODrejected_list { get; set; }
    }

    public class ODpending_list
    {
        public string employee_name { get; set; }
        public string ondutytracker_gid { get; set; }
        public string onduty_date { get; set; }
        public string onduty_from { get; set; }
        public string onduty_to { get; set; }
        public string onduty_duration { get; set; }
        public string created_date { get; set; }
        public string onduty_reason { get; set; }
        public string ondutytracker_status { get; set; }
        public string apply_employeegid { get; set; }
    }

    public class ODrejected_list
    {
        public string employee_name { get; set; }
        public string ondutytracker_gid { get; set; }
        public string onduty_date { get; set; }
        public string onduty_from { get; set; }
        public string onduty_to { get; set; }
        public string onduty_duration { get; set; }
        public string created_date { get; set; }
        public string onduty_reason { get; set; }
        public string ondutytracker_status { get; set; }
    }

    public class approveOD : result
    {
        public string ondutytracker_gid { get; set; }

    }

    // Permission Approval......//

    public class getpermissiondetails : result
    {
        public List<permissionpending_list> permissionpending_list { get; set; }
        public List<permissionrejected_list> permissionrejected_list { get; set; }
    }

    public class permissionpending_list
    {
        public string employee_name { get; set; }
        public string permission_gid { get; set; }
        public string permissiondtl_gid { get; set; }
        public string permission_date { get; set; }
        public string permission_from { get; set; }
        public string permission_to { get; set; }
        public string permission_duration { get; set; }
        public string permission_reason { get; set; }
        public string permission_status { get; set; }
        public string permission_createddate { get; set; }
    }

    public class permissionrejected_list
    {
        public string employee_name { get; set; }
        public string permission_gid { get; set; }
        public string permission_date { get; set; }
        public string permission_from { get; set; }
        public string permission_to { get; set; }
        public string permission_duration { get; set; }
        public string permission_reason { get; set; }
        public string permission_status { get; set; }
        public string permission_createddate { get; set; }
    }

    // CompOff Approval......//

    public class getcompoffdetails : result
    {
        public List<compoffpending_list> compoffpending_list { get; set; }
        public List<compoffrejected_list> compoffrejected_list { get; set; }
    }

    public class compoffpending_list
    {
        public string employee_name { get; set; }
        public string compensatoryoff_gid { get; set; }
        public string Compoff_from { get; set; }
        public string Compoff_to { get; set; }
        public string Compoff_duration { get; set; }
        public string Compoff_reason { get; set; }
        public string Compoff_status { get; set; }
    }

    public class compoffrejected_list
    {
        public string employee_name { get; set; }
        public string compensatoryoff_gid { get; set; }
        public string Compoff_from { get; set; }
        public string Compoff_to { get; set; }
        public string Compoff_duration { get; set; }
        public string Compoff_reason { get; set; }
        public string Compoff_status { get; set; }
    }

    public class approvecompoff : result
    {
        public string compensatoryoff_gid { get; set; }
    }

    public class monthwise_leavereport : result
    {
        public string response { get; set; }
        //public List<leavereport_list> leavereport_list { get; set; }
    }
    public class leavereport_list
    {
        public string duration { get; set; }
        public string leavetype_name { get; set; }
        public string leavetype_count { get; set; }

    }


    public class monthlyAttendenceReport : result
    {
        public List<monthlyAttendenceReport_list> monthlyAttendenceReport_list { get; set; }

    }

    public class monthlyAttendenceReport_list
    {
        public string attendance_date { get; set; }
        public string Login_time { get; set; }
        public string logout_time { get; set; }
        public string attendance_type { get; set; }
        public string shift_name { get; set; }

    }
    public class getleavedetails : result
    {
        public List<leave_list> leave_list { get; set; }

    }
    public class leave_list
    {
        public string leave_gid { get; set; }
        public string leavetype_gid { get; set; }
        public string leave_applydate { get; set; }
        public string leavetype_name { get; set; }
        public string leave_from { get; set; }
        public string leave_to { get; set; }
        public string noofdays_leave { get; set; }
        public string leave_reason { get; set; }
        public string approval_status { get; set; }
        public string approved_by { get; set; }
        public string applied_by { get; set; }
        public string document_name { get; set; }
    }
    public class leavecountdetails : result
    {

        public List<leavetype_list> leavetype_list { get; set; }
    }

    public class leavetype_list
    {
        public Double count_leavetaken { get; set; }
        public Double count_leaveavailable { get; set; }
        public String leavetype_gid { get; set; }
        public string leavetype_name { get; set; }
        public string lsapply_leave { get; set; }
    }
    public class holidaycalender : result
    {

        
        public List<holidaycalender_list> holidaycalender_list { get; set; }
        public List<employeename_list> employeename_list { get; set; }
    }
    public class employeename_list
    {
        public string Name { get; set; }
        public string UserCode { get; set; }
        public string employeemobileNo { get; set; }


        public string company_logo_path { get; set; }
        public string currentdatetime { get; set; }

        public string Designation { get; set; }
        public string Branch { get; set; }

        public string Department { get; set; }
        public string Joiningdate { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string email { get; set; }


    }



    public class holidaycalender_list
    {

        public string holiday_date { get; set; }
        public string holiday_name { get; set; }
        public string holiday_dayname { get; set; }
    }
    public class eventdetail : result
    {
        public List<createevent> createevent { get; set; }
    }

    public class createevent : result
    {
        public DateTime event_date { get; set; }
        public DateTime event_time { get; set; }
        public TimeSpan time { get; set; }
        public string today_event { get; set; }
        public string event_title { get; set; }
    }
    public class mdlloginsummary : result
    {
        public List<loginsummary_list> loginsummary_list { get; set; }
    }
    public class loginsummary_list : mdlloginsummary
    {
        public string attendancelogintmp_gid { get; set; }
        public string applyDate { get; set; }
        public string attendanceDate { get; set; }
        public string login_Time { get; set; }
        public string login_status { get; set; }
        public string remarks { get; set; }
    }
    public class mdllogoutsummary : result
    {
        public List<logoutsummary_list> logoutsummary_list { get; set; }
    }
    public class logoutsummary_list : mdllogoutsummary
    {
        public string attendancetmp_gid { get; set; }
        public string applyDate { get; set; }
        public string attendanceDate { get; set; }
        public string logout_Time { get; set; }
        public string logout_status { get; set; }
        public string remarks { get; set; }
    }
    public class applyondutydetails : result
    {
        public DateTime od_date { get; set; }
        public string od_fromhr { get; set; }
        public string od_tohr { get; set; }
        public string od_frommin { get; set; }
        public string od_tomin { get; set; }
        public string onduty_period { get; set; }
        public string od_session { get; set; }
        public string total_duration { get; set; }
        public string od_reason { get; set; }
        public bool status { get; set; }
        public string ondutytracker_status { get; set; }
        public string half_day_flag { get; set; }
        public string half_session { get; set; }
        public string onduty_count { get; set; }
    }
    public class onduty_detail_list : result
    {
        public List<onduty_details> onduty_details { get; set; }
    }
    public class onduty_details : result
    {
        public string ondutytracker_gid { get; set; }
        public string onduty_date { get; set; }
        public string onduty_from { get; set; }
        public string onduty_to { get; set; }
        public string onduty_duration { get; set; }
        public string onduty_reason { get; set; }
        public string ondutytracker_status { get; set; }
        public string approved_by { get; set; }
        public string approved_date { get; set; }

    }
    public class permission_details : result
    {

        public DateTime permission_date { get; set; }
        public DateTime permission_applydate { get; set; }
        public string permission_fromhr { get; set; }
        public string permission_tohr { get; set; }
        public string permission_frommin { get; set; }
        public string permission_tomin { get; set; }
        public string permission_total { get; set; }
        public string permission_reason { get; set; }
        public string permission_status { get; set; }
        public string approved_by { get; set; }
        public DateTime approved_date { get; set; }

    }
    public class permission_details_list : result
    {
        public List<permissionSummary_details> permissionSummary_details { get; set; }
    }
    public class permissionSummary_details : result
    {
        public string permission_gid { get; set; }
        public string permissiondtl_gid { get; set; }
        public string permission_date { get; set; }
        public string permission_applydate { get; set; }
        public string permission_from { get; set; }
        public string permission_to { get; set; }
        public string permission_total { get; set; }
        public string permission_reason { get; set; }
        public string permission_status { get; set; }
        public string approved_by { get; set; }
        public string approved_date { get; set; }

    }
    public class compoff_list : result
    {
        public List<compoffSummary_details> compoffSummary_details { get; set; }
    }
    public class compoffSummary_details : result
    {
        public string compensatoryoff_gid { get; set; }
        public string compoff_date { get; set; }
        public string atual_working { get; set; }
        public string compoff_reason { get; set; }
        public string compoff_status { get; set; }

    }
    public class sys_menu
    {
        //public string menu_code { get; set; }
        public string text { get; set; }
        public string sref { get; set; }
        public string icon { get; set; }
        public string label { get; set; }
        public string privilege { get; set; }
        public string menu_indication { get; set; }
        public string menu_indication1 { get; set; }
        public List<sys_submenu> submenu { get; set; }
    }

    public class sys_submenu
    {
        // public string submenu_code { get; set; }
        public string text { get; set; }
        public string sref { get; set; }
        public string menu_indication { get; set; }
        public string menu_indication1 { get; set; }
        //   public string sub_icon { get; set; }
        public List<sys_sub1menu> submenu { get; set; }
    }
    public class sys_sub1menu
    {
        // public string submenu_code { get; set; }
        public string text { get; set; }
        public string sref { get; set; }
        public string icon { get; set; }
        //  public string sub_icon { get; set; }
    }


    public class project_list
    {
        public List<privilege> privileges { get; set; }
    }
    public class privilege
    {
        public string project { get; set; }

    }
    public class projectlist
    {
        public List<privilegelevel3> privilegelevel3 { get; set; }
    }
    public class privilegelevel3
    {
        public string project { get; set; }

    }

    public class companydetails : result
    {
        public string company_name { get; set; }
        public string company_logo { get; set; }
        public string companylogo_responsive { get; set; }
    }

}