﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.pmr.Models;
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

namespace ems.pmr.DataAccess
{
    public class DaTermsandconditions
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        public void DaGetTermsandconditionsSummary(MdlTermsandconditions values)
        {
            msSQL = " select  termsconditions_gid,template_name,payment_terms, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y')  as created_date " +
                    " from pmr_trn_ttermsconditions a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<terms_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new terms_list
                    {
                        termsconditions_gid = dt["termsconditions_gid"].ToString(),
                        template_name = dt["template_name"].ToString(),
                        payment_terms = dt["payment_terms"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.terms_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostTermsandconditions(string user_gid, terms_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("PTCP");

            msSQL = " insert into pmr_trn_ttermsconditions(" +
                    " termsconditions_gid," +
                    " template_name," +
                    " template_content," +
                    " payment_terms," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    " '" + values.template_name + "'," +
                    "'" + values.template_content + "',";
            if (values.payment_terms == null || values.payment_terms == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.payment_terms.Replace("'", "") + "',";
            }
            msSQL += "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Terms Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Terms";
            }
     
       }
        public void DaGeteditTermsandconditionsSummary(string termsconditions_gid, MdlTermsandconditions values)
        {
            msSQL = " select termsconditions_gid,template_name,payment_terms,template_content from pmr_trn_ttermsconditions  where termsconditions_gid= '" + termsconditions_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<terms_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new terms_list
                    {

                        termsconditions_gid = dt["termsconditions_gid"].ToString(),
                        template_name = dt["template_name"].ToString(),
                        payment_terms = dt["payment_terms"].ToString(),
                        template_content = dt["template_content"].ToString(),

                    });
                    values.terms_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaUpdatedTermsandconditions(string user_gid, terms_list values)
        {

            msSQL = " update  pmr_trn_ttermsconditions set " +
             " template_name = '" + values.template_name + "'," +
             " payment_terms = '" + values.payment_terms + "'," +
             " template_content = '" + values.template_content + "'," +
             " updated_by = '" + user_gid + "'," +
             " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where termsconditions_gid='" + values.termsconditions_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Terms Updated Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error While Updating Terms";
                }


        }

        public void DadeleteTermsandconditionsSummary(string termsconditions_gid, terms_list values)
        {
            msSQL = "  delete from pmr_trn_ttermsconditions where termsconditions_gid='" + termsconditions_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Terms Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting Terms";
            }
        }

    }

}