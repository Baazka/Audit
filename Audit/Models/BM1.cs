using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class BM1VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class BM1
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

        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ACT_DATE { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ACT_NO { get; set; }
        public string ACT_VIOLATION_DESC { get; set; }
        [Required(ErrorMessage = "Зөрчлийн ангилал сонгоно уу.")]
        public int ACT_VIOLATION_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ACT_SUBMITTED_DATE { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ACT_DELIVERY_DATE { get; set; }
        public string ACT_AMOUNT { get; set; }
        public decimal? ACT_STATE_AMOUNT { get; set; }
        public decimal? ACT_LOCAL_AMOUNT { get; set; }
        public decimal? ACT_ORG_AMOUNT { get; set; }
        public decimal? ACT_OTHER_AMOUNT { get; set; }
        public string ACT_RCV_NAME { get; set; }
        public string ACT_RCV_ROLE { get; set; }
        public string ACT_RCV_GIVEN_NAME { get; set; }
        public string ACT_RCV_PHONE { get; set; }
        public string ACT_RCV_ADDRESS { get; set; }
        public int ACT_CONTROL_AUDITOR_ID { get; set; }
        public string ACT_CONTROL_AUDITOR { get; set; }
        public string COMPLETION_DATE { get; set; }
        public string COMPLETION_ORDER { get; set; }
        public string COMPLETION_AMOUNT { get; set; }
        public decimal? COMPLETION_STATE_AMOUNT { get; set; }
        public decimal? COMPLETION_LOCAL_AMOUNT { get; set; }
        public decimal? COMPLETION_ORG_AMOUNT { get; set; }
        public decimal? COMPLETION_OTHER_AMOUNT { get; set; }
        public string REMOVED_AMOUNT { get; set; }
        public decimal? REMOVED_LAW_AMOUNT { get; set; }
        public string REMOVED_LAW_DATE { get; set; }
        public string REMOVED_LAW_NO { get; set; }
        public decimal? REMOVED_INVALID_AMOUNT { get; set; }
        public string REMOVED_INVALID_DATE { get; set; }
        public string REMOVED_INVALID_NO { get; set; }
        public string ACT_C2_AMOUNT { get; set; }
        public string ACT_C2_NONEXPIRED { get; set; }
        public string ACT_C2_EXPIRED { get; set; }
        public int? BENEFIT_FIN { get; set; }
        public string BENEFIT_FIN_AMOUNT { get; set; }
        public int? BENEFIT_NONFIN { get; set; }
        public int EXEC_TYPE { get; set; }
        public int IS_ACTIVE { get; set; } = 1;
        

        public string CREATED_DATE { get; set; } = DateTime.Now.ToString("dd-MMM-yy");
        public string NOW_CREATED_DATE { get; set; } = DateTime.Now.ToString("yyyy.MM.dd");
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
        public List<REF_AUDIT_YEAR> refaudityears { get; set; } = new List<REF_AUDIT_YEAR>();
        public List<REF_VIOLATION_TYPE> refviolationtypes { get; set; } = new List<REF_VIOLATION_TYPE>();
        public List<REF_AUDIT_TYPE> audittypes { get; set; } = new List<REF_AUDIT_TYPE>();
        public List<REF_BUDGET_TYPE> refbudgettypes { get; set; } = new List<REF_BUDGET_TYPE>();

        public BM1 SetXml(XElement xml)
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

                if (xml.Element("ACT_DATE") != null)
                    ACT_DATE = Convert.ToDateTime(xml.Element("ACT_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("ACT_NO") != null)
                    ACT_NO = xml.Element("ACT_NO").Value;
                if (xml.Element("ACT_VIOLATION_DESC") != null)
                    ACT_VIOLATION_DESC = xml.Element("ACT_VIOLATION_DESC").Value;
                if (xml.Element("ACT_VIOLATION_TYPE") != null)
                    ACT_VIOLATION_TYPE = Convert.ToInt32(xml.Element("ACT_VIOLATION_TYPE").Value);
                if (xml.Element("VIOLATION_NAME") != null)
                    VIOLATION_NAME = xml.Element("VIOLATION_NAME").Value;
                if (xml.Element("ACT_SUBMITTED_DATE") != null)
                    ACT_SUBMITTED_DATE = Convert.ToDateTime(xml.Element("ACT_SUBMITTED_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("ACT_DELIVERY_DATE") != null)
                    ACT_DELIVERY_DATE = Convert.ToDateTime(xml.Element("ACT_DELIVERY_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("ACT_AMOUNT") != null)
                    ACT_AMOUNT = xml.Element("ACT_AMOUNT").Value;
                if (xml.Element("ACT_STATE_AMOUNT") != null)
                    ACT_STATE_AMOUNT = Convert.ToDecimal(xml.Element("ACT_STATE_AMOUNT").Value);
                if (xml.Element("ACT_LOCAL_AMOUNT") != null)
                    ACT_LOCAL_AMOUNT = Convert.ToDecimal(xml.Element("ACT_LOCAL_AMOUNT").Value);
                if (xml.Element("ACT_ORG_AMOUNT") != null)
                    ACT_ORG_AMOUNT = Convert.ToDecimal(xml.Element("ACT_ORG_AMOUNT").Value);
                if (xml.Element("ACT_OTHER_AMOUNT") != null)
                    ACT_OTHER_AMOUNT = Convert.ToDecimal(xml.Element("ACT_OTHER_AMOUNT").Value);
                if (xml.Element("ACT_RCV_NAME") != null)
                    ACT_RCV_NAME = xml.Element("ACT_RCV_NAME").Value;
                if (xml.Element("ACT_RCV_ROLE") != null)
                    ACT_RCV_ROLE = xml.Element("ACT_RCV_ROLE").Value;
                if (xml.Element("ACT_RCV_GIVEN_NAME") != null)
                    ACT_RCV_GIVEN_NAME = xml.Element("ACT_RCV_GIVEN_NAME").Value;
                if (xml.Element("ACT_RCV_PHONE") != null)
                    ACT_RCV_PHONE = xml.Element("ACT_RCV_PHONE").Value;
                if (xml.Element("ACT_RCV_ADDRESS") != null)
                    ACT_RCV_ADDRESS = xml.Element("ACT_RCV_ADDRESS").Value;
                if (xml.Element("ACT_CONTROL_AUDITOR") != null)
                    ACT_CONTROL_AUDITOR = xml.Element("ACT_CONTROL_AUDITOR").Value;
                if (xml.Element("COMPLETION_DATE") != null)
                    COMPLETION_DATE = Convert.ToDateTime(xml.Element("COMPLETION_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("COMPLETION_ORDER") != null)
                    COMPLETION_ORDER = xml.Element("COMPLETION_ORDER").Value;
                if (xml.Element("COMPLETION_AMOUNT") != null)
                    COMPLETION_AMOUNT = xml.Element("COMPLETION_AMOUNT").Value;
                if (xml.Element("COMPLETION_STATE_AMOUNT") != null)
                    COMPLETION_STATE_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_STATE_AMOUNT").Value);
                if (xml.Element("COMPLETION_LOCAL_AMOUNT") != null)
                    COMPLETION_LOCAL_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_LOCAL_AMOUNT").Value);
                if (xml.Element("COMPLETION_ORG_AMOUNT") != null)
                    COMPLETION_ORG_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_ORG_AMOUNT").Value);
                if (xml.Element("COMPLETION_OTHER_AMOUNT") != null)
                    COMPLETION_OTHER_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_OTHER_AMOUNT").Value);
                if (xml.Element("REMOVED_AMOUNT") != null)
                    REMOVED_AMOUNT = xml.Element("REMOVED_AMOUNT").Value;
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
                if (xml.Element("ACT_C2_AMOUNT") != null)
                    ACT_C2_AMOUNT = xml.Element("ACT_C2_AMOUNT").Value;
                if (xml.Element("ACT_C2_NONEXPIRED") != null)
                    ACT_C2_NONEXPIRED = xml.Element("ACT_C2_NONEXPIRED").Value;
                if (xml.Element("ACT_C2_EXPIRED") != null)
                    ACT_C2_EXPIRED = xml.Element("ACT_C2_EXPIRED").Value;
                if (xml.Element("BENEFIT_FIN") != null)
                    BENEFIT_FIN = Convert.ToInt32(xml.Element("BENEFIT_FIN").Value);
                if (xml.Element("BENEFIT_FIN_AMOUNT") != null)
                    BENEFIT_FIN_AMOUNT = xml.Element("BENEFIT_FIN_AMOUNT").Value;
                if (xml.Element("BENEFIT_NONFIN") != null)
                    BENEFIT_NONFIN = Convert.ToInt32(xml.Element("BENEFIT_NONFIN").Value);
                if (xml.Element("EXEC_TYPE") != null)
                    EXEC_TYPE = Convert.ToInt32(xml.Element("EXEC_TYPE").Value);
                if (xml.Element("NOW_CREATED_DATE") != null)
                    NOW_CREATED_DATE = Convert.ToDateTime(xml.Element("NOW_CREATED_DATE").Value).ToString("yyyy.MM.dd");
            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("BM1",
                       new XElement("ID", ID),
                       new XElement("AUDIT_ID", AUDIT_ID),
                       new XElement("ACT_DATE", Convert.ToDateTime(ACT_DATE).ToString("dd-MMM-yy")),
                       new XElement("ACT_NO", ACT_NO),
                       new XElement("ACT_VIOLATION_DESC", ACT_VIOLATION_DESC),
                       new XElement("ACT_VIOLATION_TYPE", ACT_VIOLATION_TYPE),
                       new XElement("ACT_SUBMITTED_DATE", Convert.ToDateTime(ACT_SUBMITTED_DATE).ToString("dd-MMM-yy")),
                       new XElement("ACT_DELIVERY_DATE", Convert.ToDateTime(ACT_DELIVERY_DATE).ToString("dd-MMM-yy")),
                       
                      ACT_AMOUNT != null ? new XElement("ACT_AMOUNT", ACT_AMOUNT.Split(',')) : new XElement("ACT_AMOUNT", null),

                       new XElement("ACT_STATE_AMOUNT", ACT_STATE_AMOUNT),
                       new XElement("ACT_LOCAL_AMOUNT", ACT_LOCAL_AMOUNT),
                       new XElement("ACT_ORG_AMOUNT", ACT_ORG_AMOUNT),
                       new XElement("ACT_OTHER_AMOUNT", ACT_OTHER_AMOUNT),
                       new XElement("ACT_RCV_NAME", ACT_RCV_NAME),
                       new XElement("ACT_RCV_ROLE", ACT_RCV_ROLE),
                       new XElement("ACT_RCV_GIVEN_NAME", ACT_RCV_GIVEN_NAME),
                       new XElement("ACT_RCV_PHONE", ACT_RCV_PHONE),
                       new XElement("ACT_RCV_ADDRESS", ACT_RCV_ADDRESS),
                       //new XElement("ACT_CONTROL_AUDITOR", ACT_CONTROL_AUDITOR),
                       COMPLETION_DATE != null ? new XElement("COMPLETION_DATE", Convert.ToDateTime(COMPLETION_DATE).ToString("dd-MMM-yy")) : new XElement("COMPLETION_DATE", null),
                       new XElement("COMPLETION_ORDER", COMPLETION_ORDER),
                       COMPLETION_AMOUNT != null ? new XElement("COMPLETION_AMOUNT", COMPLETION_AMOUNT.Split(',')) : new XElement("COMPLETION_AMOUNT", null),
                       new XElement("COMPLETION_STATE_AMOUNT", COMPLETION_STATE_AMOUNT),
                       new XElement("COMPLETION_LOCAL_AMOUNT", COMPLETION_LOCAL_AMOUNT),
                       new XElement("COMPLETION_ORG_AMOUNT", COMPLETION_ORG_AMOUNT),
                       new XElement("COMPLETION_OTHER_AMOUNT", COMPLETION_OTHER_AMOUNT),
                       REMOVED_AMOUNT != null ? new XElement("REMOVED_AMOUNT", REMOVED_AMOUNT.Split(',')) : new XElement("REMOVED_AMOUNT", null),
                       new XElement("REMOVED_LAW_AMOUNT", REMOVED_LAW_AMOUNT),
                       REMOVED_LAW_DATE != null ? new XElement("REMOVED_LAW_DATE", Convert.ToDateTime(REMOVED_LAW_DATE).ToString("dd-MMM-yy")) : new XElement("REMOVED_LAW_DATE", null),
                       new XElement("REMOVED_LAW_NO", REMOVED_LAW_NO),
                       new XElement("REMOVED_INVALID_AMOUNT", REMOVED_INVALID_AMOUNT),
                       REMOVED_INVALID_DATE != null ? new XElement("REMOVED_INVALID_DATE", Convert.ToDateTime(REMOVED_INVALID_DATE).ToString("dd-MMM-yy")) : new XElement("REMOVED_INVALID_DATE", null),
                       new XElement("REMOVED_INVALID_NO", REMOVED_INVALID_NO),
                       ACT_C2_AMOUNT != null ? new XElement("ACT_C2_AMOUNT", ACT_C2_AMOUNT.Split(',')) : new XElement("ACT_C2_AMOUNT", null),
                       ACT_C2_NONEXPIRED != null ? new XElement("ACT_C2_NONEXPIRED", ACT_C2_NONEXPIRED.Split(',')) : new XElement("ACT_C2_NONEXPIRED", null),
                       ACT_C2_EXPIRED != null ? new XElement("ACT_C2_EXPIRED", ACT_C2_EXPIRED.Split(',')) : new XElement("ACT_C2_EXPIRED", null),
                       new XElement("BENEFIT_FIN", BENEFIT_FIN),
                       BENEFIT_FIN_AMOUNT != null ? new XElement("BENEFIT_FIN_AMOUNT", BENEFIT_FIN_AMOUNT.Split(',')) : new XElement("BENEFIT_FIN_AMOUNT", null),
                       new XElement("BENEFIT_NONFIN", BENEFIT_NONFIN),
                       new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", CREATED_DATE)
                       );
        }
    }

}