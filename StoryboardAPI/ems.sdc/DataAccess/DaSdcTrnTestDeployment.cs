using ems.sdc.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ems.sdc.DataAccess
{
    public class DaSdcTrnTestDeployment
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGetDocumentGid,lspath, test2customergid;
        int mnResult, ver_major, ver_enhancement, ver_patch, ver_bug, ls_port, mnResult1;
        string version_major, version_enhancement, version_patch, version_bug;
        string file_description, testfile_description, filedescription;
        string moduleGID = string.Empty;
        string lsnewdll_name, lsnewdependency_name, lsnewpage_names, lsreport_names, lsapp_js, lsscript_flag, jsdocument;
        string frommail_id, sub, tomail_id, body, ls_server, ls_username, ls_password, lscontent = string.Empty;
        string deployed_date, deployed_by, created_by, created_date;

        public bool DaPostAddTestDeployment(MdlAddTest values, string user_gid)
        {
            //msSQL = "select tmpversiondocument_gid from sdc_tmp_tuploadversiondocument where created_by='" + user_gid + "'";
            //versiondocument = objdbconn.GetExecuteScalar(msSQL);
            //if (versiondocument == "")
            //{
            //    values.message = "Upload Version Document";
            //    values.status = false;
            //    return false;
            //}
                if (values.script_flag == "Yes")
            {
                msSQL = "select * from sdc_tmp_tuploadscriptdocument where created_by='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    lsscript_flag = "Y";
                }
                else
                {
                    
                    values.message = "Upload Script Document";
                    values.status = false;
                    return false;
                }
            }
            else
            {
                lsscript_flag = "N";
            }
            if (values.newdll_flag == "Yes")
            {
                if (values.newdll_name == null)
                {
                    values.message = "Enter DLL Name";
                    values.status = false;
                    return false;
                }
                else
                {
                    lsnewdll_name = values.newdll_name;
                }
               
            }
            else
            {
                lsnewdll_name = "";
            }
            if(values.newDependency_flag == "Yes")
            {
                if(values.dependency_name == null)
                {
                    values.message = "Enter Dependency Name";
                    values.status = false;
                    return false;
                }
                else
                {
                    lsnewdependency_name = values.dependency_name;
                }
                
            }
            else
            {
                lsnewdependency_name = "";
            }
            if(values.newpage_flag == "Yes")
            {
                if (values.new_pages == null)
                {
                    values.message = "Enter New Page Names";
                    values.status = false;
                    return false;
                }
                else
                {
                    lsnewpage_names = values.new_pages;
                }
               
            }
            else
            {
                lsnewpage_names = "";
            }
            if(values.newReports_flag == "Yes")
            {
                if (values.new_reports == null)
                {
                    values.message = "Enter New Report Names";
                    values.status = false;
                    return false;
                }
                else
                {
                    lsreport_names = values.new_reports;
                }
                
            }
            else
            {
                lsreport_names = "";
            }
            if(values.newjs_flag == "Yes")
            {
                //if (values.appjs_text == null)
                //{
                    msSQL = "select tmpjsdocument_gid from sdc_tmp_tuploadjsdocument where created_by='" + user_gid + "'";
                    jsdocument = objdbconn.GetExecuteScalar(msSQL);
                    if (jsdocument == "")
                    {
                        values.message = "Upload JS Document";
                        values.status = false;
                        return false;
                    }
                //}
                //else
                //{
                //    lsapp_js = values.appjs_text;
                //}
                lsapp_js = "Yes";

            }
            else
            {
                lsapp_js = "";
            }

            msSQL =" select version_Major,version_enhancement,version_patch,version_bug, created_date"+
                   " from sdc_trn_ttestdeployment where module_gid = '" + values.module_gid + "' " +
                   " order by created_date desc limit 1 ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                            
                values.version_major = objODBCDatareader["version_Major"].ToString();
                values.version_enhancement = objODBCDatareader["version_enhancement"].ToString();
                values.version_patch = objODBCDatareader["version_patch"].ToString();
                values.version_bug = objODBCDatareader["version_bug"].ToString();

            }
            else
            {
                values.version_major = "0";
                values.version_enhancement = "0";
                values.version_patch = "0";
                values.version_bug = "0";
            }
            objODBCDatareader.Close();


            if (values.test_Objective == "Major Release")
            {
                ver_major = Convert.ToInt16(values.version_major);
                ver_major += 1;
                version_major = Convert.ToString(ver_major);
                version_enhancement = "0";
                version_patch = "0";
                version_bug = "0";
            }
           else if(values.test_Objective == "Enhancement")
            {
                ver_enhancement = Convert.ToInt16(values.version_enhancement);
                ver_enhancement += 1;
                version_enhancement = Convert.ToString(ver_enhancement);
                version_major = Convert.ToString(values.version_major);
                version_patch = "0";
                version_bug = "0";
            }
           else if(values.test_Objective == "Patch")
            {
                ver_patch = Convert.ToInt16(values.version_patch);
                ver_patch += 1;
                version_patch = Convert.ToString(ver_patch);
                version_major = Convert.ToString(values.version_major);
                version_enhancement = Convert.ToString(values.version_enhancement);
                version_bug = "0";
            }
           else
            {
                ver_bug = Convert.ToInt16(values.version_bug);
                ver_bug += 1;
                version_bug = Convert.ToString(ver_bug);
                version_major = Convert.ToString(values.version_major);
                version_enhancement = Convert.ToString(values.version_enhancement);
                version_patch = Convert.ToString(values.version_patch);
            }

            foreach (string i in values.file_description)
            {
                file_description += "" + i + ",";
            }
            filedescription += file_description;

            testfile_description = filedescription.TrimEnd(',');
            ////var s = M.E.P.B
            msGetGid = objcmnfunctions.GetMasterGID("TST");
            
            msSQL = " insert into sdc_trn_ttestdeployment(" +
                   " test_gid," +
                   " module_gid, " +
                   " module_prefix, " +
                   " test_objective, " +
                   " test_description," +
                   " new_pages," +
                   " new_reports, " +
                   " new_dll," +
                   " new_dependency," +
                   " new_appjs," +
                   " version_Major, " +
                   " version_enhancement," +
                   " version_patch," +
                   " version_bug," +
                   " test_status," +
                   " testinprogress_flag," +
                   " file_description," +
                   " script_flag," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.module_gid + "', " +
                   "'" + values.module_prefix + "'," +
                   "'" + values.test_Objective + "'," +
                   "'" + values.test_description.Replace("'", "\\'") + "'," +
                   "'" + lsnewpage_names.Replace("'", "\\'") + "'," +
                   "'" + lsreport_names.Replace("'", "\\'") + "'," +
                   "'" + lsnewdll_name.Replace("'", "\\'") + "'," +
                   "'" + lsnewdependency_name.Replace("'", "\\'") + "'," +
                   "'" + lsapp_js.Replace("'", "\\'") + "'," +
                   "'" + version_major + "'," +
                   "'" + version_enhancement + "'," +
                   "'" + version_patch + "'," +
                   "'" + version_bug + "'," +
                   "'Pending'," +
                   "'N'," +
                   "'" + testfile_description + "'," +
                   "'" + lsscript_flag + "'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (values.customerdtl != null)
            {
                for (var i = 0; i < values.customerdtl.Count; i++)
                {
                    test2customergid = objcmnfunctions.GetMasterGID("T2C");

                    msSQL = "Insert into sdc_trn_ttest2customer( " +
                           " test2customer_gid, " +
                           " test_gid," +
                           " customer_gid," +
                           " customer_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + test2customergid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.customerdtl[i].customer_gid + "'," +
                           "'" + values.customerdtl[i].customer_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = "select * from sdc_tmp_tuploadscriptdocument where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetDocumentGid = objcmnfunctions.GetMasterGID("TSDU");

                    msSQL = " insert into sdc_trn_tuploadscriptdocument(" +
                     " scriptdocument_gid," +
                     " test_gid, " +
                     " file_name," +
                     " file_path," +
                     " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetDocumentGid + "'," +
                     "'" + msGetGid + "', " +
                     "'" + dt["file_name"].ToString() + "'," +
                     "'" + dt["file_path"].ToString() + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();

            msSQL = "select * from sdc_tmp_tuploadversiondocument where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetDocumentGid = objcmnfunctions.GetMasterGID("TVUD");

                    msSQL = " insert into sdc_trn_tuploadversiondocument(" +
                     " versiondocument_gid," +
                     " test_gid, " +
                     " file_name," +
                     " file_path," +
                     " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetDocumentGid + "'," +
                     "'" + msGetGid + "', " +
                     "'" + dt["file_name"].ToString() + "'," +
                     "'" + dt["file_path"].ToString() + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();

            msSQL = "select * from sdc_tmp_tuploadjsdocument where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetDocumentGid = objcmnfunctions.GetMasterGID("TJUD");

                    msSQL = " insert into sdc_trn_tuploadjsdocument(" +
                     " jsdocument_gid," +
                     " test_gid, " +
                     " file_name," +
                     " file_path," +
                     " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetDocumentGid + "'," +
                     "'" + msGetGid + "', " +
                     "'" + dt["file_name"].ToString() + "'," +
                     "'" + dt["file_path"].ToString() + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Test Record Added Successfully..!";
                msSQL = " delete from sdc_tmp_tuploadscriptdocument where created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from sdc_tmp_tuploadversiondocument where created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from sdc_tmp_tuploadjsdocument where created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

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

                    msSQL = " select DISTINCT CONCAT(module_prefix, ' (', file_description, ')') as file_description, " +
                            " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                            " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" + 
                            " from sdc_trn_ttestdeployment a " +
                            " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                            " where test_gid='" + msGetGid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        file_description = objODBCDatareader["file_description"].ToString();
                        created_date = objODBCDatareader["created_date"].ToString();
                        created_by = objODBCDatareader["created_by"].ToString();
                    }
                    objODBCDatareader.Close();

                    sub = "File Moved to Test Server";


                    lscontent = values.content;

                    body = "Dear Deployers <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + " Need to Move File to the Test Server,the details are as follows,<br />";
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
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
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
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return values.status;
        }

        // Test Summary
        public void DaGetTestSummary(MdlTestSummary values)
        {
            msSQL = " select test_gid, module_gid, test_description,module_prefix, version_Major, testinprogress_flag, uat_flag," +
                    " version_enhancement, version_patch, version_bug, test_objective, testdeploy_flag, test_status, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " CASE WHEN testdeploy_flag = 'N' THEN '-' ELSE concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',date_format(a.deployed_date,'%d-%m-%Y %h:%i %p') ) END as deployed_by" +
                    " from sdc_trn_ttestdeployment a " +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid" +
                    " LEFT JOIN adm_mst_tuser c ON a.deployed_by=c.user_gid" +
                    " order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getTestList = new List<testsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getTestList.Add(new testsummary_list
                    {
                        test_gid = dt["test_gid"].ToString(),
                        module_gid = dt["module_gid"].ToString(),
                        module_prefix = dt["module_prefix"].ToString(),
                        test_objective = dt["test_objective"].ToString(),
                        version_major = dt["version_Major"].ToString(),
                        version_enhancement = dt["version_enhancement"].ToString(),
                        version_patch = dt["version_patch"].ToString(),
                        version_bug = dt["version_bug"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        deployed_by = dt["deployed_by"].ToString(),
                        test_description = dt["test_description"].ToString(),
                        testdeploy_flag = dt["testdeploy_flag"].ToString(),
                        testinprogress_flag = dt["testinprogress_flag"].ToString(),
                        test_status = dt["test_status"].ToString(),
                        uat_flag = dt["uat_flag"].ToString(),
                    });
                    values.testsummary_list = getTestList;
                }
            }
            dt_datatable.Dispose();
        }

        public bool DaPostStatusUpdate(MdlStatusUpdate values, string user_gid)
        {
            bool status = false;
          
            msSQL = " update sdc_trn_ttestdeployment set " +
                " test_status='" + values.test_status + "'," +
                " testinprogress_flag='Y'," +
                " testdeploy_flag='N'," +
                " updated_by='" + user_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where test_gid='" + values.test_gid + "'";
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

            msSQL = " update sdc_trn_ttestdeployment set " +
                " test_status='" + values.test_status + "'," +
                " testdeploy_flag='Y'," +
                " mail_flag='" + values.mail_flag + "'," +
                " deployed_by='" + user_gid + "'," +
                " deployed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where test_gid='" + values.test_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "File Deployed Successfully in Test Server..!";

                if(values.mail_flag =="Y")
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

                       
                        msSQL = "select b.employee_emailid from sdc_trn_ttestdeployment a " +
                                "left join hrm_mst_temployee b on a.created_by = b.user_gid where test_gid='" + values.test_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select DISTINCT CONCAT(module_prefix, ' (', file_description, ')') as file_description, " +
                                " date_format(a.deployed_date, '%d-%m-%Y %h:%i %p') as deployed_date, " +
                                " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as deployed_by, " +
                                " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by " +
                                " from sdc_trn_ttestdeployment a " +
                                " left join adm_mst_tuser b on a.deployed_by = b.user_gid " +
                                " left join adm_mst_tuser c on a.created_by = c.user_gid " +
                                " where test_gid='" + values.test_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            file_description = objODBCDatareader["file_description"].ToString();
                            deployed_date = objODBCDatareader["deployed_date"].ToString();
                            deployed_by = objODBCDatareader["deployed_by"].ToString();
                            created_by = objODBCDatareader["created_by"].ToString();
                        }
                        objODBCDatareader.Close();

                        sub = "File Moved to Test Server";


                        lscontent = values.content;

                        body = "Dear " + created_by + ",  <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + " File Moved to Test Server,the details are as follows,<br />";
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

        public bool DaGetMovetoUat(MdlMoveToUAT values, string user_gid)
        {
            bool status = false;

            msGetGid = objcmnfunctions.GetMasterGID("UAT");
            msSQL = " insert into sdc_trn_tuatdeployment(" +
            " uat_gid," +
            " uat_status," +
            " created_by," +
            " created_date)" +
            " values(" +
            "'" + msGetGid + "'," +
            "'Pending', " +         
            "'" + user_gid + "'," +
            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            foreach (string i in values.test_gid)
            {

                msSQL = "select test_gid, module_gid, module_prefix, version_Major, version_enhancement, version_patch, " +
                        "version_bug, test_description, test_status, file_description, script_flag " +
                        "from sdc_trn_ttestdeployment where test_gid='" + i + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {

                        msSQL = " insert into sdc_trn_tuatdeploymentdtl(" +
                        " uat_gid," +
                        " test_gid," +
                        " module_gid," +
                        " module_prefix, " +
                        " version_major," +
                        " version_enhancement," +
                        " version_patch," +
                        " version_bug," +
                        " test_description," +
                        " test_status," +
                        " file_description," +
                        " script_flag," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + i + "', " +
                        "'" + dt["module_gid"].ToString() + "'," +
                        "'" + dt["module_prefix"].ToString() + "'," +
                        "'" + dt["version_Major"].ToString() + "'," +
                        "'" + dt["version_enhancement"].ToString() + "'," +
                        "'" + dt["version_patch"].ToString() + "'," +
                        "'" + dt["version_bug"].ToString() + "'," +
                        "'" + dt["test_description"].ToString() + "'," +
                        "'" + dt["test_status"].ToString() + "'," +
                        "'" + dt["file_description"].ToString() + "'," +
                        "'" + dt["script_flag"].ToString() + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update sdc_trn_ttestdeployment set " +
                                " uat_flag='Y'," +
                                " test_status='Moved to UAT'" +
                                " where test_gid='" + i + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (dt["script_flag"].ToString() == "Y")
                        {
                            msSQL = " update sdc_trn_tuatdeployment set " +
                                " script_flag='Y'" +
                                " where uat_gid='" + msGetGid + "'";
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

                    msSQL = " select a.uat_gid,  " +
                     " group_concat(DISTINCT CONCAT(c.module_prefix, ' (', c.file_description, ')')) as file_description, " +
                     " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                     " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date FROM sdc_trn_tuatdeployment a " +
                     " LEFT JOIN sdc_trn_tuatdeploymentdtl c on c.uat_gid = a.uat_gid " +
                     " LEFT JOIN adm_mst_tuser b ON a.created_by = b.user_gid " +
                     " where a.uat_gid='" + msGetGid + "' " +
                     " group by a.uat_gid order by a.created_date desc ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        file_description = objODBCDatareader["file_description"].ToString();
                        created_date = objODBCDatareader["created_date"].ToString();
                        created_by = objODBCDatareader["created_by"].ToString();
                    }
                    objODBCDatareader.Close();

                    sub = "File Moved to UAT Server";


                    lscontent = values.content;

                    body = "Dear Deployers <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + " Need to Move File to the UAT Server,the details are as follows,<br />";
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
                values.message = "Records Moved to UAT Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return status;
        }

        public bool DaUploadjsdocument(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
        {
            uploadjs_list objdocumentmodel = new uploadjs_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;

            String path = lspath;


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/JSFile/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try

            {

                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;

                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {

                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        if (FileExtension != ".txt")
                        {
                            objfilename.status = false;
                            objfilename.message = "Only Allows .txt File Format";
                            return false;
                        }
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        lspath = path;
                        objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SDC/JSFile/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        msSQL = " insert into sdc_tmp_tuploadjsdocument( " +
                                    " file_name," +
                                    " file_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    }

                }


                msSQL = "select tmpjsdocument_gid,file_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_by,file_path " +
                        " from sdc_tmp_tuploadjsdocument a, adm_mst_tuser c where a.created_by=c.user_gid " +
                        "  and  a.created_by='" + user_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<uploadjs_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new uploadjs_list
                        {
                            file_name = (dr_datarow["file_name"].ToString()),
                            file_path = HttpContext.Current.Server.MapPath(dr_datarow["file_path"].ToString()),
                            uploaddocument_gid = (dr_datarow["tmpjsdocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString()
                        });
                    }
                    objfilename.uploadjs_list = get_filename;
                }
                dt_datatable.Dispose();

            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;
                objfilename.message = "JS File Uploaded Successfully";
                return true;

            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading";
                return false;

            }
        }

        public bool DaUploaddocument(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;

            String path = lspath;


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/Scripts/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try

            {

                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;

                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {

                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        if(FileExtension != ".txt")
                        {
                            objfilename.status = false;
                            objfilename.message = "Only Allows .txt File Format";
                            return false;
                        }
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        lspath = path;
                        objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SDC/Scripts/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                        
                        msSQL = " insert into sdc_tmp_tuploadscriptdocument( " +
                                    " file_name," +
                                    " file_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    }

                }


                msSQL = "select tmpscriptdocument_gid,file_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_by,file_path " +
                        " from sdc_tmp_tuploadscriptdocument a, adm_mst_tuser c where a.created_by=c.user_gid " +
                        "  and  a.created_by='" + user_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<upload_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new upload_list
                        {
                            file_name = (dr_datarow["file_name"].ToString()),
                            file_path = HttpContext.Current.Server.MapPath(dr_datarow["file_path"].ToString()),
                            uploaddocument_gid = (dr_datarow["tmpscriptdocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString()
                        });
                    }
                    objfilename.upload_list = get_filename;
                }
                dt_datatable.Dispose();

            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;
                objfilename.message = "Script Document Uploaded Successfully";
                return true;

            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading";
                return false;

            }
        }

        public bool DaVersionuploaddocument(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
        {
            versionupload_list objdocumentmodel = new versionupload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;

            String path = lspath;


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/VersionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try

            {

                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;

                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {

                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        if (FileExtension != ".docx")
                        {
                            objfilename.status = false;
                            objfilename.message = "Only Allows .docx File Format";
                            return false;
                        }
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        lspath = path;
                        objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SDC/VersionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        msSQL = " insert into sdc_tmp_tuploadversiondocument( " +
                                    " file_name," +
                                    " file_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    }

                }


                msSQL = "select tmpversiondocument_gid,file_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_by,file_path " +
                        " from sdc_tmp_tuploadversiondocument a, adm_mst_tuser c where a.created_by=c.user_gid " +
                        "  and  a.created_by='" + user_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<versionupload_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new versionupload_list
                        {
                            file_name = (dr_datarow["file_name"].ToString()),
                            file_path = HttpContext.Current.Server.MapPath(dr_datarow["file_path"].ToString()),
                            uploaddocument_gid = (dr_datarow["tmpversiondocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString()
                        });
                    }
                    objfilename.versionupload_list = get_filename;
                }
                dt_datatable.Dispose();

            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;
                objfilename.message = "Version Document Uploaded Successfully";
                return true;

            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading";
                return false;

            }
        }

        public void DaGetTestDelete(string test_gid, result values)
        {
            msSQL = "delete from sdc_trn_ttestdeployment where test_gid='" + test_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Test Record Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while Deleting Test Record";
            }
        }

        public void DaGetTmpDocDelete(string user_gid, result values)
        {
            msSQL = "delete from sdc_tmp_tuploadscriptdocument where created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
        }

        public void DaGetTestDeploymentView(string test_gid, MdlAddTest values)
        {

            msSQL = " select module_prefix, test_description, CASE WHEN new_pages = '' THEN 'No' ELSE new_pages END as new_pages, " +
                    " CASE WHEN new_reports = '' THEN 'No' ELSE new_pages END as new_reports, test_objective, test_status, " +
                    " CASE WHEN script_flag = 'Y' THEN 'Yes' ELSE 'No' END as script, " +
                    " CASE WHEN new_dll = '' THEN 'No' ELSE new_pages END as new_dll, CASE WHEN new_dependency = '' THEN 'No' ELSE new_pages END as new_dependency, " +
                    " CASE WHEN new_appjs = '' THEN 'No' ELSE 'Yes' END as new_appjs, file_description from sdc_trn_ttestdeployment " +
                    " where test_gid='" + test_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.module_prefix = objODBCDatareader["module_prefix"].ToString();
                values.test_description = objODBCDatareader["test_description"].ToString();
                values.new_pages = objODBCDatareader["new_pages"].ToString();
                values.new_reports = objODBCDatareader["new_reports"].ToString();
                values.test_Objective = objODBCDatareader["test_objective"].ToString();
                values.test_status = objODBCDatareader["test_status"].ToString();
                values.newdll_name = objODBCDatareader["new_dll"].ToString();
                values.dependency_name = objODBCDatareader["new_dependency"].ToString();
                values.appjs_text = objODBCDatareader["new_appjs"].ToString();
                values.filedescription = objODBCDatareader["file_description"].ToString();
                values.script = objODBCDatareader["script"].ToString();

            }
            objODBCDatareader.Close();

           
            msSQL = " select test_gid, file_name, file_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploaded_date, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as uploaded_by " +
                    " from sdc_trn_tuploadscriptdocument a " +
                    " LEFT JOIN adm_mst_tuser b on a.created_by = b.user_gid " +
                     " where a.test_gid='" + test_gid + "' order by test_gid desc";
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

            msSQL = " select test_gid, file_name, file_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploaded_date, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as uploaded_by " +
                    " from sdc_trn_tuploadversiondocument a " +
                    " LEFT JOIN adm_mst_tuser b on a.created_by = b.user_gid " +
                     " where a.test_gid='" + test_gid + "' order by test_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getuploaddocuments = new List<versionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getuploaddocuments.Add(new versionupload_list
                    {
                        file_name = dt["file_name"].ToString(),
                        file_path = HttpContext.Current.Server.MapPath(dt["file_path"].ToString()),
                        uploaded_date = dt["uploaded_date"].ToString(),
                        uploaded_by = dt["uploaded_by"].ToString(),
                    });
                    values.versionupload_list = getuploaddocuments;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select test_gid, file_name, file_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploaded_date, " +
                   " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as uploaded_by " +
                   " from sdc_trn_tuploadjsdocument a " +
                   " LEFT JOIN adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where a.test_gid='" + test_gid + "' order by test_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getjsdocuments = new List<uploadjs_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getjsdocuments.Add(new uploadjs_list
                    {
                        file_name = dt["file_name"].ToString(),
                        file_path = HttpContext.Current.Server.MapPath(dt["file_path"].ToString()),
                        uploaded_date = dt["uploaded_date"].ToString(),
                        uploaded_by = dt["uploaded_by"].ToString(),
                    });
                    values.uploadjs_list = getjsdocuments;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select customer_gid, customer_name from sdc_trn_ttest2customer " +
                    " where test_gid='" + test_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer = new List<customer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcustomer.Add(new customer_list
                    {
                        customer_name = dt["customer_name"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                    });
                    values.customer_list = getcustomer;
                }
            }
            dt_datatable.Dispose();

        }

        public void DaGettmpJsDocumentDelete(string uploaddocument_gid, result values, uploaddocument objfilename, string user_gid)
        {
            msSQL = " delete from sdc_tmp_tuploadjsdocument where tmpjsdocument_gid='" + uploaddocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = "select tmpjsdocument_gid,file_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_by,file_path " +
                    " from sdc_tmp_tuploadjsdocument a, adm_mst_tuser c where a.created_by=c.user_gid " +
                    " and  a.created_by='" + user_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<uploadjs_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new uploadjs_list
                    {
                        file_name = (dr_datarow["file_name"].ToString()),
                        file_path = HttpContext.Current.Server.MapPath(dr_datarow["file_path"].ToString()),
                        uploaddocument_gid = (dr_datarow["tmpjsdocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString()
                    });
                }
                objfilename.uploadjs_list = get_filename;
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGettmpDocumentDelete(string uploaddocument_gid, result values, uploaddocument objfilename, string user_gid)
        {
            msSQL = " delete from sdc_tmp_tuploadscriptdocument where tmpscriptdocument_gid='" + uploaddocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = "select tmpscriptdocument_gid,file_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_by,file_path " +
                    " from sdc_tmp_tuploadscriptdocument a, adm_mst_tuser c where a.created_by=c.user_gid " +
                    " and  a.created_by='" + user_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new upload_list
                    {
                        file_name = (dr_datarow["file_name"].ToString()),
                        file_path = HttpContext.Current.Server.MapPath(dr_datarow["file_path"].ToString()),
                        uploaddocument_gid = (dr_datarow["tmpscriptdocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString()
                    });
                }
                objfilename.upload_list = get_filename;
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetversiontmpDocumentDelete(string uploaddocument_gid, result values, uploaddocument objfilename, string user_gid)
        {
            msSQL = " delete from sdc_tmp_tuploadversiondocument where tmpversiondocument_gid='" + uploaddocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = "select tmpversiondocument_gid,file_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_by,file_path " +
                    " from sdc_tmp_tuploadversiondocument a, adm_mst_tuser c where a.created_by=c.user_gid " +
                    " and  a.created_by='" + user_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<versionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new versionupload_list
                    {
                        file_name = (dr_datarow["file_name"].ToString()),
                        file_path = HttpContext.Current.Server.MapPath(dr_datarow["file_path"].ToString()),
                        uploaddocument_gid = (dr_datarow["tmpversiondocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString()
                    });
                }
                objfilename.versionupload_list = get_filename;
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }
    }
}