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

    [RoutePrefix("api/Tax")]
    [Authorize]
    public class TaxController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaTax objDaPurchase = new DaTax();
        // Module Summary
        [ActionName("GetTaxSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaxSummary()
        {
            MdlTax values = new MdlTax();
            objDaPurchase.DaGetTaxSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Terms and conditions
        [ActionName("PostTax")]
        [HttpPost]
        public HttpResponseMessage PostTax(tax_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPostTax(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("UpdatedTax")]
        [HttpPost]
        public HttpResponseMessage UpdatedTax(tax_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaUpdatedTax(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("deleteTaxSummary")]
        [HttpGet]
        public HttpResponseMessage deleteTaxSummary(string tax_gid)
        {
            tax_list objresult = new tax_list();
            objDaPurchase.DadeleteTaxSummary(tax_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}