using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "BM0 сонгоно уу.")]
        public int AUDIT_ID { get; set; }
        public string AUDIT_YEAR { get; set; }
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

        public string CLAIM_DATE { get; set; }
        public string CLAIM_NO { get; set; }
        public string CLAIM_VIOLATION_DESC { get; set; }
        public int CLAIM_VIOLATION_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }
        public string CLAIM_SUBMITTED_DATE { get; set; }
        public string CLAIM_DELIVERY_DATE { get; set; }
        public decimal CLAIM_VIOLATION_AMOUNT { get; set; }
        public string CLAIM_RCV_NAME { get; set; }
        public string CLAIM_RCV_ROLE { get; set; }
        public string CLAIM_RCV_GIVEN_NAME { get; set; }
        public string CLAIM_RCV_PHONE { get; set; }
        public string CLAIM_RCV_ADDRESS { get; set; }
        public int CLAIM_CONTROL_AUDITOR_ID { get; set; }
        public string CLAIM_CONTROL_AUDITOR { get; set; }
        public string COMPLETION_DATE { get; set; }
        public string COMPLETION_ORDER { get; set; }
        public decimal COMPLETION_AMOUNT { get; set; }
        public decimal COMPLETION_STATE_AMOUNT { get; set; }
        public decimal COMPLETION_LOCAL_AMOUNT { get; set; }
        public decimal COMPLETION_ORG_AMOUNT { get; set; }
        public decimal COMPLETION_OTHER_AMOUNT { get; set; }
        public decimal REMOVED_LAW_AMOUNT { get; set; }
        public string REMOVED_LAW_DATE { get; set; }
        public string REMOVED_LAW_NO { get; set; }
        public decimal REMOVED_INVALID_AMOUNT { get; set; }
        public string REMOVED_INVALID_DATE { get; set; }
        public string REMOVED_INVALID_NO { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public decimal CLAIM_C2_AMOUNT { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public decimal CLAIM_C2_NONEXPIRED { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public decimal CLAIM_C2_EXPIRED { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public int BENEFIT_FIN { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public decimal BENEFIT_FIN_AMOUNT { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public int BENEFIT_NONFIN { get; set; }
        public int EXEC_TYPE { get; set; }

        public int IS_ACTIVE { get; set; } = 1;
        public string CREATED_DATE { get; set; } = DateTime.Now.ToString("dd-MMM-yy");
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
        public List<REF_AUDIT_YEAR> refaudityears { get; set; } = new List<REF_AUDIT_YEAR>();
        public List<REF_VIOLATION_TYPE> refviolationtypes { get; set; } = new List<REF_VIOLATION_TYPE>();
        public List<REF_AUDIT_TYPE> audittypes { get; set; } = new List<REF_AUDIT_TYPE>();
        public List<REF_BUDGET_TYPE> refbudgettypes { get; set; } = new List<REF_BUDGET_TYPE>();

        public BM2 SetXml(XElement xml)
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
                if (xml.Element("AUDIT_YEAR") != null)
                    AUDIT_YEAR = xml.Element("AUDIT_YEAR").Value;
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

                if (xml.Element("CLAIM_DATE") != null)
                    CLAIM_DATE = Convert.ToDateTime(xml.Element("CLAIM_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("CLAIM_NO") != null)
                    CLAIM_NO = xml.Element("CLAIM_NO").Value;
                if (xml.Element("CLAIM_VIOLATION_DESC") != null)
                    CLAIM_VIOLATION_DESC = xml.Element("CLAIM_VIOLATION_DESC").Value;
                if (xml.Element("CLAIM_VIOLATION_TYPE") != null)
                    CLAIM_VIOLATION_TYPE = Convert.ToInt32(xml.Element("CLAIM_VIOLATION_TYPE").Value);
                if (xml.Element("VIOLATION_NAME") != null)
                    VIOLATION_NAME = xml.Element("VIOLATION_NAME").Value;
                if (xml.Element("CLAIM_SUBMITTED_DATE") != null)
                    CLAIM_SUBMITTED_DATE = Convert.ToDateTime(xml.Element("CLAIM_SUBMITTED_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("CLAIM_DELIVERY_DATE") != null)
                    CLAIM_DELIVERY_DATE = Convert.ToDateTime(xml.Element("CLAIM_DELIVERY_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("CLAIM_VIOLATION_AMOUNT") != null)
                    CLAIM_VIOLATION_AMOUNT = Convert.ToDecimal(xml.Element("CLAIM_VIOLATION_AMOUNT").Value);
                if (xml.Element("CLAIM_RCV_NAME") != null)
                    CLAIM_RCV_NAME = xml.Element("CLAIM_RCV_NAME").Value;
                if (xml.Element("CLAIM_RCV_ROLE") != null)
                    CLAIM_RCV_ROLE = xml.Element("CLAIM_RCV_ROLE").Value;
                if (xml.Element("CLAIM_RCV_GIVEN_NAME") != null)
                    CLAIM_RCV_GIVEN_NAME = xml.Element("CLAIM_RCV_GIVEN_NAME").Value;
                if (xml.Element("CLAIM_RCV_PHONE") != null)
                    CLAIM_RCV_PHONE = xml.Element("CLAIM_RCV_PHONE").Value;
                if (xml.Element("CLAIM_RCV_ADDRESS") != null)
                    CLAIM_RCV_ADDRESS = xml.Element("CLAIM_RCV_ADDRESS").Value;
                if (xml.Element("CLAIM_CONTROL_AUDITOR") != null)
                    CLAIM_CONTROL_AUDITOR = xml.Element("CLAIM_CONTROL_AUDITOR").Value;
                if (xml.Element("COMPLETION_DATE") != null)
                    COMPLETION_DATE = Convert.ToDateTime(xml.Element("COMPLETION_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("COMPLETION_ORDER") != null)
                    COMPLETION_ORDER = xml.Element("COMPLETION_ORDER").Value;
                if (xml.Element("COMPLETION_AMOUNT") != null)
                    COMPLETION_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_AMOUNT").Value);
                if (xml.Element("COMPLETION_STATE_AMOUNT") != null)
                    COMPLETION_STATE_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_STATE_AMOUNT").Value);
                if (xml.Element("COMPLETION_LOCAL_AMOUNT") != null)
                    COMPLETION_LOCAL_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_LOCAL_AMOUNT").Value);
                if (xml.Element("COMPLETION_ORG_AMOUNT") != null)
                    COMPLETION_ORG_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_ORG_AMOUNT").Value);
                if (xml.Element("COMPLETION_OTHER_AMOUNT") != null)
                    COMPLETION_OTHER_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_OTHER_AMOUNT").Value);
                
                if (xml.Element("REMOVED_LAW_AMOUNT") != null)
                    REMOVED_LAW_AMOUNT = Convert.ToDecimal(xml.Element("REMOVED_LAW_AMOUNT").Value);
                if (xml.Element("REMOVED_LAW_DATE") != null)
                    REMOVED_LAW_DATE = Convert.ToDateTime(xml.Element("REMOVED_LAW_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("REMOVED_LAW_NO") != null)
                    REMOVED_LAW_NO = xml.Element("REMOVED_LAW_NO").Value;
                if (xml.Element("REMOVED_INVALID_AMOUNT") != null)
                    REMOVED_INVALID_AMOUNT = Convert.ToDecimal(xml.Element("REMOVED_INVALID_AMOUNT").Value);
                if (xml.Element("REMOVED_INVALID_DATE") != null)
                    REMOVED_INVALID_DATE = Convert.ToDateTime(xml.Element("REMOVED_INVALID_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("REMOVED_INVALID_NO") != null)
                    REMOVED_INVALID_NO = xml.Element("REMOVED_INVALID_NO").Value;
                if (xml.Element("CLAIM_C2_AMOUNT") != null)
                    CLAIM_C2_AMOUNT = Convert.ToDecimal(xml.Element("CLAIM_C2_AMOUNT").Value);
                if (xml.Element("CLAIM_C2_NONEXPIRED") != null)
                    CLAIM_C2_NONEXPIRED = Convert.ToDecimal(xml.Element("CLAIM_C2_NONEXPIRED").Value);
                if (xml.Element("CLAIM_C2_EXPIRED") != null)
                    CLAIM_C2_EXPIRED = Convert.ToDecimal(xml.Element("CLAIM_C2_EXPIRED").Value);

                if (xml.Element("BENEFIT_FIN") != null)
                    BENEFIT_FIN = Convert.ToInt32(xml.Element("BENEFIT_FIN").Value);
                if (xml.Element("BENEFIT_FIN_AMOUNT") != null)
                    BENEFIT_FIN_AMOUNT = Convert.ToDecimal(xml.Element("BENEFIT_FIN_AMOUNT").Value);
                if (xml.Element("BENEFIT_NONFIN") != null)
                    BENEFIT_NONFIN = Convert.ToInt32(xml.Element("BENEFIT_NONFIN").Value);
            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("BM2",
                       new XElement("ID", ID),
                       new XElement("AUDIT_ID", AUDIT_ID),
                       //new XElement("CLAIM_DATE", Convert.ToDateTime(CLAIM_DATE).ToString("dd-MMM-yy")),
                       CLAIM_DATE != null ? new XElement("CLAIM_DATE", Convert.ToDateTime(CLAIM_DATE).ToString("dd-MMM-yy")) : new XElement("CLAIM_DATE", 0),
                       new XElement("CLAIM_NO", CLAIM_NO),
                       new XElement("CLAIM_VIOLATION_DESC", CLAIM_VIOLATION_DESC),
                       new XElement("CLAIM_VIOLATION_TYPE", CLAIM_VIOLATION_TYPE),
                       new XElement("CLAIM_SUBMITTED_DATE", Convert.ToDateTime(CLAIM_SUBMITTED_DATE).ToString("dd-MMM-yy")),
                       new XElement("CLAIM_DELIVERY_DATE", Convert.ToDateTime(CLAIM_DELIVERY_DATE).ToString("dd-MMM-yy")),
                       new XElement("CLAIM_VIOLATION_AMOUNT", CLAIM_VIOLATION_AMOUNT),
                       new XElement("CLAIM_RCV_NAME", CLAIM_RCV_NAME),
                       new XElement("CLAIM_RCV_ROLE", CLAIM_RCV_ROLE),
                       new XElement("CLAIM_RCV_GIVEN_NAME", CLAIM_RCV_GIVEN_NAME),
                       new XElement("CLAIM_RCV_PHONE", CLAIM_RCV_PHONE),
                       new XElement("CLAIM_RCV_ADDRESS", CLAIM_RCV_ADDRESS),
                       new XElement("AUDIT_YEAR", AUDIT_YEAR),
                       //new XElement("CLAIM_CONTROL_AUDITOR", CLAIM_CONTROL_AUDITOR),
                       new XElement("COMPLETION_DATE", Convert.ToDateTime(COMPLETION_DATE).ToString("dd-MMM-yy")),
                       new XElement("COMPLETION_ORDER", COMPLETION_ORDER),
                       new XElement("COMPLETION_AMOUNT", COMPLETION_AMOUNT),
                       new XElement("COMPLETION_STATE_AMOUNT", COMPLETION_STATE_AMOUNT),
                       new XElement("COMPLETION_LOCAL_AMOUNT", COMPLETION_LOCAL_AMOUNT),
                       new XElement("COMPLETION_ORG_AMOUNT", COMPLETION_ORG_AMOUNT),
                       new XElement("COMPLETION_OTHER_AMOUNT", COMPLETION_OTHER_AMOUNT),
                       new XElement("REMOVED_LAW_AMOUNT", REMOVED_LAW_AMOUNT),
                       new XElement("REMOVED_LAW_DATE", Convert.ToDateTime(REMOVED_LAW_DATE).ToString("dd-MMM-yy")),
                       new XElement("REMOVED_LAW_NO", REMOVED_LAW_NO),
                       new XElement("REMOVED_INVALID_AMOUNT", REMOVED_INVALID_AMOUNT),
                       new XElement("REMOVED_INVALID_DATE", Convert.ToDateTime(REMOVED_INVALID_DATE).ToString("dd-MMM-yy")),
                       new XElement("REMOVED_INVALID_NO", REMOVED_INVALID_NO),
                       new XElement("CLAIM_C2_AMOUNT", CLAIM_C2_AMOUNT),
                       new XElement("CLAIM_C2_NONEXPIRED", CLAIM_C2_NONEXPIRED),
                       new XElement("CLAIM_C2_EXPIRED", CLAIM_C2_EXPIRED),
                       new XElement("BENEFIT_FIN", BENEFIT_FIN),
                       new XElement("BENEFIT_FIN_AMOUNT", BENEFIT_FIN_AMOUNT),
                       new XElement("BENEFIT_NONFIN", BENEFIT_NONFIN),
                       new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", Convert.ToDateTime(CREATED_DATE).ToString("dd-MMM-yy"))
                       );
        }
    }

}