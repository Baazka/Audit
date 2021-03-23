using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class Role
    {
        public const string Active = "Active";
        public const string Admin = "Admin";
        public const string Head_Auditor = "Head_Auditor";
        public const string Branch_Auditor = "Branch_Auditor";
        public const string Director = "Director";
        public const string Stat = "Stat";

        public int ID { get; set; }
        public String Name { get; set; }

        public static bool hasPermission(System.Security.Principal.IPrincipal User, string role)
        {
            return User.IsInRole(role);
        }

        public static bool hasActiveRole(System.Security.Principal.IPrincipal User)
        {
            return User.IsInRole(Active);
        }
        public static bool hasAdminRole(System.Security.Principal.IPrincipal User)
        {
            return User.IsInRole(Admin);
        }
        public static bool hasHeadAuditorRole(System.Security.Principal.IPrincipal User)
        {
            return User.IsInRole(Head_Auditor);
        }
        public static bool hasBranchAuditorRole(System.Security.Principal.IPrincipal User)
        {
            return User.IsInRole(Branch_Auditor);
        }
        public static bool hasDirectorRole(System.Security.Principal.IPrincipal User)
        {
            return User.IsInRole(Director);
        }
        public static bool hasStatRole(System.Security.Principal.IPrincipal User)
        {
            return User.IsInRole(Stat);
        }

        public Role FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                this.ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                this.Name = elem.Element("Name").Value;

            return this;
        }
    }
}