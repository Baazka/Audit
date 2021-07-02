using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class CM3VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class CM3
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }

        public string AUDIT_TYPE { get; set; }
        public string DECISION_TYPE { get; set; }
        public string BUDGET_TYPE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }
        public int IS_STATE { get; set; }

        public int? C1_COUNT { get; set; }
        public string C1_AMOUNT { get; set; }
        public int? CURRENT_COUNT { get; set; }
        public string CURRENT_AMOUNT { get; set; }
        public int? TOTAL_COUNT { get; set; }
        public string TOTAL_AMOUNT { get; set; }
        public int? COMPLETION_DONE_COUNT { get; set; }
        public string COMPLETION_DONE_AMOUNT { get; set; }
        public int? COMPLETION_PROGRESS_COUNT { get; set; }
        public string COMPLETION_PROGRESS_AMOUNT { get; set; }
        public int? LAW_COUNT { get; set; }
        public string LAW_AMOUNT { get; set; }
        public int? LAW_CURRENT_COUNT { get; set; }
        public string LAW_CURRENT_AMOUNT { get; set; }
        public int? LAW_TOTAL_COUNT { get; set; }
        public string LAW_TOTAL_AMOUNT { get; set; }
        public int? LAW_COMP_DONE_COUNT { get; set; }
        public string LAW_COMP_DONE_AMOUNT { get; set; }
        public int? LAW_COMP_PROG_COUNT { get; set; }
        public string LAW_COMP_PROG_AMOUNT { get; set; }
        public int? LAW_COMP_INVALID_COUNT { get; set; }
        public string LAW_COMP_INVALID_AMOUNT { get; set; }
        public int? C2_COUNT { get; set; }
        public string C2_AMOUNT { get; set; }
        public int EXEC_TYPE { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public CM3 SetXml(XElement xml)
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
                if (xml.Element("AUDIT_TYPE") != null)
                    AUDIT_TYPE = xml.Element("AUDIT_TYPE").Value;
                if (xml.Element("DECISION_TYPE") != null)
                    DECISION_TYPE = xml.Element("DECISION_TYPE").Value;
                if (xml.Element("BUDGET_TYPE") != null)
                    BUDGET_TYPE = xml.Element("BUDGET_TYPE").Value;
                if (xml.Element("BUDGET_TYPE_NAME") != null)
                    BUDGET_TYPE_NAME = xml.Element("BUDGET_TYPE_NAME").Value;
                if (xml.Element("IS_STATE") != null)
                    IS_STATE = Convert.ToInt32(xml.Element("IS_STATE").Value);
                
                if (xml.Element("C1_COUNT") != null)
                    C1_COUNT = Convert.ToInt32(xml.Element("C1_COUNT").Value);
                if (xml.Element("C1_AMOUNT") != null)
                    C1_AMOUNT = Convert.ToDecimal(xml.Element("C1_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("CURRENT_COUNT") != null)
                    CURRENT_COUNT = Convert.ToInt32(xml.Element("CURRENT_COUNT").Value);
                if (xml.Element("CURRENT_AMOUNT") != null)
                    CURRENT_AMOUNT = Convert.ToDecimal(xml.Element("CURRENT_AMOUNT").Value).ToString("#,0.##");               
                if (xml.Element("TOTAL_COUNT") != null)
                    TOTAL_COUNT = Convert.ToInt32(xml.Element("TOTAL_COUNT").Value);
                if (xml.Element("TOTAL_AMOUNT") != null)
                    TOTAL_AMOUNT = Convert.ToDecimal(xml.Element("TOTAL_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("COMPLETION_DONE_COUNT") != null)
                    COMPLETION_DONE_COUNT = Convert.ToInt32(xml.Element("COMPLETION_DONE_COUNT").Value);
                if (xml.Element("COMPLETION_DONE_AMOUNT") != null)
                    COMPLETION_DONE_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_DONE_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("COMPLETION_PROGRESS_COUNT") != null)
                    COMPLETION_PROGRESS_COUNT = Convert.ToInt32(xml.Element("COMPLETION_PROGRESS_COUNT").Value);
                if (xml.Element("COMPLETION_PROGRESS_AMOUNT") != null)
                    COMPLETION_PROGRESS_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_PROGRESS_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("LAW_COUNT") != null)
                    LAW_COUNT = Convert.ToInt32(xml.Element("LAW_COUNT").Value);
                if (xml.Element("LAW_AMOUNT") != null)
                    LAW_AMOUNT = Convert.ToDecimal(xml.Element("LAW_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("LAW_CURRENT_COUNT") != null)
                    LAW_CURRENT_COUNT = Convert.ToInt32(xml.Element("LAW_CURRENT_COUNT").Value);
                if (xml.Element("LAW_CURRENT_AMOUNT") != null)
                    LAW_CURRENT_AMOUNT = Convert.ToDecimal(xml.Element("LAW_CURRENT_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("LAW_TOTAL_COUNT") != null)
                    LAW_TOTAL_COUNT = Convert.ToInt32(xml.Element("LAW_TOTAL_COUNT").Value);
                if (xml.Element("LAW_TOTAL_AMOUNT") != null)
                    LAW_TOTAL_AMOUNT = Convert.ToDecimal(xml.Element("LAW_TOTAL_AMOUNT").Value).ToString("#,0.##");                
                if (xml.Element("LAW_COMP_DONE_COUNT") != null)
                    LAW_COMP_DONE_COUNT = Convert.ToInt32(xml.Element("LAW_COMP_DONE_COUNT").Value);
                if (xml.Element("LAW_COMP_DONE_AMOUNT") != null)
                    LAW_COMP_DONE_AMOUNT = Convert.ToDecimal(xml.Element("LAW_COMP_DONE_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("LAW_COMP_PROG_COUNT") != null)
                    LAW_COMP_PROG_COUNT = Convert.ToInt32(xml.Element("LAW_COMP_PROG_COUNT").Value);
                if (xml.Element("LAW_COMP_PROG_AMOUNT") != null)
                    LAW_COMP_PROG_AMOUNT = Convert.ToDecimal(xml.Element("LAW_COMP_PROG_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("LAW_COMP_INVALID_COUNT") != null)
                    LAW_COMP_INVALID_COUNT = Convert.ToInt32(xml.Element("LAW_COMP_INVALID_COUNT").Value);
                if (xml.Element("LAW_COMP_INVALID_AMOUNT") != null)
                    LAW_COMP_INVALID_AMOUNT = Convert.ToDecimal(xml.Element("LAW_COMP_INVALID_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("C2_COUNT") != null)
                    C2_COUNT = Convert.ToInt32(xml.Element("C2_COUNT").Value);
                if (xml.Element("C2_AMOUNT") != null)
                    C2_AMOUNT = Convert.ToDecimal(xml.Element("C2_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("EXEC_TYPE") != null)
                    EXEC_TYPE = Convert.ToInt32(xml.Element("EXEC_TYPE").Value);
                if (xml.Element("CREATED_DATE") != null)
                    CREATED_DATE = Convert.ToDateTime(xml.Element("CREATED_DATE").Value);

            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("NM1",
                       new XElement("ID", ID),
                       new XElement("OFFICE_ID", OFFICE_ID),
                       new XElement("STATISTIC_PERIOD", STATISTIC_PERIOD),
                       //new XElement("AUDIT_YEAR", AUDIT_YEAR),
                       //new XElement("AUDIT_TYPE", AUDIT_TYPE),
                       //new XElement("AUDIT_CODE", AUDIT_CODE),
                       //new XElement("AUDIT_NAME", AUDIT_NAME),
                       //new XElement("AUDIT_BUDGET_TYPE", AUDIT_BUDGET_TYPE),
                       //new XElement("CORRECTED_ERROR_DESC", CORRECTED_ERROR_DESC),
                       //new XElement("CORRECTED_ERROR_TYPE", CORRECTED_ERROR_TYPE),
                       //new XElement("CORRECTED_COUNT", CORRECTED_COUNT),
                       //new XElement("CORRECTED_AMOUNT", CORRECTED_AMOUNT),
                       //new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", CREATED_DATE != null ? ((DateTime)CREATED_DATE).ToString("dd-MMM-yy") : null)
                       );
        }
    }

}