using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.sdc.Models
{
    public class MdlAddTest : result
    {
        public string module_gid { get; set; }
        public string module_prefix { get; set; }
        public string test_Objective { get; set; }
        public string new_pages { get; set; }
        public string new_reports { get; set; }
        public string test_description { get; set; }
        public string version_major { get; set; }
        public string version_enhancement { get; set; }
        public string version_patch { get; set; }
        public string version_bug { get; set; }
        public string newdll_flag { get; set; }
        public string newdll_name { get; set; }
        public string newDependency_flag { get; set; }
        public string dependency_name { get; set; }
        public string newpage_flag { get; set; }
        public string newReports_flag { get; set; }
        public string newjs_flag { get; set; }
        public string appjs_text { get; set; }
        public string script_flag { get; set; }
        public string[] file_description { get; set; }
        public string filedescription { get; set; }
        public string test_status { get; set; }
        public string content { get; set; }
        public string script { get; set; }
        
        public List<upload_list> upload_list { get; set; }
        public List<uploadjs_list> uploadjs_list { get; set; }
        public List<versionupload_list> versionupload_list { get; set; }
        public List<customerdtl> customerdtl { get; set; }
        public List<customer_list> customer_list { get; set; }
    }
    public class customerdtl
    {
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class MdlTestSummary : result
    {
        public string lsname { get; set; }
        public string lspath { get; set; }
        public List<testsummary_list> testsummary_list { get; set; }
    }
    public class testsummary_list
    {
        public string test_gid { get; set; }
        public string module_gid { get; set; }
        public string module_prefix { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string deployed_by { get; set; }
        public string test_description { get; set; }
        public string test_objective { get; set; }
        public string version_major { get; set; }
        public string version_enhancement { get; set; }
        public string version_patch { get; set; }
        public string version_bug { get; set; }
        public string testdeploy_flag { get; set; }
        public string test_status { get; set; }
        public string testinprogress_flag { get; set; }
        public string uat_flag { get; set; }
    }
    public class MdlStatusUpdate : result
    {
        public string test_gid { get; set; }
        public string test_status { get; set; }
        public string mail_flag { get; set; }
        public string uat_gid { get; set; }
        public string live_gid { get; set; }
        public string uat_status { get; set; }
        public string live_status { get; set; }
        public string content { get; set; }

    }

    public class MdlMoveToUAT : result
    {
        public string[] test_gid { get; set; }
        public string test_status { get; set; }
        public string mail_flag { get; set; }
        public string content { get; set; }
        public string[] uat_gid { get; set; }

    }
    public class uploaddocument : result
    {
        public string uploaddocument_gid { get; set; }
        public string uploadremarks { get; set; }
        public string document_type { get; set; }
        public List<upload_list> upload_list { get; set; }
        public List<versionupload_list> versionupload_list { get; set; }
        public List<uploadjs_list> uploadjs_list { get; set; }
    }
    public class upload_list
    {
        public string uploaddocument_gid { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public string document_type { get; set; }
        public string uploaded_date { get; set; }
        public string uploaded_by { get; set; }
    }

    public class versionupload_list
    {
        public string uploaddocument_gid { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public string document_type { get; set; }
        public string uploaded_date { get; set; }
        public string uploaded_by { get; set; }
    }

    public class uploadjs_list
    {
        public string uploaddocument_gid { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public string document_type { get; set; }
        public string uploaded_date { get; set; }
        public string uploaded_by { get; set; }
    }

    public class customer_list
    {
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
    }
}