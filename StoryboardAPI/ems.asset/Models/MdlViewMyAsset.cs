using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ems.asset.Models
{
    //------------------------My Assets-----------------------------//

    public class myassets : result
    //Declaration
    {
        public List<myassetsummary> myassetsummary { get; set; }

    }

    // Definition
    public class myassetsummary
    {
        public string asset_id { get; set; }
        public string assetserial_id { get; set; }
        public string asset_name { get; set; }
        public string issued_by { get; set; }
        public string issued_date { get; set; }
        public string asset_image { get; set; }
        public string acknowledge_date { get; set; }
    }

}
