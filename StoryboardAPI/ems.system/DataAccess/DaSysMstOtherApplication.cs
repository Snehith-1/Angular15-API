using System;
using System.Collections.Generic;
using ems.system.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;

namespace ems.system.DataAccess
{
    public class DaOtherApplication 
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        DataTable dt_datatable;
        string msSQL, msGetGid, lsemployee_gid;
        int mnResult;
        private string lsapplication_name;
        private string lsdescription;
        private string lsurl;
        private string lsteam_name;
        private string lscreated_by;
        private string lsphone_no;
        private string tomail_id;
        private string sub;
        private string body;
        private string cc_mailid;
        private string ls_username;
        private int ls_port;
        private string ls_server;
        public string ls_password;
        public string employee_mail, to;
        private string assign_status;

        // Get Other Applications
        public void DaGetOtherApplication(MdlOtherApplication objMdlOtherApplication)
        {
            try
            {
                msSQL = " SELECT a.otherapplication_gid,a.otherapplication_name,a.url,a.description,a.status_log, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by, " +
                    " assign_status from sys_mst_totherapplication a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by case when  a.updated_date > a.created_date then a.updated_date else a.created_date end desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var otherapplication = new List<otherapplication>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        otherapplication.Add(new otherapplication
                        {
                            otherapplication_gid = (dr_datarow["otherapplication_gid"].ToString()),
                            otherapplication_name = (dr_datarow["otherapplication_name"].ToString()),
                            url = (dr_datarow["url"].ToString()),
                            description = (dr_datarow["description"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            assign_status = (dr_datarow["assign_status"].ToString()),
                        });
                    }
                    objMdlOtherApplication.otherapplication_list = otherapplication;
                }
                dt_datatable.Dispose();
                objMdlOtherApplication.status = true;
            }
            catch
            {
                objMdlOtherApplication.status = false;
            }
        }

        //Create Other Application
        public void DaCreateOtherApplication(otherapplication values, string employee_gid)
        {
            msSQL = "select * from sys_mst_totherapplication where otherapplication_name ='" + values.otherapplication_name.Replace("'", "") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if(objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                msSQL = "select * from sys_mst_totherapplication where url ='" + values.url.Replace("'", "") + "'";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == false)
                {
                    objODBCDatareader1.Close();
                    msGetGid = objcmnfunctions.GetMasterGID("SOAT");
                    msSQL = " insert into sys_mst_totherapplication(" +
                            " otherapplication_gid," +
                            " otherapplication_name," +
                            " url," +
                            " description," +
                            " created_by," +
                            " created_date," +
                            " assign_status)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.otherapplication_name.Replace("'", "") + "'," +
                            "'" + values.url.Replace("'", "") + "'," +
                            "'" + values.description.Replace("'", "") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + values.assign_status + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Other Application Created Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured while Create";
                    }
                }
                else
                {
                    objODBCDatareader1.Close();
                    values.status = false;
                    values.message = "URL already created...";
                }
            }
            else
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Other Application name already created...";
            }
            
        }

        //Edit Other Applications
        public void DaEditOtherApplication(string otherapplication_gid, otherapplication values)
        {
            try
            {
                msSQL = " SELECT otherapplication_gid,otherapplication_name,url, description, status_log FROM sys_mst_totherapplication where otherapplication_gid='" + otherapplication_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.otherapplication_gid = objODBCDatareader["otherapplication_gid"].ToString();
                    values.otherapplication_name = objODBCDatareader["otherapplication_name"].ToString();
                    values.url = objODBCDatareader["url"].ToString();
                    values.description = objODBCDatareader["description"].ToString();
                    values.status_log = objODBCDatareader["status_log"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        //Update Other Applications
        public void DaUpdateOtherApplication(string employee_gid, otherapplication values)
        {
            msSQL = " update sys_mst_totherapplication set " +
                 " otherapplication_name='" + values.otherapplication_name + "'," +
                 " url='" + values.url + "'," +
                 " description='" + values.description + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where otherapplication_gid='" + values.otherapplication_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SOAL");

                msSQL = " insert into sys_mst_totherapplicationlog (" +
                       " otherapplication_loggid, " +
                       " otherapplication_gid, " +
                       " otherapplication_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.otherapplication_gid + "'," +
                       " '" + values.otherapplication_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Other Application Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating Other Application";
            }
        }

        // Inactive Other Applications
        public void DaInactiveOtherApplication(otherapplication values, string employee_gid)
        {
            msSQL = " update sys_mst_totherapplication set status_log='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where otherapplication_gid='" + values.otherapplication_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SOAI");

                msSQL = " insert into sys_mst_totherapplicationinactivelog (" +
                      " otherapplicationinactivelog_gid, " +
                      " otherapplication_gid," +
                      " otherapplication_name," +
                      " status_log," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.otherapplication_gid + "'," +
                      " '" + values.otherapplication_name.Replace("'", "") + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Other Application Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Other Appplication Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        //Other ApplicationInactivate History
        public void DaInactiveOtherApplicationHistory(MdlOtherApplication objhistory, string otherapplication_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status_log='N' then 'Inactive' else 'Active' end as status" +
                        " from sys_mst_totherapplicationinactivelog a " +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                        " where a.otherapplication_gid='" + otherapplication_gid + "' order by a.otherapplicationinactivelog_gid desc  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinactivehistory_list = new List<otherapplication>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinactivehistory_list.Add(new otherapplication
                        {
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            status_log = (dr_datarow["status"].ToString())
                        });
                    }
                    objhistory.otherapplication_list = getinactivehistory_list;
                }
                dt_datatable.Dispose();
                objhistory.status = true;
            }
            catch(Exception e)
            {
                objhistory.status = false;
            }
        }

        //Employee list
        public void DaGetEmployee(MdlEmployeeassign objemployee, string otherapplication_gid)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where  a.user_status<> 'N' and b.employee_gid not in (select employee_gid from sys_mst_tassignotherapplication where otherapplication_gid='" + otherapplication_gid + "') " +
                   " order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employeeasssign_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objemployee.employeeasssign_list = dt_datatable.AsEnumerable().Select(row =>
                      new employeeasssign_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }


        }
        // Assinged Employee list
        public void DaGetAssingedEmployee(MdlEmployeeassign objemployee, string otherapplication_gid)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid,c.assignotherapplication_gid from adm_mst_tuser a " +
                        " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                        " LEFT JOIN sys_mst_tassignotherapplication c ON b.employee_gid=c.employee_gid " +
                        " where  a.user_status<> 'N' and c.otherapplication_gid='" + otherapplication_gid + "' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employeeasssign_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objemployee.employeeasssign_list = dt_datatable.AsEnumerable().Select(row =>
                      new employeeasssign_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString(),
                          assignotherapplication_gid= row["assignotherapplication_gid"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }
        }

        //Assign Member
        public void DaAssignmember(Mdlassignmember values, string user_gid, string employee_gid)
        {
            foreach (string i in values.employeelist_gid)
            {
                string lsemployee_name;
                msSQL = "SELECT concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name FROM hrm_mst_temployee a left join adm_mst_tuser b " +
                        " on a.user_gid=b.user_gid " +
                        "WHERE  a.user_status<> 'N' and a.employee_gid='" + i + "'";
                lsemployee_name = objdbconn.GetExecuteScalar(msSQL);
                msGetGid = objcmnfunctions.GetMasterGID("SOAA");
                msSQL = " INSERT INTO sys_mst_tassignotherapplication(" +
                        " assignotherapplication_gid," +
                        " otherapplication_gid," +
                        " employee_gid," +
                        " member_name," +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetGid + "'," +
                        "'" + values.otherapplication_gid + "'," +
                        "'" + i + "'," +
                        "'" + lsemployee_name + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                try
                {
                    msSQL = "select otherapplication_name from sys_mst_totherapplication where otherapplication_gid='" + values.otherapplication_gid + "'";
                    lsapplication_name = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select description from sys_mst_totherapplication where otherapplication_gid='" + values.otherapplication_gid + "'";
                    lsdescription = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select url from sys_mst_totherapplication where otherapplication_gid='" + values.otherapplication_gid + "'";
                    lsurl = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select b.department_name from hrm_mst_temployee a " +
                    " left join hrm_mst_tdepartment b on a.department_gid = b.department_gid " +
                    " left join sys_mst_totherapplication c on c.created_by = a.employee_gid where c.otherapplication_gid = '" + values.otherapplication_gid + "'";
                    lsteam_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as created_by " +
                    " from adm_mst_tuser a " +
                    " left join hrm_mst_temployee b ON a.user_gid = b.user_gid " +
                    " left join sys_mst_totherapplication c ON b.employee_gid = c.created_by where c.otherapplication_gid = '" + values.otherapplication_gid + "'";
                    lscreated_by = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select employee_mobileno from hrm_mst_temployee where employee_gid='" + employee_gid + "'";
                    lsphone_no = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid='" + i + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                            " FROM adm_mst_tcompany";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        ls_server = objODBCDatareader1["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                        ls_username = objODBCDatareader1["pop_username"].ToString();
                        ls_password = objODBCDatareader1["pop_password"].ToString();
                    }
                    objODBCDatareader1.Close();

                    sub = lsapplication_name;
                    body = "<br />";
                    body = body + "Greetings! <br />";
                    body = body + "<br />";
                    body = body + "<b> Description: </b><br /> " + lsdescription + " <br />";
                    body = body + "<br />";
                    body = body + "Click here to login: <a href=" + lsurl + ">"+ lsurl + "</a>  <br />";
                    body = body + "<br />";
                    body = body + "Regards <br />";
                    body = body + "" + lsteam_name + "  <br />";
                    body = body + "<br />";
                    body = body + "For further details reach to the person below,  <br />";
                    body = body + "<br />";
                    body = body + "" + lscreated_by + "  <br />";
                    body = body + "" + lsphone_no + "  <br />";
                    body = body + "<br />";



                    cc_mailid = "";
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

            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Other Application Member Assigned Successfully...";

            }
            else
            {
                values.status = true;
                values.message = "Error Occured";

            }
        }

        //Unassign Member
        public void DaGetAssignmemberDelete(Mdlassignmember values)
        {
            foreach (string i in values.employeelist_gid)
            {
                msSQL = " delete from sys_mst_tassignotherapplication where employee_gid = '" + i +"' and otherapplication_gid = '" + values.otherapplication_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Members UnAssigned Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }

        //Assigned person list
        public void DaAssignedlinks(MdlOtherApplication objMdlOtherApplication, string user_gid)
        {
            try
            {   msSQL= "select employee_gid from hrm_mst_temployee where user_gid = '"+ user_gid + "'";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select a.url, a.otherapplication_name,a.otherapplication_gid,a.assign_status,a.status_log " +
                        " from sys_mst_totherapplication a "+
                        " left join sys_mst_tassignotherapplication b on b.otherapplication_gid = a.otherapplication_gid" +
                        " where  b.employee_gid = '"+ lsemployee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var otherapplication = new List<otherapplication>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        otherapplication.Add(new otherapplication
                        {
                            otherapplication_gid = (dr_datarow["otherapplication_gid"].ToString()),
                            otherapplication_name = (dr_datarow["otherapplication_name"].ToString()),
                            url = (dr_datarow["url"].ToString()),
                            status_log= (dr_datarow["status_log"].ToString()),
                            assign_status = (dr_datarow["assign_status"].ToString()),
                        });
                    }
                    objMdlOtherApplication.otherapplication_list = otherapplication;
                }
                dt_datatable.Dispose();
                objMdlOtherApplication.status = true;
            }
            catch
            {
                objMdlOtherApplication.status = false;
            }
            
        }
    }
}