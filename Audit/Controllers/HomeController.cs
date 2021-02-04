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
        public JsonResult GetCount()
        {
            XElement res = AppStatic.SystemController.Dashboard();            
            Info info = new Info();
            if (res !=null)
            {
                if (res.Element("Count1") != null)
                    info.Count1 = Convert.ToInt32(res.Element("Count1").Element("Cnt").Value);
                if (res.Element("Count2") != null)
                    info.Count2 = Convert.ToInt32(res.Element("Count2").Element("Cnt").Value);
                if (res.Element("Count3") != null)
                    info.Count3 = Convert.ToInt32(res.Element("Count3").Element("Cnt").Value);
                if (res.Element("Count4") != null)
                    info.Count4 = Convert.ToInt32(res.Element("Count4").Element("Cnt").Value);
                if (res.Element("Count5") != null)
                    info.Count5 = Convert.ToInt32(res.Element("Count5").Element("Cnt").Value);
                if (res.Element("Count6") != null)
                    info.Count6 = Convert.ToInt32(res.Element("Count6").Element("Cnt").Value);
            }
            return Json(new { data = info }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult OrgAddEdit()
        {
            List<SelectListItem> obj = new List<SelectListItem>();
            obj = AppStatic.AddObjItem(obj);
            ViewData["Alphabet"] = obj;

            SetViewBagData(new List<string>() { "Aimag", "AimagSoum", "Bank", "TusuwZakhiragch", "Khelber", "Khoroo", "Zardal", "Sankhuujilt", "UilAjillagaa", "Alba", "Tatwar", "NDBaiguullaga" });

            return PartialView(new Organization());
        }
        [HttpPost]
        public ActionResult OrgAddEdit(Organization org)
        {
            if (ModelState.IsValid)
            {
                if (AppStatic.SystemController.OrgAddEdit(org.ToXml(), Convert.ToInt32(User.Identity.GetUserId())))
                    return Json(new { error = false, message = AppStatic.SystemController.Message });
                else
                    AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
            }

            List<SelectListItem> obj = new List<SelectListItem>();
            obj = AppStatic.AddObjItem(obj);
            ViewData["Alphabet"] = obj;

            SetViewBagData(new List<string>() { "Aimag", "AimagSoum", "Bank", "TusuwZakhiragch", "Khelber", "Khoroo", "Zardal", "Sankhuujilt", "UilAjillagaa",  "Alba", "Tatwar", "NDBaiguullaga" });

            return PartialView(org);
        }
        [HttpPost]
        public PartialViewResult OrgDetail(int orgid)
        {
            try
            {
                XElement res = AppStatic.SystemController.OrgDetail(orgid);
                if (AppStatic.SystemController.Status)
                {
                    Organization org = new Organization().FromXml(res.Element("Org"));
                    org.banks = (from item in res.Elements("OrgBank") select new OrganizationBank().FromXml(item)).ToList();
                    List<OrganizationPerson> organizationPerson = (from item in res.Elements("OrgPerson") select new OrganizationPerson().FromXml(item)).ToList();

                    org.HEAD_PERSONID = organizationPerson.Where(m => m.PersonType == 1).FirstOrDefault().PersonID;
                    org.HEAD_ROLE = organizationPerson.Where(m => m.PersonType == 1).FirstOrDefault().ROLE;
                    org.HEAD_FIRSTNAME = organizationPerson.Where(m => m.PersonType == 1).FirstOrDefault().FIRSTNAME;
                    org.HEAD_LASTNAME = organizationPerson.Where(m => m.PersonType == 1).FirstOrDefault().LASTNAME;
                    org.HEAD_FIRST_LETTER_REGISTER = organizationPerson.Where(m => m.PersonType == 1).FirstOrDefault().REGISTER.Substring(0,1);
                    org.HEAD_LAST_LETTER_REGISTER = organizationPerson.Where(m => m.PersonType == 1).FirstOrDefault().REGISTER.Substring(1, 1);
                    org.HEAD_REGISTER = organizationPerson.Where(m => m.PersonType == 1).FirstOrDefault().REGISTER.Substring(2, 8); ;
                    org.HEAD_PHONE = organizationPerson.Where(m => m.PersonType == 1).FirstOrDefault().Phone;
                    org.HEAD_EMAIL = organizationPerson.Where(m => m.PersonType == 1).FirstOrDefault().EMAIL;
                    org.HEAD_YEAR = organizationPerson.Where(m => m.PersonType == 1).FirstOrDefault().YEAR;
                    org.HEAD_PROFESSION = organizationPerson.Where(m => m.PersonType == 1).FirstOrDefault().PROFESSION;

                    org.ACCOUNTANT_PERSONID = organizationPerson.Where(m => m.PersonType == 2).FirstOrDefault().PersonID;
                    org.ACCOUNTANT_ROLE = organizationPerson.Where(m => m.PersonType == 2).FirstOrDefault().ROLE;
                    org.ACCOUNTANT_FIRSTNAME = organizationPerson.Where(m => m.PersonType == 2).FirstOrDefault().FIRSTNAME;
                    org.ACCOUNTANT_LASTNAME = organizationPerson.Where(m => m.PersonType == 2).FirstOrDefault().LASTNAME;
                    org.ACCOUNTANT_FIRST_LETTER_REGISTER = organizationPerson.Where(m => m.PersonType == 2).FirstOrDefault().REGISTER.Substring(0, 1);
                    org.ACCOUNTANT_LAST_LETTER_REGISTER = organizationPerson.Where(m => m.PersonType == 2).FirstOrDefault().REGISTER.Substring(1, 1);
                    org.ACCOUNTANT_REGISTER = organizationPerson.Where(m => m.PersonType == 2).FirstOrDefault().REGISTER.Substring(2, 8); ;
                    org.ACCOUNTANT_PHONE = organizationPerson.Where(m => m.PersonType == 2).FirstOrDefault().Phone;
                    org.ACCOUNTANT_EMAIL = organizationPerson.Where(m => m.PersonType == 2).FirstOrDefault().EMAIL;
                    org.ACCOUNTANT_YEAR = organizationPerson.Where(m => m.PersonType == 2).FirstOrDefault().YEAR;
                    org.ACCOUNTANT_PROFESSION = organizationPerson.Where(m => m.PersonType == 1).FirstOrDefault().PROFESSION;

                    List<SelectListItem> obj = new List<SelectListItem>();
                    obj = AppStatic.AddObjItem(obj);
                    ViewData["Alphabet"] = obj;

                    SetViewBagData(new List<string>() { "Aimag", "AimagSoum", "Bank", "TusuwZakhiragch", "Khelber", "Khoroo", "Zardal", "Sankhuujilt", "UilAjillagaa", "Alba", "Tatwar", "NDBaiguullaga" });

                    return PartialView("OrgAddEdit", org);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
            return PartialView();

        }
        public PartialViewResult OrgList()
        {
            XElement res = AppStatic.SystemController.OrgList();
            if (res != null && res.Elements("OrgList") != null)
                return PartialView(res);
            return PartialView();
        }

        public ActionResult _getAlba()
        {
            XElement response = SendLibraryRequest(new List<string> { "Alba" });
            List<Alba> albas = (from item in response.Elements("Alba") select new Alba().FromXml(item)).ToList();
            return PartialView(albas);
        }
        public ActionResult _getStatus()
        {
            XElement response = SendLibraryRequest(new List<string> { "Status" });
            List<Status> statuses = (from item in response.Elements("Status") select new Status().FromXml(item)).ToList();
            return PartialView(statuses);
        }
        public ActionResult _getError()
        {
            XElement response = SendLibraryRequest(new List<string> { "Error" });
            List<Error> errors = (from item in response.Elements("Error") select new Error().FromXml(item)).ToList();
            return PartialView(errors);
        }
        private void SetViewBagData(List<string> libNames)
        {
            XElement libs = SendLibraryRequest(libNames);
            ViewBag.libs = libs;
        }
        public static XElement SendLibraryRequest(List<string> libs)
        {
            XElement elem = new XElement("LibraryList");
            foreach (string lib in libs)
            {
                elem.Add(new XElement("LibraryName", lib));
            }

            return AppStatic.SystemController.LibraryList(elem);
        }
    }
}