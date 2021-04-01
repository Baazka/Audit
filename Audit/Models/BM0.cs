﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class BM0VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class BM0
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string TOPIC_TYPE { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string AUDIT_FORM_TYPE { get; set; }
        public string AUDIT_PROPOSAL_TYPE { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }
        public string AUDIT_INCLUDED_ORG { get; set; }
        public int WORKING_PERSON { get; set; }
        public int WORKING_DAY { get; set; }
        public int WORKING_ADDITION_TIME { get; set; }
        public string AUDIT_DEPARTMENT { get; set; }
        public string AUDITOR_LEAD { get; set; }
        public string AUDITOR_MEMBER { get; set; }
        public string AUDITOR_ENTRY { get; set; }
        public int EXEC_TYPE { get; set; }
        public int IS_ACTIVE { get; set; } = 1;

        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public BM0 SetXml(XElement xml)
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
                if (xml.Element("TOPIC_TYPE") != null)
                    TOPIC_TYPE = xml.Element("TOPIC_TYPE").Value;
                if (xml.Element("TOPIC_CODE") != null)
                    TOPIC_CODE = xml.Element("TOPIC_CODE").Value;
                if (xml.Element("TOPIC_NAME") != null)
                    TOPIC_NAME = xml.Element("TOPIC_NAME").Value;
                if (xml.Element("ORDER_NO") != null)
                    ORDER_NO = xml.Element("ORDER_NO").Value;
                if (xml.Element("ORDER_DATE") != null)
                    ORDER_DATE = xml.Element("ORDER_DATE").Value;
                if (xml.Element("AUDIT_FORM_TYPE") != null)
                    AUDIT_FORM_TYPE = xml.Element("AUDIT_FORM_TYPE").Value;
                if (xml.Element("AUDIT_PROPOSAL_TYPE") != null)
                    AUDIT_PROPOSAL_TYPE = xml.Element("AUDIT_PROPOSAL_TYPE").Value;
                if (xml.Element("AUDIT_BUDGET_TYPE") != null)
                    AUDIT_BUDGET_TYPE = xml.Element("AUDIT_BUDGET_TYPE").Value;
                if (xml.Element("AUDIT_INCLUDED_ORG") != null)
                    AUDIT_INCLUDED_ORG = xml.Element("AUDIT_INCLUDED_ORG").Value;
                if (xml.Element("WORKING_PERSON") != null)
                    WORKING_PERSON = Convert.ToInt32(xml.Element("WORKING_PERSON").Value);
                if (xml.Element("WORKING_DAY") != null)
                    WORKING_DAY = Convert.ToInt32(xml.Element("WORKING_DAY").Value);
                if (xml.Element("WORKING_ADDITION_TIME") != null)
                    WORKING_ADDITION_TIME = Convert.ToInt32(xml.Element("WORKING_ADDITION_TIME").Value);
                if (xml.Element("AUDIT_DEPARTMENT") != null)
                    AUDIT_DEPARTMENT = xml.Element("AUDIT_DEPARTMENT").Value;
                if (xml.Element("AUDITOR_LEAD") != null)
                    AUDITOR_LEAD = xml.Element("AUDITOR_LEAD").Value;
                if (xml.Element("AUDITOR_MEMBER") != null)
                    AUDITOR_MEMBER = xml.Element("AUDITOR_MEMBER").Value;
                if (xml.Element("AUDITOR_ENTRY") != null)
                    AUDITOR_ENTRY = xml.Element("AUDITOR_ENTRY").Value;
    }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("BM0",
                       new XElement("ID", ID),
                       new XElement("OFFICE_ID", OFFICE_ID),
                       new XElement("STATISTIC_PERIOD", STATISTIC_PERIOD),
                       new XElement("AUDIT_TYPE", AUDIT_TYPE),
                       new XElement("TOPIC_TYPE", TOPIC_TYPE),
                       new XElement("TOPIC_CODE", TOPIC_CODE),
                       new XElement("TOPIC_NAME", TOPIC_NAME),
                       new XElement("ORDER_NO", ORDER_NO),
                       new XElement("ORDER_DATE", ORDER_DATE),
                       new XElement("AUDIT_PROPOSAL_TYPE", AUDIT_PROPOSAL_TYPE),
                       new XElement("AUDIT_BUDGET_TYPE", AUDIT_BUDGET_TYPE),
                       new XElement("AUDIT_INCLUDED_ORG", AUDIT_INCLUDED_ORG),
                       new XElement("WORKING_PERSON", WORKING_PERSON),
                       new XElement("WORKING_DAY", WORKING_DAY),
                       new XElement("WORKING_ADDITION_TIME", WORKING_ADDITION_TIME),
                       new XElement("AUDIT_DEPARTMENT", AUDIT_DEPARTMENT),
                       new XElement("AUDITOR_LEAD", AUDITOR_LEAD),
                       new XElement("AUDITOR_MEMBER", AUDITOR_MEMBER),
                       new XElement("AUDITOR_ENTRY", AUDITOR_ENTRY),
                       new XElement("EXEC_TYPE", EXEC_TYPE),
                       new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", CREATED_DATE != null ? ((DateTime)CREATED_DATE).ToString("dd-MMM-yy") : null),
                       new XElement("UPDATED_DATE", UPDATED_DATE != null ? ((DateTime)UPDATED_DATE).ToString("dd-MMM-yy") : null)
                       );
        }
    }

}