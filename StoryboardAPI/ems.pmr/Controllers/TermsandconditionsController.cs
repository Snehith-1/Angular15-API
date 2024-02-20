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
    [RoutePrefix("api/Termsandconditions")]
    [Authorize]
    public class TermsandconditionsController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaTermsandconditions objDaPurchase = new DaTermsandconditions();
        // Module Summary
        [ActionName("GetTermsandconditionsSummary")]
        [HttpGet]
        public HttpResponseMessage GetTermsandconditionsSummary()
        {
            MdlTermsandconditions values = new MdlTermsandconditions();
            objDaPurchase.DaGetTermsandconditionsSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Terms and conditions
        [ActionName("PostTermsandconditions")]
        [HttpPost]
        public HttpResponseMessage PostEntity(terms_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPostTermsandconditions(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("GeteditTermsandconditionsSummary")]
        [HttpGet]
        public HttpResponseMessage GeteditTermsandconditionsSummary(string termsconditions_gid)
        {
            MdlTermsandconditions objresult = new MdlTermsandconditions();
            objDaPurchase.DaGeteditTermsandconditionsSummary(termsconditions_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("UpdatedTermsandconditions")]
        [HttpPost]
        public HttpResponseMessage UpdatedTermsandconditions(terms_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaUpdatedTermsandconditions(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("deleteTermsandconditionsSummary")]
        [HttpGet]
        public HttpResponseMessage deleteTermsandconditionsSummary(string termsconditions_gid)
        {
            terms_list objresult = new terms_list();
            objDaPurchase.DadeleteTermsandconditionsSummary(termsconditions_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}