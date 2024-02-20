﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.system.Models
{
    public class MdlFacebook : result
    {
        public List<facebooklist> facebooklist { get; set; }
    }
    //API Models


    public class facebooklist
    {
        public string category { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public Age_Range age_range { get; set; }
        public string birthday { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string gender { get; set; }
        public Hometown hometown { get; set; }
        public string last_name { get; set; }
        public Location location { get; set; }
        public Friends friends { get; set; }
        public Likes likes { get; set; }
        public Picture picture { get; set; }
        public Posts posts { get; set; }
        public string phone { get; set; }

        public string link { get; set; }
  
        public int followers_count { get; set; }
        public Cover cover { get; set; }
    
        public Videos videos { get; set; }
    

    }
    public class Cover
    {
        public string cover_id { get; set; }
        public int offset_x { get; set; }
        public int offset_y { get; set; }
        public string source { get; set; }
        public string id { get; set; }
    }


    public class Age_Range
    {
        public int min { get; set; }
    }

    public class Hometown
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Location
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Friends
    {
        public object[] data { get; set; }
        public Summary summary { get; set; }
    }

    public class Summary
    {
        public int total_count { get; set; }
    }

    public class Likes
    {
        public Datum[] data { get; set; }
        public Paging paging { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
        public string next { get; set; }
    }

    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Datum
    {
        public string[] emails { get; set; }
        public string link { get; set; }
        public string id { get; set; }
        public DateTime created_time { get; set; }
        public int views { get; set; }
        public string permalink_url { get; set; }
        public string source { get; set; }
        public Comments comments { get; set; }
    }

    public class Picture
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public int height { get; set; }
        public bool is_silhouette { get; set; }
        public string url { get; set; }
        public int width { get; set; }

    }

    public class Posts
    {
        public Datum1[] data { get; set; }
        public Paging1 paging { get; set; }
    }

    public class Paging1
    {
        public string previous { get; set; }
        public string next { get; set; }
        public Cursors1 cursors { get; set; }
    }

    public class Datum1
    {
        public string link { get; set; }
        public string full_picture { get; set; }
        public DateTime created_time { get; set; }
        public string id { get; set; }

        public From from { get; set; }
        public string message { get; set; }

    }





    public class Videos
    {
        public Datum[] data { get; set; }
        public Paging paging { get; set; }
    }





    public class Comments
    {
        public Datum1[] data { get; set; }
        public Paging1 paging { get; set; }
    }



    public class Cursors1
    {
        public string before { get; set; }
        public string after { get; set; }
    }



    public class From
    {
        public string name { get; set; }
        public string id { get; set; }
    }


    public class Paging2
    {
        public Cursors2 cursors { get; set; }
        public string next { get; set; }
    }

    public class Cursors2
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Datum2
    {
        public string full_picture { get; set; }
        public string permalink_url { get; set; }
        public DateTime created_time { get; set; }
        public string id { get; set; }
    }


   
  


}