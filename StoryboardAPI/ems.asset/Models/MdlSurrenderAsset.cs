using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace ems.asset.Models
{
    //-------------------Surrender Assets----------------------------//

    public class surrenderassets : result
    //Declaration
    {
        public List<surrendersummary> surrendersummary { get; set; }
    }

    // Definition
    public class surrendersummary
    {
        public string asset2custodian_gid { get; set; }
        public string asset_id { get; set; }
        public string assetserial_id { get; set; }
        public string asset_name { get; set; }
        public string issued_by { get; set; }
        public string issued_date { get; set; }
        public string asset_image { get; set; }
        public string surrender_status { get; set; }
    }

    // Surrender Click

    public class surrenderstatus : result
    {
        public string asset2custodian_gid { get; set; }
        public string asset_id { get; set; }
    }
}
