using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.sdc.Models
{
    public class MdlSdcTrnUatDeployment
    {
    }

    public class MdlUatSummary :result
    {
        public string lspath { get; set; }
        public string lsname { get; set; }
        public List<uatsummary_list> uatsummary_list { get; set; }
    }
    public class uatsummary_list
    {
        public string uat_gid { get; set; }
        public string uat_status { get; set; }
        public string uatinprogress_flag { get; set; }
        public string created_by { get; set; }
        public string deployed_by { get; set; }
        public string created_date { get; set; }
        public string uatdeploy_flag { get; set; }
        public string file_description { get; set; }
        public string live_flag { get; set; }

    }
    public class MdlUatView : result
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

        //public List<uat_list> uat_list { get; set; }
        //public List<uploadjs_list> uploadjs_list { get; set; }
        //public List<versionupload_list> versionupload_list { get; set; }
        public List<filedesc_list> filedesc_list { get; set; }
        public List<upload_list> upload_list { get; set; }
        public List<uploadjs_list> uploadjs_list { get; set; }
        public List<versionupload_list> versionupload_list { get; set; }
    }
    //public class upload_list
    //{
    //    public string uploaddocument_gid { get; set; }
    //    public string file_name { get; set; }
    //    public string file_path { get; set; }
    //    public string document_type { get; set; }
    //    public string uploaded_date { get; set; }
    //    public string uploaded_by { get; set; }
    //}

    //public class versionuat_list
    //{
    //    public string uploaddocument_gid { get; set; }
    //    public string file_name { get; set; }
    //    public string file_path { get; set; }
    //    public string document_type { get; set; }
    //    public string uploaded_date { get; set; }
    //    public string uploaded_by { get; set; }
    //}

    //public class uatjs_list
    //{
    //    public string uploaddocument_gid { get; set; }
    //    public string file_name { get; set; }
    //    public string file_path { get; set; }
    //    public string document_type { get; set; }
    //    public string uploaded_date { get; set; }
    //    public string uploaded_by { get; set; }
    //}
    public class filedesc_list
    {
        public string files_description { get; set; }
    }
}