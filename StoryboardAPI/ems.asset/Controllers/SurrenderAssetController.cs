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

    [RoutePrefix("api/surrenderAsset")]
    [Authorize]
   
    public class surrenderAssetController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSurrenderAsset objDaSurrenderAsset = new DaSurrenderAsset();

        // Get Surrender Data
        [ActionName("surrender")]
        [HttpGet]
        public HttpResponseMessage PostGetSurrender()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            surrenderassets objsurrenderassets = new surrenderassets();
            objDaSurrenderAsset.DaPostGetSurrender(getsessionvalues.employee_gid, objsurrenderassets);
            return Request.CreateResponse(HttpStatusCode.OK, objsurrenderassets);
        }
        [ActionName("submitsurrender")]
        [HttpPost]
        public HttpResponseMessage PostUpdateSurrender(surrenderstatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSurrenderAsset.DaPostUpdateSurrender(values.asset2custodian_gid, values, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
