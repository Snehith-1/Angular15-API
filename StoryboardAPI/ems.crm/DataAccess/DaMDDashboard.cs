using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace ems.crm.DataAccess
{
    public class DaMDDashboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        ////////////////////////// //CRM Manager DASHBOARD START/////////////////////////////////////////
        public void DaGetsalesorderpendingcount(string user_gid, MdlMDDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select count(salesorder_gid) as salesorder_gid from smr_trn_tsalesorder where  branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<gettotalsalesorderpendingcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new gettotalsalesorderpendingcount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.getsalesordercount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetsalesorderapprovedcount(string user_gid, MdlMDDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select count(salesorder_gid) as salesorder_gid from smr_trn_tsalesorder where salesorder_status='Approved' and branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<gettotalsalesorderaprovedcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new gettotalsalesorderaprovedcount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.gettotalsalesorderaprovedcountlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetsalesorderrejectedcount(string user_gid, MdlMDDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select count(salesorder_gid) as salesorder_gid from smr_trn_tsalesorder where salesorder_status='Cancelled' and branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<gettotalsalesorderrejectedcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new gettotalsalesorderrejectedcount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.gettotalsalesorderrejectedcountlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetsalesorderinprogresscount(string user_gid, MdlMDDashboard values)
        {
            string lsbranch_gid;
            string YearLabel = DateTime.Now.Year.ToString();

            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select count(salesorder_gid) as salesorder_gid from smr_trn_tsalesorder where salesorder_status!='Approved' and branch_gid='" + lsbranch_gid + "'and year(salesorder_date)='" + YearLabel + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getModuleList = new List<gettotalsalesorderinprogresscount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new gettotalsalesorderinprogresscount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.gettotalsalesorderinprogresscountlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetcurrentyearsalesorderrejectedcount(string user_gid, MdlMDDashboard values)
        {
            string lsbranch_gid;
            string YearLabel = DateTime.Now.Year.ToString();

            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select count(salesorder_gid) as salesorder_gid from smr_trn_tsalesorder where salesorder_status='Cancelled' and branch_gid='" + lsbranch_gid + "'and year(salesorder_date)='" + YearLabel + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getModuleList = new List<getcurrentyeartotalsalesorderrejectedcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getcurrentyeartotalsalesorderrejectedcount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.getcurrentyeartotalsalesorderrejectedcountlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetpurchaseorderrejectedcount(string user_gid, MdlMDDashboard values)
        {
            string lsbranch_gid;
            string YearLabel = DateTime.Now.Year.ToString();

            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select count(purchaseorder_gid) as purchaseorder_gid from pmr_trn_tpurchaseorder where purchaseorder_flag='PO Canceled' and branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getModuleList = new List<getpurchaseorderrejectedcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderrejectedcount_list
                    {
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),

                    });
                    values.getpurchaseorderrejectedcountlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetsalesorderpendingSummary(string user_gid, MdlMDDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select salesorder_gid,so_referenceno1,date(salesorder_date) as salesorderdate, customer_name," +
                    " total_amount as Grandtotal,received_amount as  advance from smr_trn_tsalesorder ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesordersummaryemployeeList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getsalesordersummaryemployeeList
                    {


                        salesorderNo = (dt["so_referenceno1"].ToString()),
                        salesorderdate = (dt["salesorderdate"].ToString()),
                        salesorder_gid = (dt["salesorder_gid"].ToString()),
                        customername = (dt["customer_name"].ToString()),
                        Grandtotal = (dt["Grandtotal"].ToString()),
                        advance = (dt["advance"].ToString()),
                        

                    });
                    values.getsalesordersummaryemployee_List = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetsalesorderbarchart(string user_gid, MdlMDDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            string YearLabel = DateTime.Now.Year.ToString(); 

            msSQL = " select year(salesorder_date) as year ,MONTHNAME(salesorder_date) month_name,sum(grandtotal_l) as total_amount from smr_trn_tsalesorder where salesorder_date like '%" + YearLabel + "%'  and branch_gid='" + lsbranch_gid + "' " +
                    " group by year(salesorder_date),month(salesorder_date) order by year(salesorder_date),month(salesorder_date)  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderbarchart_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getsalesorderbarchart_list
                    {


                        year = (dt["year"].ToString()),
                        month_name = (dt["month_name"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),


                    });
                    values.getsalesorderbarchartlist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetsalesordervalue(string user_gid, MdlMDDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            string YearLabel = DateTime.Now.Year.ToString();

            msSQL = " select year(salesorder_date) as year ,MONTHNAME(salesorder_date) month_name,sum(grandtotal_l) as total_amount from smr_trn_tsalesorder where salesorder_date like '%" + YearLabel + "%'  and branch_gid='" + lsbranch_gid + "' " + "";
                    
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<gettotalsalesordervalue_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new gettotalsalesordervalue_list
                    {
                        total_amount = (dt["total_amount"].ToString()),

                    });
                    values.gettotalsalesordervaluelist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


    }
}