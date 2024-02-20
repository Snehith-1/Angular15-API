﻿using ems.asset.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.asset.Models;
using ems.asset.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.asset.Controllers
{
    [RoutePrefix("api/Block")]
    [Authorize]

    public class BlockController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaBlock objDaCustomer = new DaBlock();

        [ActionName("PostBlock")]
        [HttpPost]

        public HttpResponseMessage PostBlock(block_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaPostBlock(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("GetBlockSummary")]
        [HttpGet]
        public HttpResponseMessage GetBlockSummary()
        {
            blocklist values = new blocklist();
            objDaCustomer.DaGetBlockSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getunitdropdown")]
        [HttpGet]
        public HttpResponseMessage Getunitdropdown()
        {
            MdlBlock values = new MdlBlock();
            objDaCustomer.DaGetunitdropdown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("updateBlock")]
        [HttpPost]

        public HttpResponseMessage updateBlock(block_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomer.DaupdateBlock(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        [ActionName("GetdeleteBlockdetails")]
        [HttpGet]
        public HttpResponseMessage GetdeleteBlockdetails(string locationblock_gid)
        {
            block_list objresult = new block_list();
            objDaCustomer.DaGetdeleteBlockdetails(locationblock_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getbreadcrumb")]
        [HttpGet]
        public HttpResponseMessage Getbreadcrumb(string user_gid, string module_gid)
        {
            blocklist objresult = new blocklist();
            objDaCustomer.DaGetbreadcrumb(user_gid, module_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}