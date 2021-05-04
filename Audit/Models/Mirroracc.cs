using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class Mirroracc
    {
        public int OPEN_ID { get; set; }
        public bool IsShow { get; set; } = false;
        public string BUDGET_SHORT_NAME { get; set; }
        public string OPEN_ENT_BUDGET_PARENT { get; set; }
        public string OPEN_ENT_NAME { get; set; }
        public string OPEN_HEAD_ROLE { get; set; }
        public string OPEN_HEAD_NAME { get; set; }
        public string OPEN_HEAD_PHONE { get; set; }
        public string OPEN_ACC_ROLE { get; set; }
        public string OPEN_ACC_NAME { get; set; }
        public string OPEN_ACC_PHONE { get; set; }
        public int OPEN_ENT_GROUP_ID { get; set; }
        public int REPORT_YEAR { get; set; }

        public List<Tab1> tab1 { get; set; }
        public List<Tab2> tab2 { get; set; }
        public List<Tab3> tab3 { get; set; }
        public List<Tab4> tab4 { get; set; }
        public List<Tab5> tab5 { get; set; }
        public List<Tab6> tab6 { get; set; }
        public List<Tab7> tab7 { get; set; }
        public List<Tab8> tab8 { get; set; }
        public List<Print1> print1 { get; set; }
        public int AUD_LAWS_NUM { get; set; }
        public Mirroracc FromXml(XElement elem)
        {
            if (elem.Element("OPEN_ID") != null)
                OPEN_ID = Convert.ToInt32(elem.Element("OPEN_ID").Value);
            if (elem.Element("BUDGET_SHORT_NAME") != null)
                BUDGET_SHORT_NAME = elem.Element("BUDGET_SHORT_NAME").Value;
            if (elem.Element("OPEN_ENT_BUDGET_PARENT") != null)
                OPEN_ENT_BUDGET_PARENT = elem.Element("OPEN_ENT_BUDGET_PARENT").Value;
            if (elem.Element("OPEN_ENT_NAME") != null)
                OPEN_ENT_NAME = elem.Element("OPEN_ENT_NAME").Value;
            if (elem.Element("OPEN_HEAD_ROLE") != null)
                OPEN_HEAD_ROLE = elem.Element("OPEN_HEAD_ROLE").Value;
            if (elem.Element("OPEN_HEAD_NAME") != null)
                OPEN_HEAD_NAME = elem.Element("OPEN_HEAD_NAME").Value;
            if (elem.Element("OPEN_HEAD_PHONE") != null)
                OPEN_HEAD_PHONE = elem.Element("OPEN_HEAD_PHONE").Value;
            if (elem.Element("OPEN_ACC_ROLE") != null)
                OPEN_ACC_ROLE = elem.Element("OPEN_ACC_ROLE").Value;
            if (elem.Element("OPEN_ACC_NAME") != null)
                OPEN_ACC_NAME = elem.Element("OPEN_ACC_NAME").Value;
            if (elem.Element("OPEN_ACC_PHONE") != null)
                OPEN_ACC_PHONE = elem.Element("OPEN_ACC_PHONE").Value;
            if (elem.Element("OPEN_ENT_GROUP_ID") != null)
                OPEN_ENT_GROUP_ID = Convert.ToInt32(elem.Element("OPEN_ENT_GROUP_ID").Value);

            return this;
        }
        public XElement ToXml()
        {
            return new XElement("Mirroracc",
                       new XElement("OPEN_ID", OPEN_ID),
                       new XElement("BUDGET_SHORT_NAME", BUDGET_SHORT_NAME),
                       new XElement("OPEN_ENT_BUDGET_PARENT", OPEN_ENT_BUDGET_PARENT),
                       new XElement("OPEN_ENT_NAME", OPEN_ENT_NAME),
                       new XElement("OPEN_HEAD_ROLE", OPEN_HEAD_ROLE),
                       new XElement("OPEN_HEAD_NAME", OPEN_HEAD_NAME),
                       new XElement("OPEN_HEAD_PHONE", OPEN_HEAD_PHONE),
                       new XElement("OPEN_ACC_ROLE", OPEN_ACC_ROLE),
                       new XElement("OPEN_ACC_NAME", OPEN_ACC_NAME),
                       new XElement("OPEN_ACC_PHONE", OPEN_ACC_PHONE),
                       new XElement("OPEN_ENT_GROUP_ID", OPEN_ENT_GROUP_ID)
                       );
        }
    }

    public class MirroraccOrgList
    {
        public int OPEN_ID { get; set; }
        public string BUDGET_SHORT_NAME { get; set; }
        public string OPEN_ENT_BUDGET_PARENT { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string OPEN_ENT_NAME { get; set; }
        public string OPEN_ENT_REGISTER_NO { get; set; }
        public string MAYGT { get; set; }
        public int IS_FINISHED { get; set; }
        public int IS_PRINTED { get; set; }
        public string USER_NAME { get; set; }
        public string INSERTDATE { get; set; }

        public MirroraccOrgList FromXml(XElement elem)
        {
            if (elem.Element("OPEN_ID") != null)
                OPEN_ID = Convert.ToInt32(elem.Element("OPEN_ID").Value);
            if (elem.Element("BUDGET_SHORT_NAME") != null)
                BUDGET_SHORT_NAME = elem.Element("BUDGET_SHORT_NAME").Value;
            if (elem.Element("OPEN_ENT_BUDGET_PARENT") != null)
                OPEN_ENT_BUDGET_PARENT = elem.Element("OPEN_ENT_BUDGET_PARENT").Value;
            if (elem.Element("DEPARTMENT_NAME") != null)
                DEPARTMENT_NAME = elem.Element("DEPARTMENT_NAME").Value;
            if (elem.Element("OPEN_ENT_NAME") != null)
                OPEN_ENT_NAME = elem.Element("OPEN_ENT_NAME").Value;
            if (elem.Element("OPEN_ENT_REGISTER_NO") != null)
                OPEN_ENT_REGISTER_NO = elem.Element("OPEN_ENT_REGISTER_NO").Value;
            if (elem.Element("MAYGT") != null)
                MAYGT = elem.Element("MAYGT").Value;
            if (elem.Element("IS_FINISHED") != null)
                IS_FINISHED = Convert.ToInt32(elem.Element("IS_FINISHED").Value);
            if (elem.Element("IS_PRINTED") != null)
                IS_PRINTED = Convert.ToInt32(elem.Element("IS_PRINTED").Value);
            if (elem.Element("USER_NAME") != null)
                USER_NAME = elem.Element("USER_NAME").Value;
            if (elem.Element("INSERTDATE") != null)
                INSERTDATE = elem.Element("INSERTDATE").Value;
            return this;
        }
    }

    public class Tab1
    {
        public string MD_CODE { get; set; }
        public string MD_LAWS_NUM { get; set; }
        public string MD_NAME { get; set; }
        public string MD_TIME { get; set; }

        [Required(ErrorMessage = "Үнэлгээ сонгоно уу")]
        public double Data01 { get; set; }
        public string Data02 { get; set; }
        public string Data03 { get; set; }
    }

    public class Tab2
    {
        public string MD_CODE { get; set; }
        public string MD_LAWS_NUM { get; set; }
        public string MD_NAME { get; set; }
        public string MD_TIME { get; set; }
        public string Data01 { get; set; }
        public string Data02 { get; set; }
        public string Data03 { get; set; }
    }

    public class Tab3
    {
        public string MD_CODE { get; set; }
        public string MD_LAWS_NUM { get; set; }
        public string MD_NAME { get; set; }
        public string MD_TIME { get; set; }
        public string Data01 { get; set; }
        public string Data02 { get; set; }
        public string Data03 { get; set; }
    }

    public class Tab4
    {
        public string MD_CODE { get; set; }
        public string MD_LAWS_NUM { get; set; }
        public string MD_NAME { get; set; }
        public string MD_TIME { get; set; }
        public double Data01 { get; set; }
        public string Data02 { get; set; }
        public string Data03 { get; set; }
    }

    public class Tab5
    {
        public string MD_CODE { get; set; }
        public string MD_LAWS_NUM { get; set; }
        public string MD_NAME { get; set; }
        public string MD_TIME { get; set; }
        public string Data01 { get; set; }
        public string Data02 { get; set; }
        public string Data03 { get; set; }
    }

    public class Tab6
    {
        public string MD_CODE { get; set; }
        public string MD_LAWS_NUM { get; set; }
        public string MD_NAME { get; set; }
        public string MD_TIME { get; set; }
        public double Data01 { get; set; }
        public string Data02 { get; set; }
        public string Data03 { get; set; }
    }

    public class Tab7
    {
        public string MD_CODE { get; set; }
        public string MD_LAWS_NUM { get; set; }
        public string MD_NAME { get; set; }
        public string MD_TIME { get; set; }
        public string Data01 { get; set; }
        public string Data02 { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? Data03 { get; set; }
    }

    public class Tab8
    {
        public string PROJECT_NAME { get; set; }
        public string PROJECT_NUMBER { get; set; }
        public string PROJECT_START_DATE { get; set; }
        public string PROJECT_END_DATE { get; set; }
        public string PROJECT_PERCENT { get; set; }
        public string PROJECT_TOTAL_BUDGET { get; set; }
        public string PROJECT_ORG_FUND { get; set; }
        public string PROJECT_ID { get; set; }
        public int ORG_ID { get; set; }

    }
    public class Print1
    {
        public string MD_CODE { get; set; }
        public string MD_TIME { get; set; }
        public string PARENT_NAME { get; set; }
        public string MD_NAME { get; set; }
        public string MEDEELEH_TOO_HEMJEE { get; set; }
        public string MEDEELSEN { get; set; }
        public string MEDEELEEGUI { get; set; }
        public string SHAARDLAGAGUI { get; set; }
        public string HUGATSAA_HOTSROOSON { get; set; }
        public string PRECENT1 { get; set; }
        public string PRECENT2 { get; set; }

    }
}