using System;
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

namespace ems.system.DataAccess
{
    public class DaEmployeeProfile
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        int mnResult6;







        public void DaWorkExperience(string user_gid, EmployeeProfile values)
        {
            msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("EMPH");






            msSQL = " insert into hrm_mst_temployeehistory(" +
                    "employeehistory_gid," +
                    " employee_gid," +
                    " previous_company," +
                    " previuos_occupation," +
                     "work_period," +
                     "conduct_info," +
                    " employee_dept," +
                    " date_joining," +
                    " date_releiving," +
                    " reporting_to, " +
                    "leave_reason," +
                    "employee_code, " +
                    "remarks, " +
                    "created_by, " +
                   " created_date)" +
                    " values(" +
                     " '" + msGetGid + "'," +
                       " '" + lsemployee_gid + "'," +

                    "'" + values.employee_previous_company + "'," +
                     "'" + values.previous_occupation + "'," +
                      "'" + values.working_period + "'," +
                     "'" + values.HR_namecontactinformation + "'," +
                       "'" + values.department + "'," +
                         "'" + values.date_ofjoining + "'," +
                     "'" + values.date_ofrelieving + "'," +
                       "'" + values.reporting_to + "'," +
                        "'" + values.reason_toleavethepreviouscompany + "'," +
                      "'" + values.employee_code + "'," +
                       "'" + values.re_marks + "'," +
                       "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Entity Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Entity";
            }
        }

        public void DaNomination(string user_gid, EmployeeProfile values)
        {
            msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("FFMLY");






            msSQL = " insert into hrm_trn_tformfamilydetails(" +
                    "familydtl_gid," +
                    " employee_gid," +
                    " familymember_name," +
                    " age," +
                     "dob," +
                     "mobile_no," +
                    "relationship," +
                    "residing_with," +
                    " nominee, " +
                    "created_by, " +
                   " created_date)" +
                    " values(" +
                     " '" + msGetGid + "'," +
                       " '" + lsemployee_gid + "'," +

                    "'" + values.user_name + "'," +
                     "'" + values.age + "'," +
                      "'" + values.date_ofbirth + "'," +
                     "'" + values.relationship_withtheemployee + "'," +
                       "'" + values.whether_residingwiththeemployee + "'," +
                         "'" + values.put_asnomineefor + "'," +
                       "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Entity Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Entity";
            }
        }

        public void DaStatutory(string user_gid, EmployeeProfile values)
        {
            msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);







            msSQL = " insert into hrm_mst_tstatutorydetail(" +
                    "pfref_no," +
                    " esicref_no," +
                    " esic_branchoffice," +
                    " dispensary," +
                    " created_by, " +
                   " created_date)" +
                    " values(" +
                    "'" + values.provident_fund_account_no + "'," +
                     "'" + values.employee_state_insurance_no + "'," +
                      "'" + values.date_of_join_of_PF + "'," +
                       "'" + values.remarks + "'," +
                       "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Entity Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Entity";
            }
        }
        public void DaEmergencyContact(string user_gid, EmployeeProfile values)
        {
            msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
            msGetGid = objcmnfunctions.GetMasterGID("HREC");







            msSQL = " insert into hrm_tmp_temergency(" +
                    " emergency_gid," +
                    " contact_name," +
                    " contact_address," +
                    " contact_no," +
                    " contact_email," +
                    " reference_details," +
                    " created_by, " +
                   " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    "'" + values.contact_person + "'," +
                     "'" + values.contact_address + "'," +
                       "'" + values.contact_no + "'," +
                     "'" + values.contact_emailid + "'," +
                     "'" + values.remarks + "'," +
                       "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Entity Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Entity";
            }
        }


        public void DaDependent(string user_gid, EmployeeProfile values)
        {
            //msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            //lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
            msGetGid = objcmnfunctions.GetMasterGID("HRDE");







            msSQL = " insert into hrm_tmp_tdependent(" +
               " dependent_gid," +
                     " name," +
                    " relationship," +
                    " dob," +
                   " created_by, " +
                   " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                     "'" + values.name + "'," +
                       "'" + values.relationship + "'," +
                     "'" + values.date_of_birth + "'," +
                     "'" + values.remarks + "'," +
                       "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Entity Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Entity";
            }
        }
        public void DaEducation(string user_gid, EmployeeProfile values)
        {
            //msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            //lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
            msGetGid = objcmnfunctions.GetMasterGID("HRED");
            msSQL = " insert into hrm_tmp_teducation(" +
                               " education_gid," +
                               " institution_name," +
                               " degree_diploma," +
                               " field_study," +
                               " date_completion," +
                               " address_notes," +
                               " created_by, " +
                               " updated_by, " +
                              " created_date)" +
                              " updated_date)" +
                               " values(" +
                               " '" + msGetGid + "'," +
                               "'" + values.institution_name + "'," +
                                "'" + values.degree_diploma + "'," +
                                  "'" + values.field_ofstudy + "'," +
                                "'" + values.date_ofcompletion + "'," +
                                "'" + values.additional_notes + "'," +
                                  "'" + user_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Entity Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Entity";
            }
        }
    }
}