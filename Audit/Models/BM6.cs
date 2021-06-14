using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class BM6VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class BM6
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public int AUDIT_YEAR { get; set; }
        public int YEAR_LABEL { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string TOPIC_CODE { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        
        public string TOPIC_NAME { get; set; }
        public string AUDIT_NAME { get; set; }

        public int VIOLATION_COUNT { get; set; }
        public string VIOLATION_AMOUNT { get; set; }
        public int ERROR_COUNT { get; set; }
        public string ERROR_AMOUNT { get; set; }
        public int ALL_COUNT { get; set; }
        public string ALL_AMOUNT { get; set; }
        public int CORRECTED_ERROR_COUNT { get; set; }
        public string CORRECTED_ERROR_AMOUNT { get; set; }
        public int OTHER_ERROR_COUNT { get; set; }
        public string OTHER_ERROR_AMOUNT { get; set; }
        public int ACT_COUNT { get; set; }
        public string ACT_AMOUNT { get; set; }
        public int CLAIM_COUNT { get; set; }
        public string CLAIM_AMOUNT { get; set; }
        public int REFERENCE_COUNT { get; set; }
        public string REFERENCE_AMOUNT { get; set; }
        public int PROPOSAL_COUNT { get; set; }
        public string PROPOSAL_AMOUNT { get; set; }
        public int LAW_COUNT { get; set; }
        public string LAW_AMOUNT { get; set; }
        public int OTHER_COUNT { get; set; }
        public string OTHER_AMOUNT { get; set; }

        public int EXEC_TYPE { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public BM6 SetXml(XElement xml)
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
                if (xml.Element("PERIOD_LABEL") != null)
                    PERIOD_LABEL = xml.Element("PERIOD_LABEL").Value;

                if (xml.Element("YEAR_LABEL") != null)
                    YEAR_LABEL = Convert.ToInt32(xml.Element("YEAR_LABEL").Value);
                if (xml.Element("AUDIT_TYPE_NAME") != null)
                    AUDIT_TYPE_NAME = xml.Element("AUDIT_TYPE_NAME").Value;
                if (xml.Element("TOPIC_CODE") != null)
                    TOPIC_CODE = xml.Element("TOPIC_CODE").Value;
                if (xml.Element("TOPIC_NAME") != null)
                    TOPIC_NAME = xml.Element("TOPIC_NAME").Value;

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
                    VIOLATION_AMOUNT = Convert.ToDecimal(xml.Element("VIOLATION_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("ERROR_COUNT") != null)
                    ERROR_COUNT = Convert.ToInt32(xml.Element("ERROR_COUNT").Value);
                if (xml.Element("ERROR_AMOUNT") != null)
                    ERROR_AMOUNT = Convert.ToDecimal(xml.Element("ERROR_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("ALL_COUNT") != null)
                    ALL_COUNT = Convert.ToInt32(xml.Element("ALL_COUNT").Value);
                if (xml.Element("ALL_AMOUNT") != null)
                    ALL_AMOUNT = Convert.ToDecimal(xml.Element("ALL_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("CORRECTED_ERROR_COUNT") != null)
                    CORRECTED_ERROR_COUNT = Convert.ToInt32(xml.Element("CORRECTED_ERROR_COUNT").Value);
                if (xml.Element("CORRECTED_ERROR_AMOUNT") != null)
                    CORRECTED_ERROR_AMOUNT = Convert.ToDecimal(xml.Element("CORRECTED_ERROR_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("OTHER_ERROR_COUNT") != null)
                    OTHER_ERROR_COUNT = Convert.ToInt32(xml.Element("OTHER_ERROR_COUNT").Value);
                if (xml.Element("OTHER_ERROR_AMOUNT") != null)
                    OTHER_ERROR_AMOUNT = Convert.ToDecimal(xml.Element("OTHER_ERROR_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("ACT_COUNT") != null)
                    ACT_COUNT = Convert.ToInt32(xml.Element("ACT_COUNT").Value);
                if (xml.Element("ACT_AMOUNT") != null)
                    ACT_AMOUNT = Convert.ToDecimal(xml.Element("ACT_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("CLAIM_COUNT") != null)
                    CLAIM_COUNT = Convert.ToInt32(xml.Element("CLAIM_COUNT").Value);
                if (xml.Element("CLAIM_AMOUNT") != null)
                    CLAIM_AMOUNT = Convert.ToDecimal(xml.Element("CLAIM_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("REFERENCE_COUNT") != null)
                    REFERENCE_COUNT = Convert.ToInt32(xml.Element("REFERENCE_COUNT").Value);
                if (xml.Element("REFERENCE_AMOUNT") != null)
                    REFERENCE_AMOUNT = Convert.ToDecimal(xml.Element("REFERENCE_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("PROPOSAL_COUNT") != null)
                    PROPOSAL_COUNT = Convert.ToInt32(xml.Element("PROPOSAL_COUNT").Value);
                if (xml.Element("PROPOSAL_AMOUNT") != null)
                    PROPOSAL_AMOUNT = Convert.ToDecimal(xml.Element("PROPOSAL_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("LAW_COUNT") != null)
                    LAW_COUNT = Convert.ToInt32(xml.Element("LAW_COUNT").Value);
                if (xml.Element("LAW_AMOUNT") != null)
                    LAW_AMOUNT = Convert.ToDecimal(xml.Element("LAW_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("OTHER_COUNT") != null)
                    OTHER_COUNT = Convert.ToInt32(xml.Element("OTHER_COUNT").Value);
                if (xml.Element("OTHER_AMOUNT") != null)
                    OTHER_AMOUNT = Convert.ToDecimal(xml.Element("OTHER_AMOUNT").Value).ToString("#,0.##");

                if (xml.Element("EXEC_TYPE") != null)
                    EXEC_TYPE = Convert.ToInt32(xml.Element("EXEC_TYPE").Value);
            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("BM0",
                       new XElement("ID", ID),
                       new XElement("OFFICE_ID", OFFICE_ID),
                       new XElement("STATISTIC_PERIOD", STATISTIC_PERIOD),
                       //new XElement("AUDIT_TYPE", AUDIT_TYPE),
                       //new XElement("TOPIC_TYPE", TOPIC_TYPE),
                       //new XElement("TOPIC_CODE", TOPIC_CODE),
                       //new XElement("TOPIC_NAME", TOPIC_NAME),
                       //new XElement("ORDER_NO", ORDER_NO),
                       //new XElement("ORDER_DATE", ORDER_DATE),
                       //new XElement("AUDIT_PROPOSAL_TYPE", AUDIT_PROPOSAL_TYPE),
                       //new XElement("AUDIT_BUDGET_TYPE", AUDIT_BUDGET_TYPE),
                       //new XElement("AUDIT_INCLUDED_ORG", AUDIT_INCLUDED_ORG),
                       //new XElement("WORKING_PERSON", WORKING_PERSON),
                       //new XElement("WORKING_DAY", WORKING_DAY),
                       //new XElement("WORKING_ADDITION_TIME", WORKING_ADDITION_TIME),
                       //new XElement("AUDIT_DEPARTMENT", AUDIT_DEPARTMENT),
                       //new XElement("AUDITOR_LEAD", AUDITOR_LEAD),
                       //new XElement("AUDITOR_MEMBER", AUDITOR_MEMBER),
                       //new XElement("AUDITOR_ENTRY", AUDITOR_ENTRY),
                       //new XElement("EXEC_TYPE", EXEC_TYPE),
                       //new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", CREATED_DATE != null ? ((DateTime)CREATED_DATE).ToString("dd-MMM-yy") : null),
                       new XElement("UPDATED_DATE", UPDATED_DATE != null ? ((DateTime)UPDATED_DATE).ToString("dd-MMM-yy") : null)
                       );
        }
    }
}