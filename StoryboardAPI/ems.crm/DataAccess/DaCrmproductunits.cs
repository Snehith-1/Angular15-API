using ems.crm.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.NetworkInformation;


namespace ems.crm.DataAccess
{
    public class DaCrmproductunits
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msGetGid, msGetGid1, lsbaseuom_flag;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        public void DaGetcrmproductunitSummary(MdlCrmproductunits values)
        {
            msSQL = "select b.productuomclass_gid,b.productuomclass_code,b.productuomclass_name," +
                "a.productuom_code,a.productuom_name,a.sequence_level,a.convertion_rate,a.baseuom_flag,a.productuom_gid " +
                " from pmr_mst_tproductuom a" +
                " left join pmr_mst_tproductuomclass b on a.productuomclass_gid=b.productuomclass_gid  group by productuomclass_name ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<summaryproductunitgrid_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new summaryproductunitgrid_list
                    {
                        productuomclass_gid = dt["productuomclass_gid"].ToString(),
                        productuomclass_code = dt["productuomclass_code"].ToString(),
                        productuomclass_name = dt["productuomclass_name"].ToString(),
                        productuom_code = dt["productuomclass_gid"].ToString(),
                        productuom_name = dt["productuomclass_code"].ToString(),
                        sequence_level = dt["productuomclass_name"].ToString(),
                        convertion_rate = dt["productuomclass_code"].ToString(),
                        baseuom_flag = dt["productuomclass_name"].ToString(),

                    });
                    values.summaryproductunitgrid_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }




        public void DaGetcrmproductunitSummarygrid(string productuomclass_gid, MdlCrmproductunits values)
        {
            msSQL = "select a.productuom_gid, a.productuom_code, a.productuom_name,a.sequence_level,format(a.convertion_rate, 2) as convertion_rate,case when a.baseuom_flag ='N' then 'NO' when baseuom_flag='Y' then 'YES' " +
                    " end as baseuom_flag,b.productuomclass_gid,b.productuomclass_code ,b.productuomclass_name from pmr_mst_tproductuom a " +
                    " left join pmr_mst_tproductuomclass b on a.productuomclass_gid = b.productuomclass_gid  left join pmr_mst_tproduct c on a.productuom_gid=c.productuom_gid" +
                    " where b.productuomclass_gid='" + productuomclass_gid + "'  group by productuom_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<summaryproductunitgrid_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new summaryproductunitgrid_list
                    {


                        productuom_gid = dt["productuom_gid"].ToString(),
                        productuom_code = dt["productuom_code"].ToString(),
                        productuom_name = dt["productuom_name"].ToString(),
                        sequence_level = dt["sequence_level"].ToString(),
                        convertion_rate = dt["convertion_rate"].ToString(),
                        baseuom_flag = dt["baseuom_flag"].ToString(),
                        productuomclass_gid = dt["productuomclass_gid"].ToString(),

                    });
                    values.summaryproductunitgrid_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }



        public void DaPostcrmproductunits(string user_gid, summaryproductunit_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("PUCM");

            msSQL = " insert into pmr_mst_tproductuomclass " +
                " (productuomclass_gid, " +
                " productuomclass_code," +
                " productuomclass_name " +
                " ) values ( " +
                  " '" + msGetGid + "'," +
                  " '" + values.productuomclass_code + "'," +
                  " '" + values.productuomclass_name  + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Unit Successfully";
            }

        }

        public void DaUpdatedcrmproductunits(string user_gid, summaryproductunit_list values)
        {


            msSQL = " update  pmr_mst_tproductuomclass  set " +
                   " productuomclass_name = '" + values.productuomclass_name + "'," +
                   " updated_by = '" + user_gid + "'," +
                   " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where productuomclass_gid='" + values.productuomclass_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Product Unit Updated Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Product Unit";
            }




        }





        public void DaPostcrmgridproductunits(string user_gid, summaryproductunitgrid_list values)
        {

            msGetGid1 = objcmnfunctions.GetMasterGID("PPMM");

            msSQL = " insert into pmr_mst_tproductuom " +
              " (productuom_gid, " +
              " productuomclass_gid," +
              " productuom_code," +
              " productuom_name, " +
              " sequence_level, " +
              " convertion_rate, " +
              " baseuom_flag " +
              " ) values ( " +
                " '" + msGetGid1 + "'," +
                  " '" + values.productuomclass_gid + "'," +
                  " '" + values.productuom_code + "'," +
                  " '" + values.productuom_name + "'," +
                  " '" + values.sequence_level + "'," +
                  " '" + values.convertion_rate + "'," +
                  " '" + values.baseuom_flag + "')";

            mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult1 != 0)
            {
                values.status = true;
                values.message = "Product Unit Successfully";
            }

        }



        public void DaGetcrmproductunitSummary12(string productuomclass_gid ,string user_gid,MdlCrmproductunits values)
        {
            msSQL = "select productuomclass_name from pmr_mst_tproductuomclass where  productuomclass_gid='" + productuomclass_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<summaryproductunit_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new summaryproductunit_list
                    {
                      
                        productuomclass_name = dt["productuomclass_name"].ToString(),
                    });
                    values.summaryproductunit_list = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }



        public void Daupdatecrmgridproductunits(string user_gid, summaryproductunitgrid_list values)
        {

            if (values.baseuom_flag == "yes")
            {
                lsbaseuom_flag = "Y";
            }
            else
            {
                lsbaseuom_flag = "N";

            }


            msSQL = " update pmr_mst_tproductuom set " +
                    " productuom_name = '" + values.productuom_name + "'," +
                    " sequence_level = '" + values.sequence_level + "'," +
                    " convertion_rate ='" + values.convertion_rate + "'," + 
                    " baseuom_flag = '" + lsbaseuom_flag + "'" +
                    " where productuom_gid ='" + values.productuom_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Product Unit Updated Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Product Unit";
            }




        }



        public void DadeletecrmproductunitsSummary(string productuomclass_gid, summaryproductunitgrid_list values)
        {


            msSQL = " select product_gid from pmr_mst_tproduct a " +
                       " left join pmr_mst_tproductuom b on a.productuom_gid=b.productuom_gid " +
                       " left join pmr_mst_tproductuomclass c on b.productuomclass_gid=c.productuomclass_gid " +
                       " where c.productuomclass_gid='" + productuomclass_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                msSQL= "delete from pmr_mst_tproductuomclass  "+
                       " where productuomclass_gid='" + productuomclass_gid + "'";


            }
        }

        public void DadeletecrmproductunitsgridSummary(string productuom_gid, summaryproductunitgrid_list values)
        {
            msSQL = "delete from pmr_mst_tproductuom  " +
                       " where productuom_gid='" + productuom_gid + "'";

        }

        public void DaGetbreadcrumb(string user_gid, string module_gid, MdlLeadbank values)
        {
            msSQL = "   select a.module_name as module_name3,a.sref as sref3,b.module_name as module_name2 ,b.sref as sref2,c.module_name as module_name1,c.sref as sref1  from adm_mst_tmodule a " +
                        " left join adm_mst_tmodule  b on b.module_gid=a.module_gid_parent" +
                        " left join adm_mst_tmodule  c on c.module_gid=b.module_gid_parent" +
                        " where a.module_gid='" + module_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<breadcrumb_list1>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new breadcrumb_list1
                    {


                        module_name1 = dt["module_name1"].ToString(),
                        sref1 = dt["sref1"].ToString(),
                        module_name2 = dt["module_name2"].ToString(),
                        sref2 = dt["sref2"].ToString(),
                        module_name3 = dt["module_name3"].ToString(),
                        sref3 = dt["sref3"].ToString(),

                    });
                    values.breadcrumb_list1 = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }


    }
}