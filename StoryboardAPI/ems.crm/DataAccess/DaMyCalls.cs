using ems.crm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Http;

namespace ems.crm.DataAccess
{

    public class DaMyCalls
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetMycallsSummary(MdlMyCalls values, string employee_gid)
        {
            msSQL = " Select b.leadbank_name,k.campaign_title," +
              " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
              " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as regionname," +
              " Case when a.internal_notes is not null then a.internal_notes" +
              " when a.internal_notes is null then b.remarks  end as internal_notes," +
              " date(i.schedule_date) as schedule," +
              " i.schedule_type,i.schedule_remarks,z.leadstage_name," +
              " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
              " From crm_trn_tlead2campaign a" +
              " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
              " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
              " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
              " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
              " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
              " left join crm_trn_tschedulelog i on a.leadbank_gid=i.leadbank_gid " +
              " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              " order by b.leadbank_name asc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<mycalls_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new mycalls_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        lead2campaign_gid = dt["lead2campaign_gid"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        regionname = dt["regionname"].ToString()
                        
                    });

                    values.mycallslist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetPendingSummary(MdlMyCalls values, string employee_gid)
        {
            msSQL = " Select b.leadbank_name,k.campaign_title,f.call_response," +
              " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
              " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as regionname," +
              " Case when a.internal_notes is not null then a.internal_notes" +
              " when a.internal_notes is null then b.remarks  end as internal_notes," +
              " date(i.schedule_date) as schedule," +
              " i.schedule_type,i.schedule_remarks,z.leadstage_name," +
              " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
              " From crm_trn_tlead2campaign a" +
              " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
              " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
              " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
              " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
              " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
               " left join crm_trn_tcalllog f on f.leadbank_gid = a.leadbank_gid " +
              " left join crm_trn_tschedulelog i on a.leadbank_gid=i.leadbank_gid " +
              " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              " order by b.leadbank_name asc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<pending_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new pending_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        lead2campaign_gid = dt["lead2campaign_gid"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        regionname = dt["regionname"].ToString(),
                        call_response = dt["call_response"].ToString(),

                    });

                    values.pendinglist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetFollowupSummary(MdlMyCalls values, string employee_gid)
        {
            msSQL = " Select b.leadbank_name,k.campaign_title,f.call_response," +
              " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
              " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as regionname," +
              " Case when a.internal_notes is not null then a.internal_notes" +
              " when a.internal_notes is null then b.remarks  end as internal_notes," +
              " date(i.schedule_date) as schedule," +
              " i.schedule_type,i.schedule_remarks,z.leadstage_name," +
              " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
              " From crm_trn_tlead2campaign a" +
              " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
              " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
              " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
              " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
              " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
               " left join crm_trn_tcalllog f on f.leadbank_gid = a.leadbank_gid " +
              " left join crm_trn_tschedulelog i on a.leadbank_gid=i.leadbank_gid " +
              " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              " order by b.leadbank_name asc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<followup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new followup_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        lead2campaign_gid = dt["lead2campaign_gid"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        regionname = dt["regionname"].ToString(),
                        

                    });

                    values.followuplist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetClosedSummary(MdlMyCalls values, string employee_gid)
        {
            msSQL = " Select b.leadbank_name,k.campaign_title,f.call_response," +
              " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
              " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as regionname," +
              " Case when a.internal_notes is not null then a.internal_notes" +
              " when a.internal_notes is null then b.remarks  end as internal_notes," +
              " (i.schedule_date) as schedule," +
              " i.schedule_type,i.schedule_remarks,z.leadstage_name," +
              " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
              " From crm_trn_tlead2campaign a" +
              " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
              " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
              " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
              " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
              " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
               " left join crm_trn_tcalllog f on f.leadbank_gid = a.leadbank_gid " +
              " left join crm_trn_tschedulelog i on a.leadbank_gid=i.leadbank_gid " +
              " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              " order by b.leadbank_name asc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<closed_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new closed_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        lead2campaign_gid = dt["lead2campaign_gid"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        regionname = dt["regionname"].ToString(),
                        schedule_type = dt["schedule_type"].ToString(),
                        schedule = dt["schedule"].ToString(),


                    });

                    values.closedlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetDropSummary(MdlMyCalls values, string employee_gid)
        {

            msSQL = " Select b.leadbank_name,k.campaign_title,f.call_response," +
              " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
              " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as regionname," +
              " Case when a.internal_notes is not null then a.internal_notes" +
              " when a.internal_notes is null then b.remarks  end as internal_notes," +
              " date(i.schedule_date) as schedule," +
              " i.schedule_type,i.schedule_remarks,z.leadstage_name," +
              " a.lead2campaign_gid,a.lead_base, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
              " From crm_trn_tlead2campaign a" +
              " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
              " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
              " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
              " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
              " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
               " left join crm_trn_tcalllog f on f.leadbank_gid = a.leadbank_gid " +
              " left join crm_trn_tschedulelog i on a.leadbank_gid=i.leadbank_gid " +
              " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              " order by b.leadbank_name asc";



            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<drop_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new drop_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        lead2campaign_gid = dt["lead2campaign_gid"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        regionname = dt["regionname"].ToString(),
                        lead_base = dt["lead_base"].ToString(),


                    });

                    values.droplist = getModuleList;
                }
            }
            dt_datatable.Dispose();

        }

        public void DaGetProductdropdown(MdlMyCalls values)
        {
            msSQL = "select product_gid,product_name from pmr_mst_tproduct";



            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<product_list1>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new product_list1
                    {
                        product_gid = dt["product_gid"].ToString(),
                        product_name = dt["product_name"].ToString()





                    });
                    values.productlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetViewLog(MdlMyCalls values, string user_gid)
        {

            msSQL = " select concat(a.user_firstname,'',a.user_lastname) as user_gid, f.call_response," +
                     " concat( 'g.mobile' ) as contact_details, f.leadbank_gid, b.leadbank_name," +
                     " date(i.schedule_date) as schedule," +
                     " f.prospective_percentage," +
                     " i.schedule_type,i.schedule_remarks " +
                     " From adm_mst_tuser a" +
                     " left join crm_trn_tleadbank b on b.leadbank_name = b.leadbank_name " +
                     " left join crm_trn_tcampaign k on k.campaign_gid = k.campaign_gid " +
                     "  left join crm_trn_tcalllog f on f.leadbank_gid = f.leadbank_gid " +
                     " left join crm_trn_tschedulelog i on i.leadbank_gid=i.leadbank_gid " +
                     " where a.user_gid='" + user_gid + "'" +
                    
                     " order by a.user_gid asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<view_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new view_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        user_gid = dt["user_gid"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        prospective_percentage = dt["prospective_percentage"].ToString(),
                        schedule_remarks = dt["schedule_remarks"].ToString()

                    });

                    values.viewlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostShowlogsubmit(log_list values, string user_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("BCLC");
            msSQL = " insert into crm_trn_tcalllog  (" +
            " calllog_gid, " +
            " leadbank_gid," +
             " mobile_number, " +
             " call_response, " +
              " prospective_percentage, " +
              " remarks, " +
              " created_by, " +
              " created_date ) " +
            " values (" +
           " '" + msGetGid + "', " +
           " '" + values.leadbank_gid + "', " +
           " '" + values.call_response + "'," +
           " '" + values.contact_details + "'," +
          " '" + values.prospective_percentage + "'," +
           " '" + values.remarks + "'," +
           "'" + user_gid + "'," +
           "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')"; 
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = " Call Recorded Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = " Error Occurs ";

            }

        }

            public void DaGetproductgroupchange(MdlMyCalls values, string product_gid)
            {
                msSQL = " Select a.productgroup_gid, a.productgroup_name " +
                  " from pmr_mst_tproductgroup a " +
                  " inner join pmr_mst_tproduct b on a.productgroup_gid=b.productgroup_gid " +
                  " where b.product_gid='" + product_gid + "' " +
                  " order by productgroup_name asc ";



                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getModuleList = new List<productgroup_list2>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getModuleList.Add(new productgroup_list2
                        {
                            productgroup_gid = dt["productgroup_gid"].ToString(),
                            productgroup_name = dt["productgroup_name"].ToString(),





                        });
                        values.productgrouplist = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }

        public void DaPostfollowlogsubmit(flog_list values, string user_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("BCLC");
            msSQL = " insert into crm_trn_tcalllog  (" +
            " calllog_gid, " +
            " leadbank_gid," +
             " mobile_number, " +
             " call_response, " +
              " prospective_percentage, " +
              " remarks, " +
              " created_by, " +
              " created_date ) " +
            " values (" +
           " '" + msGetGid + "', " +
           " '" + values.leadbank_gid + "', " +
           " '" + values.contact_details + "'," +
           " '" + values.call_response + "'," + 
            " '" + values.prospective_percentage + "'," +
           " '" + values.remarks + "'," +
           "'" + user_gid + "'," +
           "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = " Call Recorded Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = " Error Occurs ";

            }

        }

        public void DaPostpendinglogsubmit(plog_list values, string user_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("BCLC");
            msSQL = " insert into crm_trn_tcalllog  (" +
            " calllog_gid, " +
            " leadbank_gid," +
             " mobile_number, " +
             " call_response, " +
             " prospective_percentage, " +
              " remarks, " +
              " created_by, " +
              " created_date ) " +
            " values (" +
           " '" + msGetGid + "', " +
           " '" + values.leadbank_gid + "', " +
            " '" + values.contact_details + "'," +
           " '" + values.call_response + "'," +
          
            " '" + values.prospective_percentage + "'," +
           " '" + values.remarks + "'," +
           "'" + user_gid + "'," +
           "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = " Call Recorded Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = " Error Occurs ";

            }

        }

        public void DaPostschedulesubmit(slog_list values, string user_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("BSLC");
            msSQL = " insert into crm_trn_tschedulelog  (" +
            " schedulelog_gid, " +
            " leadbank_gid," +
             " schedule_date, " +
             " schedule_time, " +
             " schedule_type, " + 
              " schedule_remarks, " +
              " status_flag, " +
              " reference_gid, " +
              " log_gid, " +
              " created_by, " +
              " created_date ) " +
            " values (" +
           " '" + msGetGid + "', " +
           " '" + values.leadbank_gid + "', " +
           " '" + values.schedule_date + "'," +
           " '" + values.schedule_time + "'," +
           " '" + values.schedule_type + "'," +
           " '" + values.schedule_remarks + "'," +
           "'" + user_gid + "'," +
           "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = " Call scheduled  Successfully";
            }
            else
            {
                values.status = false;
                values.message = " Error Occurs ";

            }

        }

        public void DaGetbreadcrumb2(string user_gid, string module_gid, MdlMyCalls values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";



            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumblist2>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumblist2
                    {




                        module_name1 = dt["module_name1"].ToString(),
                        sref1 = dt["sref1"].ToString(),
                        module_name2 = dt["module_name2"].ToString(),
                        sref2 = dt["sref2"].ToString(),
                        module_name3 = dt["module_name3"].ToString(),
                        sref3 = dt["sref3"].ToString(),



                    });
                    values.breadcrumb2_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
    }
    }



        



    
