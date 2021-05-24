using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class NM2VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class NM2
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }
        
        public int CLAIM_VIOLATION_COUNT { get; set; }
        public decimal CLAIM_VIOLATION_AMOUNT { get; set; }
        public int COMPLETION_COUNT { get; set; }
        public decimal COMPLETION_AMOUNT { get; set; }
        public int COMPLETION_STATE_COUNT { get; set; }
        public decimal COMPLETION_STATE_AMOUNT { get; set; }
        public int COMPLETION_LOCAL_COUNT { get; set; }
        public decimal COMPLETION_LOCAL_AMOUNT { get; set; }
        public int COMPLETION_ORG_COUNT { get; set; }
        public decimal COMPLETION_ORG_AMOUNT { get; set; }
        public int COMPLETION_OTHER_COUNT { get; set; }
        public decimal COMPLETION_OTHER_AMOUNT { get; set; }
        
        public int REMOVED_COUNT { get; set; }
        public decimal REMOVED_AMOUNT { get; set; }
        public int REMOVED_LAW_COUNT { get; set; }
        public decimal REMOVED_LAW_AMOUNT { get; set; }
        public int REMOVED_INVALID_COUNT { get; set; }
        public decimal REMOVED_INVALID_AMOUNT { get; set; }
        
        public int CLAIM_C2_COUNT { get; set; }
        public decimal CLAIM_C2_AMOUNT { get; set; }
        public int CLAIM_C2_NONEXPIRED_COUNT { get; set; }
        public decimal CLAIM_C2_NONEXPIRED_AMOUNT { get; set; }
        public int CLAIM_C2_EXPIRED_COUNT { get; set; }
        public decimal CLAIM_C2_EXPIRED_AMOUNT { get; set; }
        
        public int BENEFIT_FIN_COUNT { get; set; }
        public decimal BENEFIT_FIN_AMOUNT { get; set; }
        public int BENEFIT_NONFIN_COUNT { get; set; }
        public decimal BENEFIT_NONFIN_AMOUNT { get; set; }
        public int IS_ACTIVE { get; set; } = 1;
        public int EXEC_TYPE { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public NM2 SetXml(XElement xml)
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

                if (xml.Element("TOPIC_CODE") != null)
                    TOPIC_CODE = xml.Element("TOPIC_CODE").Value;
                if (xml.Element("TOPIC_NAME") != null)
                    TOPIC_NAME = xml.Element("TOPIC_NAME").Value;

                if (xml.Element("AUDIT_CODE") != null)
                    AUDIT_CODE = xml.Element("AUDIT_CODE").Value;
                if (xml.Element("AUDIT_NAME") != null)
                    AUDIT_NAME = xml.Element("AUDIT_NAME").Value;
                if (xml.Element("AUDIT_BUDGET_TYPE") != null)
                    AUDIT_BUDGET_TYPE = xml.Element("AUDIT_BUDGET_TYPE").Value;
                if (xml.Element("CLAIM_VIOLATION_COUNT") != null)
                    CLAIM_VIOLATION_COUNT = Convert.ToInt32(xml.Element("CLAIM_VIOLATION_COUNT").Value);
                if (xml.Element("CLAIM_VIOLATION_AMOUNT") != null)
                    CLAIM_VIOLATION_AMOUNT = Convert.ToDecimal(xml.Element("CLAIM_VIOLATION_AMOUNT").Value);
                if (xml.Element("COMPLETION_COUNT") != null)
                    COMPLETION_COUNT = Convert.ToInt32(xml.Element("COMPLETION_COUNT").Value);
                if (xml.Element("COMPLETION_AMOUNT") != null)
                    COMPLETION_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_AMOUNT").Value);
                if (xml.Element("COMPLETION_STATE_COUNT") != null)
                    COMPLETION_STATE_COUNT = Convert.ToInt32(xml.Element("COMPLETION_STATE_COUNT").Value);
                if (xml.Element("COMPLETION_STATE_AMOUNT") != null)
                    COMPLETION_STATE_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_STATE_AMOUNT").Value);
                if (xml.Element("COMPLETION_LOCAL_COUNT") != null)
                    COMPLETION_LOCAL_COUNT = Convert.ToInt32(xml.Element("COMPLETION_LOCAL_COUNT").Value);
                if (xml.Element("COMPLETION_LOCAL_AMOUNT") != null)
                    COMPLETION_LOCAL_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_LOCAL_AMOUNT").Value);
                if (xml.Element("COMPLETION_ORG_COUNT") != null)
                    COMPLETION_ORG_COUNT = Convert.ToInt32(xml.Element("COMPLETION_ORG_COUNT").Value);
                if (xml.Element("COMPLETION_ORG_AMOUNT") != null)
                    COMPLETION_ORG_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_ORG_AMOUNT").Value);
                if (xml.Element("COMPLETION_OTHER_COUNT") != null)
                    COMPLETION_OTHER_COUNT = Convert.ToInt32(xml.Element("COMPLETION_OTHER_COUNT").Value);
                if (xml.Element("COMPLETION_OTHER_AMOUNT") != null)
                    COMPLETION_OTHER_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_OTHER_AMOUNT").Value);
                if (xml.Element("REMOVED_COUNT") != null)
                    REMOVED_COUNT = Convert.ToInt32(xml.Element("REMOVED_COUNT").Value);
                if (xml.Element("REMOVED_AMOUNT") != null)
                    REMOVED_AMOUNT = Convert.ToDecimal(xml.Element("REMOVED_AMOUNT").Value);
                if (xml.Element("REMOVED_LAW_COUNT") != null)
                    REMOVED_LAW_COUNT = Convert.ToInt32(xml.Element("REMOVED_LAW_COUNT").Value);
                if (xml.Element("REMOVED_LAW_AMOUNT") != null)
                    REMOVED_LAW_AMOUNT = Convert.ToDecimal(xml.Element("REMOVED_LAW_AMOUNT").Value);
                if (xml.Element("REMOVED_INVALID_COUNT") != null)
                    REMOVED_INVALID_COUNT = Convert.ToInt32(xml.Element("REMOVED_INVALID_COUNT").Value);
                if (xml.Element("REMOVED_INVALID_AMOUNT") != null)
                    REMOVED_INVALID_AMOUNT = Convert.ToDecimal(xml.Element("REMOVED_INVALID_AMOUNT").Value);
                if (xml.Element("CLAIM_C2_COUNT") != null)
                    CLAIM_C2_COUNT = Convert.ToInt32(xml.Element("CLAIM_C2_COUNT").Value);
                if (xml.Element("CLAIM_C2_AMOUNT") != null)
                    CLAIM_C2_AMOUNT = Convert.ToDecimal(xml.Element("CLAIM_C2_AMOUNT").Value);
                if (xml.Element("CLAIM_C2_NONEXPIRED_COUNT") != null)
                    CLAIM_C2_NONEXPIRED_COUNT = Convert.ToInt32(xml.Element("CLAIM_C2_NONEXPIRED_COUNT").Value);
                if (xml.Element("CLAIM_C2_NONEXPIRED_AMOUNT") != null)
                    CLAIM_C2_NONEXPIRED_AMOUNT = Convert.ToDecimal(xml.Element("CLAIM_C2_NONEXPIRED_AMOUNT").Value);
                if (xml.Element("CLAIM_C2_EXPIRED_COUNT") != null)
                    CLAIM_C2_EXPIRED_COUNT = Convert.ToInt32(xml.Element("CLAIM_C2_EXPIRED_COUNT").Value);
                if (xml.Element("CLAIM_C2_EXPIRED_AMOUNT") != null)
                    CLAIM_C2_EXPIRED_AMOUNT = Convert.ToDecimal(xml.Element("CLAIM_C2_EXPIRED_AMOUNT").Value);
                if (xml.Element("BENEFIT_FIN_COUNT") != null)
                    BENEFIT_FIN_COUNT = Convert.ToInt32(xml.Element("BENEFIT_FIN_COUNT").Value);
                if (xml.Element("BENEFIT_FIN_AMOUNT") != null)
                    BENEFIT_FIN_AMOUNT = Convert.ToDecimal(xml.Element("BENEFIT_FIN_AMOUNT").Value);
                if (xml.Element("BENEFIT_NONFIN_COUNT") != null)
                    BENEFIT_NONFIN_COUNT = Convert.ToInt32(xml.Element("BENEFIT_NONFIN_COUNT").Value);
                if (xml.Element("BENEFIT_NONFIN_AMOUNT") != null)
                    BENEFIT_NONFIN_AMOUNT = Convert.ToDecimal(xml.Element("BENEFIT_NONFIN_AMOUNT").Value);
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