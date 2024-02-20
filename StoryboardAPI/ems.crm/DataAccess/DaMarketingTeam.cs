using ems.crm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;

namespace ems.crm.DataAccess
{
    public class DaMarketingTeam
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetMarketingTeamSummary(MdlMarketingTeam values)
        {
            msSQL = " select campaign_gid, campaign_title,c.branch_name as campaign_location, campaign_description, campaign_status, campaign_enddate, campaign_mailid, campaign_team,   CONCAT(b.user_firstname,' ',b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                    " from crm_trn_tcampaign a " +
                     "left join hrm_mst_tbranch c on c.branch_gid=a.campaign_location" +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by a.campaign_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<marketingteam_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new marketingteam_list
                    {
                        campaign_gid = dt["campaign_gid"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                        campaign_location= dt ["campaign_location"] .ToString(),
                        campaign_description = dt["campaign_description"].ToString(),

                        user_firstname = dt["campaign_team"].ToString(),
                        txtteammail = dt["campaign_team"].ToString(),




                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.marketingdtl = getModuleList;
                }
            }

            dt_datatable.Dispose();
        }
        public void DaGetbranchdropdown(MdlMarketingTeam values)
        {
            msSQL = " select branch_gid, branch_name" +
           " from hrm_mst_tbranch ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<branch_list1>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new branch_list1
                    {
                        branch_gid = dt["branch_gid"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                    });
                    values.Getbranchdropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetteammanagerdropdown(MdlMarketingTeam values)
        {
            msSQL = " select a.employee_gid,concat(c.user_firstname, ' ', c.user_lastname)As employee_name" +
                " from adm_mst_tmodule2employee a" +
                " left join hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                " where a.module_gid='MKT'  and c.user_status='Y' " +
         " order by employee_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<team_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new team_list
                    {
                        employee_gid = dt["employee_gid"].ToString(),
                        user_firstname = dt["employee_name"].ToString(),
                       
                    });
                    values.Getteammanagerdropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaPostmarketingteam(string user_gid, marketingteam_list values)
        {
            string lsbranch_name;


            msGetGid = objcmnfunctions.GetMasterGID("BCNP");
            //msGetGid = objcmnfunctions.GetMasterGID("HBHM");

            //msSQL = " select branch_name from hrm_mst_tbranch where branch_gid = '" + values.branch_gid + "'";
            //string lsbranch_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " insert into crm_trn_tcampaign(" +
                     " campaign_gid," +
                     " campaign_title," +
                     " campaign_description," +
                     " campaign_location," +
                     " campaign_manager," +
                     " campaign_mailid," +
                    " created_by, " +
                     " created_date)" +
                     " values(" +
                     " '" + msGetGid + "'," +
                     " '" + values.campaign_title + "'," +
                     " '" + values.campaign_description + "'," +
                     "'" + values.branch_name + "'," +
                     "'" + values.user_firstname + "'," +
                     "'" + values.txtteammail + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Marketing Team Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Marketing Team";
            }

        }
        //public void DaGeteditCurrencySummary(string termsconditions_gid, MdlMarketingCurrency values)
        //{
        //    msSQL = " select termsconditions_gid,template_name,payment_terms,template_content from crm_trn_ttermsconditions  where termsconditions_gid= '" + termsconditions_gid + "'";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    var getModuleList = new List<currency_list>();
        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        foreach (DataRow dt in dt_datatable.Rows)
        //        {
        //            getModuleList.Add(new currency_list
        //            {

        //                termsconditions_gid = dt["termsconditions_gid"].ToString(),
        //                template_name = dt["template_name"].ToString(),
        //                payment_terms = dt["payment_terms"].ToString(),
        //                template_content = dt["template_content"].ToString(),

        //            });
        //            values.currency_list = getModuleList;
        //        }
        //    }
        //    dt_datatable.Dispose();
        //}
        public void DaUpdatedmarketingteam(string user_gid, marketingteam_list values)
        {

            msSQL = " update  crm_trn_tcampaign set " +
         " campaign_title  = '" + values.campaign_title + "'," +
         " campaign_description  = '" + values.campaign_description + "'," +
          " campaign_location  = '" + values.branch_name + "'," +
         " campaign_mailid  = '" + values.txtteammail + "'," +
         " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where campaign_gid='" + values.campaign_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Marketing Team Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Marketing Team";
            }
        }

        public void DadeleteMarketingTeam(string campaign_gid, marketingteam_list values)
        {
            msSQL = "  delete from crm_trn_tcampaign  where campaign_gid='" + campaign_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Team Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Team";
            }
        }

        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlMarketingTeam values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumb_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumb_list
                    {


                        module_name1 = dt["module_name1"].ToString(),
                        sref1 = dt["sref1"].ToString(),
                        module_name2 = dt["module_name2"].ToString(),
                        sref2 = dt["sref2"].ToString(),
                        module_name3 = dt["module_name3"].ToString(),
                        sref3 = dt["sref3"].ToString(),

                    });
                    values.breadcrumb_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }



    }
}



