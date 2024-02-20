using ems.pmr.DataAccess;
using ems.pmr.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;


namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PurchaseDashboard")]
    [Authorize]
    public class PurchaseDashboardController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPurchaseDashboard objDaPurchase = new DaPurchaseDashboard();
        /////////////////////////Purchase ADMIN DASHBOARD CODE START//////
        [ActionName("Getpurchaserequisitionpendingcount")]
            [HttpGet]
            public HttpResponseMessage Getpurchaserequisitionpendingcount(string user_gid)
            {
                MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
                objDaPurchase.DaGetpurchaserequisitionpendingcount(user_gid, objresult);
                return Request.CreateResponse(HttpStatusCode.OK, objresult);
            }


        [ActionName("Getpurchaserequisitionapprovedcount")]
        [HttpGet]
        public HttpResponseMessage Getpurchaserequisitionapprovedcount(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaserequisitionapprovedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getpurchaserequisitionrejectedcount")]
        [HttpGet]
        public HttpResponseMessage Getpurchaserequisitionrejectedcount(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaserequisitionrejectedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getpurchaseorderpendingcount")]
        [HttpGet]
        public HttpResponseMessage Getpurchaseorderpendingcount(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderpendingcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getpurchaseorderapprovedcount")]
        [HttpGet]
        public HttpResponseMessage Getpurchaseorderapprovedcount(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderapprovedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getpurchaseorderrejectedcount")]
        [HttpGet]
        public HttpResponseMessage Getpurchaseorderrejectedcount(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderrejectedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getpurchaseorderammendedcount")]
        [HttpGet]
        public HttpResponseMessage Getpurchaseorderammendedcount(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderammendedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("Getgrnapprovedcount")]
        [HttpGet]
        public HttpResponseMessage Getgrnapprovedcount(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrnapprovedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getgrnlinktocompletedcount")]
        [HttpGet]
        public HttpResponseMessage Getgrnlinktocompletedcount(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrnlinktocompletedcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getgrncancelcount")]
        [HttpGet]
        public HttpResponseMessage Getgrncancelcount(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrncancelcount(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetpurchaserequisitionpendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetpurchaserequisitionpendingSummary(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaserequisitionpendingSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetpurchaserequisitionapprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetpurchaserequisitionapprovedSummary(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaserequisitionapprovedSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetpurchaserequisitionrejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetpurchaserequisitionrejectedSummary(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaserequisitionrejectedSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetpurchaseorderpendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetpurchaseorderpendingSummary(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderpendingSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetpurchaseorderapprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetpurchaseorderapprovedSummary(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderapprovedSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetpurchaseorderammendedSummary")]
        [HttpGet]
        public HttpResponseMessage GetpurchaseorderammendedSummary(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderammendedSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetpurchaseorderrejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetpurchaseorderrejectedSummary(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderrejectedSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetgrnlinktocompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetgrnlinktocompletedSummary(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrnlinktocompletedSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetgrnapprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetgrnapprovedSummary(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrnapprovedSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetgrncancelSummary")]
        [HttpGet]
        public HttpResponseMessage GetgrncancelSummary(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrncancelSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getpurchaseorderbarchart")]
        [HttpGet]
        public HttpResponseMessage Getpurchaseorderbarchart(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderbarchart(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getgrnbarchart")]
        [HttpGet]
        public HttpResponseMessage Getgrnbarchart(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrnbarchart(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        /////////////////////////Purchase ADMIN DASHBOARD CODE END//////
        ///     /////////////////////////Purchase EMPLOYEE DASHBOARD CODE  START//////
        [ActionName("Getpurchaserequisitionpendingcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getpurchaserequisitionpendingcountemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaserequisitionpendingcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("Getpurchaserequisitionapprovedcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getpurchaserequisitionapprovedcountemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaserequisitionapprovedcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getpurchaserequisitionrejectedcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getpurchaserequisitionrejectedcountemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaserequisitionrejectedcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getpurchaseorderpendingcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getpurchaseorderpendingcountemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderpendingcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getpurchaseorderapprovedcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getpurchaseorderapprovedcountemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderapprovedcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getpurchaseorderrejectedcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getpurchaseorderrejectedcountemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderrejectedcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getpurchaseorderammendedcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getpurchaseorderammendedcountemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderammendedcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getgrnapprovedcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getgrnapprovedcountemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrnapprovedcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getgrnlinktocompletedcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getgrnlinktocompletedcountemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrnlinktocompletedcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getgrncancelcountemployee")]
        [HttpGet]
        public HttpResponseMessage Getgrncancelcountemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrncancelcountemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetpurchaserequisitionpendingSummaryemployee")]
        [HttpGet]
        public HttpResponseMessage GetpurchaserequisitionpendingSummaryemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaserequisitionpendingSummaryemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetpurchaserequisitionapprovedSummaryemployee")]
        [HttpGet]
        public HttpResponseMessage GetpurchaserequisitionapprovedSummaryemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaserequisitionapprovedSummaryemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetpurchaserequisitionrejectedSummaryemployee")]
        [HttpGet]
        public HttpResponseMessage GetpurchaserequisitionrejectedSummaryemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaserequisitionrejectedSummaryemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetpurchaseorderpendingSummaryemployee")]
        [HttpGet]
        public HttpResponseMessage GetpurchaseorderpendingSummaryemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderpendingSummaryemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetpurchaseorderapprovedSummaryemployee")]
        [HttpGet]
        public HttpResponseMessage GetpurchaseorderapprovedSummaryemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderapprovedSummaryemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetpurchaseorderammendedSummaryemployee")]
        [HttpGet]
        public HttpResponseMessage GetpurchaseorderammendedSummaryemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderammendedSummaryemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetpurchaseorderrejectedSummaryemployee")]
        [HttpGet]
        public HttpResponseMessage GetpurchaseorderrejectedSummaryemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetpurchaseorderrejectedSummaryemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetgrnlinktocompletedSummaryemployee")]
        [HttpGet]
        public HttpResponseMessage GetgrnlinktocompletedSummaryemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrnlinktocompletedSummaryemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetgrnapprovedSummaryemployee")]
        [HttpGet]
        public HttpResponseMessage GetgrnapprovedSummaryemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrnapprovedSummaryemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetgrncancelSummaryemployee")]
        [HttpGet]
        public HttpResponseMessage GetgrncancelSummaryemployee(string user_gid)
        {
            MdlPurchaseDashboard objresult = new MdlPurchaseDashboard();
            objDaPurchase.DaGetgrncancelSummaryemployee(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}