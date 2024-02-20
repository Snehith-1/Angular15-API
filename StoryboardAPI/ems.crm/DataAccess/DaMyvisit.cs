using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.crm.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
//using System.Web;
//using OfficeOpenXml;
using System.Configuration;
using System.IO;
//using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;
//using static ems.crm.Models.region_list;


namespace ems.crm.DataAccess
{
    public class DaMyvisit
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetMyvisitSummary(MdlMyvisit values, string employee_gid)
        {
            msSQL = " select f.assign_to,a.log_gid,a.schedulelog_gid,b.leadbank_gid,a.schedule_remarks,a.schedule_status, " +
                 " cast(concat(a.schedule_date,' ', a.schedule_time) as datetime) as schedule," +
                " concat(b.leadbank_address1,'/',b.leadbank_address2,'/',b.leadbank_city,'/',b.leadbank_state,'-',b.leadbank_pin)as customer_address," +
                " concat(c.leadbankcontact_name,' / ',c.mobile,' / ',c.email) as contact_details," +
                " b.leadbank_name,d.region_name,a.schedule_type,a.schedule_remarks,f.lead2campaign_gid,c.leadbankcontact_gid  from crm_trn_tschedulelog a " +
                 " inner join crm_trn_tleadbank b on a.leadbank_gid=b.leadbank_gid " +
                 " inner join crm_trn_tleadbankcontact c on b.leadbank_gid = c.leadbank_gid " +
                 " left join crm_mst_tregion d on b.leadbank_region=d.region_gid " +
                 " left join crm_mst_tsource e on b.source_gid=e.source_gid " +
                 " left join crm_trn_tlead2campaign f on b.leadbank_gid=f.leadbank_gid ";
            //" where (a.schedule_type='Meeting') and a.schedule_date = curdate() " +
            //" and a.assign_to ='" + employee_gid + "'" +
            //" and c.status='Y' and c.main_contact='Y' order by b.leadbank_name asc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Myvisit_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Myvisit_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        customer_address = dt["customer_address"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        schedule_type = dt["schedule_type"].ToString(),
                        schedule = dt["schedule"].ToString(),

                        ScheduleRemarks = dt["schedule_remarks"].ToString(),
                        schedule_status = dt["schedule_status"].ToString(),





                        //created_date = dt["created_date"].ToString(),
                    });
                    values.Myvisitlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }



        public void DaGetTodayvisitSummary(MdlMyvisit values, string employee_gid)
        {
            msSQL = " select f.assign_to,a.log_gid,a.schedulelog_gid,b.leadbank_gid,a.schedule_remarks,a.schedule_status, " +
                 " cast(concat(a.schedule_date,' ', a.schedule_time) as datetime) as schedule," +
                " concat(b.leadbank_address1,'/',b.leadbank_address2,'/',b.leadbank_city,'/',b.leadbank_state,'-',b.leadbank_pin)as customer_address," +
                " concat(c.leadbankcontact_name,' / ',c.mobile,' / ',c.email) as contact_details," +
                " b.leadbank_name,d.region_name,a.schedule_type,a.schedule_remarks,f.lead2campaign_gid,c.leadbankcontact_gid  from crm_trn_tschedulelog a " +
                 " inner join crm_trn_tleadbank b on a.leadbank_gid=b.leadbank_gid " +
                 " inner join crm_trn_tleadbankcontact c on b.leadbank_gid = c.leadbank_gid " +
                 " left join crm_mst_tregion d on b.leadbank_region=d.region_gid " +
                 " left join crm_mst_tsource e on b.source_gid=e.source_gid " +
                 " left join crm_trn_tlead2campaign f on b.leadbank_gid=f.leadbank_gid ";
            //" where (a.schedule_type='Meeting') and a.schedule_date = curdate() " +
            //" and a.assign_to ='" + employee_gid + "'" +
            //" and c.status='Y' and c.main_contact='Y' order by b.leadbank_name asc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Todayvisit_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Todayvisit_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        customer_address = dt["customer_address"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        schedule_type = dt["schedule_type"].ToString(),
                        schedule = dt["schedule"].ToString(),

                        ScheduleRemarks = dt["schedule_remarks"].ToString(),
                        schedule_status = dt["schedule_status"].ToString(),





                        //created_date = dt["created_date"].ToString(),
                    });
                    values.Todayvisitlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetUpcomingvisitSummary(MdlMyvisit values, string employee_gid)
        {
            msSQL = " select f.assign_to,a.log_gid,a.schedulelog_gid,b.leadbank_gid,a.schedule_remarks,a.schedule_status, " +
                 " cast(concat(a.schedule_date,' ', a.schedule_time) as datetime) as schedule," +
                " concat(b.leadbank_address1,'/',b.leadbank_address2,'/',b.leadbank_city,'/',b.leadbank_state,'-',b.leadbank_pin)as customer_address," +
                " concat(c.leadbankcontact_name,' / ',c.mobile,' / ',c.email) as contact_details," +
                " b.leadbank_name,d.region_name,a.schedule_type,a.schedule_remarks,f.lead2campaign_gid,c.leadbankcontact_gid  from crm_trn_tschedulelog a " +
                 " inner join crm_trn_tleadbank b on a.leadbank_gid=b.leadbank_gid " +
                 " inner join crm_trn_tleadbankcontact c on b.leadbank_gid = c.leadbank_gid " +
                 " left join crm_mst_tregion d on b.leadbank_region=d.region_gid " +
                 " left join crm_mst_tsource e on b.source_gid=e.source_gid " +
                 " left join crm_trn_tlead2campaign f on b.leadbank_gid=f.leadbank_gid ";
            //" where (a.schedule_type='Meeting') and a.schedule_date = curdate() " +
            //" and a.assign_to ='" + employee_gid + "'" +
            //" and c.status='Y' and c.main_contact='Y' order by b.leadbank_name asc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Upcomingvisit_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Upcomingvisit_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        customer_address = dt["customer_address"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        schedule_type = dt["schedule_type"].ToString(),
                        schedule = dt["schedule"].ToString(),

                        ScheduleRemarks = dt["schedule_remarks"].ToString(),
                        schedule_status = dt["schedule_status"].ToString(),





                        //created_date = dt["created_date"].ToString(),
                    });
                    values.Upcomingvisitlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetProductdropdown(MdlMyvisit values)
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
                        product_name = dt["product_name"].ToString(),


                    });
                    values.productlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostonlinemeeting(online_list values,string user_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("BOMC");


            msSQL = " insert into crm_trn_tonmeetlog(" +
                     " onmeetlog_gid," +
                     " leadbank_gid   ," +
                      " onmeet_date," +
                      " onmeet_hour," +
                       " technical_aid," +
                       "onmeet_remarks,"+
                       "created_by," +

                      " created_date)" +
                      " values(" +
                      " '" + msGetGid + "'," +
                     " '" + values.leadbank_gid + "'," +
                      "'" + values.dateof_demo + "'," +
                      "'" + values.meeting_time + "'," +
                      "'" + values.technical_assist + "'," +
                       "'" + values.demo_remarks + "'," +


                        "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Postonlinemeeting Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Postonlinemeeting";
            }

        }

        public void DaPostofflinemeeting(offline_list values, string user_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("BFLC");


            msSQL = " insert into crm_trn_tfieldlog(" +
                     " fieldlog_gid," +
                     " leadbank_gid   ," +
                      " fieldvisit_date," +
                      " fieldvisit_hour," +
                       " fieldvisit_contactperson," +
                       " fieldvisit_contactperson2," +

                       " fieldvisit_location," +
                        "fieldvisit_remarks," +
                       "created_by," +
                      " created_date)" +
                      " values(" +
                      " '" + msGetGid + "'," +
                     " '" + values.leadbank_gid + "'," +
                      "'" + values.dateof_visit + "'," +
                      "'" + values.meeting_time + "'," +
                      "'" + values.visit_by + "'," +
                      "'" + values.contact_person + "'," +

                      "'" + values.Location + "'," +
                       "'" + values.meeting_remarks + "'," +
                        "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Postofflinemeeting Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Postofflinemeeting";
            }

        }

        public void Daproductgroupchange(MdlMyvisit values,string product_gid)
        {
            msSQL = " Select a.productgroup_gid, a.productgroup_name " +
              " from pmr_mst_tproductgroup a " +
              " inner join pmr_mst_tproduct b on a.productgroup_gid=b.productgroup_gid " +
              " where b.product_gid='" + product_gid + "' " +
              " order by productgroup_name asc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<productgroup_list1>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new productgroup_list1
                    {
                        productgroup_gid = dt["productgroup_gid"].ToString(),
                        productgroup_name = dt["productgroup_name"].ToString(),


                    });
                    values.productgrouplist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlMyvisit values)
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









