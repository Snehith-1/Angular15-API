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
using static OfficeOpenXml.ExcelErrorValue;
using System.Web.Http.Results;

namespace ems.system.DataAccess
{
    public class DaLinkedin
    {
        public linkedinuser_list DaGetLinkedinUser()
        {


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string requestAddressURL = "https://api.linkedin.com";
            var clientAddress = new RestClient(requestAddressURL);
            var requestAddress = new RestRequest("/v2/me", Method.GET);
            requestAddress.AddHeader("Authorization", "Bearer AQUynGDL0d98UQ3oYxWQvLaQOWTFJmcYCW7J55WBYOU2NlmUjrJiQqRG07kMv28fv757t2XOA-OytZSyZ_L0b75AgFJ3rMpv0bRjRC_jz_w1KROEp4Sw8_Ri_BoP86_-mJXnS6MtoOYFv_Bclh8fzIRlAg4I9Byg7KUz2yHxB1qj9Zb5r8MC-oOmptCNwEvV4dRXCr4LM0NQG48hsjv6SdarlpGcjpTbayJsqLDNo3eDHpHjfZR2bMGdu29rU3gYSPKwHsbQRGlmnSepGmhiGmvQxQ6IZctl7PpZpBtQJ500GtmWndMcKsXWfNmz24oCXmMDk377tHm7yKgCdXHNSXUxAuHMYA");
            requestAddress.AddHeader("Cookie", "lidc=\"b=OB41:s=O:r=O:a=O:p=O:g=4052:u=107:x=1:i=1690447109:t=1690514997:v=2:sig=AQHdvTTL2KKWVCBy0Wncp8_j0L_rhNGZ\"; bcookie=\"v=2&2dfb1a02-6afe-483b-8fe4-3538d39f83b8\"; lidc=\"b=OB71:s=O:r=O:a=O:p=O:g=2953:u=1:x=1:i=1690446031:t=1690532431:v=2:sig=AQHM4bLW4V2UCpCly3mhe6XtJ27eHCS-\"; lidc=\"b=OB41:s=O:r=O:a=O:p=O:g=4054:u=112:x=1:i=1690606086:t=1690614903:v=2:sig=AQEGtL9D6zi1XMv_X3rNOLayasKBbOud\"; bcookie=\"v=2&2dfb1a02-6afe-483b-8fe4-3538d39f83b8\"");
            IRestResponse responseAddress = clientAddress.Execute(requestAddress);
            string address_erpid = responseAddress.Content;
            string errornetsuiteJSON = responseAddress.Content;
            linkedinuser_list objMdlLinkedinMessageResponse = new linkedinuser_list();
            objMdlLinkedinMessageResponse = JsonConvert.DeserializeObject<linkedinuser_list>(errornetsuiteJSON);

            return objMdlLinkedinMessageResponse;



        }

        public linkedinprofile_list DaGetLinkedinProfile()
        {


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string requestAddressURL = "https://api.linkedin.com";
            var clientAddress = new RestClient(requestAddressURL);
            var requestAddress = new RestRequest("/v2/me?projection=(id%2CprofilePicture(displayImage~digitalmediaAsset%3AplayableStreams))", Method.GET);
            requestAddress.AddHeader("Authorization", "Bearer AQUynGDL0d98UQ3oYxWQvLaQOWTFJmcYCW7J55WBYOU2NlmUjrJiQqRG07kMv28fv757t2XOA-OytZSyZ_L0b75AgFJ3rMpv0bRjRC_jz_w1KROEp4Sw8_Ri_BoP86_-mJXnS6MtoOYFv_Bclh8fzIRlAg4I9Byg7KUz2yHxB1qj9Zb5r8MC-oOmptCNwEvV4dRXCr4LM0NQG48hsjv6SdarlpGcjpTbayJsqLDNo3eDHpHjfZR2bMGdu29rU3gYSPKwHsbQRGlmnSepGmhiGmvQxQ6IZctl7PpZpBtQJ500GtmWndMcKsXWfNmz24oCXmMDk377tHm7yKgCdXHNSXUxAuHMYA");
            requestAddress.AddHeader("Cookie", "lidc=\"b=OB41:s=O:r=O:a=O:p=O:g=4052:u=107:x=1:i=1690447109:t=1690514997:v=2:sig=AQHdvTTL2KKWVCBy0Wncp8_j0L_rhNGZ\"; bcookie=\"v=2&2dfb1a02-6afe-483b-8fe4-3538d39f83b8\"; lidc=\"b=OB71:s=O:r=O:a=O:p=O:g=2953:u=1:x=1:i=1690446031:t=1690532431:v=2:sig=AQHM4bLW4V2UCpCly3mhe6XtJ27eHCS-\"; lidc=\"b=OB41:s=O:r=O:a=O:p=O:g=4054:u=112:x=1:i=1690606086:t=1690614903:v=2:sig=AQEGtL9D6zi1XMv_X3rNOLayasKBbOud\"; bcookie=\"v=2&2dfb1a02-6afe-483b-8fe4-3538d39f83b8\"");
            IRestResponse responseAddress = clientAddress.Execute(requestAddress);
            string address_erpid = responseAddress.Content;
            string errornetsuiteJSON = responseAddress.Content;
            linkedinprofile_list objMdlLinkedinprofileMessageResponse = new linkedinprofile_list();
            objMdlLinkedinprofileMessageResponse = JsonConvert.DeserializeObject<linkedinprofile_list>(errornetsuiteJSON);
            //string profile = objMdlLinkedinprofileMessageResponse.profilePicture.displayImageObj.elements[0].identifiers[0].identifier;
            return objMdlLinkedinprofileMessageResponse;



        }
        public void DaPostlinkedin(string user_gid, post_list values, result objResult)
        {

            try
            {
                 string body_content= values.body_content;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var client = new RestClient("https://api.linkedin.com");
                var request = new RestRequest("/v2/ugcPosts", Method.POST);
                request.AddHeader("Content-Type", "text/plain");
                request.AddHeader("Authorization", "Bearer AQUynGDL0d98UQ3oYxWQvLaQOWTFJmcYCW7J55WBYOU2NlmUjrJiQqRG07kMv28fv757t2XOA-OytZSyZ_L0b75AgFJ3rMpv0bRjRC_jz_w1KROEp4Sw8_Ri_BoP86_-mJXnS6MtoOYFv_Bclh8fzIRlAg4I9Byg7KUz2yHxB1qj9Zb5r8MC-oOmptCNwEvV4dRXCr4LM0NQG48hsjv6SdarlpGcjpTbayJsqLDNo3eDHpHjfZR2bMGdu29rU3gYSPKwHsbQRGlmnSepGmhiGmvQxQ6IZctl7PpZpBtQJ500GtmWndMcKsXWfNmz24oCXmMDk377tHm7yKgCdXHNSXUxAuHMYA");
                request.AddHeader("Cookie", "lidc=\"b=OB41:s=O:r=O:a=O:p=O:g=4052:u=107:x=1:i=1690447763:t=1690514997:v=2:sig=AQF53ungx7Xv2SchfUH-1JuVmM1gXMBE\"; bcookie=\"v=2&2dfb1a02-6afe-483b-8fe4-3538d39f83b8\"; lidc=\"b=OB71:s=O:r=O:a=O:p=O:g=2953:u=1:x=1:i=1690446031:t=1690532431:v=2:sig=AQHM4bLW4V2UCpCly3mhe6XtJ27eHCS-\"; lidc=\"b=OB41:s=O:r=O:a=O:p=O:g=4055:u=112:x=1:i=1690792969:t=1690855311:v=2:sig=AQH031-Gy41KdS1Cd5hwNo4243VbJC-_\"; bcookie=\"v=2&2dfb1a02-6afe-483b-8fe4-3538d39f83b8\"");
                //             var body = @"{" + "\n" +
                //  @"    ""author"": ""urn:li:person:_dZQknI6ZQ""," + "\n" +
                //  @"    ""lifecycleState"": ""PUBLISHED""," + "\n" +
                //  @"    ""specificContent"": {" + "\n" +
                //  @"        ""com.linkedin.ugc.sharecontent"": {" + "\n" +
                //  @"            ""shareCommentary"": {" + "\n" +
                // //@"                ""text"": ""Welcome to Linkedin API!""" + "\n" +
                //// @"                ""text"": "" '" + "" + "\n" +
                // @"""text"": """ + body_content +  "\n" +
                //  //@"                ""text"": ""Welcome to Linkedin API!""" + "\n" +
                //  //@"                ""text"": ""'" + values.body_content + "'" + "\n" +                    
                //  @"            }," + "\n" +
                //  @"            ""shareMediaCategory"": ""NONE""" + "\n" +
                //  @"        }" + "\n" +
                //  @"    }," + "\n" +
                //  @"    ""visibility"": {" + "\n" +
                //  @"        ""com.linkedin.ugc.MemberNetworkVisibility"": ""PUBLIC""" + "\n" +
                //  @"    }" + "\n" +
                //  @"}";

                var body = "{\"author\":\"urn:li:person:_dZQknI6ZQ\",\"lifecycleState\":\"PUBLISHED\",\"specificContent\":{\"com.linkedin.ugc.ShareContent\":{\"shareCommentary\":{\"text\":" + "\"" + body_content + "\"" + "},\"shareMediaCategory\":\"NONE\"}},\"visibility\":{\"com.linkedin.ugc.MemberNetworkVisibility\":\"PUBLIC\"}}";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);





            }
            catch (Exception ex)
            {
                objResult.message = ex.ToString();
            }
        }
    }
}