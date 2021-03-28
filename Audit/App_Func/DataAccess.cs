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
                            "WHERE SU.USER_ID = :P_ID";

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
                cmd.Parameters.Add(":P_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_STATUS", OracleDbType.Varchar2, req.Element("V_STATUS")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_VIOLATION", OracleDbType.Varchar2, req.Element("V_VIOLATION")?.Value.Replace(",", "%"), System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

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
                    "AND (:V_VIOLATION IS NULL OR (R1.VIOLATION_DETAIL LIKE '%'||:V_VIOLATION||'%')) " +
                    "AND (:V_SEARCH IS NULL OR RD.DEPARTMENT_NAME LIKE '%'||:V_SEARCH||'%' " +
                    "OR R1.ORG_REGISTER_NO LIKE '%'||:V_SEARCH||'%' OR R1.ORG_NAME LIKE '%'||:V_SEARCH||'%' " +
                    "OR R1.ORG_CODE LIKE '%'||:V_SEARCH||'%' OR RB.BUDGET_TYPE_NAME LIKE '%'||:V_SEARCH||'%' " +
                    "OR R1.VIOLATION_DETAIL LIKE '%'||:V_SEARCH||'%' OR R1.INFORMATION_DETAIL LIKE '%'||:V_SEARCH||'%' " +
                    "OR RS.STATUS_NAME LIKE '%'||:V_SEARCH||'%') " +
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

                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT")!=null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value: null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_STATUS", OracleDbType.Varchar2, req.Element("V_STATUS")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_VIOLATION", OracleDbType.Varchar2, req.Element("V_VIOLATION")?.Value.Replace(",", "%"), System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);
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

                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true : false;

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

                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true : false;

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
                        "WHERE REG_NO LIKE '%' || :V_SRCH || '%' OR ORG_NAME LIKE '%' || :V_SRCH || '%' AND ROWNUM <= 100";

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
                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true : false;
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

                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true : false;

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
                cmd.CommandText = "SELECT ID, OFFICE_ID ,STATISTIC_PERIOD ,AUDIT_YEAR, AUDIT_TYPE, AUDIT_CODE,AUDIT_NAME, VIOLATION_COUNT, VIOLATION_AMOUNT, ERROR_COUNT, ERROR_AMOUNT, ALL_COUNT, ALL_AMOUNT, CORRECTED_ERROR_COUNT, CORRECTED_ERROR_AMOUNT, OTHER_ERROR_COUNT, OTHER_ERROR_AMOUNT, ACT_COUNT, ACT_AMOUNT, CLAIM_COUNT, CLAIM_AMOUNT, REFERENCE_COUNT, REFERENCE_AMOUNT, PROPOSAL_COUNT, PROPOSAL_AMOUNT, LAW_COUNT, LAW_AMOUNT, OTHER_COUNT, OTHER_AMOUNT, EXEC_TYPE, CREATED_DATE FROM AUD_STAT.BM6_DATA";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                
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
                cmd.CommandText = "SELECT ID, OFFICE_ID, STATISTIC_PERIOD, AUDIT_YEAR, AUDIT_TYPE, AUDIT_CODE, AUDIT_NAME, DECISION_TYPE, INCOME_STATE_COUNT, INCOME_STATE_AMOUNT, INCOME_LOCAL_COUNT, INCOME_LOCAL_NUMBER, BUDGET_STATE_COUNT, BUDGET_STATE_AMOUNT, BUDGET_LOCAL_COUNT, BUDGET_LOCAL_AMOUNT, ACCOUNTANT_COUNT, ACCOUNTANT_AMOUNT, EFFICIENCY_COUNT, EFFICIENCY_AMOUNT, LAW_COUNT, LAW_AMOUNT, MONITORING_COUNT, MONITORING_AMOUNT, PURCHASE_COUNT, PURCHASE_AMOUNT, COST_COUNT, COST_AMOUNT, OTHER_COUNT, OTHER_AMOUNT, ALL_COUNT, ALL_AMOUNT, EXEC_TYPE, CREATED_DATE FROM AUD_STAT.BM7_DATA";

                // Set parameters
                cmd.Parameters.Add(":DEPARTMENT_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                
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
                XElement req = request.Element("Parameters").Element("Request");

                //RowCount
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "F_BM8_COUNT";

                OracleParameter retParam = cmd.Parameters.Add(":Ret_val",
                    OracleDbType.Int32, System.Data.ParameterDirection.ReturnValue);
                //cmd.Parameters.Add(":DEP_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID")?.Value, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_PERIOD", OracleDbType.Varchar2, req.Element("V_PERIOD") != null && !string.IsNullOrEmpty(req.Element("V_PERIOD").Value) ? req.Element("V_PERIOD")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":P_SEARCH", OracleDbType.Varchar2, req.Element("Search")?.Value, System.Data.ParameterDirection.Input);

                cmd.ExecuteNonQuery();

                cmd.Dispose();

                // Create and execute the command
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ID, OFFICE_ID, STATISTIC_PERIOD, AUDIT_YEAR, AUDIT_TYPE, AUDIT_CODE, AUDIT_NAME, AUDIT_BUDGET_TYPE, CORRECTED_ERROR_DESC, CORRECTED_ERROR_TYPE, CORRECTED_COUNT, CORRECTED_AMOUNT, EXEC_TYPE, CREATED_DATE FROM AUD_STAT.BM8_DATA " +
                    //"WHERE :DEP_ID = 2 OR(DEP_ID != 2 AND OFFICE_ID = :DEP_ID) " +
                    "where (:V_DEPARTMENT IS NULL OR OFFICE_ID = :V_DEPARTMENT) " +
                    "AND(:V_PERIOD IS NULL OR STATISTIC_PERIOD = :V_PERIOD) " +
                    "AND(:V_SEARCH IS NULL OR OFFICE_ID LIKE '%' || :V_SEARCH || '%' " +
                    "OR STATISTIC_PERIOD LIKE '%' || :V_SEARCH || '%' OR AUDIT_YEAR LIKE '%' || :V_SEARCH || '%' " +
                    "OR AUDIT_TYPE LIKE '%' || :V_SEARCH || '%' OR AUDIT_CODE LIKE '%' || :V_SEARCH || '%' " +
                    "OR AUDIT_NAME LIKE '%' || :V_SEARCH || '%' OR AUDIT_BUDGET_TYPE LIKE '%' || :V_SEARCH || '%' " +
                    "OR CORRECTED_ERROR_DESC LIKE '%' || :V_SEARCH || '%' OR CORRECTED_ERROR_TYPE LIKE '%' || :V_SEARCH || '%' " +
                    "OR CORRECTED_COUNT LIKE '%' || :V_SEARCH || '%' OR CORRECTED_AMOUNT LIKE '%' || :V_SEARCH || '%' " +
                    "OR EXEC_TYPE LIKE '%' || :V_SEARCH || '%' OR CREATED_DATE LIKE '%' || :V_SEARCH || '%') " +
                    "ORDER BY " +
                    "CASE WHEN :ORDER_NAME IS NULL AND :ORDER_DIR IS NULL THEN ID END ASC,  " +
                    "CASE WHEN :ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'ASC' THEN OFFICE_ID END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'OFFICE_ID' AND: ORDER_DIR = 'DESC' THEN OFFICE_ID END DESC, " +
                    "CASE WHEN: ORDER_NAME = 'STATISTIC_PERIOD' AND: ORDER_DIR = 'ASC' THEN STATISTIC_PERIOD END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'STATISTIC_PERIOD' AND: ORDER_DIR = 'DESC' THEN STATISTIC_PERIOD END DESC, " +
                    "CASE WHEN: ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'ASC' THEN AUDIT_YEAR END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'AUDIT_YEAR' AND: ORDER_DIR = 'DESC' THEN AUDIT_YEAR END DESC, " +
                    "CASE WHEN: ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'ASC' THEN AUDIT_TYPE END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'AUDIT_TYPE' AND: ORDER_DIR = 'DESC' THEN AUDIT_TYPE END DESC, " +
                    "CASE WHEN: ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'ASC' THEN AUDIT_CODE END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'AUDIT_CODE' AND: ORDER_DIR = 'DESC' THEN AUDIT_CODE END DESC, " +
                    "CASE WHEN: ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'ASC' THEN AUDIT_NAME END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'AUDIT_NAME' AND: ORDER_DIR = 'DESC' THEN AUDIT_NAME END DESC, " +
                    "CASE WHEN: ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND: ORDER_DIR = 'ASC' THEN AUDIT_BUDGET_TYPE END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'AUDIT_BUDGET_TYPE' AND: ORDER_DIR = 'DESC' THEN AUDIT_BUDGET_TYPE END DESC, " +
                    "CASE WHEN: ORDER_NAME = 'CORRECTED_ERROR_DESC' AND: ORDER_DIR = 'ASC' THEN CORRECTED_ERROR_DESC END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'CORRECTED_ERROR_DESC' AND: ORDER_DIR = 'DESC' THEN CORRECTED_ERROR_DESC END DESC, " +
                    "CASE WHEN: ORDER_NAME = 'CORRECTED_ERROR_TYPE' AND: ORDER_DIR = 'ASC' THEN CORRECTED_ERROR_TYPE END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'CORRECTED_ERROR_TYPE' AND: ORDER_DIR = 'DESC' THEN CORRECTED_ERROR_TYPE END DESC, " +
                    "CASE WHEN: ORDER_NAME = 'CORRECTED_COUNT' AND: ORDER_DIR = 'ASC' THEN CORRECTED_COUNT END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'CORRECTED_COUNT' AND: ORDER_DIR = 'DESC' THEN CORRECTED_COUNT END DESC, " +
                    "CASE WHEN: ORDER_NAME = 'CORRECTED_AMOUNT' AND: ORDER_DIR = 'ASC' THEN CORRECTED_AMOUNT END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'CORRECTED_AMOUNT' AND: ORDER_DIR = 'DESC' THEN CORRECTED_AMOUNT END DESC, " +
                    "CASE WHEN: ORDER_NAME = 'EXEC_TYPE' AND: ORDER_DIR = 'ASC' THEN EXEC_TYPE END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'EXEC_TYPE' AND: ORDER_DIR = 'DESC' THEN EXEC_TYPE END DESC, " +
                    "CASE WHEN: ORDER_NAME = 'CREATED_DATE' AND: ORDER_DIR = 'ASC' THEN CREATED_DATE END ASC, " +
                    "CASE WHEN: ORDER_NAME = 'CREATED_DATE' AND: ORDER_DIR = 'DESC' THEN CREATED_DATE END DESC " +
                    "OFFSET((: PAGENUMBER /:PAGESIZE) * :PAGESIZE) ROWS " +
                    "FETCH NEXT: PAGESIZE ROWS ONLY";

                cmd.BindByName = true;
                // Set parameters
                //cmd.Parameters.Add(":DEP_ID", OracleDbType.Int32, request.Element("Parameters").Element("DEPARTMENT_ID").Value, System.Data.ParameterDirection.Input);

                cmd.Parameters.Add(":V_DEPARTMENT", OracleDbType.Int32, req.Element("V_DEPARTMENT") != null && !string.IsNullOrEmpty(req.Element("V_DEPARTMENT").Value) ? req.Element("V_DEPARTMENT")?.Value : null, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add(":V_PERIOD", OracleDbType.Varchar2, req.Element("V_PERIOD")?.Value, System.Data.ParameterDirection.Input);
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
                xmlResponseData.Add(new XElement("RowCount", retParam.Value));
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
        #endregion
        #region CM
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
        #endregion

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
                cmd.CommandText = "SELECT PROJECT_NAME, PROJECT_NUMBER, PROJECT_START_DATE, PROJECT_END_DATE, PROJECT_PERCENT, PROJECT_TOTAL_BUDGET, PROJECT_ORG_FUND " +
                    "FROM AUD_MIRRORACC.ORG_PROJECT_LIST " +
                    "WHERE ORGID = :ORG_ID " +
                    "GROUP BY PROJECT_NAME, PROJECT_NUMBER, PROJECT_START_DATE, PROJECT_END_DATE, PROJECT_PERCENT, PROJECT_TOTAL_BUDGET, PROJECT_ORG_FUND ";

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
                        "WHERE A.PROJECT_NUMBER = :PROJECT_ID " +
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
                cmd.Parameters.Add(":P_USERID", OracleDbType.Int32).Value = request.Element("Parameters").Element("USER_ID").Value;
                cmd.Parameters.Add(":P_INSDATE", OracleDbType.Varchar2).Value = request.Element("Parameters").Element("INSDATE").Value;
                //cmd.ArrayBindCount = request.Element("Parameters").Element("MD_CODE").Value.Length;


                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                object responseValue = retParam.Value;

                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true : false;

                response.CreateResponse(responseVal, string.Empty, "Амжилттай хадгаллаа");
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
                cmd.Parameters.Add(":P_PROJECT_NUMBER", OracleDbType.Int32).Value = request.Element("Parameters").Element("PROJ_NUM")?.Value;
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
                //cmd.ArrayBindCount = request.Element("Parameters").Element("MD_CODE").Value.Length;


                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                object responseValue = retParam.Value;

                bool responseVal = Convert.ToInt32(responseValue.ToString()) != 0 ? true : false;

                response.CreateResponse(responseVal, string.Empty, "Амжилттай хадгаллаа");
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }

    }
}