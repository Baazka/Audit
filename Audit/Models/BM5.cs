using System;
using System.Collections.Generic;
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
        public int AUDIT_ID { get; set; }
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
        public string ORDER_DATE { get; set; }
        public string ORDER_NO { get; set; }

        public string LAW_RESPONDANT_NAME { get; set; }
        public string LAW_VIOLATION_DESC { get; set; }
        public int LAW_VIOLATION_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }
        public string LAW_MOVING_INFORMATION { get; set; }
        public int LAW_NUMBER { get; set; }
        public decimal LAW_AMOUNT { get; set; }
        public int LAW_C2_NUMBER { get; set; }
        public decimal LAW_C2_AMOUNT { get; set; }

        public int COMPLETION_DONE { get; set; }
        public decimal COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS { get; set; }
        public decimal COMPLETION_PROGRESS_AMOUNT { get; set; }
        public int COMPLETION_INVALID { get; set; }
        public decimal COMPLETION_INVALID_AMOUNT { get; set; }

        public int EXEC_TYPE { get; set; }

        public int IS_ACTIVE { get; set; } = 1;
        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public BM5 SetXml(XElement xml)
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
                    LAW_AMOUNT = Convert.ToDecimal(xml.Element("LAW_AMOUNT").Value);
                if (xml.Element("LAW_C2_NUMBER") != null)
                    LAW_C2_NUMBER = Convert.ToInt32(xml.Element("LAW_C2_NUMBER").Value);
                if (xml.Element("LAW_C2_AMOUNT") != null)
                    LAW_C2_AMOUNT = Convert.ToDecimal(xml.Element("LAW_C2_AMOUNT").Value);

                if (xml.Element("COMPLETION_DONE") != null)
                    COMPLETION_DONE = Convert.ToInt32(xml.Element("COMPLETION_DONE").Value);
                if (xml.Element("COMPLETION_DONE_AMOUNT") != null)
                    COMPLETION_DONE_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_DONE_AMOUNT").Value);
                if (xml.Element("COMPLETION_PROGRESS") != null)
                    COMPLETION_PROGRESS = Convert.ToInt32(xml.Element("COMPLETION_PROGRESS").Value);
                if (xml.Element("COMPLETION_PROGRESS_AMOUNT") != null)
                    COMPLETION_PROGRESS_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_PROGRESS_AMOUNT").Value);
                if (xml.Element("COMPLETION_INVALID") != null)
                    COMPLETION_INVALID = Convert.ToInt32(xml.Element("COMPLETION_INVALID").Value);
                if (xml.Element("COMPLETION_INVALID_AMOUNT") != null)
                    COMPLETION_INVALID_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_INVALID_AMOUNT").Value);

                if (xml.Element("EXEC_TYPE") != null)
                    EXEC_TYPE = Convert.ToInt32(xml.Element("EXEC_TYPE").Value);
            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("BM5",
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
                       new XElement("LAW_RESPONDANT_NAME", LAW_RESPONDANT_NAME),
                       new XElement("LAW_VIOLATION_DESC", LAW_VIOLATION_DESC),
                       new XElement("LAW_VIOLATION_TYPE", LAW_VIOLATION_TYPE),
                       new XElement("LAW_MOVING_INFORMATION", LAW_MOVING_INFORMATION),
                       new XElement("LAW_NUMBER", LAW_NUMBER),
                       new XElement("LAW_AMOUNT", LAW_AMOUNT),
                       new XElement("COMPLETION_DONE", COMPLETION_DONE),
                       new XElement("COMPLETION_DONE_AMOUNT", COMPLETION_DONE_AMOUNT),
                       new XElement("COMPLETION_PROGRESS", COMPLETION_PROGRESS),
                       new XElement("COMPLETION_PROGRESS_AMOUNT", COMPLETION_PROGRESS_AMOUNT),
                       new XElement("COMPLETION_INVALID", COMPLETION_INVALID),
                       new XElement("COMPLETION_INVALID_AMOUNT", COMPLETION_INVALID_AMOUNT),
                       new XElement("LAW_C2_NUMBER", LAW_C2_NUMBER),
                       new XElement("LAW_C2_AMOUNT", LAW_C2_AMOUNT),
                       new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", CREATED_DATE)
                       );
        }
    }
}