using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoryboardAPI.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<sys_menus> menu_lists { get; set; }
    }
    public class token
    {
        public string token_type { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public int ext_expires_in { get; set; }
        public string access_token { get; set; }
    }

    public class Rootobject
    {
        public string odatacontext { get; set; }
        public object businessPhones { get; set; }
        public string displayName { get; set; }
        public string givenName { get; set; }
        public object jobTitle { get; set; }
        public string mail { get; set; }
        public object mobilePhone { get; set; }
        public object officeLocation { get; set; }
        public object preferredLanguage { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
        public string id { get; set; }
    }

    public class userlog : result
    {
        public List<userloglist> userloglist { get; set; }
    }

    public class userloglist
    {
        public string businessPhones { get; set; }
        public string displayName { get; set; }
        public string givenName { get; set; }
        public string jobTitle { get; set; }
        public string mail { get; set; }
        public string mobilePhone { get; set; }
        public string officeLocation { get; set; }
        public string preferredLanguage { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
        public string id { get; set; }
    }


    public class loginresponse
    {
        public string token { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string user_gid { get; set; }
        public string username { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        public string branch { get; set; }
        public string usercode { get; set; }
        public string departmentname { get; set; }
        public string employee_photo { get; set; }
        public string dashboard_flag { get; set; }
        
    }
    public class logininput
    {
        public string code { get; set; }
    }
    public class userlogininput
    {
        public string hostname { get; set; }
        public string company_code { get; set; }
        public string user_code { get; set; }
        public string user_password { get; set; }
        public string lawyer_email { get; set; }
        public List<sys_menus> menu_lists { get; internal set; }
    }

  
    public class sys_menus
    {
        //public string menu_code { get; set; }
        public string text { get; set; }
        public string sref { get; set; }
        public string icon { get; set; }
        public string label { get; set; }
        public string privilege { get; set; }
        public string menu_indication { get; set; }
        public string menu_indication1 { get; set; }
        public List<sys_submenu> submenu { get; set; }
    }
    public class loginERPinput
    {
        public string user_code { get; set; }
        public string company_code { get; set; }
    }

    public class loginVendorInput
    {
        public string user_code { get; set; }
        public string pass_word { get; set; }
    }

    public class appVendorInput
    {
        public string app_code { get; set; }
        public string password { get; set; }
    }
    //public class Mdladminlogin : result
    //{
    //    public string user_code { get; set; }
    //    public string user_password { get; set; }
    //    public string company_code { get; set; }
    //}
    public class PostUserLogin : result
    {
        public string user_code { get; set; }
        public string user_password { get; set; }
    }
    public class MdlMail
    {
        public string name { get; set; }
        public string mobile_no { get; set; }
        public string mail { get; set; }
        public string company_name { get; set; }
        public string location { get; set; }
        public string gst_no { get; set; }
        public string queries { get; set; }
    }

    public class einvoiceResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
    }


}