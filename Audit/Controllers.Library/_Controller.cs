using Audit.App_Func;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Audit.Controllers.Library
{
    public abstract class _Controller
    {
        protected string UserID { get; set; }

        List<string> ErrorList { get; set; }

        public List<string> GetErrors()
        {
            if (ErrorList == null) ErrorList = new List<string>();

            return this.ErrorList;
        }

        public int ErrorCount()
        {
            if (ErrorList == null) ErrorList = new List<string>();

            return ErrorList.Count;
        }

        public void ClearError()
        {
            ErrorList = new List<string>();
        }

        public void AddError(string value)
        {
            if (ErrorList == null) ErrorList = new List<string>();

            ErrorList.Add(value);
        }

        public void AddError(string code, string value)
        {
            this.AddError(string.Format("{0}: {1}", code, value));
        }

        public void AddError(Exception exception)
        {
            this.AddError(string.Format("{0}: {1}", "Exception", exception.Message));
        }

        public bool IsValid
        {
            get { return ErrorList == null || ErrorList.Count == 0; }
        }

        public bool Status { get; set; }
        public string Message { get; set; }

        protected DataResponse Result { get; set; }

        protected DataResponse GetDataResponse(XElement requestXml)
        {
            Result = DataServiceAgent.GetResponse(requestXml);

            return Result;
        }
        internal class DataServiceAgent
        {
            static DataService _service = null;

            public static DataResponse GetResponse(XElement requestXml)
            {
                DataResponse response = null;

                if (_service == null)
                {
                    _service = new DataService();
                }

                response = _service.GetResponse(requestXml);

                return response;
            }

            public static DataSet DataSetFromXml(XElement xmlData)
            {
                DataSet ds = new DataSet();

                using (XmlReader xmlReader = xmlData.CreateReader())
                {
                    ds.ReadXml(xmlReader, XmlReadMode.ReadSchema);
                }

                return ds;
            }
        }
    }
}