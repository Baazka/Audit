using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class EditUser
    {
        public int USER_ID { get; set; }
        public string USER_CODE { get; set; }
        public string USER_NAME { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public int TEAM_TYPE_ID { get; set; }
        public int AUDIT_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public EditUser FromXml(XElement elem)
        {
            if (elem.Element("USER_ID") != null)
                USER_ID = Convert.ToInt32(elem.Element("USER_ID").Value);
            if (elem.Element("USER_CODE") != null)
                USER_CODE = elem.Element("USER_CODE").Value;
            if (elem.Element("USER_NAME") != null)
                USER_NAME = elem.Element("USER_NAME").Value;
            if (elem.Element("DEPARTMENT_ID") != null)
                DEPARTMENT_ID = Convert.ToInt32(elem.Element("DEPARTMENT_ID").Value);
            if (elem.Element("TEAM_TYPE_ID") != null)
                TEAM_TYPE_ID = Convert.ToInt32(elem.Element("TEAM_TYPE_ID").Value);
            if (elem.Element("AUDIT_ID") != null)
                AUDIT_ID = Convert.ToInt32(elem.Element("AUDIT_ID").Value);
            if (elem.Element("DEPARTMENT_NAME") != null)
                DEPARTMENT_NAME = elem.Element("DEPARTMENT_NAME").Value;
            return this;
        }
    }
}