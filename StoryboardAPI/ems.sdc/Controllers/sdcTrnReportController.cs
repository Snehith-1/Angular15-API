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
    [RoutePrefix("api/SdcTrnReport")]
    [Authorize]
    public class sdcTrnReportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSdcTrnReport objDaSdcTrnReport = new DaSdcTrnReport();

        [ActionName("GetTestSummaryReport")]
        [HttpGet]
        public HttpResponseMessage GetTestSummaryReport()
        {
            MdlTestSummary values = new MdlTestSummary();
            objDaSdcTrnReport.DaGetTestSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTestReportExport")]
        [HttpGet]
        public HttpResponseMessage GetTestReportExport()
        {
            MdlTestSummary values = new MdlTestSummary();
            objDaSdcTrnReport.DaGetTestReportExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUatSummaryReport")]
        [HttpGet]
        public HttpResponseMessage GetUatSummary()
        {
            MdlUatSummary values = new MdlUatSummary();
            objDaSdcTrnReport.DaGetUatSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUatReportExport")]
        [HttpGet]
        public HttpResponseMessage GetUatReportExport()
        {
            MdlUatSummary values = new MdlUatSummary();
            objDaSdcTrnReport.DaGetUatReportExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLiveSummaryReport")]
        [HttpGet]
        public HttpResponseMessage GetLiveSummaryReport()
        {
            MdlLiveSummary values = new MdlLiveSummary();
            objDaSdcTrnReport.DaGetLiveSummaryReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLiveReportExport")]
        [HttpGet]
        public HttpResponseMessage GetLiveReportExport()
        {
            MdlLiveSummary values = new MdlLiveSummary();
            objDaSdcTrnReport.DaGetLiveReportExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
