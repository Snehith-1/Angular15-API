using ems.system.DataAccess;
using ems.system.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.system.Controllers
{

    [RoutePrefix("api/Telegram")]
    [Authorize]
    public class TelegramController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaTelegram objDaTelegram = new DaTelegram();
        [ActionName("GetTelegram")]
        [HttpGet]
        public HttpResponseMessage GetTelegram()
        {
            telegramlist values = new telegramlist();
            values = objDaTelegram.DaGetTelegram();
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TelegramUpload")]
        [HttpPost]
        public HttpResponseMessage TelegramUpload(string telegram_caption)
        {
            HttpRequest httpRequest;
            //Postassetlocationcreation values = new Postassetlocationcreation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaTelegram.DaTelegramUpload(httpRequest, getsessionvalues.user_gid, telegram_caption, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("Telegrammessage")]
        [HttpPost]
        public HttpResponseMessage Telegrammessage(message_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objDaTelegram.DaTelegrammessage( getsessionvalues.user_gid, values, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
    }
}