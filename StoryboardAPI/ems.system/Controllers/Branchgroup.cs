using ems.system.DataAccess;
using ems.system.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace ems.system.Controllers
{
    [RoutePrefix("api/Branchgroup")]
    [Authorize]

    public class BranchgroupController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaBranchgroup objDaCustomer = new DaBranchgroup();

        // Customer Add
        [ActionName("Postbranchgroup")]
        [HttpPost]
        public HttpResponseMessage Postbranchgroup(branchgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostbranchgroup(getsessionvalues.user_gid, values); 
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        //// Customer corporate Add
        //[ActionName("PostCcustomerAdd")]
        //[HttpPost]
        //public HttpResponseMessage PostCcustomerAdd(Customercomp_list values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaCustomer.DaPostCcustomerAdd(values, getsessionvalues.user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, true);
        //}

        //// Customer Summary
        //[ActionName("GetCustomerSummary")]
        //[HttpGet]
        //public HttpResponseMessage GetCustomerSummary()
        //{
        //    MdlCustomer_list values = new MdlCustomer_list();
        //    objDaCustomer.DaGetCustomerSummary(values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        ////Customer Summary corporate
        //[ActionName("GetCcustomerSummary")]
        //[HttpGet]
        //public HttpResponseMessage GetCcustomerSummary()
        //{
        //    MdlCustomercomp_list values = new MdlCustomercomp_list();
        //    objDaCustomer.DaGetCcustomerSummary(values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //[ActionName("GetCustomerContactmobile")]
        //[HttpGet]
        //public HttpResponseMessage GetCustomerContactmobile(string customercontactdetails_gid)
        //{

        //    MdlCustomerContact values = new MdlCustomerContact();
        //    objDaCustomer.DaGetCustomerContactmobile(customercontactdetails_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //[ActionName("GetCustomerdivision")]
        //[HttpGet]
        //public HttpResponseMessage GetCustomerdivision(string quotation_gid)
        //{


    }
}