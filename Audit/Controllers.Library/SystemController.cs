﻿using System.Xml.Linq;

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
        public XElement OrgList(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "OrgList"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement BM0(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM0"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement BM1(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM1"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement BM2(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM2"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement BM3(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM3"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement BM4(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM4"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement BM5(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM5"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement BM6(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM6"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement BM7(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM7"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement BM8(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "BM8"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement NM1(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM1"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement NM2(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM2"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement NM3(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM3"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement NM4(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM4"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement NM5(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM5"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement NM6(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM6"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement NM7(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "NM7"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM1A(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM1A"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM1B(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM1B"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM1C(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM1C"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM2A(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM2A"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM2B(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM2B"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM2C(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM2C"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM3A(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM3A"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM3B(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM3B"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM3C(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM3C"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM4A(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM4A"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM4B(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM4B"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM4C(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM4C"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM5(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM5"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM6(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM6"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM7(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM7"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
        public XElement CM8(string departmentID)
        {
            try
            {
                ClearError();

                if (!this.IsValid) { return null; }

                XElement requestXml = new XElement("Request",
                                               new XElement("Function", "CM8"),
                                               new XElement("Parameters",
                                                   new XElement("DEPARTMENT_ID", departmentID)));

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
    }
}