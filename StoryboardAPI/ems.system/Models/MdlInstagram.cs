﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.system.Models
{

    public class MdlInstagram : result
    {
        public List<instagramlist> instagramlist { get; set; }
    }




    public class instagramlist
    {
        public List<data1> data { get; set; }
    }

    public class data1
    {
        public string id { get; set; }
        public string username { get; set; }


    }


    //public class instagramprofile_list
    //{
    //    public List<Profilepictures> data { get; set; }
    //    public string media_type { get; set; }
    //}




    //public class Profilepictures
    //{
    //    public string media_url { get; set; }
    //    [JsonProperty("media_urls")]
    //    public Displayimage4 displayImageObj { get; set; }


    //}
    //public class Profilepictures
    //{
    //    public string media_url { get; set; }
    //}
    public class instagramprofile_list
    {
        public List<List> data { get; set; }
    }
    public class List
    {
        public string media_type { get; set; }
        public string media_url { get; set; }
    }

    public class instagramprofile1_list
    {
        public List<pictureList> data { get; set; }
        public List<videoList> videoData { get; set; }
    }

    public class pictureList
    {
        public string media_url { get; set; }
    }

    public class videoList
    {
        public string media_url { get; set; }
    }

}
