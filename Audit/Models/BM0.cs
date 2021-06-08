using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Төрийн аудитын байгууллага сонгоно уу.")]
        public int DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        [Required(ErrorMessage = "Аудитын төрөл сонгоно уу.")]
        public int AUDIT_TYPE { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        [Required(ErrorMessage = "Cонгоно уу.")]
        public int? TOPIC_TYPE { get; set; }
        
        public string TOPIC_TYPE_NAME { get; set; }
        [Required(ErrorMessage = "Сэдвийн код оруулна уу.")]
        public string TOPIC_CODE { get; set; }
        [Required(ErrorMessage = "Сэдвийн нэр оруулна уу.")]
        public string TOPIC_NAME { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORDER_NO { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORDER_DATE { get; set; }
        public int? AUDIT_FORM_TYPE { get; set; }
        public string FORM_TYPE_NAME { get; set; }
        public int? AUDIT_PROPOSAL_TYPE { get; set; }
        public string PROPOSAL_TYPE_NAME { get; set; }
        public int? AUDIT_BUDGET_TYPE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string AUDIT_INCLUDED_ORG { get; set; }
        
        public int? WORKING_PERSON { get; set; }
        public int? WORKING_DAY { get; set; }
        public int? WORKING_ADDITION_TIME { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        //public string AUDIT_DEPARTMENT { get; set; }
        public string[] AUDITOR_LEADS { get; set; }
        [Required(ErrorMessage = "Багийн ахлах оруулна уу.")]
        public string AUDITOR_LEAD { get; set; }
        public string AUDITOR_LEAD_EDIT { get; set; }
        public string[] AUDITOR_MEMBERS { get; set; }
        [Required(ErrorMessage = "Багийн гишүүн оруулна уу.")]
        public string AUDITOR_MEMBER { get; set; }
        public string AUDITOR_MEMBER_EDIT { get; set; }
       // [Required(ErrorMessage = "Утга оруулна уу.")]
        public string AUDITOR_ENTRY { get; set; }
        public int EXEC_TYPE { get; set; }
        public int IS_ACTIVE { get; set; } = 1;
        public string AUDIT_SERVICE_PAY { get; set; }
        public string CREATED_DATE { get; set; } = DateTime.Now.ToString("dd-MMM-yy");
        public DateTime? UPDATED_DATE { get; set; }

        public string DEPARTMENT_SHORT_NAME { get; set; }
        public string TEAM_DEPARTMENT_NAME { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public int? AUDIT_INCLUDED_COUNT { get; set; }
        public int YEAR_LABEL { get; set; }
        public int AUDIT_YEAR { get; set; }
        [Required(ErrorMessage = "Багийн төрөл сонгоно уу.")]
        public int AUDIT_DEPARTMENT_TYPE  { get; set; }
        [Required(ErrorMessage = "Гүйцэтгэгч газар сонгоно уу.")]
        public int AUDIT_DEPARTMENT_ID { get; set; }

        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
        public List<REF_AUDIT_YEAR> refaudityears { get; set; } = new List<REF_AUDIT_YEAR>();

        public List<REF_AUDIT_TYPE> audittypes { get; set; } = new List<REF_AUDIT_TYPE>();
        public List<REF_TOPIC_TYPE> topictypes { get; set; } = new List<REF_TOPIC_TYPE>();
        public List<REF_FORM_TYPE> formtypes { get; set; } = new List<REF_FORM_TYPE>();
        public List<REF_PROPOSAL_TYPE> proposaltypes { get; set; } = new List<REF_PROPOSAL_TYPE>();
        public List<REF_BUDGET_TYPE> refbudgettypes { get; set; } = new List<REF_BUDGET_TYPE>();
        public List<HAK> haks { get; set; } = new List<HAK>();

        public BM0 SetXml(XElement xml)
        {
            if (xml != null)
            {
                if (xml.Element("ID") != null)
                    ID = Convert.ToInt32(xml.Element("ID").Value);
                if (xml.Element("DEPARTMENT_ID") != null)
                    DEPARTMENT_ID = Convert.ToInt32(xml.Element("DEPARTMENT_ID").Value);
                if (xml.Element("DEPARTMENT_NAME") != null)
                    DEPARTMENT_NAME = xml.Element("DEPARTMENT_NAME").Value;
                if (xml.Element("STATISTIC_PERIOD") != null)
                    STATISTIC_PERIOD = Convert.ToInt32(xml.Element("STATISTIC_PERIOD").Value);
                if (xml.Element("PERIOD_LABEL") != null)
                    PERIOD_LABEL = xml.Element("PERIOD_LABEL").Value; 
                if (xml.Element("AUDIT_TYPE") != null)
                    AUDIT_TYPE = Convert.ToInt32(xml.Element("AUDIT_TYPE").Value);
                if (xml.Element("AUDIT_TYPE_NAME") != null)
                    AUDIT_TYPE_NAME = xml.Element("AUDIT_TYPE_NAME").Value;
                if (xml.Element("TOPIC_TYPE") != null)
                    TOPIC_TYPE = Convert.ToInt32(xml.Element("TOPIC_TYPE").Value);
                if (xml.Element("TOPIC_TYPE_NAME") != null)
                    TOPIC_TYPE_NAME = xml.Element("TOPIC_TYPE_NAME").Value;
                if (xml.Element("TOPIC_CODE") != null)
                    TOPIC_CODE = xml.Element("TOPIC_CODE").Value;
                if (xml.Element("TOPIC_NAME") != null)
                    TOPIC_NAME = xml.Element("TOPIC_NAME").Value;
                if (xml.Element("ORDER_NO") != null)
                    ORDER_NO = xml.Element("ORDER_NO").Value;
                if (xml.Element("ORDER_DATE") != null)
                    ORDER_DATE = Convert.ToDateTime(xml.Element("ORDER_DATE").Value).ToString("yyyy.MM.dd");
                if (xml.Element("AUDIT_FORM_TYPE") != null)
                    AUDIT_FORM_TYPE = Convert.ToInt32(xml.Element("AUDIT_FORM_TYPE").Value);
                if (xml.Element("FORM_TYPE_NAME") != null)
                    FORM_TYPE_NAME = xml.Element("FORM_TYPE_NAME").Value;
                if (xml.Element("AUDIT_PROPOSAL_TYPE") != null)
                    AUDIT_PROPOSAL_TYPE = Convert.ToInt32(xml.Element("AUDIT_PROPOSAL_TYPE").Value);
                if (xml.Element("PROPOSAL_TYPE_NAME") != null)
                    PROPOSAL_TYPE_NAME = xml.Element("PROPOSAL_TYPE_NAME").Value;
                if (xml.Element("AUDIT_BUDGET_TYPE") != null)
                    AUDIT_BUDGET_TYPE = Convert.ToInt32(xml.Element("AUDIT_BUDGET_TYPE").Value);
                if (xml.Element("BUDGET_TYPE_NAME") != null)
                    BUDGET_TYPE_NAME = xml.Element("BUDGET_TYPE_NAME").Value;
                if (xml.Element("AUDIT_INCLUDED_ORG") != null)
                    AUDIT_INCLUDED_ORG = xml.Element("AUDIT_INCLUDED_ORG").Value;
                if (xml.Element("WORKING_PERSON") != null)
                    WORKING_PERSON = Convert.ToInt32(xml.Element("WORKING_PERSON").Value);
                if (xml.Element("WORKING_DAY") != null)
                    WORKING_DAY = Convert.ToInt32(xml.Element("WORKING_DAY").Value);
                if (xml.Element("WORKING_ADDITION_TIME") != null)
                    WORKING_ADDITION_TIME = Convert.ToInt32(xml.Element("WORKING_ADDITION_TIME").Value);
                //if (xml.Element("AUDIT_DEPARTMENT") != null)
                //    AUDIT_DEPARTMENT = xml.Element("AUDIT_DEPARTMENT").Value;
                if (xml.Element("AUDITOR_LEAD") != null)
                    AUDITOR_LEAD = xml.Element("AUDITOR_LEAD").Value.Replace(",","<br/>");
                if (xml.Element("AUDITOR_MEMBER") != null)
                    AUDITOR_MEMBER = xml.Element("AUDITOR_MEMBER").Value.Replace(",", "<br/>");

                if (xml.Element("AUDITOR_LEAD_EDIT") != null)
                    AUDITOR_LEAD_EDIT = xml.Element("AUDITOR_LEAD_EDIT").Value.Replace(",", "\n");
                if (xml.Element("AUDITOR_MEMBER_EDIT") != null)
                    AUDITOR_MEMBER_EDIT = xml.Element("AUDITOR_MEMBER_EDIT").Value.Replace(",", "\n");

                if (xml.Element("AUDITOR_ENTRY") != null)
                    AUDITOR_ENTRY = xml.Element("AUDITOR_ENTRY").Value;
                if (xml.Element("AUDIT_SERVICE_PAY") != null)
                    AUDIT_SERVICE_PAY = xml.Element("AUDIT_SERVICE_PAY").Value;
                if (xml.Element("AUDIT_INCLUDED_COUNT") != null)
                    AUDIT_INCLUDED_COUNT = Convert.ToInt32(xml.Element("AUDIT_INCLUDED_COUNT").Value);
                if (xml.Element("DEPARTMENT_SHORT_NAME") != null) 
                    DEPARTMENT_SHORT_NAME = xml.Element("DEPARTMENT_SHORT_NAME").Value;
                if (xml.Element("TEAM_DEPARTMENT_NAME") != null)
                    TEAM_DEPARTMENT_NAME = xml.Element("TEAM_DEPARTMENT_NAME").Value;
                if (xml.Element("YEAR_LABEL") != null)
                    YEAR_LABEL = Convert.ToInt32(xml.Element("YEAR_LABEL").Value);
                if (xml.Element("AUDIT_DEPARTMENT_TYPE") != null)
                    AUDIT_DEPARTMENT_TYPE = Convert.ToInt32(xml.Element("AUDIT_DEPARTMENT_TYPE").Value);
                if (xml.Element("AUDIT_DEPARTMENT_ID") != null)
                    AUDIT_DEPARTMENT_ID = Convert.ToInt32(xml.Element("AUDIT_DEPARTMENT_ID").Value);
                if (xml.Element("AUDIT_YEAR") != null)
                    AUDIT_YEAR = Convert.ToInt32(xml.Element("AUDIT_YEAR").Value);
            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("BM0",
                       new XElement("ID", ID),
                       new XElement("DEPARTMENT_ID", DEPARTMENT_ID),
                       new XElement("AUDIT_YEAR", AUDIT_YEAR),
                       new XElement("STATISTIC_PERIOD", STATISTIC_PERIOD),
                       new XElement("AUDIT_TYPE", AUDIT_TYPE),
                       new XElement("TOPIC_TYPE", TOPIC_TYPE),
                       new XElement("TOPIC_CODE", TOPIC_CODE),
                       new XElement("TOPIC_NAME", TOPIC_NAME),
                       new XElement("ORDER_NO", ORDER_NO),
                       ORDER_DATE != null ? new XElement("ORDER_DATE", Convert.ToDateTime(ORDER_DATE).ToString("dd-MMM-yy")) : new XElement("ORDER_DATE", null),
                       new XElement("AUDIT_FORM_TYPE", AUDIT_FORM_TYPE),
                       new XElement("AUDIT_PROPOSAL_TYPE", AUDIT_PROPOSAL_TYPE),
                       new XElement("AUDIT_BUDGET_TYPE", AUDIT_BUDGET_TYPE),
                       new XElement("AUDIT_INCLUDED_COUNT", AUDIT_INCLUDED_COUNT),
                       new XElement("AUDIT_INCLUDED_ORG", AUDIT_INCLUDED_ORG),
                       new XElement("WORKING_PERSON", WORKING_PERSON),
                       new XElement("WORKING_DAY", WORKING_DAY),
                       new XElement("WORKING_ADDITION_TIME", WORKING_ADDITION_TIME),
                       //new XElement("AUDIT_DEPARTMENT", AUDIT_DEPARTMENT),
                       new XElement("AUDITOR_LEAD", AUDITOR_LEADS),
                       new XElement("AUDITOR_MEMBER", AUDITOR_MEMBERS),
                       new XElement("AUDITOR_ENTRY", AUDITOR_ENTRY),
                       AUDIT_SERVICE_PAY != null ? new XElement("AUDIT_SERVICE_PAY", AUDIT_SERVICE_PAY.Split(',')) : new XElement("AUDIT_SERVICE_PAY", null),
                       new XElement("AUDIT_DEPARTMENT_TYPE", AUDIT_DEPARTMENT_TYPE),
                       new XElement("AUDIT_DEPARTMENT_ID", AUDIT_DEPARTMENT_ID),
                       new XElement("EXEC_TYPE", EXEC_TYPE),
                       new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", CREATED_DATE)
                       );
        }
    }
    public class BM0Search
    {
        public int ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string YEAR_LABEL { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public string TOPIC_CODE  { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }
        public BM0Search SetXml(XElement xml)
        {
            if (xml != null)
            {
                if (xml.Element("ID") != null)
                    ID = Convert.ToInt32(xml.Element("ID").Value);
                if (xml.Element("DEPARTMENT_NAME") != null)
                    DEPARTMENT_NAME = xml.Element("DEPARTMENT_NAME").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("YEAR_LABEL") != null)
                    YEAR_LABEL = xml.Element("YEAR_LABEL").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("AUDIT_TYPE_NAME") != null)
                    AUDIT_TYPE_NAME = xml.Element("AUDIT_TYPE_NAME").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("TOPIC_TYPE_NAME") != null)
                    TOPIC_TYPE_NAME = xml.Element("TOPIC_TYPE_NAME").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("TOPIC_CODE") != null)
                    TOPIC_CODE = xml.Element("TOPIC_CODE").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("TOPIC_NAME") != null)
                    TOPIC_NAME = xml.Element("TOPIC_NAME").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("ORDER_NO") != null)
                    ORDER_NO = xml.Element("ORDER_NO").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("ORDER_DATE") != null)
                    ORDER_DATE = Convert.ToDateTime(xml.Element("ORDER_DATE").Value.Replace("\n", "").Replace("\r", "")).ToString("yyyy.MM.dd");
                if (xml.Element("BUDGET_TYPE_NAME") != null)
                    BUDGET_TYPE_NAME = xml.Element("BUDGET_TYPE_NAME").Value.Replace("\n", "").Replace("\r", "");
            }
            return this;
        }
    }
    public class BM0Search2020
    {
        public string DEPARTMENT_NAME { get; set; }
        public int AUDIT_TYPE { get; set; }
        public string YEAR_LABEL { get; set; }
        public string AUDIT_TYPE_NAME { get; set; }
        public int TOPIC_TYPE { get; set; }
        public string TOPIC_TYPE_NAME { get; set; }
        public string TOPIC_CODE { get; set; }
        public string TOPIC_NAME { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_DATE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }
        public int AUDIT_BUDGET_TYPE { get; set; }
        public BM0Search2020 SetXml(XElement xml)
        {
            if (xml != null)
            {
                
                if (xml.Element("DEPARTMENT_NAME") != null)
                    DEPARTMENT_NAME = xml.Element("DEPARTMENT_NAME").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("AUDIT_TYPE") != null)
                    AUDIT_TYPE = Convert.ToInt32(xml.Element("AUDIT_TYPE").Value.Replace("\n", "").Replace("\r", ""));
                if (xml.Element("YEAR_LABEL") != null)
                    YEAR_LABEL = xml.Element("YEAR_LABEL").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("AUDIT_TYPE_NAME") != null)
                    AUDIT_TYPE_NAME = xml.Element("AUDIT_TYPE_NAME").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("TOPIC_TYPE") != null)
                    TOPIC_TYPE = Convert.ToInt32(xml.Element("TOPIC_TYPE").Value.Replace("\n", "").Replace("\r", ""));
                if (xml.Element("AUDIT_BUDGET_TYPE") != null)
                    AUDIT_BUDGET_TYPE = Convert.ToInt32(xml.Element("AUDIT_BUDGET_TYPE").Value.Replace("\n", "").Replace("\r", ""));
                if (xml.Element("TOPIC_TYPE_NAME") != null)
                    TOPIC_TYPE_NAME = xml.Element("TOPIC_TYPE_NAME").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("TOPIC_CODE") != null)
                    TOPIC_CODE = xml.Element("TOPIC_CODE").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("TOPIC_NAME") != null)
                    TOPIC_NAME = xml.Element("TOPIC_NAME").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("ORDER_NO") != null)
                    ORDER_NO = xml.Element("ORDER_NO").Value.Replace("\n", "").Replace("\r", "");
                if (xml.Element("ORDER_DATE") != null)
                    ORDER_DATE = Convert.ToDateTime(xml.Element("ORDER_DATE").Value.Replace("\n", "").Replace("\r", "")).ToString("yyyy.MM.dd");
                if (xml.Element("BUDGET_TYPE_NAME") != null)
                    BUDGET_TYPE_NAME = xml.Element("BUDGET_TYPE_NAME").Value.Replace("\n", "").Replace("\r", "");
            }
            return this;
        }
    }
    public class Team
    {
        public int ID { get; set; }
        public int AUDIT_ID { get; set; }
        public int TEAM_TYPE_ID { get; set; }
        public int AUDITOR_ID { get; set; }
        public Team SetXml(XElement xml)
        {
            if (xml != null)
            {
                if (xml.Element("ID") != null)
                    ID = Convert.ToInt32(xml.Element("ID").Value);
                if (xml.Element("AUDIT_ID") != null)
                    AUDIT_ID = Convert.ToInt32(xml.Element("AUDIT_ID").Value);
                if (xml.Element("TEAM_TYPE_ID") != null)
                    TEAM_TYPE_ID = Convert.ToInt32(xml.Element("TEAM_TYPE_ID").Value);
                if (xml.Element("AUDITOR_ID") != null)
                    AUDITOR_ID = Convert.ToInt32(xml.Element("AUDITOR_ID").Value);
            }
            return this;
        }
    }
}