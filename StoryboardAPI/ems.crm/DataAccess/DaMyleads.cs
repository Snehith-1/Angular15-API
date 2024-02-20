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
using System.Net.NetworkInformation;

namespace ems.crm.DataAccess
{
    public class DaMyleads
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetPrivilege_gid, msGetModule2employee_gi,
         lssource_name, lsleadbank_name, lscategoryindustry_name, lscountry_name, lsregion_name, lsbankcontact, msGetGid, msGetGid1, msGetGid2, msGetGid3, msGetGid4, msGetGid5, msGetGid6, msGetGid7, msGetGid8, msGetGid9, msGetGid10;

        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5, mnResult6, mnResult7, mnResult8, mnResult9, mnResult10, mnResult11, mnResult12, mnResult13,mnResult14;

        public void DaGetMyleadsSummary(MdlMyleads values, string employee_gid)

        {
            msSQL = " Select b.leadbank_name, k.campaign_title, f.call_response, i.schedule_type," +
                " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
                " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
                    " Case when a.internal_notes is not null then a.internal_notes" +
                    " when a.internal_notes is null then b.remarks  end as internal_notes," +
                   " cast(concat(i.schedule_date,' ', i.schedule_time) as datetime) as schedule," +
                    " z.leadstage_name," +
                    " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
                    " From crm_trn_tlead2campaign a" +
                    " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
                    " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
                    " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
                    " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
                    " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
                    " left join crm_trn_tcalllog f on f.call_response=f.call_response" +
                    " left join crm_trn_tschedulelog i on a.leadbank_gid = i.leadbank_gid " +
                    " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
                     " where a.assign_to = '" + employee_gid + "' " +
              " and i.schedule_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' or i.schedule_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
              " and g.status='Y' and g.main_contact='Y' and i.assign_to=  '" + employee_gid + "' " +

                    " order by b.leadbank_name asc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<myleads_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new myleads_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        lead2campaign_gid = dt["lead2campaign_gid"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        schedule_type = dt["schedule_type"].ToString(),

                    });

                    values.myleadslist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetInprogressSummary(MdlMyleads values, string employee_gid)
        {
            msSQL = " Select b.leadbank_name, k.campaign_title, f.call_response, i.schedule_type," +
                " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
                " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
                    " Case when a.internal_notes is not null then a.internal_notes" +
                    " when a.internal_notes is null then b.remarks  end as internal_notes," +
                   " cast(concat(i.schedule_date,' ', i.schedule_time) as datetime) as schedule," +
                    " z.leadstage_name," +
                    " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
                    " From crm_trn_tlead2campaign a" +
                    " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
                    " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
                    " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
                    " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
                    " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
                    " left join crm_trn_tcalllog f on f.call_response=f.call_response" +
                    " left join crm_trn_tschedulelog i on a.leadbank_gid = i.leadbank_gid " +
                    " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
              //      " where a.assign_to = '" + employee_gid + "' " +
              //" and (a.leadstage_gid ='3')" +
              //" and g.status='Y' and g.main_contact='Y'" +
                   " order by b.leadbank_name asc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<inprogress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new inprogress_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        lead2campaign_gid = dt["lead2campaign_gid"].ToString(),
                        campaign_title = dt["campaign_title"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        region_name = dt["region_name"].ToString(),


                    });

                    values.inprogresslist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetCustomerSummary(MdlMyleads values, string employee_gid)
        {
            msSQL = " Select b.leadbank_name, k.campaign_title, f.call_response, i.schedule_type," +
                " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
                " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
                    " Case when a.internal_notes is not null then a.internal_notes" +
                    " when a.internal_notes is null then b.remarks  end as internal_notes," +
                   " cast(concat(i.schedule_date,' ', i.schedule_time) as datetime) as schedule," +
                    " z.leadstage_name," +
                    " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
                    " From crm_trn_tlead2campaign a" +
                    " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
                    " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
                    " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
                    " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
                    " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
                    " left join crm_trn_tcalllog f on f.call_response=f.call_response" +
                    " left join crm_trn_tschedulelog i on a.leadbank_gid = i.leadbank_gid " +
                    " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
                     " where a.assign_to = '" + ("employee_gid") + "' " +
              " and (a.leadstage_gid ='6')" +
              " and g.status='Y' and g.main_contact='Y' " +
                    " order by b.leadbank_name asc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<customer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new customer_list
                    {
                        campaign_title = dt["campaign_title"].ToString(),
                        internal_notes = dt["internal_notes"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        region_name = dt["region_name"].ToString(),
                    });

                    values.customerlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetDropSummary(MdlMyleads values, string employee_gid)
        {
            msSQL = " Select b.leadbank_name, k.campaign_title, f.call_response, i.schedule_type," +
                " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
                " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
                    " Case when a.internal_notes is not null then a.internal_notes" +
                    " when a.internal_notes is null then b.remarks  end as internal_notes," +
                   " cast(concat(i.schedule_date,' ', i.schedule_time) as datetime) as schedule," +
                    " z.leadstage_name," +
                    " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
                    " From crm_trn_tlead2campaign a" +
                    " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
                    " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
                    " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
                    " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
                    " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
                    " left join crm_trn_tcalllog f on f.call_response=f.call_response" +
                    " left join crm_trn_tschedulelog i on a.leadbank_gid = i.leadbank_gid " +
                    " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
                     " where a.assign_to = '" + ("employee_gid") + "'  " +
              " and (a.leadstage_gid ='5')" +
              " and g.status='Y' and g.main_contact='Y' " +
                    " order by b.leadbank_name asc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<drop_list1>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new drop_list1
                    {
                        campaign_title = dt["campaign_title"].ToString(),
                        internal_notes = dt["internal_notes"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        leadstage_name = dt["leadstage_name"].ToString(),
                    });

                    values.droplist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetAllSummary(MdlMyleads values, string employee_gid)
        {
            msSQL = " Select b.leadbank_name, k.campaign_title, f.call_response, i.schedule_type," +
                " concat(g.leadbankcontact_name,' / ',g.mobile,' / ',g.email) as contact_details," +
                " concat(d.region_name,'/',b.leadbank_city,'/',b.leadbank_state,'/',h.source_name) as region_name," +
                    " Case when a.internal_notes is not null then a.internal_notes" +
                    " when a.internal_notes is null then b.remarks  end as internal_notes," +
                   " cast(concat(i.schedule_date,' ', i.schedule_time) as datetime) as schedule," +
                    " z.leadstage_name," +
                    " a.lead2campaign_gid, a.leadbank_gid, a.campaign_gid, g.leadbankcontact_gid" +
                    " From crm_trn_tlead2campaign a" +
                    " left join crm_trn_tleadbank b on a.leadbank_gid = b.leadbank_gid        " +
                    " left join crm_mst_tregion d on b.leadbank_region=d.region_gid           " +
                    " left join crm_trn_tleadbankcontact g on b.leadbank_gid = g.leadbank_gid " +
                    " left join crm_mst_tsource h on b.source_gid=h.source_gid                " +
                    " left join crm_trn_tcampaign k on a.campaign_gid=k.campaign_gid          " +
                    " left join crm_trn_tcalllog f on f.call_response=f.call_response" +
                    " left join crm_trn_tschedulelog i on a.leadbank_gid = i.leadbank_gid " +
                    " left join crm_mst_tleadstage z on a.leadstage_gid=z.leadstage_gid" +
                     " where a.assign_to = '" + ("employee_gid") + "' " +
             " and g.status='Y' and g.main_contact='Y' " +
                    " order by b.leadbank_name asc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<all_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new all_list
                    {
                        campaign_title = dt["campaign_title"].ToString(),
                        internal_notes = dt["internal_notes"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        leadstage_name = dt["leadstage_name"].ToString(),
                    });

                    values.alllist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetleadbankSummary(string user_gid, MdlMyleads values)
        {
            msSQL = "select b.leadbankcontact_name, concat(b.address1,b.address2) As contact_details,c.region_name,d.source_name,concat(f.user_firstname,' ',f.user_lastname)As created_by," +
                    "date_format(a.created_date,'%d-%m-%Y')As created_date,a.leadbank_gid,a.lead_status,concat(f.user_firstname,' ',f.user_lastname)As assign_to " +
                    "from crm_trn_tleadbank a " +
                    "left join crm_trn_tleadbankcontact b on a.leadbank_gid=b.leadbank_gid  " +
                    "left join crm_mst_tregion c on a.leadbank_region=c.region_gid " +
                    "left join crm_mst_tsource d on a.source_gid=d.source_gid  " +
                    "left join hrm_mst_temployee e on a.created_by=e.employee_gid " +
                    "left join adm_mst_tuser f on  f.user_gid = e.user_gid " +
                    "left join crm_trn_tlead2campaign g on a.leadbank_gid = g.leadbank_gid " +
                    "left join hrm_mst_temployee h on h.employee_gid=g.assign_to";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadbank_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        source_name = dt["source_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        lead_status = dt["lead_status"].ToString(),
                        assign_to = dt["assign_to"].ToString(),
                    });
                    values.leadbank_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetleadbankeditSummary(string leadbank_gid, MdlMyleads values)
        {
            msSQL = "select a.referred_by, a.remarks, a.leadbank_name, b.leadbankcontact_name, " +
                     " b.designation, b.mobile, b.email, a.company_website, b.fax_area_code , b.fax_country_code,  " +
                     " b.country_code1, b.area_code1, b.phone1, b.country_code2, b.area_code2, b.phone2, a.leadbank_address1,  " +
                     " a.leadbank_address2, a.leadbank_city, a.leadbank_state, a.leadbank_pin, a.approval_flag, d.country_name, " +
                     " c.source_name, b.region_name, e.categoryindustry_name from crm_trn_tleadbank a " +
                     " left join crm_trn_tleadbankcontact b on a.leadbank_gid = b.leadbank_gid " +
                     " left join crm_mst_tsource c on c.source_gid = a.source_gid " +
                     "left join adm_mst_tcountry d on d.country_gid = b.country_gid " +
                     "left join crm_mst_tcategoryindustry e on e.categoryindustry_gid = a.categoryindustry_gid " +
                     "where a.leadbank_gid ='" + leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbankedit_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    {
                        getModuleList.Add(new leadbankedit_list
                        {
                            referred_by = dt["referred_by"].ToString(),
                            remarks = dt["remarks"].ToString(),
                            leadbank_name = dt["leadbank_name"].ToString(),
                            leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                            designation = dt["designation"].ToString(),
                            mobile = dt["mobile"].ToString(),
                            email = dt["email"].ToString(),
                            company_website = dt["company_website"].ToString(),
                            fax_area_code = dt["fax_area_code"].ToString(),
                            fax_country_code = dt["fax_country_code"].ToString(),
                            country_code1 = dt["country_code1"].ToString(),
                            area_code1 = dt["area_code1"].ToString(),
                            phone1 = dt["phone1"].ToString(),
                            country_code2 = dt["country_code2"].ToString(),
                            area_code2 = dt["area_code2"].ToString(),
                            phone2 = dt["phone2"].ToString(),
                            leadbank_address1 = dt["leadbank_address1"].ToString(),
                            leadbank_address2 = dt["leadbank_address2"].ToString(),
                            leadbank_city = dt["leadbank_city"].ToString(),
                            leadbank_state = dt["leadbank_state"].ToString(),
                            leadbank_pin = dt["leadbank_pin"].ToString(),
                            approval_flag = dt["approval_flag"].ToString(),
                            country_name = dt["country_name"].ToString(),
                            source_name = dt["source_name"].ToString(),
                            region_name = dt["region_name"].ToString(),
                            categoryindustry_name = dt["categoryindustry_name"].ToString(),
                        });
                        values.leadbankedit_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
        }

        public void DaGetSourcetypedropdown(string user_gid, MdlMyleads values)
        {
            msSQL = "Select source_gid,concat(source_code,'-',source_name) as source_name from crm_mst_tsource";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Source_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Source_list
                    {
                        source_gid = dt["source_gid"].ToString(),
                        source_name = dt["source_name"].ToString(),
                    });
                    values.Source_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetregiondropdown(string user_gid, MdlMyleads values)
        {
            msSQL = "SELECT region_gid, region_name FROM crm_mst_tregion Order by region_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<regionname_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new regionname_list
                    {
                        region_gid = dt["region_gid"].ToString(),
                        region_name = dt["region_name"].ToString(),

                    });
                    values.regionname_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetindustrydropdown(string user_gid, MdlMyleads values)
        {
            msSQL = "select categoryindustry_gid,categoryindustry_name  from crm_mst_tcategoryindustry";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<industryname_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new industryname_list
                    {
                        categoryindustry_gid = dt["categoryindustry_gid"].ToString(),
                        categoryindustry_name = dt["categoryindustry_name"].ToString(),

                    });
                    values.industryname_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetcompanylistdropdown(string user_gid, MdlMyleads values)
        {
            msSQL = "Select distinct leadbank_gid, leadbank_name from crm_trn_tleadbank";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<company_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new company_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                    });
                    values.company_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetcountrynamedropdown(string user_gid, MdlMyleads values)
        {
            msSQL = "Select country_gid,country_name from adm_mst_tcountry";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<country_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new country_list
                    {
                        country_gid = dt["country_gid"].ToString(),
                        country_name = dt["country_name"].ToString(),
                    });
                    values.country_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetcountrydropdown(string user_gid, MdlMyleads values)
        {
            msSQL = "Select country_gid,country_name from adm_mst_tcountry";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<country_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new country_list
                    {
                        country_gid = dt["country_gid"].ToString(),
                        country_name = dt["country_name"].ToString(),
                    });
                    values.country_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetcurrencydropdown(string user_gid, MdlMyleads values)
        {
            msSQL = "Select currencyexchange_gid,currency_code from crm_trn_tcurrencyexchange";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<currency_codelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new currency_codelist
                    {
                        currencyexchange_gid = dt["currencyexchange_gid"].ToString(),
                        currency_code = dt["currency_code"].ToString(),
                    });
                    values.currencycodelist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostleadbank(string user_gid, leadbank_list values)
        {
            msSQL = " Select source_name  from crm_mst_tsource where source_gid = '" + values.source_name + "'";
            string lssource_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select leadbank_name  from crm_trn_tleadbank where  leadbank_gid = '" + values.leadbank_name + "'";
            string lsleadbank_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select  categoryindustry_name from crm_mst_tcategoryindustry where categoryindustry_gid = '" + values.categoryindustry_name + "'";
            string lscategoryindustry_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select country_name  from adm_mst_tcountry where country_gid = '" + values.country_name + "'";
            string lscountry_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select  region_name from crm_mst_tregion where  region_gid = '" + values.region_name + "'";
            string lsregion_name = objdbconn.GetExecuteScalar(msSQL);



            msGetGid = objcmnfunctions.GetMasterGID("BMCC");

            msGetGid1 = objcmnfunctions.GetMasterGID("BLBP");

            msSQL = " INSERT INTO crm_trn_tleadbank(" +
                    " leadbank_gid," +
                    " source_gid," +
                    " leadbank_id," +
                    " leadbank_name," +
                    " status," +
                    " company_website," +
                    " approval_flag, " +
                    " lead_status," +
                    " leadbank_code," +
                    " leadbank_state," +
                    " leadbank_address1," +
                    " leadbank_address2," +
                    " leadbank_region," +
                    " leadbank_country," +
                    " leadbank_pin," +
                    " created_by," +
                    " remarks," +
                    " categoryindustry_gid," +
                    " referred_by," +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid1 + "'," +
                    " '" + values.source_name + "'," +
                    " '" + msGetGid + "'," +
                    " '" + lsleadbank_name + "'," +
                    " '  y  '," +
                    " '" + values.company_website + "'," +
                    " '  Approved  '," +
                    " '  Not Assigned  '," +
                    " '  H.Q  '," +
                    " '" + values.leadbank_state + "'," +
                    " '" + values.leadbank_address1 + "'," +
                    " '" + values.leadbank_address2 + "'," +
                    " '" + values.region_name + "'," +
                    " '" + values.country_name + "'," +
                    " '" + values.leadbank_pin + "'," +
                    " '" + user_gid + "'," +
                    " '" + values.remarks + "'," +
                    " '" + lscategoryindustry_name + "'," +
                    " '" + values.referred_by + "'," +
                    " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 0)
            {
                values.Status = false;
                values.message = "Error Occured While Inserting Records";
            }



            msGetGid2 = objcmnfunctions.GetMasterGID("BLBP");
            if (msGetGid2 == "E")
            {
                values.Status = false;
                values.message = "Create sequence code BLCC for lead bank";
            }

            msSQL = " INSERT INTO crm_trn_tleadbankcontact" +
                " (leadbankcontact_gid," +
                " leadbank_gid," +
                " leadbankcontact_name," +
                " email, mobile," +
                " phone1," +
                " country_code1," +
                " area_code1," +
                " phone2," +
                " country_code2," +
                " area_code2," +
                " fax," +
                " fax_country_code," +
                " fax_area_code," +
                " designation," +
                " created_date," +
                " created_by," +
                " leadbankbranch_name, " +
                " address1, " +
                " address2, " +
                " city, " +
                " state, " +
                " pincode, " +
                " country_gid, " +
                " region_name, " +
                " main_contact)" +
                " values( " +
                " '" + msGetGid2 + "'," +
                " '" + msGetGid1 + "'," +
                " '" + values.leadbankcontact_name + "'," +
                " '" + values.email + "'," +
                " '" + values.mobile + "'," +
                " '" + values.phone1 + "'," +
                " '" + values.country_code1 + "'," +
                " '" + values.area_code1 + "'," +
                " '" + values.phone2 + "'," +
                " '" + values.country_code2 + "'," +
                " '" + values.area_code2 + "'," +
                " '" + values.fax + "'," +
                " '" + values.fax_country_code + "'," +
                " '" + values.fax_area_code + "'," +
                " '" + values.designation + "'," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                " '" + user_gid + "'," +
                " '  H.Q  '," +
                " '" + values.leadbank_address1 + "'," +
                " '" + values.leadbank_address2 + "'," +
                " '" + values.leadbank_city + "'," +
                " '" + values.leadbank_state + "'," +
                " '" + values.leadbank_pin + "'," +
                " '" + lscountry_name + "'," +
                " '" + lsregion_name + "'," +
                " '  y  " + "')";
            mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "Select * from crm_mst_tcustomer where customer_name ='" + lsleadbank_name + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {

                msGetGid3 = objcmnfunctions.GetMasterGID("BCRM");
                if (msGetGid3 == "E")
                {
                    values.Status = false;
                    values.message = "Create sequence code BCRM for lead bank";
                }

                msSQL = " INSERT INTO crm_mst_tcustomer" +
                           " (customer_gid," +
                           " customer_id," +
                           " customer_name," +
                           " company_website," +
                           " customerbranch_code," +
                           " customer_address," +
                           " customer_address2," +
                           " customer_state," +
                           " customer_country," +
                           " customer_city," +
                           " customer_pin," +
                           " customer_region," +
                           " main_branch," +
                           " status," +
                           " created_by," +
                           " created_date)" +
                           " values( " +
                           " '" + msGetGid3 + "'," +
                           " '" + msGetGid + "'," +
                           " '" + lsleadbank_name + "'," +
                           " '" + values.company_website + "'," +
                           " '  H.Q  '," +
                           " '" + values.leadbank_address1 + "'," +
                           " '" + values.leadbank_address2 + "'," +
                           " '" + values.leadbank_state + "'," +
                           " '" + lscountry_name + "'," +
                           " '" + values.leadbank_city + "'," +
                           " '" + values.leadbank_pin + "'," +
                           " '" + values.leadbank_region + "'," +
                           " '  y  '," +
                           " '  Active  '," +
                           " '" + user_gid + "'," +
                           " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                mnResult3 = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult3 == 1)
                {
                    msSQL = " update crm_trn_tleadbank set" +
                   " customer_gid = '" + msGetGid3 + "'" +
                  " where leadbank_gid = '" + msGetGid1 + "'";
                    mnResult4 = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msGetGid4 = objcmnfunctions.GetMasterGID("BCCM");
                if (msGetGid4 == "E")
                {
                    values.Status = false;
                    values.message = "Create sequence code BCCM for lead bank";
                }

                msSQL = " INSERT INTO crm_mst_tcustomercontact" +
                           " (customercontact_gid," +
                           " customer_gid," +
                           " customercontact_name," +
                           " main_contact)" +
                           " values( " +
                           " '" + msGetGid4 + "'," +
                           " '" + lsleadbank_name + "'," +
                           " '" + msGetGid3 + "'," +
                          " '  y  '," + "')";

                mnResult5 = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult5 == 1)
                {
                    msSQL = " update crm_trn_tleadbankcontact set" +
                               " customercontact_gid = '" + msGetGid4 + "'" +
                               " where leadbankcontact_gid = '" + msGetGid2 + "'";
                    mnResult6 = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msGetGid5 = objcmnfunctions.GetMasterGID("FCOA");
                if (msGetGid5 == "E")
                {
                    values.Status = false;
                    values.message = "Create sequence code FCOA for lead bank";
                }

                msSQL = " insert into acc_mst_tchartofaccount (" +
                      " account_gid," +
                      " accountgroup_gid," +
                      " accountgroup_name," +
                      " account_code," +
                      " account_name," +
                      " has_child," +
                      " ledger_type," +
                      " display_type," +
                      " Created_Date, " +
                      " Created_By, " +
                      " gl_code) " +
                      " values (" +
                      " '" + msGetGid5 + "'," +
                      " '  FCOA000022  '," +
                      " ' Sundry Debtors '," +
                      " '" + msGetGid + "'," +
                      " '" + lsleadbank_name + "'," +
                      " ' N '," +
                      " ' N '," +
                      " ' Y '," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd") +
                      " '" + user_gid + "'," +
                      " '" + lsleadbank_name + "'," + "')";
                mnResult7 = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult7 == 1)
                {
                    msSQL = " update crm_mst_tcustomer set " +
                           " account_gid = '" + msGetGid7 + "'" +
                           " where customer_gid='" + msGetGid3 + "'";
                }
                mnResult8 = objdbconn.ExecuteNonQuerySQL(msSQL);

            }

            msSQL = " select module2employee_gid from adm_mst_tmodule2employee where hierarchy_level ='5' and module_gid = 'MKT' and employee_gid='" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                msSQL = " select a.campaign_gid,b.campaign_title from crm_trn_tcampaign2employee a " +
                 " left join crm_trn_tcampaign b on a.campaign_gid=b.campaign_gid " +
                 " where a.employee_gid='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                string lscampaign_gid = objdbconn.GetExecuteScalar(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    msGetGid6 = objcmnfunctions.GetMasterGID("BLCC");
                    if (msGetGid6 == "E")
                    {
                        values.Status = false;
                        values.message = "Create sequence code BLCC for lead bank";
                    }
                    msSQL = " Insert into crm_trn_tlead2campaign ( " +
                               " lead2campaign_gid, " +
                               " leadbank_gid, " +
                               " campaign_gid, " +
                               " created_by, " +
                               " created_date, " +
                               " lead_status, " +
                               " internal_notes, " +
                               " leadstage_gid, " +
                               " assign_to ) " +
                               " Values ( " +
                               " '" + msGetGid6 + "'," +
                               " '" + msGetGid1 + "'," +
                               " '" + lscampaign_gid + "'," +
                               " '" + user_gid + "'," +
                               " '" + DateTime.Now.ToString("yyyy-MM-dd") +
                               " ' Open '," +
                               " '" + values.remarks + "'," +
                               " ' 1 '," +
                               " '" + user_gid + "'," + "')";
                    mnResult9 = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult9 == 1)
                    {
                        msSQL = " update crm_trn_tleadbank Set " +
                           " lead_status = 'Assigned' " +
                           " where leadbank_gid = '" + msGetGid1 + "' ";
                    }
                }


            }
        }

        public void DaUpdatedleadbank(string user_gid, leadbank_list values)
        {
            msSQL = " Select source_name  from crm_mst_tsource where source_gid = '" + values.source_name + "'";
            string lssource_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select leadbank_name  from crm_trn_tleadbank where  leadbank_gid = '" + values.leadbank_name + "'";
            string lsleadbank_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select  categoryindustry_name from crm_mst_tcategoryindustry where categoryindustry_gid = '" + values.categoryindustry_name + "'";
            string lscategoryindustry_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select country_name  from adm_mst_tcountry where country_gid = '" + values.country_name + "'";
            string lscountry_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select  region_name from crm_mst_tregion where  region_gid = '" + values.region_name + "'";
            string lsregion_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select  leadbankcontact_gid from crm_trn_tleadbankcontact where  leadbankcontact_name = '" + values.leadbankcontact_name + "'";
            string lsbankcontact = objdbconn.GetExecuteScalar(msSQL);




            msSQL = " Update crm_trn_tleadbank set" +
                " source_gid = '" + values.source_name + "'," +
                " categoryindustry_gid = '" + values.categoryindustry_name + "'," +
                " leadbank_name = '" + lsleadbank_name + "'," +
                " status = '" + 'Y' + "'," +
                " company_website= '" + values.company_website + "'," +
                " leadbank_address1 = '" + values.leadbank_address1 + "'," +
                " leadbank_address2 = '" + values.leadbank_address2 + "'," +
                " leadbank_city = '" + values.leadbank_city + "'," +
                " leadbank_region = '" + values.region_name + "'," +
                " leadbank_state = '" + values.leadbank_state + "'," +
                " leadbank_country = '" + values.country_name + "'," +
                " leadbank_pin = '" + values.leadbank_pin + "'," +
                " updated_by = '" + user_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                " approval_flag = 'Approved'," +
                " referred_by= '" + values.referred_by + "'," +
                " remarks = '" + values.remarks + "'," +
                " where leadbank_gid = '" + values.leadbank_name + "'";

            mnResult10 = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " Update crm_trn_tleadbankcontact set" +
                " leadbankcontact_name = '" + values.leadbankcontact_name + "'," +
                " mobile = '" + values.mobile + "'," +
                " email = '" + values.email + "'," +
                " designation = '" + values.designation + "'," +
                " phone1 = '" + values.phone1 + "'," +
                " country_code1 ='" + values.country_code1 + "'," +
                " area_code1 = '" + values.area_code1 + "'," +
                " phone2 ='" + values.phone2 + "'," +
                " country_code2 = '" + values.country_code2 + "'," +
                " area_code2 = '" + values.area_code2 + "'," +
                " fax = '" + values.fax + "'," +
                " fax_country_code = '" + values.fax_country_code + "'," +
                " fax_area_code = '" + values.fax_area_code + "'," +
                " where leadbank_gid ='" + values.leadbank_gid + "'," +
                " leadbankcontact_gid = '" + lsbankcontact + "'";

            mnResult11 = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult11 != 0)
            {

                values.Status = true;
                values.message = "Product Updated Successfully";

            }
            else
            {
                values.Status = false;
                values.message = "Error While Updating Product";
            }
        }

        public void DaGetleadbankcontactSummary(string leadbank_gid, MdlMyleads values)

        {
            msSQL = " select a.leadbankbranch_name,concat (a.address1,a.address2) As Address,a.city,a.state, " +
                    " a.pincode,b.country_name from crm_trn_tleadbankcontact a " +
                    " left join adm_mst_tcountry b on a.country_gid=b.country_gid " +
                    " where a.leadbank_gid ='" + leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbankcontact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadbankcontact_list
                    {
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),
                        Address = dt["Address"].ToString(),
                        city = dt["city"].ToString(),
                        state = dt["state"].ToString(),
                        pincode = dt["pincode"].ToString(),
                        country_name = dt["country_name"].ToString(),
                    });
                    values.leadbankcontact_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetleadbankviewSummary(string leadbank_gid, MdlMyleads values)
        {
            msSQL = " select a.referred_by, a.remarks, a.leadbank_name, b.leadbankcontact_name, " +
                     " b.designation, b.mobile, b.email, a.company_website, b.fax_area_code , b.fax_country_code,  " +
                     " b.country_code1, b.area_code1, b.phone1,b.fax, b.country_code2, b.area_code2, b.phone2, a.leadbank_address1,  " +
                     " a.leadbank_address2, a.leadbank_city, a.leadbank_state, a.leadbank_pin, d.country_name, " +
                     " c.source_name, b.region_name, e.categoryindustry_name ,a.leadbank_code,b.leadbankbranch_name from crm_trn_tleadbank a " +
                     " left join crm_trn_tleadbankcontact b on a.leadbank_gid = b.leadbank_gid " +
                     " left join crm_mst_tsource c on c.source_gid = a.source_gid " +
                     " left join adm_mst_tcountry d on d.country_gid = b.country_gid " +
                     " left join crm_mst_tcategoryindustry e on e.categoryindustry_gid = a.categoryindustry_gid " +
                     " where a.leadbank_gid ='" + leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    {
                        getModuleList.Add(new leadbank_list
                        {
                            referred_by = dt["referred_by"].ToString(),
                            remarks = dt["remarks"].ToString(),
                            leadbank_name = dt["leadbank_name"].ToString(),
                            leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                            designation = dt["designation"].ToString(),

                            mobile = dt["mobile"].ToString(),
                            email = dt["email"].ToString(),
                            company_website = dt["company_website"].ToString(),
                            fax_area_code = dt["fax_area_code"].ToString(),
                            fax_country_code = dt["fax_country_code"].ToString(),
                            fax = dt["fax"].ToString(),

                            country_code1 = dt["country_code1"].ToString(),
                            area_code1 = dt["area_code1"].ToString(),
                            phone1 = dt["phone1"].ToString(),
                            country_code2 = dt["country_code2"].ToString(),
                            area_code2 = dt["area_code2"].ToString(),

                            phone2 = dt["phone2"].ToString(),
                            leadbank_address1 = dt["leadbank_address1"].ToString(),
                            leadbank_address2 = dt["leadbank_address2"].ToString(),
                            leadbank_city = dt["leadbank_city"].ToString(),
                            leadbank_state = dt["leadbank_state"].ToString(),

                            leadbank_pin = dt["leadbank_pin"].ToString(),
                            country_name = dt["country_name"].ToString(),
                            source_name = dt["source_name"].ToString(),
                            region_name = dt["region_name"].ToString(),

                            categoryindustry_name = dt["categoryindustry_name"].ToString(),
                            leadbank_code = dt["leadbank_code"].ToString(),
                            leadbankbranch_name = dt["leadbankbranch_name"].ToString(),
                        });
                        values.leadbank_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }


        }

        public void DaGetleadbankbranchSummary(string leadbank_gid, MdlMyleads values)
        {
            msSQL = "select a.leadbank_name,b.leadbankbranch_name,b.leadbankcontact_name,b.designation,b.mobile," +
                    "concat(b.address1, b.address2) as Address,b.city,b.state,c.country_name," +
                    "b.pincode from  crm_trn_tleadbankcontact b" +
                    "left join crm_trn_tleadbank a on a.leadbank_gid = b.leadbank_gid" +
                    "left join adm_mst_tcountry c on b.country_gid = c.country_gid" +
                    " where a.leadbank_gid ='" + leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbankbranch_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadbankbranch_list
                    {
                        leadbank_name = dt["leadbank_name"].ToString(),
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),
                        leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                        designation = dt["designation"].ToString(),
                        mobile = dt["mobile"].ToString(),
                        Address = dt["Address"].ToString(),
                        city = dt["city"].ToString(),
                        state = dt["state"].ToString(),
                        country_name = dt["country_name"].ToString(),
                        pincode = dt["pincode"].ToString(),

                    });
                    values.leadbankbranch_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostaddcustomer(string user_gid, customeradd_list values)
        {
           // msSQL = "select customer_gid from crm_trn_tleadbank where leadbank_gid = '" + values.leadbank_gid + "'";
           //string lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);
            msGetGid8 = objcmnfunctions.GetMasterGID("BCRM");
            if (msGetGid8 == "E")
            {
                values.status = false;
                values.message = "Create sequence code BCRM for lead bank";
            }


            msSQL = " INSERT INTO crm_mst_tcustomer" +
                " (customer_gid," +
                " customer_id," +
                " customer_name," +
                " company_website," +
                " customer_code," +
                " customer_address," +
                " customer_address2," +
                " customer_state," +
                " customer_country," +
                " customer_city," +

                " customer_pin," +
                " customer_region," +
                " status," +
                "  created_date," +
                " created_by)" +
                " values (" +
                " '" + msGetGid8 + "'," +
                " '" + msGetGid8 + "'," +
                " '" + values.customername + "'," +


                " '" + values.company_website + "'," +

                " '" + values.customer_code + "'," +
                " '" + values.address_line1 + "'," +
                " '" + values.address_line2 + "'," +
                " '" + values.state + " ', " +
                " '" + values.country_name + "'," +
                   " '" + values.city + "'," +

                " '" + values.pin + "'," +
                " '" + values.region_name + "'," +
                " '  Active  '," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                    " '" + user_gid + "')";


            mnResult12 = objdbconn.ExecuteNonQuerySQL(msSQL);
            //if (mnResult12 == 1)
            //{
            //    msSQL = " update crm_trn_tleadbank set " +
            //           " customer_gid = '" + msGetGid8 + "'" +
            //           " where leadbank_gid='" + values.leadbank_gid + "'";
            //}
            //mnResult13 = objdbconn.ExecuteNonQuerySQL(msSQL);
            msGetGid9 = objcmnfunctions.GetMasterGID("BCCM");


            msSQL = " INSERT INTO crm_mst_tcustomercontact" +
                         " (customercontact_gid," +
                         " customer_gid," +
                         " customerbranch_name, " +
                         " customercontact_name," +
                         " email, mobile," +
                         " designation," +
                         " created_date," +
                         " created_by," +
                         " address1, " +
                         " address2, " +
                         " state, " +
                         " country_gid, " +
                         " city, " +
                         " region, " +
                         " zip_code, " +
                         " main_contact," +
                         " phone," +
                         " area_code," +
                         " country_code," +
                         " fax," +
                         " fax_area_code," +
                         " fax_country_code)" +
                         " values( " +
                     " '" + msGetGid9 + "'," +
                     " '" + msGetGid8 + "'," +
                           " '  H.Q  '," +
                     " '" + values.contactpersonname + "'," +
                     " '" + values.email + "'," +
                     " '" + values.mobile + "'," +
                     " '" + values.designation + "'," +
                     " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                     " '" + user_gid + "'," +
                     " '" + values.address_line1 + "'," +
                     " '" + values.address_line2 + "'," +
                     " '" + values.state + "'," +
                     " '" + values.country_gid + "'," +
                     " '" + values.city + "'," +
                     " '" + values.region_name + "'," +
                     " '" + values.pin + "'," +
                     " '  y  '," +
                    " '" + values.main_contact + "'," +
                     " '" + values.mobile + "'," +
                     " '" + values.areacode + "'," +
                     " '" + values.countrycode + "'," +
                     " '" + values.fax + "'," +
                                 " '" + values.fax_area_code + "'," +
                     " '" + values.fax_country_code + "')";

            mnResult14 = objdbconn.ExecuteNonQuerySQL(msSQL);





    }

    public void DaGetbreadcrumb(string user_gid, string module_gid, MdlMyleads values)
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
