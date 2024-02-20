using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.system.Models;
using System.Configuration;
using System.IO;
using System.Text;

namespace ems.system.DataAccess
{

    public class DaTriggerUser
    {
        
        OdbcDataReader objODBCDataReader;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult;
        
      
        result objResult = new result();
        string triggerGID = string.Empty;
        //Trigger user

        public void DaGetTriggerUser(MdlTriggerUser objtriggeruser)
        {
            try
            {
                msSQL = " SELECT triggeruser_gid ,triggeruser_name ,a.remarks  ,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_ttriggeruser a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.triggeruser_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettrigger_list = new List<trigger_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettrigger_list.Add(new trigger_list
                        {
                            triggeruser_gid = (dr_datarow["triggeruser_gid"].ToString()),
                            triggeruser_name = (dr_datarow["triggeruser_name"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objtriggeruser.trigger_list = gettrigger_list;
                }
                dt_datatable.Dispose();
                objtriggeruser.status = true;
            }
            catch
            {
                objtriggeruser.status = false;
            }
        }

        public void DaCreateTriggerUser(triggeruser values, string employee_gid)
        {
              if (values.trigger_list != null)
            {
                for (var i = 0; i < values.trigger_list.Count; i++)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("TUST");
                    msSQL = " insert into sys_mst_ttriggeruser(" +
                            " triggeruser_gid ," +
                            " trigger_gid ," +
                            " triggeruser_name ," +
                            " remarks  ," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                             "'" + values.trigger_list[i].employee_gid + "'," +
                            "'" + values.trigger_list[i].employee_name + "',";

                    if (values.remarks == "" || values.remarks == null )
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.remarks.Replace("'", "") + "',";
                    }

                    msSQL += "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                
            }
                       
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Trigger User Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }

        public void DaDeleteTriggerUser(string triggeruser_gid, triggeruser values ,string employee_gid)
        {
            msSQL = " update sys_mst_ttriggeruser set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where triggeruser_gid='" + triggeruser_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                
                values.status = true;
                values.message = "Trigger User Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        
            }

        public void DaInactiveTrigger(triggeruser values, string employee_gid)
        {
            msSQL = " update sys_mst_ttriggeruser set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where triggeruser_gid='" + values.triggeruser_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("TUSI");

                msSQL = " insert into sys_mst_ttriggeruserinactivelog (" +
                      "triggeruserinactivelog_gid," +
                      "triggeruser_gid," +
                      //  " trigger_gid," +
                      //" triggeruser_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.triggeruser_gid + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Trigger Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Trigger Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaTriggerInactiveLogview(string triggeruser_gid, triggeruser values)
        {
            try
            {
                msSQL = " SELECT triggeruser_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_ttriggeruserinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where triggeruser_gid ='" + triggeruser_gid + "' order by a.triggeruserinactivelog_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettrigger_list = new List<trigger_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettrigger_list.Add(new trigger_list
                        {
                            triggeruser_gid = (dr_datarow["triggeruser_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.trigger_list = gettrigger_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetEmployee(MdlTriggerUser objtriggeruser)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                        " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                        " WHERE user_status<>'N' AND b.employee_gid NOT IN (SELECT trigger_gid FROM sys_mst_ttriggeruser)" +
                        " ORDER BY a.user_firstname ASC";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<trigger_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objtriggeruser.trigger_list = dt_datatable.AsEnumerable().Select(row =>
                      new trigger_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objtriggeruser.status = true;
            }
            catch (Exception ex)
            {
                objtriggeruser.status = false;
            }


        }
    }
}