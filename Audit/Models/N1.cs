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
                if (xml.Element("MD36") != null)
                    MD36 = xml.Element("MD36").Value != "0" ? xml.Element("MD36").Value.ToString() : "";
                if (xml.Element("MD37") != null)
                    MD37 = xml.Element("MD37").Value != "0" ? xml.Element("MD37").Value.ToString() : "";
                if (xml.Element("MD38") != null)
                    MD38 = xml.Element("MD38").Value != "0" ? xml.Element("MD38").Value.ToString() : "";
                if (xml.Element("MD39") != null)
                    MD39 = xml.Element("MD39").Value != "0" ? xml.Element("MD39").Value.ToString() : "";
                if (xml.Element("MD40") != null)
                    MD40 = xml.Element("MD40").Value != "0" ? xml.Element("MD40").Value.ToString() : "";
                if (xml.Element("MD41") != null)
                    MD41 = xml.Element("MD41").Value != "0" ? xml.Element("MD41").Value.ToString() : "";
                if (xml.Element("MD42") != null)
                    MD42 = xml.Element("MD42").Value != "0" ? xml.Element("MD42").Value.ToString() : "";
                if (xml.Element("MD43") != null)
                    MD43 = xml.Element("MD43").Value != "0" ? xml.Element("MD43").Value.ToString() : "";
                if (xml.Element("MD44") != null)
                    MD44 = xml.Element("MD44").Value != "0" ? xml.Element("MD44").Value.ToString() : "";
                if (xml.Element("MD45") != null)
                    MD45 = xml.Element("MD45").Value != "0" ? xml.Element("MD45").Value.ToString() : "";
                if (xml.Element("MD46") != null)
                    MD46 = xml.Element("MD46").Value != "0" ? xml.Element("MD46").Value.ToString() : "";
                if (xml.Element("MD47") != null)
                    MD47 = xml.Element("MD47").Value != "0" ? xml.Element("MD47").Value.ToString() : "";
                if (xml.Element("MD48") != null)
                    MD48 = xml.Element("MD48").Value != "0" ? xml.Element("MD48").Value.ToString() : "";
                if (xml.Element("MD49") != null)
                    MD49 = xml.Element("MD49").Value != "0" ? xml.Element("MD49").Value.ToString() : "";
                if (xml.Element("MD50") != null)
                    MD50 = xml.Element("MD50").Value != "0" ? xml.Element("MD50").Value.ToString() : "";
                if (xml.Element("MD51") != null)
                    MD51 = xml.Element("MD51").Value != "0" ? xml.Element("MD51").Value.ToString() : "";
                if (xml.Element("MD52") != null)
                    MD52 = xml.Element("MD52").Value != "0" ? xml.Element("MD52").Value.ToString() : "";
                if (xml.Element("MD53") != null)
                    MD53 = xml.Element("MD53").Value != "0" ? xml.Element("MD53").Value.ToString() : "";
                if (xml.Element("MD54") != null)
                    MD54 = xml.Element("MD54").Value != "0" ? xml.Element("MD54").Value.ToString() : "";
                if (xml.Element("MD55") != null)
                    MD55 = xml.Element("MD55").Value != "0" ? xml.Element("MD55").Value.ToString() : "";
                if (xml.Element("MD56") != null)
                    MD56 = xml.Element("MD56").Value != "0" ? xml.Element("MD56").Value.ToString() : "";
                if (xml.Element("MD57") != null)
                    MD57 = xml.Element("MD57").Value != "0" ? xml.Element("MD57").Value.ToString() : "";
                if (xml.Element("MD58") != null)
                    MD58 = xml.Element("MD58").Value != "0" ? xml.Element("MD58").Value.ToString() : "";
                if (xml.Element("MD59") != null)
                    MD59 = xml.Element("MD59").Value != "0" ? xml.Element("MD59").Value.ToString() : "";
                if (xml.Element("MD60") != null)
                    MD60 = xml.Element("MD60").Value != "0" ? xml.Element("MD60").Value.ToString() : "";
                if (xml.Element("MD61") != null)
                    MD61 = xml.Element("MD61").Value != "0" ? xml.Element("MD61").Value.ToString() : "";
                if (xml.Element("MD62") != null)
                    MD62 = xml.Element("MD62").Value != "0" ? xml.Element("MD62").Value.ToString() : "";
                if (xml.Element("MD63") != null)
                    MD63 = xml.Element("MD63").Value != "0" ? xml.Element("MD63").Value.ToString() : "";
                if (xml.Element("MD64") != null)
                    MD64 = xml.Element("MD64").Value != "0" ? xml.Element("MD64").Value.ToString() : "";
                if (xml.Element("MD65") != null)
                    MD65 = xml.Element("MD65").Value != "0" ? xml.Element("MD65").Value.ToString() : "";
                if (xml.Element("MD66") != null)
                    MD66 = xml.Element("MD66").Value != "0" ? xml.Element("MD66").Value.ToString() : "";
                if (xml.Element("MD67") != null)
                    MD67 = xml.Element("MD67").Value != "0" ? xml.Element("MD67").Value.ToString() : "";
                if (xml.Element("MD68") != null)
                    MD68 = xml.Element("MD68").Value != "0" ? xml.Element("MD68").Value.ToString() : "";
                if (xml.Element("MD69") != null)
                    MD69 = xml.Element("MD69").Value != "0" ? xml.Element("MD69").Value.ToString() : "";
                if (xml.Element("MD70") != null)
                    MD70 = xml.Element("MD70").Value != "0" ? xml.Element("MD70").Value.ToString() : "";
                if (xml.Element("MD71") != null)
                    MD71 = xml.Element("MD71").Value != "0" ? xml.Element("MD71").Value.ToString() : "";
                if (xml.Element("MD72") != null)
                    MD72 = xml.Element("MD72").Value != "0" ? xml.Element("MD72").Value.ToString() : "";
                if (xml.Element("MD73") != null)
                    MD73 = xml.Element("MD73").Value != "0" ? xml.Element("MD73").Value.ToString() : "";
                if (xml.Element("MD74") != null)
                    MD74 = xml.Element("MD74").Value != "0" ? xml.Element("MD74").Value.ToString() : "";
                if (xml.Element("MD75") != null)
                    MD75 = xml.Element("MD75").Value != "0" ? xml.Element("MD75").Value.ToString() : "";
                if (xml.Element("MD76") != null)
                    MD76 = xml.Element("MD76").Value != "0" ? xml.Element("MD76").Value.ToString() : "";
                if (xml.Element("MD77") != null)
                    MD77 = xml.Element("MD77").Value != "0" ? xml.Element("MD77").Value.ToString() : "";
                if (xml.Element("MD78") != null)
                    MD78 = xml.Element("MD78").Value != "0" ? xml.Element("MD78").Value.ToString() : "";
                if (xml.Element("MD79") != null)
                    MD79 = xml.Element("MD79").Value != "0" ? xml.Element("MD79").Value.ToString() : "";
                if (xml.Element("MD80") != null)
                    MD80 = xml.Element("MD80").Value != "0" ? xml.Element("MD80").Value.ToString() : "";
                if (xml.Element("MD81") != null)
                    MD81 = xml.Element("MD81").Value != "0" ? xml.Element("MD81").Value.ToString() : "";
                if (xml.Element("MD82") != null)
                    MD82 = xml.Element("MD82").Value != "0" ? xml.Element("MD82").Value.ToString() : "";
                if (xml.Element("MD83") != null)
                    MD83 = xml.Element("MD83").Value != "0" ? xml.Element("MD83").Value.ToString() : "";
                if (xml.Element("MD84") != null)
                    MD84 = xml.Element("MD84").Value != "0" ? xml.Element("MD84").Value.ToString() : "";
                if (xml.Element("MD85") != null)
                    MD85 = xml.Element("MD85").Value != "0" ? xml.Element("MD85").Value.ToString() : "";
                if (xml.Element("MD86") != null)
                    MD86 = xml.Element("MD86").Value != "0" ? xml.Element("MD86").Value.ToString() : "";
                if (xml.Element("MD87") != null)
                    MD87 = xml.Element("MD87").Value != "0" ? xml.Element("MD87").Value.ToString() : "";
                if (xml.Element("MD88") != null)
                    MD88 = xml.Element("MD88").Value != "0" ? xml.Element("MD88").Value.ToString() : "";
                if (xml.Element("MD89") != null)
                    MD89 = xml.Element("MD89").Value != "0" ? xml.Element("MD89").Value.ToString() : "";
                if (xml.Element("MD90") != null)
                    MD90 = xml.Element("MD90").Value != "0" ? xml.Element("MD90").Value.ToString() : "";
                if (xml.Element("MD91") != null)
                    MD91 = xml.Element("MD91").Value != "0" ? xml.Element("MD91").Value.ToString() : "";
                if (xml.Element("MD92") != null)
                    MD92 = xml.Element("MD92").Value != "0" ? xml.Element("MD92").Value.ToString() : "";
                if (xml.Element("MD93") != null)
                    MD93 = xml.Element("MD93").Value != "0" ? xml.Element("MD93").Value.ToString() : "";
                if (xml.Element("MD94") != null)
                    MD94 = xml.Element("MD94").Value != "0" ? xml.Element("MD94").Value.ToString() : "";
                if (xml.Element("MD95") != null)
                    MD95 = xml.Element("MD95").Value != "0" ? xml.Element("MD95").Value.ToString() : "";
                if (xml.Element("MD96") != null)
                    MD96 = xml.Element("MD96").Value != "0" ? xml.Element("MD96").Value.ToString() : "";
                if (xml.Element("MD97") != null)
                    MD97 = xml.Element("MD97").Value != "0" ? xml.Element("MD97").Value.ToString() : "";
                if (xml.Element("MD98") != null)
                    MD98 = xml.Element("MD98").Value != "0" ? xml.Element("MD98").Value.ToString() : "";
                if (xml.Element("MD99") != null)
                    MD99 = xml.Element("MD99").Value != "0" ? xml.Element("MD99").Value.ToString() : "";
                if (xml.Element("MD100") != null)
                    MD100 = xml.Element("MD100").Value != "0" ? xml.Element("MD100").Value.ToString() : "";
                if (xml.Element("MD101") != null)
                    MD101 = xml.Element("MD101").Value != "0" ? xml.Element("MD101").Value.ToString() : "";
                if (xml.Element("MD102") != null)
                    MD102 = xml.Element("MD102").Value != "0" ? xml.Element("MD102").Value.ToString() : "";
                if (xml.Element("MD103") != null)
                    MD103 = xml.Element("MD103").Value != "0" ? xml.Element("MD103").Value.ToString() : "";
                if (xml.Element("MD104") != null)
                    MD104 = xml.Element("MD104").Value != "0" ? xml.Element("MD104").Value.ToString() : "";
                if (xml.Element("MD105") != null)
                    MD105 = xml.Element("MD105").Value != "0" ? xml.Element("MD105").Value.ToString() : "";
                if (xml.Element("MD106") != null)
                    MD106 = xml.Element("MD106").Value != "0" ? xml.Element("MD106").Value.ToString() : "";
                if (xml.Element("MD158") != null)
                    MD158 = xml.Element("MD158").Value != "0" ? xml.Element("MD158").Value.ToString() : "";
                if (xml.Element("MD159") != null)
                    MD159 = xml.Element("MD159").Value != "0" ? xml.Element("MD159").Value.ToString() : "";
                if (xml.Element("MD160") != null)
                    MD160 = xml.Element("MD160").Value != "0" ? xml.Element("MD160").Value.ToString() : "";
                if (xml.Element("MD161") != null)
                    MD161 = xml.Element("MD161").Value != "0" ? xml.Element("MD161").Value.ToString() : "";
                if (xml.Element("MD162") != null)
                    MD162 = xml.Element("MD162").Value != "0" ? xml.Element("MD162").Value.ToString() : "";
                if (xml.Element("MD163") != null)
                    MD163 = xml.Element("MD163").Value != "0" ? xml.Element("MD163").Value.ToString() : "";
                if (xml.Element("MD164") != null)
                    MD164 = xml.Element("MD164").Value != "0" ? xml.Element("MD164").Value.ToString() : "";
                if (xml.Element("MD165") != null)
                    MD165 = xml.Element("MD165").Value != "0" ? xml.Element("MD165").Value.ToString() : "";
                if (xml.Element("MD166") != null)
                    MD166 = xml.Element("MD166").Value != "0" ? xml.Element("MD166").Value.ToString() : "";
                if (xml.Element("MD167") != null)
                    MD167 = xml.Element("MD167").Value != "0" ? xml.Element("MD167").Value.ToString() : "";
                if (xml.Element("MD168") != null)
                    MD168 = xml.Element("MD168").Value != "0" ? xml.Element("MD168").Value.ToString() : "";
                if (xml.Element("MD169") != null)
                    MD169 = xml.Element("MD169").Value != "0" ? xml.Element("MD169").Value.ToString() : "";


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
