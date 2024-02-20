using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.crm.Controllers
{
    [RoutePrefix("api/AdminDashboard")]
    [Authorize]
    public class Admindashboardcontroller : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAdmindashboard objDasales = new DaAdmindashboard();

        /////////////////////////ADMIN DASHBOARD PANELS CODE START//////
        [ActionName("Gettotalsalesordercount")]
        [HttpGet]
        public HttpResponseMessage Gettotalsalesordercount(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGettotalsalesordercount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("Getsalesorderpendingcount")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderpendingcount(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetsalesorderpendingcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getsalesorderinprogresscount")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderinprogresscount(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetsalesorderinprogresscount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getsalesorderapprovedcount")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderapprovedcount(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetsalesorderapprovedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("Getsalesorderrejectedcount")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderrejectedcount(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetsalesorderrejectedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getsalesorderammendedcount")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderammendedcount(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetsalesorderammendedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("Getsalesordercurrentyearcount")]
        [HttpGet]
        public HttpResponseMessage Getsalesordercurrentyearcount(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetsalesordercurrentyearcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getsalesorderrejectedyearcount")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderrejectedyearcount(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetsalesorderrejectedyearcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //summary//

        [ActionName("GetSalesorderpendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSalesorderpendingSummary(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetSalesorderpendingSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetSalesorderapprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSalesorderapprovedSummary(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetSalesorderapprovedSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetSalesorderinprogressSummary")]
        [HttpGet]
        public HttpResponseMessage GetSalesorderinprogressSummary(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetSalesorderinprogressSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetSalesorderrejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSalesorderrejectedSummary(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetSalesorderrejectedSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetSalesorderammendedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSalesorderammendedSummary(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetSalesorderammendedSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //barchart//

        [ActionName("Getsalesorderadminbarchart")]
        [HttpGet]
        public HttpResponseMessage Getsalesorderadminbarchart(string user_gid)
        {
            MdlAdmindashboard objresult = new MdlAdmindashboard();
            objDasales.DaGetsalesorderadminbarchart(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}