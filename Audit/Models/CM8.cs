using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace Audit.Models
{
    public class CM8VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class CM8
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Төрийн аудитын байгууллага сонгоно уу.")]
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }

        public string APPROVED_BUDGET { get; set; }
        public string PERFORMANCE_BUDGET { get; set; }
        public int? WORKERS { get; set; }
        public int? MANAGER { get; set; }
        public int? APPROVED_NUMBERS { get; set; }
        public int? DIRECTING_STAFF { get; set; }
        public int? SENIOR_AUDITOR_ANALYST { get; set; }
        public int? AUDITOR_ANALYST { get; set; }
        public int? OTHER_OFFICE { get; set; }
        public int? EDU_DOCTOR { get; set; }
        public int? EDU_MAGISTR { get; set; }
        public int? EDU_BAKLAVR { get; set; }
        public int? EDU_AMONGST { get; set; }
        public int? EDU_JUNIOR_AMONGST { get; set; }
        public int? PRO_ACCOUNTANT { get; set; }
        public int? ACCOUNTANT_ECONOMIST { get; set; }
        public int? LAWYER { get; set; }
        public int? INGENER { get; set; }
        public int? OTHER_PROF { get; set; }
        public int? STUDY_COUNT { get; set; }
        public int? INCLUDED_MAN { get; set; }
        public int? ONLINE_STUDY_COUNT { get; set; }
        public int? LOCAL_STUDY_COUNT { get; set; }
        public int? AUDIT_STUDY_COUNT { get; set; }
        public int? FOREIGN_STUDY_COUNT { get; set; }
        public int? FOREIGN_MAN_COUNT { get; set; }
        public int? INSIDE_STUDY_COUNT { get; set; }
        public int? INSIDE_MAN_COUNT { get; set; }
        public int? ORG_STUDY_COUNT { get; set; }
        public int? ORG_MAN_COUNT { get; set; }
        public int? RESEARCH_ALL { get; set; }
        public int? PUBLISHED_REPORT { get; set; }
        public int? NEWS_ARTICLE { get; set; }
        public int? TV_NEWS_BROADCAST { get; set; }
        public int? ORG_NEWS { get; set; }
        public int? WEB_ACCESS { get; set; }
        public int? RECEIVED_ALL { get; set; }
        public int? TAB_WORKERS { get; set; }
        public int? TAB_SKILLS { get; set; }
        public int? AUDIT_LET { get; set; }
        public int? RECEIVED_OTHER { get; set; }
        public int? DECIDED_TIME { get; set; }
        public int? DEC_EXPIRED { get; set; }
        public int? DEC_UNEXPIRED { get; set; }
        public int CREATED_BY { get; set; }
        public int UPDATED_BY { get; set; }
        public int IS_ACTIVE { get; set; } = 1;
        //public int EXEC_TYPE { get; set; }

        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public CM8 SetXml(XElement xml)
        {
            if (xml != null)
            {
                if (xml.Element("CREATED_BY") != null)
                    CREATED_BY = Convert.ToInt32(xml.Element("CREATED_BY").Value);
                if (xml.Element("UPDATED_BY") != null)
                    UPDATED_BY = Convert.ToInt32(xml.Element("UPDATED_BY").Value);
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
                
                if (xml.Element("STATISTIC_PERIOD") != null)
                    STATISTIC_PERIOD = Convert.ToInt32(xml.Element("STATISTIC_PERIOD").Value);
                if (xml.Element("APPROVED_BUDGET") != null)
                    APPROVED_BUDGET = Convert.ToDecimal(xml.Element("APPROVED_BUDGET").Value).ToString("#,0.##");
                if (xml.Element("PERFORMANCE_BUDGET") != null)
                    PERFORMANCE_BUDGET = Convert.ToDecimal(xml.Element("PERFORMANCE_BUDGET").Value).ToString("#,0.##");
                if (xml.Element("WORKERS") != null)
                    WORKERS = Convert.ToInt32(xml.Element("WORKERS").Value);
                if (xml.Element("MANAGER") != null)
                    MANAGER = Convert.ToInt32(xml.Element("MANAGER").Value);
                if (xml.Element("APPROVED_NUMBERS") != null)
                    APPROVED_NUMBERS = Convert.ToInt32(xml.Element("APPROVED_NUMBERS").Value);
                if (xml.Element("DIRECTING_STAFF") != null)
                    DIRECTING_STAFF = Convert.ToInt32(xml.Element("DIRECTING_STAFF").Value);
                if (xml.Element("SENIOR_AUDITOR_ANALYST") != null)
                    SENIOR_AUDITOR_ANALYST = Convert.ToInt32(xml.Element("SENIOR_AUDITOR_ANALYST").Value);
                if (xml.Element("AUDITOR_ANALYST") != null)
                    AUDITOR_ANALYST = Convert.ToInt32(xml.Element("AUDITOR_ANALYST").Value);
                if (xml.Element("OTHER_OFFICE") != null)
                    OTHER_OFFICE = Convert.ToInt32(xml.Element("OTHER_OFFICE").Value);
                if (xml.Element("EDU_DOCTOR") != null)
                    EDU_DOCTOR = Convert.ToInt32(xml.Element("EDU_DOCTOR").Value);
                if (xml.Element("EDU_MAGISTR") != null)
                    EDU_MAGISTR = Convert.ToInt32(xml.Element("EDU_MAGISTR").Value);
                if (xml.Element("EDU_BAKLAVR") != null)
                    EDU_BAKLAVR = Convert.ToInt32(xml.Element("EDU_BAKLAVR").Value);
                if (xml.Element("EDU_AMONGST") != null)
                    EDU_AMONGST = Convert.ToInt32(xml.Element("EDU_AMONGST").Value);
                if (xml.Element("EDU_JUNIOR_AMONGST") != null)
                    EDU_JUNIOR_AMONGST = Convert.ToInt32(xml.Element("EDU_JUNIOR_AMONGST").Value);
                if (xml.Element("PRO_ACCOUNTANT") != null)
                    PRO_ACCOUNTANT = Convert.ToInt32(xml.Element("PRO_ACCOUNTANT").Value);
                if (xml.Element("ACCOUNTANT_ECONOMIST") != null)
                    ACCOUNTANT_ECONOMIST = Convert.ToInt32(xml.Element("ACCOUNTANT_ECONOMIST").Value);
                if (xml.Element("LAWYER") != null)
                    LAWYER = Convert.ToInt32(xml.Element("LAWYER").Value);
                if (xml.Element("INGENER") != null)
                    INGENER = Convert.ToInt32(xml.Element("INGENER").Value);
                if (xml.Element("OTHER_PROF") != null)
                    OTHER_PROF = Convert.ToInt32(xml.Element("OTHER_PROF").Value);
                if (xml.Element("STUDY_COUNT") != null)
                    STUDY_COUNT = Convert.ToInt32(xml.Element("STUDY_COUNT").Value);
                if (xml.Element("INCLUDED_MAN") != null)
                    INCLUDED_MAN = Convert.ToInt32(xml.Element("INCLUDED_MAN").Value);
                if (xml.Element("ONLINE_STUDY_COUNT") != null)
                    ONLINE_STUDY_COUNT = Convert.ToInt32(xml.Element("ONLINE_STUDY_COUNT").Value);
                if (xml.Element("LOCAL_STUDY_COUNT") != null)
                    LOCAL_STUDY_COUNT = Convert.ToInt32(xml.Element("LOCAL_STUDY_COUNT").Value);
                if (xml.Element("AUDIT_STUDY_COUNT") != null)
                    AUDIT_STUDY_COUNT = Convert.ToInt32(xml.Element("AUDIT_STUDY_COUNT").Value);
                if (xml.Element("FOREIGN_STUDY_COUNT") != null)
                    FOREIGN_STUDY_COUNT = Convert.ToInt32(xml.Element("FOREIGN_STUDY_COUNT").Value);
                if (xml.Element("FOREIGN_MAN_COUNT") != null)
                    FOREIGN_MAN_COUNT = Convert.ToInt32(xml.Element("FOREIGN_MAN_COUNT").Value);
                if (xml.Element("INSIDE_STUDY_COUNT") != null)
                    INSIDE_STUDY_COUNT = Convert.ToInt32(xml.Element("INSIDE_STUDY_COUNT").Value);
                if (xml.Element("INSIDE_MAN_COUNT") != null)
                    INSIDE_MAN_COUNT = Convert.ToInt32(xml.Element("INSIDE_MAN_COUNT").Value);
                if (xml.Element("ORG_STUDY_COUNT") != null)
                    ORG_STUDY_COUNT = Convert.ToInt32(xml.Element("ORG_STUDY_COUNT").Value);
                if (xml.Element("ORG_MAN_COUNT") != null)
                    ORG_MAN_COUNT = Convert.ToInt32(xml.Element("ORG_MAN_COUNT").Value);
                if (xml.Element("RESEARCH_ALL") != null)
                    RESEARCH_ALL = Convert.ToInt32(xml.Element("RESEARCH_ALL").Value);
                if (xml.Element("PUBLISHED_REPORT") != null)
                    PUBLISHED_REPORT = Convert.ToInt32(xml.Element("PUBLISHED_REPORT").Value);
                if (xml.Element("NEWS_ARTICLE") != null)
                    NEWS_ARTICLE = Convert.ToInt32(xml.Element("NEWS_ARTICLE").Value);
                if (xml.Element("TV_NEWS_BROADCAST") != null)
                    TV_NEWS_BROADCAST = Convert.ToInt32(xml.Element("TV_NEWS_BROADCAST").Value);
                if (xml.Element("ORG_NEWS") != null)
                    ORG_NEWS = Convert.ToInt32(xml.Element("ORG_NEWS").Value);
                if (xml.Element("WEB_ACCESS") != null)
                    WEB_ACCESS = Convert.ToInt32(xml.Element("WEB_ACCESS").Value);
                if (xml.Element("RECEIVED_ALL") != null)
                    RECEIVED_ALL = Convert.ToInt32(xml.Element("RECEIVED_ALL").Value);
                if (xml.Element("TAB_WORKERS") != null)
                    TAB_WORKERS = Convert.ToInt32(xml.Element("TAB_SKILLS").Value);
                if (xml.Element("TAB_SKILLS") != null)
                    TAB_SKILLS = Convert.ToInt32(xml.Element("TAB_SKILLS").Value);
                if (xml.Element("AUDIT_LET") != null)
                    AUDIT_LET = Convert.ToInt32(xml.Element("AUDIT_LET").Value);
                if (xml.Element("RECEIVED_OTHER") != null)
                    RECEIVED_OTHER = Convert.ToInt32(xml.Element("RECEIVED_OTHER").Value);
                if (xml.Element("DECIDED_TIME") != null)
                    DECIDED_TIME = Convert.ToInt32(xml.Element("DECIDED_TIME").Value);
                if (xml.Element("DEC_EXPIRED") != null)
                    DEC_EXPIRED = Convert.ToInt32(xml.Element("DEC_EXPIRED").Value);
                if (xml.Element("DEC_UNEXPIRED") != null)
                    DEC_UNEXPIRED = Convert.ToInt32(xml.Element("DEC_UNEXPIRED").Value);

                if (xml.Element("IS_ACTIVE") != null)
                    IS_ACTIVE = Convert.ToInt32(xml.Element("IS_ACTIVE").Value);
                //if (xml.Element("EXEC_TYPE") != null)
                //    EXEC_TYPE = Convert.ToInt32(xml.Element("EXEC_TYPE").Value);
                if (xml.Element("CREATED_DATE") != null)
                    CREATED_DATE = Convert.ToDateTime(xml.Element("CREATED_DATE").Value);

            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("CM8",
                       new XElement("ID", ID),
                       new XElement("OFFICE_ID", OFFICE_ID),
                       new XElement("STATISTIC_PERIOD", STATISTIC_PERIOD),
                       APPROVED_BUDGET != null ? new XElement("APPROVED_BUDGET", APPROVED_BUDGET.Split(',')) : new XElement("APPROVED_BUDGET", null),
                       PERFORMANCE_BUDGET != null ? new XElement("PERFORMANCE_BUDGET", PERFORMANCE_BUDGET.Split(',')) : new XElement("PERFORMANCE_BUDGET", null),
                       new XElement("WORKERS", WORKERS),
                       new XElement("MANAGER", MANAGER),
                       new XElement("APPROVED_NUMBERS", APPROVED_NUMBERS),
                       new XElement("DIRECTING_STAFF", DIRECTING_STAFF),
                       new XElement("SENIOR_AUDITOR_ANALYST", SENIOR_AUDITOR_ANALYST),
                       new XElement("AUDITOR_ANALYST", AUDITOR_ANALYST),
                       new XElement("OTHER_OFFICE", OTHER_OFFICE),
                       new XElement("EDU_DOCTOR", EDU_DOCTOR),
                       new XElement("EDU_MAGISTR", EDU_MAGISTR),
                       new XElement("EDU_BAKLAVR", EDU_BAKLAVR),
                       new XElement("EDU_AMONGST", EDU_AMONGST),
                       new XElement("EDU_JUNIOR_AMONGST", EDU_JUNIOR_AMONGST),
                       new XElement("PRO_ACCOUNTANT", PRO_ACCOUNTANT),
                       new XElement("ACCOUNTANT_ECONOMIST", ACCOUNTANT_ECONOMIST),
                       new XElement("LAWYER", LAWYER),
                       new XElement("INGENER", INGENER),
                       new XElement("OTHER_PROF", OTHER_PROF),
                       new XElement("STUDY_COUNT", STUDY_COUNT),
                       new XElement("INCLUDED_MAN", INCLUDED_MAN),
                       new XElement("ONLINE_STUDY_COUNT", ONLINE_STUDY_COUNT),
                       new XElement("LOCAL_STUDY_COUNT", LOCAL_STUDY_COUNT),
                       new XElement("AUDIT_STUDY_COUNT", AUDIT_STUDY_COUNT),
                       new XElement("FOREIGN_STUDY_COUNT", FOREIGN_STUDY_COUNT),
                       new XElement("FOREIGN_MAN_COUNT", FOREIGN_MAN_COUNT),
                       new XElement("INSIDE_STUDY_COUNT", INSIDE_STUDY_COUNT),
                       new XElement("INSIDE_MAN_COUNT", INSIDE_MAN_COUNT),
                       new XElement("ORG_STUDY_COUNT", ORG_STUDY_COUNT),
                       new XElement("ORG_MAN_COUNT", ORG_MAN_COUNT),
                       new XElement("RESEARCH_ALL", RESEARCH_ALL),
                       new XElement("PUBLISHED_REPORT", PUBLISHED_REPORT),
                       new XElement("NEWS_ARTICLE", NEWS_ARTICLE),
                       new XElement("TV_NEWS_BROADCAST", TV_NEWS_BROADCAST),
                       new XElement("ORG_NEWS", ORG_NEWS),
                       new XElement("WEB_ACCESS", WEB_ACCESS),
                       new XElement("RECEIVED_ALL", RECEIVED_ALL),
                       new XElement("TAB_WORKERS", TAB_WORKERS),
                       new XElement("TAB_SKILLS", TAB_SKILLS),
                       new XElement("AUDIT_LET", AUDIT_LET),
                       new XElement("RECEIVED_OTHER", RECEIVED_OTHER),
                       new XElement("DECIDED_TIME", DECIDED_TIME),
                       new XElement("DEC_EXPIRED", DEC_EXPIRED),
                       new XElement("DEC_UNEXPIRED", DEC_UNEXPIRED),
                       //new XElement("EXEC_TYPE", EXEC_TYPE),
                       new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", CREATED_DATE != null ? ((DateTime)CREATED_DATE).ToString("dd-MMM-yy") : null)
                       );
        }
    }

}