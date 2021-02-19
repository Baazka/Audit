using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class Department
    {
        public int DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public Department FromXml(XElement elem)
        {
            if (elem.Element("DEPARTMENT_ID") != null)
                DEPARTMENT_ID = Convert.ToInt32(elem.Element("DEPARTMENT_ID").Value);
            if (elem.Element("DEPARTMENT_NAME") != null)
                DEPARTMENT_NAME = elem.Element("DEPARTMENT_NAME").Value;
            return this;
        }
    }

    public class Status
    {
        public int STATUS_ID { get; set; }
        public string STATUS_NAME { get; set; }
        public Status FromXml(XElement elem)
        {
            if (elem.Element("STATUS_ID") != null)
                STATUS_ID = Convert.ToInt32(elem.Element("STATUS_ID").Value);
            if (elem.Element("STATUS_NAME") != null)
                STATUS_NAME = elem.Element("STATUS_NAME").Value;
            return this;
        }
    }

    public class Violation
    {
        public int VIOLATION_ID { get; set; }
        public string VIOLATION_NAME { get; set; }
        public Violation FromXml(XElement elem)
        {
            if (elem.Element("VIOLATION_ID") != null)
                VIOLATION_ID = Convert.ToInt32(elem.Element("VIOLATION_ID").Value);
            if (elem.Element("VIOLATION_NAME") != null)
                VIOLATION_NAME = elem.Element("VIOLATION_NAME").Value;
            return this;
        }
    }
    public class Office
    {
        public int OFFICE_ID { get; set; }
        public string OFFICE_NAME { get; set; }
        public Office FromXml(XElement elem)
        {
            if (elem.Element("OFFICE_ID") != null)
                OFFICE_ID = Convert.ToInt32(elem.Element("OFFICE_ID").Value);
            if (elem.Element("OFFICE_NAME") != null)
                OFFICE_NAME = elem.Element("OFFICE_NAME").Value;
            return this;
        }
    }
    public class SubOffice
    {
        public int OFFICE_ID { get; set; }
        public int SUB_OFFICE_ID { get; set; }
        public string SUB_OFFICE_NAME { get; set; }
        public SubOffice FromXml(XElement elem)
        {
            if (elem.Element("OFFICE_ID") != null)
                OFFICE_ID = Convert.ToInt32(elem.Element("OFFICE_ID").Value);
            if (elem.Element("SUB_OFFICE_ID") != null)
                SUB_OFFICE_ID = Convert.ToInt32(elem.Element("SUB_OFFICE_ID").Value);
            if (elem.Element("SUB_OFFICE_NAME") != null)
                SUB_OFFICE_NAME = elem.Element("SUB_OFFICE_NAME").Value;
            return this;
        }
    }
    public class BudgetType
    {
        public int BUDGET_TYPE_ID { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }
        public BudgetType FromXml(XElement elem)
        {
            if (elem.Element("BUDGET_TYPE_ID") != null)
                BUDGET_TYPE_ID = Convert.ToInt32(elem.Element("BUDGET_TYPE_ID").Value);
            if (elem.Element("BUDGET_TYPE_NAME") != null)
                BUDGET_TYPE_NAME = elem.Element("BUDGET_TYPE_NAME").Value;
            return this;
        }
    }
    public class SubBudgetType
    {
        public int BUDGET_TYPE_ID { get; set; }
        public int SUB_BUDGET_TYPE_ID { get; set; }
        public string SUB_BUDGET_TYPE_NAME { get; set; }
        public SubBudgetType FromXml(XElement elem)
        {
            if (elem.Element("BUDGET_TYPE_ID") != null)
                BUDGET_TYPE_ID = Convert.ToInt32(elem.Element("BUDGET_TYPE_ID").Value);
            if (elem.Element("SUB_BUDGET_TYPE_ID") != null)
                SUB_BUDGET_TYPE_ID = Convert.ToInt32(elem.Element("SUB_BUDGET_TYPE_ID").Value);
            if (elem.Element("SUB_BUDGET_TYPE_NAME") != null)
                SUB_BUDGET_TYPE_NAME = elem.Element("SUB_BUDGET_TYPE_NAME").Value;
            return this;
        }
    }
    public class ActivityLib
    {
        public int ACTIVITY_ID { get; set; }
        public string ACTIVITY_NAME { get; set; }
        public ActivityLib FromXml(XElement elem)
        {
            if (elem.Element("ACTIVITY_ID") != null)
                ACTIVITY_ID = Convert.ToInt32(elem.Element("ACTIVITY_ID").Value);
            if (elem.Element("ACTIVITY_NAME") != null)
                ACTIVITY_NAME = elem.Element("ACTIVITY_NAME").Value;
            return this;
        }
    }
    public class Committee
    {
        public int COMMITTEE_ID { get; set; }
        public string COMMITTEE_NAME { get; set; }
        public Committee FromXml(XElement elem)
        {
            if (elem.Element("COMMITTEE_ID") != null)
                COMMITTEE_ID = Convert.ToInt32(elem.Element("COMMITTEE_ID").Value);
            if (elem.Element("COMMITTEE_NAME") != null)
                COMMITTEE_NAME = elem.Element("COMMITTEE_NAME").Value;
            return this;
        }
    }
    public class TaxOffice
    {
        public int TAX_OFFICE_ID { get; set; }
        public string TAX_OFFICE_NAME { get; set; }
        public TaxOffice FromXml(XElement elem)
        {
            if (elem.Element("TAX_OFFICE_ID") != null)
                TAX_OFFICE_ID = Convert.ToInt32(elem.Element("TAX_OFFICE_ID").Value);
            if (elem.Element("TAX_OFFICE_NAME") != null)
                TAX_OFFICE_NAME = elem.Element("TAX_OFFICE_NAME").Value;
            return this;
        }
    }
    public class CostType
    {
        public int COST_TYPE_ID { get; set; }
        public string COST_TYPE_NAME { get; set; }
        public CostType FromXml(XElement elem)
        {
            if (elem.Element("COST_TYPE_ID") != null)
                COST_TYPE_ID = Convert.ToInt32(elem.Element("COST_TYPE_ID").Value);
            if (elem.Element("COST_TYPE_NAME") != null)
                COST_TYPE_NAME = elem.Element("COST_TYPE_NAME").Value;
            return this;
        }
    }
    public class InsuranceOffice
    {
        public int INSURANCE_OFFICE_ID { get; set; }
        public string INSURANCE_OFFICE_NAME { get; set; }
        public InsuranceOffice FromXml(XElement elem)
        {
            if (elem.Element("INSURANCE_OFFICE_ID") != null)
                INSURANCE_OFFICE_ID = Convert.ToInt32(elem.Element("INSURANCE_OFFICE_ID").Value);
            if (elem.Element("INSURANCE_OFFICE_NAME") != null)
                INSURANCE_OFFICE_NAME = elem.Element("INSURANCE_OFFICE_NAME").Value;
            return this;
        }
    }
    public class FinancingType
    {
        public int FINANCING_TYPE_ID { get; set; }
        public string FINANCING_TYPE_NAME { get; set; }
        public FinancingType FromXml(XElement elem)
        {
            if (elem.Element("FINANCING_TYPE_ID") != null)
                FINANCING_TYPE_ID = Convert.ToInt32(elem.Element("FINANCING_TYPE_ID").Value);
            if (elem.Element("FINANCING_TYPE_NAME") != null)
                FINANCING_TYPE_NAME = elem.Element("FINANCING_TYPE_NAME").Value;
            return this;
        }
    }
    public class FinOffice
    {
        public int FIN_OFFICE_ID { get; set; }
        public string FIN_OFFICE_NAME { get; set; }
        public FinOffice FromXml(XElement elem)
        {
            if (elem.Element("FIN_OFFICE_ID") != null)
                FIN_OFFICE_ID = Convert.ToInt32(elem.Element("FIN_OFFICE_ID").Value);
            if (elem.Element("FIN_OFFICE_NAME") != null)
                FIN_OFFICE_NAME = elem.Element("FIN_OFFICE_NAME").Value;
            return this;
        }
    }
    public class Bank
    {
        public int BANK_ID { get; set; }
        public string BANK_NAME { get; set; }
        public Bank FromXml(XElement elem)
        {
            if (elem.Element("BANK_ID") != null)
                BANK_ID = Convert.ToInt32(elem.Element("BANK_ID").Value);
            if (elem.Element("BANK_NAME") != null)
                BANK_NAME = elem.Element("BANK_NAME").Value;
            return this;
        }
    }
    public class Reason
    {
        public int INACTIVE_REASON_ID { get; set; }
        public string INACTIVE_REASON_NAME { get; set; }
        public Reason FromXml(XElement elem)
        {
            if (elem.Element("INACTIVE_REASON_ID") != null)
                INACTIVE_REASON_ID = Convert.ToInt32(elem.Element("INACTIVE_REASON_ID").Value);
            if (elem.Element("INACTIVE_REASON_NAME") != null)
                INACTIVE_REASON_NAME = elem.Element("INACTIVE_REASON_NAME").Value;
            return this;
        }
    }
}