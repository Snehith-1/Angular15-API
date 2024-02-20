using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.pmr.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class MdlPurchaseDashboard:result
    {


        public List<getpurchaserequisitionpendingcount_list> getpurchaserequisitionpendingcount_list { get; set; }
        public List<getpurchaserequisitionaprovedcount_list> getpurchaserequisitionaprovedcount_list { get; set; }
        public List<getpurchaserequisitionrejectedcount_list> getpurchaserequisitionrejectedcount_list { get; set; }
        public List<getpurchaseorderpendingcount_list> getpurchaseorderpendingcount_list { get; set; }
        public List<getpurchaseorderapprovedcount_list> getpurchaseorderapprovedcount_list { get; set; }
        public List<getpurchaseorderrejectedcount_list> getpurchaseorderrejectedcount_list { get; set; }
        public List<getpurchaseorderammendedcount_list> getpurchaseorderammendedcount_list { get; set; }

        public List<getgrnapprovedcount_list> getgrnapprovedcount_list { get; set; }
        public List<getgrnlinktocompleted_list> getgrnlinktocompleted_list { get; set; }
        public List<getgrncancel_list> getgrncancel_list { get; set; }

        public List<getpurchaserequisitionpendingSummary_list> getpurchaserequisitionpendingSummary_list { get; set; }
        public List<getpurchaserequisitionapprovedSummary_list> getpurchaserequisitionapprovedSummary_list { get; set; }
        public List<getpurchaserequisitionrejectedSummary_list> getpurchaserequisitionrejectedSummary_list { get; set; }
        public List<getpurchaseorderpendingSummary_list> getpurchaseorderpendingSummary_list { get; set; }
        public List<getpurchaseorderapprovedSummary_list> getpurchaseorderapprovedSummary_list { get; set; }
        public List<getpurchaseorderammendedSummary_list> getpurchaseorderammendedSummary_list { get; set; }
        public List<getpurchaseorderrejectedSummary_list> getpurchaseorderrejectedSummary_list { get; set; }
        public List<getgrnlinktocompletedSummary_list> getgrnlinktocompletedSummary_list { get; set; }
        public List<getgrnapprovedSummary_list> getgrnapprovedSummary_list { get; set; }
        public List<getgrncancelSummary_list> getgrncancelSummary_list { get; set; }
        public List<getpurchaseorderbarchart_list> getpurchaseorderbarchart_list { get; set; }
        public List<getgrnbarchart_list> getgrnbarchart_list { get; set; }

        public List<getpurchaserequisitionpendingcountemployee_list> getpurchaserequisitionpendingcountemployee_list { get; set; }
        public List<getpurchaserequisitionaprovedcountemployee_list> getpurchaserequisitionaprovedcountemployee_list { get; set; }
        public List<getpurchaserequisitionrejectedcountemployee_list> getpurchaserequisitionrejectedcountemployee_list { get; set; }

        public List<getpurchaseorderpendingcountemployee_list> getpurchaseorderpendingcountemployee_list { get; set; }
        public List<getpurchaseorderapprovedcountemployee_list> getpurchaseorderapprovedcountemployee_list { get; set; }
        public List<getpurchaseorderrejectedcountemployee_list> getpurchaseorderrejectedcountemployee_list { get; set; }
        public List<getpurchaseorderammendedcountemployee_list> getpurchaseorderammendedcountemployee_list { get; set; }

        public List<getgrnapprovedcountemployee_list> getgrnapprovedcountemployee_list { get; set; }
        public List<getgrnlinktocompletedemployee_list> getgrnlinktocompletedemployee_list { get; set; }
        public List<getgrncancelemployee_list> getgrncancelemployee_list { get; set; }


        public List<getpurchaserequisitionpendingSummaryemployee_list> getpurchaserequisitionpendingSummaryemployee_list { get; set; }
        public List<getpurchaserequisitionapprovedSummaryemployee_list> getpurchaserequisitionapprovedSummaryemployee_list { get; set; }
        public List<getpurchaserequisitionrejectedSummaryemployee_list> getpurchaserequisitionrejectedSummaryemployee_list { get; set; }
        public List<getpurchaseorderpendingSummaryemployee_list> getpurchaseorderpendingSummaryemployee_list { get; set; }
        public List<getpurchaseorderapprovedSummaryemployee_list> getpurchaseorderapprovedSummaryemployee_list { get; set; }
        public List<getpurchaseorderammendedSummaryemployee_list> getpurchaseorderammendedSummaryemployee_list { get; set; }
        public List<getpurchaseorderrejectedSummaryemployee_list> getpurchaseorderrejectedSummaryemployee_list { get; set; }
        public List<getgrnlinktocompletedSummaryemployee_list> getgrnlinktocompletedSummaryemployee_list { get; set; }
        public List<getgrnapprovedSummaryemployee_list> getgrnapprovedSummaryemployee_list { get; set; }
        public List<getgrncancelSummaryemployee_list> getgrncancelSummaryemployee_list { get; set; }


    }
    public class getpurchaseorderbarchart_list
    {
        public string year { get; set; }
        public string month_name { get; set; }
        public string total_amount { get; set; }
    }
    public class getgrnbarchart_list
    {
        public string grn_year { get; set; }
        public string grn_month_name { get; set; }
        public string po_amount { get; set; }
    }
    public class getpurchaserequisitionpendingcount_list
    {
        public string purchaserequisition_gid { get; set; }
    }

    public class getpurchaserequisitionaprovedcount_list
    {
        public string purchaserequisition_gid { get; set; }
    }
    public class getpurchaserequisitionrejectedcount_list
    {
        public string purchaserequisition_gid { get; set; }
    }
    public class getpurchaserequisitionpendingcountemployee_list
    {
        public string purchaserequisition_gid { get; set; }
    }

    public class getpurchaserequisitionaprovedcountemployee_list
    {
        public string purchaserequisition_gid { get; set; }
    }
    public class getpurchaserequisitionrejectedcountemployee_list
    {
        public string purchaserequisition_gid { get; set; }
    }
    public class getpurchaseorderpendingcount_list
    {
        public string purchaseorder_gid { get; set; }
    }
    public class getpurchaseorderapprovedcount_list
    {
        public string purchaseorder_gid { get; set; }
    }
    public class getpurchaseorderrejectedcount_list
    {
        public string purchaseorder_gid { get; set; }
    }
    public class getpurchaseorderammendedcount_list
    {
        public string purchaseorder_gid { get; set; }
    }

    public class getpurchaseorderpendingcountemployee_list
    {
        public string purchaseorder_gid { get; set; }
    }
    public class getpurchaseorderapprovedcountemployee_list
    {
        public string purchaseorder_gid { get; set; }
    }
    public class getpurchaseorderrejectedcountemployee_list
    {
        public string purchaseorder_gid { get; set; }
    }
    public class getpurchaseorderammendedcountemployee_list
    {
        public string purchaseorder_gid { get; set; }
    }


    public class getgrnapprovedcount_list
    {
        public string grn_gid { get; set; }
    }
    public class getgrnlinktocompleted_list
    {
        public string grn_gid { get; set; }
    }
    public class getgrncancel_list
    {
        public string grn_gid { get; set; }
    }

    public class getgrnapprovedcountemployee_list
    {
        public string grn_gid { get; set; }
    }
    public class getgrnlinktocompletedemployee_list
    {
        public string grn_gid { get; set; }
    }
    public class getgrncancelemployee_list
    {
        public string grn_gid { get; set; }
    }
    public class getpurchaserequisitionpendingSummary_list
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