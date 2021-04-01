using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class BM3VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class BM3
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

        public string REFERENCE_DESC { get; set; }
        public int REFERENCE_AMOUNT { get; set; }
        public string REFERENCE_SUBMITTED_DATE { get; set; }
        public string REFERENCE_DELIVERY_DATE { get; set; }
        public string REFERENCE_RCV_NAME { get; set; }
        public string REFERENCE_RCV_ROLE { get; set; }
        public string REFERENCE_RCV_GIVEN_NAME { get; set; }
        public string REFERENCE_RCV_ADDRESS { get; set; }
        public string REFERENCE_CONTROL_AUDITOR { get; set; }

        public string COMPLETION_ORDER { get; set; }
        public int COMPLETION_DONE { get; set; }
        public int COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS { get; set; }
        public int COMPLETION_PROGRESS_AMOUNT { get; set; }
        public int C2_NONEXPIRED { get; set; }
        public int C2_NONEXPIRED_AMOUNT { get; set; }
        public int C2_EXPIRED { get; set; }
        public int C2_EXPIRED_AMOUNT { get; set; }
        
        public int BENEFIT_FIN { get; set; }
        public int BENEFIT_FIN_AMOUNT { get; set; }
        public int BENEFIT_NONFIN { get; set; }
        public int WORKING_PERSON { get; set; }
        public int WORKING_DAY { get; set; }
        public int WORKING_ADDITION_TIME { get; set; }
        public int EXEC_TYPE { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public BM3 SetXml(XElement xml)
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

                if (xml.Element("REFERENCE_DESC") != null)
                    REFERENCE_DESC = xml.Element("REFERENCE_DESC").Value;
                if (xml.Element("REFERENCE_AMOUNT") != null)
                    REFERENCE_AMOUNT = Convert.ToInt32(xml.Element("REFERENCE_AMOUNT").Value);
                if (xml.Element("REFERENCE_SUBMITTED_DATE") != null)
                    REFERENCE_SUBMITTED_DATE = xml.Element("REFERENCE_SUBMITTED_DATE").Value;
                if (xml.Element("REFERENCE_DELIVERY_DATE") != null)
                    REFERENCE_DELIVERY_DATE = xml.Element("REFERENCE_DELIVERY_DATE").Value;
                if (xml.Element("REFERENCE_RCV_NAME") != null)
                    REFERENCE_RCV_NAME = xml.Element("REFERENCE_RCV_NAME").Value;
                if (xml.Element("REFERENCE_RCV_ROLE") != null)
                    REFERENCE_RCV_ROLE = xml.Element("REFERENCE_RCV_ROLE").Value;
                if (xml.Element("REFERENCE_RCV_GIVEN_NAME") != null)
                    REFERENCE_RCV_GIVEN_NAME = xml.Element("REFERENCE_RCV_GIVEN_NAME").Value;
                if (xml.Element("REFERENCE_RCV_ADDRESS") != null)
                    REFERENCE_RCV_ADDRESS = xml.Element("REFERENCE_RCV_ADDRESS").Value;
                if (xml.Element("REFERENCE_CONTROL_AUDITOR") != null)
                    REFERENCE_CONTROL_AUDITOR = xml.Element("REFERENCE_CONTROL_AUDITOR").Value;
                if (xml.Element("COMPLETION_ORDER") != null)
                    COMPLETION_ORDER = xml.Element("COMPLETION_ORDER").Value;                
                if (xml.Element("COMPLETION_DONE") != null)
                    COMPLETION_DONE = Convert.ToInt32(xml.Element("COMPLETION_DONE").Value);
                if (xml.Element("COMPLETION_DONE_AMOUNT") != null)
                    COMPLETION_DONE_AMOUNT = Convert.ToInt32(xml.Element("COMPLETION_DONE_AMOUNT").Value);
                if (xml.Element("COMPLETION_PROGRESS") != null)
                    COMPLETION_PROGRESS = Convert.ToInt32(xml.Element("COMPLETION_PROGRESS").Value);
                if (xml.Element("COMPLETION_PROGRESS_AMOUNT") != null)
                    COMPLETION_PROGRESS_AMOUNT = Convert.ToInt32(xml.Element("COMPLETION_PROGRESS_AMOUNT").Value);
                if (xml.Element("C2_NONEXPIRED") != null)
                    C2_NONEXPIRED = Convert.ToInt32(xml.Element("C2_NONEXPIRED").Value);
                if (xml.Element("C2_NONEXPIRED_AMOUNT") != null)
                    C2_NONEXPIRED_AMOUNT = Convert.ToInt32(xml.Element("C2_NONEXPIRED_AMOUNT").Value);
                if (xml.Element("C2_EXPIRED") != null)
                    C2_EXPIRED = Convert.ToInt32(xml.Element("C2_EXPIRED").Value);
                if (xml.Element("C2_EXPIRED_AMOUNT") != null)
                    C2_EXPIRED_AMOUNT = Convert.ToInt32(xml.Element("C2_EXPIRED_AMOUNT").Value);
                
                if (xml.Element("BENEFIT_FIN") != null)
                    BENEFIT_FIN = Convert.ToInt32(xml.Element("BENEFIT_FIN").Value);
                if (xml.Element("BENEFIT_FIN_AMOUNT") != null)
                    BENEFIT_FIN_AMOUNT = Convert.ToInt32(xml.Element("BENEFIT_FIN_AMOUNT").Value);
                if (xml.Element("BENEFIT_NONFIN") != null)
                    BENEFIT_NONFIN = Convert.ToInt32(xml.Element("BENEFIT_NONFIN").Value);
                if (xml.Element("WORKING_PERSON") != null)
                    WORKING_PERSON = Convert.ToInt32(xml.Element("WORKING_PERSON").Value);
                if (xml.Element("WORKING_DAY") != null)
                    WORKING_DAY = Convert.ToInt32(xml.Element("WORKING_DAY").Value);
                if (xml.Element("WORKING_ADDITION_TIME") != null)
                    WORKING_ADDITION_TIME = Convert.ToInt32(xml.Element("WORKING_ADDITION_TIME").Value);
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