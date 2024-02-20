using ems.sdc.DataAccess;
using ems.sdc.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.sdc.Controllers
{
    [RoutePrefix("api/SdcTrnTestDeployment")]
    [Authorize]

    public class SdcTrnTestDeploymentController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSdcTrnTestDeployment objDaSdcTrnTest = new DaSdcTrnTestDeployment();

        [ActionName("PostAddTestDeployment")]
        [HttpPost]
        public HttpResponseMessage PostAddTestDeployment(MdlAddTest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcTrnTest.DaPostAddTestDeployment(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTestSummary")]
        [HttpGet]
        public HttpResponseMessage GetTestSummary()
        {
            MdlTestSummary values = new MdlTestSummary();
            objDaSdcTrnTest.DaGetTestSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage PostStatusUpdate(MdlStatusUpdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcTrnTest.DaPostStatusUpdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDeployStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage PostDeployStatusUpdate(MdlStatusUpdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcTrnTest.DaPostDeployStatusUpdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMovetoUat")]
        [HttpPost]
        public HttpResponseMessage GetMovetoUat(MdlMoveToUAT values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcTrnTest.DaGetMovetoUat(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Uploadjsdocument")]
        [HttpPost]
        public HttpResponseMessage Uploadjsdocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaSdcTrnTest.DaUploadjsdocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("Uploaddocument")]
        [HttpPost]
        public HttpResponseMessage Uploaddocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaSdcTrnTest.DaUploaddocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("Versionuploaddocument")]
        [HttpPost]
        public HttpResponseMessage Versionuploaddocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument versiondocumentname = new uploaddocument();
            objDaSdcTrnTest.DaVersionuploaddocument(httpRequest, versiondocumentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, versiondocumentname);
        }

        [ActionName("TestDelete")]
        [HttpGet]
        public HttpResponseMessage TestDelete(string test_gid)
        {
            result values = new result();
            objDaSdcTrnTest.DaGetTestDelete(test_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TmpDocDelete")]
        [HttpGet]
        public HttpResponseMessage TmpDocDelete()
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSdcTrnTest.DaGetTmpDocDelete(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TestDeploymentView")]
        [HttpGet]
        public HttpResponseMessage TestDeploymentView(string test_gid)
        {
            MdlAddTest values = new MdlAddTest();
            objDaSdcTrnTest.DaGetTestDeploymentView(test_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GettmpJsDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GettmpJsDocumentDelete(string uploaddocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            uploaddocument objvalues = new uploaddocument();
            objDaSdcTrnTest.DaGettmpJsDocumentDelete(uploaddocument_gid, values, objvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GettmpDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GettmpDocumentDelete(string uploaddocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            uploaddocument objvalues = new uploaddocument();
            objDaSdcTrnTest.DaGettmpDocumentDelete(uploaddocument_gid, values, objvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetversiontmpDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GetversiontmpDocumentDelete(string uploaddocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            uploaddocument objvalues = new uploaddocument();
            objDaSdcTrnTest.DaGetversiontmpDocumentDelete(uploaddocument_gid, values, objvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

    }
}
