using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class CM2VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class CM2
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }

        public string AUDIT_TYPE { get; set; }
        public string DECISION_TYPE { get; set; }
        public string BUDGET_TYPE { get; set; }
        public int IS_STATE { get; set; }

        public int C1_COUNT { get; set; }
        public decimal C1_AMOUNT { get; set; }
        public int CURRENT_COUNT { get; set; }
        public decimal CURRENT_AMOUNT { get; set; }
        public int PREV_COUNT { get; set; }
        public decimal PREV_AMOUNT { get; set; }
        public int CY_COUNT { get; set; }
        public decimal CY_AMOUNT { get; set; }
        public int TOTAL_COUNT { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public int COMP_STATE_COUNT { get; set; }
        public decimal COMP_STATE_AMOUNT { get; set; }
        public int COMP_LOCAL_COUNT { get; set; }
        public decimal COMP_LOCAL_AMOUNT { get; set; }
        public int COMP_ORG_COUNT { get; set; }
        public decimal COMP_ORG_AMOUNT { get; set; }
        public int COMP_OTHER_COUNT { get; set; }
        public decimal COMP_OTHER_AMOUNT { get; set; }
        public int STATISTIC_COUNT { get; set; }
        public decimal STATISTIC_AMOUNT { get; set; }
        public int C2_COUNT { get; set; }
        public decimal C2_AMOUNT { get; set; }
        public int C2_NONEXPIRED_COUNT { get; set; }
        public decimal C2_NONEXPIRED_AMOUNT { get; set; }
        public int C2_EXPIRED_COUNT { get; set; }
        public decimal C2_EXPIRED_AMOUNT { get; set; }
        public int EXEC_TYPE { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public CM2 SetXml(XElement xml)
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
                if (xml.Element("IS_STATE") != null)
                    IS_STATE = Convert.ToInt32(xml.Element("IS_STATE").Value);
                
                if (xml.Element("C1_COUNT") != null)
                    C1_COUNT = Convert.ToInt32(xml.Element("C1_COUNT").Value);
                if (xml.Element("C1_AMOUNT") != null)
                    C1_AMOUNT = Convert.ToDecimal(xml.Element("C1_AMOUNT").Value);
                if (xml.Element("CURRENT_COUNT") != null)
                    CURRENT_COUNT = Convert.ToInt32(xml.Element("CURRENT_COUNT").Value);
                if (xml.Element("CURRENT_AMOUNT") != null)
                    CURRENT_AMOUNT = Convert.ToDecimal(xml.Element("CURRENT_AMOUNT").Value);
                if (xml.Element("PREV_COUNT") != null)
                    PREV_COUNT = Convert.ToInt32(xml.Element("PREV_COUNT").Value);
                if (xml.Element("PREV_AMOUNT") != null)
                    PREV_AMOUNT = Convert.ToDecimal(xml.Element("PREV_AMOUNT").Value);
                if (xml.Element("CY_COUNT") != null)
                    CY_COUNT = Convert.ToInt32(xml.Element("CY_COUNT").Value);
                if (xml.Element("CY_AMOUNT") != null)
                    CY_AMOUNT = Convert.ToDecimal(xml.Element("CY_AMOUNT").Value);
                if (xml.Element("TOTAL_COUNT") != null)
                    TOTAL_COUNT = Convert.ToInt32(xml.Element("TOTAL_COUNT").Value);
                if (xml.Element("TOTAL_AMOUNT") != null)
                    TOTAL_AMOUNT = Convert.ToDecimal(xml.Element("TOTAL_AMOUNT").Value);
                if (xml.Element("COMP_STATE_COUNT") != null)
                    COMP_STATE_COUNT = Convert.ToInt32(xml.Element("COMP_STATE_COUNT").Value);
                if (xml.Element("COMP_STATE_AMOUNT") != null)
                    COMP_STATE_AMOUNT = Convert.ToDecimal(xml.Element("COMP_STATE_AMOUNT").Value);
                if (xml.Element("COMP_LOCAL_COUNT") != null)
                    COMP_LOCAL_COUNT = Convert.ToInt32(xml.Element("COMP_LOCAL_COUNT").Value);
                if (xml.Element("COMP_LOCAL_AMOUNT") != null)
                    COMP_LOCAL_AMOUNT = Convert.ToDecimal(xml.Element("COMP_LOCAL_AMOUNT").Value);
                if (xml.Element("COMP_ORG_COUNT") != null)
                    COMP_ORG_COUNT = Convert.ToInt32(xml.Element("COMP_ORG_COUNT").Value);
                if (xml.Element("COMP_ORG_AMOUNT") != null)
                    COMP_ORG_AMOUNT = Convert.ToDecimal(xml.Element("COMP_ORG_AMOUNT").Value);
                if (xml.Element("COMP_OTHER_COUNT") != null)
                    COMP_OTHER_COUNT = Convert.ToInt32(xml.Element("COMP_OTHER_COUNT").Value);
                if (xml.Element("COMP_OTHER_AMOUNT") != null)
                    COMP_OTHER_AMOUNT = Convert.ToDecimal(xml.Element("COMP_OTHER_AMOUNT").Value);
                if (xml.Element("STATISTIC_COUNT") != null)
                    STATISTIC_COUNT = Convert.ToInt32(xml.Element("STATISTIC_COUNT").Value);
                if (xml.Element("STATISTIC_AMOUNT") != null)
                    STATISTIC_AMOUNT = Convert.ToDecimal(xml.Element("STATISTIC_AMOUNT").Value);
                if (xml.Element("C2_COUNT") != null)
                    C2_COUNT = Convert.ToInt32(xml.Element("C2_COUNT").Value);
                if (xml.Element("C2_AMOUNT") != null)
                    C2_AMOUNT = Convert.ToDecimal(xml.Element("C2_AMOUNT").Value);
                if (xml.Element("C2_NONEXPIRED_COUNT") != null)
                    C2_NONEXPIRED_COUNT = Convert.ToInt32(xml.Element("C2_NONEXPIRED_COUNT").Value);
                if (xml.Element("C2_NONEXPIRED_AMOUNT") != null)
                    C2_NONEXPIRED_AMOUNT = Convert.ToDecimal(xml.Element("C2_NONEXPIRED_AMOUNT").Value);
                if (xml.Element("C2_EXPIRED_COUNT") != null)
                    C2_EXPIRED_COUNT = Convert.ToInt32(xml.Element("C2_EXPIRED_COUNT").Value);
                if (xml.Element("C2_EXPIRED_AMOUNT") != null)
                    C2_EXPIRED_AMOUNT = Convert.ToDecimal(xml.Element("C2_EXPIRED_AMOUNT").Value);
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