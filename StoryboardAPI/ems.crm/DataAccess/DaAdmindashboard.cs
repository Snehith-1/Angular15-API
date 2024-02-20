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

namespace ems.crm.DataAccess
{
    public class DaAdmindashboard
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        ////////////////////////// //SALES ADMIN DASHBOARD PANELS START/////////////////////////////////////////

        //sales order//
        public void DaGettotalsalesordercount(string user_gid, MdlAdmindashboard values)
        {

            msSQL = " SELECT COUNT(salesorder_gid) as salesorder_gid FROM smr_trn_tsalesorder ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<gettotalsalesordercount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new gettotalsalesordercount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.gettotalsalesordercount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        //salesorder pending//
        public void DaGetsalesorderpendingcount(string user_gid, MdlAdmindashboard values)
        {

            msSQL = " SELECT COUNT(salesorder_gid) as salesorder_gid FROM smr_trn_tsalesorder where salesorder_status IN ('Invoice Raised')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderpendingcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getsalesorderpendingcount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.getsalesorderpendingcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        //salesorder inprogress//
        public void DaGetsalesorderinprogresscount(string user_gid, MdlAdmindashboard values)
        {

            msSQL = " SELECT COUNT(salesorder_gid) as salesorder_gid FROM smr_trn_tsalesorder where salesorder_status IN ('Advance Paid') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderinprogresscount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getsalesorderinprogresscount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.getsalesorderinprogresscount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        //salesorder approved//
        public void DaGetsalesorderapprovedcount(string user_gid, MdlAdmindashboard values)
        {

            msSQL = " SELECT COUNT(salesorder_gid) as salesorder_gid FROM smr_trn_tsalesorder where salesorder_status IN ('Approved') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderapprovedcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getsalesorderapprovedcount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.getsalesorderapprovedcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        //salesorder rejected//
        public void DaGetsalesorderrejectedcount(string user_gid, MdlAdmindashboard values)
        {

            msSQL = " SELECT COUNT(salesorder_gid) as salesorder_gid FROM smr_trn_tsalesorder where salesorder_status IN ('Cancelled') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderrejectedcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getsalesorderrejectedcount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.getsalesorderrejectedcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        //salesorder ammended//
        public void DaGetsalesorderammendedcount(string user_gid, MdlAdmindashboard values)
        {

            msSQL = " SELECT COUNT(salesorder_gid)as salesorder_gid FROM smr_trn_tsalesorder where salesorder_status IN ('SO Amended')  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderammendedcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getsalesorderammendedcount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.getsalesorderammendedcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        //sales current year//
        public void DaGetsalesordercurrentyearcount(string user_gid, MdlAdmindashboard values)
        {

            msSQL = " SELECT COUNT(salesorder_gid) as salesorder_gid FROM smr_trn_tsalesorder where salesorder_status IN ('Approved') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesordercurrentyearcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getsalesordercurrentyearcount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.getsalesordercurrentyearcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        //salesorder rejected current year//
        public void DaGetsalesorderrejectedyearcount(string user_gid, MdlAdmindashboard values)
        {

            msSQL = " SELECT COUNT(salesorder_gid) as salesorder_gid FROM smr_trn_tsalesorder where salesorder_status IN ('Cancelled') ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderrejectedyearcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getsalesorderrejectedyearcount_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.getsalesorderrejectedyearcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        //summary///////
        public void DaGetSalesorderpendingSummary(string user_gid, MdlAdmindashboard values)
        {
            string msSQL = "SELECT a.salesorder_gid, a.so_referenceno1, a.salesorder_date, a.customer_name, b.branch_name,a.customer_address, a.total_amount, a.created_by, a.salesorder_status, a.branch_gid " +
               "FROM smr_trn_tsalesorder a " +
               "LEFT JOIN hrm_mst_tbranch b ON a.branch_gid = b.branch_gid " +
               "WHERE a.salesorder_status = 'Invoice Raised'";
            DataTable dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderpendingsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)


                {
                    getModuleList.Add(new getsalesorderpendingsummary_list
                    {
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        so_referenceno1 = (dt["so_referenceno1"].ToString()),
                        customer_name = (dt["customer_name"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        customer_address = (dt["customer_address"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),
                        created_by = (dt["created_by"].ToString()),
                        salesorder_status = (dt["salesorder_status"].ToString()),
                    });
                    values.getsalesorderpendingsummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetSalesorderapprovedSummary(string user_gid, MdlAdmindashboard values)
        {
            string msSQL = "SELECT a.salesorder_gid, a.so_referenceno1, a.salesorder_date, a.customer_name, b.branch_name,a.customer_address, a.total_amount, a.created_by, a.salesorder_status, a.branch_gid " +
              "FROM smr_trn_tsalesorder a " +
              "LEFT JOIN hrm_mst_tbranch b ON a.branch_gid = b.branch_gid " +
              "WHERE a.salesorder_status = 'Approved'";
            DataTable dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderapprovedsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)


                {
                    getModuleList.Add(new getsalesorderapprovedsummary_list
                    {
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        so_referenceno1 = (dt["so_referenceno1"].ToString()),
                        customer_name = (dt["customer_name"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        customer_address = (dt["customer_address"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),
                        created_by = (dt["created_by"].ToString()),
                        salesorder_status = (dt["salesorder_status"].ToString()),
                    });
                    values.getsalesorderapprovedsummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetSalesorderinprogressSummary(string user_gid, MdlAdmindashboard values)
        {
            string msSQL = "SELECT a.salesorder_gid, a.so_referenceno1, a.salesorder_date, a.customer_name, b.branch_name,a.customer_address, a.total_amount, a.created_by, a.salesorder_status, a.branch_gid " +
             "FROM smr_trn_tsalesorder a " +
             "LEFT JOIN hrm_mst_tbranch b ON a.branch_gid = b.branch_gid " +
             "WHERE a.salesorder_status = 'Advance Paid'";
            DataTable dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderinprogresssummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)


                {
                    getModuleList.Add(new getsalesorderinprogresssummary_list
                    {
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        so_referenceno1 = (dt["so_referenceno1"].ToString()),
                        customer_name = (dt["customer_name"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        customer_address = (dt["customer_address"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),
                        created_by = (dt["created_by"].ToString()),
                        salesorder_status = (dt["salesorder_status"].ToString()),
                    });
                    values.getsalesorderinprogresssummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetSalesorderrejectedSummary(string user_gid, MdlAdmindashboard values)
        {
            string msSQL = "SELECT a.salesorder_gid, a.so_referenceno1, a.salesorder_date, a.customer_name, b.branch_name,a.customer_address, a.total_amount, a.created_by, a.salesorder_status, a.branch_gid " +
            "FROM smr_trn_tsalesorder a " +
            "LEFT JOIN hrm_mst_tbranch b ON a.branch_gid = b.branch_gid " +
            "WHERE a.salesorder_status = 'Cancelled'";
            DataTable dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderrejectedsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)


                {
                    getModuleList.Add(new getsalesorderrejectedsummary_list
                    {
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        so_referenceno1 = (dt["so_referenceno1"].ToString()),
                        customer_name = (dt["customer_name"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        customer_address = (dt["customer_address"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),
                        created_by = (dt["created_by"].ToString()),
                        salesorder_status = (dt["salesorder_status"].ToString()),
                    });
                    values.getsalesorderrejectedsummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetSalesorderammendedSummary(string user_gid, MdlAdmindashboard values)
        {

            string msSQL = "SELECT a.salesorder_gid, a.so_referenceno1, a.salesorder_date, a.customer_name, b.branch_name,a.customer_address, a.total_amount, a.created_by, a.salesorder_status, a.branch_gid " +
            "FROM smr_trn_tsalesorder a " +
            "LEFT JOIN hrm_mst_tbranch b ON a.branch_gid = b.branch_gid " +
            "WHERE a.salesorder_status = 'SO Amended'";
            DataTable dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderammendedsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)


                {
                    getModuleList.Add(new getsalesorderammendedsummary_list
                    {
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        so_referenceno1 = (dt["so_referenceno1"].ToString()),
                        customer_name = (dt["customer_name"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        customer_address = (dt["customer_address"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),
                        created_by = (dt["created_by"].ToString()),
                        salesorder_status = (dt["salesorder_status"].ToString()),
                    });
                    values.getsalesorderammendedsummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        //barchart//
        public void DaGetsalesorderadminbarchart(string user_gid, MdlAdmindashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            string YearLabel = DateTime.Now.Year.ToString();

            msSQL = " select year(salesorder_date) as year ,MONTHNAME(salesorder_date) month_name,sum(total_amount) as total_amount from smr_trn_tsalesorder where salesorder_date like '%" + YearLabel + "%'  and branch_gid='" + lsbranch_gid + "' " +
                    " group by year(salesorder_date),month(salesorder_date) order by year(salesorder_date),month(salesorder_date)  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getsalesorderadminbarchart_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getsalesorderadminbarchart_list
                    {


                        year = (dt["year"].ToString()),
                        month_name = (dt["month_name"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),


                    });
                    values.getsalesorderadminbarchart_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
    }
}


