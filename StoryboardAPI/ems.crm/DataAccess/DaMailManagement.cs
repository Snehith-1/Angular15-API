using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using ems.crm.Models;

namespace ems.crm.DataAccess
{
    public class DaMailManagement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid, ls_server, ls_username, ls_password;

        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5, ls_port;
    
        public void Dasendmailtocustomer(HttpRequest httpRequest, MdlMail values)

        {

            try

            {

                msSQL = " SELECT pop_server, pop_port, pop_username, pop_password  FROM adm_mst_tcompany";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)

                {

                    ls_server = objODBCDatareader["pop_server"].ToString();

                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);

                    ls_username = objODBCDatareader["pop_username"].ToString();

                    ls_password = objODBCDatareader["pop_password"].ToString();

                }

                objODBCDatareader.Close();



                MailMessage message = new MailMessage();

                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress(ls_username);

                message.To.Add(new MailAddress(values.to));


                message.Subject = values.sub;

                message.IsBodyHtml = true; //to make message body as html  

                message.Body = values.body;

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

                    //mail_send_result = true;

                    //result = "Mail Send Successfully";



                }

                catch (Exception ex)

                {

                    //mail_send_result = false;

                    //result = ex.ToString();

                }

                //mail_details_result = true;

            }

            catch (Exception ex)

            {

                //result = ex.ToString();

                //mail_details_result = false;

            }



            //Mail Log

            //msSQL = "Insert into ocs_mst_tcustomermaillog( " +

            //                       " application_gid," +

            //                       " application_number," +

            //                       " from_mailid," +

            //                       " cc," +

            //                       " bcc," +

            //                       " email_to," +

            //                       " email_date," +

            //                       " email_subject," +

            //                       " email_content," +

            //                       " status," +

            //                       " created_date)" +

            //                       " values(" +

            //                       "'" + application_gid + "'," +

            //                       "'" + arn_no + "'," +

            //                       "'" + ls_username + "'," +

            //                       "'" + cc_mailid + "'," +

            //                       "'" + lsBccmail_id + "'," +

            //                       "'" + tomail_id + "'," +

            //                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +

            //                       "'" + sub.Replace("'", "") + "'," +

            //                       "'" + body.Replace("'", "") + "'," +

            //                       "'" + result + "'," +

            //                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            //if (mail_send_result == true && mail_details_result == true)

            //{

            //    return true;

            //}

            //else

            //{

            //    return false;

            //}

        }
    }
}
