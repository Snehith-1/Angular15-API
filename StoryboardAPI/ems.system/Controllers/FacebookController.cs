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
    [RoutePrefix("api/Facebook")]
    [Authorize]

    public class FacebookController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaFacebook objDaFacebook = new DaFacebook();
        [ActionName("GetFacebook")]
        [HttpGet]
        public HttpResponseMessage GetFacebook()
        {
            facebooklist values = new facebooklist();
            values = objDaFacebook.DaGetFacebook();
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPagedetails")]
        [HttpGet]
        public HttpResponseMessage GetPagedetails()
        {
            facebooklist values = new facebooklist();
            values = objDaFacebook.DaGetPagedetails();
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UploadImage")]
        [HttpPost]
        public HttpResponseMessage UploadImage(string image_caption)
        {
            HttpRequest httpRequest;
            //Postassetlocationcreation values = new Postassetlocationcreation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaFacebook.DaUploadImage(httpRequest, getsessionvalues.user_gid, image_caption, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
      
        [ActionName("UploadVideo")]
        [HttpPost]
        public HttpResponseMessage UploadVideo(string video_caption)
        {
            HttpRequest httpRequest;
            //Postassetlocationcreation values = new Postassetlocationcreation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaFacebook.DaUploadVideo(httpRequest, getsessionvalues.user_gid, video_caption,objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
    }
}