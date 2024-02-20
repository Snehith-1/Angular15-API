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
    public class DaMarketingmanager
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, msGetappointmentGid, msGetsheduleGid, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        private string msSql;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DaGetMarketingmanagerSummary(string user_gid, MdlMarketingmanager values)
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
            var getModuleList = new List<marketing_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new marketing_list
                    {
                        branch_name = dt["branch_name"].ToString(),
                        campaign_gid = dt["campaign_gid"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                        campaign_location = dt["campaign_location"].ToString(),
                        employeecount = dt["employeecount"].ToString(),
                        newleads = dt["newleads"].ToString(),
                        assigned_leads = dt["assigned_leads"].ToString(),
                        followup = dt["followup"].ToString(),
                        visit = dt["visit"].ToString(),
                        drop_status = dt["drop_status"].ToString(),
                        prospect = dt["prospect"].ToString(),
                        customer = dt["customer"].ToString(),


                    });
                    values.marketing_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetmarketingmanagerSummarygrid(string campaign_gid, MdlMarketingmanager values)
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
            var getModuleList = new List<marketingmanagergrid_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new marketingmanagergrid_list
                    {


                        user = dt["user"].ToString(),
                        campaign_gid = dt["campaign_gid"].ToString(),
                        total = dt["total"].ToString(),
                        followup = dt["newleads"].ToString(),
                        visit = dt["visit"].ToString(),
                        prospect = dt["prospect"].ToString(),
                        drop_status = dt["drop_status"].ToString(),
                        customer = dt["customer"].ToString(),


                    });
                    values.marketingmanagergrid_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetcampaignmanagersummary(string user_gid, string employee_gid, string campaign_gid, MdlMarketingmanager values)
        {
            msSQL = "Select  b.leadbank_name,i.call_response," +
                " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
                " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
                " Case when a.internal_notes is not null then a.internal_notes" +
                " when a.internal_notes is null then b.remarks  end as internal_notes," +
                " concat(y.user_firstname,' ',y.user_lastname)As created_by,z.leadstage_name," +
                " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
                " From crm_trn_tlead2campaign a" +
                " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
                " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
                " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
                " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
                " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
                " left join hrm_mst_temployee x on a.created_by=x.employee_gid            " +
                " left join adm_mst_tuser y on x.user_gid=y.user_gid                      " +
                " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid " +
                " left join crm_trn_tcalllog i on i.leadbank_gid=b.leadbank_gid ";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getassignproductattribute_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getassignproductattribute_list
                    {


                        leadbank_name = dt["leadbank_name"].ToString(),
                        call_response = dt["call_response"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        internal_notes = dt["internal_notes"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        lead2campaign_gid = dt["lead2campaign_gid"].ToString(),
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadstage_name = dt["leadstage_name"].ToString(),
                        campaign_gid = dt["campaign_gid"].ToString(),
                        leadbankcontact_gid = dt["leadbankcontact_gid"].ToString(),


                    });
                    values.Getassignproductattribute_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaPostschedule(string user_gid, schedule_list values)
        {
            for (int i = 0; i < values.schedules_list.ToArray().Length; i++)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BLGP");

                msSql = " insert into crm_trn_tlog ( " +
                            " log_gid, " +
                            " leadbank_gid, " +
                            " log_type, " +
                            " log_desc, " +
                            " log_by, " +
                            " reference_gid," +
                            " log_date ) " +
                            " values (  " +
                            " '" + msGetGid + "', " +
                            " '" + values.leadbank_gid + "', " +
                            " 'Schedule'," +
                            " '" + values.schedule_remarks + "', " +
                            " '" + user_gid + "'," +
                            " '" + values.reference_gid + "', " +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = false;
                    values.message = "Error Occured While Inserting Records";
                }
                else
                {

                }

                msGetappointmentGid = objcmnfunctions.GetMasterGID("APMT");


                msSQL = " insert into cmn_trn_tscheduleappointments( " +
                        " appointment_gid, " +
                        " appointment_start, " +
                        " appointment_end, " +
                        " appointment_summary, " +
                        " appointment_description, " +
                        " appointment_from, " +
                        " employee_gid, " +
                        " created_date, " +
                        " created_by, " +
                        " reference_gid " +
                        " )values( " +
                        " '" + msGetappointmentGid + "', " +
                        " '" + values.schedule_date + "', " +
                        " '" + values.schedule_date + "', " +
                        " '" + values.schedule_type + "', " +
                        " '" + values.schedule_remarks + "', " +
                        " 'CRM LEAD', " +
                        " '" + values.employee_gid + "', " +
                        " '" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                        " '" + user_gid + "', " +
                        " '" + msGetGid + "' ) ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetsheduleGid = objcmnfunctions.GetMasterGID("BSLC");
                msSQL = " Insert into crm_trn_tschedulelog ( " +
                        " schedulelog_gid, " +
                        " leadbank_gid, " +
                        " schedule_date, " +
                        " schedule_type, " +
                        " schedule_remarks, " +
                        " schedule_status, " +
                        " schedule_time, " +
                        " status_flag," +
                        " created_by, " +
                        " reference_gid," +
                        " log_gid, " +
                        " created_date ) " +
                        " Values ( " +
                        "'" + msGetsheduleGid + "'," +
                        "'" + values.leadbank_gid + "'," +
                        "'" + values.schedule_date + "'," +
                        "'" + values.schedule_type + "'," +
                        "'" + values.schedule_remarks + "'," +
                        " 'Pending'," +
                        " '" + values.schedule_time + "'," +
                        " 'N', " +
                        "'" + user_gid + "'," +
                        "'" + values.reference_gid + "'," +
                        "'" + msGetGid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update crm_trn_tlead2campaign" +
                                        " set leadstage_gid='2'" +
                                        " where leadbank_gid='" + values.leadbank_gid + "'" +
                                        " and (leadstage_gid='1' or leadstage_gid is null) ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult == 0)
                {
                    values.status = true;
                    values.message = "Lead Added successfully";
                }
                else
                {

                }
            }
        }


        public void DaGettransferdropdown(MdlMarketingmanager values)
        {
            msSQL = " SELECT campaign_gid, campaign_title FROM crm_trn_tcampaign Order by campaign_title asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<transfer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new transfer_list
                    {
                        campaign_gid = dt["campaign_gid"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                    });
                    values.transfer_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGettransfernamedropdown(MdlMarketingmanager values)
        {
            msSQL = " select a.employee_gid," +
                " concat(c.user_firstname, ' ',c.user_lastname)AS user_name" +
                " from crm_trn_tcampaign2employee a" +
                " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid=c.user_gid";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<transfername_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new transfername_list
                    {
                        employee_gid = dt["employee_gid"].ToString(),
                        user_name = dt["user_name"].ToString(),
                    });
                    values.transfername_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGettransferdropdownonchange(string campaign_gid, MdlMarketingmanager values)
        {
            msSQL = " select a.employee_gid," +
                " concat(c.user_firstname, ' ',c.user_lastname)AS user_name" +
                " from crm_trn_tcampaign2employee a" +
                " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
              "  where a.campaign_gid='" + campaign_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<transfernameonchange_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new transfernameonchange_list
                    {
                        employee_gid = dt["employee_gid"].ToString(),
                        user_name = dt["user_name"].ToString(),
                    });
                    values.transfernameonchange_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPosttransfer(string user_gid, transfer_list values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("BLGP");
            for (int i = 0; i < values.trransfer_list.ToArray().Length; i++)
            {
                msSQL = "  update crm_trn_tlead2campaign Set " +
                        " assign_to = '" + values.employee_gid + "'," +
                        " campaign_gid = '" + values.campaign_gid + "'" +
                        " where leadbank_gid = '" + values.leadbank_gid + "'and" +
                        " assign_to = '" + values.employee_gid + "' and " +
                        " lead2campaign_gid='" + values.reference_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 0)
                {
                    values.status = true;
                    values.message = "Transfer Successfully";
                }
                else
                {

                }
            }
        }
            public void Dabindelete( string employee_gid, transfer_list values)
            {
                for (int i = 0; i < values.trransfer_list.ToArray().Length; i++)
                {

                    msSQL = "  update crm_trn_tlead2campaign Set "+
                        " assign_to = '" + employee_gid + "'"+
                        " where leadbank_gid = '" + values.leadbank_gid + "'and"+
                        " assign_to = '" + employee_gid + "' and "+
                        " lead2campaign_gid='" + values.reference_gid +  "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Product Unassign Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error While Product Unassign";
                    }
                }
            }

        }
    }


               