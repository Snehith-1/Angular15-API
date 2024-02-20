using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.asset.DataAccess;
using ems.asset.Models;
using ems.utilities.Functions;
using ems.utilities.Models;


namespace StoryboardAPI.Controllers.asset
{
    [RoutePrefix("api/temporaryHandover")]
    [Authorize]
    public class temporaryHandoverController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaTemporaryHandover objDaTemporaryHandover = new DaTemporaryHandover();

        // Get Temporary Handover Data

        [ActionName("tmphandover")]
        [HttpGet]
        public HttpResponseMessage GetTempHandover()
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            tmphandoverassets objtmphandoverassets = new tmphandoverassets();
            objDaTemporaryHandover.DaGetTempHandover(getsessionvalues.employee_gid, objtmphandoverassets);
            return Request.CreateResponse(HttpStatusCode.OK, objtmphandoverassets);
        }
        //update Temporary Handover Status
        [ActionName("submittmphandover")]
        [HttpPost]
        public HttpResponseMessage PostTmpSurrenderHandover(tmphandoverstatus values)
        {
            objDaTemporaryHandover.DaPostTmpSurrenderHandover(values.asset_id, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        ////update Holding Asset.....//

        [ActionName("holdingasset")]
        [HttpPost]
        public HttpResponseMessage PostHoldingAsset(holdingasset values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTemporaryHandover.DaPostHoldingAsset(values.asset_id, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //update Surrender to IT Admin Status.....//

        [ActionName("surrenderitadmin")]
        [HttpPost]
        public HttpResponseMessage PostSurrenderITAdmin(surrenderitadmin values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTemporaryHandover.DaPostSurrenderITAdmin(values.asset_id, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
    }
}
