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
    public class DaViewMyAsset
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL;
    

        public bool DaGetMyAsset(string employee_gid, myassets objmyassets)
        {
            try
            {

                msSQL = " SELECT c.asset_name,b.asset_id,a.issued_by,date_format(a.issued_date,'%d-%m-%Y %h:%i %p') as issued_date,b.oe_serial as assetserial_id,a.asset_image,date_format(a.acknowledge_date,'%d-%m-%Y %h:%i %p') as acknowledge_date " +
                         " FROM ams_mst_tasset c  LEFT JOIN  ams_trn_tassetserial b ON c.asset_gid=b.asset_gid " +
                        " LEFT JOIN ams_trn_tasset2custodian a ON b.assetserial_gid = a.assetserial_gid" +
                         " WHERE custodiantracker_gid=(SELECT custodiantracker_gid from ams_trn_tcustodiantracker where employee_gid='" + employee_gid + "') AND a.status='Acknowledged' group by b.asset_id";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var Get_Details = new List<myassetsummary>();

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt_datatable.Rows)
                        Get_Details.Add(new myassetsummary
                        {
                            assetserial_id = (dr["assetserial_id"].ToString()),
                            asset_name = (dr["asset_name"].ToString()),
                            asset_id = (dr["asset_id"].ToString()),
                            issued_by = (dr["issued_by"].ToString()),
                            issued_date = (dr["issued_date"].ToString()),
                            asset_image = (dr["asset_image"].ToString()),
                            acknowledge_date = (dr["acknowledge_date"].ToString())
                        });
                    objmyassets.myassetsummary = Get_Details;

                }
                dt_datatable.Dispose();
                objmyassets.status = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}