using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ems.asset.Models
{
    //--------------------------Temporary Handover Assets---------------------------//

    public class tmphandoverassets : result
    //Declaration
    {
        public List<tmphandoversummary> tmphandoversummary { get; set; }
        public List<tempHoldersummary> tempHoldersummary { get; set; }
        public List<tempHoldinsassetsummary> tempHoldinsassetsummary { get; set; }
        public List<tempadminsurrendersummary> tempadminsurrendersummary { get; set; }
    }

    // Definition
    public class tmphandoversummary
    {
        public string asset_id { get; set; }
        public string assetserial_id { get; set; }
        public string asset_name { get; set; }
        public string issued_by { get; set; }
        public string issued_date { get; set; }
        public string asset_image { get; set; }
        public string tmphandover_employeecode { get; set; }
        public string tmphandover_employeename { get; set; }
        public string tmphandover_date { get; set; }
        public string tmphandover_employeeid { get; set; }
        public string tmphandover_employeedept { get; set; }
        public string tmphandover_employeedesg { get; set; }
        public string tmphandover_status { get; set; }
    }
    public class tempHoldersummary
    {
        public string asset_id { get; set; }
        public string assetserial_id { get; set; }
        public string asset_name { get; set; }
        public string issued_by { get; set; }
        public string issued_date { get; set; }
        public string asset_image { get; set; }
        public string prev_employeecode { get; set; }
        public string prev_employeename { get; set; }
        public string tmphandover_date { get; set; }
        public string prev_employeeid { get; set; }
        public string prev_employeedept { get; set; }
        public string prev_employeedesg { get; set; }
        public string tmphandover_status { get; set; }
    }

    public class tempHoldinsassetsummary
    {
        public string asset_id { get; set; }
        public string assetserial_id { get; set; }
        public string asset_name { get; set; }
        public string issued_by { get; set; }
        public string issued_date { get; set; }
        public string asset_image { get; set; }
        public string prev_employeecode { get; set; }
        public string prev_employeename { get; set; }
        public string tmphandover_date { get; set; }
        public string prev_employeeid { get; set; }
        public string prev_employeedept { get; set; }
        public string prev_employeedesg { get; set; }
        public string tmphandover_status { get; set; }

    }

    public class tempadminsurrendersummary
    {
        public string asset_id { get; set; }
        public string assetserial_id { get; set; }
        public string asset_name { get; set; }
        public string issued_by { get; set; }
        public string issued_date { get; set; }
        public string asset_image { get; set; }
        public string prev_employeecode { get; set; }
        public string prev_employeename { get; set; }
        public string tmphandover_date { get; set; }
        public string prev_employeeid { get; set; }
        public string prev_employeedept { get; set; }
        public string prev_employeedesg { get; set; }
        public string tmphandover_status { get; set; }
    }
    // Surrender Click

    public class tmphandoverstatus : result
    {
        public string asset_id { get; set; }
    }

    // Holding Asset..//
    public class holdingasset : result
    {
        public string asset_id { get; set; }
    }

    // surrender To IT Admin...///

    public class surrenderitadmin : result
    {
        public string asset_id { get; set; }
    }
}
