using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class CM4VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class CM4
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }

        public string AUDIT_TYPE { get; set; }
        public string DECISION_TYPE { get; set; }
        public string BUDGET_TYPE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }
        public int IS_STATE { get; set; }
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
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public CM4 SetXml(XElement xml)
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
                if (xml.Element("AUDIT_TYPE") != null)
                    AUDIT_TYPE = xml.Element("AUDIT_TYPE").Value;
                if (xml.Element("DECISION_TYPE") != null)
                    DECISION_TYPE = xml.Element("DECISION_TYPE").Value;
                if (xml.Element("BUDGET_TYPE") != null)
                    BUDGET_TYPE = xml.Element("BUDGET_TYPE").Value;
                if (xml.Element("BUDGET_TYPE_NAME") != null)
                    BUDGET_TYPE_NAME = xml.Element("BUDGET_TYPE_NAME").Value;
                if (xml.Element("IS_STATE") != null)
                    IS_STATE = Convert.ToInt32(xml.Element("IS_STATE").Value);
                
                if (xml.Element("VIOLATION_COUNT") != null)
                    VIOLATION_COUNT = Convert.ToInt32(xml.Element("VIOLATION_COUNT").Value);
                if (xml.Element("VIOLATION_AMOUNT") != null)
                    VIOLATION_AMOUNT = Convert.ToDecimal(xml.Element("VIOLATION_AMOUNT").Value).ToString("#,0.00");
                if (xml.Element("ERROR_COUNT") != null)
                    ERROR_COUNT = Convert.ToInt32(xml.Element("ERROR_COUNT").Value);
                if (xml.Element("ERROR_AMOUNT") != null)
                    ERROR_AMOUNT = Convert.ToDecimal(xml.Element("ERROR_AMOUNT").Value).ToString("#,0.00");         
                if (xml.Element("ALL_COUNT") != null)
                    ALL_COUNT = Convert.ToInt32(xml.Element("ALL_COUNT").Value);
                if (xml.Element("ALL_AMOUNT") != null)
                    ALL_AMOUNT = Convert.ToDecimal(xml.Element("ALL_AMOUNT").Value).ToString("#,0.00");
                if (xml.Element("CORRECTED_ERROR_COUNT") != null)
                    CORRECTED_ERROR_COUNT = Convert.ToInt32(xml.Element("CORRECTED_ERROR_COUNT").Value);
                if (xml.Element("CORRECTED_ERROR_AMOUNT") != null)
                    CORRECTED_ERROR_AMOUNT = Convert.ToDecimal(xml.Element("CORRECTED_ERROR_AMOUNT").Value).ToString("#,0.00");
                if (xml.Element("OTHER_ERROR_COUNT") != null)
                    OTHER_ERROR_COUNT = Convert.ToInt32(xml.Element("OTHER_ERROR_COUNT").Value);
                if (xml.Element("OTHER_ERROR_AMOUNT") != null)
                    OTHER_ERROR_AMOUNT = Convert.ToDecimal(xml.Element("OTHER_ERROR_AMOUNT").Value).ToString("#,0.00");
                if (xml.Element("ACT_COUNT") != null)
                    ACT_COUNT = Convert.ToInt32(xml.Element("ACT_COUNT").Value);
                if (xml.Element("ACT_AMOUNT") != null)
                    ACT_AMOUNT = Convert.ToDecimal(xml.Element("ACT_AMOUNT").Value).ToString("#,0.00");
                if (xml.Element("CLAIM_COUNT") != null)
                    CLAIM_COUNT = Convert.ToInt32(xml.Element("CLAIM_COUNT").Value);
                if (xml.Element("CLAIM_AMOUNT") != null)
                    CLAIM_AMOUNT = Convert.ToDecimal(xml.Element("CLAIM_AMOUNT").Value).ToString("#,0.00");
                if (xml.Element("REFERENCE_COUNT") != null)
                    REFERENCE_COUNT = Convert.ToInt32(xml.Element("REFERENCE_COUNT").Value);
                if (xml.Element("REFERENCE_AMOUNT") != null)
                    REFERENCE_AMOUNT = Convert.ToDecimal(xml.Element("REFERENCE_AMOUNT").Value).ToString("#,0.00");                
                if (xml.Element("PROPOSAL_COUNT") != null)
                    PROPOSAL_COUNT = Convert.ToInt32(xml.Element("PROPOSAL_COUNT").Value);
                if (xml.Element("PROPOSAL_AMOUNT") != null)
                    PROPOSAL_AMOUNT = Convert.ToDecimal(xml.Element("PROPOSAL_AMOUNT").Value).ToString("#,0.00");
                if (xml.Element("LAW_COUNT") != null)
                    LAW_COUNT = Convert.ToInt32(xml.Element("LAW_COUNT").Value);
                if (xml.Element("LAW_AMOUNT") != null)
                    LAW_AMOUNT = Convert.ToDecimal(xml.Element("LAW_AMOUNT").Value).ToString("#,0.00");
                if (xml.Element("OTHER_COUNT") != null)
                    OTHER_COUNT = Convert.ToInt32(xml.Element("OTHER_COUNT").Value);
                if (xml.Element("OTHER_AMOUNT") != null)
                    OTHER_AMOUNT = Convert.ToDecimal(xml.Element("OTHER_AMOUNT").Value).ToString("#,0.00");
                
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
                       new XElement("CREATED_DATE", CREATED_DATE != null ? ((DateTime)CREATED_DATE).ToString("dd-MMM-yy") : null)
                       );
        }
    }

}