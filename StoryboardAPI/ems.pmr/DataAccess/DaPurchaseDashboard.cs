using System;
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
    public class DaPurchaseDashboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code, lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;
        ////////////////////////// //PURCHASE ADMIN DASHBOARD START/////////////////////////////////////////
        public void DaGetpurchaserequisitionpendingcount(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select Count(purchaserequisition_gid) as purchaserequisition_gid from pmr_trn_tpurchaserequisition where purchaserequisition_flag in ('PR Pending Approval','Pending New Product') and branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaserequisitionpendingcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaserequisitionpendingcount_list
                    {
                        purchaserequisition_gid = (dt["purchaserequisition_gid"].ToString()),

                    });
                    values.getpurchaserequisitionpendingcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetpurchaserequisitionapprovedcount(string user_gid, MdlPurchaseDashboard values)
        {

            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select Count(purchaserequisition_gid) as purchaserequisition_gid from pmr_trn_tpurchaserequisition where purchaserequisition_flag in ('PR Approved','PI Approved') and branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaserequisitionaprovedcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaserequisitionaprovedcount_list
                    {
                        purchaserequisition_gid = (dt["purchaserequisition_gid"].ToString()),

                    });
                    values.getpurchaserequisitionaprovedcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetpurchaserequisitionrejectedcount(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select Count(purchaserequisition_gid) as purchaserequisition_gid from pmr_trn_tpurchaserequisition where purchaserequisition_flag in ('PR Rejected','PR Canceled') and branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaserequisitionrejectedcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaserequisitionrejectedcount_list
                    {
                        purchaserequisition_gid = (dt["purchaserequisition_gid"].ToString()),

                    });
                    values.getpurchaserequisitionrejectedcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetpurchaseorderpendingcount(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select Count(purchaseorder_gid) as purchaseorder_gid from pmr_trn_tpurchaseorder  where purchaseorder_flag='PO Approval Pending' and branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderpendingcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderpendingcount_list
                    {
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),

                    });
                    values.getpurchaseorderpendingcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetpurchaseorderapprovedcount(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select Count(purchaseorder_gid) as purchaseorder_gid from pmr_trn_tpurchaseorder  where purchaseorder_flag='PO Approved' and branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderapprovedcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderapprovedcount_list
                    {
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),

                    });
                    values.getpurchaseorderapprovedcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetpurchaseorderrejectedcount(string user_gid, MdlPurchaseDashboard values)
        {

            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select Count(purchaseorder_gid) as purchaseorder_gid  from pmr_trn_tpurchaseorder  where purchaseorder_flag in ('PO Canceled','PO Rejected') and branch_gid='" + lsbranch_gid + "' ";
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
                    values.getpurchaseorderrejectedcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetpurchaseorderammendedcount(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select Count(purchaseorder_gid) as purchaseorder_gid from pmr_trn_tpurchaseorder  where purchaseorder_flag='PO Amended' and branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderammendedcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderammendedcount_list
                    {
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),

                    });
                    values.getpurchaseorderammendedcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetgrnapprovedcount(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(grn_gid) as grn_gid from pmr_trn_tgrn where grn_status='GRN Approved' and branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrnapprovedcount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrnapprovedcount_list
                    {
                        grn_gid = (dt["grn_gid"].ToString()),

                    });
                    values.getgrnapprovedcount_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetgrnlinktocompletedcount(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select count(grn_gid) as grn_gid from pmr_trn_tgrn where grn_status='GRNLinkPO Completed' and branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrnlinktocompleted_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrnlinktocompleted_list
                    {
                        grn_gid = (dt["grn_gid"].ToString()),

                    });
                    values.getgrnlinktocompleted_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetgrncancelcount(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select count(grn_gid) as grn_gid from pmr_trn_tgrn where grn_status='GRN Cancelled' and branch_gid='" + lsbranch_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrncancel_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrncancel_list
                    {
                        grn_gid = (dt["grn_gid"].ToString()),

                    });
                    values.getgrncancel_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }



        public void DaGetpurchaserequisitionpendingSummary(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select distinct a.purchaserequisition_gid, DATE_FORMAT(a.purchaserequisition_date, '%d-%m-%Y')as purchaserequisition_date ,g.costcenter_name,concat(e.branch_name,'/',d.department_name) as branch_name,a.purchaserequisition_referencenumber , " +
                    " CASE when a.grn_flag <> 'GRN Pending' then a.grn_flag  when a.purchaseorder_flag <> 'PO Pending' then a.purchaseorder_flag  when a.purchaserequisition_flag='PR Pending Approval' then 'PI Pending Approval' " +
                    " when a.purchaserequisition_flag='PR Approved' then 'PI Approved' when a.purchaserequisition_flag='PR Rejected' then 'PI Rejected' else a.purchaserequisition_flag end as 'overall_status'," +
                    " b.user_firstname, a.purchaserequisition_remarks from pmr_trn_tpurchaserequisition a left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " left join hrm_mst_temployee c on c.user_gid = b.user_gid  left join hrm_mst_tdepartment d on c.department_gid = d.department_gid left join hrm_mst_tbranch e on a.branch_gid = e.branch_gid " +
                    " left join adm_mst_tmodule2employee  f on f.employee_gid = c.employee_gid  left join pmr_mst_tcostcenter g on a.costcenter_gid=g.costcenter_gid where a.purchaserequisition_flag  in ('PR Pending Approval','Pending New Product') and a.branch_gid= '" + lsbranch_gid + "' order by  a.purchaserequisition_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaserequisitionpendingSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaserequisitionpendingSummary_list
                    {



                        purchaserequisition_gid = (dt["purchaserequisition_gid"].ToString()),
                        purchaserequisition_date = (dt["purchaserequisition_date"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        purchaserequisition_referencenumber = (dt["purchaserequisition_referencenumber"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        user_firstname = (dt["user_firstname"].ToString()),
                        purchaserequisition_remarks = (dt["purchaserequisition_remarks"].ToString()),

                    });
                    values.getpurchaserequisitionpendingSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetpurchaserequisitionapprovedSummary(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select distinct a.purchaserequisition_gid, DATE_FORMAT(a.purchaserequisition_date, '%d-%m-%Y')as purchaserequisition_date ,g.costcenter_name,concat(e.branch_name,'/',d.department_name) as branch_name,a.purchaserequisition_referencenumber , " +
                    " CASE when a.grn_flag <> 'GRN Pending' then a.grn_flag  when a.purchaseorder_flag <> 'PO Pending' then a.purchaseorder_flag  when a.purchaserequisition_flag='PR Pending Approval' then 'PI Pending Approval' " +
                    " when a.purchaserequisition_flag='PR Approved' then 'PI Approved' when a.purchaserequisition_flag='PR Rejected' then 'PI Rejected' else a.purchaserequisition_flag end as 'overall_status'," +
                    " b.user_firstname, a.purchaserequisition_remarks from pmr_trn_tpurchaserequisition a left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " left join hrm_mst_temployee c on c.user_gid = b.user_gid  left join hrm_mst_tdepartment d on c.department_gid = d.department_gid left join hrm_mst_tbranch e on a.branch_gid = e.branch_gid " +
                    " left join adm_mst_tmodule2employee  f on f.employee_gid = c.employee_gid  left join pmr_mst_tcostcenter g on a.costcenter_gid=g.costcenter_gid where a.purchaserequisition_flag  in ('PR Approved','PI Approved') and a.branch_gid= '" + lsbranch_gid + "' order by  a.purchaserequisition_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaserequisitionapprovedSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaserequisitionapprovedSummary_list
                    {



                        purchaserequisition_gid = (dt["purchaserequisition_gid"].ToString()),
                        purchaserequisition_date = (dt["purchaserequisition_date"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        purchaserequisition_referencenumber = (dt["purchaserequisition_referencenumber"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        user_firstname = (dt["user_firstname"].ToString()),
                        purchaserequisition_remarks = (dt["purchaserequisition_remarks"].ToString()),

                    });
                    values.getpurchaserequisitionapprovedSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetpurchaserequisitionrejectedSummary(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select distinct a.purchaserequisition_gid, DATE_FORMAT(a.purchaserequisition_date, '%d-%m-%Y')as purchaserequisition_date ,g.costcenter_name,concat(e.branch_name,'/',d.department_name) as branch_name,a.purchaserequisition_referencenumber , " +
                    " CASE when a.grn_flag <> 'GRN Pending' then a.grn_flag  when a.purchaseorder_flag <> 'PO Pending' then a.purchaseorder_flag  when a.purchaserequisition_flag='PR Pending Approval' then 'PI Pending Approval' " +
                    " when a.purchaserequisition_flag='PR Approved' then 'PI Approved' when a.purchaserequisition_flag='PR Rejected' then 'PI Rejected' else a.purchaserequisition_flag end as 'overall_status'," +
                    " b.user_firstname, a.purchaserequisition_remarks from pmr_trn_tpurchaserequisition a left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " left join hrm_mst_temployee c on c.user_gid = b.user_gid  left join hrm_mst_tdepartment d on c.department_gid = d.department_gid left join hrm_mst_tbranch e on a.branch_gid = e.branch_gid " +
                    " left join adm_mst_tmodule2employee  f on f.employee_gid = c.employee_gid  left join pmr_mst_tcostcenter g on a.costcenter_gid=g.costcenter_gid where a.purchaserequisition_flag  in ('PR Rejected','PR Canceled') and a.branch_gid= '" + lsbranch_gid + "' order by  a.purchaserequisition_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaserequisitionrejectedSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaserequisitionrejectedSummary_list
                    {



                        purchaserequisition_gid = (dt["purchaserequisition_gid"].ToString()),
                        purchaserequisition_date = (dt["purchaserequisition_date"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        purchaserequisition_referencenumber = (dt["purchaserequisition_referencenumber"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        user_firstname = (dt["user_firstname"].ToString()),
                        purchaserequisition_remarks = (dt["purchaserequisition_remarks"].ToString()),

                    });
                    values.getpurchaserequisitionrejectedSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetpurchaseorderpendingSummary(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select /*+ MAX_EXECUTION_TIME(600000) */ distinct  Date_add(a.purchaseorder_date,Interval delivery_days day) as ExpectedPODeliveryDate,DATE_FORMAT(a.purchaseorder_date, '%d-%m-%Y')as purchaseorder_date , " +
                    " a.purchaseorder_gid,c.branch_name, h.costcenter_name,   (case when a.currency_code is null then b.vendor_companyname when a.currency_code is not null then " +
                    " concat(b.vendor_companyname,'/',a.currency_code) end)  as vendor_companyname,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag" +
                    " when a.grn_flag <> 'GRN Pending' then a.grn_flag else a.purchaseorder_flag end as 'overall_status', a.vendor_status,format(a.total_amount,2) as paymentamount,a.purchaseorder_remarks " +
                    " from pmr_trn_tpurchaseorder a left join  acp_mst_tvendor b on b.vendor_gid = a.vendor_gid left join hrm_mst_tbranch c on c.branch_gid = a.branch_gid " +
                    " left join adm_mst_tuser d on d.user_gid = a.created_by left join hrm_mst_temployee e on e.user_gid = d.user_gid left join adm_mst_tmodule2employee  f on f.employee_gid = e.employee_gid " +
                    " left join pmr_mst_tcostcenter h on h.costcenter_gid=a.costcenter_gid left join pmr_Trn_tpr2po i on i.purchaseorder_gid=a.purchaseorder_gid " +
                    " left join pmr_Trn_tpurchaserequisition j on j.purchaserequisition_gid=i.purchaserequisition_gid where a.purchaseorder_flag='PO Approval Pending'  and a.branch_gid= '" + lsbranch_gid + "' order by  a.purchaseorder_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderpendingSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderpendingSummary_list
                    {


                        ExpectedPODeliveryDate = (dt["ExpectedPODeliveryDate"].ToString()),
                        purchaseorder_date = (dt["purchaseorder_date"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        vendor_status = (dt["vendor_status"].ToString()),
                        paymentamount = (dt["paymentamount"].ToString()),
                        purchaseorder_remarks = (dt["purchaseorder_remarks"].ToString()),

                    });
                    values.getpurchaseorderpendingSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetpurchaseorderapprovedSummary(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select /*+ MAX_EXECUTION_TIME(600000) */ distinct  Date_add(a.purchaseorder_date,Interval delivery_days day) as ExpectedPODeliveryDate,DATE_FORMAT(a.purchaseorder_date, '%d-%m-%Y')as purchaseorder_date , " +
                    " a.purchaseorder_gid,c.branch_name, h.costcenter_name,   (case when a.currency_code is null then b.vendor_companyname when a.currency_code is not null then " +
                    " concat(b.vendor_companyname,'/',a.currency_code) end)  as vendor_companyname,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag" +
                    " when a.grn_flag <> 'GRN Pending' then a.grn_flag else a.purchaseorder_flag end as 'overall_status', a.vendor_status,format(a.total_amount,2) as paymentamount,a.purchaseorder_remarks " +
                    " from pmr_trn_tpurchaseorder a left join  acp_mst_tvendor b on b.vendor_gid = a.vendor_gid left join hrm_mst_tbranch c on c.branch_gid = a.branch_gid " +
                    " left join adm_mst_tuser d on d.user_gid = a.created_by left join hrm_mst_temployee e on e.user_gid = d.user_gid left join adm_mst_tmodule2employee  f on f.employee_gid = e.employee_gid " +
                    " left join pmr_mst_tcostcenter h on h.costcenter_gid=a.costcenter_gid left join pmr_Trn_tpr2po i on i.purchaseorder_gid=a.purchaseorder_gid " +
                    " left join pmr_Trn_tpurchaserequisition j on j.purchaserequisition_gid=i.purchaserequisition_gid where a.purchaseorder_flag='PO Approved'  and a.branch_gid= '" + lsbranch_gid + "' order by  a.purchaseorder_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderapprovedSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderapprovedSummary_list
                    {


                        ExpectedPODeliveryDate = (dt["ExpectedPODeliveryDate"].ToString()),
                        purchaseorder_date = (dt["purchaseorder_date"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        vendor_status = (dt["vendor_status"].ToString()),
                        paymentamount = (dt["paymentamount"].ToString()),
                        purchaseorder_remarks = (dt["purchaseorder_remarks"].ToString()),

                    });
                    values.getpurchaseorderapprovedSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetpurchaseorderammendedSummary(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select /*+ MAX_EXECUTION_TIME(600000) */ distinct  Date_add(a.purchaseorder_date,Interval delivery_days day) as ExpectedPODeliveryDate,DATE_FORMAT(a.purchaseorder_date, '%d-%m-%Y')as purchaseorder_date , " +
                    " a.purchaseorder_gid,c.branch_name, h.costcenter_name,   (case when a.currency_code is null then b.vendor_companyname when a.currency_code is not null then " +
                    " concat(b.vendor_companyname,'/',a.currency_code) end)  as vendor_companyname,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag" +
                    " when a.grn_flag <> 'GRN Pending' then a.grn_flag else a.purchaseorder_flag end as 'overall_status', a.vendor_status,format(a.total_amount,2) as paymentamount,a.purchaseorder_remarks " +
                    " from pmr_trn_tpurchaseorder a left join  acp_mst_tvendor b on b.vendor_gid = a.vendor_gid left join hrm_mst_tbranch c on c.branch_gid = a.branch_gid " +
                    " left join adm_mst_tuser d on d.user_gid = a.created_by left join hrm_mst_temployee e on e.user_gid = d.user_gid left join adm_mst_tmodule2employee  f on f.employee_gid = e.employee_gid " +
                    " left join pmr_mst_tcostcenter h on h.costcenter_gid=a.costcenter_gid left join pmr_Trn_tpr2po i on i.purchaseorder_gid=a.purchaseorder_gid " +
                    " left join pmr_Trn_tpurchaserequisition j on j.purchaserequisition_gid=i.purchaserequisition_gid where a.purchaseorder_flag='PO Amended'  and a.branch_gid= '" + lsbranch_gid + "'order by  a.purchaseorder_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderammendedSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderammendedSummary_list
                    {


                        ExpectedPODeliveryDate = (dt["ExpectedPODeliveryDate"].ToString()),
                        purchaseorder_date = (dt["purchaseorder_date"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        vendor_status = (dt["vendor_status"].ToString()),
                        paymentamount = (dt["paymentamount"].ToString()),
                        purchaseorder_remarks = (dt["purchaseorder_remarks"].ToString()),

                    });
                    values.getpurchaseorderammendedSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetpurchaseorderrejectedSummary(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select /*+ MAX_EXECUTION_TIME(600000) */ distinct  Date_add(a.purchaseorder_date,Interval delivery_days day) as ExpectedPODeliveryDate,DATE_FORMAT(a.purchaseorder_date, '%d-%m-%Y')as purchaseorder_date , " +
                    " a.purchaseorder_gid,c.branch_name, h.costcenter_name,   (case when a.currency_code is null then b.vendor_companyname when a.currency_code is not null then " +
                    " concat(b.vendor_companyname,'/',a.currency_code) end)  as vendor_companyname,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag" +
                    " when a.grn_flag <> 'GRN Pending' then a.grn_flag else a.purchaseorder_flag end as 'overall_status', a.vendor_status,format(a.total_amount,2) as paymentamount,a.purchaseorder_remarks " +
                    " from pmr_trn_tpurchaseorder a left join  acp_mst_tvendor b on b.vendor_gid = a.vendor_gid left join hrm_mst_tbranch c on c.branch_gid = a.branch_gid " +
                    " left join adm_mst_tuser d on d.user_gid = a.created_by left join hrm_mst_temployee e on e.user_gid = d.user_gid left join adm_mst_tmodule2employee  f on f.employee_gid = e.employee_gid " +
                    " left join pmr_mst_tcostcenter h on h.costcenter_gid=a.costcenter_gid left join pmr_Trn_tpr2po i on i.purchaseorder_gid=a.purchaseorder_gid " +
                    " left join pmr_Trn_tpurchaserequisition j on j.purchaserequisition_gid=i.purchaserequisition_gid where a.purchaseorder_flag in ('PO Canceled','PO Rejected')  and a.branch_gid= '" + lsbranch_gid + "' order by  a.purchaseorder_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderrejectedSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderrejectedSummary_list
                    {


                        ExpectedPODeliveryDate = (dt["ExpectedPODeliveryDate"].ToString()),
                        purchaseorder_date = (dt["purchaseorder_date"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        vendor_status = (dt["vendor_status"].ToString()),
                        paymentamount = (dt["paymentamount"].ToString()),
                        purchaseorder_remarks = (dt["purchaseorder_remarks"].ToString()),

                    });
                    values.getpurchaseorderrejectedSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetgrnlinktocompletedSummary(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select distinct DATE_FORMAT(a.grn_date, '%d-%m-%Y') as grn_date ,a.grn_gid,a.purchaseorder_gid, e.purchaserequisition_referencenumber as refrence_no , " +
                    " c.vendor_companyname,f.costcenter_name, format(d.total_amount,2) as po_amount,DATE_FORMAT(a.created_date, '%d-%m-%Y') as created_date,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag  " +
                    " else a.grn_flag end as 'overall_status',a.dc_no from pmr_trn_tgrn a left join pmr_trn_tgrndtl b on a.grn_gid = b.grn_gid left join acp_mst_tvendor c on a.vendor_gid = c.vendor_gid " +
                    "left join pmr_trn_tpurchaseorder d on d.purchaseorder_gid=a.purchaseorder_gid left join pmr_trn_tpurchaserequisition e on e.purchaserequisition_gid=d.purchaserequisition_gid " +
                    " left join pmr_mst_tcostcenter f on d.costcenter_gid=f.costcenter_gid where a.grn_status='GRNLinkPO Completed'  and a.branch_gid= '" + lsbranch_gid + "' order by  a.grn_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrnlinktocompletedSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrnlinktocompletedSummary_list
                    {
                        

                        grn_date = (dt["grn_date"].ToString()),
                        grn_gid = (dt["grn_gid"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        refrence_no = (dt["refrence_no"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        po_amount = (dt["po_amount"].ToString()),
                        created_date = (dt["created_date"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        dc_no = (dt["dc_no"].ToString()),

                    });
                    values.getgrnlinktocompletedSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetgrnapprovedSummary(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select distinct DATE_FORMAT(a.grn_date, '%d-%m-%Y') as grn_date ,a.grn_gid,a.purchaseorder_gid, e.purchaserequisition_referencenumber as refrence_no , " +
                    " c.vendor_companyname,f.costcenter_name, format(d.total_amount,2) as po_amount,DATE_FORMAT(a.created_date, '%d-%m-%Y') as created_date,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag  " +
                    " else a.grn_flag end as 'overall_status',a.dc_no from pmr_trn_tgrn a left join pmr_trn_tgrndtl b on a.grn_gid = b.grn_gid left join acp_mst_tvendor c on a.vendor_gid = c.vendor_gid " +
                    "left join pmr_trn_tpurchaseorder d on d.purchaseorder_gid=a.purchaseorder_gid left join pmr_trn_tpurchaserequisition e on e.purchaserequisition_gid=d.purchaserequisition_gid " +
                    " left join pmr_mst_tcostcenter f on d.costcenter_gid=f.costcenter_gid where a.grn_status='GRN Approved'  and a.branch_gid= '" + lsbranch_gid + "' order by  a.grn_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrnapprovedSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrnapprovedSummary_list
                    {


                        grn_date = (dt["grn_date"].ToString()),
                        grn_gid = (dt["grn_gid"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        refrence_no = (dt["refrence_no"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        po_amount = (dt["po_amount"].ToString()),
                        created_date = (dt["created_date"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        dc_no = (dt["dc_no"].ToString()),

                    });
                    values.getgrnapprovedSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetgrncancelSummary(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select distinct DATE_FORMAT(a.grn_date, '%d-%m-%Y') as grn_date ,a.grn_gid,a.purchaseorder_gid, e.purchaserequisition_referencenumber as refrence_no , " +
                    " c.vendor_companyname,f.costcenter_name, format(d.total_amount,2) as po_amount,DATE_FORMAT(a.created_date, '%d-%m-%Y') as created_date,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag  " +
                    " else a.grn_flag end as 'overall_status',a.dc_no from pmr_trn_tgrn a left join pmr_trn_tgrndtl b on a.grn_gid = b.grn_gid left join acp_mst_tvendor c on a.vendor_gid = c.vendor_gid " +
                    "left join pmr_trn_tpurchaseorder d on d.purchaseorder_gid=a.purchaseorder_gid left join pmr_trn_tpurchaserequisition e on e.purchaserequisition_gid=d.purchaserequisition_gid " +
                    " left join pmr_mst_tcostcenter f on d.costcenter_gid=f.costcenter_gid where a.grn_status='GRN Cancelled'  and a.branch_gid= '" + lsbranch_gid + "' order by  a.grn_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrncancelSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrncancelSummary_list
                    {


                        grn_date = (dt["grn_date"].ToString()),
                        grn_gid = (dt["grn_gid"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        refrence_no = (dt["refrence_no"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        po_amount = (dt["po_amount"].ToString()),
                        created_date = (dt["created_date"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        dc_no = (dt["dc_no"].ToString()),

                    });
                    values.getgrncancelSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetpurchaseorderbarchart(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            string  YearLabel = DateTime.Now.Year.ToString();

            msSQL = " select year(purchaseorder_date) as year ,MONTHNAME(purchaseorder_date) month_name,sum(total_amount) as total_amount from pmr_trn_tpurchaseorder where purchaseorder_date like '%" + YearLabel + "%'  and branch_gid='" + lsbranch_gid + "' " +
                    " group by year(purchaseorder_date),month(purchaseorder_date) order by year(purchaseorder_date),month(purchaseorder_date)  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderbarchart_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderbarchart_list
                    {


                        year = (dt["year"].ToString()),
                        month_name = (dt["month_name"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),
                

                    });
                    values.getpurchaseorderbarchart_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetgrnbarchart(string user_gid, MdlPurchaseDashboard values)
        {
            string lsbranch_gid;
            msSQL = " SELECT branch_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            lsbranch_gid = objdbconn.GetExecuteScalar(msSQL);
            string YearLabel = DateTime.Now.Year.ToString();

            msSQL = "  select year(grn_date) as grn_year ,MONTHNAME(grn_date) as grn_month_name,sum(b.total_amount) as po_amount from pmr_trn_tgrn  a  " +
                    " left join pmr_trn_tpurchaseorder b on a.purchaseorder_list=b.purchaseorder_gid " +
                    "where a.branch_gid='" + lsbranch_gid + "'  and grn_date like '%" + YearLabel + "%' group by year(grn_date)  ,MONTHNAME(grn_date)  order by year(grn_date),month(grn_date)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrnbarchart_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrnbarchart_list
                    {


                        grn_year = (dt["grn_year"].ToString()),
                        grn_month_name = (dt["grn_month_name"].ToString()),
                        po_amount = (dt["po_amount"].ToString()),


                    });
                    values.getgrnbarchart_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        ////////////////////////// //PURCHASE ADMIN DASHBOARD END/////////////////////////////////////////
        ///
        //////////////////////////// //PURCHASE EMPLOYEE DASHBOARD START/////////////////////////////////////////
        public void DaGetpurchaserequisitionpendingcountemployee(string user_gid, MdlPurchaseDashboard values)
        {
           
            msSQL = " select Count(purchaserequisition_gid) as purchaserequisition_gid from pmr_trn_tpurchaserequisition where purchaserequisition_flag in ('PR Pending Approval','Pending New Product') and created_by='" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaserequisitionpendingcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaserequisitionpendingcountemployee_list
                    {
                        purchaserequisition_gid = (dt["purchaserequisition_gid"].ToString()),

                    });
                    values.getpurchaserequisitionpendingcountemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetpurchaserequisitionapprovedcountemployee(string user_gid, MdlPurchaseDashboard values)
        {

           
            msSQL = " select Count(purchaserequisition_gid) as purchaserequisition_gid from pmr_trn_tpurchaserequisition where purchaserequisition_flag in ('PR Approved','PI Approved') and created_by='" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaserequisitionaprovedcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaserequisitionaprovedcountemployee_list
                    {
                        purchaserequisition_gid = (dt["purchaserequisition_gid"].ToString()),

                    });
                    values.getpurchaserequisitionaprovedcountemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetpurchaserequisitionrejectedcountemployee(string user_gid, MdlPurchaseDashboard values)
        {
          
            msSQL = " select Count(purchaserequisition_gid) as purchaserequisition_gid from pmr_trn_tpurchaserequisition where purchaserequisition_flag in ('PR Rejected','PR Canceled') and created_by='" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaserequisitionrejectedcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaserequisitionrejectedcountemployee_list
                    {
                        purchaserequisition_gid = (dt["purchaserequisition_gid"].ToString()),

                    });
                    values.getpurchaserequisitionrejectedcountemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }




        public void DaGetpurchaseorderpendingcountemployee(string user_gid, MdlPurchaseDashboard values)
        {
            
            msSQL = " select Count(purchaseorder_gid) as purchaseorder_gid from pmr_trn_tpurchaseorder  where purchaseorder_flag='PO Approval Pending' and created_by='" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderpendingcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderpendingcountemployee_list
                    {
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),

                    });
                    values.getpurchaseorderpendingcountemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetpurchaseorderapprovedcountemployee(string user_gid, MdlPurchaseDashboard values)
        {
            
            msSQL = " select Count(purchaseorder_gid) as purchaseorder_gid from pmr_trn_tpurchaseorder  where purchaseorder_flag='PO Approved' and created_by='" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderapprovedcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderapprovedcountemployee_list
                    {
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),

                    });
                    values.getpurchaseorderapprovedcountemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetpurchaseorderrejectedcountemployee(string user_gid, MdlPurchaseDashboard values)
        {

        
            msSQL = " select Count(purchaseorder_gid) as purchaseorder_gid  from pmr_trn_tpurchaseorder  where purchaseorder_flag in ('PO Canceled','PO Rejected') and created_by='" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderrejectedcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderrejectedcountemployee_list
                    {
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),

                    });
                    values.getpurchaseorderrejectedcountemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetpurchaseorderammendedcountemployee(string user_gid, MdlPurchaseDashboard values)
        {
            

            msSQL = " select Count(purchaseorder_gid) as purchaseorder_gid from pmr_trn_tpurchaseorder  where purchaseorder_flag='PO Amended' and created_by='" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderammendedcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderammendedcountemployee_list
                    {
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),

                    });
                    values.getpurchaseorderammendedcountemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetgrnapprovedcountemployee(string user_gid, MdlPurchaseDashboard values)
        {
          

            msSQL = " select count(grn_gid) as grn_gid from pmr_trn_tgrn where grn_status='GRN Approved' and user_gid='" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrnapprovedcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrnapprovedcountemployee_list
                    {
                        grn_gid = (dt["grn_gid"].ToString()),

                    });
                    values.getgrnapprovedcountemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetgrnlinktocompletedcountemployee(string user_gid, MdlPurchaseDashboard values)
        {
           
            msSQL = " select count(grn_gid) as grn_gid from pmr_trn_tgrn where grn_status='GRNLinkPO Completed' and user_gid='" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrnlinktocompletedemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrnlinktocompletedemployee_list
                    {
                        grn_gid = (dt["grn_gid"].ToString()),

                    });
                    values.getgrnlinktocompletedemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetgrncancelcountemployee(string user_gid, MdlPurchaseDashboard values)
        {
        
            msSQL = " select count(grn_gid) as grn_gid from pmr_trn_tgrn where grn_status='GRN Cancelled' and user_gid='" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrncancelemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrncancelemployee_list
                    {
                        grn_gid = (dt["grn_gid"].ToString()),

                    });
                    values.getgrncancelemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }



        public void DaGetpurchaserequisitionpendingSummaryemployee(string user_gid, MdlPurchaseDashboard values)
        {
            
            msSQL = " select distinct a.purchaserequisition_gid, DATE_FORMAT(a.purchaserequisition_date, '%d-%m-%Y')as purchaserequisition_date ,g.costcenter_name,concat(e.branch_name,'/',d.department_name) as branch_name,a.purchaserequisition_referencenumber , " +
                    " CASE when a.grn_flag <> 'GRN Pending' then a.grn_flag  when a.purchaseorder_flag <> 'PO Pending' then a.purchaseorder_flag  when a.purchaserequisition_flag='PR Pending Approval' then 'PI Pending Approval' " +
                    " when a.purchaserequisition_flag='PR Approved' then 'PI Approved' when a.purchaserequisition_flag='PR Rejected' then 'PI Rejected' else a.purchaserequisition_flag end as 'overall_status'," +
                    " b.user_firstname, a.purchaserequisition_remarks from pmr_trn_tpurchaserequisition a left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " left join hrm_mst_temployee c on c.user_gid = b.user_gid  left join hrm_mst_tdepartment d on c.department_gid = d.department_gid left join hrm_mst_tbranch e on a.branch_gid = e.branch_gid " +
                    " left join adm_mst_tmodule2employee  f on f.employee_gid = c.employee_gid  left join pmr_mst_tcostcenter g on a.costcenter_gid=g.costcenter_gid where a.purchaserequisition_flag  in ('PR Pending Approval','Pending New Product') and a.created_by='" + user_gid + "' order by  a.purchaserequisition_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaserequisitionpendingSummaryemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaserequisitionpendingSummaryemployee_list
                    {



                        purchaserequisition_gid = (dt["purchaserequisition_gid"].ToString()),
                        purchaserequisition_date = (dt["purchaserequisition_date"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        purchaserequisition_referencenumber = (dt["purchaserequisition_referencenumber"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        user_firstname = (dt["user_firstname"].ToString()),
                        purchaserequisition_remarks = (dt["purchaserequisition_remarks"].ToString()),

                    });
                    values.getpurchaserequisitionpendingSummaryemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetpurchaserequisitionapprovedSummaryemployee(string user_gid, MdlPurchaseDashboard values)
        {
           
            msSQL = " select distinct a.purchaserequisition_gid, DATE_FORMAT(a.purchaserequisition_date, '%d-%m-%Y')as purchaserequisition_date ,g.costcenter_name,concat(e.branch_name,'/',d.department_name) as branch_name,a.purchaserequisition_referencenumber , " +
                    " CASE when a.grn_flag <> 'GRN Pending' then a.grn_flag  when a.purchaseorder_flag <> 'PO Pending' then a.purchaseorder_flag  when a.purchaserequisition_flag='PR Pending Approval' then 'PI Pending Approval' " +
                    " when a.purchaserequisition_flag='PR Approved' then 'PI Approved' when a.purchaserequisition_flag='PR Rejected' then 'PI Rejected' else a.purchaserequisition_flag end as 'overall_status'," +
                    " b.user_firstname, a.purchaserequisition_remarks from pmr_trn_tpurchaserequisition a left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " left join hrm_mst_temployee c on c.user_gid = b.user_gid  left join hrm_mst_tdepartment d on c.department_gid = d.department_gid left join hrm_mst_tbranch e on a.branch_gid = e.branch_gid " +
                    " left join adm_mst_tmodule2employee  f on f.employee_gid = c.employee_gid  left join pmr_mst_tcostcenter g on a.costcenter_gid=g.costcenter_gid where a.purchaserequisition_flag  in ('PR Approved','PI Approved') and a.created_by='" + user_gid + "' order by  a.purchaserequisition_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaserequisitionapprovedSummaryemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaserequisitionapprovedSummaryemployee_list
                    {



                        purchaserequisition_gid = (dt["purchaserequisition_gid"].ToString()),
                        purchaserequisition_date = (dt["purchaserequisition_date"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        purchaserequisition_referencenumber = (dt["purchaserequisition_referencenumber"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        user_firstname = (dt["user_firstname"].ToString()),
                        purchaserequisition_remarks = (dt["purchaserequisition_remarks"].ToString()),

                    });
                    values.getpurchaserequisitionapprovedSummaryemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetpurchaserequisitionrejectedSummaryemployee(string user_gid, MdlPurchaseDashboard values)
        {

            msSQL = " select distinct a.purchaserequisition_gid, DATE_FORMAT(a.purchaserequisition_date, '%d-%m-%Y')as purchaserequisition_date ,g.costcenter_name,concat(e.branch_name,'/',d.department_name) as branch_name,a.purchaserequisition_referencenumber , " +
                    " CASE when a.grn_flag <> 'GRN Pending' then a.grn_flag  when a.purchaseorder_flag <> 'PO Pending' then a.purchaseorder_flag  when a.purchaserequisition_flag='PR Pending Approval' then 'PI Pending Approval' " +
                    " when a.purchaserequisition_flag='PR Approved' then 'PI Approved' when a.purchaserequisition_flag='PR Rejected' then 'PI Rejected' else a.purchaserequisition_flag end as 'overall_status'," +
                    " b.user_firstname, a.purchaserequisition_remarks from pmr_trn_tpurchaserequisition a left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " left join hrm_mst_temployee c on c.user_gid = b.user_gid  left join hrm_mst_tdepartment d on c.department_gid = d.department_gid left join hrm_mst_tbranch e on a.branch_gid = e.branch_gid " +
                    " left join adm_mst_tmodule2employee  f on f.employee_gid = c.employee_gid  left join pmr_mst_tcostcenter g on a.costcenter_gid=g.costcenter_gid where a.purchaserequisition_flag  in ('PR Rejected','PR Canceled') and a.created_by='" + user_gid + "' order by  a.purchaserequisition_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaserequisitionrejectedSummaryemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaserequisitionrejectedSummaryemployee_list
                    {



                        purchaserequisition_gid = (dt["purchaserequisition_gid"].ToString()),
                        purchaserequisition_date = (dt["purchaserequisition_date"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        purchaserequisition_referencenumber = (dt["purchaserequisition_referencenumber"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        user_firstname = (dt["user_firstname"].ToString()),
                        purchaserequisition_remarks = (dt["purchaserequisition_remarks"].ToString()),

                    });
                    values.getpurchaserequisitionrejectedSummaryemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetpurchaseorderpendingSummaryemployee(string user_gid, MdlPurchaseDashboard values)
        {
            
            msSQL = " select /*+ MAX_EXECUTION_TIME(600000) */ distinct  Date_add(a.purchaseorder_date,Interval delivery_days day) as ExpectedPODeliveryDate,DATE_FORMAT(a.purchaseorder_date, '%d-%m-%Y')as purchaseorder_date , " +
                    " a.purchaseorder_gid,c.branch_name, h.costcenter_name,   (case when a.currency_code is null then b.vendor_companyname when a.currency_code is not null then " +
                    " concat(b.vendor_companyname,'/',a.currency_code) end)  as vendor_companyname,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag" +
                    " when a.grn_flag <> 'GRN Pending' then a.grn_flag else a.purchaseorder_flag end as 'overall_status', a.vendor_status,format(a.total_amount,2) as paymentamount,a.purchaseorder_remarks " +
                    " from pmr_trn_tpurchaseorder a left join  acp_mst_tvendor b on b.vendor_gid = a.vendor_gid left join hrm_mst_tbranch c on c.branch_gid = a.branch_gid " +
                    " left join adm_mst_tuser d on d.user_gid = a.created_by left join hrm_mst_temployee e on e.user_gid = d.user_gid left join adm_mst_tmodule2employee  f on f.employee_gid = e.employee_gid " +
                    " left join pmr_mst_tcostcenter h on h.costcenter_gid=a.costcenter_gid left join pmr_Trn_tpr2po i on i.purchaseorder_gid=a.purchaseorder_gid " +
                    " left join pmr_Trn_tpurchaserequisition j on j.purchaserequisition_gid=i.purchaserequisition_gid where a.purchaseorder_flag='PO Approval Pending'  and a.created_by='" + user_gid + "' order by  a.purchaseorder_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderpendingSummaryemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderpendingSummaryemployee_list
                    {


                        ExpectedPODeliveryDate = (dt["ExpectedPODeliveryDate"].ToString()),
                        purchaseorder_date = (dt["purchaseorder_date"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        vendor_status = (dt["vendor_status"].ToString()),
                        paymentamount = (dt["paymentamount"].ToString()),
                        purchaseorder_remarks = (dt["purchaseorder_remarks"].ToString()),

                    });
                    values.getpurchaseorderpendingSummaryemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetpurchaseorderapprovedSummaryemployee(string user_gid, MdlPurchaseDashboard values)
        {
         
            msSQL = " select /*+ MAX_EXECUTION_TIME(600000) */ distinct  Date_add(a.purchaseorder_date,Interval delivery_days day) as ExpectedPODeliveryDate,DATE_FORMAT(a.purchaseorder_date, '%d-%m-%Y')as purchaseorder_date , " +
                    " a.purchaseorder_gid,c.branch_name, h.costcenter_name,   (case when a.currency_code is null then b.vendor_companyname when a.currency_code is not null then " +
                    " concat(b.vendor_companyname,'/',a.currency_code) end)  as vendor_companyname,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag" +
                    " when a.grn_flag <> 'GRN Pending' then a.grn_flag else a.purchaseorder_flag end as 'overall_status', a.vendor_status,format(a.total_amount,2) as paymentamount,a.purchaseorder_remarks " +
                    " from pmr_trn_tpurchaseorder a left join  acp_mst_tvendor b on b.vendor_gid = a.vendor_gid left join hrm_mst_tbranch c on c.branch_gid = a.branch_gid " +
                    " left join adm_mst_tuser d on d.user_gid = a.created_by left join hrm_mst_temployee e on e.user_gid = d.user_gid left join adm_mst_tmodule2employee  f on f.employee_gid = e.employee_gid " +
                    " left join pmr_mst_tcostcenter h on h.costcenter_gid=a.costcenter_gid left join pmr_Trn_tpr2po i on i.purchaseorder_gid=a.purchaseorder_gid " +
                    " left join pmr_Trn_tpurchaserequisition j on j.purchaserequisition_gid=i.purchaserequisition_gid where a.purchaseorder_flag='PO Approved'  and a.created_by='" + user_gid + "' order by  a.purchaseorder_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderapprovedSummaryemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderapprovedSummaryemployee_list
                    {


                        ExpectedPODeliveryDate = (dt["ExpectedPODeliveryDate"].ToString()),
                        purchaseorder_date = (dt["purchaseorder_date"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        vendor_status = (dt["vendor_status"].ToString()),
                        paymentamount = (dt["paymentamount"].ToString()),
                        purchaseorder_remarks = (dt["purchaseorder_remarks"].ToString()),

                    });
                    values.getpurchaseorderapprovedSummaryemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetpurchaseorderammendedSummaryemployee(string user_gid, MdlPurchaseDashboard values)
        {
        
            msSQL = " select /*+ MAX_EXECUTION_TIME(600000) */ distinct  Date_add(a.purchaseorder_date,Interval delivery_days day) as ExpectedPODeliveryDate,DATE_FORMAT(a.purchaseorder_date, '%d-%m-%Y')as purchaseorder_date , " +
                    " a.purchaseorder_gid,c.branch_name, h.costcenter_name,   (case when a.currency_code is null then b.vendor_companyname when a.currency_code is not null then " +
                    " concat(b.vendor_companyname,'/',a.currency_code) end)  as vendor_companyname,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag" +
                    " when a.grn_flag <> 'GRN Pending' then a.grn_flag else a.purchaseorder_flag end as 'overall_status', a.vendor_status,format(a.total_amount,2) as paymentamount,a.purchaseorder_remarks " +
                    " from pmr_trn_tpurchaseorder a left join  acp_mst_tvendor b on b.vendor_gid = a.vendor_gid left join hrm_mst_tbranch c on c.branch_gid = a.branch_gid " +
                    " left join adm_mst_tuser d on d.user_gid = a.created_by left join hrm_mst_temployee e on e.user_gid = d.user_gid left join adm_mst_tmodule2employee  f on f.employee_gid = e.employee_gid " +
                    " left join pmr_mst_tcostcenter h on h.costcenter_gid=a.costcenter_gid left join pmr_Trn_tpr2po i on i.purchaseorder_gid=a.purchaseorder_gid " +
                    " left join pmr_Trn_tpurchaserequisition j on j.purchaserequisition_gid=i.purchaserequisition_gid where a.purchaseorder_flag='PO Amended'  and a.created_by='" + user_gid + "' order by  a.purchaseorder_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderammendedSummaryemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderammendedSummaryemployee_list
                    {


                        ExpectedPODeliveryDate = (dt["ExpectedPODeliveryDate"].ToString()),
                        purchaseorder_date = (dt["purchaseorder_date"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        vendor_status = (dt["vendor_status"].ToString()),
                        paymentamount = (dt["paymentamount"].ToString()),
                        purchaseorder_remarks = (dt["purchaseorder_remarks"].ToString()),

                    });
                    values.getpurchaseorderammendedSummaryemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetpurchaseorderrejectedSummaryemployee(string user_gid, MdlPurchaseDashboard values)
        {

            msSQL = " select /*+ MAX_EXECUTION_TIME(600000) */ distinct  Date_add(a.purchaseorder_date,Interval delivery_days day) as ExpectedPODeliveryDate,DATE_FORMAT(a.purchaseorder_date, '%d-%m-%Y')as purchaseorder_date , " +
                    " a.purchaseorder_gid,c.branch_name, h.costcenter_name,   (case when a.currency_code is null then b.vendor_companyname when a.currency_code is not null then " +
                    " concat(b.vendor_companyname,'/',a.currency_code) end)  as vendor_companyname,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag" +
                    " when a.grn_flag <> 'GRN Pending' then a.grn_flag else a.purchaseorder_flag end as 'overall_status', a.vendor_status,format(a.total_amount,2) as paymentamount,a.purchaseorder_remarks " +
                    " from pmr_trn_tpurchaseorder a left join  acp_mst_tvendor b on b.vendor_gid = a.vendor_gid left join hrm_mst_tbranch c on c.branch_gid = a.branch_gid " +
                    " left join adm_mst_tuser d on d.user_gid = a.created_by left join hrm_mst_temployee e on e.user_gid = d.user_gid left join adm_mst_tmodule2employee  f on f.employee_gid = e.employee_gid " +
                    " left join pmr_mst_tcostcenter h on h.costcenter_gid=a.costcenter_gid left join pmr_Trn_tpr2po i on i.purchaseorder_gid=a.purchaseorder_gid " +
                    " left join pmr_Trn_tpurchaserequisition j on j.purchaserequisition_gid=i.purchaserequisition_gid where a.purchaseorder_flag in ('PO Canceled','PO Rejected')  and a.created_by='" + user_gid + "' order by  a.purchaseorder_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getpurchaseorderrejectedSummaryemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getpurchaseorderrejectedSummaryemployee_list
                    {


                        ExpectedPODeliveryDate = (dt["ExpectedPODeliveryDate"].ToString()),
                        purchaseorder_date = (dt["purchaseorder_date"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        branch_name = (dt["branch_name"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        vendor_status = (dt["vendor_status"].ToString()),
                        paymentamount = (dt["paymentamount"].ToString()),
                        purchaseorder_remarks = (dt["purchaseorder_remarks"].ToString()),

                    });
                    values.getpurchaseorderrejectedSummaryemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetgrnlinktocompletedSummaryemployee(string user_gid, MdlPurchaseDashboard values)
        {
         
            msSQL = " select distinct DATE_FORMAT(a.grn_date, '%d-%m-%Y') as grn_date ,a.grn_gid,a.purchaseorder_gid, e.purchaserequisition_referencenumber as refrence_no , " +
                    " c.vendor_companyname,f.costcenter_name, format(d.total_amount,2) as po_amount,DATE_FORMAT(a.created_date, '%d-%m-%Y') as created_date,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag  " +
                    " else a.grn_flag end as 'overall_status',a.dc_no from pmr_trn_tgrn a left join pmr_trn_tgrndtl b on a.grn_gid = b.grn_gid left join acp_mst_tvendor c on a.vendor_gid = c.vendor_gid " +
                    "left join pmr_trn_tpurchaseorder d on d.purchaseorder_gid=a.purchaseorder_gid left join pmr_trn_tpurchaserequisition e on e.purchaserequisition_gid=d.purchaserequisition_gid " +
                    " left join pmr_mst_tcostcenter f on d.costcenter_gid=f.costcenter_gid where a.grn_status='GRNLinkPO Completed'  and a.user_gid='" + user_gid + "' order by  a.grn_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrnlinktocompletedSummaryemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrnlinktocompletedSummaryemployee_list
                    {


                        grn_date = (dt["grn_date"].ToString()),
                        grn_gid = (dt["grn_gid"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        refrence_no = (dt["refrence_no"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        po_amount = (dt["po_amount"].ToString()),
                        created_date = (dt["created_date"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        dc_no = (dt["dc_no"].ToString()),

                    });
                    values.getgrnlinktocompletedSummaryemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetgrnapprovedSummaryemployee(string user_gid, MdlPurchaseDashboard values)
        {
           
            msSQL = " select distinct DATE_FORMAT(a.grn_date, '%d-%m-%Y') as grn_date ,a.grn_gid,a.purchaseorder_gid, e.purchaserequisition_referencenumber as refrence_no , " +
                    " c.vendor_companyname,f.costcenter_name, format(d.total_amount,2) as po_amount,DATE_FORMAT(a.created_date, '%d-%m-%Y') as created_date,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag  " +
                    " else a.grn_flag end as 'overall_status',a.dc_no from pmr_trn_tgrn a left join pmr_trn_tgrndtl b on a.grn_gid = b.grn_gid left join acp_mst_tvendor c on a.vendor_gid = c.vendor_gid " +
                    "left join pmr_trn_tpurchaseorder d on d.purchaseorder_gid=a.purchaseorder_gid left join pmr_trn_tpurchaserequisition e on e.purchaserequisition_gid=d.purchaserequisition_gid " +
                    " left join pmr_mst_tcostcenter f on d.costcenter_gid=f.costcenter_gid where a.grn_status='GRN Approved'  and a.user_gid='" + user_gid + "' order by  a.grn_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrnapprovedSummaryemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrnapprovedSummaryemployee_list
                    {


                        grn_date = (dt["grn_date"].ToString()),
                        grn_gid = (dt["grn_gid"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        refrence_no = (dt["refrence_no"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        po_amount = (dt["po_amount"].ToString()),
                        created_date = (dt["created_date"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        dc_no = (dt["dc_no"].ToString()),

                    });
                    values.getgrnapprovedSummaryemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetgrncancelSummaryemployee(string user_gid, MdlPurchaseDashboard values)
        {
          
            msSQL = " select distinct DATE_FORMAT(a.grn_date, '%d-%m-%Y') as grn_date ,a.grn_gid,a.purchaseorder_gid, e.purchaserequisition_referencenumber as refrence_no , " +
                    " c.vendor_companyname,f.costcenter_name, format(d.total_amount,2) as po_amount,DATE_FORMAT(a.created_date, '%d-%m-%Y') as created_date,CASE when a.invoice_flag <> 'IV Pending' then a.invoice_flag  " +
                    " else a.grn_flag end as 'overall_status',a.dc_no from pmr_trn_tgrn a left join pmr_trn_tgrndtl b on a.grn_gid = b.grn_gid left join acp_mst_tvendor c on a.vendor_gid = c.vendor_gid " +
                    "left join pmr_trn_tpurchaseorder d on d.purchaseorder_gid=a.purchaseorder_gid left join pmr_trn_tpurchaserequisition e on e.purchaserequisition_gid=d.purchaserequisition_gid " +
                    " left join pmr_mst_tcostcenter f on d.costcenter_gid=f.costcenter_gid where a.grn_status='GRN Cancelled'  and a.user_gid='" + user_gid + "' order by  a.grn_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<getgrncancelSummaryemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new getgrncancelSummaryemployee_list
                    {


                        grn_date = (dt["grn_date"].ToString()),
                        grn_gid = (dt["grn_gid"].ToString()),
                        purchaseorder_gid = (dt["purchaseorder_gid"].ToString()),
                        refrence_no = (dt["refrence_no"].ToString()),
                        vendor_companyname = (dt["vendor_companyname"].ToString()),
                        costcenter_name = (dt["costcenter_name"].ToString()),
                        po_amount = (dt["po_amount"].ToString()),
                        created_date = (dt["created_date"].ToString()),
                        overall_status = (dt["overall_status"].ToString()),
                        dc_no = (dt["dc_no"].ToString()),

                    });
                    values.getgrncancelSummaryemployee_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
    }
}