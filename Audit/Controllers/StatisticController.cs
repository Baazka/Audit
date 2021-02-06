using Audit.App_Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Audit.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Stat
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BM1()
        {
            XElement res = AppStatic.SystemController.BM1(User.GetClaimData("DepartmentID"));
            if (res != null && res.Elements("BM1") != null)
                return View(res);
            return View();
        }
    }
}