using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class BM2VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class BM2
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
        public string AUDIT_BUDGET_TYPE { get; set; }
        public string ORDER_DATE { get; set; }
        public string ORDER_NO { get; set; }

        public string CLAIM_NO { get; set; }
        public string CLAIM_VIOLATION_DESC { get; set; }
        public string CLAIM_VIOLATION_TYPE { get; set; }
        public string CLAIM_SUBMITTED_DATE { get; set; }
        public string CLAIM_DELIVERY_DATE { get; set; }
        public int CLAIM_VIOLATION_AMOUNT { get; set; }
        public string CLAIM_RCV_NAME { get; set; }
        public string CLAIM_RCV_ROLE { get; set; }
        public string CLAIM_RCV_GIVEN_NAME { get; set; }
        public string CLAIM_RCV_ADDRESS { get; set; }
        public string CLAIM_CONTROL_AUDITOR { get; set; }
        public string COMPLETION_ORDER { get; set; }       
        public int COMPLETION_AMOUNT { get; set; }
        public int COMPLETION_STATE_AMOUNT { get; set; }
        public int COMPLETION_LOCAL_AMOUNT { get; set; }
        public int COMPLETION_ORG_AMOUNT { get; set; }
        public int COMPLETION_OTHER_AMOUNT { get; set; }
        public int REMOVED_LAW_AMOUNT { get; set; }
        public string REMOVED_LAW_DATE { get; set; }
        public string REMOVED_LAW_NO { get; set; }
        public int REMOVED_INVALID_AMOUNT { get; set; }
        public string REMOVED_INVALID_DATE { get; set; }
        public string REMOVED_INVALID_NO { get; set; }
        public int CLAIM_C2_AMOUNT { get; set; }
        public int CLAIM_C2_NONEXPIRED { get; set; }
        public int CLAIM_C2_EXPIRED { get; set; }
        public int BENEFIT_FIN { get; set; }
        public int BENEFIT_FIN_AMOUNT { get; set; }
        public int BENEFIT_NONFIN { get; set; }
        public int EXEC_TYPE { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public BM2 SetXml(XElement xml)
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

                if (xml.Element("AUDIT_YEAR") != null)
                    AUDIT_YEAR = Convert.ToInt32(xml.Element("AUDIT_YEAR").Value);
                if (xml.Element("AUDIT_TYPE") != null)
                    AUDIT_TYPE = xml.Element("AUDIT_TYPE").Value;
                if (xml.Element("AUDIT_CODE") != null)
                    AUDIT_CODE = xml.Element("AUDIT_CODE").Value;
                if (xml.Element("AUDIT_NAME") != null)
                    AUDIT_NAME = xml.Element("AUDIT_NAME").Value;
                if (xml.Element("AUDIT_BUDGET_TYPE") != null)
                    AUDIT_BUDGET_TYPE = xml.Element("AUDIT_BUDGET_TYPE").Value;
                if (xml.Element("ORDER_DATE") != null)
                    ORDER_DATE = xml.Element("ORDER_DATE").Value;
                if (xml.Element("ORDER_NO") != null)
                    ORDER_NO = xml.Element("ORDER_NO").Value;
                
                if (xml.Element("CLAIM_NO") != null)
                    CLAIM_NO = xml.Element("CLAIM_NO").Value;
                if (xml.Element("CLAIM_VIOLATION_DESC") != null)
                    CLAIM_VIOLATION_DESC = xml.Element("CLAIM_VIOLATION_DESC").Value;
                if (xml.Element("CLAIM_VIOLATION_TYPE") != null)
                    CLAIM_VIOLATION_TYPE = xml.Element("CLAIM_VIOLATION_TYPE").Value;
                if (xml.Element("CLAIM_SUBMITTED_DATE") != null)
                    CLAIM_SUBMITTED_DATE = xml.Element("CLAIM_SUBMITTED_DATE").Value;
                if (xml.Element("CLAIM_DELIVERY_DATE") != null)
                    CLAIM_DELIVERY_DATE = xml.Element("CLAIM_DELIVERY_DATE").Value;
                if (xml.Element("CLAIM_VIOLATION_AMOUNT") != null)
                    CLAIM_VIOLATION_AMOUNT = Convert.ToInt32(xml.Element("CLAIM_VIOLATION_AMOUNT").Value);
                if (xml.Element("CLAIM_RCV_NAME") != null)
                    CLAIM_RCV_NAME = xml.Element("CLAIM_RCV_NAME").Value;
                if (xml.Element("CLAIM_RCV_ROLE") != null)
                    CLAIM_RCV_ROLE = xml.Element("CLAIM_RCV_ROLE").Value;
                if (xml.Element("CLAIM_RCV_GIVEN_NAME") != null)
                    CLAIM_RCV_GIVEN_NAME = xml.Element("CLAIM_RCV_GIVEN_NAME").Value;
                if (xml.Element("CLAIM_RCV_ADDRESS") != null)
                    CLAIM_RCV_ADDRESS = xml.Element("CLAIM_RCV_ADDRESS").Value;
                if (xml.Element("CLAIM_CONTROL_AUDITOR") != null)
                    CLAIM_CONTROL_AUDITOR = xml.Element("CLAIM_CONTROL_AUDITOR").Value;
                if (xml.Element("COMPLETION_ORDER") != null)
                    COMPLETION_ORDER = xml.Element("COMPLETION_ORDER").Value;
                if (xml.Element("COMPLETION_AMOUNT") != null)
                    COMPLETION_AMOUNT = Convert.ToInt32(xml.Element("COMPLETION_AMOUNT").Value);
                if (xml.Element("COMPLETION_STATE_AMOUNT") != null)
                    COMPLETION_STATE_AMOUNT = Convert.ToInt32(xml.Element("COMPLETION_STATE_AMOUNT").Value);
                if (xml.Element("COMPLETION_LOCAL_AMOUNT") != null)
                    COMPLETION_LOCAL_AMOUNT = Convert.ToInt32(xml.Element("COMPLETION_LOCAL_AMOUNT").Value);
                if (xml.Element("COMPLETION_ORG_AMOUNT") != null)
                    COMPLETION_ORG_AMOUNT = Convert.ToInt32(xml.Element("COMPLETION_ORG_AMOUNT").Value);
                if (xml.Element("COMPLETION_OTHER_AMOUNT") != null)
                    COMPLETION_OTHER_AMOUNT = Convert.ToInt32(xml.Element("COMPLETION_OTHER_AMOUNT").Value);
                
                if (xml.Element("REMOVED_LAW_AMOUNT") != null)
                    REMOVED_LAW_AMOUNT = Convert.ToInt32(xml.Element("REMOVED_LAW_AMOUNT").Value);
                if (xml.Element("REMOVED_LAW_DATE") != null)
                    REMOVED_LAW_DATE = xml.Element("REMOVED_LAW_DATE").Value;
                if (xml.Element("REMOVED_LAW_NO") != null)
                    REMOVED_LAW_NO = xml.Element("REMOVED_LAW_NO").Value;
                if (xml.Element("REMOVED_INVALID_AMOUNT") != null)
                    REMOVED_INVALID_AMOUNT = Convert.ToInt32(xml.Element("REMOVED_INVALID_AMOUNT").Value);
                if (xml.Element("REMOVED_INVALID_DATE") != null)
                    REMOVED_INVALID_DATE = xml.Element("REMOVED_INVALID_DATE").Value;
                if (xml.Element("REMOVED_INVALID_NO") != null)
                    REMOVED_INVALID_NO = xml.Element("REMOVED_INVALID_NO").Value;
                if (xml.Element("CLAIM_C2_AMOUNT") != null)
                    CLAIM_C2_AMOUNT = Convert.ToInt32(xml.Element("CLAIM_C2_AMOUNT").Value);
                if (xml.Element("CLAIM_C2_NONEXPIRED") != null)
                    CLAIM_C2_NONEXPIRED = Convert.ToInt32(xml.Element("CLAIM_C2_NONEXPIRED").Value);
                if (xml.Element("CLAIM_C2_EXPIRED") != null)
                    CLAIM_C2_EXPIRED = Convert.ToInt32(xml.Element("CLAIM_C2_EXPIRED").Value);

                if (xml.Element("BENEFIT_FIN") != null)
                    BENEFIT_FIN = Convert.ToInt32(xml.Element("BENEFIT_FIN").Value);
                if (xml.Element("BENEFIT_FIN_AMOUNT") != null)
                    BENEFIT_FIN_AMOUNT = Convert.ToInt32(xml.Element("BENEFIT_FIN_AMOUNT").Value);
                if (xml.Element("BENEFIT_NONFIN") != null)
                    BENEFIT_NONFIN = Convert.ToInt32(xml.Element("BENEFIT_NONFIN").Value);
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