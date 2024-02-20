using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.crm.Controllers
{
    [RoutePrefix("api/leadbank")]
    [Authorize]
    public class LeadbankController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLeadbank objDaleadbank = new DaLeadbank();

        [ActionName("GetleadbankSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbankSummary(string user_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetleadbankSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetleadbankeditSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbankeditSummary(string leadbank_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetleadbankeditSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetSourcetypedropdown")]
        [HttpGet]
        public HttpResponseMessage GetSourcetypedropdown(string user_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetSourcetypedropdown(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getregiondropdown")]
        [HttpGet]
        public HttpResponseMessage Getregiondropdown(string user_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetregiondropdown(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getindustrydropdown")]
        [HttpGet]
        public HttpResponseMessage Getindustrydropdown(string user_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetindustrydropdown(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getcompanylistdropdown")]
        [HttpGet]
        public HttpResponseMessage Getcompanylistdropdown(string user_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetcompanylistdropdown(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getcountrynamedropdown")]
        [HttpGet]
        public HttpResponseMessage Getcountrynamedropdown(string user_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetcountrynamedropdown(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Postleadbank")]
        [HttpPost]
        public HttpResponseMessage Postleadbank(leadbank_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaleadbank.DaPostleadbank(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("Postleadbankbranchadd")]
        [HttpPost]
        public HttpResponseMessage Postleadbankbranchadd(leadbank_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaleadbank.DaPostleadbankbranchadd(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("Updateleadbank")]
        [HttpPost]
        public HttpResponseMessage Updateleadbank(leadbank_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaleadbank.DaUpdateleadbank(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("GetleadbankcontactSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbankcontactSummary(string leadbank_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetleadbankcontactSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetleadbankviewSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbankviewSummary(string leadbank_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetleadbankviewSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetleadbankbranchSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbankbranchSummary(string leadbank_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetleadbankbranchSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetleadbankbranchaddSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbankbranchaddSummary(string leadbank_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetleadbankbranchaddSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetleadbankbrancheditSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbankbrancheditSummary(string leadbankcontact_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetleadbankbrancheditSummary(leadbankcontact_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Updateleadbankbranchedit")]
        [HttpPost]
        public HttpResponseMessage Updateleadbankbranchedit(leadbank_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaleadbank.DaUpdateleadbankbranchedit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("Getbranchdropdown")]
        [HttpGet]
        public HttpResponseMessage Getbranchdropdown(string leadbank_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetbranchdropdown(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getbranchdropdown1")]
        [HttpGet]
        public HttpResponseMessage Getbranchdropdown1(string leadbankcontact_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetbranchdropdown1(leadbankcontact_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("GetleadbankcontactaddSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbankcontactaddSummary(string leadbank_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetleadbankcontactaddSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetleadbankcontacteditsSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbankcontacteditsSummary(string leadbankcontact_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetleadbankcontacteditsSummary(leadbankcontact_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Postleadbankcontactadd")]
        [HttpPost]
        public HttpResponseMessage Postleadbankcontactadd(leadbank_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaleadbank.DaPostleadbankcontactadd(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }


        [ActionName("UpdateleadbankContactedit")]
        [HttpPost]
        public HttpResponseMessage UpdateleadbankContactedit(leadbank_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaleadbank.DaUpdateleadbankContactedit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("GetleadbankcontacteditSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbankcontacteditSummary(string leadbank_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetleadbankcontacteditSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostleadbankApproval")]
        [HttpPost]
        public HttpResponseMessage PostleadbankApproval(leadbank_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaleadbank.DaPostleadbankApproval(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("deleteLeadbankSummary")]
        [HttpGet]
        public HttpResponseMessage deleteLeadbankSummary(string leadbank_gid)
        {
            leadbank_list objresult = new leadbank_list();
            objDaleadbank.DadeleteLeadbankSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlLeadbank objresult = new MdlLeadbank();
            objDaleadbank.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}