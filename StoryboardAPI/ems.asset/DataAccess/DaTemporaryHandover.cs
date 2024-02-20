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
    public class DaTemporaryHandover
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string asset2custodian_gid;
        string lsassetserial_gid;
        string lsasset_image;
        string msSQL;
        int mnResult;
        public bool DaGetTempHandover(string employee_gid, tmphandoverassets objtmphandoverassets)
        {

            string[] tmparry = new string[2];
            msSQL = " SELECT e.department_name,f.designation_name,c.asset_name,b.asset_id,a.issued_by,b.oe_serial as assetserial_id,date_format(a.issued_date,'%d-%m-%Y %h:%i %p') as issued_date,a.asset_image,a.temporaryhandover_gid,a.temporaryhandover_name, " +
                    " date_format(a.temporaryhandover_date,'%d-%m-%Y %h:%i %p') as temporaryhandover_date,a.status FROM ams_mst_tasset c  LEFT JOIN  ams_trn_tassetserial b ON c.asset_gid = b.asset_gid " +
                    " LEFT JOIN ams_trn_tasset2custodian a ON b.assetserial_gid = a.assetserial_gid " +
                    " LEFT JOIN hrm_mst_temployee d ON a.temporaryhandover_gid = d.employee_gid " +
                    " LEFT JOIN hrm_mst_tdepartment e ON d.department_gid = e.department_gid " +
                    " LEFT JOIN adm_mst_tdesignation f ON d.designation_gid = f.designation_gid " +
                    " WHERE custodiantracker_gid=(SELECT custodiantracker_gid from ams_trn_tcustodiantracker where employee_gid='" + employee_gid + "') and a.status='Temporary Handover Pending' group by b.asset_id";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var Get_Details = new List<tmphandoversummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    tmparry = objcmnfunctions.Split(dr["temporaryhandover_name"].ToString(), "/");
                    Get_Details.Add(new tmphandoversummary
                    {
                        assetserial_id = (dr["assetserial_id"].ToString()),
                        asset_name = (dr["asset_name"].ToString()),
                        asset_id = (dr["asset_id"].ToString()),
                        issued_by = (dr["issued_by"].ToString()),
                        issued_date = (dr["issued_date"].ToString()),
                        asset_image = (dr["asset_image"].ToString()),
                        tmphandover_employeecode = tmparry[0],
                        tmphandover_employeename = tmparry[1],
                        tmphandover_date = (dr["temporaryhandover_date"].ToString()),
                        tmphandover_employeeid = (dr["temporaryhandover_gid"].ToString()),
                        tmphandover_employeedept = (dr["department_name"].ToString()),
                        tmphandover_employeedesg = (dr["designation_name"].ToString()),
                        tmphandover_status = (dr["status"].ToString())
                    });
                }
                objtmphandoverassets.tmphandoversummary = Get_Details;

            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.custodiantracker_gid,c.asset_name,b.asset_id,a.asset_image,a.issued_by,date_format(a.issued_date,'%d-%m-%Y %h:%i %p') as issued_date, " +
                   " date_format(a.temporaryhandover_date,'%d-%m-%Y %h:%i %p') as temporaryhandover_date,a.status,b.oe_serial as assetserial_id FROM ams_mst_tasset c  LEFT JOIN  ams_trn_tassetserial b ON c.asset_gid = b.asset_gid " +
                   " LEFT JOIN ams_trn_tasset2custodian a ON b.assetserial_gid = a.assetserial_gid " +
                   " WHERE temporaryhandover_gid='" + employee_gid + "' and a.status='Temporary Handover' group by b.asset_id";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var Get_Detail = new List<tempHoldersummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    msSQL = " SELECT e.department_name,f.designation_name,b.user_code,concat(b.user_firstname, ' ',b.user_lastname) as user_name,a.employee_gid FROM hrm_mst_temployee a " +
                            " LEFT JOIN adm_mst_tuser b ON a.user_gid=b.user_gid " +
                            " LEFT JOIN hrm_mst_tdepartment e ON a.department_gid = e.department_gid " +
                            " LEFT JOIN adm_mst_tdesignation f ON a.designation_gid = f.designation_gid " +
                            " WHERE a.employee_gid=(SELECT employee_gid from ams_trn_tcustodiantracker where custodiantracker_gid='" + dr["custodiantracker_gid"].ToString() + "')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        try
                        {
                            Get_Detail.Add(new tempHoldersummary
                            {
                                assetserial_id = (dr["assetserial_id"].ToString()),
                                asset_id = (dr["asset_id"].ToString()),
                                asset_image = (dr["asset_image"].ToString()),
                                asset_name = (dr["asset_name"].ToString()),
                                prev_employeecode = (objODBCDatareader["user_code"].ToString()),
                                prev_employeedept = (objODBCDatareader["department_name"].ToString()),
                                prev_employeedesg = (objODBCDatareader["designation_name"].ToString()),
                                prev_employeeid = (objODBCDatareader["employee_gid"].ToString()),
                                prev_employeename = (objODBCDatareader["user_name"].ToString()),
                                issued_by = (dr["issued_by"].ToString()),
                                issued_date = (dr["issued_date"].ToString()),
                                tmphandover_date = (dr["temporaryhandover_date"].ToString()),
                                tmphandover_status = (dr["status"].ToString())
                            });
                        }
                        catch (Exception ex)
                        {
                            var msg = ex.ToString();
                        }
                    }
                    objODBCDatareader.Close();
                }
                objtmphandoverassets.tempHoldersummary = Get_Detail;
            }

            msSQL = " SELECT a.custodiantracker_gid,c.asset_name,b.asset_id,a.asset_image,a.issued_by,date_format(a.issued_date,'%d-%m-%Y %h:%i %p')  as issued_date, " +
                   " date_format(a.temporaryhandover_date,'%d-%m-%Y %h:%i %p') as temporaryhandover_date,a.status,b.oe_serial as assetserial_id FROM ams_mst_tasset c  LEFT JOIN  ams_trn_tassetserial b ON c.asset_gid = b.asset_gid " +
                   " LEFT JOIN ams_trn_tasset2custodian a ON b.assetserial_gid = a.assetserial_gid " +
                   " WHERE temporaryhandover_gid='" + employee_gid + "' and a.status in ('Temporary Handover-holding','Temporary Handover Surrender Pending') group by b.asset_id";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var Get_Detailed = new List<tempHoldinsassetsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    msSQL = " SELECT e.department_name,f.designation_name,b.user_code,concat(b.user_firstname, ' ',b.user_lastname) as user_name,a.employee_gid FROM hrm_mst_temployee a " +
                            " LEFT JOIN adm_mst_tuser b ON a.user_gid=b.user_gid " +
                            " LEFT JOIN hrm_mst_tdepartment e ON a.department_gid = e.department_gid " +
                            " LEFT JOIN adm_mst_tdesignation f ON a.designation_gid = f.designation_gid " +
                            " WHERE a.employee_gid=(SELECT employee_gid from ams_trn_tcustodiantracker where custodiantracker_gid='" + dr["custodiantracker_gid"].ToString() + "')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        try
                        {
                            Get_Detailed.Add(new tempHoldinsassetsummary
                            {
                                assetserial_id = (dr["assetserial_id"].ToString()),
                                asset_id = (dr["asset_id"].ToString()),
                                asset_image = (dr["asset_image"].ToString()),
                                asset_name = (dr["asset_name"].ToString()),
                                prev_employeecode = (objODBCDatareader["user_code"].ToString()),
                                prev_employeedept = (objODBCDatareader["department_name"].ToString()),
                                prev_employeedesg = (objODBCDatareader["designation_name"].ToString()),
                                prev_employeeid = (objODBCDatareader["employee_gid"].ToString()),
                                prev_employeename = (objODBCDatareader["user_name"].ToString()),
                                issued_by = (dr["issued_by"].ToString()),
                                issued_date = (dr["issued_date"].ToString()),
                                tmphandover_date = (dr["temporaryhandover_date"].ToString()),
                                tmphandover_status = (dr["status"].ToString())
                            });
                        }
                        catch (Exception ex)
                        {
                            var msg = ex.ToString();
                        }
                    }
                    objODBCDatareader.Close();
                }
                objtmphandoverassets.tempHoldinsassetsummary = Get_Detailed;
            }

            msSQL = " SELECT a.custodiantracker_gid,c.asset_name,b.asset_id,a.asset_image,a.issued_by,date_format(a.issued_date,'%d-%m-%Y %h:%i %p') as issued_date, " +
                  " date_format(a.temporaryhandover_date,'%d-%m-%Y %h:%i %p') as temporaryhandover_date,a.status,b.oe_serial as assetserial_id FROM ams_mst_tasset c  LEFT JOIN  ams_trn_tassetserial b ON c.asset_gid = b.asset_gid " +
                  " LEFT JOIN ams_trn_tasset2custodian a ON b.assetserial_gid = a.assetserial_gid " +
                  " WHERE temporaryhandover_gid='" + employee_gid + "' and a.status='Temporary Handover Surrender' group by b.asset_id ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var Get_Detailer = new List<tempadminsurrendersummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    msSQL = " SELECT e.department_name,f.designation_name,b.user_code,concat(b.user_firstname, ' ',b.user_lastname) as user_name,a.employee_gid FROM hrm_mst_temployee a " +
                            " LEFT JOIN adm_mst_tuser b ON a.user_gid=b.user_gid " +
                            " LEFT JOIN hrm_mst_tdepartment e ON a.department_gid = e.department_gid " +
                            " LEFT JOIN adm_mst_tdesignation f ON a.designation_gid = f.designation_gid " +
                            " WHERE a.employee_gid=(SELECT employee_gid from ams_trn_tcustodiantracker where custodiantracker_gid='" + dr["custodiantracker_gid"].ToString() + "')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        try
                        {
                            Get_Detailer.Add(new tempadminsurrendersummary
                            {
                                assetserial_id = (dr["assetserial_id"].ToString()),
                                asset_id = (dr["asset_id"].ToString()),
                                asset_image = (dr["asset_image"].ToString()),
                                asset_name = (dr["asset_name"].ToString()),
                                prev_employeecode = (objODBCDatareader["user_code"].ToString()),
                                prev_employeedept = (objODBCDatareader["department_name"].ToString()),
                                prev_employeedesg = (objODBCDatareader["designation_name"].ToString()),
                                prev_employeeid = (objODBCDatareader["employee_gid"].ToString()),
                                prev_employeename = (objODBCDatareader["user_name"].ToString()),
                                issued_by = (dr["issued_by"].ToString()),
                                issued_date = (dr["issued_date"].ToString()),
                                tmphandover_date = (dr["temporaryhandover_date"].ToString()),
                                tmphandover_status = (dr["status"].ToString())
                            });
                        }
                        catch (Exception ex)
                        {
                            var msg = ex.ToString();
                        }
                    }
                    objODBCDatareader.Close();
                }
                objtmphandoverassets.tempadminsurrendersummary = Get_Detailer;
            }
            dt_datatable.Dispose();

            return true;
        }
        public bool DaPostTmpSurrenderHandover(string asset_id, tmphandoverstatus objtmphandoverstatus)
        {
            msSQL = "UPDATE ams_trn_tasset2custodian SET status='Temporary Handover' WHERE assetserial_gid=(SELECT assetserial_gid FROM ams_trn_tassetserial WHERE asset_id='" + asset_id + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " UPDATE ams_trn_tasset2history SET status='Temporary Handover'" +
                    " WHERE assetserial_gid=(SELECT assetserial_gid FROM ams_trn_tassetserial WHERE asset_id='" + asset_id + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                objtmphandoverstatus.status = true;
                objtmphandoverstatus.message = "Temporary Handover Done!";
                return true;
            }
            else
            {
                objtmphandoverstatus.status = false;
                objtmphandoverstatus.message = "Error Occured";
                return false;
            }
        }
        // Holding Asset...//
        public bool DaPostHoldingAsset(string asset_id, holdingasset values, string employee_gid)
        {

            msSQL = " select asset2custodian_gid from ams_trn_tasset2custodian a " +
                  " left join ams_trn_tassetserial b on a.assetserial_gid = b.assetserial_gid where b.asset_id = '" + asset_id + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                asset2custodian_gid = objODBCDatareader["asset2custodian_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select assetserial_gid from ams_trn_tasset2custodian where  asset2custodian_gid='" + asset2custodian_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                lsassetserial_gid = objODBCDatareader["assetserial_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ams_trn_tasset2custodian set status='Temporary Handover-holding'" +
                    " where asset2custodian_gid='" + asset2custodian_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ams_trn_tassetserial set status='Temporary Handover-holding',acknowledgement_flag='N'," +
                    " updated_by='" + employee_gid + "', updated_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'" +
                    " where assetserial_gid='" + lsassetserial_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ams_trn_tasset2history set status='Temporary Handover-holding'" +
                    " where asset2custodian_gid='" + asset2custodian_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Holding Asset Done!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
            return true;
        }
        // Surrender to IT Admin....//
        public bool DaPostSurrenderITAdmin(string asset_id, surrenderitadmin values, string employee_gid)
        {

            msSQL = " select asset2custodian_gid from ams_trn_tasset2custodian a " +
                 " left join ams_trn_tassetserial b on a.assetserial_gid = b.assetserial_gid where b.asset_id = '" + asset_id + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                asset2custodian_gid = objODBCDatareader["asset2custodian_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update ams_trn_tasset2history set status='Temporary Handover Surrender'" +
                   " where asset2custodian_gid='" + asset2custodian_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ams_trn_tasset2custodian set status='Temporary Handover Surrender'" +
                    " where asset2custodian_gid='" + asset2custodian_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select assetserial_gid,asset_image from ams_trn_tasset2custodian " +
                    " where asset2custodian_gid='" + asset2custodian_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsassetserial_gid = objODBCDatareader["assetserial_gid"].ToString();
                lsasset_image = objODBCDatareader["asset_image"].ToString();

            }
            objODBCDatareader.Close();
            msSQL = " update ams_trn_tassetserial set status='Temporary Handover Surrender'," +
                    " asset_image='" + lsasset_image + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " acknowledgement_flag='N'" +
                    " where assetserial_gid='" + lsassetserial_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Surrender to IT Admin Done!";
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
            }
            return true;
        }

    }
}
