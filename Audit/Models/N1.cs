using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Audit.Models
{
    public class N1VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class N1DF
    {
        
        public List<N1> n1Detail { get; set; } = new List<N1>();
        public List<N1> n1Footer { get; set; } = new List<N1>();
    }
    public class N1
    {

        public string ORGID { get; set; }
        //[Required(ErrorMessage = "N1 сонгоно уу.")]
        public int DEPARTMENT_ID { get; set; }
        public string INSERTUSERID { get; set; }
        public string ORGNAME { get; set; }
        public string ORGTYPE { get; set; }
        public string OPEN_HEAD_ROLE { get; set; }
        public string OPEN_HEAD_NAME { get; set; }
        public string OPEN_HEAD_PHONE { get; set; }
        public string OPEN_ACC_ROLE { get; set; }
        public string OPEN_ACC_NAME { get; set; }
        public string OPEN_ACC_PHONE { get; set; }


        public string MD1 { get; set; }
        public string MD2 { get; set; }
        public string MD3 { get; set; }
        public string MD4 { get; set; }
        public string MD5 { get; set; }
        public string MD6 { get; set; }
        public string MD7 { get; set; }
        public string MD8 { get; set; }
        public string MD9 { get; set; }
        public string MD10 { get; set; }
        public string MD11 { get; set; }
        public string MD12 { get; set; }
        public string MD13 { get; set; }
        public string MD14 { get; set; }
        public string MD15 { get; set; }
        public string MD16 { get; set; }
        public string MD17 { get; set; }
        public string MD18 { get; set; }
        public string MD19 { get; set; }
        public string MD20 { get; set; }
        public string MD21 { get; set; }
        public string MD22 { get; set; }
        public string MD23 { get; set; }
        public string MD24 { get; set; }
        public string MD25 { get; set; }
        public string MD26 { get; set; }
        public string MD27 { get; set; }
        public string MD28 { get; set; }
        public string MD29 { get; set; }
        public string MD30 { get; set; }
        public string MD31 { get; set; }
        public string MD32 { get; set; }
        public string MD33 { get; set; }
        public string MD34 { get; set; }
        public string MD35 { get; set; }


        public N1 SetXml(XElement xml)
        {
            if (xml != null)
            {
                if (xml.Element("ORGID") != null)
                    ORGID = xml.Element("ORGID").Value;
                if (xml.Element("DEPARTMENT_ID") != null)
                    DEPARTMENT_ID = Convert.ToInt32(xml.Element("DEPARTMENT_ID").Value);
                if (xml.Element("INSERTUSERID") != null)
                    INSERTUSERID = xml.Element("INSERTUSERID").Value;
                if (xml.Element("ORGNAME") != null)
                    ORGNAME = xml.Element("ORGNAME").Value;
                if (xml.Element("ORGTYPE") != null)
                    ORGTYPE = xml.Element("ORGTYPE").Value;
                if (xml.Element("OPEN_HEAD_ROLE") != null)
                    OPEN_HEAD_ROLE = xml.Element("OPEN_HEAD_ROLE").Value;
                if (xml.Element("OPEN_HEAD_NAME") != null)
                    OPEN_HEAD_NAME = xml.Element("OPEN_HEAD_NAME").Value;
                if (xml.Element("OPEN_HEAD_PHONE") != null)
                    OPEN_HEAD_PHONE = xml.Element("OPEN_HEAD_PHONE").Value !=null ? xml.Element("OPEN_HEAD_PHONE").Value.ToString() :"";
                if (xml.Element("OPEN_ACC_ROLE") != null)
                    OPEN_ACC_ROLE = xml.Element("OPEN_ACC_ROLE").Value;
                if (xml.Element("OPEN_ACC_NAME") != null)
                    OPEN_ACC_NAME = xml.Element("OPEN_ACC_NAME").Value;
                if (xml.Element("OPEN_ACC_PHONE") != null)
                    OPEN_ACC_PHONE = xml.Element("OPEN_ACC_PHONE").Value != null ? xml.Element("OPEN_ACC_PHONE").Value.ToString() : "";
                if (xml.Element("MD1") != null)
                    MD1 = xml.Element("MD1").Value != "0" ? xml.Element("MD1").Value.ToString() : "";
                if (xml.Element("MD2") != null)
                    MD2 = xml.Element("MD2").Value != "0" ? xml.Element("MD2").Value.ToString() : "";
                if (xml.Element("MD3") != null)
                    MD3 = xml.Element("MD3").Value != "0" ? xml.Element("MD3").Value.ToString() : "";
                if (xml.Element("MD4") != null)
                    MD4 = xml.Element("MD4").Value != "0" ? xml.Element("MD4").Value.ToString() : "";
                if (xml.Element("MD5") != null)
                    MD5 = xml.Element("MD5").Value != "0" ? xml.Element("MD5").Value.ToString() : "";
                if (xml.Element("MD6") != null)
                    MD6 = xml.Element("MD6").Value != "0" ? xml.Element("MD6").Value.ToString() : "";
                if (xml.Element("MD7") != null)
                    MD7 = xml.Element("MD7").Value != "0" ? xml.Element("MD7").Value.ToString() : "";
                if (xml.Element("MD8") != null)
                    MD8 = xml.Element("MD8").Value != "0" ? xml.Element("MD8").Value.ToString() : "";
                if (xml.Element("MD9") != null)
                    MD9 = xml.Element("MD9").Value != "0" ? xml.Element("MD9").Value.ToString() : "";
                if (xml.Element("MD10") != null)
                    MD10 = xml.Element("MD10").Value != "0" ? xml.Element("MD10").Value.ToString() : "";
                if (xml.Element("MD11") != null)
                    MD11 = xml.Element("MD11").Value != "0" ? xml.Element("MD11").Value.ToString() : "";
                if (xml.Element("MD12") != null)
                    MD12 = xml.Element("MD12").Value != "0" ? xml.Element("MD12").Value.ToString() : "";
                if (xml.Element("MD13") != null)
                    MD13 = xml.Element("MD13").Value != "0" ? xml.Element("MD13").Value.ToString() : "";
                if (xml.Element("MD14") != null)
                    MD14 = xml.Element("MD14").Value != "0" ? xml.Element("MD14").Value.ToString() : "";
                if (xml.Element("MD15") != null)
                    MD15 = xml.Element("MD15").Value != "0" ? xml.Element("MD15").Value.ToString() : "";
                if (xml.Element("MD16") != null)
                    MD16 = xml.Element("MD16").Value != "0" ? xml.Element("MD16").Value.ToString() : "";
                if (xml.Element("MD17") != null)
                    MD17 = xml.Element("MD17").Value != "0" ? xml.Element("MD17").Value.ToString() : "";
                if (xml.Element("MD18") != null)
                    MD18 = xml.Element("MD18").Value != "0" ? xml.Element("MD18").Value.ToString() : "";
                if (xml.Element("MD19") != null)
                    MD19 = xml.Element("MD19").Value != "0" ? xml.Element("MD19").Value.ToString() : "";
                if (xml.Element("MD20") != null)
                    MD20 = xml.Element("MD20").Value != "0" ? xml.Element("MD20").Value.ToString() : "";
                if (xml.Element("MD21") != null)
                    MD21 = xml.Element("MD21").Value != "0" ? xml.Element("MD21").Value.ToString() : "";
                if (xml.Element("MD22") != null)
                    MD22 = xml.Element("MD22").Value != "0" ? xml.Element("MD22").Value.ToString() : "";
                if (xml.Element("MD23") != null)
                    MD23 = xml.Element("MD23").Value != "0" ? xml.Element("MD23").Value.ToString() : "";
                if (xml.Element("MD24") != null)
                    MD24 = xml.Element("MD24").Value != "0" ? xml.Element("MD24").Value.ToString() : "";
                if (xml.Element("MD25") != null)
                    MD25 = xml.Element("MD25").Value != "0" ? xml.Element("MD25").Value.ToString() : "";
                if (xml.Element("MD26") != null)
                    MD26 = xml.Element("MD26").Value != "0" ? xml.Element("MD26").Value.ToString() : "";
                if (xml.Element("MD27") != null)
                    MD27 = xml.Element("MD27").Value != "0" ? xml.Element("MD27").Value.ToString() : "";
                if (xml.Element("MD28") != null)
                    MD28 = xml.Element("MD28").Value != "0" ? xml.Element("MD28").Value.ToString() : "";
                if (xml.Element("MD29") != null)
                    MD29 = xml.Element("MD29").Value != "0" ? xml.Element("MD29").Value.ToString() : "";
                if (xml.Element("MD30") != null)
                    MD30 = xml.Element("MD30").Value != "0" ? xml.Element("MD30").Value.ToString() : "";
                if (xml.Element("MD31") != null)
                    MD31 = xml.Element("MD31").Value != "0" ? xml.Element("MD31").Value.ToString() : "";
                if (xml.Element("MD32") != null)
                    MD32 = xml.Element("MD32").Value != "0" ? xml.Element("MD32").Value.ToString() : "";
                if (xml.Element("MD33") != null)
                    MD33 = xml.Element("MD33").Value != "0" ? xml.Element("MD33").Value.ToString() : "";
                if (xml.Element("MD34") != null)
                    MD34 = xml.Element("MD34").Value != "0" ? xml.Element("MD34").Value.ToString() : "";
                if (xml.Element("MD35") != null)
                    MD35 = xml.Element("MD35").Value != "0" ? xml.Element("MD35").Value.ToString() : "";


            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("N1",
                       new XElement("ORGID ", ORGID),
                       new XElement("ORGNAME ", ORGNAME),
                       new XElement("ORGTYPE ", ORGTYPE),
                       //new XElement("AUDIT_YEAR", AUDIT_YEAR),
                       //new XElement("AUDIT_TYPE", AUDIT_TYPE),
                       //new XElement("AUDIT_CODE", AUDIT_CODE),
                       //new XElement("AUDIT_NAME", AUDIT_NAME),
                       //new XElement("AUDIT_BUDGET_TYPE", AUDIT_BUDGET_TYPE),
                       //new XElement("CORRECTED_ERROR_DESC", CORRECTED_ERROR_DESC),
                       //new XElement("CORRECTED_ERROR_TYPE", CORRECTED_ERROR_TYPE),
                       //new XElement("CORRECTED_COUNT", CORRECTED_COUNT),
                       //new XElement("CORRECTED_AMOUNT", CORRECTED_AMOUNT),
                       //new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("OPEN_HEAD_ROLE ", OPEN_HEAD_ROLE),
                       new XElement("OPEN_HEAD_NAME ", OPEN_HEAD_NAME)
                       );
        }


    }
}
