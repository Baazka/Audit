using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.App_Func
{
    public class DataResponse
    {
        public bool Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public XElement XmlData { get; set; }

        public void CreateResponse()
        {
            Status = true;
            Code = string.Empty;
            Message = string.Empty;
        }

        public void CreateResponse(bool _status, string _code, string _message)
        {
            Status = _status;
            Code = _code;
            Message = _message;
        }

        public void CreateResponse(bool _status, string _code, string _message, XElement _xmlData)
        {
            Status = _status;
            Code = _code;
            Message = _message;
            XmlData = _xmlData;
        }

        public void CreateResponse(XElement _xmlData)
        {
            Status = true;
            Code = string.Empty;
            Message = string.Empty;
            XmlData = _xmlData;
        }

        public void CreateResponse(Exception _exception)
        {
            Status = false;
            Code = "E001";
            Message = _exception.Message;
        }
    }
}