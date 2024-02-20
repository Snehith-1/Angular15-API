using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using StoryboardAPI.Models;
using StoryboardAPI.Authorization;
using System.Data;
using System.Data.Odbc;
using Newtonsoft.Json;
using RestSharp;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spire.Pdf;
using System.Net.Mail;

namespace StoryboardAPI.Controllers
{
    [RoutePrefix("api/Login")]
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        dbconn objdbconn = new dbconn();
        OdbcDataReader objODBCdatareader;
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        int mnResult;
        string user_status;
        string vendoruser_status;
        string tokenvalue = string.Empty;
        string user_gid = string.Empty;
        string employee_gid = string.Empty;
        string department_gid = string.Empty;
        string password = string.Empty;
        string username = string.Empty;
        string departmentname = string.Empty;
        string dashboard_flag = string.Empty;
        string lscompany_code;
        string domain = string.Empty;
        DataTable dt_levelone, dt_leveltwo, dt_levelthree;
        string menu_ind_up_first = string.Empty;
        string menu_ind_down_first = string.Empty;
        string menu_ind_up_second = string.Empty;
        string menu_ind_down_second = string.Empty;

        // Validate Token

        [AllowAnonymous]
        [ActionName("LoginReturn")]
        [HttpPost]
        public HttpResponseMessage GetLoginReturn(logininput values)
        {
            var url = ConfigurationManager.AppSettings["host"];
            if (url == ConfigurationManager.AppSettings["livedomain_url"].ToString())
            {
                var getSpireDocLicense = ConfigurationManager.AppSettings["SpireDocLicenseKey"];
                Spire.License.LicenseProvider.SetLicenseKey(getSpireDocLicense);
            } 

            loginresponse GetLoginResponse = new loginresponse();
            string code = values.code; 
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;  
            var client = new RestSharp.RestClient("https://login.microsoftonline.com/655a0e0e-4a74-4a0c-86d8-370a992e90a6/oauth2/v2.0/token");
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("client_id", ConfigurationManager.AppSettings["client_id"]);
            request.AddParameter("code", code);
            request.AddParameter("scope", "https://graph.microsoft.com/User.Read");
            request.AddParameter("client_secret", ConfigurationManager.AppSettings["client_secret"]);
            request.AddParameter("redirect_uri", ConfigurationManager.AppSettings["redirect_url"]);
            request.AddParameter("grant_type", "authorization_code");
            IRestResponse response = client.Execute(request);
            token json = JsonConvert.DeserializeObject<token>(response.Content);

            var client1 = new RestSharp.RestClient("https://graph.microsoft.com/v1.0/me");
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request1 = new RestRequest(Method.GET);
            request1.AddHeader("Authorization", "Bearer " + json.access_token);
            IRestResponse response1 = client1.Execute(request1);
            Rootobject json1 = JsonConvert.DeserializeObject<Rootobject>(response1.Content); 
            if (json1.userPrincipalName != null && json1.userPrincipalName != "")
            {

                msSQL = " SELECT b.user_gid,a.department_gid, a.employee_gid, user_password, user_code, concat(user_firstname, ' ', user_lastname) as username FROM hrm_mst_temployee a " +
                        " INNER JOIN adm_mst_tuser b on b.user_gid = a.user_gid " +
                        " WHERE employee_emailid = '" + json1.userPrincipalName + "'";
                objODBCdatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCdatareader.HasRows == true)
                {

                    objODBCdatareader.Read();
                    var tokenresponse = Token(objODBCdatareader["user_code"].ToString(), objODBCdatareader["user_password"].ToString());
                    dynamic newobj = Newtonsoft.Json.JsonConvert.DeserializeObject(tokenresponse);
                    tokenvalue = newobj.access_token;
                    employee_gid = objODBCdatareader["employee_gid"].ToString();
                    user_gid = objODBCdatareader["user_gid"].ToString();
                    department_gid = objODBCdatareader["department_gid"].ToString();
                    GetLoginResponse.username = objODBCdatareader["username"].ToString();
                    objODBCdatareader.Close();
                }

                msSQL = " INSERT INTO adm_mst_ttoken ( " +
                         " token, " +
                         " employee_gid, " +
                         " user_gid, " +
                         " department_gid, " +
                         " company_code " +
                         " )VALUES( " +
                         " 'Bearer " + tokenvalue + "'," +
                         " '" + employee_gid + "'," +
                         " '" + user_gid + "'," +
                         " '" + department_gid + "'," +
                         " '" + ConfigurationManager.AppSettings["company_code"] + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                GetLoginResponse.status = true;
                GetLoginResponse.message = "";
                GetLoginResponse.token = "Bearer " + tokenvalue;
                GetLoginResponse.user_gid = user_gid;

            }
            else
            {
                GetLoginResponse.user_gid = null; 
            }
            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
        }

        public void LogForAudit(string strVal)
        {
            try
            {
                string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erp_documents/SSOLOG/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);

                lspath = lspath + @"\" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath, true);
                sw.WriteLine(strVal);
                sw.Close();
            }
            catch (Exception ex)
            {
            }
        }


        [AllowAnonymous]
        [ActionName("userLogin")]
        [HttpPost]
        public HttpResponseMessage GetUserLoginReturn(userlogininput values)
        {
            var getmenu = new List<sys_menus>();

            loginresponse GetLoginResponse = new loginresponse();
            try
            {
               
                var username = string.Empty;

                msSQL = " SELECT b.user_gid,a.department_gid,a.employee_gid, user_password, " +
                       " user_code, concat(user_firstname, ' ', user_lastname) as username, " +
                       " concat(c.department_code, '/', c.department_name) as departmentname,b.dashboard_flag FROM hrm_mst_temployee a " +
                       " INNER JOIN adm_mst_tuser b on b.user_gid = a.user_gid " +
                       " inner join hrm_mst_tdepartment c on a.department_gid = c.department_gid " +
                       " WHERE user_code = '" + values.user_code + "' and user_password = '" + objcmnfunctions.ConvertToAscii(values.user_password) + "'";
                objODBCdatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCdatareader.HasRows == true)
                {
                    objODBCdatareader.Read();
                    domain = Request.RequestUri.Host.ToLower();
                    var tokenresponse = Token(values.user_code, objcmnfunctions.ConvertToAscii(values.user_password), ConfigurationManager.AppSettings[domain].ToString() );
                    dynamic newobj = Newtonsoft.Json.JsonConvert.DeserializeObject(tokenresponse);
                    tokenvalue = newobj.access_token;
                    employee_gid = objODBCdatareader["employee_gid"].ToString();
                    user_gid = objODBCdatareader["user_gid"].ToString();
                    username = objODBCdatareader["username"].ToString();
                    department_gid = objODBCdatareader["department_gid"].ToString();
                    departmentname = objODBCdatareader["departmentname"].ToString();
                    dashboard_flag = objODBCdatareader["dashboard_flag"].ToString();
                    objODBCdatareader.Close();
                }
                if (user_gid !="" && values.company_code == ConfigurationManager.AppSettings["company_name"]) {
                    msSQL = " DELETE FROM adm_trn_tconsumertoken " +
                          " WHERE company_code = '" + ConfigurationManager.AppSettings["localhost"] + "' " +
                          " AND user_Code = '" + values.user_code + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = " SELECT user_gid FROM " + ConfigurationManager.AppSettings["localhost"] + ".adm_mst_tuser WHERE user_code='" + values.user_code + "' ";
                    string user_gid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " DELETE FROM adm_mst_ttoken " +
                        " WHERE company_code = '" + ConfigurationManager.AppSettings["localhost"] + "' " +
                        " AND user_gid = '" + user_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = " INSERT INTO adm_mst_ttoken ( " +
                         " token, " +
                         " employee_gid, " +
                         " user_gid, " +
                         " department_gid, " +
                         " company_code " +
                         " )VALUES( " +
                         " 'Bearer " + tokenvalue + "'," +
                         " '" + employee_gid + "'," +
                         " '" + user_gid + "'," +
                         " '" + department_gid + "'," +
                         " '" + ConfigurationManager.AppSettings["company_code"] + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                GetLoginResponse.status = true;
                GetLoginResponse.message = "";
                GetLoginResponse.token = "Bearer " + tokenvalue;
                GetLoginResponse.username = username;
                GetLoginResponse.user_gid = user_gid;
                GetLoginResponse.departmentname = departmentname; 
                GetLoginResponse.dashboard_flag = dashboard_flag;
                    if (ConfigurationManager.AppSettings["company_code"] != null && ConfigurationManager.AppSettings["company_code"] != "")

                    {
                        msSQL = " INSERT INTO adm_trn_tconsumertoken ( " +
                                                " token, " +
                                                " user_code, " +
                                                " connection_string, " +
                                                " company_code " +
                                                " )VALUES( " +
                                                " 'Bearer " + tokenvalue + "'," +
                                                " '" + values.user_code + "'," +
                                                " '" + ConfigurationManager.ConnectionStrings["AuthConn"].ToString() + "'," +
                                                " '" + ConfigurationManager.AppSettings["company_code"] + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {
                    tokenvalue = "null";
                    GetLoginResponse.status = false;
                    
                }
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = ex.ToString();
            }
            finally
            {
                //msSQL = " SELECT a.module_gid,b.module_name,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                //            " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' AND menu_level=1 AND b.lw_flag='Y' order by b.display_order asc";
                //dt_levelone = objdbconn.GetDataTable(msSQL);
                //if (dt_levelone.Rows.Count != 0)
                //{
                //    foreach (DataRow drOne in dt_levelone.Rows)
                //    {


                //        msSQL = " SELECT a.module_gid,b.module_name,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                //                " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' " +
                //                " AND b.menu_level = 2 AND b.module_gid like '" + drOne["module_gid"].ToString() + "%' AND b.lw_flag='Y' order by b.display_order asc";
                //        dt_leveltwo = objdbconn.GetDataTable(msSQL);
                //        var getmenu2 = new List<sys_submenu>();
                //        if (dt_leveltwo.Rows.Count != 0)
                //        {
                //            //menu_ind_up_first = "fa fa-angle-up";
                //            //menu_ind_down_first = "fa fa-angle-down";

                //            foreach (DataRow drTwo in dt_leveltwo.Rows)
                //            {

                //                msSQL = " SELECT a.module_gid,b.module_name,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                //                        " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' " +
                //                        " AND b.menu_level = 3 AND b.module_gid like '" + drTwo["module_gid"].ToString() + "%' AND b.lw_flag='Y' order by b.display_order asc ";
                //                dt_levelthree = objdbconn.GetDataTable(msSQL);
                //                var Get_Summary = new List<sys_sub1menu>();
                //                if (dt_levelthree.Rows.Count != 0)
                //                {
                //                    menu_ind_up_second = "fa fa-angle-up";
                //                    menu_ind_down_second = "fa fa-angle-down";
                //                    Get_Summary = dt_levelthree.AsEnumerable().Select(row =>
                //                      new sys_sub1menu
                //                      {

                //                          text = row["module_name"].ToString(),
                //                          sref = row["sref"].ToString(),
                //                          icon = row["icon"].ToString(),
                //                          // sub_icon = row["icon_name"].ToString()
                //                      }).ToList();
                //                }
                //                else
                //                {
                //                    menu_ind_up_second = "";
                //                    menu_ind_down_second = "";
                //                }
                //                dt_levelthree.Clear();
                //                getmenu2.Add(new sys_submenub
                //                {
                //                    text = drTwo["module_name"].ToString(),
                //                    sref = drTwo["sref"].ToString(),
                //                    menu_indication = menu_ind_up_second,
                //                    menu_indication1 = menu_ind_down_second,
                //                    // sub_icon = drTwo["icon_name"].ToString(),
                    
                //                });

                //            }

                //            dt_leveltwo.Clear();
                //        }
                //        else
                //        {
                //            menu_ind_up_first = "";
                //            menu_ind_down_first = "";
                //        }
                //        //var getmenu = new List<sys_menu>();
                //        getmenu.Add(new sys_menus
                //        {
                //            text = drOne["module_name"].ToString(),
                //            sref = drOne["sref"].ToString(),
                //            icon = drOne["icon"].ToString(),
                //            menu_indication = menu_ind_up_first,
                //            menu_indication1 = menu_ind_down_first,
                //            label = "label label-success",
                //            submenu = getmenu2
                //        });
                //        values.menu_lists = getmenu;
                //    }

                //}

                //dt_levelone.Clear();
       
            }
             
            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
          
        }

        // Login From ERP

        [AllowAnonymous]
        [ActionName("loginERP")]
        [HttpPost]
        public HttpResponseMessage GetUserReturn(loginERPinput values)
        {
            loginresponse GetLoginResponse = new loginresponse();
            try
            {
                var username = string.Empty;

                msSQL = " SELECT b.user_gid,a.department_gid, a.employee_gid, user_password, user_code, concat(user_firstname, ' ', user_lastname) as username FROM hrm_mst_temployee a " +
                        " INNER JOIN adm_mst_tuser b on b.user_gid = a.user_gid " +
                        " WHERE user_code = '" + values.user_code + "'";
                objODBCdatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCdatareader.HasRows == true)
                {
                    objODBCdatareader.Read();
                    string password = objODBCdatareader["user_password"].ToString();
                    var tokenresponse = Token(values.user_code, password);
                    dynamic newobj = Newtonsoft.Json.JsonConvert.DeserializeObject(tokenresponse);
                    tokenvalue = newobj.access_token;
                    employee_gid = objODBCdatareader["employee_gid"].ToString();
                    user_gid = objODBCdatareader["user_gid"].ToString();
                    username = objODBCdatareader["username"].ToString();
                    department_gid = objODBCdatareader["department_gid"].ToString();
                    objODBCdatareader.Close();
                }
                msSQL = " INSERT INTO adm_mst_ttoken ( " +
                         " token, " +
                         " employee_gid, " +
                         " user_gid, " +
                         " department_gid, " +
                         " company_code " +
                         " )VALUES( " +
                         " 'Bearer " + tokenvalue + "'," +
                         " '" + employee_gid + "'," +
                         " '" + user_gid + "'," +
                         " '" + department_gid + "'," +
                         " '" + ConfigurationManager.AppSettings["company_code"] + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                GetLoginResponse.status = true;
                GetLoginResponse.message = "";
                GetLoginResponse.token = "Bearer " + tokenvalue;
                GetLoginResponse.username = username;
                GetLoginResponse.user_gid = user_gid;
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = ex.ToString();
            }
            finally
            {

            }
            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
        }
 
        // Application Vendor Login

        [AllowAnonymous]
        [ActionName("vendorLogin")]
        [HttpPost]
        public HttpResponseMessage appVenLogin(appVendorInput values)
        {
            loginresponse GetLoginResponse = new loginresponse();
            try
            {
                domain = Request.RequestUri.Host.ToLower();
                string lscompany_code = ConfigurationManager.AppSettings[domain].ToString();
                msSQL = " SELECT vendoruser_status FROM " + lscompany_code + ".adm_mst_tvendoruser WHERE vendoruser_code='" + values.app_code + "' ";
                vendoruser_status = objdbconn.GetExecuteScalar(msSQL);
                if (vendoruser_status == "Y")
                {
                    msSQL = " SELECT vendoruser_gid,vendoruser_password, CONCAT(vendoruser_firstname, ' ' ,vendoruser_lastname) as username " +
                                " FROM " + lscompany_code + ".adm_mst_tvendoruser WHERE vendoruser_code='" + values.app_code + "' ";
                    objODBCdatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCdatareader.HasRows)
                    {
                        password = objODBCdatareader["vendoruser_password"].ToString();
                        if (password == objcmnfunctions.ConvertToAscii(values.password))
                        {
                            user_gid = objODBCdatareader["vendoruser_gid"].ToString();
                            username = objODBCdatareader["username"].ToString();
                            objODBCdatareader.Close();

                            msSQL = " SELECT applicationmaster_gid FROM " + lscompany_code + ".its_mst_tapplicationmaster WHERE application_code='" + values.app_code + "' ";
                            objODBCdatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCdatareader.HasRows)
                            {
                                objODBCdatareader.Close();

                                var ObjToken = Token(values.app_code, objcmnfunctions.ConvertToAscii(values.password), null);
                                dynamic newobj = JsonConvert.DeserializeObject(ObjToken);
                                tokenvalue = "Bearer " + newobj.access_token;
                                if (tokenvalue != null)
                                {

                                    msSQL = " DELETE FROM adm_trn_tconsumertoken " +
                                            " WHERE company_code = '" + lscompany_code + "' " +
                                            " AND user_Code = '" + values.app_code + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = " INSERT INTO adm_trn_tconsumertoken(token, company_code, user_code, connection_string) " +
                                          " VALUES('" + tokenvalue + "', '" + lscompany_code + "', '" + values.app_code + "', (SELECT connection_string FROM adm_mst_tconsumerdb " +
                                          " WHERE company_code = '" + lscompany_code + "'))";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = " SELECT vendoruser_gid,vendoruser_password, CONCAT(vendoruser_firstname, ' ' ,vendoruser_lastname) as username " +
                                            " FROM " + lscompany_code + ".adm_mst_tvendoruser WHERE vendoruser_code='" + values.app_code + "' ";
                                    objODBCdatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCdatareader.HasRows == true)
                                    {
                                        user_gid = objODBCdatareader["vendoruser_gid"].ToString();
                                    }
                                    objODBCdatareader.Close();

                                    msSQL = " INSERT INTO " + lscompany_code + ".adm_mst_ttoken ( " +
                                        " token, " +
                                        " user_gid, " +
                                        " created_date," +
                                        " company_code " +
                                        " )VALUES( " +
                                        " '" + tokenvalue + "'," +
                                        " '" + user_gid + "'," +
                                        " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        " '" + lscompany_code + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    GetLoginResponse.status = true;
                                    GetLoginResponse.message = "success";
                                    GetLoginResponse.token = tokenvalue;
                                    GetLoginResponse.username = username;

                                }
                                else
                                {
                                    GetLoginResponse.status = false;
                                }

                            }
                            else
                            {
                                objODBCdatareader.Close();

                                GetLoginResponse.status = false;
                                GetLoginResponse.message = "appcode";
                            }

                        }
                        else
                        {
                            GetLoginResponse.status = false;
                            GetLoginResponse.message = "password";
                        }
                    }
                    else
                    {
                        objODBCdatareader.Close();

                        GetLoginResponse.status = false;
                        GetLoginResponse.message = "usercode";
                    }
                }
                else
                {
                  
                    GetLoginResponse.status = false;
                    GetLoginResponse.message = "userstatus";
                }
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = "error" + ex.ToString();
            }
            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
                
        }

        // Application Lawyer Login

        [AllowAnonymous]
        [ActionName("lawyerUserLogin")]
        [HttpPost]
        public HttpResponseMessage GetLawyerUserLogin(userlogininput values)
        {
            loginresponse GetLoginResponse = new loginresponse();
            try
            {
                domain = Request.RequestUri.Host.ToLower();
                var ObjToken = Token(values.user_code, objcmnfunctions.ConvertToAscii(values.user_password), null);
                dynamic newobj = JsonConvert.DeserializeObject(ObjToken);
                tokenvalue = "Bearer " + newobj.access_token;
                if (tokenvalue != null)
                {
                    string lscompany_code = ConfigurationManager.AppSettings[domain].ToString();
                    msSQL = " DELETE FROM adm_trn_tconsumertoken " +
                            " WHERE company_code = '" + lscompany_code + "' " +
                            " AND user_Code = '" + values.user_code + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " INSERT INTO adm_trn_tconsumertoken(token, company_code, user_code, connection_string) " +
                          " VALUES('" + tokenvalue + "', '" + lscompany_code + "', '" + values.user_code + "', (SELECT connection_string FROM adm_mst_tconsumerdb " +
                          " WHERE company_code = '" + lscompany_code + "'))";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select lawyeruser_gid,lawyeruser_code,lawyeruser_password from " + lscompany_code + ".lgl_mst_tlawyeruser " +
                        " WHERE lawyeruser_status='Y' and lawyeruser_code = '" + values.user_code + "' and lawyeruser_password ='" + objcmnfunctions.ConvertToAscii(values.user_password) + "'";
                    objODBCdatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCdatareader.HasRows == true)
                    {
                        user_gid = objODBCdatareader["lawyeruser_gid"].ToString();
                    }
                    objODBCdatareader.Close();

                    msSQL = " INSERT INTO " + lscompany_code + ".adm_mst_ttoken ( " +
                        " token, " +
                        " user_gid, " +
                        " created_date," +
                        " company_code " +
                        " )VALUES( " +
                        " '" + tokenvalue + "'," +
                        " '" + user_gid + "'," +
                        " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " '" + lscompany_code + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    GetLoginResponse.status = true;
                    GetLoginResponse.message = "";
                    GetLoginResponse.token = tokenvalue;
                    GetLoginResponse.user_gid = user_gid;

                }
                else
                {
                    GetLoginResponse.status = false;
                }
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = ex.ToString();

            }

            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
 
        }

        // Application External Vendor Login _RSK

        [AllowAnonymous]
        [ActionName("externalVendorUserLogin")]
        [HttpPost]
        public HttpResponseMessage externalVendorUserLogin(userlogininput values)
        {
            loginresponse GetLoginResponse = new loginresponse();
            try
            {
                domain = Request.RequestUri.Host.ToLower();
                var ObjToken = Token(values.user_code, objcmnfunctions.ConvertToAscii(values.user_password), null);
                dynamic newobj = JsonConvert.DeserializeObject(ObjToken);
                tokenvalue = "Bearer " + newobj.access_token;
                if (tokenvalue != null)
                {
                    string lscompany_code = ConfigurationManager.AppSettings[domain].ToString();

                    msSQL = " DELETE FROM adm_trn_tconsumertoken " +
                            " WHERE company_code = '" + lscompany_code + "' " +
                            " AND user_Code = '" + values.user_code + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " INSERT INTO adm_trn_tconsumertoken(token, company_code, user_code, connection_string) " +
                          " VALUES('" + tokenvalue + "', '" + lscompany_code + "', '" + values.user_code + "', (SELECT connection_string FROM adm_mst_tconsumerdb " +
                          " WHERE company_code = '" + lscompany_code + "'))";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select external_usergid,external_usercode from  " + lscompany_code + ".rsk_mst_texternaluser " +
                       " WHERE external_userstatus='Y' and external_usercode = '" + values.user_code + "' and external_userpassword ='" + objcmnfunctions.ConvertToAscii(values.user_password) + "'";
                    objODBCdatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCdatareader.HasRows == true)
                    {
                        user_gid = objODBCdatareader["external_usergid"].ToString();
                    }
                    objODBCdatareader.Close();

                    msSQL = " INSERT INTO " + lscompany_code + ".adm_mst_ttoken ( " +
                        " token, " +
                        " user_gid, " +
                        " created_date," +
                        " company_code " +
                        " )VALUES( " +
                        " '" + tokenvalue + "'," +
                        " '" + user_gid + "'," +
                        " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " '" + lscompany_code + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    GetLoginResponse.status = true;
                    GetLoginResponse.message = "";
                    GetLoginResponse.token = tokenvalue;
                    GetLoginResponse.user_gid = user_gid;

                }
                else
                {
                    GetLoginResponse.status = false;
                }
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = ex.ToString();

            }

            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
        }

        [HttpPost]
        [ActionName("PostUserLogin")]
        public HttpResponseMessage PostUserLogin(PostUserLogin values)
        {
            loginresponse GetLoginResponse = new loginresponse();
            try
            {
                var url = ConfigurationManager.AppSettings["host"];
                if (url == ConfigurationManager.AppSettings["livedomain_url"].ToString())
                {
                    var getSpireDocLicense = ConfigurationManager.AppSettings["SpireDocLicenseKey"];
                    Spire.License.LicenseProvider.SetLicenseKey(getSpireDocLicense);
                } 

                domain = Request.RequestUri.Host.ToLower();
                    var ObjToken = Token(values.user_code, objcmnfunctions.ConvertToAscii(values.user_password), ConfigurationManager.AppSettings[domain].ToString());
                    dynamic newobj = JsonConvert.DeserializeObject(ObjToken);
                    tokenvalue = "Bearer " + newobj.access_token;
                    if (tokenvalue != null)
                    {
                        msSQL = "CALL storyboard.adm_mst_spstoretoken('" + tokenvalue + "', '" + values.user_code + "',  '" + objcmnfunctions.ConvertToAscii(values.user_password) + "', '" + ConfigurationManager.AppSettings[domain].ToString() + "')";
                        user_gid = objdbconn.GetExecuteScalar("CALL storyboard.adm_mst_spstoretoken('" + tokenvalue + "','" + values.user_code + "','" + objcmnfunctions.ConvertToAscii(values.user_password) + "','" + ConfigurationManager.AppSettings[domain].ToString() + "','Web','')");
                    GetLoginResponse.status = true;
                        GetLoginResponse.message = "";
                        GetLoginResponse.token = tokenvalue;
                        GetLoginResponse.user_gid = user_gid;

                        //return Ok(JsonConvert.SerializeObject(new {tokenvalue,user_gid }));
                        return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);

                    }
                    else
                    {
                        GetLoginResponse.status = false;
                        return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
                    }
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = ex.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
            }
        }
        public string Token(string userName, string password, string company_code = null)
        {

            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "grant_type", "password" ),
                            new KeyValuePair<string, string>( "username", userName ),
                            new KeyValuePair<string, string> ( "Password", password ),
                            new KeyValuePair<string, string>("Scope",company_code)
                        };
            var content = new FormUrlEncodedContent(pairs);
            using (var client = new HttpClient())

            {
                domain = Request.RequestUri.Host.ToLower();
                var host = HttpContext.Current.Request.Url.Host;
                if (host == "localhost")
                {
                    var response = client.PostAsync(ConfigurationManager.AppSettings["protocol"].ToString() + ConfigurationManager.AppSettings["host"].ToString() +
                   "/StoryboardAPI/token", new FormUrlEncodedContent(pairs)).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    var response = client.PostAsync(ConfigurationManager.AppSettings["protocol"].ToString() + domain +
                   "/StoryboardAPI/token", new FormUrlEncodedContent(pairs)).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }

            }
        }

        [ActionName("eInvoiceMailTrigger")]
        [HttpPost]
        public HttpResponseMessage eInvoiceMailTrigger(MdlMail values)
        {
            HttpRequest httpRequest;
            httpRequest = HttpContext.Current.Request;
            einvoiceResponse objResult = new einvoiceResponse();
            int ls_port = 0;
            string ls_server = string.Empty, ls_username = string.Empty, ls_password = string.Empty;

            msSQL = " SELECT gst_no FROM adm_mst_teinvoiceenquiry where gst_no ='" + values.gst_no + "'";
            objODBCdatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCdatareader.HasRows == false)
            {
                try

                {
                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password  FROM adm_mst_tcompany";
                    objODBCdatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCdatareader.HasRows == true)
                    {
                        ls_server = objODBCdatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCdatareader["pop_port"]);
                        ls_username = objODBCdatareader["pop_username"].ToString();
                        ls_password = objODBCdatareader["pop_password"].ToString();
                    }
                    objODBCdatareader.Close();

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    message.To.Add(new MailAddress(ConfigurationManager.AppSettings["einvoiceRecipient"].ToString()));
                    message.Subject = "Vcidex E-Invoice - Enquiry Alert!!";
                    message.IsBodyHtml = true; //to make message body as html  

                    string body = "Dear Marketing Team," + "<br/>" + "\n" + "<br/>" + "\n" + "Please Find the New Requirement for your Reference." + "<br/>" + "\n" + "<br/>" + "\n" +
                                  "<b>Company Name</b>:" + "\n" + values.company_name + "<br/>" + "\n" +
                                  "<b>Authorised Mobile No</b>:" + "\n" + values.mobile_no + "<br/>" + "\n" +
                                  "<b>Authorised Email ID</b>:" + "\n" + values.mail + "<br/>" + "\n" +
                                  "<b>GST No</b>:" + "\n" + values.gst_no + "<br/>" +
                                  "<b>Name</b>:" + "\n" + values.name + "<br/>" + "\n" +
                                  "<b>Location</b>:" + "\n" + values.location + "<br/>" +
                                  "<b>Queries</b>:" + "\n" + values.queries + "<br/>" +
                                  "<br/>";
                    message.Body = body;
                    smtp.Port = ls_port;
                    smtp.Host = ls_server; //for gmail host 
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    try
                    {
                        smtp.Send(message);
                        if (string.IsNullOrEmpty(values.name))
                            values.name = "";
                        if (string.IsNullOrEmpty(values.company_name))
                            values.company_name = "";
                        if (string.IsNullOrEmpty(values.location))
                            values.location = "";
                        if (string.IsNullOrEmpty(values.queries))
                            values.queries = "";
                        msSQL = " insert into adm_mst_teinvoiceenquiry( " +
                                " name, " +
                                " mobile_no, " +
                                " mail," +
                                " company_name, " +
                                " location, " +
                                " gst_no, " +
                                " queries " +
                                " )VALUES( " +
                                " '" + values.name.Replace("'", "") + "'," +
                                " '" + values.mobile_no + "'," +
                                " '" + values.mail + "'," +
                                " '" + values.company_name.Replace("'", "") + "'," +
                                " '" + values.location.Replace("'", "") + "'," +
                                " '" + values.gst_no + "'," +
                                " '" + values.queries.Replace("'", "") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        objResult.status = true;
                        objResult.message = "Your query has been registered successfully. Our Team will get back to you shortly!";
                    }
                    catch (Exception ex)
                    {
                        objResult.message = "Error occured while contacting our team. Please try again later!";
                    }
                }
                catch (Exception ex)
                {
                    objResult.message = "Error occured while registering your query. Kindly try again after sometime! Thank You.";
                }
            }
            else
            {
                objResult.message = "Enquiry registered already!";
            }


            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

    }
}
