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

    [RoutePrefix("api/Productgroup")]
    [Authorize]
    public class ProductgroupController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaProductgroup objDaPurchase = new DaProductgroup();
        // Module Summary
        [ActionName("GetProductgroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductgroupSummary()
        {
            MdlProductgroup values = new MdlProductgroup();
            objDaPurchase.DaGetProductgroupSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Post  Terms and conditions
        [ActionName("PostProductgroup")]
        [HttpPost]
        public HttpResponseMessage PostProductgroup(productgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPostProductgroup(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("UpdatedProductgroup")]
        [HttpPost]
        public HttpResponseMessage UpdatedProductgroup(productgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaUpdatedProductgroup(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("deleteProductgroupSummary")]
        [HttpGet]
        public HttpResponseMessage deleteProductgroupSummary(string productgroup_gid)
        {
            productgroup_list objresult = new productgroup_list();
            objDaPurchase.DadeleteProductgroupSummary(productgroup_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Unmappingvendor")]
        [HttpGet]
        public HttpResponseMessage Unmappingvendor(string user_gid, string productgroup_gid)
        {
            MdlProductgroup objresult = new MdlProductgroup();
            objDaPurchase.DaUnmappingvendor(user_gid, productgroup_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("Mappingvendor")]
        [HttpGet]
        public HttpResponseMessage Mappingvendor(string user_gid, string productgroup_gid)
        {
            MdlProductgroup objresult = new MdlProductgroup();
            objDaPurchase.DaMappingvendor(user_gid, productgroup_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostMappingvendor")]
        [HttpPost]
        public HttpResponseMessage PostMappingvendor(productgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPostMappingvendor(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("PostUnmappingvendor")]
        [HttpPost]
        public HttpResponseMessage PostUnmappingvendor(productgroup_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPostUnmappingvendor(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }


        
    }
}