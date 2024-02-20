using ems.system.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace ems.system.DataAccess
{
    public class DaUser
    {
        dbconn objdbconn = new dbconn();
        OdbcDataReader objODBCDatareader;
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objodbcDataReader;
        DataTable dt_levelone, dt_leveltwo, dt_levelthree, dt_datatable;
        string menu_ind_up_first = string.Empty;
        string menu_ind_down_first = string.Empty;
        string menu_ind_up_second = string.Empty;
        string menu_ind_down_second = string.Empty;
        string msGetGID, lsdate, msGetdtlGID, gid , msPRSQL, msSAP,employee_gid;
        string lsmobilenum, lsemployee_photo, lscompany_logo_path, lswelcome_logo, lsemployee_gid, lsPR_lists, lssystem_type, msSRE,lblEmployeeGID, lblUserCode, lsemployeereporting_to, lsdocument_flag, lsapply_leave;
        TimeSpan time;
        int mnResult;
        DateTime lsevent_time;
        public void userDataFromDB(string user_gid, UserData values)
        {
            try
            {
                msSQL = " SELECT a.user_code, CONCAT(a.user_firstname,' ',a.user_lastname) as user_name,d.employee_photo ,b.designation_name,c.department_name FROM hrm_mst_temployee d " +
                       " LEFT JOIN adm_mst_tuser a ON a.user_gid = d.user_gid " +
                       " LEFT JOIN adm_mst_tdesignation b ON b.designation_gid = d.designation_gid " +
                       " LEFT JOIN hrm_mst_tdepartment c ON c.department_gid = d.department_gid WHERE d.user_gid = '" + user_gid + "'";
                objodbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objodbcDataReader.HasRows)
                {
                    values.user_code = objodbcDataReader["user_code"].ToString();
                    values.user_name = objodbcDataReader["user_name"].ToString();
                    if (objodbcDataReader["employee_photo"].ToString() != "")
                    {
                        values.user_photo = objodbcDataReader["employee_photo"].ToString();
                    }
                    else
                    {
                        values.user_photo = "N";
                    }

                    values.user_designation = objodbcDataReader["designation_name"].ToString();
                    values.user_department = objodbcDataReader["department_name"].ToString();
                    values.message = "success";
                    values.status = true;
                }
                else
                {
                    values.message = "failure";
                    values.status = false;
                }
            }
            catch
            {
                values.message = "error";
                values.status = false;
            }
        }

        public void loadMenuFromDB(string user_gid, menu_response values)
        {

            var getmenu = new List<sys_menu>();
            //var Get_Summary = new List<sys_sub1menu>();
            // List<sys_menu> getmenu;

            try
            {


                msSQL = " SELECT a.module_gid,b.module_name,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                             " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' AND menu_level=1  order by b.display_order asc";
                dt_levelone = objdbconn.GetDataTable(msSQL);
                if (dt_levelone.Rows.Count != 0)
                {
                    foreach (DataRow drOne in dt_levelone.Rows)
                    {


                        msSQL = " SELECT a.module_gid,b.module_name,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                                " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' " +
                                " AND b.menu_level = 2 AND b.module_gid like '" + drOne["module_gid"].ToString() + "%' order by b.display_order asc";
                        dt_leveltwo = objdbconn.GetDataTable(msSQL);
                        var getmenu2 = new List<sys_submenu>();
                        if (dt_leveltwo.Rows.Count != 0)
                        {
                            //menu_ind_up_first = "fa fa-angle-up";
                            //menu_ind_down_first = "fa fa-angle-down";

                            foreach (DataRow drTwo in dt_leveltwo.Rows)
                            {

                                msSQL = " SELECT a.module_gid,b.module_name,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                                        " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' " +
                                        " AND b.menu_level = 3 AND b.module_gid like '" + drTwo["module_gid"].ToString() + "%' order by b.display_order asc ";
                                dt_levelthree = objdbconn.GetDataTable(msSQL);
                                var Get_Summary = new List<sys_sub1menu>();
                                if (dt_levelthree.Rows.Count != 0)
                                {
                                    menu_ind_up_second = "fa fa-angle-up";
                                    menu_ind_down_second = "fa fa-angle-down";
                                    Get_Summary = dt_levelthree.AsEnumerable().Select(row =>
                                      new sys_sub1menu
                                      {

                                          text = row["module_name"].ToString(),
                                          sref = row["sref"].ToString(),
                                          icon = row["icon"].ToString(),
                                          // sub_icon = row["icon_name"].ToString()
                                      }).ToList();
                                }
                                else
                                {
                                    menu_ind_up_second = "";
                                    menu_ind_down_second = "";
                                }
                                dt_levelthree.Clear();
                                getmenu2.Add(new sys_submenu
                                {
                                    text = drTwo["module_name"].ToString(),
                                    sref = drTwo["sref"].ToString(),
                                    menu_indication = menu_ind_up_second,
                                    menu_indication1 = menu_ind_down_second,
                                    // sub_icon = drTwo["icon_name"].ToString(),
                                    submenu = Get_Summary
                                });

                            }

                            dt_leveltwo.Clear();
                        }
                        else
                        {
                            menu_ind_up_first = "";
                            menu_ind_down_first = "";
                        }
                        //var getmenu = new List<sys_menu>();
                        getmenu.Add(new sys_menu
                        {
                            text = drOne["module_name"].ToString(),
                            sref = drOne["sref"].ToString(),
                            icon = drOne["icon"].ToString(),
                            menu_indication = menu_ind_up_first,
                            menu_indication1 = menu_ind_down_first,
                            label = "label label-success",
                            submenu = getmenu2
                        });


                        values.menu_list = getmenu;
                    }

                }
           
                dt_levelone.Clear();
            }
            catch (Exception)
            {
                values.status = false;
            }
            finally
            {
            }

        }

        // Module Master Summary
        public void DaGetEntitySummary(entityllist values)
        {
            msSQL = " select  entity_gid,entity_code, entity_name, entity_description, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by, a.created_date " +
                    " from adm_mst_tentity a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by entity_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<entitydtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new entitydtl
                    {
                        entity_gid = dt["entity_gid"].ToString(),
                        entity_code = dt["entity_code"].ToString(),
                        entity_name = dt["entity_name"].ToString(),
                        entity_description = dt["entity_description"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.entitydtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetFinancialyearactivitiesSummary(financialyearactivitieslist values)
        {
            msSQL = " select finyear_gid, date_format(fyear_start,'%m-%Y') as fyear_start, " +
                " year(fyear_start) as start_year,year(fyear_end) as end_year, " +
                " date_format(fyear_end,'%m-%Y') as fyear_end, yearendactivity_flag, " +
                " unauditedclosing_flag, auditedclosing_flag, created_by, created_date, " +
                " updated_by, updated_date "+
                " from adm_mst_tyearendactivities where 0=0" ;


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<financialyearactivitiesdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new financialyearactivitiesdtl
                    {
                        fyear_start = dt["fyear_start"].ToString(),
                        fyear_end = dt["fyear_end"].ToString(),
                    });
                    values.financialyearactivitiesdtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetLicensemanagementSummary(licensemanagementlist values)
        {
            msSQL = " SELECT license_gid, host_server, machine_id, server_name,port, user_name, password, active_flag, message FROM adm_mst_tlicense ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<licensemanagementdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new licensemanagementdtl
                    {
                        license_gid = dt["license_gid"].ToString(),
                        host_server = dt["host_server"].ToString(),
                        machine_id = dt["machine_id"].ToString(),
                        server_name = dt["server_name"].ToString(),
                        port = dt["port"].ToString(),
                        user_name = dt["user_name"].ToString(),
                        password = dt["password"].ToString(),
                        active_flag = dt["active_flag"].ToString(),
                        message = dt["message"].ToString(),
                    });
                    values.licensemanagementdtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetUserreportSummary(userreportlist values)
        {
            msSQL = " select a.user_gid,b.employee_gid,e.branch_name,a.user_code," +
                " case when a.user_status='Y' then 'Active' when a.user_status='N' then 'Inactive' end as user_status," +
                " CONCAT(a.user_firstname,'-',a.user_lastname) AS user_name,b.employee_mobileno," +
                " d.designation_name,c.department_name from adm_mst_tuser a" +
                " left join hrm_mst_temployee b on b.user_gid=a.user_gid" +
                " left join hrm_mst_tdepartment c on c.department_gid=b.department_gid" +
                " left join adm_mst_tdesignation d on d.designation_gid=b.designation_gid" +
                " left join hrm_mst_tbranch e on e.branch_gid=b.branch_gid";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<userreportdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new userreportdtl
                    {
                        branch_name = dt["branch_name"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        designation_name = dt["designation_name"].ToString(),
                        user_code = dt["user_code"].ToString(),
                        user_name = dt["user_name"].ToString(),
                        employee_mobileno = dt["employee_mobileno"].ToString(),
                        user_status = dt["user_status"].ToString(),
                    });
                    values.userreportdtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetEmployeereportSummary(employeereportlist values)
        {
            msSQL = " Select distinct a.user_gid,c.employee_gid,c.passport_no,c.workpermit_no,c.passport_expiredate,c.workpermit_expiredate," +
                     " concat(a.user_firstname,' ',a.user_lastname) as user_name,a.user_code," +
                     " e.branch_name, g.department_name,d.designation_name,c.employee_gender,c.employee_joiningdate," +
                     " concat(j.address1,',',j.address2,',', j.city,',', j.state,',',k.country_name,',', j.postal_code) as employee_address," +
                     " c.useraccess, a.user_status, cast(concat(y.perhour_rate, ' / ' ,x.daysalary_rate) as char) as salary,y.permonth_rate, c.fin_no,y.permonth_rate" +
                     " FROM adm_mst_tuser a " +
                     " left join hrm_mst_temployee c on a.user_gid = c.user_gid" +
                     " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid" +
                     " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid" +
                     " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid" +
                     " left join adm_mst_taddress j on c.employee_gid=j.parent_gid" +
                     " left join adm_mst_tcountry k on j.country_gid=k.country_gid" +
                     " left join pay_trn_temployee2wage x on c.employee_gid=x.employee_gid" +
                     " left join pay_mst_tdaysalarymaster y on x.daysalary_gid=y.daysalary_gid" +
                     " where a.user_status='Y'";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<employeereportdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new employeereportdtl
                    {

                        branch_name = dt["branch_name"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        designation_name = dt["designation_name"].ToString(),
                        user_code = dt["user_code"].ToString(),
                        user_name = dt["user_name"].ToString(),
                        user_status = dt["user_status"].ToString(),
                        passport_no = dt["passport_no"].ToString(),
                        workpermit_no = dt["workpermit_no"].ToString(),
                        passport_expiredate = dt["passport_expiredate"].ToString(),
                        workpermit_expiredate = dt["workpermit_expiredate"].ToString(),
                        employee_gender = dt["employee_gender"].ToString(),
                        employee_joiningdate = dt["employee_joiningdate"].ToString(),
                        salary = dt["salary"].ToString(),
                        permonth_rate = dt["permonth_rate"].ToString(),
                        fin_no = dt["fin_no"].ToString(),
                    });
                    values.employeereportdtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetActivateemployeeSummary(activateemployeelist values)
        {
            msSQL = " select   distinct a.user_gid,a.user_password,c.employee_emailid, CONCAT(a.user_firstname,' ',a.user_lastname) as employee_name," +
                     " a.user_code," +
                     " b.usergroup_name," +
                     " d.designation_name ,g.department_name,c.employee_gid,date(c.employee_joiningdate) as joining_date,c.exit_date," +
                    " e.branch_name,f.biometric_status,f.biometric_gid,c.employee_mobileno as contact ," +
                    " CASE " +
                    " WHEN a.user_status = 'Y' THEN 'Active'  " +
                    " WHEN a.user_status = 'N' THEN 'Inactive' " +
                    " END as user_status" +
                    " FROM adm_mst_tuser a " +
                    " left join adm_mst_tusergroup b on a.usergroup_gid = b.usergroup_gid " +
                    " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                    " left Join hrm_mst_tbiometric f on c.employee_gid = f.employee_gid " +
                    " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                    " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                    " left join hrm_mst_tdepartment g on c.department_gid = g.department_gid " +
                    " WHERE a.user_status<>'Y'";



            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<activateemployeedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new activateemployeedtl
                    {
                        branch_name = dt["branch_name"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        designation_name = dt["designation_name"].ToString(),
                        user_code = dt["user_code"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        joining_date = dt["joining_date"].ToString(),
                        exit_date = dt["exit_date"].ToString(),
                        contact = dt["contact"].ToString(),
                        user_status = dt["user_status"].ToString(),
                    });
                    values.activateemployeedtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetDesignationSummary(designationlist values)
        {
            msSQL = " select  designation_gid,designation_code,designation_name,  CONCAT(b.user_firstname,' ',b.user_lastname) as created_by, a.created_date " +
                    " from adm_mst_tdesignation a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by designation_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<designationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new designationdtl
                    {
                        designation_gid = dt["designation_gid"].ToString(),
                        designation_code = dt["designation_code"].ToString(),
                        designation_name = dt["designation_name"].ToString(),

                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.designationdtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void projectprivilege(string user_gid, project_list values)
        {

            msSQL = " SELECT a.module_gid FROM adm_mst_tprivilege a " +
                    " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' AND menu_level=1  order by b.display_order asc";
            dt_levelone = objdbconn.GetDataTable(msSQL);
            if (dt_levelone.Rows.Count != 0)
            {
                values.privileges = dt_levelone.AsEnumerable().Select(row => new privilege
                {
                    project = row["module_gid"].ToString()
                }).ToList();
            }
            objdbconn.CloseConn();
        }
        public void privilegelevel3(string user_gid, projectlist values)
        {

            msSQL = " SELECT a.module_gid FROM adm_mst_tprivilege a " +
                    " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' AND menu_level=3 order by b.display_order asc";
            dt_levelone = objdbconn.GetDataTable(msSQL);
            if (dt_levelone.Rows.Count != 0)
            {
                values.privilegelevel3 = dt_levelone.AsEnumerable().Select(row => new privilegelevel3
                {
                    project = row["module_gid"].ToString()
                }).ToList();
            }
            objdbconn.CloseConn();
        }

        public void getcompanydetails(companydetails values)
        {
            msSQL = "select welcome_logo,companylogo_responsive,company_name from adm_mst_tcompany where 1=1";
            objodbcDataReader = objdbconn.GetDataReader(msSQL);
            if (objodbcDataReader.HasRows == true)
            {
                values.company_logo = objodbcDataReader["welcome_logo"].ToString();
                values.companylogo_responsive = objodbcDataReader["companylogo_responsive"].ToString();
                values.company_name = objodbcDataReader["company_name"].ToString();
            }
            objodbcDataReader.Close();
            values.status = true;
        }

        public bool GetMonthlyAttendence(string user_gid, monthlyAttendence values)
        {
            try
            {

                values.monthyear = DateTime.Now.ToString("MMMM yyyy");
                lsdate = DateTime.Now.ToString("MM");
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                employee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select concat(c.user_code,'<br>',c.user_firstname,' ' ,c.user_lastname) as 'Employee'  , " +
                       " cast((select count(attendance_gid) from hrm_trn_tattendance x  where (x.employee_gid = '" + employee_gid + "') " +
                       " and MONTH(x.attendance_date) = '" + lsdate + "' and year(x.attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' group by a.employee_gid) as char) as 'totaldays', " +
                       " cast((select count(attendance_type) from hrm_trn_tattendance x  where (x.employee_gid = '" + employee_gid + "') " +
                       " and attendance_type = 'P' and MONTH(x.attendance_date) = '" + lsdate + "'  and year(x.attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' group by a.employee_gid) as char) as Present, " +
                       " cast((select count(attendance_type) from hrm_trn_tattendance x " +
                       " where (x.employee_gid = '" + employee_gid + "') and attendance_type = 'A' and MONTH(x.attendance_date) = '" + lsdate + "'  and year(x.attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' " +
                       " group by a.employee_gid) as char) as 'Absent', " +
                       " cast((select ifnull(SUM(if (x.day_session = 'NA','1','0.5')),0) as count from hrm_trn_tattendance x " +
                       " where (x.employee_gid = '" + employee_gid + "') and employee_attendance = 'Leave' and MONTH(x.attendance_date) = '" + lsdate + "' " +
                       "  and year(x.attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' group by a.employee_gid ) as char) as 'Leave', " +
                       " cast((select count(attendance_type) from hrm_trn_tattendance x " +
                       "  where (x.employee_gid = '" + employee_gid + "') and employee_attendance = 'Holiday' and  MONTH(x.attendance_date) = '" + lsdate + "'" +
                       "  and year(x.attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' group by a.employee_gid) as char) as 'Holiday', " +
                       " cast((select count(attendance_type) from hrm_trn_tattendance x " +
                       " where(a.employee_gid = x.employee_gid) and attendance_type = 'WH' and MONTH(x.attendance_date) = '" + lsdate + "'" +
                       "  and year(x.attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' group by a.employee_gid ) as char) as 'Weekoff' " +
                       " from hrm_mst_temployee a " +
                       " inner join adm_mst_tuser c on a.user_gid = c.user_gid " +
                       " left join hrm_trn_temployeetypedtl h on a.employee_gid = h.employee_gid " +
                       " where a.attendance_flag = 'Y' and a.employee_gid = '" + employee_gid + "'";
                objodbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objodbcDataReader.HasRows == true)
                {
                    if (objodbcDataReader["totaldays"].ToString() == "")
                    {
                        values.totalDays = "0";
                    }
                    else
                    {
                        values.totalDays = objodbcDataReader["totaldays"].ToString();
                    }

                    if (objodbcDataReader["Present"].ToString() == "")
                    {
                        values.countPresent = "0";
                    }
                    else
                    {
                        values.countPresent = objodbcDataReader["Present"].ToString();
                    }
                    if (objodbcDataReader["Absent"].ToString() == "")
                    {
                        values.countAbsent = "0";
                    }
                    else
                    {
                        values.countAbsent = objodbcDataReader["Absent"].ToString();
                    }
                    if (objodbcDataReader["Leave"].ToString() == "")
                    {
                        values.countLeave = "0";
                    }
                    else
                    {
                        values.countLeave = objodbcDataReader["Leave"].ToString();
                    }
                    if (objodbcDataReader["Holiday"].ToString() == "")
                    {
                        values.countholiday = "0";
                    }
                    else
                    {
                        values.countholiday = objodbcDataReader["Holiday"].ToString();
                    }
                    if (objodbcDataReader["Weekoff"].ToString() == "")
                    {
                        values.countWeekOff = "0";
                    }
                    else
                    {
                        values.countWeekOff = objodbcDataReader["Weekoff"].ToString();
                    }

                }
                objodbcDataReader.Close();

                var getdata = new List<last6MonthAttendence_list>();
                msSQL = " select concat(CAST(monthname(attendance_date) AS char),' ',year(attendance_date)) as monthname,count(attendance_gid) as total, " +
                       " (select count(attendance_gid) from hrm_trn_tattendance b where monthname(a.attendance_date) = monthname(b.attendance_date) " +
                       " and year(a.attendance_date) = year(b.attendance_date) and  b.employee_gid = '" + employee_gid + "' and attendance_type = 'P' group by monthname(attendance_date)) AS present, " +
                       " (select count(attendance_gid) from hrm_trn_tattendance b where monthname(a.attendance_date) = monthname(b.attendance_date) " +
                       " and year(a.attendance_date) = year(b.attendance_date) and  b.employee_gid = '" + employee_gid + "' and attendance_type = 'A'  group by monthname(attendance_date)) AS absent " +
                       " from hrm_trn_tattendance a where employee_gid = '" + employee_gid + "' " +
                       " and  year(attendance_date) = '" + DateTime.Now.ToString("yyyy") + "' " +
                       " group by monthname(attendance_date) ORDER BY MONTH(attendance_date) desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    if (dt_datatable.Rows.Count < 5)
                    {
                        int count = 5 - dt_datatable.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            getdata.Add(new last6MonthAttendence_list
                            {
                                monthname = "",
                                countPresent = "",
                                countAbsent = ""
                            });
                        }
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdata.Add(new last6MonthAttendence_list
                        {
                            monthname = dr_datarow["monthname"].ToString(),
                            countPresent = dr_datarow["present"].ToString(),
                            countAbsent = dr_datarow["absent"].ToString()
                        });
                    }


                    values.last6MonthAttendence_list = getdata;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public bool DaCompanypolicy(company_policy values)
        {
            try
            {
                msSQL = "select policy_name,policy_desc from hrm_trn_tpolicymanagement ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_companypolicy = new List<policy_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        get_companypolicy.Add(new policy_list
                        {
                            company_policies = dt["policy_name"].ToString(),
                            policies_description = dt["policy_desc"].ToString()
                        });
                        values.policy_list = get_companypolicy;
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public bool DaGetTeam(string user_gid, myteam values)
        {
            msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
            lsemployeereporting_to = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select c.user_code, concat(c.user_firstname,' ',c.user_lastname) as user_firstname,concat(g.user_firstname,' ',g.user_lastname) as employeereporting, b.employee_gid,d.designation_name,b.employee_mobileno, " +
                   " e.department_name,b.employee_photo from adm_mst_tmodule2employee a " +
                   " inner join hrm_mst_temployee b on a.employee_gid = b.employee_gid " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                   " left join adm_mst_tdesignation d on b.designation_gid = d.designation_gid " +
                   " left join hrm_mst_tdepartment e on e.department_gid=b.department_gid " +
                   " left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid  " +
                   " left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                   " where a.employeereporting_to = '" + lsemployeereporting_to + "' and module_gid = 'HRM' and a.hierarchy_level > 1 and  c.user_gid='" + user_gid + "'  order by c.user_firstname asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_myteam = new List<myteam_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["employee_mobileno"].ToString() == "")
                    {
                        lsmobilenum = ".";
                    }
                    else
                    {
                        lsmobilenum = dt["employee_mobileno"].ToString();
                    }
                    if (dt["employee_photo"].ToString() == "")
                    {
                        lsemployee_photo = "N";
                    }
                    else
                    {
                        lsemployee_photo = dt["employee_photo"].ToString();
                    }
                    get_myteam.Add(new myteam_list
                    {
                        employeereporting = dt["employeereporting"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_code = dt["user_code"].ToString(),
                        employee_name = dt["user_firstname"].ToString(),
                        designation = dt["designation_name"].ToString(),
                        employee_photo = lsemployee_photo,
                        employee_mobileno = lsmobilenum,
                        department = dt["department_name"].ToString()
                    });
                }
                values.myteam_list = get_myteam;
            }
            dt_datatable.Dispose();

            return true;
        }
        public bool DamonthlyAttendenceReport(string user_gid, monthlyAttendenceReport values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select date_format(a.shift_date,'%d-%m-%Y') as shift_date,a.attendance_date,b.shifttype_name, " +
                      " a.login_time,a.logout_time, " +
                      " case when a.attendance_type = 'A' then 'Absent' " +
                      " when a.attendance_type = 'P' then 'Present' " +
                      " when a.attendance_type = 'WH' then 'Weekoff' " +
                      " Else a.attendance_type end as attendance_type " +
                      " from hrm_trn_tattendance a " +
                      " left join hrm_mst_tshifttype b on a.shifttype_gid = b.shifttype_gid " +
                      "where employee_gid = '" + lsemployee_gid + "'  order by attendance_date desc Limit 30";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_report = new List<monthlyAttendenceReport_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_report.Add(new monthlyAttendenceReport_list
                        {
                            attendance_date = (dr_datarow["shift_date"].ToString()),
                            shift_name = (dr_datarow["shifttype_name"].ToString()),
                            Login_time = (dr_datarow["login_time"].ToString()),
                            logout_time = (dr_datarow["logout_time"].ToString()),
                            attendance_type = dr_datarow["attendance_type"].ToString()

                        });
                    }
                    values.monthlyAttendenceReport_list = get_report;
                }
                dt_datatable.Dispose();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public bool DaGetApplyLeaveSummary(string user_gid,  getleavedetails values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select a.leave_gid,a.leavetype_gid,a.document_name,date_format(a.leave_applydate,'%d-%m-%Y') as leave_applydate,date_format(a.leave_fromdate,'%d-%m-%Y') as leave_fromdate,date_format(a.leave_todate,'%d-%m-%Y') as leave_todate,a.leave_noofdays,b.leavetype_name, " +
                  " a.leave_reason,a.leave_status,concat(d.user_firstname,' ',d.user_lastname) as leave_approvedby " +
                  " from hrm_trn_tleave a " +
                  " left join hrm_mst_tleavetype b on a.leavetype_gid=b.leavetype_gid " +
                  " left join hrm_mst_temployee c on a.leave_approvedby=c.employee_gid " +
                  " left join adm_mst_tuser d on d.user_gid=c.user_gid " +
                  " where a.employee_gid='" + lsemployee_gid + "' order by a.leave_applydate desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<leave_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (dr_datarow["document_name"].ToString() != "")
                        {
                            lsdocument_flag = "Y";
                        }
                        else
                        {
                            lsdocument_flag = "N";
                        }
                        getleave.Add(new leave_list
                        {
                            leavetype_gid = (dr_datarow["leave_gid"].ToString()),
                            leavetype_name = (dr_datarow["leavetype_name"].ToString()),
                            leave_from = (dr_datarow["leave_fromdate"].ToString()),
                            leave_to = (dr_datarow["leave_todate"].ToString()),
                            noofdays_leave = (dr_datarow["leave_noofdays"].ToString()),
                            leave_reason = (dr_datarow["leave_reason"].ToString()),
                            approval_status = (dr_datarow["leave_status"].ToString()),
                            approved_by = (dr_datarow["leave_approvedby"].ToString()),
                            leave_applydate = (dr_datarow["leave_applydate"].ToString()),
                            document_name = lsdocument_flag
                        });
                    }
                    values.leave_list = getleave;
                }
                dt_datatable.Dispose();
                //fnopeningbalance.openingbalance(employee_gid);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool DaGetLeaveType(string user_gid, leavecountdetails values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
                //fnopeningbalance.openingbalance(employee_gid);
                var getdata = new List<leavetype_list>();
                msSQL = " select b.leavetype_name,a.total_leavecount,a.leave_taken,a.available_leavecount,b.leavetype_gid from hrm_mst_tleavecreditsdtl a " +
                        " left join hrm_mst_tleavetype b on a.leavetype_gid = b.leavetype_gid " +
                        " where a.employee_gid='" + lsemployee_gid + "' and a.month='" + DateTime.Now.ToString("MM") + "' and a.year ='" + DateTime.Now.ToString("yyyy") + "'" +
                        " and active_flag='Y'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {

                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (Convert.ToDouble(dr_datarow["available_leavecount"]) <= 0)
                        {
                            lsapply_leave = "Y";
                        }
                        else
                        {
                            lsapply_leave = "N";
                        }
                        getdata.Add(new leavetype_list

                        {
                            leavetype_gid = dr_datarow["leavetype_gid"].ToString(),
                            leavetype_name = dr_datarow["leavetype_name"].ToString(),
                            count_leavetaken = Convert.ToDouble(dr_datarow["leave_taken"]),
                            count_leaveavailable = Convert.ToDouble(dr_datarow["available_leavecount"]),
                            lsapply_leave = lsapply_leave
                        });

                    }

                    values.leavetype_list = getdata;
                }
                dt_datatable.Dispose();


                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DaGetHoliday( string user_gid, holidaycalender values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select  b.holiday_gid,employee_gid,date_format(b.holiday_date,'%d-%m-%Y')as holiday_date,holiday_name, " +
                    " cast(dayname(b.holiday_date) as char) as holiday_dayname from hrm_mst_tholiday2employee a " +
                    " left join hrm_mst_tholiday b on a.holiday_gid = b.holiday_gid where employee_gid = '" + lsemployee_gid + "' and " +
                    " year(b.holiday_date) >= '" + DateTime.Now.ToString("yyyy") + " ' order by DATE(b.holiday_date) asc, b.holiday_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_holiday = new List<holidaycalender_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        get_holiday.Add(new holidaycalender_list
                        {
                            holiday_date = dt["holiday_date"].ToString(),
                            holiday_dayname = dt["holiday_dayname"].ToString(),
                            holiday_name = dt["holiday_name"].ToString()
                        });
                    }
                    values.holidaycalender_list = get_holiday;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }


        public bool DaGettodayactivity(string user_gid, eventdetail values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select date_format(event_date,'%d-%m-%Y') as event_date,event_title,event_time " +
                    " from hrm_trn_treminder where created_by='" + lsemployee_gid + "' " +
                    " and event_date like '%" + DateTime.Now.ToString("yyyy-MM-dd") + "%'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_event = new List<createevent>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        if (dt["event_time"].ToString() == "")
                        {
                            time = TimeSpan.Parse("00:00".ToString());
                        }
                        else
                        {
                            time = TimeSpan.Parse(dt["event_time"].ToString());
                        }
                        get_event.Add(new createevent
                        {
                            time = time,
                            today_event = dt["event_date"].ToString(),
                            event_title = dt["event_title"].ToString()
                        });
                    }
                    values.createevent = get_event;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        public bool DaGetEvent(string user_gid, eventdetail values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select date_format(event_date,'%Y-%m-%d') as event_date,event_title,event_time " +
                   " from hrm_trn_treminder where created_by='" + lsemployee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_event = new List<createevent>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        if (dt["event_time"].ToString() == "")
                        {
                            lsevent_time = Convert.ToDateTime("00:00".ToString());
                        }
                        else
                        {
                            lsevent_time = Convert.ToDateTime(dt["event_time"].ToString());
                        }
                        get_event.Add(new createevent
                        {
                            event_time = lsevent_time,
                            event_date = Convert.ToDateTime(dt["event_date"].ToString()),
                            event_title = dt["event_title"].ToString()
                        });
                    }
                    values.createevent = get_event;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Event Created Successfully";
                return true;
            }
            catch
            {
                values.status = false;
                values.message = "Error Occured While Creating";
                return false;
            }
        }
        public bool DaGetLoginSummary(string user_gid, mdlloginsummary values)
        {
            try
            {

                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT attendancelogintmp_gid,date_format(created_date,'%d-%m-%Y') as applydate, date_format(attendance_date,'%d-%m-%Y') as attendancedate," +
                   " time_format(login_time, '%H:%i %p') as login,status,remarks from hrm_tmp_tattendancelogin " +
                   " where employee_gid = '" + lsemployee_gid + "' order by attendancelogintmp_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getloginsummary = new List<loginsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginsummary.Add(new loginsummary_list
                        {
                            attendancelogintmp_gid = (dr_datarow["attendancelogintmp_gid"].ToString()),
                            applyDate = (dr_datarow["applydate"].ToString()),
                            attendanceDate = (dr_datarow["attendancedate"].ToString()),
                            login_Time = (dr_datarow["login"].ToString()),
                            login_status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString())
                        });
                    }
                    values.loginsummary_list = getloginsummary;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public bool DaGetLogoutSummary(string user_gid, mdllogoutsummary values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT attendancetmp_gid,date_format(created_date,'%d-%m-%Y') as applydate, date_format(attendance_date,'%d-%m-%Y') as attendancedate," +
                  " time_format(logout_time, '%H:%i %p') as logout,status,remarks from hrm_tmp_tattendance " +
                  " where employee_gid = '" + lsemployee_gid + "' order by attendancetmp_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getloginsummary = new List<logoutsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginsummary.Add(new logoutsummary_list
                        {
                            attendancetmp_gid = (dr_datarow["attendancetmp_gid"].ToString()),
                            applyDate = (dr_datarow["applydate"].ToString()),
                            attendanceDate = (dr_datarow["attendancedate"].ToString()),
                            logout_Time = (dr_datarow["logout"].ToString()),
                            logout_status = dr_datarow["status"].ToString(),
                            remarks = dr_datarow["remarks"].ToString()

                        });
                    }
                    values.logoutsummary_list = getloginsummary;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public bool DaGetOnDutySummary(string user_gid, onduty_detail_list values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select a.ondutytracker_gid,a.employee_gid,concat(onduty_fromtime, ':', from_minutes) as onduty_from,onduty_reason," +
                        " concat(onduty_totime, ':', to_minutes) as onduty_to," +
                        " date_format(ondutytracker_date,'%d-%m-%Y') as ondutytracker_date,onduty_duration,ondutytracker_status," +
                        " concat(c.user_firstname, ' ', c.user_lastname) as onduty_approveby,onduty_approvedate" +
                        " from hrm_trn_tondutytracker a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.onduty_approveby" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.employee_gid = '" + lsemployee_gid + "' order by ondutytracker_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var onduty_details = new List<onduty_details>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        onduty_details.Add(new onduty_details
                        {
                            ondutytracker_gid = (dr_datarow["ondutytracker_gid"].ToString()),
                            onduty_from = dr_datarow["onduty_from"].ToString(),
                            onduty_to = dr_datarow["onduty_to"].ToString(),
                            onduty_date = (dr_datarow["ondutytracker_date"].ToString()),
                            onduty_duration = dr_datarow["onduty_duration"].ToString(),
                            ondutytracker_status = dr_datarow["ondutytracker_status"].ToString(),
                            approved_by = dr_datarow["onduty_approveby"].ToString(),
                            approved_date = dr_datarow["onduty_approvedate"].ToString(),
                            onduty_reason = dr_datarow["onduty_reason"].ToString()

                        });
                    }
                    values.onduty_details = onduty_details;
                }
                dt_datatable.Dispose();

                //objonduty_details.status = true;
                return true;
            }
            catch
            {
                //objonduty_details.status = false;
                return false;
            }
        }
        public bool DaGetCompOffSummary(string user_gid, compoff_list values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select a.compensatoryoff_gid,date_format(a.actualworking_fromdate,'%d-%m-%Y') as actualworking_fromdate, " +
                        " date_format(a.actualworking_fromdate,'%d-%m-%Y') as actualworking_fromdate, " +
                        " date_format(a.compensatoryoff_applydate,'%d-%m-%Y') as compensatoryoff_applydate, " +
                        " a.compoff_noofdays, " +
                        " a.compensatoryoff_reason,a.compensatoryoff_status from hrm_trn_tcompensatoryoff a " +
                      " where a.employee_gid ='" + lsemployee_gid + "' order by created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var compoff_details = new List<compoffSummary_details>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        compoff_details.Add(new compoffSummary_details
                        {
                            compensatoryoff_gid = (dr_datarow["compensatoryoff_gid"].ToString()),
                            compoff_date = (dr_datarow["compensatoryoff_applydate"].ToString()),
                            atual_working = (dr_datarow["actualworking_fromdate"].ToString()),

                            compoff_reason = dr_datarow["compensatoryoff_reason"].ToString(),
                            compoff_status = dr_datarow["compensatoryoff_status"].ToString()

                        });
                    }
                    values.compoffSummary_details = compoff_details;
                }
                dt_datatable.Dispose();

                //objcompoff_details.status = true;
                return true;
            }
            catch
            {
                // objcompoff_details.status = false;
                return false;
            }
        }

        public bool DaGetPermissionSummary(string user_gid, permission_details_list values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select a.permissiondtl_gid,a.permission_gid,date_format(a.permission_date,'%d-%m-%Y') as applydate,a.permission_applydate, " +
                        " concat(a.permission_fromhours,':',a.permission_frommins) as permission_fromhours , " +
                        " concat(a.permission_tohours,':',a.permission_tomins) as permission_tohours, " +
                        " concat(a.permission_totalhours,':',a.total_mins) as permission_totalhours, " +
                     " a.permission_reason,a.permission_status,concat(c.user_firstname,' ',c.user_lastname) as approvedby,a.permission_approveddate from hrm_trn_tpermissiondtl a " +
                    " left join hrm_mst_temployee b on b.employee_gid =a.permission_approvedby " +
                    "left join adm_mst_tuser c on c.user_gid= b.user_gid where a.employee_gid ='" + lsemployee_gid + "' order by a.permissiondtl_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var permission_details = new List<permissionSummary_details>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        permission_details.Add(new permissionSummary_details
                        {
                            permission_gid = (dr_datarow["permission_gid"].ToString()),
                            permissiondtl_gid = (dr_datarow["permissiondtl_gid"].ToString()),
                            permission_date = (dr_datarow["applydate"].ToString()),
                            permission_applydate = (dr_datarow["permission_applydate"].ToString()),
                            permission_from = (dr_datarow["permission_fromhours"].ToString()),
                            permission_to = dr_datarow["permission_tohours"].ToString(),
                            permission_total = dr_datarow["permission_totalhours"].ToString(),
                            permission_reason = dr_datarow["permission_reason"].ToString(),
                            permission_status = dr_datarow["permission_status"].ToString(),
                            approved_by = dr_datarow["approvedby"].ToString(),
                            approved_date = dr_datarow["permission_approveddate"].ToString()

                        });
                    }
                    values.permissionSummary_details = permission_details;
                }
                dt_datatable.Dispose();


                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DaGetEmployeename(string user_gid, holidaycalender values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select concat(user_firstname,' ',user_lastname) As Name ,(select company_logo_path from adm_mst_tcompany) as company_logo_path, DATE_FORMAT(NOW(), '%d-%m-%Y %H:%i:%s') as currentdatetime  from adm_mst_tuser  " +
                " left join hrm_mst_temployee using(user_gid) where employee_gid='" + lsemployee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_holiday = new List<employeename_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        get_holiday.Add(new employeename_list
                        {
                            Name = dt["Name"].ToString(),
                            company_logo_path = dt["company_logo_path"].ToString(),
                            currentdatetime = dt["currentdatetime"].ToString(),
                        });
                    }
                    values.employeename_list = get_holiday;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        public bool DaGetEmployeeList(string user_gid, holidaycalender values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " Select distinct a.user_gid,c.useraccess,c.employee_emailid,c.employee_mobileno,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , " +
                     " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as Name ,c.employee_joiningdate," +
                     " c.employee_gender,  " +
                     " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                     " d.designation_name,c.designation_gid,c.employee_gid,c.employee_emailid,e.branch_name,concat(v.user_firstname,' ',v.user_lastname) as employeereporting_to, " +
                     " CASE " +
                     " WHEN v.user_status = 'Y' THEN 'Active'  " +
                     " WHEN v.user_status = 'N' THEN 'Inactive' " +
                     " END as user_status,c.department_gid,c.branch_gid, e.branch_name, g.department_name " +
                     " FROM adm_mst_tuser a " +
                     " left join hrm_mst_temployee c on a.user_gid = c.user_gid " +
                     " left join hrm_mst_temployee p on c.employeereporting_to = p.employee_gid " +
                     " left join adm_mst_tuser v on p.user_gid = v.user_gid " +
                     " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                     " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                     " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                     " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                     " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                     " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                     //" left join sys_mst_tbaselocation s on s.baselocation_gid=c.baselocation_gid" +
                     " left join hrm_trn_temployeedtl m on m.permanentaddress_gid=j.address_gid " +
                     " where a.user_gid='" + user_gid + "'"+
                     " group by c.employee_gid " +
                     " order by c.employee_gid desc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_holiday = new List<employeename_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        get_holiday.Add(new employeename_list
                        {
                            Name = dt["Name"].ToString(),
                            UserCode = dt["user_code"].ToString(),
                            Designation = dt["designation_name"].ToString(),
                            Branch = dt["branch_name"].ToString(),
                            Department = dt["department_name"].ToString(),
                            Joiningdate = dt["employee_joiningdate"].ToString(),
                            Address = dt["emp_address"].ToString(),
                            Gender = dt["employee_gender"].ToString(),
                            email = dt["employee_emailid"].ToString(),
                            employeemobileNo = dt["employee_mobileno"].ToString(),









                        });
                    }
                    values.employeename_list = get_holiday;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch 
            {
                values.status = false;
                return false;
            }
        }
        public bool DaGetComapanylogo(string user_gid, myteam values)
        {
           

            msSQL = " select company_logo_path from adm_mst_tcompany ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_myteam = new List<myteam_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                   
                    if (dt["company_logo_path"].ToString() == "")
                    {
                        lscompany_logo_path = "N";
                    }
                    else
                    {
                        lscompany_logo_path = dt["company_logo_path"].ToString();
                    }
                    get_myteam.Add(new myteam_list
                    {

                        company_logo_path = lscompany_logo_path,

                    });
                }
                values.myteam_list = get_myteam;
            }
            dt_datatable.Dispose();

            return true;
        }
        public bool DaGetWelcomelogo(string user_gid, myteam values)
        {


            msSQL = " select welcome_logo,company_name from adm_mst_tcompany ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_myteam = new List<myteam_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    if (dt["welcome_logo"].ToString() == "")
                    {
                        lswelcome_logo = "N";
                    }
                    else
                    {
                        lswelcome_logo = dt["welcome_logo"].ToString();
                    }
                    get_myteam.Add(new myteam_list
                    {

                        welcome_logo = lswelcome_logo,
                        company_name = dt["company_name"].ToString(),
          
                    });
                }
                values.myteam_list = get_myteam;
            }
            dt_datatable.Dispose();

            return true;
        }
        //......................* 1. LEAVE APPROVAL Details *.................................//

        public bool DaGetLeaveApprovePendingDetails(string user_gid,getleavedetails values)
        {
            try
            {

                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " Select a.hierarchy_level " +
                      " from adm_mst_tsubmodule a " +
                      " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
                }
                objODBCDatareader.Close();

                // Leave Approval Pending Summary...//


                msSQL = " select a.leave_gid,a.document_name,date_format(a.leave_fromdate,'%d-%m-%Y') as leave_fromdate,date_format(a.leave_todate,'%d-%m-%Y') as leave_todate,a.leave_noofdays,b.leavetype_name, " +
                     " a.leave_reason,a.leave_status,concat(d.user_firstname,' ',d.user_lastname) as leave_appliedby " +
                     " from hrm_trn_tleave a " +
                    " left join hrm_mst_tleavetype b on a.leavetype_gid=b.leavetype_gid" +
                  " left join hrm_mst_temployee c on c.employee_gid=a.employee_gid " +
                  " left join adm_mst_tuser d on d.user_gid=c.user_gid" +
                  " left join hrm_mst_tdepartment e on e.department_gid=c.department_gid" +
                  " left join hrm_trn_tattendance s on s.employee_gid=a.employee_gid " +
                  " left join hrm_trn_tleavedtl w on a.leave_gid = w.leave_gid" +
                  " where 1=1 and a.leave_status = 'Pending'";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (c.employee_gid in ('" + lblEmployeeGID + "')";
                }

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";

                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or (w.leavedtl_gid in (select y.leavedtl_gid from hrm_trn_tapproval x left join hrm_trn_tleavedtl y on x.leave_gid=y.leave_gid " +
                        " where  x.approval_flag = 'N' and x.approved_by = '" + lsemployee_gid + "' )))";
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        msSQL += " and a.leave_gid in (select leaveapproval_gid from (select approved_by, " +
                             " leaveapproval_gid from hrm_trn_tapproval " +
                             " where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "' group by  leaveapproval_gid order by" +
                             " approval_gid asc) p where approved_by = '" + lsemployee_gid + "'))";
                        msPRSQL = " select distinct leaveapproval_gid from hrm_trn_tapproval where approval_flag = 'Y'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows)
                        {
                            objODBCDatareader.Close();
                            msPRSQL = " select distinct leaveapproval_gid from hrm_trn_tapproval where approval_flag = 'Y' " +
                                  " and approved_by in (" + lblEmployeeGID + ")";

                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows)
                            {
                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objODBCDatareader["leaveapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                            }
                            objODBCDatareader.Close();

                            if (lsPR_lists != "")
                            {
                                msPRSQL = " select distinct a.leave_gid from hrm_trn_tleave a " +
                             " left join hrm_trn_tapproval b on a.leave_gid = b.leaveapproval_gid " +
                             " where a.leave_status = 'Pending' and a.leave_gid in (" + lsPR_lists + ")" +
                             " and b.approval_flag = 'N' and b.approved_by = '" + lsemployee_gid + "'";
                                lsPR_lists = "";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows)
                                {
                                    while (objODBCDatareader.Read())
                                    {
                                        lsPR_lists = lsPR_lists + "'" + objODBCDatareader["leave_gid"].ToString() + "',";
                                    }
                                    if (lsPR_lists != "")
                                    {
                                        lsPR_lists = lsPR_lists.TrimEnd(',');
                                        msPRSQL += " or a.leave_gid in (" + lsPR_lists + ")";
                                    }
                                }
                                objODBCDatareader.Close();
                            }
                            objODBCDatareader.Close();
                        }
                        objODBCDatareader.Close();
                    }
                }
                objODBCDatareader.Close();

                msSQL = msSQL + " and a.employee_gid<>'" + lsemployee_gid + "' group by a.leave_gid order by date(a.created_date) desc,a.created_date asc,a.leave_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<leave_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (dr_datarow["document_name"].ToString() != "")
                        {
                            lsdocument_flag = "Y";
                        }
                        else
                        {
                            lsdocument_flag = "N";
                        }
                        getleave.Add(new leave_list
                        {
                            leave_gid = (dr_datarow["leave_gid"].ToString()),
                            leavetype_name = (dr_datarow["leavetype_name"].ToString()),
                            leave_from = (dr_datarow["leave_fromdate"].ToString()),
                            leave_to = (dr_datarow["leave_todate"].ToString()),
                            noofdays_leave = (dr_datarow["leave_noofdays"].ToString()),
                            leave_reason = (dr_datarow["leave_reason"].ToString()),
                            approval_status = (dr_datarow["leave_status"].ToString()),
                            applied_by = (dr_datarow["leave_appliedby"].ToString()),
                            document_name = lsdocument_flag
                        });
                    }
                    values.leave_list = getleave;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                return false;
            }
        }

        // Approved Summary...//

        //..............................* 2. LOGIN APPROVAL Details *..............................//

        public bool DaGetLoginApproval(string user_gid, getlogindetails values)
        {
            try
            {
                //Login Approval Pending Summary.....//
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);




                msSQL = " Select a.hierarchy_level " +
                      " from adm_mst_tsubmodule a " +
                      " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " SELECT distinct k.attendancelogintmp_gid,k.employee_gid,k.status,date_format(k.attendance_date, '%d-%m-%Y') as attendence_date, " +
                        " time_format(k.login_time, '%H:%i %p') as login_time, " +
                        " date_format(k.created_date, '%d-%m-%Y') as applydate , k.remarks, concat(n.user_code, '/', n.user_firstname, ' ', n.user_lastname, " +
                        "  '/', m.department_name) as employeename FROM hrm_tmp_tattendancelogin k " +
                        " left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                        " left join hrm_mst_tdepartment m on m.department_gid=c.department_gid" +
                        " left join adm_mst_tdesignation e on c.designation_gid = e.designation_gid " +
                        " left join adm_mst_tuser n on n.user_gid = c.user_gid   " +
                        " where 1 = 1 ";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (k.employee_gid in ('" + lsemployee_gid + "')";
                }

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNLTA'";
                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    //-------------------parallel flow starts here------------------------------//
                    if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or k.attendancelogintmp_gid in (select loginapproval_gid from hrm_trn_tloginapproval " +
                                 " where approval_flag = 'N' and approved_by = '" + lsemployee_gid + "' group by  loginapproval_gid))";

                        //----parallel flow ends here---------------------------------//
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        msSQL += " and k.attendancelogintmp_gid in (select loginapproval_gid from (select approved_by, loginapproval_gid from hrm_trn_tloginapproval " +
                                 " where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "' group by  loginapproval_gid order by loginapproval_gid asc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = " select distinct loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'Y'";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Close();
                            msPRSQL = " select distinct loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'Y' " +
                                      " and approved_by in ('" + lblEmployeeGID + "')";
                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objODBCDatareader["loginapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');

                            }
                            else
                            {
                                lsPR_lists = "";
                            }
                            objODBCDatareader.Close();
                            if (lsPR_lists != "")
                            {
                                msPRSQL = " select distinct a.attendancelogintmp_gid from hrm_tmp_tattendancelogin a " +
                             " left join hrm_trn_tloginapproval b on a.attendancelogintmp_gid = b.loginapproval_gid " +
                             " where a.status = 'Pending' and a.attendancelogintmp_gid in (" + lsPR_lists + ")" +
                             " and b.approval_flag = 'N' and b.approved_by = '" + lsemployee_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                                lsPR_lists = "";
                                if (objODBCDatareader.HasRows == true)
                                {
                                    while (objODBCDatareader.Read())
                                    {
                                        lsPR_lists = lsPR_lists + "'" + objODBCDatareader["attendancelogintmp_gid"].ToString() + "',";
                                    }
                                    if (lsPR_lists != "")
                                    {
                                        lsPR_lists = lsPR_lists.TrimEnd(',');
                                        msSQL += " or k.attendancelogintmp_gid in (" + lsPR_lists + ")";
                                    }

                                }
                                objODBCDatareader.Close();
                            }
                        }
                    }
                }
                msSAP = msSQL + " and k.employee_gid <>'" + employee_gid + "' and k.status = 'Pending' group by k.attendancelogintmp_gid " +
                                " order by date(k.created_date)desc,k.created_date asc,attendancelogintmp_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSAP);
                var getloginpending = new List<loginpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginpending.Add(new loginpending_list
                        {
                            attendancelogintmp_gid = (dr_datarow["attendancelogintmp_gid"].ToString()),
                            loginapply_date = (dr_datarow["applydate"].ToString()),
                            employee_name = (dr_datarow["employeename"].ToString()),
                            loginattendence_date = (dr_datarow["attendence_date"].ToString()),
                            login_time = (dr_datarow["login_time"].ToString()),
                            login_reason = (dr_datarow["remarks"].ToString()),
                            login_status = (dr_datarow["status"].ToString()),
                            apply_employeegid = (dr_datarow["employee_gid"].ToString()),
                        });
                    }
                    values.loginpending_list = getloginpending;
                }
                dt_datatable.Dispose();

                msSRE = msSQL + " and k.employee_gid <>'" + lsemployee_gid + "' and k.status = 'Rejected' group by k.attendancelogintmp_gid " +
                    " order by date(k.created_date)desc,k.created_date asc,attendancelogintmp_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSRE);
                var getloginrejected = new List<loginrejected_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginrejected.Add(new loginrejected_list
                        {
                            attendancelogintmp_gid = (dr_datarow["attendancelogintmp_gid"].ToString()),
                            loginapply_date = (dr_datarow["applydate"].ToString()),
                            employee_name = (dr_datarow["employeename"].ToString()),
                            loginattendence_date = (dr_datarow["attendence_date"].ToString()),
                            login_time = (dr_datarow["login_time"].ToString()),
                            login_reason = (dr_datarow["remarks"].ToString()),
                            login_status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.loginrejected_list = getloginrejected;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        //..............................* 3. LOGOUT APPROVAL  Details*.............................//

        public bool DaGetLogoutApproval(string user_gid,getlogoutdetails values)
        {
            msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
            lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " Select a.hierarchy_level " +
                  " from adm_mst_tsubmodule a " +
                  " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " SELECT distinct k.attendancetmp_gid,k.employee_gid,k.status, date_format(k.created_date,'%d-%m-%Y') as created_date, " +
                    " concat(n.user_code, '/', n.user_firstname, ' ', n.user_lastname, '/', m.department_name) as employeename, " +
                    " date_format(k.attendance_date, '%d-%m-%Y') as attendence_date, time_format(k.logout_time, '%H:%i %p') as logout_time,k.remarks " +
                 " FROM hrm_tmp_tattendance k " +
                 " left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                     " left join hrm_mst_tdepartment m on m.department_gid=c.department_gid" +
                 " left join adm_mst_tdesignation e on c.designation_gid = e.designation_gid " +
                 " left join adm_mst_tuser n on n.user_gid = c.user_gid   " +
                 " where  1 = 1 ";
            if (lblUserCode != "-1")
            {
                msSQL += " and (k.employee_gid in ('" + lsemployee_gid + "') ";
            }
            msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNALT'";
            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
            if (objODBCDatareader.HasRows == true)
            {
                //-------------------parallel flow starts here------------------------------//
                if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                {
                    msSQL += " or k.attendancetmp_gid in (select logoutapproval_gid from hrm_trn_tlogoutapproval " +
                             " where approval_flag = 'N' and approved_by = '" + lsemployee_gid + "' group by  logoutapproval_gid))";
                    //-------------------parallel flow END here------------------------------//
                }
                else
                {
                    msSQL += " and k.attendancetmp_gid in (select logoutapproval_gid from (select approved_by, " +
                            " logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "'" +
                            " group by  logoutapproval_gid order by logoutapproval_gid asc) p where approved_by = '" + lsemployee_gid + "'))";

                    msPRSQL = " select distinct logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'Y'" +
                              " and approved_by in ('" + lblEmployeeGID + "')";
                    objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        while (objODBCDatareader.Read())
                        {
                            lsPR_lists = lsPR_lists + "'" + objODBCDatareader["logoutapproval_gid"].ToString() + "',";
                        }
                        lsPR_lists = lsPR_lists.TrimEnd(',');

                    }
                    else
                    {
                        lsPR_lists = "";
                    }
                    objODBCDatareader.Close();
                    if (lsPR_lists != "")
                    {
                        msPRSQL = " select distinct a.attendancetmp_gid from hrm_tmp_tattendance a " +
                         " left join hrm_trn_tlogoutapproval b on a.attendancetmp_gid = b.logoutapproval_gid " +
                         " where a.status = 'Pending' and a.attendancetmp_gid in (" + lsPR_lists + ")" +
                         " and b.approval_flag = 'N' and b.approved_by = '" + lsemployee_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        lsPR_lists = "";
                        if (objODBCDatareader.HasRows == true)
                        {
                            while (objODBCDatareader.Read())
                            {
                                lsPR_lists = lsPR_lists + "'" + objODBCDatareader["attendancetmp_gid"].ToString() + "',";
                            }
                            if (lsPR_lists != "")
                            {
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                                msSQL += " or k.attendancetmp_gid in (" + lsPR_lists + ")";
                            }
                            objODBCDatareader.Close();
                        }
                    }
                }
            }
            objODBCDatareader.Close();
            msSAP = msSQL + " and k.employee_gid <>'" + lblEmployeeGID + "' and k.status = 'Pending' group by k.attendancetmp_gid " +
                            " order by date(k.created_date)desc,k.created_date asc,k.attendancetmp_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSAP);
            var getlogoutpending = new List<logoutpending_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlogoutpending.Add(new logoutpending_list
                    {
                        attendancelogouttmp_gid = (dr_datarow["attendancetmp_gid"].ToString()),
                        logoutapply_date = (dr_datarow["created_date"].ToString()),
                        employee_name = (dr_datarow["employeename"].ToString()),
                        logoutattendence_date = (dr_datarow["attendence_date"].ToString()),
                        logout_time = (dr_datarow["logout_time"].ToString()),
                        logout_reason = (dr_datarow["remarks"].ToString()),
                        logout_status = (dr_datarow["status"].ToString()),
                        apply_employeegid = (dr_datarow["employee_gid"].ToString()),
                    });
                }
                values.logoutpending_list = getlogoutpending;
            }
            dt_datatable.Dispose();

            msSRE = msSQL + " and k.employee_gid <>'" + lsemployee_gid + "' and k.status = 'Rejected' group by k.attendancetmp_gid " +
                           " order by date(k.created_date)desc,k.created_date asc,attendancetmp_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSRE);
            var getlogoutrejected = new List<logoutrejected_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlogoutrejected.Add(new logoutrejected_list
                    {
                        attendancelogouttmp_gid = (dr_datarow["attendancetmp_gid"].ToString()),
                        logoutapply_date = (dr_datarow["created_date"].ToString()),
                        employee_name = (dr_datarow["employeename"].ToString()),
                        logoutattendence_date = (dr_datarow["attendence_date"].ToString()),
                        logout_time = (dr_datarow["logout_time"].ToString()),
                        logout_reason = (dr_datarow["remarks"].ToString()),
                        logout_status = (dr_datarow["status"].ToString()),
                    });
                }
                values.logoutrejected_list = getlogoutrejected;
            }
            dt_datatable.Dispose();


            return true;
        }
        //..............................* 4. OD APPROVAL Details *..................................//

        public bool DaGetODSummaryDetails(string user_gid, getODdetails values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " Select a.hierarchy_level " +
                     " from adm_mst_tsubmodule a " +
                     " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
                }
                objODBCDatareader.Close();

                lsPR_lists = "";
                msSQL = " select distinct l.employee_gid,k.ondutytracker_gid,k.ondutytracker_status,date_format(k.ondutytracker_date,'%d-%m-%Y') as ondutydate, " +
                        " concat(k.onduty_fromtime,':',k.from_minutes) as onduty_fromtime,concat(k.onduty_totime,':',k.to_minutes) as onduty_totime,k.onduty_duration,date_format(k.created_date, '%d-%m-%Y') as createddate,k.onduty_reason, " +
                        " concat(n.user_code, '/', n.user_firstname, ' ', n.user_lastname, '/', m.department_name) as employee_name " +
                        "  from hrm_trn_tondutytracker k" +
                        " left join hrm_mst_temployee l on l.employee_gid=k.employee_gid" +
                        " left join hrm_mst_tdepartment m on m.department_gid=l.department_gid" +
                        " left join adm_mst_tuser n on n.user_gid=l.user_gid" +
                        " left join hrm_trn_tattendance t on t.employee_gid=k.employee_gid " +
                        " where k.ondutytracker_status='Pending' ";
                msSQL += " and (l.employee_gid in ('" + lsemployee_gid + "')) ";
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    string lsapprovaltype = (objODBCDatareader["approval_type"].ToString());
                    objODBCDatareader.Close();
                    if (lsapprovaltype == "Parallel")
                    {
                        msSQL += " or (k.ondutytracker_gid in (select ondutyapproval_gid from hrm_trn_tapproval where " +
                                 " approval_flag = 'N' and approved_by = '" + lsemployee_gid + "' group by  ondutyapproval_gid) " +
                                 " and k.ondutytracker_status ='Pending') ";
                    }
                    else
                    {
                        msSQL += " and k.ondutytracker_gid in (select ondutyapproval_gid from (select approved_by, " +
                                 " ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "'" +
                                 " group by  ondutyapproval_gid order by ondutyapproval_gid desc) p where " +
                                 " approved_by = '" + lsemployee_gid + "')";

                        msPRSQL = " select distinct ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N'";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Close();
                            msPRSQL = " select distinct ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N'" +
                                      " and approved_by = '" + lsemployee_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows == true)
                            {

                                string lsondutyapproval_gid = objODBCDatareader["ondutyapproval_gid"].ToString();
                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + lsondutyapproval_gid + "',";
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                                objODBCDatareader.Close();

                            }
                            objODBCDatareader.Close();
                            if (lsPR_lists != "")
                            {
                                msSQL += " or k.ondutytracker_gid in (" + lsPR_lists + ")";
                            }
                        }
                        objODBCDatareader.Close();
                    }
                }
                objODBCDatareader.Close();

                msSQL = msSQL + "  order by date(k.created_date) desc,k.created_date asc,k.ondutytracker_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getODpending = new List<ODpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getODpending.Add(new ODpending_list
                        {
                            ondutytracker_gid = (dr_datarow["ondutytracker_gid"].ToString()),
                            created_date = (dr_datarow["createddate"].ToString()),
                            onduty_date = (dr_datarow["ondutydate"].ToString()),
                            onduty_from = (dr_datarow["onduty_fromtime"].ToString()),
                            onduty_to = (dr_datarow["onduty_totime"].ToString()),
                            onduty_duration = (dr_datarow["onduty_duration"].ToString()),
                            onduty_reason = (dr_datarow["onduty_reason"].ToString()),
                            ondutytracker_status = (dr_datarow["ondutytracker_status"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            apply_employeegid = (dr_datarow["employee_gid"].ToString()),
                        });
                    }
                    values.ODpending_list = getODpending;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        //..............................* 6. COMPOFF APPROVAL Details *..............................//

        public bool DaGetCompoffSummaryDetails(string user_gid, getcompoffdetails values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " Select a.hierarchy_level " +
                     " from adm_mst_tsubmodule a " +
                     " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
                }
                objODBCDatareader.Close();

                lsPR_lists = "";
                msSQL = " select distinct o.compensatoryoff_gid,o.compensatoryoff_status,o.compensatoryoff_reason,date_format(o.actualworking_todate, '%d-%m-%Y') " +
                        " as Compoff_to  ,date_format(o.actualworking_fromdate, '%d-%m-%Y') as actual_working,o.compoff_noofdays, " +
                        " date_format(o.compensatoryoff_applydate, '%d-%m-%Y') as compensatoryoff_applydate, " +
                        " concat(r.user_code, '/', r.user_firstname, ' ', r.user_lastname, '/', q.department_name) as employee_name " +
                         " from hrm_trn_tcompensatoryoff o " +
                         " left join hrm_mst_temployee p on p.employee_gid=o.employee_gid" +
                         " left join hrm_mst_tdepartment q on q.department_gid=p.department_gid" +
                         " left join adm_mst_tuser r on r.user_gid=p.user_gid" +
                         " where o.compensatoryoff_status='Pending'";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (p.employee_gid in ('" + lsemployee_gid + "') and o.employee_gid<>'" + lblEmployeeGID + "' ) ";
                }
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or (o.compensatoryoff_gid in (select compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + lsemployee_gid + "' group by  compensatoryoff_gid) and o.compensatoryoff_status ='Approval Pending') ";
                    }
                    else
                    {
                        msSQL += " and o.compensatoryoff_gid in (select compensatoryoff_gid from (select approved_by, " +
                                 " compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "'" +
                                 " group by  compensatoryoff_gid order by compensatoryoff_gid desc) p where approved_by = '" + lsemployee_gid + "')";
                        msPRSQL = " select distinct compensatoryoff_gid from hrm_trn_tapproval ";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            msPRSQL = " select distinct compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' " +
                                      " and approved_by = '" + lsemployee_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                string lscompensatoryoff_gid = objODBCDatareader["compensatoryoff_gid"].ToString();

                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + lscompensatoryoff_gid + "',";

                                }
                                objODBCDatareader.Close();
                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                            }
                            objODBCDatareader.Close();
                            if (lsPR_lists != "")
                            {
                                msSQL += " or o.compensatoryoff_gid in (" + lsPR_lists + ")";
                            }
                        }
                    }
                }
                msSQL = msSQL + " group by o.compensatoryoff_gid order by" +
                            "  date(o.created_date) desc,o.created_date asc,o.compensatoryoff_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcompoffpending = new List<compoffpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcompoffpending.Add(new compoffpending_list
                        {
                            compensatoryoff_gid = (dr_datarow["compensatoryoff_gid"].ToString()),
                            Compoff_from = (dr_datarow["compensatoryoff_applydate"].ToString()),
                            Compoff_to = (dr_datarow["actual_working"].ToString()),
                            Compoff_duration = (dr_datarow["compoff_noofdays"].ToString()),
                            Compoff_reason = (dr_datarow["compensatoryoff_reason"].ToString()),
                            Compoff_status = (dr_datarow["compensatoryoff_status"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.compoffpending_list = getcompoffpending;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        //..............................* 5. PERMISSION APPROVAL  Details *..........................//

        public bool GetPermissionSummaryDetails(string user_gid, getpermissiondetails values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select system_type from hrm_mst_thrconfig " +
                       " where actualcompany_code='" + ConfigurationManager.ConnectionStrings["company_code"] + "'";
                lssystem_type = objdbconn.GetExecuteScalar(msSQL);

                lsPR_lists = "";
                msSQL = " select distinct a.permission_gid,a.permissiondtl_gid,a.permission_reason,a.permission_status,concat(a.permission_totalhours, ':', a.total_mins, ':', '00') " +
                        " as permission_total ,concat(a.permission_fromhours, ':', a.permission_frommins) as permission_from," +
                        " concat(a.permission_tohours, ':', a.permission_tomins) as permission_to ,date_format(a.permission_date, '%d-%m-%Y') as permission_date, " +
                        " date_format(a.permission_applydate, '%d-%m-%Y') as createddate, " +
                        " concat(d.user_code, '/', d.user_firstname, ' ', d.user_lastname, '/', e.department_name) as employee_name " +
                        " from hrm_trn_tpermissiondtl a left join hrm_mst_temployee i on a.employee_gid=i.employee_gid" +
                        " left join hrm_mst_tdepartment e on e.department_gid=i.department_gid " +
                        " left join adm_mst_Tuser d on d.user_gid=i.user_gid" +
                        " left join hrm_trn_temployeetypedtl j on i.employee_gid=j.employee_gid " +
                        " where a.permission_status='Pending'";
                if (lssystem_type == "AUDIT")
                {
                    msSQL += " and j.employeetype_name='Roll' ";
                }
                msSQL += " and (i.employee_gid in ('" + lsemployee_gid + "')) ";
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or (a.permissiondtl_gid in (select permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + lblEmployeeGID + "' group by  permissionapproval_gid) and a.permission_status ='Approval Pending') ";
                    }
                    else
                    {
                        msSQL += " and a.permissiondtl_gid in (select permissionapproval_gid from (select approved_by, " +
                                 " permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "'" +
                                 " group by  permissionapproval_gid order by permissionapproval_gid desc) p where approved_by = '" + lsemployee_gid + "')";

                        msPRSQL = " select distinct permissionapproval_gid from hrm_trn_tapproval ";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            msPRSQL = " select distinct permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + lsemployee_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                string lspermissionapproval_gid = objODBCDatareader["permissionapproval_gid"].ToString();

                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + lspermissionapproval_gid + "',";
                                }

                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                            }
                            objODBCDatareader.Close();
                            if (lsPR_lists != "")
                            {
                                msSQL += " or a.permissiondtl_gid in (" + lsPR_lists + ")";
                            }

                        }

                    }
                }
                msSQL = msSQL + " and a.permission_status='Pending' order by date(a.created_date) desc,a.created_date asc,a.permissiondtl_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpermissionpending = new List<permissionpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getpermissionpending.Add(new permissionpending_list
                        {
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            permission_gid = (dr_datarow["permission_gid"].ToString()),
                            permissiondtl_gid = (dr_datarow["permissiondtl_gid"].ToString()),
                            permission_date = (dr_datarow["permission_date"].ToString()),
                            permission_from = (dr_datarow["permission_from"].ToString()),
                            permission_to = (dr_datarow["permission_to"].ToString()),
                            permission_duration = (dr_datarow["permission_total"].ToString()),
                            permission_reason = (dr_datarow["permission_reason"].ToString()),
                            permission_status = (dr_datarow["permission_status"].ToString()),
                            permission_createddate = (dr_datarow["createddate"].ToString()),
                        });
                    }
                    values.permissionpending_list = getpermissionpending;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }


        public void DaDeletedashboardlogin(string attendancelogintmp_gid, createvendor values)
        {
            msSQL = "delete from sys_mst_ttaskinitiate where taskinitiate_gid='" + attendancelogintmp_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Task details Deleted successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Task";
            }
        }
           public bool DaGetLeaveapproved(string user_gid,getleavedetails values)
        {
            try
            {

                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " Select a.hierarchy_level " +
                      " from adm_mst_tsubmodule a " +
                      " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
                }
                objODBCDatareader.Close();

                // Leave Approved Summary...//


                msSQL = " select a.leave_gid,a.document_name,date_format(a.leave_fromdate,'%d-%m-%Y') as leave_fromdate,date_format(a.leave_todate,'%d-%m-%Y') as leave_todate,a.leave_noofdays,b.leavetype_name, " +
                     " a.leave_reason,a.leave_status,concat(d.user_firstname,' ',d.user_lastname) as leave_appliedby " +
                     " from hrm_trn_tleave a " +
                    " left join hrm_mst_tleavetype b on a.leavetype_gid=b.leavetype_gid" +
                  " left join hrm_mst_temployee c on c.employee_gid=a.employee_gid " +
                  " left join adm_mst_tuser d on d.user_gid=c.user_gid" +
                  " left join hrm_mst_tdepartment e on e.department_gid=c.department_gid" +
                  " left join hrm_trn_tattendance s on s.employee_gid=a.employee_gid " +
                  " left join hrm_trn_tleavedtl w on a.leave_gid = w.leave_gid" +
                  " where 1=1 and a.leave_status = 'Approved'";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (c.employee_gid in ('" + lsemployee_gid + "')";
                }

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";

                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or (w.leavedtl_gid in (select y.leavedtl_gid from hrm_trn_tapproval x left join hrm_trn_tleavedtl y on x.leave_gid=y.leave_gid " +
                        " where  x.approval_flag = 'N' and x.approved_by = '" + lsemployee_gid + "' )))";
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        msSQL += " and a.leave_gid in (select leaveapproval_gid from (select approved_by, " +
                             " leaveapproval_gid from hrm_trn_tapproval " +
                             " where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "' group by  leaveapproval_gid order by" +
                             " approval_gid asc) p where approved_by = '" + lsemployee_gid + "'))";
                        msPRSQL = " select distinct leaveapproval_gid from hrm_trn_tapproval where approval_flag = 'Y'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows)
                        {
                            objODBCDatareader.Close();
                            msPRSQL = " select distinct leaveapproval_gid from hrm_trn_tapproval where approval_flag = 'Y' " +
                                  " and approved_by in (" + lblEmployeeGID + ")";

                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows)
                            {
                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objODBCDatareader["leaveapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                            }
                            objODBCDatareader.Close();

                            if (lsPR_lists != "")
                            {
                                msPRSQL = " select distinct a.leave_gid from hrm_trn_tleave a " +
                             " left join hrm_trn_tapproval b on a.leave_gid = b.leaveapproval_gid " +
                             " where a.leave_status = 'Approved' and a.leave_gid in (" + lsPR_lists + ")" +
                             " and b.approval_flag = 'N' and b.approved_by = '" + lsemployee_gid + "'";
                                lsPR_lists = "";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows)
                                {
                                    while (objODBCDatareader.Read())
                                    {
                                        lsPR_lists = lsPR_lists + "'" + objODBCDatareader["leave_gid"].ToString() + "',";
                                    }
                                    if (lsPR_lists != "")
                                    {
                                        lsPR_lists = lsPR_lists.TrimEnd(',');
                                        msPRSQL += " or a.leave_gid in (" + lsPR_lists + ")";
                                    }
                                }
                                objODBCDatareader.Close();
                            }
                            objODBCDatareader.Close();
                        }
                        objODBCDatareader.Close();
                    }
                }
                objODBCDatareader.Close();

                msSQL = msSQL + " and a.employee_gid<>'" + lblEmployeeGID + "' group by a.leave_gid order by date(a.created_date) desc,a.created_date asc,a.leave_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<leave_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (dr_datarow["document_name"].ToString() != "")
                        {
                            lsdocument_flag = "Y";
                        }
                        else
                        {
                            lsdocument_flag = "N";
                        }
                        getleave.Add(new leave_list
                        {
                            leave_gid = (dr_datarow["leave_gid"].ToString()),
                            leavetype_name = (dr_datarow["leavetype_name"].ToString()),
                            leave_from = (dr_datarow["leave_fromdate"].ToString()),
                            leave_to = (dr_datarow["leave_todate"].ToString()),
                            noofdays_leave = (dr_datarow["leave_noofdays"].ToString()),
                            leave_reason = (dr_datarow["leave_reason"].ToString()),
                            approval_status = (dr_datarow["leave_status"].ToString()),
                            applied_by = (dr_datarow["leave_appliedby"].ToString()),
                            document_name = lsdocument_flag
                        });
                    }
                    values.leave_list = getleave;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                return false;
            }
        }

        // Approved Summary...//

        public bool DaGetLoginApproved(string user_gid, getlogindetails values)
        {
            try
            {
                //Login APPROVED Pending Summary.....//
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);




                msSQL = " Select a.hierarchy_level " +
                      " from adm_mst_tsubmodule a " +
                      " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " SELECT distinct k.attendancelogintmp_gid,k.employee_gid,k.status,date_format(k.attendance_date, '%d-%m-%Y') as attendence_date, " +
                        " time_format(k.login_time, '%H:%i %p') as login_time, " +
                        " date_format(k.created_date, '%d-%m-%Y') as applydate , k.remarks, concat(n.user_code, '/', n.user_firstname, ' ', n.user_lastname, " +
                        "  '/', m.department_name) as employeename FROM hrm_tmp_tattendancelogin k " +
                        " left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                        " left join hrm_mst_tdepartment m on m.department_gid=c.department_gid" +
                        " left join adm_mst_tdesignation e on c.designation_gid = e.designation_gid " +
                        " left join adm_mst_tuser n on n.user_gid = c.user_gid   " +
                        " where 1 = 1 ";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (k.employee_gid in ('" + lsemployee_gid + "')";
                }

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNLTA'";
                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    //-------------------parallel flow starts here------------------------------//
                    if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or k.attendancelogintmp_gid in (select loginapproval_gid from hrm_trn_tloginapproval " +
                                 " where approval_flag = 'N' and approved_by = '" + lsemployee_gid + "' group by  loginapproval_gid))";

                        //----parallel flow ends here---------------------------------//
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        msSQL += " and k.attendancelogintmp_gid in (select loginapproval_gid from (select approved_by, loginapproval_gid from hrm_trn_tloginapproval " +
                                 " where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "' group by  loginapproval_gid order by loginapproval_gid asc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = " select distinct loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'Y'";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Close();
                            msPRSQL = " select distinct loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'Y' " +
                                      " and approved_by in ('" + lblEmployeeGID + "')";
                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objODBCDatareader["loginapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');

                            }
                            else
                            {
                                lsPR_lists = "";
                            }
                            objODBCDatareader.Close();
                            if (lsPR_lists != "")
                            {
                                msPRSQL = " select distinct a.attendancelogintmp_gid from hrm_tmp_tattendancelogin a " +
                             " left join hrm_trn_tloginapproval b on a.attendancelogintmp_gid = b.loginapproval_gid " +
                             " where a.status = 'Approved' and a.attendancelogintmp_gid in (" + lsPR_lists + ")" +
                             " and b.approval_flag = 'N' and b.approved_by = '" + lsemployee_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                                lsPR_lists = "";
                                if (objODBCDatareader.HasRows == true)
                                {
                                    while (objODBCDatareader.Read())
                                    {
                                        lsPR_lists = lsPR_lists + "'" + objODBCDatareader["attendancelogintmp_gid"].ToString() + "',";
                                    }
                                    if (lsPR_lists != "")
                                    {
                                        lsPR_lists = lsPR_lists.TrimEnd(',');
                                        msSQL += " or k.attendancelogintmp_gid in (" + lsPR_lists + ")";
                                    }

                                }
                                objODBCDatareader.Close();
                            }
                        }
                    }
                }
                msSAP = msSQL + " and k.employee_gid <>'" + employee_gid + "' and k.status = 'Approved' group by k.attendancelogintmp_gid " +
                                " order by date(k.created_date)desc,k.created_date asc,attendancelogintmp_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSAP);
                var getloginpending = new List<loginpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginpending.Add(new loginpending_list
                        {
                            attendancelogintmp_gid = (dr_datarow["attendancelogintmp_gid"].ToString()),
                            loginapply_date = (dr_datarow["applydate"].ToString()),
                            employee_name = (dr_datarow["employeename"].ToString()),
                            loginattendence_date = (dr_datarow["attendence_date"].ToString()),
                            login_time = (dr_datarow["login_time"].ToString()),
                            login_reason = (dr_datarow["remarks"].ToString()),
                            login_status = (dr_datarow["status"].ToString()),
                            apply_employeegid = (dr_datarow["employee_gid"].ToString()),
                        });
                    }
                    values.loginpending_list = getloginpending;
                }
                dt_datatable.Dispose();

                msSRE = msSQL + " and k.employee_gid <>'" + lsemployee_gid + "' and k.status = 'Rejected' group by k.attendancelogintmp_gid " +
                    " order by date(k.created_date)desc,k.created_date asc,attendancelogintmp_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSRE);
                var getloginrejected = new List<loginrejected_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginrejected.Add(new loginrejected_list
                        {
                            attendancelogintmp_gid = (dr_datarow["attendancelogintmp_gid"].ToString()),
                            loginapply_date = (dr_datarow["applydate"].ToString()),
                            employee_name = (dr_datarow["employeename"].ToString()),
                            loginattendence_date = (dr_datarow["attendence_date"].ToString()),
                            login_time = (dr_datarow["login_time"].ToString()),
                            login_reason = (dr_datarow["remarks"].ToString()),
                            login_status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.loginrejected_list = getloginrejected;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        //..............................* 3. LOGOUT APPROVED  Details*.............................//

        public bool DaGetLogoutApproved(string user_gid, getlogoutdetails values)
        {
            msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
            lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " Select a.hierarchy_level " +
                  " from adm_mst_tsubmodule a " +
                  " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " SELECT distinct k.attendancetmp_gid,k.employee_gid,k.status, date_format(k.created_date,'%d-%m-%Y') as created_date, " +
                    " concat(n.user_code, '/', n.user_firstname, ' ', n.user_lastname, '/', m.department_name) as employeename, " +
                    " date_format(k.attendance_date, '%d-%m-%Y') as attendence_date, time_format(k.logout_time, '%H:%i %p') as logout_time,k.remarks " +
                 " FROM hrm_tmp_tattendance k " +
                 " left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                     " left join hrm_mst_tdepartment m on m.department_gid=c.department_gid" +
                 " left join adm_mst_tdesignation e on c.designation_gid = e.designation_gid " +
                 " left join adm_mst_tuser n on n.user_gid = c.user_gid   " +
                 " where  1 = 1 ";
            if (lblUserCode != "-1")
            {
                msSQL += " and (k.employee_gid in ('" + lsemployee_gid + "') ";
            }
            msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNALT'";
            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
            if (objODBCDatareader.HasRows == true)
            {
                //-------------------parallel flow starts here------------------------------//
                if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                {
                    msSQL += " or k.attendancetmp_gid in (select logoutapproval_gid from hrm_trn_tlogoutapproval " +
                             " where approval_flag = 'N' and approved_by = '" + lsemployee_gid + "' group by  logoutapproval_gid))";
                    //-------------------parallel flow END here------------------------------//
                }
                else
                {
                    msSQL += " and k.attendancetmp_gid in (select logoutapproval_gid from (select approved_by, " +
                            " logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "'" +
                            " group by  logoutapproval_gid order by logoutapproval_gid asc) p where approved_by = '" + lsemployee_gid + "'))";

                    msPRSQL = " select distinct logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'Y'" +
                              " and approved_by in ('" + lblEmployeeGID + "')";
                    objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        while (objODBCDatareader.Read())
                        {
                            lsPR_lists = lsPR_lists + "'" + objODBCDatareader["logoutapproval_gid"].ToString() + "',";
                        }
                        lsPR_lists = lsPR_lists.TrimEnd(',');

                    }
                    else
                    {
                        lsPR_lists = "";
                    }
                    objODBCDatareader.Close();
                    if (lsPR_lists != "")
                    {
                        msPRSQL = " select distinct a.attendancetmp_gid from hrm_tmp_tattendance a " +
                         " left join hrm_trn_tlogoutapproval b on a.attendancetmp_gid = b.logoutapproval_gid " +
                         " where a.status = 'Approved' and a.attendancetmp_gid in (" + lsPR_lists + ")" +
                         " and b.approval_flag = 'N' and b.approved_by = '" + lsemployee_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        lsPR_lists = "";
                        if (objODBCDatareader.HasRows == true)
                        {
                            while (objODBCDatareader.Read())
                            {
                                lsPR_lists = lsPR_lists + "'" + objODBCDatareader["attendancetmp_gid"].ToString() + "',";
                            }
                            if (lsPR_lists != "")
                            {
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                                msSQL += " or k.attendancetmp_gid in (" + lsPR_lists + ")";
                            }
                            objODBCDatareader.Close();
                        }
                    }
                }
            }
            objODBCDatareader.Close();
            msSAP = msSQL + " and k.employee_gid <>'" + lblEmployeeGID + "' and k.status = 'Approved' group by k.attendancetmp_gid " +
                            " order by date(k.created_date)desc,k.created_date asc,k.attendancetmp_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSAP);
            var getlogoutpending = new List<logoutpending_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlogoutpending.Add(new logoutpending_list
                    {
                        attendancelogouttmp_gid = (dr_datarow["attendancetmp_gid"].ToString()),
                        logoutapply_date = (dr_datarow["created_date"].ToString()),
                        employee_name = (dr_datarow["employeename"].ToString()),
                        logoutattendence_date = (dr_datarow["attendence_date"].ToString()),
                        logout_time = (dr_datarow["logout_time"].ToString()),
                        logout_reason = (dr_datarow["remarks"].ToString()),
                        logout_status = (dr_datarow["status"].ToString()),
                        apply_employeegid = (dr_datarow["employee_gid"].ToString()),
                    });
                }
                values.logoutpending_list = getlogoutpending;
            }
            dt_datatable.Dispose();

            msSRE = msSQL + " and k.employee_gid <>'" + lsemployee_gid + "' and k.status = 'Rejected' group by k.attendancetmp_gid " +
                           " order by date(k.created_date)desc,k.created_date asc,attendancetmp_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSRE);
            var getlogoutrejected = new List<logoutrejected_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlogoutrejected.Add(new logoutrejected_list
                    {
                        attendancelogouttmp_gid = (dr_datarow["attendancetmp_gid"].ToString()),
                        logoutapply_date = (dr_datarow["created_date"].ToString()),
                        employee_name = (dr_datarow["employeename"].ToString()),
                        logoutattendence_date = (dr_datarow["attendence_date"].ToString()),
                        logout_time = (dr_datarow["logout_time"].ToString()),
                        logout_reason = (dr_datarow["remarks"].ToString()),
                        logout_status = (dr_datarow["status"].ToString()),
                    });
                }
                values.logoutrejected_list = getlogoutrejected;
            }
            dt_datatable.Dispose();


            return true;
        }
        //..............................* 4. OD APPROVED Details *..................................//

        public bool DaGetODApprovedSummaryDetails(string user_gid, getODdetails values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " Select a.hierarchy_level " +
                     " from adm_mst_tsubmodule a " +
                     " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
                }
                objODBCDatareader.Close();

                lsPR_lists = "";
                msSQL = " select distinct l.employee_gid,k.ondutytracker_gid,k.ondutytracker_status,date_format(k.ondutytracker_date,'%d-%m-%Y') as ondutydate, " +
                        " concat(k.onduty_fromtime,':',k.from_minutes) as onduty_fromtime,concat(k.onduty_totime,':',k.to_minutes) as onduty_totime,k.onduty_duration,date_format(k.created_date, '%d-%m-%Y') as createddate,k.onduty_reason, " +
                        " concat(n.user_code, '/', n.user_firstname, ' ', n.user_lastname, '/', m.department_name) as employee_name " +
                        "  from hrm_trn_tondutytracker k" +
                        " left join hrm_mst_temployee l on l.employee_gid=k.employee_gid" +
                        " left join hrm_mst_tdepartment m on m.department_gid=l.department_gid" +
                        " left join adm_mst_tuser n on n.user_gid=l.user_gid" +
                        " left join hrm_trn_tattendance t on t.employee_gid=k.employee_gid " +
                        " where k.ondutytracker_status='Approved' ";
                msSQL += " and (l.employee_gid in ('" + lsemployee_gid + "')) ";
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    string lsapprovaltype = (objODBCDatareader["approval_type"].ToString());
                    objODBCDatareader.Close();
                    if (lsapprovaltype == "Parallel")
                    {
                        msSQL += " or (k.ondutytracker_gid in (select ondutyapproval_gid from hrm_trn_tapproval where " +
                                 " approval_flag = 'N' and approved_by = '" + lsemployee_gid + "' group by  ondutyapproval_gid) " +
                                 " and k.ondutytracker_status ='Approved') ";
                    }
                    else
                    {
                        msSQL += " and k.ondutytracker_gid in (select ondutyapproval_gid from (select approved_by, " +
                                 " ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "'" +
                                 " group by  ondutyapproval_gid order by ondutyapproval_gid desc) p where " +
                                 " approved_by = '" + lsemployee_gid + "')";

                        msPRSQL = " select distinct ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N'";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Close();
                            msPRSQL = " select distinct ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N'" +
                                      " and approved_by = '" + lsemployee_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows == true)
                            {

                                string lsondutyapproval_gid = objODBCDatareader["ondutyapproval_gid"].ToString();
                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + lsondutyapproval_gid + "',";
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                                objODBCDatareader.Close();

                            }
                            objODBCDatareader.Close();
                            if (lsPR_lists != "")
                            {
                                msSQL += " or k.ondutytracker_gid in (" + lsPR_lists + ")";
                            }
                        }
                        objODBCDatareader.Close();
                    }
                }
                objODBCDatareader.Close();

                msSQL = msSQL + "  order by date(k.created_date) desc,k.created_date asc,k.ondutytracker_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getODpending = new List<ODpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getODpending.Add(new ODpending_list
                        {
                            ondutytracker_gid = (dr_datarow["ondutytracker_gid"].ToString()),
                            created_date = (dr_datarow["createddate"].ToString()),
                            onduty_date = (dr_datarow["ondutydate"].ToString()),
                            onduty_from = (dr_datarow["onduty_fromtime"].ToString()),
                            onduty_to = (dr_datarow["onduty_totime"].ToString()),
                            onduty_duration = (dr_datarow["onduty_duration"].ToString()),
                            onduty_reason = (dr_datarow["onduty_reason"].ToString()),
                            ondutytracker_status = (dr_datarow["ondutytracker_status"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            apply_employeegid = (dr_datarow["employee_gid"].ToString()),
                        });
                    }
                    values.ODpending_list = getODpending;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        //..............................* 6. COMPOFF APPROVED Details *..............................//

        public bool DaGetCompoffapprovedSummaryDetails(string user_gid, getcompoffdetails values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " Select a.hierarchy_level " +
                     " from adm_mst_tsubmodule a " +
                     " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
                }
                objODBCDatareader.Close();

                lsPR_lists = "";
                msSQL = " select distinct o.compensatoryoff_gid,o.compensatoryoff_status,o.compensatoryoff_reason,date_format(o.actualworking_todate, '%d-%m-%Y') " +
                        " as Compoff_to  ,date_format(o.actualworking_fromdate, '%d-%m-%Y') as actual_working,o.compoff_noofdays, " +
                        " date_format(o.compensatoryoff_applydate, '%d-%m-%Y') as compensatoryoff_applydate, " +
                        " concat(r.user_code, '/', r.user_firstname, ' ', r.user_lastname, '/', q.department_name) as employee_name " +
                         " from hrm_trn_tcompensatoryoff o " +
                         " left join hrm_mst_temployee p on p.employee_gid=o.employee_gid" +
                         " left join hrm_mst_tdepartment q on q.department_gid=p.department_gid" +
                         " left join adm_mst_tuser r on r.user_gid=p.user_gid" +
                         " where o.compensatoryoff_status='Approved'";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (p.employee_gid in ('" + lsemployee_gid + "') and o.employee_gid<>'" + lblEmployeeGID + "' ) ";
                }
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or (o.compensatoryoff_gid in (select compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + lsemployee_gid + "' group by  compensatoryoff_gid) and o.compensatoryoff_status ='Approved') ";
                    }
                    else
                    {
                        msSQL += " and o.compensatoryoff_gid in (select compensatoryoff_gid from (select approved_by, " +
                                 " compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "'" +
                                 " group by  compensatoryoff_gid order by compensatoryoff_gid desc) p where approved_by = '" + lsemployee_gid + "')";
                        msPRSQL = " select distinct compensatoryoff_gid from hrm_trn_tapproval ";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            msPRSQL = " select distinct compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' " +
                                      " and approved_by = '" + lsemployee_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                string lscompensatoryoff_gid = objODBCDatareader["compensatoryoff_gid"].ToString();

                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + lscompensatoryoff_gid + "',";

                                }
                                objODBCDatareader.Close();
                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                            }
                            objODBCDatareader.Close();
                            if (lsPR_lists != "")
                            {
                                msSQL += " or o.compensatoryoff_gid in (" + lsPR_lists + ")";
                            }
                        }
                    }
                }
                msSQL = msSQL + " group by o.compensatoryoff_gid order by" +
                            "  date(o.created_date) desc,o.created_date asc,o.compensatoryoff_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcompoffpending = new List<compoffpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcompoffpending.Add(new compoffpending_list
                        {
                            compensatoryoff_gid = (dr_datarow["compensatoryoff_gid"].ToString()),
                            Compoff_from = (dr_datarow["compensatoryoff_applydate"].ToString()),
                            Compoff_to = (dr_datarow["actual_working"].ToString()),
                            Compoff_duration = (dr_datarow["compoff_noofdays"].ToString()),
                            Compoff_reason = (dr_datarow["compensatoryoff_reason"].ToString()),
                            Compoff_status = (dr_datarow["compensatoryoff_status"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.compoffpending_list = getcompoffpending;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        //..............................* 5. PERMISSION APPROVED  Details *..........................//

        public bool GetPermissionapprovedSummaryDetails(string user_gid, getpermissiondetails values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select system_type from hrm_mst_thrconfig " +
                       " where actualcompany_code='" + ConfigurationManager.ConnectionStrings["company_code"] + "'";
                lssystem_type = objdbconn.GetExecuteScalar(msSQL);

                lsPR_lists = "";
                msSQL = " select distinct a.permission_gid,a.permissiondtl_gid,a.permission_reason,a.permission_status,concat(a.permission_totalhours, ':', a.total_mins, ':', '00') " +
                        " as permission_total ,concat(a.permission_fromhours, ':', a.permission_frommins) as permission_from," +
                        " concat(a.permission_tohours, ':', a.permission_tomins) as permission_to ,date_format(a.permission_date, '%d-%m-%Y') as permission_date, " +
                        " date_format(a.permission_applydate, '%d-%m-%Y') as createddate, " +
                        " concat(d.user_code, '/', d.user_firstname, ' ', d.user_lastname, '/', e.department_name) as employee_name " +
                        " from hrm_trn_tpermissiondtl a left join hrm_mst_temployee i on a.employee_gid=i.employee_gid" +
                        " left join hrm_mst_tdepartment e on e.department_gid=i.department_gid " +
                        " left join adm_mst_Tuser d on d.user_gid=i.user_gid" +
                        " left join hrm_trn_temployeetypedtl j on i.employee_gid=j.employee_gid " +
                        " where a.permission_status='Approved'";
                if (lssystem_type == "AUDIT")
                {
                    msSQL += " and j.employeetype_name='Roll' ";
                }
                msSQL += " and (i.employee_gid in ('" + lsemployee_gid + "')) ";
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or (a.permissiondtl_gid in (select permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + lblEmployeeGID + "' group by  permissionapproval_gid) and a.permission_status ='Approved') ";
                    }
                    else
                    {
                        msSQL += " and a.permissiondtl_gid in (select permissionapproval_gid from (select approved_by, " +
                                 " permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "'" +
                                 " group by  permissionapproval_gid order by permissionapproval_gid desc) p where approved_by = '" + lsemployee_gid + "')";

                        msPRSQL = " select distinct permissionapproval_gid from hrm_trn_tapproval ";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            msPRSQL = " select distinct permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + lsemployee_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                string lspermissionapproval_gid = objODBCDatareader["permissionapproval_gid"].ToString();

                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + lspermissionapproval_gid + "',";
                                }

                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                            }
                            objODBCDatareader.Close();
                            if (lsPR_lists != "")
                            {
                                msSQL += " or a.permissiondtl_gid in (" + lsPR_lists + ")";
                            }

                        }

                    }
                }
                msSQL = msSQL + " and a.permission_status='Approved' order by date(a.created_date) desc,a.created_date asc,a.permissiondtl_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpermissionpending = new List<permissionpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getpermissionpending.Add(new permissionpending_list
                        {
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            permission_gid = (dr_datarow["permission_gid"].ToString()),
                            permissiondtl_gid = (dr_datarow["permissiondtl_gid"].ToString()),
                            permission_date = (dr_datarow["permission_date"].ToString()),
                            permission_from = (dr_datarow["permission_from"].ToString()),
                            permission_to = (dr_datarow["permission_to"].ToString()),
                            permission_duration = (dr_datarow["permission_total"].ToString()),
                            permission_reason = (dr_datarow["permission_reason"].ToString()),
                            permission_status = (dr_datarow["permission_status"].ToString()),
                            permission_createddate = (dr_datarow["createddate"].ToString()),
                        });
                    }
                    values.permissionpending_list = getpermissionpending;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }




        // Rejected Summary...//
        public bool DaGetLeaverejected(string user_gid, getleavedetails values)
        {
            try
            {

                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " Select a.hierarchy_level " +
                      " from adm_mst_tsubmodule a " +
                      " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
                }
                objODBCDatareader.Close();

                // Leave Approved Summary...//


                msSQL = " select a.leave_gid,a.document_name,date_format(a.leave_fromdate,'%d-%m-%Y') as leave_fromdate,date_format(a.leave_todate,'%d-%m-%Y') as leave_todate,a.leave_noofdays,b.leavetype_name, " +
                     " a.leave_reason,a.leave_status,concat(d.user_firstname,' ',d.user_lastname) as leave_appliedby " +
                     " from hrm_trn_tleave a " +
                    " left join hrm_mst_tleavetype b on a.leavetype_gid=b.leavetype_gid" +
                  " left join hrm_mst_temployee c on c.employee_gid=a.employee_gid " +
                  " left join adm_mst_tuser d on d.user_gid=c.user_gid" +
                  " left join hrm_mst_tdepartment e on e.department_gid=c.department_gid" +
                  " left join hrm_trn_tattendance s on s.employee_gid=a.employee_gid " +
                  " left join hrm_trn_tleavedtl w on a.leave_gid = w.leave_gid" +
                  " where 1=1 and a.leave_status = 'Rejected'";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (c.employee_gid in ('" + lsemployee_gid + "')";
                }

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";

                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or (w.leavedtl_gid in (select y.leavedtl_gid from hrm_trn_tapproval x left join hrm_trn_tleavedtl y on x.leave_gid=y.leave_gid " +
                        " where  x.approval_flag = 'N' and x.approved_by = '" + lsemployee_gid + "' )))";
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        msSQL += " and a.leave_gid in (select leaveapproval_gid from (select approved_by, " +
                             " leaveapproval_gid from hrm_trn_tapproval " +
                             " where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "' group by  leaveapproval_gid order by" +
                             " approval_gid asc) p where approved_by = '" + lsemployee_gid + "'))";
                        msPRSQL = " select distinct leaveapproval_gid from hrm_trn_tapproval where approval_flag = 'Y'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows)
                        {
                            objODBCDatareader.Close();
                            msPRSQL = " select distinct leaveapproval_gid from hrm_trn_tapproval where approval_flag = 'Y' " +
                                  " and approved_by in (" + lblEmployeeGID + ")";

                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows)
                            {
                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objODBCDatareader["leaveapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                            }
                            objODBCDatareader.Close();

                            if (lsPR_lists != "")
                            {
                                msPRSQL = " select distinct a.leave_gid from hrm_trn_tleave a " +
                             " left join hrm_trn_tapproval b on a.leave_gid = b.leaveapproval_gid " +
                             " where a.leave_status = 'Rejected' and a.leave_gid in (" + lsPR_lists + ")" +
                             " and b.approval_flag = 'N' and b.approved_by = '" + lsemployee_gid + "'";
                                lsPR_lists = "";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows)
                                {
                                    while (objODBCDatareader.Read())
                                    {
                                        lsPR_lists = lsPR_lists + "'" + objODBCDatareader["leave_gid"].ToString() + "',";
                                    }
                                    if (lsPR_lists != "")
                                    {
                                        lsPR_lists = lsPR_lists.TrimEnd(',');
                                        msPRSQL += " or a.leave_gid in (" + lsPR_lists + ")";
                                    }
                                }
                                objODBCDatareader.Close();
                            }
                            objODBCDatareader.Close();
                        }
                        objODBCDatareader.Close();
                    }
                }
                objODBCDatareader.Close();

                msSQL = msSQL + " and a.employee_gid<>'" + lblEmployeeGID + "' group by a.leave_gid order by date(a.created_date) desc,a.created_date asc,a.leave_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleave = new List<leave_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (dr_datarow["document_name"].ToString() != "")
                        {
                            lsdocument_flag = "Y";
                        }
                        else
                        {
                            lsdocument_flag = "N";
                        }
                        getleave.Add(new leave_list
                        {
                            leave_gid = (dr_datarow["leave_gid"].ToString()),
                            leavetype_name = (dr_datarow["leavetype_name"].ToString()),
                            leave_from = (dr_datarow["leave_fromdate"].ToString()),
                            leave_to = (dr_datarow["leave_todate"].ToString()),
                            noofdays_leave = (dr_datarow["leave_noofdays"].ToString()),
                            leave_reason = (dr_datarow["leave_reason"].ToString()),
                            approval_status = (dr_datarow["leave_status"].ToString()),
                            applied_by = (dr_datarow["leave_appliedby"].ToString()),
                            document_name = lsdocument_flag
                        });
                    }
                    values.leave_list = getleave;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                return false;
            }
        }
        public bool DaGetLoginrejected(string user_gid, getlogindetails values)
        {
            try
            {
                //Login APPROVED Pending Summary.....//
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);




                msSQL = " Select a.hierarchy_level " +
                      " from adm_mst_tsubmodule a " +
                      " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " SELECT distinct k.attendancelogintmp_gid,k.employee_gid,k.status,date_format(k.attendance_date, '%d-%m-%Y') as attendence_date, " +
                        " time_format(k.login_time, '%H:%i %p') as login_time, " +
                        " date_format(k.created_date, '%d-%m-%Y') as applydate , k.remarks, concat(n.user_code, '/', n.user_firstname, ' ', n.user_lastname, " +
                        "  '/', m.department_name) as employeename FROM hrm_tmp_tattendancelogin k " +
                        " left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                        " left join hrm_mst_tdepartment m on m.department_gid=c.department_gid" +
                        " left join adm_mst_tdesignation e on c.designation_gid = e.designation_gid " +
                        " left join adm_mst_tuser n on n.user_gid = c.user_gid   " +
                        " where 1 = 1 ";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (k.employee_gid in ('" + lsemployee_gid + "')";
                }

                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNLTA'";
                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    //-------------------parallel flow starts here------------------------------//
                    if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or k.attendancelogintmp_gid in (select loginapproval_gid from hrm_trn_tloginapproval " +
                                 " where approval_flag = 'N' and approved_by = '" + lsemployee_gid + "' group by  loginapproval_gid))";

                        //----parallel flow ends here---------------------------------//
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        msSQL += " and k.attendancelogintmp_gid in (select loginapproval_gid from (select approved_by, loginapproval_gid from hrm_trn_tloginapproval " +
                                 " where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "' group by  loginapproval_gid order by loginapproval_gid asc) p where approved_by = '" + employee_gid + "'))";

                        msPRSQL = " select distinct loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'Y'";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Close();
                            msPRSQL = " select distinct loginapproval_gid from hrm_trn_tloginapproval where approval_flag = 'Y' " +
                                      " and approved_by in ('" + lblEmployeeGID + "')";
                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + objODBCDatareader["loginapproval_gid"].ToString() + "',";
                                }
                                lsPR_lists = lsPR_lists.TrimEnd(',');

                            }
                            else
                            {
                                lsPR_lists = "";
                            }
                            objODBCDatareader.Close();
                            if (lsPR_lists != "")
                            {
                                msPRSQL = " select distinct a.attendancelogintmp_gid from hrm_tmp_tattendancelogin a " +
                             " left join hrm_trn_tloginapproval b on a.attendancelogintmp_gid = b.loginapproval_gid " +
                             " where a.status = 'Rejected' and a.attendancelogintmp_gid in (" + lsPR_lists + ")" +
                             " and b.approval_flag = 'N' and b.approved_by = '" + lsemployee_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                                lsPR_lists = "";
                                if (objODBCDatareader.HasRows == true)
                                {
                                    while (objODBCDatareader.Read())
                                    {
                                        lsPR_lists = lsPR_lists + "'" + objODBCDatareader["attendancelogintmp_gid"].ToString() + "',";
                                    }
                                    if (lsPR_lists != "")
                                    {
                                        lsPR_lists = lsPR_lists.TrimEnd(',');
                                        msSQL += " or k.attendancelogintmp_gid in (" + lsPR_lists + ")";
                                    }

                                }
                                objODBCDatareader.Close();
                            }
                        }
                    }
                }
                msSAP = msSQL + " and k.employee_gid <>'" + employee_gid + "' and k.status = 'Rejected' group by k.attendancelogintmp_gid " +
                                " order by date(k.created_date)desc,k.created_date asc,attendancelogintmp_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSAP);
                var getloginpending = new List<loginpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginpending.Add(new loginpending_list
                        {
                            attendancelogintmp_gid = (dr_datarow["attendancelogintmp_gid"].ToString()),
                            loginapply_date = (dr_datarow["applydate"].ToString()),
                            employee_name = (dr_datarow["employeename"].ToString()),
                            loginattendence_date = (dr_datarow["attendence_date"].ToString()),
                            login_time = (dr_datarow["login_time"].ToString()),
                            login_reason = (dr_datarow["remarks"].ToString()),
                            login_status = (dr_datarow["status"].ToString()),
                            apply_employeegid = (dr_datarow["employee_gid"].ToString()),
                        });
                    }
                    values.loginpending_list = getloginpending;
                }
                dt_datatable.Dispose();

                msSRE = msSQL + " and k.employee_gid <>'" + lsemployee_gid + "' and k.status = 'Rejected' group by k.attendancelogintmp_gid " +
                    " order by date(k.created_date)desc,k.created_date asc,attendancelogintmp_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSRE);
                var getloginrejected = new List<loginrejected_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloginrejected.Add(new loginrejected_list
                        {
                            attendancelogintmp_gid = (dr_datarow["attendancelogintmp_gid"].ToString()),
                            loginapply_date = (dr_datarow["applydate"].ToString()),
                            employee_name = (dr_datarow["employeename"].ToString()),
                            loginattendence_date = (dr_datarow["attendence_date"].ToString()),
                            login_time = (dr_datarow["login_time"].ToString()),
                            login_reason = (dr_datarow["remarks"].ToString()),
                            login_status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.loginrejected_list = getloginrejected;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        //..............................* 3. LOGOUT Rejected  Details*.............................//

        public bool DaGetLogoutrejected(string user_gid, getlogoutdetails values)
        {
            msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
            lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " Select a.hierarchy_level " +
                  " from adm_mst_tsubmodule a " +
                  " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
            }
            objODBCDatareader.Close();


            msSQL = " SELECT distinct k.attendancetmp_gid,k.employee_gid,k.status, date_format(k.created_date,'%d-%m-%Y') as created_date, " +
                    " concat(n.user_code, '/', n.user_firstname, ' ', n.user_lastname, '/', m.department_name) as employeename, " +
                    " date_format(k.attendance_date, '%d-%m-%Y') as attendence_date, time_format(k.logout_time, '%H:%i %p') as logout_time,k.remarks " +
                 " FROM hrm_tmp_tattendance k " +
                 " left join hrm_mst_temployee c on k.employee_gid = c.employee_gid " +
                     " left join hrm_mst_tdepartment m on m.department_gid=c.department_gid" +
                 " left join adm_mst_tdesignation e on c.designation_gid = e.designation_gid " +
                 " left join adm_mst_tuser n on n.user_gid = c.user_gid   " +
                 " where  1 = 1 ";
            if (lblUserCode != "-1")
            {
                msSQL += " and (k.employee_gid in ('" + lsemployee_gid + "') ";
            }
            msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMATNALT'";
            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
            if (objODBCDatareader.HasRows == true)
            {
                //-------------------parallel flow starts here------------------------------//
                if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                {
                    msSQL += " or k.attendancetmp_gid in (select logoutapproval_gid from hrm_trn_tlogoutapproval " +
                             " where approval_flag = 'N' and approved_by = '" + lsemployee_gid + "' group by  logoutapproval_gid))";
                    //-------------------parallel flow END here------------------------------//
                }
                else
                {
                    msSQL += " and k.attendancetmp_gid in (select logoutapproval_gid from (select approved_by, " +
                            " logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "'" +
                            " group by  logoutapproval_gid order by logoutapproval_gid asc) p where approved_by = '" + lsemployee_gid + "'))";

                    msPRSQL = " select distinct logoutapproval_gid from hrm_trn_tlogoutapproval where approval_flag = 'Y'" +
                              " and approved_by in ('" + lblEmployeeGID + "')";
                    objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        while (objODBCDatareader.Read())
                        {
                            lsPR_lists = lsPR_lists + "'" + objODBCDatareader["logoutapproval_gid"].ToString() + "',";
                        }
                        lsPR_lists = lsPR_lists.TrimEnd(',');

                    }
                    else
                    {
                        lsPR_lists = "";
                    }
                    objODBCDatareader.Close();
                    if (lsPR_lists != "")
                    {
                        msPRSQL = " select distinct a.attendancetmp_gid from hrm_tmp_tattendance a " +
                         " left join hrm_trn_tlogoutapproval b on a.attendancetmp_gid = b.logoutapproval_gid " +
                         " where a.status = 'Rejected' and a.attendancetmp_gid in (" + lsPR_lists + ")" +
                         " and b.approval_flag = 'N' and b.approved_by = '" + lsemployee_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        lsPR_lists = "";
                        if (objODBCDatareader.HasRows == true)
                        {
                            while (objODBCDatareader.Read())
                            {
                                lsPR_lists = lsPR_lists + "'" + objODBCDatareader["attendancetmp_gid"].ToString() + "',";
                            }
                            if (lsPR_lists != "")
                            {
                                lsPR_lists = lsPR_lists.TrimEnd(',');
                                msSQL += " or k.attendancetmp_gid in (" + lsPR_lists + ")";
                            }
                            objODBCDatareader.Close();
                        }
                    }
                }
            }
            objODBCDatareader.Close();
            msSAP = msSQL + " and k.employee_gid <>'" + lblEmployeeGID + "' and k.status = 'Rejected' group by k.attendancetmp_gid " +
                            " order by date(k.created_date)desc,k.created_date asc,k.attendancetmp_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSAP);
            var getlogoutpending = new List<logoutpending_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlogoutpending.Add(new logoutpending_list
                    {
                        attendancelogouttmp_gid = (dr_datarow["attendancetmp_gid"].ToString()),
                        logoutapply_date = (dr_datarow["created_date"].ToString()),
                        employee_name = (dr_datarow["employeename"].ToString()),
                        logoutattendence_date = (dr_datarow["attendence_date"].ToString()),
                        logout_time = (dr_datarow["logout_time"].ToString()),
                        logout_reason = (dr_datarow["remarks"].ToString()),
                        logout_status = (dr_datarow["status"].ToString()),
                        apply_employeegid = (dr_datarow["employee_gid"].ToString()),
                    });
                }
                values.logoutpending_list = getlogoutpending;
            }
            dt_datatable.Dispose();

            msSRE = msSQL + " and k.employee_gid <>'" + lsemployee_gid + "' and k.status = 'Rejected' group by k.attendancetmp_gid " +
                           " order by date(k.created_date)desc,k.created_date asc,attendancetmp_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSRE);
            var getlogoutrejected = new List<logoutrejected_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlogoutrejected.Add(new logoutrejected_list
                    {
                        attendancelogouttmp_gid = (dr_datarow["attendancetmp_gid"].ToString()),
                        logoutapply_date = (dr_datarow["created_date"].ToString()),
                        employee_name = (dr_datarow["employeename"].ToString()),
                        logoutattendence_date = (dr_datarow["attendence_date"].ToString()),
                        logout_time = (dr_datarow["logout_time"].ToString()),
                        logout_reason = (dr_datarow["remarks"].ToString()),
                        logout_status = (dr_datarow["status"].ToString()),
                    });
                }
                values.logoutrejected_list = getlogoutrejected;
            }
            dt_datatable.Dispose();


            return true;
        }
        //..............................* 4. OD Rejected Details *..................................//

        public bool DaGetODrejectedSummaryDetails(string user_gid, getODdetails values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " Select a.hierarchy_level " +
                     " from adm_mst_tsubmodule a " +
                     " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
                }
                objODBCDatareader.Close();

                lsPR_lists = "";
                msSQL = " select distinct l.employee_gid,k.ondutytracker_gid,k.ondutytracker_status,date_format(k.ondutytracker_date,'%d-%m-%Y') as ondutydate, " +
                        " concat(k.onduty_fromtime,':',k.from_minutes) as onduty_fromtime,concat(k.onduty_totime,':',k.to_minutes) as onduty_totime,k.onduty_duration,date_format(k.created_date, '%d-%m-%Y') as createddate,k.onduty_reason, " +
                        " concat(n.user_code, '/', n.user_firstname, ' ', n.user_lastname, '/', m.department_name) as employee_name " +
                        "  from hrm_trn_tondutytracker k" +
                        " left join hrm_mst_temployee l on l.employee_gid=k.employee_gid" +
                        " left join hrm_mst_tdepartment m on m.department_gid=l.department_gid" +
                        " left join adm_mst_tuser n on n.user_gid=l.user_gid" +
                        " left join hrm_trn_tattendance t on t.employee_gid=k.employee_gid " +
                        " where k.ondutytracker_status='Rejected' ";
                msSQL += " and (l.employee_gid in ('" + lsemployee_gid + "')) ";
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    string lsapprovaltype = (objODBCDatareader["approval_type"].ToString());
                    objODBCDatareader.Close();
                    if (lsapprovaltype == "Parallel")
                    {
                        msSQL += " or (k.ondutytracker_gid in (select ondutyapproval_gid from hrm_trn_tapproval where " +
                                 " approval_flag = 'N' and approved_by = '" + lsemployee_gid + "' group by  ondutyapproval_gid) " +
                                 " and k.ondutytracker_status ='Rejected') ";
                    }
                    else
                    {
                        msSQL += " and k.ondutytracker_gid in (select ondutyapproval_gid from (select approved_by, " +
                                 " ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "'" +
                                 " group by  ondutyapproval_gid order by ondutyapproval_gid desc) p where " +
                                 " approved_by = '" + lsemployee_gid + "')";

                        msPRSQL = " select distinct ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N'";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Close();
                            msPRSQL = " select distinct ondutyapproval_gid from hrm_trn_tapproval where approval_flag = 'N'" +
                                      " and approved_by = '" + lsemployee_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows == true)
                            {

                                string lsondutyapproval_gid = objODBCDatareader["ondutyapproval_gid"].ToString();
                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + lsondutyapproval_gid + "',";
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                                objODBCDatareader.Close();

                            }
                            objODBCDatareader.Close();
                            if (lsPR_lists != "")
                            {
                                msSQL += " or k.ondutytracker_gid in (" + lsPR_lists + ")";
                            }
                        }
                        objODBCDatareader.Close();
                    }
                }
                objODBCDatareader.Close();

                msSQL = msSQL + "  order by date(k.created_date) desc,k.created_date asc,k.ondutytracker_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getODpending = new List<ODpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getODpending.Add(new ODpending_list
                        {
                            ondutytracker_gid = (dr_datarow["ondutytracker_gid"].ToString()),
                            created_date = (dr_datarow["createddate"].ToString()),
                            onduty_date = (dr_datarow["ondutydate"].ToString()),
                            onduty_from = (dr_datarow["onduty_fromtime"].ToString()),
                            onduty_to = (dr_datarow["onduty_totime"].ToString()),
                            onduty_duration = (dr_datarow["onduty_duration"].ToString()),
                            onduty_reason = (dr_datarow["onduty_reason"].ToString()),
                            ondutytracker_status = (dr_datarow["ondutytracker_status"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            apply_employeegid = (dr_datarow["employee_gid"].ToString()),
                        });
                    }
                    values.ODpending_list = getODpending;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        //..............................* 6. COMPOFF Rejected Details *..............................//

        public bool DaGetCompoffrejectedSummaryDetails(string user_gid, getcompoffdetails values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " Select a.hierarchy_level " +
                     " from adm_mst_tsubmodule a " +
                     " where a.employee_gid = '" + lsemployee_gid + "' and a.module_gid = 'HRM' and a.submodule_id='HRM'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lblUserCode = objODBCDatareader["hierarchy_level"].ToString();
                }
                objODBCDatareader.Close();

                lsPR_lists = "";
                msSQL = " select distinct o.compensatoryoff_gid,o.compensatoryoff_status,o.compensatoryoff_reason,date_format(o.actualworking_todate, '%d-%m-%Y') " +
                        " as Compoff_to  ,date_format(o.actualworking_fromdate, '%d-%m-%Y') as actual_working,o.compoff_noofdays, " +
                        " date_format(o.compensatoryoff_applydate, '%d-%m-%Y') as compensatoryoff_applydate, " +
                        " concat(r.user_code, '/', r.user_firstname, ' ', r.user_lastname, '/', q.department_name) as employee_name " +
                         " from hrm_trn_tcompensatoryoff o " +
                         " left join hrm_mst_temployee p on p.employee_gid=o.employee_gid" +
                         " left join hrm_mst_tdepartment q on q.department_gid=p.department_gid" +
                         " left join adm_mst_tuser r on r.user_gid=p.user_gid" +
                         " where o.compensatoryoff_status='Rejected'";
                if (lblUserCode != "-1")
                {
                    msSQL += " and (p.employee_gid in ('" + lsemployee_gid + "') and o.employee_gid<>'" + lblEmployeeGID + "' ) ";
                }
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or (o.compensatoryoff_gid in (select compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + lsemployee_gid + "' group by  compensatoryoff_gid) and o.compensatoryoff_status ='Rejected') ";
                    }
                    else
                    {
                        msSQL += " and o.compensatoryoff_gid in (select compensatoryoff_gid from (select approved_by, " +
                                 " compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "'" +
                                 " group by  compensatoryoff_gid order by compensatoryoff_gid desc) p where approved_by = '" + lsemployee_gid + "')";
                        msPRSQL = " select distinct compensatoryoff_gid from hrm_trn_tapproval ";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            msPRSQL = " select distinct compensatoryoff_gid from hrm_trn_tapproval where approval_flag = 'N' " +
                                      " and approved_by = '" + lsemployee_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                string lscompensatoryoff_gid = objODBCDatareader["compensatoryoff_gid"].ToString();

                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + lscompensatoryoff_gid + "',";

                                }
                                objODBCDatareader.Close();
                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                            }
                            objODBCDatareader.Close();
                            if (lsPR_lists != "")
                            {
                                msSQL += " or o.compensatoryoff_gid in (" + lsPR_lists + ")";
                            }
                        }
                    }
                }
                msSQL = msSQL + " group by o.compensatoryoff_gid order by" +
                            "  date(o.created_date) desc,o.created_date asc,o.compensatoryoff_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcompoffpending = new List<compoffpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcompoffpending.Add(new compoffpending_list
                        {
                            compensatoryoff_gid = (dr_datarow["compensatoryoff_gid"].ToString()),
                            Compoff_from = (dr_datarow["compensatoryoff_applydate"].ToString()),
                            Compoff_to = (dr_datarow["actual_working"].ToString()),
                            Compoff_duration = (dr_datarow["compoff_noofdays"].ToString()),
                            Compoff_reason = (dr_datarow["compensatoryoff_reason"].ToString()),
                            Compoff_status = (dr_datarow["compensatoryoff_status"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.compoffpending_list = getcompoffpending;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        //..............................* 5. PERMISSION Rejected  Details *..........................//

        public bool GetPermissionrejectedSummaryDetails(string user_gid, getpermissiondetails values)
        {
            try
            {
                msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
                lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select a.employeereporting_to from adm_mst_tmodule2employee a left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid left join adm_mst_tuser c on b.user_gid = c.user_gid  where a.employee_gid = '" + lsemployee_gid + "' and module_gid = 'HRM'";
                lblEmployeeGID = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "select system_type from hrm_mst_thrconfig " +
                       " where actualcompany_code='" + ConfigurationManager.ConnectionStrings["company_code"] + "'";
                lssystem_type = objdbconn.GetExecuteScalar(msSQL);

                lsPR_lists = "";
                msSQL = " select distinct a.permission_gid,a.permissiondtl_gid,a.permission_reason,a.permission_status,concat(a.permission_totalhours, ':', a.total_mins, ':', '00') " +
                        " as permission_total ,concat(a.permission_fromhours, ':', a.permission_frommins) as permission_from," +
                        " concat(a.permission_tohours, ':', a.permission_tomins) as permission_to ,date_format(a.permission_date, '%d-%m-%Y') as permission_date, " +
                        " date_format(a.permission_applydate, '%d-%m-%Y') as createddate, " +
                        " concat(d.user_code, '/', d.user_firstname, ' ', d.user_lastname, '/', e.department_name) as employee_name " +
                        " from hrm_trn_tpermissiondtl a left join hrm_mst_temployee i on a.employee_gid=i.employee_gid" +
                        " left join hrm_mst_tdepartment e on e.department_gid=i.department_gid " +
                        " left join adm_mst_Tuser d on d.user_gid=i.user_gid" +
                        " left join hrm_trn_temployeetypedtl j on i.employee_gid=j.employee_gid " +
                        " where a.permission_status='Rejected'";
                if (lssystem_type == "AUDIT")
                {
                    msSQL += " and j.employeetype_name='Roll' ";
                }
                msSQL += " and (i.employee_gid in ('" + lsemployee_gid + "')) ";
                msPRSQL = " select approval_type from adm_mst_tmodule where module_gid = 'HRMLEVARL'";
                objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (objODBCDatareader["approval_type"].ToString() == "Parallel")
                    {
                        msSQL += " or (a.permissiondtl_gid in (select permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + lblEmployeeGID + "' group by  permissionapproval_gid) and a.permission_status ='Rejected') ";
                    }
                    else
                    {
                        msSQL += " and a.permissiondtl_gid in (select permissionapproval_gid from (select approved_by, " +
                                 " permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and seqhierarchy_view='Y' and approved_by = '" + lsemployee_gid + "'" +
                                 " group by  permissionapproval_gid order by permissionapproval_gid desc) p where approved_by = '" + lsemployee_gid + "')";

                        msPRSQL = " select distinct permissionapproval_gid from hrm_trn_tapproval ";
                        objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            msPRSQL = " select distinct permissionapproval_gid from hrm_trn_tapproval where approval_flag = 'N' and approved_by = '" + lsemployee_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msPRSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                string lspermissionapproval_gid = objODBCDatareader["permissionapproval_gid"].ToString();

                                while (objODBCDatareader.Read())
                                {
                                    lsPR_lists = lsPR_lists + "'" + lspermissionapproval_gid + "',";
                                }

                                if (lsPR_lists != "")
                                {
                                    lsPR_lists = lsPR_lists.TrimEnd(',');
                                }
                            }
                            objODBCDatareader.Close();
                            if (lsPR_lists != "")
                            {
                                msSQL += " or a.permissiondtl_gid in (" + lsPR_lists + ")";
                            }

                        }

                    }
                }
                msSQL = msSQL + " and a.permission_status='Rejected' order by date(a.created_date) desc,a.created_date asc,a.permissiondtl_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpermissionpending = new List<permissionpending_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getpermissionpending.Add(new permissionpending_list
                        {
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            permission_gid = (dr_datarow["permission_gid"].ToString()),
                            permissiondtl_gid = (dr_datarow["permissiondtl_gid"].ToString()),
                            permission_date = (dr_datarow["permission_date"].ToString()),
                            permission_from = (dr_datarow["permission_from"].ToString()),
                            permission_to = (dr_datarow["permission_to"].ToString()),
                            permission_duration = (dr_datarow["permission_total"].ToString()),
                            permission_reason = (dr_datarow["permission_reason"].ToString()),
                            permission_status = (dr_datarow["permission_status"].ToString()),
                            permission_createddate = (dr_datarow["createddate"].ToString()),
                        });
                    }
                    values.permissionpending_list = getpermissionpending;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }




        public void DaGetBranchgroupSummary(branchgrouplist values)
        {
            msSQL = " select  branchgroup_gid,branchgroup_code, branchgroup_name, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by, a.created_date " +
                    " from hrm_mst_tbranchgroup a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by branchgroup_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<branchgroupdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new branchgroupdtl
                    {
                        branchgroup_gid = dt["branchgroup_gid"].ToString(),
                        branchgroup_code = dt["branchgroup_code"].ToString(),
                        branchgroup_name = dt["branchgroup_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.branchgroupdtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetBranchSummary(branchlist values)
        {
            msSQL = " select  branch_gid,branch_code, branch_name,branch_prefix, branch_location, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by, a.created_date " +
                    " from hrm_mst_tbranch a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by branch_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<branchdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new branchdtl
                    {
                        branch_gid = dt["branch_gid"].ToString(),
                        branch_code = dt["branch_code"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        branch_prefix = dt["branch_prefix"].ToString(),
                        branch_location = dt["branch_location"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.branchdtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


    }

}