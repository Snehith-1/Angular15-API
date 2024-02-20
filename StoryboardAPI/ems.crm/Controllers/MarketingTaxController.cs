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

    [RoutePrefix("api/MarketingTax")]
    [Authorize]
    public class MarketingTaxController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMarketingTax objDaMarketing = new DaMarketingTax();
        // Module Summary
        [ActionName("GetMarketingTaxSummary")]
        [HttpGet]
        public HttpResponseMessage GetMarketingTaxSummary()
        {
            MdlMarketingTax values = new MdlMarketingTax();
            objDaMarketing.DaGetMarketingTaxSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Terms and conditions
        [ActionName("PostMarketingTax")]
        [HttpPost]
        public HttpResponseMessage PostMarketingTax(tax_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarketing.DaPostMarketingTax(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("UpdatedMarketingTax")]
        [HttpPost]
        public HttpResponseMessage UpdatedMarketingTax(tax_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarketing.DaUpdatedMarketingTax(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("deleteMarketingTaxSummary")]
        [HttpGet]
        public HttpResponseMessage deleteMarketingTaxSummary(string tax_gid)
        {
            tax_list objresult = new tax_list();
            objDaMarketing.DadeleteMarketingTaxSummary(tax_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("PostTaxsplit")]
        [HttpPost]
        public HttpResponseMessage PostTaxsplit(splittax_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarketing.DaPostTaxsplit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }


        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlMarketingTax objresult = new MdlMarketingTax();
            objDaMarketing.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}