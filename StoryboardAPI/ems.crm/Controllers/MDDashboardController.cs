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
    [RoutePrefix("api/MDDashboard")]
    [Authorize]
    public class MDDashboardController:ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMDDashboard objDaPurchase = new DaMDDashboard();
        /////////////////////////Purchase ADMIN DASHBOARD CODE START//////
        [ActionName("Gettotalsalesorderpendingcount")]
        [HttpGet]
        public HttpResponseMessage Gettotalsalesorderpendingcount(string user_gid)
        {
            MdlMDDashboard objresult = new MdlMDDashboard();
            objDaPurchase.DaGetsalesorderpendingcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getsalesorderapprovedcount")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderapprovedcount(string user_gid)
        {
            MdlMDDashboard objresult = new MdlMDDashboard();
            objDaPurchase.DaGetsalesorderapprovedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getsalesorderrejectedcount")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderrejectedcount(string user_gid)
        {
            MdlMDDashboard objresult = new MdlMDDashboard();
            objDaPurchase.DaGetsalesorderrejectedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getsalesorderinprogresscount")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderinprogresscount(string user_gid)
        
        {
            MdlMDDashboard objresult = new MdlMDDashboard();
            objDaPurchase.DaGetsalesorderinprogresscount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getcurrentyearsalesorderrejectedcount")]
        [HttpGet]
        public HttpResponseMessage Getcurrentyearsalesorderrejectedcount(string user_gid)
        {
            MdlMDDashboard objresult = new MdlMDDashboard();
            objDaPurchase.DaGetcurrentyearsalesorderrejectedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getpurchaseorderrejectedcount")]
        [HttpGet]
        public HttpResponseMessage Getpurchaseorderrejectedcount(string user_gid)
        {
            MdlMDDashboard objresult = new MdlMDDashboard();
            objDaPurchase.DaGetpurchaseorderrejectedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetsalesorderSummary")]
        [HttpGet]
        public HttpResponseMessage GetsalesorderSummary(string user_gid)
        {
            MdlMDDashboard objresult = new MdlMDDashboard();
            objDaPurchase.DaGetsalesorderpendingSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getsalesorderbarchart")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderbarchart(string user_gid)
        {
            MdlMDDashboard objresult = new MdlMDDashboard();
            objDaPurchase.DaGetsalesorderbarchart(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getsalesordervalue")]
        [HttpGet]
        public HttpResponseMessage Getsalesordervalue(string user_gid)
        {
            MdlMDDashboard objresult = new MdlMDDashboard();
            objDaPurchase.DaGetsalesordervalue(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }



    }
}