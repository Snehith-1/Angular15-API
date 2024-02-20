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
    [RoutePrefix("api/Productunit")]
    [Authorize]
    public class ProductunitController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaProductunit objDaPurchase = new DaProductunit();
        // Module Summary
        [ActionName("GetProductunitSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductunitSummary()
        {
            MdlProductunit values = new MdlProductunit();
            objDaPurchase.DaGetProductunitSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductunitSummarygrid")]
        [HttpGet]
        public HttpResponseMessage GetProductunitSummarygrid(string productuomclass_gid)
        {
            MdlProductunit objresult = new MdlProductunit();
            objDaPurchase.DaGetProductunitSummarygrid(productuomclass_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostProductunit")]
        [HttpPost]
        public HttpResponseMessage PostProductunit(productunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaPostProductunit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("UpdatedProductunit")]
        [HttpPost]
        public HttpResponseMessage UpdatedProductunit(productunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPurchase.DaUpdatedProductunit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("deleteProductunitSummary")]
        [HttpGet]
        public HttpResponseMessage deleteProductunitSummary(string productuomclass_gid)
        {
            productunit_list objresult = new productunit_list();
            objDaPurchase.DadeleteProductunitSummary(productuomclass_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}