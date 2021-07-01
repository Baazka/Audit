using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class CM1VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class CM1
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }

        public string AUDIT_TYPE { get; set; }
        public string CATEGORY_TYPE { get; set; }
        public int IS_STATE { get; set; }
        public int WORKING_PERSON { get; set; }
        public int WORKING_DAY { get; set; }
        public int WORKING_ADDITION_TIME { get; set; }
        public int EXECUTORY { get; set; }
        public int EXEC_DECISION { get; set; }
        public int EXEC_COLLECTION { get; set; }
        public int EXEC_TRUSTED { get; set; }
        public int PERFORMED { get; set; }
        public int PERF_DECISION { get; set; }
        public int PERF_COLLECTION { get; set; }
        public int PERF_TRUSTED { get; set; }
        public int PERF_NOT_AUDITED { get; set; }
        public int PROPOSAL { get; set; }
        public int PROP_UNVIOLATED { get; set; }
        public int PROP_RESTRICTED { get; set; }
        public int PROP_NEGATIVE { get; set; }
        public int PROP_NOT { get; set; }
        public int TPA_COUNT { get; set; }
        public string TPA_AMOUNT { get; set; }
        public int AUDITED_INCLUDED_ORG { get; set; }
        public int BENEFIT_FIN_COUNT { get; set; }
        public string BENEFIT_FIN_AMOUNT { get; set; }
        public int BENEFIT_NONFIN { get; set; }
        public int EXEC_TYPE { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public CM1 SetXml(XElement xml)
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
                if (xml.Element("CATEGORY_TYPE") != null)
                    CATEGORY_TYPE = xml.Element("CATEGORY_TYPE").Value;
                if (xml.Element("IS_STATE") != null)
                    IS_STATE = Convert.ToInt32(xml.Element("IS_STATE").Value);
                if (xml.Element("WORKING_PERSON") != null)
                    WORKING_PERSON = Convert.ToInt32(xml.Element("WORKING_PERSON").Value);
                if (xml.Element("WORKING_DAY") != null)
                    WORKING_DAY = Convert.ToInt32(xml.Element("WORKING_DAY").Value);
                if (xml.Element("WORKING_ADDITION_TIME") != null)
                    WORKING_ADDITION_TIME = Convert.ToInt32(xml.Element("WORKING_ADDITION_TIME").Value);
                if (xml.Element("EXECUTORY") != null)
                    EXECUTORY = Convert.ToInt32(xml.Element("EXECUTORY").Value);
                if (xml.Element("EXEC_DECISION") != null)
                    EXEC_DECISION = Convert.ToInt32(xml.Element("EXEC_DECISION").Value);
                if (xml.Element("EXEC_COLLECTION") != null)
                    EXEC_COLLECTION = Convert.ToInt32(xml.Element("EXEC_COLLECTION").Value);
                if (xml.Element("EXEC_TRUSTED") != null)
                    EXEC_TRUSTED = Convert.ToInt32(xml.Element("EXEC_TRUSTED").Value);
                if (xml.Element("PERFORMED") != null)
                    PERFORMED = Convert.ToInt32(xml.Element("PERFORMED").Value);
                if (xml.Element("PERF_DECISION") != null)
                    PERF_DECISION = Convert.ToInt32(xml.Element("PERF_DECISION").Value);
                if (xml.Element("PERF_COLLECTION") != null)
                    PERF_COLLECTION = Convert.ToInt32(xml.Element("PERF_COLLECTION").Value);
                if (xml.Element("PERF_TRUSTED") != null)
                    PERF_TRUSTED = Convert.ToInt32(xml.Element("PERF_TRUSTED").Value);
                if (xml.Element("PERF_NOT_AUDITED") != null)
                    PERF_NOT_AUDITED = Convert.ToInt32(xml.Element("PERF_NOT_AUDITED").Value);
                if (xml.Element("PROPOSAL") != null)
                    PROPOSAL = Convert.ToInt32(xml.Element("PROPOSAL").Value);
                if (xml.Element("PROP_UNVIOLATED") != null)
                    PROP_UNVIOLATED = Convert.ToInt32(xml.Element("PROP_UNVIOLATED").Value);
                if (xml.Element("PROP_RESTRICTED") != null)
                    PROP_RESTRICTED = Convert.ToInt32(xml.Element("PROP_RESTRICTED").Value);
                if (xml.Element("PROP_NEGATIVE") != null)
                    PROP_NEGATIVE = Convert.ToInt32(xml.Element("PROP_NEGATIVE").Value);
                if (xml.Element("PROP_NOT") != null)
                    PROP_NOT = Convert.ToInt32(xml.Element("PROP_NOT").Value);
                if (xml.Element("TPA_COUNT") != null)
                    TPA_COUNT = Convert.ToInt32(xml.Element("TPA_COUNT").Value);
                if (xml.Element("TPA_AMOUNT") != null)
                    TPA_AMOUNT = Convert.ToDecimal(xml.Element("TPA_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("AUDITED_INCLUDED_ORG") != null)
                    AUDITED_INCLUDED_ORG = Convert.ToInt32(xml.Element("AUDITED_INCLUDED_ORG").Value);
                if (xml.Element("BENEFIT_FIN_COUNT") != null)
                    BENEFIT_FIN_COUNT = Convert.ToInt32(xml.Element("BENEFIT_FIN_COUNT").Value);
                if (xml.Element("BENEFIT_FIN_AMOUNT") != null)
                    BENEFIT_FIN_AMOUNT = Convert.ToDecimal(xml.Element("BENEFIT_FIN_AMOUNT").Value).ToString("#,0.##");
                if (xml.Element("BENEFIT_NONFIN") != null)
                    BENEFIT_NONFIN = Convert.ToInt32(xml.Element("BENEFIT_NONFIN").Value);
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