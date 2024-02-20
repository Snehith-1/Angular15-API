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


namespace StoryboardAPI.Controllers.ems.asset
{
    [Authorize]
    [RoutePrefix("api/landingPage")]
    public class landingPageController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        // Get Landing Page Data

        [ActionName("landingpagedata")]
        [HttpGet]
        public HttpResponseMessage GetLandingPage()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            landingpagemodel objlandingpagemodel = new landingpagemodel();
            DaLandingPage objDaLandingPage = new DaLandingPage();
            objDaLandingPage.DaGetLandingPage(objlandingpagemodel, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlandingpagemodel);
        }
        
    }
}
