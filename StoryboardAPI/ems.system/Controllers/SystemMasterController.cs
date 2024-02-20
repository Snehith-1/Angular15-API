using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.system.Models;
using ems.system.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.system.Controllers
{
    [RoutePrefix("api/SystemMaster")]
    [Authorize]

    public class SystemMasterController : ApiController
    {
        DaSystemMaster objDaSystemMaster = new DaSystemMaster();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        //
        //Blood Group
        [ActionName("GetBloodGroup")]
        [HttpGet]
        public HttpResponseMessage GetBloodGroup()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBloodGroup(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreateBloodGroup")]
        [HttpPost]
        public HttpResponseMessage CreateBloodGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateBloodGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditBloodGroup")]
        [HttpGet]
        public HttpResponseMessage EditBloodGroup(string bloodgroup_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditBloodGroup(bloodgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBloodGroup")]
        [HttpPost]
        public HttpResponseMessage UpdateBloodGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateBloodGroup(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBloodGroup")]
        [HttpPost]
        public HttpResponseMessage InactiveBloodGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveBloodGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBloodGroup")]
        [HttpGet]
        public HttpResponseMessage DeleteBloodGroup(string bloodgroup_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteBloodGroup(bloodgroup_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BloodGroupInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BloodGroupInactiveLogview(string bloodgroup_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaBloodGroupInactiveLogview(bloodgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Base Location
        [ActionName("GetBaseLocation")]
        [HttpGet]
        public HttpResponseMessage GetBaseLocation()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBaseLocation(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("GetBaseLocationlist")]
        [HttpGet]
        public HttpResponseMessage GetBaseLocationlist()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBaseLocationlist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreateBaseLocation")]
        [HttpPost]
        public HttpResponseMessage CreateBaseLocation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateBaseLocation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditBaseLocation")]
        [HttpGet]
        public HttpResponseMessage EditBaseLocation(string baselocation_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditBaseLocation(baselocation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBaseLocation")]
        [HttpPost]
        public HttpResponseMessage UpdateBaseLocation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateBaseLocation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBaseLocation")]
        [HttpPost]
        public HttpResponseMessage InactiveBaseLocation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveBaseLocation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBaseLocation")]
        [HttpGet]
        public HttpResponseMessage DeleteBaseLocation(string baselocation_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteBaseLocation(baselocation_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BaseLocationInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BaseLocationInactiveLogview(string baselocation_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaBaseLocationInactiveLogview(baselocation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Physical Status
        [ActionName("GetPhysicalStatus")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalStatus()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetPhysicalStatus(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreatePhysicalStatus")]
        [HttpPost]
        public HttpResponseMessage CreatePhysicalStatus(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreatePhysicalStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditPhysicalStatus")]
        [HttpGet]
        public HttpResponseMessage EditPhysicalStatus(string physicalstatus_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditPhysicalStatus(physicalstatus_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePhysicalStatus")]
        [HttpPost]
        public HttpResponseMessage UpdatePhysicalStatus(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdatePhysicalStatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactivePhysicalStatus")]
        [HttpPost]
        public HttpResponseMessage InactivePhysicalStatus(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactivePhysicalStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeletePhysicalStatus")]
        [HttpGet]
        public HttpResponseMessage DeletePhysicalStatus(string physicalstatus_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeletePhysicalStatus(physicalstatus_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PhysicalStatusInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage PhysicalStatusInactiveLogview(string physicalstatus_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaPhysicalStatusInactiveLogview(physicalstatus_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Calendar Group
        [ActionName("GetCalendarGroup")]
        [HttpGet]
        public HttpResponseMessage GetCalendarGroup()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetCalendarGroup(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreateCalendarGroup")]
        [HttpPost]
        public HttpResponseMessage CreateCalendarGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateCalendarGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditCalendarGroup")]
        [HttpGet]
        public HttpResponseMessage EditCalendarGroup(string calendargroup_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditCalendarGroup(calendargroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCalendarGroup")]
        [HttpPost]
        public HttpResponseMessage UpdateCalendarGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateCalendarGroup(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCalendarGroup")]
        [HttpPost]
        public HttpResponseMessage InactiveCalendarGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveCalendarGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCalendarGroup")]
        [HttpGet]
        public HttpResponseMessage DeleteCalendarGroup(string calendargroup_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteCalendarGroup(calendargroup_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CalendarGroupInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CalendarGroupInactiveLogview(string calendargroup_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaCalendarGroupInactiveLogview(calendargroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Client Role
        [ActionName("GetClientRole")]
        [HttpGet]
        public HttpResponseMessage GetClientRole()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetClientRole(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreateClientRole")]
        [HttpPost]
        public HttpResponseMessage CreateClientRole(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateClientRole(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditClientRole")]
        [HttpGet]
        public HttpResponseMessage EditClientRole(string clientrole_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditClientRole(clientrole_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateClientRole")]
        [HttpPost]
        public HttpResponseMessage UpdateClientRole(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateClientRole(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveClientRole")]
        [HttpPost]
        public HttpResponseMessage InactiveClientRole(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveClientRole(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteClientRole")]
        [HttpGet]
        public HttpResponseMessage DeleteClientRole(string clientrole_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteClientRole(clientrole_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ClientRoleInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ClientRoleInactiveLogview(string clientrole_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaClientRoleInactiveLogview(clientrole_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Salutation
        [ActionName("GetSalutation")]
        [HttpGet]
        public HttpResponseMessage GetSalutation()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetSalutation(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreateSalutation")]
        [HttpPost]
        public HttpResponseMessage CreateSalutation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateSalutation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditSalutation")]
        [HttpGet]
        public HttpResponseMessage EditSalutation(string salutation_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditSalutation(salutation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSalutation")]
        [HttpPost]
        public HttpResponseMessage UpdateSalutation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateSalutation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSalutation")]
        [HttpPost]
        public HttpResponseMessage InactiveSalutation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveSalutation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSalutation")]
        [HttpGet]
        public HttpResponseMessage DeleteSalutation(string salutation_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteSalutation(salutation_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SalutationInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage SalutationInactiveLogview(string salutation_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaSalutationInactiveLogview(salutation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Project

        [ActionName("CreateProject")]
        [HttpPost]
        public HttpResponseMessage CreateProject(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateProject(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProject")]
        [HttpGet]
        public HttpResponseMessage GetProject()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetProject(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("EditProject")]
        [HttpGet]
        public HttpResponseMessage EditProject(string project_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditProject(project_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateProject")]
        [HttpPost]
        public HttpResponseMessage UpdateProject(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateProject(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveProject")]
        [HttpPost]
        public HttpResponseMessage InactiveProject(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveProject(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteProject")]
        [HttpGet]
        public HttpResponseMessage DeleteProject(string project_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteProject(project_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ProjectInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ProjectInactiveLogview(string project_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaProjectInactiveLogview(project_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Cluster Add
        [ActionName("PostClusterAdd")]
        [HttpPost]
        public HttpResponseMessage PostClusterAdd(cluster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostClusterAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Cluster Summary
        [ActionName("GetClusterSummary")]
        [HttpGet]
        public HttpResponseMessage GetClusterSummary()
        {
            cluster objmaster = new cluster();
            objDaSystemMaster.DaGetClusterSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetClusterEdit")]
        [HttpGet]
        public HttpResponseMessage GetClusterEdit(string cluster_gid)
        {
            cluster objmaster = new cluster();
            objDaSystemMaster.DaGetClusterEdit(cluster_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostClusterUpdate")]
        [HttpPost]
        public HttpResponseMessage PostClusterUpdate(cluster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostClusterUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCluster2BaseLocation")]
        [HttpGet]
        public HttpResponseMessage GetCluster2BaseLocation(string cluster_gid)
        {
            cluster objmaster = new cluster();
            objDaSystemMaster.DaGetCluster2BaseLocation(cluster_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //UnTagged Locations
        [ActionName("GetUnTaggedLocations")]
        [HttpGet]
        public HttpResponseMessage GetUnTaggedLocations()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetUnTaggedLocations(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //UnTagged Locations Edit
        [ActionName("GetUnTaggedLocationsEdit")]
        [HttpGet]
        public HttpResponseMessage GetUnTaggedLocationsEdit(string cluster_gid)
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetUnTaggedLocationsEdit(objmaster, cluster_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("InactiveCluster")]
        [HttpPost]
        public HttpResponseMessage InactiveCluster(cluster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveCluster(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ClusterInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ClusterInactiveLogview(string cluster_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaClusterInactiveLogview(cluster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Region Add
        [ActionName("PostRegionAdd")]
        [HttpPost]
        public HttpResponseMessage PostRegionAdd(region values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostRegionAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Region Summary
        [ActionName("GetRegionSummary")]
        [HttpGet]
        public HttpResponseMessage GetRegionSummary()
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetRegionSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetRegionEdit")]
        [HttpGet]
        public HttpResponseMessage GetRegionEdit(string region_gid)
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetRegionEdit(region_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostRegionUpdate")]
        [HttpPost]
        public HttpResponseMessage PostRegionUpdate(region values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostRegionUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRegion2Cluster")]
        [HttpGet]
        public HttpResponseMessage GetRegion2Cluster(string region_gid)
        {
            cluster objmaster = new cluster();
            objDaSystemMaster.DaGetRegion2Cluster(region_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //UnTagged Clusters
        [ActionName("GetUnTaggedClusters")]
        [HttpGet]
        public HttpResponseMessage GetUnTaggedClusters()
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetUnTaggedClusters(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //UnTagged Clusters
        [ActionName("GetUnTaggedClustersEdit")]
        [HttpGet]
        public HttpResponseMessage GetUnTaggedClustersEdit(string region_gid)
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetUnTaggedClustersEdit(objmaster, region_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostRegionInactive")]
        [HttpPost]
        public HttpResponseMessage PostRegionInactive(region values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostRegionInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRegionInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetRegionInactiveLogview(string region_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaGetRegionInactiveLogview(region_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //// Zone Add
        [ActionName("PostZoneAdd")]
        [HttpPost]
        public HttpResponseMessage PostZoneAdd(zone values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostZoneAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Zone Summary
        [ActionName("GetZoneSummary")]
        [HttpGet]
        public HttpResponseMessage GetZoneSummary()
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetZoneSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetZoneEdit")]
        [HttpGet]
        public HttpResponseMessage GetZoneEdit(string zone_gid)
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetZoneEdit(zone_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostZoneUpdate")]
        [HttpPost]
        public HttpResponseMessage PostZoneUpdate(zone values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostZoneUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetZone2Region")]
        [HttpGet]
        public HttpResponseMessage GetZone2Region(string zone_gid)
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetZone2Region(zone_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //UnTagged Region
        [ActionName("GetUnTaggedRegions")]
        [HttpGet]
        public HttpResponseMessage GetUnTaggedRegions()
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetUnTaggedRegions(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //UnTagged Region
        [ActionName("GetUnTaggedRegionsEdit")]
        [HttpGet]
        public HttpResponseMessage GetUnTaggedRegionsEdit(string zone_gid)
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetUnTaggedRegionsEdit(objmaster, zone_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostZoneInactive")]
        [HttpPost]
        public HttpResponseMessage PostZoneInactive(zone values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostZoneInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetZoneInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetZoneInactiveLogview(string zone_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaGetZoneInactiveLogview(zone_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Region List
        [ActionName("GetRegionList")]
        [HttpGet]
        public HttpResponseMessage GetRegionList()
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetRegionList(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        // Vertical List

        [ActionName("GetVerticallist")]
        [HttpGet]
        public HttpResponseMessage GetVerticallist()
        {
            mdlvertical objmaster = new mdlvertical();
            objDaSystemMaster.DaGetVerticallist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        // Employee list 

        [ActionName("GetEmployeelist")]
        [HttpGet]
        public HttpResponseMessage GetEmployeelist()
        {
            mdlemployee objmaster = new mdlemployee();
            objDaSystemMaster.DaGetEmployeelist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        // Region Head Add
        [ActionName("PostRegionHeadAdd")]
        [HttpPost]
        public HttpResponseMessage PostRegionHeadAdd(mdlregionhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostRegionHeadAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Region Head Summary
        [ActionName("GetRegionHeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetRegionHeadSummary()
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetRegionHeadSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostRegionHeadInactive")]
        [HttpPost]
        public HttpResponseMessage PostRegionHeadInactive(mdlregionhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostRegionHeadInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRegionHeadInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetRegionHeadInactiveLogview(string regionhead_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaGetRegionHeadInactiveLogview(regionhead_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRegionHeadEdit")]
        [HttpGet]
        public HttpResponseMessage GetRegionHeadEdit(string regionhead_gid)
        {
            mdlregionhead objmaster = new mdlregionhead();
            objDaSystemMaster.DaGetRegionHeadEdit(regionhead_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostRegionHeadUpdate")]
        [HttpPost]
        public HttpResponseMessage PostRegionHeadUpdate(mdlregionhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostRegionHeadUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Zone List
        [ActionName("GetZoneList")]
        [HttpGet]
        public HttpResponseMessage GetZoneList()
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetZoneList(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        // Business Head Add
        [ActionName("PostBusinessHeadAdd")]
        [HttpPost]
        public HttpResponseMessage PostBusinessHeadAdd(mdlbusinesshead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostBusinessHeadAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Business Head Summary
        [ActionName("GetBusinessHeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetBusinessHeadSummary()
        {
            mdlbusinesshead objmaster = new mdlbusinesshead();
            objDaSystemMaster.DaGetBusinessHeadSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostBusinessHeadInactive")]
        [HttpPost]
        public HttpResponseMessage PostBusinessHeadInactive(mdlbusinesshead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostBusinessHeadInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBusinessHeadInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetBusinessHeadInactiveLogview(string businesshead_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaGetBusinessHeadInactiveLogview(businesshead_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBusinessHeadEdit")]
        [HttpGet]
        public HttpResponseMessage GetBusinessHeadEdit(string businesshead_gid)
        {
            mdlbusinesshead objmaster = new mdlbusinesshead();
            objDaSystemMaster.DaGetBusinessHeadEdit(businesshead_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostBusinessHeadUpdate")]
        [HttpPost]
        public HttpResponseMessage PostBusinessHeadUpdate(mdlbusinesshead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostBusinessHeadUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Group Business Head Add
        [ActionName("PostGroupBusinessHeadAdd")]
        [HttpPost]
        public HttpResponseMessage PostGroupBusinessHeadAdd(mdlbusinesshead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostGroupBusinessHeadAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Business Head Summary
        [ActionName("GetGroupBusinessHeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetGroupBusinessHeadSummary()
        {
            mdlbusinesshead objmaster = new mdlbusinesshead();
            objDaSystemMaster.DaGetGroupBusinessHeadSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostGroupBusinessHeadInactive")]
        [HttpPost]
        public HttpResponseMessage PostGroupBusinessHeadInactive(mdlbusinesshead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostGroupBusinessHeadInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupBusinessHeadInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetGroupBusinessHeadInactiveLogview(string groupbusinesshead_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaGetGroupBusinessHeadInactiveLogview(groupbusinesshead_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupBusinessHeadEdit")]
        [HttpGet]
        public HttpResponseMessage GetGroupBusinessHeadEdit(string groupbusinesshead_gid)
        {
            mdlbusinesshead objmaster = new mdlbusinesshead();
            objDaSystemMaster.DaGetGroupBusinessHeadEdit(groupbusinesshead_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostGroupBusinessHeadUpdate")]
        [HttpPost]
        public HttpResponseMessage PostGroupBusinessHeadUpdate(mdlbusinesshead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostGroupBusinessHeadUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Cluster Head codes

        // Clusters List
        [ActionName("GetClusterslist")]
        [HttpGet]
        public HttpResponseMessage GetClusterslist()
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetClusterslist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        // Cluster Head Add
        [ActionName("PostClusterheadAdd")]
        [HttpPost]
        public HttpResponseMessage PostClusterheadAdd(mdlclusterhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostClusterheadAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("InactiveClusterhead")]
        [HttpPost]
        public HttpResponseMessage InactiveClusterhead(mdlclusterhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveClusterhead(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ClusterheadInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ClusterheadInactiveLogview(string clusterhead_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaClusterheadInactiveLogview(clusterhead_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Cluster Head Summary
        [ActionName("GetClusterHeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetClusterHeadSummary()
        {
            cluster objmaster = new cluster();
            objDaSystemMaster.DaGetClusterHeadSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }


        [ActionName("GetClusterHeadEdit")]
        [HttpGet]
        public HttpResponseMessage GetClusterHeadEdit(string clusterhead_gid)
        {
            mdlclusterhead objmaster = new mdlclusterhead();
            objDaSystemMaster.DaGetClusterHeadEdit(clusterhead_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostClusterHeadUpdate")]
        [HttpPost]
        public HttpResponseMessage PostClusterHeadUpdate(mdlclusterhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostClusterHeadUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Zonal Head Codes



        [ActionName("GetZonallist")]
        [HttpGet]
        public HttpResponseMessage GetZonalslist()
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetZonalslist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostZonalheadAdd")]
        [HttpPost]
        public HttpResponseMessage PostZonalheadAdd(mdlzonalhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostZonalheadAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("InactiveZonalhead")]
        [HttpPost]
        public HttpResponseMessage InactiveZonalhead(mdlzonalhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveZonalhead(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ZonalheadInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ZonalheadInactiveLogview(string zonalhead_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaZonalheadInactiveLogview(zonalhead_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Cluster Head Summary
        [ActionName("GetZonalHeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetZonalHeadSummary()
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetZonalHeadSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }


        [ActionName("GetZonalHeadEdit")]
        [HttpGet]
        public HttpResponseMessage GetZonalHeadEdit(string zonalhead_gid)
        {
            mdlzonalhead objmaster = new mdlzonalhead();
            objDaSystemMaster.DaGetZonalHeadEdit(zonalhead_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostZonalHeadUpdate")]
        [HttpPost]
        public HttpResponseMessage PostZonalHeadUpdate(mdlzonalhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostZonalHeadUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Product Head Codes


        [ActionName("PostProductheadAdd")]
        [HttpPost]
        public HttpResponseMessage PostProductheadAdd(mdlproducthead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostProductheadAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("InactiveProducthead")]
        [HttpPost]
        public HttpResponseMessage InactiveProducthead(mdlproducthead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveProducthead(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ProductheadInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ProductheadInactiveLogview(string producthead_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaProductheadInactiveLogview(producthead_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetProductHeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductHeadSummary()
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetProductHeadSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }


        [ActionName("GetProductHeadEdit")]
        [HttpGet]
        public HttpResponseMessage GetProductHeadEdit(string producthead_gid)
        {
            mdlproducthead objmaster = new mdlproducthead();
            objDaSystemMaster.DaGetProductHeadEdit(producthead_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostProductHeadUpdate")]
        [HttpPost]
        public HttpResponseMessage PostProductHeadUpdate(mdlproducthead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostProductHeadUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Task

        [ActionName("PostTaskAdd")]
        [HttpPost]
        public HttpResponseMessage PostTaskAdd(MdlTask values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostTaskAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaskSummary()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetTaskSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("EditTask")]
        [HttpGet]
        public HttpResponseMessage EditTask(string task_gid)
        {
            MdlTask objmaster = new MdlTask();
            objDaSystemMaster.DaEditTask(task_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("UpdateTask")]
        [HttpPost]
        public HttpResponseMessage UpdateTask(MdlTask values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateTask(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveTask")]
        [HttpPost]
        public HttpResponseMessage InactiveTask(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveTask(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TaskInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage TaskInactiveLogview(string task_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaTaskInactiveLogview(task_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteTask")]
        [HttpGet]
        public HttpResponseMessage DeleteTask(string task_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteTask(task_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskMultiselectList")]
        [HttpGet]
        public HttpResponseMessage GetTaskMultiselectList(string task_gid)
        {
            MdlTask objmaster = new MdlTask();
            objDaSystemMaster.DaGetTaskMultiselectList(task_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

    }
}