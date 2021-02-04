using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class Organization
    {
        public int OrgID { get; set; }
        [Required(ErrorMessage = "Дугаар оруулна уу.")]
        public string ORG_CODE { get; set; }
        [Required(ErrorMessage = "Регистрийн дугаар оруулна уу.")]
        public string REGISTER_NO { get; set; }
        [Required(ErrorMessage = "УБ-ийн дугаар оруулна уу.")]
        public string UB_NUMBER { get; set; }
        [Required(ErrorMessage = "Нэр оруулна уу.")]
        public string ORG_NAME { get; set; }
        private DateTime? _REG_DATE;
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Байгуулагдсан огноо оруулна уу.")]
        public DateTime REG_DATE {
            get
            {
                if (_REG_DATE.HasValue)
                    return _REG_DATE.Value;
                else
                    return DateTime.Now;
            }
            set
            {
                _REG_DATE = value;
            }
        }
        [Required(ErrorMessage = "Утас оруулна уу.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Тоон утга оруулна уу.")]
        public string ORG_PHONE { get; set; }
        [Required(ErrorMessage = "И-мэйл оруулна уу.")]
        public string EMAIL { get; set; }
        [Required(ErrorMessage = "Аймаг/нийслэл оруулна уу.")]
        public int AimagID { get; set; }
        [Required(ErrorMessage = "Сум/дүүрэг оруулна уу.")]
        public int SoumID { get; set; }
        [Required(ErrorMessage = "Албан ёсны хаяг оруулна уу.")]
        public string ORG_ADDRESS { get; set; }
        [Required(ErrorMessage = "Цахим хуудас оруулна уу.")]
        public string WEBSITE { get; set; }
        [Required(ErrorMessage = "Факс оруулна уу.")]
        public string FAX { get; set; }

        public List<OrganizationBank> banks { get; set; }

        [Required(ErrorMessage = "Санхүүжилтийн хэлбэр сонгоно уу.")]
        public int SankhuujiltID { get; set; }
        [Required(ErrorMessage = "Төсөв захирагч сонгоно уу.")]
        public int TusuwZakhiragchID { get; set; }
        [Required(ErrorMessage = "Хэлбэр сонгоно уу.")]
        public int KhelberID { get; set; }
        [Required(ErrorMessage = "Алба сонгоно уу.")]
        public int AlbaID { get; set; }
        [Required(ErrorMessage = "Харъяа байнгын хороо сонгоно уу.")]
        public int KhorooID { get; set; }
        [Required(ErrorMessage = "Төсвийн зарлагын хөтөлбөрийн ангилал сонгоно уу.")]
        public int ZardlinAngilalID { get; set; }
        [Required(ErrorMessage = "Үйл ажиллагааны ангилал сонгоно уу.")]
        public int UilAjillagaaID { get; set; }
        [Required(ErrorMessage = "Санхүүгийн тайлан тушаадаг газар сонгоно уу.")]
        public int SankhuuTailanID { get; set; }
        [Required(ErrorMessage = "Харилцагч татварын байгууллага сонгоно уу.")]
        public int TatwarID { get; set; }
        [Required(ErrorMessage = "Харьцдаг НД байгууллага байгууллага сонгоно уу.")]
        public int ND_BaiguullagaID { get; set; }
        public string Description { get; set; }

        public int InActiveReasonID { get; set; }
        public int ParentID { get; set; }
        public int IsActive { get; set; }

        public int HEAD_PERSONID { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string HEAD_ROLE { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string HEAD_FIRSTNAME { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string HEAD_LASTNAME { get; set; }
        public string HEAD_FIRST_LETTER_REGISTER { get; set; }
        public string HEAD_LAST_LETTER_REGISTER { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Регистрийн дугаар зөв оруулна уу.")]
        public string HEAD_REGISTER { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string HEAD_PHONE { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string HEAD_EMAIL { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public float HEAD_YEAR { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string HEAD_PROFESSION { get; set; }
        public bool HEAD_ISACTIVE { get; set; }

        public int ACCOUNTANT_PERSONID { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ACCOUNTANT_ROLE { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ACCOUNTANT_FIRSTNAME { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ACCOUNTANT_LASTNAME { get; set; }
        public string ACCOUNTANT_FIRST_LETTER_REGISTER { get; set; }
        public string ACCOUNTANT_LAST_LETTER_REGISTER { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Регистрийн дугаар зөв оруулна уу.")]
        public string ACCOUNTANT_REGISTER { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ACCOUNTANT_PHONE { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ACCOUNTANT_EMAIL { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public float ACCOUNTANT_YEAR { get; set; }
        [Required(ErrorMessage = "Утга оруулна уу.")]
        public string ACCOUNTANT_PROFESSION { get; set; }
        public bool ACCOUNTANT_ISACTIVE { get; set; }
        public Organization FromXml(XElement elem)
        {
            if (elem.Element("OrgID") != null)
                OrgID = Convert.ToInt32(elem.Element("OrgID").Value);
            if (elem.Element("ORG_CODE") != null)
                ORG_CODE = elem.Element("ORG_CODE").Value;
            if (elem.Element("REGISTER_NO") != null)
                REGISTER_NO = elem.Element("REGISTER_NO").Value;
            if (elem.Element("UB_NUMBER") != null)
                UB_NUMBER = elem.Element("UB_NUMBER").Value;
            if (elem.Element("ORG_NAME") != null)
                ORG_NAME = elem.Element("ORG_NAME").Value;
            if (elem.Element("REG_DATE") != null)
                REG_DATE = Convert.ToDateTime(elem.Element("REG_DATE").Value);
            if (elem.Element("ORG_PHONE") != null)
                ORG_PHONE = elem.Element("ORG_PHONE").Value;
            if (elem.Element("AimagID") != null)
                AimagID = Convert.ToInt32(elem.Element("AimagID").Value);
            if (elem.Element("SoumID") != null)
                SoumID = Convert.ToInt32(elem.Element("SoumID").Value);
            if (elem.Element("ORG_ADDRESS") != null)
                ORG_ADDRESS = elem.Element("ORG_ADDRESS").Value;
            if (elem.Element("WEBSITE") != null)
                WEBSITE = elem.Element("WEBSITE").Value;
            if (elem.Element("EMAIL") != null)
                EMAIL = elem.Element("EMAIL").Value;
            if (elem.Element("FAX") != null)
                FAX = elem.Element("FAX").Value;
            if (elem.Element("OrgBank") != null)
                banks = (from item in elem.Elements("OrgBank") select new OrganizationBank().FromXml(item)).ToList();

            if (elem.Element("TusuwZakhiragchID") != null)
                TusuwZakhiragchID = Convert.ToInt32(elem.Element("TusuwZakhiragchID").Value);
            if (elem.Element("KhelberID") != null)
                KhelberID = Convert.ToInt32(elem.Element("KhelberID").Value);
            if (elem.Element("KhorooID") != null)
                KhorooID = Convert.ToInt32(elem.Element("KhorooID").Value);
            if (elem.Element("ZardlinAngilalID") != null)
                ZardlinAngilalID = Convert.ToInt32(elem.Element("ZardlinAngilalID").Value);
            if (elem.Element("SankhuujiltID") != null)
                SankhuujiltID = Convert.ToInt32(elem.Element("SankhuujiltID").Value);
            if (elem.Element("UilAjillagaaID") != null)
                UilAjillagaaID = Convert.ToInt32(elem.Element("UilAjillagaaID").Value);
            if (elem.Element("AlbaID") != null)
                AlbaID = Convert.ToInt32(elem.Element("AlbaID").Value);
            if (elem.Element("TatwarID") != null)
                TatwarID = Convert.ToInt32(elem.Element("TatwarID").Value);
            if (elem.Element("ND_BaiguullagaID") != null)
                ND_BaiguullagaID = Convert.ToInt32(elem.Element("ND_BaiguullagaID").Value);
            if (elem.Element("Description") != null)
                Description = elem.Element("Description").Value;

            return this;
        }
        public XElement ToXml()
        {
            return new XElement("Organization",
                       new XElement("OrgID", OrgID),
                       new XElement("ORG_CODE", ORG_CODE),
                       new XElement("REGISTER_NO", REGISTER_NO),
                       new XElement("UB_NUMBER", UB_NUMBER),
                       new XElement("ORG_NAME", ORG_NAME),
                       new XElement("REG_DATE", REG_DATE),
                       new XElement("ORG_PHONE", ORG_PHONE),
                       new XElement("EMAIL", EMAIL),
                       new XElement("AimagID", AimagID),
                       new XElement("SoumID", SoumID),
                       new XElement("ORG_ADDRESS", ORG_ADDRESS),
                       new XElement("WEBSITE", WEBSITE),
                       new XElement("FAX", FAX),

                       new XElement("TusuwZakhiragchID", TusuwZakhiragchID),
                       new XElement("KhelberID", KhelberID),
                       new XElement("KhorooID", KhorooID),
                       new XElement("ZardlinAngilalID", ZardlinAngilalID),
                       new XElement("SankhuujiltID", SankhuujiltID),
                       new XElement("UilAjillagaaID", UilAjillagaaID),
                       new XElement("AlbaID", AlbaID),
                       new XElement("TatwarID", TatwarID),
                       new XElement("ND_BaiguullagaID", ND_BaiguullagaID),
                       new XElement("Description", Description),

                       new XElement("HEAD_PERSONID", HEAD_PERSONID),
                       new XElement("HEAD_ROLE", HEAD_ROLE),
                       new XElement("HEAD_FIRSTNAME", HEAD_FIRSTNAME),
                       new XElement("HEAD_LASTNAME", HEAD_LASTNAME),
                       new XElement("HEAD_PHONE", HEAD_PHONE),
                       new XElement("HEAD_EMAIL", HEAD_EMAIL),
                       new XElement("HEAD_YEAR", HEAD_YEAR),
                       new XElement("HEAD_PROFESSION", HEAD_PROFESSION),
                       new XElement("HEAD_REGISTER", HEAD_FIRST_LETTER_REGISTER + HEAD_LAST_LETTER_REGISTER + HEAD_REGISTER),

                       new XElement("ACCOUNTANT_PERSONID", ACCOUNTANT_PERSONID),
                       new XElement("ACCOUNTANT_ROLE", ACCOUNTANT_ROLE),
                       new XElement("ACCOUNTANT_FIRSTNAME", ACCOUNTANT_FIRSTNAME),
                       new XElement("ACCOUNTANT_LASTNAME", ACCOUNTANT_LASTNAME),
                       new XElement("ACCOUNTANT_PHONE", ACCOUNTANT_PHONE),
                       new XElement("ACCOUNTANT_EMAIL", ACCOUNTANT_EMAIL),
                       new XElement("ACCOUNTANT_YEAR", ACCOUNTANT_YEAR),
                       new XElement("ACCOUNTANT_PROFESSION", ACCOUNTANT_PROFESSION),
                       new XElement("ACCOUNTANT_REGISTER", ACCOUNTANT_FIRST_LETTER_REGISTER + ACCOUNTANT_LAST_LETTER_REGISTER + ACCOUNTANT_REGISTER),

                       new XElement("BankList",
                           from item in banks
                           select new XElement("OrganizationBank",
                                      new XElement("OrgID", item.OrgID),
                                      new XElement("BankID", item.BankID),
                                      new XElement("BankAccount", item.BankAccount),
                                      new XElement("Description", item.Description))));
        }
    }
    public class OrganizationBank
    {
        public int OrgID { get; set; }
        public int BankID { get; set; }
        public string BankAccount { get; set; }
        public string Description { get; set; }
        public OrganizationBank FromXml(XElement elem)
        {
            if (elem.Element("OrgID") != null)
                OrgID = Convert.ToInt32(elem.Element("OrgID").Value);
            if (elem.Element("BankID") != null)
                BankID = Convert.ToInt32(elem.Element("BankID").Value);
            if (elem.Element("BankAccount") != null)
                BankAccount = elem.Element("BankAccount").Value;
            if (elem.Element("Description") != null)
                Description = elem.Element("Description").Value;
            return this;
        }
    }
    public class OrganizationPerson
    {
        public int OrgID { get; set; }
        public int PersonID { get; set; }
        public string ROLE { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string REGISTER { get; set; }
        public string Phone { get; set; }
        public string EMAIL { get; set; }
        public float YEAR { get; set; }
        public string PROFESSION { get; set; }
        public int PersonType { get; set; }
        public OrganizationPerson FromXml(XElement elem)
        {
            if (elem.Element("OrgID") != null)
                OrgID = Convert.ToInt32(elem.Element("OrgID").Value);
            if (elem.Element("PersonID") != null)
                PersonID = Convert.ToInt32(elem.Element("PersonID").Value);
            if (elem.Element("ROLE") != null)
                ROLE = elem.Element("ROLE").Value;
            if (elem.Element("FIRSTNAME") != null)
                FIRSTNAME = elem.Element("FIRSTNAME").Value;
            if (elem.Element("LASTNAME") != null)
                LASTNAME = elem.Element("LASTNAME").Value;
            if (elem.Element("REGISTER") != null)
                REGISTER = elem.Element("REGISTER").Value;
            if (elem.Element("Phone") != null)
                Phone = elem.Element("Phone").Value;
            if (elem.Element("EMAIL") != null)
                EMAIL = elem.Element("EMAIL").Value;
            if (elem.Element("YEAR") != null)
                YEAR = float.Parse(elem.Element("YEAR").Value);
            if (elem.Element("PROFESSION") != null)
                PROFESSION = elem.Element("PROFESSION").Value;
            if (elem.Element("PersonType") != null)
                PersonType = Convert.ToInt32(elem.Element("PersonType").Value);

            return this;
        }
    }
}