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
            try
            {
                XElement responseDepartment = SendLibraryRequest("Department");
                Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();

                XElement responsePeriod = SendLibraryRequest("StatPeriod");
                Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();

                XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();

                XElement responseRefTopicType = SendLibraryRequest("RefTopicType");
                Globals.topictypes = (from item in responseRefTopicType.Elements("Library") select new REF_TOPIC_TYPE().FromXml(item)).ToList();

                XElement responseRefFormType = SendLibraryRequest("RefFormType");
                Globals.formtypes = (from item in responseRefFormType.Elements("Library") select new REF_FORM_TYPE().FromXml(item)).ToList();

                XElement responseRefProposalType = SendLibraryRequest("RefProposalType");
                Globals.proposaltypes = (from item in responseRefProposalType.Elements("Library") select new REF_PROPOSAL_TYPE().FromXml(item)).ToList();

                XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();

                XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();

                XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return View();
        }
        public ActionResult PowerBI()
        {
            return View();
        }
        public PartialViewResult BM0Search(int officeid)
        {
            List<BM0Search> bM0Search = new List<BM0Search>();
            XElement res = AppStatic.SystemController.BM0Search(officeid);
            if (res != null && res.Elements("BM0Search") != null)
            {
                bM0Search = (from item in res.Elements("BM0Search") select new BM0Search().SetXml(item)).ToList();
            }
            return PartialView(bM0Search);
        }
        public PartialViewResult SystemUserModal()
        {
            List<SystemUser> systemuser = new List<SystemUser>();
            if (Globals.systemusers.Count != 0)
                systemuser = Globals.systemusers;
            else
            {
                XElement res = AppStatic.SystemController.SystemUser();
                if (res != null && res.Elements("SystemUser") != null)
                {
                    systemuser = (from item in res.Elements("SystemUser") select new SystemUser().FromXml(item)).ToList();
                }
            }            
            return PartialView(systemuser);
        }
        public ActionResult BM0()
        {
            BM0VM res = new BM0VM();
            try
            {
                if (Globals.departments.Count > 0 || Globals.periods.Count > 0)
                {
                    res.departments = Globals.departments;
                    res.periods = Globals.periods;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    res.departments = Globals.departments;

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
            bm0.AUDITOR_ENTRY = User.GetClaimData("USER_NAME");
            bm0.DEPARTMENT_ID = Convert.ToInt32(User.GetClaimData("DepartmentID"));
            bm0.DEPARTMENT_NAME = User.GetClaimData("DepartmentName");
            bm0.TEAM_DEPARTMENT_NAME = User.GetClaimData("DepartmentName");
            try
            {
                if (Globals.departments.Count > 0)
                    bm0.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm0.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm0.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm0.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm0.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm0.refaudityears = Globals.refaudityears;
                }
                if (Globals.audittypes.Count > 0)
                    bm0.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm0.audittypes = Globals.audittypes;
                }
                if (Globals.topictypes.Count > 0)
                    bm0.topictypes = Globals.topictypes;
                else
                {
                    XElement responseRefTopicType = SendLibraryRequest("RefTopicType");
                    Globals.topictypes = (from item in responseRefTopicType.Elements("Library") select new REF_TOPIC_TYPE().FromXml(item)).ToList();
                    bm0.topictypes = Globals.topictypes;
                }
                if (Globals.formtypes.Count > 0)
                    bm0.formtypes = Globals.formtypes;
                else
                {
                    XElement responseRefFormType = SendLibraryRequest("RefFormType");
                    Globals.formtypes = (from item in responseRefFormType.Elements("Library") select new REF_FORM_TYPE().FromXml(item)).ToList();
                    bm0.formtypes = Globals.formtypes;
                }
                if (Globals.proposaltypes.Count > 0)
                    bm0.proposaltypes = Globals.proposaltypes;
                else
                {
                    XElement responseRefProposalType = SendLibraryRequest("RefProposalType");
                    Globals.proposaltypes = (from item in responseRefProposalType.Elements("Library") select new REF_PROPOSAL_TYPE().FromXml(item)).ToList();
                    bm0.proposaltypes = Globals.proposaltypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm0.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm0.refbudgettypes = Globals.refbudgettypes;
                }
                if (Globals.haks.Count > 0)
                    bm0.haks = Globals.haks;
                else
                {
                    XElement responseHak = SendLibraryRequest("HAK");
                    Globals.haks = (from item in responseHak.Elements("Library") select new HAK().FromXml(item)).ToList();
                    bm0.haks = Globals.haks;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
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
                    bm0.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm0.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm0.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm0.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm0.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm0.refaudityears = Globals.refaudityears;
                }
                if (Globals.audittypes.Count > 0)
                    bm0.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm0.audittypes = Globals.audittypes;
                }
                if (Globals.topictypes.Count > 0)
                    bm0.topictypes = Globals.topictypes;
                else
                {
                    XElement responseRefTopicType = SendLibraryRequest("RefTopicType");
                    Globals.topictypes = (from item in responseRefTopicType.Elements("Library") select new REF_TOPIC_TYPE().FromXml(item)).ToList();
                    bm0.topictypes = Globals.topictypes;
                }
                if (Globals.formtypes.Count > 0)
                    bm0.formtypes = Globals.formtypes;
                else
                {
                    XElement responseRefFormType = SendLibraryRequest("RefFormType");
                    Globals.formtypes = (from item in responseRefFormType.Elements("Library") select new REF_FORM_TYPE().FromXml(item)).ToList();
                    bm0.formtypes = Globals.formtypes;
                }
                if (Globals.proposaltypes.Count > 0)
                    bm0.proposaltypes = Globals.proposaltypes;
                else
                {
                    XElement responseRefProposalType = SendLibraryRequest("RefProposalType");
                    Globals.proposaltypes = (from item in responseRefProposalType.Elements("Library") select new REF_PROPOSAL_TYPE().FromXml(item)).ToList();
                    bm0.proposaltypes = Globals.proposaltypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm0.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm0.refbudgettypes = Globals.refbudgettypes;
                }
                if (Globals.haks.Count > 0)
                    bm0.haks = Globals.haks;
                else
                {
                    XElement responseHak = SendLibraryRequest("HAK");
                    Globals.haks = (from item in responseHak.Elements("Library") select new HAK().FromXml(item)).ToList();
                    bm0.haks = Globals.haks;
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
                if (Globals.departments.Count > 0)
                    bm0.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm0.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm0.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm0.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm0.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm0.refaudityears = Globals.refaudityears;
                }
                if (Globals.audittypes.Count > 0)
                    bm0.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm0.audittypes = Globals.audittypes;
                }
                if (Globals.topictypes.Count > 0)
                    bm0.topictypes = Globals.topictypes;
                else
                {
                    XElement responseRefTopicType = SendLibraryRequest("RefTopicType");
                    Globals.topictypes = (from item in responseRefTopicType.Elements("Library") select new REF_TOPIC_TYPE().FromXml(item)).ToList();
                    bm0.topictypes = Globals.topictypes;
                }
                if (Globals.formtypes.Count > 0)
                    bm0.formtypes = Globals.formtypes;
                else
                {
                    XElement responseRefFormType = SendLibraryRequest("RefFormType");
                    Globals.formtypes = (from item in responseRefFormType.Elements("Library") select new REF_FORM_TYPE().FromXml(item)).ToList();
                    bm0.formtypes = Globals.formtypes;
                }
                if (Globals.proposaltypes.Count > 0)
                    bm0.proposaltypes = Globals.proposaltypes;
                else
                {
                    XElement responseRefProposalType = SendLibraryRequest("RefProposalType");
                    Globals.proposaltypes = (from item in responseRefProposalType.Elements("Library") select new REF_PROPOSAL_TYPE().FromXml(item)).ToList();
                    bm0.proposaltypes = Globals.proposaltypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm0.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm0.refbudgettypes = Globals.refbudgettypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm0.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseHak = SendLibraryRequest("HAK");
                    Globals.haks = (from item in responseHak.Elements("Library") select new HAK().FromXml(item)).ToList();
                    bm0.haks = Globals.haks;
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
        public ActionResult BM1AddEdit()
        {
            BM1 bm1 = new BM1();
            try
            {
                if (Globals.departments.Count > 0)
                    bm1.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm1.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm1.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm1.periods = Globals.periods;
                }
                if(Globals.refaudityears.Count > 0)
                    bm1.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm1.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm1.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm1.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm1.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm1.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm1.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm1.refbudgettypes = Globals.refbudgettypes;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }

            return PartialView(bm1);
        }
        [HttpPost]
        public ActionResult BM1AddEdit(BM1 bm1)
        {
            if (ModelState.IsValid)
            {
                if (bm1.ID != 0)
                {
                    if (AppStatic.SystemController.BM1Update(Convert.ToInt32(User.Identity.GetUserId()), bm1.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
                else
                {
                    if (AppStatic.SystemController.BM1Insert(Convert.ToInt32(User.Identity.GetUserId()), bm1.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
            }
            try
            {
                if (Globals.departments.Count > 0)
                    bm1.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm1.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm1.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm1.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm1.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm1.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm1.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm1.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm1.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm1.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm1.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm1.refbudgettypes = Globals.refbudgettypes;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(bm1);

        }
        public ActionResult BM1Detail(int id)
        {
            BM1 bm1 = new BM1();
            try
            {
                XElement res = AppStatic.SystemController.BM1Detail(id);
                if (res != null && res.Elements("BM1Detail") != null)
                {
                    bm1 = new BM1().SetXml(res.Element("BM1Detail"));
                }

                if (Globals.departments.Count > 0)
                    bm1.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm1.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm1.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm1.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm1.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm1.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm1.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm1.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm1.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm1.audittypes = Globals.audittypes;                    
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm1.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm1.refbudgettypes = Globals.refbudgettypes;
                }
                return PartialView("BM1AddEdit", bm1);
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView("BM1AddEdit", bm1);
        }
        [HttpPost]
        public JsonResult BM1Delete(int id)
        {
            return AppStatic.SystemController.BM1Delete(Convert.ToInt32(User.Identity.GetUserId()), id, DateTime.Now.ToString("dd-MMM-yy"))
                ? Json(new { error = false, message = AppStatic.SystemController.Message })
                : Json(new { error = true, message = AppStatic.SystemController.Message });
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
        public ActionResult BM2AddEdit()
        {
            BM2 bm2 = new BM2();

            try
            {
                if (Globals.departments.Count > 0)
                    bm2.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm2.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm2.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm2.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm2.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm2.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm2.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm2.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm2.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm2.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm2.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm2.refbudgettypes = Globals.refbudgettypes;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }

            return PartialView(bm2);
        }
        [HttpPost]
        public ActionResult BM2AddEdit(BM2 bm2)
        {
            if (ModelState.IsValid)
            {
                if (bm2.ID != 0)
                {
                    if (AppStatic.SystemController.BM2Update(Convert.ToInt32(User.Identity.GetUserId()), bm2.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
                else
                {
                    if (AppStatic.SystemController.BM2Insert(Convert.ToInt32(User.Identity.GetUserId()), bm2.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
            }
            try
            {
                if (Globals.departments.Count > 0)
                    bm2.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm2.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm2.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm2.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm2.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm2.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm2.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm2.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm2.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm2.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm2.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm2.refbudgettypes = Globals.refbudgettypes;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(bm2);

        }
        public ActionResult BM2Detail(int id)
        {
            BM2 bm2 = new BM2();
            try
            {
                XElement res = AppStatic.SystemController.BM2Detail(id);
                if (res != null && res.Elements("BM2Detail") != null)
                {
                    bm2 = new BM2().SetXml(res.Element("BM2Detail"));
                }

                if (Globals.departments.Count > 0)
                    bm2.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm2.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm2.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm2.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm2.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm2.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm2.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm2.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm2.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm2.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm2.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm2.refbudgettypes = Globals.refbudgettypes;
                }
                return PartialView("BM2AddEdit", bm2);
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView("BM2AddEdit", bm2);
        }
        [HttpPost]
        public JsonResult BM2Delete(int id)
        {
            return AppStatic.SystemController.BM2Delete(Convert.ToInt32(User.Identity.GetUserId()), id, DateTime.Now.ToString("dd-MMM-yy"))
                ? Json(new { error = false, message = AppStatic.SystemController.Message })
                : Json(new { error = true, message = AppStatic.SystemController.Message });
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
        public ActionResult BM3AddEdit()
        {
            BM3 bm3 = new BM3();

            try
            {
                if (Globals.departments.Count > 0)
                    bm3.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm3.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm3.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm3.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm3.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm3.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm3.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm3.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm3.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm3.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm3.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm3.refbudgettypes = Globals.refbudgettypes;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }

            return PartialView(bm3);
        }
        [HttpPost]
        public ActionResult BM3AddEdit(BM3 bm3)
        {
            if (ModelState.IsValid)
            {
                if (bm3.ID != 0)
                {
                    if (AppStatic.SystemController.BM3Update(Convert.ToInt32(User.Identity.GetUserId()), bm3.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
                else
                {
                    if (AppStatic.SystemController.BM3Insert(Convert.ToInt32(User.Identity.GetUserId()), bm3.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
            }
            try
            {
                if (Globals.departments.Count > 0)
                    bm3.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm3.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm3.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm3.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm3.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm3.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm3.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm3.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm3.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm3.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm3.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm3.refbudgettypes = Globals.refbudgettypes;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(bm3);

        }
        public ActionResult BM3Detail(int id)
        {
            BM3 bm3 = new BM3();
            try
            {
                XElement res = AppStatic.SystemController.BM3Detail(id);
                if (res != null && res.Elements("BM3Detail") != null)
                {
                    bm3 = new BM3().SetXml(res.Element("BM3Detail"));
                }

                if (Globals.departments.Count > 0)
                    bm3.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm3.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm3.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm3.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm3.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm3.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm3.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm3.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm3.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm3.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm3.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm3.refbudgettypes = Globals.refbudgettypes;
                }
                return PartialView("BM3AddEdit", bm3);
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView("BM3AddEdit", bm3);
        }
        [HttpPost]
        public JsonResult BM3Delete(int id)
        {
            return AppStatic.SystemController.BM3Delete(Convert.ToInt32(User.Identity.GetUserId()), id, DateTime.Now.ToString("dd-MMM-yy"))
                ? Json(new { error = false, message = AppStatic.SystemController.Message })
                : Json(new { error = true, message = AppStatic.SystemController.Message });
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
        public ActionResult BM4AddEdit()
        {
            BM4 bm4 = new BM4();

            try
            {
                if (Globals.departments.Count > 0)
                    bm4.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm4.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm4.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm4.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm4.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm4.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm4.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm4.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm4.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm4.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm4.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm4.refbudgettypes = Globals.refbudgettypes;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }

            return PartialView(bm4);
        }
        [HttpPost]
        public ActionResult BM4AddEdit(BM4 bm4)
        {
            if (ModelState.IsValid)
            {
                if (bm4.ID != 0)
                {
                    if (AppStatic.SystemController.BM4Update(Convert.ToInt32(User.Identity.GetUserId()), bm4.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
                else
                {
                    if (AppStatic.SystemController.BM4Insert(Convert.ToInt32(User.Identity.GetUserId()), bm4.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
            }
            try
            {
                if (Globals.departments.Count > 0)
                    bm4.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm4.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm4.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm4.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm4.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm4.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm4.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm4.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm4.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm4.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm4.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm4.refbudgettypes = Globals.refbudgettypes;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(bm4);

        }
        public ActionResult BM4Detail(int id)
        {
            BM4 bm4 = new BM4();
            try
            {
                XElement res = AppStatic.SystemController.BM4Detail(id);
                if (res != null && res.Elements("BM4Detail") != null)
                {
                    bm4 = new BM4().SetXml(res.Element("BM4Detail"));
                }

                if (Globals.departments.Count > 0)
                    bm4.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm4.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm4.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm4.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm4.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm4.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm4.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm4.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm4.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm4.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm4.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm4.refbudgettypes = Globals.refbudgettypes;
                }
                return PartialView("BM4AddEdit", bm4);
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView("BM4AddEdit", bm4);
        }
        [HttpPost]
        public JsonResult BM4Delete(int id)
        {
            return AppStatic.SystemController.BM4Delete(Convert.ToInt32(User.Identity.GetUserId()), id, DateTime.Now.ToString("dd-MMM-yy"))
                ? Json(new { error = false, message = AppStatic.SystemController.Message })
                : Json(new { error = true, message = AppStatic.SystemController.Message });
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
        public ActionResult BM5AddEdit()
        {
            BM5 bm5 = new BM5();

            try
            {
                if (Globals.departments.Count > 0)
                    bm5.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm5.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm5.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm5.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm5.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm5.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm5.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm5.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm5.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm5.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm5.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm5.refbudgettypes = Globals.refbudgettypes;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }

            return PartialView(bm5);
        }
        [HttpPost]
        public ActionResult BM5AddEdit(BM5 bm5)
        {
            if (ModelState.IsValid)
            {
                if (bm5.ID != 0)
                {
                    if (AppStatic.SystemController.BM5Update(Convert.ToInt32(User.Identity.GetUserId()), bm5.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
                else
                {
                    if (AppStatic.SystemController.BM5Insert(Convert.ToInt32(User.Identity.GetUserId()), bm5.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
            }
            try
            {
                if (Globals.departments.Count > 0)
                    bm5.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm5.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm5.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm5.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm5.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm5.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm5.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm5.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm5.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm5.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm5.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm5.refbudgettypes = Globals.refbudgettypes;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(bm5);

        }
        public ActionResult BM5Detail(int id)
        {
            BM5 bm5 = new BM5();
            try
            {
                XElement res = AppStatic.SystemController.BM5Detail(id);
                if (res != null && res.Elements("BM5Detail") != null)
                {
                    bm5 = new BM5().SetXml(res.Element("BM5Detail"));
                }

                if (Globals.departments.Count > 0)
                    bm5.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm5.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm5.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm5.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm5.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm5.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm5.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm5.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm5.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm5.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm5.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm5.refbudgettypes = Globals.refbudgettypes;
                }
                return PartialView("BM5AddEdit", bm5);
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView("BM5AddEdit", bm5);
        }
        [HttpPost]
        public JsonResult BM5Delete(int id)
        {
            return AppStatic.SystemController.BM5Delete(Convert.ToInt32(User.Identity.GetUserId()), id, DateTime.Now.ToString("dd-MMM-yy"))
                ? Json(new { error = false, message = AppStatic.SystemController.Message })
                : Json(new { error = true, message = AppStatic.SystemController.Message });
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
                bm8.departments = Globals.departments;
            else
            {
                XElement responseDepartment = SendLibraryRequest("Department");
                Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                bm8.departments = Globals.departments;
            }
            if (Globals.periods.Count > 0)
                bm8.periods = Globals.periods;
            else
            {
                XElement responsePeriod = SendLibraryRequest("StatPeriod");
                Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                bm8.periods = Globals.periods;
            }
            if (Globals.refaudityears.Count > 0)
                bm8.refaudityears = Globals.refaudityears;
            else
            {
                XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                bm8.refaudityears = Globals.refaudityears;
            }
            if (Globals.refviolationtypes.Count > 0)
                bm8.refviolationtypes = Globals.refviolationtypes;
            else
            {
                XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                bm8.refviolationtypes = Globals.refviolationtypes;
            }
            if (Globals.audittypes.Count > 0)
                bm8.audittypes = Globals.audittypes;
            else
            {
                XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                bm8.audittypes = Globals.audittypes;
            }
            if (Globals.refbudgettypes.Count > 0)
                bm8.refbudgettypes = Globals.refbudgettypes;
            else
            {
                XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                bm8.refbudgettypes = Globals.refbudgettypes;
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
                    bm8.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm8.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm8.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm8.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm8.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm8.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm8.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm8.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm8.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm8.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm8.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm8.refbudgettypes = Globals.refbudgettypes;
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
                if (Globals.departments.Count > 0)
                    bm8.departments = Globals.departments;
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    bm8.departments = Globals.departments;
                }
                if (Globals.periods.Count > 0)
                    bm8.periods = Globals.periods;
                else
                {
                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    bm8.periods = Globals.periods;
                }
                if (Globals.refaudityears.Count > 0)
                    bm8.refaudityears = Globals.refaudityears;
                else
                {
                    XElement responseRefAuditYear = SendLibraryRequest("RefAuditYear");
                    Globals.refaudityears = (from item in responseRefAuditYear.Elements("Library") select new REF_AUDIT_YEAR().FromXml(item)).ToList();
                    bm8.refaudityears = Globals.refaudityears;
                }
                if (Globals.refviolationtypes.Count > 0)
                    bm8.refviolationtypes = Globals.refviolationtypes;
                else
                {
                    XElement responseRefViolationType = SendLibraryRequest("RefViolationType");
                    Globals.refviolationtypes = (from item in responseRefViolationType.Elements("Library") select new REF_VIOLATION_TYPE().FromXml(item)).ToList();
                    bm8.refviolationtypes = Globals.refviolationtypes;
                }
                if (Globals.audittypes.Count > 0)
                    bm8.audittypes = Globals.audittypes;
                else
                {
                    XElement responseRefAuditType = SendLibraryRequest("RefAuditType");
                    Globals.audittypes = (from item in responseRefAuditType.Elements("Library") select new REF_AUDIT_TYPE().FromXml(item)).ToList();
                    bm8.audittypes = Globals.audittypes;
                }
                if (Globals.refbudgettypes.Count > 0)
                    bm8.refbudgettypes = Globals.refbudgettypes;
                else
                {
                    XElement responseRefBudgetType = SendLibraryRequest("RefBudgetType");
                    Globals.refbudgettypes = (from item in responseRefBudgetType.Elements("Library") select new REF_BUDGET_TYPE().FromXml(item)).ToList();
                    bm8.refbudgettypes = Globals.refbudgettypes;
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