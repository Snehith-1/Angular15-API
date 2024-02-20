using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.system.DataAccess;
using ems.system.Models;

namespace ems.system.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        DaUser objdauser = new DaUser();

        [Route("userdata")]
        [HttpGet]
        public HttpResponseMessage getUserData(string user_gid)
        {
            UserData objresult = new UserData();
            objdauser.userDataFromDB(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // Get User Menu

        [ActionName("menu")]
        [HttpGet]
        public HttpResponseMessage getMenu(string user_gid)
        {
            menu_response objresult = new menu_response();
            objdauser.loadMenuFromDB(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        // Module Summary
        [ActionName("GetEntitySummary")]
        [HttpGet]
        public HttpResponseMessage GetModuleSummary()
        {
            entityllist values = new entityllist();
            objdauser.DaGetEntitySummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLicensemanagementSummary")]
        [HttpGet]
        public HttpResponseMessage GetLicensemanagementSummary()
        {
            licensemanagementlist values = new licensemanagementlist();
            objdauser.DaGetLicensemanagementSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDesignationSummary")]
        [HttpGet]
        public HttpResponseMessage GetDesignationSummary()
        {
            designationlist values = new designationlist();
            objdauser.DaGetDesignationSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetActivateemployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetModuleeeSummary()
        {
            activateemployeelist values = new activateemployeelist();
            objdauser.DaGetActivateemployeeSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetFinancialyearactivitiesSummary")]
        [HttpGet]
        public HttpResponseMessage GetFinancialyearactivitiesSummary()
        {
            financialyearactivitieslist values = new financialyearactivitieslist();
            objdauser.DaGetFinancialyearactivitiesSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUserreportSummary")]
        [HttpGet]
        public HttpResponseMessage GetModuleeeeSummary()
        {
            userreportlist values = new userreportlist();
            objdauser.DaGetUserreportSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmployeereportSummary")]
        [HttpGet]
        public HttpResponseMessage GetModuleeeeeSummary()
        {
            employeereportlist values = new employeereportlist();
            objdauser.DaGetEmployeereportSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBranchSummary")]
        [HttpGet]
        public HttpResponseMessage GetBranchSummary()
        {
            branchlist values = new branchlist();
            objdauser.DaGetBranchSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("privilege")]
        [HttpGet]
        public HttpResponseMessage getprivilege(string user_gid)
        {
            project_list objresult = new project_list();
            objdauser.projectprivilege(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("privilegelevel3")]
        [HttpGet]
        public HttpResponseMessage getprivilegelevel3(string user_gid)
        {
            projectlist objresult = new projectlist();
            objdauser.privilegelevel3(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("companydetails")]
        [HttpGet]
        public HttpResponseMessage getcompanydetails()
        {
            companydetails objcompanydetails = new companydetails();
            objdauser.getcompanydetails(objcompanydetails);
            return Request.CreateResponse(HttpStatusCode.OK, objcompanydetails);
        }



        [ActionName("monthlyAttendence")]
        [HttpGet]
        public HttpResponseMessage GetMonthlyAttendence(string user_gid)
        {
            monthlyAttendence objresult = new monthlyAttendence();
            objdauser.GetMonthlyAttendence(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        //[ActionName("policy")]
        //[HttpGet]

        //public HttpResponseMessage getcompanypolicy()
        //{
        //    company_policy objresult = new company_policy();
        //    objDaCompanyPolicy.companypolicy_da(objresult);
        //    return Request.CreateResponse(HttpStatusCode.OK, objresult);
        //}


        [ActionName("policy")]
        [HttpGet]
        public HttpResponseMessage getcompanypolicy()
        {
            company_policy values = new company_policy();
            objdauser.DaCompanypolicy(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("myteam")]
        [HttpGet]
        public HttpResponseMessage getmyteam(string user_gid)
        {
            myteam objresult = new myteam();
            objdauser.DaGetTeam(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("monthlyAttendenceReport")]
        [HttpGet]
        public HttpResponseMessage GetmonthlyAttendenceReport(string user_gid)
        {
            monthlyAttendenceReport objresult = new monthlyAttendenceReport();
            objdauser.DamonthlyAttendenceReport(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("leavesummary")]
        [HttpGet]
        public HttpResponseMessage applyleavesummary(string user_gid)
        {
            getleavedetails objresult = new getleavedetails();
            objdauser.DaGetApplyLeaveSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("leavetype")]
        [HttpGet]
        public HttpResponseMessage getleavetype(string user_gid)
        {
            leavecountdetails objresult = new leavecountdetails();
            objdauser.DaGetLeaveType(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("holidaycalender")]
        [HttpGet]
        public HttpResponseMessage GetHolidayCalender(string user_gid)
        {
            holidaycalender objresult = new holidaycalender();
            objdauser.DaGetHoliday(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("todayactivity")]
        [HttpGet]
        public HttpResponseMessage GetTodayActivity(string user_gid)
        {
            eventdetail objresult = new eventdetail();
            objdauser.DaGettodayactivity(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("event")]
        [HttpGet]
        public HttpResponseMessage GetEvent(string user_gid)
        {
            eventdetail objresult = new eventdetail();
            objdauser.DaGetEvent(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("loginSummary")]
        [HttpGet]
        public HttpResponseMessage LoginSummary(string user_gid)
        {
            mdlloginsummary objresult = new mdlloginsummary();
            objdauser.DaGetLoginSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("logoutSummary")]
        [HttpGet]
        public HttpResponseMessage LogoutSummary(string user_gid)
        {
            mdllogoutsummary objresult = new mdllogoutsummary();
            objdauser.DaGetLogoutSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("ondutySummary")]
        [HttpGet]
        public HttpResponseMessage GetOnDutySummary(string user_gid)
        {
            onduty_detail_list objresult = new onduty_detail_list();
            objdauser.DaGetOnDutySummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("compOffSummary")]
        [HttpGet]
        public HttpResponseMessage GetCompOffSummary(string user_gid)
        {
            compoff_list objresult = new compoff_list();
            objdauser.DaGetCompOffSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("permissionSummary")]
        [HttpGet]
        public HttpResponseMessage GetPermissionSummary(string user_gid)
        {
            permission_details_list objresult = new permission_details_list();
            objdauser.DaGetPermissionSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("employeename")]
        [HttpGet]
        public HttpResponseMessage GetEmployeename(string user_gid)
        {
            holidaycalender objresult = new holidaycalender();
            objdauser.DaGetEmployeename(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("employeeList")]
        [HttpGet]
        public HttpResponseMessage employeeList(string user_gid)
        {
            holidaycalender objresult = new holidaycalender();
            objdauser.DaGetEmployeeList(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("comapanylogo")]
        [HttpGet]
        public HttpResponseMessage GetComapanylogo(string user_gid)
        {
            myteam objresult = new myteam();
            objdauser.DaGetComapanylogo(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("welcomelogo")]
        [HttpGet]
        public HttpResponseMessage GetWelcomelogo(string user_gid)
        {
            myteam objresult = new myteam();
            objdauser.DaGetWelcomelogo(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // Approval Leave pending Summary....//
        [ActionName("getleaveapprovependingdetails")]
        [HttpGet]
        public HttpResponseMessage getleaveapprovependingdetails(string user_gid)
        {
            getleavedetails objresult = new getleavedetails();
            objdauser.DaGetLeaveApprovePendingDetails(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("getloginsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getloginsummarydetails(string user_gid)
        {
            getlogindetails objresult = new getlogindetails();
            objdauser.DaGetLoginApproval(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("getlogoutsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getlogoutsummarydetails(string user_gid)
        {
            getlogoutdetails objresult = new getlogoutdetails();
            objdauser.DaGetLogoutApproval(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //..............................* 4. OD APPROVAL Details *..................................//

        [ActionName("getODsummarydetails")]
        [HttpGet]
        public HttpResponseMessage GetODSummaryDetails(string user_gid)
        {
            getODdetails objresult = new getODdetails();
            objdauser.DaGetODSummaryDetails(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }



        //..............................* 6. COMPOFF APPROVAL Details *..............................//
        [ActionName("getCompoffsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getCompoffsummarydetails(string user_gid)
        {
            getcompoffdetails objresult = new getcompoffdetails();
            objdauser.DaGetCompoffSummaryDetails(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        //..............................* 5. PERMISSION APPROVAL  Details *..........................//
        [ActionName("getPermissionsummarydetails")]
        [HttpGet]
        public HttpResponseMessage GetPermissionSummaryDetails(string user_gid)
        {
            getpermissiondetails objresult = new getpermissiondetails();
            objdauser.GetPermissionSummaryDetails(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        //[ActionName("Deletedashboardlogin")]
        //[HttpGet]
        //public HttpResponseMessage Deletedashboardlogin(string attendancelogintmp_gid)
        //{
        //    UserData objresult = new UserData();
        //    objdauser.DaDeletedashboardlogin(attendancelogintmp_gid, objresult);
        //    return Request.CreateResponse(HttpStatusCode.OK, objresult);
        //}



        //..............................* 1. Leave APPROved  Details *..........................//
        [ActionName("getleaveapprovedsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getleaveapprovedsummarydetails(string user_gid)
        {
            getleavedetails objresult = new getleavedetails();
            objdauser.DaGetLeaveapproved(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("getloginapprovedsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getloginapprovedsummarydetails(string user_gid)
        {
            getlogindetails objresult = new getlogindetails();
            objdauser.DaGetLoginApproved(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("getlogoutapprovedsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getlogoutapprovedsummarydetails(string user_gid)
        {
            getlogoutdetails objresult = new getlogoutdetails();
            objdauser.DaGetLogoutApproved(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //..............................* 4. OD APPROVED Details *..................................//

        [ActionName("getODapprovedsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getODapprovedsummarydetails(string user_gid)
        {
            getODdetails objresult = new getODdetails();
            objdauser.DaGetODApprovedSummaryDetails(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }



        //..............................* 6. COMPOFF APPROVED Details *..............................//
        [ActionName("getCompoffapprovedsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getCompoffapprovedsummarydetails(string user_gid)
        {
            getcompoffdetails objresult = new getcompoffdetails();
            objdauser.DaGetCompoffapprovedSummaryDetails(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        //..............................* 5. PERMISSION APPROVED  Details *..........................//
        [ActionName("getPermissionrejectedsummarydetails")]
        [HttpGet]
        public HttpResponseMessage GetPermissionrejectedSummaryDetails(string user_gid)
        {
            getpermissiondetails objresult = new getpermissiondetails();
            objdauser.GetPermissionrejectedSummaryDetails(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        //..............................* 1. Leave Rejected  Details *..........................//
        [ActionName("getleaverejectedsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getleaverejectedsummarydetails(string user_gid)
        {
            getleavedetails objresult = new getleavedetails();
            objdauser.DaGetLeaverejected(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("getloginrejectedsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getloginrejectedsummarydetails(string user_gid)
        {
            getlogindetails objresult = new getlogindetails();
            objdauser.DaGetLoginrejected(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("getlogoutrejectedsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getlogoutrejectedsummarydetails(string user_gid)
        {
            getlogoutdetails objresult = new getlogoutdetails();
            objdauser.DaGetLogoutrejected(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //..............................* 4. OD Rejected Details *..................................//

        [ActionName("getODrejectedsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getODrejectedsummarydetails(string user_gid)
        {
            getODdetails objresult = new getODdetails();
            objdauser.DaGetODrejectedSummaryDetails(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }



        //..............................* 6. COMPOFF Rejected Details *..............................//
        [ActionName("getCompoffrejectedsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getCompoffrejectedsummarydetails(string user_gid)
        {
            getcompoffdetails objresult = new getcompoffdetails();
            objdauser.DaGetCompoffrejectedSummaryDetails(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        //..............................* 5. PERMISSION Rejected  Details *..........................//
        [ActionName("getPermissionrejectedsummarydetails")]
        [HttpGet]
        public HttpResponseMessage getPermissionrejectedsummarydetails(string user_gid)
        {
            getpermissiondetails objresult = new getpermissiondetails();
            objdauser.GetPermissionrejectedSummaryDetails(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("deletedashboardlogin")]
        [HttpGet]
        public HttpResponseMessage deletedashboardlogin(string attendancelogintmp_gid)
        {
            createvendor values = new createvendor();
             objdauser.DaDeletedashboardlogin(attendancelogintmp_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBranchgroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetModuleeSummary()
        {
            branchgrouplist values = new branchgrouplist();
            objdauser.DaGetBranchgroupSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //[ActionName("monthlyAttendence")]
        //[HttpGet]
        //public HttpResponseMessage GetMonthlyAttendence()
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    monthlyAttendence objmonthlyAttendence = new monthlyAttendence();
        //    objDaHrmDashboard.DaGetMonthlyAttendence(objmonthlyAttendence,getsessionvalues.employee_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, objmonthlyAttendence);
        //}
    }


}
