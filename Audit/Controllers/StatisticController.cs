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

        public static XElement SendLibraryRequest(string lib)
        {
            XElement elem = new XElement("lib");
            elem.Add(new XElement("LibraryName", lib));

            return AppStatic.SystemController.Library(elem);
        }
    }
}