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

    [RoutePrefix("api/Myvisit")]
    [Authorize]
    public class MyvisitController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMyvisit objDaMarketing = new DaMyvisit();
        // Module Summary
        [ActionName("GetMyvisitSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyvisitSummary()
        {
            MdlMyvisit values = new MdlMyvisit();
            objDaMarketing.DaGetMyvisitSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTodaysvisitSummary")]
        [HttpGet]
        public HttpResponseMessage GetTodaysvisitSummary()
        {
            MdlMyvisit values = new MdlMyvisit();

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarketing.DaGetTodayvisitSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("GetUpcomingvisitSummary")]
        [HttpGet]
        public HttpResponseMessage GetUpcomingvisitSummary()
        {
            MdlMyvisit values = new MdlMyvisit();

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarketing.DaGetUpcomingvisitSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

            [ActionName("GetProductdropdown")]
            [HttpGet]
            public HttpResponseMessage GetProductdropdown()
            {
                MdlMyvisit values = new MdlMyvisit();
                objDaMarketing.DaGetProductdropdown(values);
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }

        [ActionName("GetPostonlinemeeting")]
        [HttpPost]
        public HttpResponseMessage GetPostonlinemeeting(online_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarketing.DaPostonlinemeeting(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("GetPostofflinemeeting")]
        [HttpPost]
        public HttpResponseMessage GetPostofflinemeeting(offline_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarketing.DaPostofflinemeeting(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("getproductgroupchange")]
        [HttpGet]
        public HttpResponseMessage getproductgroupchange(string product_gid)
        {
            MdlMyvisit values = new MdlMyvisit();
            objDaMarketing.Daproductgroupchange(values,product_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlMyvisit objresult = new MdlMyvisit();
            objDaMarketing.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
    }










