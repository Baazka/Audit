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
    public class ExamController : Controller
    {
        // GET: Exam
        public ActionResult Index(string id)
        {
            OrgVM res = new OrgVM();
            try
            {
                XElement response = AppStatic.SystemController.MenuRole(Convert.ToInt32(User.Identity.GetUserId()), Convert.ToInt32(Globals.Decrypt(id)));
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
        public static XElement SendLibraryRequest(string lib)
        {
            XElement elem = new XElement("lib");
            elem.Add(new XElement("LibraryName", lib));

            return AppStatic.SystemController.Library(elem);
        }
    }
}