using ems.sdc.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ems.sdc.DataAccess
{
    public class DaSdcTrnUatDeployment
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult, ls_port;
        string moduleGID = string.Empty;
        string frommail_id, sub, tomail_id, body, ls_server, ls_username, ls_password, lscontent = string.Empty;
        string deployed_date, deployed_by, created_by, created_date, file_description;
        // Test Summary
        public void DaGetUatSummary(MdlUatSummary values)
        {
            msSQL = " select a.uat_gid, uat_status, uatinprogress_flag, uatdeploy_flag, live_flag, " +
                    " group_concat(DISTINCT CONCAT(c.module_prefix, ' (', file_description, ')')) as file_description, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                    " CASE WHEN uatdeploy_flag = 'N' THEN '-' ELSE concat(d.user_firstname,' ',d.user_lastname,' / ',d.user_code,' / ',date_format(a.deployed_date,'%d-%m-%Y %h:%i %p') ) END as deployed_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date FROM sdc_trn_tuatdeployment a " +
                    " LEFT JOIN sdc_trn_tuatdeploymentdtl c on c.uat_gid = a.uat_gid " +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by = b.user_gid " +
                    " LEFT JOIN adm_mst_tuser d ON a.deployed_by = d.user_gid " +
                    " group by a.uat_gid order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUatList = new List<uatsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getUatList.Add(new uatsummary_list
                    {
                        uat_gid = dt["uat_gid"].ToString(),
                        uat_status = dt["uat_status"].ToString(),
                        uatinprogress_flag = dt["uatinprogress_flag"].ToString(),
                        uatdeploy_flag = dt["uatdeploy_flag"].ToString(),                     
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        deployed_by = dt["deployed_by"].ToString(),
                        file_description = dt["file_description"].ToString(),
                        live_flag = dt["live_flag"].ToString(),
                    });
                    values.uatsummary_list = getUatList;
                }
            }
            dt_datatable.Dispose();
        }

        public bool DaPostStatusUpdate(MdlStatusUpdate values, string user_gid)
        {
            bool status = false;

            msSQL = " update sdc_trn_tuatdeployment set " +
                " uat_status='" + values.uat_status + "'," +
                " uatinprogress_flag='Y'," +
                " uatdeploy_flag='N'," +
                " updated_by='" + user_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where uat_gid='" + values.uat_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Status Updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return status;
        }

        public bool DaPostDeployStatusUpdate(MdlStatusUpdate values, string user_gid)
        {
            bool status = false;

            msSQL = " update sdc_trn_tuatdeployment set " +
                " uat_status='" + values.uat_status + "'," +
                " uatdeploy_flag='Y'," +
                " mail_flag='" + values.mail_flag + "'," +
                " deployed_by='" + user_gid + "'," +
                " deployed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where uat_gid='" + values.uat_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "File Moved to UAT Successfully..!";

                if (values.mail_flag == "Y")
                {

                    try
                    {

                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Read();
                            frommail_id = objODBCDatareader["company_mail"].ToString();
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();

                        }
                        objODBCDatareader.Close();


                        msSQL = "select b.employee_emailid from sdc_trn_tuatdeployment a " +
                                "left join hrm_mst_temployee b on a.created_by = b.user_gid where uat_gid='" + values.uat_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select group_concat(DISTINCT CONCAT(d.module_prefix, ' (', file_description, ')')) as file_description, " +
                                " date_format(a.deployed_date, '%d-%m-%Y %h:%i %p') as deployed_date, " +
                                " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as deployed_by, " +
                                " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by " +
                                " from sdc_trn_tuatdeployment a " +
                                " left join sdc_trn_tuatdeploymentdtl d on d.uat_gid=a.uat_gid " +
                                " left join adm_mst_tuser b on a.deployed_by = b.user_gid " +
                                " left join adm_mst_tuser c on a.created_by = c.user_gid " +
                                " where a.uat_gid='" + values.uat_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            file_description = objODBCDatareader["file_description"].ToString();
                            deployed_date = objODBCDatareader["deployed_date"].ToString();
                            deployed_by = objODBCDatareader["deployed_by"].ToString();
                            created_by = objODBCDatareader["created_by"].ToString();
                        }
                        objODBCDatareader.Close();

                        sub = "File Moved to UAT Server";


                        lscontent = values.content;

                        body = "Dear " + created_by + ",  <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + " File Moved to UAT Server,the details are as follows,<br />";
                        body = body + "<br />";
                        body = body + "<b>File Description :</b> " + file_description + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Deployed By  :</b> " + deployed_by + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Deployed On  :</b> " + deployed_date + "<br />";
                        body = body + "<br />";

                        body = body + "<b>Thanks & Regards, </b><br/> ";
                        body = body + "<br />";
                        body = body + "<b> Team Deployers </b> ";
                        body = body + "<br />";

                        //cc_mailid = "";
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(ls_username);
                        message.To.Add(new MailAddress(tomail_id));
                        //message.CC.Add(cc);


                        //if (cc != null & cc != string.Empty & cc != "")
                        //{
                        //    lsCCReceipients = cc.Split(',');
                        //    if (cc.Length == 0)
                        //    {
                        //        message.CC.Add(new MailAddress(cc));
                        //    }
                        //    else
                        //    {
                        //        foreach (string CCEmail in lsCCReceipients)
                        //        {
                        //            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        //        }
                        //    }
                        //}

                        message.Subject = sub;
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = body;
                        smtp.Port = ls_port;
                        smtp.Host = ls_server; //for gmail host  
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);

                        values.status = true;

                        //if (values.status == true)
                        //{
                        //    msSQL = "Insert into osd_trn_tmailcount( " +
                        //    " servicerequest_gid," +
                        //    " from_mail," +
                        //    " to_mail," +
                        //    " cc_mail," +
                        //    " mail_status," +
                        //    " mail_senddate, " +
                        //    " created_by," +
                        //    " created_date)" +
                        //    " values(" +
                        //    "'" + msGetGid + "'," +
                        //    "'" + ls_username + "'," +
                        //    "'" + tomail_id + "'," +
                        //    "'" + cc + "'," +
                        //    "'Service Request Assigned'," +
                        //    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        //    "'" + user_gid + "'," +
                        //    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //}

                    }
                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;

                    }

                }

            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return status;
        }

        public bool DaGetMovetoLive(MdlMoveToUAT values, string user_gid)
        {
            bool status = false;

            msGetGid = objcmnfunctions.GetMasterGID("LIV");
            msSQL = " insert into sdc_trn_tlivedeployment(" +
            " live_gid," +
            " live_status," +
            " created_by," +
            " created_date)" +
            " values(" +
            "'" + msGetGid + "'," +
            "'Pending', " +
            "'" + user_gid + "'," +
            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            foreach (string i in values.uat_gid)
            {

                msSQL = "select uat_gid, test_gid, module_gid, module_prefix, version_Major, version_enhancement, version_patch, " +
                        "version_bug, test_description, file_description, script_flag " +
                        "from sdc_trn_tuatdeploymentdtl where uat_gid='" + i + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {

                        msSQL = " insert into sdc_trn_tlivedeploymentdtl(" +
                        " live_gid," +
                        " uat_gid," +
                        " test_gid, " +
                        " module_gid," +
                        " module_prefix, " +
                        " version_major," +
                        " version_enhancement," +
                        " version_patch," +
                        " version_bug," +
                        " test_description," +
                        " file_description," +
                        " script_flag," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + i + "', " +
                        "'" + dt["test_gid"].ToString() + "'," +
                        "'" + dt["module_gid"].ToString() + "'," +
                        "'" + dt["module_prefix"].ToString() + "'," +
                        "'" + dt["version_Major"].ToString() + "'," +
                        "'" + dt["version_enhancement"].ToString() + "'," +
                        "'" + dt["version_patch"].ToString() + "'," +
                        "'" + dt["version_bug"].ToString() + "'," +
                        "'" + dt["test_description"].ToString() + "'," +
                        "'" + dt["file_description"].ToString() + "'," +
                        "'" + dt["script_flag"].ToString() + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update sdc_trn_tuatdeployment set " +
                                " live_flag='Y'," +
                                " uat_status='Moved to LIVE'" +
                                " where uat_gid='" + i + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (dt["script_flag"].ToString() == "Y")
                        {
                            msSQL = " update sdc_trn_tlivedeployment set " +
                                " script_flag='Y'" +
                                " where live_gid='" + msGetGid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
            }
            if (mnResult != 0)
            {

                try
                {

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        frommail_id = objODBCDatareader["company_mail"].ToString();
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();

                    }
                    objODBCDatareader.Close();

                    msSQL = "SELECT email_id FROM hrm_mst_tdepartment where department_name='Deployers'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select a.live_gid,  " +
                     " group_concat(DISTINCT CONCAT(c.module_prefix, ' (', c.file_description, ')')) as file_description, " +
                     " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                     " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date FROM sdc_trn_tlivedeployment a " +
                     " LEFT JOIN sdc_trn_tlivedeploymentdtl c on c.live_gid = a.live_gid " +
                     " LEFT JOIN adm_mst_tuser b ON a.created_by = b.user_gid " +
                     " where a.live_gid='" + msGetGid + "' " +
                     " group by a.live_gid order by a.created_date desc ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        file_description = objODBCDatareader["file_description"].ToString();
                        created_date = objODBCDatareader["created_date"].ToString();
                        created_by = objODBCDatareader["created_by"].ToString();
                    }
                    objODBCDatareader.Close();

                    sub = "File Moved to LIVE Server";


                    lscontent = values.content;

                    body = "Dear Deployers <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + " Need to Move File to the Live Server,the details are as follows,<br />";
                    body = body + "<br />";
                    body = body + "<b>File Description :</b> " + file_description + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Created By  :</b> " + created_by + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Created On  :</b> " + created_date + "<br />";
                    body = body + "<br />";

                    body = body + "<b>Thanks & Regards, </b><br/> ";
                    body = body + "<br />";
                    body = body + "<b>" + created_by + "</b> ";
                    body = body + "<br />";

                    //cc_mailid = "";
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    message.To.Add(new MailAddress(tomail_id));


                    message.Subject = sub;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = body;
                    smtp.Port = ls_port;
                    smtp.Host = ls_server; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;



                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;

                }
                values.status = true;
                values.message = "Records Moved to LIVE Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return status;
        }

        public void DaGetUatDeploymentView(string uat_gid, MdlUatView values)
        {

            msSQL = " select a.uat_gid, uat_status, uatinprogress_flag, uatdeploy_flag, " +
                    " group_concat(DISTINCT CONCAT(c.module_prefix, ' (', c.file_description, ')')) as file_description, " +
                    " CASE WHEN new_pages = '' THEN 'No' ELSE new_pages END as new_pages, " +
                    " CASE WHEN d.new_reports = '' THEN 'No' ELSE new_reports END as new_reports, " +
                    " CASE WHEN a.script_flag = 'Y' THEN 'Yes' ELSE 'No' END as script, " +
                    " CASE WHEN new_dll = '' THEN 'No' ELSE new_dll END as new_dll, " +
                    " CASE WHEN new_dependency = '' THEN 'No' ELSE new_dependency END as new_dependency, " +
                    " CASE WHEN new_appjs = '' THEN 'No' ELSE new_appjs END as new_appjs, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date FROM sdc_trn_tuatdeployment a " +
                    " LEFT JOIN sdc_trn_tuatdeploymentdtl c on c.uat_gid = a.uat_gid " +
                    " LEFT JOIN sdc_trn_ttestdeployment d on d.test_gid = c.test_gid " +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by = b.user_gid " +
                    " where a.uat_gid='" + uat_gid + "' " +
                    " group by a.uat_gid order by a.created_date desc ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.new_pages = objODBCDatareader["new_pages"].ToString();
                values.new_reports = objODBCDatareader["new_reports"].ToString();
                values.newdll_name = objODBCDatareader["new_dll"].ToString();
                values.dependency_name = objODBCDatareader["new_dependency"].ToString();
                values.appjs_text = objODBCDatareader["new_appjs"].ToString();
                values.filedescription = objODBCDatareader["file_description"].ToString();
                values.script = objODBCDatareader["script"].ToString();

            }
            objODBCDatareader.Close();

            msSQL = "  select a.test_gid, file_name, file_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploaded_date, " + 
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as uploaded_by " +
                    " from sdc_trn_tuatdeploymentdtl a " +
                    " LEFT JOIN sdc_trn_tuploadscriptdocument c on a.test_gid = c.test_gid " +
                    " LEFT JOIN adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where a.uat_gid='" + uat_gid + "' and script_flag='Y' order by uat_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getscriptdocuments = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getscriptdocuments.Add(new upload_list
                    {
                        file_name = dt["file_name"].ToString(),
                        file_path = HttpContext.Current.Server.MapPath(dt["file_path"].ToString()),
                        uploaded_date = dt["uploaded_date"].ToString(),
                        uploaded_by = dt["uploaded_by"].ToString(),
                    });
                    values.upload_list = getscriptdocuments;
                }
            }
            dt_datatable.Dispose();

            msSQL = "  select a.test_gid, file_name, file_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploaded_date, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as uploaded_by " +
                    " from sdc_trn_tuatdeploymentdtl a " +
                    " LEFT JOIN sdc_trn_tuploadjsdocument c on a.test_gid = c.test_gid " +
                    " LEFT JOIN adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where a.uat_gid='" + uat_gid + "' and script_flag='Y' order by uat_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getuploaddocuments = new List<uploadjs_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getuploaddocuments.Add(new uploadjs_list
                    {
                        file_name = dt["file_name"].ToString(),
                        file_path = HttpContext.Current.Server.MapPath(dt["file_path"].ToString()),
                        uploaded_date = dt["uploaded_date"].ToString(),
                        uploaded_by = dt["uploaded_by"].ToString(),
                    });
                    values.uploadjs_list = getuploaddocuments;
                }
            }
            dt_datatable.Dispose();

            msSQL = "  select a.test_gid, file_name, file_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploaded_date, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as uploaded_by " +
                    " from sdc_trn_tuatdeploymentdtl a " +
                    " LEFT JOIN sdc_trn_tuploadversiondocument c on a.test_gid = c.test_gid " +
                    " LEFT JOIN adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where a.uat_gid='" + uat_gid + "' and script_flag='Y' order by uat_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getversiondocuments = new List<versionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getversiondocuments.Add(new versionupload_list
                    {
                        file_name = dt["file_name"].ToString(),
                        file_path = HttpContext.Current.Server.MapPath(dt["file_path"].ToString()),
                        uploaded_date = dt["uploaded_date"].ToString(),
                        uploaded_by = dt["uploaded_by"].ToString(),
                    });
                    values.versionupload_list = getversiondocuments;
                }
            }
            dt_datatable.Dispose();

            msSQL = "  select DISTINCT CONCAT(b.module_prefix, ' - ', b.test_description) as file_description from sdc_trn_tuatdeploymentdtl a" +
                    " LEFT JOIN sdc_trn_ttestdeployment b on b.test_gid = a.test_gid " +
                    " where uat_gid='" + uat_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer = new List<filedesc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcustomer.Add(new filedesc_list
                    {
                        files_description = dt["file_description"].ToString(),
                    });
                    values.filedesc_list = getcustomer;
                }
            }
            dt_datatable.Dispose();

        }
    }
}