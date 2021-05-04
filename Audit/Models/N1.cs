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
    public class N1
    {

        public string ORGID { get; set; }
        //[Required(ErrorMessage = "N1 сонгоно уу.")]
        public string INSERTUSERID { get; set; }
        public string ORGNAME { get; set; }
        public string ORGTYPE { get; set; }
        public string ORGP_ROLE { get; set; }
        public string ORGP_FIRSTNAME { get; set; }
        public int ORGP_PHONE { get; set; }
        public string ORGP_ROLE2 { get; set; }
        public string ORGP_FIRSTNAME2 { get; set; }
        public string ORGP_PHONE2 { get; set; }
        public string FooterType { get; set; }


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

        public N1 SetXml(XElement xml)
        {

            return this;
        }


    }
}

