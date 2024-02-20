using ems.pmr.DataAccess;
using ems.pmr.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.pmr.Controllers
{
  
        [RoutePrefix("api/Currency")]
        [Authorize]
        public class CurrencyController : ApiController
        {

            session_values Objgetgid = new session_values();
            logintoken getsessionvalues = new logintoken();
            DaCurrency objDaPurchase = new DaCurrency();
            // Module Summary
            [ActionName("GetCurrencySummary")]
            [HttpGet]
            public HttpResponseMessage GetCurrencySummary()
            {
                MdlCurrency values = new MdlCurrency();
                objDaPurchase.DaGetCurrencySummary(values);
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
        [ActionName("Getcountrydropdown")]
        [HttpGet]
        public HttpResponseMessage Getcountrydropdown()
        {
            MdlCurrency values = new MdlCurrency();
            objDaPurchase.DaGetcountrydropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Terms and conditions
        [ActionName("PostCurrency")]
        [HttpPost]
        public HttpResponseMessage PostEntity(currency_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPostCurrency(getsessionvalues.user_gid, values);
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
        [ActionName("UpdatedCurrency")]
        [HttpPost]
        public HttpResponseMessage UpdatedCurrency(currency_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaUpdatedCurrency(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("deleteCurrencySummary")]
        [HttpGet]
        public HttpResponseMessage deleteCurrencySummary(string currencyexchange_gid)
        {
            currency_list objresult = new currency_list();
            objDaPurchase.DadeleteCurrencySummary(currencyexchange_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlCurrency objresult = new MdlCurrency();
            objDaPurchase.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
    }