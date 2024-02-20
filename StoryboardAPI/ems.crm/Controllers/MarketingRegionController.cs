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

    [RoutePrefix("api/MarketingRegion")]
    [Authorize]
    public class MarketingRegionController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMarketingRegion objDaMarketing = new DaMarketingRegion();
        // Module Summary
        [ActionName("GetMarketingRegionSummary")]
        [HttpGet]
        public HttpResponseMessage GetMarketingRegionSummary()
        {
            MdlMarketingRegion values = new MdlMarketingRegion();
            objDaMarketing.DaGetMarketingRegionSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Terms and conditions
        [ActionName("PostMarketingregion")]
        [HttpPost]
        public HttpResponseMessage PostMarketingRegion(region_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarketing.DaPostMarketingRegion(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("UpdatedMarketingregion")]
        [HttpPost]
        public HttpResponseMessage UpdatedMarketingRegion(region_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarketing.DaUpdatedMarketingRegion(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("deleteMarketingRegionSummary")]
        [HttpGet]
        public HttpResponseMessage deleteMarketingRegionSummary(string Region_gid)
        {
            region_list objresult = new region_list();
            objDaMarketing.DadeleteMarketingRegionSummary(Region_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlMarketingRegion objresult = new MdlMarketingRegion();
            objDaMarketing.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}