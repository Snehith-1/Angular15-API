
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

    [RoutePrefix("api/crmproductunits")]
    [Authorize]
    public class CrmproductunitsController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaCrmproductunits objDaCrmproductunits = new DaCrmproductunits();

        [ActionName("GetcrmproductunitSummary")]
        [HttpGet]
        public HttpResponseMessage GetcrmproductunitSummary()
        {
            MdlCrmproductunits values = new MdlCrmproductunits();
            objDaCrmproductunits.DaGetcrmproductunitSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetcrmproductunitSummarygrid")]
        [HttpGet]
        public HttpResponseMessage GetcrmproductunitSummarygrid(string productuomclass_gid)
        {
            MdlCrmproductunits objresult = new MdlCrmproductunits();
            objDaCrmproductunits.DaGetcrmproductunitSummarygrid(productuomclass_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Postcrmproductunits")]
        [HttpPost]
        public HttpResponseMessage Postcrmproductunits(summaryproductunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCrmproductunits.DaPostcrmproductunits(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("Updatedcrmproductunits")]
        [HttpPost]
        public HttpResponseMessage Updatedcrmproductunits(summaryproductunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCrmproductunits.DaUpdatedcrmproductunits(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("Postcrmgridproductunits")]
        [HttpPost]
        public HttpResponseMessage Postcrmgridproductunits(summaryproductunitgrid_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCrmproductunits.DaPostcrmgridproductunits(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("updatecrmgridproductunits")]
        [HttpPost]
        public HttpResponseMessage updatecrmgridproductunits(summaryproductunitgrid_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCrmproductunits.Daupdatecrmgridproductunits(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("deletecrmproductunitsSummary")]
        [HttpGet]
        public HttpResponseMessage deletecrmproductunitsSummary(string productuomclass_gid)
        {
            summaryproductunitgrid_list objresult = new summaryproductunitgrid_list();
            objDaCrmproductunits.DadeletecrmproductunitsSummary(productuomclass_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("deletecrmproductunitsgridSummary")]
        [HttpGet]
        public HttpResponseMessage deletecrmproductunitsgridSummary(string productuom_gid)
        {
            summaryproductunitgrid_list objresult = new summaryproductunitgrid_list();
            objDaCrmproductunits.DadeletecrmproductunitsgridSummary(productuom_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaCrmproductunits.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }




    }
}