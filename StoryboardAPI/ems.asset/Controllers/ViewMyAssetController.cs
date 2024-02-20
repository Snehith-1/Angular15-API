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
    [RoutePrefix("api/viewMyAsset")]
    [Authorize]
    public class viewMyAssetController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaViewMyAsset objDaViewMyAsset = new DaViewMyAsset();

        // Get Myasset Data
        [ActionName("myasset")]
        [HttpGet]
        public HttpResponseMessage GetMyAsset()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            myassets objmyasset = new myassets();
            objDaViewMyAsset.DaGetMyAsset(getsessionvalues.employee_gid, objmyasset);
            return Request.CreateResponse(HttpStatusCode.OK, objmyasset);
        }

    }
}
