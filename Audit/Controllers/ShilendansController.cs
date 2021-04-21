using Audit.App_Func;
using Audit.Models;
using Microsoft.AspNet.Identity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace Audit.Controllers
{
    [ApplicationAuthorize]
    public class ShilendansController : Controller
    {
        // GET: Shilendans
        Organization organization = new Organization();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        Dictionary<string, object> row;
        public ActionResult Index()
        {
            OrgVM res = new OrgVM();
            try
            {
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

        public PartialViewResult Menus()
        {
            return PartialView();
        }
        
        public static XElement SendLibraryRequest(string lib)
        {
            XElement elem = new XElement("lib");
            elem.Add(new XElement("LibraryName", lib));

            return AppStatic.SystemController.Library(elem);
        }

        public ActionResult OrgList()
        {
            List<OrgList> orgLists = new List<OrgList>();
            //XElement res = AppStatic.SystemController.OrgList(User.GetClaimData("DepartmentID"));
            //if (res != null && res.Elements("OrgList") != null)
            //    orgLists = (from item in res.Elements("OrgList") select new OrgList().FromXml(item)).ToList();
            return View(orgLists);
        }

        public ActionResult OrgDetail(int orgid)
        {
            try
            {
                XElement res = AppStatic.SystemController.OrgDetail(orgid);
                if (res != null && res.Elements("OrgDetail") != null)
                {
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

                
                XElement tb1res = AppStatic.SystemController.Table1List();
                XElement tblprojectlist = AppStatic.SystemController.TableProjectList(Convert.ToInt32(organization.ORG_ID));

                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();

                StringReader sr = new StringReader(tb1res.ToString());
                StringReader sr2 = new StringReader(tblprojectlist.ToString());

                ds.ReadXml(sr, XmlReadMode.InferSchema);
                ds1.ReadXml(sr2, XmlReadMode.InferSchema);

                DataRow[] table1 = ds.Tables[0].Select();
                DataRow[] table2 = ds.Tables[0].Select("TAB_ID = " + 2);
                DataRow[] table3 = ds.Tables[0].Select("TAB_ID = " + 3);
                DataRow[] table4 = ds.Tables[0].Select("TAB_ID = " + 4);
                DataRow[] table5 = ds.Tables[0].Select("TAB_ID = " + 5);
                DataRow[] table6 = ds.Tables[0].Select("TAB_ID = " + 6);
                DataRow[] table7 = ds.Tables[0].Select("TAB_ID = " + 7);

                if(ds1.Tables.Count > 0) { 
                    DataRow[] table8 = ds1.Tables[0].Select();
                    organization.tab8 = new List<Tab8>();

                    for (int i = 0; i < table8.Length; i++)
                    {
                        organization.tab8.Add(
                                new Tab8
                                {
                                    PROJECT_NAME = table8[i].Field<string>("PROJECT_NAME"),
                                    PROJECT_NUMBER = table8[i].Field<string>("PROJECT_NUMBER"),
                                    PROJECT_START_DATE = table8[i].Field<string>("PROJECT_START_DATE"),
                                    PROJECT_END_DATE = table8[i].Field<string>("PROJECT_END_DATE"),
                                    PROJECT_PERCENT = table8[i].Field<string>("PROJECT_PERCENT"),
                                    PROJECT_TOTAL_BUDGET = table8[i].Field<string>("PROJECT_TOTAL_BUDGET"),
                                    PROJECT_ORG_FUND = table8[i].Field<string>("PROJECT_ORG_FUND"),
                                    PROJECT_ID = table8[i].Field<string>("PROJECT_ID"),
                                    ORG_ID = orgid
                                }
                            );
                    }
                }

                organization.tab1 = new List<Tab1>();
                organization.tab2 = new List<Tab2>();
                organization.tab3 = new List<Tab3>();
                organization.tab4 = new List<Tab4>();
                organization.tab5 = new List<Tab5>();
                organization.tab6 = new List<Tab6>();
                organization.tab7 = new List<Tab7>();


                XElement MirrOrgDataLists = AppStatic.SystemController.MirrDataList(orgid);
                DataSet DsTables = new DataSet();

                StringReader sr1 = new StringReader(MirrOrgDataLists.ToString());

                DsTables.ReadXml(sr1, XmlReadMode.InferSchema);

                if (DsTables != null && DsTables.Tables.Count > 0)
                {
                    for (int i = 0; i < table1.Length; i++)
                    {
                        var md = Convert.ToInt32(table1[i].Field<string>("MD_CODE"));
                        organization.tab1.Add(
                            new Tab1
                            {
                                MD_CODE = table1[i].Field<string>("MD_CODE"),
                                MD_LAWS_NUM = table1[i].Field<string>("MD_LAWS_NUM"),
                                MD_NAME = table1[i].Field<string>("MD_NAME"),
                                MD_TIME = table1[i].Field<string>("MD_TIME"),
                                Data01 = Convert.ToDouble(DsTables.Tables["MirrDataList"].Rows[md - 1].Field<string>("DATA01"))
                            }
                        );
                    }
                    for (int i = 0; i < table2.Length; i++)
                    {
                        var md = Convert.ToInt32(table2[i].Field<string>("MD_CODE"));
                        organization.tab2.Add(
                                new Tab2
                                {
                                    MD_CODE = table2[i].Field<string>("MD_CODE"),
                                    MD_LAWS_NUM = table2[i].Field<string>("MD_LAWS_NUM"),
                                    MD_NAME = table2[i].Field<string>("MD_NAME"),
                                    MD_TIME = table2[i].Field<string>("MD_TIME"),
                                    Data01 = DsTables.Tables["MirrDataList"].Rows[md - 1].Field<string>("DATA01")
                                }
                            );
                    }
                    for (int i = 0; i < table3.Length; i++)
                    {
                        var md = Convert.ToInt32(table3[i].Field<string>("MD_CODE"));
                        organization.tab3.Add(
                                new Tab3
                                {
                                    MD_CODE = table3[i].Field<string>("MD_CODE"),
                                    MD_LAWS_NUM = table3[i].Field<string>("MD_LAWS_NUM"),
                                    MD_NAME = table3[i].Field<string>("MD_NAME"),
                                    MD_TIME = table3[i].Field<string>("MD_TIME"),
                                    Data01 = DsTables.Tables["MirrDataList"].Rows[md - 1].Field<string>("DATA01")
                                }
                            );
                    }
                    for (int i = 0; i < table4.Length; i++)
                    {
                        var md = Convert.ToInt32(table4[i].Field<string>("MD_CODE"));
                        organization.tab4.Add(
                                new Tab4
                                {
                                    MD_CODE = table4[i].Field<string>("MD_CODE"),
                                    MD_LAWS_NUM = table4[i].Field<string>("MD_LAWS_NUM"),
                                    MD_NAME = table4[i].Field<string>("MD_NAME"),
                                    MD_TIME = table4[i].Field<string>("MD_TIME"),
                                    Data01 = Convert.ToDouble(DsTables.Tables["MirrDataList"].Rows[md - 1].Field<string>("DATA01")),
                                    Data02 =DsTables.Tables["MirrDataList"].Rows[md - 1].Field<string>("DATA02")
                                }
                            );
                    }
                    for (int i = 0; i < table5.Length; i++)
                    {
                        var md = Convert.ToInt32(table5[i].Field<string>("MD_CODE"));
                        organization.tab5.Add(
                                new Tab5
                                {
                                    MD_CODE = table5[i].Field<string>("MD_CODE"),
                                    MD_LAWS_NUM = table5[i].Field<string>("MD_LAWS_NUM"),
                                    MD_NAME = table5[i].Field<string>("MD_NAME"),
                                    MD_TIME = table5[i].Field<string>("MD_TIME"),
                                    Data01 = DsTables.Tables["MirrDataList"].Rows[md - 1].Field<string>("DATA01")
                                }
                            );
                    }
                    for (int i = 0; i < table6.Length; i++)
                    {
                        var md = Convert.ToInt32(table6[i].Field<string>("MD_CODE"));
                        organization.tab6.Add(
                                new Tab6
                                {
                                    MD_CODE = table6[i].Field<string>("MD_CODE"),
                                    MD_LAWS_NUM = table6[i].Field<string>("MD_LAWS_NUM"),
                                    MD_NAME = table6[i].Field<string>("MD_NAME"),
                                    MD_TIME = table6[i].Field<string>("MD_TIME"),
                                    Data01 = Convert.ToDouble(DsTables.Tables["MirrDataList"].Rows[md - 1].Field<string>("DATA01")),
                                    Data02 = DsTables.Tables["MirrDataList"].Rows[md - 1].Field<string>("DATA02")
                                }
                            );
                    }
                    for (int i = 0; i < table7.Length; i++)
                    {
                        var md = Convert.ToInt32(table7[i].Field<string>("MD_CODE"));
                        organization.tab7.Add(
                                new Tab7
                                {
                                    MD_CODE = table7[i].Field<string>("MD_CODE"),
                                    MD_LAWS_NUM = table7[i].Field<string>("MD_LAWS_NUM"),
                                    MD_NAME = table7[i].Field<string>("MD_NAME"),
                                    MD_TIME = table7[i].Field<string>("MD_TIME"),
                                    Data01 = DsTables.Tables["MirrDataList"].Rows[md - 1].Field<string>("DATA01"),
                                    Data02 = DsTables.Tables["MirrDataList"].Rows[md - 1].Field<string>("DATA02")
                                }
                            );
                    }
                    
                    
                }
                else
                {
                    for (int i = 0; i < table1.Length; i++)
                    {
                        organization.tab1.Add(new Tab1
                        {
                            MD_CODE = table1[i].Field<string>("MD_CODE"),
                            MD_LAWS_NUM = table1[i].Field<string>("MD_LAWS_NUM"),
                            MD_NAME = table1[i].Field<string>("MD_NAME"),
                            MD_TIME = table1[i].Field<string>("MD_TIME"),
                            Data01 = 0.00
                        });
                    }
                    for (int i = 0; i < table2.Length; i++)
                    {
                        organization.tab2.Add(new Tab2
                        {
                            MD_CODE = table2[i].Field<string>("MD_CODE"),
                            MD_LAWS_NUM = table2[i].Field<string>("MD_LAWS_NUM"),
                            MD_NAME = table2[i].Field<string>("MD_NAME"),
                            MD_TIME = table2[i].Field<string>("MD_TIME"),
                            Data01 = null
                        });
                    }
                    for (int i = 0; i < table3.Length; i++)
                    {
                        organization.tab3.Add(new Tab3
                        {
                            MD_CODE = table3[i].Field<string>("MD_CODE"),
                            MD_LAWS_NUM = table3[i].Field<string>("MD_LAWS_NUM"),
                            MD_NAME = table3[i].Field<string>("MD_NAME"),
                            MD_TIME = table3[i].Field<string>("MD_TIME"),
                            Data01 = null
                        });
                    }
                    for (int i = 0; i < table4.Length; i++)
                    {
                        organization.tab4.Add(new Tab4
                        {
                            MD_CODE = table4[i].Field<string>("MD_CODE"),
                            MD_NAME = table4[i].Field<string>("MD_NAME"),
                            Data01 = 0.00,
                            Data02 = null
                        });
                    }
                    for (int i = 0; i < table5.Length; i++)
                    {
                        organization.tab5.Add(new Tab5
                        {
                            MD_CODE = table5[i].Field<string>("MD_CODE"),
                            MD_LAWS_NUM = table5[i].Field<string>("MD_LAWS_NUM"),
                            MD_NAME = table5[i].Field<string>("MD_NAME"),
                            MD_TIME = table5[i].Field<string>("MD_TIME"),
                            Data01 = null
                        });
                    }
                    for (int i = 0; i < table6.Length; i++)
                    {
                        organization.tab6.Add(new Tab6
                        {
                            MD_CODE = table6[i].Field<string>("MD_CODE"),
                            MD_NAME = table6[i].Field<string>("MD_NAME"),
                            Data01 = 0.00,
                            Data02 = null
                        });
                    }
                    for (int i = 0; i < table7.Length; i++)
                    {
                        organization.tab7.Add(new Tab7
                        {
                            MD_CODE = table7[i].Field<string>("MD_CODE"),
                            MD_LAWS_NUM = table7[i].Field<string>("MD_LAWS_NUM"),
                            MD_NAME = table7[i].Field<string>("MD_NAME"),
                            MD_TIME = table7[i].Field<string>("MD_TIME"),
                            Data01 = null,
                            Data02 = null,
                            Data03 = DateTime.Now
                        });
                    }
                }

                return PartialView("AddShilenDans", organization);
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }

            return PartialView("AddShilenDans", organization);
        }
        public ActionResult AddShilenDans(Organization organization)
        {
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
        [ValidateAntiForgeryToken]
        public ActionResult AddShilenDans(Organization organization, string button)
        {
            //if (TryValidateModel(organization.tab1[0].Data01))
            //{
                if (organization.ORG_ID != 0)
                {
                    int YearCode = 2020;
                    DateTime InsDate = DateTime.Now;

                    int mdcodes = 0;
                    double data01 = 0;
                    string data02 = null;
                    int is_finish = 0;

                    switch (button)
                    {
                        case "tab1save":
                            {
                                for (int i = 0; i < organization.tab1.Count(); i++)
                                {
                                    mdcodes = Convert.ToInt32(organization.tab1[i].MD_CODE);
                                    data01 = Convert.ToDouble(organization.tab1[i].Data01);
                                    data02 = " ";
                                    is_finish = 0;
                                    var result = AppStatic.SystemController.MirrorAccInsert(YearCode, Convert.ToInt32(organization.ORG_ID), mdcodes, data01, data02, is_finish, Convert.ToInt32(User.Identity.GetUserId()), InsDate);
                                }
                                ViewBag.Tabid = "#maygt1-1";
                            }
                            break;
                        case "tab2save":
                            {
                                for (int i = 0; i < organization.tab2.Count(); i++)
                                {
                                    mdcodes = Convert.ToInt32(organization.tab2[i].MD_CODE);
                                    data01 = Convert.ToDouble(organization.tab2[i].Data01);
                                    data02 = " ";
                                    is_finish = 0;
                                    var result = AppStatic.SystemController.MirrorAccInsert(YearCode, Convert.ToInt32(organization.ORG_ID), mdcodes, data01, data02, is_finish, Convert.ToInt32(User.Identity.GetUserId()), InsDate);
                                }
                                ViewBag.Tabid = "#maygt1-2";
                            }
                            break;
                        case "tab3save":
                            {
                                for (int i = 0; i < organization.tab3.Count(); i++)
                                {
                                    mdcodes = Convert.ToInt32(organization.tab3[i].MD_CODE);
                                    data01 = Convert.ToDouble(organization.tab3[i].Data01);
                                    data02 = " ";
                                    is_finish = 0;
                                    var result = AppStatic.SystemController.MirrorAccInsert(YearCode, Convert.ToInt32(organization.ORG_ID), mdcodes, data01, data02, is_finish, Convert.ToInt32(User.Identity.GetUserId()), InsDate);
                                }
                                ViewBag.Tabid = "#maygt1-3";
                            }
                            break;
                        case "tab4save":
                            {
                                for (int i = 0; i < organization.tab4.Count(); i++)
                                {
                                    mdcodes = Convert.ToInt32(organization.tab4[i].MD_CODE);
                                    data01 = Convert.ToDouble(organization.tab4[i].Data01);
                                    data02 = organization.tab4[i].Data02;
                                    is_finish = 1;
                                    var result = AppStatic.SystemController.MirrorAccInsert(YearCode, Convert.ToInt32(organization.ORG_ID), mdcodes, data01, data02, is_finish, Convert.ToInt32(User.Identity.GetUserId()), InsDate);
                                }
                                ViewBag.Tabid = "#maygt1-4";
                            }
                            break;
                        case "tab5save":
                            {
                                for (int i = 0; i < organization.tab5.Count(); i++)
                                {
                                    mdcodes = Convert.ToInt32(organization.tab5[i].MD_CODE);
                                    data01 = Convert.ToDouble(organization.tab5[i].Data01);
                                    data02 = " ";
                                    is_finish = 0;
                                    var result = AppStatic.SystemController.MirrorAccInsert(YearCode, Convert.ToInt32(organization.ORG_ID), mdcodes, data01, data02, is_finish, Convert.ToInt32(User.Identity.GetUserId()), InsDate);
                                }
                                ViewBag.Tabid = "#maygt2";
                            }
                            break;
                        case "tab6save":
                            {
                                for (int i = 0; i < organization.tab6.Count(); i++)
                                {
                                    mdcodes = Convert.ToInt32(organization.tab6[i].MD_CODE);
                                    data01 = Convert.ToDouble(organization.tab6[i].Data01);
                                    data02 = organization.tab6[i].Data02;
                                    is_finish = 1;
                                    var result = AppStatic.SystemController.MirrorAccInsert(YearCode, Convert.ToInt32(organization.ORG_ID), mdcodes, data01, data02, is_finish, Convert.ToInt32(User.Identity.GetUserId()), InsDate);
                                }
                                ViewBag.Tabid = "#maygt2";
                            }
                            break;
                        case "tab7save":
                            {
                                Random rand = new Random();

                                string project_name = organization.tab7[2].Data02;
                                string project_num = organization.tab7[3].Data02;
                                string project_start_date = organization.tab7[7].Data02;
                                string project_end_date = organization.tab7[8].Data02;
                                int project_percent = Convert.ToInt32(organization.tab7[9].Data01);
                                string project_budget = organization.tab7[10].Data01.ToString();
                                string project_fund = organization.tab7[1].Data02;
                                int project_law_num = organization.AUD_LAWS_NUM;
                                int project_id = rand.Next(100000, 999999);
                                int project_is_active = 1;

                                for (int i = 0; i < organization.tab7.Count(); i++)
                                {
                                    mdcodes = Convert.ToInt32(organization.tab7[i].MD_CODE);
                                    data01 = Convert.ToDouble(organization.tab7[i].Data01);
                                    data02 = organization.tab7[i].Data02;
                                    var result = AppStatic.SystemController.OrgProjectInsert(YearCode, Convert.ToInt32(organization.ORG_ID), project_name, project_num, project_start_date, project_end_date, project_percent, project_budget, project_fund, mdcodes, data01, data02, Convert.ToInt32(User.Identity.GetUserId()), InsDate, project_law_num, project_id, project_is_active);
                                }
                                ViewBag.ModalID = "#InsertProjectModal";
                            }
                            break;
                    }
                    var result1 = true;
                    //return ViewBag.Tabid = "maygt1-2";

                    try
                    {

                        if (result1 == true)
                        {
                            //return Json(new { error = false, message = AppStatic.SystemController.Message });
                            ViewBag.Results = AppStatic.SystemController.Message;
                            return (ViewBag);
                        }
                        else
                        {
                            ViewBag.Results = "Хадгалахад алдаа гарлаа !!!";
                        }

                    }
                    catch (Exception ex)
                    {
                        Globals.WriteErrorLog(ex);
                    }
                }
            //}
            //else
            //{
            //    return View(organization);
            //}
            
            return PartialView(organization);

        }


        [HttpPost]
        public ActionResult OrgProjectEdit(Organization organization, string button)
        {
            //if (ModelState.IsValid)
            //{

                int YearCode = 2020;
                DateTime InsDate = DateTime.Now;

                int mdcodes = 0;
                double data01 = 0;
                string data02 = null;

                switch (button)
                {
                    case "tab7save":
                        {
                            int project_id = Convert.ToInt32(Session["ProjectIDs"].ToString());
                            int org_id = Convert.ToInt32(Session["OrganizationIDs"].ToString());
                            string project_name = organization.tab7[2].Data02;
                            string project_num = organization.tab7[3].Data02;
                            string project_start_date = organization.tab7[7].Data02;
                            string project_end_date = organization.tab7[8].Data02;
                            int project_percent = Convert.ToInt32(organization.tab7[9].Data01);
                            string project_budget = organization.tab7[10].Data01.ToString();
                            string project_fund = organization.tab7[1].Data02;
                            int project_law_num = organization.AUD_LAWS_NUM;
                            int is_active = 1;

                            for (int i = 0; i < organization.tab7.Count(); i++)
                            {
                                mdcodes = Convert.ToInt32(organization.tab7[i].MD_CODE);
                                data01 = Convert.ToDouble(organization.tab7[i].Data01);
                                data02 = organization.tab7[i].Data02;
                                var result = AppStatic.SystemController.OrgProjectInsert(YearCode, org_id, project_name, project_num, project_start_date, project_end_date, project_percent, project_budget, project_fund, mdcodes, data01, data02, Convert.ToInt32(User.Identity.GetUserId()), InsDate, project_law_num, project_id, is_active);
                            }
                        }
                        break;
                }
                var result1 = true;
                //return RedirectToAction("Index", "Shilendans");
            //return Json(new { error = false, message = AppStatic.SystemController.Message });

            try
                {

                    if (result1 == true)
                    {
                        return ViewBag.Results = AppStatic.SystemController.Message;
                    }
                    else
                    {
                        ViewBag.No = "Хадгалахад алдаа гарлаа !!!";
                    }

                }
                catch (Exception ex)
                {
                    Globals.WriteErrorLog(ex);
                }
            //}
            //else
            //{
            //    ViewBag.No = "Энэ мэдээлэл мэдээллийн санд байхгүй байна.";
            //}

            return PartialView(organization);

        }

        public ActionResult OrgProjectDelete(Organization organization, int org_id, int pro_id)
        {
            var result = AppStatic.SystemController.OrgProjectDelete(org_id, pro_id);
            Json(new { error = false, message = AppStatic.SystemController.Message });
            ViewBag.Tabid = "#maygt3";
            ViewBag.Results = AppStatic.SystemController.Message;

            bool res = true;
            try
            {

                if (res == true)
                {
                    //return View("Index", "Shilendans");
                    return RedirectToAction("Index", "Shilendans");
                    //return PartialView("AddShilenDans","", organization);
                }
                else
                {
                    return ViewBag.Results = AppStatic.SystemController.Message;
                }

            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            //}
            //else
            //{
            //    ViewBag.No = "Энэ мэдээлэл мэдээллийн санд байхгүй байна.";
            //}

            return PartialView(organization);

        }

        public ActionResult OrgProjectEdit(int pro_id, int org_id)
        {
            Session["ProjectIDs"] = pro_id;
            Session["OrganizationIDs"] = org_id;
            XElement MirrOrgProjects = AppStatic.SystemController.OrgProjectDataList(pro_id);
            DataSet DsOrgProjects = new DataSet();

            StringReader sr1 = new StringReader(MirrOrgProjects.ToString());

            DsOrgProjects.ReadXml(sr1, XmlReadMode.InferSchema);

            DataRow[] table7 = DsOrgProjects.Tables[0].Select();
            organization.tab7 = new List<Tab7>();

            if (DsOrgProjects != null && DsOrgProjects.Tables.Count > 0)
            {
                for (int i = 0; i < table7.Length; i++)
                {
                    //var md = Convert.ToInt32(DsOrgProjects.Tables["OrgProjectDataList"].Rows[i].Field<string>("MDCODE"));
                    organization.tab7.Add(
                        new Tab7
                        {
                            MD_CODE = DsOrgProjects.Tables["OrgProjectDataList"].Rows[i].Field<string>("MDCODE"),
                            MD_LAWS_NUM = DsOrgProjects.Tables["OrgProjectDataList"].Rows[i].Field<string>("MD_LAWS_NUM"),
                            MD_NAME = DsOrgProjects.Tables["OrgProjectDataList"].Rows[i].Field<string>("MD_NAME"),
                            MD_TIME = DsOrgProjects.Tables["OrgProjectDataList"].Rows[i].Field<string>("MD_TIME"),
                            Data01 = DsOrgProjects.Tables["OrgProjectDataList"].Rows[i].Field<string>("DATA01"),
                            Data02 = DsOrgProjects.Tables["OrgProjectDataList"].Rows[i].Field<string>("DATA02")
                        }
                    );
                }
            }
            return PartialView("OrgProjectEdit", organization);
        }

        public ActionResult Print(Organization organization, int org_id)
        {
            XElement tblprojectlist = AppStatic.SystemController.PrintDataList(org_id);

            DataSet print_ds = new DataSet();

            StringReader print_sr = new StringReader(tblprojectlist.ToString());

            print_ds.ReadXml(print_sr, XmlReadMode.InferSchema);

            DataRow[] print1 = print_ds.Tables[0].Select();
            organization.print1 = new List<Print1>();

            for (int i = 0; i < print1.Length; i++)
            {
                organization.print1.Add(
                        new Print1
                        {
                            MEDEELEH_TOO = print1[i].Field<string>("MEDEELEH"),
                            MEDEELSEN_TOO = print1[i].Field<string>("MEDEELSEN"),
                            MEDEELEEGUI_TOO = print1[i].Field<string>("MEDEELEEGUI"),
                            HAMAARALGUI = print1[i].Field<string>("HOTSORCH_ORUULSAN"),
                            HUGATSAA_HOTSROOSON = print1[i].Field<string>("HAMAARALGUI"),
                            MEDEELSEN_PERCENT = print1[i].Field<string>("PRECENT"),
                            HUGATSAA_HOTSROOSON_PERCENT = print1[i].Field<string>("PRECENT2")
                        }
                    );
            }
           return View(organization);
        }

        public JsonResult OrgConfirm(int orgid)
        {
            return AppStatic.SystemController.OrgConfirm(Convert.ToInt32(User.Identity.GetUserId()), orgid)
                 ? Json(new { error = false, message = AppStatic.SystemController.Message })
                 : Json(new { error = true, message = AppStatic.SystemController.Message });
        }

        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<TSource>(List<TSource> items)
            {
                DataTable dataTable = new DataTable(typeof(TSource).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                foreach (TSource item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }

    }
}
