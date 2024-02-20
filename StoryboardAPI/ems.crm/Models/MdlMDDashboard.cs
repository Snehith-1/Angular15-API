using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    //public class result
    //{
    //    public bool status { get; set; }
    //    public string message { get; set; }
    //}
    public class MdlMDDashboard : result
    {


        public List<gettotalsalesorderpendingcount_list> getsalesordercount_list { get; set; }
        public List<gettotalsalesorderaprovedcount_list> gettotalsalesorderaprovedcountlist { get; set; }
        public List<gettotalsalesorderinprogresscount_list> gettotalsalesorderinprogresscountlist { get; set; }
        public List<gettotalsalesorderrejectedcount_list> gettotalsalesorderrejectedcountlist { get; set; }

        public List<getpurchaseorderrejectedcount_list> getpurchaseorderrejectedcountlist { get; set; }
        public List<getsalesorderbarchart_list> getsalesorderbarchartlist { get; set; }

        public List<gettotalsalesordervalue_list> gettotalsalesordervaluelist { get; set; }



        //public List<gettotalsalesorderinprogresscount_list> gettotalsalesorderinprogresscountlist { get; set; }


        public List<getcurrentyeartotalsalesorderrejectedcount_list> getcurrentyeartotalsalesorderrejectedcountlist { get; set; }

        public List<getsalesordersummaryemployeeList> getsalesordersummaryemployee_List { get; set; }





    }


    public class gettotalsalesorderpendingcount_list
    {
        public string salesorder_gid { get; set; }
    }

    public class getpurchaseorderrejectedcount_list
    {
        public string purchaseorder_gid { get; set; }
    }

    public class gettotalsalesorderaprovedcount_list
    {
        public string salesorder_gid { get; set; }
    }
    public class gettotalsalesorderrejectedcount_list
    {
        public string salesorder_gid { get; set; }
    }
    public class gettotalsalesorderinprogresscount_list
    {
        public string salesorder_gid { get; set; }
    }
    public class getcurrentyeartotalsalesorderrejectedcount_list
    {
        public string salesorder_gid { get; set; }
    }
    public class getcurrentyearrejectedcount_list
    {
        public string salesorder_gid { get; set; }
    }
    public class getpurchaserequisitionpendingcountemployee_list
    {
        public string purchaserequisition_gid { get; set; }
    }

    public class getpurchaserequisitionaprovedcountemployee_list
    {
        public string purchaserequisition_gid { get; set; }
    }
    
    public class getsalesordersummaryemployeeList
    {

        public string salesorderNo { get; set; }
        public string salesorderdate { get; set; }
        public string salesorder_gid { get; set; }
        public string customername { get; set; }
        public string Grandtotal { get; set; }
        public string advance { get; set; }
        public string user_firstname { get; set; }
        public string purchaserequisition_remarks { get; set; }

    }

    public class getpurchaserequisitionapprovedSummary_list
    {

        public string purchaserequisition_gid { get; set; }
        public string purchaserequisition_date { get; set; }
        public string costcenter_name { get; set; }
        public string branch_name { get; set; }
        public string purchaserequisition_referencenumber { get; set; }
        public string overall_status { get; set; }
        public string user_firstname { get; set; }
        public string purchaserequisition_remarks { get; set; }

    }
    public class getpurchaserequisitionrejectedSummary_list
    {

        public string purchaserequisition_gid { get; set; }
        public string purchaserequisition_date { get; set; }
        public string costcenter_name { get; set; }
        public string branch_name { get; set; }
        public string purchaserequisition_referencenumber { get; set; }
        public string overall_status { get; set; }
        public string user_firstname { get; set; }
        public string purchaserequisition_remarks { get; set; }

    }
    public class getpurchaseorderpendingSummary_list
    {

        public string ExpectedPODeliveryDate { get; set; }
        public string purchaseorder_date { get; set; }
        public string purchaseorder_gid { get; set; }
        public string branch_name { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string vendor_status { get; set; }
        public string paymentamount { get; set; }
        public string purchaseorder_remarks { get; set; }

    }
    public class getpurchaseorderammendedSummary_list
    {

        public string ExpectedPODeliveryDate { get; set; }
        public string purchaseorder_date { get; set; }
        public string purchaseorder_gid { get; set; }
        public string branch_name { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string vendor_status { get; set; }
        public string paymentamount { get; set; }
        public string purchaseorder_remarks { get; set; }

    }
    public class getpurchaseorderrejectedSummary_list
    {

        public string ExpectedPODeliveryDate { get; set; }
        public string purchaseorder_date { get; set; }
        public string purchaseorder_gid { get; set; }
        public string branch_name { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string vendor_status { get; set; }
        public string paymentamount { get; set; }
        public string purchaseorder_remarks { get; set; }

    }

    public class getpurchaseorderapprovedSummary_list
    {

        public string ExpectedPODeliveryDate { get; set; }
        public string purchaseorder_date { get; set; }
        public string purchaseorder_gid { get; set; }
        public string branch_name { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string vendor_status { get; set; }
        public string paymentamount { get; set; }
        public string purchaseorder_remarks { get; set; }

    }
    public class getsalesorderbarchart_list
    {
        public string year { get; set; }
        public string month_name { get; set; }
        public string total_amount { get; set; }
    }
    public class gettotalsalesordervalue_list
    {
        public string total_amount { get; set; }
    }

    public class getgrncancelSummary_list
    {
        public string grn_date { get; set; }
        public string grn_gid { get; set; }
        public string purchaseorder_gid { get; set; }
        public string refrence_no { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string created_date { get; set; }
        public string po_amount { get; set; }
        public string dc_no { get; set; }

    }

    public class getgrnapprovedSummary_list
    {
        public string grn_date { get; set; }
        public string grn_gid { get; set; }
        public string purchaseorder_gid { get; set; }
        public string refrence_no { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string created_date { get; set; }
        public string po_amount { get; set; }
        public string dc_no { get; set; }

    }
    public class getgrnlinktocompletedSummary_list
    {
        public string grn_date { get; set; }
        public string grn_gid { get; set; }
        public string purchaseorder_gid { get; set; }
        public string refrence_no { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string created_date { get; set; }
        public string po_amount { get; set; }
        public string dc_no { get; set; }

    }
    public class getpurchaserequisitionpendingSummaryemployee_list
    {

        public string purchaserequisition_gid { get; set; }
        public string purchaserequisition_date { get; set; }
        public string costcenter_name { get; set; }
        public string branch_name { get; set; }
        public string purchaserequisition_referencenumber { get; set; }
        public string overall_status { get; set; }
        public string user_firstname { get; set; }
        public string purchaserequisition_remarks { get; set; }

    }

    public class getpurchaserequisitionapprovedSummaryemployee_list
    {

        public string purchaserequisition_gid { get; set; }
        public string purchaserequisition_date { get; set; }
        public string costcenter_name { get; set; }
        public string branch_name { get; set; }
        public string purchaserequisition_referencenumber { get; set; }
        public string overall_status { get; set; }
        public string user_firstname { get; set; }
        public string purchaserequisition_remarks { get; set; }

    }
    public class getpurchaserequisitionrejectedSummaryemployee_list
    {

        public string purchaserequisition_gid { get; set; }
        public string purchaserequisition_date { get; set; }
        public string costcenter_name { get; set; }
        public string branch_name { get; set; }
        public string purchaserequisition_referencenumber { get; set; }
        public string overall_status { get; set; }
        public string user_firstname { get; set; }
        public string purchaserequisition_remarks { get; set; }

    }
    public class getpurchaseorderpendingSummaryemployee_list
    {

        public string ExpectedPODeliveryDate { get; set; }
        public string purchaseorder_date { get; set; }
        public string purchaseorder_gid { get; set; }
        public string branch_name { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string vendor_status { get; set; }
        public string paymentamount { get; set; }
        public string purchaseorder_remarks { get; set; }

    }
    public class getpurchaseorderammendedSummaryemployee_list
    {

        public string ExpectedPODeliveryDate { get; set; }
        public string purchaseorder_date { get; set; }
        public string purchaseorder_gid { get; set; }
        public string branch_name { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string vendor_status { get; set; }
        public string paymentamount { get; set; }
        public string purchaseorder_remarks { get; set; }

    }
    public class getpurchaseorderrejectedSummaryemployee_list
    {

        public string ExpectedPODeliveryDate { get; set; }
        public string purchaseorder_date { get; set; }
        public string purchaseorder_gid { get; set; }
        public string branch_name { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string vendor_status { get; set; }
        public string paymentamount { get; set; }
        public string purchaseorder_remarks { get; set; }

    }

    public class getpurchaseorderapprovedSummaryemployee_list
    {

        public string ExpectedPODeliveryDate { get; set; }
        public string purchaseorder_date { get; set; }
        public string purchaseorder_gid { get; set; }
        public string branch_name { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string vendor_status { get; set; }
        public string paymentamount { get; set; }
        public string purchaseorder_remarks { get; set; }

    }


    public class getgrncancelSummaryemployee_list
    {
        public string grn_date { get; set; }
        public string grn_gid { get; set; }
        public string purchaseorder_gid { get; set; }
        public string refrence_no { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string created_date { get; set; }
        public string po_amount { get; set; }
        public string dc_no { get; set; }

    }

    public class getgrnapprovedSummaryemployee_list
    {
        public string grn_date { get; set; }
        public string grn_gid { get; set; }
        public string purchaseorder_gid { get; set; }
        public string refrence_no { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string created_date { get; set; }
        public string po_amount { get; set; }
        public string dc_no { get; set; }

    }
    public class getgrnlinktocompletedSummaryemployee_list
    {
        public string grn_date { get; set; }
        public string grn_gid { get; set; }
        public string purchaseorder_gid { get; set; }
        public string refrence_no { get; set; }
        public string costcenter_name { get; set; }
        public string vendor_companyname { get; set; }
        public string overall_status { get; set; }
        public string created_date { get; set; }
        public string po_amount { get; set; }
        public string dc_no { get; set; }

    }
}