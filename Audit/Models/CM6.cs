using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class CM6VM
    {
        public int DeparmentID { get; set; }
        public int PeriodID { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();
    }
    public class CM6
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Төрийн аудитын байгууллага сонгоно уу.")]
        public int OFFICE_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int STATISTIC_PERIOD { get; set; }
        public string PERIOD_LABEL { get; set; }
        [Required(ErrorMessage = "Төсөв сонгоно уу.")]
        public string AUD_NAME { get; set; }
        public int IS_STATE { get; set; }

        public int? ALL_COUNT { get; set; }
        public decimal? ALL_AMOUNT { get; set; }
        public int? PROCESSED_INCOMED_COUNT { get; set; }
        public decimal? PROCESSED_INCOMED_AMOUNT { get; set; }
        public int? PROCESSED_COSTS_COUNT { get; set; }
        public decimal? PROCESSED_COSTS_AMOUNT { get; set; }

        public int? ALL_C1_COUNT { get; set; }
        public decimal? ALL_C2_AMOUNT { get; set; }
        public int? ACCEPTED_INCOMED_COUNT { get; set; }
        public decimal? ACCEPTED_INCOMED_AMOUNT { get; set; }
        public int? ACCEPTED_COSTS_COUNT { get; set; }
        public decimal? ACCEPTED_COSTS_AMOUNT { get; set; }
        
        public int IS_ACTIVE { get; set; } = 1;
        public int EXEC_TYPE { get; set; }

        public string CREATED_DATE { get; set; } = DateTime.Now.ToString("dd-MMM-yy");
        public DateTime? UPDATED_DATE { get; set; }
        public List<Department> departments { get; set; } = new List<Department>();
        public List<Period> periods { get; set; } = new List<Period>();

        public CM6 SetXml(XElement xml)
        {
            if (xml != null)
            {
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
                if (xml.Element("AUD_NAME") != null)
                    AUD_NAME = xml.Element("AUD_NAME").Value;
                if (xml.Element("IS_STATE") != null)
                    IS_STATE = Convert.ToInt32(xml.Element("IS_STATE").Value);

                if (xml.Element("ALL_COUNT") != null)
                    ALL_COUNT = Convert.ToInt32(xml.Element("ALL_COUNT").Value);
                if (xml.Element("ALL_AMOUNT") != null)
                    ALL_AMOUNT = Convert.ToDecimal(xml.Element("ALL_AMOUNT").Value);
                if (xml.Element("PROCESSED_INCOMED_COUNT") != null)
                    PROCESSED_INCOMED_COUNT = Convert.ToInt32(xml.Element("PROCESSED_INCOMED_COUNT").Value);
                if (xml.Element("PROCESSED_INCOMED_AMOUNT") != null)
                    PROCESSED_INCOMED_AMOUNT = Convert.ToDecimal(xml.Element("PROCESSED_INCOMED_AMOUNT").Value);
                if (xml.Element("PROCESSED_COSTS_COUNT") != null)
                    PROCESSED_COSTS_COUNT = Convert.ToInt32(xml.Element("PROCESSED_COSTS_COUNT").Value);
                if (xml.Element("PROCESSED_COSTS_AMOUNT") != null)
                    PROCESSED_COSTS_AMOUNT = Convert.ToDecimal(xml.Element("PROCESSED_COSTS_AMOUNT").Value);
                if (xml.Element("ALL_C1_COUNT") != null)
                    ALL_C1_COUNT = Convert.ToInt32(xml.Element("ALL_C1_COUNT").Value);
                if (xml.Element("ALL_C2_AMOUNT") != null)
                    ALL_C2_AMOUNT = Convert.ToDecimal(xml.Element("ALL_C2_AMOUNT").Value);
                if (xml.Element("ACCEPTED_INCOMED_COUNT") != null)
                    ACCEPTED_INCOMED_COUNT = Convert.ToInt32(xml.Element("ACCEPTED_INCOMED_COUNT").Value);
                if (xml.Element("ACCEPTED_INCOMED_AMOUNT") != null)
                    ACCEPTED_INCOMED_AMOUNT = Convert.ToDecimal(xml.Element("ACCEPTED_INCOMED_AMOUNT").Value);
                if (xml.Element("ACCEPTED_COSTS_COUNT") != null)
                    ACCEPTED_COSTS_COUNT = Convert.ToInt32(xml.Element("ACCEPTED_COSTS_COUNT").Value);
                if (xml.Element("ACCEPTED_COSTS_AMOUNT") != null)
                    ACCEPTED_COSTS_AMOUNT = Convert.ToDecimal(xml.Element("ACCEPTED_COSTS_AMOUNT").Value);

                if (xml.Element("IS_ACTIVE") != null)
                    IS_ACTIVE = Convert.ToInt32(xml.Element("IS_ACTIVE").Value);
                if (xml.Element("EXEC_TYPE") != null)
                    EXEC_TYPE = Convert.ToInt32(xml.Element("EXEC_TYPE").Value);

            }
            return this;
        }
        public XElement ToXml()
        {
            return new XElement("CM6",
                       new XElement("ID", ID),
                       new XElement("OFFICE_ID", OFFICE_ID),
                       new XElement("STATISTIC_PERIOD", STATISTIC_PERIOD),
                       new XElement("AUD_NAME", AUD_NAME),
                       new XElement("IS_STATE", IS_STATE),
                       new XElement("ALL_COUNT", ALL_COUNT),
                       new XElement("ALL_AMOUNT", ALL_AMOUNT),
                       new XElement("PROCESSED_INCOMED_COUNT", PROCESSED_INCOMED_COUNT),
                       new XElement("PROCESSED_INCOMED_AMOUNT", PROCESSED_INCOMED_AMOUNT),
                       new XElement("PROCESSED_COSTS_COUNT", PROCESSED_COSTS_COUNT),
                       new XElement("PROCESSED_COSTS_AMOUNT", PROCESSED_COSTS_AMOUNT),
                       new XElement("ALL_C1_COUNT", ALL_C1_COUNT),
                       new XElement("ALL_C2_AMOUNT", ALL_C2_AMOUNT),
                       new XElement("ACCEPTED_INCOMED_COUNT", ACCEPTED_INCOMED_COUNT),
                       new XElement("ACCEPTED_INCOMED_AMOUNT", ACCEPTED_INCOMED_AMOUNT),
                       new XElement("ACCEPTED_COSTS_COUNT", ACCEPTED_COSTS_COUNT),
                       new XElement("ACCEPTED_COSTS_AMOUNT", ACCEPTED_COSTS_AMOUNT),
                       new XElement("IS_ACTIVE", IS_ACTIVE),
                       new XElement("CREATED_DATE", Convert.ToDateTime(CREATED_DATE).ToString("dd-MMM-yy"))
                       );
        }
    }

}