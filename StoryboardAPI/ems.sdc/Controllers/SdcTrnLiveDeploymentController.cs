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
    [RoutePrefix("api/SdcTrnLiveDeployment")]
    [Authorize]

    public class SdcTrnLiveDeploymentController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSdcTrnLiveDeployment objDaSdcTrnLive = new DaSdcTrnLiveDeployment();

        [ActionName("GetLiveSummary")]
        [HttpGet]
        public HttpResponseMessage GetUatSummary()
        {
            MdlLiveSummary values = new MdlLiveSummary();
            objDaSdcTrnLive.DaGetLiveSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage PostStatusUpdate(MdlStatusUpdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcTrnLive.DaPostStatusUpdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDeployStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage PostDeployStatusUpdate(MdlStatusUpdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcTrnLive.DaPostDeployStatusUpdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("LiveDeploymentView")]
        [HttpGet]
        public HttpResponseMessage LiveDeploymentView(string live_gid)
        {
            MdlUatView values = new MdlUatView();
            objDaSdcTrnLive.DaGetLiveDeploymentView(live_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}