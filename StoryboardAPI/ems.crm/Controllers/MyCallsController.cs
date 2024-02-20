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
using System.Reflection.Emit;

namespace ems.crm.Controllers
{
    [RoutePrefix("api/Mycalls")]
    [Authorize]
    public class MyCallsController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        DaMyCalls objDaCustomer = new DaMyCalls();


        [ActionName("GetMycallsSummary")]
        [HttpGet]
        public HttpResponseMessage GetMycallsSummary()
        {
            MdlMyCalls values = new MdlMyCalls();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaGetMycallsSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetPendingSummary()
        {
            MdlMyCalls values = new MdlMyCalls();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaGetPendingSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetFollowupSummary")]
        [HttpGet]
        public HttpResponseMessage GetFollowupSummary()
        {
            MdlMyCalls values = new MdlMyCalls();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaGetFollowupSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetClosedSummary")]
        [HttpGet]

        public HttpResponseMessage GetClosedSummary()
        {
            MdlMyCalls values = new MdlMyCalls();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaGetClosedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDropSummary")]
        [HttpGet]

        public HttpResponseMessage GetDropSummary()
        {
            MdlMyCalls values = new MdlMyCalls();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaGetDropSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductdropdown")]
        [HttpGet]

        public HttpResponseMessage GetProductdropdown()
        {
            MdlMyCalls values = new MdlMyCalls();
            objDaCustomer.DaGetProductdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetViewLog")]
        [HttpGet]
        public HttpResponseMessage GetViewLog()
        {
            MdlMyCalls values = new MdlMyCalls();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaGetViewLog(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostShowlogsubmit")]
        [HttpPost]
        public HttpResponseMessage PostShowlogsubmit(log_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostShowlogsubmit(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, true);

        }

        [ActionName("Getproductgroupchange")]
        [HttpGet]

        public HttpResponseMessage Getproductgroupchange(string product_gid)
        {
            MdlMyCalls values = new MdlMyCalls();
            objDaCustomer.DaGetproductgroupchange(values, product_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
     
        }

        [ActionName("Postfollowlogsubmit")]
        [HttpPost]
        public HttpResponseMessage Postfollowlogsubmit(flog_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostfollowlogsubmit(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, true);

        }

        [ActionName("Postpendinglogsubmit")]
        [HttpPost]
        public HttpResponseMessage Postpendinglogsubmit(plog_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostpendinglogsubmit(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, true);

        }

        [ActionName("Postschedulesubmit")]
        [HttpPost]
        public HttpResponseMessage Postschedulesubmit(slog_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostschedulesubmit(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, true);

        }

        [ActionName("Getbreadcrumb2")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb2(string user_gid, string module_gid)
        {
            MdlMyCalls objresult = new MdlMyCalls();
            objDaCustomer.DaGetbreadcrumb2(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}