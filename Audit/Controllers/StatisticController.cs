using Audit.App_Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Audit.Controllers
{
    [ApplicationAuthorize]
    [Authorize(Roles = "Stat")]
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
            XElement res = AppStatic.SystemController.BM8(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("BM8") != null)
                return View(res);
            return View();
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
    }
}