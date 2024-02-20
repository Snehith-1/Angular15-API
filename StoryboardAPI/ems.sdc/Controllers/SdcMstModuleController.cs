using ems.sdc.DataAccess;
using ems.sdc.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.sdc.Controllers
{
    [RoutePrefix("api/SdcMstModule")]
    [Authorize]

    public class SdcMstModuleController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSdcMstModule objDaSdcMstModule = new DaSdcMstModule();

        // Modulel Add
        [ActionName("PostModuleAdd")]
        [HttpPost]
        public HttpResponseMessage PostModuleAdd(moduleadd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcMstModule.DaPostModuleAdd(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Module Summary
        [ActionName("GetModuleSummary")]
        [HttpGet]
        public HttpResponseMessage GetModuleSummary()
        {
            moduledtllist values = new moduledtllist();
            objDaSdcMstModule.DaGetModuleSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetModuleView")]
        [HttpGet]
        public HttpResponseMessage GetModuleView(string module_gid)
        {
            moduleadd values = new moduleadd();
            objDaSdcMstModule.DaGetModuleView(module_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostModuleUpdate")]
        [HttpPost]
        public HttpResponseMessage PostModuleUpdate(moduleadd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcMstModule.DaPostModuleUpdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Module Delete
        [ActionName("GetModuleDelete")]
        [HttpGet]
        public HttpResponseMessage GetModuleDelete(string module_gid)
        {
            result values = new result();
            objDaSdcMstModule.DaGetModuleDelete(module_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Module Delete
        [ActionName("GetCustomersList")]
        [HttpGet]
        public HttpResponseMessage GetCustomersList(string module_gid)
        {
            customer values = new customer();
            objDaSdcMstModule.DaGetCustomersList(values, module_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCustomerAssign")]
        [HttpPost]
        public HttpResponseMessage PostCustomerAssign(MdlcustomerAssign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcMstModule.DaPostCustomerAssign(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Module2Customer
        [ActionName("GetModule2Customer")]
        [HttpGet]
        public HttpResponseMessage GetModule2Customer(string module_gid)
        {
            moduledtllist values = new moduledtllist();
            objDaSdcMstModule.DaGetModule2Customer(values, module_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
