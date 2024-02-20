using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.crm.Controllers
{
    [RoutePrefix("api/MarketingTeam")]
    [Authorize]
    public class MarketingTeamController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMarketingTeam objDaPurchase = new DaMarketingTeam();





        // Module Summary 
        [ActionName("GetMarketingTeamSummary")]
        [HttpGet]
        public HttpResponseMessage GetMarketingTeamSummary()
        {
            MdlMarketingTeam values = new MdlMarketingTeam();
            objDaPurchase.DaGetMarketingTeamSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getbranchdropdown")]
        [HttpGet]
        public HttpResponseMessage Getbranchdropdown()
        {
            MdlMarketingTeam values = new MdlMarketingTeam();
            objDaPurchase.DaGetbranchdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getteammanagerdropdown")]
        [HttpGet]
        public HttpResponseMessage Getteammanagerdropdown()
        {
            MdlMarketingTeam values = new MdlMarketingTeam();
            objDaPurchase.DaGetteammanagerdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Terms and conditions
        [ActionName("Postmarketingteam")]
        [HttpPost]
        public HttpResponseMessage Postmarketingteam(marketingteam_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPostmarketingteam(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        //[ActionName("GeteditCurrencySummary")]
        //[HttpGet]
        //public HttpResponseMessage GeteditCurrencySummary(string termsconditions_gid)
        //{
        //    MdlCurrency objresult = new MdlCurrency();
        //    objDaPurchase.DaGeteditCurrencySummary(termsconditions_gid, objresult);
        //    return Request.CreateResponse(HttpStatusCode.OK, objresult);
        //}
        [ActionName("Updatedmarketingteam")]
        [HttpPost]
        public HttpResponseMessage Updatedmarketingteam(marketingteam_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaUpdatedmarketingteam(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("deleteMarketingTeam")]
        [HttpGet]
        public HttpResponseMessage deleteMarketingTeam(string campaign_gid)
        {
            marketingteam_list objresult = new marketingteam_list();
            objDaPurchase.DadeleteMarketingTeam(campaign_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }



        //[ActionName("deleteMarketingTeam")]
        //[HttpGet]
        //public HttpResponseMessage deleteMarketingTeam(marketingteam_list values)
        //{
        //    marketingteam_list objresult = new marketingteam_list();
        //    objDaPurchase.DadeleteMarketingTeam(getsessionvalues.campaign_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, objresult);
        //}

        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlMarketingTeam objresult = new MdlMarketingTeam();
            objDaPurchase.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}
