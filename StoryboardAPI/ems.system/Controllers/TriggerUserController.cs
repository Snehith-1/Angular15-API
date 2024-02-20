using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.system.Models;
using ems.system.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.system.Controllers
{
    [RoutePrefix("api/TriggerUser")]
    [Authorize]

    public class TriggerUserController : ApiController
    {
        DaTriggerUser objDaTriggerUser = new DaTriggerUser();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

       
            [ActionName("GetTriggerUser")]
            [HttpGet]
            public HttpResponseMessage GetTriggerUser()
            {
                MdlTriggerUser objtriggeruser = new MdlTriggerUser();
            objDaTriggerUser.DaGetTriggerUser(objtriggeruser);
                return Request.CreateResponse(HttpStatusCode.OK, objtriggeruser);
            }

            [ActionName("CreateTriggerUser")]
            [HttpPost]
            public HttpResponseMessage CreateTriggerUser(triggeruser values)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTriggerUser.DaCreateTriggerUser(values, getsessionvalues.employee_gid);
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }

       
        [ActionName("DeleteTriggerUser")]
        [HttpGet]
        public HttpResponseMessage DeleteTriggerUser(string triggeruser_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            triggeruser values = new triggeruser();
            objDaTriggerUser.DaDeleteTriggerUser(triggeruser_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveTrigger")]
        [HttpPost]
        public HttpResponseMessage InactiveTrigger(triggeruser values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTriggerUser.DaInactiveTrigger(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TriggerInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage TriggerInactiveLogview(string triggeruser_gid)
        {
            triggeruser values = new triggeruser();
            objDaTriggerUser.DaTriggerInactiveLogview(triggeruser_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Employee")]
        [HttpGet]
        public HttpResponseMessage getEmployee()
        {
            MdlTriggerUser objMdlTriggerUser = new MdlTriggerUser();
            objDaTriggerUser.DaGetEmployee(objMdlTriggerUser);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlTriggerUser);
        }
    }
    }
