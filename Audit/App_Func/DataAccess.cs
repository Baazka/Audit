using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace Audit.App_Func
{
    public class DataAccess
    {
        public static DataResponse LibraryList(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                SqlCommand cmd = new SqlCommand("LibraryList");
                cmd.Parameters.Add("@LibraryList", SqlDbType.Xml).Value = request.Element("Parameters").Element("LibraryList").ToString();
                
                DataSet ds = DataConnection.GetDataSet(cmd);

                ds.DataSetName = "ResponseData";

                int index = 0;
                foreach (XElement x in request.Element("Parameters").Element("LibraryList").Elements("LibraryName"))
                {
                    if (ds.Tables.Count > index) ds.Tables[index].TableName = x.Value;
                    index++;
                }

                //if (ds.Tables.Contains("Aimag") && ds.Tables.Contains("AimagSoum"))
                //{
                //    DataRelation relation = new DataRelation("ParentChild", ds.Tables["Aimag"].Columns["ID"], ds.Tables["AimagSoum"].Columns["AimagID"], true);
                //    relation.Nested = true;
                //    ds.Relations.Add(relation);
                //}

                StringWriter sw = new StringWriter();
                ds.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
                response.CreateResponse();
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse UserLogin(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["RegConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_CHECK_USER";

                // Set parameters
                OracleParameter retParam = cmd.Parameters.Add(":Ret_val",
                    OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);
                cmd.Parameters.Add(":P_LOGINNAME", OracleDbType.Varchar2, request.Element("Parameters").Element("Username").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_PASSWORD", OracleDbType.Varchar2, request.Element("Parameters").Element("Password").Value, System.Data.ParameterDirection.Input);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                con.Close();

                object responseValue = retParam.Value;

                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0? true:false;

                if (responseVal)
                {
                    XElement xmlResponseData = new XElement("UserID", responseValue);
                    response.CreateResponse(xmlResponseData);
                }
                response.CreateResponse(responseVal, string.Empty, "Нэр нууц үг буруу байна.");
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse UserProfile(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["RegConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT US.USER_ID, US.USER_CODE, US.USER_NAME, US.USER_DEPARTMENT_ID, US.USER_TYPE_ID, US.USER_EMAIL, US.USER_REG_DATE, RD.DEPARTMENT_NAME, UST.USER_TYPE_NAME " +
                         "FROM SYSTEM_USER US " +
                         "INNER JOIN REF_DEPARTMENT RD ON RD.DEPARTMENT_ID = US.USER_DEPARTMENT_ID " +
                         "INNER JOIN SYSTEM_USER_TYPE UST ON UST.USER_TYPE_ID = US.USER_TYPE_ID WHERE USER_ID = :USER_ID";

                // Set parameters
                //OracleParameter retParam = cmd.Parameters.Add(":Ret_val",
                    //OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);
                cmd.Parameters.Add(":USER_ID", OracleDbType.Int32, request.Element("Parameters").Element("USER_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "SystemUser";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse Library(XElement request)
        {
            DataResponse response = new DataResponse();

            var libName = request.Element("libName").Value;
            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["RegConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                if(libName == "Department")
                    cmd.CommandText = "SELECT DEPARTMENT_ID, DEPARTMENT_NAME FROM REF_DEPARTMENT";
                else if(libName == "Status")
                    cmd.CommandText = "SELECT STATUS_ID, STATUS_NAME FROM REF_STATUS";
                else if (libName == "Violation")
                    cmd.CommandText = "SELECT VIOLATION_ID, VIOLATION_NAME FROM REF_VIOLATION";

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "Library";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse OrgList(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["RegConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT R1.ORG_ID, R1.ORG_DEPARTMENT_ID, RD.DEPARTMENT_NAME, R1.ORG_REGISTER_NO," +
                                "R1.ORG_NAME, R1.ORG_CODE, R1.ORG_BUDGET_TYPE_ID, RB.BUDGET_TYPE_NAME," +
                                "R1.ORG_CONCENTRATOR_ID, R2.ORG_NAME AS ORG_CONCENTRATOR_NAME, R1.VIOLATION_DETAIL," +
                                "R1.ORG_STATUS_ID, RS.STATUS_NAME, R1.INFORMATION_DETAIL" +
                    "FROM REG_ORGANIZATION R1 " +
                    "INNER JOIN REF_DEPARTMENT RD ON R1.ORG_DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN REF_BUDGET_TYPE RB ON R1.ORG_BUDGET_TYPE_ID = RB.BUDGET_TYPE_ID " +
                    "LEFT JOIN REG_ORGANIZATION R2 ON R1.ORG_CONCENTRATOR_ID = R2.ORG_ID " +
                    "INNER JOIN REF_STATUS RS ON R1.ORG_STATUS_ID = RS.STATUS_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR(:DEPARTMENT_ID != 23 AND OFFICE_CODE = :DEPARTMENT_ID))";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM0";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }

        public static DataResponse BM0(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_TYPE ,AUDIT_TYPE_NAME,"+
                    "TOPIC_TYPE ,TOPIC_CODE ,TOPIC_NAME ,ORDER_NO ,ORDER_DATE ,AUDIT_FORM_TYPE ,AUDIT_PROPOSAL_TYPE ,"+
                    "AUDIT_BUDGET_TYPE ,AUDIT_INCLUDED_ORG ,WORKING_PERSON ,WORKING_DAY ,WORKING_ADDITION_TIME ,"+
                    "AUDIT_DEPARTMENT ,AUDITOR_LEAD ,AUDITOR_MEMBER ,AUDITOR_ENTRY ,EXEC_TYPE ,BM.CREATED_DATE FROM BM0_DATA BM "+
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID "+
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID "+
                    "WHERE(:DEPARTMENT_ID = 23 OR(:DEPARTMENT_ID != 23 AND OFFICE_CODE = :DEPARTMENT_ID)) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM0";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM1(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ,"+
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ,"+
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ,"+
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ,"+
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM "+
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID "+
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID "+
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM1";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM2(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM2";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM3(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM3";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM4(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM4";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM5(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM5";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM6(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM6";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM7(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM7";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM8(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM8";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse NM1(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM1";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse NM2(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM2";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse NM3(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM3";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse NM4(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM4";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse NM5(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM5";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse NM6(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM6";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse NM7(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM7";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM1A(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM1A";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM1B(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM1B";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM1C(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM1C";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM2A(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM2A";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM2B(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM2B";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM2C(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM2C";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM3A(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM3A";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM3B(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM3B";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM3C(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM3C";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM4A(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM4A";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM4B(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM4B";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM4C(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM4C";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM5(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM5";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM6(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM6";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM7(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM7";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM8(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select OFFICE_ID ,DEPARTMENT_NAME,STATISTIC_PERIOD ,AUDIT_YEAR ,AUDIT_TYPE ," +
                    "AUDIT_TYPE_NAME,AUDIT_CODE ,AUDIT_NAME ,AUDIT_BUDGET_TYPE ,ORDER_DATE ,ORDER_NO ,ACT_NO ," +
                    "ACT_VIOLATION_DESC ,ACT_VIOLATION_TYPE ,ACT_SUBMITTED_DATE ,ACT_DELIVERY_DATE ,ACT_AMOUNT ," +
                    "ACT_STATE_AMOUNT ,ACT_LOCAL_AMOUNT ,ACT_ORG_AMOUNT ,ACT_OTHER_AMOUNT ,ACT_RCV_NAME ,ACT_RCV_ROLE ," +
                    "ACT_RCV_GIVEN_NAME ,ACT_RCV_ADDRESS ,ACT_CONTROL_AUDITOR ,COMPLETION_ORDER ,COMPLETION_AMOUNT ," +
                    "COMPLETION_STATE_AMOUNT ,COMPLETION_LOCAL_AMOUNT ,COMPLETION_ORG_AMOUNT ,COMPLETION_OTHER_AMOUNT ," +
                    "REMOVED_AMOUNT ,REMOVED_LAW_AMOUNT ,REMOVED_LAW_DATE_NO ,REMOVED_INVALID_AMOUNT ," +
                    "REMOVED_INVALID_DATE_NO ,ACT_C2_AMOUNT ,ACT_C2_NONEXPIRED ,ACT_C2_EXPIRED ,BENEFIT_FIN ," +
                    "BENEFIT_FIN_AMOUNT ,BENEFIT_NONFIN ,EXEC_TYPE ,BM.CREATED_DATE from bm1_data BM " +
                    "INNER JOIN AUD_REG.REF_AUDIT_TYPE ON AUDIT_TYPE = AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT ON OFFICE_ID = DEPARTMENT_ID " +
                    "WHERE(:DEPARTMENT_ID = 23 OR: DEPARTMENT_ID != 23 AND OFFICE_ID = :DEPARTMENT_ID) AND ROWNUM <= 5";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                //OracleDataReader dr = cmd.ExecuteReader();
                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM8";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
    }
}