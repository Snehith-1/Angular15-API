using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.crm.Controllers

{
    [RoutePrefix("api/Employeedashboard")]
    [Authorize]
  public class EmployeeDashboardController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaEmployeeDashboard objDaEmployee = new DaEmployeeDashboard();


        [ActionName("Getsalesordrependingcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getsalesordrependingcountemployee(string user_gid)
        {
            MdlEmployeedashboard objresult = new MdlEmployeedashboard();
            objDaEmployee.DaGetsalesordrependingcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getsalesorderapprovedcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderapprovedcountemployee(string user_gid)
        {
            MdlEmployeedashboard objresult = new MdlEmployeedashboard();
            objDaEmployee.DaGetsalesorderapprovedcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getsalesorderrejectedcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderrejectedcountemployee(string user_gid)
        {
            MdlEmployeedashboard objresult = new MdlEmployeedashboard();
            objDaEmployee.DaGetsalesorderrejectedcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getsalesorderincountemployee")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderincountemployee(string user_gid)
        {
            MdlEmployeedashboard objresult = new MdlEmployeedashboard();
            objDaEmployee.DaGetsalesorderincountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetsalesCurrentorderapprovedcountemployee")]
        [HttpGet]
        public HttpResponseMessage GetsalesCurrentorderapprovedcountemployee(string user_gid)
        {
            MdlEmployeedashboard objresult = new MdlEmployeedashboard();
            objDaEmployee.DaGetsalesCurrentorderapprovedcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getsalesorderammendedcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderammendedcountemployee(string user_gid)
        {
            MdlEmployeedashboard objresult = new MdlEmployeedashboard();
            objDaEmployee.DaGetsalesorderammendedcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }







        [ActionName("GetSalesorderpendingemployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetSalesorderpendingemployeeSummary(string user_gid)
        {
            MdlEmployeedashboard objresult = new MdlEmployeedashboard();
            objDaEmployee.DaGetSalesorderpendingemployeeSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetSalesorderapprovedemployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetSalesorderapprovedemployeeSummary(string user_gid)
        {
            MdlEmployeedashboard objresult = new MdlEmployeedashboard();
            objDaEmployee.DaGetSalesorderapprovedemployeeSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("GetSalesorderinprogressemployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetSalesorderinprogressemployeeSummary(string user_gid)
        {
            MdlEmployeedashboard objresult = new MdlEmployeedashboard();
            objDaEmployee.DaGetSalesorderinprogressemployeeSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetSalesorderRejectedemployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetSalesorderRejectedemployeeSummary(string user_gid)
        {
            MdlEmployeedashboard objresult = new MdlEmployeedashboard();
            objDaEmployee.DaGetSalesorderRejectedemployeeSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetSalesorderammendedemployeeSummary")]
        [HttpGet]
        public HttpResponseMessage GetSalesorderammendedemployeeSummary(string user_gid)
        {
            MdlEmployeedashboard objresult = new MdlEmployeedashboard();
            objDaEmployee.DaGetSalesorderammendedemployeeSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}