﻿using ems.system.DataAccess;
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
    [RoutePrefix("api/Linkedin")]
    [Authorize]
    public class LinkedinController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLinkedin objDaLinkedin = new DaLinkedin();
        [ActionName("GetLinkedinUser")]
        [HttpGet]
        public HttpResponseMessage GetLinkedinUser()
        {
            linkedinuser_list values = new linkedinuser_list();
            values = objDaLinkedin.DaGetLinkedinUser();
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLinkedinProfile")]
        [HttpGet]
        public HttpResponseMessage GetLinkedinProfile()
        {
            linkedinprofile_list values = new linkedinprofile_list();
            values = objDaLinkedin.DaGetLinkedinProfile();
            return Request.CreateResponse(HttpStatusCode.OK, values.profilePicture.displayImageObj.elements[0].identifiers[0].identifier);
        }

        [ActionName("Postlinkedin")]
        [HttpPost]
        public HttpResponseMessage Postlinkedin(post_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objDaLinkedin.DaPostlinkedin(getsessionvalues.user_gid, values, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
    }
}