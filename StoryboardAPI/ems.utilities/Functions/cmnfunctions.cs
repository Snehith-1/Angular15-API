using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO; 
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;


namespace ems.utilities.Functions
{
    public class cmnfunctions
    {
        ems.utilities.Functions.dbconn objdbconn = new ems.utilities.Functions.dbconn();
        OdbcCommand cmdQuery = new OdbcCommand();
        OdbcDataReader objreader;
        DataSet objdataset = new DataSet();
        string lsTempGid = string.Empty;
        int mnResult, ls_port;
        string msSQL, ls_username, ls_password, ls_server;
        string scalar = string.Empty;
        DataTable objTblRQ = new DataTable("objTblRQ");
        DataTable table = new DataTable("table");
        DataTable dt_table;
        DataColumn myCol0;
        string lblemployeereporting_to, lsemployeeGID;
        int lscount;
        String[] lsCCReceipients;
        string return_path, upload_gid, path, company_code, file_path, file_name;
        HttpRequest httpRequest;
        HttpPostedFile httpPostedFile;
        // Split By Expression
        MemoryStream ms = new MemoryStream();
        MemoryStream ms_stream = new MemoryStream();
        Stream ls_readStream;
        public string[] Split(string input, string pattern)
        {
            string[] elements = Regex.Split(input, pattern);
            return elements;
        }

        public string ConvertToAscii(string str)
        {
            int iIndex;
            int lenOfUserString;
            string newUserPass = string.Empty;
            string tmp;
            lenOfUserString = str.Length;
            for (iIndex = 0; iIndex < lenOfUserString; iIndex++)
            {
                tmp = str.Substring(iIndex, 1);
                tmp = (((int)Convert.ToChar(tmp)) - lenOfUserString).ToString();
                newUserPass = newUserPass + (tmp.Length < 3 ? "0" : "") + tmp;
            }
            return newUserPass;
        }

        public bool Mailer(string from, string to, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential("no-reply@samunnati.com", "Vision18");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Document Upload //


        // Get Master GID
        public bool mail(string to, string sub, string body)
        {
            try
            {
                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objreader = objdbconn.GetDataReader(msSQL);
                if (objreader.HasRows)
                {
                    ls_server = objreader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objreader["pop_port"]);
                    ls_username = objreader["pop_username"].ToString();
                    ls_password = objreader["pop_password"].ToString();
                }
                objreader.Close();
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                message.To.Add(new MailAddress(to));
                message.Subject = sub;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = ls_port;
                smtp.Host = ls_server; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string GetMasterGID(string pModule_Code)
        {
            DateTime date_value = DateTime.Now;
            int lsSeqNo = 0;
            string lsSeq_flag = null;
            string lscomapnyname = null;
            string lsfinyear = null;
            lsTempGid = null;

            msSQL = " SELECT year(fyear_start) AS finyear FROM adm_mst_tyearendactivities " +
                   " WHERE '" + date_value.ToString("yyyy-MM-dd") + "' BETWEEN fyear_start AND fyear_end ";
            objreader = objdbconn.GetDataReader(msSQL);
            if (objreader.HasRows == false)
            {
                objreader.Close();
                msSQL = " SELECT year(fyear_start) AS finyear FROM adm_mst_tyearendactivities " +
                        " ORDER BY finyear_gid DESC LIMIT 0,1 ";
                objreader = objdbconn.GetDataReader(msSQL);
                if (objreader.HasRows == true)
                {
                    lsfinyear = objreader["finyear"].ToString();
                    objreader.Close();
                }
            }
            else
            {
                lsfinyear = objreader["finyear"].ToString();
            }

            msSQL = " SELECT company_code FROM adm_mst_tcompany ";
            objreader = objdbconn.GetDataReader(msSQL);
            if (objreader.HasRows == true)
            {
                lscomapnyname = objreader["company_code"].ToString();

            }
            objreader.Close();
            msSQL = "SELECT sequence_flag FROM adm_mst_tsequence WHERE sequence_code='" + pModule_Code + "' AND finyear='" + lsfinyear + "'";
            objreader = objdbconn.GetDataReader(msSQL);
            if (objreader.HasRows == true)
            {
                lsSeq_flag = objreader["sequence_flag"].ToString();
            }
            objreader.Close();
            if (lsSeq_flag == "N")
            {
                msSQL = " SELECT sequence_curval, sequence_format, " +
                        " sequence_code FROM adm_mst_tsequence " +
                        " WHERE sequence_code = '" + pModule_Code + "' and finyear='" + lsfinyear + "'";
                objreader = objdbconn.GetDataReader(msSQL);

                if (objreader.HasRows == true)
                {
                    lsSeqNo = Convert.ToInt32(objreader["sequence_curval"]) + 1;
                    lsTempGid = pModule_Code + date_value.ToString("yyyyMMdd") + lsSeqNo;
                }
                objreader.Close();
            }
            else if (lsSeq_flag == "Y")
            {
                msSQL = " SELECT sequence_curval, sequence_format, " +
                        " sequence_code FROM adm_mst_tsequence " +
                        " WHERE sequence_code = '" + pModule_Code + "' and finyear='" + lsfinyear + "'";
                objreader = objdbconn.GetDataReader(msSQL);

                if (objreader.HasRows == true)
                {
                    lsSeqNo = Convert.ToInt32(objreader["sequence_curval"]) + 1;
                    lsTempGid = pModule_Code + lsSeqNo.ToString("D" + objreader["sequence_format"]);
                }
                objreader.Close();
            }
            msSQL = " UPDATE adm_mst_tsequence SET " +
                    " sequence_curval = '" + lsSeqNo + "' WHERE " +
                    " sequence_code = '" + pModule_Code + "' AND finyear='" + lsfinyear + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                if (lsTempGid == null)
                {
                    return "E";
                }
                else
                    return lsTempGid;
            }
            else
            {
                return "E";
            }
        }

        public void PopSummaryLGL(string lblEmployeeGID)
        {

            myCol0 = new DataColumn();
            myCol0.DataType = System.Type.GetType("System.String");
            myCol0.MaxLength = -1;
            myCol0.AllowDBNull = true;
            myCol0.ColumnName = "employee_gid";
            objTblRQ.Columns.Add(myCol0);
            DataColumn myCol1 = new DataColumn("hierarchy_level");
            myCol1.DataType = System.Type.GetType("System.Int32");
            myCol1.AllowDBNull = false;
            objTblRQ.Columns.Add(myCol1);
            objTblRQ.AcceptChanges();

            // Recursive Looping
            msSQL = " select a.employee_gid, a.hierarchy_level, concat(b.user_firstname, '-', b.user_code) as user" +
                    " from adm_mst_tsubmodule a  " + " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " + " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " + " where a.module_gid = 'LGL' and a.submodule_id='LGLLCMSRP' and a.employee_gid = '" + lblEmployeeGID + "' ";
            objreader = objdbconn.GetDataReader(msSQL);
            if (objreader.HasRows == true)
            {
                objreader.Read();
                DataRow myNewRow = objTblRQ.NewRow();
                myNewRow["employee_gid"] = objreader["employee_gid"].ToString();
                myNewRow["hierarchy_level"] = objreader["hierarchy_level"].ToString();
                objTblRQ.Rows.Add(myNewRow);
                objTblRQ.AcceptChanges();
            }
            objreader.Close();
            objdbconn.CloseConn();
            // childloop(HttpContext.Current.Session("employee_gid"))
            childloopTopLGL(lblEmployeeGID);
            foundRow(table);
            return;
        }
        public void PopSummary(string lblEmployeeGID, string lbluser, Double lscount)
        {

            myCol0 = new DataColumn();
            myCol0.DataType = System.Type.GetType("System.String");
            myCol0.MaxLength = -1;
            myCol0.AllowDBNull = true;
            myCol0.ColumnName = "employee_gid";
            objTblRQ.Columns.Add(myCol0);
            DataColumn myCol1 = new DataColumn("hierarchy_level");
            myCol1.DataType = System.Type.GetType("System.Int32");
            myCol1.AllowDBNull = false;
            objTblRQ.Columns.Add(myCol1);
            objTblRQ.AcceptChanges();

            // Recursive Looping
            msSQL = " select a.employee_gid, a.hierarchy_level, concat(b.user_firstname, '-', b.user_code) as user from adm_mst_tsubmodule a  " + " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " + " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " + " where a.module_gid = 'HRM' and a.submodule_id='HRMLEVARL' and a.employee_gid = '" + lblEmployeeGID + "' ";
            objreader = objdbconn.GetDataReader(msSQL);
            if (objreader.HasRows == true)
            {
                objreader.Read();
                DataRow myNewRow = objTblRQ.NewRow();
                myNewRow["employee_gid"] = objreader["employee_gid"].ToString();
                myNewRow["hierarchy_level"] = objreader["hierarchy_level"].ToString();
                objTblRQ.Rows.Add(myNewRow);
                objTblRQ.AcceptChanges();
            }
            objreader.Close();
            objdbconn.CloseConn();
            // childloop(HttpContext.Current.Session("employee_gid"))
            childloopTop(lblEmployeeGID);
            foundRow(table);
            return;
        }
        public DataTable foundRow(DataTable table)
        {
            lscount = objTblRQ.Rows.Count;
            table = objTblRQ;
            foundcount(lscount);
            return table;
        }
        public Double foundcount(Double lscount)
        {

            lscount = objTblRQ.Rows.Count;

            return lscount;
        }
        public void childloopTopLGL(string employee)
        {
            msSQL = " select a.employeereporting_to, concat(b.user_firstname, '-', b.user_code) as user " +
                    " from adm_mst_tmodule2employee a " + " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid " +
                    " inner join adm_mst_tuser b on c.user_gid = b.user_gid " + " where a.module_gid = 'LGL' " +
                    " and a.employee_gid = '" + employee + "'" + " and a.hierarchy_level <> '-1' ";
            objreader = objdbconn.GetDataReader(msSQL);
            objreader.Read();
            if (objreader.HasRows == true)
            {
                lblemployeereporting_to = objreader["employeereporting_to"].ToString();
                objreader.Close();
                msSQL = " select a.employee_gid, a.hierarchy_level, concat(b.user_firstname, '-', b.user_code) as user from adm_mst_tsubmodule a  " +
                    " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " + " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " +
                    " where a.module_gid = 'LGL' and a.submodule_id='LGLLCMSRP' and a.employee_gid = '" + lblemployeereporting_to + "' ";
                objreader = objdbconn.GetDataReader(msSQL);
                if (objreader.HasRows == true)
                {
                    objreader.Read();
                    DataRow myNewRow = objTblRQ.NewRow();
                    myNewRow["employee_gid"] = objreader["employee_gid"].ToString();
                    myNewRow["hierarchy_level"] = objreader["hierarchy_level"].ToString();
                    objTblRQ.Rows.Add(myNewRow);
                    objTblRQ.AcceptChanges();
                }
                objreader.Close();
                childloopTopLGL(lblemployeereporting_to);
            }
            objreader.Close();
            objdbconn.CloseConn();
            return;
        }

        public void childloopTop(string employee)
        {
            msSQL = " select a.employeereporting_to, concat(b.user_firstname, '-', b.user_code) as user " +
                    " from adm_mst_tmodule2employee a " + " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid " +
                    " inner join adm_mst_tuser b on c.user_gid = b.user_gid " + " where a.module_gid = 'HRM' " +
                    " and a.employee_gid = '" + employee + "'" + " and a.hierarchy_level <> '-1' ";
            objreader = objdbconn.GetDataReader(msSQL);
            objreader.Read();
            if (objreader.HasRows == true)
            {
                lblemployeereporting_to = objreader["employeereporting_to"].ToString();
                objreader.Close();
                msSQL = " select a.employee_gid, a.hierarchy_level, concat(b.user_firstname, '-', b.user_code) as user from adm_mst_tsubmodule a  " +
                    " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " + " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " +
                    " where a.module_gid = 'HRM' and a.submodule_id='HRMLEVARL' and a.employee_gid = '" + lblemployeereporting_to + "' ";
                objreader = objdbconn.GetDataReader(msSQL);
                if (objreader.HasRows == true)
                {
                    objreader.Read();
                    DataRow myNewRow = objTblRQ.NewRow();
                    myNewRow["employee_gid"] = objreader["employee_gid"].ToString();
                    myNewRow["hierarchy_level"] = objreader["hierarchy_level"].ToString();
                    objTblRQ.Rows.Add(myNewRow);
                    objTblRQ.AcceptChanges();
                }
                objreader.Close();
                childloopTop(lblemployeereporting_to);
            }
            objreader.Close();
            objdbconn.CloseConn();
            return;
        }


        public string childloop(string employee)
        {
            msSQL = " select a.*, concat(b.user_firstname, '-', b.user_code) as user  from adm_mst_tmodule2employee a  " +
                " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " +
                " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " +
                " where a.module_gid = 'HRM'  " +
                " and a.employeereporting_to = '" + employee + "'";
            dt_table = objdbconn.GetDataTable(msSQL);
            foreach (DataRow dr_datarow in dt_table.Rows)
            {
                msSQL = " select a.*, b.user_gid  from adm_mst_tmodule2employee a  " +
                    " inner join hrm_mst_temployee c on a.employee_gid = c.employee_gid  " +
                    " inner join adm_mst_tuser b on c.user_gid = b.user_gid  " +
                    " where a.module_gid = 'HRM' ";
                msSQL += " and a.employee_gid = '" + dr_datarow["employee_gid"].ToString() + "'";
                objreader = objdbconn.GetDataReader(msSQL);
                if (objreader.HasRows == true)
                {
                    objreader.Read();
                    lsemployeeGID = lsemployeeGID + "'" + objreader["employee_gid"].ToString() + "',";
                }
                objreader.Close();
                childloop(dr_datarow["employee_gid"].ToString());
            }

            dt_table.Dispose();
            return lsemployeeGID;
        }

        // Send Mail

        public bool Mail(string to, string cc, string sub, string body)
        {
            try
            {
                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objreader = objdbconn.GetDataReader(msSQL);
                if (objreader.HasRows)
                {
                    ls_server = objreader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objreader["pop_port"]);
                    ls_username = objreader["pop_username"].ToString();
                    ls_password = objreader["pop_password"].ToString();
                }
                objreader.Close();
                objdbconn.CloseConn();
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                message.To.Add(new MailAddress(to));
                if (cc != null & cc != string.Empty & cc != "")
                {
                    lsCCReceipients = cc.Split(',');
                    if (cc.Length == 0)
                    {
                        message.CC.Add(new MailAddress(cc));
                    }
                    else
                    {
                        foreach (string CCEmail in lsCCReceipients)
                        {
                            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        }
                    }
                }
                message.Subject = sub;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = ls_port;
                smtp.Host = ls_server; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public int SendSMTP2(string strFrom, string strpwd, string strTo, string strSubject, string strBody, string strCC, string strBCC, string strAttachments)
        {

            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
            objreader = objdbconn.GetDataReader(msSQL);
            if (objreader.HasRows)
            {
                ls_server = objreader["pop_server"].ToString();
                ls_port = Convert.ToInt32(objreader["pop_port"]);
                ls_username = objreader["pop_username"].ToString();
                ls_password = objreader["pop_password"].ToString();
            }
            objreader.Close();
            MailMessage objMailMessage = new MailMessage();
            objMailMessage.From = new MailAddress(strFrom);
            // Set the recepient address of the mail message
            objMailMessage.To.Add(new MailAddress(strTo));


            if (strCC != null & strCC != string.Empty)
            {
                lsCCReceipients = strCC.Split(',');
                if (strCC.Length == 0)
                {
                    objMailMessage.CC.Add(new MailAddress(strCC));
                }
                else
                {
                    foreach (string CCEmail in lsCCReceipients)
                    {
                        objMailMessage.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                    }
                }
            }

            if (strBCC != null & strBCC != string.Empty)
            {
                objMailMessage.Bcc.Add(new MailAddress(strBCC));
            }

            objMailMessage.Subject = strSubject;
            // Set the body of the mail message
            objMailMessage.Body = strBody;

            // Set the format of the mail message body as HTML
            objMailMessage.IsBodyHtml = true;
            //  Set the priority of the mail message to normal
            objMailMessage.Priority = MailPriority.Normal;
            SmtpClient objSmtpClient = new SmtpClient();
            objSmtpClient.Host = ls_server;
            objSmtpClient.Port = ls_port;
            objSmtpClient.EnableSsl = true;
            objSmtpClient.UseDefaultCredentials = true;
            objSmtpClient.Credentials = new NetworkCredential(strFrom, strpwd);
            try
            {
                objSmtpClient.Send(objMailMessage);
            }
            catch
            {
                return 0;
            }

            return 1;
        }
        public DataTable ExcelToDataTable(string FileName,string range)
        {
            DataTable datatable =new DataTable();
            int totalSheet = 1;
            string lsConnectionString = string.Empty;
            string fileExtension = Path.GetExtension(FileName);
            if (fileExtension == ".xls")
            {
                lsConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
            }
            else if(fileExtension == ".xlsx")
            {
                lsConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0';";
            }
           
            using (OleDbConnection objConn = new OleDbConnection(lsConnectionString))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                sheetName = sheetName.Replace("'", "").Trim () + range;
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM ["+ sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
               
                datatable = ds.Tables["excelData"];  
                objConn.Close();
            }
            return datatable;
        }
        public string uploadFile(string path, string file_name)
        {
            int iUploadedCnt = 0;
            string sPath = "";
            //    sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/locker/");
            sPath = path;
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    if (!File.Exists(sPath + file_name))
                    {
                        hpf.SaveAs(sPath + file_name);
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }
            if (iUploadedCnt > 0)
            {
                return iUploadedCnt + " Files Uploaded Successfully";
            }
            else
            {
                return "Upload Failed";
            }
        }


        public void sendMessage(string number, string message)
        {

            WebClient webClient = new WebClient();
            dbconn objdbconn = new dbconn();
            OdbcDataReader objODBCDataReader;
            string INSTANCE_ID;
            string CLIENT_ID;
            string CLIENT_SECRET, API_URL;
            msSQL = " select whatsapp_client_id,whatsapp_instance_id,whatsapp_client_secret from adm_mst_tcompany ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                INSTANCE_ID = objODBCDataReader["whatsapp_instance_id"].ToString();
                CLIENT_ID = objODBCDataReader["whatsapp_client_id"].ToString();
                CLIENT_SECRET = objODBCDataReader["whatsapp_client_secret"].ToString();
                API_URL = "http://enterprise.whatsmate.net/v3/whatsapp/single/text/message/" + INSTANCE_ID;
                objODBCDataReader.Close();
                objdbconn.CloseConn();
                try
                {
                    Payload payloadObj = new Payload(number, message);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string postData = serializer.Serialize(payloadObj);

                    webClient.Headers["content-type"] = "application/json";
                    webClient.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
                    webClient.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;

                    webClient.Encoding = Encoding.UTF8;
                    string response = webClient.UploadString(API_URL, postData);
                    Console.WriteLine(response);
                }
                catch
                {

                }
            }
            else
            {
                objODBCDataReader.Close();
                objdbconn.CloseConn();
            }

        }

        private class Payload
        {
            public string number;
            public string message;

            public Payload(string num, string msg)
            {
                number = num;
                message = msg;
            }
        }

        public void LogForAudit(string strVal)
        {

            try
            {
                string lspath = HttpContext.Current.Server.MapPath("../../documents/") + ConfigurationManager.AppSettings["company_code"] + GetMasterGID("LOGF") + "_" + System.IO.Path.GetFileName(HttpContext.Current.Request.Url.ToString()).Replace(".aspx", string.Empty).Replace("?ls=", string.Empty) + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if ((!System.IO.File.Exists(lspath)))
                    System.IO.File.Create(lspath).Dispose();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath);
                sw.WriteLine(strVal);
                sw.Close();
            }
            catch
            {
            }
        }

        public string  PopTransactionUpload(string Document_path,string employee_gid,string module_name,string folder_name)
        {

            try
            {
                string lsfile_gid = "";
                string msdocument_gid = GetMasterGID("UPLF");
                msSQL = " select comapany_code from adm_mst_tcompany where 1=1";
                company_code = objdbconn.GetExecuteScalar(msSQL);
                file_path = ConfigurationManager.AppSettings["file_path"].ToString();
                path = file_path + "/documents/" + company_code + "/" + module_name + "/" + folder_name;
                if (httpRequest.Files.Count > 0)
                {
                    file_name = httpPostedFile.FileName;
                    ls_readStream = httpPostedFile.InputStream;
                    ls_readStream.CopyTo(ms);

                    file_name = msdocument_gid + file_name;

                    path = path + lsfile_gid;
                    FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);

                    ms.WriteTo(file);
                    file.Close();
                    ms.Close();
                    
                }
                return path;
            }
            catch
            {
                return "error";
            }
        }

        public string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}