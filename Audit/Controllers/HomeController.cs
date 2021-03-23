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
    public class HomeController : Controller
    {
        public ActionResult Index(int menuid)
        {
            OrgVM res = new OrgVM();
            try
            {
                XElement response = AppStatic.SystemController.MenuRole(Convert.ToInt32(User.Identity.GetUserId()), menuid);
                if (response != null && response.Elements("MenuRole") != null)
                    res.menuRoles = (from item in response.Elements("MenuRole") select new MenuRole().FromXml(item)).ToList();

                if (Globals.departments.Count > 0 || Globals.statuses.Count > 0 || Globals.violations.Count > 0 || Globals.offices.Count > 0 || Globals.subOffices.Count > 0 || Globals.budgetTypes.Count > 0 || Globals.activities.Count > 0 || Globals.subBudgetTypes.Count > 0 || Globals.committees.Count > 0 || Globals.taxOffices.Count > 0 || Globals.costTypes.Count > 0 || Globals.insuranceOffices.Count > 0 || Globals.finOffices.Count > 0 || Globals.financingTypes.Count > 0 || Globals.banks.Count > 0)
                {
                    res.departments = Globals.departments;
                    res.statuses = Globals.statuses;
                    res.violations = Globals.violations;
                    res.offices = Globals.offices;
                    res.subOffices = Globals.subOffices;
                    res.budgetTypes = Globals.budgetTypes;
                    res.activities = Globals.activities;
                    res.subBudgetTypes = Globals.subBudgetTypes;
                    res.committees = Globals.committees;
                    res.taxOffices = Globals.taxOffices;
                    res.costTypes = Globals.costTypes;
                    res.insuranceOffices = Globals.insuranceOffices;
                    res.finOffices = Globals.finOffices;
                    res.financingTypes = Globals.financingTypes;
                    res.banks = Globals.banks;
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

                    XElement responseOffice = SendLibraryRequest("Office");
                    Globals.offices = (from item in responseOffice.Elements("Library") select new Office().FromXml(item)).ToList();
                    res.offices = Globals.offices;

                    XElement responseSubOffice = SendLibraryRequest("SubOffice");
                    Globals.subOffices = (from item in responseSubOffice.Elements("Library") select new SubOffice().FromXml(item)).ToList();
                    res.subOffices = Globals.subOffices;

                    XElement responseBudgetType = SendLibraryRequest("BudgetType");
                    Globals.budgetTypes = (from item in responseBudgetType.Elements("Library") select new BudgetType().FromXml(item)).ToList();
                    res.budgetTypes = Globals.budgetTypes;

                    XElement responseActivity = SendLibraryRequest("Activity");
                    Globals.activities = (from item in responseActivity.Elements("Library") select new ActivityLib().FromXml(item)).ToList();
                    res.activities = Globals.activities;

                    XElement responseSubBudgetType = SendLibraryRequest("SubBudgetType");
                    Globals.subBudgetTypes = (from item in responseSubBudgetType.Elements("Library") select new SubBudgetType().FromXml(item)).ToList();
                    res.subBudgetTypes = Globals.subBudgetTypes;

                    XElement responseCommittee = SendLibraryRequest("Committee");
                    Globals.committees = (from item in responseCommittee.Elements("Library") select new Committee().FromXml(item)).ToList();
                    res.committees = Globals.committees;

                    XElement responseTaxOffice = SendLibraryRequest("TaxOffice");
                    Globals.taxOffices = (from item in responseTaxOffice.Elements("Library") select new TaxOffice().FromXml(item)).ToList();
                    res.taxOffices = Globals.taxOffices;

                    XElement responseCostType = SendLibraryRequest("CostType");
                    Globals.costTypes = (from item in responseCostType.Elements("Library") select new CostType().FromXml(item)).ToList();
                    res.costTypes = Globals.costTypes;

                    XElement responseInsuranceOffice = SendLibraryRequest("InsuranceOffice");
                    Globals.insuranceOffices = (from item in responseInsuranceOffice.Elements("Library") select new InsuranceOffice().FromXml(item)).ToList();
                    res.insuranceOffices = Globals.insuranceOffices;

                    XElement responseFinOffice = SendLibraryRequest("FinOffice");
                    Globals.finOffices = (from item in responseFinOffice.Elements("Library") select new FinOffice().FromXml(item)).ToList();
                    res.insuranceOffices = Globals.insuranceOffices;

                    XElement responseFinancingType = SendLibraryRequest("FinancingType");
                    Globals.financingTypes = (from item in responseFinancingType.Elements("Library") select new FinancingType().FromXml(item)).ToList();
                    res.financingTypes = Globals.financingTypes;

                    XElement responseBank = SendLibraryRequest("Bank");
                    Globals.banks = (from item in responseBank.Elements("Library") select new Bank().FromXml(item)).ToList();
                    res.banks = Globals.banks;

                    return View(res);
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return View(res);
        }
        [AllowAnonymous]
        public ActionResult Home()
        {
            return View();
        }
        [AllowAnonymous]
        public PartialViewResult Menus()
        {
            XElement res = AppStatic.SystemController.MenuList(Convert.ToInt32(User.Identity.GetUserId()));
            return PartialView(res);
        }
        public ActionResult OrgList()
        {
            List<OrgList> orgLists = new List<OrgList>();
            return View(orgLists);
        }
        public ActionResult OrgDetail(int orgid)
        {
            Organization organization = new Organization();
            try
            {
                XElement res = AppStatic.SystemController.OrgDetail(orgid);
                if (res != null && res.Elements("OrgDetail") != null) {
                    organization = new Organization().FromXml(res.Element("OrgDetail"));
                    //ubinfo
                    XElement resUB = AppStatic.SystemController.OrgUB(organization.ORG_REGISTER_NO);
                    if (resUB != null && resUB.Elements("UBList") != null)
                    {
                        organization.organizationUBs = (from item in resUB.Elements("UBList") select new OrganizationUB().FromXml(item)).ToList();
                    }
                    //mofinfo
                    XElement resMOF = AppStatic.SystemController.OrgMOF(organization.ORG_REGISTER_NO);
                    if (resMOF != null && resMOF.Elements("MOFList") != null)
                    {
                        organization.organizationMOFs = (from item in resMOF.Elements("MOFList") select new OrganizationMOF().FromXml(item)).ToList();
                    }
                }
                if (Globals.departments.Count > 0 || Globals.offices.Count > 0 || Globals.subOffices.Count > 0 || Globals.budgetTypes.Count > 0 || Globals.activities.Count > 0 || Globals.subBudgetTypes.Count > 0 || Globals.committees.Count > 0 || Globals.taxOffices.Count > 0 || Globals.costTypes.Count > 0 || Globals.insuranceOffices.Count > 0 || Globals.finOffices.Count > 0 || Globals.financingTypes.Count > 0 || Globals.banks.Count > 0)
                {
                    organization.departments = Globals.departments;
                    organization.offices = Globals.offices;
                    organization.subOffices = Globals.subOffices;
                    organization.budgetTypes = Globals.budgetTypes;
                    organization.activities = Globals.activities;
                    organization.subBudgetTypes = Globals.subBudgetTypes;
                    organization.committees = Globals.committees;
                    organization.taxOffices = Globals.taxOffices;
                    organization.costTypes = Globals.costTypes;
                    organization.insuranceOffices = Globals.insuranceOffices;
                    organization.finOffices = Globals.finOffices;
                    organization.financingTypes = Globals.financingTypes;
                    organization.banks = Globals.banks;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    organization.departments = Globals.departments;

                    XElement responseOffice = SendLibraryRequest("Office");
                    Globals.offices = (from item in responseOffice.Elements("Library") select new Office().FromXml(item)).ToList();
                    organization.offices = Globals.offices;

                    XElement responseSubOffice = SendLibraryRequest("SubOffice");
                    Globals.subOffices = (from item in responseSubOffice.Elements("Library") select new SubOffice().FromXml(item)).ToList();
                    organization.subOffices = Globals.subOffices;

                    XElement responseBudgetType = SendLibraryRequest("BudgetType");
                    Globals.budgetTypes = (from item in responseBudgetType.Elements("Library") select new BudgetType().FromXml(item)).ToList();
                    organization.budgetTypes = Globals.budgetTypes;

                    XElement responseActivity = SendLibraryRequest("Activity");
                    Globals.activities = (from item in responseActivity.Elements("Library") select new ActivityLib().FromXml(item)).ToList();
                    organization.activities = Globals.activities;

                    XElement responseSubBudgetType = SendLibraryRequest("SubBudgetType");
                    Globals.subBudgetTypes = (from item in responseSubBudgetType.Elements("Library") select new SubBudgetType().FromXml(item)).ToList();
                    organization.subBudgetTypes = Globals.subBudgetTypes;

                    XElement responseCommittee = SendLibraryRequest("Committee");
                    Globals.committees = (from item in responseCommittee.Elements("Library") select new Committee().FromXml(item)).ToList();
                    organization.committees = Globals.committees;

                    XElement responseTaxOffice = SendLibraryRequest("TaxOffice");
                    Globals.taxOffices = (from item in responseTaxOffice.Elements("Library") select new TaxOffice().FromXml(item)).ToList();
                    organization.taxOffices = Globals.taxOffices;

                    XElement responseCostType = SendLibraryRequest("CostType");
                    Globals.costTypes = (from item in responseCostType.Elements("Library") select new CostType().FromXml(item)).ToList();
                    organization.costTypes = Globals.costTypes;

                    XElement responseInsuranceOffice = SendLibraryRequest("InsuranceOffice");
                    Globals.insuranceOffices = (from item in responseInsuranceOffice.Elements("Library") select new InsuranceOffice().FromXml(item)).ToList();
                    organization.insuranceOffices = Globals.insuranceOffices;

                    XElement responseFinOffice = SendLibraryRequest("FinOffice");
                    Globals.finOffices = (from item in responseFinOffice.Elements("Library") select new FinOffice().FromXml(item)).ToList();
                    organization.insuranceOffices = Globals.insuranceOffices;

                    XElement responseFinancingType = SendLibraryRequest("FinancingType");
                    Globals.financingTypes = (from item in responseFinancingType.Elements("Library") select new FinancingType().FromXml(item)).ToList();
                    organization.financingTypes = Globals.financingTypes;

                    XElement responseBank = SendLibraryRequest("Bank");
                    Globals.banks = (from item in responseBank.Elements("Library") select new Bank().FromXml(item)).ToList();
                    organization.banks = Globals.banks;

                    return View(res);
                }
                return PartialView("OrgAddEdit", organization);
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView("OrgAddEdit", organization);
        }
        public ActionResult OrgAddEdit()
        {
            Organization organization = new Organization();
            try
            {
                if (Globals.departments.Count > 0)
                {
                    organization.departments = Globals.departments;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    organization.departments = Globals.departments;
                }
                if (Globals.offices.Count > 0)
                {
                    organization.offices = Globals.offices;
                }
                else
                {
                    XElement responseOffice = SendLibraryRequest("Office");
                    Globals.offices = (from item in responseOffice.Elements("Library") select new Office().FromXml(item)).ToList();
                    organization.offices = Globals.offices;
                }
                if (Globals.subOffices.Count > 0)
                {
                    organization.subOffices = Globals.subOffices;
                }
                else
                {
                    XElement responseSubOffice = SendLibraryRequest("SubOffice");
                    Globals.subOffices = (from item in responseSubOffice.Elements("Library") select new SubOffice().FromXml(item)).ToList();
                    organization.subOffices = Globals.subOffices;
                }
                if (Globals.budgetTypes.Count > 0)
                {
                    organization.budgetTypes = Globals.budgetTypes;
                }
                else
                {
                    XElement responseBudgetType = SendLibraryRequest("BudgetType");
                    Globals.budgetTypes = (from item in responseBudgetType.Elements("Library") select new BudgetType().FromXml(item)).ToList();
                    organization.budgetTypes = Globals.budgetTypes;
                }
                if (Globals.activities.Count > 0)
                {
                    organization.activities = Globals.activities;
                }
                else
                {
                    XElement responseActivity = SendLibraryRequest("Activity");
                    Globals.activities = (from item in responseActivity.Elements("Library") select new ActivityLib().FromXml(item)).ToList();
                    organization.activities = Globals.activities;
                }
                if (Globals.subBudgetTypes.Count > 0)
                {
                    organization.subBudgetTypes = Globals.subBudgetTypes;
                }
                else
                {
                    XElement responseSubBudgetType = SendLibraryRequest("SubBudgetType");
                    Globals.subBudgetTypes = (from item in responseSubBudgetType.Elements("Library") select new SubBudgetType().FromXml(item)).ToList();
                    organization.subBudgetTypes = Globals.subBudgetTypes;
                }
                if (Globals.committees.Count > 0)
                {
                    organization.committees = Globals.committees;
                }
                else
                {
                    XElement responseCommittee = SendLibraryRequest("Committee");
                    Globals.committees = (from item in responseCommittee.Elements("Library") select new Committee().FromXml(item)).ToList();
                    organization.committees = Globals.committees;
                }
                if (Globals.taxOffices.Count > 0)
                {
                    organization.taxOffices = Globals.taxOffices;
                }
                else
                {
                    XElement responseTaxOffice = SendLibraryRequest("TaxOffice");
                    Globals.taxOffices = (from item in responseTaxOffice.Elements("Library") select new TaxOffice().FromXml(item)).ToList();
                    organization.taxOffices = Globals.taxOffices;
                }
                if (Globals.costTypes.Count > 0)
                {
                    organization.costTypes = Globals.costTypes;
                }
                else
                {
                    XElement responseCostType = SendLibraryRequest("CostType");
                    Globals.costTypes = (from item in responseCostType.Elements("Library") select new CostType().FromXml(item)).ToList();
                    organization.costTypes = Globals.costTypes;
                }
                if (Globals.insuranceOffices.Count > 0)
                {
                    organization.insuranceOffices = Globals.insuranceOffices;
                }
                else
                {
                    XElement responseInsuranceOffice = SendLibraryRequest("InsuranceOffice");
                    Globals.insuranceOffices = (from item in responseInsuranceOffice.Elements("Library") select new InsuranceOffice().FromXml(item)).ToList();
                    organization.insuranceOffices = Globals.insuranceOffices;
                }
                if (Globals.financingTypes.Count > 0)
                {
                    organization.financingTypes = Globals.financingTypes;
                }
                else
                {
                    XElement responseFinancingType = SendLibraryRequest("FinancingType");
                    Globals.financingTypes = (from item in responseFinancingType.Elements("Library") select new FinancingType().FromXml(item)).ToList();
                    organization.financingTypes = Globals.financingTypes;
                }
                if (Globals.banks.Count > 0)
                {
                    organization.banks = Globals.banks;
                }
                else
                {
                    XElement responseBank = SendLibraryRequest("Bank");
                    Globals.banks = (from item in responseBank.Elements("Library") select new Bank().FromXml(item)).ToList();
                    organization.banks = Globals.banks;
                }
                if (Globals.finOffices.Count > 0)
                {
                    organization.finOffices = Globals.finOffices;
                }
                else
                {
                    XElement responseFinOffice = SendLibraryRequest("FinOffice");
                    Globals.finOffices = (from item in responseFinOffice.Elements("Library") select new FinOffice().FromXml(item)).ToList();
                    organization.finOffices = Globals.finOffices;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(organization);
        }
        [HttpPost]
        public ActionResult OrgAddEdit(Organization organization)
        {
            if (ModelState.IsValid)
            {
                if(organization.ORG_ID != 0)
                {
                    if (AppStatic.SystemController.OrgSave(Convert.ToInt32(User.Identity.GetUserId()), organization.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
                else
                {
                    if (AppStatic.SystemController.OrgInsert(Convert.ToInt32(User.Identity.GetUserId()), organization.ToXml()))
                        return Json(new { error = false, message = AppStatic.SystemController.Message });
                    else
                        AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
                }
            }
            try
            {
                //ubinfo
                XElement resUB = AppStatic.SystemController.OrgUB(organization.ORG_REGISTER_NO);
                if (resUB != null && resUB.Elements("UBList") != null)
                {
                    organization.organizationUBs = (from item in resUB.Elements("UBList") select new OrganizationUB().FromXml(item)).ToList();
                }
                //mofinfo
                XElement resMOF = AppStatic.SystemController.OrgMOF(organization.ORG_REGISTER_NO);
                if (resMOF != null && resMOF.Elements("MOFList") != null)
                {
                    organization.organizationMOFs = (from item in resMOF.Elements("MOFList") select new OrganizationMOF().FromXml(item)).ToList();
                }
                if (Globals.departments.Count > 0)
                {
                    organization.departments = Globals.departments;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    organization.departments = Globals.departments;
                }
                if (Globals.offices.Count > 0)
                {
                    organization.offices = Globals.offices;
                }
                else
                {
                    XElement responseOffice = SendLibraryRequest("Office");
                    Globals.offices = (from item in responseOffice.Elements("Library") select new Office().FromXml(item)).ToList();
                    organization.offices = Globals.offices;
                }
                if (Globals.subOffices.Count > 0)
                {
                    organization.subOffices = Globals.subOffices;
                }
                else
                {
                    XElement responseSubOffice = SendLibraryRequest("SubOffice");
                    Globals.subOffices = (from item in responseSubOffice.Elements("Library") select new SubOffice().FromXml(item)).ToList();
                    organization.subOffices = Globals.subOffices;
                }
                if (Globals.budgetTypes.Count > 0)
                {
                    organization.budgetTypes = Globals.budgetTypes;
                }
                else
                {
                    XElement responseBudgetType = SendLibraryRequest("BudgetType");
                    Globals.budgetTypes = (from item in responseBudgetType.Elements("Library") select new BudgetType().FromXml(item)).ToList();
                    organization.budgetTypes = Globals.budgetTypes;
                }
                if (Globals.activities.Count > 0)
                {
                    organization.activities = Globals.activities;
                }
                else
                {
                    XElement responseActivity = SendLibraryRequest("Activity");
                    Globals.activities = (from item in responseActivity.Elements("Library") select new ActivityLib().FromXml(item)).ToList();
                    organization.activities = Globals.activities;
                }
                if (Globals.subBudgetTypes.Count > 0)
                {
                    organization.subBudgetTypes = Globals.subBudgetTypes;
                }
                else
                {
                    XElement responseSubBudgetType = SendLibraryRequest("SubBudgetType");
                    Globals.subBudgetTypes = (from item in responseSubBudgetType.Elements("Library") select new SubBudgetType().FromXml(item)).ToList();
                    organization.subBudgetTypes = Globals.subBudgetTypes;
                }
                if (Globals.committees.Count > 0)
                {
                    organization.committees = Globals.committees;
                }
                else
                {
                    XElement responseCommittee = SendLibraryRequest("Committee");
                    Globals.committees = (from item in responseCommittee.Elements("Library") select new Committee().FromXml(item)).ToList();
                    organization.committees = Globals.committees;
                }
                if (Globals.taxOffices.Count > 0)
                {
                    organization.taxOffices = Globals.taxOffices;
                }
                else
                {
                    XElement responseTaxOffice = SendLibraryRequest("TaxOffice");
                    Globals.taxOffices = (from item in responseTaxOffice.Elements("Library") select new TaxOffice().FromXml(item)).ToList();
                    organization.taxOffices = Globals.taxOffices;
                }
                if (Globals.costTypes.Count > 0)
                {
                    organization.costTypes = Globals.costTypes;
                }
                else
                {
                    XElement responseCostType = SendLibraryRequest("CostType");
                    Globals.costTypes = (from item in responseCostType.Elements("Library") select new CostType().FromXml(item)).ToList();
                    organization.costTypes = Globals.costTypes;
                }
                if (Globals.insuranceOffices.Count > 0)
                {
                    organization.insuranceOffices = Globals.insuranceOffices;
                }
                else
                {
                    XElement responseInsuranceOffice = SendLibraryRequest("InsuranceOffice");
                    Globals.insuranceOffices = (from item in responseInsuranceOffice.Elements("Library") select new InsuranceOffice().FromXml(item)).ToList();
                    organization.insuranceOffices = Globals.insuranceOffices;
                }
                if (Globals.financingTypes.Count > 0)
                {
                    organization.financingTypes = Globals.financingTypes;
                }
                else
                {
                    XElement responseFinancingType = SendLibraryRequest("FinancingType");
                    Globals.financingTypes = (from item in responseFinancingType.Elements("Library") select new FinancingType().FromXml(item)).ToList();
                    organization.financingTypes = Globals.financingTypes;
                }
                if (Globals.banks.Count > 0)
                {
                    organization.banks = Globals.banks;
                }
                else
                {
                    XElement responseBank = SendLibraryRequest("Bank");
                    Globals.banks = (from item in responseBank.Elements("Library") select new Bank().FromXml(item)).ToList();
                    organization.banks = Globals.banks;
                }
                if (Globals.finOffices.Count > 0)
                {
                    organization.finOffices = Globals.finOffices;
                }
                else
                {
                    XElement responseFinOffice = SendLibraryRequest("FinOffice");
                    Globals.finOffices = (from item in responseFinOffice.Elements("Library") select new FinOffice().FromXml(item)).ToList();
                    organization.finOffices = Globals.finOffices;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(organization);

        }
        public JsonResult OrgConfirm(int orgid)
        {
            return AppStatic.SystemController.OrgConfirm(Convert.ToInt32(User.Identity.GetUserId()), orgid)
                 ? Json(new { error = false, message = AppStatic.SystemController.Message })
                 : Json(new { error = true, message = AppStatic.SystemController.Message });
        }
        [HttpGet]
        public ActionResult OrgDelete(int orgid)
        {
            OrganizationDelete organizationDelete = new OrganizationDelete();
            organizationDelete.ORG_ID = orgid;
            try
            {                
                if (Globals.reasons.Count > 0)
                {
                    organizationDelete.reasons = Globals.reasons;
                }
                else
                {
                    XElement responseReason = SendLibraryRequest("Reason");
                    Globals.reasons = (from item in responseReason.Elements("Library") select new Reason().FromXml(item)).ToList();
                    organizationDelete.reasons = Globals.reasons;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(organizationDelete);
        }
        [HttpPost]
        public ActionResult OrgDelete(OrganizationDelete organizationDelete)
        {
            if (ModelState.IsValid)
            {
                if (AppStatic.SystemController.OrgDelete(Convert.ToInt32(User.Identity.GetUserId()), organizationDelete.ToXml()))
                    return Json(new { error = false, message = AppStatic.SystemController.Message });
                else
                    AppStatic.SetError(AppStatic.SystemController.GetErrors(), AppStatic.SystemController.Message, ModelState);
            }
            try
            {
                if (Globals.reasons.Count > 0)
                {
                    organizationDelete.reasons = Globals.reasons;
                }
                else
                {
                    XElement responseReason = SendLibraryRequest("Reason");
                    Globals.reasons = (from item in responseReason.Elements("Library") select new Reason().FromXml(item)).ToList();
                    organizationDelete.reasons = Globals.reasons;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(organizationDelete);

        }
        public ActionResult DataSearch()
        {
            return PartialView();
        }
        public PartialViewResult DataList(string vsearch)
        {
            List<DataList> datalist = new List<DataList>();
            XElement res = AppStatic.SystemController.DataSearch(vsearch);
            if (res != null && res.Elements("DataList") != null)
                datalist = (from item in res.Elements("DataList") select new DataList().FromXml(item)).ToList();
            return PartialView(datalist);
        }
        public PartialViewResult AddTabUB(int reg_id)
        {
            OrganizationUB item = new OrganizationUB();
            try
            {
                XElement res = AppStatic.SystemController.OrgUBsingle(reg_id);
                if (res != null && res.Elements("UBsingle") != null)
                {
                    item = new OrganizationUB().FromXml(res.Element("UBsingle"));
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(item);
        }
        public PartialViewResult AddTabMOF(int reg_id)
        {
            OrganizationMOF item = new OrganizationMOF();
            try
            {
                XElement res = AppStatic.SystemController.OrgMOFsingle(reg_id);
                if (res != null && res.Elements("MOFsingle") != null)
                {
                    item = new OrganizationMOF().FromXml(res.Element("MOFsingle"));
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return PartialView(item);
        }

        public static XElement SendLibraryRequest(string lib)
        {
            XElement elem = new XElement("lib");
                elem.Add(new XElement("LibraryName", lib));
            
            return AppStatic.SystemController.Library(elem);
        }
    }
}