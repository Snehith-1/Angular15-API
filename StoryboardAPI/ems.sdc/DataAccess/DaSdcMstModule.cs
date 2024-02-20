using ems.sdc.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace ems.sdc.DataAccess
{
    public class DaSdcMstModule
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetDocumentGid;
        string msGetModuleCode, success;
        int mnResult;
        string moduleGID = string.Empty;

        public void DaPostModuleAdd(moduleadd values, string user_gid)
        {

             msSQL = " select module_name from sdc_mst_tmodule " +
                     " where module_prefix='" + values.module_prefix + "' ";
             objODBCDatareader = objdbconn.GetDataReader(msSQL);
             if (objODBCDatareader.HasRows == true)
             {
                 values.status = false;
                 values.message = "Module Prefix Already Exist";
             }
             else
             {
                 msGetGid = objcmnfunctions.GetMasterGID("MDLM");

                 msGetModuleCode = objcmnfunctions.GetMasterGID("MDC");

                 msSQL = " insert into sdc_mst_tmodule(" +
                  " module_gid," +
                  " module_code, " +
                  " module_name," +
                  " module_prefix," +
                  " availability," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + msGetModuleCode + "', " +
                  "'" + values.module_name.Replace("'", "\\'") + "'," +
                  "'" + values.module_prefix + "'," +
                  "'" + values.availability + "'," +
                  "'" + user_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                 mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                 if (mnResult != 0)
                 {
                     values.status = true;
                     values.message = "Module Details are Added Successfully..!";
                 }
                 else
                 {
                        values.status = false;
                        values.message = "Error Occured..!";
                 }
             }
             objODBCDatareader.Close();
         }
        // Module Master Summary
        public void DaGetModuleSummary(moduledtllist values)
        {
            msSQL = " select module_gid, module_code, module_name,module_prefix, availability, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from sdc_mst_tmodule a " +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid" +
                    " order by module_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<moduledtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new moduledtl
                    {
                        module_gid = dt["module_gid"].ToString(),
                        module_code = dt["module_code"].ToString(),
                        module_name = dt["module_name"].ToString(),
                        module_prefix = dt["module_prefix"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        availability = dt["availability"].ToString(),
                    });
                    values.moduledtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        // Module Edit 
        public void DaGetModuleView(string module_gid, moduleadd values)
        {
            msSQL = " select module_gid,module_name,module_code,module_prefix, availability from sdc_mst_tmodule " +
                    " where module_gid='" + module_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.module_gid = objODBCDatareader["module_gid"].ToString();
                values.module_name = objODBCDatareader["module_name"].ToString();
                values.module_code = objODBCDatareader["module_code"].ToString();
                values.module_prefix = objODBCDatareader["module_prefix"].ToString();
                values.availability = objODBCDatareader["availability"].ToString();
            }
            objODBCDatareader.Close();
        }

        public bool DaPostModuleUpdate(moduleadd values, string user_gid)
        {
            bool status = false;
         
                msSQL = " select module_gid from sdc_mst_tmodule " +
                        " where module_prefix='"+ values.module_prefix +"'";
                moduleGID = objdbconn.GetExecuteScalar(msSQL);
                if (moduleGID != "")
                {
                    if (moduleGID != values.module_gid)
                    {
                        values.message = "Module Prefix Already Exist";
                        values.status = false;
                        return status;
                    }
                }
                msSQL = " update sdc_mst_tmodule set " +
                    " module_name='" + values.module_name.Replace("'", "\\'") + "'," +
                    " module_prefix='" + values.module_prefix + "'," +
                    " availability='" + values.availability + "'," +
                    " updated_by='" + user_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where module_gid='" + values.module_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Module Details are Updated Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            return status;
        }

        public void DaGetModuleDelete(string module_gid, result values)
        {
           
                msSQL = " delete from sdc_mst_tmodule where module_gid='" + module_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Module Details are Deleted Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            
        }

        // Module Master Summary
        public void DaGetCustomersList(customer values, string module_gid)
        { 
            msSQL = "select customer_name, customer_code, customer_gid, customer_city, customer_state from crm_mst_tcustomer " +
                    "where customer_gid NOT IN (SELECT customer_gid from sdc_trn_tmodule2customer where module_gid='" + module_gid + "') and " +
                    "customer_name IS NOT NULL group by customer_name";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCustomerList = new List<customerlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCustomerList.Add(new customerlist
                    {
                        customer_name = dt["customer_name"].ToString(),
                        customer_code = dt["customer_code"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_city = dt["customer_city"].ToString(),
                        customer_state = dt["customer_state"].ToString(),
                    });
                    values.customerlist = getCustomerList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostCustomerAssign(MdlcustomerAssign values, string user_gid)
        {
            foreach (string i in values.customer_gid)
            {

                msSQL = "Select customer_name, customer_code, customer_state, customer_city from crm_mst_tcustomer where customer_gid='" + i + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    {
                        values.customer_name = objODBCDatareader["customer_name"].ToString();
                        values.customer_code = objODBCDatareader["customer_code"].ToString();
                        values.customer_state = objODBCDatareader["customer_state"].ToString();
                        values.customer_city = objODBCDatareader["customer_city"].ToString();

                        objODBCDatareader.Close();
                        msGetGid = objcmnfunctions.GetMasterGID("M2C");

                        msSQL = " insert into sdc_trn_tmodule2customer(" +
                         " module2customer_gid," +
                         " module_gid," +
                         " customer_gid," +
                         " customer_name, " +
                         " customer_code," +
                         " customer_city," +
                         " customer_state," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + values.module_gid + "'," +
                         "'" + i + "', " +
                         "'" + values.customer_name + "'," +
                         "'" + values.customer_code + "'," +
                         "'" + values.customer_city + "'," +
                         "'" + values.customer_state + "'," +
                         "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                }
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Module Details are Tagged Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        // Module Master Summary
        public void DaGetModule2Customer(moduledtllist values, string module_gid)
        {
            msSQL = " select module_gid, module2customer_gid, customer_name,customer_gid " +
                    " from sdc_trn_tmodule2customer " +
                    " where module_gid = '" + module_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<moduledtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new moduledtl
                    {
                        module_gid = dt["module_gid"].ToString(),
                        module2customer_gid = dt["module2customer_gid"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                    });
                    values.moduledtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
    }
}