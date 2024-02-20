using ems.system.DataAccess;
using ems.system.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace ems.system.Controllers
{
    [RoutePrefix("api/Instagram")]
    [Authorize]


    public class InstagramController: ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaInstagram objDaInstagram = new DaInstagram();

        [ActionName("GetInstagram")]
        [HttpGet]

        public HttpResponseMessage GetInstagram()
        
        
        
        
        {
            instagramlist values = new instagramlist();
            values = objDaInstagram.DaGetInstagram();
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInstagramProfile")]
        [HttpGet]
        public HttpResponseMessage GetInstagramProfile()
        {
            instagramprofile1_list values = new instagramprofile1_list();
            values = objDaInstagram.DaGetInstagramProfile();
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}