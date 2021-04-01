using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class OrgListRequest : DataTableAjaxPostModel
    {
        public int ORG_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string ORG_REGISTER_NO { get; set; }
        public string ORG_NAME { get; set; }
        public string ORG_CODE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }
        public string VIOLATION_DETAIL { get; set; }
        public string ORG_CONCENTRATOR_NAME { get; set; }
        public string STATUS_NAME { get; set; }
        public string INFORMATION_DETAIL { get; set; }
        public int[] status { get; set; }
        public List<string> violation { get; set; }
        public int? DeparmentID { get; set; }
    }
    public class OrgListResponse : DataTableAjaxResponModel
    {
        public List<OrgList> data { get; set; } = new List<OrgList>();
    }
    public class BM0ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
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

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM0ListResponse : DataTableAjaxResponModel
    {
        public List<BM0> data { get; set; } = new List<BM0>();
    }
    public class BM1ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
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

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM1ListResponse : DataTableAjaxResponModel
    {
        public List<BM1> data { get; set; } = new List<BM1>();
    }
    public class BM2ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
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

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM2ListResponse : DataTableAjaxResponModel
    {
        public List<BM2> data { get; set; } = new List<BM2>();
    }
    public class BM3ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
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

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM3ListResponse : DataTableAjaxResponModel
    {
        public List<BM3> data { get; set; } = new List<BM3>();
    }
    public class BM4ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
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

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM4ListResponse : DataTableAjaxResponModel
    {
        public List<BM4> data { get; set; } = new List<BM4>();
    }
    public class BM5ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
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

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM5ListResponse : DataTableAjaxResponModel
    {
        public List<BM5> data { get; set; } = new List<BM5>();
    }
    public class BM6ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
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

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM6ListResponse : DataTableAjaxResponModel
    {
        public List<BM6> data { get; set; } = new List<BM6>();
    }
    public class BM7ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
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

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM7ListResponse : DataTableAjaxResponModel
    {
        public List<BM7> data { get; set; } = new List<BM7>();
    }

    public class BM8ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public string AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }
        public string CORRECTED_ERROR_DESC { get; set; }
        public string CORRECTED_ERROR_TYPE { get; set; }
        public string CORRECTED_COUNT { get; set; }
        public string CORRECTED_AMOUNT { get; set; }
        public string EXEC_TYPE { get; set; }
        public string CREATED_DATE { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM8ListResponse : DataTableAjaxResponModel
    {
        public List<BM8> data { get; set; } = new List<BM8>();
    }
    public class DataTableAjaxResponModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public bool Status { get; set; }
        public string error { get; set; }
        public string Message { get; set; }
    }

    public class DataTableAjaxPostModel
    {
        // properties are not capital due to json mapping
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
}