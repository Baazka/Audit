using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class MenuRole
    {
        public int USER_TYPE_ID { get; set; }
        public string USER_TYPE_NAME { get; set; }
        public string ROLE_NAME { get; set; }
        public MenuRole FromXml(XElement elem)
        {
            if (elem.Element("USER_TYPE_ID") != null)
                this.USER_TYPE_ID = Convert.ToInt32(elem.Element("USER_TYPE_ID").Value);
            if (elem.Element("USER_TYPE_NAME") != null)
                this.USER_TYPE_NAME = elem.Element("USER_TYPE_NAME").Value;
            if (elem.Element("ROLE_NAME") != null)
                this.ROLE_NAME = elem.Element("ROLE_NAME").Value;

            return this;
        }
    }
}