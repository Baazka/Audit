using System.Xml.Linq;

using Audit.App_Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Audit.Controllers.Library
{
    public class SystemController : _Controller
    {
        public XElement LibraryList(XElement xLibraryName)
        {
            try
            {
                ClearError();

                if (xLibraryName == null || !xLibraryName.HasElements)
                {
                    this.AddError("Параметер хоосон байна");
                }

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "LibraryList"),
                                               new XElement("Parameters", xLibraryName));

                DataResponse response = GetDataResponse(requestXml);

                if (!response.Status)
                {
                    this.AddError(response.Code, response.Message);
                }

                Message = response.Message;
                Status = response.Status;

                if (response.Status)
                {
                    return response.XmlData;
                }
            }
            catch (Exception ex)
            {
                this.AddError(ex);
            }

            return null;
        }
        public XElement UserLogin(string username, string password)
        {
            try
            {
                ClearError();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    this.AddError("Хэрэглэгчийн нэр, нууц үгийг оруулна уу");
                }

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "UserLogin"),
                                               new XElement("Parameters",
                                                   new XElement("Username", username),
                                                   new XElement("Password", password)));

                DataResponse response = GetDataResponse(requestXml);

                if (!response.Status)
                {
                    this.AddError(response.Code, response.Message);
                }

                Message = response.Message;
                Status = response.Status;

                if (response.Status)
                {
                    return response.XmlData;
                }
            }
            catch (Exception ex)
            {
                this.AddError(ex);
            }

            return null;
        }
        public XElement Dashboard()
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "Dashboard"));

                DataResponse response = GetDataResponse(requestXml);

                if (!response.Status)
                {
                    this.AddError(response.Code, response.Message);
                }

                Message = response.Message;
                Status = response.Status;

                if (response.Status)
                {
                    return response.XmlData;
                }
            }
            catch (Exception ex)
            {
                this.AddError(ex);
            }

            return null;
        }
        public XElement OrgList()
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgList"));

                DataResponse response = GetDataResponse(requestXml);

                if (!response.Status)
                {
                    this.AddError(response.Code, response.Message);
                }

                Message = response.Message;
                Status = response.Status;

                if (response.Status)
                {
                    return response.XmlData;
                }
            }
            catch (Exception ex)
            {
                this.AddError(ex);
            }

            return null;
        }
        public bool OrgAddEdit(XElement xOrg, int userid)
        {
            try
            {
                ClearError();

                if (xOrg == null)
                {
                    this.AddError("Мэдээлэл хоосон байна");
                }
                
                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgAddEdit"),
                                               new XElement("Parameters",
                                                   new XElement("UserID", userid),
                                                   xOrg.Element("OrgID"),
                                                   xOrg.Element("ORG_CODE"),
                                                   xOrg.Element("REGISTER_NO"),
                                                   xOrg.Element("UB_NUMBER"),
                                                   xOrg.Element("ORG_NAME"),
                                                   xOrg.Element("REG_DATE"),
                                                   xOrg.Element("ORG_PHONE"),
                                                   xOrg.Element("EMAIL"),
                                                   xOrg.Element("AimagID"),
                                                   xOrg.Element("SoumID"),
                                                   xOrg.Element("ORG_ADDRESS"),
                                                   xOrg.Element("WEBSITE"),
                                                   xOrg.Element("FAX"),

                                                   xOrg.Elements("BankList"),

                                                   xOrg.Element("TusuwZakhiragchID"),
                                                   xOrg.Element("KhelberID"),
                                                   xOrg.Element("KhorooID"),
                                                   xOrg.Element("ZardlinAngilalID"),
                                                   xOrg.Element("SankhuujiltID"),
                                                   xOrg.Element("UilAjillagaaID"),
                                                   xOrg.Element("AlbaID"),
                                                   xOrg.Element("TatwarID"),
                                                   xOrg.Element("ND_BaiguullagaID"),
                                                   xOrg.Element("Description"),

                                                   xOrg.Element("InActiveReasonID"),
                                                   xOrg.Element("ParentID"),
                                                   xOrg.Element("IsActive"),

                                                   xOrg.Element("HEAD_PERSONID"),
                                                   xOrg.Element("HEAD_ROLE"),
                                                   xOrg.Element("HEAD_FIRSTNAME"),
                                                   xOrg.Element("HEAD_LASTNAME"),
                                                   xOrg.Element("HEAD_REGISTER"),
                                                   xOrg.Element("HEAD_PHONE"),
                                                   xOrg.Element("HEAD_EMAIL"),
                                                   xOrg.Element("HEAD_YEAR"),
                                                   xOrg.Element("HEAD_PROFESSION"),

                                                   xOrg.Element("ACCOUNTANT_PERSONID"),
                                                   xOrg.Element("ACCOUNTANT_ROLE"),
                                                   xOrg.Element("ACCOUNTANT_FIRSTNAME"),
                                                   xOrg.Element("ACCOUNTANT_LASTNAME"),
                                                   xOrg.Element("ACCOUNTANT_REGISTER"),
                                                   xOrg.Element("ACCOUNTANT_PHONE"),
                                                   xOrg.Element("ACCOUNTANT_EMAIL"),
                                                   xOrg.Element("ACCOUNTANT_YEAR"),
                                                   xOrg.Element("ACCOUNTANT_PROFESSION")
                                                   ));

                DataResponse response = GetDataResponse(requestXml);

                if (!response.Status)
                {
                    this.AddError(response.Code, response.Message);
                }

                Message = response.Message;
                Status = response.Status;

                return response.Status;
            }
            catch (Exception ex)
            {
                this.AddError(ex);
            }

            return false;
        }
        public XElement OrgDetail(int OrgID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgDetail"),
                                                new XElement("Parameters",
                                                   new XElement("OrgID", OrgID)));

                DataResponse response = GetDataResponse(requestXml);

                if (!response.Status)
                {
                    this.AddError(response.Code, response.Message);
                }

                Message = response.Message;
                Status = response.Status;

                if (response.Status)
                {
                    return response.XmlData;
                }
            }
            catch (Exception ex)
            {
                this.AddError(ex);
            }

            return null;
        }
    }
}