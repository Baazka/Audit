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
        public XElement Library(XElement xLibraryName)
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
                                               new XElement("Function", "Library"),
                                               new XElement("libName", xLibraryName));

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
        public XElement MenuList(int userid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "MenuList"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid)));

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

        public XElement MenuRole(int userid,int menuid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "MenuRole"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   new XElement("MENU_ID", menuid)));

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
        public XElement UserProfile(string userid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "UserProfile"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid)));

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
        public bool UserCodeChange(int userid, string oldpass, string newpass, string updated_date)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "UserCodeChange"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   new XElement("USER_OLDCODE", oldpass),
                                                   new XElement("USER_NEWCODE", newpass),
                                                   new XElement("UPDATED_DATE", updated_date)));

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
        public XElement OrgList(XElement element, string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgList"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID),
                                                   element));

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
        public XElement OrgDetail(int orgid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgDetail"),
                                               new XElement("Parameters",
                                                   new XElement("ORG_ID", orgid)));

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
        public XElement OrgUB(string regno)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgUB"),
                                               new XElement("Parameters",
                                                   new XElement("REG_NO", regno)));

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
        public XElement OrgUBsingle(int regid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgUBsingle"),
                                               new XElement("Parameters",
                                                   new XElement("REG_ID", regid)));

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
        public XElement OrgMOF(string regno)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgMOF"),
                                               new XElement("Parameters",
                                                   new XElement("REG_NO", regno)));

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
        public XElement OrgMOFsingle(int regid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgMOFsingle"),
                                               new XElement("Parameters",
                                                   new XElement("REG_ID", regid)));

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
        public XElement OrgTAX(string regno)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgTAX"),
                                               new XElement("Parameters",
                                                   new XElement("REG_NO", regno)));

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
        public XElement OrgTAXsingle(int regid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgTAXsingle"),
                                               new XElement("Parameters",
                                                   new XElement("REG_ID", regid)));

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
        public bool OrgSave(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgSave"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool OrgInsert(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgInsert"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool OrgConfirm(int userid, int orgid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgConfirm"),
                                               new XElement("Parameters",
                                                   new XElement("ORG_ID", orgid),
                                                   new XElement("USER_ID", userid)));

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
        public bool OrgDelete(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgDelete"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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

        public XElement DataSearch(string vsearch)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "DataSearch"),
                                               new XElement("Parameters",
                                                   new XElement("V_SRCH", vsearch)));

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
        #region BM
        public XElement SystemUser()
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "SystemUser"));

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
        public  XElement BM0Search(int officeid, int periodid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM0Search"),
                                               new XElement("Parameters",
                                                   new XElement("OFFICE_ID", officeid),
                                                   new XElement("PERIOD_ID", periodid)));

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
        public XElement BM0(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM0"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement BM0Detail(int id)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM0Detail"),
                                               new XElement("Parameters",
                                                   new XElement("P_ID", id)));

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
        public bool BM0Update(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM0Update"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM0Insert(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM0Insert"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM0Delete(int userid, int id, string updatedate)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM0Delete"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   new XElement("ID", id),
                                                   new XElement("UPDATED_DATE", updatedate)));

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
        public XElement BM1(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM1"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement BM1Detail(int id)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM1Detail"),
                                               new XElement("Parameters",
                                                   new XElement("P_ID", id)));

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
        public bool BM1Update(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM1Update"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM1Insert(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM1Insert"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM1Delete(int userid, int id, string updatedate)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM1Delete"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   new XElement("ID", id),
                                                   new XElement("UPDATED_DATE", updatedate)));

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
        public XElement BM2(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM2"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement BM2Detail(int id)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM2Detail"),
                                               new XElement("Parameters",
                                                   new XElement("P_ID", id)));

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
        public bool BM2Update(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM2Update"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM2Insert(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM2Insert"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM2Delete(int userid, int id, string updatedate)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM2Delete"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   new XElement("ID", id),
                                                   new XElement("UPDATED_DATE", updatedate)));

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
        public XElement BM3(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM3"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement BM3Detail(int id)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM3Detail"),
                                               new XElement("Parameters",
                                                   new XElement("P_ID", id)));

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
        public bool BM3Update(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM3Update"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM3Insert(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM3Insert"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM3Delete(int userid, int id, string updatedate)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM3Delete"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   new XElement("ID", id),
                                                   new XElement("UPDATED_DATE", updatedate)));

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
        public XElement BM4(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM4"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement BM4Detail(int id)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM4Detail"),
                                               new XElement("Parameters",
                                                   new XElement("P_ID", id)));

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
        public bool BM4Update(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM4Update"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM4Insert(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM4Insert"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM4Delete(int userid, int id, string updatedate)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM4Delete"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   new XElement("ID", id),
                                                   new XElement("UPDATED_DATE", updatedate)));

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
        public XElement BM5(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM5"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement BM5Detail(int id)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM5Detail"),
                                               new XElement("Parameters",
                                                   new XElement("P_ID", id)));

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
        public bool BM5Update(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM5Update"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM5Insert(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM5Insert"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM5Delete(int userid, int id, string updatedate)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM5Delete"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   new XElement("ID", id),
                                                   new XElement("UPDATED_DATE", updatedate)));

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
        public XElement BM6(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM6"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement BM7(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM7"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement BM8(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM8"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement BM8Detail(int id)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM8Detail"),
                                               new XElement("Parameters",
                                                   new XElement("P_ID", id)));

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
        public bool BM8Update(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM8Update"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM8Insert(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM8Insert"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool BM8Delete(int userid, int id, string updatedate)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM8Delete"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   new XElement("ID", id),
                                                   new XElement("UPDATED_DATE", updatedate)));

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
        public XElement N1(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "N1"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        #endregion
        #region NM
        public XElement NM1(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM1"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement NM2(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM2"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement NM3(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM3"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement NM4(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM4"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement NM5(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM5"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement NM6(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM6"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement NM7(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM7"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        #endregion
        #region CM
        public XElement CM1(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM1"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement CM2(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM2"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement CM3(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM3"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement CM4(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM4"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement CM5(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM5"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement CM6(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM6"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement CM6Detail(int id)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM6Detail"),
                                               new XElement("Parameters",
                                                   new XElement("P_ID", id)));

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
        public bool CM6Update(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM6Update"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool CM6Insert(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM6Insert"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool CM6Delete(int userid, int id, string updatedate)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM6Delete"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   new XElement("ID", id),
                                                   new XElement("UPDATED_DATE", updatedate)));

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
        public XElement CM7(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM7"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement CM7Detail(int id)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM7Detail"),
                                               new XElement("Parameters",
                                                   new XElement("P_ID", id)));

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
        public bool CM7Update(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM7Update"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool CM7Insert(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM7Insert"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool CM7Delete(int userid, int id, string updatedate)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM7Delete"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   new XElement("ID", id),
                                                   new XElement("UPDATED_DATE", updatedate)));

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
        public XElement CM8(XElement element, string usertype)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM8"),
                                               new XElement("Parameters",
                                                   new XElement("USER_TYPE", usertype),
                                                   element));

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
        public XElement CM8Detail(int id)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM8Detail"),
                                               new XElement("Parameters",
                                                   new XElement("P_ID", id)));

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
        public bool CM8Update(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM8Update"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool CM8Insert(int userid, XElement element)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM8Insert"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   element));

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
        public bool CM8Delete(int userid, int id, string updatedate)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM8Delete"),
                                               new XElement("Parameters",
                                                   new XElement("USER_ID", userid),
                                                   new XElement("ID", id),
                                                   new XElement("UPDATED_DATE", updatedate)));

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
        public XElement OrgSearch(string vsearch)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgSearch"),
                                               new XElement("Parameters",
                                                   new XElement("V_SEARCH", vsearch)));

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
        #endregion

        #region Shilen 
        public XElement MirrorOrgList(XElement element, int departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "MirrorOrgList"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID),
                                                   element));

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
        public XElement MirrorOrgDetail(int openid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "MirrorOrgDetail"),
                                               new XElement("Parameters",
                                                   new XElement("OPEN_ID", openid)));

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
        public XElement Table1List()
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "Table1List"));

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

        public XElement MirrDataList(int orgid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "MirrDataList"),
                                               new XElement("Parameters",
                                               new XElement("ORGID", orgid)));

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
        public XElement PrintDataList(int orgid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "PrintDataList"),
                                               new XElement("Parameters",
                                               new XElement("ORGID", orgid)));

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
        public XElement Print2DataList(int orgid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "Print2DataList"),
                                               new XElement("Parameters",
                                               new XElement("ORGID", orgid)));

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
        public bool MirrorAccInsert(int yearcode, int orgid, int mdcodes, double data01, string data02, int is_finish ,int userid, DateTime Insdate)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "MirrorAccInsert"),
                                               new XElement("Parameters",
                                                   new XElement("YEAR_CODE", yearcode),
                                                   new XElement("ORG_ID", orgid),
                                                   new XElement("MD_CODE", mdcodes),
                                                   new XElement("DATA01", data01),
                                                   new XElement("DATA02", data02),
                                                   new XElement("ISFINISH", is_finish),
                                                   new XElement("USER_ID", userid),
                                                   new XElement("INSDATE", Insdate)));

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
        public bool OrgProjectInsert(int yearcode, int orgid, string project_name, string project_number, string project_start_date, string project_end_date, int project_percent, string project_budget, string project_fund ,int mdcodes, double data01, string data02, int userid, DateTime Insdate, int project_law_num, int project_id, int project_is_active)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return false; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgProjectInsert"),
                                               new XElement("Parameters",
                                                   new XElement("YEAR_CODE", yearcode),
                                                   new XElement("ORG_ID", orgid),
                                                   new XElement("PROJ_NAME", project_name),
                                                   new XElement("PROJ_NUM", project_number),
                                                   new XElement("PROJ_START_DATE", project_start_date),
                                                   new XElement("PROJ_END_DATE", project_end_date),
                                                   new XElement("PROJ_PERCENT", project_percent),
                                                   new XElement("PROJ_BUDGET", project_budget),
                                                   new XElement("PROJ_FUND", project_fund),
                                                   new XElement("MD_CODE", mdcodes),
                                                   new XElement("DATA01", data01),
                                                   new XElement("DATA02", data02),
                                                   new XElement("USER_ID", userid),
                                                   new XElement("INSDATE", Insdate),
                                                   new XElement("PROJ_LAW_NUM", project_law_num),
                                                   new XElement("PROJ_ID", project_id),
                                                   new XElement("PROJ_IS_ACTIVE", project_is_active)));

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

        public XElement TableProjectList(int orgid)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "TableProjectList"),
                                               new XElement("Parameters",
                                                   new XElement("ORG_ID", orgid)));

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
        public XElement OrgProjectDataList(int ID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgProjectDataList"),
                                               new XElement("Parameters",
                                               new XElement("PROJECT_ID", ID)));

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

        public XElement OrgProjectDelete(int org_id, int pro_id)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgProjectDelete"),
                                               new XElement("Parameters",
                                               new XElement("ORG_ID", org_id),
                                               new XElement("PRO_ID", pro_id)));

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

        #endregion'
    }
}