using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class DataList
    {
        public string SRC_NAME { get; set; }
        public int SRC_ID { get; set; }
        public string REG_ID { get; set; }
        public string REG_NO { get; set; }
        public string ORG_NAME { get; set; }
        public DataList FromXml(XElement elem)
        {
            if (elem.Element("SRC_NAME") != null)
                SRC_NAME = elem.Element("SRC_NAME").Value;
            if (elem.Element("SRC_ID") != null)
                SRC_ID = Convert.ToInt32(elem.Element("SRC_ID").Value);
            if (elem.Element("REG_ID") != null)
                REG_ID = elem.Element("REG_ID").Value;
            if (elem.Element("REG_NO") != null)
                REG_NO = elem.Element("REG_NO").Value;
            if (elem.Element("ORG_NAME") != null)
                ORG_NAME = elem.Element("ORG_NAME").Value;

            return this;
        }
    }
}