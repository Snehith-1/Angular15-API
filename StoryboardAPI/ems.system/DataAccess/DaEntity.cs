using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.system.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using System.Data;
using System.Web;
using OfficeOpenXml;
using System.Configuration;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Mail;

namespace ems.system.DataAccess
{
    public class DaEntity
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msEmployeeGID, lsemployee_gid, lsentity_code, lsdesignation_code,lsCode, msGetGid, msGetGid1, msGetPrivilege_gid, msGetModule2employee_gid;
        int mnResult, mnResult1, mnResult2, mnResult3, mnResult4, mnResult5;

        int mnResult6;


        public void DaPostEntity( string user_gid, entity_list values)
        {
            //msSQL = " SELECT employee_gid FROM adm_mst_tuser a left join hrm_mst_temployee b on b.user_gid=a.user_gid WHERE a.user_gid='" + user_gid + "' ";
            //lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);
            msGetGid = objcmnfunctions.GetMasterGID("CENT");
            msGetGid1 = objcmnfunctions.GetMasterGID("ENT");
            msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='CENT' order by finyear desc limit 0,1 ";
            lsCode = objdbconn.GetExecuteScalar(msSQL);

            lsentity_code = "ENT" + "000" + lsCode;






            msSQL = " insert into adm_mst_tentity(" +
                    " entity_gid," +
                    " entity_code," +
                    " entity_name," +
                    " entity_description," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    " '" + lsentity_code + "'," +
                    "'" + values.entity_name + "',";
            if (values.entity_description == null || values.entity_description == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.entity_description.Replace("'", "") + "',";
            }
            msSQL += "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Entity Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Entity";
            }
        }

        // Module Master Summary
        public void DaGetEntitySummary(entityllist values)
        {
            msSQL = " select  entity_gid,entity_code, entity_name, entity_description, CONCAT(b.user_firstname,' ',b.user_lastname) as created_by, a.created_date " +
                    " from adm_mst_tentity a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by entity_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getModuleList = new List<entitydtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getModuleList.Add(new entitydtl
                    {
                        entity_gid = dt["entity_gid"].ToString(),
                        entity_code = dt["entity_code"].ToString(),
                        entity_name = dt["entity_name"].ToString(),
                        entity_description = dt["entity_description"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.entitydtl = getModuleList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetupdateentitydetails(string user_gid, entity_list values)
        {
            msSQL = " update  adm_mst_tentity set " +
                 " entity_name = '" + values.entity_name + "'," +
                 " entity_description = '" + values.entity_description + "'," +
                 " updated_by = '" + user_gid + "'," +
                 " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where entity_gid='" + values.entity_gid + "'  ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Entity Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Updating Entity";
            }
        }

        public void DaGetdeleteentitydetails(string entity_gid, entity_list values)
        {
            msSQL = "  delete from adm_mst_tentity where entity_gid='" + entity_gid + "'  ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Designation Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Designation";
            }
        }
        public void DaPostDesignation(string user_gid, designation_list values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("SDGM");
            msGetGid1 = objcmnfunctions.GetMasterGID("DES");
            msSQL = " Select sequence_curval from adm_mst_tsequence where sequence_code ='SDGM' order by finyear desc limit 0,1 ";
            lsCode = objdbconn.GetExecuteScalar(msSQL);

            lsdesignation_code = "DES" + "000" + lsCode;

            msSQL = " insert into adm_mst_tdesignation(" +
                    " designation_gid," +
                    " designation_code," +
                    " designation_name," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    " '" + msGetGid + "'," +
                    " '" + lsdesignation_code + "'," +
                    "'" + values.designation_name + "',";
            msSQL += "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Designation Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error While Adding Designation";
            }
        }

    }
}
