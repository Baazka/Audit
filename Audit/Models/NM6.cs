using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class NM6VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class NM6
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        
        public int VIOLATION_COUNT { get; set; }
        public decimal VIOLATION_AMOUNT { get; set; }
        public int ERROR_COUNT { get; set; }
        public decimal ERROR_AMOUNT { get; set; }
        public int ALL_COUNT { get; set; }
        public decimal ALL_AMOUNT { get; set; }
        public int CORRECTED_ERROR_COUNT { get; set; }
        public decimal CORRECTED_ERROR_AMOUNT { get; set; }
        public int OTHER_ERROR_COUNT { get; set; }
        public decimal OTHER_ERROR_AMOUNT { get; set; }
        public int ACT_COUNT { get; set; }
        public decimal ACT_AMOUNT { get; set; }
        public int CLAIM_COUNT { get; set; }
        public decimal CLAIM_AMOUNT { get; set; }
        public int REFERENCE_COUNT { get; set; }
        public decimal REFERENCE_AMOUNT { get; set; }
        public int PROPOSAL_COUNT { get; set; }
        public decimal PROPOSAL_AMOUNT { get; set; }
        public int LAW_COUNT { get; set; }
        public decimal LAW_AMOUNT { get; set; }
        public int OTHER_COUNT { get; set; }
        public decimal OTHER_AMOUNT { get; set; }

        public int IS_ACTIVE { get; set; } = 1;
        public int EXEC_TYPE { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public NM6 SetXml(XElement xml)
        {
            if (xml != null)
            {
                if (xml.Element("ID") != null)
                    ID = Convert.ToInt32(xml.Element("ID").Value);
                if (xml.Element("OFFICE_ID") != null)
                    OFFICE_ID = Convert.ToInt32(xml.Element("OFFICE_ID").Value);
                if (xml.Element("DEPARTMENT_NAME") != null)
                    DEPARTMENT_NAME = xml.Element("DEPARTMENT_NAME").Value;
                if (xml.Element("STATISTIC_PERIOD") != null)
                    STATISTIC_PERIOD = Convert.ToInt32(xml.Element("STATISTIC_PERIOD").Value);
                if (xml.Element("AUDIT_YEAR") != null)
                    AUDIT_YEAR = Convert.ToInt32(xml.Element("AUDIT_YEAR").Value);
                if (xml.Element("AUDIT_TYPE") != null)
                    AUDIT_TYPE = xml.Element("AUDIT_TYPE").Value;
                if (xml.Element("AUDIT_CODE") != null)
                    AUDIT_CODE = xml.Element("AUDIT_CODE").Value;
                if (xml.Element("AUDIT_NAME") != null)
                    AUDIT_NAME = xml.Element("AUDIT_NAME").Value;

                if (xml.Element("VIOLATION_COUNT") != null)
                    VIOLATION_COUNT = Convert.ToInt32(xml.Element("VIOLATION_COUNT").Value);
                if (xml.Element("VIOLATION_AMOUNT") != null)
                    VIOLATION_AMOUNT = Convert.ToDecimal(xml.Element("VIOLATION_AMOUNT").Value);
                if (xml.Element("ERROR_COUNT") != null)
                    ERROR_COUNT = Convert.ToInt32(xml.Element("ERROR_COUNT").Value);
                if (xml.Element("ERROR_AMOUNT") != null)
                    ERROR_AMOUNT = Convert.ToDecimal(xml.Element("ERROR_AMOUNT").Value);
                if (xml.Element("ALL_COUNT") != null)
                    ALL_COUNT = Convert.ToInt32(xml.Element("ALL_COUNT").Value);
                if (xml.Element("ALL_AMOUNT") != null)
                    ALL_AMOUNT = Convert.ToDecimal(xml.Element("ALL_AMOUNT").Value);
                if (xml.Element("CORRECTED_ERROR_COUNT") != null)
                    CORRECTED_ERROR_COUNT = Convert.ToInt32(xml.Element("CORRECTED_ERROR_COUNT").Value);
                if (xml.Element("CORRECTED_ERROR_AMOUNT") != null)
                    CORRECTED_ERROR_AMOUNT = Convert.ToDecimal(xml.Element("CORRECTED_ERROR_AMOUNT").Value);
                if (xml.Element("OTHER_ERROR_COUNT") != null)
                    OTHER_ERROR_COUNT = Convert.ToInt32(xml.Element("OTHER_ERROR_COUNT").Value);
                if (xml.Element("OTHER_ERROR_AMOUNT") != null)
                    OTHER_ERROR_AMOUNT = Convert.ToDecimal(xml.Element("OTHER_ERROR_AMOUNT").Value);
                if (xml.Element("ACT_COUNT") != null)
                    ACT_COUNT = Convert.ToInt32(xml.Element("ACT_COUNT").Value);
                if (xml.Element("ACT_AMOUNT") != null)
                    ACT_AMOUNT = Convert.ToDecimal(xml.Element("ACT_AMOUNT").Value);
                if (xml.Element("CLAIM_COUNT") != null)
                    CLAIM_COUNT = Convert.ToInt32(xml.Element("CLAIM_COUNT").Value);
                if (xml.Element("CLAIM_AMOUNT") != null)
                    CLAIM_AMOUNT = Convert.ToDecimal(xml.Element("CLAIM_AMOUNT").Value);
                if (xml.Element("REFERENCE_COUNT") != null)
                    REFERENCE_COUNT = Convert.ToInt32(xml.Element("REFERENCE_COUNT").Value);
                if (xml.Element("REFERENCE_AMOUNT") != null)
                    REFERENCE_AMOUNT = Convert.ToDecimal(xml.Element("REFERENCE_AMOUNT").Value);
                if (xml.Element("PROPOSAL_COUNT") != null)
                    PROPOSAL_COUNT = Convert.ToInt32(xml.Element("PROPOSAL_COUNT").Value);
                if (xml.Element("PROPOSAL_AMOUNT") != null)
                    PROPOSAL_AMOUNT = Convert.ToDecimal(xml.Element("PROPOSAL_AMOUNT").Value);
                if (xml.Element("LAW_COUNT") != null)
                    LAW_COUNT = Convert.ToInt32(xml.Element("LAW_COUNT").Value);
                if (xml.Element("LAW_AMOUNT") != null)
                    LAW_AMOUNT = Convert.ToDecimal(xml.Element("LAW_AMOUNT").Value);

                if (xml.Element("EXEC_TYPE") != null)
                    EXEC_TYPE = Convert.ToInt32(xml.Element("EXEC_TYPE").Value);
                if (xml.Element("CREATED_DATE") != null)
                    CREATED_DATE = Convert.ToDateTime(xml.Element("CREATED_DATE").Value);

            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("NM1",
                       new XElement("ID", ID),
                       new XElement("OFFICE_ID", OFFICE_ID),
                       new XElement("STATISTIC_PERIOD", STATISTIC_PERIOD),
                       //new XElement("AUDIT_YEAR", AUDIT_YEAR),
                       //new XElement("AUDIT_TYPE", AUDIT_TYPE),
                       //new XElement("AUDIT_CODE", AUDIT_CODE),
                       //new XElement("AUDIT_NAME", AUDIT_NAME),
                       //new XElement("AUDIT_BUDGET_TYPE", AUDIT_BUDGET_TYPE),
                       //new XElement("CORRECTED_ERROR_DESC", CORRECTED_ERROR_DESC),
                       //new XElement("CORRECTED_ERROR_TYPE", CORRECTED_ERROR_TYPE),
                       //new XElement("CORRECTED_COUNT", CORRECTED_COUNT),
                       //new XElement("CORRECTED_AMOUNT", CORRECTED_AMOUNT),
                       //new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", CREATED_DATE != null ? ((DateTime)CREATED_DATE).ToString("dd-MMM-yy") : null),
                       new XElement("UPDATED_DATE", UPDATED_DATE != null ? ((DateTime)UPDATED_DATE).ToString("dd-MMM-yy") : null)
                       );
        }
    }

}