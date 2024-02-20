using ems.crm.DataAccess;
using ems.crm.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.crm.Controllers
{
    [RoutePrefix("api/Mailmanagement")]
    [Authorize]
    public class MailManagementController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMailManagement objDaMarketing = new DaMailManagement();

        [ActionName("mailfunction")]
        [HttpPost]
        public HttpResponseMessage mailfunction(MdlMail values)
        {
            HttpRequest httpRequest;
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaMarketing.Dasendmailtocustomer(httpRequest, values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

    }
}