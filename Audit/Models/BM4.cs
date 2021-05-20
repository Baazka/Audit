using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class BM4VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class BM4
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "BM0 сонгоно уу.")]
        public int AUDIT_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public string YEAR_LABEL { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }

        public string PROPOSAL_DATE { get; set; }
        public string PROPOSAL_NO { get; set; }
        public string PROPOSAL_VIOLATION_DESC { get; set; }
        public int PROPOSAL_VIOLATION_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }
        public string VIOLATION_RESPONDENT { get; set; }
        public string PROPOSAL_SUBMITTED_DATE { get; set; }
        public string PROPOSAL_DELIVERY_DATE { get; set; }
        public int PROPOSAL_COUNT { get; set; }
        public decimal PROPOSAL_AMOUNT { get; set; }
        public string PROPOSAL_RCV_NAME { get; set; }
        public string PROPOSAL_RCV_ROLE { get; set; }
        public string PROPOSAL_RCV_GIVEN_NAME { get; set; }
        public string PROPOSAL_RCV_PHONE { get; set; }
        public string PROPOSAL_RCV_ADDRESS { get; set; }
        public int PROPOSAL_CONTROL_AUDITOR_ID { get; set; }
        public string PROPOSAL_CONTROL_AUDITOR { get; set; }

        public string COMPLETION_DATE { get; set; }
        public string COMPLETION_ORDER { get; set; }
        public int COMPLETION_DONE { get; set; }
        public decimal COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS { get; set; }
        public decimal COMPLETION_PROGRESS_AMOUNT { get; set; }

        public int IS_ACTIVE { get; set; } = 1;
        public string CREATED_DATE { get; set; } = DateTime.Now.ToString("dd-MMM-yy");
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
        public List<REF_AUDIT_YEAR> refaudityears { get; set; } = new List<REF_AUDIT_YEAR>();
        public List<REF_VIOLATION_TYPE> refviolationtypes { get; set; } = new List<REF_VIOLATION_TYPE>();
        public List<REF_AUDIT_TYPE> audittypes { get; set; } = new List<REF_AUDIT_TYPE>();
        public List<REF_BUDGET_TYPE> refbudgettypes { get; set; } = new List<REF_BUDGET_TYPE>();

        public BM4 SetXml(XElement xml)
        {
            if (xml != null)
            {
                if (xml.Element("ID") != null)
                    ID = Convert.ToInt32(xml.Element("ID").Value);
                if (xml.Element("AUDIT_ID") != null)
                    AUDIT_ID = Convert.ToInt32(xml.Element("AUDIT_ID").Value);
                if (xml.Element("DEPARTMENT_NAME") != null)
                    DEPARTMENT_NAME = xml.Element("DEPARTMENT_NAME").Value;
                if (xml.Element("STATISTIC_PERIOD") != null)
                    STATISTIC_PERIOD = Convert.ToInt32(xml.Element("STATISTIC_PERIOD").Value);
                if (xml.Element("PERIOD_LABEL") != null)
                    PERIOD_LABEL = xml.Element("PERIOD_LABEL").Value;
                if (xml.Element("YEAR_LABEL") != null)
                    YEAR_LABEL = xml.Element("YEAR_LABEL").Value;
                if (xml.Element("AUDIT_TYPE_NAME") != null)
                    AUDIT_TYPE_NAME = xml.Element("AUDIT_TYPE_NAME").Value;
                if (xml.Element("TOPIC_TYPE_NAME") != null)
                    TOPIC_TYPE_NAME = xml.Element("TOPIC_TYPE_NAME").Value;
                if (xml.Element("TOPIC_CODE") != null)
                    TOPIC_CODE = xml.Element("TOPIC_CODE").Value;
                if (xml.Element("TOPIC_NAME") != null)
                    TOPIC_NAME = xml.Element("TOPIC_NAME").Value;
                if (xml.Element("ORDER_NO") != null)
                    ORDER_NO = xml.Element("ORDER_NO").Value;
                if (xml.Element("ORDER_DATE") != null)
                    ORDER_DATE = Convert.ToDateTime(xml.Element("ORDER_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("BUDGET_TYPE_NAME") != null)
                    BUDGET_TYPE_NAME = xml.Element("BUDGET_TYPE_NAME").Value;

                if (xml.Element("PROPOSAL_DATE") != null)
                    PROPOSAL_DATE = xml.Element("PROPOSAL_DATE").Value;
                if (xml.Element("PROPOSAL_NO") != null)
                    PROPOSAL_NO = xml.Element("PROPOSAL_NO").Value;
                if (xml.Element("PRO_VIOLATION_DESC") != null)
                    PROPOSAL_VIOLATION_DESC = xml.Element("PRO_VIOLATION_DESC").Value;
                if (xml.Element("PRO_VIOLATION_TYPE") != null)
                    PROPOSAL_VIOLATION_TYPE = Convert.ToInt32(xml.Element("PRO_VIOLATION_TYPE").Value);
                if (xml.Element("VIOLATION_NAME") != null)
                    VIOLATION_NAME = xml.Element("VIOLATION_NAME").Value;
                if (xml.Element("VIOLATION_RESPONDENT") != null)
                    VIOLATION_RESPONDENT = xml.Element("VIOLATION_RESPONDENT").Value;
                if (xml.Element("PRO_SUBMITTED_DATE") != null)
                    PROPOSAL_SUBMITTED_DATE = Convert.ToDateTime(xml.Element("PRO_SUBMITTED_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("PROPOSAL_DELIVERY_DATE") != null)
                    PROPOSAL_DELIVERY_DATE = Convert.ToDateTime(xml.Element("PROPOSAL_DELIVERY_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("PROPOSAL_VIOLATION_COUNT") != null)
                    PROPOSAL_COUNT = Convert.ToInt32(xml.Element("PROPOSAL_VIOLATION_COUNT").Value);
                if (xml.Element("PROPOSAL_AMOUNT") != null)
                    PROPOSAL_AMOUNT = Convert.ToDecimal(xml.Element("PROPOSAL_AMOUNT").Value);
                if (xml.Element("PROPOSAL_RCV_NAME") != null)
                    PROPOSAL_RCV_NAME = xml.Element("PROPOSAL_RCV_NAME").Value;
                if (xml.Element("PROPOSAL_RCV_ROLE") != null)
                    PROPOSAL_RCV_ROLE = xml.Element("PROPOSAL_RCV_ROLE").Value;
                if (xml.Element("PROPOSAL_RCV_GIVEN_NAME") != null)
                    PROPOSAL_RCV_GIVEN_NAME = xml.Element("PROPOSAL_RCV_GIVEN_NAME").Value;
                if (xml.Element("PROPOSAL_RCV_PHONE") != null)
                    PROPOSAL_RCV_PHONE = xml.Element("PROPOSAL_RCV_PHONE").Value;
                if (xml.Element("PRO_RCV_ADDRESS") != null)
                    PROPOSAL_RCV_ADDRESS = xml.Element("PRO_RCV_ADDRESS").Value;
                if (xml.Element("PROPOSAL_CONTROL_AUDITOR") != null)
                    PROPOSAL_CONTROL_AUDITOR = xml.Element("PROPOSAL_CONTROL_AUDITOR").Value;
                if (xml.Element("COMPLETION_DATE") != null)
                    COMPLETION_DATE = Convert.ToDateTime(xml.Element("COMPLETION_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("COMPLETION_ORDER") != null)
                    COMPLETION_ORDER = xml.Element("COMPLETION_ORDER").Value;
                if (xml.Element("COMPLETION_DONE") != null)
                    COMPLETION_DONE = Convert.ToInt32(xml.Element("COMPLETION_DONE").Value);
                if (xml.Element("COMPLETION_DONE_AMOUNT") != null)
                    COMPLETION_DONE_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_DONE_AMOUNT").Value);
                if (xml.Element("COMPLETION_PROGRESS") != null)
                    COMPLETION_PROGRESS = Convert.ToInt32(xml.Element("COMPLETION_PROGRESS").Value);
                if (xml.Element("COMPLETION_PROGRESS_AMOUNT") != null)
                    COMPLETION_PROGRESS_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_PROGRESS_AMOUNT").Value);
                
            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("BM4",
                       new XElement("ID", ID),
                       new XElement("AUDIT_ID", AUDIT_ID),
                       new XElement("PROPOSAL_DATE", Convert.ToDateTime(PROPOSAL_DATE).ToString("dd-MMM-yy")),
                       new XElement("PROPOSAL_NO", PROPOSAL_NO),
                       new XElement("PROPOSAL_VIOLATION_DESC", PROPOSAL_VIOLATION_DESC),
                       new XElement("PROPOSAL_VIOLATION_TYPE", PROPOSAL_VIOLATION_TYPE),
                       new XElement("VIOLATION_RESPONDENT", VIOLATION_RESPONDENT),
                       new XElement("PROPOSAL_SUBMITTED_DATE", Convert.ToDateTime(PROPOSAL_SUBMITTED_DATE).ToString("dd-MMM-yy")),
                       new XElement("PROPOSAL_DELIVERY_DATE", Convert.ToDateTime(PROPOSAL_DELIVERY_DATE).ToString("dd-MMM-yy")),
                       new XElement("PROPOSAL_COUNT", PROPOSAL_COUNT),
                       new XElement("PROPOSAL_AMOUNT", PROPOSAL_AMOUNT),
                       new XElement("PROPOSAL_RCV_NAME", PROPOSAL_RCV_NAME),
                       new XElement("PROPOSAL_RCV_ROLE", PROPOSAL_RCV_ROLE),
                       new XElement("PROPOSAL_RCV_GIVEN_NAME", PROPOSAL_RCV_GIVEN_NAME),
                       new XElement("PROPOSAL_RCV_PHONE", PROPOSAL_RCV_PHONE),
                       new XElement("PROPOSAL_RCV_ADDRESS", PROPOSAL_RCV_ADDRESS),
                       //new XElement("PROPOSAL_CONTROL_AUDITOR", PROPOSAL_CONTROL_AUDITOR),
                       new XElement("COMPLETION_DATE", Convert.ToDateTime(COMPLETION_DATE).ToString("dd-MMM-yy")),
                       new XElement("COMPLETION_ORDER", COMPLETION_ORDER),
                       new XElement("COMPLETION_DONE", COMPLETION_DONE),
                       new XElement("COMPLETION_DONE_AMOUNT", COMPLETION_DONE_AMOUNT),
                       new XElement("COMPLETION_PROGRESS", COMPLETION_PROGRESS),
                       new XElement("COMPLETION_PROGRESS_AMOUNT", COMPLETION_PROGRESS_AMOUNT),
                       new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", Convert.ToDateTime(CREATED_DATE).ToString("dd-MMM-yy"))
                       );
        }
    }
}