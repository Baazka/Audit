﻿using System;
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
    public class ParentBudgetType
    {
        public int PARENT_BUDGET_ID { get; set; }
        public string PARENT_BUDGET_NAME { get; set; }
        public ParentBudgetType FromXml(XElement elem)
        {
            if (elem.Element("PARENT_BUDGET_ID") != null)
                PARENT_BUDGET_ID = Convert.ToInt32(elem.Element("PARENT_BUDGET_ID").Value);
            if (elem.Element("PARENT_BUDGET_NAME") != null)
                PARENT_BUDGET_NAME = elem.Element("PARENT_BUDGET_NAME").Value;
            return this;
        }
    }

    public class TtzBudgetType
    {
        public int TTZ_CODE { get; set; }
        public string TTZ_NAME { get; set; }
        public TtzBudgetType FromXml(XElement elem)
        {
            if (elem.Element("TTZ_CODE") != null)
                TTZ_CODE = Convert.ToInt32(elem.Element("TTZ_CODE").Value);
            if (elem.Element("TTZ_NAME") != null)
                TTZ_NAME = elem.Element("TTZ_NAME").Value;
            return this;
        }
    }

    public class LegalStatus
    {
        public int LEGAL_STATUS_ID { get; set; }
        public string LEGAL_STATUS_NAME { get; set; }
        public LegalStatus FromXml(XElement elem)
        {
            if (elem.Element("LEGAL_STATUS_ID") != null)
                LEGAL_STATUS_ID = Convert.ToInt32(elem.Element("LEGAL_STATUS_ID").Value);
            if (elem.Element("LEGAL_STATUS_NAME") != null)
                LEGAL_STATUS_NAME = elem.Element("LEGAL_STATUS_NAME").Value;
            return this;
        }
    }

    public class OrgLegalStatus
    {
        public int LEGAL_STATUS_ID { get; set; }
        public string LEGAL_STATUS_NAME { get; set; }
        public OrgLegalStatus FromXml(XElement elem)
        {
            if (elem.Element("LEGAL_STATUS_ID") != null)
                LEGAL_STATUS_ID = Convert.ToInt32(elem.Element("LEGAL_STATUS_ID").Value);
            if (elem.Element("LEGAL_STATUS_NAME") != null)
                LEGAL_STATUS_NAME = elem.Element("LEGAL_STATUS_NAME").Value;
            return this;
        }
    }

    public class PropertyType
    {
        public int PROPERTY_TYPE_ID { get; set; }
        public string PROPERTY_TYPE_NAME { get; set; }
        public PropertyType FromXml(XElement elem)
        {
            if (elem.Element("PROPERTY_TYPE_ID") != null)
                PROPERTY_TYPE_ID = Convert.ToInt32(elem.Element("PROPERTY_TYPE_ID").Value);
            if (elem.Element("PROPERTY_TYPE_NAME") != null)
                PROPERTY_TYPE_NAME = elem.Element("PROPERTY_TYPE_NAME").Value;
            return this;
        }
    }

    public class SourceType
    {
        public int SOURCE_TYPE_ID { get; set; }
        public string SOURCE_TYPE_NAME { get; set; }
        public SourceType FromXml(XElement elem)
        {
            if (elem.Element("SOURCE_TYPE_ID") != null)
                SOURCE_TYPE_ID = Convert.ToInt32(elem.Element("SOURCE_TYPE_ID").Value);
            if (elem.Element("SOURCE_TYPE_NAME") != null)
                SOURCE_TYPE_NAME = elem.Element("SOURCE_TYPE_NAME").Value;
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

    public class BudgetLevel
    {
        public int BUDGET_LEVEL_ID { get; set; }
        public string BUDGET_LEVEL_NAME { get; set; }
        public BudgetLevel FromXml(XElement elem)
        {
            if (elem.Element("BUDGET_LEVEL_ID") != null)
                BUDGET_LEVEL_ID = Convert.ToInt32(elem.Element("BUDGET_LEVEL_ID").Value);
            if (elem.Element("BUDGET_LEVEL_NAME") != null)
                BUDGET_LEVEL_NAME = elem.Element("BUDGET_LEVEL_NAME").Value;
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
    public class Period
    {
        public int ID { get; set; }
        public string PERIOD_LABEL { get; set; }
        public Period FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("PERIOD_LABEL") != null)
                PERIOD_LABEL = elem.Element("PERIOD_LABEL").Value;
            return this;
        }
    }
    public class REF_AUDIT_TYPE
    {
        public int AUDIT_TYPE_ID { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public REF_AUDIT_TYPE FromXml(XElement elem)
        {
            if (elem.Element("AUDIT_TYPE_ID") != null)
                AUDIT_TYPE_ID = Convert.ToInt32(elem.Element("AUDIT_TYPE_ID").Value);
            if (elem.Element("AUDIT_TYPE_NAME") != null)
                AUDIT_TYPE_NAME = elem.Element("AUDIT_TYPE_NAME").Value;
            return this;
        }
    }
    public class REF_TOPIC_TYPE
    {
        public int TOPIC_TYPE_ID { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public int TOPIC_AUDIT_TYPE_ID { get; set; }
        public REF_TOPIC_TYPE FromXml(XElement elem)
        {
            if (elem.Element("TOPIC_TYPE_ID") != null)
                TOPIC_TYPE_ID = Convert.ToInt32(elem.Element("TOPIC_TYPE_ID").Value);
            if (elem.Element("TOPIC_TYPE_NAME") != null)
                TOPIC_TYPE_NAME = elem.Element("TOPIC_TYPE_NAME").Value;
            if (elem.Element("TOPIC_AUDIT_TYPE_ID") != null)
                TOPIC_AUDIT_TYPE_ID = Convert.ToInt32(elem.Element("TOPIC_AUDIT_TYPE_ID").Value);
            return this;
        }
    }
    public class REF_FORM_TYPE
    {
        public int FORM_TYPE_ID { get; set; }
        public string FORM_TYPE_NAME { get; set; }
        public int FORM_AUDIT_TYPE_ID { get; set; }
        public REF_FORM_TYPE FromXml(XElement elem)
        {
            if (elem.Element("FORM_TYPE_ID") != null)
                FORM_TYPE_ID = Convert.ToInt32(elem.Element("FORM_TYPE_ID").Value);
            if (elem.Element("FORM_TYPE_NAME") != null)
                FORM_TYPE_NAME = elem.Element("FORM_TYPE_NAME").Value;
            if (elem.Element("FORM_AUDIT_TYPE_ID") != null)
                FORM_AUDIT_TYPE_ID = Convert.ToInt32(elem.Element("FORM_AUDIT_TYPE_ID").Value);
            return this;
        }
    }
    public class REF_PROPOSAL_TYPE
    {
        public int PROPOSAL_TYPE_ID { get; set; }
        public string PROPOSAL_TYPE_NAME { get; set; }
        public int PROPOSAL_AUDIT_TYPE_ID { get; set; }
        public REF_PROPOSAL_TYPE FromXml(XElement elem)
        {
            if (elem.Element("PROPOSAL_TYPE_ID") != null)
                PROPOSAL_TYPE_ID = Convert.ToInt32(elem.Element("PROPOSAL_TYPE_ID").Value);
            if (elem.Element("PROPOSAL_TYPE_NAME") != null)
                PROPOSAL_TYPE_NAME = elem.Element("PROPOSAL_TYPE_NAME").Value;
            if (elem.Element("PROPOSAL_AUDIT_TYPE_ID") != null)
                PROPOSAL_AUDIT_TYPE_ID = Convert.ToInt32(elem.Element("PROPOSAL_AUDIT_TYPE_ID").Value);
            return this;
        }
    }
    public class REF_BUDGET_TYPE
    {
        public int BUDGET_TYPE_ID { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }
        public int BUDGET_AUDIT_TYPE_ID { get; set; }
        public REF_BUDGET_TYPE FromXml(XElement elem)
        {
            if (elem.Element("BUDGET_TYPE_ID") != null)
                BUDGET_TYPE_ID = Convert.ToInt32(elem.Element("BUDGET_TYPE_ID").Value);
            if (elem.Element("BUDGET_TYPE_NAME") != null)
                BUDGET_TYPE_NAME = elem.Element("BUDGET_TYPE_NAME").Value;
            if (elem.Element("BUDGET_AUDIT_TYPE_ID") != null)
                BUDGET_AUDIT_TYPE_ID = Convert.ToInt32(elem.Element("BUDGET_AUDIT_TYPE_ID").Value);
            return this;
        }
    }
    public class REF_AUDIT_YEAR
    {
        public int YEAR_ID { get; set; }
        public string YEAR_LABEL { get; set; }
        public REF_AUDIT_YEAR FromXml(XElement elem)
        {
            if (elem.Element("YEAR_ID") != null)
                YEAR_ID = Convert.ToInt32(elem.Element("YEAR_ID").Value);
            if (elem.Element("YEAR_LABEL") != null)
                YEAR_LABEL = elem.Element("YEAR_LABEL").Value;
            return this;
        }
    }
    public class REF_VIOLATION_TYPE
    {
        public int VIOLATION_ID { get; set; }
        public string VIOLATION_NAME { get; set; }
        public REF_VIOLATION_TYPE FromXml(XElement elem)
        {
            if (elem.Element("VIOLATION_ID") != null)
                VIOLATION_ID = Convert.ToInt32(elem.Element("VIOLATION_ID").Value);
            if (elem.Element("VIOLATION_NAME") != null)
                VIOLATION_NAME = elem.Element("VIOLATION_NAME").Value;
            return this;
        }
    }
    public class HAK
    {
        public int DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public HAK FromXml(XElement elem)
        {
            if (elem.Element("DEPARTMENT_ID") != null)
                DEPARTMENT_ID = Convert.ToInt32(elem.Element("DEPARTMENT_ID").Value);
            if (elem.Element("DEPARTMENT_NAME") != null)
                DEPARTMENT_NAME = elem.Element("DEPARTMENT_NAME").Value;
            return this;
        }
    }
}