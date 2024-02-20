using ems.crm.DataAccess;
using ems.crm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.crm.Controllers
{
    [RoutePrefix("api/CRMProductGroup")]
    [Authorize]
    public class CRMProductGroupController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaCRMProductGroup objDacrm = new DaCRMProductGroup();
        [ActionName("GetProductgroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductgroupSummary()
        {
            MdlCRMProductGroup values = new MdlCRMProductGroup();
            objDacrm.DaGetProductgroupSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Terms and conditions
        [ActionName("PostProductgroup")]
        [HttpPost]
        public HttpResponseMessage PostProductgroup(productgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDacrm.DaPostProductgroup(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("UpdatedProductgroup")]
        [HttpPost]
        public HttpResponseMessage UpdatedProductgroup(productgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDacrm.DaUpdatedProductgroup(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("deleteProductgroupSummary")]
        [HttpGet]
        public HttpResponseMessage deleteProductgroupSummary(string productgroup_gid)
        {
            productgroup_list objresult = new productgroup_list();
            objDacrm.DadeleteProductgroupSummary(productgroup_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlCRMProductGroup objresult = new MdlCRMProductGroup();
            objDacrm.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}

