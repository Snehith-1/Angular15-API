using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Web.Http;
using System.Net;
using System.Web;
using ems.crm.DataAccess;
using ems.crm.Models;

namespace ems.crm.Controllers
{
    [RoutePrefix("api/Assignvisit")]
    [Authorize]
    public class AssignvisitController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAssignvisit objDacrm = new DaAssignvisit();
        // Module Summarys
        [ActionName("GetassignvisitSummary")]
        [HttpGet]
        public HttpResponseMessage GetassignvisitSummary()
        {
            MdlAssignvisit values = new MdlAssignvisit();
            objDacrm.DaGetassignvisitSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getmarketingteamdropdown")]
        [HttpGet]
        public HttpResponseMessage Getmarketingteamdropdown()
        {
            MdlAssignvisit values = new MdlAssignvisit();
            objDacrm.DaGetmarketingteamdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getexecutedropdown")]
        [HttpGet]
        public HttpResponseMessage Getexecutedropdown()
        {
            MdlAssignvisit values = new MdlAssignvisit();
            objDacrm.DaGetexecutedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getmarketingteamdropdownonchange")]
        [HttpGet]
        public HttpResponseMessage Getmarketingteamdropdownonchange(string user_gid, string campaign_gid)
        {
            MdlAssignvisit objresult = new MdlAssignvisit();
            objDacrm.DaGetmarketingteamdropdownonchange(user_gid, campaign_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    
        [ActionName("Assignassignvisit")]
        [HttpPost]
        public HttpResponseMessage Assignassignvisit(assignvisitlist values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDacrm.DaAssignassignvisit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
          
        }
        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlAssignvisit objresult = new MdlAssignvisit();
            objDacrm.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}