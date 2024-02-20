using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.asset.Models
{

        // Get Landing Page Data

        public class landingpagemodel : result
        {
            public string user_name { get; set; }
            public string employee_phote { get; set; }
            public int count_acknowledgement { get; set; }
            public int count_myasset { get; set; }
            public int count_temporaryhandover { get; set; }
            public int count_surrender { get; set; }
            public int count_tmpsurrender { get; set; }
            public int count_tmpholding { get; set; }
            public int count_response { get; set; }
            public int count_myapprovals { get; set; }
        }

    

}