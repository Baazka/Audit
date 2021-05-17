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
        [Required(ErrorMessage = "Төрийн аудитын байгууллага сонгоно уу.")]
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public int AUDIT_YEAR { get; set; }
        public int AUDIT_TYPE { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public int AUDIT_BUDGET_TYPE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORDER_DATE { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORDER_NO { get; set; }

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
        public string PROPOSAL_RCV_ADDRESS { get; set; }
        public string PROPOSAL_CONTROL_AUDITOR { get; set; }

        public string COMPLETION_ORDER { get; set; }
        public int COMPLETION_DONE { get; set; }
        public decimal COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS { get; set; }
        public decimal COMPLETION_PROGRESS_AMOUNT { get; set; }

        public int EXEC_TYPE { get; set; }

        public int IS_ACTIVE { get; set; } = 1;
        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
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
                    AUDIT_TYPE = Convert.ToInt32(xml.Element("AUDIT_TYPE").Value);
                if (xml.Element("AUDIT_TYPE_NAME") != null)
                    AUDIT_TYPE_NAME = xml.Element("AUDIT_TYPE_NAME").Value;
                if (xml.Element("AUDIT_CODE") != null)
                    AUDIT_CODE = xml.Element("AUDIT_CODE").Value;
                if (xml.Element("AUDIT_NAME") != null)
                    AUDIT_NAME = xml.Element("AUDIT_NAME").Value;
                if (xml.Element("AUDIT_BUDGET_TYPE") != null)
                    AUDIT_BUDGET_TYPE = Convert.ToInt32(xml.Element("AUDIT_BUDGET_TYPE").Value);
                if (xml.Element("BUDGET_TYPE_NAME") != null)
                    BUDGET_TYPE_NAME = xml.Element("BUDGET_TYPE_NAME").Value;
                if (xml.Element("ORDER_DATE") != null)
                    ORDER_DATE = xml.Element("ORDER_DATE").Value;
                if (xml.Element("ORDER_NO") != null)
                    ORDER_NO = xml.Element("ORDER_NO").Value;

                if (xml.Element("PROPOSAL_NO") != null)
                    PROPOSAL_NO = xml.Element("PROPOSAL_NO").Value;
                if (xml.Element("PROPOSAL_VIOLATION_DESC") != null)
                    PROPOSAL_VIOLATION_DESC = xml.Element("PROPOSAL_VIOLATION_DESC").Value;
                if (xml.Element("VIOLATION_RESPONDENT") != null)
                    VIOLATION_RESPONDENT = xml.Element("VIOLATION_RESPONDENT").Value;
                if (xml.Element("PROPOSAL_SUBMITTED_DATE") != null)
                    PROPOSAL_SUBMITTED_DATE = xml.Element("PROPOSAL_SUBMITTED_DATE").Value;
                if (xml.Element("PROPOSAL_DELIVERY_DATE") != null)
                    PROPOSAL_DELIVERY_DATE = xml.Element("PROPOSAL_DELIVERY_DATE").Value;
                if (xml.Element("PROPOSAL_COUNT") != null)
                    PROPOSAL_COUNT = Convert.ToInt32(xml.Element("PROPOSAL_COUNT").Value);
                if (xml.Element("PROPOSAL_AMOUNT") != null)
                    PROPOSAL_AMOUNT = Convert.ToDecimal(xml.Element("PROPOSAL_AMOUNT").Value);
                if (xml.Element("PROPOSAL_RCV_NAME") != null)
                    PROPOSAL_RCV_NAME = xml.Element("PROPOSAL_RCV_NAME").Value;
                if (xml.Element("PROPOSAL_RCV_ROLE") != null)
                    PROPOSAL_RCV_ROLE = xml.Element("PROPOSAL_RCV_ROLE").Value;
                if (xml.Element("PROPOSAL_RCV_GIVEN_NAME") != null)
                    PROPOSAL_RCV_GIVEN_NAME = xml.Element("PROPOSAL_RCV_GIVEN_NAME").Value;
                if (xml.Element("PROPOSAL_RCV_ADDRESS") != null)
                    PROPOSAL_RCV_ADDRESS = xml.Element("PROPOSAL_RCV_ADDRESS").Value;
                if (xml.Element("PROPOSAL_CONTROL_AUDITOR") != null)
                    PROPOSAL_CONTROL_AUDITOR = xml.Element("PROPOSAL_CONTROL_AUDITOR").Value;
                if (xml.Element("PROPOSAL_VIOLATION_TYPE") != null)
                    PROPOSAL_VIOLATION_TYPE = Convert.ToInt32(xml.Element("PROPOSAL_VIOLATION_TYPE").Value);
                if (xml.Element("VIOLATION_NAME") != null)
                    VIOLATION_NAME = xml.Element("VIOLATION_NAME").Value;

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
                
                if (xml.Element("EXEC_TYPE") != null)
                    EXEC_TYPE = Convert.ToInt32(xml.Element("EXEC_TYPE").Value);
            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("BM4",
                       new XElement("ID", ID),
                       new XElement("AUDIT_ID", AUDIT_ID),
                       new XElement("OFFICE_ID", OFFICE_ID),
                       new XElement("STATISTIC_PERIOD", STATISTIC_PERIOD),
                       new XElement("AUDIT_YEAR", AUDIT_YEAR),
                       new XElement("AUDIT_TYPE", AUDIT_TYPE),
                       new XElement("AUDIT_CODE", AUDIT_CODE),
                       new XElement("AUDIT_NAME", AUDIT_NAME),
                       new XElement("AUDIT_BUDGET_TYPE", AUDIT_BUDGET_TYPE),
                       new XElement("ORDER_DATE", ORDER_DATE),
                       new XElement("ORDER_NO", ORDER_NO),
                       new XElement("PROPOSAL_NO", PROPOSAL_NO),
                       new XElement("PROPOSAL_VIOLATION_DESC", PROPOSAL_VIOLATION_DESC),
                       new XElement("VIOLATION_RESPONDENT", VIOLATION_RESPONDENT),
                       new XElement("PROPOSAL_SUBMITTED_DATE", PROPOSAL_SUBMITTED_DATE),
                       new XElement("PROPOSAL_DELIVERY_DATE", PROPOSAL_DELIVERY_DATE),
                       new XElement("PROPOSAL_COUNT", PROPOSAL_COUNT),
                       new XElement("PROPOSAL_AMOUNT", PROPOSAL_AMOUNT),
                       new XElement("PROPOSAL_RCV_NAME", PROPOSAL_RCV_NAME),
                       new XElement("PROPOSAL_RCV_ROLE", PROPOSAL_RCV_ROLE),
                       new XElement("PROPOSAL_RCV_GIVEN_NAME", PROPOSAL_RCV_GIVEN_NAME),
                       new XElement("PROPOSAL_RCV_ADDRESS", PROPOSAL_RCV_ADDRESS),
                       new XElement("PROPOSAL_CONTROL_AUDITOR", PROPOSAL_CONTROL_AUDITOR),
                       new XElement("COMPLETION_ORDER", COMPLETION_ORDER),
                       new XElement("COMPLETION_DONE", COMPLETION_DONE),
                       new XElement("COMPLETION_DONE_AMOUNT", COMPLETION_DONE_AMOUNT),
                       new XElement("COMPLETION_PROGRESS", COMPLETION_PROGRESS),
                       new XElement("COMPLETION_PROGRESS_AMOUNT", COMPLETION_PROGRESS_AMOUNT),
                       new XElement("PROPOSAL_VIOLATION_TYPE", PROPOSAL_VIOLATION_TYPE),
                       new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", CREATED_DATE)
                       );
        }
    }
}