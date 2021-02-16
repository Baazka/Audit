using Audit.App_Func;
using Audit.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Audit.Controllers
{
    [ApplicationAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            OrgVM res = new OrgVM();
            try
            {
                if (Globals.departments.Count > 0 || Globals.statuses.Count > 0 || Globals.violations.Count > 0)
                {
                    res.departments = Globals.departments;
                    res.statuses = Globals.statuses;
                    res.violations = Globals.violations;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    res.departments = Globals.departments;

                    XElement responseStatus = SendLibraryRequest("Status");
                    Globals.statuses = (from item in responseStatus.Elements("Library") select new Status().FromXml(item)).ToList();
                    res.statuses = Globals.statuses;

                    XElement responseViolation = SendLibraryRequest("Violation");
                    Globals.violations = (from item in responseViolation.Elements("Library") select new Violation().FromXml(item)).ToList();
                    res.violations = Globals.violations;

                    return View(res);
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return View(res);
        }
        public PartialViewResult Menus()
        {
            return PartialView();
        }
        public ActionResult OrgList()
        {
            List<OrgList> orgLists = new List<OrgList>();
            XElement res = AppStatic.SystemController.OrgList(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("OrgList") != null)
                orgLists = (from item in res.Elements("OrgList") select new OrgList().FromXml(item)).ToList();
            return View(orgLists);
        }
        

        public static XElement SendLibraryRequest(string lib)
        {
            XElement elem = new XElement("lib");
                elem.Add(new XElement("LibraryName", lib));
            
            return AppStatic.SystemController.Library(elem);
        }
    }
}