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
    [RoutePrefix("api/EmployeeProfile")]
    [Authorize]

    public class EmployeeProfileController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaEmployeeProfile objDaemployee = new DaEmployeeProfile();



        //[ActionName("PersonalDetailsSubmit")]
        //[HttpPost]
        //public HttpResponseMessage PersonalDetailsSubmit(EmployeeProfile values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaemployee.DaPersonalDetails(getsessionvalues.user_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, true);
        //}

        //[ActionName("ChangePassword")]
        //[HttpPost]
        //public HttpResponseMessage ChangePassword(EmployeeProfile values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaemployee.DaChangePassword(getsessionvalues.user_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, true);
        //}


        [ActionName("WorkExperienceSubmit")]
        [HttpPost]
        public HttpResponseMessage WorkExperienceSubmit(EmployeeProfile values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaemployee.DaWorkExperience(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("NominationSubmit")]
        [HttpPost]
        public HttpResponseMessage NominationSubmit(EmployeeProfile values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaemployee.DaNomination(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("StatutorySubmit")]
        [HttpPost]
        public HttpResponseMessage StatutorySubmit(EmployeeProfile values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaemployee.DaStatutory(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("EmergencySubmit")]
        [HttpPost]
        public HttpResponseMessage EmergencySubmit(EmployeeProfile values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaemployee.DaEmergencyContact(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("DependentSubmit")]
        [HttpPost]
        public HttpResponseMessage DependentSubmit(EmployeeProfile values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaemployee.DaDependent(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [ActionName("EducationSubmit")]
        [HttpPost]
        public HttpResponseMessage EducationSubmit(EmployeeProfile values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaemployee.DaEducation(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
    }
}