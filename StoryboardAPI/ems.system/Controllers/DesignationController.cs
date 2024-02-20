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
    [RoutePrefix("api/Designation")]
    [Authorize]

    public class DesignationController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaDesignation objDaCustomer = new DaDesignation();

        // Customer Add
        [ActionName("PostDesignation")]
        [HttpPost]
        public HttpResponseMessage PostDesignation(designation_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostDesignation(getsessionvalues.user_gid, values);
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

        //    customerdivision values = new customerdivision();
        //    objDaCustomer.DaGetCustomerdivision(quotation_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        ////  Customer View

        //[ActionName("GetCustomerView")]
        //[HttpGet]
        //public HttpResponseMessage GetCustomerView(string customer_gid)
        //{
        //    MdlCustomerAdd values = new MdlCustomerAdd();
        //    objDaCustomer.DaGetCustomerView(customer_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
        //// Customer Edit
        //[ActionName("PostCustomerUpdate")]
        //[HttpPost]
        //public HttpResponseMessage PostCustomerUpdate(MdlCustomerUpdate values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaCustomer.DaPostCustomerUpdate(values, getsessionvalues.user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        ////Add Division

        //[ActionName("PostAddDivisionCustomer")]
        //[HttpPost]
        //public HttpResponseMessage PostAddDivisionCustomer(MdlAddDivisionCustomer values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaCustomer.DaPostAddDivisionCustomer(values, getsessionvalues.user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
        //[ActionName("GetDivisionSummary")]
        //[HttpGet]
        //public HttpResponseMessage GetDivisionSummary(string customer_gid)
        //{

        //    MdlDivision values = new MdlDivision();
        //    objDaCustomer.DaGetDivisionSummary(customer_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}


        //// Customer Contact Details

        //[ActionName("PostCustomerContactDetails")]
        //[HttpPost]
        //public HttpResponseMessage PostCustomerContactDetails(MdlCustomerContactDetails values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaCustomer.DaPostCustomerContactDetails(values, getsessionvalues.user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //[ActionName("GetCustomerContactDetailsSummary")]
        //[HttpGet]
        //public HttpResponseMessage CustomerContactDetailsSummary(string customer_gid)
        //{

        //    MdlCustomerContact values = new Models.MdlCustomerContact();
        //    objDaCustomer.DaGetCustomerContactDetailsSummary(customer_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        ////Contact details delete
        //[ActionName("CustomerContactDetailsDelete")]
        //[HttpGet]
        //public HttpResponseMessage CustomerContactDetailsDelete(string customercontactdetails_gid)
        //{
        //    MdlCustomerContactDetails values = new MdlCustomerContactDetails();
        //    objDaCustomer.DaGetCustomerContactDetailsDelete(customercontactdetails_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        ////Customer division delete
        //[ActionName("CustomerDivisionDelete")]
        //[HttpGet]
        //public HttpResponseMessage CustomerDivisionDelete(string customer2division_gid)
        //{
        //    MdlAddDivisionCustomer values = new MdlAddDivisionCustomer();
        //    objDaCustomer.DaGetCustomerDivisionDelete(customer2division_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}



    }
}