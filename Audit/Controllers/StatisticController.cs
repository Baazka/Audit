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
            BM0VM res = new BM0VM();
            try
            {
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
        public ActionResult BM0AddEdit()
        {
            BM0 bm0 = new BM0();
            if (Globals.departments.Count > 0)
            {
                bm0.departments = Globals.departments;
            }
            else
            {
                XElement responseDepartment = SendLibraryRequest("Department");
                Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                bm0.departments = Globals.departments;
            }
            if (Globals.periods.Count > 0)
            {
                bm0.periods = Globals.periods;
            }
            else
            {
                XElement responsePeriod = SendLibraryRequest("StatPeriod");
                Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                bm0.periods = Globals.periods;
            }
            return PartialView(bm0);
        }
        [HttpPost]
        public ActionResult BM0AddEdit(BM0 bm0)
        {
            if (ModelState.IsValid)
            {
                if (bm0.ID != 0)
                {
                    if (AppStatic.SystemController.BM0Update(Convert.ToInt32(User.Identity.GetUserId()), bm0.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
                else
                {
                    if (AppStatic.SystemController.BM0Insert(Convert.ToInt32(User.Identity.GetUserId()), bm0.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
            }
            try
            {
                if (Globals.departments.Count > 0)
                {
                    bm0.departments = Globals.departments;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm0.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                {
                    bm0.periods = Globals.periods;
                }
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm0.periods = Globals.periods;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(bm0);

        }
        public ActionResult BM0Detail(int id)
        {
            BM0 bm0 = new BM0();
            try
            {
                XElement res = AppStatic.SystemController.BM0Detail(id);
                if (res != null && res.Elements("BM0Detail") != null)
                {
                    bm0 = new BM0().SetXml(res.Element("BM0Detail"));

                }
                if (Globals.departments.Count > 0 || Globals.periods.Count > 0)
                {
                    bm0.departments = Globals.departments;
                    bm0.periods = Globals.periods;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm0.departments = Globals.departments;

                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm0.periods = Globals.periods;

                    return View(res);
                }
                return PartialView("BM0AddEdit", bm0);
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView("BM0AddEdit", bm0);
        }
        [HttpPost]
        public JsonResult BM0Delete(int id)
        {
            return AppStatic.SystemController.BM0Delete(Convert.ToInt32(User.Identity.GetUserId()), id, DateTime.Now.ToString("dd-MMM-yy"))
                ? Json(new { error = false, message = AppStatic.SystemController.Message })
                : Json(new { error = true, message = AppStatic.SystemController.Message });
        }
        public ActionResult BM1()
        {
            BM1VM res = new BM1VM();
            try
            {
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
        public ActionResult BM2()
        {
            BM2VM res = new BM2VM();
            try
            {
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
        public ActionResult BM3()
        {
            BM3VM res = new BM3VM();
            try
            {
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
        public ActionResult BM4()
        {
            BM4VM res = new BM4VM();
            try
            {
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
        public ActionResult BM5()
        {
            BM5VM res = new BM5VM();
            try
            {
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
        public ActionResult BM6()
        {
            BM6VM res = new BM6VM();
            try
            {
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
        public ActionResult BM7()
        {
            BM7VM res = new BM7VM();
            try
            {
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
                if (res != null && res.Elements("BM8Detail") != null)
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
            NM1VM res = new NM1VM();
            try
            {
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
        public ActionResult NM2()
        {
            NM2VM res = new NM2VM();
            try
            {
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
        public ActionResult NM3()
        {
            NM3VM res = new NM3VM();
            try
            {
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
        public ActionResult NM4()
        {
            NM4VM res = new NM4VM();
            try
            {
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
        public ActionResult NM5()
        {
            NM5VM res = new NM5VM();
            try
            {
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
        public ActionResult NM6()
        {
            NM6VM res = new NM6VM();
            try
            {
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
        public ActionResult NM7()
        {
            NM7VM res = new NM7VM();
            try
            {
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
        public ActionResult CM1A()
        {
            CM1VM res = new CM1VM();
            try
            {
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
        public ActionResult CM1B()
        {
            CM1VM res = new CM1VM();
            try
            {
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
        public ActionResult CM1C()
        {
            CM1VM res = new CM1VM();
            try
            {
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
        public ActionResult CM2A()
        {
            CM2VM res = new CM2VM();
            try
            {
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
        public ActionResult CM2B()
        {
            CM2VM res = new CM2VM();
            try
            {
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
        public ActionResult CM2C()
        {
            CM2VM res = new CM2VM();
            try
            {
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
        public ActionResult CM3A()
        {
            CM3VM res = new CM3VM();
            try
            {
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
        public ActionResult CM3B()
        {
            CM3VM res = new CM3VM();
            try
            {
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
        public ActionResult CM3C()
        {
            CM3VM res = new CM3VM();
            try
            {
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
        public ActionResult CM4A()
        {
            CM4VM res = new CM4VM();
            try
            {
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
        public ActionResult CM4B()
        {
            CM4VM res = new CM4VM();
            try
            {
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
        public ActionResult CM4C()
        {
            CM4VM res = new CM4VM();
            try
            {
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
        public ActionResult CM5()
        {
            CM5VM res = new CM5VM();
            try
            {
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
        public ActionResult CM6()
        {
            CM6VM res = new CM6VM();
            try
            {
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
        public ActionResult CM6AddEdit()
        {
            CM6 cm6 = new CM6();
            if (Globals.departments.Count > 0)
            {
                cm6.departments = Globals.departments;
            }
            else
            {
                XElement responseDepartment = SendLibraryRequest("Department");
                Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                cm6.departments = Globals.departments;
            }
            if (Globals.periods.Count > 0)
            {
                cm6.periods = Globals.periods;
            }
            else
            {
                XElement responsePeriod = SendLibraryRequest("StatPeriod");
                Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                cm6.periods = Globals.periods;
            }
            return PartialView(cm6);
        }
        [HttpPost]
        public ActionResult CM6AddEdit(CM6 cm6)
        {
            if (ModelState.IsValid)
            {
                if (cm6.ID != 0)
                {
                    if (AppStatic.SystemController.CM6Update(Convert.ToInt32(User.Identity.GetUserId()), cm6.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
                else
                {
                    if (AppStatic.SystemController.CM6Insert(Convert.ToInt32(User.Identity.GetUserId()), cm6.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
            }
            try
            {
                if (Globals.departments.Count > 0)
                {
                    cm6.departments = Globals.departments;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    cm6.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                {
                    cm6.periods = Globals.periods;
                }
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    cm6.periods = Globals.periods;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(cm6);

        }
        public ActionResult CM6Detail(int id)
        {
            CM6 cm6 = new CM6();
            try
            {
                XElement res = AppStatic.SystemController.CM6Detail(id);
                if (res != null && res.Elements("CM6Detail") != null)
                {
                    cm6 = new CM6().SetXml(res.Element("CM6Detail"));
                }
                if (Globals.departments.Count > 0 || Globals.periods.Count > 0)
                {
                    cm6.departments = Globals.departments;
                    cm6.periods = Globals.periods;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    cm6.departments = Globals.departments;

                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    cm6.periods = Globals.periods;

                    return View(res);
                }
                return PartialView("CM6AddEdit", cm6);
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView("CM6AddEdit", cm6);
        }
        [HttpPost]
        public JsonResult CM6Delete(int id)
        {
            return AppStatic.SystemController.CM6Delete(Convert.ToInt32(User.Identity.GetUserId()), id, DateTime.Now.ToString("dd-MMM-yy"))
                ? Json(new { error = false, message = AppStatic.SystemController.Message })
                : Json(new { error = true, message = AppStatic.SystemController.Message });
        }
        public ActionResult CM7()
        {
            CM7VM res = new CM7VM();
            try
            {
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
        public ActionResult CM7AddEdit()
        {
            CM7 cm7 = new CM7();
            if (Globals.departments.Count > 0)
            {
                cm7.departments = Globals.departments;
            }
            else
            {
                XElement responseDepartment = SendLibraryRequest("Department");
                Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                cm7.departments = Globals.departments;
            }
            if (Globals.periods.Count > 0)
            {
                cm7.periods = Globals.periods;
            }
            else
            {
                XElement responsePeriod = SendLibraryRequest("StatPeriod");
                Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                cm7.periods = Globals.periods;
            }
            return PartialView(cm7);
        }
        [HttpPost]
        public ActionResult CM7AddEdit(CM7 cm7)
        {
            if (ModelState.IsValid)
            {
                if (cm7.ID != 0)
                {
                    if (AppStatic.SystemController.CM7Update(Convert.ToInt32(User.Identity.GetUserId()), cm7.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
                else
                {
                    if (AppStatic.SystemController.CM7Insert(Convert.ToInt32(User.Identity.GetUserId()), cm7.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
            }
            try
            {
                if (Globals.departments.Count > 0)
                {
                    cm7.departments = Globals.departments;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    cm7.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                {
                    cm7.periods = Globals.periods;
                }
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    cm7.periods = Globals.periods;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(cm7);

        }
        public ActionResult CM7Detail(int id)
        {
            CM7 cm7 = new CM7();
            try
            {
                XElement res = AppStatic.SystemController.CM7Detail(id);
                if (res != null && res.Elements("CM7Detail") != null)
                {
                    cm7 = new CM7().SetXml(res.Element("CM7Detail"));
                }
                if (Globals.departments.Count > 0 || Globals.periods.Count > 0)
                {
                    cm7.departments = Globals.departments;
                    cm7.periods = Globals.periods;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    cm7.departments = Globals.departments;

                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    cm7.periods = Globals.periods;

                    return View(res);
                }
                return PartialView("CM7AddEdit", cm7);
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView("CM7AddEdit", cm7);
        }
        [HttpPost]
        public JsonResult CM7Delete(int id)
        {
            return AppStatic.SystemController.CM7Delete(Convert.ToInt32(User.Identity.GetUserId()), id, DateTime.Now.ToString("dd-MMM-yy"))
                ? Json(new { error = false, message = AppStatic.SystemController.Message })
                : Json(new { error = true, message = AppStatic.SystemController.Message });
        }
        public ActionResult CM8()
        {
            CM8VM res = new CM8VM();
            try
            {
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
        public ActionResult CM8AddEdit()
        {
            CM8 cm8 = new CM8();
            if (Globals.departments.Count > 0)
            {
                cm8.departments = Globals.departments;
            }
            else
            {
                XElement responseDepartment = SendLibraryRequest("Department");
                Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                cm8.departments = Globals.departments;
            }
            if (Globals.periods.Count > 0)
            {
                cm8.periods = Globals.periods;
            }
            else
            {
                XElement responsePeriod = SendLibraryRequest("StatPeriod");
                Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                cm8.periods = Globals.periods;
            }
            return PartialView(cm8);
        }
        [HttpPost]
        public ActionResult CM8AddEdit(CM8 cm8)
        {
            if (ModelState.IsValid)
            {
                if (cm8.ID != 0)
                {
                    if (AppStatic.SystemController.CM8Update(Convert.ToInt32(User.Identity.GetUserId()), cm8.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
                else
                {
                    if (AppStatic.SystemController.CM8Insert(Convert.ToInt32(User.Identity.GetUserId()), cm8.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
            }
            try
            {
                if (Globals.departments.Count > 0)
                {
                    cm8.departments = Globals.departments;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    cm8.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                {
                    cm8.periods = Globals.periods;
                }
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    cm8.periods = Globals.periods;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(cm8);

        }
        public ActionResult CM8Detail(int id)
        {
            CM8 cm8 = new CM8();
            try
            {
                XElement res = AppStatic.SystemController.CM8Detail(id);
                if (res != null && res.Elements("CM8Detail") != null)
                {
                    cm8 = new CM8().SetXml(res.Element("CM8Detail"));
                }
                if (Globals.departments.Count > 0 || Globals.periods.Count > 0)
                {
                    cm8.departments = Globals.departments;
                    cm8.periods = Globals.periods;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    cm8.departments = Globals.departments;

                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    cm8.periods = Globals.periods;

                    return View(res);
                }
                return PartialView("CM8AddEdit", cm8);
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView("CM8AddEdit", cm8);
        }
        [HttpPost]
        public JsonResult CM8Delete(int id)
        {
            return AppStatic.SystemController.CM8Delete(Convert.ToInt32(User.Identity.GetUserId()), id, DateTime.Now.ToString("dd-MMM-yy"))
                ? Json(new { error = false, message = AppStatic.SystemController.Message })
                : Json(new { error = true, message = AppStatic.SystemController.Message });
        }

        public static XElement SendLibraryRequest(string lib)
        {
            XElement elem = new XElement("lib");
            elem.Add(new XElement("LibraryName", lib));

            return AppStatic.SystemController.Library(elem);
        }
    }
}