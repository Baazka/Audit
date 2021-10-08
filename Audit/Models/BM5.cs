using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class BM5VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class BM5
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

        public string LAW_RESPONDANT_NAME { get; set; }
        public string LAW_VIOLATION_DESC { get; set; }
        [Required(ErrorMessage = "Зөрчлийн ангилал сонгоно уу.")]
        public int LAW_VIOLATION_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }
        public string LAW_MOVING_INFORMATION { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public int? LAW_NUMBER { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string LAW_AMOUNT { get; set; }
        public int? LAW_C2_NUMBER { get; set; }
        public string LAW_C2_AMOUNT { get; set; }

        public int? COMPLETION_DONE { get; set; }
        public string COMPLETION_DONE_AMOUNT { get; set; }
        public int? COMPLETION_PROGRESS { get; set; }
        public string COMPLETION_PROGRESS_AMOUNT { get; set; }
        public int? COMPLETION_INVALID { get; set; }
        public string COMPLETION_INVALID_AMOUNT { get; set; }

        public int IS_ACTIVE { get; set; } = 1;
        public string CREATED_DATE { get; set; } = DateTime.Now.ToString("dd-MMM-yy");
        public string NOW_CREATED_DATE { get; set; } = DateTime.Now.ToString("yyyy.MM.dd");
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
        public List<REF_AUDIT_YEAR> refaudityears { get; set; } = new List<REF_AUDIT_YEAR>();
        public List<REF_VIOLATION_TYPE> refviolationtypes { get; set; } = new List<REF_VIOLATION_TYPE>();
        public List<REF_AUDIT_TYPE> audittypes { get; set; } = new List<REF_AUDIT_TYPE>();
        public List<REF_BUDGET_TYPE> refbudgettypes { get; set; } = new List<REF_BUDGET_TYPE>();

        public BM5 SetXml(XElement xml)
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

                if (xml.Element("LAW_RESPONDANT_NAME") != null)
                    LAW_RESPONDANT_NAME = xml.Element("LAW_RESPONDANT_NAME").Value;
                if (xml.Element("LAW_VIOLATION_DESC") != null)
                    LAW_VIOLATION_DESC = xml.Element("LAW_VIOLATION_DESC").Value;
                if (xml.Element("LAW_VIOLATION_TYPE") != null)
                    LAW_VIOLATION_TYPE = Convert.ToInt32(xml.Element("LAW_VIOLATION_TYPE").Value);
                if (xml.Element("VIOLATION_NAME") != null)
                    VIOLATION_NAME = xml.Element("VIOLATION_NAME").Value;
                if (xml.Element("LAW_MOVING_INFORMATION") != null)
                    LAW_MOVING_INFORMATION = xml.Element("LAW_MOVING_INFORMATION").Value;
                if (xml.Element("LAW_NUMBER") != null)
                    LAW_NUMBER = Convert.ToInt32(xml.Element("LAW_NUMBER").Value);
                if (xml.Element("LAW_AMOUNT") != null)
                    LAW_AMOUNT = xml.Element("LAW_AMOUNT").Value;
                if (xml.Element("LAW_C2_NUMBER") != null)
                    LAW_C2_NUMBER = Convert.ToInt32(xml.Element("LAW_C2_NUMBER").Value);
                if (xml.Element("LAW_C2_AMOUNT") != null)
                    LAW_C2_AMOUNT = xml.Element("LAW_C2_AMOUNT").Value;

                if (xml.Element("COMPLETION_DONE") != null)
                    COMPLETION_DONE = Convert.ToInt32(xml.Element("COMPLETION_DONE").Value);
                if (xml.Element("COMPLETION_DONE_AMOUNT") != null)
                    COMPLETION_DONE_AMOUNT = xml.Element("COMPLETION_DONE_AMOUNT").Value;
                if (xml.Element("COMPLETION_PROGRESS") != null)
                    COMPLETION_PROGRESS = Convert.ToInt32(xml.Element("COMPLETION_PROGRESS").Value);
                if (xml.Element("COMPLETION_PROGRESS_AMOUNT") != null)
                    COMPLETION_PROGRESS_AMOUNT = xml.Element("COMPLETION_PROGRESS_AMOUNT").Value;
                if (xml.Element("COMPLETION_INVALID") != null)
                    COMPLETION_INVALID = Convert.ToInt32(xml.Element("COMPLETION_INVALID").Value);
                if (xml.Element("COMPLETION_INVALID_AMOUNT") != null)
                    COMPLETION_INVALID_AMOUNT = xml.Element("COMPLETION_INVALID_AMOUNT").Value;
            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("BM5",
                       new XElement("ID", ID),
                       new XElement("AUDIT_ID", AUDIT_ID),
                       new XElement("LAW_RESPONDANT_NAME", LAW_RESPONDANT_NAME),
                       new XElement("LAW_VIOLATION_DESC", LAW_VIOLATION_DESC),
                       new XElement("LAW_VIOLATION_TYPE", LAW_VIOLATION_TYPE),
                       new XElement("LAW_MOVING_INFORMATION", LAW_MOVING_INFORMATION),
                       new XElement("LAW_NUMBER", LAW_NUMBER),
                       LAW_AMOUNT != null ? new XElement("LAW_AMOUNT", LAW_AMOUNT.Split(',')) : new XElement("LAW_AMOUNT", null),
                       new XElement("COMPLETION_DONE", COMPLETION_DONE),
                       COMPLETION_DONE_AMOUNT != null ? new XElement("COMPLETION_DONE_AMOUNT", COMPLETION_DONE_AMOUNT.Split(',')) : new XElement("COMPLETION_DONE_AMOUNT", null),
                       new XElement("COMPLETION_PROGRESS", COMPLETION_PROGRESS),
                       COMPLETION_PROGRESS_AMOUNT != null ? new XElement("COMPLETION_PROGRESS_AMOUNT", COMPLETION_PROGRESS_AMOUNT.Split(',')) : new XElement("COMPLETION_PROGRESS_AMOUNT", null),
                       new XElement("COMPLETION_INVALID", COMPLETION_INVALID),
                       COMPLETION_INVALID_AMOUNT != null ? new XElement("COMPLETION_INVALID_AMOUNT", COMPLETION_INVALID_AMOUNT.Split(',')) : new XElement("COMPLETION_INVALID_AMOUNT", null),
                       new XElement("LAW_C2_NUMBER", LAW_C2_NUMBER),
                       LAW_C2_AMOUNT != null ? new XElement("LAW_C2_AMOUNT", LAW_C2_AMOUNT.Split(',')) : new XElement("LAW_C2_AMOUNT", null),
                       new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", Convert.ToDateTime(CREATED_DATE).ToString("dd-MMM-yy"))
                       );
        }
    }

    public class BM5List
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

        public string LAW_RESPONDANT_NAME { get; set; }
        public string LAW_VIOLATION_DESC { get; set; }
        [Required(ErrorMessage = "Зөрчлийн ангилал сонгоно уу.")]
        public int LAW_VIOLATION_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }
        public string LAW_MOVING_INFORMATION { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public int? LAW_NUMBER { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string LAW_AMOUNT { get; set; }
        public int? LAW_C2_NUMBER { get; set; }
        public string LAW_C2_AMOUNT { get; set; }

        public int? COMPLETION_DONE { get; set; }
        public string COMPLETION_DONE_AMOUNT { get; set; }
        public int? COMPLETION_PROGRESS { get; set; }
        public string COMPLETION_PROGRESS_AMOUNT { get; set; }
        public int? COMPLETION_INVALID { get; set; }
        public string COMPLETION_INVALID_AMOUNT { get; set; }
        public int CREATED_BY { get; set; }
        public int UPDATED_BY { get; set; }
        public int IS_ACTIVE { get; set; } = 1;
        public string CREATED_DATE { get; set; } = DateTime.Now.ToString("dd-MMM-yy");
        public string NOW_CREATED_DATE { get; set; } = DateTime.Now.ToString("yyyy.MM.dd");
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
        public List<REF_AUDIT_YEAR> refaudityears { get; set; } = new List<REF_AUDIT_YEAR>();
        public List<REF_VIOLATION_TYPE> refviolationtypes { get; set; } = new List<REF_VIOLATION_TYPE>();
        public List<REF_AUDIT_TYPE> audittypes { get; set; } = new List<REF_AUDIT_TYPE>();
        public List<REF_BUDGET_TYPE> refbudgettypes { get; set; } = new List<REF_BUDGET_TYPE>();

        public BM5List SetXml(XElement xml)
        {
            if (xml != null)
            {
                if (xml.Element("CREATED_BY") != null)
                    CREATED_BY = Convert.ToInt32(xml.Element("CREATED_BY").Value);
                if (xml.Element("UPDATED_BY") != null)
                    UPDATED_BY = Convert.ToInt32(xml.Element("UPDATED_BY").Value);
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

                if (xml.Element("LAW_RESPONDANT_NAME") != null)
                    LAW_RESPONDANT_NAME = xml.Element("LAW_RESPONDANT_NAME").Value;
                if (xml.Element("LAW_VIOLATION_DESC") != null)
                    LAW_VIOLATION_DESC = xml.Element("LAW_VIOLATION_DESC").Value;
                if (xml.Element("LAW_VIOLATION_TYPE") != null)
                    LAW_VIOLATION_TYPE = Convert.ToInt32(xml.Element("LAW_VIOLATION_TYPE").Value);
                if (xml.Element("VIOLATION_NAME") != null)
                    VIOLATION_NAME = xml.Element("VIOLATION_NAME").Value;
                if (xml.Element("LAW_MOVING_INFORMATION") != null)
                    LAW_MOVING_INFORMATION = xml.Element("LAW_MOVING_INFORMATION").Value;
                if (xml.Element("LAW_NUMBER") != null)
                    LAW_NUMBER = Convert.ToInt32(xml.Element("LAW_NUMBER").Value);
                if (xml.Element("LAW_AMOUNT") != null)
                    LAW_AMOUNT = Convert.ToDecimal(xml.Element("LAW_AMOUNT").Value).ToString("#,0.00");
                if (xml.Element("LAW_C2_NUMBER") != null)
                    LAW_C2_NUMBER = Convert.ToInt32(xml.Element("LAW_C2_NUMBER").Value);
                if (xml.Element("LAW_C2_AMOUNT") != null)
                    LAW_C2_AMOUNT = Convert.ToDecimal(xml.Element("LAW_C2_AMOUNT").Value).ToString("#,0.00");

                if (xml.Element("COMPLETION_DONE") != null)
                    COMPLETION_DONE = Convert.ToInt32(xml.Element("COMPLETION_DONE").Value);
                if (xml.Element("COMPLETION_DONE_AMOUNT") != null)
                    COMPLETION_DONE_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_DONE_AMOUNT").Value).ToString("#,0.00");
                if (xml.Element("COMPLETION_PROGRESS") != null)
                    COMPLETION_PROGRESS = Convert.ToInt32(xml.Element("COMPLETION_PROGRESS").Value);
                if (xml.Element("COMPLETION_PROGRESS_AMOUNT") != null)
                    COMPLETION_PROGRESS_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_PROGRESS_AMOUNT").Value).ToString("#,0.00");
                if (xml.Element("COMPLETION_INVALID") != null)
                    COMPLETION_INVALID = Convert.ToInt32(xml.Element("COMPLETION_INVALID").Value);
                if (xml.Element("COMPLETION_INVALID_AMOUNT") != null)
                    COMPLETION_INVALID_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_INVALID_AMOUNT").Value).ToString("#,0.00");
            }
            return this;
        }
   
    }
}