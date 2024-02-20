using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.asset.Models;
using ems.asset.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
namespace StoryboardAPI.Controllers.asset
{
    [RoutePrefix("api/acknowledgeMyAsset")]
    [Authorize]
    public class acknowledgeMyAssetController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAcknowledgeAsset objDaAcknowledgeAsset = new DaAcknowledgeAsset();

        // Get Acknowledgement Pending Data
        [ActionName("acknowledgement")]
        [HttpGet]
        public HttpResponseMessage GetAcknowledgement()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            acknowledgementasset objacknowledgementasset = new acknowledgementasset();
            objDaAcknowledgeAsset.DaGetAcknowledgement(getsessionvalues.employee_gid, objacknowledgementasset);
            return Request.CreateResponse(HttpStatusCode.OK, objacknowledgementasset);
        }

        //Update Acknowledgement Status
        [ActionName("submitacknowledgement")]
        [HttpPost]
        public HttpResponseMessage PostAcknowledgementStatus(ackstatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAcknowledgeAsset.DaPostAcknowledgementStatus(values.asset2custodian_gid, values,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Reject Acknowledgement Status
        [ActionName("acknowledgementreject")]
        [HttpPost]
        public HttpResponseMessage PostAcknowledgementReject(ackrejectstatus objvalues)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAcknowledgeAsset.DaPostAcknowledgementReject(objvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
    }
}
