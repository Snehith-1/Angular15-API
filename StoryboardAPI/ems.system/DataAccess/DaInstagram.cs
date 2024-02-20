﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.system.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
using System.Web;
using OfficeOpenXml;
using System.Configuration;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using RestSharp;
using Newtonsoft.Json;
namespace ems.system.DataAccess
{
    public class DaInstagram
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        int mnResult6;

        public instagramlist DaGetInstagram()
        {


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string requestAddressURL = "https://graph.instagram.com/me/media?fields=id%2Cusername&access_token=IGQVJVYkJLTUxSWTZAQZAFBSU1pSM2prQW1LcUZAyNUdlRWx1LTZAkWHFmLWhRVzBWTnlOMnMxeGVuRWZAiTUhkbnQ2S0J6VDM4WEVPZATd5V1Jhd29PY3BnYWZA0clFpR1FuczhjNVdXZAkhuTTQza1RDM3VvRQZDZD";
            var clientAddress = new RestClient(requestAddressURL);
            var requestAddress = new RestRequest(Method.GET);
            IRestResponse responseAddress = clientAddress.Execute(requestAddress);
            string address_erpid = responseAddress.Content;       
            string errornetsuiteJSON = responseAddress.Content;
            instagramlist objMdlInstagramMessageResponse = new instagramlist();
            objMdlInstagramMessageResponse = JsonConvert.DeserializeObject<instagramlist>(errornetsuiteJSON);
            return objMdlInstagramMessageResponse;



        }
        public instagramprofile1_list DaGetInstagramProfile()
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string requestAddressURL = "https://graph.instagram.com/me/media?fields=media_type,media_url&access_token=IGQVJVYkJLTUxSWTZAQZAFBSU1pSM2prQW1LcUZAyNUdlRWx1LTZAkWHFmLWhRVzBWTnlOMnMxeGVuRWZAiTUhkbnQ2S0J6VDM4WEVPZATd5V1Jhd29PY3BnYWZA0clFpR1FuczhjNVdXZAkhuTTQza1RDM3VvRQZDZD";
            var clientAddress = new RestClient(requestAddressURL);
            var requestAddress = new RestRequest(Method.GET);
            IRestResponse responseAddress = clientAddress.Execute(requestAddress);
            string address_erpid = responseAddress.Content;
            string errornetsuiteJSON = responseAddress.Content;
            instagramprofile1_list objinstagramprofile1_list = new instagramprofile1_list();
            instagramprofile_list objMdlInstagramMessageResponse = new instagramprofile_list();
            objMdlInstagramMessageResponse = JsonConvert.DeserializeObject<instagramprofile_list>(errornetsuiteJSON);
            var get_picturelist = new List<pictureList>();
            var get_videoList = new List<videoList>();
            foreach (var item in objMdlInstagramMessageResponse.data)
            {
                if(item.media_type == "IMAGE")
                {
                    get_picturelist.Add(new pictureList
                    {
                        media_url = item.media_url
                    });
                }
                else
                {
                    get_videoList.Add(new videoList
                    {
                        media_url = item.media_url
                    });
                }
            }
            objinstagramprofile1_list.data = get_picturelist;
            objinstagramprofile1_list.videoData = get_videoList;

            return objinstagramprofile1_list;

        }
    }
}