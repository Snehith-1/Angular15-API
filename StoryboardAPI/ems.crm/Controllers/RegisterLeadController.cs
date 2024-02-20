using ems.crm.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ems.crm.Models;

namespace ems.crm.Controllers
{
    [RoutePrefix("api/registerlead")]
    [Authorize]
    public class RegisterLeadController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaRegisterLead objDaRegisterLead = new DaRegisterLead();

        // Module Summary
        [ActionName("GetRegisterLeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetRegisterLeadSummary()
        {
            MdlRegisterLead values = new MdlRegisterLead();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRegisterLead.DaGetRegisterLeadSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Post  Terms and conditions
        [ActionName("Postregisterlead")]
        [HttpPost]
        public HttpResponseMessage Postregisterlead(Registerlead_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRegisterLead.DaPostregisterlead(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("postbranchlead")]
        [HttpPost]
        public HttpResponseMessage postbranchlead(leadaddbranch_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRegisterLead.Dapostbranchlead(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("Getbranchdropdown")]
        [HttpGet]
        public HttpResponseMessage Getbranchdropdown(string leadbank_gid)
        {
            MdlRegisterLead objresult = new MdlRegisterLead();
            objDaRegisterLead.DaGetbranchdropdown(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("postcontactlead")]
        [HttpPost]
        public HttpResponseMessage postcontactlead(Registerlead_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRegisterLead.Dapostcontactlead(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }


        [ActionName("Getcountrynamedropdown")]
        [HttpGet]
        public HttpResponseMessage Getcountrynamedropdown()
        {
            MdlRegisterLead values = new MdlRegisterLead();
            objDaRegisterLead.DaGetcountrynamedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getregiondropdown")]
        [HttpGet]
        public HttpResponseMessage Getregiondropdown()
        {
            MdlRegisterLead values = new MdlRegisterLead();
            objDaRegisterLead.DaGetregiondropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getindustrydropdown")]
        [HttpGet]
        public HttpResponseMessage Getindustrydropdown()
        {
            MdlRegisterLead values = new MdlRegisterLead();
            objDaRegisterLead.DaGetindustrydropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSourcetypedropdown")]
        [HttpGet]
        public HttpResponseMessage GetSourcetypedropdown()
        {
            MdlRegisterLead values = new MdlRegisterLead();
            objDaRegisterLead.DaGetSourcetypedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcompanylistdropdown")]
        [HttpGet]
        public HttpResponseMessage Getcompanylistdropdown()
        {
            MdlRegisterLead values = new MdlRegisterLead();
            objDaRegisterLead.DaGetcompanylistdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deleteregisterleadSummary")]
        [HttpGet]
        public HttpResponseMessage deleteregisterleadSummary(string leadbank_gid)
        {
            GetRegisterLeadSummary_list objresult = new GetRegisterLeadSummary_list();
            objDaRegisterLead.DadeleteregisterleadSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GeteditleadSummary")]
        [HttpGet]
        public HttpResponseMessage GeteditleadSummary(string leadbank_gid)
        {
            MdlRegisterLead objresult = new MdlRegisterLead();
            objDaRegisterLead.DaGeteditleadSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetregisterleadviewSummary")]
        [HttpGet]
        public HttpResponseMessage GetregisterleadviewSummary(string leadbank_gid)
        {
            MdlRegisterLead objresult = new MdlRegisterLead();
            objDaRegisterLead.DaGetregisterleadviewSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetregistercontactSummary")]
        [HttpGet]
        public HttpResponseMessage GetregistercontactSummary(string leadbank_gid)
        {
            MdlRegisterLead objresult = new MdlRegisterLead();
            objDaRegisterLead.DaGetregistercontactSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetregisterleadbranchSummary")]
        [HttpGet]
        public HttpResponseMessage GetregisterleadbranchSummary(string leadbank_gid)
        {
            MdlRegisterLead objresult = new MdlRegisterLead();
            objDaRegisterLead.DaGetregisterleadbranchSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetleadbranchaddSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbranchaddSummary(string leadbank_gid)
        {
            MdlRegisterLead objresult = new MdlRegisterLead();
            objDaRegisterLead.DaGetleadbranchaddSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("GetleadcontactaddSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadcontactaddSummary(string leadbank_gid)
        {
            MdlRegisterLead objresult = new MdlRegisterLead();
            objDaRegisterLead.DaGetleadcontactaddSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }



        [ActionName("Updatedregisterlead")]
        [HttpPost]
        public HttpResponseMessage Updatedregisterlead(Registerlead_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRegisterLead.DaUpdatedregisterlead(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("GetleadbrancheditSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbrancheditSummary(string leadbank_gid)
        {
            MdlRegisterLead objresult = new MdlRegisterLead();
            objDaRegisterLead.DaGetleadbankbrancheditSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetleadcontacteditSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadcontacteditSummary(string leadbank_gid)
        {
            MdlRegisterLead objresult = new MdlRegisterLead();
            objDaRegisterLead.DaGetleadcontacteditSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Updateleadbranchedit")]
        [HttpPost]
        public HttpResponseMessage Updateleadbranchedit(leadaddbranch_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRegisterLead.DaUpdateleadbranchedit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("UpdateleadContactedit")]
        [HttpPost]
        public HttpResponseMessage UpdateleadContactedit(Registerlead_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRegisterLead.DaUpdateleadContactedit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlRegisterLead objresult = new MdlRegisterLead();
            objDaRegisterLead.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}
