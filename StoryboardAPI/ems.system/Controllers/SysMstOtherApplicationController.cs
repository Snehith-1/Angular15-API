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
    [RoutePrefix("api/OtherApplication")]
    public class OtherApplicationController : ApiController //Controller 
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaOtherApplication objDaOtherApplication = new DaOtherApplication();
        
        //Get Other Application
        [ActionName("GetOtherApplication")]
        [HttpGet]
        public HttpResponseMessage GetOtherApplication()
        {
            MdlOtherApplication objMdlOtherApplication = new MdlOtherApplication();
            objDaOtherApplication.DaGetOtherApplication(objMdlOtherApplication);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlOtherApplication);
        }

        //Create Other Application
        [ActionName("CreateOtherApplication")]
        [HttpPost]
        public HttpResponseMessage CreateOtherApplication(otherapplication values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaOtherApplication.DaCreateOtherApplication(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Other Application
        [ActionName("EditOtherApplication")]
        [HttpGet]
        public HttpResponseMessage EditOtherApplication(string otherapplication_gid)
        {
            otherapplication values = new otherapplication();
            objDaOtherApplication.DaEditOtherApplication(otherapplication_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Other Application
        [ActionName("UpdateOtherApplication")]
        [HttpPost]
        public HttpResponseMessage UpdateOtherApplication(otherapplication values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaOtherApplication.DaUpdateOtherApplication(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Inactivate Other Application
        [ActionName("InactiveOtherApplication")]
        [HttpPost]
        public HttpResponseMessage InactiveOtherApplication(otherapplication values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaOtherApplication.DaInactiveOtherApplication(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //OtherApplication Inactivate log
        [ActionName("InactiveOtherApplicationHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveOtherApplicationHistory(string otherapplication_gid)
        {
            MdlOtherApplication objhistory = new MdlOtherApplication();
            objDaOtherApplication.DaInactiveOtherApplicationHistory(objhistory, otherapplication_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objhistory);
        }

        //Employee list
        [ActionName("Employee")]
        [HttpGet]
        public HttpResponseMessage Employee(string otherapplication_gid)
        {
            MdlEmployeeassign objMdlEmployee = new MdlEmployeeassign();
            objDaOtherApplication.DaGetEmployee(objMdlEmployee, otherapplication_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }

        //Assigned Employee list
        [ActionName("AssignedEmployee")]
        [HttpGet]
        public HttpResponseMessage AssignedEmployee(string otherapplication_gid)
        {
            MdlEmployeeassign objMdlEmployee = new MdlEmployeeassign();
            objDaOtherApplication.DaGetAssingedEmployee(objMdlEmployee, otherapplication_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }

        //Assign Employee
        [ActionName("Assignmember")]
        [HttpPost]
        public HttpResponseMessage Assignmember(Mdlassignmember values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaOtherApplication.DaAssignmember(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Unassign Employee
        [ActionName("GetAssignmemberDelete")]
        [HttpPost]
        public HttpResponseMessage GetAssignmemberDelete(Mdlassignmember values)
        {
            MdlEmployeeassign objMdlEmployee = new MdlEmployeeassign();
            objDaOtherApplication.DaGetAssignmemberDelete(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Assign Links
        [ActionName("Assignedlinks")]
        [HttpGet]
        public HttpResponseMessage Assignedlinks()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            MdlOtherApplication objMdlOtherApplication = new MdlOtherApplication();
            objDaOtherApplication.DaAssignedlinks(objMdlOtherApplication, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlOtherApplication);
        }
    }
}