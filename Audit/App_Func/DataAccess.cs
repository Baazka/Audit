using Audit.Models;
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
                cmd.Parameters.Add(":USER_ID", OracleDbType.Int32, request.Element("Parameters").Element("USER_ID").Value, System.Data.ParameterDirection.Input);

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
        public static DataResponse UserCodeChange(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["RegConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_REG.SYSTEM_USER SET USER_PASSWORD = :P_USER_NEWCODE, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE  WHERE USER_ID = :P_USER_ID";

                // Set parameters
                cmd.Parameters.Add(":P_USER_NEWCODE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_NEWCODE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("UPDATED_DATE")?.Value;
                cmd.Parameters.Add(":P_USER_ID", OracleDbType.Int32, request.Element("Parameters").Element("USER_ID").Value, System.Data.ParameterDirection.Input);
                //cmd.Parameters.Add(":P_USER_OLDCODE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_OLDCODE").Value, System.Data.ParameterDirection.Input);
                //
                
                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Нууц үг солигдлоо.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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
                if (libName == "Department")
                    cmd.CommandText = "SELECT DEPARTMENT_ID, DEPARTMENT_NAME FROM AUD_REG.REF_DEPARTMENT WHERE IS_ACTIVE = 1 AND DEPARTMENT_TYPE = 1 ORDER BY DEPARTMENT_ID ASC";
                else if (libName == "Status")
                    cmd.CommandText = "SELECT STATUS_ID, STATUS_NAME FROM REF_STATUS WHERE IS_ACTIVE = 1 ORDER BY STATUS_ID ASC";
                else if (libName == "Violation")
                    cmd.CommandText = "SELECT VIOLATION_ID, VIOLATION_NAME FROM REF_VIOLATION WHERE IS_ACTIVE = 1 ORDER BY VIOLATION_ID ASC";
                else if (libName == "Office")
                    cmd.CommandText = "SELECT OFFICE_ID, OFFICE_NAME FROM AUD_REG.REF_OFFICE WHERE IS_ACTIVE = 1 ORDER BY OFFICE_ID ASC";
                else if (libName == "SubOffice")
                    cmd.CommandText = "SELECT OFFICE_ID, SUB_OFFICE_ID, SUB_OFFICE_NAME FROM AUD_REG.REF_SUB_OFFICE WHERE IS_ACTIVE = 1 ORDER BY SUB_OFFICE_ID ASC";
                else if (libName == "BudgetType")
                    cmd.CommandText = "SELECT BUDGET_TYPE_ID, BUDGET_TYPE_NAME FROM AUD_REG.REF_BUDGET_TYPE WHERE IS_ACTIVE = 1";
                else if (libName == "Activity")
                    cmd.CommandText = "SELECT ACTIVITY_ID, ACTIVITY_NAME FROM AUD_REG.REF_ACTIVITY WHERE IS_ACTIVE = 1 ORDER BY ACTIVITY_ID ASC";
                else if (libName == "SubBudgetType")
                    cmd.CommandText = "SELECT BUDGET_TYPE_ID, SUB_BUDGET_TYPE_ID, SUB_BUDGET_TYPE_NAME FROM AUD_REG.REF_SUB_BUDGET_TYPE WHERE IS_ACTIVE = 1 ORDER BY BUDGET_TYPE_ID ASC";
                else if (libName == "Committee")
                    cmd.CommandText = "SELECT COMMITTEE_ID, COMMITTEE_NAME FROM AUD_REG.REF_COMMITTEE WHERE IS_ACTIVE = 1 ORDER BY COMMITTEE_ID ASC";
                else if (libName == "TaxOffice")
                    cmd.CommandText = "SELECT TAX_OFFICE_ID, TAX_OFFICE_NAME FROM AUD_REG.REF_TAX_OFFICE WHERE IS_ACTIVE = 1 ORDER BY TAX_OFFICE_ID ASC";
                else if (libName == "CostType")
                    cmd.CommandText = "SELECT COST_TYPE_ID, COST_TYPE_NAME FROM AUD_REG.REF_COST_TYPE WHERE IS_ACTIVE = 1";
                else if (libName == "InsuranceOffice")
                    cmd.CommandText = "SELECT INSURANCE_OFFICE_ID, INSURANCE_OFFICE_NAME FROM AUD_REG.REF_INSURANCE_OFFICE WHERE IS_ACTIVE = 1 ORDER BY INSURANCE_OFFICE_ID ASC";
                else if (libName == "FinancingType")
                    cmd.CommandText = "SELECT FINANCING_TYPE_ID, FINANCING_TYPE_NAME FROM AUD_REG.REF_FINANCING_TYPE WHERE IS_ACTIVE = 1";
                else if (libName == "FinOffice")
                    cmd.CommandText = "SELECT FIN_OFFICE_ID, FIN_OFFICE_NAME FROM AUD_REG.REF_FIN_OFFICE WHERE IS_ACTIVE = 1 ORDER BY FIN_OFFICE_ID ASC";
                else if (libName == "Bank")
                    cmd.CommandText = "SELECT BANK_ID, BANK_NAME FROM AUD_REG.REF_BANK WHERE IS_ACTIVE = 1 ORDER BY BANK_ID";
                else if (libName == "Reason")
                    cmd.CommandText = "SELECT INACTIVE_REASON_ID, INACTIVE_REASON_NAME FROM AUD_REG.REF_INACTIVE_REASON WHERE IS_ACTIVE = 1 ORDER BY INACTIVE_REASON_ID";
                else if (libName == "StatPeriod")
                    cmd.CommandText = "SELECT ID,PERIOD_LABEL FROM AUD_STAT.REF_PERIOD ORDER BY ID";
                else if (libName == "RefAuditType")
                    cmd.CommandText = "SELECT AUDIT_TYPE_ID, AUDIT_TYPE_NAME FROM AUD_STAT.REF_AUDIT_TYPE WHERE IS_ACTIVE = 1 ORDER BY AUDIT_TYPE_ID ASC";
                else if (libName == "RefTopicType")
                    cmd.CommandText = "SELECT TOPIC_TYPE_ID, TOPIC_TYPE_NAME, TOPIC_AUDIT_TYPE_ID FROM AUD_STAT.REF_TOPIC_TYPE WHERE IS_ACTIVE = 1 ORDER BY TOPIC_TYPE_ID ASC";
                else if (libName == "RefFormType")
                    cmd.CommandText = "SELECT FORM_TYPE_ID, FORM_TYPE_NAME, FORM_AUDIT_TYPE_ID FROM AUD_STAT.REF_FORM_TYPE WHERE IS_ACTIVE = 1 ORDER BY FORM_TYPE_ID ASC";
                else if (libName == "RefProposalType")
                    cmd.CommandText = "SELECT PROPOSAL_TYPE_ID, PROPOSAL_TYPE_NAME, PROPOSAL_AUDIT_TYPE_ID FROM AUD_STAT.REF_PROPOSAL_TYPE WHERE IS_ACTIVE = 1 ORDER BY PROPOSAL_TYPE_ID ASC";
                else if (libName == "RefBudgetType")
                    cmd.CommandText = "SELECT BUDGET_TYPE_ID, BUDGET_TYPE_NAME, BUDGET_AUDIT_TYPE_ID FROM AUD_STAT.REF_BUDGET_TYPE WHERE IS_ACTIVE = 1 ORDER BY BUDGET_TYPE_ID ASC";
                else if (libName == "RefAuditYear")
                    cmd.CommandText = "SELECT YEAR_ID, YEAR_LABEL FROM AUD_STAT.REF_AUDIT_YEAR WHERE IS_ACTIVE = 1 ORDER BY YEAR_ID DESC";
                else if (libName == "RefViolationType")
                    cmd.CommandText = "SELECT VIOLATION_ID, VIOLATION_NAME FROM AUD_STAT.REF_VIOLATION_TYPE WHERE IS_ACTIVE = 1 ORDER BY VIOLATION_ID ASC";
                else if (libName == "HAK")
                    cmd.CommandText = "SELECT DEPARTMENT_ID, DEPARTMENT_NAME FROM AUD_ORG.REF_DEPARTMENT WHERE DEPARTMENT_TYPE = 2 AND IS_ACTIVE = 1 ORDER BY DEPARTMENT_NAME ASC";
                
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
        public static DataResponse MenuList(XElement request)
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
                cmd.CommandText = "SELECT SM.ID MENU_ID, SM.MENU_NAME, SM.MENU_ROUTE FROM AUD_REG.SYSTEM_USER SU "+
                            "INNER JOIN AUD_REG.SYSTEM_USER_TYPE SUT ON SU.USER_TYPE_ID = SUT.USER_TYPE_ID "+
                            "INNER JOIN AUD_REG.USER_ROLE UR ON SU.USER_ID = UR.USER_ID AND UR.ROLE_TYPE = 1 "+
                            "INNER JOIN AUD_REG.SYSTEM_MENU SM ON UR.ROLE_ID = SM.ID "+
                            "WHERE SU.USER_ID = :P_ID ORDER BY SM.ID";

                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("USER_ID")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "MenuList";

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
        public static DataResponse MenuRole(XElement request)
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
                cmd.CommandText = "SELECT SUT.USER_TYPE_ID, SUT.USER_TYPE_NAME, SMR.ROLE_NAME FROM AUD_REG.SYSTEM_USER SU " +
                                "INNER JOIN AUD_REG.SYSTEM_USER_TYPE SUT ON SU.USER_TYPE_ID = SUT.USER_TYPE_ID " +
                                "INNER JOIN AUD_REG.USER_ROLE UR ON SU.USER_ID = UR.USER_ID AND UR.ROLE_TYPE = 2 " +
                                "INNER JOIN AUD_REG.SYSTEM_MENU_ROLE SMR ON UR.ROLE_ID = SMR.ID " +
                                "WHERE SU.USER_ID = :P_ID AND SMR.MENU_ID = :M_ID";

                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("USER_ID")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":M_ID", OracleDbType.Int32, request.Element("Parameters").Element("MENU_ID")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "MenuRole";

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
        #region Reg
        public static DataResponse OrgList(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["RegConfig"]);
                con.Open();

                XElement req = request.Element("Parameters").Element("Request");
                
                //RowCount
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_ORG_COUNT";

                OracleParameter retParam = cmd.Parameters.Add(":Ret_val",
                    OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);
                cmd.Parameters.Add(":DEP_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value  :null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_STATUS", OracleDbType.Varchar2, req.Element("V_STATUS") != null && !string.IsNullOrEmpty(req.Element("V_STATUS").Value) ? req.Element("V_STATUS")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_VIOLATION", OracleDbType.Varchar2, req.Element("V_VIOLATION") != null && !string.IsNullOrEmpty(req.Element("V_VIOLATION").Value) ? req.Element("V_VIOLATION")?.Value.Replace(",", "%") : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_SEARCH", OracleDbType.Varchar2, req.Element("Search") != null && !string.IsNullOrEmpty(req.Element("Search").Value) ? req.Element("Search")?.Value : null, System.Data.ParameterDirection.Input);

                cmd.ExecuteNonQuery();

                cmd.Dispose();

                //Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT R1.ORG_ID, R1.ORG_DEPARTMENT_ID, RD.DEPARTMENT_NAME, R1.ORG_REGISTER_NO, R1.ORG_NAME, R1.ORG_CODE, R1.ORG_BUDGET_TYPE_ID, RB.BUDGET_TYPE_NAME, R1.ORG_CONCENTRATOR_ID, R2.ORG_NAME AS RG_CONCENTRATOR_NAME, R1.VIOLATION_DETAIL, R1.ORG_STATUS_ID, RS.STATUS_NAME, R1.INFORMATION_DETAIL " +
                    "FROM REG_ORGANIZATION R1 " +
                    "INNER JOIN REF_DEPARTMENT RD ON R1.ORG_DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN REF_BUDGET_TYPE RB ON R1.ORG_BUDGET_TYPE_ID = RB.BUDGET_TYPE_ID " +
                    "LEFT JOIN REG_ORGANIZATION R2 ON R1.ORG_CONCENTRATOR_ID = R2.ORG_ID " +
                    "INNER JOIN REF_STATUS RS ON R1.ORG_STATUS_ID = RS.STATUS_ID " +
                    "WHERE R1.IS_ACTIVE = 1 AND (:DEP_ID = 2 OR (:DEP_ID !=2 AND R1.ORG_DEPARTMENT_ID = :DEP_ID)) " +
                    "AND (:V_DEPARTMENT IS NULL OR R1.ORG_DEPARTMENT_ID = :V_DEPARTMENT) " +
                    "AND (:V_STATUS IS NULL OR (R1.ORG_STATUS_ID IN (:V_STATUS))) " +
                    "AND (:V_BUDGET_TYPE IS NULL OR (R1.ORG_BUDGET_TYPE_ID IN (:V_BUDGET_TYPE))) " +
                    "AND (:V_VIOLATION IS NULL OR (R1.VIOLATION_DETAIL LIKE '%'||:V_VIOLATION||'%')) " +
                    "AND (:V_SEARCH IS NULL OR UPPER(RD.DEPARTMENT_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                    "OR UPPER(R1.ORG_REGISTER_NO) LIKE '%'||UPPER(:V_SEARCH)||'%' OR UPPER(R1.ORG_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                    "OR UPPER(R1.ORG_CODE) LIKE '%'||UPPER(:V_SEARCH)||'%' OR UPPER(RB.BUDGET_TYPE_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                    "OR UPPER(R1.VIOLATION_DETAIL) LIKE '%'||UPPER(:V_SEARCH)||'%' OR UPPER(R1.INFORMATION_DETAIL) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                    "OR UPPER(RS.STATUS_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN R1.ORG_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'DEPARTMENT_NAME' AND :ORDER_DIR = 'ASC' THEN RD.DEPARTMENT_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'DEPARTMENT_NAME' AND :ORDER_DIR = 'DESC' THEN RD.DEPARTMENT_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ORG_REGISTER_NO' AND :ORDER_DIR = 'ASC' THEN R1.ORG_REGISTER_NO END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ORG_REGISTER_NO' AND :ORDER_DIR = 'DESC' THEN R1.ORG_REGISTER_NO END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ORG_NAME' AND :ORDER_DIR = 'ASC' THEN R1.ORG_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ORG_NAME' AND :ORDER_DIR = 'DESC' THEN R1.ORG_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ORG_CODE' AND :ORDER_DIR = 'ASC' THEN R1.ORG_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ORG_CODE' AND :ORDER_DIR = 'DESC' THEN R1.ORG_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'BUDGET_TYPE_NAME' AND :ORDER_DIR = 'ASC' THEN RB.BUDGET_TYPE_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'BUDGET_TYPE_NAME' AND :ORDER_DIR = 'DESC' THEN RB.BUDGET_TYPE_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'CONCENTRATOR_NAME' AND :ORDER_DIR = 'ASC' THEN R2.ORG_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'CONCENTRATOR_NAME' AND :ORDER_DIR = 'DESC' THEN R2.ORG_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'VIOLATION_DETAIL' AND :ORDER_DIR = 'ASC' THEN R1.VIOLATION_DETAIL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'VIOLATION_DETAIL' AND :ORDER_DIR = 'DESC' THEN R1.VIOLATION_DETAIL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'STATUS_NAME' AND :ORDER_DIR = 'ASC' THEN RS.STATUS_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'STATUS_NAME' AND :ORDER_DIR = 'DESC' THEN RS.STATUS_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'INFORMATION_DETAIL' AND :ORDER_DIR = 'ASC' THEN R1.INFORMATION_DETAIL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'INFORMATION_DETAIL' AND :ORDER_DIR = 'DESC' THEN R1.INFORMATION_DETAIL END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters
                cmd.Parameters.Add(":DEP_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT")!=null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value :null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_STATUS", OracleDbType.Varchar2, req.Element("V_STATUS") != null && !string.IsNullOrEmpty(req.Element("V_STATUS").Value) ? req.Element("V_STATUS")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_VIOLATION", OracleDbType.Varchar2, req.Element("V_VIOLATION") != null && !string.IsNullOrEmpty(req.Element("V_VIOLATION").Value) ? req.Element("V_VIOLATION")?.Value.Replace(",", "%") : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_BUDGET_TYPE", OracleDbType.Varchar2, req.Element("V_BUDGET_TYPE")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search") != null && !string.IsNullOrEmpty(req.Element("Search").Value) ? req.Element("Search")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();                
                con.Close();

                dtTable.TableName = "OrgList";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", retParam.Value));
                response.CreateResponse(xmlResponseData);

            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse OrgDetail(XElement request)
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
                cmd.CommandText = "SELECT RO.ORG_ID, RO.ORG_DEPARTMENT_ID, RO.ORG_CODE, RO.ORG_NAME, RO.ORG_REGISTER_NO, RO.ORG_REGISTER_NUMBER, RO.ORG_REG_DATE, RO.ORG_OFFICE_ID, ORG_FIN_OFFICE_ID, " +
                    "RO.ORG_SUB_OFFICE_ID, RO.ORG_ADDRESS, RO.ORG_WEBSITE, RO.ORG_EMAIL, RO.ORG_PHONE, RO.ORG_FAX," +
                    "ROB.ORGB_ID, ROB.ORGB_BANK_ID, ROB.ORGB_BANK_ACCOUNT, ROB.ORGB_DESCRIPTION," +
                    "ROB2.ORGB_ID ORGB_ID2, ROB2.ORGB_BANK_ID ORGB_BANK_ID2, ROB2.ORGB_BANK_ACCOUNT ORGB_BANK_ACCOUNT2, ROB2.ORGB_DESCRIPTION ORGB_DESCRIPTION2, " +
                    "ROP.ORGP_ID, ROP.ORGP_ROLE, ROP.ORGP_ROLE_DATE, ROP.ORGP_REGISTER_NO, ROP.ORGP_LASTNAME, ROP.ORGP_FIRSTNAME, ROP.ORGP_PHONE, ROP.ORGP_EMAIL, ROP.ORGP_EXPERIENCE_YEAR, ROP.ORGP_PROFESSION, "+
                    "ROP2.ORGP_ID ORGP_ID2, ROP2.ORGP_ROLE ORGP_ROLE2, ROP2.ORGP_ROLE_DATE ORGP_ROLE_DATE2, ROP2.ORGP_REGISTER_NO ORGP_REGISTER_NO2, ROP2.ORGP_LASTNAME ORGP_LASTNAME2, ROP2.ORGP_FIRSTNAME ORGP_FIRSTNAME2, ROP2.ORGP_PHONE ORGP_PHONE2, ROP2.ORGP_EMAIL ORGP_EMAIL2, ROP2.ORGP_EXPERIENCE_YEAR ORGP_EXPERIENCE_YEAR2, ROP2.ORGP_PROFESSION ORGP_PROFESSION2, " +
                    "RO.ORG_BUDGET_TYPE_ID, RB.BUDGET_TYPE_NAME, RO.ORG_ACTIVITY_ID, RO.ORG_SUB_BUDGET_TYPE_ID, RO.ORG_COMMITTEE_ID, RO.ORG_TAX_OFFICE_ID, RO.ORG_COST_TYPE_ID, RO.ORG_INSURANCE_OFFICE_ID, RO.ORG_FINANCING_TYPE_ID " +
                    "FROM AUD_REG.REG_ORGANIZATION RO " +
                    "INNER JOIN AUD_REG.REG_ORGANIZATION_BANK ROB ON RO.ORG_ID = ROB.ORGB_ORG_ID " +
                    "INNER JOIN AUD_REG.REG_ORGANIZATION_BANK ROB2 ON RO.ORG_ID = ROB2.ORGB_ORG_ID " +
                    "INNER JOIN AUD_REG.REG_ORGANIZATION_PERSON ROP ON RO.ORG_ID = ROP.ORGP_ORG_ID " +
                    "INNER JOIN AUD_REG.REG_ORGANIZATION_PERSON ROP2 ON RO.ORG_ID = ROP2.ORGP_ORG_ID " +
                    "INNER JOIN AUD_REG.REF_BUDGET_TYPE RB ON RO.ORG_BUDGET_TYPE_ID = RB.BUDGET_TYPE_ID " +
                    "WHERE ROB.ORGB_BANK_TYPE_ID = 1 AND ROB.IS_ACTIVE = 1 AND ROB2.ORGB_BANK_TYPE_ID = 2 AND ROB2.IS_ACTIVE = 1 " +
                    "AND ROP.ORGP_PERSON_TYPE_ID = 1 AND ROP.IS_ACTIVE = 1 AND ROP2.ORGP_PERSON_TYPE_ID = 2 AND ROP2.IS_ACTIVE = 1 " +
                    "AND ORG_ID = :ORG_ID";

                // Set parameters
                cmd.Parameters.Add(":ORG_ID", OracleDbType.Int32, request.Element("Parameters").Element("ORG_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "OrgDetail";

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
        public static DataResponse OrgUB(XElement request)
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
                cmd.CommandText = "SELECT UB_ID, UB_REGISTER_NO ,UB_NAME ,UB_DOCUMENT_NO ,UB_REG_DATE ,UB_CATEGORY "+
                        "FROM AUD_REG.UBEG_REGISTRATION WHERE UPPER(TRIM(UB_REGISTER_NO)) = :REG_NO";

                // Set parameters
                cmd.Parameters.Add(":REG_NO", OracleDbType.Varchar2, request.Element("Parameters").Element("REG_NO").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "UBList";

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
        public static DataResponse OrgMOF(XElement request)
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
                cmd.CommandText = "SELECT MOF_ID, MOF_REGISTER_NO, MOF_NAME, MOF_TEZ, MOF_TTZ, MOF_TSHZ, MOF_SALBAR, MOF_BUDGET_TYPE, MOF_AIMAG, MOF_SUM, MOF_MAIN_ACCOUNT, MOF_EXTEND_ACCOUNT " +  
                    "FROM AUD_REG.MOF_REGISTRATION WHERE UPPER(TRIM(MOF_REGISTER_NO)) = :REG_NO";

                // Set parameters
                cmd.Parameters.Add(":REG_NO", OracleDbType.Varchar2, request.Element("Parameters").Element("REG_NO").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "MOFList";

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
        public static DataResponse OrgTAX(XElement request)
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
                cmd.CommandText = "SELECT GDSR_NUMBER ,COMPANY_REG_NO ,LEGAL_NAME ,LEGAL_STATUS ,LEGAL_STATUS_NAME ,COMPANY_REG_DATE ,PROPERTY_TYPE ,PROPERTY_TYPE_NAME , "+
                " NUMBER_FOUNDERS ,OPERATION ,SOFF_OFF_CODE ,OFF_NAME ,SOFF_CODE ,SOFF_NAME ,SECTOR ,SECTOR_CODE ,SECTOR_NAME ,SUB_SECTOR ,SUB_SECTOR_CODE_NAME , "+
                " ELEMENT ,ELEMENT_NAME ,DIVISION ,DIVISION_NAME ,REGION FROM AUD_REG.TAX_REGISTRATION WHERE GDSR_NUMBER = :REG_NO";

                // Set parameters
                cmd.Parameters.Add(":REG_NO", OracleDbType.Varchar2, request.Element("Parameters").Element("REG_NO").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "TAXList";

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
        public static DataResponse OrgUBsingle(XElement request)
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
                cmd.CommandText = "SELECT UB_ID, UB_REGISTER_NO ,UB_NAME ,UB_DOCUMENT_NO ,UB_REG_DATE ,UB_CATEGORY " +
                        "FROM AUD_REG.UBEG_REGISTRATION WHERE UB_ID = :REG_ID";

                // Set parameters
                cmd.Parameters.Add(":REG_ID", OracleDbType.Varchar2, request.Element("Parameters").Element("REG_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "UBsingle";

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
        public static DataResponse OrgMOFsingle(XElement request)
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
                cmd.CommandText = "SELECT MOF_ID, MOF_REGISTER_NO, MOF_NAME, MOF_TEZ, MOF_TTZ, MOF_TSHZ, MOF_SALBAR, MOF_BUDGET_TYPE, MOF_AIMAG, MOF_SUM, MOF_MAIN_ACCOUNT, MOF_EXTEND_ACCOUNT " +
                    "FROM AUD_REG.MOF_REGISTRATION WHERE MOF_ID = :REG_ID";

                // Set parameters
                cmd.Parameters.Add(":REG_ID", OracleDbType.Varchar2, request.Element("Parameters").Element("REG_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "MOFsingle";

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
        public static DataResponse OrgTAXsingle(XElement request)
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
                cmd.CommandText = "SELECT TAX_ID, GDSR_NUMBER ,COMPANY_REG_NO ,LEGAL_NAME ,LEGAL_STATUS ,LEGAL_STATUS_NAME ,COMPANY_REG_DATE ,PROPERTY_TYPE ,PROPERTY_TYPE_NAME ,"+
                " NUMBER_FOUNDERS ,OPERATION ,SOFF_OFF_CODE ,OFF_NAME ,SOFF_CODE ,SOFF_NAME ,SECTOR ,SECTOR_CODE ,SECTOR_NAME ,SUB_SECTOR ,SUB_SECTOR_CODE_NAME ,"+
                " ELEMENT ,ELEMENT_NAME ,DIVISION ,DIVISION_NAME ,REGION FROM AUD_REG.TAX_REGISTRATION WHERE TAX_ID = :REG_ID";

                // Set parameters
                cmd.Parameters.Add(":REG_ID", OracleDbType.Varchar2, request.Element("Parameters").Element("REG_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "TAXsingle";

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
        public static DataResponse OrgSave(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                XElement elem = request.Element("Parameters").Element("Organization");
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["RegConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_ORG_SAVE";

                // Set parameters
                OracleParameter retParam = cmd.Parameters.Add(":Ret_val", OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);
                    
                cmd.Parameters.Add(":P_ORGID", OracleDbType.Int32).Value = elem.Element("ORG_ID").Value;
                cmd.Parameters.Add(":P_ORGBID1", OracleDbType.Int32).Value = elem.Element("ORGB_ID").Value;
                cmd.Parameters.Add(":P_ORGBID2", OracleDbType.Int32).Value = elem.Element("ORGB_ID2").Value;
                cmd.Parameters.Add(":P_ORGPID1", OracleDbType.Int32).Value = elem.Element("ORGP_ID").Value;
                cmd.Parameters.Add(":P_ORGPID2", OracleDbType.Int32).Value = elem.Element("ORGP_ID2").Value;

                cmd.Parameters.Add(":P_ORGCODE", OracleDbType.Varchar2).Value = elem.Element("ORG_CODE")?.Value;                
                cmd.Parameters.Add(":P_ORGREGNO", OracleDbType.Int32).Value = elem.Element("ORG_REGISTER_NO")?.Value;               
                cmd.Parameters.Add(":P_UBNUMBER", OracleDbType.Varchar2).Value = elem.Element("ORG_REGISTER_NUMBER")?.Value;                
                cmd.Parameters.Add(":P_ORGNAME", OracleDbType.Varchar2).Value = elem.Element("ORG_NAME")?.Value;
                cmd.Parameters.Add(":P_ORGDATE", OracleDbType.Varchar2).Value = elem.Element("ORG_REG_DATE")?.Value;                
                cmd.Parameters.Add(":P_OFFICE_ID", OracleDbType.Int32).Value = elem.Element("ORG_OFFICE_ID")?.Value;                
                cmd.Parameters.Add(":P_SUBOFFICE_ID", OracleDbType.Int32).Value = elem.Element("ORG_SUB_OFFICE_ID")?.Value;                
                cmd.Parameters.Add(":P_ORGADDRESS", OracleDbType.Varchar2).Value = elem.Element("ORG_ADDRESS")?.Value;                
                cmd.Parameters.Add(":P_WEB", OracleDbType.Varchar2).Value = elem.Element("ORG_WEBSITE")?.Value;                
                cmd.Parameters.Add(":P_EMAIL", OracleDbType.Varchar2).Value = elem.Element("ORG_EMAIL")?.Value;                
                cmd.Parameters.Add(":P_ORGPHONE", OracleDbType.Varchar2).Value = elem.Element("ORG_PHONE")?.Value;                
                cmd.Parameters.Add(":P_FAX", OracleDbType.Varchar2).Value = elem.Element("ORG_FAX")?.Value;

                cmd.Parameters.Add(":P_BANKID1", OracleDbType.Int32).Value = elem.Element("ORGB_BANK_ID")?.Value;
                cmd.Parameters.Add(":P_BANKID2", OracleDbType.Int32).Value = elem.Element("ORGB_BANK_ID2")?.Value;
                cmd.Parameters.Add(":P_BANKACCOUNT1", OracleDbType.Int64).Value = elem.Element("ORGB_BANK_ACCOUNT")?.Value;
                cmd.Parameters.Add(":P_BANKACCOUNT2", OracleDbType.Int64).Value = elem.Element("ORGB_BANK_ACCOUNT2")?.Value;
                cmd.Parameters.Add(":P_BANKDESC1", OracleDbType.Varchar2).Value = elem.Element("ORGB_DESCRIPTION")?.Value;
                cmd.Parameters.Add(":P_BANKDESC2", OracleDbType.Varchar2).Value = elem.Element("ORGB_DESCRIPTION2")?.Value;

                cmd.Parameters.Add(":P_BUDGET_TYPE", OracleDbType.Int32).Value = elem.Element("ORG_BUDGET_TYPE_ID")?.Value;
                cmd.Parameters.Add(":P_ACTIVITY", OracleDbType.Int32).Value = elem.Element("ORG_ACTIVITY_ID")?.Value;
                cmd.Parameters.Add(":P_SUB_BUDGET_TYPE", OracleDbType.Int32).Value = elem.Element("ORG_SUB_BUDGET_TYPE_ID")?.Value;
                cmd.Parameters.Add(":P_DEPARTMENT_ID", OracleDbType.Int32).Value = elem.Element("ORG_DEPARTMENT_ID")?.Value;
                cmd.Parameters.Add(":P_COMMITTEE", OracleDbType.Int32).Value = elem.Element("ORG_COMMITTEE_ID")?.Value;
                cmd.Parameters.Add(":P_TAXOFFICE", OracleDbType.Int32).Value = elem.Element("ORG_TAX_OFFICE_ID")?.Value;
                cmd.Parameters.Add(":P_COST_TYPE", OracleDbType.Int32).Value = elem.Element("ORG_COST_TYPE_ID")?.Value;
                cmd.Parameters.Add(":P_INSURANCE", OracleDbType.Int32).Value = elem.Element("ORG_INSURANCE_OFFICE_ID")?.Value;
                cmd.Parameters.Add(":P_FINOFFICE", OracleDbType.Int32).Value = elem.Element("ORG_FIN_OFFICE_ID")?.Value;
                cmd.Parameters.Add(":P_FINANCING_TYPE", OracleDbType.Int32).Value = elem.Element("ORG_FINANCING_TYPE_ID")?.Value;

                cmd.Parameters.Add(":P_ROLE1", OracleDbType.Varchar2).Value = elem.Element("ORGP_ROLE")?.Value;
                cmd.Parameters.Add(":P_ROLE2", OracleDbType.Varchar2).Value = elem.Element("ORGP_ROLE2")?.Value;
                cmd.Parameters.Add(":P_REGNO1", OracleDbType.Varchar2).Value = elem.Element("ORGP_REGISTER_NO")?.Value;
                cmd.Parameters.Add(":P_REGNO2", OracleDbType.Varchar2).Value = elem.Element("ORGP_REGISTER_NO2")?.Value;
                cmd.Parameters.Add(":P_LNAME1", OracleDbType.Varchar2).Value = elem.Element("ORGP_LASTNAME")?.Value;
                cmd.Parameters.Add(":P_LNAME2", OracleDbType.Varchar2).Value = elem.Element("ORGP_LASTNAME2")?.Value;
                cmd.Parameters.Add(":P_FNAME1", OracleDbType.Varchar2).Value = elem.Element("ORGP_FIRSTNAME")?.Value;
                cmd.Parameters.Add(":P_FNAME2", OracleDbType.Varchar2).Value = elem.Element("ORGP_FIRSTNAME2")?.Value;
                cmd.Parameters.Add(":P_ROLEDATE1", OracleDbType.Varchar2).Value = elem.Element("ORGP_ROLE_DATE")?.Value;
                cmd.Parameters.Add(":P_ROLEDATE2", OracleDbType.Varchar2).Value = elem.Element("ORGP_ROLE_DATE2")?.Value;
                cmd.Parameters.Add(":P_PHONE1", OracleDbType.Varchar2).Value = elem.Element("ORGP_PHONE")?.Value;
                cmd.Parameters.Add(":P_PHONE2", OracleDbType.Varchar2).Value = elem.Element("ORGP_PHONE2")?.Value;
                cmd.Parameters.Add(":P_EMAIL1", OracleDbType.Varchar2).Value = elem.Element("ORGP_EMAIL")?.Value;
                cmd.Parameters.Add(":P_EMAIL2", OracleDbType.Varchar2).Value = elem.Element("ORGP_EMAIL2")?.Value;
                cmd.Parameters.Add(":P_WYEAR1", OracleDbType.Varchar2).Value = elem.Element("ORGP_EXPERIENCE_YEAR")?.Value;
                cmd.Parameters.Add(":P_WYEAR2", OracleDbType.Varchar2).Value = elem.Element("ORGP_EXPERIENCE_YEAR2")?.Value;
                cmd.Parameters.Add(":P_PROF1", OracleDbType.Varchar2).Value = elem.Element("ORGP_PROFESSION")?.Value;
                cmd.Parameters.Add(":P_PROF2", OracleDbType.Varchar2).Value = elem.Element("ORGP_PROFESSION2")?.Value;
                
                cmd.Parameters.Add(":P_USERID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                object responseValue = retParam.Value;

                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true  :false;

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse OrgInsert(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                XElement elem = request.Element("Parameters").Element("Organization");
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["RegConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_ORG_INSERT";

                // Set parameters
                OracleParameter retParam = cmd.Parameters.Add(":Ret_val", OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);

                cmd.Parameters.Add(":P_ORGCODE", OracleDbType.Varchar2).Value = elem.Element("ORG_CODE")?.Value;
                cmd.Parameters.Add(":P_ORGREGNO", OracleDbType.Int32).Value = elem.Element("ORG_REGISTER_NO")?.Value;
                cmd.Parameters.Add(":P_UBNUMBER", OracleDbType.Varchar2).Value = elem.Element("ORG_REGISTER_NUMBER")?.Value;
                cmd.Parameters.Add(":P_ORGNAME", OracleDbType.Varchar2).Value = elem.Element("ORG_NAME")?.Value;
                cmd.Parameters.Add(":P_ORGDATE", OracleDbType.Varchar2).Value = elem.Element("ORG_REG_DATE")?.Value;
                cmd.Parameters.Add(":P_OFFICE_ID", OracleDbType.Int32).Value = elem.Element("ORG_OFFICE_ID")?.Value;
                cmd.Parameters.Add(":P_SUBOFFICE_ID", OracleDbType.Int32).Value = elem.Element("ORG_SUB_OFFICE_ID")?.Value;
                cmd.Parameters.Add(":P_ORGADDRESS", OracleDbType.Varchar2).Value = elem.Element("ORG_ADDRESS")?.Value;
                cmd.Parameters.Add(":P_WEB", OracleDbType.Varchar2).Value = elem.Element("ORG_WEBSITE")?.Value;
                cmd.Parameters.Add(":P_EMAIL", OracleDbType.Varchar2).Value = elem.Element("ORG_EMAIL")?.Value;
                cmd.Parameters.Add(":P_ORGPHONE", OracleDbType.Varchar2).Value = elem.Element("ORG_PHONE")?.Value;
                cmd.Parameters.Add(":P_FAX", OracleDbType.Varchar2).Value = elem.Element("ORG_FAX")?.Value;

                cmd.Parameters.Add(":P_BANKID1", OracleDbType.Int32).Value = elem.Element("ORGB_BANK_ID")?.Value;
                cmd.Parameters.Add(":P_BANKID2", OracleDbType.Int32).Value = elem.Element("ORGB_BANK_ID2")?.Value;
                cmd.Parameters.Add(":P_BANKACCOUNT1", OracleDbType.Int32).Value = elem.Element("ORGB_BANK_ACCOUNT")?.Value;
                cmd.Parameters.Add(":P_BANKACCOUNT2", OracleDbType.Int32).Value = elem.Element("ORGB_BANK_ACCOUNT2")?.Value;
                cmd.Parameters.Add(":P_BANKDESC1", OracleDbType.Varchar2).Value = elem.Element("ORGB_DESCRIPTION")?.Value;
                cmd.Parameters.Add(":P_BANKDESC2", OracleDbType.Varchar2).Value = elem.Element("ORGB_DESCRIPTION2")?.Value;

                cmd.Parameters.Add(":P_BUDGET_TYPE", OracleDbType.Int32).Value = elem.Element("ORG_BUDGET_TYPE_ID")?.Value;
                cmd.Parameters.Add(":P_ACTIVITY", OracleDbType.Int32).Value = elem.Element("ORG_ACTIVITY_ID")?.Value;
                cmd.Parameters.Add(":P_SUB_BUDGET_TYPE", OracleDbType.Int32).Value = elem.Element("ORG_SUB_BUDGET_TYPE_ID")?.Value;
                cmd.Parameters.Add(":P_DEPARTMENT_ID", OracleDbType.Int32).Value = elem.Element("ORG_DEPARTMENT_ID")?.Value;
                cmd.Parameters.Add(":P_COMMITTEE", OracleDbType.Int32).Value = elem.Element("ORG_COMMITTEE_ID")?.Value;
                cmd.Parameters.Add(":P_TAXOFFICE", OracleDbType.Int32).Value = elem.Element("ORG_TAX_OFFICE_ID")?.Value;
                cmd.Parameters.Add(":P_COST_TYPE", OracleDbType.Int32).Value = elem.Element("ORG_COST_TYPE_ID")?.Value;
                cmd.Parameters.Add(":P_INSURANCE", OracleDbType.Int32).Value = elem.Element("ORG_INSURANCE_OFFICE_ID")?.Value;
                cmd.Parameters.Add(":P_FINOFFICE", OracleDbType.Int32).Value = elem.Element("ORG_FIN_OFFICE_ID")?.Value;
                cmd.Parameters.Add(":P_FINANCING_TYPE", OracleDbType.Int32).Value = elem.Element("ORG_FINANCING_TYPE_ID")?.Value;

                cmd.Parameters.Add(":P_ROLE1", OracleDbType.Varchar2).Value = elem.Element("ORGP_ROLE")?.Value;
                cmd.Parameters.Add(":P_ROLE2", OracleDbType.Varchar2).Value = elem.Element("ORGP_ROLE2")?.Value;
                cmd.Parameters.Add(":P_REGNO1", OracleDbType.Varchar2).Value = elem.Element("ORGP_REGISTER_NO")?.Value;
                cmd.Parameters.Add(":P_REGNO2", OracleDbType.Varchar2).Value = elem.Element("ORGP_REGISTER_NO2")?.Value;
                cmd.Parameters.Add(":P_LNAME1", OracleDbType.Varchar2).Value = elem.Element("ORGP_LASTNAME")?.Value;
                cmd.Parameters.Add(":P_LNAME2", OracleDbType.Varchar2).Value = elem.Element("ORGP_LASTNAME2")?.Value;
                cmd.Parameters.Add(":P_FNAME1", OracleDbType.Varchar2).Value = elem.Element("ORGP_FIRSTNAME")?.Value;
                cmd.Parameters.Add(":P_FNAME2", OracleDbType.Varchar2).Value = elem.Element("ORGP_FIRSTNAME2")?.Value;
                cmd.Parameters.Add(":P_ROLEDATE1", OracleDbType.Varchar2).Value = elem.Element("ORGP_ROLE_DATE")?.Value;
                cmd.Parameters.Add(":P_ROLEDATE2", OracleDbType.Varchar2).Value = elem.Element("ORGP_ROLE_DATE2")?.Value;
                cmd.Parameters.Add(":P_PHONE1", OracleDbType.Varchar2).Value = elem.Element("ORGP_PHONE")?.Value;
                cmd.Parameters.Add(":P_PHONE2", OracleDbType.Varchar2).Value = elem.Element("ORGP_PHONE2")?.Value;
                cmd.Parameters.Add(":P_EMAIL1", OracleDbType.Varchar2).Value = elem.Element("ORGP_EMAIL")?.Value;
                cmd.Parameters.Add(":P_EMAIL2", OracleDbType.Varchar2).Value = elem.Element("ORGP_EMAIL2")?.Value;
                cmd.Parameters.Add(":P_WYEAR1", OracleDbType.Varchar2).Value = elem.Element("ORGP_EXPERIENCE_YEAR")?.Value;
                cmd.Parameters.Add(":P_WYEAR2", OracleDbType.Varchar2).Value = elem.Element("ORGP_EXPERIENCE_YEAR2")?.Value;
                cmd.Parameters.Add(":P_PROF1", OracleDbType.Varchar2).Value = elem.Element("ORGP_PROFESSION")?.Value;
                cmd.Parameters.Add(":P_PROF2", OracleDbType.Varchar2).Value = elem.Element("ORGP_PROFESSION2")?.Value;

                cmd.Parameters.Add(":P_USERID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                object responseValue = retParam.Value;

                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true  :false;

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse DataSearch(XElement request)
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
                cmd.CommandText = "SELECT SRC_NAME, SRC_ID, REG_ID, REG_NO, ORG_NAME FROM AUD_REG.V_ORG_SEARCH " +
                        "WHERE (REG_NO LIKE '%' || :V_SRCH || '%' OR ORG_NAME LIKE '%' || :V_SRCH || '%') AND ROWNUM <= 100";

                // Set parameters
                cmd.Parameters.Add(":V_SRCH", OracleDbType.Varchar2, request.Element("Parameters").Element("V_SRCH").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "DataList";

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
        public static DataResponse OrgConfirm(XElement request)
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
                cmd.CommandText = "F_ORG_CONFIRM";

                // Set parameters
                OracleParameter retParam = cmd.Parameters.Add(":Ret_val",
                    OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);
                cmd.Parameters.Add(":P_ORGID", OracleDbType.Int32, request.Element("Parameters").Element("ORG_ID").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_USERID", OracleDbType.Int32, request.Element("Parameters").Element("USER_ID").Value, System.Data.ParameterDirection.Input);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                con.Close();

                object responseValue = retParam.Value;
                string responseMsg = "";
                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true  :false;
                if (responseVal)
                    responseMsg = "Баталгаажууллаа.";
                else
                    responseMsg = "Мэдээлэл баталгаажуулах төлөвт шилжээгүй байна.";

                response.CreateResponse(responseVal, string.Empty, responseMsg);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse OrgDelete(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                XElement elem = request.Element("Parameters").Element("Organization");
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["RegConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_ORG_INACTIVE";

                // Set parameters
                OracleParameter retParam = cmd.Parameters.Add(":Ret_val", OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);

                cmd.Parameters.Add(":P_ORGID", OracleDbType.Int32).Value = elem.Element("ORG_ID").Value;
                cmd.Parameters.Add(":P_USERID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_IDATE", OracleDbType.Varchar2).Value = elem.Element("P_IDATE")?.Value;
                cmd.Parameters.Add(":P_REASONID", OracleDbType.Int32).Value = elem.Element("P_REASONID")?.Value;
                cmd.Parameters.Add(":P_REASONDESC", OracleDbType.Varchar2).Value = elem.Element("P_REASONDESC")?.Value;
                cmd.Parameters.Add(":P_PARENTID", OracleDbType.Int32).Value = elem.Element("P_PARENTID")?.Value;


                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                object responseValue = retParam.Value;

                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true  :false;

                response.CreateResponse(responseVal, string.Empty, "Устгалаа");
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        #endregion
        #region BM
        public static DataResponse SystemUser(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();
                //XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT SU.USER_ID, SU.USER_CODE, SU.USER_NAME, RD.DEPARTMENT_ID, RD.DEPARTMENT_NAME " +
                    "FROM AUD_REG.SYSTEM_USER SU " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON SU.USER_DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "WHERE SU.IS_ACTIVE = 1 AND SU.USER_TYPE_ID IN(3,4) AND SU.IS_TEST = 0 " +
                    "ORDER BY RD.DEPARTMENT_ID, SU.USER_CODE";

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
        public static DataResponse BM0Search(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT BM.ID, RP.PERIOD_LABEL, RD.DEPARTMENT_NAME, RAY.YEAR_LABEL, RAT.AUDIT_TYPE_NAME, RTT.TOPIC_TYPE_NAME, BM.TOPIC_CODE, BM.TOPIC_NAME, BM.ORDER_NO, BM.ORDER_DATE " +
                    "FROM AUD_STAT.BM0_DATA BM " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON RP.ID = BM.STATISTIC_PERIOD " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON RD.DEPARTMENT_ID = BM.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON BM.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON BM.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON BM.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND BM.DEPARTMENT_ID = :P_OFFICE_ID AND BM.STATISTIC_PERIOD = :P_PERIOD_ID " +
                    "ORDER BY ORDER_NO DESC";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":P_OFFICE_ID", OracleDbType.Int32, request.Element("Parameters").Element("OFFICE_ID").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_PERIOD_ID", OracleDbType.Int32, request.Element("Parameters").Element("PERIOD_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM0Search";

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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(BM.ID) " +
                        "FROM AUD_STAT.BM0_DATA BM " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON BM.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON BM.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON BM.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON BM.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_FORM_TYPE RFT ON BM.AUDIT_FORM_TYPE = RFT.FORM_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_PROPOSAL_TYPE RPT ON BM.AUDIT_PROPOSAL_TYPE = RPT.PROPOSAL_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON BM.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_DEPARTMENT_TYPE RDT ON BM.AUDIT_DEPARTMENT_TYPE = RDT.DEPARTMENT_TYPE_ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD1 ON BM.DEPARTMENT_ID = RD1.DEPARTMENT_ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD2 ON BM.AUDIT_DEPARTMENT_ID = RD2.DEPARTMENT_ID " +
                    "INNER JOIN AUD_REG.SYSTEM_USER SUS ON BM.AUDITOR_ENTRY_ID = SUS.USER_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND (:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND BM.DEPARTMENT_ID = :V_DEPARTMENT)) " +
                    "AND BM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(BM.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RPT.PROPOSAL_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(BM.AUDIT_INCLUDED_ORG) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RD2.DEPARTMENT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(SUS.USER_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(SUS.USER_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT BM.ID, BM.STATISTIC_PERIOD, RP.PERIOD_LABEL, RD1.DEPARTMENT_NAME, RAY.YEAR_LABEL, RAT.AUDIT_TYPE_NAME, RTT.TOPIC_TYPE_NAME, BM.TOPIC_CODE, BM.TOPIC_NAME, BM.ORDER_NO, BM.ORDER_DATE, RFT.FORM_TYPE_NAME, RPT.PROPOSAL_TYPE_NAME, RBT.BUDGET_TYPE_NAME, BM.AUDIT_INCLUDED_COUNT, BM.AUDIT_INCLUDED_ORG, BM.WORKING_PERSON, BM.WORKING_DAY, BM.WORKING_ADDITION_TIME, BM.AUDIT_SERVICE_PAY, RDT.DEPARTMENT_SHORT_NAME, RD2.DEPARTMENT_NAME AS TEAM_DEPARTMENT_NAME, (SELECT LISTAGG(SU.USER_CODE||'-'||SU.USER_NAME,',') WITHIN GROUP (ORDER BY TD.ID) " +
                    "FROM AUD_STAT.BM0_TEAM_DATA TD " +
                    "INNER JOIN AUD_REG.SYSTEM_USER SU ON TD.AUDITOR_ID = SU.USER_ID " +
                    "WHERE TD.TEAM_TYPE_ID = 1 AND TD.AUDIT_ID = BM.ID) AS AUDITOR_LEAD, " +
                    "(SELECT LISTAGG(SU.USER_CODE || '-' || SU.USER_NAME, ',') WITHIN GROUP(ORDER BY TD.ID) " +
                    "FROM AUD_STAT.BM0_TEAM_DATA TD INNER JOIN AUD_REG.SYSTEM_USER SU ON TD.AUDITOR_ID = SU.USER_ID " +
                    "WHERE TD.TEAM_TYPE_ID = 2 AND TD.AUDIT_ID = BM.ID) AS AUDITOR_MEMBER, " +
                    "SUS.USER_CODE || '-' || SUS.USER_NAME AS AUDITOR_ENTRY " +
                    "FROM AUD_STAT.BM0_DATA BM " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON BM.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON BM.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON BM.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON BM.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_FORM_TYPE RFT ON BM.AUDIT_FORM_TYPE = RFT.FORM_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_PROPOSAL_TYPE RPT ON BM.AUDIT_PROPOSAL_TYPE = RPT.PROPOSAL_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON BM.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_DEPARTMENT_TYPE RDT ON BM.AUDIT_DEPARTMENT_TYPE = RDT.DEPARTMENT_TYPE_ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD1 ON BM.DEPARTMENT_ID = RD1.DEPARTMENT_ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD2 ON BM.AUDIT_DEPARTMENT_ID = RD2.DEPARTMENT_ID " +
                    "INNER JOIN AUD_REG.SYSTEM_USER SUS ON BM.AUDITOR_ENTRY_ID = SUS.USER_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND (:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND BM.DEPARTMENT_ID = :V_DEPARTMENT)) " +
                    "AND BM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(BM.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RPT.PROPOSAL_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(BM.AUDIT_INCLUDED_ORG) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RD2.DEPARTMENT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(SUS.USER_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(SUS.USER_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN BM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'DEPARTMENT_NAME' AND: ORDER_DIR = 'ASC' THEN RD1.DEPARTMENT_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'DEPARTMENT_NAME' AND: ORDER_DIR = 'DESC' THEN RD1.DEPARTMENT_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE_NAME' AND: ORDER_DIR = 'ASC' THEN RAT.AUDIT_TYPE_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE_NAME' AND: ORDER_DIR = 'DESC' THEN RAT.AUDIT_TYPE_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_TYPE_NAME' AND: ORDER_DIR = 'ASC' THEN RTT.TOPIC_TYPE_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_TYPE_NAME' AND: ORDER_DIR = 'DESC' THEN RTT.TOPIC_TYPE_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND: ORDER_DIR = 'ASC' THEN BM.TOPIC_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND: ORDER_DIR = 'DESC' THEN BM.TOPIC_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND: ORDER_DIR = 'ASC' THEN BM.TOPIC_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND: ORDER_DIR = 'DESC' THEN BM.TOPIC_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND: ORDER_DIR = 'ASC' THEN BM.ORDER_NO END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND: ORDER_DIR = 'DESC' THEN BM.ORDER_NO END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_DATE' AND: ORDER_DIR = 'ASC' THEN BM.ORDER_DATE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_DATE' AND: ORDER_DIR = 'DESC' THEN BM.ORDER_DATE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'FORM_TYPE_NAME' AND: ORDER_DIR = 'ASC' THEN RFT.FORM_TYPE_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'FORM_TYPE_NAME' AND: ORDER_DIR = 'DESC' THEN RFT.FORM_TYPE_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PROPOSAL_TYPE_NAME' AND: ORDER_DIR = 'ASC' THEN RPT.PROPOSAL_TYPE_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PROPOSAL_TYPE_NAME' AND: ORDER_DIR = 'DESC' THEN RPT.PROPOSAL_TYPE_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'BUDGET_TYPE_NAME' AND: ORDER_DIR = 'ASC' THEN RBT.BUDGET_TYPE_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'BUDGET_TYPE_NAME' AND: ORDER_DIR = 'DESC' THEN RBT.BUDGET_TYPE_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'DEPARTMENT_SHORT_NAME' AND: ORDER_DIR = 'ASC' THEN RDT.DEPARTMENT_SHORT_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'DEPARTMENT_SHORT_NAME' AND: ORDER_DIR = 'DESC' THEN RDT.DEPARTMENT_SHORT_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TEAM_DEPARTMENT_NAME' AND: ORDER_DIR = 'ASC' THEN RD2.DEPARTMENT_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TEAM_DEPARTMENT_NAME' AND: ORDER_DIR = 'DESC' THEN RD2.DEPARTMENT_NAME END DESC " +
                    //"CASE WHEN :ORDER_NAME = 'WORKING_PERSON' AND: ORDER_DIR = 'ASC' THEN BM.WORKING_PERSON END ASC, " +
                    //"CASE WHEN :ORDER_NAME = 'WORKING_PERSON' AND: ORDER_DIR = 'DESC' THEN BM.WORKING_PERSON END DESC, " +
                    //"CASE WHEN :ORDER_NAME = 'WORKING_DAY' AND: ORDER_DIR = 'ASC' THEN BM.WORKING_DAY END ASC, " +
                    //"CASE WHEN :ORDER_NAME = 'WORKING_DAY' AND: ORDER_DIR = 'DESC' THEN BM.WORKING_DAY END DESC, " +
                    //"CASE WHEN :ORDER_NAME = 'WORKING_ADDITION_TIME' AND: ORDER_DIR = 'ASC' THEN BM.WORKING_ADDITION_TIME END ASC, " +
                    //"CASE WHEN :ORDER_NAME = 'WORKING_ADDITION_TIME' AND: ORDER_DIR = 'DESC' THEN BM.WORKING_ADDITION_TIME END DESC, " +
                    //"CASE WHEN :ORDER_NAME = 'AUDIT_DEPARTMENT_ID' AND: ORDER_DIR = 'ASC' THEN BM.AUDIT_DEPARTMENT_ID END ASC, " +
                    //"CASE WHEN :ORDER_NAME = 'AUDIT_DEPARTMENT_ID' AND: ORDER_DIR = 'DESC' THEN BM.AUDIT_DEPARTMENT_ID END DESC " +
                    //"--CASE WHEN :ORDER_NAME = 'AUDITOR_LEAD' AND: ORDER_DIR = 'ASC' THEN BM.AUDITOR_LEAD END ASC, " +
                    //"--CASE WHEN: ORDER_NAME = 'AUDITOR_LEAD' AND: ORDER_DIR = 'DESC' THEN BM.AUDITOR_LEAD END DESC, " +
                    //"--CASE WHEN: ORDER_NAME = 'AUDITOR_MEMBER' AND: ORDER_DIR = 'ASC' THEN BM.AUDITOR_MEMBER END ASC, " +
                    //"--CASE WHEN: ORDER_NAME = 'AUDITOR_MEMBER' AND: ORDER_DIR = 'DESC' THEN BM.AUDITOR_MEMBER END DESC, " +
                    //"--CASE WHEN: ORDER_NAME = 'AUDITOR_ENTRY' AND: ORDER_DIR = 'ASC' THEN BM.AUDITOR_ENTRY END ASC, " +
                    //"--CASE WHEN: ORDER_NAME = 'AUDITOR_ENTRY' AND: ORDER_DIR = 'DESC' THEN BM.AUDITOR_ENTRY END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM0";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM0Detail(XElement request)
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
                cmd.CommandText = "SELECT ID, STATISTIC_PERIOD, DEPARTMENT_ID, AUDIT_YEAR, AUDIT_TYPE, TOPIC_TYPE, TOPIC_CODE, TOPIC_NAME, ORDER_NO, ORDER_DATE, AUDIT_FORM_TYPE, AUDIT_PROPOSAL_TYPE, AUDIT_BUDGET_TYPE, AUDIT_INCLUDED_COUNT, AUDIT_INCLUDED_ORG, WORKING_PERSON, WORKING_DAY, WORKING_ADDITION_TIME, AUDIT_DEPARTMENT_TYPE, AUDIT_DEPARTMENT_ID, AUDIT_SERVICE_PAY, AUDITOR_ENTRY_ID FROM BM0_DATA WHERE ID = :P_ID";

                // Set parameters
                cmd.BindByName = true;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("P_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ID, AUDIT_ID, TEAM_TYPE_ID, AUDITOR_ID FROM BM0_TEAM_DATA WHERE AUDIT_ID = :P_ID";

                // Set parameters
                cmd.BindByName = true;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("P_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTableTeam = new DataTable();
                dtTableTeam.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                con.Close();

                dtTable.TableName = "BM0Detail";
                dtTableTeam.TableName = "TeamData";

                DataSet ds = new DataSet();
                ds.Tables.Add(dtTable);
                ds.Tables.Add(dtTableTeam);

                StringWriter sw = new StringWriter();
                ds.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM0Update(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM0");
                
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM0_DATA SET OFFICE_ID = :P_OFFICE_ID, STATISTIC_PERIOD = :P_STATISTIC_PERIOD, AUDIT_TYPE = :P_AUDIT_TYPE, TOPIC_TYPE = :P_TOPIC_TYPE, TOPIC_CODE = :P_TOPIC_CODE, TOPIC_NAME = :P_TOPIC_NAME, ORDER_NO = :P_ORDER_NO, ORDER_DATE = :P_ORDER_DATE, AUDIT_FORM_TYPE = :P_AUDIT_FORM_TYPE, AUDIT_PROPOSAL_TYPE = :P_AUDIT_PROPOSAL_TYPE, AUDIT_BUDGET_TYPE = :P_AUDIT_BUDGET_TYPE, AUDIT_INCLUDED_ORG = :P_AUDIT_INCLUDED_ORG, WORKING_PERSON = :P_WORKING_PERSON, WORKING_DAY = :P_WORKING_DAY, WORKING_ADDITION_TIME = :P_WORKING_ADDITION_TIME, AUDIT_DEPARTMENT = :P_AUDIT_DEPARTMENT, AUDITOR_LEAD = :P_AUDITOR_LEAD, AUDITOR_MEMBER = :P_AUDITOR_MEMBER, AUDITOR_ENTRY = :P_AUDITOR_ENTRY, AUDIT_SERVICE_PAY = :P_AUDIT_SERVICE_PAY, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = elem.Element("ID")?.Value; //lnNextVal;
                cmd.Parameters.Add(":P_STATISTIC_PERIOD", OracleDbType.Int32).Value = elem.Element("STATISTIC_PERIOD")?.Value;
                cmd.Parameters.Add(":P_DEPARTMENT_ID", OracleDbType.Int32).Value = elem.Element("DEPARTMENT_ID")?.Value;
                cmd.Parameters.Add(":P_AUDIT_YEAR", OracleDbType.Int32).Value = elem.Element("AUDIT_YEAR")?.Value;
                cmd.Parameters.Add(":P_AUDIT_TYPE", OracleDbType.Int32).Value = elem.Element("AUDIT_TYPE")?.Value;
                cmd.Parameters.Add(":P_TOPIC_TYPE", OracleDbType.Int32).Value = elem.Element("TOPIC_TYPE")?.Value;
                cmd.Parameters.Add(":P_TOPIC_CODE", OracleDbType.Varchar2).Value = elem.Element("TOPIC_CODE")?.Value;
                cmd.Parameters.Add(":P_TOPIC_NAME", OracleDbType.Varchar2).Value = elem.Element("TOPIC_NAME")?.Value;
                cmd.Parameters.Add(":P_ORDER_NO", OracleDbType.Varchar2).Value = elem.Element("ORDER_NO")?.Value;
                cmd.Parameters.Add(":P_ORDER_DATE", OracleDbType.Varchar2).Value = elem.Element("ORDER_DATE")?.Value;
                cmd.Parameters.Add(":P_AUDIT_FORM_TYPE", OracleDbType.Int32).Value = elem.Element("AUDIT_FORM_TYPE") != null && elem.Element("AUDIT_FORM_TYPE").Value != "" ? elem.Element("AUDIT_FORM_TYPE").Value : null;
                cmd.Parameters.Add(":P_AUDIT_PROPOSAL_TYPE", OracleDbType.Int32).Value = elem.Element("AUDIT_PROPOSAL_TYPE") != null && elem.Element("AUDIT_PROPOSAL_TYPE").Value != "" ? elem.Element("AUDIT_PROPOSAL_TYPE").Value : null;
                cmd.Parameters.Add(":P_AUDIT_BUDGET_TYPE", OracleDbType.Int32).Value = elem.Element("AUDIT_BUDGET_TYPE") != null && elem.Element("AUDIT_BUDGET_TYPE").Value != "" ? elem.Element("AUDIT_BUDGET_TYPE").Value : null;
                cmd.Parameters.Add(":P_AUDIT_INCLUDED_COUNT", OracleDbType.Int32).Value = elem.Element("AUDIT_INCLUDED_COUNT")?.Value;
                cmd.Parameters.Add(":P_AUDIT_INCLUDED_ORG", OracleDbType.Varchar2).Value = elem.Element("AUDIT_INCLUDED_ORG")?.Value;
                cmd.Parameters.Add(":P_WORKING_PERSON", OracleDbType.Int32).Value = elem.Element("WORKING_PERSON") != null && elem.Element("WORKING_PERSON").Value != "" ? elem.Element("WORKING_PERSON").Value : null;
                cmd.Parameters.Add(":P_WORKING_DAY", OracleDbType.Int32).Value = elem.Element("WORKING_DAY") != null && elem.Element("WORKING_DAY").Value != "" ? elem.Element("WORKING_DAY").Value : null;
                cmd.Parameters.Add(":P_WORKING_ADDITION_TIME", OracleDbType.Int32).Value = elem.Element("P_WORKING_ADDITION_TIME") != null && elem.Element("P_WORKING_ADDITION_TIME").Value != "" ? elem.Element("P_WORKING_ADDITION_TIME").Value : null;
                cmd.Parameters.Add(":P_AUDIT_DEPARTMENT_TYPE", OracleDbType.Int32).Value = elem.Element("AUDIT_DEPARTMENT_TYPE")?.Value;
                cmd.Parameters.Add(":P_AUDIT_DEPARTMENT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_DEPARTMENT_TYPE")?.Value == "2" ? elem.Element("AUDIT_DEPARTMENT_ID")?.Value : elem.Element("DEPARTMENT_ID")?.Value;
                cmd.Parameters.Add(":P_AUDIT_SERVICE_PAY", OracleDbType.Decimal).Value = elem.Element("AUDIT_SERVICE_PAY") != null && elem.Element("AUDIT_SERVICE_PAY").Value != "" ? elem.Element("AUDIT_SERVICE_PAY").Value : null;
                cmd.Parameters.Add(":P_AUDITOR_ENTRY_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_IS_ACTIVE", OracleDbType.Int32).Value = elem.Element("IS_ACTIVE")?.Value;
                cmd.Parameters.Add(":P_CREATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_CREATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                cmd.Dispose();
                //Teams
                string[] leads = elem.Element("AUDITOR_LEAD")?.Value.Split(',');
                string[] members = elem.Element("AUDITOR_MEMBER")?.Value.Split(',');

                // delete query add
                for (int i = 0; i < leads.Count(); i++)
                {

                    OracleCommand cmdLead = con.CreateCommand();

                    cmdLead.CommandType = CommandType.Text;
                    cmdLead.CommandText = "INSERT INTO AUD_STAT.BM0_TEAM_DATA(AUDIT_ID, TEAM_TYPE_ID, AUDITOR_ID) " +
                        "VALUES(:V_AUDIT_ID, :V_TEAM_TYPE_ID, :V_AUDITOR_ID)";

                    cmdLead.BindByName = true;
                    cmdLead.Parameters.Add(":V_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("ID")?.Value;
                    cmdLead.Parameters.Add(":V_TEAM_TYPE_ID", OracleDbType.Int32).Value = 1;
                    cmdLead.Parameters.Add(":V_AUDITOR_ID", OracleDbType.Int32).Value = Convert.ToInt32(leads[i]);

                    cmdLead.ExecuteNonQuery();
                    cmdLead.Dispose();
                }
                for (int i = 0; i < members.Count(); i++)
                {

                    OracleCommand cmdMember = con.CreateCommand();

                    cmdMember.CommandType = CommandType.Text;
                    cmdMember.CommandText = "INSERT INTO AUD_STAT.BM0_TEAM_DATA(AUDIT_ID, TEAM_TYPE_ID, AUDITOR_ID) " +
                        "VALUES(:V_AUDIT_ID, :V_TEAM_TYPE_ID, :V_AUDITOR_ID)";
                    cmdMember.BindByName = true;
                    cmdMember.Parameters.Add(":V_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("ID")?.Value;
                    cmdMember.Parameters.Add(":V_TEAM_TYPE_ID", OracleDbType.Int32).Value = 2;
                    cmdMember.Parameters.Add(":V_AUDITOR_ID", OracleDbType.Int32).Value = Convert.ToInt32(members[i]); ;

                    cmdMember.ExecuteNonQuery();
                    cmdMember.Dispose();
                }


                transaction.Commit();
                con.Close();
                bool responseVal = rowsUpdated == 0 ? false : true;
                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM0Insert(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleCommand cmd2 = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "SELECT AUD_STAT.SEQ_BM0_DATA.NEXTVAL FROM DUAL";
                int lnNextVal = Convert.ToInt32(cmd2.ExecuteScalar());

                XElement elem = request.Element("Parameters").Element("BM0");
                
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO AUD_STAT.BM0_DATA (ID,STATISTIC_PERIOD, DEPARTMENT_ID, AUDIT_YEAR, AUDIT_TYPE, TOPIC_TYPE, TOPIC_CODE, TOPIC_NAME, ORDER_NO, ORDER_DATE, AUDIT_FORM_TYPE, AUDIT_PROPOSAL_TYPE, AUDIT_BUDGET_TYPE, AUDIT_INCLUDED_COUNT, AUDIT_INCLUDED_ORG, WORKING_PERSON, WORKING_DAY, WORKING_ADDITION_TIME, AUDIT_DEPARTMENT_TYPE, AUDIT_DEPARTMENT_ID, AUDIT_SERVICE_PAY, AUDITOR_ENTRY_ID, IS_ACTIVE, CREATED_BY, CREATED_DATE) " +
                    "VALUES(:P_ID, :P_STATISTIC_PERIOD, :P_DEPARTMENT_ID, :P_AUDIT_YEAR, :P_AUDIT_TYPE, :P_TOPIC_TYPE, :P_TOPIC_CODE, :P_TOPIC_NAME, :P_ORDER_NO, :P_ORDER_DATE, :P_AUDIT_FORM_TYPE, :P_AUDIT_PROPOSAL_TYPE, :P_AUDIT_BUDGET_TYPE, :P_AUDIT_INCLUDED_COUNT, :P_AUDIT_INCLUDED_ORG, :P_WORKING_PERSON, :P_WORKING_DAY, :P_WORKING_ADDITION_TIME, :P_AUDIT_DEPARTMENT_TYPE, :P_AUDIT_DEPARTMENT_ID, :P_AUDIT_SERVICE_PAY, :P_AUDITOR_ENTRY_ID, :P_IS_ACTIVE, :P_CREATED_BY, :P_CREATED_DATE)";

                // Set parameters
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = lnNextVal;
                cmd.Parameters.Add(":P_STATISTIC_PERIOD", OracleDbType.Int32).Value = elem.Element("STATISTIC_PERIOD")?.Value;
                cmd.Parameters.Add(":P_DEPARTMENT_ID", OracleDbType.Int32).Value = elem.Element("DEPARTMENT_ID")?.Value;
                cmd.Parameters.Add(":P_AUDIT_YEAR", OracleDbType.Int32).Value = elem.Element("AUDIT_YEAR")?.Value;
                cmd.Parameters.Add(":P_AUDIT_TYPE", OracleDbType.Int32).Value = elem.Element("AUDIT_TYPE")?.Value;
                cmd.Parameters.Add(":P_TOPIC_TYPE", OracleDbType.Int32).Value = elem.Element("TOPIC_TYPE")?.Value;
                cmd.Parameters.Add(":P_TOPIC_CODE", OracleDbType.Varchar2).Value = elem.Element("TOPIC_CODE")?.Value;
                cmd.Parameters.Add(":P_TOPIC_NAME", OracleDbType.Varchar2).Value = elem.Element("TOPIC_NAME")?.Value;
                cmd.Parameters.Add(":P_ORDER_NO", OracleDbType.Varchar2).Value = elem.Element("ORDER_NO")?.Value;
                cmd.Parameters.Add(":P_ORDER_DATE", OracleDbType.Varchar2).Value = elem.Element("ORDER_DATE")?.Value;
                cmd.Parameters.Add(":P_AUDIT_FORM_TYPE", OracleDbType.Int32).Value = elem.Element("AUDIT_FORM_TYPE") != null && elem.Element("AUDIT_FORM_TYPE").Value != "" ? elem.Element("AUDIT_FORM_TYPE").Value : null;
                cmd.Parameters.Add(":P_AUDIT_PROPOSAL_TYPE", OracleDbType.Int32).Value = elem.Element("AUDIT_PROPOSAL_TYPE") != null && elem.Element("AUDIT_PROPOSAL_TYPE").Value != "" ? elem.Element("AUDIT_PROPOSAL_TYPE").Value : null;
                cmd.Parameters.Add(":P_AUDIT_BUDGET_TYPE", OracleDbType.Int32).Value = elem.Element("AUDIT_BUDGET_TYPE") != null && elem.Element("AUDIT_BUDGET_TYPE").Value != "" ? elem.Element("AUDIT_BUDGET_TYPE").Value : null;
                cmd.Parameters.Add(":P_AUDIT_INCLUDED_COUNT", OracleDbType.Int32).Value = elem.Element("AUDIT_INCLUDED_COUNT")?.Value;
                cmd.Parameters.Add(":P_AUDIT_INCLUDED_ORG", OracleDbType.Varchar2).Value = elem.Element("AUDIT_INCLUDED_ORG")?.Value;
                cmd.Parameters.Add(":P_WORKING_PERSON", OracleDbType.Int32).Value = elem.Element("WORKING_PERSON") != null && elem.Element("WORKING_PERSON").Value != "" ? elem.Element("WORKING_PERSON").Value : null;
                cmd.Parameters.Add(":P_WORKING_DAY", OracleDbType.Int32).Value = elem.Element("WORKING_DAY") != null && elem.Element("WORKING_DAY").Value != "" ? elem.Element("WORKING_DAY").Value : null;
                cmd.Parameters.Add(":P_WORKING_ADDITION_TIME", OracleDbType.Int32).Value = elem.Element("P_WORKING_ADDITION_TIME") != null && elem.Element("P_WORKING_ADDITION_TIME").Value != "" ? elem.Element("P_WORKING_ADDITION_TIME").Value : null; 
                cmd.Parameters.Add(":P_AUDIT_DEPARTMENT_TYPE", OracleDbType.Int32).Value = elem.Element("AUDIT_DEPARTMENT_TYPE")?.Value;
                cmd.Parameters.Add(":P_AUDIT_DEPARTMENT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_DEPARTMENT_TYPE")?.Value =="2"? elem.Element("AUDIT_DEPARTMENT_ID")?.Value: elem.Element("DEPARTMENT_ID")?.Value;
                cmd.Parameters.Add(":P_AUDIT_SERVICE_PAY", OracleDbType.Decimal).Value = elem.Element("AUDIT_SERVICE_PAY") != null && elem.Element("AUDIT_SERVICE_PAY").Value != "" ? elem.Element("AUDIT_SERVICE_PAY").Value : null;
                cmd.Parameters.Add(":P_AUDITOR_ENTRY_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_IS_ACTIVE", OracleDbType.Int32).Value = elem.Element("IS_ACTIVE")?.Value;
                cmd.Parameters.Add(":P_CREATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_CREATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                cmd.Dispose();
                //Teams
                string[] leads = elem.Element("AUDITOR_LEAD")?.Value.Split(',');
                string[] members = elem.Element("AUDITOR_MEMBER")?.Value.Split(',');

                for(int i = 0; i < leads.Count(); i++)
                {

                    OracleCommand cmdLead = con.CreateCommand();

                    cmdLead.CommandType = CommandType.Text;
                    cmdLead.CommandText = "INSERT INTO AUD_STAT.BM0_TEAM_DATA(AUDIT_ID, TEAM_TYPE_ID, AUDITOR_ID) " +
                        "VALUES(:V_AUDIT_ID, :V_TEAM_TYPE_ID, :V_AUDITOR_ID)";

                    cmdLead.BindByName = true;
                    cmdLead.Parameters.Add(":V_AUDIT_ID", OracleDbType.Int32).Value = lnNextVal;
                    cmdLead.Parameters.Add(":V_TEAM_TYPE_ID", OracleDbType.Int32).Value = 1;
                    cmdLead.Parameters.Add(":V_AUDITOR_ID", OracleDbType.Int32).Value = Convert.ToInt32(leads[i]);

                    cmdLead.ExecuteNonQuery();
                    cmdLead.Dispose();
                }
                for (int i = 0; i < members.Count(); i++)
                {

                    OracleCommand cmdMember = con.CreateCommand();

                    cmdMember.CommandType = CommandType.Text;
                    cmdMember.CommandText = "INSERT INTO AUD_STAT.BM0_TEAM_DATA(AUDIT_ID, TEAM_TYPE_ID, AUDITOR_ID) " +
                        "VALUES(:V_AUDIT_ID, :V_TEAM_TYPE_ID, :V_AUDITOR_ID)";
                    cmdMember.BindByName = true;
                    cmdMember.Parameters.Add(":V_AUDIT_ID", OracleDbType.Int32).Value = lnNextVal;
                    cmdMember.Parameters.Add(":V_TEAM_TYPE_ID", OracleDbType.Int32).Value = 2;
                    cmdMember.Parameters.Add(":V_AUDITOR_ID", OracleDbType.Int32).Value = Convert.ToInt32(members[i]); ;

                    cmdMember.ExecuteNonQuery();
                    cmdMember.Dispose();
                }


                transaction.Commit();
                con.Close();
                bool responseVal = rowsUpdated == 0 ? false : true;
                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM0Delete(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM0_DATA SET IS_ACTIVE = 0, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("UPDATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Устгалаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(BM.ID) "+
                    "FROM AUD_STAT.BM1_DATA BM " +
                    "INNER JOIN AUD_STAT.BM0_DATA B ON BM.AUDIT_ID = B.ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON B.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON B.DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON B.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON B.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON B.TOPIC_TYPE = RTT.TOPIC_TYPE_ID LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON B.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_VIOLATION_TYPE RVT ON BM.ACT_VIOLATION_TYPE = RVT.VIOLATION_ID " +
                    "LEFT JOIN AUD_REG.SYSTEM_USER SU ON BM.ACT_CONTROL_AUDITOR_ID = SU.USER_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND B.IS_ACTIVE = 1 " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR (:V_USER_TYPE = 'Branch_Auditor' AND B.AUDIT_DEPARTMENT_ID = :V_DEPARTMENT))  " +
                    "AND B.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(B.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%'  " +
                    "OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.ACT_VIOLATION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(B.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%'  " +
                    "OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.ACT_VIOLATION_DESC) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(SU.USER_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.COMPLETION_ORDER) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT B.STATISTIC_PERIOD, RP.PERIOD_LABEL, B.DEPARTMENT_ID, RD.DEPARTMENT_NAME, RAY.YEAR_LABEL, B.AUDIT_TYPE, RAT.AUDIT_TYPE_NAME, RTT.TOPIC_TYPE_NAME, B.TOPIC_CODE, B.TOPIC_NAME, B.ORDER_NO, B.ORDER_DATE, RBT.BUDGET_TYPE_NAME, BM.ACT_DATE, BM.ACT_NO, BM.ACT_VIOLATION_DESC, BM.ACT_VIOLATION_TYPE, RVT.VIOLATION_NAME, BM.ACT_SUBMITTED_DATE, BM.ACT_DELIVERY_DATE, BM.ACT_AMOUNT, BM.ACT_STATE_AMOUNT, BM.ACT_LOCAL_AMOUNT, BM.ACT_ORG_AMOUNT, BM.ACT_OTHER_AMOUNT, BM.ACT_RCV_NAME, BM.ACT_RCV_ROLE, BM.ACT_RCV_GIVEN_NAME, BM.ACT_RCV_PHONE, BM.ACT_RCV_ADDRESS, SU.USER_CODE||' - '||SU.USER_NAME ACT_CONTROL_AUDITOR, BM.COMPLETION_DATE, BM.COMPLETION_ORDER, BM.COMPLETION_AMOUNT, BM.COMPLETION_STATE_AMOUNT, BM.COMPLETION_LOCAL_AMOUNT, BM.COMPLETION_ORG_AMOUNT, BM.COMPLETION_OTHER_AMOUNT, BM.REMOVED_AMOUNT, BM.REMOVED_LAW_AMOUNT, BM.REMOVED_LAW_DATE, BM.REMOVED_LAW_NO, BM.REMOVED_INVALID_AMOUNT, BM.REMOVED_INVALID_DATE, BM.REMOVED_INVALID_NO, BM.ACT_C2_AMOUNT, BM.ACT_C2_NONEXPIRED, BM.ACT_C2_EXPIRED, BM.BENEFIT_FIN, BM.BENEFIT_FIN_AMOUNT, BM.BENEFIT_NONFIN " +
                    "FROM AUD_STAT.BM1_DATA BM " +
                    "INNER JOIN AUD_STAT.BM0_DATA B ON BM.AUDIT_ID = B.ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON B.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON B.DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON B.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON B.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON B.TOPIC_TYPE = RTT.TOPIC_TYPE_ID LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON B.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_VIOLATION_TYPE RVT ON BM.ACT_VIOLATION_TYPE = RVT.VIOLATION_ID " +
                    "LEFT JOIN AUD_REG.SYSTEM_USER SU ON BM.ACT_CONTROL_AUDITOR_ID = SU.USER_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND B.IS_ACTIVE = 1 " +
                    "AND (:V_USER_TYPE != 'BRANCH_AUDITOR' OR (:V_USER_TYPE = 'BRANCH_AUDITOR' AND B.AUDIT_DEPARTMENT_ID = :V_DEPARTMENT))  " +
                    "AND B.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(B.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%'  " +
                    "OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.ACT_VIOLATION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(B.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%'  " +
                    "OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.ACT_VIOLATION_DESC) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(SU.USER_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.COMPLETION_ORDER) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN BM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_DEPARTMENT_ID' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_DEPARTMENT_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_DEPARTMENT_ID' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_DEPARTMENT_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND :ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND :ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND :ORDER_DIR = 'ASC' THEN B.TOPIC_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND :ORDER_DIR = 'DESC' THEN B.TOPIC_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND :ORDER_DIR = 'ASC' THEN B.TOPIC_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND :ORDER_DIR = 'DESC' THEN B.TOPIC_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_BUDGET_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_BUDGET_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND :ORDER_DIR = 'ASC' THEN B.ORDER_NO END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND :ORDER_DIR = 'DESC' THEN B.ORDER_NO END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ACT_VIOLATION_DESC' AND :ORDER_DIR = 'ASC' THEN BM.ACT_VIOLATION_DESC END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ACT_VIOLATION_DESC' AND :ORDER_DIR = 'DESC' THEN BM.ACT_VIOLATION_DESC END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'USER_NAME' AND :ORDER_DIR = 'ASC' THEN SU.USER_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'USER_NAME' AND :ORDER_DIR = 'DESC' THEN SU.USER_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'COMPLETION_ORDER' AND :ORDER_DIR = 'ASC' THEN BM.COMPLETION_ORDER END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'COMPLETION_ORDER' AND :ORDER_DIR = 'DESC' THEN BM.COMPLETION_ORDER END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ACT_VIOLATION_TYPE' AND :ORDER_DIR = 'ASC' THEN BM.ACT_VIOLATION_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ACT_VIOLATION_TYPE' AND :ORDER_DIR = 'DESC' THEN BM.ACT_VIOLATION_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM1";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM1Detail(XElement request)
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
                cmd.CommandText = "SELECT OFFICE_ID, STATISTIC_PERIOD, AUDIT_YEAR, AUDIT_TYPE, AUDIT_CODE, AUDIT_NAME, AUDIT_BUDGET_TYPE, ORDER_DATE, ORDER_NO, ACT_NO, ACT_VIOLATION_DESC, ACT_VIOLATION_TYPE, ACT_SUBMITTED_DATE, ACT_DELIVERY_DATE, ACT_AMOUNT, ACT_STATE_AMOUNT, ACT_LOCAL_AMOUNT, ACT_ORG_AMOUNT, ACT_OTHER_AMOUNT, ACT_RCV_NAME, ACT_RCV_ROLE, ACT_RCV_GIVEN_NAME, ACT_RCV_ADDRESS, ACT_CONTROL_AUDITOR, COMPLETION_ORDER, COMPLETION_AMOUNT, COMPLETION_STATE_AMOUNT, COMPLETION_LOCAL_AMOUNT, COMPLETION_ORG_AMOUNT, COMPLETION_OTHER_AMOUNT, REMOVED_AMOUNT, REMOVED_LAW_AMOUNT, REMOVED_LAW_DATE_NO, REMOVED_INVALID_AMOUNT, REMOVED_INVALID_DATE_NO, ACT_C2_AMOUNT, ACT_C2_NONEXPIRED, ACT_C2_EXPIRED, BENEFIT_FIN, BENEFIT_FIN_AMOUNT, BENEFIT_NONFIN, EXEC_TYPE, ID, AUDIT_ID FROM AUD_STAT.BM1_DATA WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("P_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM1Detail";

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
        public static DataResponse BM1Update(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM1");
                
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM1_DATA SET AUDIT_ID = :P_AUDIT_ID, ACT_DATE = :P_ACT_DATE, ACT_NO = :P_ACT_NO, ACT_VIOLATION_DESC = :P_ACT_VIOLATION_DESC, ACT_VIOLATION_TYPE = :P_ACT_VIOLATION_TYPE, ACT_SUBMITTED_DATE = :P_ACT_SUBMITTED_DATE, ACT_DELIVERY_DATE = :P_ACT_DELIVERY_DATE, ACT_AMOUNT = :P_ACT_AMOUNT, ACT_STATE_AMOUNT = :P_ACT_STATE_AMOUNT, ACT_LOCAL_AMOUNT = :P_ACT_LOCAL_AMOUNT, ACT_ORG_AMOUNT = :P_ACT_ORG_AMOUNT, ACT_OTHER_AMOUNT = :P_ACT_OTHER_AMOUNT, ACT_RCV_NAME = :P_ACT_RCV_NAME, ACT_RCV_ROLE = :P_ACT_RCV_ROLE, ACT_RCV_GIVEN_NAME = :P_ACT_RCV_GIVEN_NAME, ACT_RCV_PHONE = :P_ACT_RCV_PHONE, ACT_RCV_ADDRESS = :P_ACT_RCV_ADDRESS, COMPLETION_DATE = :P_COMPLETION_DATE, COMPLETION_ORDER = :P_COMPLETION_ORDER, COMPLETION_AMOUNT = :P_COMPLETION_AMOUNT, COMPLETION_STATE_AMOUNT = :P_COMPLETION_STATE_AMOUNT, COMPLETION_DATE = :P_COMPLETION_DATE,  COMPLETION_DATE = :P_COMPLETION_DATE, COMPLETION_DATE = :P_COMPLETION_DATE, COMPLETION_LOCAL_AMOUNT = :P_COMPLETION_LOCAL_AMOUNT, COMPLETION_ORG_AMOUNT = :P_COMPLETION_ORG_AMOUNT, COMPLETION_OTHER_AMOUNT = :P_COMPLETION_OTHER_AMOUNT, REMOVED_AMOUNT = :P_REMOVED_AMOUNT, REMOVED_LAW_AMOUNT = :P_REMOVED_LAW_AMOUNT, REMOVED_LAW_DATE = :P_REMOVED_LAW_DATE, REMOVED_LAW_NO = :P_REMOVED_LAW_NO, REMOVED_INVALID_AMOUNT = :P_REMOVED_INVALID_AMOUNT, REMOVED_INVALID_DATE = :P_REMOVED_INVALID_DATE, REMOVED_INVALID_NO = :P_REMOVED_INVALID_NO, ACT_C2_AMOUNT = :P_ACT_C2_AMOUNT, ACT_C2_NONEXPIRED = :P_ACT_C2_NONEXPIRED, ACT_C2_EXPIRED = :P_ACT_C2_EXPIRED, BENEFIT_FIN = :P_BENEFIT_FIN, BENEFIT_FIN_AMOUNT = :P_BENEFIT_FIN_AMOUNT, BENEFIT_NONFIN = :P_BENEFIT_NONFIN, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_ID")?.Value;
                cmd.Parameters.Add(":P_ACT_DATE", OracleDbType.Varchar2).Value = elem.Element("ACT_DATE")?.Value;
                cmd.Parameters.Add(":P_ACT_NO", OracleDbType.Varchar2).Value = elem.Element("ACT_NO")?.Value;
                cmd.Parameters.Add(":P_ACT_VIOLATION_DESC", OracleDbType.Varchar2).Value = elem.Element("ACT_VIOLATION_DESC")?.Value;
                cmd.Parameters.Add(":P_ACT_VIOLATION_TYPE", OracleDbType.Int32).Value = elem.Element("ACT_VIOLATION_TYPE")?.Value;
                cmd.Parameters.Add(":P_ACT_SUBMITTED_DATE", OracleDbType.Varchar2).Value = elem.Element("ACT_SUBMITTED_DATE")?.Value;
                cmd.Parameters.Add(":P_ACT_DELIVERY_DATE", OracleDbType.Varchar2).Value = elem.Element("ACT_DELIVERY_DATE")?.Value;
                cmd.Parameters.Add(":P_ACT_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACT_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACT_STATE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACT_STATE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACT_LOCAL_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACT_LOCAL_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACT_ORG_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACT_ORG_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACT_OTHER_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACT_OTHER_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACT_RCV_NAME", OracleDbType.Varchar2).Value = elem.Element("ACT_RCV_NAME")?.Value;
                cmd.Parameters.Add(":P_ACT_RCV_ROLE", OracleDbType.Varchar2).Value = elem.Element("ACT_RCV_ROLE")?.Value;
                cmd.Parameters.Add(":P_ACT_RCV_GIVEN_NAME", OracleDbType.Varchar2).Value = elem.Element("ACT_RCV_GIVEN_NAME")?.Value;
                cmd.Parameters.Add(":ACT_RCV_PHONE", OracleDbType.Varchar2).Value = elem.Element("ACT_RCV_PHONE")?.Value;
                cmd.Parameters.Add(":P_ACT_RCV_ADDRESS", OracleDbType.Varchar2).Value = elem.Element("ACT_RCV_ADDRESS")?.Value;
                //cmd.Parameters.Add(":P_ACT_CONTROL_AUDITOR_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_COMPLETION_DATE", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_DATE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_ORDER", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_ORDER")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_STATE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_STATE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_LOCAL_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_LOCAL_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_ORG_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_ORG_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_OTHER_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_OTHER_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_AMOUNT", OracleDbType.Decimal).Value = elem.Element("REMOVED_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_LAW_AMOUNT", OracleDbType.Decimal).Value = elem.Element("REMOVED_LAW_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_LAW_DATE", OracleDbType.Varchar2).Value = elem.Element("REMOVED_LAW_DATE")?.Value;
                cmd.Parameters.Add(":P_REMOVED_LAW_NO", OracleDbType.Varchar2).Value = elem.Element("REMOVED_LAW_NO")?.Value;
                cmd.Parameters.Add(":P_REMOVED_INVALID_AMOUNT", OracleDbType.Decimal).Value = elem.Element("REMOVED_INVALID_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_INVALID_DATE", OracleDbType.Varchar2).Value = elem.Element("REMOVED_INVALID_DATE")?.Value;
                cmd.Parameters.Add(":P_REMOVED_INVALID_NO", OracleDbType.Varchar2).Value = elem.Element("REMOVED_INVALID_NO")?.Value;
                cmd.Parameters.Add(":P_ACT_C2_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACT_C2_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACT_C2_NONEXPIRED", OracleDbType.Decimal).Value = elem.Element("ACT_C2_NONEXPIRED")?.Value;
                cmd.Parameters.Add(":P_ACT_C2_EXPIRED", OracleDbType.Decimal).Value = elem.Element("ACT_C2_EXPIRED")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_FIN", OracleDbType.Int32).Value = elem.Element("BENEFIT_FIN")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_FIN_AMOUNT", OracleDbType.Decimal).Value = elem.Element("BENEFIT_FIN_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_NONFIN", OracleDbType.Int32).Value = elem.Element("BENEFIT_NONFIN")?.Value;
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = elem.Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM1Insert(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM1");
                
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO AUD_STAT.BM1_DATA(AUDIT_ID, ACT_DATE, ACT_NO, ACT_VIOLATION_DESC, ACT_VIOLATION_TYPE, ACT_SUBMITTED_DATE, ACT_DELIVERY_DATE, ACT_AMOUNT, ACT_STATE_AMOUNT, ACT_LOCAL_AMOUNT, ACT_ORG_AMOUNT, ACT_OTHER_AMOUNT, ACT_RCV_NAME, ACT_RCV_ROLE, ACT_RCV_GIVEN_NAME, ACT_RCV_PHONE, ACT_RCV_ADDRESS, ACT_CONTROL_AUDITOR_ID, COMPLETION_DATE, COMPLETION_ORDER, COMPLETION_AMOUNT, COMPLETION_STATE_AMOUNT, COMPLETION_LOCAL_AMOUNT, COMPLETION_ORG_AMOUNT, COMPLETION_OTHER_AMOUNT, REMOVED_AMOUNT, REMOVED_LAW_AMOUNT, REMOVED_LAW_DATE, REMOVED_LAW_NO, REMOVED_INVALID_AMOUNT, REMOVED_INVALID_DATE, REMOVED_INVALID_NO, ACT_C2_AMOUNT, ACT_C2_NONEXPIRED, ACT_C2_EXPIRED, BENEFIT_FIN, BENEFIT_FIN_AMOUNT, BENEFIT_NONFIN, IS_ACTIVE, CREATED_BY, CREATED_DATE) " +
                    "VALUES (:P_AUDIT_ID, :P_ACT_DATE, :P_ACT_NO, :P_ACT_VIOLATION_DESC, :P_ACT_VIOLATION_TYPE, :P_ACT_SUBMITTED_DATE, :P_ACT_DELIVERY_DATE, :P_ACT_AMOUNT, :P_ACT_STATE_AMOUNT, :P_ACT_LOCAL_AMOUNT, :P_ACT_ORG_AMOUNT, :P_ACT_OTHER_AMOUNT, :P_ACT_RCV_NAME, :P_ACT_RCV_ROLE, :P_ACT_RCV_GIVEN_NAME, :P_ACT_RCV_PHONE, :P_ACT_RCV_ADDRESS, :P_ACT_CONTROL_AUDITOR_ID, :P_COMPLETION_DATE, :P_COMPLETION_ORDER, :P_COMPLETION_AMOUNT, :P_COMPLETION_STATE_AMOUNT, :P_COMPLETION_LOCAL_AMOUNT, :P_COMPLETION_ORG_AMOUNT, :P_COMPLETION_OTHER_AMOUNT, :P_REMOVED_AMOUNT, :P_REMOVED_LAW_AMOUNT, :P_REMOVED_LAW_DATE, :P_REMOVED_LAW_NO, :P_REMOVED_INVALID_AMOUNT, :P_REMOVED_INVALID_DATE, :P_REMOVED_INVALID_NO, :P_ACT_C2_AMOUNT, :P_ACT_C2_NONEXPIRED, :P_ACT_C2_EXPIRED, :P_BENEFIT_FIN, :P_BENEFIT_FIN_AMOUNT, :P_BENEFIT_NONFIN, :P_IS_ACTIVE, :P_CREATED_BY, :P_CREATED_DATE)";

                // Set parameters
                cmd.Parameters.Add(":P_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_ID")?.Value;
                cmd.Parameters.Add(":P_ACT_DATE", OracleDbType.Varchar2).Value = elem.Element("ACT_DATE")?.Value;
                cmd.Parameters.Add(":P_ACT_NO", OracleDbType.Varchar2).Value = elem.Element("ACT_NO")?.Value;
                cmd.Parameters.Add(":P_ACT_VIOLATION_DESC", OracleDbType.Varchar2).Value = elem.Element("ACT_VIOLATION_DESC")?.Value;
                cmd.Parameters.Add(":P_ACT_VIOLATION_TYPE", OracleDbType.Int32).Value = elem.Element("ACT_VIOLATION_TYPE")?.Value;
                cmd.Parameters.Add(":P_ACT_SUBMITTED_DATE", OracleDbType.Varchar2).Value = elem.Element("ACT_SUBMITTED_DATE")?.Value;
                cmd.Parameters.Add(":P_ACT_DELIVERY_DATE", OracleDbType.Varchar2).Value = elem.Element("ACT_DELIVERY_DATE")?.Value;
                cmd.Parameters.Add(":P_ACT_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACT_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACT_STATE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACT_STATE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACT_LOCAL_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACT_LOCAL_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACT_ORG_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACT_ORG_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACT_OTHER_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACT_OTHER_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACT_RCV_NAME", OracleDbType.Varchar2).Value = elem.Element("ACT_RCV_NAME")?.Value;
                cmd.Parameters.Add(":P_ACT_RCV_ROLE", OracleDbType.Varchar2).Value = elem.Element("ACT_RCV_ROLE")?.Value;
                cmd.Parameters.Add(":P_ACT_RCV_GIVEN_NAME", OracleDbType.Varchar2).Value = elem.Element("ACT_RCV_GIVEN_NAME")?.Value;
                cmd.Parameters.Add(":P_ACT_RCV_PHONE", OracleDbType.Varchar2).Value = elem.Element("ACT_RCV_PHONE")?.Value;
                cmd.Parameters.Add(":P_ACT_RCV_ADDRESS", OracleDbType.Varchar2).Value = elem.Element("ACT_RCV_ADDRESS")?.Value;
                cmd.Parameters.Add(":P_ACT_CONTROL_AUDITOR_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_COMPLETION_DATE", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_DATE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_ORDER", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_ORDER")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_STATE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_STATE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_LOCAL_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_LOCAL_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_ORG_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_ORG_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_OTHER_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_OTHER_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_AMOUNT", OracleDbType.Decimal).Value = elem.Element("REMOVED_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_LAW_AMOUNT", OracleDbType.Decimal).Value = elem.Element("REMOVED_LAW_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_LAW_DATE", OracleDbType.Varchar2).Value = elem.Element("REMOVED_LAW_DATE")?.Value;
                cmd.Parameters.Add(":P_REMOVED_LAW_NO", OracleDbType.Varchar2).Value = elem.Element("REMOVED_LAW_NO")?.Value;
                cmd.Parameters.Add(":P_REMOVED_INVALID_AMOUNT", OracleDbType.Decimal).Value = elem.Element("REMOVED_INVALID_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_INVALID_DATE", OracleDbType.Varchar2).Value = elem.Element("REMOVED_INVALID_DATE")?.Value;
                cmd.Parameters.Add(":P_REMOVED_INVALID_NO", OracleDbType.Varchar2).Value = elem.Element("REMOVED_INVALID_NO")?.Value;
                cmd.Parameters.Add(":P_ACT_C2_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACT_C2_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACT_C2_NONEXPIRED", OracleDbType.Decimal).Value = elem.Element("ACT_C2_NONEXPIRED")?.Value;
                cmd.Parameters.Add(":P_ACT_C2_EXPIRED", OracleDbType.Decimal).Value = elem.Element("ACT_C2_EXPIRED")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_FIN", OracleDbType.Int32).Value = elem.Element("BENEFIT_FIN")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_FIN_AMOUNT", OracleDbType.Decimal).Value = elem.Element("BENEFIT_FIN_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_NONFIN", OracleDbType.Int32).Value = elem.Element("BENEFIT_NONFIN")?.Value;
                cmd.Parameters.Add(":P_IS_ACTIVE", OracleDbType.Int32).Value = elem.Element("IS_ACTIVE")?.Value;
                cmd.Parameters.Add(":P_CREATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_CREATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM1Delete(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM1_DATA " +
                    "SET IS_ACTIVE = 0, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("UPDATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Устгалаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(BM.ID) " +
                    "FROM AUD_STAT.BM2_DATA BM " +
                    "INNER JOIN AUD_STAT.BM0_DATA B ON BM.AUDIT_ID = B.ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON B.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON B.DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON B.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON B.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON B.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON B.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_VIOLATION_TYPE RVT ON BM.CLAIM_VIOLATION_TYPE = RVT.VIOLATION_ID " +
                    "LEFT JOIN AUD_REG.SYSTEM_USER SU ON BM.CLAIM_CONTROL_AUDITOR_ID = SU.USER_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND B.IS_ACTIVE = 1 " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR (:V_USER_TYPE = 'Branch_Auditor' AND B.AUDIT_DEPARTMENT_ID = :V_DEPARTMENT)) " +
                    "AND B.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(B.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.CLAIM_VIOLATION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(B.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.CLAIM_VIOLATION_DESC) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(SU.USER_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.COMPLETION_ORDER) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT B.STATISTIC_PERIOD, RP.PERIOD_LABEL, B.DEPARTMENT_ID, RD.DEPARTMENT_NAME, RAY.YEAR_LABEL, B.AUDIT_TYPE, RAT.AUDIT_TYPE_NAME, RTT.TOPIC_TYPE_NAME, B.TOPIC_CODE, B.TOPIC_NAME, B.ORDER_DATE, B.ORDER_NO, RBT.BUDGET_TYPE_NAME, BM.CLAIM_DATE, BM.CLAIM_NO, BM.CLAIM_VIOLATION_DESC, BM.CLAIM_VIOLATION_TYPE, BM.CLAIM_SUBMITTED_DATE, BM.CLAIM_DELIVERY_DATE, BM.CLAIM_VIOLATION_AMOUNT, BM.CLAIM_RCV_NAME, BM.CLAIM_RCV_ROLE, BM.CLAIM_RCV_GIVEN_NAME, BM.CLAIM_RCV_PHONE, BM.CLAIM_RCV_ADDRESS, SU.USER_CODE||' - '||SU.USER_NAME CLAIM_CONTROL_AUDITOR, BM.COMPLETION_DATE, BM.COMPLETION_ORDER, BM.COMPLETION_AMOUNT, BM.COMPLETION_STATE_AMOUNT, BM.COMPLETION_LOCAL_AMOUNT, BM.COMPLETION_ORG_AMOUNT, BM.COMPLETION_OTHER_AMOUNT, BM.REMOVED_LAW_AMOUNT, BM.REMOVED_LAW_DATE, BM.REMOVED_LAW_NO, BM.REMOVED_INVALID_AMOUNT, BM.REMOVED_INVALID_DATE, BM.REMOVED_INVALID_NO, BM.CLAIM_C2_AMOUNT, BM.CLAIM_C2_NONEXPIRED, BM.CLAIM_C2_EXPIRED, BM.BENEFIT_FIN, BM.BENEFIT_FIN_AMOUNT, BM.BENEFIT_NONFIN " +
                    "FROM AUD_STAT.BM2_DATA BM " +
                    "INNER JOIN AUD_STAT.BM0_DATA B ON BM.AUDIT_ID = B.ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON B.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON B.DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON B.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON B.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON B.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON B.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_VIOLATION_TYPE RVT ON BM.CLAIM_VIOLATION_TYPE = RVT.VIOLATION_ID " +
                    "LEFT JOIN AUD_REG.SYSTEM_USER SU ON BM.CLAIM_CONTROL_AUDITOR_ID = SU.USER_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND B.IS_ACTIVE = 1 " +
                    "AND (:V_USER_TYPE != 'BRANCH_AUDITOR' OR (:V_USER_TYPE = 'Branch_Auditor' AND B.AUDIT_DEPARTMENT_ID = :V_DEPARTMENT)) " +
                    "AND B.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(B.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.CLAIM_VIOLATION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(B.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.CLAIM_VIOLATION_DESC) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(SU.USER_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.COMPLETION_ORDER) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN BM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_DEPARTMENT_ID' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_DEPARTMENT_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_DEPARTMENT_ID' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_DEPARTMENT_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND :ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND :ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND :ORDER_DIR = 'ASC' THEN B.TOPIC_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND :ORDER_DIR = 'DESC' THEN B.TOPIC_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND :ORDER_DIR = 'ASC' THEN B.TOPIC_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND :ORDER_DIR = 'DESC' THEN B.TOPIC_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_BUDGET_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_BUDGET_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND :ORDER_DIR = 'ASC' THEN B.ORDER_NO END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND :ORDER_DIR = 'DESC' THEN B.ORDER_NO END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'CLAIM_VIOLATION_DESC' AND :ORDER_DIR = 'ASC' THEN BM.CLAIM_VIOLATION_DESC END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'CLAIM_VIOLATION_DESC' AND :ORDER_DIR = 'DESC' THEN BM.CLAIM_VIOLATION_DESC END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'USER_NAME' AND :ORDER_DIR = 'ASC' THEN SU.USER_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'USER_NAME' AND :ORDER_DIR = 'DESC' THEN SU.USER_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'COMPLETION_ORDER' AND :ORDER_DIR = 'ASC' THEN BM.COMPLETION_ORDER END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'COMPLETION_ORDER' AND :ORDER_DIR = 'DESC' THEN BM.COMPLETION_ORDER END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'CLAIM_VIOLATION_TYPE' AND :ORDER_DIR = 'ASC' THEN BM.CLAIM_VIOLATION_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'CLAIM_VIOLATION_TYPE' AND :ORDER_DIR = 'DESC' THEN BM.CLAIM_VIOLATION_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM2";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM2Detail(XElement request)
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
                cmd.CommandText = "SELECT OFFICE_ID, STATISTIC_PERIOD, AUDIT_YEAR, AUDIT_TYPE, AUDIT_CODE, AUDIT_NAME, AUDIT_BUDGET_TYPE, ORDER_DATE, ORDER_NO, CLAIM_NO, CLAIM_VIOLATION_DESC, CLAIM_VIOLATION_TYPE, CLAIM_SUBMITTED_DATE, CLAIM_DELIVERY_DATE, CLAIM_VIOLATION_AMOUNT, CLAIM_RCV_NAME, CLAIM_RCV_ROLE, CLAIM_RCV_GIVEN_NAME, CLAIM_RCV_ADDRESS, CLAIM_CONTROL_AUDITOR, COMPLETION_ORDER, COMPLETION_AMOUNT, COMPLETION_STATE_AMOUNT, COMPLETION_LOCAL_AMOUNT, COMPLETION_ORG_AMOUNT, COMPLETION_OTHER_AMOUNT, REMOVED_LAW_AMOUNT, REMOVED_LAW_DATE, REMOVED_LAW_NO, REMOVED_INVALID_AMOUNT, REMOVED_INVALID_DATE, REMOVED_INVALID_NO, CLAIM_C2_AMOUNT, CLAIM_C2_NONEXPIRED, CLAIM_C2_EXPIRED, BENEFIT_FIN, BENEFIT_FIN_AMOUNT, BENEFIT_NONFIN, EXEC_TYPE, CREATED_DATE, ID, IS_ACTIVE, CREATED_BY, UPDATED_BY, UPDATED_DATE, AUDIT_ID FROM AUD_STAT.BM2_DATA WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("P_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM2Detail";

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
        public static DataResponse BM2Update(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM2");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM2_DATA SET AUDIT_ID = :P_AUDIT_ID, CLAIM_DATE = :P_CLAIM_DATE, CLAIM_NO = :P_CLAIM_NO, CLAIM_VIOLATION_DESC = :P_CLAIM_VIOLATION_DESC, CLAIM_VIOLATION_TYPE = :P_CLAIM_VIOLATION_TYPE, CLAIM_SUBMITTED_DATE = :P_CLAIM_SUBMITTED_DATE, CLAIM_DELIVERY_DATE = :P_CLAIM_DELIVERY_DATE, CLAIM_VIOLATION_AMOUNT = :P_CLAIM_VIOLATION_AMOUNT, CLAIM_RCV_NAME = :P_CLAIM_RCV_NAME, CLAIM_RCV_ROLE = :P_CLAIM_RCV_ROLE, CLAIM_RCV_GIVEN_NAME = :P_CLAIM_RCV_GIVEN_NAME, CLAIM_RCV_PHONE = :P_CLAIM_RCV_PHONE, CLAIM_RCV_ADDRESS = :P_CLAIM_RCV_ADDRESS, COMPLETION_DATE = :P_COMPLETION_DATE, COMPLETION_ORDER = :P_COMPLETION_ORDER, COMPLETION_AMOUNT = :P_COMPLETION_AMOUNT, COMPLETION_STATE_AMOUNT = :P_COMPLETION_STATE_AMOUNT, COMPLETION_LOCAL_AMOUNT = :P_COMPLETION_LOCAL_AMOUNT, COMPLETION_ORG_AMOUNT = :P_COMPLETION_ORG_AMOUNT, COMPLETION_OTHER_AMOUNT = :P_COMPLETION_OTHER_AMOUNT, REMOVED_LAW_AMOUNT = :P_REMOVED_LAW_AMOUNT, REMOVED_LAW_DATE = :P_REMOVED_LAW_DATE, REMOVED_LAW_NO = :P_REMOVED_LAW_NO, REMOVED_INVALID_AMOUNT = :P_REMOVED_INVALID_AMOUNT, REMOVED_INVALID_DATE = :P_REMOVED_INVALID_DATE, REMOVED_INVALID_NO = :P_REMOVED_INVALID_NO, CLAIM_C2_AMOUNT = :P_CLAIM_C2_AMOUNT, CLAIM_C2_NONEXPIRED = :P_CLAIM_C2_NONEXPIRED, CLAIM_C2_EXPIRED = :P_CLAIM_C2_EXPIRED, BENEFIT_FIN = :P_BENEFIT_FIN, BENEFIT_FIN_AMOUNT = :P_BENEFIT_FIN_AMOUNT, BENEFIT_NONFIN = :P_BENEFIT_NONFIN, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_ID")?.Value;
                cmd.Parameters.Add(":P_CLAIM_DATE", OracleDbType.Varchar2).Value = elem.Element("CLAIM_DATE")?.Value;
                cmd.Parameters.Add(":P_CLAIM_NO", OracleDbType.Varchar2).Value = elem.Element("CLAIM_NO")?.Value;
                cmd.Parameters.Add(":P_CLAIM_VIOLATION_DESC", OracleDbType.Varchar2).Value = elem.Element("CLAIM_VIOLATION_DESC")?.Value;
                cmd.Parameters.Add(":P_CLAIM_VIOLATION_TYPE", OracleDbType.Int32).Value = elem.Element("CLAIM_VIOLATION_TYPE")?.Value;
                cmd.Parameters.Add(":P_CLAIM_SUBMITTED_DATE", OracleDbType.Varchar2).Value = elem.Element("CLAIM_SUBMITTED_DATE")?.Value;
                cmd.Parameters.Add(":P_CLAIM_DELIVERY_DATE", OracleDbType.Varchar2).Value = elem.Element("CLAIM_DELIVERY_DATE")?.Value;
                cmd.Parameters.Add(":P_CLAIM_VIOLATION_AMOUNT", OracleDbType.Decimal).Value = elem.Element("CLAIM_VIOLATION_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_CLAIM_RCV_NAME", OracleDbType.Varchar2).Value = elem.Element("CLAIM_RCV_NAME")?.Value;
                cmd.Parameters.Add(":P_CLAIM_RCV_ROLE", OracleDbType.Varchar2).Value = elem.Element("CLAIM_RCV_ROLE")?.Value;
                cmd.Parameters.Add(":P_CLAIM_RCV_GIVEN_NAME", OracleDbType.Varchar2).Value = elem.Element("CLAIM_RCV_GIVEN_NAME")?.Value;
                cmd.Parameters.Add(":P_CLAIM_RCV_PHONE", OracleDbType.Varchar2).Value = elem.Element("CLAIM_RCV_PHONE")?.Value;
                cmd.Parameters.Add(":P_CLAIM_RCV_ADDRESS", OracleDbType.Varchar2).Value = elem.Element("CLAIM_RCV_ADDRESS")?.Value;
                cmd.Parameters.Add(":P_CLAIM_CONTROL_AUDITOR_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_COMPLETION_DATE", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_DATE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_ORDER", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_ORDER")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_STATE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_STATE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_LOCAL_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_LOCAL_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_ORG_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_ORG_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_OTHER_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_OTHER_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_LAW_AMOUNT", OracleDbType.Decimal).Value = elem.Element("REMOVED_LAW_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_LAW_DATE", OracleDbType.Varchar2).Value = elem.Element("REMOVED_LAW_DATE")?.Value;
                cmd.Parameters.Add(":P_REMOVED_LAW_NO", OracleDbType.Varchar2).Value = elem.Element("REMOVED_LAW_NO")?.Value;
                cmd.Parameters.Add(":P_REMOVED_INVALID_AMOUNT", OracleDbType.Decimal).Value = elem.Element("REMOVED_INVALID_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_INVALID_DATE", OracleDbType.Varchar2).Value = elem.Element("REMOVED_INVALID_DATE")?.Value;
                cmd.Parameters.Add(":P_REMOVED_INVALID_NO", OracleDbType.Varchar2).Value = elem.Element("REMOVED_INVALID_NO")?.Value;
                cmd.Parameters.Add(":P_CLAIM_C2_AMOUNT", OracleDbType.Decimal).Value = elem.Element("CLAIM_C2_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_CLAIM_C2_NONEXPIRED", OracleDbType.Decimal).Value = elem.Element("CLAIM_C2_NONEXPIRED")?.Value;
                cmd.Parameters.Add(":P_CLAIM_C2_EXPIRED", OracleDbType.Decimal).Value = elem.Element("CLAIM_C2_EXPIRED")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_FIN", OracleDbType.Int32).Value = elem.Element("BENEFIT_FIN")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_FIN_AMOUNT", OracleDbType.Decimal).Value = elem.Element("BENEFIT_FIN_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_NONFIN", OracleDbType.Int32).Value = elem.Element("BENEFIT_NONFIN")?.Value;
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = elem.Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM2Insert(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM2");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO AUD_STAT.BM2_DATA (AUDIT_ID, CLAIM_DATE, CLAIM_NO, CLAIM_VIOLATION_DESC, CLAIM_VIOLATION_TYPE, CLAIM_SUBMITTED_DATE, CLAIM_DELIVERY_DATE, CLAIM_VIOLATION_AMOUNT, CLAIM_RCV_NAME, CLAIM_RCV_ROLE, CLAIM_RCV_GIVEN_NAME, CLAIM_RCV_PHONE, CLAIM_RCV_ADDRESS, CLAIM_CONTROL_AUDITOR_ID, COMPLETION_DATE, COMPLETION_ORDER, COMPLETION_AMOUNT, COMPLETION_STATE_AMOUNT, COMPLETION_LOCAL_AMOUNT, COMPLETION_ORG_AMOUNT, COMPLETION_OTHER_AMOUNT, REMOVED_LAW_AMOUNT, REMOVED_LAW_DATE, REMOVED_LAW_NO, REMOVED_INVALID_AMOUNT, REMOVED_INVALID_DATE, REMOVED_INVALID_NO, CLAIM_C2_AMOUNT, CLAIM_C2_NONEXPIRED, CLAIM_C2_EXPIRED, BENEFIT_FIN, BENEFIT_FIN_AMOUNT, BENEFIT_NONFIN, IS_ACTIVE, CREATED_BY, CREATED_DATE) " +
                    "VALUES(:P_AUDIT_ID, :P_CLAIM_DATE, :P_CLAIM_NO, :P_CLAIM_VIOLATION_DESC, :P_CLAIM_VIOLATION_TYPE, :P_CLAIM_SUBMITTED_DATE, :P_CLAIM_DELIVERY_DATE, :P_CLAIM_VIOLATION_AMOUNT, :P_CLAIM_RCV_NAME, :P_CLAIM_RCV_ROLE, :P_CLAIM_RCV_GIVEN_NAME, :P_CLAIM_RCV_PHONE, :P_CLAIM_RCV_ADDRESS, :P_CLAIM_CONTROL_AUDITOR_ID, :P_COMPLETION_DATE, :P_COMPLETION_ORDER, :P_COMPLETION_AMOUNT, :P_COMPLETION_STATE_AMOUNT, :P_COMPLETION_LOCAL_AMOUNT, :P_COMPLETION_ORG_AMOUNT, :P_COMPLETION_OTHER_AMOUNT, :P_REMOVED_LAW_AMOUNT, :P_REMOVED_LAW_DATE, :P_REMOVED_LAW_NO, :P_REMOVED_INVALID_AMOUNT, :P_REMOVED_INVALID_DATE, :P_REMOVED_INVALID_NO, :P_CLAIM_C2_AMOUNT, :P_CLAIM_C2_NONEXPIRED, :P_CLAIM_C2_EXPIRED, :P_BENEFIT_FIN, :P_BENEFIT_FIN_AMOUNT, :P_BENEFIT_NONFIN, :P_IS_ACTIVE, :P_CREATED_BY, :P_CREATED_DATE)";

                // Set parameters
                cmd.Parameters.Add(":P_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_ID")?.Value;
                cmd.Parameters.Add(":P_CLAIM_DATE", OracleDbType.Varchar2).Value = elem.Element("CLAIM_DATE")?.Value;
                cmd.Parameters.Add(":P_CLAIM_NO", OracleDbType.Varchar2).Value = elem.Element("CLAIM_NO")?.Value;
                cmd.Parameters.Add(":P_CLAIM_VIOLATION_DESC", OracleDbType.Varchar2).Value = elem.Element("CLAIM_VIOLATION_DESC")?.Value;
                cmd.Parameters.Add(":P_CLAIM_VIOLATION_TYPE", OracleDbType.Int32).Value = elem.Element("CLAIM_VIOLATION_TYPE")?.Value;
                cmd.Parameters.Add(":P_CLAIM_SUBMITTED_DATE", OracleDbType.Varchar2).Value = elem.Element("CLAIM_SUBMITTED_DATE")?.Value;
                cmd.Parameters.Add(":P_CLAIM_DELIVERY_DATE", OracleDbType.Varchar2).Value = elem.Element("CLAIM_DELIVERY_DATE")?.Value;
                cmd.Parameters.Add(":P_CLAIM_VIOLATION_AMOUNT", OracleDbType.Decimal).Value = elem.Element("CLAIM_VIOLATION_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_CLAIM_RCV_NAME", OracleDbType.Varchar2).Value = elem.Element("CLAIM_RCV_NAME")?.Value;
                cmd.Parameters.Add(":P_CLAIM_RCV_ROLE", OracleDbType.Varchar2).Value = elem.Element("CLAIM_RCV_ROLE")?.Value;
                cmd.Parameters.Add(":P_CLAIM_RCV_GIVEN_NAME", OracleDbType.Varchar2).Value = elem.Element("CLAIM_RCV_GIVEN_NAME")?.Value;
                cmd.Parameters.Add(":P_CLAIM_RCV_PHONE", OracleDbType.Varchar2).Value = elem.Element("CLAIM_RCV_PHONE")?.Value;
                cmd.Parameters.Add(":P_CLAIM_RCV_ADDRESS", OracleDbType.Varchar2).Value = elem.Element("CLAIM_RCV_ADDRESS")?.Value;
                cmd.Parameters.Add(":P_CLAIM_CONTROL_AUDITOR_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_COMPLETION_DATE", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_DATE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_ORDER", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_ORDER")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_STATE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_STATE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_LOCAL_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_LOCAL_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_ORG_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_ORG_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_OTHER_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_OTHER_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_LAW_AMOUNT", OracleDbType.Decimal).Value = elem.Element("REMOVED_LAW_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_LAW_DATE", OracleDbType.Varchar2).Value = elem.Element("REMOVED_LAW_DATE")?.Value;
                cmd.Parameters.Add(":P_REMOVED_LAW_NO", OracleDbType.Varchar2).Value = elem.Element("REMOVED_LAW_NO")?.Value;
                cmd.Parameters.Add(":P_REMOVED_INVALID_AMOUNT", OracleDbType.Decimal).Value = elem.Element("REMOVED_INVALID_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REMOVED_INVALID_DATE", OracleDbType.Varchar2).Value = elem.Element("REMOVED_INVALID_DATE")?.Value;
                cmd.Parameters.Add(":P_REMOVED_INVALID_NO", OracleDbType.Varchar2).Value = elem.Element("REMOVED_INVALID_NO")?.Value;
                cmd.Parameters.Add(":P_CLAIM_C2_AMOUNT", OracleDbType.Decimal).Value = elem.Element("CLAIM_C2_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_CLAIM_C2_NONEXPIRED", OracleDbType.Decimal).Value = elem.Element("CLAIM_C2_NONEXPIRED")?.Value;
                cmd.Parameters.Add(":P_CLAIM_C2_EXPIRED", OracleDbType.Decimal).Value = elem.Element("CLAIM_C2_EXPIRED")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_FIN", OracleDbType.Int32).Value = elem.Element("BENEFIT_FIN")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_FIN_AMOUNT", OracleDbType.Decimal).Value = elem.Element("BENEFIT_FIN_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_NONFIN", OracleDbType.Int32).Value = elem.Element("BENEFIT_NONFIN")?.Value;
                cmd.Parameters.Add(":P_IS_ACTIVE", OracleDbType.Int32).Value = elem.Element("IS_ACTIVE")?.Value;
                cmd.Parameters.Add(":P_CREATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_CREATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM2Delete(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM2_DATA " +
                    "SET IS_ACTIVE = 0, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("UPDATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Устгалаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(BM.ID)" +
                    "FROM AUD_STAT.BM3_DATA BM " +
                    "INNER JOIN AUD_STAT.BM0_DATA B ON BM.AUDIT_ID = B.ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON B.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON B.DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON B.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON B.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON B.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON B.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_VIOLATION_TYPE RVT ON BM.REFERENCE_TYPE = RVT.VIOLATION_ID " +
                    "LEFT JOIN AUD_REG.SYSTEM_USER SU ON BM.REFERENCE_CONTROL_AUDITOR_ID = SU.USER_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND B.IS_ACTIVE = 1 AND (:V_USER_TYPE != 'Branch_Auditor' OR (:V_USER_TYPE = 'Branch_Auditor' " +
                    "AND B.AUDIT_DEPARTMENT_ID = :V_DEPARTMENT))  AND B.STATISTIC_PERIOD = :V_PERIOD " +
                    "AND(:V_SEARCH IS NULL OR UPPER(B.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.REFERENCE_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(B.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%'  " +
                    "OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.REFERENCE_DESC) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(SU.USER_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.COMPLETION_ORDER) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT B.STATISTIC_PERIOD, RP.PERIOD_LABEL, B.DEPARTMENT_ID, RD.DEPARTMENT_NAME, RAY.YEAR_LABEL, B.AUDIT_TYPE, RAT.AUDIT_TYPE_NAME, RTT.TOPIC_TYPE_NAME, B.TOPIC_CODE, B.TOPIC_NAME, B.ORDER_NO, B.ORDER_DATE, RBT.BUDGET_TYPE_NAME, BM.REFERENCE_DESC, BM.REFERENCE_TYPE, BM.REFERENCE_COUNT, BM.REFERENCE_AMOUNT, BM.REFERENCE_SUBMITTED_DATE, BM.REFERENCE_DELIVERY_DATE, BM.REFERENCE_RCV_NAME, BM.REFERENCE_RCV_ROLE, BM.REFERENCE_RCV_GIVEN_NAME, BM.REFERENCE_RCV_PHONE, BM.REFERENCE_RCV_ADDRESS, SU.USER_CODE||' - '||SU.USER_NAME REFERENCE_CONTROL_AUDITOR, BM.COMPLETION_DATE, BM.COMPLETION_ORDER, BM.COMPLETION_DONE, BM.COMPLETION_DONE_AMOUNT, BM.COMPLETION_PROGRESS, BM.COMPLETION_PROGRESS_AMOUNT, BM.C2_NONEXPIRED, BM.C2_NONEXPIRED_AMOUNT, BM.C2_EXPIRED, BM.C2_EXPIRED_AMOUNT, BM.BENEFIT_FIN, BM.BENEFIT_FIN_AMOUNT, BM.BENEFIT_NONFIN " +
                    "FROM AUD_STAT.BM3_DATA BM " +
                    "INNER JOIN AUD_STAT.BM0_DATA B ON BM.AUDIT_ID = B.ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON B.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON B.DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON B.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON B.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON B.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON B.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_VIOLATION_TYPE RVT ON BM.REFERENCE_TYPE = RVT.VIOLATION_ID " +
                    "LEFT JOIN AUD_REG.SYSTEM_USER SU ON BM.REFERENCE_CONTROL_AUDITOR_ID = SU.USER_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND B.IS_ACTIVE = 1 AND (:V_USER_TYPE != 'Branch_Auditor' OR (:V_USER_TYPE = 'Branch_Auditor' " +
                    "AND B.AUDIT_DEPARTMENT_ID = :V_DEPARTMENT))  AND B.STATISTIC_PERIOD = :V_PERIOD " +
                    "AND(:V_SEARCH IS NULL OR UPPER(B.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.REFERENCE_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(B.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%'  " +
                    "OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.REFERENCE_DESC) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(SU.USER_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.COMPLETION_ORDER) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN BM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_DEPARTMENT_ID' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_DEPARTMENT_ID END ASC," +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_DEPARTMENT_ID' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_DEPARTMENT_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND :ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND :ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_TYPE END ASC,  " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_TYPE END DESC,  " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND :ORDER_DIR = 'ASC' THEN B.TOPIC_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND :ORDER_DIR = 'DESC' THEN B.TOPIC_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND :ORDER_DIR = 'ASC' THEN B.TOPIC_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND :ORDER_DIR = 'DESC' THEN B.TOPIC_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_BUDGET_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_BUDGET_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND :ORDER_DIR = 'ASC' THEN B.ORDER_NO END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND :ORDER_DIR = 'DESC' THEN B.ORDER_NO END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'REFERENCE_DESC' AND :ORDER_DIR = 'ASC' THEN BM.REFERENCE_DESC END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'REFERENCE_DESC' AND :ORDER_DIR = 'DESC' THEN BM.REFERENCE_DESC END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'USER_NAME' AND :ORDER_DIR = 'ASC' THEN SU.USER_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'USER_NAME' AND :ORDER_DIR = 'DESC' THEN SU.USER_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'COMPLETION_ORDER' AND :ORDER_DIR = 'ASC' THEN BM.COMPLETION_ORDER END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'COMPLETION_ORDER' AND :ORDER_DIR = 'DESC' THEN BM.COMPLETION_ORDER END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'REFERENCE_TYPE' AND :ORDER_DIR = 'ASC' THEN BM.REFERENCE_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'REFERENCE_TYPE' AND :ORDER_DIR = 'DESC' THEN BM.REFERENCE_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM3";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM3Detail(XElement request)
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
                cmd.CommandText = "SELECT OFFICE_ID, STATISTIC_PERIOD, AUDIT_YEAR, AUDIT_TYPE, AUDIT_CODE, AUDIT_NAME, AUDIT_BUDGET_TYPE, ORDER_DATE, ORDER_NO, REFERENCE_DESC, REFERENCE_AMOUNT, REFERENCE_SUBMITTED_DATE, REFERENCE_DELIVERY_DATE, REFERENCE_RCV_NAME, REFERENCE_RCV_ROLE, REFERENCE_RCV_GIVEN_NAME, REFERENCE_RCV_ADDRESS, REFERENCE_CONTROL_AUDITOR, COMPLETION_ORDER, COMPLETION_DONE, COMPLETION_DONE_AMOUNT, COMPLETION_PROGRESS, COMPLETION_PROGRESS_AMOUNT, C2_NONEXPIRED, C2_NONEXPIRED_AMOUNT, C2_EXPIRED, C2_EXPIRED_AMOUNT, BENEFIT_FIN, BENEFIT_FIN_AMOUNT, BENEFIT_NONFIN, WORKING_PERSON, WORKING_DAY, WORKING_ADDITION_TIME, EXEC_TYPE, CREATED_DATE, REFERENCE_TYPE, REFERENCE_COUNT, ID, IS_ACTIVE, CREATED_BY, UPDATED_BY, UPDATED_DATE, AUDIT_ID FROM AUD_STAT.BM3_DATA WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("P_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM3Detail";

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
        public static DataResponse BM3Update(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM3");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM3_DATA SET AUDIT_ID = :P_AUDIT_ID, REFERENCE_DESC = :P_REFERENCE_DESC, REFERENCE_TYPE = :P_REFERENCE_TYPE, REFERENCE_COUNT = :P_REFERENCE_COUNT, REFERENCE_AMOUNT = :P_REFERENCE_AMOUNT, REFERENCE_SUBMITTED_DATE = :P_REFERENCE_SUBMITTED_DATE, REFERENCE_DELIVERY_DATE = :P_REFERENCE_DELIVERY_DATE, REFERENCE_RCV_NAME = :P_REFERENCE_RCV_NAME, REFERENCE_RCV_ROLE = :P_REFERENCE_RCV_ROLE, REFERENCE_RCV_GIVEN_NAME = :P_REFERENCE_RCV_GIVEN_NAME, REFERENCE_RCV_PHONE = :P_REFERENCE_RCV_PHONE, REFERENCE_RCV_ADDRESS = :P_REFERENCE_RCV_ADDRESS, COMPLETION_DATE = :P_COMPLETION_DATE, COMPLETION_ORDER = :P_COMPLETION_ORDER, COMPLETION_DONE = :P_COMPLETION_DONE, COMPLETION_DONE_AMOUNT = :P_COMPLETION_DONE_AMOUNT, COMPLETION_PROGRESS = :P_COMPLETION_PROGRESS, COMPLETION_PROGRESS_AMOUNT = :P_COMPLETION_PROGRESS_AMOUNT, C2_NONEXPIRED = :P_C2_NONEXPIRED, C2_NONEXPIRED_AMOUNT = :P_C2_NONEXPIRED_AMOUNT, C2_EXPIRED = :P_C2_EXPIRED, C2_EXPIRED_AMOUNT = :P_C2_EXPIRED_AMOUNT, BENEFIT_FIN = :P_BENEFIT_FIN, BENEFIT_FIN_AMOUNT = :P_BENEFIT_FIN_AMOUNT, BENEFIT_NONFIN = :P_BENEFIT_NONFIN, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_ID")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_DESC", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_DESC")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_TYPE", OracleDbType.Int32).Value = elem.Element("REFERENCE_TYPE")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_COUNT", OracleDbType.Int32).Value = elem.Element("REFERENCE_COUNT")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("REFERENCE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_SUBMITTED_DATE", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_SUBMITTED_DATE")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_DELIVERY_DATE", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_DELIVERY_DATE")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_RCV_NAME", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_RCV_NAME")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_RCV_ROLE", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_RCV_ROLE")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_RCV_GIVEN_NAME", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_RCV_GIVEN_NAME")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_RCV_PHONE", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_RCV_PHONE")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_RCV_ADDRESS", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_RCV_ADDRESS")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_CONTROL_AUDITOR_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_COMPLETION_DATE", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_DATE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_ORDER", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_ORDER")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_DONE", OracleDbType.Int32).Value = elem.Element("COMPLETION_DONE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_DONE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_DONE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_PROGRESS", OracleDbType.Int32).Value = elem.Element("COMPLETION_PROGRESS")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_PROGRESS_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_PROGRESS_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_C2_NONEXPIRED", OracleDbType.Int32).Value = elem.Element("C2_NONEXPIRED")?.Value;
                cmd.Parameters.Add(":P_C2_NONEXPIRED_AMOUNT", OracleDbType.Decimal).Value = elem.Element("C2_NONEXPIRED_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_C2_EXPIRED", OracleDbType.Int32).Value = elem.Element("C2_EXPIRED")?.Value;
                cmd.Parameters.Add(":P_C2_EXPIRED_AMOUNT", OracleDbType.Decimal).Value = elem.Element("C2_EXPIRED_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_FIN", OracleDbType.Int32).Value = elem.Element("BENEFIT_FIN")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_FIN_AMOUNT", OracleDbType.Decimal).Value = elem.Element("BENEFIT_FIN_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_NONFIN", OracleDbType.Int32).Value = elem.Element("BENEFIT_NONFIN")?.Value;
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = elem.Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM3Insert(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM3");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO AUD_STAT.BM3_DATA (AUDIT_ID, REFERENCE_DESC, REFERENCE_TYPE, REFERENCE_COUNT, REFERENCE_AMOUNT, REFERENCE_SUBMITTED_DATE, REFERENCE_DELIVERY_DATE, REFERENCE_RCV_NAME, REFERENCE_RCV_ROLE, REFERENCE_RCV_GIVEN_NAME, REFERENCE_RCV_PHONE, REFERENCE_RCV_ADDRESS, REFERENCE_CONTROL_AUDITOR_ID, COMPLETION_DATE, COMPLETION_ORDER, COMPLETION_DONE, COMPLETION_DONE_AMOUNT, COMPLETION_PROGRESS, COMPLETION_PROGRESS_AMOUNT, C2_NONEXPIRED, C2_NONEXPIRED_AMOUNT, C2_EXPIRED, C2_EXPIRED_AMOUNT, BENEFIT_FIN, BENEFIT_FIN_AMOUNT, BENEFIT_NONFIN, IS_ACTIVE, CREATED_BY, CREATED_DATE) " +
                    "VALUES(:P_AUDIT_ID, :P_REFERENCE_DESC, :P_REFERENCE_TYPE, :P_REFERENCE_COUNT, :P_REFERENCE_AMOUNT, :P_REFERENCE_SUBMITTED_DATE, :P_REFERENCE_DELIVERY_DATE, :P_REFERENCE_RCV_NAME, :P_REFERENCE_RCV_ROLE, :P_REFERENCE_RCV_GIVEN_NAME, :P_REFERENCE_RCV_PHONE, :P_REFERENCE_RCV_ADDRESS, :P_REFERENCE_CONTROL_AUDITOR_ID, :P_COMPLETION_DATE, :P_COMPLETION_ORDER, :P_COMPLETION_DONE, :P_COMPLETION_DONE_AMOUNT, :P_COMPLETION_PROGRESS, :P_COMPLETION_PROGRESS_AMOUNT, :P_C2_NONEXPIRED, :P_C2_NONEXPIRED_AMOUNT, :P_C2_EXPIRED, :P_C2_EXPIRED_AMOUNT, :P_BENEFIT_FIN, :P_BENEFIT_FIN_AMOUNT, :P_BENEFIT_NONFIN, :P_IS_ACTIVE, :P_CREATED_BY, :P_CREATED_DATE)";

                // Set parameters
                cmd.Parameters.Add(":P_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_ID")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_DESC", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_DESC")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_TYPE", OracleDbType.Int32).Value = elem.Element("REFERENCE_TYPE")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_COUNT", OracleDbType.Int32).Value = elem.Element("REFERENCE_COUNT")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("REFERENCE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_SUBMITTED_DATE", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_SUBMITTED_DATE")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_DELIVERY_DATE", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_DELIVERY_DATE")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_RCV_NAME", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_RCV_NAME")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_RCV_ROLE", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_RCV_ROLE")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_RCV_GIVEN_NAME", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_RCV_GIVEN_NAME")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_RCV_PHONE", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_RCV_PHONE")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_RCV_ADDRESS", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_RCV_ADDRESS")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_CONTROL_AUDITOR_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_COMPLETION_DATE", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_DATE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_ORDER", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_ORDER")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_DONE", OracleDbType.Int32).Value = elem.Element("COMPLETION_DONE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_DONE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_DONE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_PROGRESS", OracleDbType.Int32).Value = elem.Element("COMPLETION_PROGRESS")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_PROGRESS_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_PROGRESS_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_C2_NONEXPIRED", OracleDbType.Int32).Value = elem.Element("C2_NONEXPIRED")?.Value;
                cmd.Parameters.Add(":P_C2_NONEXPIRED_AMOUNT", OracleDbType.Decimal).Value = elem.Element("C2_NONEXPIRED_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_C2_EXPIRED", OracleDbType.Int32).Value = elem.Element("C2_EXPIRED")?.Value;
                cmd.Parameters.Add(":P_C2_EXPIRED_AMOUNT", OracleDbType.Decimal).Value = elem.Element("C2_EXPIRED_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_FIN", OracleDbType.Int32).Value = elem.Element("BENEFIT_FIN")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_FIN_AMOUNT", OracleDbType.Decimal).Value = elem.Element("BENEFIT_FIN_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_BENEFIT_NONFIN", OracleDbType.Int32).Value = elem.Element("BENEFIT_NONFIN")?.Value;
                cmd.Parameters.Add(":P_IS_ACTIVE", OracleDbType.Int32).Value = elem.Element("IS_ACTIVE")?.Value;
                cmd.Parameters.Add(":P_CREATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_CREATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM3Delete(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM3_DATA " +
                    "SET IS_ACTIVE = 0, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("UPDATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Устгалаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(BM.ID)" +
                    "FROM AUD_STAT.BM4_DATA BM " +
                    "INNER JOIN AUD_STAT.BM0_DATA B ON BM.AUDIT_ID = B.ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON B.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON B.DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON B.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON B.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON B.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON B.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_VIOLATION_TYPE RVT ON BM.PRO_VIOLATION_TYPE = RVT.VIOLATION_ID " +
                    "LEFT JOIN AUD_REG.SYSTEM_USER SU ON BM.PRO_CONTROL_AUDITOR_ID = SU.USER_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND B.IS_ACTIVE = 1 " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR (:V_USER_TYPE = 'Branch_Auditor' AND B.AUDIT_DEPARTMENT_ID = :V_DEPARTMENT)) " +
                    "AND B.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(B.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%'  " +
                    "OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.PRO_VIOLATION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(B.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%'  OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.PRO_VIOLATION_DESC) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(SU.USER_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.COMPLETION_ORDER) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT B.STATISTIC_PERIOD, RP.PERIOD_LABEL, B.DEPARTMENT_ID, RD.DEPARTMENT_NAME, RAY.YEAR_LABEL, B.AUDIT_TYPE, RAT.AUDIT_TYPE_NAME, RTT.TOPIC_TYPE_NAME, B.TOPIC_CODE, B.TOPIC_NAME, B.ORDER_NO, B.ORDER_DATE, RBT.BUDGET_TYPE_NAME, BM.PROPOSAL_DATE, BM.PROPOSAL_NO, BM.PRO_VIOLATION_DESC, BM.PRO_VIOLATION_TYPE, BM.VIOLATION_RESPONDENT, BM.PRO_SUBMITTED_DATE, BM.PROPOSAL_DELIVERY_DATE, BM.PROPOSAL_VIOLATION_COUNT, BM.PROPOSAL_AMOUNT, BM.PROPOSAL_RCV_NAME, BM.PROPOSAL_RCV_ROLE, BM.PROPOSAL_RCV_GIVEN_NAME, BM.PROPOSAL_RCV_PHONE, BM.PRO_RCV_ADDRESS, SU.USER_CODE||' - '||SU.USER_NAME PROPOSAL_CONTROL_AUDITOR, BM.COMPLETION_DATE, BM.COMPLETION_ORDER, BM.COMPLETION_DONE, BM.COMPLETION_DONE_AMOUNT, BM.COMPLETION_PROGRESS, BM.COMPLETION_PROGRESS_AMOUNT " +
                    "FROM AUD_STAT.BM4_DATA BM " +
                    "INNER JOIN AUD_STAT.BM0_DATA B ON BM.AUDIT_ID = B.ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON B.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON B.DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON B.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON B.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON B.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON B.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_VIOLATION_TYPE RVT ON BM.PRO_VIOLATION_TYPE = RVT.VIOLATION_ID " +
                    "LEFT JOIN AUD_REG.SYSTEM_USER SU ON BM.PRO_CONTROL_AUDITOR_ID = SU.USER_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND B.IS_ACTIVE = 1 " +
                    "AND (:V_USER_TYPE != 'BRANCH_AUDITOR' OR (:V_USER_TYPE = 'BRANCH_AUDITOR' AND B.AUDIT_DEPARTMENT_ID = :V_DEPARTMENT)) " +
                    "AND B.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(B.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%'  " +
                    "OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.PRO_VIOLATION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(B.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%'  OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.PRO_VIOLATION_DESC) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(SU.USER_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.COMPLETION_ORDER) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN BM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_DEPARTMENT_ID' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_DEPARTMENT_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_DEPARTMENT_ID' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_DEPARTMENT_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND :ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND :ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND :ORDER_DIR = 'ASC' THEN B.TOPIC_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND :ORDER_DIR = 'DESC' THEN B.TOPIC_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND :ORDER_DIR = 'ASC' THEN B.TOPIC_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND :ORDER_DIR = 'DESC' THEN B.TOPIC_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_BUDGET_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_BUDGET_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND :ORDER_DIR = 'ASC' THEN B.ORDER_NO END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND :ORDER_DIR = 'DESC' THEN B.ORDER_NO END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PRO_VIOLATION_DESC' AND :ORDER_DIR = 'ASC' THEN BM.PRO_VIOLATION_DESC END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PRO_VIOLATION_DESC' AND :ORDER_DIR = 'DESC' THEN BM.PRO_VIOLATION_DESC END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'USER_NAME' AND :ORDER_DIR = 'ASC' THEN SU.USER_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'USER_NAME' AND :ORDER_DIR = 'DESC' THEN SU.USER_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'COMPLETION_ORDER' AND :ORDER_DIR = 'ASC' THEN BM.COMPLETION_ORDER END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'COMPLETION_ORDER' AND :ORDER_DIR = 'DESC' THEN BM.COMPLETION_ORDER END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PROPOSAL_VIOLATION_TYPE' AND :ORDER_DIR = 'ASC' THEN BM.PRO_VIOLATION_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PROPOSAL_VIOLATION_TYPE' AND :ORDER_DIR = 'DESC' THEN BM.PRO_VIOLATION_TYPE END DESC " + 
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM4";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM4Detail(XElement request)
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
                cmd.CommandText = "SELECT OFFICE_ID, STATISTIC_PERIOD, AUDIT_YEAR, AUDIT_TYPE, AUDIT_CODE, AUDIT_NAME, AUDIT_BUDGET_TYPE, ORDER_DATE, ORDER_NO, PROPOSAL_NO, PROPOSAL_VIOLATION_DESC, VIOLATION_RESPONDENT, PROPOSAL_SUBMITTED_DATE, PROPOSAL_DELIVERY_DATE, PROPOSAL_AMOUNT, PROPOSAL_RCV_NAME, PROPOSAL_RCV_ROLE, PROPOSAL_RCV_GIVEN_NAME, PROPOSAL_RCV_ADDRESS, PROPOSAL_CONTROL_AUDITOR, COMPLETION_ORDER, COMPLETION_DONE, COMPLETION_DONE_AMOUNT, COMPLETION_PROGRESS, COMPLETION_PROGRESS_AMOUNT, EXEC_TYPE, CREATED_DATE, PROPOSAL_VIOLATION_TYPE, PROPOSAL_COUNT, ID, IS_ACTIVE, CREATED_BY, UPDATED_BY, UPDATED_DATE, AUDIT_ID FROM AUD_STAT.BM4_DATA WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("P_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM4Detail";

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
        public static DataResponse BM4Update(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM4");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM4_DATA SET AUDIT_ID = :P_AUDIT_ID, PROPOSAL_DATE = :P_PROPOSAL_DATE, PROPOSAL_NO = :P_PROPOSAL_NO, PRO_VIOLATION_DESC = :P_PROPOSAL_VIOLATION_DESC, PRO_VIOLATION_TYPE = :P_PROPOSAL_VIOLATION_TYPE, VIOLATION_RESPONDENT = :P_VIOLATION_RESPONDENT, PRO_SUBMITTED_DATE = :P_PROPOSAL_SUBMITTED_DATE, PROPOSAL_DELIVERY_DATE = :P_PROPOSAL_DELIVERY_DATE, PROPOSAL_VIOLATION_COUNT = :P_PROPOSAL_VIOLATION_COUNT, PROPOSAL_AMOUNT = :P_PROPOSAL_AMOUNT, PROPOSAL_RCV_NAME = :P_PROPOSAL_RCV_NAME, PROPOSAL_RCV_ROLE = :P_PROPOSAL_RCV_ROLE, PROPOSAL_RCV_GIVEN_NAME = :P_PROPOSAL_RCV_GIVEN_NAME, PROPOSAL_RCV_PHONE = :P_PROPOSAL_RCV_PHONE, PRO_RCV_ADDRESS = :P_PROPOSAL_RCV_ADDRESS, COMPLETION_DATE = :P_COMPLETION_DATE, COMPLETION_ORDER = :P_COMPLETION_ORDER, COMPLETION_DONE = :P_COMPLETION_DONE, COMPLETION_DONE_AMOUNT = :P_COMPLETION_DONE_AMOUNT, COMPLETION_PROGRESS = :P_COMPLETION_PROGRESS, COMPLETION_PROGRESS_AMOUNT = :P_COMPLETION_PROGRESS_AMOUNT,  UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_ID")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_DATE", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_DATE")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_NO", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_NO")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_VIOLATION_DESC", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_VIOLATION_DESC")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_VIOLATION_TYPE", OracleDbType.Int32).Value = elem.Element("PROPOSAL_VIOLATION_TYPE")?.Value;
                cmd.Parameters.Add(":P_VIOLATION_RESPONDENT", OracleDbType.Varchar2).Value = elem.Element("VIOLATION_RESPONDENT")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_SUBMITTED_DATE", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_SUBMITTED_DATE")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_DELIVERY_DATE", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_DELIVERY_DATE")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_VIOLATION_COUNT", OracleDbType.Int32).Value = elem.Element("PROPOSAL_VIOLATION_COUNT")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_AMOUNT", OracleDbType.Decimal).Value = elem.Element("PROPOSAL_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_RCV_NAME", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_RCV_NAME")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_RCV_ROLE", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_RCV_ROLE")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_RCV_GIVEN_NAME", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_RCV_GIVEN_NAME")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_RCV_PHONE", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_RCV_PHONE")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_RCV_ADDRESS", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_RCV_ADDRESS")?.Value;
                //cmd.Parameters.Add(":P_PROPOSAL_CONTROL_AUDITOR_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_COMPLETION_DATE", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_DATE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_ORDER", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_ORDER")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_DONE", OracleDbType.Int32).Value = elem.Element("COMPLETION_DONE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_DONE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_DONE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_PROGRESS", OracleDbType.Int32).Value = elem.Element("COMPLETION_PROGRESS")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_PROGRESS_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_PROGRESS_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = elem.Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM4Insert(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM4");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO AUD_STAT.BM4_DATA (AUDIT_ID, PROPOSAL_DATE, PROPOSAL_NO, PRO_VIOLATION_DESC, PRO_VIOLATION_TYPE, VIOLATION_RESPONDENT, PRO_SUBMITTED_DATE, PROPOSAL_DELIVERY_DATE, PROPOSAL_VIOLATION_COUNT, PROPOSAL_AMOUNT, PROPOSAL_RCV_NAME, PROPOSAL_RCV_ROLE, PROPOSAL_RCV_GIVEN_NAME, PROPOSAL_RCV_PHONE, PRO_RCV_ADDRESS, PRO_CONTROL_AUDITOR_ID, COMPLETION_DATE, COMPLETION_ORDER, COMPLETION_DONE, COMPLETION_DONE_AMOUNT, COMPLETION_PROGRESS, COMPLETION_PROGRESS_AMOUNT, IS_ACTIVE, CREATED_BY, CREATED_DATE) " +
                    "VALUES(:P_AUDIT_ID, :P_PROPOSAL_DATE, :P_PROPOSAL_NO, :P_PROPOSAL_VIOLATION_DESC, :P_PROPOSAL_VIOLATION_TYPE, :P_VIOLATION_RESPONDENT, :P_PROPOSAL_SUBMITTED_DATE, :P_PROPOSAL_DELIVERY_DATE, :P_PROPOSAL_VIOLATION_COUNT, :P_PROPOSAL_AMOUNT, :P_PROPOSAL_RCV_NAME, :P_PROPOSAL_RCV_ROLE, :P_PROPOSAL_RCV_GIVEN_NAME, :P_PROPOSAL_RCV_PHONE, :P_PROPOSAL_RCV_ADDRESS, :P_PROPOSAL_CONTROL_AUDITOR_ID, :P_COMPLETION_DATE, :P_COMPLETION_ORDER, :P_COMPLETION_DONE, :P_COMPLETION_DONE_AMOUNT, COMPLETION_PROGRESS, :P_COMPLETION_PROGRESS_AMOUNT, :P_IS_ACTIVE, :P_CREATED_BY, :P_CREATED_DATE)";

                // Set parameters
                cmd.Parameters.Add(":P_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_ID")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_DATE", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_DATE")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_NO", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_NO")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_VIOLATION_DESC", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_VIOLATION_DESC")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_VIOLATION_TYPE", OracleDbType.Int32).Value = elem.Element("PROPOSAL_VIOLATION_TYPE")?.Value;
                cmd.Parameters.Add(":P_VIOLATION_RESPONDENT", OracleDbType.Varchar2).Value = elem.Element("VIOLATION_RESPONDENT")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_SUBMITTED_DATE", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_SUBMITTED_DATE")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_DELIVERY_DATE", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_DELIVERY_DATE")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_VIOLATION_COUNT", OracleDbType.Int32).Value = elem.Element("PROPOSAL_VIOLATION_COUNT")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_AMOUNT", OracleDbType.Decimal).Value = elem.Element("PROPOSAL_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_RCV_NAME", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_RCV_NAME")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_RCV_ROLE", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_RCV_ROLE")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_RCV_GIVEN_NAME", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_RCV_GIVEN_NAME")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_RCV_PHONE", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_RCV_PHONE")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_RCV_ADDRESS", OracleDbType.Varchar2).Value = elem.Element("PROPOSAL_RCV_ADDRESS")?.Value;
                cmd.Parameters.Add(":P_PROPOSAL_CONTROL_AUDITOR_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_COMPLETION_DATE", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_DATE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_ORDER", OracleDbType.Varchar2).Value = elem.Element("COMPLETION_ORDER")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_DONE", OracleDbType.Int32).Value = elem.Element("COMPLETION_DONE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_DONE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_DONE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_PROGRESS", OracleDbType.Int32).Value = elem.Element("COMPLETION_PROGRESS")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_PROGRESS_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_PROGRESS_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_IS_ACTIVE", OracleDbType.Int32).Value = elem.Element("IS_ACTIVE")?.Value;
                cmd.Parameters.Add(":P_CREATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_CREATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM4Delete(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM4_DATA " +
                    "SET IS_ACTIVE = 0, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("UPDATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Устгалаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(BM.ID) " +
                    "FROM AUD_STAT.BM5_DATA BM " +
                    "INNER JOIN AUD_STAT.BM0_DATA B ON BM.AUDIT_ID = B.ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON B.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON B.DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON B.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON B.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON B.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON B.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_VIOLATION_TYPE RVT ON BM.LAW_VIOLATION_TYPE = RVT.VIOLATION_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND B.IS_ACTIVE = 1 " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR (:V_USER_TYPE = 'Branch_Auditor' AND B.AUDIT_DEPARTMENT_ID = :V_DEPARTMENT)) " +
                    "AND B.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(B.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.LAW_VIOLATION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(B.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.LAW_VIOLATION_DESC) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT B.STATISTIC_PERIOD, RP.PERIOD_LABEL, B.DEPARTMENT_ID, RD.DEPARTMENT_NAME, RAY.YEAR_LABEL, B.AUDIT_TYPE, RAT.AUDIT_TYPE_NAME, RTT.TOPIC_TYPE_NAME, B.TOPIC_CODE, B.TOPIC_NAME, B.ORDER_NO, B.ORDER_DATE, RBT.BUDGET_TYPE_NAME, BM.LAW_RESPONDANT_NAME, BM.LAW_VIOLATION_DESC, BM.LAW_VIOLATION_TYPE, BM.LAW_MOVING_INFORMATION, BM.LAW_NUMBER, BM.LAW_AMOUNT, BM.COMPLETION_DONE, BM.COMPLETION_DONE_AMOUNT, BM.COMPLETION_PROGRESS, BM.COMPLETION_PROGRESS_AMOUNT, BM.COMPLETION_INVALID, BM.COMPLETION_INVALID_AMOUNT, BM.LAW_C2_NUMBER, BM.LAW_C2_AMOUNT, BM.IS_ACTIVE, BM.CREATED_BY, BM.CREATED_DATE, BM.UPDATED_BY, BM.UPDATED_DATE " +
                    "FROM AUD_STAT.BM5_DATA BM " +
                    "INNER JOIN AUD_STAT.BM0_DATA B ON BM.AUDIT_ID = B.ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON B.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON B.DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON B.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON B.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON B.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON B.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_VIOLATION_TYPE RVT ON BM.LAW_VIOLATION_TYPE = RVT.VIOLATION_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND B.IS_ACTIVE = 1 " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR (:V_USER_TYPE = 'Branch_Auditor' AND B.AUDIT_DEPARTMENT_ID = :V_DEPARTMENT)) " +
                    "AND B.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(B.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.LAW_VIOLATION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(B.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(B.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.LAW_VIOLATION_DESC) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN BM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_DEPARTMENT_ID' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_DEPARTMENT_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_DEPARTMENT_ID' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_DEPARTMENT_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND :ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND :ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND :ORDER_DIR = 'ASC' THEN B.TOPIC_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND :ORDER_DIR = 'DESC' THEN B.TOPIC_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND :ORDER_DIR = 'ASC' THEN B.TOPIC_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND :ORDER_DIR = 'DESC' THEN B.TOPIC_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND :ORDER_DIR = 'ASC' THEN B.AUDIT_BUDGET_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND :ORDER_DIR = 'DESC' THEN B.AUDIT_BUDGET_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND :ORDER_DIR = 'ASC' THEN B.ORDER_NO END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND :ORDER_DIR = 'DESC' THEN B.ORDER_NO END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'LAW_VIOLATION_DESC' AND :ORDER_DIR = 'ASC' THEN BM.LAW_VIOLATION_DESC END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'LAW_VIOLATION_DESC' AND :ORDER_DIR = 'DESC' THEN BM.LAW_VIOLATION_DESC END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'LAW_VIOLATION_TYPE ' AND :ORDER_DIR = 'ASC' THEN BM.LAW_VIOLATION_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'LAW_VIOLATION_TYPE ' AND :ORDER_DIR = 'DESC' THEN BM.LAW_VIOLATION_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM5";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM5Detail(XElement request)
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
                cmd.CommandText = "SELECT OFFICE_ID, STATISTIC_PERIOD, AUDIT_YEAR, AUDIT_TYPE, AUDIT_CODE, AUDIT_NAME, AUDIT_BUDGET_TYPE, ORDER_DATE, ORDER_NO, LAW_RESPONDANT_NAME, LAW_VIOLATION_DESC, LAW_VIOLATION_TYPE, LAW_MOVING_INFORMATION, LAW_NUMBER, LAW_AMOUNT, COMPLETION_DONE, COMPLETION_DONE_AMOUNT, COMPLETION_PROGRESS, COMPLETION_PROGRESS_AMOUNT, COMPLETION_INVALID, COMPLETION_INVALID_AMOUNT, LAW_C2_NUMBER, LAW_C2_AMOUNT, EXEC_TYPE, CREATED_DATE, ID, IS_ACTIVE, CREATED_BY, UPDATED_BY, UPDATED_DATE, AUDIT_ID FROM AUD_STAT.BM5_DATA WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("P_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM5Detail";

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
        public static DataResponse BM5Update(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM5");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM5_DATA SET AUDIT_ID = :P_AUDIT_ID, LAW_RESPONDANT_NAME = :P_LAW_RESPONDANT_NAME, LAW_VIOLATION_DESC = :P_LAW_VIOLATION_DESC, LAW_VIOLATION_TYPE = :P_LAW_VIOLATION_TYPE, LAW_MOVING_INFORMATION = :P_LAW_MOVING_INFORMATION, LAW_NUMBER = :P_LAW_NUMBER, LAW_AMOUNT = :P_LAW_AMOUNT, COMPLETION_DONE = :P_COMPLETION_DONE, COMPLETION_DONE_AMOUNT = :P_COMPLETION_DONE_AMOUNT, COMPLETION_PROGRESS = :P_COMPLETION_PROGRESS, COMPLETION_PROGRESS_AMOUNT = :P_COMPLETION_PROGRESS_AMOUNT, COMPLETION_INVALID = :P_COMPLETION_INVALID, COMPLETION_INVALID_AMOUNT = :P_COMPLETION_INVALID_AMOUNT, LAW_C2_NUMBER = :P_LAW_C2_NUMBER, LAW_C2_AMOUNT = :P_LAW_C2_AMOUNT, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_ID")?.Value;
                cmd.Parameters.Add(":P_LAW_RESPONDANT_NAME", OracleDbType.Varchar2).Value = elem.Element("LAW_RESPONDANT_NAME")?.Value;
                cmd.Parameters.Add(":P_LAW_VIOLATION_DESC", OracleDbType.Varchar2).Value = elem.Element("LAW_VIOLATION_DESC")?.Value;
                cmd.Parameters.Add(":P_LAW_VIOLATION_TYPE", OracleDbType.Int32).Value = elem.Element("LAW_VIOLATION_TYPE")?.Value;
                cmd.Parameters.Add(":P_LAW_MOVING_INFORMATION", OracleDbType.Varchar2).Value = elem.Element("LAW_MOVING_INFORMATION")?.Value;
                cmd.Parameters.Add(":P_LAW_NUMBER", OracleDbType.Int32).Value = elem.Element("LAW_NUMBER")?.Value;
                cmd.Parameters.Add(":P_LAW_AMOUNT", OracleDbType.Decimal).Value = elem.Element("LAW_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_DONE", OracleDbType.Int32).Value = elem.Element("COMPLETION_DONE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_DONE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_DONE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_PROGRESS", OracleDbType.Int32).Value = elem.Element("COMPLETION_PROGRESS")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_PROGRESS_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_PROGRESS_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_INVALID", OracleDbType.Int32).Value = elem.Element("COMPLETION_INVALID")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_INVALID_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_INVALID_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_LAW_C2_NUMBER", OracleDbType.Int32).Value = elem.Element("LAW_C2_NUMBER")?.Value;
                cmd.Parameters.Add(":P_LAW_C2_AMOUNT", OracleDbType.Decimal).Value = elem.Element("LAW_C2_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = elem.Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM5Insert(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM5");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO BM5_DATA (AUDIT_ID, LAW_RESPONDANT_NAME, LAW_VIOLATION_DESC, LAW_VIOLATION_TYPE, LAW_MOVING_INFORMATION, LAW_NUMBER, LAW_AMOUNT, COMPLETION_DONE, COMPLETION_DONE_AMOUNT, COMPLETION_PROGRESS, COMPLETION_PROGRESS_AMOUNT, COMPLETION_INVALID, COMPLETION_INVALID_AMOUNT, LAW_C2_NUMBER, LAW_C2_AMOUNT, IS_ACTIVE, CREATED_BY, CREATED_DATE) " +
                    "VALUES(:P_AUDIT_ID, :P_LAW_RESPONDANT_NAME, :P_LAW_VIOLATION_DESC, :P_LAW_VIOLATION_TYPE, :P_LAW_MOVING_INFORMATION, :P_LAW_NUMBER, :P_LAW_AMOUNT, :P_COMPLETION_DONE, :P_COMPLETION_DONE_AMOUNT, :P_COMPLETION_PROGRESS, :P_COMPLETION_PROGRESS_AMOUNT, :P_COMPLETION_INVALID, :P_COMPLETION_INVALID_AMOUNT, :P_LAW_C2_NUMBER, :P_LAW_C2_AMOUNT, :P_IS_ACTIVE, :P_CREATED_BY, :P_CREATED_DATE)";

                // Set parameters
                cmd.Parameters.Add(":P_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_ID")?.Value;
                cmd.Parameters.Add(":P_LAW_RESPONDANT_NAME", OracleDbType.Varchar2).Value = elem.Element("LAW_RESPONDANT_NAME")?.Value;
                cmd.Parameters.Add(":P_LAW_VIOLATION_DESC", OracleDbType.Varchar2).Value = elem.Element("LAW_VIOLATION_DESC")?.Value;
                cmd.Parameters.Add(":P_LAW_VIOLATION_TYPE", OracleDbType.Int32).Value = elem.Element("LAW_VIOLATION_TYPE")?.Value;
                cmd.Parameters.Add(":P_LAW_MOVING_INFORMATION", OracleDbType.Varchar2).Value = elem.Element("LAW_MOVING_INFORMATION")?.Value;
                cmd.Parameters.Add(":P_LAW_NUMBER", OracleDbType.Int32).Value = elem.Element("LAW_NUMBER")?.Value;
                cmd.Parameters.Add(":P_LAW_AMOUNT", OracleDbType.Decimal).Value = elem.Element("LAW_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_DONE", OracleDbType.Int32).Value = elem.Element("COMPLETION_DONE")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_DONE_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_DONE_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_PROGRESS", OracleDbType.Int32).Value = elem.Element("COMPLETION_PROGRESS")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_PROGRESS_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_PROGRESS_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_INVALID", OracleDbType.Int32).Value = elem.Element("COMPLETION_INVALID")?.Value;
                cmd.Parameters.Add(":P_COMPLETION_INVALID_AMOUNT", OracleDbType.Decimal).Value = elem.Element("COMPLETION_INVALID_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_LAW_C2_NUMBER", OracleDbType.Int32).Value = elem.Element("LAW_C2_NUMBER")?.Value;
                cmd.Parameters.Add(":P_LAW_C2_AMOUNT", OracleDbType.Decimal).Value = elem.Element("LAW_C2_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_IS_ACTIVE", OracleDbType.Int32).Value = elem.Element("IS_ACTIVE")?.Value;
                cmd.Parameters.Add(":P_CREATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_CREATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM5Delete(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM5_DATA " +
                    "SET IS_ACTIVE = 0, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("UPDATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Устгалаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(BM.ID) " +
                    "FROM AUD_STAT.BM6_DATA BM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON BM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON BM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND BM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND BM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(BM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(BM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(BM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT BM.ID, BM.OFFICE_ID, RD.DEPARTMENT_NAME, BM.STATISTIC_PERIOD, RP.PERIOD_LABEL, BM.AUDIT_YEAR, BM.AUDIT_TYPE, BM.AUDIT_CODE, BM.AUDIT_NAME, BM.VIOLATION_COUNT, BM.VIOLATION_AMOUNT,BM.ERROR_COUNT, BM.ERROR_AMOUNT,BM.ALL_COUNT, BM.ALL_AMOUNT, BM.CORRECTED_ERROR_COUNT, BM.CORRECTED_ERROR_AMOUNT,BM.OTHER_ERROR_COUNT, BM.OTHER_ERROR_AMOUNT,BM.ACT_COUNT,BM.ACT_AMOUNT,BM.CLAIM_COUNT, BM.CLAIM_AMOUNT,BM.REFERENCE_COUNT,BM.REFERENCE_AMOUNT, BM.PROPOSAL_COUNT, BM.PROPOSAL_AMOUNT,BM.LAW_COUNT, BM.LAW_AMOUNT, BM.OTHER_COUNT,BM.OTHER_AMOUNT, BM.EXEC_TYPE, BM.IS_ACTIVE, BM.CREATED_BY, BM.CREATED_DATE, BM.UPDATED_BY, BM.UPDATED_DATE " +
                    "FROM AUD_STAT.BM6_DATA BM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON BM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON BM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND BM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND BM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(BM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(BM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(BM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN BM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN BM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN BM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'ASC' THEN BM.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'DESC' THEN BM.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN BM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN BM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'ASC' THEN BM.AUDIT_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'DESC' THEN BM.AUDIT_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'ASC' THEN BM.AUDIT_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'DESC' THEN BM.AUDIT_NAME END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM6";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(BM.ID) " +
                    "FROM AUD_STAT.BM7_DATA BM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON BM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON BM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND BM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND BM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(BM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(BM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(BM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.DECISION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT BM.ID, BM.OFFICE_ID, RD.DEPARTMENT_NAME, BM.STATISTIC_PERIOD, RP.PERIOD_LABEL, BM.AUDIT_YEAR, BM.AUDIT_TYPE, BM.AUDIT_CODE, BM.AUDIT_NAME, BM.DECISION_TYPE,BM.INCOME_STATE_COUNT,BM.INCOME_STATE_AMOUNT, BM.INCOME_LOCAL_COUNT, BM.INCOME_LOCAL_NUMBER, BM.BUDGET_STATE_COUNT,BM.BUDGET_STATE_AMOUNT,BM.BUDGET_LOCAL_COUNT, BM.BUDGET_LOCAL_AMOUNT, BM.ACCOUNTANT_COUNT, BM.ACCOUNTANT_AMOUNT, BM.EFFICIENCY_COUNT, BM.EFFICIENCY_AMOUNT, BM.LAW_COUNT, BM.LAW_AMOUNT, BM.MONITORING_COUNT, BM.MONITORING_AMOUNT, BM.PURCHASE_COUNT, BM.PURCHASE_AMOUNT, BM.COST_COUNT, BM.COST_AMOUNT, BM.OTHER_COUNT, BM.OTHER_AMOUNT, BM.ALL_COUNT, BM.ALL_AMOUNT, BM.EXEC_TYPE, BM.IS_ACTIVE, BM.CREATED_BY, BM.CREATED_DATE, BM.UPDATED_BY, BM.UPDATED_DATE " +
                    "FROM AUD_STAT.BM7_DATA BM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON BM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON BM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND BM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND BM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(BM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(BM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(BM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.DECISION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN BM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN BM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN BM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'ASC' THEN BM.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'DESC' THEN BM.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN BM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN BM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'ASC' THEN BM.AUDIT_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'DESC' THEN BM.AUDIT_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'ASC' THEN BM.AUDIT_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'DESC' THEN BM.AUDIT_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'DECISION_TYPE' AND: ORDER_DIR = 'DESC' THEN BM.DECISION_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'DECISION_TYPE' AND: ORDER_DIR = 'DESC' THEN BM.DECISION_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM7";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
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
                XElement req = request.Element("Parameters").Element("Request");

                //RowCount
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(BM.ID) " +
                        "FROM AUD_STAT.BM8_DATA BM " +
                    "INNER JOIN AUD_STAT.BM0_DATA B ON BM.AUDIT_ID = B.ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON B.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON B.DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON B.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON B.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON B.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON B.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_VIOLATION_TYPE RVT ON BM.CORRECTED_ERROR_TYPE = RVT.VIOLATION_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND B.IS_ACTIVE = 1 " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR (:V_USER_TYPE = 'Branch_Auditor' AND b.audit_department_id = :V_DEPARTMENT)) " +
                    "AND b.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(b.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%'  " +
                    "OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.CORRECTED_ERROR_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(b.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(b.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(b.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.CORRECTED_ERROR_DESC) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT B.STATISTIC_PERIOD, RP.PERIOD_LABEL, B.DEPARTMENT_ID, RD.DEPARTMENT_NAME, RAY.YEAR_LABEL, B.AUDIT_TYPE, RAT.AUDIT_TYPE_NAME, RTT.TOPIC_TYPE_NAME, B.TOPIC_CODE, B.TOPIC_NAME, B.ORDER_NO, B.ORDER_DATE, RBT.BUDGET_TYPE_NAME, BM.CORRECTED_ERROR_DESC, BM.CORRECTED_ERROR_TYPE, BM.CORRECTED_COUNT, BM.CORRECTED_AMOUNT " +
                    "FROM AUD_STAT.BM8_DATA BM " +
                    "INNER JOIN AUD_STAT.BM0_DATA B ON BM.AUDIT_ID = B.ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON B.STATISTIC_PERIOD = RP.ID " +
                    "INNER JOIN AUD_ORG.REF_DEPARTMENT RD ON B.DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_YEAR RAY ON B.AUDIT_YEAR = RAY.YEAR_ID " +
                    "INNER JOIN AUD_STAT.REF_AUDIT_TYPE RAT ON B.AUDIT_TYPE = RAT.AUDIT_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_TOPIC_TYPE RTT ON B.TOPIC_TYPE = RTT.TOPIC_TYPE_ID " +
                    "LEFT JOIN AUD_STAT.REF_BUDGET_TYPE RBT ON B.AUDIT_BUDGET_TYPE = RBT.BUDGET_TYPE_ID " +
                    "INNER JOIN AUD_STAT.REF_VIOLATION_TYPE RVT ON BM.CORRECTED_ERROR_TYPE = RVT.VIOLATION_ID " +
                    "WHERE BM.IS_ACTIVE = 1 AND B.IS_ACTIVE = 1 " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR (:V_USER_TYPE = 'Branch_Auditor' AND b.audit_department_id = :V_DEPARTMENT)) " +
                    "AND b.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(b.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%'  " +
                    "OR UPPER(RAT.AUDIT_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.CORRECTED_ERROR_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(b.TOPIC_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(b.TOPIC_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(RBT.BUDGET_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(RTT.TOPIC_TYPE_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(b.ORDER_NO) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(BM.CORRECTED_ERROR_DESC) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY  " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN BM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'audit_department_id' AND :ORDER_DIR = 'ASC' THEN b.audit_department_id END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'audit_department_id' AND :ORDER_DIR = 'DESC' THEN b.audit_department_id END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND :ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND :ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND :ORDER_DIR = 'ASC' THEN b.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND :ORDER_DIR = 'DESC' THEN b.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND :ORDER_DIR = 'ASC' THEN b.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND :ORDER_DIR = 'DESC' THEN b.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND :ORDER_DIR = 'ASC' THEN b.TOPIC_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_CODE' AND :ORDER_DIR = 'DESC' THEN b.TOPIC_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND :ORDER_DIR = 'ASC' THEN b.TOPIC_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'TOPIC_NAME' AND :ORDER_DIR = 'DESC' THEN b.TOPIC_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND :ORDER_DIR = 'ASC' THEN b.AUDIT_BUDGET_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND :ORDER_DIR = 'DESC' THEN b.AUDIT_BUDGET_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND :ORDER_DIR = 'ASC' THEN b.ORDER_NO END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'ORDER_NO' AND :ORDER_DIR = 'DESC' THEN b.ORDER_NO END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'CORRECTED_ERROR_DESC' AND :ORDER_DIR = 'ASC' THEN BM.CORRECTED_ERROR_DESC END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'CORRECTED_ERROR_DESC' AND :ORDER_DIR = 'DESC' THEN BM.CORRECTED_ERROR_DESC END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'CORRECTED_ERROR_TYPE' AND :ORDER_DIR = 'ASC' THEN BM.CORRECTED_ERROR_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'CORRECTED_ERROR_TYPE' AND :ORDER_DIR = 'DESC' THEN BM.CORRECTED_ERROR_TYPE END DESC " +
                    "OFFSET((:PAGENUMBER / :PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value  :null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM8";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM8Detail(XElement request)
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
                cmd.CommandText = "SELECT OFFICE_ID, STATISTIC_PERIOD, AUDIT_YEAR, AUDIT_TYPE, AUDIT_CODE, AUDIT_NAME, AUDIT_BUDGET_TYPE, CORRECTED_ERROR_DESC, CORRECTED_ERROR_TYPE, CORRECTED_COUNT, CORRECTED_AMOUNT, EXEC_TYPE, CREATED_DATE, ID, IS_ACTIVE, CREATED_BY FROM AUD_STAT.BM8_DATA WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("P_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "BM8Detail";

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
        public static DataResponse BM8Update(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM8");                
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM8_DATA SET OCORRECTED_ERROR_DESC = :P_CORRECTED_ERROR_DESC, CORRECTED_ERROR_TYPE = :P_CORRECTED_ERROR_TYPE, CORRECTED_COUNT = :P_CORRECTED_COUNT, CORRECTED_AMOUNT = :P_CORRECTED_AMOUNT, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_CORRECTED_ERROR_DESC", OracleDbType.Varchar2).Value = elem.Element("P_CORRECTED_ERROR_DESC") != null && !string.IsNullOrEmpty(elem.Element("P_CORRECTED_ERROR_DESC").Value) ? elem.Element("CORRECTED_ERROR_DESC").Value : null;
                cmd.Parameters.Add(":P_CORRECTED_ERROR_TYPE", OracleDbType.Varchar2).Value = elem.Element("CORRECTED_ERROR_TYPE") != null && !string.IsNullOrEmpty(elem.Element("CORRECTED_ERROR_TYPE").Value) ? elem.Element("CORRECTED_ERROR_TYPE").Value : null;
                cmd.Parameters.Add(":P_CORRECTED_COUNT", OracleDbType.Int32).Value = elem.Element("CORRECTED_COUNT") != null && !string.IsNullOrEmpty(elem.Element("CORRECTED_COUNT").Value) ? elem.Element("CORRECTED_COUNT").Value : null;
                cmd.Parameters.Add(":P_CORRECTED_AMOUNT", OracleDbType.Int32).Value = elem.Element("CORRECTED_AMOUNT") != null && !string.IsNullOrEmpty(elem.Element("CORRECTED_AMOUNT").Value) ? elem.Element("CORRECTED_AMOUNT").Value : null;
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = elem.Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM8Insert(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("BM8");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO AUD_STAT.BM8_DATA(AUDIT_ID, CORRECTED_ERROR_DESC, CORRECTED_ERROR_TYPE, CORRECTED_COUNT, CORRECTED_AMOUNT, IS_ACTIVE, CREATED_BY, CREATED_DATE) " +
                    "VALUES (:P_AUDIT_ID, :P_CORRECTED_ERROR_DESC, :P_CORRECTED_ERROR_TYPE, :P_CORRECTED_COUNT, :P_CORRECTED_AMOUNT, :P_IS_ACTIVE, :P_CREATED_BY, :P_CREATED_DATE)";
                // Set parameters
                cmd.Parameters.Add(":P_AUDIT_ID", OracleDbType.Int32).Value = elem.Element("AUDIT_ID")?.Value;                
                cmd.Parameters.Add(":P_CORRECTED_ERROR_DESC", OracleDbType.Varchar2).Value = elem.Element("P_CORRECTED_ERROR_DESC") != null && !string.IsNullOrEmpty(elem.Element("P_CORRECTED_ERROR_DESC").Value) ? elem.Element("CORRECTED_ERROR_DESC").Value : null;
                cmd.Parameters.Add(":P_CORRECTED_ERROR_TYPE", OracleDbType.Varchar2).Value = elem.Element("CORRECTED_ERROR_TYPE") != null && !string.IsNullOrEmpty(elem.Element("CORRECTED_ERROR_TYPE").Value) ? elem.Element("CORRECTED_ERROR_TYPE").Value : null;
                cmd.Parameters.Add(":P_CORRECTED_COUNT", OracleDbType.Int32).Value = elem.Element("CORRECTED_COUNT") != null && !string.IsNullOrEmpty(elem.Element("CORRECTED_COUNT").Value) ? elem.Element("CORRECTED_COUNT").Value : null;
                cmd.Parameters.Add(":P_CORRECTED_AMOUNT", OracleDbType.Int32).Value = elem.Element("CORRECTED_AMOUNT") != null && !string.IsNullOrEmpty(elem.Element("CORRECTED_AMOUNT").Value) ? elem.Element("CORRECTED_AMOUNT").Value : null;
                cmd.Parameters.Add(":P_IS_ACTIVE", OracleDbType.Int32).Value = elem.Element("IS_ACTIVE")?.Value;
                cmd.Parameters.Add(":P_CREATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_CREATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;
                
                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse BM8Delete(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.BM8_DATA "+
                    "SET IS_ACTIVE = 0, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("UPDATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Устгалаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse OrgSearch(XElement request)
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
                cmd.CommandText = "SELECT ORG_ID, ORG_CODE, ORG_NAME, ORG_REGISTER_NO FROM AUD_REG.REG_ORGANIZATION WHERE IS_ACTIVE = 1 " +
                    "AND(:P_SEARCH IS NULL OR UPPER(ORG_CODE) LIKE '%' || UPPER(:P_SEARCH) || '%' OR UPPER(ORG_NAME) LIKE '%' || UPPER(:P_SEARCH) || '%' " +
                    "OR UPPER(ORG_REGISTER_NO) LIKE '%' || UPPER(:P_SEARCH) || '%') ORDER BY ORG_DEPARTMENT_ID";

                // Set parameters
                cmd.Parameters.Add(":P_SEARCH", OracleDbType.Int32, request.Element("Parameters").Element("V_SEARCH").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "OrgSearch";

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
       
        #endregion
        #region NM
        public static DataResponse NM1(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(NM.ID) " +
                    "FROM AUD_STAT.NM1_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT NM.ID,NM.OFFICE_ID, RD.DEPARTMENT_NAME, NM.STATISTIC_PERIOD, RP.PERIOD_LABEL, NM.AUDIT_YEAR, NM.AUDIT_TYPE, NM.AUDIT_CODE, NM.AUDIT_NAME, NM.AUDIT_BUDGET_TYPE, NM.ACT_COUNT, NM.ACT_AMOUNT, NM.COMPLETION_COUNT, NM.COMPLETION_AMOUNT, NM.COMPLETION_STATE_COUNT,NM.COMPLETION_STATE_AMOUNT, NM.COMPLETION_LOCAL_COUNT, NM.COMPLETION_LOCAL_AMOUNT, NM.COMPLETION_ORG_COUNT, NM.COMPLETION_ORG_AMOUNT, NM.COMPLETION_OTHER_COUNT, NM.COMPLETION_OTHER_AMOUNT,NM.REMOVED_COUNT, NM.REMOVED_AMOUNT, NM.REMOVED_LAW_COUNT,NM.REMOVED_LAW_AMOUNT, NM.REMOVED_INVALID_COUNT, NM.REMOVED_INVALID_AMOUNT, NM.ACT_C2_COUNT,NM.ACT_C2_AMOUNT, NM.ACT_NONEXPIRED_COUNT, NM.ACT_EXPIRED_COUNT,NM.ACT_EXPIRED_AMOUNT,NM.BENEFIT_FIN,NM.BENEFIT_FIN_AMOUNT, NM.BENEFIT_NONFIN, NM.EXEC_TYPE, NM.IS_ACTIVE, NM.CREATED_BY, NM.CREATED_DATE, NM.UPDATED_BY, NM.UPDATED_DATE " +
                    "FROM AUD_STAT.NM1_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN NM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME IS NOT NULL AND: ORDER_DIR = 'ASC' THEN 'NM.' ||:ORDER_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME IS NOT NULL AND: ORDER_DIR = 'DESC' THEN 'NM.' ||:ORDER_NAME END DESC " + 
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM1";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(NM.ID) " +
                    "FROM AUD_STAT.NM2_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT NM.ID,NM.OFFICE_ID, RD.DEPARTMENT_NAME, NM.STATISTIC_PERIOD, RP.PERIOD_LABEL, NM.AUDIT_YEAR, NM.AUDIT_TYPE, NM.AUDIT_CODE, NM.AUDIT_NAME, NM.AUDIT_BUDGET_TYPE, NM.CLAIM_VIOLATION_COUNT,NM.CLAIM_VIOLATION_AMOUNT, NM.COMPLETION_COUNT,NM.COMPLETION_AMOUNT,NM.COMPLETION_STATE_COUNT, NM.COMPLETION_STATE_AMOUNT,NM.COMPLETION_LOCAL_COUNT,NM.COMPLETION_LOCAL_AMOUNT, NM.COMPLETION_ORG_COUNT, NM.COMPLETION_ORG_AMOUNT, NM.COMPLETION_OTHER_COUNT, NM.COMPLETION_OTHER_AMOUNT, NM.REMOVED_COUNT, NM.REMOVED_AMOUNT, NM.REMOVED_LAW_COUNT, NM.REMOVED_LAW_AMOUNT, NM.REMOVED_INVALID_COUNT, NM.REMOVED_INVALID_AMOUNT, NM.CLAIM_C2_COUNT, NM.CLAIM_C2_AMOUNT, NM.CLAIM_C2_NONEXPIRED_COUNT, NM.CLAIM_C2_NONEXPIRED_AMOUNT, NM.CLAIM_C2_EXPIRED_COUNT, NM.CLAIM_C2_EXPIRED_AMOUNT, NM.BENEFIT_FIN_COUNT, NM.BENEFIT_FIN_AMOUNT, NM.BENEFIT_NONFIN_COUNT, NM.BENEFIT_NONFIN_AMOUNT, NM.EXEC_TYPE, NM.IS_ACTIVE, NM.CREATED_BY, NM.CREATED_DATE, NM.UPDATED_BY, NM.UPDATED_DATE " +
                    "FROM AUD_STAT.NM2_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN NM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN NM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN NM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_BUDGET_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_BUDGET_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM2";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(NM.ID) " +
                    "FROM AUD_STAT.NM3_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT NM.ID,NM.OFFICE_ID, RD.DEPARTMENT_NAME, NM.STATISTIC_PERIOD, RP.PERIOD_LABEL, NM.AUDIT_YEAR, NM.AUDIT_TYPE, NM.AUDIT_CODE, NM.AUDIT_NAME, NM.AUDIT_BUDGET_TYPE,NM.REFERENCE_COUNT,NM.REFERENCE_AMOUNT, NM.COMPLETION_DONE_COUNT, NM.COMPLETION_DONE_AMOUNT, NM.COMPLETION_PROGRESS_COUNT, NM.COMPLETION_PROGRESS_AMOUNT, NM.C2_NONEXPIRED_COUNT, NM.C2_NONEXPIRED_AMOUNT, NM.C2_EXPIRED_COUNT, NM.C2_EXPIRED_AMOUNT, NM.BENEFIT_FIN_COUNT, NM.BENEFIT_FIN_AMOUNT, NM.BENEFIT_NONFIN_COUNT, NM.WORKING_PERSON, NM.WORKING_DAY, NM.WORKING_ADDITION_TIME, NM.EXEC_TYPE, NM.IS_ACTIVE, NM.CREATED_BY, NM.CREATED_DATE, NM.UPDATED_BY, NM.UPDATED_DATE " +
                    "FROM AUD_STAT.NM3_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN NM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN NM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN NM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_BUDGET_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_BUDGET_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM3";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(NM.ID) " +
                    "FROM AUD_STAT.NM4_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT NM.ID,NM.OFFICE_ID, RD.DEPARTMENT_NAME, NM.STATISTIC_PERIOD, RP.PERIOD_LABEL, NM.AUDIT_YEAR, NM.AUDIT_TYPE, NM.AUDIT_CODE, NM.AUDIT_NAME, NM.AUDIT_BUDGET_TYPE,NM.PROPOSAL_COUNT, NM.PROPOSAL_AMOUNT, NM.COMPLETION_DONE_COUNT, NM.COMPLETION_DONE_AMOUNT,NM.COMPLETION_PROGRESS_COUNT, NM.COMPLETION_PROGRESS_AMOUNT, NM.EXEC_TYPE,NM.CREATED_DATE,NM.PROPOSAL_VIOLATION_TYPE, NM.IS_ACTIVE, NM.CREATED_BY, NM.UPDATED_BY, NM.UPDATED_DATE " +
                    "FROM AUD_STAT.NM4_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN NM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN NM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN NM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_BUDGET_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_BUDGET_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM4";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(NM.ID) " +
                    "FROM AUD_STAT.NM5_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT NM.ID,NM.OFFICE_ID, RD.DEPARTMENT_NAME, NM.STATISTIC_PERIOD, RP.PERIOD_LABEL, NM.AUDIT_YEAR, NM.AUDIT_TYPE, NM.AUDIT_CODE, NM.AUDIT_NAME, NM.AUDIT_BUDGET_TYPE, NM.LAW_COUNT, NM.LAW_AMOUNT, NM.COMPLETION_DONE_COUNT, NM.COMPLETION_DONE_AMOUNT, NM.COMPLETION_PROGRESS_COUNT, NM.COMPLETION_PROGRESS_AMOUNT,NM.COMPLETION_INVALID_COUNT,NM.COMPLETION_INVALID_AMOUNT, NM.LAW_C2_NUMBER_COUNT, NM.LAW_C2_AMOUNT,NM.EXEC_TYPE, NM.IS_ACTIVE, NM.CREATED_BY, NM.CREATED_DATE, NM.UPDATED_BY, NM.UPDATED_DATE " +
                    "FROM AUD_STAT.NM5_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN NM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN NM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN NM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_BUDGET_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_BUDGET_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM5";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(NM.ID) " +
                    "FROM AUD_STAT.NM6_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT NM.ID,NM.OFFICE_ID, RD.DEPARTMENT_NAME, NM.STATISTIC_PERIOD, RP.PERIOD_LABEL, NM.AUDIT_YEAR, NM.AUDIT_TYPE, NM.AUDIT_CODE, NM.AUDIT_NAME, NM.VIOLATION_COUNT, NM.VIOLATION_AMOUNT, NM.ERROR_COUNT, NM.ERROR_AMOUNT, NM.ALL_COUNT, NM.ALL_AMOUNT, NM.CORRECTED_ERROR_COUNT, NM.CORRECTED_ERROR_AMOUNT, NM.OTHER_ERROR_COUNT, NM.OTHER_ERROR_AMOUNT, NM.ACT_COUNT, NM.ACT_AMOUNT, NM.CLAIM_COUNT, NM.CLAIM_AMOUNT, NM.REFERENCE_COUNT, NM.REFERENCE_AMOUNT, NM.PROPOSAL_COUNT, NM.PROPOSAL_AMOUNT, NM.LAW_COUNT, NM.LAW_AMOUNT,NM.OTHER_COUNT, NM.OTHER_AMOUNT,NM.EXEC_TYPE, NM.IS_ACTIVE, NM.CREATED_BY, NM.CREATED_DATE, NM.UPDATED_BY, NM.UPDATED_DATE " +
                    "FROM AUD_STAT.NM6_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN NM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN NM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN NM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_NAME END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM6";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(NM.ID) " +
                    "FROM AUD_STAT.NM7_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.DECISION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT NM.ID,NM.OFFICE_ID, RD.DEPARTMENT_NAME, NM.STATISTIC_PERIOD, RP.PERIOD_LABEL, NM.AUDIT_YEAR, NM.AUDIT_TYPE, NM.AUDIT_CODE, NM.AUDIT_NAME, NM.DECISION_TYPE, NM.INCOME_STATE_COUNT, NM.INCOME_STATE_AMOUNT, NM.INCOME_LOCAL_COUNT,NM.INCOME_LOCAL_NUMBER,NM.BUDGET_STATE_COUNT, NM.BUDGET_STATE_AMOUNT,NM.BUDGET_LOCAL_COUNT, NM.BUDGET_LOCAL_AMOUNT, NM.ACCOUNTANT_COUNT, NM.ACCOUNTANT_AMOUNT, NM.EFFICIENCY_COUNT, NM.EFFICIENCY_AMOUNT, NM.LAW_COUNT, NM.LAW_AMOUNT, NM.MONITORING_COUNT, NM.MONITORING_AMOUNT, NM.PURCHASE_COUNT, NM.PURCHASE_AMOUNT,NM.COST_COUNT, NM.COST_AMOUNT, NM.OTHER_COUNT, NM.OTHER_AMOUNT, NM.ALL_COUNT, NM.ALL_AMOUNT,NM.EXEC_TYPE, NM.IS_ACTIVE, NM.CREATED_BY, NM.CREATED_DATE, NM.UPDATED_BY, NM.UPDATED_DATE " +
                    "FROM AUD_STAT.NM7_DATA NM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON NM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON NM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND NM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND NM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(NM.AUDIT_YEAR) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.AUDIT_CODE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(NM.AUDIT_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' OR UPPER(NM.DECISION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN NM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN NM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN NM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_YEAR END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_YEAR END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_CODE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_CODE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'ASC' THEN NM.AUDIT_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'DESC' THEN NM.AUDIT_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'DECISION_TYPE' AND: ORDER_DIR = 'ASC' THEN NM.DECISION_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'DECISION_TYPE' AND: ORDER_DIR = 'DESC' THEN NM.DECISION_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "NM7";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        #endregion
        #region CM
        public static DataResponse CM1(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(CM.ID) " +
                    "FROM AUD_STAT.CM1_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE CM.AUDIT_TYPE = :V_TYPE " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(CM.CATEGORY_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_TYPE", OracleDbType.Int32, req.Element("V_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CM.ID, CM.OFFICE_ID, RD.DEPARTMENT_NAME, CM.STATISTIC_PERIOD, RP.PERIOD_LABEL, CM.AUDIT_TYPE, CM.CATEGORY_TYPE, CM.IS_STATE,CM.WORKING_PERSON, CM.WORKING_DAY, CM.WORKING_ADDITION_TIME, CM.EXECUTORY, CM.EXEC_DECISION, CM.EXEC_COLLECTION, CM.EXEC_TRUSTED, CM.PERFORMED, CM.PERF_DECISION, CM.PERF_COLLECTION, CM.PERF_TRUSTED, CM.PERF_NOT_AUDITED, CM.PROPOSAL, CM.PROP_UNVIOLATED, CM.PROP_RESTRICTED, CM.PROP_NEGATIVE, CM.PROP_NOT, CM.TPA_COUNT, CM.TPA_AMOUNT, CM.AUDITED_INCLUDED_ORG, CM.BENEFIT_FIN_COUNT, CM.BENEFIT_FIN_AMOUNT, CM.BENEFIT_NONFIN, CM.EXEC_TYPE, CM.CREATED_DATE " +
                    "FROM AUD_STAT.CM1_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE CM.AUDIT_TYPE = :V_TYPE " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(CM.CATEGORY_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN CM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN CM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN CM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'CATEGORY_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.CATEGORY_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'CATEGORY_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.CATEGORY_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_TYPE", OracleDbType.Int32, req.Element("V_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM1";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM2(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(CM.ID) " +
                    "FROM AUD_STAT.CM2_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE CM.AUDIT_TYPE = :V_TYPE " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(CM.DECISION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%'OR UPPER(CM.BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_TYPE", OracleDbType.Int32, req.Element("V_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CM.ID, CM.OFFICE_ID, RD.DEPARTMENT_NAME, CM.STATISTIC_PERIOD, RP.PERIOD_LABEL, CM.AUDIT_TYPE, CM.DECISION_TYPE, CM.BUDGET_TYPE, CM.IS_STATE, CM.C1_COUNT, CM.C1_AMOUNT, CM.CURRENT_COUNT, CM.CURRENT_AMOUNT, CM.PREV_COUNT, CM.PREV_AMOUNT, CM.CY_COUNT, CM.CY_AMOUNT, CM.TOTAL_COUNT, CM.TOTAL_AMOUNT, CM.COMP_STATE_COUNT, CM.COMP_STATE_AMOUNT, CM.COMP_LOCAL_COUNT, CM.COMP_LOCAL_AMOUNT, CM.COMP_ORG_COUNT, CM.COMP_ORG_AMOUNT, CM.COMP_OTHER_COUNT, CM.COMP_OTHER_AMOUNT, CM.STATISTIC_COUNT, CM.STATISTIC_AMOUNT, CM.C2_COUNT, CM.C2_AMOUNT, CM.C2_NONEXPIRED_COUNT, CM.C2_NONEXPIRED_AMOUNT, CM.C2_EXPIRED_COUNT, CM.C2_EXPIRED_AMOUNT, CM.EXEC_TYPE, CM.IS_ACTIVE, CM.CREATED_BY, CM.CREATED_DATE, CM.UPDATED_BY, CM.UPDATED_DATE " +
                    "FROM AUD_STAT.CM2_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE CM.AUDIT_TYPE = :V_TYPE " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(CM.DECISION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%'OR UPPER(CM.BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN CM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN CM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN CM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'DECISION_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.DECISION_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'DECISION_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.DECISION_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'BUDGET_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.BUDGET_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'BUDGET_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.BUDGET_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_TYPE", OracleDbType.Int32, req.Element("V_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM2";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM3(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(CM.ID) " +
                    "FROM AUD_STAT.CM3_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE CM.AUDIT_TYPE = :V_TYPE " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(CM.DECISION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%'OR UPPER(CM.BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_TYPE", OracleDbType.Int32, req.Element("V_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CM.ID, CM.OFFICE_ID, RD.DEPARTMENT_NAME, CM.STATISTIC_PERIOD, RP.PERIOD_LABEL, CM.AUDIT_TYPE, CM.DECISION_TYPE, CM.BUDGET_TYPE, CM.IS_STATE, CM.C1_COUNT, CM.C1_AMOUNT, CM.CURRENT_COUNT, CM.CURRENT_AMOUNT, CM.TOTAL_COUNT, CM.TOTAL_AMOUNT, CM.COMPLETION_DONE_COUNT, CM.COMPLETION_DONE_AMOUNT, CM.COMPLETION_PROGRESS_COUNT, CM.COMPLETION_PROGRESS_AMOUNT, CM.LAW_COUNT, CM.LAW_AMOUNT, CM.LAW_CURRENT_COUNT, CM.LAW_CURRENT_AMOUNT, CM.LAW_TOTAL_COUNT, CM.LAW_TOTAL_AMOUNT, CM.LAW_COMP_DONE_COUNT, CM.LAW_COMP_DONE_AMOUNT, CM.LAW_COMP_PROG_COUNT, CM.LAW_COMP_PROG_AMOUNT, CM.LAW_COMP_INVALID_COUNT, CM.LAW_COMP_INVALID_AMOUNT,CM.C2_COUNT, CM.C2_AMOUNT, CM.EXEC_TYPE,  CM.CREATED_DATE, CM.IS_ACTIVE, CM.CREATED_BY, CM.UPDATED_BY, CM.UPDATED_DATE " +
                    "FROM AUD_STAT.CM3_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE CM.AUDIT_TYPE = :V_TYPE " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(CM.DECISION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%'OR UPPER(CM.BUDGET_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN CM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN CM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN CM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'DECISION_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.DECISION_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'DECISION_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.DECISION_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'BUDGET_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.BUDGET_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'BUDGET_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.BUDGET_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_TYPE", OracleDbType.Int32, req.Element("V_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM3";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM4(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(CM.ID) " +
                    "FROM AUD_STAT.CM4_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE CM.AUDIT_TYPE = :V_TYPE " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(CM.DECISION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%')";
                cmd.BindByName = true;
                cmd.Parameters.Add(":V_TYPE", OracleDbType.Int32, req.Element("V_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CM.ID, CM.OFFICE_ID, RD.DEPARTMENT_NAME, CM.STATISTIC_PERIOD, RP.PERIOD_LABEL, CM.AUDIT_TYPE, CM.DECISION_TYPE,CM.IS_STATE, CM.VIOLATION_COUNT, CM.VIOLATION_AMOUNT, CM.ERROR_COUNT, CM.ERROR_AMOUNT, CM.ALL_COUNT, CM.ALL_AMOUNT, CM.CORRECTED_ERROR_COUNT, CM.CORRECTED_ERROR_AMOUNT, CM.OTHER_ERROR_COUNT, CM.OTHER_ERROR_AMOUNT, CM.ACT_COUNT, CM.ACT_AMOUNT, CM.CLAIM_COUNT, CM.CLAIM_AMOUNT, CM.REFERENCE_COUNT, CM.REFERENCE_AMOUNT, CM.PROPOSAL_COUNT, CM.PROPOSAL_AMOUNT, CM.LAW_COUNT, CM.LAW_AMOUNT, CM.OTHER_COUNT, CM.OTHER_AMOUNT, CM.EXEC_TYPE, CM.CREATED_DATE, CM.IS_ACTIVE, CM.CREATED_BY, CM.UPDATED_BY, CM.UPDATED_DATE " +
                    "FROM AUD_STAT.CM4_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE CM.AUDIT_TYPE = :V_TYPE " +
                    "AND (:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(CM.DECISION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN CM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN CM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN CM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'DECISION_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.DECISION_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'DECISION_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.DECISION_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_TYPE", OracleDbType.Int32, req.Element("V_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM4";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(CM.ID) " +
                    "FROM AUD_STAT.CM5_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(CM.DECISION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') ";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CM.ID, CM.OFFICE_ID, RD.DEPARTMENT_NAME, CM.STATISTIC_PERIOD, RP.PERIOD_LABEL, CM.AUDIT_TYPE, CM.DECISION_TYPE, CM.INCOME_STATE_COUNT, CM.INCOME_STATE_AMOUNT, CM.INCOME_LOCAL_COUNT, CM.INCOME_LOCAL_AMOUNT, CM.BUDGET_STATE_COUNT, CM.BUDGET_STATE_AMOUNT, CM.BUDGET_LOCAL_COUNT, CM.BUDGET_LOCAL_AMOUNT, CM.ACCOUNTANT_COUNT, CM.ACCOUNTANT_AMOUNT, CM.EFFICIENCY_COUNT, CM.EFFICIENCY_AMOUNT, CM.LAW_COUNT, CM.LAW_AMOUNT, CM.MONITORING_COUNT, CM.MONITORING_AMOUNT, CM.PURCHASE_COUNT, CM.PURCHASE_AMOUNT, CM.COST_COUNT, CM.COST_AMOUNT, CM.OTHER_COUNT, CM.OTHER_AMOUNT, CM.EXEC_TYPE, CM.CREATED_DATE, CM.IS_ACTIVE, CM.CREATED_BY, CM.UPDATED_BY, CM.UPDATED_DATE " +
                    "FROM AUD_STAT.CM5_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUDIT_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(CM.DECISION_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN CM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN CM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN CM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.AUDIT_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.AUDIT_TYPE END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'DECISION_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.DECISION_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'DECISION_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.DECISION_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM5";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(CM.ID) " +
                    "FROM AUD_STAT.CM6_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUD_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CM.ID, CM.OFFICE_ID, RD.DEPARTMENT_NAME, CM.STATISTIC_PERIOD, RP.PERIOD_LABEL, CM.AUD_NAME, CM.IS_STATE, CM.ALL_COUNT, CM.ALL_AMOUNT, CM.PROCESSED_INCOMED_COUNT, CM.PROCESSED_INCOMED_AMOUNT, CM.PROCESSED_COSTS_COUNT, CM.PROCESSED_COSTS_AMOUNT, CM.ALL_C1_COUNT, CM.ALL_C2_AMOUNT, CM.ACCEPTED_INCOMED_COUNT, CM.ACCEPTED_INCOMED_AMOUNT, CM.ACCEPTED_COSTS_COUNT, CM.ACCEPTED_COSTS_AMOUNT, CM.EXEC_TYPE, CM.CREATED_DATE, CM.IS_ACTIVE, CM.CREATED_BY,CM.UPDATED_BY, CM.UPDATED_DATE " +
                    "FROM AUD_STAT.CM6_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUD_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN CM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN CM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN CM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUD_NAME' AND: ORDER_DIR = 'ASC' THEN CM.AUD_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUD_NAME' AND: ORDER_DIR = 'DESC' THEN CM.AUD_NAME END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM6";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM6Detail(XElement request)
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
                cmd.CommandText = "SELECT ID, OFFICE_ID, STATISTIC_PERIOD, AUD_NAME, IS_STATE, ALL_COUNT, ALL_AMOUNT, PROCESSED_INCOMED_COUNT, PROCESSED_INCOMED_AMOUNT, PROCESSED_COSTS_COUNT, PROCESSED_COSTS_AMOUNT, ALL_C1_COUNT, ALL_C2_AMOUNT, ACCEPTED_INCOMED_COUNT, ACCEPTED_INCOMED_AMOUNT, ACCEPTED_COSTS_COUNT, ACCEPTED_COSTS_AMOUNT, EXEC_TYPE, CREATED_DATE, CREATED_BY, IS_ACTIVE FROM " +
                    "CM6_DATA WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("P_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM6Detail";

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
        public static DataResponse CM6Update(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                XElement elem = request.Element("Parameters").Element("CM6");
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.CM6_DATA " +
                    "SET OFFICE_ID = :P_OFFICE_ID, STATISTIC_PERIOD = :P_STATISTIC_PERIOD, AUD_NAME = :P_AUD_NAME, IS_STATE = :P_IS_STATE, ALL_COUNT = :P_ALL_COUNT, ALL_AMOUNT = :P_ALL_AMOUNT, PROCESSED_INCOMED_COUNT = :P_PROCESSED_INCOMED_COUNT, PROCESSED_INCOMED_AMOUNT = :P_PROCESSED_INCOMED_AMOUNT, PROCESSED_COSTS_COUNT = :P_PROCESSED_COSTS_COUNT, PROCESSED_COSTS_AMOUNT = :P_PROCESSED_COSTS_AMOUNT, ALL_C1_COUNT = :P_ALL_C1_COUNT, ALL_C2_AMOUNT = :P_ALL_C2_AMOUNT, ACCEPTED_INCOMED_COUNT = :P_ACCEPTED_INCOMED_COUNT, ACCEPTED_INCOMED_AMOUNT = :P_ACCEPTED_INCOMED_AMOUNT, ACCEPTED_COSTS_COUNT = :P_ACCEPTED_COSTS_COUNT, ACCEPTED_COSTS_AMOUNT = :P_ACCEPTED_COSTS_AMOUNT, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_OFFICE_ID", OracleDbType.Int32).Value = elem.Element("OFFICE_ID").Value;
                cmd.Parameters.Add(":P_STATISTIC_PERIOD", OracleDbType.Int32).Value = elem.Element("STATISTIC_PERIOD").Value;
                cmd.Parameters.Add(":P_AUD_NAME", OracleDbType.Varchar2).Value = elem.Element("P_AUD_NAME")?.Value;
                cmd.Parameters.Add(":P_IS_STATE", OracleDbType.Int32).Value = elem.Element("IS_STATE")?.Value;
                cmd.Parameters.Add(":P_ALL_COUNT", OracleDbType.Int32).Value = elem.Element("ALL_COUNT")?.Value;
                cmd.Parameters.Add(":P_ALL_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ALL_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_PROCESSED_INCOMED_COUNT", OracleDbType.Int32).Value = elem.Element("PROCESSED_INCOMED_COUNT")?.Value;
                cmd.Parameters.Add(":P_PROCESSED_INCOMED_AMOUNT", OracleDbType.Decimal).Value = elem.Element("PROCESSED_INCOMED_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_PROCESSED_COSTS_COUNT", OracleDbType.Int32).Value = elem.Element("PROCESSED_COSTS_COUNT")?.Value;
                cmd.Parameters.Add(":P_PROCESSED_COSTS_AMOUNT", OracleDbType.Decimal).Value = elem.Element("PROCESSED_COSTS_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ALL_C1_COUNT", OracleDbType.Int32).Value = elem.Element("ALL_C1_COUNT")?.Value;
                cmd.Parameters.Add(":P_ALL_C2_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ALL_C2_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACCEPTED_INCOMED_COUNT", OracleDbType.Int32).Value = elem.Element("ACCEPTED_INCOMED_COUNT")?.Value;
                cmd.Parameters.Add(":P_ACCEPTED_INCOMED_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACCEPTED_INCOMED_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACCEPTED_COSTS_COUNT", OracleDbType.Int32).Value = elem.Element("ACCEPTED_COSTS_COUNT")?.Value;
                cmd.Parameters.Add(":P_ACCEPTED_COSTS_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACCEPTED_COSTS_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = elem.Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM6Insert(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                XElement elem = request.Element("Parameters").Element("CM6");
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO AUD_STAT.CM6_DATA ( OFFICE_ID, STATISTIC_PERIOD, AUD_NAME, IS_STATE, ALL_COUNT, ALL_AMOUNT, PROCESSED_INCOMED_COUNT, PROCESSED_INCOMED_AMOUNT, PROCESSED_COSTS_COUNT, PROCESSED_COSTS_AMOUNT, ALL_C1_COUNT, ALL_C2_AMOUNT, ACCEPTED_INCOMED_COUNT, ACCEPTED_INCOMED_AMOUNT, ACCEPTED_COSTS_COUNT, ACCEPTED_COSTS_AMOUNT, IS_ACTIVE, CREATED_BY, CREATED_DATE) " +
                    "VALUES(:P_OFFICE_ID, :P_STATISTIC_PERIOD, :P_AUD_NAME, :P_IS_STATE, :P_ALL_COUNT, :P_ALL_AMOUNT, :P_PROCESSED_INCOMED_COUNT, :P_PROCESSED_INCOMED_AMOUNT, :P_PROCESSED_COSTS_COUNT, :P_PROCESSED_COSTS_AMOUNT, :P_ALL_C1_COUNT, :P_ALL_C2_AMOUNT, :P_ACCEPTED_INCOMED_COUNT, :P_ACCEPTED_INCOMED_AMOUNT, :P_ACCEPTED_COSTS_COUNT, :P_ACCEPTED_COSTS_AMOUNT, :P_IS_ACTIVE, :P_CREATED_BY, :P_CREATED_DATE)";

                // Set parameters
                cmd.Parameters.Add(":P_OFFICE_ID", OracleDbType.Int32).Value = elem.Element("OFFICE_ID")?.Value;
                cmd.Parameters.Add(":P_STATISTIC_PERIOD", OracleDbType.Int32).Value = elem.Element("STATISTIC_PERIOD")?.Value;
                cmd.Parameters.Add(":P_AUD_NAME", OracleDbType.Varchar2).Value = elem.Element("AUD_NAME")?.Value;
                cmd.Parameters.Add(":P_IS_STATE", OracleDbType.Int32).Value = elem.Element("IS_STATE")?.Value;
                cmd.Parameters.Add(":P_ALL_COUNT", OracleDbType.Int32).Value = elem.Element("ALL_COUNT")?.Value;
                cmd.Parameters.Add(":P_ALL_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ALL_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_PROCESSED_INCOMED_COUNT", OracleDbType.Int32).Value = elem.Element("PROCESSED_INCOMED_COUNT")?.Value;
                cmd.Parameters.Add(":P_PROCESSED_INCOMED_AMOUNT", OracleDbType.Decimal).Value = elem.Element("PROCESSED_INCOMED_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_PROCESSED_COSTS_COUNT", OracleDbType.Int32).Value = elem.Element("PROCESSED_COSTS_COUNT")?.Value;
                cmd.Parameters.Add(":P_PROCESSED_COSTS_AMOUNT", OracleDbType.Decimal).Value = elem.Element("PROCESSED_COSTS_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ALL_C1_COUNT", OracleDbType.Int32).Value = elem.Element("ALL_C1_COUNT")?.Value;
                cmd.Parameters.Add(":P_ALL_C2_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ALL_C2_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACCEPTED_INCOMED_COUNT", OracleDbType.Int32).Value = elem.Element("ACCEPTED_INCOMED_COUNT")?.Value;
                cmd.Parameters.Add(":P_ACCEPTED_INCOMED_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACCEPTED_INCOMED_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_ACCEPTED_COSTS_COUNT", OracleDbType.Int32).Value = elem.Element("ACCEPTED_COSTS_COUNT")?.Value;
                cmd.Parameters.Add(":P_ACCEPTED_COSTS_AMOUNT", OracleDbType.Decimal).Value = elem.Element("ACCEPTED_COSTS_AMOUNT")?.Value;
                cmd.Parameters.Add(":P_IS_ACTIVE", OracleDbType.Int32).Value = elem.Element("IS_ACTIVE")?.Value;
                cmd.Parameters.Add(":P_CREATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_CREATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM6Delete(XElement request)
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
                cmd.CommandText = "UPDATE AUD_STAT.CM6_DATA " +
                    "SET IS_ACTIVE = 0, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("UPDATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Устгалаа");
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(CM.ID) " +
                    "FROM AUD_STAT.CM7_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUD_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(CM.NAME_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%')";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CM.ID, CM.OFFICE_ID, RD.DEPARTMENT_NAME, CM.STATISTIC_PERIOD, RP.PERIOD_LABEL, CM.AUD_NAME, CM.NAME_TYPE, CM.REFERENCE_COUNT, CM.BUDGET_EXPENSES, CM.HUMAN_RESOURCES, CM.PLANNED_COMPLETED, CM.OTHER, CM.COMP_DONE, CM.COMP_PROGRESS, CM.RESOLVED_COMPLAINT_COUNT, CM.REFERENCE_NOT_COMP, CM.EXEC_TYPE, CM.CREATED_DATE, CM.IS_ACTIVE, CM.CREATED_BY,CM.UPDATED_BY, CM.UPDATED_DATE " +
                    "FROM AUD_STAT.CM7_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD AND(:V_SEARCH IS NULL OR UPPER(CM.AUD_NAME) LIKE '%' || UPPER(:V_SEARCH) || '%' " +
                    "OR UPPER(CM.NAME_TYPE) LIKE '%' || UPPER(:V_SEARCH) || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN CM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN CM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN CM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'AUD_NAME' AND: ORDER_DIR = 'ASC' THEN CM.AUD_NAME END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'AUD_NAME' AND: ORDER_DIR = 'DESC' THEN CM.AUD_NAME END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'NAME_TYPE' AND: ORDER_DIR = 'ASC' THEN CM.NAME_TYPE END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'NAME_TYPE' AND: ORDER_DIR = 'DESC' THEN CM.NAME_TYPE END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM7";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM7Detail(XElement request)
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
                cmd.CommandText = "SELECT ID, OFFICE_ID, STATISTIC_PERIOD, AUD_NAME, NAME_TYPE, REFERENCE_COUNT, BUDGET_EXPENSES, HUMAN_RESOURCES, PLANNED_COMPLETED, OTHER, COMP_DONE, COMP_PROGRESS, RESOLVED_COMPLAINT_COUNT, REFERENCE_NOT_COMP, EXEC_TYPE, CREATED_DATE, CREATED_BY, IS_ACTIVE " +
                    "FROM CM7_DATA WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("P_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM7Detail";

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
        public static DataResponse CM7Update(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("CM7");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.CM7_DATA " +
                    "SET OFFICE_ID = :P_OFFICE_ID, STATISTIC_PERIOD = :P_STATISTIC_PERIOD, AUD_NAME = :P_AUD_NAME, NAME_TYPE = :P_NAME_TYPE, REFERENCE_COUNT = :P_REFERENCE_COUNT, BUDGET_EXPENSES = :P_BUDGET_EXPENSES, HUMAN_RESOURCES = :P_HUMAN_RESOURCES, PLANNED_COMPLETED = :P_PLANNED_COMPLETED, OTHER = :P_OTHER, COMP_DONE = :P_COMP_DONE, COMP_PROGRESS = :P_COMP_PROGRESS, RESOLVED_COMPLAINT_COUNT = :P_RESOLVED_COMPLAINT_COUNT, REFERENCE_NOT_COMP = :P_REFERENCE_NOT_COMP, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_OFFICE_ID", OracleDbType.Int32).Value = elem.Element("OFFICE_ID").Value;
                cmd.Parameters.Add(":P_STATISTIC_PERIOD", OracleDbType.Int32).Value = elem.Element("STATISTIC_PERIOD").Value;
                cmd.Parameters.Add(":P_AUD_NAME", OracleDbType.Varchar2).Value = elem.Element("AUD_NAME")?.Value;
                cmd.Parameters.Add(":P_NAME_TYPE", OracleDbType.Varchar2).Value = elem.Element("NAME_TYPE")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_COUNT", OracleDbType.Int32).Value = elem.Element("REFERENCE_COUNT")?.Value;
                cmd.Parameters.Add(":P_BUDGET_EXPENSES", OracleDbType.Int32).Value = elem.Element("BUDGET_EXPENSES")?.Value;
                cmd.Parameters.Add(":P_HUMAN_RESOURCES", OracleDbType.Int32).Value = elem.Element("HUMAN_RESOURCES")?.Value;
                cmd.Parameters.Add(":P_PLANNED_COMPLETED", OracleDbType.Int32).Value = elem.Element("PLANNED_COMPLETED")?.Value;
                cmd.Parameters.Add(":P_OTHER", OracleDbType.Int32).Value = elem.Element("OTHER")?.Value;
                cmd.Parameters.Add(":P_COMP_DONE", OracleDbType.Int32).Value = elem.Element("COMP_DONE")?.Value;
                cmd.Parameters.Add(":P_COMP_PROGRESS", OracleDbType.Int32).Value = elem.Element("COMP_PROGRESS")?.Value;
                cmd.Parameters.Add(":P_RESOLVED_COMPLAINT_COUNT", OracleDbType.Int32).Value = elem.Element("RESOLVED_COMPLAINT_COUNT")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_NOT_COMP", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_NOT_COMP")?.Value;
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = elem.Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM7Insert(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("CM7");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO AUD_STAT.CM7_DATA ( OFFICE_ID, STATISTIC_PERIOD, AUD_NAME, NAME_TYPE, REFERENCE_COUNT, BUDGET_EXPENSES, HUMAN_RESOURCES, PLANNED_COMPLETED, OTHER, COMP_DONE, COMP_PROGRESS, RESOLVED_COMPLAINT_COUNT, REFERENCE_NOT_COMP, IS_ACTIVE, CREATED_BY, CREATED_DATE) " +
                    "VALUES(:P_OFFICE_ID, :P_STATISTIC_PERIOD, :P_AUD_NAME, :P_NAME_TYPE, :P_REFERENCE_COUNT, :P_BUDGET_EXPENSES, :P_HUMAN_RESOURCES, :P_PLANNED_COMPLETED, :P_OTHER, :P_COMP_DONE, :P_COMP_PROGRESS, :P_RESOLVED_COMPLAINT_COUNT, :P_REFERENCE_NOT_COMP, :P_IS_ACTIVE, :P_CREATED_BY, :P_CREATED_DATE)";

                // Set parameters
                cmd.Parameters.Add(":P_OFFICE_ID", OracleDbType.Int32).Value = elem.Element("OFFICE_ID")?.Value;
                cmd.Parameters.Add(":P_STATISTIC_PERIOD", OracleDbType.Int32).Value = elem.Element("STATISTIC_PERIOD")?.Value;
                cmd.Parameters.Add(":P_AUD_NAME", OracleDbType.Varchar2).Value = elem.Element("AUD_NAME")?.Value;
                cmd.Parameters.Add(":P_NAME_TYPE", OracleDbType.Varchar2).Value = elem.Element("NAME_TYPE")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_COUNT", OracleDbType.Int32).Value = elem.Element("REFERENCE_COUNT")?.Value;
                cmd.Parameters.Add(":P_BUDGET_EXPENSES", OracleDbType.Int32).Value = elem.Element("BUDGET_EXPENSES")?.Value;
                cmd.Parameters.Add(":P_HUMAN_RESOURCES", OracleDbType.Int32).Value = elem.Element("HUMAN_RESOURCES")?.Value;
                cmd.Parameters.Add(":P_PLANNED_COMPLETED", OracleDbType.Int32).Value = elem.Element("PLANNED_COMPLETED")?.Value;
                cmd.Parameters.Add(":P_OTHER", OracleDbType.Int32).Value = elem.Element("OTHER")?.Value;
                cmd.Parameters.Add(":P_COMP_DONE", OracleDbType.Int32).Value = elem.Element("COMP_DONE")?.Value;
                cmd.Parameters.Add(":P_COMP_PROGRESS", OracleDbType.Int32).Value = elem.Element("COMP_PROGRESS")?.Value;
                cmd.Parameters.Add(":P_RESOLVED_COMPLAINT_COUNT", OracleDbType.Int32).Value = elem.Element("RESOLVED_COMPLAINT_COUNT")?.Value;
                cmd.Parameters.Add(":P_REFERENCE_NOT_COMP", OracleDbType.Varchar2).Value = elem.Element("REFERENCE_NOT_COMP")?.Value;
                cmd.Parameters.Add(":P_IS_ACTIVE", OracleDbType.Int32).Value = elem.Element("IS_ACTIVE")?.Value;
                cmd.Parameters.Add(":P_CREATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_CREATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM7Delete(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.CM7_DATA " +
                    "SET IS_ACTIVE = 0, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("UPDATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Устгалаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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
                XElement req = request.Element("Parameters").Element("Request");
                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(CM.ID) " +
                    "FROM AUD_STAT.CM8_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD"; 

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                DataTable dtTableCount = new DataTable();
                dtTableCount.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();

                dtTableCount.TableName = "RowCount";
                var count = dtTableCount.Rows[0][0];
                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CM.ID, CM.OFFICE_ID, RD.DEPARTMENT_NAME, CM.STATISTIC_PERIOD, RP.PERIOD_LABEL, CM.APPROVED_BUDGET,CM.PERFORMANCE_BUDGET, CM.WORKERS, CM.APPROVED_NUMBERS, CM.DIRECTING_STAFF, CM.SENIOR_AUDITOR_ANALYST,CM.AUDITOR_ANALYST, CM.OTHER_OFFICE, CM.EDU_DOCTOR, CM.EDU_MAGISTR, CM.EDU_BAKLAVR, CM.EDU_AMONGST, CM.EDU_JUNIOR_AMONGST, CM.PRO_ACCOUNTANT, CM.ACCOUNTANT_ECONOMIST, CM.LAWYER, CM.INGENER, CM.OTHER_PROF, CM.STUDY_COUNT, CM.INCLUDED_MAN, CM.ONLINE_STUDY_COUNT, CM.LOCAL_STUDY_COUNT, CM.AUDIT_STUDY_COUNT, CM.FOREIGN_STUDY_COUNT, CM.FOREIGN_MAN_COUNT, CM.INSIDE_STUDY_COUNT, CM.INSIDE_MAN_COUNT, CM.ORG_STUDY_COUNT, CM.ORG_MAN_COUNT, CM.RESEARCH_ALL, CM.PUBLISHED_REPORT, CM.NEWS_ARTICLE, CM.TV_NEWS_BROADCAST, CM.ORG_NEWS, CM.WEB_ACCESS, CM.RECEIVED_ALL, CM.TAB_WORKERS, CM.TAB_SKILLS,CM.AUDIT_LET, CM.RECEIVED_OTHER, CM.DECIDED_TIME, CM.DEC_EXPIRED, CM.DEC_UNEXPIRED, CM.EXEC_TYPE,CM.CREATED_DATE, CM.IS_ACTIVE, CM.CREATED_BY,CM.UPDATED_BY, CM.UPDATED_DATE " +
                    "FROM AUD_STAT.CM8_DATA CM " +
                    "INNER JOIN AUD_REG.REF_DEPARTMENT RD ON CM.OFFICE_ID = RD.DEPARTMENT_ID " +
                    "INNER JOIN AUD_STAT.REF_PERIOD RP ON CM.STATISTIC_PERIOD = RP.ID " +
                    "WHERE(:V_USER_TYPE != 'Branch_Auditor' OR(:V_USER_TYPE = 'Branch_Auditor' AND CM.OFFICE_ID = :V_DEPARTMENT)) " +
                    "AND CM.STATISTIC_PERIOD = :V_PERIOD " +
                    "ORDER BY " +
                    "CASE WHEN: ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN CM.ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN CM.OFFICE_ID END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN CM.OFFICE_ID END DESC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'ASC' THEN RP.PERIOD_LABEL END ASC, " +
                    "CASE WHEN :ORDER_NAME = 'PERIOD_LABEL' AND: ORDER_DIR = 'DESC' THEN RP.PERIOD_LABEL END DESC " +
                    "OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters  
                cmd.Parameters.Add(":V_USER_TYPE", OracleDbType.Varchar2, request.Element("Parameters").Element("USER_TYPE").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Int32, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM8";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM8Detail(XElement request)
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
                cmd.CommandText = "SELECT ID, OFFICE_ID, STATISTIC_PERIOD, APPROVED_BUDGET, PERFORMANCE_BUDGET, WORKERS, APPROVED_NUMBERS, DIRECTING_STAFF, SENIOR_AUDITOR_ANALYST, AUDITOR_ANALYST, OTHER_OFFICE, EDU_DOCTOR, EDU_MAGISTR, EDU_BAKLAVR, EDU_AMONGST, EDU_JUNIOR_AMONGST, PRO_ACCOUNTANT, ACCOUNTANT_ECONOMIST, LAWYER, INGENER, OTHER_PROF, STUDY_COUNT, INCLUDED_MAN, ONLINE_STUDY_COUNT, LOCAL_STUDY_COUNT, AUDIT_STUDY_COUNT, FOREIGN_STUDY_COUNT, FOREIGN_MAN_COUNT, INSIDE_STUDY_COUNT, INSIDE_MAN_COUNT, ORG_STUDY_COUNT, ORG_MAN_COUNT, RESEARCH_ALL, PUBLISHED_REPORT, NEWS_ARTICLE, TV_NEWS_BROADCAST, ORG_NEWS, WEB_ACCESS, RECEIVED_ALL, TAB_WORKERS, TAB_SKILLS, AUDIT_LET, RECEIVED_OTHER, DECIDED_TIME, DEC_EXPIRED, DEC_UNEXPIRED, EXEC_TYPE, CREATED_DATE, CREATED_BY, UPDATED_BY, UPDATED_DATE, IS_ACTIVE " +
                    "FROM CM8_DATA WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32, request.Element("Parameters").Element("P_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "CM8Detail";

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
        public static DataResponse CM8Update(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("CM8");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.CM8_DATA " +
                    "SET OFFICE_ID = :P_OFFICE_ID, STATISTIC_PERIOD = :P_STATISTIC_PERIOD, APPROVED_BUDGET = :P_APPROVED_BUDGET, PERFORMANCE_BUDGET = :P_PERFORMANCE_BUDGET, WORKERS = :P_WORKERS, APPROVED_NUMBERS = :P_APPROVED_NUMBERS, DIRECTING_STAFF = :P_DIRECTING_STAFF, SENIOR_AUDITOR_ANALYST = :P_SENIOR_AUDITOR_ANALYST, AUDITOR_ANALYST = :P_AUDITOR_ANALYST, OTHER_OFFICE = :P_OTHER_OFFICE, EDU_DOCTOR = :P_EDU_DOCTOR, EDU_MAGISTR = :P_EDU_MAGISTR, EDU_BAKLAVR = :P_EDU_BAKLAVR, EDU_AMONGST = :P_EDU_AMONGST, EDU_JUNIOR_AMONGST = :P_EDU_JUNIOR_AMONGST, PRO_ACCOUNTANT = :P_PRO_ACCOUNTANT, ACCOUNTANT_ECONOMIST = :P_ACCOUNTANT_ECONOMIST, LAWYER = :P_LAWYER, INGENER = :P_INGENER, OTHER_PROF = :P_OTHER_PROF, STUDY_COUNT = :P_STUDY_COUNT, INCLUDED_MAN = :P_INCLUDED_MAN, ONLINE_STUDY_COUNT = :P_ONLINE_STUDY_COUNT, LOCAL_STUDY_COUNT = :P_LOCAL_STUDY_COUNT, AUDIT_STUDY_COUNT = :P_AUDIT_STUDY_COUNT, FOREIGN_STUDY_COUNT = :P_FOREIGN_STUDY_COUNT, FOREIGN_MAN_COUNT = :P_FOREIGN_MAN_COUNT, INSIDE_STUDY_COUNT = :P_INSIDE_STUDY_COUNT, INSIDE_MAN_COUNT = :P_INSIDE_MAN_COUNT, ORG_STUDY_COUNT = :P_ORG_STUDY_COUNT, ORG_MAN_COUNT = :P_ORG_MAN_COUNT, RESEARCH_ALL = :P_RESEARCH_ALL, PUBLISHED_REPORT = :P_PUBLISHED_REPORT, NEWS_ARTICLE = :P_NEWS_ARTICLE, TV_NEWS_BROADCAST = :P_TV_NEWS_BROADCAST, ORG_NEWS = :P_ORG_NEWS, WEB_ACCESS = :P_WEB_ACCESS, RECEIVED_ALL = :P_RECEIVED_ALL, TAB_WORKERS = :P_TAB_WORKERS, TAB_SKILLS = :P_TAB_SKILLS, AUDIT_LET = :P_AUDIT_LET, RECEIVED_OTHER = :P_RECEIVED_OTHER, DECIDED_TIME = :P_DECIDED_TIME, DEC_EXPIRED = :P_DEC_EXPIRED, DEC_UNEXPIRED = :P_DEC_UNEXPIRED, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_OFFICE_ID", OracleDbType.Int32).Value = elem.Element("OFFICE_ID").Value;
                cmd.Parameters.Add(":P_STATISTIC_PERIOD", OracleDbType.Int32).Value = elem.Element("STATISTIC_PERIOD").Value;
                cmd.Parameters.Add(":P_APPROVED_BUDGET", OracleDbType.Int32).Value = elem.Element("APPROVED_BUDGET")?.Value;
                cmd.Parameters.Add(":P_PERFORMANCE_BUDGET", OracleDbType.Int32).Value = elem.Element("PERFORMANCE_BUDGET")?.Value;
                cmd.Parameters.Add(":P_WORKERS", OracleDbType.Int32).Value = elem.Element("WORKERS")?.Value;
                cmd.Parameters.Add(":P_APPROVED_NUMBERS", OracleDbType.Int32).Value = elem.Element("APPROVED_NUMBERS")?.Value;
                cmd.Parameters.Add(":P_DIRECTING_STAFF", OracleDbType.Int32).Value = elem.Element("DIRECTING_STAFF")?.Value;
                cmd.Parameters.Add(":P_SENIOR_AUDITOR_ANALYST", OracleDbType.Int32).Value = elem.Element("SENIOR_AUDITOR_ANALYST")?.Value;
                cmd.Parameters.Add(":P_AUDITOR_ANALYST", OracleDbType.Int32).Value = elem.Element("AUDITOR_ANALYST")?.Value;
                cmd.Parameters.Add(":P_OTHER_OFFICE", OracleDbType.Int32).Value = elem.Element("OTHER_OFFICE")?.Value;
                cmd.Parameters.Add(":P_EDU_DOCTOR", OracleDbType.Int32).Value = elem.Element("EDU_DOCTOR")?.Value;
                cmd.Parameters.Add(":P_EDU_MAGISTR", OracleDbType.Int32).Value = elem.Element("EDU_MAGISTR")?.Value;
                cmd.Parameters.Add(":P_EDU_BAKLAVR", OracleDbType.Int32).Value = elem.Element("EDU_BAKLAVR")?.Value;
                cmd.Parameters.Add(":P_EDU_AMONGST", OracleDbType.Int32).Value = elem.Element("EDU_AMONGST")?.Value;
                cmd.Parameters.Add(":P_EDU_JUNIOR_AMONGST", OracleDbType.Int32).Value = elem.Element("EDU_JUNIOR_AMONGST")?.Value;
                cmd.Parameters.Add(":P_PRO_ACCOUNTANT", OracleDbType.Int32).Value = elem.Element("PRO_ACCOUNTANT")?.Value;
                cmd.Parameters.Add(":P_ACCOUNTANT_ECONOMIST", OracleDbType.Int32).Value = elem.Element("ACCOUNTANT_ECONOMIST")?.Value;
                cmd.Parameters.Add(":P_LAWYER", OracleDbType.Int32).Value = elem.Element("LAWYER")?.Value;
                cmd.Parameters.Add(":P_INGENER", OracleDbType.Int32).Value = elem.Element("INGENER")?.Value;
                cmd.Parameters.Add(":P_OTHER_PROF", OracleDbType.Int32).Value = elem.Element("OTHER_PROF")?.Value;
                cmd.Parameters.Add(":P_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_INCLUDED_MAN", OracleDbType.Int32).Value = elem.Element("INCLUDED_MAN")?.Value;
                cmd.Parameters.Add(":P_ONLINE_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("ONLINE_STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_LOCAL_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("LOCAL_STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_AUDIT_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("AUDIT_STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_FOREIGN_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("FOREIGN_STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_FOREIGN_MAN_COUNT", OracleDbType.Int32).Value = elem.Element("FOREIGN_MAN_COUNT")?.Value;
                cmd.Parameters.Add(":P_INSIDE_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("INSIDE_STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_INSIDE_MAN_COUNT", OracleDbType.Int32).Value = elem.Element("INSIDE_MAN_COUNT")?.Value;
                cmd.Parameters.Add(":P_ORG_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("ORG_STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_ORG_MAN_COUNT", OracleDbType.Int32).Value = elem.Element("ORG_MAN_COUNT")?.Value;
                cmd.Parameters.Add(":P_RESEARCH_ALL", OracleDbType.Int32).Value = elem.Element("RESEARCH_ALL")?.Value;
                cmd.Parameters.Add(":P_PUBLISHED_REPORT", OracleDbType.Int32).Value = elem.Element("PUBLISHED_REPORT")?.Value;
                cmd.Parameters.Add(":P_NEWS_ARTICLE", OracleDbType.Int32).Value = elem.Element("NEWS_ARTICLE")?.Value;
                cmd.Parameters.Add(":P_TV_NEWS_BROADCAST", OracleDbType.Int32).Value = elem.Element("TV_NEWS_BROADCAST")?.Value;
                cmd.Parameters.Add(":P_ORG_NEWS", OracleDbType.Int32).Value = elem.Element("ORG_NEWS")?.Value;
                cmd.Parameters.Add(":P_WEB_ACCESS", OracleDbType.Int32).Value = elem.Element("WEB_ACCESS")?.Value;
                cmd.Parameters.Add(":P_RECEIVED_ALL", OracleDbType.Int32).Value = elem.Element("RECEIVED_ALL")?.Value;
                cmd.Parameters.Add(":P_TAB_WORKERS", OracleDbType.Int32).Value = elem.Element("TAB_WORKERS")?.Value;
                cmd.Parameters.Add(":P_TAB_SKILLS", OracleDbType.Int32).Value = elem.Element("TAB_SKILLS")?.Value;
                cmd.Parameters.Add(":P_AUDIT_LET", OracleDbType.Int32).Value = elem.Element("AUDIT_LET")?.Value;
                cmd.Parameters.Add(":P_RECEIVED_OTHER", OracleDbType.Int32).Value = elem.Element("RECEIVED_OTHER")?.Value;
                cmd.Parameters.Add(":P_DECIDED_TIME", OracleDbType.Int32).Value = elem.Element("DECIDED_TIME")?.Value;
                cmd.Parameters.Add(":P_DEC_EXPIRED", OracleDbType.Int32).Value = elem.Element("DEC_EXPIRED")?.Value;
                cmd.Parameters.Add(":P_DEC_UNEXPIRED", OracleDbType.Int32).Value = elem.Element("DEC_UNEXPIRED")?.Value;
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = elem.Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM8Insert(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                XElement elem = request.Element("Parameters").Element("CM8");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO AUD_STAT.CM8_DATA ( OFFICE_ID, STATISTIC_PERIOD, APPROVED_BUDGET, PERFORMANCE_BUDGET, WORKERS, APPROVED_NUMBERS, DIRECTING_STAFF, SENIOR_AUDITOR_ANALYST, AUDITOR_ANALYST, OTHER_OFFICE, EDU_DOCTOR, EDU_MAGISTR, EDU_BAKLAVR, EDU_AMONGST, EDU_JUNIOR_AMONGST, PRO_ACCOUNTANT, ACCOUNTANT_ECONOMIST, LAWYER, INGENER, OTHER_PROF, STUDY_COUNT, INCLUDED_MAN, ONLINE_STUDY_COUNT, LOCAL_STUDY_COUNT, AUDIT_STUDY_COUNT, FOREIGN_STUDY_COUNT, FOREIGN_MAN_COUNT, INSIDE_STUDY_COUNT, INSIDE_MAN_COUNT, ORG_STUDY_COUNT, ORG_MAN_COUNT, RESEARCH_ALL, PUBLISHED_REPORT, NEWS_ARTICLE, TV_NEWS_BROADCAST, ORG_NEWS, WEB_ACCESS, RECEIVED_ALL, TAB_WORKERS, TAB_SKILLS, AUDIT_LET, RECEIVED_OTHER, DECIDED_TIME, DEC_EXPIRED, DEC_UNEXPIRED, IS_ACTIVE, CREATED_BY, CREATED_DATE) " +
                    "VALUES(:P_OFFICE_ID, :P_STATISTIC_PERIOD, :P_APPROVED_BUDGET, :P_PERFORMANCE_BUDGET, :P_WORKERS, :P_APPROVED_NUMBERS, :P_DIRECTING_STAFF, :P_SENIOR_AUDITOR_ANALYST, :P_AUDITOR_ANALYST, :P_OTHER_OFFICE, :P_EDU_DOCTOR, :P_EDU_MAGISTR, :P_EDU_BAKLAVR, :P_EDU_AMONGST, :P_EDU_JUNIOR_AMONGST, :P_PRO_ACCOUNTANT, :P_ACCOUNTANT_ECONOMIST, :P_LAWYER, :P_INGENER, :P_OTHER_PROF, :P_STUDY_COUNT, :P_INCLUDED_MAN, :P_ONLINE_STUDY_COUNT, :P_LOCAL_STUDY_COUNT, :P_AUDIT_STUDY_COUNT, :P_FOREIGN_STUDY_COUNT, :P_FOREIGN_MAN_COUNT, :P_INSIDE_STUDY_COUNT, :P_INSIDE_MAN_COUNT, :P_ORG_STUDY_COUNT, :P_ORG_MAN_COUNT, :P_RESEARCH_ALL, :P_PUBLISHED_REPORT, :P_NEWS_ARTICLE, :P_TV_NEWS_BROADCAST, :P_ORG_NEWS, :P_WEB_ACCESS, :P_RECEIVED_ALL, :P_TAB_WORKERS, :P_TAB_SKILLS, :P_AUDIT_LET, :P_RECEIVED_OTHER, :P_DECIDED_TIME, :P_DEC_EXPIRED, :P_DEC_UNEXPIRED, :P_IS_ACTIVE, :P_CREATED_BY, :P_CREATED_DATE)";

                // Set parameters
                cmd.Parameters.Add(":P_OFFICE_ID", OracleDbType.Int32).Value = elem.Element("OFFICE_ID")?.Value;
                cmd.Parameters.Add(":P_STATISTIC_PERIOD", OracleDbType.Int32).Value = elem.Element("STATISTIC_PERIOD")?.Value;
                cmd.Parameters.Add(":P_APPROVED_BUDGET", OracleDbType.Int32).Value = elem.Element("APPROVED_BUDGET")?.Value;
                cmd.Parameters.Add(":P_PERFORMANCE_BUDGET", OracleDbType.Int32).Value = elem.Element("PERFORMANCE_BUDGET")?.Value;
                cmd.Parameters.Add(":P_WORKERS", OracleDbType.Int32).Value = elem.Element("WORKERS")?.Value;
                cmd.Parameters.Add(":P_APPROVED_NUMBERS", OracleDbType.Int32).Value = elem.Element("APPROVED_NUMBERS")?.Value;
                cmd.Parameters.Add(":P_DIRECTING_STAFF", OracleDbType.Int32).Value = elem.Element("DIRECTING_STAFF")?.Value;
                cmd.Parameters.Add(":P_SENIOR_AUDITOR_ANALYST", OracleDbType.Int32).Value = elem.Element("SENIOR_AUDITOR_ANALYST")?.Value;
                cmd.Parameters.Add(":P_AUDITOR_ANALYST", OracleDbType.Int32).Value = elem.Element("AUDITOR_ANALYST")?.Value;
                cmd.Parameters.Add(":P_OTHER_OFFICE", OracleDbType.Int32).Value = elem.Element("OTHER_OFFICE")?.Value;
                cmd.Parameters.Add(":P_EDU_DOCTOR", OracleDbType.Int32).Value = elem.Element("EDU_DOCTOR")?.Value;
                cmd.Parameters.Add(":P_EDU_MAGISTR", OracleDbType.Int32).Value = elem.Element("EDU_MAGISTR")?.Value;
                cmd.Parameters.Add(":P_EDU_BAKLAVR", OracleDbType.Int32).Value = elem.Element("EDU_BAKLAVR")?.Value;
                cmd.Parameters.Add(":P_EDU_AMONGST", OracleDbType.Int32).Value = elem.Element("EDU_AMONGST")?.Value;
                cmd.Parameters.Add(":P_EDU_JUNIOR_AMONGST", OracleDbType.Int32).Value = elem.Element("EDU_JUNIOR_AMONGST")?.Value;
                cmd.Parameters.Add(":P_PRO_ACCOUNTANT", OracleDbType.Int32).Value = elem.Element("PRO_ACCOUNTANT")?.Value;
                cmd.Parameters.Add(":P_ACCOUNTANT_ECONOMIST", OracleDbType.Int32).Value = elem.Element("ACCOUNTANT_ECONOMIST")?.Value;
                cmd.Parameters.Add(":P_LAWYER", OracleDbType.Int32).Value = elem.Element("LAWYER")?.Value;
                cmd.Parameters.Add(":P_INGENER", OracleDbType.Int32).Value = elem.Element("INGENER")?.Value;
                cmd.Parameters.Add(":P_OTHER_PROF", OracleDbType.Int32).Value = elem.Element("OTHER_PROF")?.Value;
                cmd.Parameters.Add(":P_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_INCLUDED_MAN", OracleDbType.Int32).Value = elem.Element("INCLUDED_MAN")?.Value;
                cmd.Parameters.Add(":P_ONLINE_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("ONLINE_STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_LOCAL_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("LOCAL_STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_AUDIT_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("AUDIT_STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_FOREIGN_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("FOREIGN_STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_FOREIGN_MAN_COUNT", OracleDbType.Int32).Value = elem.Element("FOREIGN_MAN_COUNT")?.Value;
                cmd.Parameters.Add(":P_INSIDE_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("INSIDE_STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_INSIDE_MAN_COUNT", OracleDbType.Int32).Value = elem.Element("INSIDE_MAN_COUNT")?.Value;
                cmd.Parameters.Add(":P_ORG_STUDY_COUNT", OracleDbType.Int32).Value = elem.Element("ORG_STUDY_COUNT")?.Value;
                cmd.Parameters.Add(":P_ORG_MAN_COUNT", OracleDbType.Int32).Value = elem.Element("ORG_MAN_COUNT")?.Value;
                cmd.Parameters.Add(":P_RESEARCH_ALL", OracleDbType.Int32).Value = elem.Element("RESEARCH_ALL")?.Value;
                cmd.Parameters.Add(":P_PUBLISHED_REPORT", OracleDbType.Int32).Value = elem.Element("PUBLISHED_REPORT")?.Value;
                cmd.Parameters.Add(":P_NEWS_ARTICLE", OracleDbType.Int32).Value = elem.Element("NEWS_ARTICLE")?.Value;
                cmd.Parameters.Add(":P_TV_NEWS_BROADCAST", OracleDbType.Int32).Value = elem.Element("TV_NEWS_BROADCAST")?.Value;
                cmd.Parameters.Add(":P_ORG_NEWS", OracleDbType.Int32).Value = elem.Element("ORG_NEWS")?.Value;
                cmd.Parameters.Add(":P_WEB_ACCESS", OracleDbType.Int32).Value = elem.Element("WEB_ACCESS")?.Value;
                cmd.Parameters.Add(":P_RECEIVED_ALL", OracleDbType.Int32).Value = elem.Element("RECEIVED_ALL")?.Value;
                cmd.Parameters.Add(":P_TAB_WORKERS", OracleDbType.Int32).Value = elem.Element("TAB_WORKERS")?.Value;
                cmd.Parameters.Add(":P_TAB_SKILLS", OracleDbType.Int32).Value = elem.Element("TAB_SKILLS")?.Value;
                cmd.Parameters.Add(":P_AUDIT_LET", OracleDbType.Int32).Value = elem.Element("AUDIT_LET")?.Value;
                cmd.Parameters.Add(":P_RECEIVED_OTHER", OracleDbType.Int32).Value = elem.Element("RECEIVED_OTHER")?.Value;
                cmd.Parameters.Add(":P_DECIDED_TIME", OracleDbType.Int32).Value = elem.Element("DECIDED_TIME")?.Value;
                cmd.Parameters.Add(":P_DEC_EXPIRED", OracleDbType.Int32).Value = elem.Element("DEC_EXPIRED")?.Value;
                cmd.Parameters.Add(":P_DEC_UNEXPIRED", OracleDbType.Int32).Value = elem.Element("DEC_UNEXPIRED")?.Value;
                cmd.Parameters.Add(":P_IS_ACTIVE", OracleDbType.Int32).Value = elem.Element("IS_ACTIVE")?.Value;
                cmd.Parameters.Add(":P_CREATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_CREATED_DATE", OracleDbType.Varchar2).Value = elem.Element("CREATED_DATE")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Хадгаллаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse CM8Delete(XElement request)
        {
            DataResponse response = new DataResponse();

            // Open a connection to the database
            OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
            con.Open();

            // Create and execute the command
            OracleCommand cmd = con.CreateCommand();
            OracleTransaction transaction;

            // Start a local transaction
            transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE AUD_STAT.CM8_DATA " +
                    "SET IS_ACTIVE = 0, UPDATED_BY = :P_UPDATED_BY, UPDATED_DATE = :P_UPDATED_DATE " +
                    "WHERE ID = :P_ID";

                // Set parameters
                cmd.Parameters.Add(":P_UPDATED_BY", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_UPDATED_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("UPDATED_DATE")?.Value;
                cmd.Parameters.Add(":P_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ID")?.Value;

                int rowsUpdated = cmd.ExecuteNonQuery();
                transaction.Commit();
                bool responseVal = rowsUpdated == 0 ? false : true;
                cmd.Dispose();
                con.Close();

                response.CreateResponse(responseVal, string.Empty, "Устгалаа");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.CreateResponse(ex);
            }

            return response;
        }
        #endregion

        #region Shilendans
        public static DataResponse MirrorOrgList(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["RegConfig"]);
                con.Open();

                XElement req = request.Element("Parameters").Element("Request");

                //RowCount
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_OPEN_ORG_COUNT";

                OracleParameter retParam = cmd.Parameters.Add(":Ret_val",
                    OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);
                cmd.Parameters.Add(":DEP_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                //cmd.Parameters.Add(":P_STATUS", OracleDbType.Varchar2, req.Element("V_STATUS") != null && !string.IsNullOrEmpty(req.Element("V_STATUS").Value) ? req.Element("V_STATUS")?.Value : null, System.Data.ParameterDirection.Input);
                //cmd.Parameters.Add(":P_VIOLATION", OracleDbType.Varchar2, req.Element("V_VIOLATION") != null && !string.IsNullOrEmpty(req.Element("V_VIOLATION").Value) ? req.Element("V_VIOLATION")?.Value.Replace(",", "%") : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_SEARCH", OracleDbType.Varchar2, req.Element("Search") != null && !string.IsNullOrEmpty(req.Element("Search").Value) ? req.Element("Search")?.Value : null, System.Data.ParameterDirection.Input);

                cmd.ExecuteNonQuery();

                cmd.Dispose();

                //Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT A.OPEN_ID, C.BUDGET_SHORT_NAME, A.OPEN_ENT_BUDGET_PARENT, D.DEPARTMENT_NAME, A.OPEN_ENT_NAME, A.OPEN_ENT_REGISTER_NO, " +
                                  "CASE WHEN A.OPEN_ENT_GROUP_ID IN (1,2) THEN 'Маягт 1' WHEN A.OPEN_ENT_GROUP_ID = 3 THEN 'Маягт 4' END MAYGT, " +
                                  "(SELECT IS_FINISH FROM AUD_MIRRORACC.SHILENDANSDATA WHERE MDCODE IN(107, 165) AND IS_FINISH = 1 AND ORGID = A.OPEN_ID) IS_FINISHED, " +
                                  "(SELECT IS_PRINT FROM AUD_MIRRORACC.SHILENDANSDATA WHERE MDCODE = 107 AND IS_PRINT = 1 AND ORGID = A.OPEN_ID) IS_PRINTED, " +
                                  "(SELECT K.USER_NAME FROM AUD_MIRRORACC.SHILENDANSDATA J INNER JOIN AUD_REG.SYSTEM_USER K ON J.INSERTUSERID = K.USER_ID WHERE J.MDCODE = 106 AND J.ORGID = A.OPEN_ID) USER_NAME, " +
                                  "(SELECT TO_CHAR(J.INSERTDATE, 'YYYY-MM-DD') INSERTDATE FROM AUD_MIRRORACC.SHILENDANSDATA J INNER JOIN AUD_REG.SYSTEM_USER K ON J.INSERTUSERID = K.USER_ID WHERE J.MDCODE = 106 AND J.ORGID = A.OPEN_ID) INSERTDATE " +
                                  "FROM AUD_MIRRORACC.OPENACC_ENTITY A " +
                                  "LEFT JOIN AUD_MIRRORACC.SHILENDANSDATA B ON A.OPEN_ID = B.ORGID " +
                                  "INNER JOIN AUD_MIRRORACC.REF_BUDGET_TYPE C ON A.OPEN_ENT_BUDGET_TYPE = C.BUDGET_TYPE_ID " +
                                  "INNER JOIN AUD_REG.REF_DEPARTMENT D ON A.OPEN_ENT_DEPARTMENT_ID = D.DEPARTMENT_ID " +
                                  "WHERE A.IS_ACTIVE = 1 AND A.OPEN_ENT_GROUP_ID IN(1,2,3) AND (:DEPARTMENT_ID IN (2, 101) OR (:DEPARTMENT_ID NOT IN(2, 101) AND A.OPEN_ENT_DEPARTMENT_ID = :DEPARTMENT_ID)) " +
                                  "AND (:V_DEPARTMENT IS NULL OR A.OPEN_ENT_DEPARTMENT_ID = :V_DEPARTMENT) " +
                                  "AND (:V_BUDGET_TYPE IS NULL OR (A.OPEN_ENT_BUDGET_TYPE IN (:V_BUDGET_TYPE))) " +
                                  "AND (:V_SEARCH IS NULL OR UPPER(C.BUDGET_SHORT_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                                  "OR UPPER(D.DEPARTMENT_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                                  "OR UPPER(A.OPEN_ENT_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' OR UPPER(A.OPEN_ENT_REGISTER_NO) LIKE '%'||UPPER(:V_SEARCH)||'%') " +
                                  "GROUP BY A.OPEN_ID, C.BUDGET_SHORT_NAME, A.OPEN_ENT_BUDGET_PARENT, D.DEPARTMENT_NAME, A.OPEN_ENT_NAME, A.OPEN_ENT_REGISTER_NO, A.OPEN_ENT_GROUP_ID " +
                                  "ORDER BY " +
                                  "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN A.OPEN_ID END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'DEPARTMENT_NAME' AND :ORDER_DIR = 'ASC' THEN D.DEPARTMENT_NAME END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'DEPARTMENT_NAME' AND :ORDER_DIR = 'DESC' THEN D.DEPARTMENT_NAME END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'BUDGET_SHORT_NAME' AND :ORDER_DIR = 'ASC' THEN C.BUDGET_SHORT_NAME END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'BUDGET_SHORT_NAME' AND :ORDER_DIR = 'DESC' THEN C.BUDGET_SHORT_NAME END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'MAYGT' AND :ORDER_DIR = 'ASC' THEN MAYGT END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'MAYGT' AND :ORDER_DIR = 'DESC' THEN MAYGT END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'IS_FINISHED' AND :ORDER_DIR = 'ASC' THEN IS_FINISHED END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'IS_FINISHED' AND :ORDER_DIR = 'DESC' THEN IS_FINISHED END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'IS_PRINTED' AND :ORDER_DIR = 'ASC' THEN IS_PRINTED END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'IS_PRINTED' AND :ORDER_DIR = 'DESC' THEN IS_PRINTED END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'USER_NAME' AND :ORDER_DIR = 'ASC' THEN USER_NAME END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'USER_NAME' AND :ORDER_DIR = 'DESC' THEN USER_NAME END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'INSERTDATE' AND :ORDER_DIR = 'ASC' THEN INSERTDATE END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'INSERTDATE' AND :ORDER_DIR = 'DESC' THEN INSERTDATE END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'OPEN_ENT_NAME' AND :ORDER_DIR = 'ASC' THEN A.OPEN_ENT_NAME END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'OPEN_ENT_NAME' AND :ORDER_DIR = 'DESC' THEN A.OPEN_ENT_NAME END DESC " +
                                  "OFFSET((: PAGENUMBER /:PAGESIZE) * :PAGESIZE) ROWS " +
                                  "FETCH NEXT: PAGESIZE ROWS ONLY";

                //"SELECT R1.ORG_ID, R1.ORG_DEPARTMENT_ID, RD.DEPARTMENT_NAME, R1.ORG_REGISTER_NO, R1.ORG_NAME, R1.ORG_CODE, R1.ORG_BUDGET_TYPE_ID, RB.BUDGET_TYPE_NAME, R1.ORG_CONCENTRATOR_ID, R2.ORG_NAME AS RG_CONCENTRATOR_NAME, R1.VIOLATION_DETAIL, R1.ORG_STATUS_ID, RS.STATUS_NAME, R1.INFORMATION_DETAIL, " +
                //"(SELECT IS_FINISH FROM AUD_MIRRORACC.SHILENDANSDATA WHERE MDCODE = 109 AND IS_FINISH = 1 AND ORGID = R1.ORG_ID) TAB1_IS_FINISH, " +
                //"(SELECT IS_FINISH FROM AUD_MIRRORACC.SHILENDANSDATA WHERE MDCODE = 165 AND IS_FINISH = 1 AND ORGID = R1.ORG_ID) TAB2_IS_FINISH, " +
                //"(SELECT PROJECT_IS_ACTIVE FROM AUD_MIRRORACC.ORG_PROJECT_LIST WHERE MDCODE = 172 AND PROJECT_IS_ACTIVE = 1 AND ORGID = R1.ORG_ID) TAB3_IS_FINISH " +
                //"FROM AUD_REG.REG_ORGANIZATION R1 " +
                //"INNER JOIN AUD_REG.REF_DEPARTMENT RD ON R1.ORG_DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                //"INNER JOIN AUD_REG.REF_BUDGET_TYPE RB ON R1.ORG_BUDGET_TYPE_ID = RB.BUDGET_TYPE_ID " +
                //"LEFT JOIN AUD_REG.REG_ORGANIZATION R2 ON R1.ORG_CONCENTRATOR_ID = R2.ORG_ID " +
                //"INNER JOIN AUD_REG.REF_STATUS RS ON R1.ORG_STATUS_ID = RS.STATUS_ID " +
                //"WHERE R1.IS_ACTIVE = 1 AND (:DEP_ID = 2 OR (:DEP_ID !=2 AND R1.ORG_DEPARTMENT_ID = :DEP_ID)) " +
                //"AND (:V_DEPARTMENT IS NULL OR R1.ORG_DEPARTMENT_ID = :V_DEPARTMENT) " +
                //"AND (:V_STATUS IS NULL OR (R1.ORG_STATUS_ID IN (:V_STATUS))) " +
                //"AND (:V_BUDGET_TYPE IS NULL OR (R1.ORG_BUDGET_TYPE_ID IN (:V_BUDGET_TYPE))) " +
                //"AND (:V_VIOLATION IS NULL OR (R1.VIOLATION_DETAIL LIKE '%'||:V_VIOLATION||'%')) " +
                //"AND (:V_SEARCH IS NULL OR UPPER(RD.DEPARTMENT_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                //"OR UPPER(R1.ORG_REGISTER_NO) LIKE '%'||UPPER(:V_SEARCH)||'%' OR UPPER(R1.ORG_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                //"OR UPPER(R1.ORG_CODE) LIKE '%'||UPPER(:V_SEARCH)||'%' OR UPPER(RB.BUDGET_TYPE_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                //"OR UPPER(R1.VIOLATION_DETAIL) LIKE '%'||UPPER(:V_SEARCH)||'%' OR UPPER(R1.INFORMATION_DETAIL) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                //"OR UPPER(RS.STATUS_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%') " +
                //"ORDER BY " +
                //"CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN R1.ORG_ID END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'DEPARTMENT_NAME' AND :ORDER_DIR = 'ASC' THEN RD.DEPARTMENT_NAME END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'DEPARTMENT_NAME' AND :ORDER_DIR = 'DESC' THEN RD.DEPARTMENT_NAME END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'ORG_REGISTER_NO' AND :ORDER_DIR = 'ASC' THEN R1.ORG_REGISTER_NO END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'ORG_REGISTER_NO' AND :ORDER_DIR = 'DESC' THEN R1.ORG_REGISTER_NO END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'ORG_NAME' AND :ORDER_DIR = 'ASC' THEN R1.ORG_NAME END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'ORG_NAME' AND :ORDER_DIR = 'DESC' THEN R1.ORG_NAME END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'ORG_CODE' AND :ORDER_DIR = 'ASC' THEN R1.ORG_CODE END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'ORG_CODE' AND :ORDER_DIR = 'DESC' THEN R1.ORG_CODE END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'BUDGET_TYPE_NAME' AND :ORDER_DIR = 'ASC' THEN RB.BUDGET_TYPE_NAME END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'BUDGET_TYPE_NAME' AND :ORDER_DIR = 'DESC' THEN RB.BUDGET_TYPE_NAME END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'CONCENTRATOR_NAME' AND :ORDER_DIR = 'ASC' THEN R2.ORG_NAME END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'CONCENTRATOR_NAME' AND :ORDER_DIR = 'DESC' THEN R2.ORG_NAME END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'VIOLATION_DETAIL' AND :ORDER_DIR = 'ASC' THEN R1.VIOLATION_DETAIL END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'VIOLATION_DETAIL' AND :ORDER_DIR = 'DESC' THEN R1.VIOLATION_DETAIL END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'STATUS_NAME' AND :ORDER_DIR = 'ASC' THEN RS.STATUS_NAME END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'STATUS_NAME' AND :ORDER_DIR = 'DESC' THEN RS.STATUS_NAME END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'INFORMATION_DETAIL' AND :ORDER_DIR = 'ASC' THEN R1.INFORMATION_DETAIL END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'INFORMATION_DETAIL' AND :ORDER_DIR = 'DESC' THEN R1.INFORMATION_DETAIL END DESC " +
                //"OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                //"FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                //cmd.Parameters.Add(":V_STATUS", OracleDbType.Varchar2, req.Element("V_STATUS")?.Value, System.Data.ParameterDirection.Input);
                //cmd.Parameters.Add(":V_VIOLATION", OracleDbType.Varchar2, req.Element("V_VIOLATION")?.Value.Replace(",", "%"), System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_BUDGET_TYPE", OracleDbType.Varchar2, req.Element("V_BUDGET_TYPE")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "MirroraccOrgList";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", retParam.Value));
                response.CreateResponse(xmlResponseData);

            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }

        public static DataResponse MirrorHakOrgList(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["RegConfig"]);
                con.Open();

                XElement req = request.Element("Parameters").Element("Request");

                //RowCount
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_OPEN_HAK_ORG_COUNT";

                OracleParameter retParam = cmd.Parameters.Add(":Ret_val",
                    OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);
                cmd.Parameters.Add(":UserID", OracleDbType.Int32, request.Element("Parameters").Element("UserID")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                //cmd.Parameters.Add(":P_STATUS", OracleDbType.Varchar2, req.Element("V_STATUS") != null && !string.IsNullOrEmpty(req.Element("V_STATUS").Value) ? req.Element("V_STATUS")?.Value : null, System.Data.ParameterDirection.Input);
                //cmd.Parameters.Add(":P_VIOLATION", OracleDbType.Varchar2, req.Element("V_VIOLATION") != null && !string.IsNullOrEmpty(req.Element("V_VIOLATION").Value) ? req.Element("V_VIOLATION")?.Value.Replace(",", "%") : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_SEARCH", OracleDbType.Varchar2, req.Element("Search") != null && !string.IsNullOrEmpty(req.Element("Search").Value) ? req.Element("Search")?.Value : null, System.Data.ParameterDirection.Input);

                cmd.ExecuteNonQuery();

                cmd.Dispose();

                //Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT A.OPEN_ID, C.BUDGET_SHORT_NAME, A.OPEN_ENT_BUDGET_PARENT, D.DEPARTMENT_NAME, A.OPEN_ENT_NAME, A.OPEN_ENT_REGISTER_NO, " +
                                  "CASE WHEN A.OPEN_ENT_GROUP_ID IN (1,2) THEN 'Маягт 1' WHEN A.OPEN_ENT_GROUP_ID = 3 THEN 'Маягт 4' END MAYGT, " +
                                  "(SELECT IS_FINISH FROM AUD_MIRRORACC.SHILENDANSDATA WHERE MDCODE IN(107, 165) AND IS_FINISH = 1 AND ORGID = A.OPEN_ID) IS_FINISHED, " +
                                  "(SELECT IS_PRINT FROM AUD_MIRRORACC.SHILENDANSDATA WHERE MDCODE = 107 AND IS_PRINT = 1 AND ORGID = A.OPEN_ID) IS_PRINTED, " +
                                  "(SELECT K.USER_NAME FROM AUD_MIRRORACC.SHILENDANSDATA J INNER JOIN AUD_REG.SYSTEM_USER K ON J.INSERTUSERID = K.USER_ID WHERE J.MDCODE = 106 AND J.ORGID = A.OPEN_ID) USER_NAME, " +
                                  "(SELECT TO_CHAR(J.INSERTDATE, 'YYYY-MM-DD') INSERTDATE FROM AUD_MIRRORACC.SHILENDANSDATA J INNER JOIN AUD_REG.SYSTEM_USER K ON J.INSERTUSERID = K.USER_ID WHERE J.MDCODE = 106 AND J.ORGID = A.OPEN_ID) INSERTDATE " +
                                  "FROM AUD_MIRRORACC.OPENACC_ENTITY A " +
                                  "LEFT JOIN AUD_MIRRORACC.SHILENDANSDATA B ON A.OPEN_ID = B.ORGID " +
                                  "INNER JOIN AUD_MIRRORACC.REF_BUDGET_TYPE C ON A.OPEN_ENT_BUDGET_TYPE = C.BUDGET_TYPE_ID " +
                                  "INNER JOIN AUD_REG.REF_DEPARTMENT D ON A.OPEN_ENT_DEPARTMENT_ID = D.DEPARTMENT_ID " +
                                  "INNER JOIN AUD_REG.SYSTEM_USER_DEPARTMENT E ON A.OPEN_ENT_DEPARTMENT_ID = E.DEP_ID " +
                                  "WHERE A.IS_ACTIVE = 1 AND A.OPEN_ENT_GROUP_ID IN(1,2,3) AND E.DEP_USER_ID = :UserID " +
                                  "AND (:V_DEPARTMENT IS NULL OR A.OPEN_ENT_DEPARTMENT_ID = :V_DEPARTMENT) " +
                                  "AND (:V_BUDGET_TYPE IS NULL OR (A.OPEN_ENT_BUDGET_TYPE IN (:V_BUDGET_TYPE))) " +
                                  "AND (:V_SEARCH IS NULL OR UPPER(C.BUDGET_SHORT_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                                  "OR UPPER(D.DEPARTMENT_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                                  "OR UPPER(A.OPEN_ENT_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' OR UPPER(A.OPEN_ENT_REGISTER_NO) LIKE '%'||UPPER(:V_SEARCH)||'%') " +
                                  "GROUP BY A.OPEN_ID, C.BUDGET_SHORT_NAME, A.OPEN_ENT_BUDGET_PARENT, D.DEPARTMENT_NAME, A.OPEN_ENT_NAME, A.OPEN_ENT_REGISTER_NO, A.OPEN_ENT_GROUP_ID " +
                                  "ORDER BY " +
                                  "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN D.DEPARTMENT_NAME END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'DEPARTMENT_NAME' AND :ORDER_DIR = 'ASC' THEN D.DEPARTMENT_NAME END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'DEPARTMENT_NAME' AND :ORDER_DIR = 'DESC' THEN D.DEPARTMENT_NAME END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'BUDGET_SHORT_NAME' AND :ORDER_DIR = 'ASC' THEN C.BUDGET_SHORT_NAME END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'BUDGET_SHORT_NAME' AND :ORDER_DIR = 'DESC' THEN C.BUDGET_SHORT_NAME END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'MAYGT' AND :ORDER_DIR = 'ASC' THEN MAYGT END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'MAYGT' AND :ORDER_DIR = 'DESC' THEN MAYGT END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'IS_FINISHED' AND :ORDER_DIR = 'ASC' THEN IS_FINISHED END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'IS_FINISHED' AND :ORDER_DIR = 'DESC' THEN IS_FINISHED END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'IS_PRINTED' AND :ORDER_DIR = 'ASC' THEN IS_PRINTED END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'IS_PRINTED' AND :ORDER_DIR = 'DESC' THEN IS_PRINTED END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'USER_NAME' AND :ORDER_DIR = 'ASC' THEN USER_NAME END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'USER_NAME' AND :ORDER_DIR = 'DESC' THEN USER_NAME END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'INSERTDATE' AND :ORDER_DIR = 'ASC' THEN INSERTDATE END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'INSERTDATE' AND :ORDER_DIR = 'DESC' THEN INSERTDATE END DESC, " +
                                  "CASE WHEN :ORDER_NAME = 'OPEN_ENT_NAME' AND :ORDER_DIR = 'ASC' THEN A.OPEN_ENT_NAME END ASC, " +
                                  "CASE WHEN :ORDER_NAME = 'OPEN_ENT_NAME' AND :ORDER_DIR = 'DESC' THEN A.OPEN_ENT_NAME END DESC " +
                                  "OFFSET((: PAGENUMBER /:PAGESIZE) * :PAGESIZE) ROWS " +
                                  "FETCH NEXT: PAGESIZE ROWS ONLY";

                //"SELECT R1.ORG_ID, R1.ORG_DEPARTMENT_ID, RD.DEPARTMENT_NAME, R1.ORG_REGISTER_NO, R1.ORG_NAME, R1.ORG_CODE, R1.ORG_BUDGET_TYPE_ID, RB.BUDGET_TYPE_NAME, R1.ORG_CONCENTRATOR_ID, R2.ORG_NAME AS RG_CONCENTRATOR_NAME, R1.VIOLATION_DETAIL, R1.ORG_STATUS_ID, RS.STATUS_NAME, R1.INFORMATION_DETAIL, " +
                //"(SELECT IS_FINISH FROM AUD_MIRRORACC.SHILENDANSDATA WHERE MDCODE = 109 AND IS_FINISH = 1 AND ORGID = R1.ORG_ID) TAB1_IS_FINISH, " +
                //"(SELECT IS_FINISH FROM AUD_MIRRORACC.SHILENDANSDATA WHERE MDCODE = 165 AND IS_FINISH = 1 AND ORGID = R1.ORG_ID) TAB2_IS_FINISH, " +
                //"(SELECT PROJECT_IS_ACTIVE FROM AUD_MIRRORACC.ORG_PROJECT_LIST WHERE MDCODE = 172 AND PROJECT_IS_ACTIVE = 1 AND ORGID = R1.ORG_ID) TAB3_IS_FINISH " +
                //"FROM AUD_REG.REG_ORGANIZATION R1 " +
                //"INNER JOIN AUD_REG.REF_DEPARTMENT RD ON R1.ORG_DEPARTMENT_ID = RD.DEPARTMENT_ID " +
                //"INNER JOIN AUD_REG.REF_BUDGET_TYPE RB ON R1.ORG_BUDGET_TYPE_ID = RB.BUDGET_TYPE_ID " +
                //"LEFT JOIN AUD_REG.REG_ORGANIZATION R2 ON R1.ORG_CONCENTRATOR_ID = R2.ORG_ID " +
                //"INNER JOIN AUD_REG.REF_STATUS RS ON R1.ORG_STATUS_ID = RS.STATUS_ID " +
                //"WHERE R1.IS_ACTIVE = 1 AND (:DEP_ID = 2 OR (:DEP_ID !=2 AND R1.ORG_DEPARTMENT_ID = :DEP_ID)) " +
                //"AND (:V_DEPARTMENT IS NULL OR R1.ORG_DEPARTMENT_ID = :V_DEPARTMENT) " +
                //"AND (:V_STATUS IS NULL OR (R1.ORG_STATUS_ID IN (:V_STATUS))) " +
                //"AND (:V_BUDGET_TYPE IS NULL OR (R1.ORG_BUDGET_TYPE_ID IN (:V_BUDGET_TYPE))) " +
                //"AND (:V_VIOLATION IS NULL OR (R1.VIOLATION_DETAIL LIKE '%'||:V_VIOLATION||'%')) " +
                //"AND (:V_SEARCH IS NULL OR UPPER(RD.DEPARTMENT_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                //"OR UPPER(R1.ORG_REGISTER_NO) LIKE '%'||UPPER(:V_SEARCH)||'%' OR UPPER(R1.ORG_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                //"OR UPPER(R1.ORG_CODE) LIKE '%'||UPPER(:V_SEARCH)||'%' OR UPPER(RB.BUDGET_TYPE_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                //"OR UPPER(R1.VIOLATION_DETAIL) LIKE '%'||UPPER(:V_SEARCH)||'%' OR UPPER(R1.INFORMATION_DETAIL) LIKE '%'||UPPER(:V_SEARCH)||'%' " +
                //"OR UPPER(RS.STATUS_NAME) LIKE '%'||UPPER(:V_SEARCH)||'%') " +
                //"ORDER BY " +
                //"CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN R1.ORG_ID END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'DEPARTMENT_NAME' AND :ORDER_DIR = 'ASC' THEN RD.DEPARTMENT_NAME END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'DEPARTMENT_NAME' AND :ORDER_DIR = 'DESC' THEN RD.DEPARTMENT_NAME END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'ORG_REGISTER_NO' AND :ORDER_DIR = 'ASC' THEN R1.ORG_REGISTER_NO END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'ORG_REGISTER_NO' AND :ORDER_DIR = 'DESC' THEN R1.ORG_REGISTER_NO END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'ORG_NAME' AND :ORDER_DIR = 'ASC' THEN R1.ORG_NAME END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'ORG_NAME' AND :ORDER_DIR = 'DESC' THEN R1.ORG_NAME END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'ORG_CODE' AND :ORDER_DIR = 'ASC' THEN R1.ORG_CODE END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'ORG_CODE' AND :ORDER_DIR = 'DESC' THEN R1.ORG_CODE END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'BUDGET_TYPE_NAME' AND :ORDER_DIR = 'ASC' THEN RB.BUDGET_TYPE_NAME END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'BUDGET_TYPE_NAME' AND :ORDER_DIR = 'DESC' THEN RB.BUDGET_TYPE_NAME END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'CONCENTRATOR_NAME' AND :ORDER_DIR = 'ASC' THEN R2.ORG_NAME END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'CONCENTRATOR_NAME' AND :ORDER_DIR = 'DESC' THEN R2.ORG_NAME END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'VIOLATION_DETAIL' AND :ORDER_DIR = 'ASC' THEN R1.VIOLATION_DETAIL END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'VIOLATION_DETAIL' AND :ORDER_DIR = 'DESC' THEN R1.VIOLATION_DETAIL END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'STATUS_NAME' AND :ORDER_DIR = 'ASC' THEN RS.STATUS_NAME END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'STATUS_NAME' AND :ORDER_DIR = 'DESC' THEN RS.STATUS_NAME END DESC, " +
                //"CASE WHEN :ORDER_NAME = 'INFORMATION_DETAIL' AND :ORDER_DIR = 'ASC' THEN R1.INFORMATION_DETAIL END ASC, " +
                //"CASE WHEN :ORDER_NAME = 'INFORMATION_DETAIL' AND :ORDER_DIR = 'DESC' THEN R1.INFORMATION_DETAIL END DESC " +
                //"OFFSET ((:PAGENUMBER/:PAGESIZE) * :PAGESIZE) ROWS " +
                //"FETCH NEXT :PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters
                cmd.Parameters.Add(":UserID", OracleDbType.Int32, request.Element("Parameters").Element("UserID").Value, System.Data.ParameterDirection.Input);

                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                //cmd.Parameters.Add(":V_STATUS", OracleDbType.Varchar2, req.Element("V_STATUS")?.Value, System.Data.ParameterDirection.Input);
                //cmd.Parameters.Add(":V_VIOLATION", OracleDbType.Varchar2, req.Element("V_VIOLATION")?.Value.Replace(",", "%"), System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_BUDGET_TYPE", OracleDbType.Varchar2, req.Element("V_BUDGET_TYPE")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_NAME", OracleDbType.Varchar2, req.Element("OrderName")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":ORDER_DIR", OracleDbType.Varchar2, req.Element("OrderDir")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGENUMBER", OracleDbType.Int32, req.Element("PageNumber").Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":PAGESIZE", OracleDbType.Int32, req.Element("PageSize").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "MirroraccHakOrgList";

                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                xmlResponseData.Add(new XElement("RowCount", retParam.Value));
                response.CreateResponse(xmlResponseData);

            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }

        public static DataResponse MirrorOrgDetail(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["MirroraccConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT A.OPEN_ID ,B.BUDGET_SHORT_NAME, A.OPEN_ENT_BUDGET_PARENT, A.OPEN_ENT_NAME, A.OPEN_HEAD_ROLE, A.OPEN_HEAD_NAME, A.OPEN_HEAD_PHONE, A.OPEN_ACC_ROLE, A.OPEN_ACC_NAME, A.OPEN_ACC_PHONE, A.OPEN_ENT_GROUP_ID " +
                                  "FROM AUD_MIRRORACC.OPENACC_ENTITY A " +
                                  "INNER JOIN AUD_MIRRORACC.REF_BUDGET_TYPE B ON A.OPEN_ENT_BUDGET_TYPE = B.BUDGET_TYPE_ID " +
                                  "WHERE A.IS_ACTIVE = 1 " +
                                  "AND A.OPEN_ID = :OPEN_ID ";

                // Set parameters
                cmd.Parameters.Add(":OPEN_ID", OracleDbType.Int32, request.Element("Parameters").Element("OPEN_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "MirrorOrgDetail";

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
        public static DataResponse Table1List(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["MirroraccConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "SELECT B.MD_CODE, B.MD_LAWS_NUM, B.MD_NAME, B.MD_TIME, B.TAB_ID, A.DATA01, A.DATA02, A.DATA03 " +
                //    "FROM SHILENDANSDATA A " +
                //    "RIGHT JOIN MD_DESC B ON A.MDCODE = B.MD_CODE " +
                //    "ORDER BY B.MD_CODE ASC ";

                cmd.CommandText = "SELECT MD_CODE, MD_LAWS_NUM, MD_NAME, MD_TIME, TAB_ID " +
                    "FROM MD_DESC " +
                    "ORDER BY MD_CODE ASC ";

                // Set parameters
                //cmd.Parameters.Add("");

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "Table1List";

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

        public static DataResponse MirrDataList(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["MirroraccConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT B.MD_CODE, B.MD_LAWS_NUM, B.MD_TIME, B.MD_NAME, A.DATA01, A.DATA02 " +
                        "FROM AUD_MIRRORACC.SHILENDANSDATA A " +
                        "JOIN AUD_MIRRORACC.MD_DESC B ON A.MDCODE = B.MD_CODE " +
                        "WHERE A.ORGID = :ORGID " +
                        "ORDER BY B.MD_CODE ";

                // Set parameters
                cmd.Parameters.Add(":ORGID", OracleDbType.Varchar2, request.Element("Parameters").Element("ORGID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "MirrDataList";

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

        public static DataResponse PrintDataList(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["MirroraccConfig"]);
                con.Open();

                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_ORG_PRINTED";

                OracleParameter retParam = cmd.Parameters.Add(":Ret_val",
                    OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);
                cmd.Parameters.Add(":OPEN_ID", OracleDbType.Int32, request.Element("Parameters").Element("ORGID")?.Value, System.Data.ParameterDirection.Input);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT B.MD_CODE ,B.MD_TIME, CASE WHEN B.IS_PREW = 0 THEN B.MD_NAME END PARENT_NAME, B.MD_LAWS_NUM || ' ' || B.MD_NAME AS MD_NAME, " +
                                "	   CASE WHEN A.DATA01 IS NOT NULL THEN 1 ELSE NULL END MEDEELEH_TOO_HEMJEE, " +
                                "	   CASE WHEN A.DATA01 = 1 THEN 1 ELSE NULL END MEDEELSEN, + " +
                                "	   CASE WHEN A.DATA01 = 2 THEN 1 ELSE NULL END MEDEELEEGUI, " +
                                "	   CASE WHEN A.DATA01 = 4 THEN 1 ELSE NULL END SHAARDLAGAGUI, " +
                                "	   CASE WHEN A.DATA01 = 3 THEN 1 ELSE NULL END HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(CASE WHEN A.DATA01 IN(1, 3) THEN 100.00 ELSE NULL END, 2) PRECENT1, " +
                                "	   ROUND(CASE WHEN A.DATA01 = 1 THEN 100.00 ELSE NULL END, 2) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA A " +
                                "INNER JOIN AUD_MIRRORACC.MD_DESC B ON A.MDCODE = B.MD_CODE " +
                                "WHERE A.ORGID = :ORGID AND A.MDCODE IN(1, 2, 35) " +
                                "UNION ALL " +
                                "SELECT B.MD_CODE ,B.MD_TIME, (SELECT MD_NAME || ' ' || MD_LAWS_NUM AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 3) PARENT_NAME, B.MD_LAWS_NUM || ' ' || B.MD_NAME AS MD_NAME, " +
                                "	   CASE WHEN A.DATA01 IS NOT NULL THEN 1 ELSE NULL END MEDEELEH_TOO_HEMJEE, " +
                                "	   CASE WHEN A.DATA01 = 1 THEN 1 ELSE NULL END MEDEELSEN, " +
                                "	   CASE WHEN A.DATA01 = 2 THEN 1 ELSE NULL END MEDEELEEGUI, " +
                                "	   CASE WHEN A.DATA01 = 4 THEN 1 ELSE NULL END SHAARDLAGAGUI, " +
                                "	   CASE WHEN A.DATA01 = 3 THEN 1 ELSE NULL END HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(CASE WHEN A.DATA01 IN(1, 3) THEN 100.00 ELSE NULL END, 2) PRECENT1, " +
                                "	   ROUND(CASE WHEN A.DATA01 = 1 THEN 100.00 ELSE NULL END, 2) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA A " +
                                "INNER JOIN AUD_MIRRORACC.MD_DESC B ON A.MDCODE = B.MD_CODE " +
                                "WHERE A.ORGID = :ORGID AND A.MDCODE IN(4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19) " +
                                "UNION ALL " +
                                "SELECT B.MD_CODE ,B.MD_TIME, CASE WHEN B.IS_PREW = 0 THEN B.MD_NAME END PARENT_NAME, B.MD_LAWS_NUM || ' ' || B.MD_NAME AS MD_NAME, " +
                                "	   CASE WHEN A.DATA01 IS NOT NULL THEN 1 ELSE NULL END MEDEELEH_TOO_HEMJEE, " +
                                "	   CASE WHEN A.DATA01 = 1 THEN 1 ELSE NULL END MEDEELSEN, " +
                                "	   CASE WHEN A.DATA01 = 2 THEN 1 ELSE NULL END MEDEELEEGUI, " +
                                "	   CASE WHEN A.DATA01 = 4 THEN 1 ELSE NULL END SHAARDLAGAGUI, " +
                                "	   CASE WHEN A.DATA01 = 3 THEN 1 ELSE NULL END HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(CASE WHEN A.DATA01 IN(1, 3) THEN 100.00 ELSE NULL END, 2) PRECENT1, " +
                                "	   ROUND(CASE WHEN A.DATA01 = 1 THEN 100.00 ELSE NULL END, 2) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA A " +
                                "INNER JOIN AUD_MIRRORACC.MD_DESC B ON A.MDCODE = B.MD_CODE " +
                                "WHERE A.ORGID = :ORGID AND A.MDCODE IN(20, 21, 22, 23, 24, 25, 26) " +
                                "UNION ALL " +
                                "SELECT B.MD_CODE ,B.MD_TIME, (SELECT MD_NAME || ' ' || MD_LAWS_NUM AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 27) PARENT_NAME, B.MD_LAWS_NUM || ' ' || B.MD_NAME AS MD_NAME, " +
                                "	   CASE WHEN A.DATA01 IS NOT NULL THEN 1 ELSE NULL END MEDEELEH_TOO_HEMJEE, " +
                                "	   CASE WHEN A.DATA01 = 1 THEN 1 ELSE NULL END MEDEELSEN, " +
                                "	   CASE WHEN A.DATA01 = 2 THEN 1 ELSE NULL END MEDEELEEGUI, " +
                                "	   CASE WHEN A.DATA01 = 4 THEN 1 ELSE NULL END SHAARDLAGAGUI, " +
                                "	   CASE WHEN A.DATA01 = 3 THEN 1 ELSE NULL END HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(CASE WHEN A.DATA01 IN(1, 3) THEN 100.00 ELSE NULL END, 2) PRECENT1, " +
                                "	   ROUND(CASE WHEN A.DATA01 = 1 THEN 100.00 ELSE NULL END, 2) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA A " +
                                "INNER JOIN AUD_MIRRORACC.MD_DESC B ON A.MDCODE = B.MD_CODE " +
                                "WHERE A.ORGID = :ORGID AND A.MDCODE IN(28,29,30,31) " +
                                "UNION ALL " +
                                "SELECT B.MD_CODE ,B.MD_TIME, CASE WHEN B.IS_PREW = 0 THEN B.MD_NAME END PARENT_NAME, B.MD_LAWS_NUM || ' ' || B.MD_NAME AS MD_NAME, " +
                                "	   CASE WHEN A.DATA01 IS NOT NULL THEN 1 ELSE NULL END MEDEELEH_TOO_HEMJEE, " +
                                "	   CASE WHEN A.DATA01 = 1 THEN 1 ELSE NULL END MEDEELSEN, " +
                                "	   CASE WHEN A.DATA01 = 2 THEN 1 ELSE NULL END MEDEELEEGUI, " +
                                "	   CASE WHEN A.DATA01 = 4 THEN 1 ELSE NULL END SHAARDLAGAGUI, " +
                                "	   CASE WHEN A.DATA01 = 3 THEN 1 ELSE NULL END HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(CASE WHEN A.DATA01 IN(1, 3) THEN 100.00 ELSE NULL END, 2) PRECENT1, " +
                                "	   ROUND(CASE WHEN A.DATA01 = 1 THEN 100.00 ELSE NULL END, 2) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA A " +
                                "INNER JOIN AUD_MIRRORACC.MD_DESC B ON A.MDCODE = B.MD_CODE " +
                                "WHERE A.ORGID = :ORGID AND A.MDCODE IN(32,33,34) " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME , AA.MD_NAME, " +
                                "    SUM(CASE WHEN MDCODE = 37 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "    SUM(CASE WHEN MDCODE = 37 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 39 THEN DATA01 END) MEDEELSEN, " +
                                "    SUM(CASE WHEN MDCODE = 39 THEN DATA01 END) MEDEELEEGUI, " +
                                "    SUM(CASE WHEN MDCODE = 37 THEN NULL END) SHAARDLAGAGUI, " +
                                "    SUM(CASE WHEN MDCODE = 41 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "    ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 39 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 37 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 37 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "    ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 41 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 37 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 37 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 37) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 36) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 38 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 38 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 40 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 40 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 38 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 42 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 40 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 38 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 38 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 42 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 38 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 38 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 38) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 36) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "    SUM(CASE WHEN MDCODE = 43 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "    SUM(CASE WHEN MDCODE = 43 THEN DATA01 END) MEDEELSEN, " +
                                "    SUM(CASE WHEN MDCODE = 43 THEN NULL END) MEDEELEEGUI, " +
                                "    SUM(CASE WHEN MDCODE = 43 THEN NULL END) SHAARDLAGAGUI, " +
                                "    SUM(CASE WHEN MDCODE = 43 THEN NULL END) HUGATSAA_HOTSROOSON, " +
                                "    ROUND(SUM(CASE WHEN MDCODE = 43 AND DATA01 != 0 THEN 100 ELSE NULL END), 1) PRECENT1, " +
                                "    ROUND(SUM(CASE WHEN MDCODE = 43 AND DATA01 != 0 THEN 100 ELSE NULL END), 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 43) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 36) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 44 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 44 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 44 THEN NULL END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 44 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 44 THEN NULL END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(SUM(CASE WHEN MDCODE = 44 AND DATA01 != 0 THEN 100 ELSE 0 END), 1) PRECENT1, " +
                                "     ROUND(SUM(CASE WHEN MDCODE = 44 AND DATA01 != 0 THEN 100 ELSE 0 END), 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 44) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 36) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 46 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 46 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 48 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 48 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 46 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 50 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 48 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 46 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 46 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 50 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 46 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 46 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 46) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 45) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 47 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 47 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 49 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 49 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 47 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 51 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 49 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 47 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 47 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 51 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 47 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 47 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 47) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 45) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 53 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 53 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 55 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 55 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 53 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 57 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 55 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 53 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 53 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 57 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 53 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 53 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 53) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 52) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 54 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 54 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 56 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 56 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 54 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 58 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 56 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 54 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 54 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 58 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 54 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 54 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 54) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 52) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 60 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 60 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 62 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 62 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 60 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 64 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 62 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 60 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 60 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 64 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 60 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 60 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 60) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 59) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 61 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 61 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 63 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 63 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 61 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 65 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 63 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 61 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 61 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 65 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 61 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 61 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 61) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 59) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT B.MD_CODE ,B.MD_TIME, CASE WHEN B.IS_PREW = 0 THEN B.MD_NAME END PARENT_NAME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME, " +
                                "     CASE WHEN A.DATA01 IS NOT NULL THEN 1 ELSE NULL END MEDEELEH_TOO_HEMJEE, " +
                                "     CASE WHEN A.DATA01 = 1 THEN 1 ELSE NULL END MEDEELSEN, " +
                                "     CASE WHEN A.DATA01 = 2 THEN 1 ELSE NULL END MEDEELEEGUI, " +
                                "     CASE WHEN A.DATA01 = 4 THEN 1 ELSE NULL END SHAARDLAGAGUI, " +
                                "     CASE WHEN A.DATA01 = 3 THEN 1 ELSE NULL END HUGATSAA_HOTSROOSON, " +
                                "     ROUND(CASE WHEN A.DATA01 IN(1, 3) THEN 100.00 ELSE 0 END, 2) PRECENT1, " +
                                "     ROUND(CASE WHEN A.DATA01 = 1 THEN 100.00 ELSE 0 END, 2) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA A " +
                                "INNER JOIN AUD_MIRRORACC.MD_DESC B ON A.MDCODE = B.MD_CODE " +
                                "WHERE ORGID = :ORGID AND A.MDCODE = 66 " +
                                "UNION ALL " +
                                "SELECT B.MD_CODE ,B.MD_TIME, CASE WHEN B.IS_PREW = 0 THEN B.MD_NAME END PARENT_NAME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME, " +
                                "     CASE WHEN A.DATA01 IS NOT NULL THEN 1 ELSE NULL END MEDEELEH_TOO_HEMJEE, " +
                                "     CASE WHEN A.DATA01 = 1 THEN 1 ELSE NULL END MEDEELSEN, " +
                                "     CASE WHEN A.DATA01 = 2 THEN 1 ELSE NULL END MEDEELEEGUI, " +
                                "     CASE WHEN A.DATA01 = 4 THEN 1 ELSE NULL END SHAARDLAGAGUI, " +
                                "     CASE WHEN A.DATA01 = 3 THEN 1 ELSE NULL END HUGATSAA_HOTSROOSON, " +
                                "     ROUND(CASE WHEN A.DATA01 IN(1, 3) THEN 100.00 ELSE 0 END, 2) PRECENT1, " +
                                "     ROUND(CASE WHEN A.DATA01 = 1 THEN 100.00 ELSE 0 END, 2) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA A " +
                                "INNER JOIN AUD_MIRRORACC.MD_DESC B ON A.MDCODE = B.MD_CODE " +
                                "WHERE ORGID = :ORGID AND A.MDCODE = 67 " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 69 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 69 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 71 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 71 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 69 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 73 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 71 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 69 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 69 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 73 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 69 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 69 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 69) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 68) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 70 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 70 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 72 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 72 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 70 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 74 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 72 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 70 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 70 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 74 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 70 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 70 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 70) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 68) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 76 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 76 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 78 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 78 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 76 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 80 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 78 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 76 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 76 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 80 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 76 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 76 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 76) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 75) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 77 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 77 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 79 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 79 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 77 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 81 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 79 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 77 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 77 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 81 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 77 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 77 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 77) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 75) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 83 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 83 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 85 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 85 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 83 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 87 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 85 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 83 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 83 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 87 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 83 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 83 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 83) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 82) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 84 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 84 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 86 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 86 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 84 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 88 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 86 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 84 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 84 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 88 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 84 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 84 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 84) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 82) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 90 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 90 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 92 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 92 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 90 THEN 0 END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 94 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 92 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 90 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 90 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 94 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 90 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 90 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 90) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 89) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 91 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 91 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 93 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 93 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 91 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 95 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 93 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 91 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 91 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 95 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE  = 91 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 91 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 91) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 89) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 97 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 97 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 99 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 99 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 97 THEN 0 END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 101 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 99 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 97 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 97 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 101 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 97 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 97 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 97) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 96) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 98 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 98 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 100 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 100 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 98 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 102 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 100 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 98 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 98 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 102 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 98 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 98 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 98) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 96) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "     SUM(CASE WHEN MDCODE = 104 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "     SUM(CASE WHEN MDCODE = 104 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 105 THEN DATA01 END) MEDEELSEN, " +
                                "     SUM(CASE WHEN MDCODE = 105 THEN DATA01 END) MEDEELEEGUI, " +
                                "     SUM(CASE WHEN MDCODE = 104 THEN NULL END) SHAARDLAGAGUI, " +
                                "     SUM(CASE WHEN MDCODE = 106 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 105 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 104 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 104 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "     ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 106 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 104 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 104 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 104) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 103) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME, AA.PARENT_NAME, AA.MD_NAME, " +
                                "SUM(CASE WHEN MDCODE = 3 THEN NULL END) MEDEELEH_TOO_HEMJEE, " +
                                "SUM(CASE WHEN MDCODE = 3 THEN NULL END) MEDEELSEN, " +
                                "SUM(CASE WHEN MDCODE = 3 THEN NULL END) MEDEELEEGUI, " +
                                "SUM(CASE WHEN MDCODE = 3 THEN NULL END) SHAARDLAGAGUI, " +
                                "SUM(CASE WHEN MDCODE = 3 THEN NULL END) HUGATSAA_HOTSROOSON, " +
                                "SUM(CASE WHEN DATA01 IN(1, 3) AND MDCODE IN(1, 2, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 28, 29, 30, 31, 32, 33, 34, 35, 66, 67) THEN 100 ELSE NULL END) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 39 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 37 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 37 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 40 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 38 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 38 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(SUM(CASE WHEN MDCODE = 43 AND DATA01 != 0 THEN 100 ELSE NULL END), 1) + " +
                                "ROUND(SUM(CASE WHEN MDCODE = 44 AND DATA01 != 0 THEN 100 ELSE 0 END), 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 48 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 46 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 46 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 49 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 47 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 47 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 55 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 53 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 53 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 56 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 54 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 54 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 62 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 60 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 60 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 63 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 61 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 61 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 71 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 69 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 69 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 72 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 70 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 70 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 78 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 76 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 76 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 79 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 77 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 77 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 85 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 83 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 83 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 86 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 84 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 84 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 92 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 90 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 90 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 93 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 91 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 91 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 99 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 97 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 97 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 100 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 98 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 98 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 105 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 104 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 104 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "SUM(CASE WHEN DATA01 = 1 AND MDCODE IN(1, 2, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 28, 29, 30, 31, 32, 33, 34, 35, 66, 67) THEN 100 ELSE NULL END) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 41 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 37 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 37 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 42 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 38 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 38 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(SUM(CASE WHEN MDCODE = 43 AND DATA01 != 0 THEN 100 ELSE NULL END), 1) + " +
                                "ROUND(SUM(CASE WHEN MDCODE = 44 AND DATA01 != 0 THEN 100 ELSE 0 END), 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 50 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 46 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 46 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 51 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 47 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 47 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 57 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 53 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 53 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 58 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 54 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 54 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 64 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 60 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 60 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 65 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 61 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 61 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 73 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 69 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 69 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 74 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 70 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 70 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 80 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 76 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 76 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 81 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 77 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 77 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 87 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 83 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 83 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 88 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 84 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 84 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 94 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 90 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 90 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 95 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 91 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 91 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 101 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 97 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 97 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 102 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 98 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 98 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 106 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 104 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 104 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, 'Тухайн байгууллагын шилэн дансны нийт мэдээллийн дундаж хувь, хэмжээ' AS PARENT_NAME, MD_TIME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 3) AA " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, AA.PARENT_NAME, AA.MD_NAME";

                // Set parameters
                cmd.Parameters.Add(":ORGID", OracleDbType.Varchar2, request.Element("Parameters").Element("ORGID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "PrintDataList";

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

        public static DataResponse Print2DataList(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["MirroraccConfig"]);
                con.Open();

                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_ORG_PRINTED";

                OracleParameter retParam = cmd.Parameters.Add(":Ret_val",
                    OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);
                cmd.Parameters.Add(":OPEN_ID", OracleDbType.Int32, request.Element("Parameters").Element("ORGID")?.Value, System.Data.ParameterDirection.Input);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 116 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 116 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 118 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 118 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 116 THEN NULL END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 120 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 118 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 116 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 116 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 120 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 116 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 116 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 116) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 115) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 117 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 117 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 119 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 119 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 117 THEN NULL END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 121 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 119 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 117 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 117 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 121 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 117 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 117 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 117) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 115) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 123 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 123 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 125 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 125 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 123 THEN NULL END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 127 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 125 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 123 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 123 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 127 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 123 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 123 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 123) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 122) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 124 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 124 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 126 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 126 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 124 THEN NULL END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 128 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 126 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 124 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 124 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 128 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 124 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 124 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 124) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 122) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 130 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 130 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 132 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 132 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 130 THEN NULL END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 134 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 132 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 130 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 130 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 134 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 130 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 130 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 130) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 129) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 131 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 131 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 133 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 133 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 131 THEN NULL END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 135 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 133 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 131 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 131 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 135 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 131 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 131 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 131) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 129) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 137 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 137 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 139 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 139 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 137 THEN NULL END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 141 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 139 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 137 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 137 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 141 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 137 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 137 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 137) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 136) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 138 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 138 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 140 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 140 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 138 THEN 0 END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 142 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 140 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 138 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 138 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 142 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 138 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 138 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 138) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 136) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT B.MD_CODE ,B.MD_TIME, CASE WHEN B.IS_PREW = 0 THEN B.MD_NAME END PARENT_NAME, B.MD_LAWS_NUM || ' ' || B.MD_NAME AS MD_NAME, " +
                                "	   CASE WHEN A.DATA01 IS NOT NULL THEN 1 ELSE NULL END MEDEELEH_TOO_HEMJEE, " +
                                "	   CASE WHEN A.DATA01 = 1 THEN 1 ELSE NULL END MEDEELSEN, " +
                                "	   CASE WHEN A.DATA01 = 2 THEN 1 ELSE NULL END MEDEELEEGUI, " +
                                "	   CASE WHEN A.DATA01 = 4 THEN 1 ELSE 0 END SHAARDLAGAGUI, " +
                                "	   CASE WHEN A.DATA01 = 3 THEN 1 ELSE NULL END HUGATSAA_HOTSROOSON, " +
                                "	   CASE WHEN A.DATA01 IN(1, 3) THEN 100 ELSE NULL END PRECENT1, " +
                                "	   CASE WHEN A.DATA01 = 1 THEN 100 ELSE NULL END PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA A " +
                                "INNER JOIN AUD_MIRRORACC.MD_DESC B ON A.MDCODE = B.MD_CODE " +
                                "WHERE A.ORGID = :ORGID AND A.MDCODE IN(143, 144) " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 146 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 146 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 148 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 148 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 146 THEN NULL END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 150 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 148 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 146 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 146 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 150 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 146 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 146 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 146) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 145) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 147 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 147 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 149 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 149 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 147 THEN NULL END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 151 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 149 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 147 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 147 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 151 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 147 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 147 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 147) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 145) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 152 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 152 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 152 THEN NULL END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN DATA01 = 152 THEN 0 END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 152 THEN NULL END) HUGATSAA_HOTSROOSON, " +
                                "	   SUM(CASE WHEN MDCODE = 152 AND DATA01 != 0 THEN 100 END) PRECENT1, " +
                                "	   SUM(CASE WHEN MDCODE = 152 AND DATA01 != 0 THEN 100 END) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 152) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 145) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 153 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 153 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 153 THEN NULL END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN DATA01 = 153 THEN 0 END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 153 THEN NULL END) HUGATSAA_HOTSROOSON, " +
                                "	   SUM(CASE WHEN MDCODE = 153 AND DATA01 != 0 THEN 100 END) PRECENT1, " +
                                "	   SUM(CASE WHEN MDCODE = 153 AND DATA01 != 0 THEN 100 END) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 153) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 145) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 155 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 155 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 157 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 157 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 155 THEN NULL END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 159 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 157 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 155 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 155 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 159 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 155 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 155 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 155) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 154) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 156 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 156 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 158 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 158 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 156 THEN NULL END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 160 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 158 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 156 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 156 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 160 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 156 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 156 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 156) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 154) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME , BB.PARENT_NAME, AA.MD_NAME, " +
                                "	   SUM(CASE WHEN MDCODE = 162 THEN DATA01 END) MEDEELEH_TOO_HEMJEE, " +
                                "	   SUM(CASE WHEN MDCODE = 162 THEN DATA01 END) - SUM(CASE WHEN MDCODE = 163 THEN DATA01 END) MEDEELSEN, " +
                                "	   SUM(CASE WHEN MDCODE = 163 THEN DATA01 END) MEDEELEEGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 162 THEN NULL END) SHAARDLAGAGUI, " +
                                "	   SUM(CASE WHEN MDCODE = 164 THEN DATA01 END) HUGATSAA_HOTSROOSON, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 163 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 162 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 162 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "	   ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 164 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 162 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 162 THEN DATA01 END) ELSE NULL END, 1) PRECENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, MD_LAWS_NUM || ' ' || MD_NAME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 162) AA, " +
                                "(SELECT MD_LAWS_NUM || ' ' || MD_NAME AS PARENT_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 161) BB " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, BB.PARENT_NAME, AA.MD_NAME " +
                                "UNION ALL " +
                                "SELECT AA.MD_CODE, AA.MD_TIME, AA.PARENT_NAME, AA.MD_NAME, " +
                                "SUM(CASE WHEN MDCODE = 3 THEN NULL END) MEDEELEH_TOO_HEMJEE, " +
                                "SUM(CASE WHEN MDCODE = 3 THEN NULL END) - SUM(CASE WHEN MDCODE = 105 THEN DATA01 END) MEDEELSEN, " +
                                "SUM(CASE WHEN MDCODE = 3 THEN NULL END) MEDEELEEGUI, " +
                                "SUM(CASE WHEN MDCODE = 3 THEN NULL END) SHAARDLAGAGUI, " +
                                "SUM(CASE WHEN MDCODE = 3 THEN NULL END) HUGATSAA_HOTSROOSON, " +
                                "SUM(CASE WHEN DATA01 IN(1, 3) AND MDCODE IN(143, 144) THEN 100 ELSE NULL END) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 118 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 116 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 116 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 119 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 117 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 117 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 125 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 123 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 123 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 126 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 124 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 124 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 132 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 130 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 130 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 133 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 131 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 131 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 139 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 137 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 137 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 140 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 138 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 138 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 148 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 146 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 146 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 149 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 147 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 147 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "SUM(CASE WHEN MDCODE = 152 AND DATA01 != 0 THEN 100 END) + " +
                                "SUM(CASE WHEN MDCODE = 153 AND DATA01 != 0 THEN 100 END) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 157 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 155 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 155 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 158 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 156 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 156 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 163 AND DATA01 != 0 THEN DATA01 ELSE NULL END) / CASE WHEN SUM(CASE WHEN MDCODE = 162 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 162 THEN DATA01 END) ELSE NULL END, 1) PRECENT1, " +
                                "SUM(CASE WHEN DATA01 = 1 AND MDCODE IN(143, 144) THEN 100 ELSE NULL END) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 120 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 116 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 116 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 121 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 117 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 117 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 127 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 123 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 123 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 128 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 124 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 124 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 134 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 130 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 130 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 135 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 131 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 131 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 141 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 137 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 137 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 142 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 138 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 138 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 150 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 146 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 146 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 151 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 147 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 147 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "SUM(CASE WHEN MDCODE = 152 AND DATA01 != 0 THEN 100 END) + " +
                                "SUM(CASE WHEN MDCODE = 153 AND DATA01 != 0 THEN 100 END) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 159 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 155 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 155 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 160 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 156 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 156 THEN DATA01 END) ELSE NULL END, 1) + " +
                                "ROUND(100 - 100 * SUM(CASE WHEN MDCODE = 164 THEN DATA01 END) / CASE WHEN SUM(CASE WHEN MDCODE = 162 THEN DATA01 END) != 0 THEN SUM(CASE WHEN MDCODE = 162 THEN DATA01 END) ELSE NULL END, 1) PERCENT2 " +
                                "FROM AUD_MIRRORACC.SHILENDANSDATA, " +
                                "(SELECT MD_CODE, MD_TIME, 'Тухайн байгууллагын шилэн дансны нийт мэдээллийн дундаж хувь, хэмжээ' AS PARENT_NAME, MD_TIME AS MD_NAME FROM AUD_MIRRORACC.MD_DESC WHERE MD_CODE = 3) AA " +
                                "WHERE ORGID = :ORGID " +
                                "GROUP BY AA.MD_CODE, AA.MD_TIME, AA.PARENT_NAME, AA.MD_NAME";

                // Set parameters
                cmd.Parameters.Add(":ORGID", OracleDbType.Varchar2, request.Element("Parameters").Element("ORGID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "PrintDataList";

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
        public static DataResponse TableProjectList(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["MirroraccConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PROJECT_NAME, PROJECT_NUMBER, PROJECT_START_DATE, PROJECT_END_DATE, PROJECT_PERCENT, PROJECT_TOTAL_BUDGET, PROJECT_ORG_FUND, PROJECT_ID " +
                    "FROM AUD_MIRRORACC.ORG_PROJECT_LIST " +
                    "WHERE PROJECT_IS_ACTIVE = 1 AND ORGID = :ORG_ID " +
                    "GROUP BY PROJECT_NAME, PROJECT_NUMBER, PROJECT_START_DATE, PROJECT_END_DATE, PROJECT_PERCENT, PROJECT_TOTAL_BUDGET, PROJECT_ORG_FUND, PROJECT_ID ";

                // Set parameters
                cmd.Parameters.Add(":ORG_ID", OracleDbType.Int32, request.Element("Parameters").Element("ORG_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "TableProjectList";

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

        public static DataResponse OrgProjectDataList(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["MirroraccConfig"]);
                con.Open();

                // Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT A.MDCODE, B.MD_LAWS_NUM, B.MD_NAME, B.MD_TIME, A.DATA01, A.DATA02 " +
                        "FROM AUD_MIRRORACC.ORG_PROJECT_LIST A " +
                        "JOIN AUD_MIRRORACC.MD_DESC B ON A.MDCODE = B.MD_CODE " +
                        "WHERE A.PROJECT_ID = :PROJECT_ID " +
                        "ORDER BY B.MD_CODE ";

                // Set parameters
                cmd.Parameters.Add(":PROJECT_ID", OracleDbType.Int32, request.Element("Parameters").Element("PROJECT_ID").Value, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);

                cmd.Dispose();
                con.Close();

                dtTable.TableName = "OrgProjectDataList";

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

        public static DataResponse MirrorAccInsert(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["MirroraccConfig"]);
                con.Open();

                //Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_MIRRORACC_INSERT";

                // Set parameters
                OracleParameter retParam = cmd.Parameters.Add(":Ret_val", OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);

                cmd.Parameters.Add(":P_YEARCODE", OracleDbType.Int32).Value = request.Element("Parameters").Element("YEAR_CODE")?.Value;
                cmd.Parameters.Add(":P_ORGID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ORG_ID")?.Value;
                cmd.Parameters.Add(":P_MD_CODE", OracleDbType.Int32).Value = request.Element("Parameters").Element("MD_CODE")?.Value;
                cmd.Parameters.Add(":P_DATA01", OracleDbType.Double).Value = request.Element("Parameters").Element("DATA01")?.Value;
                cmd.Parameters.Add(":P_DATA02", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("DATA02")?.Value;
                cmd.Parameters.Add(":P_ISFINISH", OracleDbType.Int32).Value = request.Element("Parameters").Element("ISFINISH")?.Value;
                cmd.Parameters.Add(":P_USERID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_INSDATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("INSDATE").Value;
                //cmd.ArrayBindCount = request.Element("Parameters").Element("MD_CODE").Value.Length;


                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                object responseValue = retParam.Value;

                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true  :false;

                response.CreateResponse(responseVal, string.Empty, "Амжилттай хадгаллаа");
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }

        public static DataResponse OrgProjectDelete(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["MirroraccConfig"]);
                con.Open();

                //Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_ORG_PROJECT_DELETE";

                // Set parameters
                OracleParameter retParam = cmd.Parameters.Add(":Ret_val", OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);
               
                cmd.Parameters.Add(":P_ORGID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ORG_ID")?.Value;
                cmd.Parameters.Add(":P_PROID", OracleDbType.Int32).Value = request.Element("Parameters").Element("PRO_ID")?.Value;


                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                object responseValue = retParam.Value;

                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true  :false;

                response.CreateResponse(responseVal, string.Empty, "Амжилттай устгалаа");
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }

        public static DataResponse OrgProjectInsert(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["MirroraccConfig"]);
                con.Open();

                //Create and execute the command
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_ORG_PROJECT_INSERT";

                // Set parameters
                OracleParameter retParam = cmd.Parameters.Add(":Ret_val", OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);

                cmd.Parameters.Add(":P_YEARCODE", OracleDbType.Int32).Value = request.Element("Parameters").Element("YEAR_CODE")?.Value;
                cmd.Parameters.Add(":P_ORGID", OracleDbType.Int32).Value = request.Element("Parameters").Element("ORG_ID")?.Value;
                cmd.Parameters.Add(":P_PROJECT_NAME", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("PROJ_NAME")?.Value;
                cmd.Parameters.Add(":P_PROJECT_NUMBER", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("PROJ_NUM")?.Value;
                cmd.Parameters.Add(":P_PROJECT_START_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("PROJ_START_DATE")?.Value;
                cmd.Parameters.Add(":P_PROJECT_END_DATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("PROJ_END_DATE")?.Value;
                cmd.Parameters.Add(":P_PROJECT_PERCENT", OracleDbType.Int32).Value = request.Element("Parameters").Element("PROJ_PERCENT")?.Value;
                cmd.Parameters.Add(":P_PROJECT_TOTAL_BUDGET", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("PROJ_BUDGET")?.Value;
                cmd.Parameters.Add(":P_PROJECT_ORG_FUND", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("PROJ_FUND")?.Value;
                cmd.Parameters.Add(":P_MD_CODE", OracleDbType.Int32).Value = request.Element("Parameters").Element("MD_CODE")?.Value;
                cmd.Parameters.Add(":P_DATA01", OracleDbType.Double).Value = request.Element("Parameters").Element("DATA01")?.Value;
                cmd.Parameters.Add(":P_DATA02", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("DATA02")?.Value;
                cmd.Parameters.Add(":P_USERID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_INSDATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("INSDATE").Value;
                cmd.Parameters.Add(":P_LAWS_NUMBER", OracleDbType.Int32).Value = request.Element("Parameters").Element("PROJ_LAW_NUM").Value;
                cmd.Parameters.Add(":P_PROJ_ID", OracleDbType.Int32).Value = request.Element("Parameters").Element("PROJ_ID").Value;
                cmd.Parameters.Add(":P_PROJ_IS_ACTIVE", OracleDbType.Int32).Value = request.Element("Parameters").Element("PROJ_IS_ACTIVE").Value;
                //cmd.ArrayBindCount = request.Element("Parameters").Element("MD_CODE").Value.Length;


                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                object responseValue = retParam.Value;

                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true  :false;

                response.CreateResponse(responseVal, string.Empty, "Амжилттай хадгаллаа");
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }

        #endregion'

        #region Tailan
        public static DataResponse ReportN1(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {

                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();
                XElement req = request.Element("Parameters").Element("Request");

                string mayagt = null;

                if(req.Element("V_Mayagt")?.Value != "")
                {
                    mayagt = req.Element("V_Mayagt")?.Value;
                }
                else
                {
                    mayagt = "1,2,3,4,5";
                }
                


                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " WITH negtgel1 AS(" +
                                    "SELECT * FROM (" +
                                    "SELECT ORGID,  INSERTUSERID,MDCODE,DATA01 FROM AUD_MIRRORACC.SHILENDANSDATA A " +
                                    "LEFT JOIN AUD_MIRRORACC.OPENACC_ENTITY ROP ON A.ORGID = ROP.OPEN_ID " +
                                    "LEFT JOIN AUD_MIRRORACC.REF_BUDGET_TYPE C ON ROP.OPEN_ENT_BUDGET_TYPE = C.BUDGET_TYPE_ID " +
                                    "WHERE MDCODE BETWEEN 1 AND 35 and OPEN_ENT_DEPARTMENT_ID = :V_DEPARTMENT AND UPPER(ROP.OPEN_ENT_NAME) LIKE '%'|| UPPER(:V_SEARCH) ||'%' AND ROP.OPEN_ENT_GROUP_ID IN ("+ mayagt + ") " +
                                    ") D " +
                                    "PIVOT (" +
                                    "MAX(DATA01)" +
                                    "FOR MDCODE IN (1 MD1, 2 MD2, 3 MD3, 4 MD4, 5 MD5, 6 MD6, 7 MD7, 8 MD8, 9 MD9, 10 MD10,11 MD11, 12 MD12, 13 MD13, 14 MD14, 15 MD15, 16 MD16, 17 MD17, 18 MD18, 19 MD19, 20 MD20,21 MD21, 22 MD22, 23 MD23, 24 MD24, 25 MD25, 26 MD26, 27 MD27, 28 MD28, 29 MD29, 30 MD30,31 MD31, 32 MD32,33 MD33,34 MD34,35 MD35)" +
                                    ") P1 " +
                                    ")" +
                                    "SELECT ORGID, " +
                                    "COUNT(CASE WHEN MD1 = 1 THEN 1 WHEN MD1 = 2 THEN 1 WHEN MD1 = 3 THEN 1 WHEN MD1 = 4 THEN 1  END) MD1," +
                                    "COUNT(CASE WHEN MD2 = 1 THEN 1 WHEN MD2 = 2 THEN 1 WHEN MD2 = 3 THEN 1 WHEN MD2 = 4 THEN 1  END) MD2," +
                                    "COUNT(CASE WHEN MD3 = 1 THEN 1 WHEN MD3 = 2 THEN 1 WHEN MD3 = 3 THEN 1 WHEN MD3 = 4 THEN 1  END) MD3," +
                                    "COUNT(CASE WHEN MD4 = 1 THEN 1 WHEN MD4 = 2 THEN 1 WHEN MD4 = 3 THEN 1 WHEN MD4 = 4 THEN 1  END) MD4," +
                                    "COUNT(CASE WHEN MD5 = 1 THEN 1 WHEN MD5 = 2 THEN 1 WHEN MD5 = 3 THEN 1 WHEN MD5 = 4 THEN 1  END) MD5," +
                                    "COUNT(CASE WHEN MD6 = 1 THEN 1 WHEN MD6 = 2 THEN 1 WHEN MD6 = 3 THEN 1 WHEN MD6 = 4 THEN 1  END) MD6," +
                                    "COUNT(CASE WHEN MD7 = 1 THEN 1 WHEN MD7 = 2 THEN 1 WHEN MD7 = 3 THEN 1 WHEN MD7 = 4 THEN 1  END) MD7," +
                                    "COUNT(CASE WHEN MD8 = 1 THEN 1 WHEN MD8 = 2 THEN 1 WHEN MD8 = 3 THEN 1 WHEN MD8 = 4 THEN 1  END) MD8," +
                                    "COUNT(CASE WHEN MD9 = 1 THEN 1 WHEN MD9 = 2 THEN 1 WHEN MD9 = 3 THEN 1 WHEN MD9 = 4 THEN 1  END) MD9," +
                                    "COUNT(CASE WHEN MD10 = 1 THEN 1 WHEN MD10 = 2 THEN 1 WHEN MD10 = 3 THEN 1 WHEN MD10 = 4 THEN 1  END) MD10," +
                                    "COUNT(CASE WHEN MD11 = 1 THEN 1 WHEN MD11 = 2 THEN 1 WHEN MD11 = 3 THEN 1 WHEN MD11 = 4 THEN 1  END) MD11," +
                                    "COUNT(CASE WHEN MD12 = 1 THEN 1 WHEN MD12 = 2 THEN 1 WHEN MD12 = 3 THEN 1 WHEN MD12 = 4 THEN 1  END) MD12," +
                                    "COUNT(CASE WHEN MD13 = 1 THEN 1 WHEN MD13 = 2 THEN 1 WHEN MD13 = 3 THEN 1 WHEN MD13 = 4 THEN 1  END) MD13," +
                                    "COUNT(CASE WHEN MD14 = 1 THEN 1 WHEN MD14 = 2 THEN 1 WHEN MD14 = 3 THEN 1 WHEN MD14 = 4 THEN 1  END) MD14," +
                                    "COUNT(CASE WHEN MD15 = 1 THEN 1 WHEN MD15 = 2 THEN 1 WHEN MD15 = 3 THEN 1 WHEN MD15 = 4 THEN 1  END) MD15," +
                                    "COUNT(CASE WHEN MD16 = 1 THEN 1 WHEN MD16 = 2 THEN 1 WHEN MD16 = 3 THEN 1 WHEN MD16 = 4 THEN 1  END) MD16," +
                                    "COUNT(CASE WHEN MD17 = 1 THEN 1 WHEN MD17 = 2 THEN 1 WHEN MD17 = 3 THEN 1 WHEN MD17 = 4 THEN 1  END) MD17," +
                                    "COUNT(CASE WHEN MD18 = 1 THEN 1 WHEN MD18 = 2 THEN 1 WHEN MD18 = 3 THEN 1 WHEN MD18 = 4 THEN 1  END) MD18," +
                                    "COUNT(CASE WHEN MD19 = 1 THEN 1 WHEN MD19 = 2 THEN 1 WHEN MD19 = 3 THEN 1 WHEN MD19 = 4 THEN 1  END) MD19," +
                                    "COUNT(CASE WHEN MD20 = 1 THEN 1 WHEN MD20 = 2 THEN 1 WHEN MD20 = 3 THEN 1 WHEN MD20 = 4 THEN 1  END) MD20," +
                                    "COUNT(CASE WHEN MD21 = 1 THEN 1 WHEN MD21 = 2 THEN 1 WHEN MD21 = 3 THEN 1 WHEN MD21 = 4 THEN 1  END) MD21," +
                                    "COUNT(CASE WHEN MD22 = 1 THEN 1 WHEN MD22 = 2 THEN 1 WHEN MD22 = 3 THEN 1 WHEN MD22 = 4 THEN 1  END) MD22," +
                                    "COUNT(CASE WHEN MD23 = 1 THEN 1 WHEN MD23 = 2 THEN 1 WHEN MD23 = 3 THEN 1 WHEN MD23 = 4 THEN 1  END) MD23," +
                                    "COUNT(CASE WHEN MD24 = 1 THEN 1 WHEN MD24 = 2 THEN 1 WHEN MD24 = 3 THEN 1 WHEN MD24 = 4 THEN 1  END) MD24," +
                                    "COUNT(CASE WHEN MD25 = 1 THEN 1 WHEN MD25 = 2 THEN 1 WHEN MD25 = 3 THEN 1 WHEN MD25 = 4 THEN 1  END) MD25," +
                                    "COUNT(CASE WHEN MD26 = 1 THEN 1 WHEN MD26 = 2 THEN 1 WHEN MD26 = 3 THEN 1 WHEN MD26 = 4 THEN 1  END) MD26," +
                                    "COUNT(CASE WHEN MD27 = 1 THEN 1 WHEN MD27 = 2 THEN 1 WHEN MD27 = 3 THEN 1 WHEN MD27 = 4 THEN 1  END) MD27," +
                                    "COUNT(CASE WHEN MD28 = 1 THEN 1 WHEN MD28 = 2 THEN 1 WHEN MD28 = 3 THEN 1 WHEN MD28 = 4 THEN 1  END) MD28," +
                                    "COUNT(CASE WHEN MD29 = 1 THEN 1 WHEN MD29 = 2 THEN 1 WHEN MD29 = 3 THEN 1 WHEN MD29 = 4 THEN 1  END) MD29," +
                                    "COUNT(CASE WHEN MD30 = 1 THEN 1 WHEN MD30 = 2 THEN 1 WHEN MD30 = 3 THEN 1 WHEN MD30 = 4 THEN 1  END) MD30," +
                                    "COUNT(CASE WHEN MD31 = 1 THEN 1 WHEN MD31 = 2 THEN 1 WHEN MD31 = 3 THEN 1 WHEN MD31 = 4 THEN 1  END) MD31," +
                                    "COUNT(CASE WHEN MD32 = 1 THEN 1 WHEN MD32 = 2 THEN 1 WHEN MD32 = 3 THEN 1 WHEN MD32 = 4 THEN 1  END) MD32," +
                                    "COUNT(CASE WHEN MD35 = 1 THEN 1 WHEN MD35 = 2 THEN 1 WHEN MD35 = 3 THEN 1 WHEN MD35 = 4 THEN 1  END) MD35 " +
                                    "FROM negtgel1 " +
                                    "GROUP BY ORGID";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search") != null && !string.IsNullOrEmpty(req.Element("Search").Value) ? req.Element("Search")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_Mayagt", OracleDbType.Varchar2, req.Element("V_Mayagt") != null && !string.IsNullOrEmpty(req.Element("V_Mayagt").Value) ? req.Element("V_Mayagt")?.Value : null, System.Data.ParameterDirection.Input);

                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);
                dtTable.TableName = "N1";

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "WITH negtgel1 AS( "+
                                        "SELECT * FROM (" +
                                        "SELECT ORGID, YEARCODE, INSERTUSERID, MDCODE, DATA01,ROP.OPEN_ENT_NAME AS ORGNAME, C.BUDGET_TYPE_NAME AS ORGTYPE,ROP.OPEN_HEAD_ROLE, ROP.OPEN_HEAD_NAME, ROP.OPEN_HEAD_PHONE, ROP.OPEN_ACC_ROLE, ROP.OPEN_ACC_NAME , ROP.OPEN_ACC_PHONE FROM AUD_MIRRORACC.SHILENDANSDATA A "+
                                        "LEFT JOIN AUD_MIRRORACC.OPENACC_ENTITY ROP ON A.ORGID = ROP.OPEN_ID "+
                                        "LEFT JOIN AUD_MIRRORACC.REF_BUDGET_TYPE C ON ROP.OPEN_ENT_BUDGET_TYPE = C.BUDGET_TYPE_ID "+
                                        "WHERE MDCODE BETWEEN 1 AND 35 and OPEN_ENT_DEPARTMENT_ID = :V_DEPARTMENT AND UPPER(ROP.OPEN_ENT_NAME) LIKE '%'|| UPPER(:V_SEARCH) ||'%' AND ROP.OPEN_ENT_GROUP_ID IN (" + mayagt + ") " +

                                        ") D " +
                                        "PIVOT ( " +
                                        "MAX(DATA01) " +
                                        "FOR MDCODE IN (1 MD1, 2 MD2, 3 MD3, 4 MD4, 5 MD5, 6 MD6, 7 MD7, 8 MD8, 9 MD9, 10 MD10,11 MD11, 12 MD12, 13 MD13, 14 MD14, 15 MD15, 16 MD16, 17 MD17, 18 MD18, 19 MD19, 20 MD20,21 MD21, 22 MD22, 23 MD23, 24 MD24, 25 MD25, 26 MD26, 27 MD27, 28 MD28, 29 MD29, 30 MD30,31 MD31, 32 MD32,33 MD33,34 MD34,35 MD35) " +
                                        ") P1 " +
                                        ") " +
                                        "SELECT ORGID, INSERTUSERID,ORGNAME,ORGTYPE,OPEN_HEAD_ROLE, OPEN_HEAD_NAME, OPEN_HEAD_PHONE,OPEN_ACC_ROLE, OPEN_ACC_NAME ,OPEN_ACC_PHONE, " +
                                        "MD1,MD2,MD3, MD4,MD5, MD6, MD7, MD8, MD9,  MD10, MD11,  MD12,  MD13,  MD14,  MD15,  MD16,  MD17,  MD18,  MD19,  MD20, MD21,  MD22, MD23,  MD24,  MD25,  MD26,  MD27,  MD28,  MD29,  MD30, MD31,  MD32, MD33, MD34, MD35 " +
                                        "FROM negtgel1";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search") != null && !string.IsNullOrEmpty(req.Element("Search").Value) ? req.Element("Search")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_Mayagt", OracleDbType.Varchar2, req.Element("V_Mayagt") != null && !string.IsNullOrEmpty(req.Element("V_Mayagt").Value) ? req.Element("V_Mayagt")?.Value : null, System.Data.ParameterDirection.Input);


                DataTable dtTable2 = new DataTable();
                dtTable2.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);
                dtTable2.TableName = "N1Footer";

                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(dtTable);
                dataSet.Tables.Add(dtTable2);
                cmd.Dispose();
                con.Close();



                StringWriter sw = new StringWriter();
                dataSet.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                // xmlResponseData.Add(new XElement("RowCount", count));
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }

        public static DataResponse Report1N2(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {

                // Open a connection to the database
                OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.AppSettings["StatConfig"]);
                con.Open();
                XElement req = request.Element("Parameters").Element("Request");
                

                OracleCommand cmd = con.CreateCommand();
             
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "WITH negtgel2 AS(" +
                                "SELECT* FROM( " +
                                "SELECT ORGID, INSERTUSERID, MDCODE, DATA01 FROM AUD_MIRRORACC.SHILENDANSDATA A " +
                                "LEFT JOIN AUD_MIRRORACC.OPENACC_ENTITY ROP ON A.ORGID = ROP.OPEN_ID " +
                                "WHERE MDCODE BETWEEN 33 AND 169 AND OPEN_ENT_DEPARTMENT_ID = :V_DEPARTMENT " +
                                "AND UPPER(ROP.OPEN_ENT_NAME) LIKE '%'|| UPPER(:V_SEARCH) ||'%' " +
                                ") D " +
                                "PIVOT( " +
                                "MAX(DATA01) " +
                                "FOR MDCODE IN(33 MD33, 34 MD34, 37 MD37, 38 MD38, 39 MD39, 40 MD40, 41 MD41, 42 MD42, 43 MD43, 44 MD44, 45 MD45, 46 MD46, 47 MD47, 48 MD48, 49 MD49, 50 MD50, 51 MD51, 52 MD52, 53 MD53, 54 MD54, 55 MD55, 56 MD56, 57 MD57, 58 MD58, 59 MD59, 60 MD60, 61 MD61, 62 MD62, 63 MD63, 64 MD64, 65 MD65, 66 MD66, 67 MD67, 68 MD68, 69 MD69, 70 MD70, 71 MD71, 72 MD72, 73 MD73, 74 MD74, 75 MD75, 76 MD76, 77 MD77, 78 MD78, 79 MD79, 80 MD80, 81 MD81, 82 MD82, 83 MD83, 84 MD84, 85 MD85, 86 MD86, 87 MD87, 88 MD88, 89 MD89, 90 MD90, 91 MD91, 92 MD92, 93 MD93, 94 MD94, 95 MD95, 96 MD96, 97 MD97, 98 MD98, 99 MD99, 100 MD100, 101 MD101, 102 MD102, 103 MD103, 104 MD104, 105 MD105, 106 MD106, 158 MD158, 159 MD159, 160 MD160, 161 Md161, 162 MD162, 165 MD165, 166 MD166, 167 MD167, 168 MD168, 169 MD169) " +
                                ") P1 " +
                                ") " +
                                "SELECT ORGID, " +
                                "COUNT(CASE WHEN MD33 = 1 THEN 1 WHEN MD33 = 2 THEN 1 WHEN MD33 = 3 THEN 1 WHEN MD33 = 4 THEN 1  END) MD33, " +
                                "COUNT(CASE WHEN MD34 = 1 THEN 1 WHEN MD34 = 2 THEN 1 WHEN MD34 = 3 THEN 1 WHEN MD34 = 4 THEN 1  END) MD34, " +
                                "COUNT(CASE WHEN MD37 = 1 THEN 1 WHEN MD37 = 2 THEN 1 WHEN MD37 = 3 THEN 1 WHEN MD37 = 4 THEN 1  END) MD37, " +
                                "COUNT(CASE WHEN MD38 = 1 THEN 1 WHEN MD38 = 2 THEN 1 WHEN MD38 = 3 THEN 1 WHEN MD38 = 4 THEN 1  END) MD38, " +
                                "COUNT(CASE WHEN MD39 = 1 THEN 1 WHEN MD39 = 2 THEN 1 WHEN MD39 = 3 THEN 1 WHEN MD39 = 4 THEN 1  END) MD39, " +
                                "COUNT(CASE WHEN MD40 = 1 THEN 1 WHEN MD40 = 2 THEN 1 WHEN MD40 = 3 THEN 1 WHEN MD40 = 4 THEN 1  END) MD40, " +
                                "COUNT(CASE WHEN MD41 = 1 THEN 1 WHEN MD41 = 2 THEN 1 WHEN MD41 = 3 THEN 1 WHEN MD41 = 4 THEN 1  END) MD41, " +
                                "COUNT(CASE WHEN MD42 = 1 THEN 1 WHEN MD42 = 2 THEN 1 WHEN MD42 = 3 THEN 1 WHEN MD42 = 4 THEN 1  END) MD42, " +
                                "COUNT(CASE WHEN MD43 = 1 THEN 1 WHEN MD43 = 2 THEN 1 WHEN MD43 = 3 THEN 1 WHEN MD43 = 4 THEN 1  END) MD43, " +
                                "COUNT(CASE WHEN MD44 = 1 THEN 1 WHEN MD44 = 2 THEN 1 WHEN MD44 = 3 THEN 1 WHEN MD44 = 4 THEN 1  END) MD44, " +
                                "COUNT(CASE WHEN MD45 = 1 THEN 1 WHEN MD45 = 2 THEN 1 WHEN MD45 = 3 THEN 1 WHEN MD45 = 4 THEN 1  END) MD45, " +
                                "COUNT(CASE WHEN MD46 = 1 THEN 1 WHEN MD46 = 2 THEN 1 WHEN MD46 = 3 THEN 1 WHEN MD46 = 4 THEN 1  END) MD46, " +
                                "COUNT(CASE WHEN MD47 = 1 THEN 1 WHEN MD47 = 2 THEN 1 WHEN MD47 = 3 THEN 1 WHEN MD47 = 4 THEN 1  END) MD47, " +
                                "COUNT(CASE WHEN MD48 = 1 THEN 1 WHEN MD48 = 2 THEN 1 WHEN MD48 = 3 THEN 1 WHEN MD48 = 4 THEN 1  END) MD48, " +
                                "COUNT(CASE WHEN MD49 = 1 THEN 1 WHEN MD49 = 2 THEN 1 WHEN MD49 = 3 THEN 1 WHEN MD49 = 4 THEN 1  END) MD49, " +
                                "COUNT(CASE WHEN MD50 = 1 THEN 1 WHEN MD50 = 2 THEN 1 WHEN MD50 = 3 THEN 1 WHEN MD50 = 4 THEN 1  END) MD50, " +
                                "COUNT(CASE WHEN MD51 = 1 THEN 1 WHEN MD51 = 2 THEN 1 WHEN MD51 = 3 THEN 1 WHEN MD51 = 4 THEN 1  END) MD51, " +
                                "COUNT(CASE WHEN MD52 = 1 THEN 1 WHEN MD52 = 2 THEN 1 WHEN MD52 = 3 THEN 1 WHEN MD52 = 4 THEN 1  END) MD52, " +
                                "COUNT(CASE WHEN MD53 = 1 THEN 1 WHEN MD53 = 2 THEN 1 WHEN MD53 = 3 THEN 1 WHEN MD53 = 4 THEN 1  END) MD53, " +
                                "COUNT(CASE WHEN MD54 = 1 THEN 1 WHEN MD54 = 2 THEN 1 WHEN MD54 = 3 THEN 1 WHEN MD54 = 4 THEN 1  END) MD54, " +
                                "COUNT(CASE WHEN MD55 = 1 THEN 1 WHEN MD55 = 2 THEN 1 WHEN MD55 = 3 THEN 1 WHEN MD55 = 4 THEN 1  END) MD55, " +
                                "COUNT(CASE WHEN MD56 = 1 THEN 1 WHEN MD56 = 2 THEN 1 WHEN MD56 = 3 THEN 1 WHEN MD56 = 4 THEN 1  END) MD56, " +
                                "COUNT(CASE WHEN MD57 = 1 THEN 1 WHEN MD57 = 2 THEN 1 WHEN MD57 = 3 THEN 1 WHEN MD57 = 4 THEN 1  END) MD57, " +
                                "COUNT(CASE WHEN MD58 = 1 THEN 1 WHEN MD58 = 2 THEN 1 WHEN MD58 = 3 THEN 1 WHEN MD58 = 4 THEN 1  END) MD58, " +
                                "COUNT(CASE WHEN MD59 = 1 THEN 1 WHEN MD59 = 2 THEN 1 WHEN MD59 = 3 THEN 1 WHEN MD59 = 4 THEN 1  END) MD59, " +
                                "COUNT(CASE WHEN MD60 = 1 THEN 1 WHEN MD60 = 2 THEN 1 WHEN MD60 = 3 THEN 1 WHEN MD60 = 4 THEN 1  END) MD60, " +
                                "COUNT(CASE WHEN MD61 = 1 THEN 1 WHEN MD61 = 2 THEN 1 WHEN MD61 = 3 THEN 1 WHEN MD61 = 4 THEN 1  END) MD61, " +
                                "COUNT(CASE WHEN MD62 = 1 THEN 1 WHEN MD62 = 2 THEN 1 WHEN MD62 = 3 THEN 1 WHEN MD62 = 4 THEN 1  END) MD62, " +
                                "COUNT(CASE WHEN MD63 = 1 THEN 1 WHEN MD63 = 2 THEN 1 WHEN MD63 = 3 THEN 1 WHEN MD63 = 4 THEN 1  END) MD63, " +
                                "COUNT(CASE WHEN MD64 = 1 THEN 1 WHEN MD64 = 2 THEN 1 WHEN MD64 = 3 THEN 1 WHEN MD64 = 4 THEN 1  END) MD64, " +
                                "COUNT(CASE WHEN MD65 = 1 THEN 1 WHEN MD65 = 2 THEN 1 WHEN MD65 = 3 THEN 1 WHEN MD65 = 4 THEN 1  END) MD65, " +
                                "COUNT(CASE WHEN MD66 = 1 THEN 1 WHEN MD66 = 2 THEN 1 WHEN MD66 = 3 THEN 1 WHEN MD66 = 4 THEN 1  END) MD66, " +
                                "COUNT(CASE WHEN MD67 = 1 THEN 1 WHEN MD67 = 2 THEN 1 WHEN MD67 = 3 THEN 1 WHEN MD67 = 4 THEN 1  END) MD67, " +
                                "COUNT(CASE WHEN MD68 = 1 THEN 1 WHEN MD68 = 2 THEN 1 WHEN MD68 = 3 THEN 1 WHEN MD68 = 4 THEN 1  END) MD68, " +
                                "COUNT(CASE WHEN MD69 = 1 THEN 1 WHEN MD69 = 2 THEN 1 WHEN MD69 = 3 THEN 1 WHEN MD69 = 4 THEN 1  END) MD69, " +
                                "COUNT(CASE WHEN MD70 = 1 THEN 1 WHEN MD70 = 2 THEN 1 WHEN MD70 = 3 THEN 1 WHEN MD70 = 4 THEN 1  END) MD70, " +
                                "COUNT(CASE WHEN MD71 = 1 THEN 1 WHEN MD71 = 2 THEN 1 WHEN MD71 = 3 THEN 1 WHEN MD71 = 4 THEN 1  END) MD71, " +
                                "COUNT(CASE WHEN MD72 = 1 THEN 1 WHEN MD72 = 2 THEN 1 WHEN MD72 = 3 THEN 1 WHEN MD72 = 4 THEN 1  END) MD72, " +
                                "COUNT(CASE WHEN MD73 = 1 THEN 1 WHEN MD73 = 2 THEN 1 WHEN MD73 = 3 THEN 1 WHEN MD73 = 4 THEN 1  END) MD73, " +
                                "COUNT(CASE WHEN MD74 = 1 THEN 1 WHEN MD74 = 2 THEN 1 WHEN MD74 = 3 THEN 1 WHEN MD74 = 4 THEN 1  END) MD74, " +
                                "COUNT(CASE WHEN MD75 = 1 THEN 1 WHEN MD75 = 2 THEN 1 WHEN MD75 = 3 THEN 1 WHEN MD75 = 4 THEN 1  END) MD75, " +
                                "COUNT(CASE WHEN MD76 = 1 THEN 1 WHEN MD76 = 2 THEN 1 WHEN MD76 = 3 THEN 1 WHEN MD76 = 4 THEN 1  END) MD76, " +
                                "COUNT(CASE WHEN MD77 = 1 THEN 1 WHEN MD77 = 2 THEN 1 WHEN MD77 = 3 THEN 1 WHEN MD77 = 4 THEN 1  END) MD77, " +
                                "COUNT(CASE WHEN MD78 = 1 THEN 1 WHEN MD78 = 2 THEN 1 WHEN MD78 = 3 THEN 1 WHEN MD78 = 4 THEN 1  END) MD78, " +
                                "COUNT(CASE WHEN MD79 = 1 THEN 1 WHEN MD79 = 2 THEN 1 WHEN MD79 = 3 THEN 1 WHEN MD79 = 4 THEN 1  END) MD79, " +
                                "COUNT(CASE WHEN MD80 = 1 THEN 1 WHEN MD80 = 2 THEN 1 WHEN MD80 = 3 THEN 1 WHEN MD80 = 4 THEN 1  END) MD80, " +
                                "COUNT(CASE WHEN MD81 = 1 THEN 1 WHEN MD81 = 2 THEN 1 WHEN MD81 = 3 THEN 1 WHEN MD81 = 4 THEN 1  END) MD81, " +
                                "COUNT(CASE WHEN MD82 = 1 THEN 1 WHEN MD82 = 2 THEN 1 WHEN MD82 = 3 THEN 1 WHEN MD82 = 4 THEN 1  END) MD82, " +
                                "COUNT(CASE WHEN MD83 = 1 THEN 1 WHEN MD83 = 2 THEN 1 WHEN MD83 = 3 THEN 1 WHEN MD83 = 4 THEN 1  END) MD83, " +
                                "COUNT(CASE WHEN MD84 = 1 THEN 1 WHEN MD84 = 2 THEN 1 WHEN MD84 = 3 THEN 1 WHEN MD84 = 4 THEN 1  END) MD84, " +
                                "COUNT(CASE WHEN MD85 = 1 THEN 1 WHEN MD85 = 2 THEN 1 WHEN MD85 = 3 THEN 1 WHEN MD85 = 4 THEN 1  END) MD85, " +
                                "COUNT(CASE WHEN MD86 = 1 THEN 1 WHEN MD86 = 2 THEN 1 WHEN MD86 = 3 THEN 1 WHEN MD86 = 4 THEN 1  END) MD86, " +
                                "COUNT(CASE WHEN MD87 = 1 THEN 1 WHEN MD87 = 2 THEN 1 WHEN MD87 = 3 THEN 1 WHEN MD87 = 4 THEN 1  END) MD87, " +
                                "COUNT(CASE WHEN MD88 = 1 THEN 1 WHEN MD88 = 2 THEN 1 WHEN MD88 = 3 THEN 1 WHEN MD88 = 4 THEN 1  END) MD88, " +
                                "COUNT(CASE WHEN MD89 = 1 THEN 1 WHEN MD89 = 2 THEN 1 WHEN MD89 = 3 THEN 1 WHEN MD89 = 4 THEN 1  END) MD89, " +
                                "COUNT(CASE WHEN MD90 = 1 THEN 1 WHEN MD90 = 2 THEN 1 WHEN MD90 = 3 THEN 1 WHEN MD90 = 4 THEN 1  END) MD90, " +
                                "COUNT(CASE WHEN MD91 = 1 THEN 1 WHEN MD91 = 2 THEN 1 WHEN MD91 = 3 THEN 1 WHEN MD91 = 4 THEN 1  END) MD91, " +
                                "COUNT(CASE WHEN MD92 = 1 THEN 1 WHEN MD92 = 2 THEN 1 WHEN MD92 = 3 THEN 1 WHEN MD92 = 4 THEN 1  END) MD92, " +
                                "COUNT(CASE WHEN MD93 = 1 THEN 1 WHEN MD93 = 2 THEN 1 WHEN MD93 = 3 THEN 1 WHEN MD93 = 4 THEN 1  END) MD93, " +
                                "COUNT(CASE WHEN MD94 = 1 THEN 1 WHEN MD94 = 2 THEN 1 WHEN MD94 = 3 THEN 1 WHEN MD94 = 4 THEN 1  END) MD94, " +
                                "COUNT(CASE WHEN MD95 = 1 THEN 1 WHEN MD95 = 2 THEN 1 WHEN MD95 = 3 THEN 1 WHEN MD95 = 4 THEN 1  END) MD95, " +
                                "COUNT(CASE WHEN MD96 = 1 THEN 1 WHEN MD96 = 2 THEN 1 WHEN MD96 = 3 THEN 1 WHEN MD96 = 4 THEN 1  END) MD96, " +
                                "COUNT(CASE WHEN MD97 = 1 THEN 1 WHEN MD97 = 2 THEN 1 WHEN MD97 = 3 THEN 1 WHEN MD97 = 4 THEN 1  END) MD97, " +
                                "COUNT(CASE WHEN MD98 = 1 THEN 1 WHEN MD98 = 2 THEN 1 WHEN MD98 = 3 THEN 1 WHEN MD98 = 4 THEN 1  END) MD98, " +
                                "COUNT(CASE WHEN MD99 = 1 THEN 1 WHEN MD99 = 2 THEN 1 WHEN MD99 = 3 THEN 1 WHEN MD99 = 4 THEN 1  END) MD99, " +
                                "COUNT(CASE WHEN MD100 = 1 THEN 1 WHEN MD100 = 2 THEN 1 WHEN MD100 = 3 THEN 1 WHEN MD100 = 4 THEN 1  END) MD100, " +
                                "COUNT(CASE WHEN MD101 = 1 THEN 1 WHEN MD101 = 2 THEN 1 WHEN MD101 = 3 THEN 1 WHEN MD101 = 4 THEN 1  END) MD101, " +
                                "COUNT(CASE WHEN MD102 = 1 THEN 1 WHEN MD102 = 2 THEN 1 WHEN MD102 = 3 THEN 1 WHEN MD102 = 4 THEN 1  END) MD102, " +
                                "COUNT(CASE WHEN MD103 = 1 THEN 1 WHEN MD103 = 2 THEN 1 WHEN MD103 = 3 THEN 1 WHEN MD103 = 4 THEN 1  END) MD103, " +
                                "COUNT(CASE WHEN MD104 = 1 THEN 1 WHEN MD104 = 2 THEN 1 WHEN MD104 = 3 THEN 1 WHEN MD104 = 4 THEN 1  END) MD104, " +
                                "COUNT(CASE WHEN MD105 = 1 THEN 1 WHEN MD105 = 2 THEN 1 WHEN MD105 = 3 THEN 1 WHEN MD105 = 4 THEN 1  END) MD105, " +
                                "COUNT(CASE WHEN MD106 = 1 THEN 1 WHEN MD106 = 2 THEN 1 WHEN MD106 = 3 THEN 1 WHEN MD106 = 4 THEN 1  END) MD106, " +
                                "COUNT(CASE WHEN MD158 = 1 THEN 1 WHEN MD158 = 2 THEN 1 WHEN MD158 = 3 THEN 1 WHEN MD158 = 4 THEN 1  END) MD158, " +
                                "COUNT(CASE WHEN MD159 = 1 THEN 1 WHEN MD159 = 2 THEN 1 WHEN MD159 = 3 THEN 1 WHEN MD159 = 4 THEN 1  END) MD159, " +
                                "COUNT(CASE WHEN MD160 = 1 THEN 1 WHEN MD160 = 2 THEN 1 WHEN MD160 = 3 THEN 1 WHEN MD160 = 4 THEN 1  END) MD160, " +
                                "COUNT(CASE WHEN MD161 = 1 THEN 1 WHEN MD161 = 2 THEN 1 WHEN MD161 = 3 THEN 1 WHEN MD161 = 4 THEN 1  END) MD161, " +
                                "COUNT(CASE WHEN MD162 = 1 THEN 1 WHEN MD162 = 2 THEN 1 WHEN MD162 = 3 THEN 1 WHEN MD162 = 4 THEN 1  END) MD162, " +
                                "COUNT(CASE WHEN MD165 = 1 THEN 1 WHEN MD165 = 2 THEN 1 WHEN MD165 = 3 THEN 1 WHEN MD165 = 4 THEN 1  END) MD165, " +
                                "COUNT(CASE WHEN MD166 = 1 THEN 1 WHEN MD166 = 2 THEN 1 WHEN MD166 = 3 THEN 1 WHEN MD166 = 4 THEN 1  END) MD166, " +
                                "COUNT(CASE WHEN MD167 = 1 THEN 1 WHEN MD167 = 2 THEN 1 WHEN MD167 = 3 THEN 1 WHEN MD167 = 4 THEN 1  END) MD167, " +
                                "COUNT(CASE WHEN MD168 = 1 THEN 1 WHEN MD168 = 2 THEN 1 WHEN MD168 = 3 THEN 1 WHEN MD168 = 4 THEN 1  END) MD168, " +
                                "COUNT(CASE WHEN MD169 = 1 THEN 1 WHEN MD169 = 2 THEN 1 WHEN MD169 = 3 THEN 1 WHEN MD169 = 4 THEN 1  END) MD169 " +
                                "FROM negtgel2 " +
                                "GROUP BY ORGID";
               

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search") != null && !string.IsNullOrEmpty(req.Element("Search").Value) ? req.Element("Search")?.Value : null, System.Data.ParameterDirection.Input);


                DataTable dtTable = new DataTable();
                dtTable.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);
                dtTable.TableName = "Report1N2";


                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "WITH Negtgel2 AS( " +
                               "SELECT* FROM( " +
                               "SELECT ORGID, YEARCODE, INSERTUSERID, MDCODE, DATA01, ROP.OPEN_ENT_NAME AS ORGNAME, C.BUDGET_TYPE_NAME AS ORGTYPE, ROP.OPEN_HEAD_ROLE, ROP.OPEN_HEAD_NAME, ROP.OPEN_HEAD_PHONE, ROP.OPEN_ACC_ROLE, ROP.OPEN_ACC_NAME , ROP.OPEN_ACC_PHONE FROM AUD_MIRRORACC.SHILENDANSDATA A " +
                               "LEFT JOIN AUD_MIRRORACC.OPENACC_ENTITY ROP ON A.ORGID = ROP.OPEN_ID " +
                               "LEFT JOIN AUD_MIRRORACC.REF_BUDGET_TYPE C ON ROP.OPEN_ENT_BUDGET_TYPE = C.BUDGET_TYPE_ID " +
                               "WHERE MDCODE IN(33,34,37,38,39,40,41,42,43,44,45,46,47,48,49,57,58,59,60,61,62,50,51,52,53,54,55,56,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,158,159,160,161,162,165,166,167,168,169) " +
                               "AND UPPER(ROP.OPEN_ENT_NAME) LIKE '%'|| UPPER(:V_SEARCH) ||'%' AND OPEN_ENT_DEPARTMENT_ID = :V_DEPARTMENT " +
                               //"AND rop.OPEN_ENT_NAME like '%ИТХ/ДЭЛГЭРХААН/%' " +
                               ") D " +
                               "PIVOT( " +
                               "MAX(DATA01) " +
                               "FOR MDCODE IN(33 MD33, 34 MD34, 37 MD37, 38 MD38, 39 MD39, 40 MD40, 41 MD41, 42 MD42, 43 MD43, 44 MD44, 45 MD45, 46 MD46, 47 MD47, 48 MD48, 49 MD49, 56 MD56, 57 MD57, 58 MD58, 59 MD59, 60 MD60, 61 MD61, 62 MD62, 50 MD50, 51 MD51, 52 MD52, 53 MD53, 54 MD54, 55 MD55, 63 MD63, 64 MD64, 65 MD65, 66 MD66, 67 MD67, 68 MD68, 69 MD69, 70 MD70, 71 MD71, 72 MD72, 73 MD73, 74 MD74, 75 MD75, 76 MD76, 77 MD77, 78 MD78, 79 MD79, 80 MD80, 81 MD81, 82 MD82, 83 MD83, 84 MD84, 85 MD85, 86 MD86, 87 MD87, 88 MD88, 89 MD89, 90 MD90, 91 MD91, 92 MD92, 93 MD93, 94 MD94, 95 MD95, 96 MD96, 97 MD97, 98 MD98, 99 MD99, 100 MD100, 101 MD101, 102 MD102, 103 MD103, 104 MD104, 105 MD105, 106 MD106, 158 MD158, 159 MD159, 160 MD160, 161 Md161, 162 MD162, 165 MD165, 166 MD166, 167 MD167, 168 MD168, 169 MD169) " +
                               ") P1 " +
                               ") " +
                               "SELECT ORGID, ORGNAME, ORGTYPE, INSERTUSERID, " +
                               "MD33, MD34, MD37, MD38, MD39, MD40, MD41, MD42, MD43, MD44, MD45, MD46, MD47, MD48, MD49, MD56, MD57, MD58, MD59, MD60, MD61, MD62, MD50, MD51, MD52, MD53, MD54, MD55, MD63, MD64, MD65, MD66, MD67, MD68, MD69, MD70, MD71, MD72, MD73, MD74, MD75, MD76, MD77, MD78, MD79, MD80, MD81, MD82, MD83, MD84, MD85, MD86, MD87, MD88, MD89, MD90, MD91, MD92, MD93, MD94, MD95, MD96, MD97, MD98, MD99, MD100, MD101, MD102, MD103, MD104, MD105, MD106, MD158, MD159, MD160, Md161, MD162, MD165, MD166, MD167, MD168, MD169 " +
                               "FROM Negtgel2";

                cmd.BindByName = true;
                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search") != null && !string.IsNullOrEmpty(req.Element("Search").Value) ? req.Element("Search")?.Value : null, System.Data.ParameterDirection.Input);
                

                DataTable dtTable2 = new DataTable();
                dtTable2.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);
                dtTable2.TableName = "Report1N2Footer";

                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(dtTable);
                dataSet.Tables.Add(dtTable2);
                cmd.Dispose();
                con.Close();



                StringWriter sw = new StringWriter();
                dataSet.WriteXml(sw, XmlWriteMode.WriteSchema);

                XElement xmlResponseData = XElement.Parse(sw.ToString());
                response.CreateResponse(xmlResponseData);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        #endregion

    }
}