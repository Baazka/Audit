using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class BM8
    {
        public string OFFICE_ID { get; set; }
        public string AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }
        public string CORRECTED_ERROR_DESC { get; set; }
        public string CORRECTED_ERROR_TYPE { get; set; }
        public string CORRECTED_COUNT { get; set; }
        public string CORRECTED_AMOUNT { get; set; }
        
        public BM8 SetXml(XElement xml)
        {
            if (xml != null)
            {
                if (xml.Element("OFFICE_ID") != null)
                    OFFICE_ID = xml.Element("OFFICE_ID").Value;
                if (xml.Element("AUDIT_YEAR") != null)
                    AUDIT_YEAR = xml.Element("AUDIT_YEAR").Value;
                if (xml.Element("AUDIT_TYPE") != null)
                    AUDIT_TYPE = xml.Element("AUDIT_TYPE").Value;
                if (xml.Element("AUDIT_CODE") != null)
                    AUDIT_CODE = xml.Element("AUDIT_CODE").Value;
                if (xml.Element("AUDIT_NAME") != null)
                    AUDIT_NAME = xml.Element("AUDIT_NAME").Value;
                if (xml.Element("AUDIT_BUDGET_TYPE") != null)
                    AUDIT_BUDGET_TYPE = xml.Element("AUDIT_BUDGET_TYPE").Value;
                if (xml.Element("CORRECTED_ERROR_DESC") != null)
                    CORRECTED_ERROR_DESC = xml.Element("CORRECTED_ERROR_DESC").Value;
                if (xml.Element("CORRECTED_ERROR_TYPE") != null)
                    CORRECTED_ERROR_TYPE = xml.Element("CORRECTED_ERROR_TYPE").Value;
                if (xml.Element("CORRECTED_COUNT") != null)
                    CORRECTED_COUNT = xml.Element("CORRECTED_COUNT").Value;
                if (xml.Element("CORRECTED_AMOUNT") != null)
                    CORRECTED_AMOUNT = xml.Element("CORRECTED_AMOUNT").Value;

            }
            return this;
        }
    }

}