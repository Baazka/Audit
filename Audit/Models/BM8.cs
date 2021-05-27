using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class BM8VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class BM8
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
       // [Required(ErrorMessage = "Утга оруулна уу.")]
        public string CORRECTED_ERROR_DESC { get; set; }
        [Required(ErrorMessage = "Алдааны ангилал оруулна уу.")]
        public int CORRECTED_ERROR_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }
        public int? CORRECTED_COUNT { get; set; }
        public decimal? CORRECTED_AMOUNT { get; set; }
        public int IS_ACTIVE { get; set; } = 1;

        public string CREATED_DATE { get; set; } = DateTime.Now.ToString("dd-MMM-yy");

        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
        public List<REF_AUDIT_YEAR> refaudityears { get; set; } = new List<REF_AUDIT_YEAR>();
        public List<REF_VIOLATION_TYPE> refviolationtypes { get; set; } = new List<REF_VIOLATION_TYPE>();
        public List<REF_AUDIT_TYPE> audittypes { get; set; } = new List<REF_AUDIT_TYPE>();
        public List<REF_BUDGET_TYPE> refbudgettypes { get; set; } = new List<REF_BUDGET_TYPE>();

        public BM8 SetXml(XElement xml)
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

                if (xml.Element("CORRECTED_ERROR_DESC") != null)
                    CORRECTED_ERROR_DESC = xml.Element("CORRECTED_ERROR_DESC").Value;
                if (xml.Element("CORRECTED_ERROR_TYPE") != null)
                    CORRECTED_ERROR_TYPE = Convert.ToInt32(xml.Element("CORRECTED_ERROR_TYPE").Value);
                if (xml.Element("VIOLATION_NAME") != null)
                    VIOLATION_NAME = xml.Element("VIOLATION_NAME").Value;
                if (xml.Element("CORRECTED_COUNT") != null)
                    CORRECTED_COUNT = Convert.ToInt32(xml.Element("CORRECTED_COUNT").Value);
                if (xml.Element("CORRECTED_AMOUNT") != null)
                    CORRECTED_AMOUNT = Convert.ToDecimal(xml.Element("CORRECTED_AMOUNT").Value);
            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("BM8",
                       new XElement("ID", ID),
                       new XElement("AUDIT_ID", AUDIT_ID),
                       new XElement("CORRECTED_ERROR_DESC", CORRECTED_ERROR_DESC),
                       new XElement("CORRECTED_ERROR_TYPE", CORRECTED_ERROR_TYPE),
                       new XElement("CORRECTED_COUNT", CORRECTED_COUNT),
                       new XElement("CORRECTED_AMOUNT", CORRECTED_AMOUNT),
                       new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", Convert.ToDateTime(CREATED_DATE).ToString("dd-MMM-yy"))
                       );
        }
    }

}