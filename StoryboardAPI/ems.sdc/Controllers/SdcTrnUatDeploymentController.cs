using ems.sdc.DataAccess;
using ems.sdc.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.sdc.Controllers
{
    [RoutePrefix("api/SdcTrnUatDeployment")]
    [Authorize]

    public class SdcTrnUatDeploymentController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSdcTrnUatDeployment objDaSdcTrnUat = new DaSdcTrnUatDeployment();

        [ActionName("GetUatSummary")]
        [HttpGet]
        public HttpResponseMessage GetUatSummary()
        {
            MdlUatSummary values = new MdlUatSummary();
            objDaSdcTrnUat.DaGetUatSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage PostStatusUpdate(MdlStatusUpdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcTrnUat.DaPostStatusUpdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDeployStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage PostDeployStatusUpdate(MdlStatusUpdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcTrnUat.DaPostDeployStatusUpdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMovetoLive")]
        [HttpPost]
        public HttpResponseMessage GetMovetoLive(MdlMoveToUAT values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcTrnUat.DaGetMovetoLive(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UatDeploymentView")]
        [HttpGet]
        public HttpResponseMessage UatDeploymentView(string uat_gid)
        {
            MdlUatView values = new MdlUatView();
            objDaSdcTrnUat.DaGetUatDeploymentView(uat_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
