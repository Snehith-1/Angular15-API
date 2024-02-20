using ems.crm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.NetworkInformation;

namespace ems.crm.DataAccess
{
    public class DaLeadbank
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string lssource_name, lsleadbank_name, lscategoryindustry_name, lscountry_name,
            lsregion_name, lsbankcontact, msGetGid, msGetGid1, msGetGid2, msGetGid3, msGetGid4,
            msGetGid5, msGetGid6, msGetGid7, msGetGid8, msGetGid9, msGetGid10, msGetGid11;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5,
            mnResult6, mnResult7, mnResult8, mnResult9, mnResult10, mnResult11,
            mnResult12, mnResult13, mnResult14, mnResult15, mnResult16, mnResult17, mnResult18, mnResult19;

        public void DaGetleadbankSummary(string user_gid, MdlLeadbank values)
        {
            msSQL = "select b.leadbankcontact_name,b.leadbankcontact_gid, concat(b.address1,b.address2) As contact_details,concat(c.region_name,' / ',a.leadbank_city,' / ',a.leadbank_state) as region_name,concat(f.user_firstname,' ',f.user_lastname)As created_by," +
                    "date_format(a.created_date,'%d-%m-%Y')As created_date,a.leadbank_gid,a.lead_status,concat(f.user_firstname,' ',f.user_lastname)As assign_to , " +
                    "concat(case when d.source_name is null then '' else d.source_name end,' / ',case when l.categoryindustry_name is null then '' else l.categoryindustry_name end ) as source_name from crm_trn_tleadbank a " +
                    "left join crm_trn_tleadbankcontact b on a.leadbank_gid=b.leadbank_gid  " +
                    "left join crm_mst_tregion c on a.leadbank_region=c.region_gid " +
                    "left join crm_mst_tsource d on a.source_gid=d.source_gid  " +
                    "left join hrm_mst_temployee e on a.created_by=e.employee_gid " +
                    "left join adm_mst_tuser f on  f.user_gid =  a.created_by " +
                    "left join crm_trn_tlead2campaign g on a.leadbank_gid = g.leadbank_gid " +
                    "left join hrm_mst_temployee h on h.employee_gid=g.assign_to "+
                    "left join crm_mst_tcategoryindustry l on a.categoryindustry_gid = l.categoryindustry_gid  group by a.leadbank_gid Order by date(a.created_date) desc,a.created_date asc,a.leadbank_gid desc ";
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

        public void DaGetSourcetypedropdown(string user_gid, MdlLeadbank values)
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

        public void DaGetindustrydropdown(string user_gid, MdlLeadbank values)
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


        public void DaGetregiondropdown(string user_gid, MdlLeadbank values)
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


        public void DaGetcompanylistdropdown(string user_gid, MdlLeadbank values)
        {
            msSQL = "SELECT leadbank_gid,leadbank_name FROM crm_trn_tleadbank where leadbank_name!='' group by leadbank_name ";
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
        public void DaGetcountrynamedropdown(string user_gid, MdlLeadbank values)
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
                    " '" + values.categoryindustry_name + "'," +
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

        public void DaUpdateleadbank(string user_gid, leadbank_list values)
        {
            msSQL = " Select source_gid  from crm_mst_tsource where source_name = '" + values.source_name + "'";
            string lssource_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select leadbank_gid  from crm_trn_tleadbank where  leadbank_name = '" + values.leadbank_name + "'";
            string lsleadbank_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select  categoryindustry_name from crm_mst_tcategoryindustry where categoryindustry_gid = '" + values.categoryindustry_name + "'";
            string lscategoryindustry_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select country_gid  from adm_mst_tcountry where country_name = '" + values.country_name + "'";
            string lscountry_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select  region_name from crm_mst_tregion where  region_gid = '" + values.region_name + "'";
            string lsregion_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select  leadbankcontact_gid from crm_trn_tleadbankcontact where  leadbankcontact_name = '" + values.leadbankcontact_name + "'";
            string lsbankcontact = objdbconn.GetExecuteScalar(msSQL);




            msSQL = " Update crm_trn_tleadbank set" +
                " source_gid = '" + values.source_name + "'," +
                " categoryindustry_gid = '" + values.categoryindustry_name + "'," +
                " leadbank_name = '" + values.leadbank_name + "'," +
                " status = '" + 'Y' + "'," +
                " company_website= '" + values.company_website + "'," +
                " leadbank_address1 = '" + values.leadbank_address1 + "'," +
                " leadbank_address2 = '" + values.leadbank_address2 + "'," +
                " leadbank_city = '" + values.leadbank_city + "'," +
                " leadbank_region = '" + values.region_name + "'," +
                " leadbank_state = '" + values.leadbank_state + "'," +
                " leadbank_country = '" + lscountry_gid + "'," +
                " leadbank_pin = '" + values.leadbank_pin + "'," +
                " updated_by = '" + user_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                " approval_flag = 'Approved'," +
                " referred_by= '" + values.referred_by + "'," +
                " remarks = '" + values.remarks + "'" +
                " where leadbank_gid = '" + lsleadbank_name + "'";

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
                " fax_area_code = '" + values.fax_area_code + "'" +
                " where leadbank_gid ='" + lsleadbank_name + "'" +
                " and leadbankcontact_gid = '" + values.leadbankcontact_gid + "'"; 

            mnResult11 = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult11 != 0)
            {

                values.Status = true;
                values.message = "LeadBank Updated Successfully";

            }
            else
            {
                values.Status = false;
                values.message = "Error While Updating LeadBank";
            }



        }
        public void DaGetleadbankeditSummary(string leadbank_gid, MdlLeadbank values)
        {
            msSQL = "select a.referred_by, a.remarks, a.leadbank_name, b.leadbankcontact_name,a.source_gid,b.country_gid, a.categoryindustry_gid, " +
                     " b.designation, b.mobile, b.email, a.company_website, b.fax_area_code , b.fax_country_code, f.region_gid, " +
                     " b.country_code1, b.area_code1, b.phone1, b.country_code2, b.area_code2, b.phone2, a.leadbank_address1,  " +
                     " a.leadbank_address2, a.leadbank_city, a.leadbank_state, a.leadbank_pin, a.approval_flag, d.country_name, " +
                     " c.source_name, b.region_name, e.categoryindustry_name from crm_trn_tleadbank a " +
                     " left join crm_trn_tleadbankcontact b on a.leadbank_gid = b.leadbank_gid " +
                     " left join crm_mst_tsource c on c.source_gid = a.source_gid " +
                     "left join adm_mst_tcountry d on d.country_gid = b.country_gid " +
                     "left join crm_mst_tregion f on b.region_name = f.region_name " +
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
                            source_gid = dt["source_gid"].ToString(),
                            country_gid = dt["country_gid"].ToString(),
                            region_gid = dt["region_gid"].ToString(),
                            categoryindustry_gid = dt["categoryindustry_gid"].ToString(),
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
        public void DaGetleadbankviewSummary(string leadbank_gid, MdlLeadbank values)
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
        public void DaGetleadbankbranchSummary(string leadbank_gid, MdlLeadbank values)
        {
            msSQL = "select a.leadbank_name,b.leadbankbranch_name,b.mobile,b.email," +
                    "b.designation from  crm_trn_tleadbankcontact b " +
                    "left join crm_trn_tleadbank a on a.leadbank_gid=b.leadbank_gid" +
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
                        mobile = dt["mobile"].ToString(),
                        email = dt["email"].ToString(),
                        designation = dt["designation"].ToString(),

                    });
                    values.leadbankbranch_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetleadbankcontactSummary(string leadbank_gid, MdlLeadbank values)
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

        public void DaGetleadbankbranchaddSummary(string leadbank_gid, MdlLeadbank values)
        {
            msSQL = " select a.leadbank_gid,b.leadbankcontact_gid,a.leadbank_name, b.leadbankbranch_name as branch_name, b.leadbankcontact_name as contact_name, " +
                    " b.designation as desig, b.mobile as mobileno," +
                    " concat(b.address1, b.address2) as Address,b.city as City,b.state as State,c.country_name  " +
                    " as Country,b.pincode as pin from  crm_trn_tleadbankcontact b " +
                    " left join crm_trn_tleadbank a on a.leadbank_gid = b.leadbank_gid" +
                    " left join adm_mst_tcountry c on b.country_gid = c.country_gid " +
                    " where a.leadbank_gid ='" + leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadbank_list
                    {

                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        leadbankcontact_gid = dt["leadbankcontact_gid"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        contact_name = dt["contact_name"].ToString(),
                        desig = dt["desig"].ToString(),
                        mobileno = dt["mobileno"].ToString(),
                        Address = dt["Address"].ToString(),
                        City = dt["City"].ToString(),
                        State = dt["State"].ToString(),
                        Country = dt["Country"].ToString(),
                        pin = dt["pin"].ToString(),
                    });
                    values.leadbank_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }



        public void DaGetleadbankbrancheditSummary(string leadbankcontact_gid, MdlLeadbank values)
        {
            msSQL = " select  b.leadbankbranch_name, b.leadbankcontact_name, b.designation, b.mobile,b.email," +
                    " b.address1, b.address2,b.city,b.state,b.region_name,c.country_name,b.pincode from  crm_trn_tleadbankcontact b " +
                    " left join adm_mst_tcountry c on b.country_gid = c.country_gid " +
                    " where b.leadbankcontact_gid ='" + leadbankcontact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadbank_list
                    {
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),
                        leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                        designation = dt["designation"].ToString(),
                        email = dt["email"].ToString(),
                        mobile = dt["mobile"].ToString(),
                        address1 = dt["address1"].ToString(),
                        address2 = dt["address2"].ToString(),
                        city = dt["city"].ToString(),
                        state = dt["state"].ToString(),
                        country_name = dt["country_name"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        pincode = dt["pincode"].ToString(),
                    });
                    values.leadbank_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaPostleadbankbranchadd(string user_gid, leadbank_list values)
        {

            msSQL = " Select country_name  from adm_mst_tcountry where country_gid = '" + values.country_name + "'";
            string lscountry_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select  region_name from crm_mst_tregion where  region_gid = '" + values.region_name + "'";
            string lsregion_name = objdbconn.GetExecuteScalar(msSQL);
            msGetGid8 = objcmnfunctions.GetMasterGID("BLCC");
            if (msGetGid6 == "E")
            {
                values.Status = false;
                values.message = "Create sequence code BLCC for lead bank";
            }
            msSQL = " insert into crm_trn_tleadbankcontact" +
                    "(leadbankcontact_gid," +
                    " leadbank_gid, " +
                    " leadbankbranch_name," +
                    " leadbankcontact_name," +
                    " designation," +
                    " mobile," +
                    " email," +
                    " address1," +
                    " address2," +
                    " city," +
                    " state," +
                    " country," +
                    " pincode," +
                    " region_name ," +
                    " country_gid," +
                    " created_by, " +
                    " created_date)" +
                    " values ( " +
                    " '" + msGetGid8 + "'," +
                    " '" + values.leadbank_gid + "'," +
                    " '" + values.leadbankbranch_name + "'," +
                    " '" + values.leadbankcontact_name + "'," +
                    " '" + values.designation + "'," +
                    " '" + values.mobile + "'," +
                    " '" + values.email + "'," +
                    " '" + values.address1 + "'," +
                    " '" + values.address2 + "'," +
                    " '" + values.city + "'," +
                    " '" + values.state + "'," +
                    " '" + lscountry_name + "'," +
                    " '" + values.pincode + "'," +
                    " '" + lsregion_name + "'," +
                    " '" + values.region_name + "'," +
                    " '" + user_gid + "'," +
                    " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult11 = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult11 != 0)
            {

                values.Status = true;
                values.message = "LeadBank Branch Updated Successfully";

            }
            else
            {
                values.Status = false;
                values.message = "Error While Updating LeadBank Branch";
            }


        }

        public void DaUpdateleadbankbranchedit(string user_gid, leadbank_list values)
        {

            msSQL = " Select country_gid  from adm_mst_tcountry where country_name = '" + values.country_name + "'";
            string lscountry_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select  region_name from crm_mst_tregion where  region_gid = '" + values.region_name + "'";
            string lsregion_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " Select  leadbankcontact_gid from crm_trn_tleadbankcontact where  leadbankcontact_name = '" + values.leadbankcontact_name + "'";
            string lsbankcontact = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Update crm_trn_tleadbankcontact set" +
                 " leadbankbranch_name = '" + values.leadbankbranch_name + "'," +
                 " leadbankcontact_name = '" + values.leadbankcontact_name + "'," +
                 " designation = '" + values.designation + "'," +
                 " mobile = '" + values.mobile + "'," +
                 " email =  '" + values.email + "'," +
                 " address1 = '" + values.address1 + "'," +
                 " address2 = '" + values.address2 + "'," +
                 " city =  '" + values.city + "'," +
                 " state =  '" + values.state + "'," +
                 " country =   '" + values.country_name + "'," +
                 " country_gid = '" + lscountry_gid + "'," +
                 " region_name =  '" + values.region_name + "'," +
                 " pincode = '" + values.pincode + "'" +
                 " where leadbank_gid = '" + values.leadbank_gid + "'";

            mnResult13 = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult13 != 0)
            {

                values.Status = true;
                values.message = "leadbank Branch Updated Successfully";

            }
            else
            {
                values.Status = false;
                values.message = "Error While Updating Branch";
            }


        }


        public void DaGetbranchdropdown(string leadbank_gid, MdlLeadbank values)
        {
            if (string.IsNullOrEmpty(leadbank_gid))
            {
                msSQL = "SELECT leadbank_gid,leadbankbranch_name FROM crm_trn_tleadbankcontact  where leadbankbranch_name!='' GROUP BY leadbankbranch_name";
            }
            else
            {
                msSQL = " select leadbank_gid,leadbankbranch_name from crm_trn_tleadbankcontact" +
                  " where leadbank_gid ='" + leadbank_gid + "'";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbankcontact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadbankcontact_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),

                    });
                    values.leadbankcontact_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetbranchdropdown1(string leadbankcontact_gid, MdlLeadbank values)
        {
            if (string.IsNullOrEmpty(leadbankcontact_gid))
            {
                msSQL = "SELECT leadbank_gid,leadbankbranch_name FROM crm_trn_tleadbankcontact  where leadbankbranch_name!='' GROUP BY leadbankbranch_name";
            }
            else
            {
                msSQL = " select leadbank_gid,leadbankbranch_name from crm_trn_tleadbankcontact" +
                  " where leadbankcontact_gid ='" + leadbankcontact_gid + "'";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbankcontact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadbankcontact_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),

                    });
                    values.leadbankcontact_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetleadbankcontactaddSummary(string leadbank_gid, MdlLeadbank values)
        {
            msSQL = "select leadbank_gid,leadbankcontact_gid,leadbankbranch_name, leadbankcontact_name , mobile,email, designation, fax_area_code, fax_country_code, fax ," +
                    "country_code1,area_code1 , country_code2,area_code2 ," +
                    "phone2 from crm_trn_tleadbankcontact " +
                    "where leadbank_gid ='" + leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadbank_list
                    {

                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),
                        leadbankcontact_gid = dt["leadbankcontact_gid"].ToString(),
                        leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                        designation = dt["designation"].ToString(),
                        mobile = dt["mobile"].ToString(),
                        email = dt["email"].ToString(),
                        fax_area_code = dt["fax_area_code"].ToString(),
                        fax_country_code = dt["fax_country_code"].ToString(),
                        fax = dt["fax"].ToString(),
                        country_code1 = dt["country_code1"].ToString(),
                        area_code1 = dt["area_code1"].ToString(),
                        country_code2 = dt["country_code2"].ToString(),
                        area_code2 = dt["area_code2"].ToString(),
                        phone2 = dt["phone2"].ToString(),

                    });
                    values.leadbank_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetleadbankcontacteditsSummary(string leadbankcontact_gid, MdlLeadbank values)
        {
            msSQL = "select leadbank_gid,leadbankcontact_gid,leadbankbranch_name, leadbankcontact_name , mobile,email, designation, fax_area_code, fax_country_code, fax ," +
                    "country_code1,area_code1 , country_code2,area_code2 ," +
                    "phone2 from crm_trn_tleadbankcontact " +
                    "where leadbankcontact_gid ='" + leadbankcontact_gid + "'";
           
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadbank_list
                    {

                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbankcontact_gid = dt["leadbankcontact_gid"].ToString(),
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),
                        leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                        designation = dt["designation"].ToString(),
                        mobile = dt["mobile"].ToString(),
                        email = dt["email"].ToString(),
                        fax_area_code = dt["fax_area_code"].ToString(),
                        fax_country_code = dt["fax_country_code"].ToString(),
                        fax = dt["fax"].ToString(),
                        country_code1 = dt["country_code1"].ToString(),
                        area_code1 = dt["area_code1"].ToString(),
                        country_code2 = dt["country_code2"].ToString(),
                        area_code2 = dt["area_code2"].ToString(),
                        phone2 = dt["phone2"].ToString(),

                    });
                    values.leadbank_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }







        public void DaPostleadbankcontactadd(string user_gid, leadbank_list values)
        {
            msGetGid9 = objcmnfunctions.GetMasterGID("BLCC");
            if (msGetGid9 == "E")
            {
                values.Status = false;
                values.message = "Create sequence code BLCC for lead bank";
            }

            msSQL = " INSERT INTO crm_trn_tleadbankcontact" +
                " (leadbankcontact_gid," +
                " leadbank_gid," +
                " leadbankcontact_name," +
                " email," +
                " mobile," +
                " designation," +
                " phone1," +
                " country_code1," +
                " area_code1," +
                " phone2," +
                " country_code2," +
                " area_code2," +
                " fax," +
                " fax_country_code," +
                " fax_area_code," +
                " created_by," +
                " created_date)" +
                " values( " +
                " '" + msGetGid9 + "'," +
                " '" + values.leadbank_gid + "'," +
                " '" + values.leadbankcontact_name + "'," +
                " '" + values.email + "'," +
                " '" + values.designation + "'," +
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
                " '" + user_gid + "'," +
                " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

            mnResult12 = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult12 != 0)
            {

                values.Status = true;
                values.message = "LeadBank Branch Updated Successfully";

            }
            else
            {
                values.Status = false;
                values.message = "Error While Updating LeadBank Branch";
            }


        }


        public void DaUpdateleadbankContactedit(string user_gid, leadbank_list values)
        {


            msSQL = " Update crm_trn_tleadbankcontact set" +
                    " leadbankbranch_name = '" + values.leadbankbranch_name + "'," +
                    " leadbankcontact_name = '" + values.leadbankcontact_name + "'," +
                    " designation = '" + values.designation + "'," +
                    " mobile = '" + values.mobile + "'," +
                    " email =  '" + values.email + "'," +
                    " phone1 = '" + values.phone1 + "'," +
                    " country_code1 = '" + values.country_code1 + "'," +
                    " area_code1 =  '" + values.area_code1 + "'," +
                    " phone2 =  '" + values.phone2 + "'," +
                    " country_code2 =   '" + values.country_code2 + "'," +
                    " area_code2 = '" + values.area_code2 + "'," +
                    " fax =  '" + values.fax + "'," +
                    " fax_country_code = '" + values.fax_country_code + "'," +
                    " fax_area_code = '" + values.fax_area_code + "'" +
                    " where leadbank_gid = '" + values.leadbank_gid + "'";

            mnResult14 = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult14 != 0)
            {

                values.Status = true;
                values.message = "leadbank Contact Updated Successfully";

            }
            else
            {
                values.Status = false;
                values.message = "Error While Updating Contact";
            }


        }

        public void DaGetleadbankcontacteditSummary(string leadbank_gid, MdlLeadbank values)
        {
            msSQL = "select leadbank_gid,leadbankbranch_name, leadbankcontact_name , " +
                    "mobile,email , designation " +
                    "from crm_trn_tleadbankcontact " +
                    "where leadbank_gid ='" + leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadbank_list
                    {

                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),
                        leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                        designation = dt["designation"].ToString(),
                        email = dt["email"].ToString(),
                        mobile = dt["mobile"].ToString(),


                    });
                    values.leadbank_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostleadbankApproval(string user_gid, leadbank_list values)
        {

            msSQL = " Select country_gid  from adm_mst_tcountry where country_name = '" + values.country_name + "'";
            string lscountry_gid = objdbconn.GetExecuteScalar(msSQL);
           

            msSQL = "Select * from crm_trn_tleadbank " +
               "where leadbank_gid ='" + values.leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count == 0)
            {
                msGetGid10 = objcmnfunctions.GetMasterGID("BCRM");
                msSQL = " INSERT INTO crm_mst_tcustomer " +
                    " (customer_gid, " +
                    " customer_id, " +
                    " customer_name, " +
                    " company_website, " +
                    " customer_code," +
                    " customer_address," +
                    " customer_address2," +
                    " customer_country," +
                    " customer_region," +
                    " customer_city," +
                    " customer_state," +
                    " customer_pin," +
                    " main_branch," +
                    " status," +
                    " created_by, " +
                    " created_date, " +
                    " created_flag)" +
                    " values ( " +
                    " '" + msGetGid10 + "'," +
                    " '" + values.leadbank_gid + "'," +
                    " '" + values.leadbank_name + "'," +
                    " '" + values.company_website + "'," +
                    " '" + values.leadbank_code + "'," +
                    " '" + values.leadbank_address1 + "'," +
                    " '" + values.leadbank_address2 + "'," +
                    " '" + lscountry_gid + "'," +
                    " '" + values.region_name + "'," +
                    " '" + values.leadbank_city + "'," +
                    " '" + values.leadbank_state + "'," +
                    " '" + values.leadbank_pin + "'," +
                    " '  y  '," +
                    " ' '," +
                    " '" + user_gid + "'," +
                    " '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                    " '" + values.created_flag + "')";
                mnResult15 = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult15 == 1)
                {
                    msSQL = " update crm_trn_tleadbank set" +
                            " customer_gid = '" + msGetGid10 + "'" +
                            " where leadbank_gid = '" + values.leadbank_gid + "'";
                    mnResult16 = objdbconn.ExecuteNonQuerySQL(msSQL);
                };
            };

            msSQL = "Select * from  crm_trn_tleadbankcontact " +
           "where leadbank_gid ='" + values.leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                msGetGid11 = objcmnfunctions.GetMasterGID("BCCM");
                if (msGetGid4 == "E")
                {
                    values.Status = false;
                    values.message = "Create sequence code BCCM for lead bank";
                };
                msSQL = " INSERT INTO crm_mst_tcustomercontact " +
                            " (customercontact_gid," +
                            " customer_gid," +
                            " customercontact_name," +
                            " email," +
                            " mobile," +
                            " designation," +
                            " did_number," +
                            " created_date," +
                            " created_by," +
                            " address1, " +
                            " address2, " +
                            " state, " +
                            " country_gid, " +
                            " city, " +
                            " region, " +
                            " zip_code, " +
                            " customerbranch_name, " +
                            " main_contact)" +
                            " values( " +
                            " '" + msGetGid11 + "'," +
                            " '" + msGetGid10 + "'," +
                            " '" + values.email + "'," +
                            " '" + values.mobile + "'," +
                            " '" + values.designation + "'," +
                            " '" + values.did_number + "'," +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd") +
                            " '" + user_gid + "'," +
                            " '" + values.leadbank_address1 + "'," +
                            " '" + values.leadbank_address2 + "'," +
                            " '" + values.leadbank_state + "'," +
                            " '" + lscountry_gid + "'," +
                            " '" + values.leadbank_city + "'," +
                            " '" + values.region_name + "'," +
                            " '" + values.zip_code + "'," +
                            " '" + values.leadbankbranch_name + "'," +
                            " '  y  '," + "')";
                mnResult16 = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult16 == 1)
                {
                    msSQL = " update crm_trn_tleadbankcontact set" +
                            " customercontact_gid = '" + msGetGid11 + "'" +
                            " where leadbank_gid = '" + values.leadbank_gid + "'";
                    mnResult17 = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

            }

        }

        public void DadeleteLeadbankSummary(string leadbank_gid, leadbank_list values)
        {


            msSQL = "Select * from crm_trn_tleadbank " +
               "where leadbank_gid ='" +leadbank_gid + "' and customer_gid is null";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                msSQL = " select * from crm_trn_ttelelead2campaign where leadbank_gid='" +leadbank_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count == 0)
                {
                    msSQL = " select * from crm_trn_tlead2campaign where leadbank_gid= '" + leadbank_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count == 0)
                    {
                        msSQL = " Delete from crm_trn_tleadbank " +
                                " where leadbank_gid='" + leadbank_gid + "'";
                        mnResult18 = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult18 == 1) 
                        {
                            msSQL = " Delete from crm_trn_tleadbankcontact "+
                                    " where leadbank_gid = '" + leadbank_gid + "'";
                            mnResult19 = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                    }
                    if (mnResult18 != 0)
                    {

                        values.Status = true;
                        values.message = "leadbank deleted Successfully";

                    }
                    else
                    {
                        values.Status = false;
                        values.message = "Error While Deleting leadbank";
                    }
                }
            }
        }

        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlLeadbank values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumb_list1>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumb_list1
                    {


                        module_name1 = dt["module_name1"].ToString(),
                        sref1 = dt["sref1"].ToString(),
                        module_name2 = dt["module_name2"].ToString(),
                        sref2 = dt["sref2"].ToString(),
                        module_name3 = dt["module_name3"].ToString(),
                        sref3 = dt["sref3"].ToString(),

                    });
                    values.breadcrumb_list1 = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
    }
}