using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Audit.Models
{
    public class Organization
    {
        public int ORG_ID { get; set; }
        public bool IsShow { get; set; } = false;
        public int ORGB_ID { get; set; }
        public int ORGB_ID2 { get; set; }
        public int ORGP_ID { get; set; }
        public int ORGP_ID2 { get; set; }
        [Required(ErrorMessage = "Дугаар оруулна уу.")]
        public string ORG_CODE { get; set; }
        [Required(ErrorMessage = "Нэр оруулна уу.")]
        [RegularExpression("^[а-яА-Я|ө|ү|Ө|Ү| |,|\\.]*$", ErrorMessage ="Текст утга биш байна.")]
        public string ORG_NAME { get; set; }
        [Required(ErrorMessage = "Регистрийн дугаар оруулна уу.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Тоон утга оруулна уу.")]
        [StringLength(int.MaxValue, MinimumLength = 7, ErrorMessage = "7 оронтой байна.")]
        public string ORG_REGISTER_NO { get; set; }
        [Required(ErrorMessage = "УБ-ийн дугаар оруулна уу.")]
        public string ORG_REGISTER_NUMBER { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? ORG_REG_DATE { get; set; }
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORG_OFFICE_ID { get; set; }
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORG_SUB_OFFICE_ID { get; set; }
        [Required(ErrorMessage = "Албан ёсны хаяг оруулна уу.")]
        public string ORG_ADDRESS { get; set; }
        [Required(ErrorMessage = "Цахим хуудас оруулна уу.")]
        public string ORG_WEBSITE { get; set; }
        [Required(ErrorMessage = "И-мэйл оруулна уу.")]
        public string ORG_EMAIL { get; set; }
        [Required(ErrorMessage = "Утас оруулна уу.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Тоон утга оруулна уу.")]
        public string ORG_PHONE { get; set; }
        //[Required(ErrorMessage = "Факс оруулна уу.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Тоон утга оруулна уу.")]
        public string ORG_FAX { get; set; }

        public string ORG_CONCENTRATOR_NAME { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }

        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORGB_BANK_ID { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Тоон утга оруулна уу.")]
        public string ORGB_BANK_ACCOUNT { get; set; }
        public string ORGB_DESCRIPTION { get; set; }
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORGB_BANK_ID2 { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Тоон утга оруулна уу.")]
        public string ORGB_BANK_ACCOUNT2 { get; set; }
        public string ORGB_DESCRIPTION2 { get; set; }

        //udirdlaga
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORGP_ROLE { get; set; }        
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? ORGP_ROLE_DATE { get; set; }
        [RegularExpression(@"^([а-яА-Я|ө|ү|Ө|Ү]{2})([0-9]{8})$", ErrorMessage = "Регистрийн дугаар зөв оруулна уу.")]
        public string ORGP_REGISTER_NO { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORGP_LASTNAME { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORGP_FIRSTNAME { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Тоон утга оруулна уу.")]
        public string ORGP_PHONE { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORGP_EMAIL { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORGP_EXPERIENCE_YEAR { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORGP_PROFESSION { get; set; }

        //nybo
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORGP_ROLE2 { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? ORGP_ROLE_DATE2 { get;set; }
        [RegularExpression(@"^([а-яА-Я|ө|ү|Ө|Ү]{2})([0-9]{8})$", ErrorMessage = "Регистрийн дугаар зөв оруулна уу.")]
        public string ORGP_REGISTER_NO2 { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORGP_LASTNAME2 { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORGP_FIRSTNAME2 { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Тоон утга оруулна уу.")]
        public string ORGP_PHONE2 { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORGP_EMAIL2 { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORGP_EXPERIENCE_YEAR2 { get; set; }
        //[Required(ErrorMessage = "Утга оруулна уу.")]
        public string ORGP_PROFESSION2 { get; set; }

        //busad
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORG_BUDGET_TYPE_ID { get; set; }
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORG_ACTIVITY_ID { get; set; }
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORG_SUB_BUDGET_TYPE_ID { get; set; }
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORG_DEPARTMENT_ID { get; set; }
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORG_COMMITTEE_ID { get; set; }
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORG_TAX_OFFICE_ID { get; set; }
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORG_COST_TYPE_ID { get; set; }
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORG_INSURANCE_OFFICE_ID { get; set; }
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORG_FIN_OFFICE_ID { get; set; }
        [Required(ErrorMessage = "Утга сонгоно уу.")]
        public int ORG_FINANCING_TYPE_ID { get; set; }

        public List<Office> offices { get; set; } = new List<Office>();
        public List<SubOffice> subOffices { get; set; } = new List<SubOffice>();

        public List<ActivityLib> activities { get; set; } = new List<ActivityLib>();
        public List<BudgetType> budgetTypes { get; set; } = new List<BudgetType>();
        public List<SubBudgetType> subBudgetTypes { get; set; } = new List<SubBudgetType>();
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Committee> committees { get; set; } = new List<Committee>();
        public List<TaxOffice> taxOffices { get; set; } = new List<TaxOffice>();
        public List<CostType> costTypes { get; set; } = new List<CostType>();
        public List<InsuranceOffice> insuranceOffices { get; set; } = new List<InsuranceOffice>();
        public List<FinOffice> finOffices { get; set; } = new List<FinOffice>();
        public List<FinancingType> financingTypes { get; set; } = new List<FinancingType>();
        public List<Bank> banks { get; set; } = new List<Bank>();

        public List<OrganizationUB> organizationUBs { get; set; } = new List<OrganizationUB>();
        public List<OrganizationMOF> organizationMOFs { get; set; } = new List<OrganizationMOF>();
        public List<OrganizationTAX> organizationTAXs { get; set; } = new List<OrganizationTAX>();

        public List<Tab1> tab1 { get; set; }
        public List<Tab2> tab2 { get; set; }
        public List<Tab3> tab3 { get; set; }
        public List<Tab4> tab4 { get; set; }
        public List<Tab5> tab5 { get; set; }
        public List<Tab6> tab6 { get; set; }
        public List<Tab7> tab7 { get; set; }
        public List<Tab8> tab8 { get; set; }
        public int AUD_LAWS_NUM { get; set; }
        public Organization FromXml(XElement elem)
        {
            if (elem.Element("ORG_ID") != null)
                ORG_ID = Convert.ToInt32(elem.Element("ORG_ID").Value);
            if (elem.Element("ORGB_ID") != null)
                ORGB_ID = Convert.ToInt32(elem.Element("ORGB_ID").Value);
            if (elem.Element("ORGB_ID2") != null)
                ORGB_ID2 = Convert.ToInt32(elem.Element("ORGB_ID2").Value);
            if (elem.Element("ORGP_ID") != null)
                ORGP_ID = Convert.ToInt32(elem.Element("ORGP_ID").Value);
            if (elem.Element("ORGP_ID2") != null)
                ORGP_ID2 = Convert.ToInt32(elem.Element("ORGP_ID2").Value);
            if (elem.Element("ORG_DEPARTMENT_ID") != null)
                ORG_DEPARTMENT_ID = Convert.ToInt32(elem.Element("ORG_DEPARTMENT_ID").Value);
            if (elem.Element("ORG_CODE") != null)
                ORG_CODE = elem.Element("ORG_CODE").Value;
            if (elem.Element("ORG_NAME") != null)
                ORG_NAME = elem.Element("ORG_NAME").Value;
            if (elem.Element("ORG_REGISTER_NO") != null)
                ORG_REGISTER_NO = elem.Element("ORG_REGISTER_NO").Value;
            if (elem.Element("ORG_REGISTER_NUMBER") != null)
                ORG_REGISTER_NUMBER = elem.Element("ORG_REGISTER_NUMBER").Value;
            if (elem.Element("ORG_CONCENTRATOR_NAME") != null)
                ORG_CONCENTRATOR_NAME = elem.Element("ORG_CONCENTRATOR_NAME").Value;
            if (elem.Element("BUDGET_TYPE_NAME") != null)
                BUDGET_TYPE_NAME = elem.Element("BUDGET_TYPE_NAME").Value;
            if (elem.Element("ORG_REG_DATE") != null)
                ORG_REG_DATE = Convert.ToDateTime(elem.Element("ORG_REG_DATE").Value);
            if (elem.Element("ORG_OFFICE_ID") != null)
                ORG_OFFICE_ID = Convert.ToInt32(elem.Element("ORG_OFFICE_ID").Value);
            if (elem.Element("ORG_SUB_OFFICE_ID") != null)
                ORG_SUB_OFFICE_ID = Convert.ToInt32(elem.Element("ORG_SUB_OFFICE_ID").Value);
            if (elem.Element("ORG_ADDRESS") != null)
                ORG_ADDRESS = elem.Element("ORG_ADDRESS").Value;
            if (elem.Element("ORG_WEBSITE") != null)
                ORG_WEBSITE = elem.Element("ORG_WEBSITE").Value;
            if (elem.Element("ORG_EMAIL") != null)
                ORG_EMAIL = elem.Element("ORG_EMAIL").Value;
            if (elem.Element("ORG_PHONE") != null)
                ORG_PHONE = elem.Element("ORG_PHONE").Value;
            if (elem.Element("ORG_FAX") != null)
                ORG_FAX = elem.Element("ORG_FAX").Value;
            //bank
            if (elem.Element("ORGB_BANK_ID") != null)
                ORGB_BANK_ID = Convert.ToInt32(elem.Element("ORGB_BANK_ID").Value);
            if (elem.Element("ORGB_BANK_ACCOUNT") != null)
                ORGB_BANK_ACCOUNT = elem.Element("ORGB_BANK_ACCOUNT").Value;
            if (elem.Element("ORGB_DESCRIPTION") != null)
                ORGB_DESCRIPTION = elem.Element("ORGB_DESCRIPTION").Value;
            if (elem.Element("ORGB_BANK_ID2") != null)
                ORGB_BANK_ID2 = Convert.ToInt32(elem.Element("ORGB_BANK_ID2").Value);
            if (elem.Element("ORGB_BANK_ACCOUNT2") != null)
                ORGB_BANK_ACCOUNT2 = elem.Element("ORGB_BANK_ACCOUNT2").Value;
            if (elem.Element("ORGB_DESCRIPTION2") != null)
                ORGB_DESCRIPTION2 = elem.Element("ORGB_DESCRIPTION2").Value;

            //udirdlaga
            if (elem.Element("ORGP_ROLE") != null)
                ORGP_ROLE = elem.Element("ORGP_ROLE").Value;
            if (elem.Element("ORGP_ROLE_DATE") != null)
                ORGP_ROLE_DATE = Convert.ToDateTime(elem.Element("ORGP_ROLE_DATE").Value);
            if (elem.Element("ORGP_REGISTER_NO") != null)
                ORGP_REGISTER_NO = elem.Element("ORGP_REGISTER_NO").Value;
            if (elem.Element("ORGP_LASTNAME") != null)
                ORGP_LASTNAME = elem.Element("ORGP_LASTNAME").Value;
            if (elem.Element("ORGP_FIRSTNAME") != null)
                ORGP_FIRSTNAME = elem.Element("ORGP_FIRSTNAME").Value;
            if (elem.Element("ORGP_PHONE") != null)
                ORGP_PHONE = elem.Element("ORGP_PHONE").Value;
            if (elem.Element("ORGP_EMAIL") != null)
                ORGP_EMAIL = elem.Element("ORGP_EMAIL").Value;
            if (elem.Element("ORGP_EXPERIENCE_YEAR") != null)
                ORGP_EXPERIENCE_YEAR = elem.Element("ORGP_EXPERIENCE_YEAR").Value;
            if (elem.Element("ORGP_PROFESSION") != null)
                ORGP_PROFESSION = elem.Element("ORGP_PROFESSION").Value;

            //nybo
            if (elem.Element("ORGP_ROLE2") != null)
                ORGP_ROLE2 = elem.Element("ORGP_ROLE2").Value;
            if (elem.Element("ORGP_ROLE_DATE2") != null)
                ORGP_ROLE_DATE2 = Convert.ToDateTime(elem.Element("ORGP_ROLE_DATE2").Value);
            if (elem.Element("ORGP_REGISTER_NO2") != null)
                ORGP_REGISTER_NO2 = elem.Element("ORGP_REGISTER_NO2").Value;
            if (elem.Element("ORGP_LASTNAME2") != null)
                ORGP_LASTNAME2 = elem.Element("ORGP_LASTNAME2").Value;
            if (elem.Element("ORGP_FIRSTNAME2") != null)
                ORGP_FIRSTNAME2 = elem.Element("ORGP_FIRSTNAME2").Value;
            if (elem.Element("ORGP_PHONE2") != null)
                ORGP_PHONE2 = elem.Element("ORGP_PHONE2").Value;
            if (elem.Element("ORGP_EMAIL2") != null)
                ORGP_EMAIL2 = elem.Element("ORGP_EMAIL2").Value;
            if (elem.Element("ORGP_EXPERIENCE_YEAR2") != null)
                ORGP_EXPERIENCE_YEAR2 = elem.Element("ORGP_EXPERIENCE_YEAR2").Value;
            if (elem.Element("ORGP_PROFESSION2") != null)
                ORGP_PROFESSION2 = elem.Element("ORGP_PROFESSION2").Value;

            if (elem.Element("ORG_BUDGET_TYPE_ID") != null)
                ORG_BUDGET_TYPE_ID = Convert.ToInt32(elem.Element("ORG_BUDGET_TYPE_ID").Value);
            if (elem.Element("ORG_ACTIVITY_ID") != null)
                ORG_ACTIVITY_ID = Convert.ToInt32(elem.Element("ORG_ACTIVITY_ID").Value);
            if (elem.Element("ORG_SUB_BUDGET_TYPE_ID") != null)
                ORG_SUB_BUDGET_TYPE_ID = Convert.ToInt32(elem.Element("ORG_SUB_BUDGET_TYPE_ID").Value);
            if (elem.Element("ORG_COMMITTEE_ID") != null)
                ORG_COMMITTEE_ID = Convert.ToInt32(elem.Element("ORG_COMMITTEE_ID").Value);
            if (elem.Element("ORG_TAX_OFFICE_ID") != null)
                ORG_TAX_OFFICE_ID = Convert.ToInt32(elem.Element("ORG_TAX_OFFICE_ID").Value);
            if (elem.Element("ORG_COST_TYPE_ID") != null)
                ORG_COST_TYPE_ID = Convert.ToInt32(elem.Element("ORG_COST_TYPE_ID").Value);
            if (elem.Element("ORG_INSURANCE_OFFICE_ID") != null)
                ORG_INSURANCE_OFFICE_ID = Convert.ToInt32(elem.Element("ORG_INSURANCE_OFFICE_ID").Value);
            if (elem.Element("ORG_FIN_OFFICE_ID") != null)
                ORG_FIN_OFFICE_ID = Convert.ToInt32(elem.Element("ORG_FIN_OFFICE_ID").Value);
            if (elem.Element("ORG_FINANCING_TYPE_ID") != null)
                ORG_FINANCING_TYPE_ID = Convert.ToInt32(elem.Element("ORG_FINANCING_TYPE_ID").Value);
            
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("Organization",
                       new XElement("ORG_ID", ORG_ID),
                       new XElement("ORGB_ID", ORGB_ID),
                       new XElement("ORGB_ID2", ORGB_ID2),
                       new XElement("ORGP_ID", ORGP_ID),
                       new XElement("ORGP_ID2", ORGP_ID2),
                       new XElement("ORG_CODE", ORG_CODE),
                       new XElement("ORG_NAME", ORG_NAME),
                       new XElement("ORG_REGISTER_NO", ORG_REGISTER_NO),
                       new XElement("ORG_REGISTER_NUMBER", ORG_REGISTER_NUMBER),
                       new XElement("ORG_REG_DATE", ORG_REG_DATE != null ? ((DateTime)ORG_REG_DATE).ToString("yyyy/MM/dd") : null),
                       new XElement("ORG_OFFICE_ID", ORG_OFFICE_ID),
                       new XElement("ORG_SUB_OFFICE_ID", ORG_SUB_OFFICE_ID),
                       new XElement("ORG_ADDRESS", ORG_ADDRESS),
                       new XElement("ORG_WEBSITE", ORG_WEBSITE),
                       new XElement("ORG_EMAIL", ORG_EMAIL),
                       new XElement("ORG_PHONE", ORG_PHONE),
                       new XElement("ORG_FAX", ORG_FAX),

                       new XElement("ORGB_BANK_ID", ORGB_BANK_ID),
                       new XElement("ORGB_BANK_ACCOUNT", ORGB_BANK_ACCOUNT),
                       new XElement("ORGB_DESCRIPTION", ORGB_DESCRIPTION),
                       new XElement("ORGB_BANK_ID2", ORGB_BANK_ID2),
                       new XElement("ORGB_BANK_ACCOUNT2", ORGB_BANK_ACCOUNT2),
                       new XElement("ORGB_DESCRIPTION2", ORGB_DESCRIPTION2),

                       new XElement("ORGP_ROLE", ORGP_ROLE),
                       new XElement("ORGP_ROLE_DATE", ORGP_ROLE_DATE != null ? ((DateTime)ORGP_ROLE_DATE).ToString("yyyy/MM/dd") : null),
                       new XElement("ORGP_REGISTER_NO", ORGP_REGISTER_NO),
                       new XElement("ORGP_LASTNAME", ORGP_LASTNAME),
                       new XElement("ORGP_FIRSTNAME", ORGP_FIRSTNAME),
                       new XElement("ORGP_PHONE", ORGP_PHONE),
                       new XElement("ORGP_EMAIL", ORGP_EMAIL),
                       new XElement("ORGP_EXPERIENCE_YEAR", ORGP_EXPERIENCE_YEAR),
                       new XElement("ORGP_PROFESSION", ORGP_PROFESSION),

                       new XElement("ORGP_ROLE2", ORGP_ROLE2),
                       new XElement("ORGP_ROLE_DATE2", ORGP_ROLE_DATE2 !=null ? ((DateTime)ORGP_ROLE_DATE2).ToString("yyyy/MM/dd") : null),
                       new XElement("ORGP_REGISTER_NO2", ORGP_REGISTER_NO2),
                       new XElement("ORGP_LASTNAME2", ORGP_LASTNAME2),
                       new XElement("ORGP_FIRSTNAME2", ORGP_FIRSTNAME2),
                       new XElement("ORGP_PHONE2", ORGP_PHONE2),
                       new XElement("ORGP_EMAIL2", ORGP_EMAIL2),
                       new XElement("ORGP_EXPERIENCE_YEAR2", ORGP_EXPERIENCE_YEAR2),
                       new XElement("ORGP_PROFESSION2", ORGP_PROFESSION2),

                       new XElement("ORG_BUDGET_TYPE_ID", ORG_BUDGET_TYPE_ID),
                       new XElement("ORG_ACTIVITY_ID", ORG_ACTIVITY_ID),
                       new XElement("ORG_SUB_BUDGET_TYPE_ID", ORG_SUB_BUDGET_TYPE_ID),
                       new XElement("ORG_DEPARTMENT_ID", ORG_DEPARTMENT_ID),
                       new XElement("ORG_COMMITTEE_ID", ORG_COMMITTEE_ID),
                       new XElement("ORG_TAX_OFFICE_ID", ORG_TAX_OFFICE_ID),
                       new XElement("ORG_COST_TYPE_ID", ORG_COST_TYPE_ID),
                       new XElement("ORG_INSURANCE_OFFICE_ID", ORG_INSURANCE_OFFICE_ID),
                       new XElement("ORG_FIN_OFFICE_ID", ORG_FIN_OFFICE_ID), 
                       new XElement("ORG_FINANCING_TYPE_ID", ORG_FINANCING_TYPE_ID)
                       );
        }
    }
    public class OrganizationUB
    {
        public int UB_ID { get; set; }
        public string UB_REGISTER_NO { get; set; }
        public string UB_NAME { get; set; }
        public string UB_DOCUMENT_NO { get; set; }
        public DateTime UB_REG_DATE { get; set; }
        public string UB_CATEGORY { get; set; }
        public OrganizationUB FromXml(XElement elem)
        {
            if (elem.Element("UB_ID") != null)
                UB_ID = Convert.ToInt32(elem.Element("UB_ID").Value);
            if (elem.Element("UB_REGISTER_NO") != null)
                UB_REGISTER_NO = elem.Element("UB_REGISTER_NO").Value;
            if (elem.Element("UB_NAME") != null)
                UB_NAME = elem.Element("UB_NAME").Value;
            if (elem.Element("UB_DOCUMENT_NO") != null)
                UB_DOCUMENT_NO = elem.Element("UB_DOCUMENT_NO").Value;
            if (elem.Element("UB_REG_DATE") != null)
                UB_REG_DATE = Convert.ToDateTime(elem.Element("UB_REG_DATE").Value);
            if (elem.Element("UB_CATEGORY") != null)
                UB_CATEGORY = elem.Element("UB_CATEGORY").Value;
            return this;
        }
    }
    public class OrganizationMOF
    {
        public int MOF_ID { get; set; }
        public string MOF_REGISTER_NO { get; set; }
        public string MOF_NAME { get; set; }
        public string MOF_TEZ { get; set; }
        public string MOF_TTZ { get; set; }
        public string MOF_TSHZ { get; set; }
        public string MOF_SALBAR { get; set; }
        public string MOF_BUDGET_TYPE { get; set; }
        public string MOF_AIMAG { get; set; }
        public string MOF_SUM { get; set; }
        public string MOF_MAIN_ACCOUNT { get; set; }
        public string MOF_EXTEND_ACCOUNT { get; set; }
        public OrganizationMOF FromXml(XElement elem)
        {
            if (elem.Element("MOF_ID") != null)
                MOF_ID = Convert.ToInt32(elem.Element("MOF_ID").Value);
            if (elem.Element("MOF_REGISTER_NO") != null)
                MOF_REGISTER_NO = elem.Element("MOF_REGISTER_NO").Value;
            if (elem.Element("MOF_NAME") != null)
                MOF_NAME = elem.Element("MOF_NAME").Value;
            if (elem.Element("MOF_TEZ") != null)
                MOF_TEZ = elem.Element("MOF_TEZ").Value;
            if (elem.Element("MOF_TTZ") != null)
                MOF_TTZ = elem.Element("MOF_TTZ").Value;
            if (elem.Element("MOF_TSHZ") != null)
                MOF_TSHZ = elem.Element("MOF_TSHZ").Value;
            if (elem.Element("MOF_SALBAR") != null)
                MOF_SALBAR = elem.Element("MOF_SALBAR").Value;
            if (elem.Element("MOF_BUDGET_TYPE") != null)
                MOF_BUDGET_TYPE = elem.Element("MOF_BUDGET_TYPE").Value;
            if (elem.Element("MOF_AIMAG") != null)
                MOF_AIMAG = elem.Element("MOF_AIMAG").Value;
            if (elem.Element("MOF_SUM") != null)
                MOF_SUM = elem.Element("MOF_SUM").Value;
            if (elem.Element("MOF_MAIN_ACCOUNT") != null)
                MOF_MAIN_ACCOUNT = elem.Element("MOF_MAIN_ACCOUNT").Value;
            if (elem.Element("MOF_EXTEND_ACCOUNT") != null)
                MOF_EXTEND_ACCOUNT = elem.Element("MOF_EXTEND_ACCOUNT").Value;

            return this;
        }
    }
    public class OrganizationTAX
    {
        public int TAX_ID { get; set; }
        public string GDSR_NUMBER { get; set; }
        public string COMPANY_REG_NO { get; set; }
        public string LEGAL_NAME { get; set; }
        public int LEGAL_STATUS { get; set; }
        public string LEGAL_STATUS_NAME { get; set; }
        public DateTime COMPANY_REG_DATE { get; set; }
        public int PROPERTY_TYPE { get; set; }
        public string PROPERTY_TYPE_NAME { get; set; }
        public string NUMBER_FOUNDERS { get; set; }
        public string OPERATION { get; set; }
        public int SOFF_OFF_CODE { get; set; }
        public string OFF_NAME { get; set; }
        public int SOFF_CODE { get; set; }
        public string SOFF_NAME { get; set; }
        public string SECTOR { get; set; }
        public string SECTOR_CODE { get; set; }
        public string SECTOR_NAME { get; set; }
        public string SUB_SECTOR { get; set; }
        public string SUB_SECTOR_CODE_NAME { get; set; }
        public string ELEMENT { get; set; }
        public string ELEMENT_NAME { get; set; }
        public string DIVISION { get; set; }
        public string DIVISION_NAME { get; set; }
        public string REGION { get; set; }
        public OrganizationTAX FromXml(XElement elem)
        {
            if (elem.Element("TAX_ID") != null)
                TAX_ID = Convert.ToInt32(elem.Element("TAX_ID").Value);
            if (elem.Element("GDSR_NUMBER") != null)
                GDSR_NUMBER = elem.Element("GDSR_NUMBER").Value;
            if (elem.Element("COMPANY_REG_NO") != null)
                COMPANY_REG_NO = elem.Element("COMPANY_REG_NO").Value;
            if (elem.Element("LEGAL_NAME") != null)
                LEGAL_NAME = elem.Element("LEGAL_NAME").Value;
            if (elem.Element("LEGAL_STATUS") != null)
                LEGAL_STATUS = Convert.ToInt32(elem.Element("LEGAL_STATUS").Value);
            if (elem.Element("LEGAL_STATUS_NAME") != null)
                LEGAL_STATUS_NAME = elem.Element("LEGAL_STATUS_NAME").Value;
            if (elem.Element("COMPANY_REG_DATE") != null)
                COMPANY_REG_DATE = Convert.ToDateTime(elem.Element("COMPANY_REG_DATE").Value);
            if (elem.Element("PROPERTY_TYPE") != null)
                PROPERTY_TYPE = Convert.ToInt32(elem.Element("PROPERTY_TYPE").Value);
            if (elem.Element("PROPERTY_TYPE_NAME") != null)
                PROPERTY_TYPE_NAME = elem.Element("PROPERTY_TYPE_NAME").Value;
            if (elem.Element("NUMBER_FOUNDERS") != null)
                NUMBER_FOUNDERS = elem.Element("NUMBER_FOUNDERS").Value;
            if (elem.Element("OPERATION") != null)
                OPERATION = elem.Element("OPERATION").Value;
            if (elem.Element("SOFF_OFF_CODE") != null)
                SOFF_OFF_CODE = Convert.ToInt32(elem.Element("SOFF_OFF_CODE").Value);
            if (elem.Element("OFF_NAME") != null)
                OFF_NAME = elem.Element("OFF_NAME").Value;
            if (elem.Element("SOFF_CODE") != null)
                SOFF_CODE = Convert.ToInt32(elem.Element("SOFF_CODE").Value);
            if (elem.Element("SOFF_NAME") != null)
                SOFF_NAME = elem.Element("SOFF_NAME").Value;
            if (elem.Element("SECTOR") != null)
                SECTOR = elem.Element("SECTOR").Value;
            if (elem.Element("SECTOR_CODE") != null)
                SECTOR_CODE = elem.Element("SECTOR_CODE").Value;
            if (elem.Element("SECTOR_NAME") != null)
                SECTOR_NAME = elem.Element("SECTOR_NAME").Value;
            if (elem.Element("SUB_SECTOR") != null)
                SUB_SECTOR = elem.Element("SUB_SECTOR").Value;
            if (elem.Element("SUB_SECTOR_CODE_NAME") != null)
                SUB_SECTOR_CODE_NAME = elem.Element("SUB_SECTOR_CODE_NAME").Value;
            if (elem.Element("ELEMENT") != null)
                ELEMENT = elem.Element("ELEMENT").Value;
            if (elem.Element("ELEMENT_NAME") != null)
                ELEMENT_NAME = elem.Element("ELEMENT_NAME").Value;
            if (elem.Element("DIVISION") != null)
                DIVISION = elem.Element("DIVISION").Value;
            if (elem.Element("DIVISION_NAME") != null)
                DIVISION_NAME = elem.Element("DIVISION_NAME").Value;
            if (elem.Element("REGION") != null)
                REGION = elem.Element("REGION").Value;

            return this;
        }
    }
    public class OrgVM
    {
        public List<MenuRole> menuRoles { get; set; }
        public int DeparmentID { get; set; }
        public int[] StatusIDs { get; set; }
        public MultiSelectList Status { get; set; }
        public int[] ViolationIDs { get; set; }
        public MultiSelectList Violation { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Status> statuses { get; set; } = new List<Status>();
        public List<Violation> violations { get; set; } = new List<Violation>();
        public List<Office> offices { get; set; } = new List<Office>();
        public List<SubOffice> subOffices { get; set; } = new List<SubOffice>();

        public List<BudgetType> budgetTypes { get; set; } = new List<BudgetType>();
        public List<ActivityLib> activities { get; set; } = new List<ActivityLib>();
        public List<SubBudgetType> subBudgetTypes { get; set; } = new List<SubBudgetType>();
        public List<Committee> committees { get; set; } = new List<Committee>();
        public List<TaxOffice> taxOffices { get; set; } = new List<TaxOffice>();
        public List<CostType> costTypes { get; set; } = new List<CostType>();
        public List<InsuranceOffice> insuranceOffices { get; set; } = new List<InsuranceOffice>();
        public List<FinOffice> finOffices { get; set; } = new List<FinOffice>();
        public List<FinancingType> financingTypes { get; set; } = new List<FinancingType>();
        public List<Bank> banks { get; set; } = new List<Bank>();

    }
    public class OrgList
    {
        public int ORG_ID { get;set; }
        public int ORG_DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string ORG_REGISTER_NO { get; set; }
        public string ORG_NAME { get; set; }
        public string ORG_CODE { get; set; }
        public int ORG_BUDGET_TYPE_ID { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }
        public string VIOLATION_DETAIL { get; set; }
        public int ORG_CONCENTRATOR_ID { get; set; }
        public string ORG_CONCENTRATOR_NAME { get; set; }
        public int ORG_STATUS_ID { get; set; }
        public string STATUS_NAME { get; set; }
        public string INFORMATION_DETAIL { get; set; }
        public OrgList FromXml(XElement elem)
        {
            if (elem.Element("ORG_ID") != null)
                ORG_ID = Convert.ToInt32(elem.Element("ORG_ID").Value);
            if (elem.Element("ORG_DEPARTMENT_ID") != null)
                ORG_DEPARTMENT_ID = Convert.ToInt32(elem.Element("ORG_DEPARTMENT_ID").Value);
            if (elem.Element("DEPARTMENT_NAME") != null)
                DEPARTMENT_NAME = elem.Element("DEPARTMENT_NAME").Value;
            if (elem.Element("ORG_REGISTER_NO") != null)
                ORG_REGISTER_NO = elem.Element("ORG_REGISTER_NO").Value;
            if (elem.Element("ORG_NAME") != null)
                ORG_NAME = elem.Element("ORG_NAME").Value;
            if (elem.Element("ORG_CODE") != null)
                ORG_CODE = elem.Element("ORG_CODE").Value;
            if (elem.Element("ORG_BUDGET_TYPE_ID") != null)
                ORG_BUDGET_TYPE_ID = Convert.ToInt32(elem.Element("ORG_BUDGET_TYPE_ID").Value);
            if (elem.Element("BUDGET_TYPE_NAME") != null)
                BUDGET_TYPE_NAME = elem.Element("BUDGET_TYPE_NAME").Value;
            if (elem.Element("VIOLATION_DETAIL") != null)
                VIOLATION_DETAIL = elem.Element("VIOLATION_DETAIL").Value;
            if (elem.Element("ORG_CONCENTRATOR_ID") != null)
                ORG_CONCENTRATOR_ID = Convert.ToInt32(elem.Element("ORG_CONCENTRATOR_ID").Value);
            if (elem.Element("ORG_CONCENTRATOR_NAME") != null)
                ORG_CONCENTRATOR_NAME = elem.Element("ORG_CONCENTRATOR_NAME").Value;
            if (elem.Element("ORG_STATUS_ID") != null)
                ORG_STATUS_ID = Convert.ToInt32(elem.Element("ORG_STATUS_ID").Value);
            if (elem.Element("STATUS_NAME") != null)
                STATUS_NAME = elem.Element("STATUS_NAME").Value;
            if (elem.Element("INFORMATION_DETAIL") != null)
                INFORMATION_DETAIL = elem.Element("INFORMATION_DETAIL").Value;

            return this;
        }
    }
    public class OrganizationDelete
    {
        public int ORG_ID { get; set; }
        public DateTime? P_IDATE { get; set; }
        public int P_REASONID { get; set; }
        public string P_REASONDESC { get; set; }
        public int P_PARENTID { get; set; }
        public List<Reason> reasons { get; set; } = new List<Reason>();
        public XElement ToXml()
        {
            return new XElement("Organization",
                       new XElement("ORG_ID", ORG_ID),
                       new XElement("ORG_REG_DATE", P_IDATE != null ? ((DateTime)P_IDATE).ToString("yyyy/MM/dd") : null),
                       new XElement("P_REASONID", P_REASONID),
                       new XElement("P_REASONDESC", P_REASONDESC),
                       new XElement("P_PARENTID", P_PARENTID)
                       );
        }
    }

    public class Tab1
    {
        public string MD_CODE { get; set; }
        public string MD_LAWS_NUM { get; set; }
        public string MD_NAME { get; set; }
        public string MD_TIME { get; set; }
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
        public double Data01 { get; set; }
        public string Data02 { get; set; }
        public string Data03 { get; set; }
    }

    public class Tab3
    {
        public string MD_CODE { get; set; }
        public string MD_LAWS_NUM { get; set; }
        public string MD_NAME { get; set; }
        public string MD_TIME { get; set; }
        public double Data01 { get; set; }
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
        public double Data01 { get; set; }
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
        public double Data01 { get; set; }
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
        public int ORG_ID { get; set; }

    }
}