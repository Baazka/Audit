using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class CM7VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class CM7
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }

        public string AUD_NAME { get; set; }
        public string NAME_TYPE { get; set; }

        public int REFERENCE_COUNT { get; set; }
        public int BUDGET_EXPENSES { get; set; }
        public int HUMAN_RESOURCES { get; set; }
        public int PLANNED_COMPLETED { get; set; }
        public int OTHER { get; set; }
        public int COMP_DONE { get; set; }
        public int COMP_PROGRESS { get; set; }
        public int RESOLVED_COMPLAINT_COUNT { get; set; }
        public string REFERENCE_NOT_COMP { get; set; }
                
        public int IS_ACTIVE { get; set; } = 1;
        public int EXEC_TYPE { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public CM7 SetXml(XElement xml)
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
                if (xml.Element("AUD_NAME") != null)
                    AUD_NAME = xml.Element("AUD_NAME").Value;
                if (xml.Element("NAME_TYPE") != null)
                    NAME_TYPE = xml.Element("NAME_TYPE").Value;
                if (xml.Element("REFERENCE_COUNT") != null)
                    REFERENCE_COUNT = Convert.ToInt32(xml.Element("REFERENCE_COUNT").Value);
                if (xml.Element("BUDGET_EXPENSES") != null)
                    BUDGET_EXPENSES = Convert.ToInt32(xml.Element("BUDGET_EXPENSES").Value);
                if (xml.Element("HUMAN_RESOURCES") != null)
                    HUMAN_RESOURCES = Convert.ToInt32(xml.Element("HUMAN_RESOURCES").Value);
                if (xml.Element("PLANNED_COMPLETED") != null)
                    PLANNED_COMPLETED = Convert.ToInt32(xml.Element("PLANNED_COMPLETED").Value);
                if (xml.Element("OTHER") != null)
                    OTHER = Convert.ToInt32(xml.Element("OTHER").Value);
                if (xml.Element("COMP_DONE") != null)
                    COMP_DONE = Convert.ToInt32(xml.Element("COMP_DONE").Value);
                if (xml.Element("COMP_PROGRESS") != null)
                    COMP_PROGRESS = Convert.ToInt32(xml.Element("COMP_PROGRESS").Value);
                if (xml.Element("RESOLVED_COMPLAINT_COUNT") != null)
                    RESOLVED_COMPLAINT_COUNT = Convert.ToInt32(xml.Element("RESOLVED_COMPLAINT_COUNT").Value);
                if (xml.Element("REFERENCE_NOT_COMP") != null)
                    REFERENCE_NOT_COMP = xml.Element("REFERENCE_NOT_COMP").Value;
                
                if (xml.Element("IS_ACTIVE") != null)
                    IS_ACTIVE = Convert.ToInt32(xml.Element("IS_ACTIVE").Value);
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