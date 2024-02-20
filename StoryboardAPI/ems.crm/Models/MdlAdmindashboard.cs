using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.crm.DataAccess;

namespace ems.crm.Models
{
 
    public class MdlAdmindashboard:result
    {
        //PANELS//

        public List<gettotalsalesordercount_list> gettotalsalesordercount_list { get; set; }
        public List<getsalesorderpendingcount_list> getsalesorderpendingcount_list { get; set; }
        public List<getsalesorderinprogresscount_list> getsalesorderinprogresscount_list { get; set; }
        public List<getsalesorderapprovedcount_list> getsalesorderapprovedcount_list { get; set; }
        public List<getsalesorderrejectedcount_list> getsalesorderrejectedcount_list { get; set; }
        public List<getsalesorderammendedcount_list> getsalesorderammendedcount_list { get; set; }
        public List<getsalesordercurrentyearcount_list> getsalesordercurrentyearcount_list { get; set; }
        public List<getsalesorderrejectedyearcount_list> getsalesorderrejectedyearcount_list { get; set; }

        //summary//
        public List<getsalesorderpendingsummary_list> getsalesorderpendingsummary_list { get; set; }
        public List<getsalesorderapprovedsummary_list> getsalesorderapprovedsummary_list { get; set; }
        public List<getsalesorderinprogresssummary_list> getsalesorderinprogresssummary_list { get; set; }
        public List<getsalesorderrejectedsummary_list> getsalesorderrejectedsummary_list { get; set; }

        public List<getsalesorderammendedsummary_list> getsalesorderammendedsummary_list { get; set; }
        
        //barchart//
        public List<getsalesorderadminbarchart_list> getsalesorderadminbarchart_list { get; set; }

    }
    public class gettotalsalesordercount_list
    {
        public string salesorder_gid { get; set; }
    }
    public class getsalesorderpendingcount_list
    {
        public string salesorder_gid { get; set; }
    }
    public class getsalesorderinprogresscount_list
    {
        public string salesorder_gid { get; set; }
    }
    public class getsalesorderapprovedcount_list
    {
        public string salesorder_gid { get; set; }
    }
    public class getsalesorderrejectedcount_list
    {
        public string salesorder_gid { get; set; }
    }
    public class getsalesorderammendedcount_list
    {
        public string salesorder_gid { get; set; }
    }
    public class getsalesordercurrentyearcount_list
    {
        public string salesorder_gid { get; set; }
    }
    public class getsalesorderrejectedyearcount_list
    {
        public string salesorder_gid { get; set; }
    }

    //SUMMARY//
    public class getsalesorderpendingsummary_list
    {

        public string salesorder_date { get; set; }
        public string so_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string branch_name { get; set; }
        public string customer_address { get; set; }
        public string total_amount { get; set; }
        public string created_by { get; set; }
        public string salesorder_status { get; set; }

    }

    public class getsalesorderapprovedsummary_list
    {

        public string salesorder_date { get; set; }
        public string so_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string branch_name { get; set; }
        public string customer_address { get; set; }
        public string total_amount { get; set; }
        public string created_by { get; set; }
        public string salesorder_status { get; set; }

    }

    public class getsalesorderinprogresssummary_list
    {

        public string salesorder_date { get; set; }
        public string so_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string branch_name { get; set; }
        public string customer_address { get; set; }
        public string total_amount { get; set; }
        public string created_by { get; set; }
        public string salesorder_status { get; set; }

    }

    public class getsalesorderrejectedsummary_list
    {

        public string salesorder_date { get; set; }
        public string so_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string branch_name { get; set; }
        public string customer_address { get; set; }
        public string total_amount { get; set; }
        public string created_by { get; set; }
        public string salesorder_status { get; set; }

    }

    public class getsalesorderammendedsummary_list
    {

        public string salesorder_date { get; set; }
        public string so_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string branch_name { get; set; }
        public string customer_address { get; set; }
        public string total_amount { get; set; }
        public string created_by { get; set; }
        public string salesorder_status { get; set; }

    }
    //barchart//
    public class getsalesorderadminbarchart_list
    {
        public string year { get; set; }
        public string month_name { get; set; }
        public string total_amount { get; set; }
    }
}
