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

    [RoutePrefix("api/Productattributes")]
    [Authorize]
    public class ProductattributesController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaProductattributes objDaPurchase = new DaProductattributes();
        // Module Summary
        [ActionName("GetProductattributesSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductattributesSummary()
        {
            MdlProductattributes values = new MdlProductattributes();
            objDaPurchase.DaGetProductattributesSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Terms and conditions
        [ActionName("PostProductattributes")]
        [HttpPost]
        public HttpResponseMessage PostProductattributes(productattributes_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPostProductattributes(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("UpdatedProductattributes")]
        [HttpPost]
        public HttpResponseMessage UpdatedProductattributes(productattributes_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaUpdatedProductattributes(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("deleteProductattributesSummary")]
        [HttpGet]
        public HttpResponseMessage deleteProductattributesSummary(string productattribute_gid)
        {
            productattributes_list objresult = new productattributes_list();
            objDaPurchase.DadeleteProductattributesSummary(productattribute_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Getassignproductattribute")]
        [HttpGet]
        public HttpResponseMessage Getassignproductattribute(string user_gid, string productattribute_gid)
        {
            MdlProductattributes objresult = new MdlProductattributes();
            objDaPurchase.DaGetassignproductattribute(user_gid, productattribute_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Productassign")]
        [HttpPost]
        public HttpResponseMessage Productassign(productattributes_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaProductassign(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("Getunassignproductattribute")]
        [HttpGet]
        public HttpResponseMessage Getunassignproductattribute(string user_gid, string productattribute_gid)
        {
            MdlProductattributes objresult = new MdlProductattributes();
            objDaPurchase.DaGetunassignproductattribute(user_gid, productattribute_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Productunassign")]
        [HttpPost]
        public HttpResponseMessage Productunassign(productattributes_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaProductunassign(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

    }
}