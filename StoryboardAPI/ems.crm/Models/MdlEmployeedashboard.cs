using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlEmployeedashboard : result
    {

        public List<Getsalesordrependingcountemployee_list> Getsalesordrependingcountemployeelist { get; set; }
        public List<Getsalesorderapprovedcountemployee_list> Getsalesorderapprovedcountemployeelist { get; set; }
        public List<Getsalesorderrejectedcountemployee_list> Getsalesorderrejectedcountemployeelist { get; set; }
        public List<Getsalesorderincountemployee_list> Getsalesorderincountemployeelist { get; set; }
        public List<GetsalesCurrentorderapprovedcountemployee_list> GetsalesCurrentorderapprovedcountemployeelist { get; set; }
        public List<Getsalesorderammendedcountemployee_list> Getsalesorderammendedcountemployeelist { get; set; }


        public List<GetSalesorderpendingemployeeSummary_list> GetSalesorderpendingemployeeSummary_list { get; set; }
        public List<GetSalesorderapprovedemployeeSummary_list> GetSalesorderapprovedemployeeSummary_list { get; set; }
        public List<GetSalesorderinprogressemployeeSummary_list> GetSalesorderinprogressemployeeSummary_list { get; set; }
        public List<GetSalesorderRejectedemployeeSummary_list> GetSalesorderRejectedemployeeSummary_list { get; set; }
        public List<GetSalesorderammendedemployeeSummary_list> GetSalesorderammendedemployeeSummary_list { get; set; }

    }


    public class Getsalesordrependingcountemployee_list
    {
        public string salesorder_gid { get; set; }
    }

    public class Getsalesorderapprovedcountemployee_list
    {
        public string salesorder_gid { get; set; }
    }
    public class Getsalesorderrejectedcountemployee_list
    {
        public string salesorder_gid { get; set; }
    }
    public class Getsalesorderincountemployee_list
    {
        public string salesorder_gid { get; set; }
    }
    public class GetsalesCurrentorderapprovedcountemployee_list
    {
        public string salesorder_gid { get; set; }
    }
    public class Getsalesorderammendedcountemployee_list
    {
        public string salesorder_gid { get; set; }
    }



    public class GetSalesorderpendingemployeeSummary_list
    {

        public string salesorder_gid { get; set; }
        public string so_referenceno1 { get; set; }
        public string salesorder_date { get; set; }
        public string customer_contact_person { get; set; }
        public string total_amount { get; set; }
    }


    public class GetSalesorderapprovedemployeeSummary_list
    {

        public string salesorder_gid { get; set; }
        public string so_referenceno1 { get; set; }
        public string salesorder_date { get; set; }
        public string customer_contact_person { get; set; }
        public string total_amount { get; set; }
    }


    public class GetSalesorderinprogressemployeeSummary_list
    {

        public string salesorder_gid { get; set; }
        public string so_referenceno1 { get; set; }
        public string salesorder_date { get; set; }
        public string customer_contact_person { get; set; }
        public string total_amount { get; set; }
    }


    public class GetSalesorderRejectedemployeeSummary_list
    {

        public string salesorder_gid { get; set; }
        public string so_referenceno1 { get; set; }
        public string salesorder_date { get; set; }
        public string customer_contact_person { get; set; }
        public string total_amount { get; set; }
    }


    public class GetSalesorderammendedemployeeSummary_list
    {

        public string salesorder_gid { get; set; }
        public string so_referenceno1 { get; set; }
        public string salesorder_date { get; set; }
        public string customer_contact_person { get; set; }
        public string total_amount { get; set; }
    }



}