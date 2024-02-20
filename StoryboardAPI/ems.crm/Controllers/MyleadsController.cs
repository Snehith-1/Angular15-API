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
    [RoutePrefix("api/Myleads")]
    [Authorize]
    public class MyleadsController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMyleads objDaMyleads = new DaMyleads();

        [ActionName("GetMyleadsSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyleadsSummary()
        {
            MdlMyleads values = new MdlMyleads();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyleads.DaGetMyleadsSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetInprogressSummary")]
        [HttpGet]

        public HttpResponseMessage GetInprogressSummary()
        {
            MdlMyleads values = new MdlMyleads();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyleads.DaGetInprogressSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCustomerSummary")]
        [HttpGet]

        public HttpResponseMessage GetCustomerSummary()
        {
            MdlMyleads values = new MdlMyleads();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyleads.DaGetCustomerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDropSummary")]
        [HttpGet]

        public HttpResponseMessage GetDropSummary()
        {
            MdlMyleads values = new MdlMyleads();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyleads.DaGetDropSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAllSummary")]
        [HttpGet]
        public HttpResponseMessage GetAllSummary()
        {
            MdlMyleads values = new MdlMyleads();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyleads.DaGetAllSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetleadbankSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbankSummary(string user_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetleadbankSummary(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetleadbankeditSummary")]
        [HttpGet]
        public HttpResponseMessage GetleadbankeditSummary(string leadbank_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetleadbankeditSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetSourcetypedropdown")]
        [HttpGet]
        public HttpResponseMessage GetSourcetypedropdown(string user_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetSourcetypedropdown(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getregiondropdown")]
        [HttpGet]
        public HttpResponseMessage Getregiondropdown(string user_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetregiondropdown(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getindustrydropdown")]
        [HttpGet]
        public HttpResponseMessage Getindustrydropdown(string user_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetindustrydropdown(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getcompanylistdropdown")]
        [HttpGet]
        public HttpResponseMessage Getcompanylistdropdown(string user_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetcompanylistdropdown(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        
        [ActionName("Getcountrynamedropdown")]
        [HttpGet]
        public HttpResponseMessage Getcountrynamedropdown(string user_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetcountrynamedropdown(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getcountrydropdown")]
        [HttpGet]
        public HttpResponseMessage Getcountrydropdown(string user_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetcountrydropdown(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getcurrencydropdown")]
        [HttpGet]
        public HttpResponseMessage Getcurrencydropdown(string user_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetcurrencydropdown(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Postleadbank")]
        [HttpPost]
        public HttpResponseMessage Postleadbank(leadbank_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyleads.DaPostleadbank(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("Updatedleadbank")]
        [HttpPost]
        public HttpResponseMessage Updatedleadbank(leadbank_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyleads.DaUpdatedleadbank(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        public HttpResponseMessage GetleadbankcontactSummary(string leadbank_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetleadbankcontactSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        public HttpResponseMessage GetleadbankviewSummary(string leadbank_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetleadbankviewSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        public HttpResponseMessage GetleadbankbranchSummary(string leadbank_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetleadbankbranchSummary(leadbank_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Postaddcustomer")]
        [HttpPost]
        public HttpResponseMessage Postaddcustomer(customeradd_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyleads.DaPostaddcustomer(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlMyleads objresult = new MdlMyleads();
            objDaMyleads.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


    }

}