using ems.sdc.Models;
using ems.utilities.Functions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ems.sdc.DataAccess
{
    public class DaSdcTrnReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        StringBuilder sb = new StringBuilder();
        string msSQL;

        public void DaGetTestSummary(MdlTestSummary values)
        {
            msSQL = " select test_gid, module_gid, test_description,module_prefix, version_Major, testinprogress_flag, uat_flag," +
                    " version_enhancement, version_patch, version_bug, test_objective, testdeploy_flag, test_status, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " CASE WHEN testdeploy_flag = 'N' THEN '-' ELSE concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',date_format(a.deployed_date,'%d-%m-%Y %h:%i %p') ) END as deployed_by" +
                    " from sdc_trn_ttestdeployment a " +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid" +
                    " LEFT JOIN adm_mst_tuser c ON a.deployed_by=c.user_gid" +
                    " where testdeploy_flag <> 'N'" +
                    " order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getTestList = new List<testsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getTestList.Add(new testsummary_list
                    {
                        test_gid = dt["test_gid"].ToString(),
                        module_gid = dt["module_gid"].ToString(),
                        module_prefix = dt["module_prefix"].ToString(),
                        test_objective = dt["test_objective"].ToString(),
                        version_major = dt["version_Major"].ToString(),
                        version_enhancement = dt["version_enhancement"].ToString(),
                        version_patch = dt["version_patch"].ToString(),
                        version_bug = dt["version_bug"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        deployed_by = dt["deployed_by"].ToString(),
                        test_description = dt["test_description"].ToString(),
                        testdeploy_flag = dt["testdeploy_flag"].ToString(),
                        testinprogress_flag = dt["testinprogress_flag"].ToString(),
                        test_status = dt["test_status"].ToString(),
                        uat_flag = dt["uat_flag"].ToString(),
                    });
                    values.testsummary_list = getTestList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetTestReportExport(MdlTestSummary values)
        {
            msSQL = " select module_prefix as 'Module Prefix', test_objective as Objective, concat_ws('.',version_major,version_enhancement,version_patch,version_bug) as Version, " +
                    "  test_description as Description, test_status as Status, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as 'created By', date_format(a.created_date,'%d-%m-%Y %h:%i %p') as 'Created Date'," +
                    " CASE WHEN testdeploy_flag = 'N' THEN '-' ELSE concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code,' / ',date_format(a.deployed_date,'%d-%m-%Y %h:%i %p') ) END as 'Deployed By / On'" +
                    " from sdc_trn_ttestdeployment a " +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid" +
                    " LEFT JOIN adm_mst_tuser c ON a.deployed_by=c.user_gid" +
                    " where testdeploy_flag <> 'N'" +
                    " order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Test Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/TestReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Test_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/TestReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 8])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(file);
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }
        public void DaGetUatSummary(MdlUatSummary values)
        {
            msSQL = " select a.uat_gid, uat_status, uatinprogress_flag, uatdeploy_flag, live_flag, " +
                    " group_concat(DISTINCT CONCAT(c.module_prefix, ' (', file_description, ')')) as file_description, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                    " CASE WHEN uatdeploy_flag = 'N' THEN '-' ELSE concat(d.user_firstname,' ',d.user_lastname,' / ',d.user_code,' / ',date_format(a.deployed_date,'%d-%m-%Y %h:%i %p') ) END as deployed_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date FROM sdc_trn_tuatdeployment a " +
                    " LEFT JOIN sdc_trn_tuatdeploymentdtl c on c.uat_gid = a.uat_gid " +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by = b.user_gid " +
                    " LEFT JOIN adm_mst_tuser d ON a.deployed_by = d.user_gid " +
                    " where uatdeploy_flag <> 'N'" +
                    " group by a.uat_gid order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUatList = new List<uatsummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getUatList.Add(new uatsummary_list
                    {
                        uat_gid = dt["uat_gid"].ToString(),
                        uat_status = dt["uat_status"].ToString(),
                        uatinprogress_flag = dt["uatinprogress_flag"].ToString(),
                        uatdeploy_flag = dt["uatdeploy_flag"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        deployed_by = dt["deployed_by"].ToString(),
                        file_description = dt["file_description"].ToString(),
                        live_flag = dt["live_flag"].ToString(),
                    });
                    values.uatsummary_list = getUatList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetUatReportExport(MdlUatSummary values)
        {
            msSQL = " select  group_concat(DISTINCT CONCAT(c.module_prefix, ' (', file_description, ')')) as 'Module Prefix', " +
                    " group_concat(DISTINCT CONCAT(c.module_prefix, ' -', test_description)) as Description, " +
                    " uat_status as Status,  concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as 'created By', " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'Created Date', " +
                    " CASE WHEN uatdeploy_flag = 'N' THEN '-' ELSE concat(d.user_firstname,' ',d.user_lastname,' / ',d.user_code,' / ',date_format(a.deployed_date,'%d-%m-%Y %h:%i %p') ) END as 'Deployed By / On'" +
                    " FROM sdc_trn_tuatdeployment a " +
                    " LEFT JOIN sdc_trn_tuatdeploymentdtl c on c.uat_gid = a.uat_gid " +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by = b.user_gid " +
                    " LEFT JOIN adm_mst_tuser d ON a.deployed_by = d.user_gid " +
                    " where uatdeploy_flag <> 'N'" +
                    " group by a.uat_gid order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("UAT Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/UATReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "UAT_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/UATReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(file);
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }

        public void DaGetLiveSummaryReport(MdlLiveSummary values)
        {
            msSQL = " select a.live_gid, live_status, liveinprogress_flag, livedeploy_flag, " +
                    " group_concat(DISTINCT CONCAT(c.module_prefix, ' (', file_description, ')')) as file_description, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                    " CASE WHEN livedeploy_flag = 'N' THEN '-' ELSE concat(d.user_firstname,' ',d.user_lastname,' / ',d.user_code,' / ',date_format(a.livedeployed_date,'%d-%m-%Y %h:%i %p') ) END as deployed_by," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date FROM sdc_trn_tlivedeployment a " +
                    " LEFT JOIN sdc_trn_tlivedeploymentdtl c on c.live_gid = a.live_gid " +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by = b.user_gid " +
                     " LEFT JOIN adm_mst_tuser d ON a.livedeployed_by = d.user_gid " +
                     " where livedeploy_flag<>'N'" +
                    " group by a.live_gid order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getLiveList = new List<livesummary_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getLiveList.Add(new livesummary_list
                    {
                        live_gid = dt["live_gid"].ToString(),
                        live_status = dt["live_status"].ToString(),
                        liveinprogress_flag = dt["liveinprogress_flag"].ToString(),
                        livedeploy_flag = dt["livedeploy_flag"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        deployed_by = dt["deployed_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        file_description = dt["file_description"].ToString(),
                    });
                    values.livesummary_list = getLiveList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetLiveReportExport(MdlLiveSummary values)
        {
            msSQL = " select group_concat(DISTINCT CONCAT(c.module_prefix, ' (', file_description, ')')) as 'Module Prefix', " +
                    " group_concat(DISTINCT CONCAT(module_prefix, ' -', test_description)) as Description, live_status as Status, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as 'Created By', " +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'Created Date', " +
                    " CASE WHEN livedeploy_flag = 'N' THEN '-' ELSE concat(d.user_firstname,' ',d.user_lastname,' / ',d.user_code,' / ',date_format(a.livedeployed_date,'%d-%m-%Y %h:%i %p') ) END as 'Deployed By / On'" +
                    " FROM sdc_trn_tlivedeployment a " +
                    " LEFT JOIN sdc_trn_tlivedeploymentdtl c on c.live_gid = a.live_gid " +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by = b.user_gid " +
                    " LEFT JOIN adm_mst_tuser d ON a.livedeployed_by = d.user_gid " +
                    " where livedeploy_flag<>'N'" + 
                    " group by a.live_gid order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Live Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/LiveReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Live_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SDC/LiveReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(file);
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }
    }
}