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
    //[Authorize(Roles = "Director")]
    public class StatisticController : Controller
    {
        // GET: Stat
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PowerBI()
        {
            return View();
        }
        public ActionResult BM0()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("BM0") != null)
                return View(res);
            return View();
        }
        public ActionResult BM1()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("BM1") != null)
                return View(res);
            return View();
        }
        public ActionResult BM2()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("BM2") != null)
                return View(res);
            return View();
        }
        public ActionResult BM3()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("BM3") != null)
                return View(res);
            return View();
        }
        public ActionResult BM4()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("BM4") != null)
                return View(res);
            return View();
        }
        public ActionResult BM5()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("BM5") != null)
                return View(res);
            return View();
        }
        public ActionResult BM6()
        {
            XElement res = AppStatic.SystemController.BM6(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("BM6") != null)
                return View(res);
            return View();
        }
        public ActionResult BM7()
        {
            XElement res = AppStatic.SystemController.BM7(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("BM7") != null)
                return View(res);
            return View();
        }
        public ActionResult BM8()
        {
            BM8VM res = new BM8VM();
            try
            {
                //XElement response = AppStatic.SystemController.MenuRole(Convert.ToInt32(User.Identity.GetUserId()), Convert.ToInt32(Globals.Decrypt(id)));
                //if (response != null && response.Elements("MenuRole") != null)
                //    res.menuRoles = (from item in response.Elements("MenuRole") select new MenuRole().FromXml(item)).ToList();

                if (Globals.departments.Count > 0)
                {
                    res.departments = Globals.departments;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    res.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                {
                    res.periods = Globals.periods;
                }
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    res.periods = Globals.periods;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return View(res);
        }
        public ActionResult BM8AddEdit()
        {
            BM8 bm8 = new BM8();
            if (Globals.departments.Count > 0)
            {
                bm8.departments = Globals.departments;
            }
            else
            {
                XElement responseDepartment = SendLibraryRequest("Department");
                Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                bm8.departments = Globals.departments;
            }
            if (Globals.periods.Count > 0)
            {
                bm8.periods = Globals.periods;
            }
            else
            {
                XElement responsePeriod = SendLibraryRequest("StatPeriod");
                Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                bm8.periods = Globals.periods;
            }
            return PartialView(bm8);
        }
        [HttpPost]
        public ActionResult BM8AddEdit(BM8 bm8)
        {
            if (ModelState.IsValid)
            {
                if (bm8.ID != 0)
                {
                    if (AppStatic.SystemController.BM8Update(Convert.ToInt32(User.Identity.GetUserId()), bm8.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
                else
                {
                    if (AppStatic.SystemController.BM8Insert(Convert.ToInt32(User.Identity.GetUserId()), bm8.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
            }
            try
            {
                if (Globals.departments.Count > 0)
                {
                    bm8.departments = Globals.departments;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm8.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                {
                    bm8.periods = Globals.periods;
                }
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm8.periods = Globals.periods;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(bm8);

        }
        public ActionResult BM8Detail(int id)
        {
            BM8 bm8 = new BM8();
            try
            {
                XElement res = AppStatic.SystemController.BM8Detail(id);
                if (res != null && res.Elements("OrgDetail") != null)
                {
                    bm8 = new BM8().SetXml(res.Element("BM8Detail"));
                    
                }
                if (Globals.departments.Count > 0 || Globals.periods.Count > 0)
                {
                    bm8.departments = Globals.departments;
                    bm8.periods = Globals.periods;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm8.departments = Globals.departments;

                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm8.periods = Globals.periods;

                    return View(res);
                }
                return PartialView("BM8AddEdit", bm8);
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView("BM8AddEdit", bm8);
        }
        [HttpPost]
        public JsonResult BM8Delete(int id)
        {
            return AppStatic.SystemController.BM8Delete(Convert.ToInt32(User.Identity.GetUserId()), id, DateTime.Now.ToString("dd-MMM-yy"))
                ? Json(new { error = false, message = AppStatic.SystemController.Message })
                : Json(new { error = true, message = AppStatic.SystemController.Message });
        }
        public ActionResult NM1()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("NM1") != null)
                return View(res);
            return View();
        }
        public ActionResult NM2()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("NM2") != null)
                return View(res);
            return View();
        }
        public ActionResult NM3()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("NM3") != null)
                return View(res);
            return View();
        }
        public ActionResult NM4()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("NM4") != null)
                return View(res);
            return View();
        }
        public ActionResult NM5()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("NM5") != null)
                return View(res);
            return View();
        }
        public ActionResult NM6()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("NM6") != null)
                return View(res);
            return View();
        }
        public ActionResult NM7()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("NM7") != null)
                return View(res);
            return View();
        }
        public ActionResult CM1A()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM1A") != null)
                return View(res);
            return View();
        }
        public ActionResult CM1B()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM1B") != null)
                return View(res);
            return View();
        }
        public ActionResult CM1C()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM1C") != null)
                return View(res);
            return View();
        }
        public ActionResult CM2A()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM2A") != null)
                return View(res);
            return View();
        }
        public ActionResult CM2B()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM2B") != null)
                return View(res);
            return View();
        }
        public ActionResult CM2C()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM2C") != null)
                return View(res);
            return View();
        }
        public ActionResult CM3A()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM3A") != null)
                return View(res);
            return View();
        }
        public ActionResult CM3B()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM3B") != null)
                return View(res);
            return View();
        }
        public ActionResult CM3C()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM3C") != null)
                return View(res);
            return View();
        }
        public ActionResult CM4A()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM4A") != null)
                return View(res);
            return View();
        }
        public ActionResult CM4B()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM4B") != null)
                return View(res);
            return View();
        }
        public ActionResult CM4C()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM4C") != null)
                return View(res);
            return View();
        }
        public ActionResult CM5()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM5") != null)
                return View(res);
            return View();
        }
        public ActionResult CM6()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM6") != null)
                return View(res);
            return View();
        }
        public ActionResult CM7()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM7") != null)
                return View(res);
            return View();
        }
        public ActionResult CM8()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("CM8") != null)
                return View(res);
            return View();
        }

        public static XElement SendLibraryRequest(string lib)
        {
            XElement elem = new XElement("lib");
            elem.Add(new XElement("LibraryName", lib));

            return AppStatic.SystemController.Library(elem);
        }
    }
}