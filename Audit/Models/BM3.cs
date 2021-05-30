using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string REFERENCE_DESC { get; set; }
        public int REFERENCE_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public int REFERENCE_COUNT { get; set; }
        public decimal REFERENCE_AMOUNT { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string REFERENCE_SUBMITTED_DATE { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string REFERENCE_DELIVERY_DATE { get; set; }
        public string REFERENCE_RCV_NAME { get; set; }
        public string REFERENCE_RCV_ROLE { get; set; }
        public string REFERENCE_RCV_GIVEN_NAME { get; set; }
        public string REFERENCE_RCV_PHONE { get; set; }
        public string REFERENCE_RCV_ADDRESS { get; set; }
        public int REFERENCE_CONTROL_AUDITOR_ID { get; set; }
        public string REFERENCE_CONTROL_AUDITOR { get; set; }

        public string COMPLETION_DATE { get; set; }
        public string COMPLETION_ORDER { get; set; }
        public int COMPLETION_DONE { get; set; }
        public decimal COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS { get; set; }
        public decimal COMPLETION_PROGRESS_AMOUNT { get; set; }
        public int C2_NONEXPIRED { get; set; }
        public decimal C2_NONEXPIRED_AMOUNT { get; set; }
        public int C2_EXPIRED { get; set; }
        public decimal C2_EXPIRED_AMOUNT { get; set; }

        public int BENEFIT_FIN { get; set; }
        public decimal BENEFIT_FIN_AMOUNT { get; set; }
        public int BENEFIT_NONFIN { get; set; }

        public int IS_ACTIVE { get; set; } = 1;
        public string CREATED_DATE { get; set; } = DateTime.Now.ToString("dd-MMM-yy");
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
        public List<REF_AUDIT_YEAR> refaudityears { get; set; } = new List<REF_AUDIT_YEAR>();
        public List<REF_VIOLATION_TYPE> refviolationtypes { get; set; } = new List<REF_VIOLATION_TYPE>();
        public List<REF_AUDIT_TYPE> audittypes { get; set; } = new List<REF_AUDIT_TYPE>();
        public List<REF_BUDGET_TYPE> refbudgettypes { get; set; } = new List<REF_BUDGET_TYPE>();

        public BM3 SetXml(XElement xml)
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

                if (xml.Element("REFERENCE_DESC") != null)
                    REFERENCE_DESC = xml.Element("REFERENCE_DESC").Value;
                if (xml.Element("REFERENCE_TYPE") != null)
                    REFERENCE_TYPE = Convert.ToInt32(xml.Element("REFERENCE_TYPE").Value);
                if (xml.Element("REFERENCE_RCV_PHONE") != null)
                    REFERENCE_RCV_PHONE = xml.Element("REFERENCE_RCV_PHONE").Value;
                if (xml.Element("VIOLATION_NAME") != null)
                    VIOLATION_NAME = xml.Element("VIOLATION_NAME").Value;
                if (xml.Element("REFERENCE_COUNT") != null)
                    REFERENCE_COUNT = Convert.ToInt32(xml.Element("REFERENCE_COUNT").Value);
                if (xml.Element("REFERENCE_AMOUNT") != null)
                    REFERENCE_AMOUNT = Convert.ToDecimal(xml.Element("REFERENCE_AMOUNT").Value);
                if (xml.Element("REFERENCE_SUBMITTED_DATE") != null)
                    REFERENCE_SUBMITTED_DATE = Convert.ToDateTime(xml.Element("REFERENCE_SUBMITTED_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("REFERENCE_DELIVERY_DATE") != null)
                    REFERENCE_DELIVERY_DATE = Convert.ToDateTime(xml.Element("REFERENCE_DELIVERY_DATE").Value).ToString("yyyy.MM.dd");
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
                if (xml.Element("C2_NONEXPIRED") != null)
                    C2_NONEXPIRED = Convert.ToInt32(xml.Element("C2_NONEXPIRED").Value);
                if (xml.Element("C2_NONEXPIRED_AMOUNT") != null)
                    C2_NONEXPIRED_AMOUNT = Convert.ToDecimal(xml.Element("C2_NONEXPIRED_AMOUNT").Value);
                if (xml.Element("C2_EXPIRED") != null)
                    C2_EXPIRED = Convert.ToInt32(xml.Element("C2_EXPIRED").Value);
                if (xml.Element("C2_EXPIRED_AMOUNT") != null)
                    C2_EXPIRED_AMOUNT = Convert.ToDecimal(xml.Element("C2_EXPIRED_AMOUNT").Value);
                
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
            return new XElement("BM3",
                       new XElement("ID", ID),
                       new XElement("AUDIT_ID", AUDIT_ID),
                       new XElement("REFERENCE_DESC", REFERENCE_DESC),
                       new XElement("REFERENCE_TYPE", REFERENCE_TYPE),
                       new XElement("REFERENCE_COUNT", REFERENCE_COUNT),
                       new XElement("REFERENCE_AMOUNT", REFERENCE_AMOUNT),
                       REFERENCE_SUBMITTED_DATE != null ? new XElement("REFERENCE_SUBMITTED_DATE", Convert.ToDateTime(REFERENCE_SUBMITTED_DATE).ToString("dd-MMM-yy")) : new XElement("REFERENCE_SUBMITTED_DATE", null),
                       REFERENCE_DELIVERY_DATE != null ? new XElement("REFERENCE_DELIVERY_DATE", Convert.ToDateTime(REFERENCE_DELIVERY_DATE).ToString("dd-MMM-yy")) : new XElement("REFERENCE_DELIVERY_DATE", null),
                       new XElement("REFERENCE_RCV_NAME", REFERENCE_RCV_NAME),
                       new XElement("REFERENCE_RCV_ROLE", REFERENCE_RCV_ROLE),
                       new XElement("REFERENCE_RCV_GIVEN_NAME", REFERENCE_RCV_GIVEN_NAME),
                       new XElement("REFERENCE_RCV_PHONE", REFERENCE_RCV_PHONE),
                       new XElement("REFERENCE_RCV_ADDRESS", REFERENCE_RCV_ADDRESS),
                       COMPLETION_DATE != null ? new XElement("COMPLETION_DATE", Convert.ToDateTime(COMPLETION_DATE).ToString("dd-MMM-yy")) : new XElement("COMPLETION_DATE", null),
                       new XElement("COMPLETION_ORDER", COMPLETION_ORDER),
                       new XElement("COMPLETION_DONE", COMPLETION_DONE),
                       new XElement("COMPLETION_DONE_AMOUNT", COMPLETION_DONE_AMOUNT),
                       new XElement("COMPLETION_PROGRESS", COMPLETION_PROGRESS),
                       new XElement("COMPLETION_PROGRESS_AMOUNT", COMPLETION_PROGRESS_AMOUNT),
                       new XElement("C2_NONEXPIRED", C2_NONEXPIRED),
                       new XElement("C2_NONEXPIRED_AMOUNT", C2_NONEXPIRED_AMOUNT),
                       new XElement("C2_EXPIRED", C2_EXPIRED),
                       new XElement("C2_EXPIRED_AMOUNT", C2_EXPIRED_AMOUNT),
                       new XElement("BENEFIT_FIN", BENEFIT_FIN),
                       new XElement("BENEFIT_FIN_AMOUNT", BENEFIT_FIN_AMOUNT),
                       new XElement("BENEFIT_NONFIN", BENEFIT_NONFIN),
                       new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", Convert.ToDateTime(CREATED_DATE).ToString("dd-MMM-yy"))
                       );
        }
    }


}