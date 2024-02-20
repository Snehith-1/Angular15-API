using ems.crm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;

namespace ems.crm.DataAccess
{
    public class DaEmployeeDashboard
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        public void DaGetsalesordrependingcountemployee(string user_gid, MdlEmployeedashboard values)
        {

            msSQL = "select count(salesorder_gid)as salesorder_gid,so_referenceno1" +
                    ",salesorder_date,customer_contact_person,total_amount from smr_trn_tsalesorder" +
                    " where salesorder_status = 'Invoice Raised' and created_by = '" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getsalesordrependingcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getsalesordrependingcountemployee_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.Getsalesordrependingcountemployeelist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetsalesorderapprovedcountemployee(string user_gid, MdlEmployeedashboard values)
        {

            msSQL = "select count(salesorder_gid)as salesorder_gid,so_referenceno1 " +
                    " ,salesorder_date,customer_contact_person,total_amount from smr_trn_tsalesorder" +
                    " where salesorder_status = 'Approved' and created_by = '" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getsalesorderapprovedcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getsalesorderapprovedcountemployee_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.Getsalesorderapprovedcountemployeelist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetsalesorderrejectedcountemployee(string user_gid, MdlEmployeedashboard values)
        {

            msSQL = "select count(salesorder_gid)as salesorder_gid,so_referenceno1 " +
                    ",salesorder_date,customer_contact_person,total_amount from smr_trn_tsalesorder" +
                    " where salesorder_status = 'Cancelled'and created_by = '" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getsalesorderrejectedcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getsalesorderrejectedcountemployee_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.Getsalesorderrejectedcountemployeelist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetsalesorderincountemployee(string user_gid, MdlEmployeedashboard values)
        {

            msSQL = "select count(salesorder_gid)as salesorder_gid,so_referenceno1  " +
                    ",salesorder_date,customer_contact_person,total_amount from smr_trn_tsalesorder" +
                    " where salesorder_status = 'Advance Paid' and created_by = '" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getsalesorderincountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getsalesorderincountemployee_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.Getsalesorderincountemployeelist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetsalesCurrentorderapprovedcountemployee(string user_gid, MdlEmployeedashboard values)
        {

            msSQL = " select count(salesorder_gid)as salesorder_gid,so_referenceno1  " +
                    ",salesorder_date,customer_contact_person,total_amount from smr_trn_tsalesorder" +
                    " where invoice_flag = 'Invoice Inprogress'and created_by = '" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetsalesCurrentorderapprovedcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetsalesCurrentorderapprovedcountemployee_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.GetsalesCurrentorderapprovedcountemployeelist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetsalesorderammendedcountemployee(string user_gid, MdlEmployeedashboard values)
        {

            msSQL = " select count(salesorder_gid)as salesorder_gid,so_referenceno1, " +
                    "salesorder_date,customer_contact_person,total_amount from smr_trn_tsalesorder" +
                    " where salesorder_status = 'SO Amended' and created_by = '" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<Getsalesorderammendedcountemployee_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new Getsalesorderammendedcountemployee_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),

                    });
                    values.Getsalesorderammendedcountemployeelist = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }








        public void DaGetSalesorderpendingemployeeSummary(string user_gid, MdlEmployeedashboard values)
        {
            msSQL = " select salesorder_gid,so_referenceno1, " +
                     " date_format(salesorder_date,'%y-%m-%Y')as salesorder_date,customer_contact_person,total_amount from smr_trn_tsalesorder" +
                     " where salesorder_status = 'Invoice Raised' and created_by = '" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetSalesorderpendingemployeeSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetSalesorderpendingemployeeSummary_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),
                        so_referenceno1 = (dt["so_referenceno1"].ToString()),
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        customer_contact_person = (dt["customer_contact_person"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),
                    });
                    values.GetSalesorderpendingemployeeSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetSalesorderapprovedemployeeSummary(string user_gid, MdlEmployeedashboard values)
        {
            msSQL = " select salesorder_gid,so_referenceno1, " +
                     "date_format(salesorder_date,'%y-%m-%Y')as salesorder_date,customer_contact_person,total_amount from smr_trn_tsalesorder" +
                     " where salesorder_status = 'Approved' and created_by = '" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetSalesorderapprovedemployeeSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetSalesorderapprovedemployeeSummary_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),
                        so_referenceno1 = (dt["so_referenceno1"].ToString()),
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        customer_contact_person = (dt["customer_contact_person"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),
                    });
                    values.GetSalesorderapprovedemployeeSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetSalesorderinprogressemployeeSummary(string user_gid, MdlEmployeedashboard values)
        {
            msSQL = "select salesorder_gid,so_referenceno1," +
                     "date_format(salesorder_date,'%y-%m-%Y')as salesorder_date,customer_contact_person,total_amount from smr_trn_tsalesorder" +
                     " where salesorder_status = 'Advance Paid' and created_by = '" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetSalesorderinprogressemployeeSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetSalesorderinprogressemployeeSummary_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),
                        so_referenceno1 = (dt["so_referenceno1"].ToString()),
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        customer_contact_person = (dt["customer_contact_person"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),
                    });
                    values.GetSalesorderinprogressemployeeSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetSalesorderRejectedemployeeSummary(string user_gid, MdlEmployeedashboard values)
        {
            msSQL = "select salesorder_gid,so_referenceno1, " +
                     "date_format(salesorder_date,'%y-%m-%Y')as salesorder_date ,customer_contact_person,total_amount from smr_trn_tsalesorder" +
                     " where salesorder_status = 'Cancelled' and created_by = '" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetSalesorderRejectedemployeeSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetSalesorderRejectedemployeeSummary_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),
                        so_referenceno1 = (dt["so_referenceno1"].ToString()),
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        customer_contact_person = (dt["customer_contact_person"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),
                    });
                    values.GetSalesorderRejectedemployeeSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetSalesorderammendedemployeeSummary(string user_gid, MdlEmployeedashboard values)
        {
            msSQL = "select salesorder_gid,so_referenceno1, " +
                     "date_format(salesorder_date,'%y-%m-%Y')as salesorder_date,customer_contact_person,total_amount from smr_trn_tsalesorder" +
                     "  where salesorder_status = 'SO Amended'and created_by = '" + user_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<GetSalesorderammendedemployeeSummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new GetSalesorderammendedemployeeSummary_list
                    {
                        salesorder_gid = (dt["salesorder_gid"].ToString()),
                        so_referenceno1 = (dt["so_referenceno1"].ToString()),
                        salesorder_date = (dt["salesorder_date"].ToString()),
                        customer_contact_person = (dt["customer_contact_person"].ToString()),
                        total_amount = (dt["total_amount"].ToString()),
                    });
                    values.GetSalesorderammendedemployeeSummary_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }

    }
}