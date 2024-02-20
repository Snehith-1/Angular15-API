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


namespace ems.crm.Controllers
{
    [RoutePrefix("api/Source")]
    [Authorize]

    public class SourceController : ApiController
    {
        
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSource objDaSource = new DaSource();

        [ActionName("PostSource")]
        [HttpPost]
        public HttpResponseMessage PostSource(source_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSource.DaPostSource(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("GetSourceSummary")]
        [HttpGet]
        public HttpResponseMessage GetSourceSummary()
        {
            sourcelist values = new sourcelist();
            objDaSource.DaGetSourceSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getupdatesourcedetails")]
        [HttpPost]
        public HttpResponseMessage Getupdatesourcedetails(source_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSource.DaGetupdatesourcedetails(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("Getdeletesourcedetails")]
        [HttpGet]
        public HttpResponseMessage Getdeletesourcedetails(string source_gid)
        {
            source_list objresult = new source_list();
            objDaSource.DaGetdeletesourcedetails(source_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlSource objresult = new MdlSource();
            objDaSource.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }





    }
}