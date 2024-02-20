﻿using System;
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
using static ems.crm.Models.MdlRegisterLead;
using System.Net.NetworkInformation;

namespace ems.crm.DataAccess
{
    public class DaRegisterLead
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, source_name, msconGetGID, leadbankcontact_gid, lsdesignation_code, lssource_name, lscategoryindustry_name, lsleadbank_name, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DaGetRegisterLeadSummary(MdlRegisterLead values,string employee_gid)
        {
            msSQL = " SELECT distinct a.leadbank_gid, a.leadbank_gid,a.approval_flag,a.remarks, " +
                " a.leadbank_name, a.status, a.agent_name,a.created_by,a.lead_status, " +
                " a.source_gid,concat(a.source_gid ,' / ',a.categoryindustry_gid )as sourceindustry_name, " +
                " concat(a.leadbank_region,' / ',a.leadbank_city,' / ',a.leadbank_state) as region_name, " +
                " a.created_date, " +
                " concat(f.leadbankcontact_name,' / ',f.country_code1,'-',f.mobile,' / ',f.email)as contact_details, " +
                " g.assign_to, " +
                " concat(j.user_firstname,' ',j.user_lastname) as assignedto " +
                " from crm_trn_tleadbank a " +
                " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                " left join crm_mst_tsource d on a.source_gid = d.source_gid " +
                " left join crm_mst_tcategoryindustry l on a.categoryindustry_gid = l.categoryindustry_gid " +
                " left join crm_mst_tregion e on a.leadbank_region = e.region_gid " +
                " left join crm_trn_tleadbankcontact f on a.leadbank_gid = f.leadbank_gid " +
                " left join crm_trn_tlead2campaign g on a.leadbank_gid = g.leadbank_gid " +
                " left join hrm_mst_temployee h on  g.assign_to = h.employee_gid " +
                " left join adm_mst_tuser j on h.user_gid = j.user_gid" +
                " where a.created_by='"+employee_gid+ "' Order by a.created_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetRegisterLeadSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetRegisterLeadSummary_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        approval_flag = dt["approval_flag"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        agent_name = dt["agent_name"].ToString(),
                        lead_status = dt["lead_status"].ToString(),
                        source_gid = dt["source_gid"].ToString(),
                        source_name = dt["sourceindustry_name"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        contact_details = dt["contact_details"].ToString(),
                        assign_to = dt["assign_to"].ToString(),
                        assignedto = dt["assignedto"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        Status = dt["Status"].ToString(),

                    });
                    values.GetRegisterLeadSummarylist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostregisterlead(string employee_gid, Registerlead_list values)
        {

           msSQL = " Select source_name from crm_mst_tsource where source_gid = '" + values.source_name + "'";
            string lssource_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select  leadbank_name from crm_trn_tleadbank where leadbank_gid = '" + values.leadbank_name + "'";
            string lsleadbank_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select  categoryindustry_name  from crm_mst_tcategoryindustry where categoryindustry_gid = '" + values.categoryindustry_name + "'";
            string lscategoryindustry_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select region_name  from crm_mst_tregion where  region_gid = '" + values.region_name + "'";
            string lsregion_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select  country_name from adm_mst_tcountry where  country_gid = '" + values.country_name + "'";
            string lscountry_name = objdbconn.GetExecuteScalar(msSQL);




            msGetGid = objcmnfunctions.GetMasterGID("BMCC");
            if (msGetGid == "E")
            {
                values.status = false;
                values.message = "Create sequence code BMCC for lead bank";
            }



            msGetGid1 = objcmnfunctions.GetMasterGID("BLBP");
            if (msGetGid1 == "E")
            {
                values.status = false;
                values.message = "Create sequence code BLBP for lead bank";
            }


 msSQL = " INSERT INTO crm_trn_tleadbank(" +
                    " leadbank_gid," +
                    " source_gid," +
                    " leadbank_id," +
                    " leadbank_name," +
                    " status," +
                    " company_website," +
                    " main_branch," +
                    " approval_flag, " +
                    " lead_status," +
                    " leadbank_code," +
                    " leadbank_state," +
                    " leadbank_address1," +
                    " leadbank_address2," +
                    " leadbank_city," +
                    " leadbank_region," +
                    " leadbank_country," +
                    " leadbank_pin," +
                    " created_by," +
                    " remarks," +
                    " categoryindustry_gid," +
                    " assign_to," +
                    " referred_by," +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid1 + "'," +
                    " '" + lssource_name + "'," +
                    " '" + msGetGid + "'," +
                    " '" + lsleadbank_name + "'," +
                    " '  Y  '," +
                    " '" + values.company_website + "'," +
                    " '  Y  '," +
                    " '  Approved  '," +
                    " '  Not Assigned  '," +
                    " '  H.Q  '," +
                    " '" + values.leadbank_state + "'," +
                    " '" + values.leadbank_address1 + "'," +
                    " '" + values.leadbank_address2 + "'," +
                    " '" + values.leadbank_city + "'," +
                    " '" + lsregion_name + "'," +
                    " '" + lscountry_name + "'," +
                    " '" + values.leadbank_pin + "'," +
                    " '" + employee_gid + "'," +
                    " '" + values.remarks + "'," +
                    " '" + lscategoryindustry_name + "'," +
                    " '" + employee_gid + "'," +
                    " '" + values.referred_by + "'," +
                    " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 0)
            {
                values.status = false;
                values.message = "Error Occured While Inserting Records";
            }

            msconGetGID = objcmnfunctions.GetMasterGID("BLCC");

            if (msconGetGID == "E")
            {
                values.status = false;
                values.message = "Create sequence code BLCC for lead contact ";
            }

            msSQL = " INSERT INTO crm_trn_tleadbankcontact(" +
                    " leadbankcontact_gid," +
                    " leadbank_gid," +
                    " leadbankbranch_name," +
                    " leadbankcontact_name," +
                    " email," +
                    " main_contact," +
                    " mobile," +
                    " designation, " +
                    " phone1," +
                    " country_code1," +
                    " area_code1," +
                    " phone2," +
                    " country_code2," +
                    " area_code2," +
                    " fax_country_code," +
                    " fax_area_code," +
                    " fax," +
                    " created_by," +
                    " address1," +
                    " address2,"+
                    " country," +
                    " region_name," +
                    " state," +
                    " pincode," +
                    " created_date)" +
                    " values(" +
                    " '" + msconGetGID + "'," +
                    " '" + msGetGid1 + "'," +
                    " '  H.Q  '," +
                    " '" + values.leadbankcontact_name + "'," +
                    " '" + values.email + "'," +
                    " '  Y  '," +
                    " '" + values.mobile + "'," +
                    " '" + values.designation + "'," +
                    " '" + values.phone1 + "'," +
                    " '" + values.country_code1 + "'," +
                    " '" + values.area_code1 + "'," +
                    " '" + values.phone2 + "'," +
                    " '" + values.country_code2 + "'," +
                    " '" + values.area_code2 + "'," +
                    " '" + values.fax_country_code + "'," +
                    " '" + values.fax_area_code + "'," +
                    " '" + values.fax + "'," +
                    " '" + employee_gid + "'," +
                    " '" + values.leadbank_address1 + "'," +
                    " '" + values.leadbank_address2 + "'," +
                    " '" + lscountry_name + "'," +
                    " '" + lsregion_name + "'," +
                     " '" + values.leadbank_state + "'," +
                    " '" + values.leadbank_pin + "'," +
                    " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 0)
            {
                values.status = false;
                values.message = "Error Occured While Inserting Records";
            }


        }

        public void Dapostbranchlead( string user_gid, leadaddbranch_list values)
        {

           msSQL = " Select region_name  from crm_mst_tregion where  region_gid = '" + values.region_name + "'";
            string lsregion_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select  country_name from adm_mst_tcountry where  country_gid = '" + values.country + "'";
            string lscountry_name = objdbconn.GetExecuteScalar(msSQL);


           msconGetGID = objcmnfunctions.GetMasterGID("BLCC");

            if (msconGetGID == "E")
            {
                values.status = false;
                values.message = "Create sequence code BLCC for lead branch ";
            }

            msSQL = " INSERT INTO crm_trn_tleadbankcontact(" +
                    " leadbankcontact_gid," +
                    " leadbank_gid," +
                    " leadbankbranch_name," +
                    " leadbankcontact_name," +
                    " email," +
                    " mobile," +
                    " designation, " +
                    " created_by," +
                    " address1," +
                    " address2," +
                    " country," +
                    " region_name," +
                    " state," +
                    " pincode," +
                    " created_date)" +
                    " values(" +
                    " '" + msconGetGID + "'," +
                    " '" + values.leadbank_gid + "'," +
                    " '  H.Q  '," +
                    " '" + values.leadbankcontact_name + "'," +
                    " '" + values.email + "'," +
                    " '" + values.mobile + "'," +
                    " '" + values.designation + "'," +
                    " '" + user_gid + "'," +
                    " '" + values.address1 + "'," +
                    " '" + values.address2 + "'," +
                    " '" + lscountry_name + "'," +
                    " '" + lsregion_name + "'," +
                     " '" + values.state + "'," +
                    " '" + values.pincode + "'," +
                    " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 0)
            {
                values.status = false;
                values.message = "Error Occured While Inserting Records";
            }


        }



        public void DaGetbranchdropdown(string leadbank_gid, MdlRegisterLead values)
        {
            msSQL = " select leadbankbranch_name,leadbank_gid from crm_trn_tleadbankcontact" +
                    " where leadbank_gid ='" + leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<branch_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new branch_list
                    {
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),

                    });
                    values.branch_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetleadbankbrancheditSummary(string leadbank_gid, MdlRegisterLead values)
        {
            msSQL = "select leadbankbranch_name,leadbankcontact_gid,leadbankcontact_name, designation, mobile,email,address1, address2,city,state,region_name,country,pincode  from  crm_trn_tleadbankcontact  " +
                    " where leadbank_gid ='" + leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadaddbranch_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadaddbranch_list
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
                        country = dt["country"].ToString(),
                        region_name = dt["region_name"].ToString(),
                        pincode = dt["pincode"].ToString(),
                    });
                    values.leadaddbranch_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void Dapostcontactlead(string user_gid, Registerlead_list values)

        {

         msconGetGID = objcmnfunctions.GetMasterGID("BLCC");

            if (msconGetGID == "E")
            {
                values.status = false;
                values.message = "Create sequence code BLCC for lead branch ";
            }

            msSQL = " INSERT INTO crm_trn_tleadbankcontact(" +
                    " leadbankcontact_gid," +
                    " leadbank_gid," +
                    " leadbankbranch_name," +
                    " leadbankcontact_name," +
                    " email," +
                    " mobile," +
                    " designation, " +
                    " created_by," +
                    " phone1," +
                    " country_code1," +
                    " area_code1," +
                    " phone2," +
                    " country_code2," +
                    " area_code2," +
                    " fax," +
                    " fax_country_code," +
                    " fax_area_code," +
                    " created_date)" +
                    " values(" +
                    " '" + msconGetGID + "'," +
                    " '" + values.leadbank_gid + "'," +
                    " '  H.Q  '," +
                    " '" + values.leadbankcontact_name + "'," +
                    " '" + values.email + "'," +
                    " '" + values.mobile + "'," +
                    " '" + values.designation + "'," +
                    " '" + user_gid + "'," +
                    " '" + values.phone1 + "'," +
                    " '" + values.country_code1 + "'," +
                    " '" + values.area_code1 + "'," +
                    " '" + values.phone2 + "'," +
                    " '" + values.country_code2 + "'," +
                    " '" + values.area_code2 + "'," +
                    " '" + values.fax + "'," +
                    " '" + values.fax_country_code + "'," +
                    " '" + values.fax_area_code + "'," +
                    " '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 0)
            {
                values.status = false;
                values.message = "Error Occured While Inserting Records";
            }


        }


        public void DaGeteditleadSummary(string leadbank_gid, MdlRegisterLead values)
        {
            msSQL = " SELECT a.leadbank_gid,a.leadbank_id,a.hq_flag,a.remarks,a.referred_by, " +
                    "  a.source_gid, a.categoryindustry_gid, a.leadbank_type, a.leadbank_name, a.status,  " +
                    " a.agent_name, a.leadbank_code, a.leadbank_address1,a.leadbank_address2, " +
                    " a.leadbank_city, a.leadbank_state, a.leadbank_region," +
                    " a.leadbank_country, a.leadbank_pin, c.leadbank_gid,a.leadbankgroup_gid,c.leadbankcontact_gid, " +
                    "  c.leadbankcontact_name, right(c.mobile,10) as mobile, c.email, c.designation, c.did_number, a.company_website , " +
                    "  c.phone1, c.phone2, c.fax, c.country_code1, c.country_code2, c.area_code1, c.area_code2, c.fax_country_code, c.fax_area_code  " +
                    " from crm_trn_tleadbank a " +
                    " left join crm_trn_tleadbankcontact c on a.leadbank_gid=c.leadbank_gid" +
                    " where a.leadbank_gid ='" + leadbank_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Registereditlead_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Registereditlead_list
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbankcontact_gid = dt["leadbankcontact_gid"].ToString(),
                        categoryindustry_gid = dt["categoryindustry_gid"].ToString(),
                        source_name = dt["source_gid"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                        leadbank_address1 = dt["leadbank_address1"].ToString(),
                        leadbank_address2 = dt["leadbank_address2"].ToString(),
                        leadbank_city = dt["leadbank_city"].ToString(),
                        region_name = dt["leadbank_region"].ToString(),
                        leadbank_state = dt["leadbank_state"].ToString(),
                        leadbank_country = dt["leadbank_country"].ToString(),
                        leadbank_pin = dt["leadbank_pin"].ToString(),
                        leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                        mobile = dt["mobile"].ToString(),
                        email = dt["email"].ToString(),
                        company_website = dt["company_website"].ToString(),
                        designation = dt["designation"].ToString(),
                        phone1 = dt["phone1"].ToString(),
                        phone2 = dt["phone2"].ToString(),
                        country_code1 = dt["country_code1"].ToString(),
                        country_code2 = dt["country_code2"].ToString(),
                        area_code1 = dt["area_code1"].ToString(),
                        area_code2 = dt["area_code2"].ToString(),
                        fax_country_code = dt["fax_country_code"].ToString(),
                        fax_area_code = dt["fax_area_code"].ToString(),
                        fax = dt["fax"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        referred_by = dt["referred_by"].ToString(),
                        status = dt["status"].ToString(),




                    });
                    values.Registereditlead_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetregisterleadviewSummary(string leadbank_gid, MdlRegisterLead values)
        {
            msSQL = " SELECT a.leadbank_gid,a.leadbank_id,a.hq_flag,a.remarks,a.referred_by, " +
                    "  a.source_gid, a.categoryindustry_gid, a.leadbank_type, a.leadbank_name, a.status,  " +
                    " a.agent_name, a.leadbank_code, a.leadbank_address1,a.leadbank_address2, " +
                    " a.leadbank_city, a.leadbank_state, a.leadbank_region," +
                    " a.leadbank_country, a.leadbank_pin, c.leadbank_gid,a.leadbankgroup_gid,c.leadbankcontact_gid, " +
                    "  c.leadbankcontact_name, right(c.mobile,10) as mobile, c.email, c.designation, c.did_number, a.company_website , " +
                    "  c.phone1, c.phone2, c.fax, c.country_code1, c.country_code2, c.area_code1, c.area_code2, c.fax_country_code, c.fax_area_code  " +
                    " from crm_trn_tleadbank a " +
                    " left join crm_trn_tleadbankcontact c on a.leadbank_gid=c.leadbank_gid" +
                    " where a.leadbank_gid ='" + leadbank_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Registereditlead_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    {
                        getModuleList.Add(new Registereditlead_list
                        {
                            leadbank_gid = dt["leadbank_gid"].ToString(),
                            leadbankcontact_gid = dt["leadbankcontact_gid"].ToString(),
                            categoryindustry_gid = dt["categoryindustry_gid"].ToString(),
                            source_name = dt["source_gid"].ToString(),
                            leadbank_name = dt["leadbank_name"].ToString(),
                            leadbank_address1 = dt["leadbank_address1"].ToString(),
                            leadbank_address2 = dt["leadbank_address2"].ToString(),
                            leadbank_city = dt["leadbank_city"].ToString(),
                            region_name = dt["leadbank_region"].ToString(),
                            leadbank_state = dt["leadbank_state"].ToString(),
                            leadbank_country = dt["leadbank_country"].ToString(),
                            leadbank_pin = dt["leadbank_pin"].ToString(),
                            leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                            mobile = dt["mobile"].ToString(),
                            email = dt["email"].ToString(),
                            company_website = dt["company_website"].ToString(),
                            designation = dt["designation"].ToString(),
                            phone1 = dt["phone1"].ToString(),
                            phone2 = dt["phone2"].ToString(),
                            country_code1 = dt["country_code1"].ToString(),
                            country_code2 = dt["country_code2"].ToString(),
                            area_code1 = dt["area_code1"].ToString(),
                            area_code2 = dt["area_code2"].ToString(),
                            fax_country_code = dt["fax_country_code"].ToString(),
                            fax_area_code = dt["fax_area_code"].ToString(),
                            fax = dt["fax"].ToString(),
                            remarks = dt["remarks"].ToString(),
                            referred_by = dt["referred_by"].ToString(),
                            status = dt["status"].ToString(),

                        });
                        values.Registerviewlead_list = getModuleList;
                    }
                }
                dt_datatable.Dispose();
            }
        }


        public void DaGetleadbranchaddSummary(string leadbank_gid, MdlRegisterLead values)
        {
          
         msSQL = " SELECT distinct a.leadbankcontact_gid,a.leadbank_gid,a.leadbankbranch_name,a.leadbankcontact_name,a.mobile,a.designation,concat (a.address1,a.address2) As Address," +
                 " a.city, a.state, a.pincode, a.country,a.region_name,a.email,a.country_code1,a.country_gid " +
                 " from crm_trn_tleadbankcontact a" +
                 " where a.leadbank_gid ='" + leadbank_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadaddbranch_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadaddbranch_list
                    {



                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),
                        leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                        designation = dt["designation"].ToString(),
                        mobile = dt["mobile"].ToString(),
                        Address = dt["Address"].ToString(),
                        city = dt["city"].ToString(),
                        state = dt["state"].ToString(),
                        country = dt["country"].ToString(),
                        pincode = dt["pincode"].ToString(),
                    });
                    values.leadaddbranch_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetleadcontactaddSummary(string leadbank_gid, MdlRegisterLead values)
        {

            msSQL = " select leadbankbranch_name,leadbank_gid,leadbankcontact_gid,leadbankcontact_name,designation,mobile,email,fax,fax_country_code," +
                    " fax_area_code,phone1,country_code1,area_code1,phone2,country_code2,area_code2 " +
                    " from crm_trn_tleadbankcontact " +
                    " where leadbank_gid ='" + leadbank_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Registerlead_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Registerlead_list
                    {



                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),
                        leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                        leadbankcontact_gid = dt["leadbankcontact_gid"].ToString(),
                        designation = dt["designation"].ToString(),
                        mobile = dt["mobile"].ToString(),
                        email = dt["email"].ToString(),
                        fax_country_code = dt["fax_country_code"].ToString(),
                        fax_area_code = dt["fax_area_code"].ToString(),
                        fax = dt["fax"].ToString(),
                        phone1 = dt["phone1"].ToString(),
                        phone2 = dt["phone2"].ToString(),
                        country_code1 = dt["country_code1"].ToString(),
                        country_code2 = dt["country_code2"].ToString(),
                        area_code1 = dt["area_code1"].ToString(),
                        area_code2 = dt["area_code2"].ToString(),


                    });
                    values.Registerlead_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetleadcontacteditSummary(string leadbank_gid, MdlRegisterLead values)
        {

            msSQL = " select leadbankbranch_name,leadbank_gid,leadbankcontact_gid,leadbankcontact_name,designation,mobile,email,fax,fax_country_code," +
                    " fax_area_code,phone1,country_code1,area_code1,phone2,country_code2,area_code2 " +
                    " from crm_trn_tleadbankcontact " +
                    " where leadbank_gid ='" + leadbank_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Registerlead_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Registerlead_list
                    {



                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),
                        leadbankcontact_name = dt["leadbankcontact_name"].ToString(),
                        leadbankcontact_gid = dt["leadbankcontact_gid"].ToString(),
                        designation = dt["designation"].ToString(),
                        mobile = dt["mobile"].ToString(),
                        email = dt["email"].ToString(),
                        fax_country_code = dt["fax_country_code"].ToString(),
                        fax_area_code = dt["fax_area_code"].ToString(),
                        fax = dt["fax"].ToString(),
                        phone1 = dt["phone1"].ToString(),
                        phone2 = dt["phone2"].ToString(),
                        country_code1 = dt["country_code1"].ToString(),
                        country_code2 = dt["country_code2"].ToString(),
                        area_code1 = dt["area_code1"].ToString(),
                        area_code2 = dt["area_code2"].ToString(),


                    });
                    values.Registerlead_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetregisterleadbranchSummary(string leadbank_gid, MdlRegisterLead values)
        {
            msSQL = " select a.leadbankbranch_name,concat (a.address1,a.address2) As Address,b.leadbank_city,a.state, " +
                    " a.pincode,b.leadbank_country from crm_trn_tleadbankcontact a " +
                    " left join crm_trn_tleadbank  b on a.leadbank_gid=b.leadbank_gid " +
                    " where a.leadbank_gid ='" + leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadbranch_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadbranch_list
                    {
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),
                        Address = dt["Address"].ToString(),
                        leadbank_city = dt["leadbank_city"].ToString(),
                        state = dt["state"].ToString(),
                        pincode = dt["pincode"].ToString(),
                        leadbank_country = dt["leadbank_country"].ToString(),
                    });
                    values.leadbranch_list = getModuleList;
                }
            }
            dt_datatable.Dispose(); 
        }

        public void DaGetregistercontactSummary(string leadbank_gid, MdlRegisterLead values)
        {
            msSQL = "select a.leadbank_name,b.leadbankbranch_name,b.mobile,b.email," +
                    "b.designation from  crm_trn_tleadbankcontact b " +
                    "left join crm_trn_tleadbank a on a.leadbank_gid=b.leadbank_gid" +
                    " where a.leadbank_gid ='" + leadbank_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<leadcontact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new leadcontact_list
                    {
                        leadbank_name = dt["leadbank_name"].ToString(),
                        leadbankbranch_name = dt["leadbankbranch_name"].ToString(),
                        mobile = dt["mobile"].ToString(),
                        email = dt["email"].ToString(),
                        designation = dt["designation"].ToString(),



                    });
                    values.leadcontact_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }



        public void DaUpdatedregisterlead(string employee_gid, Registerlead_list values)
        {
            msSQL = " Select source_name from crm_mst_tsource where source_gid = '" + values.source_name + "'";
            string lssource_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select  leadbank_name from crm_trn_tleadbank where leadbank_gid = '" + values.leadbank_name + "'";
            string lsleadbank_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select  categoryindustry_name  from crm_mst_tcategoryindustry where categoryindustry_gid = '" + values.categoryindustry_name + "'";
            string lscategoryindustry_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select region_name  from crm_mst_tregion where  region_gid = '" + values.region_name + "'";
            string lsregion_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select  country_name from adm_mst_tcountry where  country_gid = '" + values.country_name + "'";
            string lscountry_name = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " update  crm_trn_tleadbank  set " +
          " leadbank_gid = '" + msGetGid1 + "'," +
          " source_gid = '" + lssource_name + "'," +
          " leadbank_id = '" + msGetGid + "'," +
          " leadbank_name = '" + lsleadbank_name + "'," +
          " company_website = '" + values.company_website + "'," +
          " leadbank_state = '" + values.leadbank_state + "'," +
          " leadbank_address1 = '" + values.leadbank_address1 + "'," +
          " leadbank_address2 = '" + values.leadbank_address2 + "'," +
          " leadbank_city = '" + values.leadbank_city + "'," +
          " leadbank_region = '" + lsregion_name + "'," +
          " leadbank_country = '" + lscountry_name + "'," +
          " leadbank_pin = '" + values.leadbank_pin + "'," +
          " remarks = '" + values.remarks + "'," +
          " categoryindustry_gid = '" + lscategoryindustry_name + "'," +
          " referred_by = '" + values.referred_by + "'," +
          " updated_by = '" + employee_gid + "'," +
          " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where leadbank_gid='" + values.leadbank_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 0)
            {
                values.status = false;
                values.message = "Error Occured While Inserting Records";
            }

            msSQL = " update  crm_trn_tleadbankcontact  set " +
         " leadbankcontact_gid = '" + msconGetGID + "'," +
         " leadbank_gid = '" + msGetGid1 + "'," +
         " leadbankcontact_name = '" + values.leadbankcontact_name + "'," +
         " email = '" + values.email + "'," +
         " mobile = '" + values.mobile + "'," +
         " designation = '" + values.designation + "'," +
         " phone1 = '" + values.phone1 + "'," +
         " country_code1 = '" + values.country_code1 + "'," +
         " area_code1 = '" + values.area_code1 + "'," +
         " phone2 = '" + values.phone2 + "'," +
         " country_code2 = '" + values.country_code2 + "'," +
         " area_code2 = '" + values.area_code2 + "'," +
         " fax_country_code = '" + values.fax_country_code + "'," +
         " fax_area_code = '" + values.fax_area_code + "'," +
         " fax = '" + values.fax + "'," +
         " address1 = '" + values.leadbank_address1 + "'," +
         " address2 = '" + values.leadbank_address2 + "'," +
         " country = '" + lscountry_name + "'," +
         " region_name = '" + lsregion_name + "'," +
         " state = '" + values.state + "'," +
         " pincode = '" + values.leadbank_pin + "'," +
         " updated_by = '" + employee_gid + "'," +
         " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where leadbank_gid='" + values.leadbank_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult == 0)
            {
                values.status = false;
                values.message = "Error Occured While Inserting Records";
            }


        }


        public void DadeleteregisterleadSummary(string leadbank_gid, GetRegisterLeadSummary_list values)
        {
            msSQL = "  delete from crm_trn_tleadbank where leadbank_gid='" + leadbank_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "  delete from crm_trn_tleadbankcontact where leadbank_gid='" + leadbank_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Lead Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Lead";
            }
        }

        public void DaGetcountrynamedropdown(MdlRegisterLead values)
        {
            msSQL = " select  country_gid, country_code, country_name " +
                    " from adm_mst_tcountry ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getcountrynamedropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getcountrynamedropdown
                    {
                        country_gid = dt["country_gid"].ToString(),
                        country_name = dt["country_name"].ToString(),
                    });
                    values.Getcountrynamedropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetregiondropdown(MdlRegisterLead values)
        {
            msSQL = " select region_gid, region_code, region_name " +
                    " from crm_mst_tregion ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getregiondropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getregiondropdown
                    {
                        region_gid = dt["region_gid"].ToString(),
                        region_name = dt["region_name"].ToString(),
                    });
                    values.Getregiondropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetindustrydropdown(MdlRegisterLead values)
        {
            msSQL = " select categoryindustry_gid,categoryindustry_code,categoryindustry_name " +
                "  from crm_mst_tcategoryindustry ";
                
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getindustrydropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getindustrydropdown
                    {
                        categoryindustry_gid = dt["categoryindustry_gid"].ToString(),
                        categoryindustry_name = dt["categoryindustry_name"].ToString(),
                    });
                    values.Getindustrydropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetSourcetypedropdown(MdlRegisterLead values)
        {
            msSQL = " select source_gid,concat(source_code,'-',source_name) as source_name " +
                "  from crm_mst_tsource ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetSourcetypedropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetSourcetypedropdown
                    {
                        source_gid = dt["source_gid"].ToString(),
                        source_name = dt["source_name"].ToString(),
                    });
                    values.GetSourcetypedropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetcompanylistdropdown(MdlRegisterLead values)
        {
            msSQL = " select leadbank_gid,leadbank_name " +
                "  from crm_trn_tleadbank ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getcompanylistdropdown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getcompanylistdropdown
                    {
                        leadbank_gid = dt["leadbank_gid"].ToString(),
                        leadbank_name = dt["leadbank_name"].ToString(),
                    });
                    values.Getcompanylistdropdown = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaUpdateleadbranchedit(string user_gid, leadaddbranch_list values)
        {
       
            msSQL = " Select region_name  from crm_mst_tregion where  region_gid = '" + values.region_name + "'";
            string lsregion_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " Select  country_name from adm_mst_tcountry where  country_gid = '" + values.country + "'";
            string country_name = objdbconn.GetExecuteScalar(msSQL);



            msSQL = " Update crm_trn_tleadbankcontact set" +
                     " leadbankbranch_name = '" + values.leadbankbranch_name + "'," +
                     " leadbankcontact_name = '" + values.leadbankcontact_name + "'," +
                     " designation = '" + values.designation + "'," +
                     " mobile = '" + values.mobile + "'," +
                     " email =  '" + values.email + "'," +
                     " address1 =  '" + values.address1 + "'," +
                     " address2 =  '" + values.address2 + "'," +
                     " region_name =  '" + values.region_name + "'," +
                     " country =  '" + values.country_name + "'," +
                     " state = '" + values.state + "'," +
                     " pincode = '" + values.pincode + "'" +
                     " where leadbank_gid = '" + values.leadbank_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 0)
            {
                values.status = false;
                values.message = "Error Occured While Inserting Records";
            }



        }
        public void DaUpdateleadContactedit(string user_gid, Registerlead_list values)
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

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult == 0)
            {
                values.status = false;
                values.message = "Error Occured While Inserting Records";
            }
        }

        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlRegisterLead values)
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
