using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.asset.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    //----------------------Acknowledgement--------------------------------------//

    // Acknowledgement Data By Employee ID

    public class acknowledgementasset : result
    //Declaration
    {
        public List<acksummary> acksummary { get; set; }
    }

    // Definition
    public class acksummary
    {
        public string asset2custodian_gid { get; set; }
        public string asset_id { get; set; }
        public string assetserial_id { get; set; }
        public string asset_name { get; set; }
        public string issued_by { get; set; }
        public string issued_date { get; set; }
        public string asset_image { get; set; }
        public string acknowledge_flag { get; set; }
    }

    // Acknowledgement Click 

    public class ackstatus : result
    {
        public string asset2custodian_gid { get; set; }
        public string asset_id { get; set; }
    }

    // Reject Acknowledgement Click 

    public class ackrejectstatus : result
    {
        public string asset2custodian_gid { get; set; }
        public string asset_id { get; set; }
        public string reason_reject { get; set; }
    }
}