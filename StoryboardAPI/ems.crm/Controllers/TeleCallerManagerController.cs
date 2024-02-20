using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using static OfficeOpenXml.ExcelErrorValue;
using System.Web.UI.WebControls;

namespace ems.crm.Controllers
{
    [RoutePrefix("api/TeleCallerManager")]
    [Authorize]

    public class TeleCallerManagerController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaTeleCallerManager objDaTeleCallerManager = new DaTeleCallerManager();

        [ActionName("GettelecallerteammanagerSummary"
        )]
        [HttpGet]
        public HttpResponseMessage GettelecallerteammanagerSummary(string user_gid)
        {
            MdlTeleCallerManager objresult = new MdlTeleCallerManager();
            objDaTeleCallerManager.DatelecallerteammanagerSummary(getsessionvalues.employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GettelecallerteammanagerSummary1")]
        [HttpGet]
        public HttpResponseMessage GettelecallerteammanagerSummary1(string campaign_gid)
        {
            MdlTeleCallerManager objresult = new MdlTeleCallerManager();
            objDaTeleCallerManager.DatelecallerteammanagerSummary1(campaign_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}