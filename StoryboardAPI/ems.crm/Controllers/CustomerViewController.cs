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
    [RoutePrefix("api/CustomerView")]
    [Authorize]
    public class CustomerViewController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaCustomerView objDaMarketing = new DaCustomerView();




        [ActionName("Getfrommailiddropdown")]
        [HttpGet]
        public HttpResponseMessage Getfrommailiddropdown()
        {
            MdlCustomerView values = new MdlCustomerView();
            objDaMarketing.DaGetfrommaildropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //[ActionName("mailfunction")]
        //[HttpPost]
        //public HttpResponseMessage mailfunction(string sub, string body, string to)
        //{
        //    HttpRequest httpRequest;
        //    //Postassetlocationcreation values = new Postassetlocationcreation();

        //    httpRequest = HttpContext.Current.Request;
        //    result objResult = new result();
        //    objDaMarketing.Dasendmailtocustomer(httpRequest, sub, body, to);
        //    return Request.CreateResponse(HttpStatusCode.OK, objResult);
        //}


        [ActionName("Postcontactupdate")]
        [HttpPost]
        public HttpResponseMessage Postcontactupdate(cont_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarketing.DaPostcontactupdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("GetEnquirySummary")]
        [HttpGet]
        public HttpResponseMessage GetEnquirySummary()
        {
            MdlCustomerView values = new MdlCustomerView();
            objDaMarketing.DaGetEnquirySummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
