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
    public class DaCustomerView
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid, ls_server, ls_username, ls_password;

        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5, ls_port;



        public void DaGetfrommaildropdown(MdlCustomerView values)
        {
            msSQL = " SELECT company_gid,pop_server, pop_port, pop_username, pop_password  FROM adm_mst_tcompany";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<from_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new from_list
                    {
                        mail = dt["pop_username"].ToString(),
                        company_gid = dt["company_gid"].ToString(),




                        //created_date = dt["created_date"].ToString(),
                    });
                    values.mailidlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        //public void Dasendmailtocustomer(HttpRequest httpRequest, string sub, string body, string tomail_id)

        //{

        //    try

        //    {

        //        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password  FROM adm_mst_tcompany";

        //        objODBCDatareader = objdbconn.GetDataReader(msSQL);

        //        if (objODBCDatareader.HasRows == true)

        //        {

        //            ls_server = objODBCDatareader["pop_server"].ToString();

        //            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);

        //            ls_username = objODBCDatareader["pop_username"].ToString();

        //            ls_password = objODBCDatareader["pop_password"].ToString();

        //        }

        //        objODBCDatareader.Close();



        //        MailMessage message = new MailMessage();

        //        SmtpClient smtp = new SmtpClient();

        //        message.From = new MailAddress(ls_username);

        //        message.To.Add(new MailAddress(tomail_id));


        //        message.Subject = sub;

        //        message.IsBodyHtml = true; //to make message body as html  

        //        message.Body = body;

        //        smtp.Port = ls_port;

        //        smtp.Host = ls_server; //for gmail host  

        //        smtp.EnableSsl = true;

        //        smtp.UseDefaultCredentials = false;

        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //        smtp.Credentials = new NetworkCredential(ls_username, ls_password);

        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

        //        try
        //        {

        //            smtp.Send(message);

        //            //mail_send_result = true;

        //            //result = "Mail Send Successfully";



        //        }

        //        catch (Exception ex)

        //        {

        //            //mail_send_result = false;

        //            //result = ex.ToString();

        //        }

        //        //mail_details_result = true;

        //    }

        //    catch (Exception ex)

        //    {

        //        //result = ex.ToString();

        //        //mail_details_result = false;

        //    }



        //    //Mail Log

        //    //msSQL = "Insert into ocs_mst_tcustomermaillog( " +

        //    //                       " application_gid," +

        //    //                       " application_number," +

        //    //                       " from_mailid," +

        //    //                       " cc," +

        //    //                       " bcc," +

        //    //                       " email_to," +

        //    //                       " email_date," +

        //    //                       " email_subject," +

        //    //                       " email_content," +

        //    //                       " status," +

        //    //                       " created_date)" +

        //    //                       " values(" +

        //    //                       "'" + application_gid + "'," +

        //    //                       "'" + arn_no + "'," +

        //    //                       "'" + ls_username + "'," +

        //    //                       "'" + cc_mailid + "'," +

        //    //                       "'" + lsBccmail_id + "'," +

        //    //                       "'" + tomail_id + "'," +

        //    //                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +

        //    //                       "'" + sub.Replace("'", "") + "'," +

        //    //                       "'" + body.Replace("'", "") + "'," +

        //    //                       "'" + result + "'," +

        //    //                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

        //    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



        //    //if (mail_send_result == true && mail_details_result == true)

        //    //{

        //    //    return true;

        //    //}

        //    //else

        //    //{

        //    //    return false;

        //    //}

        //}



        public void DaPostcontactupdate(cont_list values, string user_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AUCV");
            msSQL = "insert into crm_trn_tapprovecontactview(" +
            " approvecontact_gid, " +
            " contact_person," +
            " source," +
            " region," +
            " company_code," +
            " fax," +
            " city," +
            " country," +
            " designation," +
            " branch_name," +
            " email," +
            " address1," +
            " address2," +
            " state," +
            " company_name, " +
            " tele1," +
            " tele2," +
            " website, " +
            " approve_flag, " +
            " mobile, " +
            " leadbank_gid, " +
            " employee_gid, " +
             " date," +
             " status," +
              " request_from," +
             " updated_flag, " +
            " pincode)" +
         " Values(" +
           " '" + msGetGid + "', " +
           " '" + values.approvecontact_gid + "', " +
           " '" + values.contact_person + "'," +
           " '" + values.source + "'," +
            " '" + values.region + "'," +
           " '" + values.company_code + "'," +
           " '" + values.fax + "'," +
           " '" + values.city + "'," +
           " '" + values.country + "'," +
           " '" + values.designation + "'," +
           " '" + values.branch_name + "'," +
           " '" + values.email + "'," +
           " '" + values.address1 + "'," +
           " '" + values.address2 + "'," +
           " '" + values.state + "'," +
           " '" + values.company_name + "'," +
           " '" + values.tele1 + "'," +
           " '" + values.tele2 + "'," +
           " '" + values.website + "'," +
           " '" + values.approve_flag + "'," +
           " '" + values.mobile + "'," +
           " '" + values.leadbank_gid + "'," +
           " '" + values.employee_gid + "'," +
           " '" + values.date + "'," +
           " '" + values.status + "'," +
           " '" + values.request_from + "'," +
           " '" + values.updated_flag + "'," +
           " '" + values.pincode + "'," +
           "'" + user_gid + "'," +
           "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //if (mnResult != 0)
            //{
            //    values.status = true;
            //    values.message = " Call Updated Successfully";
            //}
            //else
            //{
            //    values.status = false;
            //    values.message = " Error Occurs ";



            //}



        }


        public void DaGetEnquirySummary(MdlCustomerView values)
        {
            msSQL = " Select distinct concat(a.enquiry_gid,' / ',a.enquiry_type) as enquiry_referencenumber, format(a.potorder_value,2)as potorder_value," +
                " a.enquiry_gid,a.enquiry_date,m.referred_by,concat(b.user_firstname,' ',b.user_lastname) as campaign," +
                " a.customer_name,a.branch_gid," +
                " a.customer_gid,a.lead_status,z.branch_name, " +
                " concat(o.region_name,' / ',m.leadbank_city,' / ',m.leadbank_state) as region_name," +
                " a.enquiry_referencenumber,a.enquiry_status,a.enquiry_type, " +
                " concat(f.user_firstname,' ',f.user_lastname) as user_firstname,a.enquiry_remarks,a.potorder_value ," +
                " a.contact_person,a.contact_email,a.contact_address,p.customer_rating, " +
                " case when a.contact_person is null then concat(n.leadbankcontact_name,' / ',n.mobile,' / ',n.email) " +
                " when a.contact_person is not null then concat(a.contact_person,' / ',a.contact_number,' / ',a.contact_email) end" +
                " as contact_details,a.enquiry_referencenumber, " +
                " r.leadstage_name,a.enquiry_type from smr_trn_tsalesenquiry a  " +
                " left join crm_trn_tleadbank m on m.leadbank_gid=a.customer_gid " +
                " left join crm_trn_tleadbankcontact n on n.leadbank_gid=m.leadbank_gid " +
                " left join crm_mst_tregion o on m.leadbank_region=o.region_gid " +
                " left join crm_trn_tenquiry2campaign p on p.enquiry_gid=a.enquiry_gid " +
                " left join crm_mst_tleadstage r on r.leadstage_gid=p.leadstage_gid " +
                " left join smr_trn_tcampaign q on q.campaign_gid=p.campaign_gid " +
                " left join hrm_mst_temployee d on d.employee_gid=p.assign_to " +
                " left join adm_mst_tuser b on b.user_gid= d.user_gid " +
                " left join hrm_mst_temployee k on k.employee_gid=a.created_by " +
                " left join adm_mst_tuser f on f.user_gid= k.user_gid " +
                " left join hrm_mst_tbranch z on a.branch_gid=z.branch_gid " +
                " left join crm_mst_tsource s on s.source_gid=m.source_gid " +
                " where 1=1 ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<enquiry_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new enquiry_list
                    {
                        enquiry_gid = dt["enquiry_referencenumber"].ToString(),
                        branch_gid = dt["branch_gid"].ToString(),
                        branch_name = dt["branch_name"].ToString(),



                        enquiry_date = dt["enquiry_date"].ToString(),



                        //leadbank_gid = dt["leadbank_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        contact_number = dt["contact_details"].ToString(),



                        enquiry_referencenumber = dt["enquiry_referencenumber"].ToString(),



                        customer_rating = dt["customer_rating"].ToString(),
                        lead_status = dt["lead_status"].ToString(),
                        enquiry_status = dt["enquiry_status"].ToString(),






                        contact_details = dt["contact_details"].ToString(),
                        contact_email = dt["contact_email"].ToString(),
                        contact_address = dt["contact_address"].ToString(),






                        potorder_value = dt["potorder_value"].ToString(),
                        //created_by = dt["created_by"].ToString(),
                        //created_date = dt["created_date"].ToString(),
                    });
                    values.enquirydtl = getModuleList;
                }
            }



            dt_datatable.Dispose();
        }
    }
}



