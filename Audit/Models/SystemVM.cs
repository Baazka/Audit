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
        public int TAB1_IS_FINISH { get; set; }
        public int TAB2_IS_FINISH { get; set; }
        public int TAB3_IS_FINISH { get; set; }
        public int[] status { get; set; }
        public List<string> violation { get; set; }
        public int? DeparmentID { get; set; }
        public int[] budget_type { get; set; }
    }
    public class OrgListResponse : DataTableAjaxResponModel
    {
        public List<OrgList> data { get; set; } = new List<OrgList>();
    }

    public class MirrorOrgListRequest : DataTableAjaxPostModel
    {
        public int OPEN_ID { get; set; }
        public string OPEN_ENT_BUDGET_TYPE { get; set; }
        public string OPEN_ENT_BUDGET_PARENT { get; set; }
        public string OPEN_ENT_DEPARTMENT_ID { get; set; }
        public string OPEN_ENT_NAME { get; set; }
        public string OPEN_ENT_REGISTER_NO { get; set; }
        public int TAB3_IS_FINISH { get; set; }
        public int[] status { get; set; }
        public List<string> violation { get; set; }
        public int? DeparmentID { get; set; }
        public int? budget_type { get; set; }
    }
    public class MirrorOrgListResponse : DataTableAjaxResponModel
    {
        public List<MirroraccOrgList> data { get; set; } = new List<MirroraccOrgList>();
    }

    public class MirrorHakOrgListRequest : DataTableAjaxPostModel
    {
        public int OPEN_ID { get; set; }
        public string OPEN_ENT_BUDGET_TYPE { get; set; }
        public string OPEN_ENT_BUDGET_PARENT { get; set; }
        public string OPEN_ENT_DEPARTMENT_ID { get; set; }
        public string OPEN_ENT_NAME { get; set; }
        public string OPEN_ENT_REGISTER_NO { get; set; }
        public int TAB3_IS_FINISH { get; set; }
        public int[] status { get; set; }
        public List<string> violation { get; set; }
        public int? DeparmentID { get; set; }
        public int? budget_type { get; set; }
    }
    public class MirrorHakOrgListResponse : DataTableAjaxResponModel
    {
        public List<MirroraccHakOrgList> data { get; set; } = new List<MirroraccHakOrgList>();
    }

    public class BM0ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string FORM_TYPE_NAME { get; set; }
        public string PROPOSAL_TYPE_NAME { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }
        public string AUDIT_INCLUDED_ORG { get; set; }
        public int WORKING_PERSON { get; set; }
        public int WORKING_DAY { get; set; }
        public int WORKING_ADDITION_TIME { get; set; }
        public string AUDIT_DEPARTMENT { get; set; }
        public string AUDITOR_LEAD { get; set; }
        public string AUDITOR_MEMBER { get; set; }
        public string AUDITOR_ENTRY { get; set; }
        public string DEPARTMENT_SHORT_NAME { get; set; }
        public string TEAM_DEPARTMENT_NAME { get; set; }
        public int AUDIT_INCLUDED_COUNT { get; set; }
        public string YEAR_LABEL { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM0ListResponse : DataTableAjaxResponModel
    {
        public List<BM0> data { get; set; } = new List<BM0>();
    }
    public class BM1ListRequest : DataTableAjaxPostModel
    {
        public string DEPARTMENT_NAME { get; set; }
        public string PERIOD_LABEL { get; set; }
        public string YEAR_LABEL { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }

        public string ACT_NO { get; set; }
        public string ACT_VIOLATION_DESC { get; set; }
        public int ACT_VIOLATION_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }
        public string ACT_SUBMITTED_DATE { get; set; }
        public string ACT_DELIVERY_DATE { get; set; }
        public decimal ACT_AMOUNT { get; set; }
        public decimal ACT_STATE_AMOUNT { get; set; }
        public decimal ACT_LOCAL_AMOUNT { get; set; }
        public decimal ACT_ORG_AMOUNT { get; set; }
        public decimal ACT_OTHER_AMOUNT { get; set; }
        public string ACT_RCV_NAME { get; set; }
        public string ACT_RCV_ROLE { get; set; }
        public string ACT_RCV_GIVEN_NAME { get; set; }
        public string ACT_RCV_ADDRESS { get; set; }
        public string ACT_CONTROL_AUDITOR { get; set; }
        public string COMPLETION_ORDER { get; set; }
        public decimal COMPLETION_AMOUNT { get; set; }
        public decimal COMPLETION_STATE_AMOUNT { get; set; }
        public decimal COMPLETION_LOCAL_AMOUNT { get; set; }
        public decimal COMPLETION_ORG_AMOUNT { get; set; }
        public decimal COMPLETION_OTHER_AMOUNT { get; set; }
        public decimal REMOVED_AMOUNT { get; set; }
        public decimal REMOVED_LAW_AMOUNT { get; set; }
        public string REMOVED_LAW_DATE_NO { get; set; }
        public decimal REMOVED_INVALID_AMOUNT { get; set; }
        public string REMOVED_INVALID_DATE_NO { get; set; }
        public decimal ACT_C2_AMOUNT { get; set; }
        public decimal ACT_C2_NONEXPIRED { get; set; }
        public decimal ACT_C2_EXPIRED { get; set; }
        public int BENEFIT_FIN { get; set; }
        public decimal BENEFIT_FIN_AMOUNT { get; set; }
        public int BENEFIT_NONFIN { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM1ListResponse : DataTableAjaxResponModel
    {
        public List<BM1> data { get; set; } = new List<BM1>();
    }
    public class BM2ListRequest : DataTableAjaxPostModel
    {
        public string DEPARTMENT_NAME { get; set; }
        public string PERIOD_LABEL { get; set; }
        public string YEAR_LABEL { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }

        public string CLAIM_NO { get; set; }
        public string CLAIM_VIOLATION_DESC { get; set; }
        public int CLAIM_VIOLATION_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }
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
        public int BENEFIT_FIN { get; set; }
        public decimal BENEFIT_FIN_AMOUNT { get; set; }
        public int BENEFIT_NONFIN { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM2ListResponse : DataTableAjaxResponModel
    {
        public List<BM2> data { get; set; } = new List<BM2>();
    }
    public class BM3ListRequest : DataTableAjaxPostModel
    {
        public string DEPARTMENT_NAME { get; set; }
        public string PERIOD_LABEL { get; set; }
        public string YEAR_LABEL { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }

        public string REFERENCE_DESC { get; set; }
        public string VIOLATION_NAME { get; set; }
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

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM3ListResponse : DataTableAjaxResponModel
    {
        public List<BM3> data { get; set; } = new List<BM3>();
    }
    public class BM4ListRequest : DataTableAjaxPostModel
    {
        public string DEPARTMENT_NAME { get; set; }
        public string PERIOD_LABEL { get; set; }
        public string YEAR_LABEL { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }

        public string PROPOSAL_NO { get; set; }
        public string PROPOSAL_VIOLATION_DESC { get; set; }
        public string VIOLATION_RESPONDENT { get; set; }
        public string PROPOSAL_SUBMITTED_DATE { get; set; }
        public string PROPOSAL_DELIVERY_DATE { get; set; }
        public int PROPOSAL_COUNT { get; set; }
        public decimal PROPOSAL_AMOUNT { get; set; }
        public string PROPOSAL_RCV_NAME { get; set; }
        public string PROPOSAL_RCV_ROLE { get; set; }
        public string PROPOSAL_RCV_GIVEN_NAME { get; set; }
        public string PROPOSAL_RCV_ADDRESS { get; set; }
        public string PROPOSAL_CONTROL_AUDITOR { get; set; }
        public int PROPOSAL_VIOLATION_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }

        public string COMPLETION_ORDER { get; set; }
        public int COMPLETION_DONE { get; set; }
        public decimal COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS { get; set; }
        public decimal COMPLETION_PROGRESS_AMOUNT { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM4ListResponse : DataTableAjaxResponModel
    {
        public List<BM4> data { get; set; } = new List<BM4>();
    }
    public class BM5ListRequest : DataTableAjaxPostModel
    {
        public string DEPARTMENT_NAME { get; set; }
        public string PERIOD_LABEL { get; set; }
        public string YEAR_LABEL { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }

        public string LAW_RESPONDANT_NAME { get; set; }
        public string LAW_VIOLATION_DESC { get; set; }
        public int LAW_VIOLATION_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }
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

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class BM5ListResponse : DataTableAjaxResponModel
    {
        public List<BM5> data { get; set; } = new List<BM5>();
    }
    public class BM6ListRequest : DataTableAjaxPostModel
    {
        public string DEPARTMENT_NAME { get; set; }
        public string PERIOD_LABEL { get; set; }
        public string YEAR_LABEL { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }

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
    public class BM6ListResponse : DataTableAjaxResponModel
    {
        public List<BM6> data { get; set; } = new List<BM6>();
    }
    public class BM7ListRequest : DataTableAjaxPostModel
    {
        public string DEPARTMENT_NAME { get; set; }
        public string PERIOD_LABEL { get; set; }
        public string YEAR_LABEL { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }

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
    public class BM7ListResponse : DataTableAjaxResponModel
    {
        public List<BM7> data { get; set; } = new List<BM7>();
    }

    public class BM8ListRequest : DataTableAjaxPostModel
    {
        public string DEPARTMENT_NAME { get; set; }
        public string PERIOD_LABEL { get; set; }
        public string YEAR_LABEL { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }

        public string CORRECTED_ERROR_DESC { get; set; }
        public int CORRECTED_ERROR_TYPE { get; set; }
        public string VIOLATION_NAME { get; set; }
        public string CORRECTED_COUNT { get; set; }
        public string CORRECTED_AMOUNT { get; set; }

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
    public class CM1ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
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
        public decimal TPA_AMOUNT { get; set; }
        public int AUDITED_INCLUDED_ORG { get; set; }
        public int BENEFIT_FIN_COUNT { get; set; }
        public decimal BENEFIT_FIN_AMOUNT { get; set; }
        public int BENEFIT_NONFIN { get; set; }

        public int Type { get; set; }
        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class CM1ListResponse : DataTableAjaxResponModel
    {
        public List<CM1> data { get; set; } = new List<CM1>();
    }
    public class CM2ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
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

        public int Type { get; set; }
        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class CM2ListResponse : DataTableAjaxResponModel
    {
        public List<CM2> data { get; set; } = new List<CM2>();
    }
    public class CM3ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string DECISION_TYPE { get; set; }
        public string BUDGET_TYPE { get; set; }
        public int IS_STATE { get; set; }

        public int C1_COUNT { get; set; }
        public decimal C1_AMOUNT { get; set; }
        public int CURRENT_COUNT { get; set; }
        public decimal CURRENT_AMOUNT { get; set; }
        public int TOTAL_COUNT { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
        public int COMPLETION_DONE_COUNT { get; set; }
        public decimal COMPLETION_DONE_AMOUNT { get; set; }
        public int COMPLETION_PROGRESS_COUNT { get; set; }
        public decimal COMPLETION_PROGRESS_AMOUNT { get; set; }
        public int LAW_COUNT { get; set; }
        public decimal LAW_AMOUNT { get; set; }
        public int LAW_CURRENT_COUNT { get; set; }
        public decimal LAW_CURRENT_AMOUNT { get; set; }
        public int LAW_TOTAL_COUNT { get; set; }
        public decimal LAW_TOTAL_AMOUNT { get; set; }
        public int LAW_COMP_DONE_COUNT { get; set; }
        public decimal LAW_COMP_DONE_AMOUNT { get; set; }
        public int LAW_COMP_PROG_COUNT { get; set; }
        public decimal LAW_COMP_PROG_AMOUNT { get; set; }
        public int LAW_COMP_INVALID_COUNT { get; set; }
        public decimal LAW_COMP_INVALID_AMOUNT { get; set; }
        public int C2_COUNT { get; set; }
        public decimal C2_AMOUNT { get; set; }
        public int EXEC_TYPE { get; set; }

        public int Type { get; set; }
        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class CM3ListResponse : DataTableAjaxResponModel
    {
        public List<CM3> data { get; set; } = new List<CM3>();
    }
    public class CM4ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string DECISION_TYPE { get; set; }
        public string BUDGET_TYPE { get; set; }
        public int IS_STATE { get; set; }

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

        public int Type { get; set; }
        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class CM4ListResponse : DataTableAjaxResponModel
    {
        public List<CM4> data { get; set; } = new List<CM4>();
    }
    public class CM5ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public string AUDIT_TYPE { get; set; }
        public string DECISION_TYPE { get; set; }

        public int INCOME_STATE_COUNT { get; set; }
        public decimal INCOME_STATE_AMOUNT { get; set; }
        public int INCOME_LOCAL_COUNT { get; set; }
        public decimal INCOME_LOCAL_AMOUNT { get; set; }

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
    public class CM5ListResponse : DataTableAjaxResponModel
    {
        public List<CM5> data { get; set; } = new List<CM5>();
    }
    public class CM6ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public string AUD_NAME { get; set; }
        public int IS_STATE { get; set; }

        public int ALL_COUNT { get; set; }
        public decimal ALL_AMOUNT { get; set; }
        public int PROCESSED_INCOMED_COUNT { get; set; }
        public decimal PROCESSED_INCOMED_AMOUNT { get; set; }
        public int PROCESSED_COSTS_COUNT { get; set; }
        public decimal PROCESSED_COSTS_AMOUNT { get; set; }

        public int ALL_C1_COUNT { get; set; }
        public decimal ALL_C2_AMOUNT { get; set; }
        public int ACCEPTED_INCOMED_COUNT { get; set; }
        public decimal ACCEPTED_INCOMED_AMOUNT { get; set; }
        public int ACCEPTED_COSTS_COUNT { get; set; }
        public decimal ACCEPTED_COSTS_AMOUNT { get; set; }

        public int EXEC_TYPE { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class CM6ListResponse : DataTableAjaxResponModel
    {
        public List<CM6> data { get; set; } = new List<CM6>();
    }
    public class CM7ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
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
        public int EXEC_TYPE { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class CM7ListResponse : DataTableAjaxResponModel
    {
        public List<CM7> data { get; set; } = new List<CM7>();
    }

    public class CM8ListRequest : DataTableAjaxPostModel
    {
        public int ID { get; set; }
        public int APPROVED_BUDGET { get; set; }
        public int PERFORMANCE_BUDGET { get; set; }
        public int WORKERS { get; set; }
        public int APPROVED_NUMBERS { get; set; }
        public int DIRECTING_STAFF { get; set; }
        public int SENIOR_AUDITOR_ANALYST { get; set; }
        public int AUDITOR_ANALYST { get; set; }
        public int OTHER_OFFICE { get; set; }
        public int EDU_DOCTOR { get; set; }
        public int EDU_MAGISTR { get; set; }
        public int EDU_BAKLAVR { get; set; }
        public int EDU_AMONGST { get; set; }
        public int EDU_JUNIOR_AMONGST { get; set; }
        public int PRO_ACCOUNTANT { get; set; }
        public int ACCOUNTANT_ECONOMIST { get; set; }
        public int LAWYER { get; set; }
        public int INGENER { get; set; }
        public int OTHER_PROF { get; set; }
        public int STUDY_COUNT { get; set; }
        public int INCLUDED_MAN { get; set; }
        public int ONLINE_STUDY_COUNT { get; set; }
        public int LOCAL_STUDY_COUNT { get; set; }
        public int AUDIT_STUDY_COUNT { get; set; }
        public int FOREIGN_STUDY_COUNT { get; set; }
        public int FOREIGN_MAN_COUNT { get; set; }
        public int INSIDE_STUDY_COUNT { get; set; }
        public int INSIDE_MAN_COUNT { get; set; }
        public int ORG_STUDY_COUNT { get; set; }
        public int ORG_MAN_COUNT { get; set; }
        public int RESEARCH_ALL { get; set; }
        public int PUBLISHED_REPORT { get; set; }
        public int NEWS_ARTICLE { get; set; }
        public int TV_NEWS_BROADCAST { get; set; }
        public int ORG_NEWS { get; set; }
        public int WEB_ACCESS { get; set; }
        public int RECEIVED_ALL { get; set; }
        public int TAB_WORKERS { get; set; }
        public int TAB_SKILLS { get; set; }
        public int AUDIT_LET { get; set; }
        public int RECEIVED_OTHER { get; set; }
        public int DECIDED_TIME { get; set; }
        public int DEC_EXPIRED { get; set; }
        public int DEC_UNEXPIRED { get; set; }
        public string EXEC_TYPE { get; set; }

        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

    }
    public class CM8ListResponse : DataTableAjaxResponModel
    {
        public List<CM8> data { get; set; } = new List<CM8>();
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
    public class N1ListRequest : DataTableAjaxPostModel
    {
        public string ORGID { get; set; }
        //[Required(ErrorMessage = "N1 сонгоно уу.")]
        public string INSERTUSERID { get; set; }
        public string ORGNAME { get; set; }
        public string ORGTYPE { get; set; }
        public string OPEN_HEAD_ROLE { get; set; }
        public string OPEN_HEAD_NAME { get; set; }
        public int OPEN_HEAD_PHONE { get; set; }
        public string OPEN_ACC_ROLE { get; set; }
        public string OPEN_ACC_NAME { get; set; }
        public Decimal OPEN_ACC_PHONE { get; set; }

        public int TypeID { get; set; }


        public int MD1 { get; set; }
        public int MD2 { get; set; }
        public int MD3 { get; set; }
        public int MD4 { get; set; }
        public int MD5 { get; set; }
        public int MD6 { get; set; }
        public int MD7 { get; set; }
        public int MD8 { get; set; }
        public int MD9 { get; set; }
        public int MD10 { get; set; }
        public int MD11 { get; set; }
        public int MD12 { get; set; }
        public int MD13 { get; set; }
        public int MD14 { get; set; }
        public int MD15 { get; set; }
        public int MD16 { get; set; }
        public int MD17 { get; set; }
        public int MD18 { get; set; }
        public int MD19 { get; set; }
        public int MD20 { get; set; }
        public int MD21 { get; set; }
        public int MD22 { get; set; }
        public int MD23 { get; set; }
        public int MD24 { get; set; }
        public int MD25 { get; set; }
        public int MD26 { get; set; }
        public int MD27 { get; set; }
        public int MD28 { get; set; }
        public int MD29 { get; set; }
        public int MD30 { get; set; }
        public int MD31 { get; set; }
        public int MD32 { get; set; }
        public int MD33 { get; set; }
        public int MD34 { get; set; }
        public int MD35 { get; set; }

        public string MD36 { get; set; }
        public string MD37 { get; set; }
        public string MD38 { get; set; }
        public string MD39 { get; set; }
        public string MD40 { get; set; }
        public string MD41 { get; set; }
        public string MD42 { get; set; }
        public string MD43 { get; set; }
        public string MD44 { get; set; }
        public string MD45 { get; set; }
        public string MD46 { get; set; }
        public string MD47 { get; set; }
        public string MD48 { get; set; }
        public string MD49 { get; set; }
        public string MD50 { get; set; }
        public string MD51 { get; set; }
        public string MD52 { get; set; }
        public string MD53 { get; set; }
        public string MD54 { get; set; }
        public string MD55 { get; set; }
        public string MD56 { get; set; }
        public string MD57 { get; set; }
        public string MD58 { get; set; }
        public string MD59 { get; set; }
        public string MD60 { get; set; }
        public string MD61 { get; set; }
        public string MD62 { get; set; }
        public string MD63 { get; set; }
        public string MD64 { get; set; }
        public string MD65 { get; set; }
        public string MD66 { get; set; }
        public string MD67 { get; set; }
        public string MD68 { get; set; }
        public string MD69 { get; set; }
        public string MD70 { get; set; }
        public string MD71 { get; set; }
        public string MD72 { get; set; }
        public string MD73 { get; set; }
        public string MD74 { get; set; }
        public string MD75 { get; set; }
        public string MD76 { get; set; }
        public string MD77 { get; set; }
        public string MD78 { get; set; }
        public string MD79 { get; set; }
        public string MD80 { get; set; }
        public string MD81 { get; set; }
        public string MD82 { get; set; }
        public string MD83 { get; set; }
        public string MD84 { get; set; }
        public string MD85 { get; set; }
        public string MD86 { get; set; }
        public string MD87 { get; set; }
        public string MD88 { get; set; }
        public string MD89 { get; set; }
        public string MD90 { get; set; }
        public string MD91 { get; set; }
        public string MD92 { get; set; }
        public string MD93 { get; set; }
        public string MD94 { get; set; }
        public string MD95 { get; set; }
        public string MD96 { get; set; }
        public string MD97 { get; set; }
        public string MD98 { get; set; }
        public string MD99 { get; set; }
        public string MD100 { get; set; }
        public string MD101 { get; set; }
        public string MD102 { get; set; }
        public string MD103 { get; set; }
        public string MD104 { get; set; }
        public string MD105 { get; set; }
        public string MD106 { get; set; }
        public string MD158 { get; set; }
        public string MD159 { get; set; }
        public string MD160 { get; set; }
        public string MD161 { get; set; }
        public string MD162 { get; set; }
        public string MD163 { get; set; }
        public string MD164 { get; set; }
        public string MD165 { get; set; }
        public string MD166 { get; set; }
        public string MD167 { get; set; }
        public string MD168 { get; set; }
        public string MD169 { get; set; }
        public int? DeparmentID { get; set; }
        public int? PeriodID { get; set; }

        public string Mayagt { get; set; }
    }
    public class N1ListResponse : DataTableAjaxResponModel
    {
        public List<N1> data { get; set; } = new List<N1>();
    }
   
    
}