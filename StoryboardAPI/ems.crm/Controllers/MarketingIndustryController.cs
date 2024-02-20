using ems.crm.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Net.Http;
using System.Web.Http;
using ems.crm.Models;
using System.Web.UI.WebControls;


namespace ems.crm.Controllers
{
    [RoutePrefix("api/Industry")]
    [Authorize]

    public class IndustryController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMarketingIndustry objDaCustomer = new DaMarketingIndustry();



        [ActionName("PostIndustry")]
        [HttpPost]
        public HttpResponseMessage PostIndustry(industry_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostIndustry(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("GetIndustrySummary")]
        [HttpGet]
        public HttpResponseMessage GetIndustrySummary()
        {
            MdlMarketingIndustry values = new MdlMarketingIndustry();
            objDaCustomer.DaGetIndustrySummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetupdateIndustrytdetails")]
        [HttpPost]
        public HttpResponseMessage GetupdateIndustrytdetails(industry_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaGetupdateIndustrytdetails(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }





        [ActionName("Getdeleteindustrydetails")]
        [HttpGet]
        public HttpResponseMessage GetdeleteIndustrydetails(string categoryindustry_gid)
        {
            industry_list objresult = new industry_list();
            objDaCustomer.DaGetdeleteindustrydetails( categoryindustry_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            MdlMarketingIndustry objresult = new MdlMarketingIndustry();
            objDaCustomer.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}