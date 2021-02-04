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

                if (ds.Tables.Contains("Aimag") && ds.Tables.Contains("AimagSoum"))
                {
                    DataRelation relation = new DataRelation("ParentChild", ds.Tables["Aimag"].Columns["ID"], ds.Tables["AimagSoum"].Columns["AimagID"], true);
                    relation.Nested = true;
                    ds.Relations.Add(relation);
                }

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
                SqlCommand cmd = new SqlCommand("UserLogin");
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = request.Element("Parameters").Element("Username").Value;
                cmd.Parameters.Add("@UserPassword", SqlDbType.NVarChar, 50).Value = request.Element("Parameters").Element("Password").Value;

                cmd.Parameters.Add("@ResponseCode", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ResponseMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                DataSet ds = DataConnection.GetDataSet(cmd);

                bool responseValue = Convert.ToBoolean(cmd.Parameters["@Return"].Value);
                string responseCode = cmd.Parameters["@ResponseCode"].Value.ToString();
                string responseMsg = cmd.Parameters["@ResponseMessage"].Value.ToString();

                if (responseValue)
                {

                    ds.DataSetName = "ResponseData";
                    if (ds.Tables.Count > 0)
                    {
                        ds.Tables[0].TableName = "User";
                    }

                    StringWriter sw = new StringWriter();
                    ds.WriteXml(sw, XmlWriteMode.IgnoreSchema);

                    XElement xmlResponseData = XElement.Parse(sw.ToString());
                    response.CreateResponse(xmlResponseData);
                }
                //Return response
                response.CreateResponse(responseValue, responseCode, responseMsg);
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public static DataResponse Dashboard(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                SqlCommand cmd = new SqlCommand("Dashboard");
                DataSet ds = DataConnection.GetDataSet(cmd);

                ds.DataSetName = "ResponseData";
                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].TableName = "Count1";
                    ds.Tables[1].TableName = "Count2";
                    ds.Tables[2].TableName = "Count3";
                    ds.Tables[3].TableName = "Count4";
                    ds.Tables[4].TableName = "Count5";
                    ds.Tables[5].TableName = "Count6";
                }

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
        public static DataResponse OrgList(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                SqlCommand cmd = new SqlCommand("OrgList");

                DataSet ds = DataConnection.GetDataSet(cmd);

                ds.DataSetName = "ResponseData";
                if (ds.Tables.Count > 0) ds.Tables[0].TableName = "OrgList";

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
        public static DataResponse OrgAddEdit(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                SqlCommand cmd = new SqlCommand("OrgAddEdit");
                if (request.Element("Parameters").Element("OrgID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("OrgID").Value))
                    cmd.Parameters.Add("@OrgID", SqlDbType.Int).Value = request.Element("Parameters").Element("OrgID").Value;
                if (request.Element("Parameters").Element("ORG_CODE") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ORG_CODE").Value))
                    cmd.Parameters.Add("@ORG_CODE", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("ORG_CODE").Value;
                if (request.Element("Parameters").Element("REGISTER_NO") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("REGISTER_NO").Value))
                    cmd.Parameters.Add("@REGISTER_NO", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("REGISTER_NO").Value;
                if (request.Element("Parameters").Element("UB_NUMBER") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("UB_NUMBER").Value))
                    cmd.Parameters.Add("@UB_NUMBER", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("UB_NUMBER").Value;
                if (request.Element("Parameters").Element("ORG_NAME") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ORG_NAME").Value))
                    cmd.Parameters.Add("@ORG_NAME", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("ORG_NAME").Value;                
                if (request.Element("Parameters").Element("REG_DATE") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("REG_DATE").Value))
                    cmd.Parameters.Add("@REG_DATE", SqlDbType.DateTime).Value = request.Element("Parameters").Element("REG_DATE").Value;

                if (request.Element("Parameters").Element("ORG_PHONE") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ORG_PHONE").Value))
                    cmd.Parameters.Add("@ORG_PHONE", SqlDbType.VarChar).Value = request.Element("Parameters").Element("ORG_PHONE").Value;
                if (request.Element("Parameters").Element("EMAIL") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("EMAIL").Value))
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = request.Element("Parameters").Element("EMAIL").Value;
                if (request.Element("Parameters").Element("AimagID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("AimagID").Value))
                    cmd.Parameters.Add("@AimagID", SqlDbType.Int).Value = request.Element("Parameters").Element("AimagID").Value;
                if (request.Element("Parameters").Element("SoumID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("SoumID").Value))
                    cmd.Parameters.Add("@SoumID", SqlDbType.Int).Value = request.Element("Parameters").Element("SoumID").Value;
                if (request.Element("Parameters").Element("ORG_ADDRESS") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ORG_ADDRESS").Value))
                    cmd.Parameters.Add("@ORG_ADDRESS", SqlDbType.VarChar).Value = request.Element("Parameters").Element("ORG_ADDRESS").Value;
                if (request.Element("Parameters").Element("WEBSITE") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("WEBSITE").Value))
                    cmd.Parameters.Add("@WEBSITE", SqlDbType.VarChar).Value = request.Element("Parameters").Element("WEBSITE").Value;
                if (request.Element("Parameters").Element("FAX") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("FAX").Value))
                    cmd.Parameters.Add("@FAX", SqlDbType.VarChar).Value = request.Element("Parameters").Element("FAX").Value;

                if (request.Element("Parameters").Element("BankList") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("BankList").Value))
                    cmd.Parameters.Add("@BankList", SqlDbType.Xml).Value = request.Element("Parameters").Element("BankList").ToString();

                if (request.Element("Parameters").Element("TusuwZakhiragchID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("TusuwZakhiragchID").Value))
                    cmd.Parameters.Add("@TusuwZakhiragchID", SqlDbType.Int).Value = request.Element("Parameters").Element("TusuwZakhiragchID").Value;
                if (request.Element("Parameters").Element("KhelberID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("KhelberID").Value))
                    cmd.Parameters.Add("@KhelberID", SqlDbType.Int).Value = request.Element("Parameters").Element("KhelberID").Value;
                if (request.Element("Parameters").Element("KhorooID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("KhorooID").Value))
                    cmd.Parameters.Add("@KhorooID", SqlDbType.Int).Value = request.Element("Parameters").Element("KhorooID").Value;
                if (request.Element("Parameters").Element("ZardlinAngilalID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ZardlinAngilalID").Value))
                    cmd.Parameters.Add("@ZardlinAngilalID", SqlDbType.Int).Value = request.Element("Parameters").Element("ZardlinAngilalID").Value;
                if (request.Element("Parameters").Element("SankhuujiltID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("SankhuujiltID").Value))
                    cmd.Parameters.Add("@SankhuujiltID", SqlDbType.Int).Value = request.Element("Parameters").Element("SankhuujiltID").Value;
                if (request.Element("Parameters").Element("UilAjillagaaID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("UilAjillagaaID").Value))
                    cmd.Parameters.Add("@UilAjillagaaID", SqlDbType.Int).Value = request.Element("Parameters").Element("UilAjillagaaID").Value;
                if (request.Element("Parameters").Element("AlbaID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("AlbaID").Value))
                    cmd.Parameters.Add("@AlbaID", SqlDbType.Int).Value = request.Element("Parameters").Element("AlbaID").Value;
                if (request.Element("Parameters").Element("TatwarID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("TatwarID").Value))
                    cmd.Parameters.Add("@TatwarID", SqlDbType.Int).Value = request.Element("Parameters").Element("TatwarID").Value;
                if (request.Element("Parameters").Element("ND_BaiguullagaID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ND_BaiguullagaID").Value))
                    cmd.Parameters.Add("@ND_BaiguullagaID", SqlDbType.Int).Value = request.Element("Parameters").Element("ND_BaiguullagaID").Value;
                if (request.Element("Parameters").Element("Description") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("Description").Value))
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar,250).Value = request.Element("Parameters").Element("Description").Value;

                if (request.Element("Parameters").Element("HEAD_PERSONID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("HEAD_PERSONID").Value))
                    cmd.Parameters.Add("@HEAD_PERSONID", SqlDbType.Int).Value = request.Element("Parameters").Element("HEAD_PERSONID").Value;
                if (request.Element("Parameters").Element("HEAD_ROLE") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("HEAD_ROLE").Value))
                    cmd.Parameters.Add("@HEAD_ROLE", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("HEAD_ROLE").Value;
                if (request.Element("Parameters").Element("HEAD_FIRSTNAME") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("HEAD_FIRSTNAME").Value))
                    cmd.Parameters.Add("@HEAD_FIRSTNAME", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("HEAD_FIRSTNAME").Value;
                if (request.Element("Parameters").Element("HEAD_LASTNAME") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("HEAD_LASTNAME").Value))
                    cmd.Parameters.Add("@HEAD_LASTNAME", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("HEAD_LASTNAME").Value;
                if (request.Element("Parameters").Element("HEAD_REGISTER") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("HEAD_REGISTER").Value))
                    cmd.Parameters.Add("@HEAD_REGISTER", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("HEAD_REGISTER").Value;
                if (request.Element("Parameters").Element("HEAD_PHONE") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("HEAD_PHONE").Value))
                    cmd.Parameters.Add("@HEAD_PHONE", SqlDbType.VarChar).Value = request.Element("Parameters").Element("HEAD_PHONE").Value;
                if (request.Element("Parameters").Element("HEAD_EMAIL") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("HEAD_EMAIL").Value))
                    cmd.Parameters.Add("@HEAD_EMAIL", SqlDbType.VarChar).Value = request.Element("Parameters").Element("HEAD_EMAIL").Value;
                if (request.Element("Parameters").Element("HEAD_YEAR") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("HEAD_YEAR").Value))
                    cmd.Parameters.Add("@HEAD_YEAR", SqlDbType.Float).Value = request.Element("Parameters").Element("HEAD_YEAR").Value;
                if (request.Element("Parameters").Element("HEAD_PROFESSION") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("HEAD_PROFESSION").Value))
                    cmd.Parameters.Add("@HEAD_PROFESSION", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("HEAD_PROFESSION").Value;
                
                if (request.Element("Parameters").Element("ACCOUNTANT_PERSONID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ACCOUNTANT_PERSONID").Value))
                    cmd.Parameters.Add("@ACCOUNTANT_PERSONID", SqlDbType.Int).Value = request.Element("Parameters").Element("ACCOUNTANT_PERSONID").Value;
                if (request.Element("Parameters").Element("ACCOUNTANT_ROLE") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ACCOUNTANT_ROLE").Value))
                    cmd.Parameters.Add("@ACCOUNTANT_ROLE", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("ACCOUNTANT_ROLE").Value;
                if (request.Element("Parameters").Element("ACCOUNTANT_FIRSTNAME") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ACCOUNTANT_FIRSTNAME").Value))
                    cmd.Parameters.Add("@ACCOUNTANT_FIRSTNAME", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("ACCOUNTANT_FIRSTNAME").Value;
                if (request.Element("Parameters").Element("ACCOUNTANT_LASTNAME") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ACCOUNTANT_LASTNAME").Value))
                    cmd.Parameters.Add("@ACCOUNTANT_LASTNAME", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("ACCOUNTANT_LASTNAME").Value;
                if (request.Element("Parameters").Element("ACCOUNTANT_REGISTER") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ACCOUNTANT_REGISTER").Value))
                    cmd.Parameters.Add("@ACCOUNTANT_REGISTER", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("ACCOUNTANT_REGISTER").Value;
                if (request.Element("Parameters").Element("ACCOUNTANT_PHONE") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ACCOUNTANT_PHONE").Value))
                    cmd.Parameters.Add("@ACCOUNTANT_PHONE", SqlDbType.VarChar).Value = request.Element("Parameters").Element("ACCOUNTANT_PHONE").Value;
                if (request.Element("Parameters").Element("ACCOUNTANT_EMAIL") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ACCOUNTANT_EMAIL").Value))
                    cmd.Parameters.Add("@ACCOUNTANT_EMAIL", SqlDbType.VarChar).Value = request.Element("Parameters").Element("ACCOUNTANT_EMAIL").Value;
                if (request.Element("Parameters").Element("ACCOUNTANT_YEAR") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ACCOUNTANT_YEAR").Value))
                    cmd.Parameters.Add("@ACCOUNTANT_YEAR", SqlDbType.Float).Value = request.Element("Parameters").Element("ACCOUNTANT_YEAR").Value;
                if (request.Element("Parameters").Element("ACCOUNTANT_PROFESSION") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ACCOUNTANT_PROFESSION").Value))
                    cmd.Parameters.Add("@ACCOUNTANT_PROFESSION", SqlDbType.NVarChar).Value = request.Element("Parameters").Element("ACCOUNTANT_PROFESSION").Value;

                if (request.Element("Parameters").Element("InActiveReasonID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("InActiveReasonID").Value))
                    cmd.Parameters.Add("@InActiveReasonID", SqlDbType.Int).Value = request.Element("Parameters").Element("InActiveReasonID").Value;
                if (request.Element("Parameters").Element("ParentID") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("ParentID").Value))
                    cmd.Parameters.Add("@ParentID", SqlDbType.Int).Value = request.Element("Parameters").Element("ParentID").Value;
                if (request.Element("Parameters").Element("IsActive") != null && !string.IsNullOrEmpty(request.Element("Parameters").Element("IsActive").Value))
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = request.Element("Parameters").Element("IsActive").Value;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = request.Element("Parameters").Element("UserID").Value;

                cmd.Parameters.Add("@ResponseCode", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ResponseMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Return", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                DataConnection.ExecuteProcedure(cmd);

                bool responseValue = Convert.ToBoolean(cmd.Parameters["@Return"].Value);
                string responseCode = cmd.Parameters["@ResponseCode"].Value.ToString();
                string responseMsg = cmd.Parameters["@ResponseMessage"].Value.ToString();

                //Return response
                response.CreateResponse(responseValue, responseCode, responseMsg);
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
                SqlCommand cmd = new SqlCommand("OrgDetail");
                cmd.Parameters.Add("@OrgID", SqlDbType.Int).Value = request.Element("Parameters").Element("OrgID").Value;

                DataSet ds = DataConnection.GetDataSet(cmd);

                ds.DataSetName = "ResponseData";
                if (ds.Tables.Count > 0) ds.Tables[0].TableName = "Org";
                if (ds.Tables.Count > 0) ds.Tables[1].TableName = "OrgBank";
                if (ds.Tables.Count > 0) ds.Tables[2].TableName = "OrgPerson";

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
    }
}