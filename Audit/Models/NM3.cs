using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class NM3VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class NM3
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }
        
        public int REFERENCE_COUNT { get; set; }
        public decimal REFERENCE_AMOUNT { get; set; }
        public int REFERENCE_TYPE { get; set; }
        public int COMPLETION_DONE_COUNT { get; set; }
        public decimal COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS_COUNT { get; set; }
        public decimal COMPLETION_PROGRESS_AMOUNT { get; set; }
        public int C2_NONEXPIRED_COUNT { get; set; }
        public decimal C2_NONEXPIRED_AMOUNT { get; set; }
        public int C2_EXPIRED_COUNT { get; set; }
        public decimal C2_EXPIRED_AMOUNT { get; set; }
        public int BENEFIT_FIN_COUNT { get; set; }
        public decimal BENEFIT_FIN_AMOUNT { get; set; }        
        public int BENEFIT_NONFIN_COUNT { get; set; }

        public int WORKING_PERSON { get; set; }
        public int WORKING_DAY { get; set; }
        public int WORKING_ADDITION_TIME { get; set; }
        
        public int IS_ACTIVE { get; set; } = 1;
        public int EXEC_TYPE { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public NM3 SetXml(XElement xml)
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
                if (xml.Element("AUDIT_YEAR") != null)
                    AUDIT_YEAR = Convert.ToInt32(xml.Element("AUDIT_YEAR").Value);
                if (xml.Element("AUDIT_TYPE") != null)
                    AUDIT_TYPE = xml.Element("AUDIT_TYPE").Value;
                if (xml.Element("AUDIT_CODE") != null)
                    AUDIT_CODE = xml.Element("AUDIT_CODE").Value;
                if (xml.Element("AUDIT_NAME") != null)
                    AUDIT_NAME = xml.Element("AUDIT_NAME").Value;
                if (xml.Element("AUDIT_BUDGET_TYPE") != null)
                    AUDIT_BUDGET_TYPE = xml.Element("AUDIT_BUDGET_TYPE").Value;

                if (xml.Element("REFERENCE_COUNT") != null)
                    REFERENCE_COUNT = Convert.ToInt32(xml.Element("REFERENCE_COUNT").Value);
                if (xml.Element("REFERENCE_AMOUNT") != null)
                    REFERENCE_AMOUNT = Convert.ToDecimal(xml.Element("REFERENCE_AMOUNT").Value);
                if (xml.Element("REFERENCE_TYPE") != null)
                    REFERENCE_TYPE = Convert.ToInt32(xml.Element("REFERENCE_TYPE").Value);
                if (xml.Element("COMPLETION_DONE_COUNT") != null)
                    COMPLETION_DONE_COUNT = Convert.ToInt32(xml.Element("COMPLETION_DONE_COUNT").Value);
                if (xml.Element("COMPLETION_DONE_AMOUNT") != null)
                    COMPLETION_DONE_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_DONE_AMOUNT").Value);
                if (xml.Element("COMPLETION_PROGRESS_COUNT") != null)
                    COMPLETION_PROGRESS_COUNT = Convert.ToInt32(xml.Element("COMPLETION_PROGRESS_COUNT").Value);
                if (xml.Element("COMPLETION_PROGRESS_AMOUNT") != null)
                    COMPLETION_PROGRESS_AMOUNT = Convert.ToDecimal(xml.Element("COMPLETION_PROGRESS_AMOUNT").Value);
                if (xml.Element("C2_NONEXPIRED_COUNT") != null)
                    C2_NONEXPIRED_COUNT = Convert.ToInt32(xml.Element("C2_NONEXPIRED_COUNT").Value);
                if (xml.Element("C2_NONEXPIRED_AMOUNT") != null)
                    C2_NONEXPIRED_AMOUNT = Convert.ToDecimal(xml.Element("C2_NONEXPIRED_AMOUNT").Value);
                if (xml.Element("C2_EXPIRED_COUNT") != null)
                    C2_EXPIRED_COUNT = Convert.ToInt32(xml.Element("C2_EXPIRED_COUNT").Value);
                if (xml.Element("C2_EXPIRED_AMOUNT") != null)
                    C2_EXPIRED_AMOUNT = Convert.ToDecimal(xml.Element("C2_EXPIRED_AMOUNT").Value);
                if (xml.Element("BENEFIT_FIN_COUNT") != null)
                    BENEFIT_FIN_COUNT = Convert.ToInt32(xml.Element("BENEFIT_FIN_COUNT").Value);
                if (xml.Element("BENEFIT_FIN_AMOUNT") != null)
                    BENEFIT_FIN_AMOUNT = Convert.ToDecimal(xml.Element("BENEFIT_FIN_AMOUNT").Value);
                if (xml.Element("BENEFIT_NONFIN_COUNT") != null)
                    BENEFIT_NONFIN_COUNT = Convert.ToInt32(xml.Element("BENEFIT_NONFIN_COUNT").Value);
                if (xml.Element("WORKING_PERSON") != null)
                    WORKING_PERSON = Convert.ToInt32(xml.Element("WORKING_PERSON").Value);
                if (xml.Element("WORKING_DAY") != null)
                    WORKING_DAY = Convert.ToInt32(xml.Element("WORKING_DAY").Value);
                if (xml.Element("WORKING_ADDITION_TIME") != null)
                    WORKING_ADDITION_TIME = Convert.ToInt32(xml.Element("WORKING_ADDITION_TIME").Value);
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
                       new XElement("CREATED_DATE", CREATED_DATE != null ? ((DateTime)CREATED_DATE).ToString("dd-MMM-yy") : null),
                       new XElement("UPDATED_DATE", UPDATED_DATE != null ? ((DateTime)UPDATED_DATE).ToString("dd-MMM-yy") : null)
                       );
        }
    }

}