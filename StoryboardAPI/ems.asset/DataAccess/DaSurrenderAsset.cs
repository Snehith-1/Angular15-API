using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.asset.Models;
using ems.utilities.Functions;

namespace ems.asset.DataAccess
{
    public class DaSurrenderAsset
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable, dt_datatable1;
        string msSQL;
        int mnResult;
        string serialid;
        string imgpath;
        string lsemployee_name;
        string MsgetAsset2historyGID;

        public bool DaPostGetSurrender(string employee_gid, surrenderassets objsurrenderassets)
        {

            msSQL = " SELECT a.asset2custodian_gid, c.asset_name,a.status,b.asset_id,a.issued_by,date_format(a.issued_date,'%d-%m-%Y %h:%i %p') as issued_date,b.oe_serial as assetserial_id,a.asset_image FROM ams_mst_tasset c  LEFT JOIN  ams_trn_tassetserial b ON c.asset_gid=b.asset_gid " +
                    " LEFT JOIN ams_trn_tasset2custodian a ON b.assetserial_gid = a.assetserial_gid" +
                     " WHERE custodiantracker_gid=(SELECT custodiantracker_gid from ams_trn_tcustodiantracker where employee_gid='" + employee_gid + "') and (a.status='Surrender Pending' or a.status='Surrender' or a.status='Confirmed') group by b.asset_id";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var Get_Details = new List<surrendersummary>();

                foreach (DataRow dr in dt_datatable.Rows)
                {
                    Get_Details.Add(new surrendersummary
                    {
                        asset2custodian_gid =(dr["asset2custodian_gid"].ToString ()),
                        assetserial_id = (dr["assetserial_id"].ToString()),
                        asset_name = (dr["asset_name"].ToString()).Trim(),
                        asset_id = (dr["asset_id"].ToString()),
                        issued_by = (dr["issued_by"].ToString()),
                        issued_date = (dr["issued_date"].ToString()),
                        asset_image = (dr["asset_image"].ToString()),
                        surrender_status = (dr["status"].ToString())
                    });
                }

                msSQL = " select a.asset2custodian_gid,a.assetserial_gid,c.oe_serial as assetserial_id,c.asset_id,b.asset_name,a.issued_by,date_format(a.issued_date,'%d-%m-%Y') as issued_date,a.asset_image,a.status " +
                        " from ams_trn_tasset2history a " +
                        " left join ams_mst_tasset b on a.asset_gid = b.asset_gid " +
                        " left join ams_trn_tassetserial c on a.assetserial_gid=c.assetserial_gid " +
                        " where a.status = 'Confirmed' and employee_gid = '" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt_datatable.Rows)
                    {
                        Get_Details.Add(new surrendersummary
                        {
                            asset2custodian_gid = (dr["asset2custodian_gid"].ToString()),
                            assetserial_id = (dr["assetserial_id"].ToString()),
                            asset_name = (dr["asset_name"].ToString()).Trim(),
                            asset_id = (dr["asset_id"].ToString()),
                            issued_by = (dr["issued_by"].ToString()),
                            issued_date = (dr["issued_date"].ToString()),
                            asset_image = (dr["asset_image"].ToString()),
                            surrender_status = (dr["status"].ToString())
                        });
                    }
                }
                objsurrenderassets.surrendersummary = Get_Details;

            objsurrenderassets.status = true;
            dt_datatable.Dispose();

            return true;
        }
        public bool DaPostUpdateSurrender(string asset2custodian_gid, surrenderstatus objsurrenderstatus, string employee_gid,string user_gid)
        {

            msSQL = " UPDATE ams_trn_tasset2custodian SET status='Surrender'"+
                    " where asset2custodian_gid='" + asset2custodian_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "Select concat(user_firstname,'  ',user_lastname) as employee_name from adm_mst_tuser where user_gid='" + user_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                lsemployee_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
            MsgetAsset2historyGID = objcmnfunctions.GetMasterGID("AST2H");
            msSQL = " SELECT a.custodiantracker_gid,a.asset_gid,a.assetserial_gid,a.asset_image, " +
                    " b.employee_gid,b.employee_name,a.issuedemployee_gid,a.issued_by,date_format(a.issued_date,'%Y-%m-%d') as issued_date " +
                    " FROM ams_trn_tasset2custodian a " +
                    " LEFT JOIN ams_trn_tcustodiantracker b ON a.custodiantracker_gid=b.custodiantracker_gid " +
                    " WHERE a.asset2custodian_gid='" + asset2custodian_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                imgpath = objODBCDatareader["asset_image"].ToString();
                serialid = objODBCDatareader["assetserial_gid"].ToString();
                msSQL = " INSERT INTO ams_trn_tasset2history (" +
                        " asset2history_gid," +
                        " custodiantracker_gid," +
                        " asset_gid," +
                        " asset2custodian_gid," +
                        " assetserial_gid," +
                        " status," +
                        " asset_image," +
                        " employee_gid," +
                        " employee_name," +
                        " issuedemployee_gid," +
                        " issued_by," +
                        " issued_date," +
                        " surrender_gid," +
                        " surrender_name," +
                        " surrender_date," +
                        " created_by," +
                        " created_date)" +
                        " VALUES(" +
                        "'" + MsgetAsset2historyGID + "'," +
                        "'" + objODBCDatareader["custodiantracker_gid"] + "'," +
                        "'" + objODBCDatareader["asset_gid"] + "'," +
                        "'" + objODBCDatareader["assetserial_gid"] + "'," +
                        "'" + objODBCDatareader["assetserial_gid"] + "'," +
                        "'Surrender'," +
                        "'" + objODBCDatareader["asset_image"] + "'," +
                        "'" + objODBCDatareader["employee_gid"] + "'," +
                        "'" + objODBCDatareader["employee_name"] + "'," +
                        "'" + objODBCDatareader["issuedemployee_gid"] + "'," +
                        "'" + objODBCDatareader["issued_by"] + "'," +
                        "'" + objODBCDatareader["issued_date"] + "'," +
                        "'" + employee_gid + "'," +
                        "'" + lsemployee_name + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                objODBCDatareader.Close();
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
        
            if (mnResult == 1)
            {
                objsurrenderstatus.status = true;
                objsurrenderstatus.message = "Surrender Done!";
                return true;
            }
            else
            {
                objsurrenderstatus.status = false;
                objsurrenderstatus.message = "Error Occured";
                return false;
            }
        }
    }
}