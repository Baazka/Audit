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

        [Display(Name = "Одоогийн нууц үг")]
        [DataType(DataType.Password)]
        public string UserOldPassword { get; set; }
        [Display(Name = "Шинэ нууц үг")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Compare("UserPassword", ErrorMessage = "Шинэ нууц үгтэй тохирохгүй байна!")]
        [Display(Name = "Шинэ нууц үг /давтах/")]
        [DataType(DataType.Password)]
        public string UserPasswordConfirm { get; set; }
        public bool IsAdmin { get; set; }
        public int AlbaID { get; set; }

        public int USER_ID { get; set; }
        public string USER_CODE { get; set; }
        public string USER_NAME { get; set; }
        public int USER_DEPARTMENT_ID { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public int USER_TYPE_ID { get; set; }
        public string USER_TYPE_NAME { get; set; }
        public string USER_EMAIL { get; set; }
        public List<EditUser> editUser { get; set; } = new List<EditUser>();
        public DateTime USER_REG_DATE { get; set; }


        public int USER_ID_EDIT { get; set; }
        public string USER_CODE_EDIT { get; set; }
        public string USER_NAME_EDIT { get; set; }
        public int DEPARTMENT_ID_EDIT { get; set; }
        public int TEAM_TYPE_ID_EDIT { get; set; }
        public int AUDIT_ID_EDIT { get; set; }
        public string DEPARTMENT_NAME_EDIT { get; set; }
        public SystemUser FromXml(XElement elem)
        {
            if (elem.Element("USER_ID") != null)
                this.USER_ID = Convert.ToInt32(elem.Element("USER_ID").Value);
            if (elem.Element("USER_CODE") != null)
                this.UserName = elem.Element("USER_CODE").Value;
            if (elem.Element("USER_NAME") != null)
                this.USER_NAME = elem.Element("USER_NAME").Value;
            if (elem.Element("USER_DEPARTMENT_ID") != null)
                this.USER_DEPARTMENT_ID = Convert.ToInt32(elem.Element("USER_DEPARTMENT_ID").Value);
            if (elem.Element("DEPARTMENT_ID") != null)
                this.DEPARTMENT_ID = Convert.ToInt32(elem.Element("DEPARTMENT_ID").Value);
            if (elem.Element("DEPARTMENT_NAME") != null)
                this.DEPARTMENT_NAME = elem.Element("DEPARTMENT_NAME").Value;
            if (elem.Element("USER_TYPE_ID") != null)
                this.USER_TYPE_ID = Convert.ToInt32(elem.Element("USER_TYPE_ID").Value);
            if (elem.Element("USER_TYPE_NAME") != null)
                this.USER_TYPE_NAME = elem.Element("USER_TYPE_NAME").Value;
            if (elem.Element("USER_EMAIL") != null)
                this.USER_EMAIL = elem.Element("USER_EMAIL").Value;
            if (elem.Element("USER_CODE") != null)
                this.USER_CODE = elem.Element("USER_CODE").Value;
            if (elem.Element("USER_REG_DATE") != null)
                this.USER_REG_DATE = Convert.ToDateTime(elem.Element("USER_REG_DATE").Value);


            if (elem.Element("USER_ID_EDIT") != null)
                USER_ID_EDIT = Convert.ToInt32(elem.Element("USER_ID_EDIT").Value);
            if (elem.Element("USER_CODE_EDIT") != null)
                USER_CODE_EDIT = elem.Element("USER_CODE_EDIT").Value;
            if (elem.Element("USER_NAME_EDIT") != null)
                USER_NAME_EDIT = elem.Element("USER_NAME_EDIT").Value;
            if (elem.Element("DEPARTMENT_ID_EDIT") != null)
                DEPARTMENT_ID_EDIT = Convert.ToInt32(elem.Element("DEPARTMENT_ID_EDIT").Value);
            if (elem.Element("TEAM_TYPE_ID_EDIT") != null)
                TEAM_TYPE_ID_EDIT = Convert.ToInt32(elem.Element("TEAM_TYPE_ID_EDIT").Value);
            if (elem.Element("AUDIT_ID_EDIT") != null)
                AUDIT_ID_EDIT = Convert.ToInt32(elem.Element("AUDIT_ID_EDIT").Value);
            if (elem.Element("DEPARTMENT_NAME_EDIT") != null)
                DEPARTMENT_NAME_EDIT = elem.Element("DEPARTMENT_NAME_EDIT").Value;
            return this;
        }
    }
}