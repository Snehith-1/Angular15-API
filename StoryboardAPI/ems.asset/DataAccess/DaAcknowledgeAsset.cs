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
    public class DaAcknowledgeAsset
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL;
        string serialid;
        string MsgetAsset2historyGID;
        int mnResult;
        string assetImage;
        string custodiantracker_gid, asset_gid, assetserial_gid, issued_by;
        string employee_gid, employee_name, issuedemployee_gid, issued_date;

        public bool DaGetAcknowledgement(string employee_gid, acknowledgementasset objacknowledgementasset)
        {

            msSQL = " SELECT a.asset_image,a.issued_by, date_format(a.issued_date,'%d-%m-%Y %h:%i %p') as issued_date,b.asset_id,a.asset2custodian_gid," +
                    " b.oe_serial as assetserial_id,c.asset_name,a.asset_image" +
                    " FROM ams_trn_tasset2custodian a " +
                    " LEFT JOIN ams_trn_tassetserial b ON a.assetserial_gid=b.assetserial_gid " +
                    " LEFT JOIN ams_mst_tasset c ON c.asset_gid=a.asset_gid " +
                    " WHERE custodiantracker_gid=(SELECT custodiantracker_gid from ams_trn_tcustodiantracker where employee_gid='" + employee_gid + "') AND a.status='Acknowledgement Pending'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_acksummary = new List<acksummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_acksummary.Add(new acksummary
                    {
                        asset2custodian_gid = (dr_datarow["asset2custodian_gid"].ToString ()),
                        assetserial_id = (dr_datarow["assetserial_id"].ToString()),
                        asset_id = (dr_datarow["asset_id"].ToString()),
                        asset_name = (dr_datarow["asset_name"].ToString()),
                        issued_by = (dr_datarow["issued_by"].ToString()),
                        issued_date = (dr_datarow["issued_date"].ToString()),
                        asset_image = (dr_datarow["asset_image"].ToString()),
                        acknowledge_flag = "Y"
                    });
                }
                objacknowledgementasset.acksummary = get_acksummary;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaPostAcknowledgementReject(ackrejectstatus objackstatus,string user_gid)
        {

            MsgetAsset2historyGID = objcmnfunctions.GetMasterGID("AST2H");
            msSQL = " SELECT a.custodiantracker_gid,a.asset_gid,a.assetserial_gid,a.asset_image, " +
                    " b.employee_gid,b.employee_name,a.issuedemployee_gid,a.issued_by,date_format(a.issued_date,'%Y-%m-%d') as issued_date " +
                    " FROM ams_trn_tasset2custodian a " +
                    " LEFT JOIN ams_trn_tcustodiantracker b ON a.custodiantracker_gid=b.custodiantracker_gid " +
                    " WHERE a.asset2custodian_gid='" + objackstatus.asset2custodian_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                serialid = objODBCDatareader["assetserial_gid"].ToString();
                assetImage = objODBCDatareader["asset_image"].ToString();
                custodiantracker_gid = objODBCDatareader["custodiantracker_gid"].ToString();
                asset_gid = objODBCDatareader["asset_gid"].ToString();
                assetserial_gid = objODBCDatareader["assetserial_gid"].ToString();
                employee_gid = objODBCDatareader["employee_gid"].ToString();
                employee_name = objODBCDatareader["employee_name"].ToString();
                issuedemployee_gid = objODBCDatareader["issuedemployee_gid"].ToString();
                issued_by = objODBCDatareader["issued_by"].ToString();
                issued_date = objODBCDatareader["issued_date"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " INSERT INTO ams_trn_tasset2history (" +
                     " asset2history_gid," +
                     " custodiantracker_gid," +
                     " asset_gid," +
                     " assetserial_gid," +
                     " asset2custodian_gid,"+
                     " status," +
                     " asset_image," +
                     " employee_gid," +
                     " employee_name," +
                     " issuedemployee_gid," +
                     " issued_by," +
                     " issued_date," +
                     " rejected_reason," +
                     " created_by," +
                     " created_date)" +
                     " VALUES(" +
                     "'" + MsgetAsset2historyGID + "'," +
                     "'" + custodiantracker_gid + "'," +
                     "'" + asset_gid + "'," +
                     "'" + assetserial_gid + "'," +
                     "'" + objackstatus.asset2custodian_gid + "'," +
                     "'Rejected'," +
                     "'" + assetImage + "'," +
                     "'" + employee_gid + "'," +
                     "'" + employee_name + "'," +
                     "'" + issuedemployee_gid + "'," +
                     "'" + issued_by + "'," +
                     "'" + issued_date + "'," +
                     "'" + objackstatus.reason_reject + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " UPDATE ams_trn_tassetserial SET status='Free'," +
                  " acknowledgement_flag='Y'," +
                  " rejected_asset='Rejected'," +
                  " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                  " WHERE assetserial_gid='" + serialid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
          
           
            if (mnResult != 0)
            {
                msSQL = " delete from ams_trn_tasset2custodian " +
                        " WHERE asset2custodian_gid = '" + objackstatus.asset2custodian_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                objackstatus.status = true;
                objackstatus.message = "Acknowledgement Rejected Successfully!";

                return true;
            }
            else
            {
                objackstatus.status = false;
                objackstatus.message = "Error Occured";
                return false;
            }
            
        }

        public bool DaPostAcknowledgementStatus(string asset2custodian_gid, ackstatus objackstatus, string user_gid)
        {
            msSQL = " UPDATE ams_trn_tasset2custodian SET status='Acknowledged',acknowledge_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " WHERE asset2custodian_gid='"+ asset2custodian_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            MsgetAsset2historyGID = objcmnfunctions.GetMasterGID("AST2H");
            msSQL = " SELECT a.custodiantracker_gid,a.asset_gid,a.assetserial_gid,a.asset_image, " +
                    " b.employee_gid,b.employee_name,a.issuedemployee_gid,a.issued_by,date_format(a.issued_date,'%Y-%m-%d') as issued_date " +
                    " FROM ams_trn_tasset2custodian a " +
                    " LEFT JOIN ams_trn_tcustodiantracker b ON a.custodiantracker_gid=b.custodiantracker_gid " +
                    " WHERE a.asset2custodian_gid='" + asset2custodian_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                serialid = objODBCDatareader["assetserial_gid"].ToString();
                assetImage = objODBCDatareader["asset_image"].ToString();
                objODBCDatareader.Read();
                msSQL = " INSERT INTO ams_trn_tasset2history (" +
                        " asset2history_gid," +
                        " custodiantracker_gid," +
                        " asset_gid," +
                        " assetserial_gid," +
                        " status," +
                        " asset_image," +
                        " employee_gid," +
                        " employee_name," +
                        " issuedemployee_gid," +
                        " issued_by," +
                        " issued_date," +
                        " created_by," +
                        " created_date)" +
                        " VALUES(" +
                        "'" + MsgetAsset2historyGID + "'," +
                        "'" + objODBCDatareader["custodiantracker_gid"].ToString() + "'," +
                        "'" + objODBCDatareader["asset_gid"].ToString() + "'," +
                        "'" + objODBCDatareader["assetserial_gid"].ToString() + "'," +
                        "'Acknowledged'," +
                        "'" + objODBCDatareader["asset_image"].ToString() + "'," +
                        "'" + objODBCDatareader["employee_gid"].ToString() + "'," +
                        "'" + objODBCDatareader["employee_name"].ToString() + "'," +
                        "'" + objODBCDatareader["issuedemployee_gid"].ToString() + "'," +
                        "'" + objODBCDatareader["issued_by"].ToString() + "'," +
                        "'" + objODBCDatareader["issued_date"].ToString() + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                objODBCDatareader.Close();
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult == 1)
            {
                objackstatus.status = true;
                objackstatus.message = "Acknowledgement Done!";
                return true;
            }
            else
            {
                objackstatus.status = false;
                objackstatus.message = "Error Occured";
                return false;
            }
        }
    }
}