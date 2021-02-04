using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{

    public class SystemUser
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Нэвтрэх нэр оруулна уу!")]
        [Display(Name = "Нэвтрэх нэр")]
        public string UserName { get; set; }
        [Display(Name = "Цахим хаяг")]
        public string EmailAddress { get; set; }

        [Display(Name = "Нууц үг")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Compare("UserPassword", ErrorMessage = "Нууц үгтэй тохирохгүй байна!")]
        [Display(Name = "Нууц үг /давтах/")]
        [DataType(DataType.Password)]
        public string UserPasswordConfirm { get; set; }
        public bool IsAdmin { get; set; }
        public int AlbaID { get; set; }
 
        public SystemUser FromXml(XElement elem)
        {
            if (elem.Element("UserID") != null)
                this.UserID = Convert.ToInt32(elem.Element("UserID").Value);

            if (elem.Element("UserName") != null)
                this.UserName = elem.Element("UserName").Value;
            if (elem.Element("UserPassword") != null)
                this.UserPassword = elem.Element("UserPassword").Value;
            if (elem.Element("UserPassword") != null)
                this.UserPasswordConfirm = elem.Element("UserPassword").Value;
            if (elem.Element("IsAdmin") != null)
                this.IsAdmin = Convert.ToBoolean(elem.Element("IsAdmin").Value);
            if (elem.Element("AlbaID") != null)
                this.AlbaID = Convert.ToInt32(elem.Element("AlbaID").Value);
            return this;
        }
    }
}