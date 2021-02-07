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
            return View();
        }
        public ActionResult OrgList()
        {
            List<OrgList> orgLists = new List<OrgList>();
            XElement res = AppStatic.SystemController.OrgList(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("OrgList") != null)
                orgLists = (from item in res.Elements("OrgList") select new OrgList().FromXml(item)).ToList();
            return View(orgLists);
        }
        public ActionResult _getDepartment()
        {
            XElement response = SendLibraryRequest("Department");
            List<Department> departments = (from item in response.Elements("Library") select new Department().FromXml(item)).ToList();
            return PartialView(departments);
        }
        public ActionResult _getStatus()
        {
            XElement response = SendLibraryRequest("Status");
            List<Status> statuses = (from item in response.Elements("Library") select new Status().FromXml(item)).ToList();
            return PartialView(statuses);
        }
        public ActionResult _getViolation()
        {
            XElement response = SendLibraryRequest("Violation");
            List<Violation> errors = (from item in response.Elements("Library") select new Violation().FromXml(item)).ToList();
            return PartialView(errors);
        }
        public static XElement SendLibraryRequest(string lib)
        {
            XElement elem = new XElement("lib");
                elem.Add(new XElement("LibraryName", lib));
            
            return AppStatic.SystemController.Library(elem);
        }
    }
}