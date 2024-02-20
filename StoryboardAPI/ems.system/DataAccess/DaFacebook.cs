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
    public class DaFacebook
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        int mnResult6;

        public facebooklist DaGetFacebook()
        {

            //var client = new RestClient("https://graph.facebook.com");
            //var request = new RestRequest("/v17.0/me?fields=id%2Cname%2Cabout%2Cage_range%2Cbirthday%2Ceducation%2Cemail%2Cfirst_name%2Cgender%2Chometown%2Clast_name%2Clocation%2Crelationship_status%2Cfriends%2Clikes%7Bemails%2Clink%2Ccreated_time%7D%2Cpicture%2Cposts%7Blink%2Cfull_picture%2Ccreated_time%7D%2Cevents%2Cgroups&access_token=EAALVntBtuioBAPY4qza7LIA2ZAzQQ4HVBzsqMVCrgweHKFLUj8jBymt3b2emLKcyVwosChfRfrDDDsJQlAFzLcvfOLQYBWqdIRAek9OZCPYawIL2LBFgYbrW10U3YYS4gMy8gO5EIEgZArcCGiCTCGcqruFlVAVHKcMn1O9knJDJ5Q5K9PjBQvXhiXNTrYA01eGTMldd3bDReVumUo5Dw6snNZAex0mN7r4pY6BpwpUgPKtNHevb", Method.GET);
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string requestAddressURL = "https://graph.facebook.com/v17.0/me?fields=id%2Cname%2Cabout%2Cage_range%2Cbirthday%2Ceducation%2Cemail%2Cfirst_name%2Cgender%2Chometown%2Clast_name%2Clocation%2Crelationship_status%2Cfriends%2Clikes%7Bemails%2Clink%2Ccreated_time%7D%2Cpicture%2Cposts%7Blink%2Cfull_picture%2Ccreated_time%7D%2Cevents%2Cgroups&access_token=EAAOQNwSSuIABOZBPl7JvA2xLonEOMb4pEXmP1pXDbfmv5PjnlyTDKrWIL0IyWxfYYltu1DKGxuFOuzpO2nSW440ZCvG2nezGF5F3mbvtvZBpcRvgjBKgVoUt2usZBQwjwin0TNipUYR2omNVoejCGvTdUt55IzCFwchUlpV8BvXRpPNdtMAaANr6";
            var clientAddress = new RestClient(requestAddressURL);
            var requestAddress = new RestRequest(Method.GET);
            IRestResponse responseAddress = clientAddress.Execute(requestAddress);
            string address_erpid = responseAddress.Content;
            string errornetsuiteJSON = responseAddress.Content;
            facebooklist objMdlFacebookMessageResponse = new facebooklist();
            objMdlFacebookMessageResponse = JsonConvert.DeserializeObject<facebooklist>(errornetsuiteJSON);

            return objMdlFacebookMessageResponse;



        }
        public facebooklist DaGetPagedetails()
        {


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string requestAddressURL = "https://graph.facebook.com/v17.0/me?fields=id%2Cname%2Cfollowers_count%2Ccover%2Clink%2Cpicture%2Cvideos%7Bcomments%2Cviews%2Cid%2Cpermalink_url%2Csource%7D%2Cphone%2Ccategory%7Bcomments%2Cviews%2Cid%2Cpermalink_url%2Csource%7D%2Cposts%7Bfull_picture%2Cpermalink_url%2Ccreated_time%7D&access_token=EAAI2sEM1Y0cBO3ZBG3I6BElD9WoYZCaYSWDdzDPcXYZBrdUKDtmT8Tb4CsReT3kcrfsxYdLnS26M8L3QFIPM1kc4ah0pFnku9BfiLopcLQUOxOWItlHFTgih7edznEDBgd5um7xRxZCZCOL2OOqu16eCdB1UP5oZCA7KtyqqaDhpJzNEMyDazGpv1NuVrwTlUZD";
            var clientAddress = new RestClient(requestAddressURL);
            var requestAddress = new RestRequest(Method.GET);
            IRestResponse responseAddress = clientAddress.Execute(requestAddress);
            string address_erpid = responseAddress.Content;
            string errornetsuiteJSON = responseAddress.Content;
            facebooklist objMdlFacebookMessageResponse = new facebooklist();
            objMdlFacebookMessageResponse = JsonConvert.DeserializeObject<facebooklist>(errornetsuiteJSON);

            return objMdlFacebookMessageResponse;



        }
        public void DaUploadImage(HttpRequest httpRequest, string user_gid, string image_caption, result objResult)
        {
            //uploadimageproductcategorycreationSummary objdocumentmodel = new uploadimageproductcategorycreationSummary();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            HttpPostedFile httpPostedFile;

            string lspath;
            string msGetGid;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            MemoryStream ms = new MemoryStream();
            lspath = ConfigurationManager.AppSettings["imgfile_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Facebook/Images post/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        string lscompany_document_flag = string.Empty;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        lspath = ConfigurationManager.AppSettings["imgfile_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Facebook/Images post/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        string status;
                        status = objcmnfunctions.uploadFile(lspath + msdocument_gid, FileExtension);
                        string local_path = "D:web/Angular/AngularUI/src/assets/images/";
                   
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Facebook/Images post/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        string final_path = local_path + lspath + msdocument_gid + FileExtension;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        var client = new RestClient("https://graph.facebook.com");
                        var request = new RestRequest("/109598978875297/photos", Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("access_token", "EAAI2sEM1Y0cBO3ZBG3I6BElD9WoYZCaYSWDdzDPcXYZBrdUKDtmT8Tb4CsReT3kcrfsxYdLnS26M8L3QFIPM1kc4ah0pFnku9BfiLopcLQUOxOWItlHFTgih7edznEDBgd5um7xRxZCZCOL2OOqu16eCdB1UP5oZCA7KtyqqaDhpJzNEMyDazGpv1NuVrwTlUZD");
                        IRestRequest restRequest = request.AddFile("source", final_path);
                        request.AddParameter("message", image_caption);
                        IRestResponse response = client.Execute(request);

                    }
                }
            }
            catch (Exception ex)
            {
                objResult.message = ex.ToString();
            }
            //return true;

        }

        public void DaUploadVideo(HttpRequest httpRequest, string user_gid, string video_caption, result objResult)
        {
            //uploadimageproductcategorycreationSummary objdocumentmodel = new uploadimageproductcategorycreationSummary();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            HttpPostedFile httpPostedFile;

            string lspath;
            string msGetGid;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            MemoryStream ms = new MemoryStream();
            lspath = ConfigurationManager.AppSettings["imgfile_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Facebook/Videos post/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        string lscompany_document_flag = string.Empty;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        lspath = ConfigurationManager.AppSettings["imgfile_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Facebook/Videos post/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        string status;
                        status = objcmnfunctions.uploadFile(lspath + msdocument_gid, FileExtension);
                        string local_path = "D:web/Angular/AngularUI/src";
                        ms.Close();
                        lspath = "/assets/images/erpdocument" + "/" + lscompany_code + "/" + "Facebook/Videos post/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        string final_path = local_path + lspath + msdocument_gid + FileExtension;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        var client = new RestClient("https://graph.facebook.com");
                        var request = new RestRequest("/109598978875297/videos", Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("access_token", "EAAI2sEM1Y0cBO3ZBG3I6BElD9WoYZCaYSWDdzDPcXYZBrdUKDtmT8Tb4CsReT3kcrfsxYdLnS26M8L3QFIPM1kc4ah0pFnku9BfiLopcLQUOxOWItlHFTgih7edznEDBgd5um7xRxZCZCOL2OOqu16eCdB1UP5oZCA7KtyqqaDhpJzNEMyDazGpv1NuVrwTlUZD");
                        IRestRequest restRequest = request.AddFile("source", final_path);
                        request.AddParameter("message", video_caption);
                        IRestResponse response = client.Execute(request);

                    }
                }
            }
            catch (Exception ex)
            {
                objResult.message = ex.ToString();
            }
            //return true;

        }
    }
}