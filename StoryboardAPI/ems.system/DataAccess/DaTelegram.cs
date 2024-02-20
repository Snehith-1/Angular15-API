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
    public class DaTelegram
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        int mnResult6;

        public telegramlist DaGetTelegram()
        {

           
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string requestAddressURL = "https://api.telegram.org/bot6684855132:AAGm3867vW-kITkULJWPPimqdG6TdCCqt7M/getChat?chat_id=@MYSOFTWAREDEVLEOPERGROUP";
            var clientAddress = new RestClient(requestAddressURL);
            var requestAddress = new RestRequest(Method.GET);
            IRestResponse responseAddress = clientAddress.Execute(requestAddress);
            string address_erpid = responseAddress.Content;
            string errornetsuiteJSON = responseAddress.Content;
            telegramlist objMdlTelegramMessageResponse = new telegramlist();
            objMdlTelegramMessageResponse = JsonConvert.DeserializeObject<telegramlist>(errornetsuiteJSON);

            return objMdlTelegramMessageResponse;



        }
        public void DaTelegramUpload(HttpRequest httpRequest, string user_gid, string telegram_caption, result objResult)
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
            lspath = ConfigurationManager.AppSettings["imgfile_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Telegram/post/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

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

                        lspath = ConfigurationManager.AppSettings["imgfile_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Telegram/post/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        string status;
                        status = objcmnfunctions.uploadFile(lspath + msdocument_gid, FileExtension);
                        string local_path = "E:/Angular15/AngularUI/src";
                        ms.Close();
                        lspath = "/assets/images/erpdocument" + "/" + lscompany_code + "/" + "Telegram/post/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        string final_path = local_path + lspath + msdocument_gid + FileExtension;
                        
                        if (FileExtension == ".jpg" | FileExtension == ".png" | FileExtension == ".jpeg" | FileExtension == ".gif" | FileExtension == ".JPG" | FileExtension == ".JPEG" | FileExtension == ".JIFF" | FileExtension == ".TIFF" | FileExtension == ".PNG" | FileExtension == ".GIF")
                        {

                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        var client = new RestClient("https://api.telegram.org");
                        var request = new RestRequest("/bot6684855132:AAGm3867vW-kITkULJWPPimqdG6TdCCqt7M/sendPhoto?chat_id=@MYSOFTWAREDEVLEOPERGROUP&caption=" + telegram_caption + "", Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddFile("photo", final_path);
                        IRestResponse response = client.Execute(request);


                    }


                        else if (FileExtension == ".mp4" | FileExtension == ".MP4" | FileExtension == ".avi" | FileExtension == ".mkv" | FileExtension == ".wmv" | FileExtension == ".mov" | FileExtension == ".WebM" | FileExtension == ".flv" | FileExtension == ".hevc" | FileExtension == ".vpg")

                        {

                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                            var client = new RestClient("https://api.telegram.org");
                            var request = new RestRequest("/bot6684855132:AAGm3867vW-kITkULJWPPimqdG6TdCCqt7M/sendVideo?chat_id=@MYSOFTWAREDEVLEOPERGROUP&caption=" + telegram_caption + "", Method.POST);
                            request.AlwaysMultipartFormData = true;
                            request.AddFile("video", final_path);
                            IRestResponse response = client.Execute(request);


                        }
                   


                }
                }
            }
            catch (Exception ex)
            {
                objResult.message = ex.ToString();
            }
            //return true;

        }
        public void DaTelegrammessage(string user_gid, message_list values, result objResult)
        {

            try { 
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var client = new RestClient("https://api.telegram.org");
            var request = new RestRequest("/bot6684855132:AAGm3867vW-kITkULJWPPimqdG6TdCCqt7M/sendMessage?chat_id=@MYSOFTWAREDEVLEOPERGROUP&text=" + values.telegram_caption + "", Method.POST);
            IRestResponse response = client.Execute(request);
            }
            catch (Exception ex)
            {
                objResult.message = ex.ToString();
            }
        }
    


    }
}