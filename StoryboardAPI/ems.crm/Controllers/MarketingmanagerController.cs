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
    [RoutePrefix("api/Marketingmanager")]
    [Authorize]

    public class MarketingmanagerController : ApiController
    {
            session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMarketingmanager objDamarketingmanager = new DaMarketingmanager();

        [ActionName("GetMarketingmanagerSummary")]
        [HttpGet]
        public HttpResponseMessage GetMarketingmanagerSummary(string user_gid)
        {
            MdlMarketingmanager objresult = new MdlMarketingmanager();
            objDamarketingmanager.DaGetMarketingmanagerSummary(user_gid,objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetmarketingmanagerSummarygrid")]
        [HttpGet]
        public HttpResponseMessage GetmarketingmanagerSummarygrid(string campaign_gid)
        {
            MdlMarketingmanager objresult = new MdlMarketingmanager();
            objDamarketingmanager.DaGetmarketingmanagerSummarygrid(campaign_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getcampaignmanagersummary")]
        [HttpGet]
        public HttpResponseMessage Getcampaignmanagersummary(string user_gid, string campaign_gid,string employee_gid)
        {
            MdlMarketingmanager objresult = new MdlMarketingmanager();
            objDamarketingmanager.DaGetcampaignmanagersummary(user_gid, employee_gid, campaign_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        //[ActionName("bindelete")]
        //[HttpPost]
        //public HttpResponseMessage bindelete(productattributes_list values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDamarketingmanager.Dabindelete(getsessionvalues.user_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, true);
        //}
        [ActionName("Postschedule")]
        [HttpPost]
        public HttpResponseMessage Postschedule( schedule_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDamarketingmanager.DaPostschedule(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

      
        [ActionName("Gettransferdropdown")]
        [HttpGet]
        public HttpResponseMessage Gettransferdropdown()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            objDamarketingmanager.DaGettransferdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Gettransfernamedropdown")]
        [HttpGet]
        public HttpResponseMessage Gettransfernamedropdown()
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            objDamarketingmanager.DaGettransfernamedropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Gettransferdropdownonchange")]
        [HttpGet]
        public HttpResponseMessage Gettransferdropdownonchange(string campaign_gid)
        {
            MdlMarketingmanager values = new MdlMarketingmanager();
            objDamarketingmanager.DaGettransferdropdownonchange(campaign_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Posttransfer")]
        [HttpPost]
        public HttpResponseMessage Posttransfer(transfer_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDamarketingmanager.DaPosttransfer(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("bindelete")]
        [HttpGet]
        public HttpResponseMessage bindelete(string employee_gid)
        {
            transfer_list objresult = new transfer_list();
            objDamarketingmanager.Dabindelete(employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }

}

   

