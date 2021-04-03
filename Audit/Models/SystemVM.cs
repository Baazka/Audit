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
        public int[] budget_type { get; set; }
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
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }
        public string ORDER_DATE { get; set; }
        public string ORDER_NO { get; set; }
        public string ACT_NO { get; set; }
        public string ACT_VIOLATION_DESC { get; set; }
        public string ACT_VIOLATION_TYPE { get; set; }
        public string ACT_SUBMITTED_DATE { get; set; }
        public string ACT_DELIVERY_DATE { get; set; }
        public int ACT_AMOUNT { get; set; }
        public int ACT_STATE_AMOUNT { get; set; }
        public int ACT_LOCAL_AMOUNT { get; set; }
        public int ACT_ORG_AMOUNT { get; set; }
        public int ACT_OTHER_AMOUNT { get; set; }
        public string ACT_RCV_NAME { get; set; }
        public string ACT_RCV_ROLE { get; set; }
        public string ACT_RCV_GIVEN_NAME { get; set; }
        public string ACT_RCV_ADDRESS { get; set; }
        public string ACT_CONTROL_AUDITOR { get; set; }
        public string COMPLETION_ORDER { get; set; }
        public int COMPLETION_AMOUNT { get; set; }
        public int COMPLETION_STATE_AMOUNT { get; set; }
        public int COMPLETION_LOCAL_AMOUNT { get; set; }
        public int COMPLETION_ORG_AMOUNT { get; set; }
        public int COMPLETION_OTHER_AMOUNT { get; set; }
        public int REMOVED_AMOUNT { get; set; }
        public int REMOVED_LAW_AMOUNT { get; set; }
        public string REMOVED_LAW_DATE_NO { get; set; }
        public int REMOVED_INVALID_AMOUNT { get; set; }
        public string REMOVED_INVALID_DATE_NO { get; set; }
        public int ACT_C2_AMOUNT { get; set; }
        public int ACT_C2_NONEXPIRED { get; set; }
        public int ACT_C2_EXPIRED { get; set; }
        public int BENEFIT_FIN { get; set; }
        public int BENEFIT_FIN_AMOUNT { get; set; }
        public int BENEFIT_NONFIN { get; set; }
        public int EXEC_TYPE { get; set; }

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
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }
        public string ORDER_DATE { get; set; }
        public string ORDER_NO { get; set; }

        public string CLAIM_NO { get; set; }
        public string CLAIM_VIOLATION_DESC { get; set; }
        public string CLAIM_VIOLATION_TYPE { get; set; }
        public string CLAIM_SUBMITTED_DATE { get; set; }
        public string CLAIM_DELIVERY_DATE { get; set; }
        public decimal CLAIM_VIOLATION_AMOUNT { get; set; }
        public string CLAIM_RCV_NAME { get; set; }
        public string CLAIM_RCV_ROLE { get; set; }
        public string CLAIM_RCV_GIVEN_NAME { get; set; }
        public string CLAIM_RCV_ADDRESS { get; set; }
        public string CLAIM_CONTROL_AUDITOR { get; set; }
        public string COMPLETION_ORDER { get; set; }
        public decimal COMPLETION_AMOUNT { get; set; }
        public decimal COMPLETION_STATE_AMOUNT { get; set; }
        public decimal COMPLETION_LOCAL_AMOUNT { get; set; }
        public decimal COMPLETION_ORG_AMOUNT { get; set; }
        public decimal COMPLETION_OTHER_AMOUNT { get; set; }
        public decimal REMOVED_LAW_AMOUNT { get; set; }
        public string REMOVED_LAW_DATE { get; set; }
        public string REMOVED_LAW_NO { get; set; }
        public decimal REMOVED_INVALID_AMOUNT { get; set; }
        public string REMOVED_INVALID_DATE { get; set; }
        public string REMOVED_INVALID_NO { get; set; }
        public decimal CLAIM_C2_AMOUNT { get; set; }
        public decimal CLAIM_C2_NONEXPIRED { get; set; }
        public decimal CLAIM_C2_EXPIRED { get; set; }
        public decimal BENEFIT_FIN { get; set; }
        public decimal BENEFIT_FIN_AMOUNT { get; set; }
        public decimal BENEFIT_NONFIN { get; set; }
        public int EXEC_TYPE { get; set; }

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
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }
        public string ORDER_DATE { get; set; }
        public string ORDER_NO { get; set; }

        public string REFERENCE_DESC { get; set; }
        public decimal REFERENCE_AMOUNT { get; set; }
        public string REFERENCE_SUBMITTED_DATE { get; set; }
        public string REFERENCE_DELIVERY_DATE { get; set; }
        public string REFERENCE_RCV_NAME { get; set; }
        public string REFERENCE_RCV_ROLE { get; set; }
        public string REFERENCE_RCV_GIVEN_NAME { get; set; }
        public string REFERENCE_RCV_ADDRESS { get; set; }
        public string REFERENCE_CONTROL_AUDITOR { get; set; }

        public string COMPLETION_ORDER { get; set; }
        public decimal COMPLETION_DONE { get; set; }
        public decimal COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS { get; set; }
        public decimal COMPLETION_PROGRESS_AMOUNT { get; set; }
        public int C2_NONEXPIRED { get; set; }
        public decimal C2_NONEXPIRED_AMOUNT { get; set; }
        public int C2_EXPIRED { get; set; }
        public decimal C2_EXPIRED_AMOUNT { get; set; }

        public int BENEFIT_FIN { get; set; }
        public decimal BENEFIT_FIN_AMOUNT { get; set; }
        public int BENEFIT_NONFIN { get; set; }
        public int WORKING_PERSON { get; set; }
        public int WORKING_DAY { get; set; }
        public int WORKING_ADDITION_TIME { get; set; }
        public int EXEC_TYPE { get; set; }

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
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }
        public string ORDER_DATE { get; set; }
        public string ORDER_NO { get; set; }

        public string PROPOSAL_NO { get; set; }
        public string PROPOSAL_VIOLATION_DESC { get; set; }
        public string VIOLATION_RESPONDENT { get; set; }
        public string PROPOSAL_SUBMITTED_DATE { get; set; }
        public string PROPOSAL_DELIVERY_DATE { get; set; }
        public decimal PROPOSAL_AMOUNT { get; set; }
        public string PROPOSAL_RCV_NAME { get; set; }
        public string PROPOSAL_RCV_ROLE { get; set; }
        public string PROPOSAL_RCV_GIVEN_NAME { get; set; }
        public string PROPOSAL_RCV_ADDRESS { get; set; }
        public string PROPOSAL_CONTROL_AUDITOR { get; set; }
        public string PROPOSAL_VIOLATION_TYPE { get; set; }

        public string COMPLETION_ORDER { get; set; }
        public int COMPLETION_DONE { get; set; }
        public decimal COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS { get; set; }
        public decimal COMPLETION_PROGRESS_AMOUNT { get; set; }

        public int EXEC_TYPE { get; set; }

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
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }
        public string ORDER_DATE { get; set; }
        public string ORDER_NO { get; set; }

        public string LAW_RESPONDANT_NAME { get; set; }
        public string LAW_VIOLATION_DESC { get; set; }
        public string LAW_VIOLATION_TYPE { get; set; }
        public string LAW_MOVING_INFORMATION { get; set; }
        public int LAW_NUMBER { get; set; }
        public decimal LAW_AMOUNT { get; set; }
        public int LAW_C2_NUMBER { get; set; }
        public decimal LAW_C2_AMOUNT { get; set; }

        public int COMPLETION_DONE { get; set; }
        public decimal COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS { get; set; }
        public decimal COMPLETION_PROGRESS_AMOUNT { get; set; }
        public int COMPLETION_INVALID { get; set; }
        public decimal COMPLETION_INVALID_AMOUNT { get; set; }

        public int EXEC_TYPE { get; set; }

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
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }

        public int VIOLATION_COUNT { get; set; }
        public decimal VIOLATION_AMOUNT { get; set; }
        public int ERROR_COUNT { get; set; }
        public decimal ERROR_AMOUNT { get; set; }
        public int ALL_COUNT { get; set; }
        public decimal ALL_AMOUNT { get; set; }
        public int CORRECTED_ERROR_COUNT { get; set; }
        public decimal CORRECTED_ERROR_AMOUNT { get; set; }
        public int OTHER_ERROR_COUNT { get; set; }
        public decimal OTHER_ERROR_AMOUNT { get; set; }
        public int ACT_COUNT { get; set; }
        public decimal ACT_AMOUNT { get; set; }
        public int CLAIM_COUNT { get; set; }
        public decimal CLAIM_AMOUNT { get; set; }
        public int REFERENCE_COUNT { get; set; }
        public decimal REFERENCE_AMOUNT { get; set; }
        public int PROPOSAL_COUNT { get; set; }
        public decimal PROPOSAL_AMOUNT { get; set; }
        public int LAW_COUNT { get; set; }
        public decimal LAW_AMOUNT { get; set; }
        public int OTHER_COUNT { get; set; }
        public decimal OTHER_AMOUNT { get; set; }

        public int EXEC_TYPE { get; set; }

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
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }

        public int INCOME_STATE_COUNT { get; set; }
        public decimal INCOME_STATE_AMOUNT { get; set; }
        public int INCOME_LOCAL_COUNT { get; set; }
        public int INCOME_LOCAL_NUMBER { get; set; }
        public int BUDGET_STATE_COUNT { get; set; }
        public decimal BUDGET_STATE_AMOUNT { get; set; }
        public int BUDGET_LOCAL_COUNT { get; set; }
        public decimal BUDGET_LOCAL_AMOUNT { get; set; }
        public int ACCOUNTANT_COUNT { get; set; }
        public decimal ACCOUNTANT_AMOUNT { get; set; }
        public int EFFICIENCY_COUNT { get; set; }
        public decimal EFFICIENCY_AMOUNT { get; set; }
        public int LAW_COUNT { get; set; }
        public decimal LAW_AMOUNT { get; set; }
        public int MONITORING_COUNT { get; set; }
        public decimal MONITORING_AMOUNT { get; set; }
        public int PURCHASE_COUNT { get; set; }
        public decimal PURCHASE_AMOUNT { get; set; }
        public int COST_COUNT { get; set; }
        public decimal COST_AMOUNT { get; set; }
        public int OTHER_COUNT { get; set; }
        public decimal OTHER_AMOUNT { get; set; }
        public int ALL_COUNT { get; set; }
        public decimal ALL_AMOUNT { get; set; }

        public int EXEC_TYPE { get; set; }

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
    public class NM1ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }
        public int ACT_COUNT { get; set; }
        public decimal ACT_AMOUNT { get; set; }
        public int COMPLETION_COUNT { get; set; }
        public decimal COMPLETION_AMOUNT { get; set; }
        public int COMPLETION_STATE_COUNT { get; set; }
        public decimal COMPLETION_STATE_AMOUNT { get; set; }
        public int COMPLETION_LOCAL_COUNT { get; set; }
        public decimal COMPLETION_LOCAL_AMOUNT { get; set; }
        public int COMPLETION_ORG_COUNT { get; set; }
        public decimal COMPLETION_ORG_AMOUNT { get; set; }
        public int COMPLETION_OTHER_COUNT { get; set; }
        public decimal COMPLETION_OTHER_AMOUNT { get; set; }
        public int REMOVED_COUNT { get; set; }
        public decimal REMOVED_AMOUNT { get; set; }
        public int REMOVED_LAW_COUNT { get; set; }
        public decimal REMOVED_LAW_AMOUNT { get; set; }
        public int REMOVED_INVALID_COUNT { get; set; }
        public decimal REMOVED_INVALID_AMOUNT { get; set; }
        public int ACT_C2_COUNT { get; set; }
        public decimal ACT_C2_AMOUNT { get; set; }
        public int ACT_NONEXPIRED_COUNT { get; set; }
        public decimal ACT_NONEXPIRED_AMOUNT { get; set; }
        public int ACT_EXPIRED_COUNT { get; set; }
        public decimal ACT_EXPIRED_AMOUNT { get; set; }
        public int BENEFIT_FIN { get; set; }
        public decimal BENEFIT_FIN_AMOUNT { get; set; }
        public int BENEFIT_NONFIN { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class NM1ListResponse : DataTableAjaxResponModel
    {
        public List<NM1> data { get; set; } = new List<NM1>();
    }
    public class NM2ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }

        public int CLAIM_VIOLATION_COUNT { get; set; }
        public decimal CLAIM_VIOLATION_AMOUNT { get; set; }
        public int COMPLETION_COUNT { get; set; }
        public decimal COMPLETION_AMOUNT { get; set; }
        public int COMPLETION_STATE_COUNT { get; set; }
        public decimal COMPLETION_STATE_AMOUNT { get; set; }
        public int COMPLETION_LOCAL_COUNT { get; set; }
        public decimal COMPLETION_LOCAL_AMOUNT { get; set; }
        public int COMPLETION_ORG_COUNT { get; set; }
        public decimal COMPLETION_ORG_AMOUNT { get; set; }
        public int COMPLETION_OTHER_COUNT { get; set; }
        public decimal COMPLETION_OTHER_AMOUNT { get; set; }

        public int REMOVED_COUNT { get; set; }
        public decimal REMOVED_AMOUNT { get; set; }
        public int REMOVED_LAW_COUNT { get; set; }
        public decimal REMOVED_LAW_AMOUNT { get; set; }
        public int REMOVED_INVALID_COUNT { get; set; }
        public decimal REMOVED_INVALID_AMOUNT { get; set; }

        public int CLAIM_C2_COUNT { get; set; }
        public decimal CLAIM_C2_AMOUNT { get; set; }
        public int CLAIM_C2_NONEXPIRED_COUNT { get; set; }
        public decimal CLAIM_C2_NONEXPIRED_AMOUNT { get; set; }
        public int CLAIM_C2_EXPIRED_COUNT { get; set; }
        public decimal CLAIM_C2_EXPIRED_AMOUNT { get; set; }

        public int BENEFIT_FIN_COUNT { get; set; }
        public decimal BENEFIT_FIN_AMOUNT { get; set; }
        public int BENEFIT_NONFIN_COUNT { get; set; }
        public decimal BENEFIT_NONFIN_AMOUNT { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class NM2ListResponse : DataTableAjaxResponModel
    {
        public List<NM2> data { get; set; } = new List<NM2>();
    }
    public class NM3ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string STATISTIC_PERIOD { get; set; }
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

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class NM3ListResponse : DataTableAjaxResponModel
    {
        public List<NM3> data { get; set; } = new List<NM3>();
    }
    public class NM4ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }

        public int PROPOSAL_COUNT { get; set; }
        public decimal PROPOSAL_AMOUNT { get; set; }
        public int PROPOSAL_VIOLATION_TYPE { get; set; }
        public int COMPLETION_DONE_COUNT { get; set; }
        public decimal COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS_COUNT { get; set; }
        public decimal COMPLETION_PROGRESS_AMOUNT { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class NM4ListResponse : DataTableAjaxResponModel
    {
        public List<NM4> data { get; set; } = new List<NM4>();
    }
    public class NM5ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string AUDIT_BUDGET_TYPE { get; set; }

        public int LAW_COUNT { get; set; }
        public decimal LAW_AMOUNT { get; set; }
        public int COMPLETION_DONE_COUNT { get; set; }
        public decimal COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS_COUNT { get; set; }
        public decimal COMPLETION_PROGRESS_AMOUNT { get; set; }
        public int COMPLETION_INVALID_COUNT { get; set; }
        public decimal COMPLETION_INVALID_AMOUNT { get; set; }
        public int LAW_C2_NUMBER_COUNT { get; set; }
        public decimal LAW_C2_AMOUNT { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class NM5ListResponse : DataTableAjaxResponModel
    {
        public List<NM5> data { get; set; } = new List<NM5>();
    }
    public class NM6ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }

        public int VIOLATION_COUNT { get; set; }
        public decimal VIOLATION_AMOUNT { get; set; }
        public int ERROR_COUNT { get; set; }
        public decimal ERROR_AMOUNT { get; set; }
        public int ALL_COUNT { get; set; }
        public decimal ALL_AMOUNT { get; set; }
        public int CORRECTED_ERROR_COUNT { get; set; }
        public decimal CORRECTED_ERROR_AMOUNT { get; set; }
        public int OTHER_ERROR_COUNT { get; set; }
        public decimal OTHER_ERROR_AMOUNT { get; set; }
        public int ACT_COUNT { get; set; }
        public decimal ACT_AMOUNT { get; set; }
        public int CLAIM_COUNT { get; set; }
        public decimal CLAIM_AMOUNT { get; set; }
        public int REFERENCE_COUNT { get; set; }
        public decimal REFERENCE_AMOUNT { get; set; }
        public int PROPOSAL_COUNT { get; set; }
        public decimal PROPOSAL_AMOUNT { get; set; }
        public int LAW_COUNT { get; set; }
        public decimal LAW_AMOUNT { get; set; }
        public int OTHER_COUNT { get; set; }
        public decimal OTHER_AMOUNT { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class NM6ListResponse : DataTableAjaxResponModel
    {
        public List<NM6> data { get; set; } = new List<NM6>();
    }
    public class NM7ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        public int AUDIT_YEAR { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string AUDIT_CODE { get; set; }
        public string AUDIT_NAME { get; set; }
        public string DECISION_TYPE { get; set; }

        public int INCOME_STATE_COUNT { get; set; }
        public decimal INCOME_STATE_AMOUNT { get; set; }
        public int INCOME_LOCAL_COUNT { get; set; }
        public int INCOME_LOCAL_NUMBER { get; set; }
        public int BUDGET_STATE_COUNT { get; set; }
        public decimal BUDGET_STATE_AMOUNT { get; set; }
        public int BUDGET_LOCAL_COUNT { get; set; }
        public decimal BUDGET_LOCAL_AMOUNT { get; set; }
        public int ACCOUNTANT_COUNT { get; set; }
        public decimal ACCOUNTANT_AMOUNT { get; set; }
        public int EFFICIENCY_COUNT { get; set; }
        public decimal EFFICIENCY_AMOUNT { get; set; }
        public int LAW_COUNT { get; set; }
        public decimal LAW_AMOUNT { get; set; }
        public int MONITORING_COUNT { get; set; }
        public decimal MONITORING_AMOUNT { get; set; }
        public int PURCHASE_COUNT { get; set; }
        public decimal PURCHASE_AMOUNT { get; set; }
        public int COST_COUNT { get; set; }
        public decimal COST_AMOUNT { get; set; }
        public int OTHER_COUNT { get; set; }
        public decimal OTHER_AMOUNT { get; set; }
        public int ALL_COUNT { get; set; }
        public decimal ALL_AMOUNT { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class NM7ListResponse : DataTableAjaxResponModel
    {
        public List<NM7> data { get; set; } = new List<NM7>();
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