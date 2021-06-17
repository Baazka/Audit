using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class NM7VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class NM7
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
        public string DECISION_TYPE { get; set; }

        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }

        public int INCOME_STATE_COUNT { get; set; }
        public string INCOME_STATE_AMOUNT { get; set; }
        public int INCOME_LOCAL_COUNT { get; set; }
        public string INCOME_LOCAL_NUMBER { get; set; }
        public int BUDGET_STATE_COUNT { get; set; }
        public string BUDGET_STATE_AMOUNT { get; set; }
        public int BUDGET_LOCAL_COUNT { get; set; }
        public string BUDGET_LOCAL_AMOUNT { get; set; }
        public int ACCOUNTANT_COUNT { get; set; }
        public string ACCOUNTANT_AMOUNT { get; set; }
        public int EFFICIENCY_COUNT { get; set; }
        public string EFFICIENCY_AMOUNT { get; set; }
        public int LAW_COUNT { get; set; }
        public string LAW_AMOUNT { get; set; }
        public int MONITORING_COUNT { get; set; }
        public string MONITORING_AMOUNT { get; set; }
        public int PURCHASE_COUNT { get; set; }
        public string PURCHASE_AMOUNT { get; set; }
        public int COST_COUNT { get; set; }
        public string COST_AMOUNT { get; set; }
        public int OTHER_COUNT { get; set; }
        public string OTHER_AMOUNT { get; set; }
        public int ALL_COUNT { get; set; }
        public string ALL_AMOUNT { get; set; }

        public int IS_ACTIVE { get; set; } = 1;
        public int EXEC_TYPE { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public NM7 SetXml(XElement xml)
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
                if (xml.Element("DECISION_TYPE") != null)
                    DECISION_TYPE = xml.Element("DECISION_TYPE").Value;

                if (xml.Element("TOPIC_NAME") != null)
                    TOPIC_NAME = xml.Element("TOPIC_NAME").Value;
                if (xml.Element("TOPIC_CODE") != null)
                    TOPIC_CODE = xml.Element("TOPIC_CODE").Value;

                if (xml.Element("INCOME_STATE_COUNT") != null)
                    INCOME_STATE_COUNT = Convert.ToInt32(xml.Element("INCOME_STATE_COUNT").Value);
                if (xml.Element("INCOME_STATE_AMOUNT") != null)
                    INCOME_STATE_AMOUNT = Convert.ToDecimal(xml.Element("INCOME_STATE_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("INCOME_LOCAL_COUNT") != null)
                    INCOME_LOCAL_COUNT = Convert.ToInt32(xml.Element("INCOME_LOCAL_COUNT").Value);
                if (xml.Element("INCOME_LOCAL_NUMBER") != null)
                    INCOME_LOCAL_NUMBER = Convert.ToDecimal(xml.Element("INCOME_LOCAL_NUMBER").Value).ToString("#,0.##");

                if (xml.Element("BUDGET_STATE_COUNT") != null)
                    BUDGET_STATE_COUNT = Convert.ToInt32(xml.Element("BUDGET_STATE_COUNT").Value);
                if (xml.Element("BUDGET_STATE_AMOUNT") != null)
                    BUDGET_STATE_AMOUNT = Convert.ToDecimal(xml.Element("BUDGET_STATE_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("BUDGET_LOCAL_COUNT") != null)
                    BUDGET_LOCAL_COUNT = Convert.ToInt32(xml.Element("BUDGET_LOCAL_COUNT").Value);
                if (xml.Element("BUDGET_LOCAL_AMOUNT") != null)
                    BUDGET_LOCAL_AMOUNT = Convert.ToDecimal(xml.Element("BUDGET_LOCAL_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("ACCOUNTANT_COUNT") != null)
                    ACCOUNTANT_COUNT = Convert.ToInt32(xml.Element("ACCOUNTANT_COUNT").Value);
                if (xml.Element("ACCOUNTANT_AMOUNT") != null)
                    ACCOUNTANT_AMOUNT = Convert.ToDecimal(xml.Element("ACCOUNTANT_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("EFFICIENCY_COUNT") != null)
                    EFFICIENCY_COUNT = Convert.ToInt32(xml.Element("EFFICIENCY_COUNT").Value);
                if (xml.Element("EFFICIENCY_AMOUNT") != null)
                    EFFICIENCY_AMOUNT = Convert.ToDecimal(xml.Element("EFFICIENCY_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("LAW_COUNT") != null)
                    LAW_COUNT = Convert.ToInt32(xml.Element("LAW_COUNT").Value);
                if (xml.Element("LAW_AMOUNT") != null)
                    LAW_AMOUNT = Convert.ToDecimal(xml.Element("LAW_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("MONITORING_COUNT") != null)
                    MONITORING_COUNT = Convert.ToInt32(xml.Element("MONITORING_COUNT").Value);
                if (xml.Element("MONITORING_AMOUNT") != null)
                    MONITORING_AMOUNT = Convert.ToDecimal(xml.Element("MONITORING_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("PURCHASE_COUNT") != null)
                    PURCHASE_COUNT = Convert.ToInt32(xml.Element("PURCHASE_COUNT").Value);
                if (xml.Element("PURCHASE_AMOUNT") != null)
                    PURCHASE_AMOUNT = Convert.ToDecimal(xml.Element("PURCHASE_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("COST_COUNT") != null)
                    COST_COUNT = Convert.ToInt32(xml.Element("COST_COUNT").Value);
                if (xml.Element("COST_AMOUNT") != null)
                    COST_AMOUNT = Convert.ToDecimal(xml.Element("COST_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("OTHER_COUNT") != null)
                    OTHER_COUNT = Convert.ToInt32(xml.Element("OTHER_COUNT").Value);
                if (xml.Element("OTHER_AMOUNT") != null)
                    OTHER_AMOUNT = Convert.ToDecimal(xml.Element("OTHER_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("ALL_COUNT") != null)
                    ALL_COUNT = Convert.ToInt32(xml.Element("ALL_COUNT").Value);
                if (xml.Element("ALL_AMOUNT") != null)
                    ALL_AMOUNT = Convert.ToDecimal(xml.Element("ALL_AMOUNT").Value).ToString("#,0.##");

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