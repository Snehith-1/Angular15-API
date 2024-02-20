using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.system.DataAccess;
using ems.system.Models;
namespace ems.system.Controllers
{
    [Authorize]
    [RoutePrefix("api/ManageEmployee")]
    public class ManageEmployeeController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaManageEmployee objDaManageEmployee = new DaManageEmployee();

        [ActionName("EmployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeSummary()
        {
            employee_list objemployee_list = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeSummary(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }
        // Active Users
        [ActionName("EmployeeActiveSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeActiveSummary()
        {
            employee_list objemployee_list = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeActiveSummary(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        // Pending Users
        [ActionName("EmployeePendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeePendingSummary()
        {
            employee_list objemployee_list = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeePendingSummary(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        // Relieving Users
        [ActionName("EmployeeRelievedSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeRelievedSummary()
        {
            employee_list objemployee_list = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeRelievedSummary(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        // Inactive Users
        [ActionName("EmployeeInactiveSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeInactiveSummary()
        {
            employee_list objemployee_list = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeInactiveSummary(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }

        [ActionName("EmployeeAdd")]
        [HttpPost]
        public HttpResponseMessage EmployeeAdd(employee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeAdd(values, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EmployeeRelievingAdd")]
        [HttpPost]
        public HttpResponseMessage EmployeeRelievingAdd(employee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeRelievingAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EmployeeRelievingView")]
        [HttpGet]
        public HttpResponseMessage EmployeeRelievingView(string employee_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            employee values = new employee();
            objDaManageEmployee.DaEmployeeRelievingView(values,employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("EmployeeRelievingEdit")]
        [HttpPost]
        public HttpResponseMessage EmployeeRelievingEdit(employee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeRelievingEdit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Pending Approval
        [ActionName("EmployeePendingApproval")]
        [HttpGet]
        public HttpResponseMessage GetEmployeePendingApproval(string employee_gid)
        {
            employee_list objemployee = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeePendingApproval(objemployee, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }

        

        [ActionName("EmployeeEditView")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeEditView( string employee_gid)
        {
            employee_list objemployee = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeEditView(objemployee, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }

        //User code verification

        [ActionName("UserCodeCheck")]
        [HttpGet]
        public HttpResponseMessage GetUserCodeCheck(string user_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaUserCodeCheck(values,user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Pending View
        [ActionName("EmployeePendingEditView")]
        [HttpGet]
        public HttpResponseMessage GetEmployeePendingEditView(string employee_gid)
        {
            employee_list objemployee = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeePendingEditView(objemployee, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }


        [ActionName("PopRole")]
        [HttpGet]
        public HttpResponseMessage GetPopRole()
        {
            role_list objrolemaster = new role_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopRole(objrolemaster);
            return Request.CreateResponse(HttpStatusCode.OK, objrolemaster);
        }
        [ActionName("PopReportingTo")]
        [HttpGet]
        public HttpResponseMessage GetPopReportingTo()
        {
            reportingto_list objreportingto = new reportingto_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopReportingTo(objreportingto);
            return Request.CreateResponse(HttpStatusCode.OK, objreportingto);
        }
        [ActionName("EmployeeDeactivate")]
        [HttpGet]
        public HttpResponseMessage PostEmployeeDeactivate(string employee_gid, string exit_date)
        {
            employee objemployee = new employee();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeDeactivate(employee_gid, getsessionvalues.employee_gid, exit_date, objemployee);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }
        [ActionName("EmployeeActivate")]
        [HttpGet]
        public HttpResponseMessage PostEmployeeActivate(string employee_gid)
        {
            employee objemployee = new employee();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeActivate(employee_gid, getsessionvalues.employee_gid, objemployee);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }
        [ActionName("EmployeeUpdate")]
        [HttpPost]
        public HttpResponseMessage PostEmployeeUpdate(employee objemployee)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeUpdate(objemployee, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }

        [ActionName("EmployeeProfileUpdate")]
        [HttpPost]
        public HttpResponseMessage EmployeeProfileUpdate(employee objemployee)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeeProfileUpdate(objemployee, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }

        //Pending Update
        [ActionName("EmployeePendingUpdate")]
        [HttpPost]
        public HttpResponseMessage PostEmployeePendingUpdate(employee objemployee)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEmployeePendingUpdate(objemployee, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }

        [ActionName("PopCountry")]
        [HttpGet]
        public HttpResponseMessage GetPopCountry()
        {
            country_list objcountry_list = new country_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopCountry(objcountry_list);
            return Request.CreateResponse(HttpStatusCode.OK, objcountry_list);
        }
        [ActionName("PopBranch")]
        [HttpGet]
        public HttpResponseMessage GetPopBranch()
        {
            employee_list objemployee_list = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopBranch(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }
        [ActionName("PopDepartment")]
        [HttpGet]
        public HttpResponseMessage GetPopDepartment()
        {
            employee_list objemployee_list = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopDepartment(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }
        [ActionName("PopDesignation")]
        [HttpGet]
        public HttpResponseMessage GetPopDesignation()
        {
            employee_list objemployee_list = new employee_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopDesignation(objemployee_list);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_list);
        }
        [ActionName("PopRoleEdit")]
        [HttpGet]
        public HttpResponseMessage GetPopRoleEdit(string employee_gid)
        {
            role_list objrolemaster = new role_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopRoleEdit(objrolemaster,employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objrolemaster);
        }
        [ActionName("PopReportingToEdit")]
        [HttpGet]
        public HttpResponseMessage GetPopReportingToEdit(string employee_gid)
        {
            reportingto_list objreportingto = new reportingto_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPopReportingToEdit(objreportingto,employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objreportingto);
        }
        [ActionName("EntityName")]
        [HttpGet]
        public HttpResponseMessage GetEntityName()
        {
            employee objemployee = new employee();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaEntity(objemployee);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }
        //[ActionName("PopEntity")]
        //[HttpGet]
        //public HttpResponseMessage GetPopEntity()
        //{
        //    entity_list objentity_list = new entity_list();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = objgetgid.gettokenvalues(token);
        //    objDaManageEmployee.DaPopEntity(objentity_list);
        //    return Request.CreateResponse(HttpStatusCode.OK, objentity_list);
        //}

        [ActionName("ResetPswdEdit")]
        [HttpGet]
        public HttpResponseMessage ResetPswdEdit(string employee_gid)
        {
            employee values = new employee();
            var status = objDaManageEmployee.DaGetResetPswdEdit(values, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PasswordUpdate")]
        [HttpPost]
        public HttpResponseMessage PasswordUpdate(employee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPostPasswordUpdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UserCodeUpdate")]
        [HttpPost]
        public HttpResponseMessage UserCodeUpdate(employee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPostUserCodeUpdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EmployeeExport")]
        [HttpGet]
        public HttpResponseMessage EmployeeExport()
        {
            export values = new export();
            objDaManageEmployee.DaEmployeeExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Onboarding:
        [ActionName("PostTask")]
        [HttpPost]
        public HttpResponseMessage PostTask(tasklist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPostTask(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskList")]
        [HttpGet]
        public HttpResponseMessage GetTaskList(string employee_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetTaskList(employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTempTaskList")]
        [HttpGet]
        public HttpResponseMessage GetTempTaskList(string employee_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetTempTaskList(employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaskSummary(string employee_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetTaskSummary(employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteTaskInitiated")]
        [HttpGet]
        public HttpResponseMessage DeleteTaskInitiated(string taskinitiate_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaDeleteTaskInitiated(taskinitiate_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostOverallTask")]
        [HttpPost]
        public HttpResponseMessage PostOverallTask(tasklist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaPostOverallTask(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InitiateTask")]
        [HttpPost]
        public HttpResponseMessage InitiateTask(tasklist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaInitiateTask(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmployeeSubmit")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeSubmit(string employee_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetEmployeeSubmit(employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteTaskInitiatedUnsaved")]
        [HttpGet]
        public HttpResponseMessage DeleteTaskInitiatedUnsaved()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaDeleteTaskInitiatedUnsaved(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskInitiate")]
        [HttpGet]
        public HttpResponseMessage GetTaskInitiate(string taskinitiate_gid, string employee_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            tasksummarylist values = new tasksummarylist();
            objDaManageEmployee.DaGetTaskInitiate(taskinitiate_gid, employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CancelTaskInitiate")]
        [HttpGet]
        public HttpResponseMessage CancelTaskInitiate(string taskinitiate_gid, string employee_gid)
        {
            tasklist values = new tasklist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaCancelTaskInitiate(taskinitiate_gid, values, employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyTaskPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyTaskPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetMyTaskPendingSummary(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("CompleteTask")]
        [HttpPost]
        public HttpResponseMessage CompleteTask(tasklist values)
        {
            //tasklist values = new tasklist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaCompleteTask(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCompleteflag")]
        [HttpGet]
        public HttpResponseMessage GetCompleteflag(string taskinitiate_gid, string employee_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetCompleteflag(taskinitiate_gid, employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyTashCount")]
        [HttpGet]
        public HttpResponseMessage GetMyTashCount()
        {
            countlist values = new countlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaManageEmployee.DaGetMyTashCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyTaskCompleteSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyTaskCompleteSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlTaskList values = new MdlTaskList();
            objDaManageEmployee.DaGetMyTaskCompleteSummary(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTaskDetails")]
        [HttpGet]
        public HttpResponseMessage GetTaskDetails(string taskinitiate_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            tasksummarylist values = new tasksummarylist();
            objDaManageEmployee.DaGetTaskDetails(taskinitiate_gid, getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
