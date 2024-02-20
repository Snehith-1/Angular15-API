using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.sdc.Models
{
    public class MdlSdcTrnLiveDeployment
    {
    }

    public class MdlLiveSummary : result
    {
        public string lspath { get; set; }
        public string lsname { get; set; }
        public List<livesummary_list> livesummary_list { get; set; }
    }
    public class livesummary_list
    {
        public string live_gid { get; set; }
        public string live_status { get; set; }
        public string liveinprogress_flag { get; set; }
        public string created_by { get; set; }
        public string deployed_by { get; set; }
        public string created_date { get; set; }
        public string livedeploy_flag { get; set; }
        public string file_description { get; set; }
        public string live_flag { get; set; }

    }
}