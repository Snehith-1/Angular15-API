using ems.crm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ems.crm.DataAccess
{
    public class DaTeleCallerManager
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, msGetappointmentGid, msGetsheduleGid, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        private string msSql;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DatelecallerteammanagerSummary(string employee_gid, MdlTeleCallerManager values)
        {
            msSQL = " SELECT  a.campaign_gid, a.campaign_title,a.campaign_location,c.branch_name, " +
                    " (select count(x.employee_gid) as employeecount from crm_trn_tcampaign2employee x " +
                    " where x.campaign_gid= a.campaign_gid) as employeecount, " +
                    " (SELECT count(x.lead2campaign_gid) as total FROM crm_trn_tlead2campaign x " +
                    " where x.campaign_gid = a.campaign_gid) as assigned_leads, " +
                    " (SELECT count(x.lead2campaign_gid) as new FROM crm_trn_tlead2campaign x " +
                    " where x.so_status <>'Y' and  (x.leadstage_gid ='1' or x.leadstage_gid is null) and x.campaign_gid = a.campaign_gid ) as newleads, " +
                    " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                    " where x.so_status <>'Y' and  (x.leadstage_gid ='2')  and x.campaign_gid = a.campaign_gid ) as followup, " +
                    " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                    " where x.so_status <>'Y' and  (x.leadstage_gid ='4')  and x.campaign_gid = a.campaign_gid ) as visit, " +
                    " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                    " where x.so_status <>'Y' and  (x.leadstage_gid ='3')  and x.campaign_gid = a.campaign_gid ) as prospect, " +
                    " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                    " where   (x.leadstage_gid ='5')  and x.campaign_gid = a.campaign_gid ) as drop_status, " +
                    " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                    " where   (x.leadstage_gid ='6')  and x.campaign_gid = a.campaign_gid ) as customer " +
                    " FROM crm_trn_tcampaign a " +
                    " left join hrm_mst_tbranch c on a.campaign_location = c.branch_gid " +
                    " where a.campaign_gid in (select team_gid " +
                    " from cmn_trn_tmanagerprivilege)" +
                    " group by a.campaign_gid desc ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<telecaller_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new telecaller_list
                    {
                        
                        campaign_gid = dt["campaign_gid"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                        campaign_location = dt["campaign_location"].ToString(),
                        employeecount = dt["employeecount"].ToString(),
                        branch_name = dt["branch_name"].ToString(),


                        assigned_leads = dt["assigned_leads"].ToString(),
                        followup = dt["followup"].ToString(),
                       
                        drop_status = dt["drop_status"].ToString(),
                        prospect = dt["prospect"].ToString(),
                       


                    });
                    values.telecallerlist = getModuleList;
                }
            }
            dt_datatable.Dispose();

        }
   



public void DatelecallerteammanagerSummary1(string campaign_gid, MdlTeleCallerManager values)
{
    msSQL = " select distinct a.campaign_gid,e.department_name," +
                " a.employee_gid as assign_to,concat(c.user_firstname, '-', c.user_code) as user, " +
                " ( SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                " where x.assign_to = a.employee_gid and x.campaign_gid = a.campaign_gid) as total, " +
                " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                " where x.assign_to = a.employee_gid and (x.leadstage_gid ='1' or x.leadstage_gid is null) and x.campaign_gid = a.campaign_gid) as newleads, " +
                " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                " where x.assign_to = a.employee_gid and x.leadstage_gid ='2' and x.campaign_gid = a.campaign_gid) as followup, " +
                " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                " where x.assign_to = a.employee_gid and x.leadstage_gid ='4' and x.campaign_gid = a.campaign_gid) as visit, " +
                " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                " where x.assign_to = a.employee_gid and x.leadstage_gid ='3' and x.campaign_gid = a.campaign_gid) as prospect, " +
                " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                " where x.assign_to = a.employee_gid and x.leadstage_gid ='5' and x.campaign_gid = a.campaign_gid) as drop_status, " +
                " (SELECT count(x.lead2campaign_gid) FROM crm_trn_tlead2campaign x " +
                " where x.assign_to = a.employee_gid and x.leadstage_gid ='6' and x.campaign_gid = a.campaign_gid) as customer " +
                " from crm_trn_tcampaign2employee a" +
                " left join hrm_mst_temployee b on a.employee_gid = b.employee_gid " +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                " left join hrm_mst_tdepartment e on b.department_gid=e.department_gid" +
                " where a.campaign_gid= '" + campaign_gid + "' ";
    dt_datatable = objdbconn.GetDataTable(msSQL);
    var getModuleList = new List<Telecallermanager_list>();
    if (dt_datatable.Rows.Count != 0)
    {
        foreach (DataRow dt in dt_datatable.Rows)
        {
            getModuleList.Add(new Telecallermanager_list
            {


                user = dt["user"].ToString(),
                campaign_gid = dt["campaign_gid"].ToString(),
                total = dt["total"].ToString(),
                followup = dt["followup"].ToString(),

                prospect = dt["prospect"].ToString(),
                drop_status = dt["drop_status"].ToString(),
              


            });
            values.Telecallermanagerlist = getModuleList;
        }
    }
    dt_datatable.Dispose();
}
    }
}