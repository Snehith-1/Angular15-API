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
using System.Configuration;
using System.IO;

namespace ems.pmr.Controllers
{
    [RoutePrefix("api/PmrMstVendorRegister")]
    [Authorize]
    public class PmrMstVendorRegisterController:ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaPmrMstVendorRegister objpurchase = new DaPmrMstVendorRegister();
        // Module Summary
        [ActionName("GetVendorRegisterSummary")]
        [HttpGet]
        public HttpResponseMessage GetVendorRegisterSummary()
        {
            MdlPmrMstVendorRegister values = new MdlPmrMstVendorRegister();
            objpurchase.DaGetVendorRegisterSummary (values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetBreadCrumb")]
        [HttpGet]
        public HttpResponseMessage GetBreadCrumb(string user_gid, string module_gid)
        {
            MdlPmrMstVendorRegister objresult = new MdlPmrMstVendorRegister();
            objpurchase.DaGetBreadCrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetCurrency")]
        [HttpGet]
        public HttpResponseMessage GetCurrency()
        {
            MdlPmrMstVendorRegister values = new MdlPmrMstVendorRegister();
            objpurchase.DaGetCurrency (values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCountry")]
        [HttpGet]
        public HttpResponseMessage GetCountry()
        {
            MdlPmrMstVendorRegister values = new MdlPmrMstVendorRegister();
            objpurchase.DaGetCountry(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetTax")]
        [HttpGet]
        public HttpResponseMessage GetTax()
        {
            MdlPmrMstVendorRegister values = new MdlPmrMstVendorRegister();
            objpurchase.DaGetTax(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostVendorRegister")]
        [HttpPost]
        public HttpResponseMessage PostVendorRegister(vendor_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostVendorRegister(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }


        [ActionName("GetVendorRegisterDetail")]
        [HttpGet]
        public HttpResponseMessage GetVendorRegisterDetail(string user_gid, string vendorregister_gid)
        {
            MdlPmrMstVendorRegister objresult = new MdlPmrMstVendorRegister();
            objpurchase.DaGetVendorRegisterDetail(user_gid, vendorregister_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("PostVendorRegisterUpdate")]
        [HttpPost]
        public HttpResponseMessage PostVendorRegisterUpdate (vendor_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostVendorRegisterUpdate(getsessionvalues.user_gid,  values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("PostVendorRegisterAdditionalInformation")]
        [HttpPost]
        public HttpResponseMessage PostVendorRegisterAdditionalInformation(vendor_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostVendorRegisterAdditionalInformation(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("PostVendorRegisterAttachment")]
        [HttpPost]
        public HttpResponseMessage PostVendorRegisterAttachment(vendor_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objpurchase.DaPostVendorRegisterAttachment(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("GetDocumentType")]
        [HttpGet]
        public HttpResponseMessage GetDocumentType()
        {
            MdlPmrMstVendorRegister values = new MdlPmrMstVendorRegister();
            objpurchase.DaGetDocumentType(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("VendorRegisterSummaryDelete")]
        [HttpGet]
        public HttpResponseMessage VendorRegisterSummaryDelete(string vendorregister_gid)
        {
            vendor_list objresult = new vendor_list();
            objpurchase.DaVendorRegisterSummaryDelete(vendorregister_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetVendorRegisterDocumentDetail")]
        [HttpGet]
        public HttpResponseMessage GetVendorRegisterDocumentDetail(string user_gid, string vendorregister_gid)
        {
            MdlPmrMstVendorRegister objresult = new MdlPmrMstVendorRegister();
            objpurchase.DaGetVendorRegisterDocumentDetail(user_gid, vendorregister_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetVendorRegisterDocumentDetailDownload")]
        [HttpGet]
        public HttpResponseMessage GetVendorRegisterDocumentDetailDownload (string document_gid)
        {
            MdlPmrMstVendorRegister objresult = new MdlPmrMstVendorRegister();
            objpurchase.DaGetVendorRegisterDocumentDetailDownload(document_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        










    }
}