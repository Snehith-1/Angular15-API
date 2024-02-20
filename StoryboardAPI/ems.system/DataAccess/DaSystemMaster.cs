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

    public class DaSystemMaster
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, msGet_LocationGid, clusterGID, msGet_clusterGid, regionGID, msGet_regionGid, msGetTaskCode,
            msGetTask2AssignedToGid, msGetTask2EscalationMailToGid;
        int mnResult, mnResultSub1, mnResultSub2;
        string lscluster_gid, lsvertical_gid, lsregion_gid, lsvertical_code;
        string lsmaster_value;
        string lstask_gid, mspSQL;

        //Blood Group

        public void DaGetBloodGroup(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.bloodgroup_gid ,a.bloodgroup_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tbloodgroup a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.bloodgroup_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            bloodgroup_gid = (dr_datarow["bloodgroup_gid"].ToString()),
                            bloodgroup_name = (dr_datarow["bloodgroup_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateBloodGroup(master values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("SBGT");
            msSQL = " insert into sys_mst_tbloodgroup(" +
                    " bloodgroup_gid ," +
                    " lms_code," +
                    " bureau_code," +
                    " bloodgroup_name ," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.bloodgroup_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Blood Group Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }

        public void DaEditBloodGroup(string bloodgroup_gid, master values)
        {
            try
            {
                msSQL = " SELECT bloodgroup_gid,bloodgroup_name,lms_code, bureau_code, status as Status FROM sys_mst_tbloodgroup " +
                        " where bloodgroup_gid='" + bloodgroup_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.bloodgroup_gid = objODBCDatareader["bloodgroup_gid"].ToString();
                    values.bloodgroup_name = objODBCDatareader["bloodgroup_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_bloodgroup = objODBCDatareader["status_bloodgroup"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateBloodGroup(string employee_gid, master values)
        {
            msSQL = "select updated_by, updated_date,bloodgroup_name from sys_mst_tbloodgroup where bloodgroup_gid ='" + values.bloodgroup_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SBGL");
                    msSQL = " insert into sys_mst_tbloodgrouplog(" +
                              " bloodgroup_loggid   ," +
                              " bloodgroup_gid," +
                              " bloodgroup_name , " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.bloodgroup_gid + "'," +
                              "'" + objODBCDatareader["bloodgroup_name "].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update sys_mst_tbloodgroup set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " bloodgroup_name='" + values.bloodgroup_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where bloodgroup_gid='" + values.bloodgroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Blood Group updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }

        public void DaInactiveBloodGroup(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tbloodgroup set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where bloodgroup_gid='" + values.bloodgroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SBGI");

                msSQL = " insert into sys_mst_tbloodgroupinactivelog (" +
                      " bloodgroupinactivelog_gid   , " +
                      " bloodgroup_gid," +
                      " bloodgroup_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.bloodgroup_gid + "'," +
                      " '" + values.bloodgroup_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Blood Group Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Blood Group Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteBloodGroup(string bloodgroup_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tbloodgroup   set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where bloodgroup_gid='" + bloodgroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Blood Group Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaBloodGroupInactiveLogview(string bloodgroup_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT bloodgroup_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tbloodgroupinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where bloodgroup_gid ='" + bloodgroup_gid + "' order by a.bloodgroupinactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            bloodgroup_gid = (dr_datarow["bloodgroup_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        //Base Location

        public void DaGetBaseLocation(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.baselocation_gid ,a.baselocation_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tbaselocation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.baselocation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            baselocation_name = (dr_datarow["baselocation_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetBaseLocationlist(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.baselocation_gid ,a.baselocation_name " +
                        " FROM sys_mst_tbaselocation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.baselocation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlocation_list = new List<location_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlocation_list.Add(new location_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            baselocation_name = (dr_datarow["baselocation_name"].ToString()),

                        });
                    }
                    objmaster.location_list = getlocation_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateBaseLocation(master values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("SBLT");
            msSQL = " insert into sys_mst_tbaselocation(" +
                    " baselocation_gid ," +
                    " lms_code," +
                    " bureau_code," +
                    " baselocation_name ," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.baselocation_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Base Location Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }

        public void DaEditBaseLocation(string baselocation_gid, master values)
        {
            try
            {
                msSQL = " SELECT baselocation_gid,baselocation_name,lms_code, bureau_code, status as Status FROM sys_mst_tbaselocation " +
                        " where baselocation_gid='" + baselocation_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.baselocation_gid = objODBCDatareader["baselocation_gid"].ToString();
                    values.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_baselocation = objODBCDatareader["status_baselocation"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateBaseLocation(string employee_gid, master values)
        {
            msSQL = "select updated_by, updated_date,baselocation_name from sys_mst_tbaselocation where baselocation_gid ='" + values.baselocation_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SBLL");
                    msSQL = " insert into sys_mst_tbaselocationlog(" +
                              " baselocation_loggid," +
                              " baselocation_gid," +
                              " baselocation_name , " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.baselocation_gid + "'," +
                              "'" + objODBCDatareader["baselocation_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update sys_mst_tbaselocation set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " baselocation_name='" + values.baselocation_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where baselocation_gid='" + values.baselocation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Base Location updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }

        public void DaInactiveBaseLocation(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tbaselocation set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where baselocation_gid='" + values.baselocation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SBLI");

                msSQL = " insert into sys_mst_tbaselocationinactivelog (" +
                      " baselocationinactivelog_gid   , " +
                      " baselocation_gid," +
                      " baselocation_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.baselocation_gid + "'," +
                      " '" + values.baselocation_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Base Location Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Base Location  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteBaseLocation(string baselocation_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tbaselocation  set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where baselocation_gid='" + baselocation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Base Location Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaBaseLocationInactiveLogview(string baselocation_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT baselocation_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tbaselocationinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where baselocation_gid ='" + baselocation_gid + "' order by a.baselocationinactivelog_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        //physical status

        public void DaGetPhysicalStatus(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.physicalstatus_gid,a.physicalstatus_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tphysicalstatus a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.physicalstatus_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            physicalstatus_gid = (dr_datarow["physicalstatus_gid"].ToString()),
                            physicalstatus_name = (dr_datarow["physicalstatus_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreatePhysicalStatus(master values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("SPST");
            msSQL = " insert into sys_mst_tphysicalstatus(" +
                    " physicalstatus_gid," +
                    " lms_code," +
                    " bureau_code," +
                    " physicalstatus_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.physicalstatus_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Physical Status Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }

        public void DaEditPhysicalStatus(string physicalstatus_gid, master values)
        {
            try
            {
                msSQL = " SELECT physicalstatus_gid,physicalstatus_name,lms_code, bureau_code, status as Status FROM sys_mst_tphysicalstatus " +
                        " where physicalstatus_gid='" + physicalstatus_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.physicalstatus_gid = objODBCDatareader["physicalstatus_gid"].ToString();
                    values.physicalstatus_name = objODBCDatareader["physicalstatus_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_physicalstatus = objODBCDatareader["status_physicalstatus"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdatePhysicalStatus(string employee_gid, master values)
        {
            msSQL = "select updated_by, updated_date,physicalstatus_name from sys_mst_tphysicalstatus where physicalstatus_gid ='" + values.physicalstatus_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SPSL");
                    msSQL = " insert into sys_mst_tphysicalstatuslog(" +
                              " physicalstatus_loggid," +
                              " physicalstatus_gid," +
                              " physicalstatus_name," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.physicalstatus_gid + "'," +
                              "'" + objODBCDatareader["physicalstatus_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update sys_mst_tphysicalstatus set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " physicalstatus_name='" + values.physicalstatus_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where physicalstatus_gid='" + values.physicalstatus_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Physical Status updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }

        public void DaInactivePhysicalStatus(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tphysicalstatus set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where physicalstatus_gid='" + values.physicalstatus_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SPSI");

                msSQL = " insert into sys_mst_tphysicalstatusinactivelog (" +
                      " physicalstatusinactivelog_gid   , " +
                      " physicalstatus_gid," +
                      " physicalstatus_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.physicalstatus_gid + "'," +
                      " '" + values.physicalstatus_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Physical Status Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Physical Status Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeletePhysicalStatus(string physicalstatus_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tphysicalstatus  set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where physicalstatus_gid='" + physicalstatus_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Physical Status Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaPhysicalStatusInactiveLogview(string physicalstatus_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT physicalstatus_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tphysicalstatusinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where physicalstatus_gid ='" + physicalstatus_gid + "' order by a.physicalstatusinactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            physicalstatus_gid = (dr_datarow["physicalstatus_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        //Calendar Group

        public void DaGetCalendarGroup(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.calendargroup_gid ,a.calendargroup_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tcalendargroup a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.calendargroup_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            calendargroup_gid = (dr_datarow["calendargroup_gid"].ToString()),
                            calendargroup_name = (dr_datarow["calendargroup_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateCalendarGroup(master values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("SCGT");
            msSQL = " insert into sys_mst_tcalendargroup(" +
                    " calendargroup_gid ," +
                    " lms_code," +
                    " bureau_code," +
                    " calendargroup_name ," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.calendargroup_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Calendar Group Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }

        public void DaEditCalendarGroup(string calendargroup_gid, master values)
        {
            try
            {
                msSQL = " SELECT calendargroup_gid,calendargroup_name,lms_code, bureau_code, status as Status FROM sys_mst_tcalendargroup " +
                        " where calendargroup_gid='" + calendargroup_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.calendargroup_gid = objODBCDatareader["calendargroup_gid"].ToString();
                    values.calendargroup_name = objODBCDatareader["calendargroup_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_calendargroup = objODBCDatareader["status_calendargroup"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateCalendarGroup(string employee_gid, master values)
        {
            msSQL = "select updated_by, updated_date,calendargroup_name from sys_mst_tcalendargroup where calendargroup_gid ='" + values.calendargroup_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SCGL");
                    msSQL = " insert into sys_mst_tcalendargrouplog(" +
                              " calendargroup_loggid   ," +
                              " calendargroup_gid," +
                              " calendargroup_name , " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.calendargroup_gid + "'," +
                              "'" + objODBCDatareader["calendargroup_name "].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update sys_mst_tcalendargroup set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " calendargroup_name='" + values.calendargroup_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where calendargroup_gid='" + values.calendargroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Calendar Group updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }

        public void DaInactiveCalendarGroup(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tcalendargroup set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where calendargroup_gid='" + values.calendargroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SCGI");

                msSQL = " insert into sys_mst_tcalendargroupinactivelog (" +
                      " calendargroupinactivelog_gid   , " +
                      " calendargroup_gid," +
                      " calendargroup_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.calendargroup_gid + "'," +
                      " '" + values.calendargroup_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Calendar Group Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Calendar Group Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteCalendarGroup(string calendargroup_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tcalendargroup  set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where calendargroup_gid='" + calendargroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Calendar Group Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaCalendarGroupInactiveLogview(string calendargroup_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT calendargroup_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tcalendargroupinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where calendargroup_gid ='" + calendargroup_gid + "' order by a.calendargroupinactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            calendargroup_gid = (dr_datarow["calendargroup_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        //Client Role

        public void DaGetClientRole(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.clientrole_gid  ,a.clientrole_name ,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tclientrole a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.clientrole_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            clientrole_gid = (dr_datarow["clientrole_gid"].ToString()),
                            clientrole_name = (dr_datarow["clientrole_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }

            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }

        public void DaCreateClientRole(master values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("SCRT");
            msSQL = " insert into sys_mst_tclientrole(" +
                    " clientrole_gid  ," +
                    " lms_code," +
                    " bureau_code," +
                    " clientrole_name  ," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.clientrole_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Client Role Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }

        public void DaEditClientRole(string clientrole_gid, master values)
        {
            try
            {
                msSQL = " SELECT clientrole_gid ,clientrole_name ,lms_code, bureau_code, status as Status FROM sys_mst_tclientrole " +
                        " where clientrole_gid ='" + clientrole_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.clientrole_gid = objODBCDatareader["clientrole_gid"].ToString();
                    values.clientrole_name = objODBCDatareader["clientrole_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_clientrole = objODBCDatareader["status_clientrole"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateClientRole(string employee_gid, master values)
        {
            msSQL = "select updated_by, updated_date,clientrole_name  from sys_mst_tclientrole where clientrole_gid  ='" + values.clientrole_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SCRL");
                    msSQL = " insert into sys_mst_tclientrolelog(" +
                              " clientrole_loggid   ," +
                              " clientrole_gid ," +
                              " clientrole_name  , " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.clientrole_gid + "'," +
                              "'" + objODBCDatareader["clientrole_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update sys_mst_tclientrole set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " clientrole_name ='" + values.clientrole_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where clientrole_gid ='" + values.clientrole_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Client Role updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }

        public void DaInactiveClientRole(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tclientrole set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where clientrole_gid ='" + values.clientrole_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SCRI");

                msSQL = " insert into sys_mst_tclientroleinactivelog (" +
                      " clientroleinactivelog_gid   , " +
                      " clientrole_gid ," +
                      " clientrole_name  ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.clientrole_gid + "'," +
                      " '" + values.clientrole_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Client Role Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Client Role Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteClientRole(string clientrole_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tclientrole  set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where clientrole_gid='" + clientrole_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Client Role Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaClientRoleInactiveLogview(string clientrole_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT clientrole_gid ,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tclientroleinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where clientrole_gid  ='" + clientrole_gid + "' order by a.clientroleinactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            clientrole_gid = (dr_datarow["clientrole_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        //Salutation

        public void DaGetSalutation(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.salutation_gid ,a.salutation_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tsalutation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.salutation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            salutation_gid = (dr_datarow["salutation_gid"].ToString()),
                            salutation_name = (dr_datarow["salutation_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateSalutation(master values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("SSMT");
            msSQL = " insert into sys_mst_tsalutation(" +
                    " salutation_gid," +
                    " lms_code," +
                    " bureau_code," +
                    " salutation_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.salutation_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Salutation Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }

        public void DaEditSalutation(string salutation_gid, master values)
        {
            try
            {
                msSQL = " SELECT salutation_gid,salutation_name ,lms_code, bureau_code, status as Status FROM sys_mst_tsalutation " +
                        " where salutation_gid='" + salutation_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.salutation_gid = objODBCDatareader["salutation_gid"].ToString();
                    values.salutation_name = objODBCDatareader["salutation_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_salutation = objODBCDatareader["status_salutation"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateSalutation(string employee_gid, master values)
        {
            msSQL = "select updated_by, updated_date,salutation_name  from sys_mst_tsalutation where salutation_gid ='" + values.salutation_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SSML");
                    msSQL = " insert into sys_mst_tsalutationlog(" +
                              " salutation_loggid    ," +
                              " salutation_gid," +
                              " salutation_name, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.salutation_gid + "'," +
                              "'" + objODBCDatareader["salutation_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update sys_mst_tsalutation set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " salutation_name ='" + values.salutation_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where salutation_gid='" + values.salutation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Salutation updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }

        public void DaInactiveSalutation(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tsalutation set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where salutation_gid ='" + values.salutation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SSMI");

                msSQL = " insert into sys_mst_tsalutationinactivelog (" +
                      " salutationinactivelog_gid, " +
                      " salutation_gid," +
                      " salutation_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.salutation_gid + "'," +
                      " '" + values.salutation_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Salutation Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Salutation  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteSalutation(string salutation_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tsalutation  set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where salutation_gid='" + salutation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Salutation Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaSalutationInactiveLogview(string salutation_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT salutation_gid ,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tsalutationinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where salutation_gid  ='" + salutation_gid + "' order by a.salutationinactivelog_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            salutation_gid = (dr_datarow["salutation_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        //PROJECT

        // Add Project
        public void DaGetProject(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.project_gid ,a.project,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tproject a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.project_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            project_gid = (dr_datarow["project_gid"].ToString()),
                            project_name = (dr_datarow["project"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateProject(master values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("SMPR");
            msSQL = " insert into sys_mst_tproject(" +
                    " project_gid ," +
                    " lms_code," +
                    " bureau_code," +
                    " project," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.project_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Project Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }

        //  Edit Project 

        public void DaEditProject(string project_gid, master values)
        {
            try
            {
                msSQL = " SELECT project_gid,project,lms_code, bureau_code, status as status FROM sys_mst_tproject " +
                        " where project_gid='" + project_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.project_gid = objODBCDatareader["project_gid"].ToString();
                    values.project_name = objODBCDatareader["project"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_project = objODBCDatareader["status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateProject(string employee_gid, master values)
        {
            msSQL = "select updated_by, updated_date,project from sys_mst_tproject where project_gid ='" + values.project_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("PRLG");
                    msSQL = " insert into sys_mst_tprojectlogid(" +
                              " projectlog_gid," +
                              " project_gid," +
                              " project , " +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.project_gid + "'," +
                              "'" + objODBCDatareader["project"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update sys_mst_tproject set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " project='" + values.project_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where project_gid='" + values.project_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Project updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }

        //Project Status

        public void DaInactiveProject(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tproject set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where project_gid='" + values.project_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SMPI");

                msSQL = " insert into sys_mst_tprojectinactivelog (" +
                      " projectinactivelog_gid   , " +
                      " project_gid," +
                      " project ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date)" +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.project_gid + "'," +
                      " '" + values.project_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Project Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Project  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaProjectInactiveLogview(string project_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT project_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status, a.remarks" +
                        " FROM sys_mst_tprojectinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where project_gid ='" + project_gid + "' order by a.projectinactivelog_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            project_gid = (dr_datarow["project_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        // Delete Project 

        public void DaDeleteProject(string project_gid, string employee_gid, result values)
        {
            msSQL = " select project  from sys_mst_tproject where project_gid='" + project_gid + "'";
            lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from sys_mst_tproject where project_gid ='" + project_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Project Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                         "master_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'project '," +
                         "'" + lsmaster_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostClusterAdd(cluster values, string employee_gid)
        {
            msSQL = "select cluster_name from sys_mst_tclustermapping where cluster_name = '" + values.cluster_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Cluster Name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("CLST");
                msSQL = " insert into sys_mst_tclustermapping(" +
                        " cluster_gid ," +
                        " cluster_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.cluster_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.locationlist.Count; i++)
                {
                    msGet_LocationGid = objcmnfunctions.GetMasterGID("CL2L");

                    msSQL = "Insert into sys_mst_tcluster2baselocation( " +
                           " cluster2baselocation_gid, " +
                           " cluster_gid," +
                           " baselocation_gid," +
                           " baselocation_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_LocationGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.locationlist[i].baselocation_gid + "'," +
                           "'" + values.locationlist[i].baselocation_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Cluster Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }

        public void DaGetClusterSummary(cluster objmaster)
        {
            try
            {
                msSQL = " SELECT a.cluster_gid ,a.cluster_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tclustermapping a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcluster_list = new List<cluster_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcluster_list.Add(new cluster_list
                        {
                            cluster_gid = (dr_datarow["cluster_gid"].ToString()),
                            cluster_name = (dr_datarow["cluster_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString())
                        });
                    }
                    objmaster.cluster_list = getcluster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetClusterEdit(string cluster_gid, cluster objmaster)
        {
            msSQL = " select cluster_gid,cluster_name, status as status from sys_mst_tclustermapping " +
                    " where cluster_gid='" + cluster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.cluster_gid = objODBCDatareader["cluster_gid"].ToString();
                objmaster.cluster_name = objODBCDatareader["cluster_name"].ToString();
                objmaster.cluster_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select cluster2baselocation_gid,cluster_gid,baselocation_gid, baselocation_name from sys_mst_tcluster2baselocation " +
                  " where cluster_gid='" + cluster_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlocationList = new List<locationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getlocationList.Add(new locationlist
                    {
                        baselocation_gid = dt["baselocation_gid"].ToString(),
                        baselocation_name = dt["baselocation_name"].ToString(),
                        cluster2baselocation_gid = dt["cluster2baselocation_gid"].ToString(),
                    });
                    objmaster.locationlist = getlocationList;
                }
            }
            dt_datatable.Dispose();

        }

        public bool DaPostClusterUpdate(string employee_gid, cluster values)
        {
            msSQL = "select cluster_gid from sys_mst_tclustermapping where cluster_name='" + values.cluster_name.Replace("'", "\\'") + "'";
            clusterGID = objdbconn.GetExecuteScalar(msSQL);
            if (clusterGID != "")
            {
                if (clusterGID != values.cluster_gid)
                {
                    values.status = false;
                    values.message = "Cluster Name Already Exist";
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date,cluster_name from sys_mst_tclustermapping where cluster_gid ='" + values.cluster_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CLSL");
                    msSQL = " insert into sys_mst_tclustermappinglog(" +
                              " clusterlog_gid," +
                              " cluster_gid," +
                              " cluster_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.cluster_gid + "'," +
                              "'" + objODBCDatareader["cluster_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tclustermapping set cluster_name='" + values.cluster_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where cluster_gid='" + values.cluster_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from sys_mst_tcluster2baselocation where cluster_gid ='" + values.cluster_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.locationlist.Count; i++)
                {
                    msGet_LocationGid = objcmnfunctions.GetMasterGID("CL2L");

                    msSQL = "Insert into sys_mst_tcluster2baselocation( " +
                           " cluster2baselocation_gid, " +
                           " cluster_gid," +
                           " baselocation_gid," +
                           " baselocation_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_LocationGid + "'," +
                           "'" + values.cluster_gid + "'," +
                           "'" + values.locationlist[i].baselocation_gid + "'," +
                           "'" + values.locationlist[i].baselocation_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Cluster updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Cluster";
                return false;
            }
        }
        public void DaGetCluster2BaseLocation(string cluster_gid, cluster objmaster)
        {
            msSQL = " select cluster2baselocation_gid,cluster_gid,baselocation_gid, baselocation_name from sys_mst_tcluster2baselocation " +
                  " where cluster_gid='" + cluster_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlocationList = new List<locationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getlocationList.Add(new locationlist
                    {
                        baselocation_gid = dt["baselocation_gid"].ToString(),
                        baselocation_name = dt["baselocation_name"].ToString(),
                        cluster2baselocation_gid = dt["cluster2baselocation_gid"].ToString(),
                    });
                    objmaster.locationlist = getlocationList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetUnTaggedLocations(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT baselocation_gid ,baselocation_name FROM sys_mst_tbaselocation" +
                        " where delete_flag='N' and baselocation_gid not in (select baselocation_gid from sys_mst_tcluster2baselocation) " +
                        " order by baselocation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            baselocation_name = (dr_datarow["baselocation_name"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetUnTaggedLocationsEdit(MdlSystemMaster objmaster, string cluster_gid)
        {
            try
            {
                msSQL = " SELECT baselocation_gid ,baselocation_name FROM sys_mst_tbaselocation" +
                        " where delete_flag='N' and baselocation_gid not in (select baselocation_gid from sys_mst_tcluster2baselocation where cluster_gid<>'" + cluster_gid + "') " +
                        " order by baselocation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            baselocation_name = (dr_datarow["baselocation_name"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaInactiveCluster(cluster values, string employee_gid)
        {
            msSQL = " update sys_mst_tclustermapping set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where cluster_gid='" + values.cluster_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("CLSI");

                msSQL = " insert into sys_mst_tclusterinactivelog (" +
                      " clusterinactivelog_gid , " +
                      " cluster_gid," +
                      " cluster_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.cluster_gid + "'," +
                      " '" + values.cluster_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Cluster Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Cluster  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaClusterInactiveLogview(string cluster_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT cluster_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tclusterinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where cluster_gid ='" + cluster_gid + "' order by a.clusterinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            cluster_gid = (dr_datarow["cluster_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaPostRegionAdd(region values, string employee_gid)
        {
            msSQL = "select region_name from sys_mst_tregionmapping where region_name = '" + values.region_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Region Name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("RGNM");
                msSQL = " insert into sys_mst_tregionmapping(" +
                        " region_gid ," +
                        " region_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.region_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.cluster_list.Count; i++)
                {
                    msGet_clusterGid = objcmnfunctions.GetMasterGID("R2CL");

                    msSQL = "Insert into sys_mst_tregion2cluster( " +
                           " region2cluster_gid, " +
                           " region_gid," +
                           " cluster_gid," +
                           " cluster_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_clusterGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.cluster_list[i].cluster_gid + "'," +
                           "'" + values.cluster_list[i].cluster_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Region Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }

        public void DaGetRegionSummary(region objmaster)
        {
            try
            {
                msSQL = " SELECT a.region_gid ,a.region_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tregionmapping a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getregion_list = new List<region_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getregion_list.Add(new region_list
                        {
                            region_gid = (dr_datarow["region_gid"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString())
                        });
                    }
                    objmaster.region_list = getregion_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetRegionEdit(string region_gid, region objmaster)
        {
            msSQL = " select region_gid,region_name, status as status from sys_mst_tregionmapping " +
                    " where region_gid='" + region_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.region_gid = objODBCDatareader["region_gid"].ToString();
                objmaster.region_name = objODBCDatareader["region_name"].ToString();
                objmaster.region_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select region2cluster_gid,region_gid,cluster_gid, cluster_name from sys_mst_tregion2cluster " +
                  " where region_gid='" + region_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getclusterList = new List<cluster_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getclusterList.Add(new cluster_list
                    {
                        cluster_gid = dt["cluster_gid"].ToString(),
                        cluster_name = dt["cluster_name"].ToString(),
                        region2cluster_gid = dt["region2cluster_gid"].ToString(),
                    });
                    objmaster.cluster_list = getclusterList;
                }
            }
            dt_datatable.Dispose();

        }

        public bool DaPostRegionUpdate(string employee_gid, region values)
        {
            msSQL = "select region_gid from sys_mst_tregionmapping where region_name='" + values.region_name.Replace("'", "\\'") + "'";
            regionGID = objdbconn.GetExecuteScalar(msSQL);
            if (regionGID != "")
            {
                if (regionGID != values.region_gid)
                {
                    values.status = false;
                    values.message = "Region Name Already Exist";
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date,region_name from sys_mst_tregionmapping where region_gid ='" + values.region_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("RGNL");
                    msSQL = " insert into sys_mst_tregionmappinglog(" +
                              " regionlog_gid," +
                              " region_gid," +
                              " region_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.region_gid + "'," +
                              "'" + objODBCDatareader["region_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tregionmapping set region_name='" + values.region_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where region_gid='" + values.region_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from sys_mst_tregion2cluster where region_gid ='" + values.region_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.cluster_list.Count; i++)
                {
                    msGet_regionGid = objcmnfunctions.GetMasterGID("R2CL");

                    msSQL = "Insert into sys_mst_tregion2cluster( " +
                           " region2cluster_gid, " +
                           " region_gid," +
                           " cluster_gid," +
                           " cluster_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_regionGid + "'," +
                           "'" + values.region_gid + "'," +
                           "'" + values.cluster_list[i].cluster_gid + "'," +
                           "'" + values.cluster_list[i].cluster_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Region updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Region";
                return false;
            }
        }
        public void DaGetRegion2Cluster(string region_gid, cluster objmaster)
        {
            msSQL = " select region2cluster_gid,region_gid,cluster_gid, cluster_name from sys_mst_tregion2cluster " +
                  " where region_gid='" + region_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getclusterList = new List<cluster_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getclusterList.Add(new cluster_list
                    {
                        cluster_gid = dt["cluster_gid"].ToString(),
                        cluster_name = dt["cluster_name"].ToString(),
                        region2cluster_gid = dt["region2cluster_gid"].ToString(),
                    });
                    objmaster.cluster_list = getclusterList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetUnTaggedClusters(region objmaster)
        {
            try
            {
                msSQL = " SELECT cluster_gid ,cluster_name FROM sys_mst_tclustermapping" +
                        " where cluster_gid not in (select cluster_gid from sys_mst_tregion2cluster) " +
                        " order by cluster_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcluster_list = new List<cluster_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcluster_list.Add(new cluster_list
                        {
                            cluster_gid = (dr_datarow["cluster_gid"].ToString()),
                            cluster_name = (dr_datarow["cluster_name"].ToString()),
                        });
                    }
                    objmaster.cluster_list = getcluster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetUnTaggedClustersEdit(region objmaster, string region_gid)
        {
            try
            {
                msSQL = " SELECT cluster_gid ,cluster_name FROM sys_mst_tclustermapping" +
                        " where cluster_gid not in (select cluster_gid from sys_mst_tregion2cluster where region_gid<>'" + region_gid + "') " +
                        " order by cluster_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcluster_list = new List<cluster_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcluster_list.Add(new cluster_list
                        {
                            cluster_gid = (dr_datarow["cluster_gid"].ToString()),
                            cluster_name = (dr_datarow["cluster_name"].ToString()),
                        });
                    }
                    objmaster.cluster_list = getcluster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostRegionInactive(region values, string employee_gid)
        {
            msSQL = " update sys_mst_tregionmapping set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where region_gid='" + values.region_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("RGIL");

                msSQL = " insert into sys_mst_tregioninactivelog (" +
                      " regioninactivelog_gid , " +
                      " region_gid," +
                      " region_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.region_gid + "'," +
                      " '" + values.region_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Region Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Region Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaGetRegionInactiveLogview(string region_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT region_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tregioninactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where region_gid ='" + region_gid + "' order by a.regioninactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            region_gid = (dr_datarow["region_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaPostZoneAdd(zone values, string employee_gid)
        {
            msSQL = "select zone_name from sys_mst_tzonemapping where zone_name = '" + values.zone_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Zone Name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("ZONG");
                msSQL = " insert into sys_mst_tzonemapping(" +
                        " zone_gid ," +
                        " zone_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.zone_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.region_list.Count; i++)
                {
                    msGet_regionGid = objcmnfunctions.GetMasterGID("Z2RG");

                    msSQL = "Insert into sys_mst_tzone2region( " +
                           " zone2region_gid, " +
                           " zone_gid," +
                           " region_gid," +
                           " region_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_regionGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.region_list[i].region_gid + "'," +
                           "'" + values.region_list[i].region_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Zone Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }

        public void DaGetZoneSummary(zone objmaster)
        {
            try
            {
                msSQL = " SELECT a.zone_gid ,a.zone_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tzonemapping a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getzone_list = new List<zone_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getzone_list.Add(new zone_list
                        {
                            zone_gid = (dr_datarow["zone_gid"].ToString()),
                            zone_name = (dr_datarow["zone_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString())
                        });
                    }
                    objmaster.zone_list = getzone_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetZoneEdit(string zone_gid, zone objmaster)
        {
            msSQL = " select zone_gid,zone_name, status as status from sys_mst_tzonemapping " +
                    " where zone_gid='" + zone_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.zone_gid = objODBCDatareader["zone_gid"].ToString();
                objmaster.zone_name = objODBCDatareader["zone_name"].ToString();
                objmaster.zone_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select zone2region_gid,zone_gid,region_gid, region_name from sys_mst_tzone2region " +
                  " where zone_gid='" + zone_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getregionList = new List<region_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getregionList.Add(new region_list
                    {
                        region_gid = dt["region_gid"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        zone2region_gid = dt["zone2region_gid"].ToString(),
                    });
                    objmaster.region_list = getregionList;
                }
            }
            dt_datatable.Dispose();

        }

        public bool DaPostZoneUpdate(string employee_gid, zone values)
        {
            string zoneGID;
            msSQL = "select zone_gid from sys_mst_tzonemapping where zone_name='" + values.zone_name.Replace("'", "\\'") + "'";
            zoneGID = objdbconn.GetExecuteScalar(msSQL);
            if (zoneGID != "")
            {
                if (zoneGID != values.zone_gid)
                {
                    values.status = false;
                    values.message = "Zone Name Already Exist";
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date,zone_name from sys_mst_tzonemapping where zone_gid ='" + values.zone_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ZONL");
                    msSQL = " insert into sys_mst_tzonemappinglog(" +
                              " zonelog_gid," +
                              " zone_gid," +
                              " zone_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.zone_gid + "'," +
                              "'" + objODBCDatareader["zone_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tzonemapping set zone_name='" + values.zone_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where zone_gid='" + values.zone_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from sys_mst_tzone2region where zone_gid ='" + values.zone_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.region_list.Count; i++)
                {
                    msGet_regionGid = objcmnfunctions.GetMasterGID("Z2RG");

                    msSQL = "Insert into sys_mst_tzone2region( " +
                           " zone2region_gid, " +
                           " zone_gid," +
                           " region_gid," +
                           " region_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_regionGid + "'," +
                           "'" + values.zone_gid + "'," +
                           "'" + values.region_list[i].region_gid + "'," +
                           "'" + values.region_list[i].region_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Zone updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Zone";
                return false;
            }
        }
        public void DaGetZone2Region(string zone_gid, region objmaster)
        {
            msSQL = " select zone2region_gid,zone_gid,region_gid, region_name from sys_mst_tzone2region " +
                  " where zone_gid='" + zone_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getregionList = new List<region_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getregionList.Add(new region_list
                    {
                        region_gid = dt["region_gid"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        zone2region_gid = dt["zone2region_gid"].ToString(),
                    });
                    objmaster.region_list = getregionList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetUnTaggedRegions(zone objmaster)
        {
            try
            {
                msSQL = " SELECT region_gid ,region_name FROM sys_mst_tregionmapping" +
                        " where region_gid not in (select region_gid from sys_mst_tzone2region) " +
                        " order by region_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getregion_list = new List<region_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getregion_list.Add(new region_list
                        {
                            region_gid = (dr_datarow["region_gid"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                        });
                    }
                    objmaster.region_list = getregion_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetUnTaggedRegionsEdit(zone objmaster, string zone_gid)
        {
            try
            {
                msSQL = " SELECT region_gid ,region_name FROM sys_mst_tregionmapping" +
                        " where region_gid not in (select region_gid from sys_mst_tzone2region where zone_gid<>'" + zone_gid + "') " +
                        " order by region_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getregion_list = new List<region_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getregion_list.Add(new region_list
                        {
                            region_gid = (dr_datarow["region_gid"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                        });
                    }
                    objmaster.region_list = getregion_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostZoneInactive(zone values, string employee_gid)
        {
            msSQL = " update sys_mst_tzonemapping set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where zone_gid='" + values.zone_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ZNIL");

                msSQL = " insert into sys_mst_tzoneinactivelog (" +
                      " zoneinactivelog_gid , " +
                      " zone_gid," +
                      " zone_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.zone_gid + "'," +
                      " '" + values.zone_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Zone Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Zone Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaGetZoneInactiveLogview(string zone_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT zone_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tzoneinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where zone_gid ='" + zone_gid + "' order by a.zoneinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            zone_gid = (dr_datarow["zone_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetRegionList(region objmaster)
        {
            try
            {
                msSQL = " SELECT region_gid ,region_name " +
                        " FROM sys_mst_tregionmapping" +
                        " order by region_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getregion_list = new List<region_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getregion_list.Add(new region_list
                        {
                            region_gid = (dr_datarow["region_gid"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString())
                        });
                    }
                    objmaster.region_list = getregion_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }
        public void DaGetVerticallist(mdlvertical objmaster)
        {
            try
            {
                msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical " +
                        " order by vertical_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getvertical_list = new List<vertical_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getvertical_list.Add(new vertical_list
                        {
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                        });
                    }
                    objmaster.vertical_list = getvertical_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }
        public void DaGetEmployeelist(mdlemployee objmaster)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objmaster.employeelist = dt_datatable.AsEnumerable().Select(row =>
                      new employeelist
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }
        // Region Head Add
        public void DaPostRegionHeadAdd(mdlregionhead values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("RGHD");
            msSQL = " insert into sys_mst_tregionhead(" +
                    " regionhead_gid," +
                    " region_gid ," +
                    " region_name," +
                    " vertical_gid," +
                    " vertical_name," +
                    " employee_gid, " +
                    " employee_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.region_gid + "'," +
                    "'" + values.region_name + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.vertical_name + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Region Head Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaGetRegionHeadSummary(region objmaster)
        {
            try
            {
                msSQL = " SELECT a.regionhead_gid ,a.employee_name,a.employee_gid, a.region_name, a.vertical_name, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tregionhead a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getclusterhead_list = new List<regionhead_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getclusterhead_list.Add(new regionhead_list
                        {
                            regionhead_gid = (dr_datarow["regionhead_gid"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            region_name = (dr_datarow["region_name"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString())
                        });
                    }
                    objmaster.regionhead_list = getclusterhead_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostRegionHeadInactive(mdlregionhead values, string employee_gid)
        {
            msSQL = " update sys_mst_tregionhead set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where regionhead_gid='" + values.regionhead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("RHIL");

                msSQL = " insert into sys_mst_tregionheadinactivelog (" +
                      " regionheadinactivelog_gid , " +
                      " regionhead_gid," +
                      " employee_gid ," +
                      " employee_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.regionhead_gid + "'," +
                      " '" + values.employee_gid + "'," +
                      " '" + values.employee_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Region Head Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Region Head Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaGetRegionHeadInactiveLogview(string regionhead_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT regionhead_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tregionheadinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where regionhead_gid ='" + regionhead_gid + "' order by a.regionheadinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            regionhead_gid = (dr_datarow["regionhead_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetRegionHeadEdit(string regionhead_gid, mdlregionhead objmaster)
        {
            msSQL = " select regionhead_gid,region_gid, region_name, vertical_gid, vertical_name, " +
                    " employee_gid, employee_name, status as status from sys_mst_tregionhead " +
                    " where regionhead_gid='" + regionhead_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.regionhead_gid = objODBCDatareader["regionhead_gid"].ToString();
                objmaster.region_gid = objODBCDatareader["region_gid"].ToString();
                objmaster.region_name = objODBCDatareader["region_name"].ToString();
                objmaster.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                objmaster.employee_gid = objODBCDatareader["employee_gid"].ToString();
                objmaster.employee_name = objODBCDatareader["employee_name"].ToString();
                objmaster.regionhead_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT region_gid ,region_name " +
                        " FROM sys_mst_tregionmapping" +
                        " order by region_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getregion_list = new List<region_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getregion_list.Add(new region_list
                    {
                        region_gid = (dr_datarow["region_gid"].ToString()),
                        region_name = (dr_datarow["region_name"].ToString())
                    });
                }
                objmaster.region_list = getregion_list;
            }
            dt_datatable.Dispose();
            objmaster.status = true;

            msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical" +
                       " order by vertical_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvertical_list = new List<vertical_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvertical_list.Add(new vertical_list
                    {
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                    });
                }
                objmaster.vertical_list = getvertical_list;
            }
            dt_datatable.Dispose();
            objmaster.status = true;

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_employee = new List<employee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.employeelist = dt_datatable.AsEnumerable().Select(row =>
                  new employeelist
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();
            objmaster.status = true;

        }

        public bool DaPostRegionHeadUpdate(string employee_gid, mdlregionhead values)
        {

            msSQL = "select updated_by, updated_date,employee_gid, employee_name, " +
                    "region_gid, region_name, vertical_gid, vertical_name from sys_mst_tregionhead where regionhead_gid ='" + values.regionhead_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("RGHL");
                    msSQL = " insert into sys_mst_tregionheadlog(" +
                              " regionheadlog_gid," +
                              " regionhead_gid," +
                              " region_gid , " +
                              " region_name , " +
                              " vertical_gid , " +
                              " vertical_name , " +
                              " employee_gid , " +
                              " employee_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.regionhead_gid + "'," +
                              "'" + objODBCDatareader["region_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["region_name"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_name"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                           " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                           " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, " +
                           " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                           " businesshead_gid, businesshead_name from ocs_mst_tapplication a " +
                           " left join ocs_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
                           " where regionalhead_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='3' and " +
                           " a.vertical_gid='" + values.vertical_gid + "' and a.region_gid='" + values.region_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into ocs_mst_tapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " vertical_gid," +
                         " vertical_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'RH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication set " +
                                " regionalhead_gid='" + values.employee_gid + "'," +
                                " regionalhead_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_trn_tapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='3' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tregionhead set region_gid='" + values.region_gid + "'," +
                 " region_name='" + values.region_name + "'," +
                 " vertical_gid='" + values.vertical_gid + "'," +
                 " vertical_name='" + values.vertical_name + "'," +
                 " employee_gid='" + values.employee_gid + "'," +
                 " employee_name='" + values.employee_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where regionhead_gid='" + values.regionhead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Region Head updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Region Head";
                return false;
            }
        }

        public void DaGetZoneList(zone objmaster)
        {
            try
            {
                msSQL = " SELECT zone_gid ,zone_name " +
                        " FROM sys_mst_tzonemapping" +
                        " order by zone_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getzone_list = new List<zone_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getzone_list.Add(new zone_list
                        {
                            zone_gid = (dr_datarow["zone_gid"].ToString()),
                            zone_name = (dr_datarow["zone_name"].ToString())
                        });
                    }
                    objmaster.zone_list = getzone_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }
        // Business Head Add
        public void DaPostBusinessHeadAdd(mdlbusinesshead values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("BSHD");
            msSQL = " insert into sys_mst_tbusinesshead(" +
                    " businesshead_gid," +
                    " zone_gid ," +
                    " zone_name," +
                    " vertical_gid," +
                    " vertical_name," +
                    " employee_gid, " +
                    " employee_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.zone_gid + "'," +
                    "'" + values.zone_name + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.vertical_name + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Business Head Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaGetBusinessHeadSummary(mdlbusinesshead objmaster)
        {
            try
            {
                msSQL = " SELECT a.businesshead_gid ,a.employee_name,a.employee_gid, a.zone_name, a.vertical_name, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tbusinesshead a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbusinesshead_list = new List<businesshead_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbusinesshead_list.Add(new businesshead_list
                        {
                            businesshead_gid = (dr_datarow["businesshead_gid"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            zone_name = (dr_datarow["zone_name"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString())
                        });
                    }
                    objmaster.businesshead_list = getbusinesshead_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostBusinessHeadInactive(mdlbusinesshead values, string employee_gid)
        {
            msSQL = " update sys_mst_tbusinesshead set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where businesshead_gid='" + values.businesshead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BHIL");

                msSQL = " insert into sys_mst_tbusinessheadinactivelog (" +
                      " businessheadinactivelog_gid , " +
                      " businesshead_gid," +
                      " employee_gid ," +
                      " employee_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.businesshead_gid + "'," +
                      " '" + values.employee_gid + "'," +
                      " '" + values.employee_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Business Head Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Business Head Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaGetBusinessHeadInactiveLogview(string businesshead_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT businesshead_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tbusinessheadinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where businesshead_gid ='" + businesshead_gid + "' order by a.businessheadinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            businesshead_gid = (dr_datarow["businesshead_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetBusinessHeadEdit(string businesshead_gid, mdlbusinesshead objmaster)
        {
            msSQL = " select businesshead_gid,zone_gid, zone_name, vertical_gid, vertical_name, " +
                    " employee_gid, employee_name, status as status from sys_mst_tbusinesshead " +
                    " where businesshead_gid='" + businesshead_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.businesshead_gid = objODBCDatareader["businesshead_gid"].ToString();
                objmaster.zone_gid = objODBCDatareader["zone_gid"].ToString();
                objmaster.zone_name = objODBCDatareader["zone_name"].ToString();
                objmaster.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                objmaster.employee_gid = objODBCDatareader["employee_gid"].ToString();
                objmaster.employee_name = objODBCDatareader["employee_name"].ToString();
                objmaster.businesshead_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT zone_gid ,zone_name FROM sys_mst_tzonemapping" +
                    " order by zone_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getzone_list = new List<zone_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getzone_list.Add(new zone_list
                    {
                        zone_gid = (dr_datarow["zone_gid"].ToString()),
                        zone_name = (dr_datarow["zone_name"].ToString())
                    });
                }
                objmaster.zone_list = getzone_list;
            }
            dt_datatable.Dispose();
            objmaster.status = true;

            msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical" +
                       " order by vertical_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvertical_list = new List<vertical_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvertical_list.Add(new vertical_list
                    {
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                    });
                }
                objmaster.vertical_list = getvertical_list;
            }
            dt_datatable.Dispose();
            objmaster.status = true;

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_employee = new List<employee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.employeelist = dt_datatable.AsEnumerable().Select(row =>
                  new employeelist
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();
            objmaster.status = true;

        }

        public bool DaPostBusinessHeadUpdate(string employee_gid, mdlbusinesshead values)
        {

            msSQL = "select updated_by, updated_date,employee_gid, employee_name, " +
                    "zone_gid, zone_name, vertical_gid, vertical_name from sys_mst_tbusinesshead where businesshead_gid ='" + values.businesshead_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("BSHL");
                    msSQL = " insert into sys_mst_tbusinessheadlog(" +
                              " businessheadlog_gid," +
                              " businesshead_gid," +
                              " zone_gid , " +
                              " zone_name , " +
                              " vertical_gid , " +
                              " vertical_name , " +
                              " employee_gid , " +
                              " employee_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.businesshead_gid + "'," +
                              "'" + objODBCDatareader["zone_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["zone_name"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_name"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                           " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                           " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, " +
                           " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                           " businesshead_gid, businesshead_name from ocs_mst_tapplication a " +
                           " left join ocs_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
                           " where businesshead_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='5' and " +
                           " a.vertical_gid='" + values.vertical_gid + "' and a.zone_gid='" + values.zone_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into ocs_mst_tapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " vertical_gid," +
                         " vertical_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'BVH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication set " +
                                " businesshead_gid='" + values.employee_gid + "'," +
                                " businesshead_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_trn_tapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='5' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tbusinesshead set zone_gid='" + values.zone_gid + "'," +
                 " zone_name='" + values.zone_name + "'," +
                 " vertical_gid='" + values.vertical_gid + "'," +
                 " vertical_name='" + values.vertical_name + "'," +
                 " employee_gid='" + values.employee_gid + "'," +
                 " employee_name='" + values.employee_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where businesshead_gid='" + values.businesshead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Business Head updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Business Head";
                return false;
            }
        }

        // Group Business Head Add
        public void DaPostGroupBusinessHeadAdd(mdlbusinesshead values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("GBHD");
            msSQL = " insert into sys_mst_tgroupbusinesshead(" +
                    " groupbusinesshead_gid," +
                    " zone_gid ," +
                    " zone_name," +
                    " vertical_gid," +
                    " vertical_name," +
                    " employee_gid, " +
                    " employee_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.zone_gid + "'," +
                    "'" + values.zone_name + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.vertical_name + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Group Business Head Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaGetGroupBusinessHeadSummary(mdlbusinesshead objmaster)
        {
            try
            {
                msSQL = " SELECT a.groupbusinesshead_gid ,a.employee_name,a.employee_gid, a.zone_name, a.vertical_name, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tgroupbusinesshead a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbusinesshead_list = new List<businesshead_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbusinesshead_list.Add(new businesshead_list
                        {
                            groupbusinesshead_gid = (dr_datarow["groupbusinesshead_gid"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            zone_name = (dr_datarow["zone_name"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString())
                        });
                    }
                    objmaster.businesshead_list = getbusinesshead_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostGroupBusinessHeadInactive(mdlbusinesshead values, string employee_gid)
        {
            msSQL = " update sys_mst_tgroupbusinesshead set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where groupbusinesshead_gid='" + values.groupbusinesshead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("GBIL");

                msSQL = " insert into sys_mst_tgroupbusinessheadinactivelog (" +
                      " groupbusinessheadinactivelog_gid , " +
                      " groupbusinesshead_gid," +
                      " employee_gid ," +
                      " employee_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.groupbusinesshead_gid + "'," +
                      " '" + values.employee_gid + "'," +
                      " '" + values.employee_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Group Business Head Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Group Business Head Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaGetGroupBusinessHeadInactiveLogview(string groupbusinesshead_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT groupbusinesshead_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tgroupbusinessheadinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where groupbusinesshead_gid ='" + groupbusinesshead_gid + "' order by a.groupbusinessheadinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            groupbusinesshead_gid = (dr_datarow["groupbusinesshead_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetGroupBusinessHeadEdit(string groupbusinesshead_gid, mdlbusinesshead objmaster)
        {
            msSQL = " select groupbusinesshead_gid,zone_gid, zone_name, vertical_gid, vertical_name, " +
                    " employee_gid, employee_name, status as status from sys_mst_tgroupbusinesshead " +
                    " where groupbusinesshead_gid='" + groupbusinesshead_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.groupbusinesshead_gid = objODBCDatareader["groupbusinesshead_gid"].ToString();
                objmaster.zone_gid = objODBCDatareader["zone_gid"].ToString();
                objmaster.zone_name = objODBCDatareader["zone_name"].ToString();
                objmaster.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                objmaster.employee_gid = objODBCDatareader["employee_gid"].ToString();
                objmaster.employee_name = objODBCDatareader["employee_name"].ToString();
                objmaster.businesshead_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT zone_gid ,zone_name FROM sys_mst_tzonemapping" +
                    " order by zone_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getzone_list = new List<zone_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getzone_list.Add(new zone_list
                    {
                        zone_gid = (dr_datarow["zone_gid"].ToString()),
                        zone_name = (dr_datarow["zone_name"].ToString())
                    });
                }
                objmaster.zone_list = getzone_list;
            }
            dt_datatable.Dispose();
            objmaster.status = true;

            msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical" +
                       " order by vertical_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvertical_list = new List<vertical_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvertical_list.Add(new vertical_list
                    {
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                    });
                }
                objmaster.vertical_list = getvertical_list;
            }
            dt_datatable.Dispose();
            objmaster.status = true;

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_employee = new List<employee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.employeelist = dt_datatable.AsEnumerable().Select(row =>
                  new employeelist
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();
            objmaster.status = true;
        }

        public bool DaPostGroupBusinessHeadUpdate(string employee_gid, mdlbusinesshead values)
        {

            msSQL = "select updated_by, updated_date,employee_gid, employee_name, " +
                    "zone_gid, zone_name, vertical_gid, vertical_name from sys_mst_tgroupbusinesshead where groupbusinesshead_gid ='" + values.groupbusinesshead_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("GBHL");
                    msSQL = " insert into sys_mst_tgroupbusinessheadlog(" +
                              " groupbusinessheadlog_gid," +
                              " groupbusinesshead_gid," +
                              " zone_gid , " +
                              " zone_name , " +
                              " vertical_gid , " +
                              " vertical_name , " +
                              " employee_gid , " +
                              " employee_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.groupbusinesshead_gid + "'," +
                              "'" + objODBCDatareader["zone_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["zone_name"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_name"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tgroupbusinesshead set zone_gid='" + values.zone_gid + "'," +
                 " zone_name='" + values.zone_name + "'," +
                 " vertical_gid='" + values.vertical_gid + "'," +
                 " vertical_name='" + values.vertical_name + "'," +
                 " employee_gid='" + values.employee_gid + "'," +
                 " employee_name='" + values.employee_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where groupbusinesshead_gid='" + values.groupbusinesshead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Group Business Head updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Group Business Head";
                return false;
            }
        }

        //Cluster Head codes

        public void DaGetClusterslist(region objmaster)
        {
            try
            {
                msSQL = " SELECT cluster_gid ,cluster_name FROM sys_mst_tclustermapping" +
                        " order by cluster_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcluster_list = new List<cluster_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcluster_list.Add(new cluster_list
                        {
                            cluster_gid = (dr_datarow["cluster_gid"].ToString()),
                            cluster_name = (dr_datarow["cluster_name"].ToString()),
                        });
                    }
                    objmaster.cluster_list = getcluster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostClusterheadAdd(mdlclusterhead values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CLHD");
            msSQL = " insert into sys_mst_tclusterhead(" +
                    " clusterhead_gid," +
                    " cluster_gid ," +
                    " cluster_name," +
                    " vertical_gid," +
                    " vertical_name," +
                    " employee_gid, " +
                    " employee_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.cluster_gid + "'," +
                    "'" + values.cluster_name + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.vertical_name + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Cluster Head Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaInactiveClusterhead(mdlclusterhead values, string employee_gid)
        {
            msSQL = " update sys_mst_tclusterhead set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where clusterhead_gid='" + values.clusterhead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            string lsemployeegid = objdbconn.GetExecuteScalar("select employee_gid from sys_mst_tclusterhead where clusterhead_gid = '" + values.clusterhead_gid + "' ");
            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("CHDA");

                msSQL = " insert into sys_mst_tclusterheadinactivelog (" +
                      " clusterheadinactivelog_gid , " +
                      " clusterhead_gid," +
                      " employee_gid, " +
                      " employee_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.clusterhead_gid + "'," +
                      " '" + lsemployeegid + "'," +
                      " '" + values.employee_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Cluster Head Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Cluster Head  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaClusterheadInactiveLogview(string clusterhead_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT clusterhead_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tclusterheadinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where clusterhead_gid ='" + clusterhead_gid + "' order by a.clusterheadinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            clusterhead_gid = (dr_datarow["clusterhead_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetClusterHeadSummary(cluster objmaster)
        {
            try
            {
                msSQL = " SELECT a.clusterhead_gid ,a.employee_name,a.employee_gid, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,vertical_name,cluster_name," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tclusterhead a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getclusterhead_list = new List<clusterhead_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getclusterhead_list.Add(new clusterhead_list
                        {
                            clusterhead_gid = (dr_datarow["clusterhead_gid"].ToString()),
                            cluster_name = (dr_datarow["cluster_name"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString())
                        });
                    }
                    objmaster.clusterhead_list = getclusterhead_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetClusterHeadEdit(string clusterhead_gid, mdlclusterhead objmaster)
        {
            msSQL = " select clusterhead_gid,cluster_gid,cluster_name,vertical_gid,vertical_name,employee_gid,employee_name, status as status from sys_mst_tclusterhead " +
                    " where clusterhead_gid='" + clusterhead_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.clusterhead_gid = objODBCDatareader["clusterhead_gid"].ToString();
                objmaster.cluster_gid = objODBCDatareader["cluster_gid"].ToString();
                objmaster.cluster_name = objODBCDatareader["cluster_name"].ToString();
                objmaster.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                objmaster.employee_gid = objODBCDatareader["employee_gid"].ToString();
                objmaster.employee_name = objODBCDatareader["employee_name"].ToString();
                objmaster.clusterhead_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT cluster_gid ,cluster_name FROM sys_mst_tclustermapping" +
                           " order by cluster_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcluster_list = new List<cluster_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcluster_list.Add(new cluster_list
                    {
                        cluster_gid = (dr_datarow["cluster_gid"].ToString()),
                        cluster_name = (dr_datarow["cluster_name"].ToString()),
                    });
                }
                objmaster.cluster_list = getcluster_list;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
           " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
           " where user_status<>'N' order by a.user_firstname asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemployee_list = new List<employeelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getemployee_list.Add(new employeelist
                    {
                        employee_gid = (dr_datarow["employee_gid"].ToString()),
                        employee_name = (dr_datarow["employee_name"].ToString()),
                    });
                }
                objmaster.employeelist = getemployee_list;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical" +
                       " order by vertical_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvertical_list = new List<vertical_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvertical_list.Add(new vertical_list
                    {
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                    });
                }
                objmaster.vertical_list = getvertical_list;
            }
            dt_datatable.Dispose();

        }

        public bool DaPostClusterHeadUpdate(string employee_gid, mdlclusterhead values)
        {


            msSQL = "select updated_by, updated_date,clusterhead_gid,employee_name,employee_gid,cluster_gid,cluster_name,vertical_gid,vertical_name from sys_mst_tclusterhead where clusterhead_gid ='" + values.clusterhead_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CHDL");
                    msSQL = " insert into sys_mst_tclusterheadlog(" +
                              " clusterheadlog_gid," +
                              " clusterhead_gid," +
                              " employee_gid, " +
                              " employee_name , " +
                              " vertical_gid," +
                              " vertical_name , " +
                              " cluster_gid," +
                              " cluster_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.clusterhead_gid + "'," +
                              "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_name"].ToString() + "'," +
                              "'" + objODBCDatareader["cluster_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["cluster_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);             
                }

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                           " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                           " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, " +
                           " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                           " businesshead_gid, businesshead_name from ocs_mst_tapplication a " +
                           " left join ocs_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
                           " where clustermanager_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='2' and " +
                           " a.vertical_gid='" + values.vertical_gid + "' and a.cluster_gid='" + values.cluster_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into ocs_mst_tapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " vertical_gid," +
                         " vertical_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'CH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication set " +
                                " clustermanager_gid='" + values.employee_gid + "'," +
                                " clustermanager_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_trn_tapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='2' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tclusterhead set " +
                " employee_gid='" + values.employee_gid + "'," +
                " employee_name='" + values.employee_name + "'," +
                " vertical_gid='" + values.vertical_gid + "'," +
                " vertical_name='" + values.vertical_name + "'," +
                " cluster_gid='" + values.cluster_gid + "'," +
                " cluster_name='" + values.cluster_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where clusterhead_gid='" + values.clusterhead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Cluster Head updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Cluster Head";
                return false;
            }
        }


        //Zonal Head codes

        public void DaGetZonalslist(zone objmaster)
        {
            try
            {
                msSQL = " SELECT zone_gid ,zone_name FROM sys_mst_tzonemapping" +
                        " order by zone_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getzone_list = new List<zone_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getzone_list.Add(new zone_list
                        {
                            zone_gid = (dr_datarow["zone_gid"].ToString()),
                            zone_name = (dr_datarow["zone_name"].ToString()),
                        });
                    }
                    objmaster.zone_list = getzone_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostZonalheadAdd(mdlzonalhead values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("ZNHD");
            msSQL = " insert into sys_mst_tZonalhead(" +
                    " zonalhead_gid," +
                    " zonal_gid ," +
                    " zonal_name," +
                    " vertical_gid," +
                    " vertical_name," +
                    " employee_gid, " +
                    " employee_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.zonal_gid + "'," +
                    "'" + values.zonal_name + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.vertical_name + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Zonal Head Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaInactiveZonalhead(mdlzonalhead values, string employee_gid)
        {
            msSQL = " update sys_mst_tzonalhead set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where zonalhead_gid='" + values.zonalhead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            string lsemployeegid = objdbconn.GetExecuteScalar("select employee_gid from sys_mst_tzonalhead where zonalhead_gid = '" + values.zonalhead_gid + "' ");
            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ZNDA");

                msSQL = " insert into sys_mst_tzonalheadinactivelog (" +
                      " zonalheadinactivelog_gid , " +
                      " zonalhead_gid," +
                      " employee_gid, " +
                      " employee_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.zonalhead_gid + "'," +
                      " '" + lsemployeegid + "'," +
                      " '" + values.employee_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Zonal Head Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Zonal Head  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaZonalheadInactiveLogview(string zonalhead_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT zonalhead_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tzonalheadinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where zonalhead_gid ='" + zonalhead_gid + "' order by a.zonalheadinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            zonalhead_gid = (dr_datarow["zonalhead_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetZonalHeadSummary(zone objmaster)
        {
            try
            {
                msSQL = " SELECT a.zonalhead_gid ,a.employee_name,a.employee_gid, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,vertical_name,zonal_name," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tzonalhead a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getzonalhead_list = new List<zonalhead_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getzonalhead_list.Add(new zonalhead_list
                        {
                            zonalhead_gid = (dr_datarow["zonalhead_gid"].ToString()),
                            zonal_name = (dr_datarow["zonal_name"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString())
                        });
                    }
                    objmaster.zonalhead_list = getzonalhead_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetZonalHeadEdit(string zonalhead_gid, mdlzonalhead objmaster)
        {
            msSQL = " select zonalhead_gid,zonal_gid,zonal_name,vertical_gid,vertical_name,employee_gid,employee_name, status as status from sys_mst_tzonalhead " +
                    " where zonalhead_gid='" + zonalhead_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.zonalhead_gid = objODBCDatareader["zonalhead_gid"].ToString();
                objmaster.zonal_gid = objODBCDatareader["zonal_gid"].ToString();
                objmaster.zonal_name = objODBCDatareader["zonal_name"].ToString();
                objmaster.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                objmaster.vertical_name = objODBCDatareader["vertical_name"].ToString();
                objmaster.employee_gid = objODBCDatareader["employee_gid"].ToString();
                objmaster.employee_name = objODBCDatareader["employee_name"].ToString();
                objmaster.zonalhead_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT zone_gid ,zone_name FROM sys_mst_tzonemapping" +
                       " order by zone_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getzone_list = new List<zone_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getzone_list.Add(new zone_list
                    {
                        zone_gid = (dr_datarow["zone_gid"].ToString()),
                        zone_name = (dr_datarow["zone_name"].ToString()),
                    });
                }
                objmaster.zone_list = getzone_list;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
           " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
           " where user_status<>'N' order by a.user_firstname asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemployee_list = new List<employeelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getemployee_list.Add(new employeelist
                    {
                        employee_gid = (dr_datarow["employee_gid"].ToString()),
                        employee_name = (dr_datarow["employee_name"].ToString()),
                    });
                }
                objmaster.employeelist = getemployee_list;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical" +
                       " order by vertical_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvertical_list = new List<vertical_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvertical_list.Add(new vertical_list
                    {
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                    });
                }
                objmaster.vertical_list = getvertical_list;
            }
            dt_datatable.Dispose();

        }

        public bool DaPostZonalHeadUpdate(string employee_gid, mdlzonalhead values)
        {


            msSQL = "select updated_by, updated_date,zonal_name,employee_name,employee_gid,zonal_gid,vertical_gid,vertical_name from sys_mst_tzonalhead where zonalhead_gid ='" + values.zonalhead_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ZNDL");
                    msSQL = " insert into sys_mst_tzonalheadlog(" +
                              " zonalheadlog_gid," +
                              " zonalhead_gid," +
                              " employee_gid, " +
                              " employee_name , " +
                              " vertical_gid," +
                              " vertical_name , " +
                              " zonal_gid," +
                              " zonal_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.zonalhead_gid + "'," +
                              "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["vertical_name"].ToString() + "'," +
                              "'" + objODBCDatareader["zonal_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["zonal_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select a.application_gid, application_no, customer_name, customer_urn, b.approval_status, vertical_gid, vertical_name, " +
                           " baselocation_gid, baselocation_name, cluster_gid, cluster_name, region_gid, region_name, zone_gid, zone_name, " +
                           " relationshipmanager_gid, relationshipmanager_name, drm_gid, drm_name, " +
                           " clustermanager_gid, clustermanager_name, regionalhead_gid, regionalhead_name, zonalhead_gid, zonalhead_name, " +
                           " businesshead_gid, businesshead_name from ocs_mst_tapplication a " +
                           " left join ocs_trn_tapplicationapproval b on a.application_gid = b.application_gid " +
                           " where zonalhead_gid = '" + objODBCDatareader["employee_gid"].ToString() + "' and b.approval_status = 'Pending' and b.hierary_level='4' and " +
                           " a.vertical_gid='" + values.vertical_gid + "' and a.zone_gid='" + values.zonal_gid + "'  group by b.application_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into ocs_mst_tapplicationlog(" +
                         " application_gid," +
                         " application_no, " +
                         " customer_urn," +
                         " customer_name," +
                         " vertical_gid," +
                         " vertical_name, " +
                         " baselocation_gid," +
                         " baselocation_name, " +
                         " cluster_gid," +
                         " cluster_name," +
                         " region_gid," +
                         " region_name, " +
                         " zone_gid," +
                         " zone_name, " +
                         " relationshipmanager_gid," +
                         " relationshipmanager_name," +
                         " drm_gid," +
                         " drm_name, " +
                         " clustermanager_gid," +
                         " clustermanager_name, " +
                         " regionalhead_gid," +
                         " regionalhead_name," +
                         " zonalhead_gid," +
                         " zonalhead_name, " +
                         " businesshead_gid," +
                         " businesshead_name," +
                         " head_change," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + dt["application_gid"].ToString() + "'," +
                         "'" + dt["application_no"].ToString() + "'," +
                         "'" + dt["customer_urn"].ToString() + "'," +
                         "'" + dt["customer_name"].ToString() + "'," +
                         "'" + dt["vertical_gid"].ToString() + "'," +
                         "'" + dt["vertical_name"].ToString() + "'," +
                         "'" + dt["baselocation_gid"].ToString() + "'," +
                         "'" + dt["baselocation_name"].ToString() + "'," +
                         "'" + dt["cluster_gid"].ToString() + "'," +
                         "'" + dt["cluster_name"].ToString() + "'," +
                         "'" + dt["region_gid"].ToString() + "'," +
                         "'" + dt["region_name"].ToString() + "'," +
                         "'" + dt["zone_gid"].ToString() + "'," +
                         "'" + dt["zone_name"].ToString() + "'," +
                         "'" + dt["relationshipmanager_gid"].ToString() + "'," +
                         "'" + dt["relationshipmanager_name"].ToString() + "'," +
                         "'" + dt["drm_gid"].ToString() + "'," +
                         "'" + dt["drm_name"].ToString() + "'," +
                         "'" + dt["clustermanager_gid"].ToString() + "'," +
                         "'" + dt["clustermanager_name"].ToString() + "'," +
                         "'" + dt["regionalhead_gid"].ToString() + "'," +
                         "'" + dt["regionalhead_name"].ToString() + "'," +
                         "'" + dt["zonalhead_gid"].ToString() + "'," +
                         "'" + dt["zonalhead_name"].ToString() + "'," +
                         "'" + dt["businesshead_gid"].ToString() + "'," +
                         "'" + dt["businesshead_name"].ToString() + "'," +
                         "'ZH'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication set " +
                                " zonalhead_gid='" + values.employee_gid + "'," +
                                " zonalhead_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and vertical_gid='" + values.vertical_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_trn_tapplicationapproval set " +
                                " approval_gid='" + values.employee_gid + "'," +
                                " approval_name='" + values.employee_name + "'" +
                                " where application_gid='" + dt["application_gid"].ToString() + "' and hierary_level='4' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tzonalhead set " +
                " employee_gid='" + values.employee_gid + "'," +
                " employee_name='" + values.employee_name + "'," +
                " vertical_gid='" + values.vertical_gid + "'," +
                " vertical_name='" + values.vertical_name + "'," +
                " zonal_gid='" + values.zonal_gid + "'," +
                " zonal_name='" + values.zonal_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where zonalhead_gid='" + values.zonalhead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Zonal Head updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Zonal Head";
                return false;
            }
        }
        //Product Head codes



        public void DaPostProductheadAdd(mdlproducthead values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("PRHD");
            msSQL = " insert into sys_mst_tProducthead(" +
                    " producthead_gid," +
                    " product_gid ," +
                    " product_name," +
                    " employee_gid, " +
                    " employee_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Head Added successfully";
            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }

        }

        public void DaInactiveProducthead(mdlproducthead values, string employee_gid)
        {
            msSQL = " update sys_mst_tproducthead set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where producthead_gid='" + values.producthead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            string lsemployeegid = objdbconn.GetExecuteScalar("select employee_gid from sys_mst_tproducthead where producthead_gid = '" + values.producthead_gid + "' ");
            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("PRDA");

                msSQL = " insert into sys_mst_tproductheadinactivelog (" +
                      " productheadinactivelog_gid , " +
                      " producthead_gid," +
                      " employee_gid, " +
                      " employee_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.producthead_gid + "'," +
                      " '" + lsemployeegid + "'," +
                      " '" + values.employee_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Product Head Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Product Head  Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaProductheadInactiveLogview(string producthead_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT producthead_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tproductheadinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where producthead_gid ='" + producthead_gid + "' order by a.productheadinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            producthead_gid = (dr_datarow["producthead_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetProductHeadSummary(zone objmaster)
        {
            try
            {
                msSQL = " SELECT a.producthead_gid ,a.employee_name,a.employee_gid, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,product_name," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tproducthead a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getproducthead_list = new List<producthead_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getproducthead_list.Add(new producthead_list
                        {
                            producthead_gid = (dr_datarow["producthead_gid"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString())
                        });
                    }
                    objmaster.producthead_list = getproducthead_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetProductHeadEdit(string producthead_gid, mdlproducthead objmaster)
        {
            msSQL = " select producthead_gid,product_name,product_gid,employee_gid,employee_name, status as status from sys_mst_tproducthead " +
                    " where producthead_gid='" + producthead_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.producthead_gid = objODBCDatareader["producthead_gid"].ToString();
                objmaster.product_gid = objODBCDatareader["product_gid"].ToString();
                objmaster.product_name = objODBCDatareader["product_name"].ToString();
                objmaster.employee_gid = objODBCDatareader["employee_gid"].ToString();
                objmaster.employee_name = objODBCDatareader["employee_name"].ToString();
                objmaster.producthead_status = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT zone_gid ,zone_name FROM sys_mst_tzonemapping" +
                       " order by zone_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getzone_list = new List<zone_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getzone_list.Add(new zone_list
                    {
                        zone_gid = (dr_datarow["zone_gid"].ToString()),
                        zone_name = (dr_datarow["zone_name"].ToString()),
                    });
                }
                objmaster.zone_list = getzone_list;
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
           " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
           " where user_status<>'N' order by a.user_firstname asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemployee_list = new List<employeelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getemployee_list.Add(new employeelist
                    {
                        employee_gid = (dr_datarow["employee_gid"].ToString()),
                        employee_name = (dr_datarow["employee_name"].ToString()),
                    });
                }
                objmaster.employeelist = getemployee_list;
            }
            dt_datatable.Dispose();



        }

        public bool DaPostProductHeadUpdate(string employee_gid, mdlproducthead values)
        {


            msSQL = "select updated_by, updated_date,product_name,employee_name,employee_gid,product_gid from sys_mst_tproducthead where producthead_gid ='" + values.producthead_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("PRDL");
                    msSQL = " insert into sys_mst_tproductheadlog(" +
                              " productheadlog_gid," +
                              " producthead_gid," +
                              " employee_gid, " +
                              " employee_name , " +
                              " product_gid," +
                              " product_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.producthead_gid + "'," +
                              "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                              "'" + objODBCDatareader["product_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["product_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update sys_mst_tproducthead set " +
                " employee_gid='" + values.employee_gid + "'," +
                " employee_name='" + values.employee_name + "'," +
                " product_gid='" + values.product_gid + "'," +
                " product_name='" + values.product_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where producthead_gid='" + values.producthead_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Head updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Product Head";
                return false;
            }
        }

        //Task

        public void DaPostTaskAdd(MdlTask values, string employee_gid)
        {
            msSQL = "select task_name from sys_mst_ttask where task_name = '" + values.task_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Task Already Exists";
            }
            else
            {

                msGetGid = objcmnfunctions.GetMasterGID("STSK");
                msGetTaskCode = objcmnfunctions.GetMasterGID("TSKC");
                msSQL = " insert into sys_mst_ttask(" +
                        " task_gid ," +
                        " task_code ," +
                        " task_name," +
                        " lms_code ," +
                        " bureau_code ," +
                        " task_description," +
                        " tat," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + msGetTaskCode + "',";
                if (values.task_name == "" || values.task_name == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.task_name.Replace("'", "") + "',";
                }
                if (values.lms_code == "" || values.lms_code == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.lms_code.Replace("'", "") + "',";
                }
                if (values.bureau_code == "" || values.bureau_code == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
                }
                if (values.task_description == "" || values.task_description == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.task_description.Replace("'", "") + "',";
                }
                if (values.tat == "" || values.tat == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.tat.Replace("'", "") + "',";
                }
                  msSQL +=  "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.assigned_to.Count; i++)
                {
                    msGetTask2AssignedToGid = objcmnfunctions.GetMasterGID("TAST");
                    msSQL = "Insert into sys_mst_ttask2assignedto( " +
                           " task2assignedto_gid, " +
                           " task_gid," +
                           " assignedto_gid," +
                           " assignedto_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetTask2AssignedToGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.assigned_to[i].employee_gid + "'," +
                           "'" + values.assigned_to[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResultSub1 = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                for (var i = 0; i < values.escalationmail_to.Count; i++)
                {
                    msGetTask2EscalationMailToGid = objcmnfunctions.GetMasterGID("TEMT");
                    msSQL = "Insert into sys_mst_ttask2escalationmailto( " +
                           " task2escalationmailto_gid, " +
                           " task_gid," +
                           " escalationmailto_gid," +
                           " escalationmailto_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetTask2EscalationMailToGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.escalationmail_to[i].employee_gid + "'," +
                           "'" + values.escalationmail_to[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResultSub2 = objdbconn.ExecuteNonQuerySQL(msSQL);
                }


                if (mnResult != 0 && mnResultSub1 != 0 && mnResultSub2 != 0)
                {
                    values.status = true;
                    values.message = "Task Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding Task";
                    values.status = false;
                }

                objODBCDatareader.Close();
            }
        }

        public void DaGetTaskSummary(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.task_gid ,a.task_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_ttask a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.task_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            task_gid = (dr_datarow["task_gid"].ToString()),
                            task_name = (dr_datarow["task_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaEditTask(string task_gid, MdlTask objmaster)
        {

            msSQL = " SELECT task_gid,task_code,task_name,lms_code, bureau_code,task_description,tat, status as Status FROM sys_mst_ttask " +
                    " where task_gid='" + task_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objmaster.task_gid = objODBCDatareader["task_gid"].ToString();
                objmaster.task_code = objODBCDatareader["task_code"].ToString();
                objmaster.task_name = objODBCDatareader["task_name"].ToString();
                objmaster.lms_code = objODBCDatareader["lms_code"].ToString();
                objmaster.bureau_code = objODBCDatareader["bureau_code"].ToString();
                objmaster.task_description = objODBCDatareader["task_description"].ToString();
                objmaster.tat = objODBCDatareader["tat"].ToString();              
                objmaster.Status = objODBCDatareader["Status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select assignedto_gid,assignedto_name from sys_mst_ttask2assignedto " +
            " where task_gid='" + task_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassignedtoList = new List<assignedto_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getassignedtoList.Add(new assignedto_list
                    {
                        employee_gid = dt["assignedto_gid"].ToString(),
                        employee_name = dt["assignedto_name"].ToString(),
                    });
                    objmaster.assigned_to = getassignedtoList;
                }
            }

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
              " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
              " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);            
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.assignedto_general = dt_datatable.AsEnumerable().Select(row =>
                  new assignedto_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

            msSQL = " select escalationmailto_gid,escalationmailto_name from sys_mst_ttask2escalationmailto " +
            " where task_gid='" + task_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getescalationmailtoList = new List<escalationmailto_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getescalationmailtoList.Add(new escalationmailto_list
                    {
                        employee_gid = dt["escalationmailto_gid"].ToString(),
                        employee_name = dt["escalationmailto_name"].ToString(),
                    });
                    objmaster.escalationmail_to = getescalationmailtoList;
                }
            }

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
              " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
              " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);            
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.escalationmailto_general = dt_datatable.AsEnumerable().Select(row =>
                  new escalationmailto_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

        }

        public bool DaUpdateTask(string employee_gid, MdlTask values)
        {           
            msSQL = "select task_gid, task_name, updated_by, updated_date from sys_mst_ttask where task_gid='" + values.task_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("PMSL");
                    msSQL = " insert into sys_mst_ttasklog(" +
                              " tasklog_gid  ," +
                              " task_gid," +
                              " task_name, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + objODBCDatareader["task_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["task_name"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

          
            msSQL = " update sys_mst_ttask set ";
            if (values.task_name == "" || values.task_name == null)
            {
                msSQL += " task_name='',";
            }
            else
            {
                msSQL += " task_name='" + values.task_name + "',";
            }
            if (values.task_description == "" || values.task_description == null)
            {
                msSQL += " task_description='',";
            }
            else
            {
                msSQL += " task_description='" + values.task_description + "',";
            }
            if (values.tat == "" || values.tat == null)
            {
                msSQL += " tat='',";
            }
            else
            {
                msSQL += " tat='" + values.tat + "',";
            }
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }          

        msSQL += " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where task_gid='" + values.task_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from sys_mst_ttask2assignedto where task_gid = '" + values.task_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.assigned_to.Count; i++)
                {
                    msGetTask2AssignedToGid = objcmnfunctions.GetMasterGID("TAST");
                    msSQL = "Insert into sys_mst_ttask2assignedto( " +
                           " task2assignedto_gid, " +
                           " task_gid," +
                           " assignedto_gid," +
                           " assignedto_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetTask2AssignedToGid + "'," +
                           "'" + values.task_gid + "'," +
                           "'" + values.assigned_to[i].employee_gid + "'," +
                           "'" + values.assigned_to[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResultSub1 = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = " delete from sys_mst_ttask2escalationmailto where task_gid = '" + values.task_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.escalationmail_to.Count; i++)
                {
                    msGetTask2EscalationMailToGid = objcmnfunctions.GetMasterGID("TEMT");
                    msSQL = "Insert into sys_mst_ttask2escalationmailto( " +
                           " task2escalationmailto_gid, " +
                           " task_gid," +
                           " escalationmailto_gid," +
                           " escalationmailto_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetTask2EscalationMailToGid + "'," +
                           "'" + values.task_gid + "'," +
                           "'" + values.escalationmail_to[i].employee_gid + "'," +
                           "'" + values.escalationmail_to[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResultSub2 = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult != 0 && mnResultSub1 != 0 && mnResultSub2 != 0)
            {            

                values.status = true;
                values.message = "Task Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Task";
                return false;
            }
        }

        public void DaInactiveTask(master values, string employee_gid)
        {
            msSQL = " select * from sys_mst_ttaskinitiate where task_gid='" + values.task_gid + "' and (task_status= 'null' or task_status = 'Initiated')";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                objODBCDatareader1.Close();
                values.message = "Can't able to inactive Task, Because it is tagged to Employee Onboarding";
                values.status = false;
            }
            else
            {
                msSQL = " update sys_mst_ttask set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where task_gid='" + values.task_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("STKI");

                msSQL = " insert into sys_mst_ttaskinactivelog (" +
                      " taskinactivelog_gid  , " +
                      " task_gid," +
                      " task_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.task_gid + "'," +
                      " '" + values.task_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Task Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Task Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }
        }

        public void DaTaskInactiveLogview(string task_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT task_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_ttaskinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where task_gid ='" + task_gid + "' order by a.taskinactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            task_gid = (dr_datarow["task_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaDeleteTask(string task_gid, string employee_gid, master values)
        {
            //msSQL = " update sys_mst_ttask   set delete_flag='Y'," +
            //        " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            //       "  deleted_by='" + employee_gid + "'" +
            //       " where task_gid='" + task_gid + "' ";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //if (mnResult != 0)
            //{

            //    values.status = true;
            //    values.message = "Task Deleted Successfully";

            //}
            //else
            //{
            //    values.status = false;
            //    values.message = "Error Occurred";
            //}
            msSQL = " select * from sys_mst_ttaskinitiate where task_gid='" + task_gid + "' and (task_status= 'null' or task_status = 'Initiated')";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                objODBCDatareader1.Close();
                values.message = "Can't able to delete Task, Because it is tagged to Employee Onboarding";
                values.status = false;
            }
            else
            {
                objODBCDatareader1.Close();
                msSQL = " select task_name from sys_mst_ttask where task_gid='" + task_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from sys_mst_ttask where task_gid='" + task_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Task Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                    msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                             "master_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'Task'," +
                             "'" + lsmaster_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }

        }

        public void DaGetTaskMultiselectList(string task_gid, MdlTask objmaster)
        {

            msSQL = " SELECT GROUP_CONCAT(distinct(b.assignedto_name) SEPARATOR ', ') as assignedto_name, " +
                    " GROUP_CONCAT(distinct(c.escalationmailto_name) SEPARATOR ', ') as escalationmailto_name FROM sys_mst_ttask a " +
                    " left join sys_mst_ttask2assignedto b on a.task_gid = b.task_gid" +
                    " left join sys_mst_ttask2escalationmailto c on a.task_gid = c.task_gid" +              
                    " where a.task_gid='" + task_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objmaster.assignedto_name = objODBCDatareader["assignedto_name"].ToString();
                objmaster.escalationmailto_name = objODBCDatareader["escalationmailto_name"].ToString();
               
            }
            objODBCDatareader.Close();
        }

    }
}