using Audit.App_Func;
using Audit.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace Audit.Controllers
{
    public class ServiceController : ApiController
    {
        [HttpPost]
        public OrgListResponse OrgList(OrgListRequest request)
        {
            OrgListResponse response = new OrgListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PARENT_BUDGET_ID != null && request.PARENT_BUDGET_ID != 0)
                    elem.Add(new XElement("V_PARENT_BUDGET_ID", request.PARENT_BUDGET_ID));
                else
                    elem.Add(new XElement("V_PARENT_BUDGET_ID", null));

                if (request.budget_type != null)
                    elem.Add(new XElement("V_BUDGET_TYPE", request.budget_type));
                else
                    elem.Add(new XElement("V_BUDGET_TYPE", null));

                if (request.BUDGET_LEVEL_ID != null)
                    elem.Add(new XElement("V_BUDGET_LEVEL_ID", request.BUDGET_LEVEL_ID));
                else
                    elem.Add(new XElement("V_BUDGET_LEVEL_ID", null));

                if (request.LEGAL_STATUS_ID != null)
                    elem.Add(new XElement("V_LEGAL_STATUS_ID", request.LEGAL_STATUS_ID));
                else
                    elem.Add(new XElement("V_LEGAL_STATUS_ID", null));

                if (request.PROPERTY_TYPE_ID != null)
                    elem.Add(new XElement("V_PROPERTY_TYPE_ID", request.PROPERTY_TYPE_ID));
                else
                    elem.Add(new XElement("V_PROPERTY_TYPE_ID", null));

                //if (request.budget_type != null)
                //{
                //    string ss = String.Join(",", request.budget_type.Select(p => p.ToString()).ToArray());
                //    elem.Add(new XElement("V_BUDGET_TYPE", ss));
                //}
                //else
                //    elem.Add(new XElement("V_BUDGET_TYPE", null));

                XElement res = AppStatic.SystemController.OrgList(elem, User.GetClaimData("DepartmentID"));
                if (res != null && res.Elements("OrgList") != null)
                    response.data = (from item in res.Elements("OrgList") select new OrgList().FromXml(item)).ToList();

                response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }

        [HttpPost]
        public AuditOrgListResponse AuditOrgList(AuditOrgListRequest request)
        {
            AuditOrgListResponse response = new AuditOrgListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.LEGAL_STATUS_ID != null)
                    elem.Add(new XElement("V_LEGAL_STATUS_ID", request.LEGAL_STATUS_ID));
                else
                    elem.Add(new XElement("V_LEGAL_STATUS_ID", null));

                if (request.PROPERTY_TYPE_ID != null)
                    elem.Add(new XElement("V_PROPERTY_TYPE_ID", request.PROPERTY_TYPE_ID));
                else
                    elem.Add(new XElement("V_PROPERTY_TYPE_ID", null));

                if (request.SOURCE_TYPE_ID != null)
                    elem.Add(new XElement("V_SOURCE_TYPE_ID", request.SOURCE_TYPE_ID));
                else
                    elem.Add(new XElement("V_SOURCE_TYPE_ID", null));

                XElement res = AppStatic.SystemController.AuditOrgList(elem);
                if (res != null && res.Elements("AuditOrgList") != null)
                    response.data = (from item in res.Elements("AuditOrgList") select new AuditOrgList().FromXml(item)).ToList();

                response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }

        [HttpPost]
        public MirrorOrgListResponse MirrorOrgList(MirrorOrgListRequest request)
        {
            MirrorOrgListResponse response = new MirrorOrgListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PARENT_BUDGET_ID != null && request.PARENT_BUDGET_ID != 0)
                    elem.Add(new XElement("V_PARENT_BUDGET_ID", request.PARENT_BUDGET_ID));
                else
                    elem.Add(new XElement("V_ParentBudgetID", null));

                if (request.BUDGET_LEVEL_ID != null && request.BUDGET_LEVEL_ID != 0)
                    elem.Add(new XElement("V_BUDGET_LEVEL_ID", request.BUDGET_LEVEL_ID));
                else
                    elem.Add(new XElement("V_BUDGET_LEVEL_ID", null));

                if (request.budget_type != null)
                    elem.Add(new XElement("V_BUDGET_TYPE", request.budget_type));
                else
                    elem.Add(new XElement("V_BUDGET_TYPE", null));

                //if (request.budget_type != null)
                //{
                //    string ss = String.Join(",", request.budget_type.Select(p => p.ToString()).ToArray());
                //    elem.Add(new XElement("V_BUDGET_TYPE", ss));
                //}
                //else
                //    elem.Add(new XElement("V_BUDGET_TYPE", null));

                XElement res = AppStatic.SystemController.MirrorOrgList(elem, Convert.ToInt32(User.GetClaimData("DepartmentID")));
                if (res != null && res.Elements("MirroraccOrgList") != null)
                    response.data = (from item in res.Elements("MirroraccOrgList") select new MirroraccOrgList().FromXml(item)).ToList();

                response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }

        [HttpPost]
        public MirrorHakOrgListResponse MirrorHakOrgList(MirrorHakOrgListRequest request)
        {
            MirrorHakOrgListResponse response = new MirrorHakOrgListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.budget_type != null)
                    elem.Add(new XElement("V_BUDGET_TYPE", request.budget_type));
                else
                    elem.Add(new XElement("V_BUDGET_TYPE", null));
                //if (request.budget_type != null)
                //{
                //    string ss = String.Join(",", request.budget_type.Select(p => p.ToString()).ToArray());
                //    elem.Add(new XElement("V_BUDGET_TYPE", ss));
                //}
                //else
                //    elem.Add(new XElement("V_BUDGET_TYPE", null));

                XElement res = AppStatic.SystemController.MirrorHakOrgList(elem, Convert.ToInt32(User.Identity.GetUserId()));
                if (res != null && res.Elements("MirroraccHakOrgList") != null)
                    response.data = (from item in res.Elements("MirroraccHakOrgList") select new MirroraccHakOrgList().FromXml(item)).ToList();

                response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }

        [HttpPost]
        public BM0ListResponse BM0List(BM0ListRequest request)
        {
            BM0ListResponse response = new BM0ListResponse();
            try
            {
                
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.BM0(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<BM0> bm0Body = new List<BM0>();
                List<BM0> AllData = new List<BM0>();
                List<BM0> bm0 = new List<BM0>();
                BM0 Niit = new BM0();
                var typ = typeof(BM0);
                if (res != null && res.Elements("BM0") != null)
                {
                    bm0Body = (from item in res.Elements("BM0") select new BM0().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new BM0().SetXml(item)).ToList();
                    //response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                    response.recordsTotal = AllData.Count();
                }

                if (bm0Body.Count > 0)
                {
                    var depname = typ.GetProperty("DEPARTMENT_NAME");
                    var pay = typ.GetProperty("AUDIT_SERVICE_PAY");
                    var time = typ.GetProperty("WORKING_ADDITION_TIME");
                    var day = typ.GetProperty("WORKING_DAY");
                    var person = typ.GetProperty("WORKING_PERSON");
                    var included = typ.GetProperty("AUDIT_INCLUDED_COUNT");

                    int COUNT = 0;
                    int COUNT1 = 0;
                    int COUNT2 = 0;
                    int COUNT3 = 0;
                    Decimal AMOUNT = 0;

                    foreach (BM0 nm3 in AllData)
                    {
                        if (nm3.AUDIT_SERVICE_PAY != "0" && nm3.AUDIT_SERVICE_PAY != null)
                        {
                            string strNii1 = nm3.AUDIT_SERVICE_PAY.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii1);
                            AMOUNT += Amount1;
                        }
                        if (nm3.WORKING_ADDITION_TIME != 0 && nm3.WORKING_ADDITION_TIME != null)
                        {
                            COUNT += Convert.ToInt32(nm3.WORKING_ADDITION_TIME);
                        }
                        if (nm3.WORKING_DAY != 0 && nm3.WORKING_DAY != null)
                        {
                            COUNT1 += Convert.ToInt32(nm3.WORKING_DAY);
                        }
                        if (nm3.WORKING_PERSON != 0 && nm3.WORKING_PERSON != null)
                        {
                            COUNT2 += Convert.ToInt32(nm3.WORKING_PERSON);
                        }
                        if (nm3.AUDIT_INCLUDED_COUNT != 0 && nm3.AUDIT_INCLUDED_COUNT != null)
                        {
                            COUNT3 += Convert.ToInt32(nm3.AUDIT_INCLUDED_COUNT);
                        }
                    }
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    time.SetValue(Niit, COUNT);
                    day.SetValue(Niit, COUNT1);
                    person.SetValue(Niit, COUNT2);
                    included.SetValue(Niit, COUNT3);

                    depname.SetValue(Niit, "Нийт дүн");


                    bm0 = bm0Body;
                    bm0.Add(Niit);

                    response.data = bm0;
                }
                else
                {
                    response.data = bm0Body;
                }
                
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            
            return response;
        }
      
        [HttpPost]
        public BM1ListResponse BM1List(BM1ListRequest request)
        {
            BM1ListResponse response = new BM1ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.BM1(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<BM1List> Body = new List<BM1List>();
                List<BM1List> AllData = new List<BM1List>();
                List<BM1List> list = new List<BM1List>();
                BM1List Niit = new BM1List();
                var typ = typeof(BM1List);
                if (res != null && res.Elements("BM1") != null)
                {
                    Body = (from item in res.Elements("BM1") select new BM1List().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new BM1List().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                   

                if (Body.Count > 0)
                {
                    var depname = typ.GetProperty("DEPARTMENT_NAME");

                    var pay = typ.GetProperty("ACT_AMOUNT");
                    var pay1 = typ.GetProperty("ACT_STATE_AMOUNT");
                    var pay2 = typ.GetProperty("ACT_LOCAL_AMOUNT");
                    var pay3 = typ.GetProperty("ACT_ORG_AMOUNT");
                    var pay4 = typ.GetProperty("ACT_OTHER_AMOUNT");
                    var pay5 = typ.GetProperty("COMPLETION_AMOUNT");
                    var pay6 = typ.GetProperty("COMPLETION_STATE_AMOUNT");
                    var pay7 = typ.GetProperty("COMPLETION_LOCAL_AMOUNT");
                    var pay8 = typ.GetProperty("COMPLETION_ORG_AMOUNT");
                    var pay9 = typ.GetProperty("COMPLETION_OTHER_AMOUNT");
                    var pay10 = typ.GetProperty("REMOVED_AMOUNT");
                    var pay11 = typ.GetProperty("REMOVED_LAW_AMOUNT");
                    var pay12 = typ.GetProperty("REMOVED_INVALID_AMOUNT");
                    var pay13 = typ.GetProperty("ACT_C2_AMOUNT");
                    var pay14 = typ.GetProperty("BENEFIT_FIN_AMOUNT");
                    var pay15 = typ.GetProperty("ACT_C2_NONEXPIRED");
                    var pay16 = typ.GetProperty("ACT_C2_EXPIRED");

                    var count = typ.GetProperty("BENEFIT_FIN");
                    var count1 = typ.GetProperty("BENEFIT_NONFIN");

                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;
                    Decimal AMOUNT5 = 0;
                    Decimal AMOUNT6 = 0;
                    Decimal AMOUNT7 = 0;
                    Decimal AMOUNT8 = 0;
                    Decimal AMOUNT9 = 0;
                    Decimal AMOUNT10 = 0;
                    Decimal AMOUNT11 = 0;
                    Decimal AMOUNT12 = 0;
                    Decimal AMOUNT13 = 0;
                    Decimal AMOUNT14 = 0;
                    Decimal AMOUNT15 = 0;
                    Decimal AMOUNT16 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;


                    foreach (BM1List data in AllData)
                    {
                        if (data.ACT_AMOUNT != "0" && data.ACT_AMOUNT != null)
                        {
                            string strNii = data.ACT_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT += Amount1;
                        }
                        if (data.ACT_STATE_AMOUNT != "0" && data.ACT_STATE_AMOUNT != null)
                        {
                            string strNii = data.ACT_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount1;
                        }
                        if (data.ACT_LOCAL_AMOUNT != "0" && data.ACT_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.ACT_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount1;
                        }
                        if (data.ACT_ORG_AMOUNT != "0" && data.ACT_ORG_AMOUNT != null)
                        {
                            string strNii = data.ACT_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount1;
                        }
                        if (data.ACT_OTHER_AMOUNT != "0" && data.ACT_OTHER_AMOUNT != null)
                        {
                            string strNii = data.ACT_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount1;
                        }
                        if (data.COMPLETION_AMOUNT != "0" && data.COMPLETION_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT5 += Amount1;
                        }
                        if (data.COMPLETION_STATE_AMOUNT != "0" && data.COMPLETION_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT6 += Amount1;
                        }
                        if (data.COMPLETION_LOCAL_AMOUNT != "0" && data.COMPLETION_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT7 += Amount1;
                        }
                        if (data.COMPLETION_ORG_AMOUNT != "0" && data.COMPLETION_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT8 += Amount1;
                        }
                        if (data.COMPLETION_OTHER_AMOUNT != "0" && data.COMPLETION_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT9 += Amount1;
                        }
                        if (data.REMOVED_AMOUNT != "0" && data.REMOVED_AMOUNT != null)
                        {
                            string strNii = data.REMOVED_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT10 += Amount1;
                        }
                        if (data.REMOVED_LAW_AMOUNT != "0" && data.REMOVED_LAW_AMOUNT != null)
                        {
                            string strNii = data.REMOVED_LAW_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT11 += Amount1;
                        }
                        if (data.REMOVED_INVALID_AMOUNT != "0" && data.REMOVED_INVALID_AMOUNT != null)
                        {
                            string strNii = data.REMOVED_INVALID_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT12 += Amount1;
                        }
                        if (data.ACT_C2_AMOUNT != "0" && data.ACT_C2_AMOUNT != null)
                        {
                            string strNii = data.ACT_C2_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT13 += Amount1;
                        }
                        if (data.BENEFIT_FIN_AMOUNT != "0" && data.BENEFIT_FIN_AMOUNT != null)
                        {
                            string strNii = data.BENEFIT_FIN_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT14 += Amount1;
                        }
                        if (data.ACT_C2_NONEXPIRED != "0" && data.ACT_C2_NONEXPIRED != null)
                        {
                            string strNii = data.ACT_C2_NONEXPIRED.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT15 += Amount1;
                        }
                        if (data.ACT_C2_EXPIRED != "0" && data.ACT_C2_EXPIRED != null)
                        {
                            string strNii = data.ACT_C2_EXPIRED.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT16 += Amount1;
                        }

                        if (data.BENEFIT_FIN != 0 && data.BENEFIT_FIN != null)
                        {
                            NUMBER += Convert.ToInt32(data.BENEFIT_FIN);
                        }
                        if (data.BENEFIT_NONFIN != 0 && data.BENEFIT_NONFIN != null)
                        {
                            NUMBER1 += Convert.ToInt32(data.BENEFIT_NONFIN);
                        }
                    }
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.00"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.00"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.00"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.00"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.00"));
                    pay11.SetValue(Niit, AMOUNT11.ToString("#,0.00"));
                    pay12.SetValue(Niit, AMOUNT12.ToString("#,0.00"));
                    pay13.SetValue(Niit, AMOUNT13.ToString("#,0.00"));
                    pay14.SetValue(Niit, AMOUNT14.ToString("#,0.00"));
                    pay15.SetValue(Niit, AMOUNT15.ToString("#,0.00"));
                    pay16.SetValue(Niit, AMOUNT16.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);


                    depname.SetValue(Niit, "Нийт дүн");

                    list = Body;
                    list.Add(Niit);
                    

                    response.data = list;
                }
                else
                {
                    response.data = Body;
                }
                
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public BM2ListResponse BM2List(BM2ListRequest request)
        {
            BM2ListResponse response = new BM2ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));


                response.recordsTotal = 0;
                XElement res = AppStatic.SystemController.BM2(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<BM2List> Body = new List<BM2List>();
                List<BM2List> AllData = new List<BM2List>();
                List<BM2List> list = new List<BM2List>();
                BM2List Niit = new BM2List();
                var typ = typeof(BM2List);
                if (res != null && res.Elements("BM2") != null)
                {
                    Body = (from item in res.Elements("BM2") select new BM2List().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new BM2List().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                if (Body.Count > 0)
                {
                    var depname = typ.GetProperty("DEPARTMENT_NAME");

                    var pay = typ.GetProperty("CLAIM_VIOLATION_AMOUNT");
                    var pay1 = typ.GetProperty("COMPLETION_AMOUNT");
                    var pay2 = typ.GetProperty("COMPLETION_STATE_AMOUNT");
                    var pay3 = typ.GetProperty("COMPLETION_LOCAL_AMOUNT");
                    var pay4 = typ.GetProperty("COMPLETION_ORG_AMOUNT");
                    var pay5 = typ.GetProperty("COMPLETION_OTHER_AMOUNT");
                    var pay6 = typ.GetProperty("REMOVED_LAW_AMOUNT");
                    var pay7 = typ.GetProperty("REMOVED_INVALID_AMOUNT");
                    var pay8 = typ.GetProperty("BENEFIT_FIN_AMOUNT");
                    var pay9 = typ.GetProperty("CLAIM_C2_NONEXPIRED");
                    var pay10 = typ.GetProperty("CLAIM_C2_EXPIRED");

                    var count = typ.GetProperty("BENEFIT_FIN");
                    var count1 = typ.GetProperty("BENEFIT_NONFIN");

                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;
                    Decimal AMOUNT5 = 0;
                    Decimal AMOUNT6 = 0;
                    Decimal AMOUNT7 = 0;
                    Decimal AMOUNT8 = 0;
                    Decimal AMOUNT9 = 0;
                    Decimal AMOUNT10 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;

                    foreach (BM2List data in AllData)
                    {
                        if (data.CLAIM_VIOLATION_AMOUNT != "0" && data.CLAIM_VIOLATION_AMOUNT != null)
                        {
                            string strNii = data.CLAIM_VIOLATION_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT += Amount1;
                        }
                        if (data.COMPLETION_AMOUNT != "0" && data.COMPLETION_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount1;
                        }
                        if (data.COMPLETION_STATE_AMOUNT != "0" && data.COMPLETION_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount1;
                        }
                        if (data.COMPLETION_LOCAL_AMOUNT != "0" && data.COMPLETION_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount1;
                        }
                        if (data.COMPLETION_ORG_AMOUNT != "0" && data.COMPLETION_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount1;
                        }
                        if (data.COMPLETION_OTHER_AMOUNT != "0" && data.COMPLETION_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT5 += Amount1;
                        }
                        if (data.REMOVED_LAW_AMOUNT != "0" && data.REMOVED_LAW_AMOUNT != null)
                        {
                            string strNii = data.REMOVED_LAW_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT6 += Amount1;
                        }
                        if (data.REMOVED_INVALID_AMOUNT != "0" && data.REMOVED_INVALID_AMOUNT != null)
                        {
                            string strNii = data.REMOVED_INVALID_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT7 += Amount1;
                        }
                        if (data.BENEFIT_FIN_AMOUNT != "0" && data.BENEFIT_FIN_AMOUNT != null)
                        {
                            string strNii = data.BENEFIT_FIN_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT8 += Amount1;
                        }
                        if (data.CLAIM_C2_NONEXPIRED != "0" && data.CLAIM_C2_NONEXPIRED != null)
                        {
                            string strNii = data.CLAIM_C2_NONEXPIRED.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT9 += Amount1;
                        }
                        if (data.CLAIM_C2_EXPIRED != "0" && data.CLAIM_C2_EXPIRED != null)
                        {
                            string strNii = data.CLAIM_C2_EXPIRED.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT10 += Amount1;
                        }


                        if (data.BENEFIT_FIN != 0 && data.BENEFIT_FIN != null)
                        {
                            NUMBER += Convert.ToInt32(data.BENEFIT_FIN);
                        }
                        if (data.BENEFIT_NONFIN != 0 && data.BENEFIT_NONFIN != null)
                        {
                            NUMBER1 += Convert.ToInt32(data.BENEFIT_NONFIN);
                        }
                    }
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.00"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.00"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.00"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.00"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);


                    depname.SetValue(Niit, "Нийт дүн");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
                }
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public BM3ListResponse BM3List(BM3ListRequest request)
        {
            BM3ListResponse response = new BM3ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.BM3(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<BM3List> Body = new List<BM3List>();
                List<BM3List> AllData = new List<BM3List>();
                List<BM3List> list = new List<BM3List>();
                BM3List Niit = new BM3List();
                var typ = typeof(BM3List);
                if (res != null && res.Elements("BM3") != null)
                    Body = (from item in res.Elements("BM3") select new BM3List().SetXml(item)).ToList();
                AllData = (from item in res.Elements("RowCount") select new BM3List().SetXml(item)).ToList();
                response.recordsTotal = AllData.Count();

                if (Body.Count > 0)
                {
                    var depname = typ.GetProperty("DEPARTMENT_NAME");

                    var pay = typ.GetProperty("REFERENCE_AMOUNT");
                    var pay1 = typ.GetProperty("COMPLETION_DONE_AMOUNT");
                    var pay2 = typ.GetProperty("COMPLETION_PROGRESS_AMOUNT");
                    var pay3 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var pay4 = typ.GetProperty("C2_EXPIRED_AMOUNT");
                    var pay5 = typ.GetProperty("BENEFIT_FIN_AMOUNT");

                    var count = typ.GetProperty("BENEFIT_FIN");
                    var count1 = typ.GetProperty("BENEFIT_NONFIN");
                    var count2 = typ.GetProperty("C2_EXPIRED");
                    var count3 = typ.GetProperty("C2_NONEXPIRED");
                    var count4 = typ.GetProperty("COMPLETION_PROGRESS");
                    var count5 = typ.GetProperty("COMPLETION_DONE");
                    var count6 = typ.GetProperty("REFERENCE_COUNT");

                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;
                    Decimal AMOUNT5 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;
                    int NUMBER5 = 0;
                    int NUMBER6 = 0;

                    foreach (BM3List data in AllData)
                    {
                        if (data.REFERENCE_AMOUNT != "0" && data.REFERENCE_AMOUNT != null)
                        {
                            string strNii = data.REFERENCE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT += Amount1;
                        }
                        if (data.COMPLETION_DONE_AMOUNT != "0" && data.COMPLETION_DONE_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_DONE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount1;
                        }
                        if (data.COMPLETION_PROGRESS_AMOUNT != "0" && data.COMPLETION_PROGRESS_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_PROGRESS_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount1;
                        }
                        if (data.C2_NONEXPIRED_AMOUNT != "0" && data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount1;
                        }
                        if (data.C2_EXPIRED_AMOUNT != "0" && data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount1;
                        }
                        if (data.BENEFIT_FIN_AMOUNT != "0" && data.BENEFIT_FIN_AMOUNT != null)
                        {
                            string strNii = data.BENEFIT_FIN_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT5 += Amount1;
                        }
                       


                        if (data.BENEFIT_FIN != 0 && data.BENEFIT_FIN != null)
                        {
                            NUMBER += Convert.ToInt32(data.BENEFIT_FIN);
                        }
                        if (data.BENEFIT_NONFIN != 0 && data.BENEFIT_NONFIN != null)
                        {
                            NUMBER1 += Convert.ToInt32(data.BENEFIT_NONFIN);
                        }
                        if (data.C2_EXPIRED != 0 && data.C2_EXPIRED != null)
                        {
                            NUMBER2 += Convert.ToInt32(data.C2_EXPIRED);
                        }
                        if (data.C2_NONEXPIRED != 0 && data.C2_NONEXPIRED != null)
                        {
                            NUMBER3 += Convert.ToInt32(data.C2_NONEXPIRED);
                        }
                        if (data.COMPLETION_PROGRESS != 0 && data.COMPLETION_PROGRESS != null)
                        {
                            NUMBER4 += Convert.ToInt32(data.COMPLETION_PROGRESS);
                        }
                        if (data.COMPLETION_DONE != 0 && data.COMPLETION_DONE != null)
                        {
                            NUMBER5 += Convert.ToInt32(data.COMPLETION_DONE);
                        }
                        if (data.REFERENCE_COUNT != 0 && data.REFERENCE_COUNT != null)
                        {
                            NUMBER6 += Convert.ToInt32(data.REFERENCE_COUNT);
                        }
                    }
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);
                    count6.SetValue(Niit, NUMBER6);


                    depname.SetValue(Niit, "Нийт дүн");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
                }
                
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public BM4ListResponse BM4List(BM4ListRequest request)
        {
            BM4ListResponse response = new BM4ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.BM4(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<BM4List> Body = new List<BM4List>();
                List<BM4List> AllData = new List<BM4List>();
                List<BM4List> list = new List<BM4List>();
                BM4List Niit = new BM4List();
                var typ = typeof(BM4List);
                if (res != null && res.Elements("BM4") != null)
                {
                    Body = (from item in res.Elements("BM4") select new BM4List().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new BM4List().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                    


                if (Body.Count > 0)
                {
                    var depname = typ.GetProperty("DEPARTMENT_NAME");

                    var pay = typ.GetProperty("PROPOSAL_AMOUNT");
                    var pay1 = typ.GetProperty("COMPLETION_DONE_AMOUNT");
                    var pay2 = typ.GetProperty("COMPLETION_PROGRESS_AMOUNT");

                    var count = typ.GetProperty("PROPOSAL_COUNT");
                    var count1 = typ.GetProperty("COMPLETION_DONE");
                    var count2 = typ.GetProperty("COMPLETION_PROGRESS");

                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;

                    foreach (BM4List data in AllData)
                    {
                        if (data.PROPOSAL_AMOUNT != "0" && data.PROPOSAL_AMOUNT != null)
                        {
                            string strNii = data.PROPOSAL_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT += Amount1;
                        }
                        if (data.COMPLETION_DONE_AMOUNT != "0" && data.COMPLETION_DONE_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_DONE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount1;
                        }
                        if (data.COMPLETION_PROGRESS_AMOUNT != "0" && data.COMPLETION_PROGRESS_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_PROGRESS_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount1;
                        }
                      
                       



                        if (data.PROPOSAL_COUNT != 0 && data.PROPOSAL_COUNT != null)
                        {
                            NUMBER += Convert.ToInt32(data.PROPOSAL_COUNT);
                        }
                        if (data.COMPLETION_DONE != 0 && data.COMPLETION_DONE != null)
                        {
                            NUMBER1 += Convert.ToInt32(data.COMPLETION_DONE);
                        }
                        if (data.COMPLETION_PROGRESS != 0 && data.COMPLETION_PROGRESS != null)
                        {
                            NUMBER2 += Convert.ToInt32(data.COMPLETION_PROGRESS);
                        }
                      
                    }
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);


                    depname.SetValue(Niit, "Нийт дүн");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
                }
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public BM5ListResponse BM5List(BM5ListRequest request)
        {
            BM5ListResponse response = new BM5ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.BM5(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<BM5List> Body = new List<BM5List>();
                List<BM5List> AllData = new List<BM5List>();
                List<BM5List> list = new List<BM5List>();
                BM5List Niit = new BM5List();
                var typ = typeof(BM5List);
                if (res != null && res.Elements("BM5") != null)
                {
                    Body = (from item in res.Elements("BM5") select new BM5List().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("BM5") select new BM5List().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                   

                if (Body.Count > 0)
                {
                    var depname = typ.GetProperty("DEPARTMENT_NAME");

                    var pay = typ.GetProperty("LAW_AMOUNT");
                    var pay1 = typ.GetProperty("LAW_C2_AMOUNT");
                    var pay2 = typ.GetProperty("COMPLETION_DONE_AMOUNT");
                    var pay3 = typ.GetProperty("COMPLETION_PROGRESS_AMOUNT");
                    var pay4 = typ.GetProperty("COMPLETION_INVALID_AMOUNT");

                    var count = typ.GetProperty("LAW_NUMBER");
                    var count1 = typ.GetProperty("LAW_C2_NUMBER");
                    var count2 = typ.GetProperty("COMPLETION_DONE");
                    var count3 = typ.GetProperty("COMPLETION_PROGRESS");
                    var count4 = typ.GetProperty("COMPLETION_INVALID");

                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;

                    foreach (BM5List data in AllData)
                    {
                        if (data.LAW_AMOUNT != "0" && data.LAW_AMOUNT != null)
                        {
                            string strNii = data.LAW_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT += Amount1;
                        }
                        if (data.LAW_C2_AMOUNT != "0" && data.LAW_C2_AMOUNT != null)
                        {
                            string strNii = data.LAW_C2_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount1;
                        }
                        if (data.COMPLETION_DONE_AMOUNT != "0" && data.COMPLETION_DONE_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_DONE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount1;
                        }
                        if (data.COMPLETION_PROGRESS_AMOUNT != "0" && data.COMPLETION_PROGRESS_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_PROGRESS_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount1;
                        }
                        if (data.COMPLETION_INVALID_AMOUNT != "0" && data.COMPLETION_INVALID_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_INVALID_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount1;
                        }
                     



                        if (data.LAW_NUMBER != 0 && data.LAW_NUMBER != null)
                        {
                            NUMBER += Convert.ToInt32(data.LAW_NUMBER);
                        }
                        if (data.LAW_C2_NUMBER != 0 && data.LAW_C2_NUMBER != null)
                        {
                            NUMBER1 += Convert.ToInt32(data.LAW_C2_NUMBER);
                        }
                        if (data.COMPLETION_DONE != 0 && data.COMPLETION_DONE != null)
                        {
                            NUMBER2 += Convert.ToInt32(data.COMPLETION_DONE);
                        }
                        if (data.COMPLETION_PROGRESS != 0 && data.COMPLETION_PROGRESS != null)
                        {
                            NUMBER3 += Convert.ToInt32(data.COMPLETION_PROGRESS);
                        }
                        if (data.COMPLETION_INVALID != 0 && data.COMPLETION_INVALID != null)
                        {
                            NUMBER4 += Convert.ToInt32(data.COMPLETION_INVALID);
                        }
                       
                       
                    }
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);


                    depname.SetValue(Niit, "Нийт дүн");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
                }
                
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public BM6ListResponse BM6List(BM6ListRequest request)
        {
            BM6ListResponse response = new BM6ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.BM6(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<BM6> Body = new List<BM6>();
                List<BM6> AllData = new List<BM6>();
                List<BM6> list = new List<BM6>();
                BM6 Niit = new BM6();
                var typ = typeof(BM6);
                if (res != null && res.Elements("BM6") != null)
                {
                    Body = (from item in res.Elements("BM6") select new BM6().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new BM6().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                   
                if (Body.Count > 0)
                {
                    var depname = typ.GetProperty("DEPARTMENT_NAME");

                    var pay = typ.GetProperty("VIOLATION_AMOUNT");
                    var pay1 = typ.GetProperty("ERROR_AMOUNT");
                    var pay2 = typ.GetProperty("ALL_AMOUNT");
                    var pay3 = typ.GetProperty("CORRECTED_ERROR_AMOUNT");
                    var pay4 = typ.GetProperty("OTHER_ERROR_AMOUNT");
                    var pay5 = typ.GetProperty("ACT_AMOUNT");
                    var pay6 = typ.GetProperty("CLAIM_AMOUNT");
                    var pay7 = typ.GetProperty("REFERENCE_AMOUNT");
                    var pay8 = typ.GetProperty("PROPOSAL_AMOUNT");
                    var pay9 = typ.GetProperty("LAW_AMOUNT");
                    var pay10 = typ.GetProperty("OTHER_AMOUNT");

                    var count = typ.GetProperty("VIOLATION_COUNT");
                    var count1 = typ.GetProperty("ERROR_COUNT");
                    var count2 = typ.GetProperty("ALL_COUNT");
                    var count3 = typ.GetProperty("CORRECTED_ERROR_COUNT");
                    var count4 = typ.GetProperty("OTHER_ERROR_COUNT");
                    var count5 = typ.GetProperty("ACT_COUNT");
                    var count6 = typ.GetProperty("CLAIM_COUNT");
                    var count7 = typ.GetProperty("REFERENCE_COUNT");
                    var count8 = typ.GetProperty("PROPOSAL_COUNT");
                    var count9 = typ.GetProperty("LAW_COUNT");
                    var count10 = typ.GetProperty("OTHER_COUNT");

                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;
                    Decimal AMOUNT5 = 0;
                    Decimal AMOUNT6 = 0;
                    Decimal AMOUNT7 = 0;
                    Decimal AMOUNT8 = 0;
                    Decimal AMOUNT9 = 0;
                    Decimal AMOUNT10 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;
                    int NUMBER5 = 0;
                    int NUMBER6 = 0;
                    int NUMBER7 = 0;
                    int NUMBER8 = 0;
                    int NUMBER9 = 0;
                    int NUMBER10 = 0;

                    foreach (BM6 data in AllData)
                    {
                        if (data.VIOLATION_AMOUNT != "0" && data.VIOLATION_AMOUNT != null)
                        {
                            string strNii = data.VIOLATION_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT += Amount1;
                        }
                        if (data.ERROR_AMOUNT != "0" && data.ERROR_AMOUNT != null)
                        {
                            string strNii = data.ERROR_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount1;
                        }
                        if (data.ALL_AMOUNT != "0" && data.ALL_AMOUNT != null)
                        {
                            string strNii = data.ALL_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount1;
                        }
                        if (data.CORRECTED_ERROR_AMOUNT != "0" && data.CORRECTED_ERROR_AMOUNT != null)
                        {
                            string strNii = data.CORRECTED_ERROR_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount1;
                        }
                        if (data.OTHER_ERROR_AMOUNT != "0" && data.OTHER_ERROR_AMOUNT != null)
                        {
                            string strNii = data.OTHER_ERROR_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount1;
                        }

                        if (data.ACT_AMOUNT != "0" && data.ACT_AMOUNT != null)
                        {
                            string strNii = data.ACT_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT5 += Amount1;
                        }
                        if (data.CLAIM_AMOUNT != "0" && data.CLAIM_AMOUNT != null)
                        {
                            string strNii = data.CLAIM_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT6 += Amount1;
                        }
                        if (data.REFERENCE_AMOUNT != "0" && data.REFERENCE_AMOUNT != null)
                        {
                            string strNii = data.REFERENCE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT7 += Amount1;
                        }
                        if (data.PROPOSAL_AMOUNT != "0" && data.PROPOSAL_AMOUNT != null)
                        {
                            string strNii = data.PROPOSAL_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT8 += Amount1;
                        }
                        if (data.LAW_AMOUNT != "0" && data.LAW_AMOUNT != null)
                        {
                            string strNii = data.LAW_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT9 += Amount1;
                        }
                        if (data.OTHER_AMOUNT != "0" && data.OTHER_AMOUNT != null)
                        {
                            string strNii = data.OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT10 += Amount1;
                        }



                        if (data.VIOLATION_COUNT != 0 && data.VIOLATION_COUNT != null)
                        {
                            NUMBER += Convert.ToInt32(data.VIOLATION_COUNT);
                        }
                        if (data.ERROR_COUNT != 0 && data.ERROR_COUNT != null)
                        {
                            NUMBER1 += Convert.ToInt32(data.ERROR_COUNT);
                        }
                        if (data.ALL_COUNT != 0 && data.ALL_COUNT != null)
                        {
                            NUMBER2 += Convert.ToInt32(data.ALL_COUNT);
                        }
                        if (data.CORRECTED_ERROR_COUNT != 0 && data.CORRECTED_ERROR_COUNT != null)
                        {
                            NUMBER3 += Convert.ToInt32(data.CORRECTED_ERROR_COUNT);
                        }
                        if (data.OTHER_ERROR_COUNT != 0 && data.OTHER_ERROR_COUNT != null)
                        {
                            NUMBER4 += Convert.ToInt32(data.OTHER_ERROR_COUNT);
                        }
                        if (data.ACT_COUNT != 0 && data.ACT_COUNT != null)
                        {
                            NUMBER5 += Convert.ToInt32(data.ACT_COUNT);
                        }
                        if (data.CLAIM_COUNT != 0 && data.CLAIM_COUNT != null)
                        {
                            NUMBER6 += Convert.ToInt32(data.CLAIM_COUNT);
                        }
                        if (data.REFERENCE_COUNT != 0 && data.REFERENCE_COUNT != null)
                        {
                            NUMBER7 += Convert.ToInt32(data.REFERENCE_COUNT);
                        }
                        if (data.PROPOSAL_COUNT != 0 && data.PROPOSAL_COUNT != null)
                        {
                            NUMBER8 += Convert.ToInt32(data.PROPOSAL_COUNT);
                        }
                        if (data.LAW_COUNT != 0 && data.LAW_COUNT != null)
                        {
                            NUMBER9 += Convert.ToInt32(data.LAW_COUNT);
                        }
                        if (data.OTHER_COUNT != 0 && data.OTHER_COUNT != null)
                        {
                            NUMBER10 += Convert.ToInt32(data.OTHER_COUNT);
                        }

                    }
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.00"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.00"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.00"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.00"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);
                    count6.SetValue(Niit, NUMBER6);
                    count7.SetValue(Niit, NUMBER7);
                    count8.SetValue(Niit, NUMBER8);
                    count9.SetValue(Niit, NUMBER9);
                    count10.SetValue(Niit, NUMBER10);


                    depname.SetValue(Niit, "Нийт дүн");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
                }
                
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public BM7ListResponse BM7List(BM7ListRequest request)
        {
            BM7ListResponse response = new BM7ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.BM7(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<BM7> Body = new List<BM7>();
                List<BM7> AllData = new List<BM7>();
                List<BM7> list = new List<BM7>();
                BM7 Niit = new BM7();
                var typ = typeof(BM7);
                if (res != null && res.Elements("BM7") != null)
                {
                    Body = (from item in res.Elements("BM7") select new BM7().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new BM7().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }

                if (Body.Count > 0)
                {
                    var depname = typ.GetProperty("DEPARTMENT_NAME");

                    var pay = typ.GetProperty("INCOME_STATE_AMOUNT");
                    var pay1 = typ.GetProperty("BUDGET_STATE_AMOUNT");
                    var pay2 = typ.GetProperty("BUDGET_LOCAL_AMOUNT");
                    var pay3 = typ.GetProperty("ACCOUNTANT_AMOUNT");
                    var pay4 = typ.GetProperty("EFFICIENCY_AMOUNT");
                    var pay5 = typ.GetProperty("LAW_AMOUNT");
                    var pay6 = typ.GetProperty("MONITORING_AMOUNT");
                    var pay7 = typ.GetProperty("PURCHASE_AMOUNT");
                    var pay8 = typ.GetProperty("COST_AMOUNT");
                    var pay9 = typ.GetProperty("OTHER_AMOUNT");
                    var pay10 = typ.GetProperty("ALL_AMOUNT");

                    var count = typ.GetProperty("INCOME_STATE_COUNT");
                    var count1 = typ.GetProperty("INCOME_LOCAL_COUNT");
                    var count2 = typ.GetProperty("INCOME_LOCAL_NUMBER");
                    var count3 = typ.GetProperty("BUDGET_STATE_COUNT");
                    var count4 = typ.GetProperty("BUDGET_LOCAL_COUNT");
                    var count5 = typ.GetProperty("ACCOUNTANT_COUNT");
                    var count6 = typ.GetProperty("EFFICIENCY_COUNT");
                    var count7 = typ.GetProperty("LAW_COUNT");
                    var count8 = typ.GetProperty("MONITORING_COUNT");
                    var count9 = typ.GetProperty("PURCHASE_COUNT");
                    var count10 = typ.GetProperty("COST_COUNT");
                    var count11 = typ.GetProperty("OTHER_COUNT");
                    var count12 = typ.GetProperty("ALL_COUNT");

                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;
                    Decimal AMOUNT5 = 0;
                    Decimal AMOUNT6 = 0;
                    Decimal AMOUNT7 = 0;
                    Decimal AMOUNT8 = 0;
                    Decimal AMOUNT9 = 0;
                    Decimal AMOUNT10 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;
                    int NUMBER5 = 0;
                    int NUMBER6 = 0;
                    int NUMBER7 = 0;
                    int NUMBER8 = 0;
                    int NUMBER9 = 0;
                    int NUMBER10 = 0;
                    int NUMBER11 = 0;
                    int NUMBER12 = 0;

                    foreach (BM7 data in AllData)
                    {
                        if (data.INCOME_STATE_AMOUNT != "0" && data.INCOME_STATE_AMOUNT != null)
                        {
                            string strNii = data.INCOME_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT += Amount1;
                        }
                        if (data.BUDGET_STATE_AMOUNT != "0" && data.BUDGET_STATE_AMOUNT != null)
                        {
                            string strNii = data.BUDGET_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount1;
                        }
                        if (data.BUDGET_LOCAL_AMOUNT != "0" && data.BUDGET_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.BUDGET_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount1;
                        }
                        if (data.ACCOUNTANT_AMOUNT != "0" && data.ACCOUNTANT_AMOUNT != null)
                        {
                            string strNii = data.ACCOUNTANT_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount1;
                        }
                        if (data.EFFICIENCY_AMOUNT != "0" && data.EFFICIENCY_AMOUNT != null)
                        {
                            string strNii = data.EFFICIENCY_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount1;
                        }

                        if (data.LAW_AMOUNT != "0" && data.LAW_AMOUNT != null)
                        {
                            string strNii = data.LAW_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT5 += Amount1;
                        }
                        if (data.MONITORING_AMOUNT != "0" && data.MONITORING_AMOUNT != null)
                        {
                            string strNii = data.MONITORING_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT6 += Amount1;
                        }
                        if (data.PURCHASE_AMOUNT != "0" && data.PURCHASE_AMOUNT != null)
                        {
                            string strNii = data.PURCHASE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT7 += Amount1;
                        }
                        if (data.COST_AMOUNT != "0" && data.COST_AMOUNT != null)
                        {
                            string strNii = data.COST_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT8 += Amount1;
                        }
                        if (data.OTHER_AMOUNT != "0" && data.OTHER_AMOUNT != null)
                        {
                            string strNii = data.OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT9 += Amount1;
                        }
                        if (data.ALL_AMOUNT != "0" && data.ALL_AMOUNT != null)
                        {
                            string strNii = data.ALL_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT10 += Amount1;
                        }



                        if (data.INCOME_STATE_COUNT != 0 && data.INCOME_STATE_COUNT != null)
                        {
                            NUMBER += Convert.ToInt32(data.INCOME_STATE_COUNT);
                        }
                        if (data.INCOME_LOCAL_COUNT != 0 && data.INCOME_LOCAL_COUNT != null)
                        {
                            NUMBER1 += Convert.ToInt32(data.INCOME_LOCAL_COUNT);
                        }
                        if (data.INCOME_LOCAL_NUMBER != 0 && data.INCOME_LOCAL_NUMBER != null)
                        {
                            NUMBER2 += Convert.ToInt32(data.INCOME_LOCAL_NUMBER);
                        }
                        if (data.BUDGET_STATE_COUNT != 0 && data.BUDGET_STATE_COUNT != null)
                        {
                            NUMBER3 += Convert.ToInt32(data.BUDGET_STATE_COUNT);
                        }
                        if (data.BUDGET_LOCAL_COUNT != 0 && data.BUDGET_LOCAL_COUNT != null)
                        {
                            NUMBER4 += Convert.ToInt32(data.BUDGET_LOCAL_COUNT);
                        }
                        if (data.ACCOUNTANT_COUNT != 0 && data.ACCOUNTANT_COUNT != null)
                        {
                            NUMBER5 += Convert.ToInt32(data.ACCOUNTANT_COUNT);
                        }
                        if (data.EFFICIENCY_COUNT != 0 && data.EFFICIENCY_COUNT != null)
                        {
                            NUMBER6 += Convert.ToInt32(data.EFFICIENCY_COUNT);
                        }
                        if (data.LAW_COUNT != 0 && data.LAW_COUNT != null)
                        {
                            NUMBER7 += Convert.ToInt32(data.LAW_COUNT);
                        }
                        if (data.MONITORING_COUNT != 0 && data.MONITORING_COUNT != null)
                        {
                            NUMBER8 += Convert.ToInt32(data.MONITORING_COUNT);
                        }
                        if (data.PURCHASE_COUNT != 0 && data.PURCHASE_COUNT != null)
                        {
                            NUMBER9 += Convert.ToInt32(data.PURCHASE_COUNT);
                        }
                        if (data.COST_COUNT != 0 && data.COST_COUNT != null)
                        {
                            NUMBER10 += Convert.ToInt32(data.COST_COUNT);
                        }
                        if (data.OTHER_COUNT != 0 && data.OTHER_COUNT != null)
                        {
                            NUMBER11 += Convert.ToInt32(data.OTHER_COUNT);
                        }
                        if (data.ALL_COUNT != 0 && data.ALL_COUNT != null)
                        {
                            NUMBER12 += Convert.ToInt32(data.ALL_COUNT);
                        }
                    }
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.00"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.00"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.00"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.00"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);
                    count6.SetValue(Niit, NUMBER6);
                    count7.SetValue(Niit, NUMBER7);
                    count8.SetValue(Niit, NUMBER8);
                    count9.SetValue(Niit, NUMBER9);
                    count10.SetValue(Niit, NUMBER10);
                    count11.SetValue(Niit, NUMBER11);
                    count12.SetValue(Niit, NUMBER12);


                    depname.SetValue(Niit, "Нийт дүн");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
                }
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public BM8ListResponse BM8List(BM8ListRequest request)
        {
            BM8ListResponse response = new BM8ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID !=null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.BM8(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<BM8List> Body = new List<BM8List>();
                List<BM8List> AllData = new List<BM8List>();
                List<BM8List> list = new List<BM8List>();
                BM8List Niit = new BM8List();
                var typ = typeof(BM8List);
                if (res != null && res.Elements("BM8") != null)
                {
                    Body = (from item in res.Elements("BM8") select new BM8List().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new BM8List().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                    
                if (Body.Count > 0)
                {
                    var depname = typ.GetProperty("DEPARTMENT_NAME");

                    var pay = typ.GetProperty("CORRECTED_AMOUNT");

                    var count = typ.GetProperty("CORRECTED_COUNT");

                    Decimal AMOUNT = 0;

                    int NUMBER = 0;

                    foreach (BM8List data in AllData)
                    {
                        if (data.CORRECTED_AMOUNT != "0" && data.CORRECTED_AMOUNT != null)
                        {
                            string strNii = data.CORRECTED_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii);
                            AMOUNT += Amount1;
                        }
                     

                        if (data.CORRECTED_COUNT != 0 && data.CORRECTED_COUNT != null)
                        {
                            NUMBER += Convert.ToInt32(data.CORRECTED_COUNT);
                        }
                 

                    }
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);


                    depname.SetValue(Niit, "Нийт дүн");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
                }
                
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public NM1ListResponse NM1List(NM1ListRequest request)
        {
            NM1ListResponse response = new NM1ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.NM1(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<NM1> body = new List<NM1>();
                List<NM1> AllData = new List<NM1>();

                if (res != null && res.Elements("NM1") != null)
                {
                    body = (from item in res.Elements("NM1") select new NM1().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new NM1().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                    
                if (body.Count > 0)
                {
                    List<NM1> total = new List<NM1>();
                    NM1 Niit = new NM1();

                    var typ = typeof(NM1);
                    var depname = typ.GetProperty("TOPIC_CODE");
                    var pay = typ.GetProperty("ACT_AMOUNT");
                    var pay1 = typ.GetProperty("COMPLETION_AMOUNT");
                    var pay2 = typ.GetProperty("COMPLETION_STATE_AMOUNT");
                    var pay3 = typ.GetProperty("COMPLETION_LOCAL_AMOUNT");
                    var pay4 = typ.GetProperty("COMPLETION_ORG_AMOUNT");
                    var pay5 = typ.GetProperty("COMPLETION_OTHER_AMOUNT");
                    var pay6 = typ.GetProperty("REMOVED_AMOUNT");
                    var pay7 = typ.GetProperty("REMOVED_LAW_AMOUNT");
                    var pay8 = typ.GetProperty("REMOVED_INVALID_AMOUNT");
                    var pay9 = typ.GetProperty("ACT_C2_AMOUNT");
                    var pay10 = typ.GetProperty("ACT_NONEXPIRED_AMOUNT");
                    var pay11 = typ.GetProperty("ACT_EXPIRED_AMOUNT");
                    var pay12 = typ.GetProperty("BENEFIT_FIN_AMOUNT");

                    var count = typ.GetProperty("ACT_COUNT");
                    var count1 = typ.GetProperty("COMPLETION_COUNT");
                    var count2 = typ.GetProperty("COMPLETION_STATE_COUNT");
                    var count3 = typ.GetProperty("COMPLETION_LOCAL_COUNT");
                    var count4 = typ.GetProperty("COMPLETION_ORG_COUNT");
                    var count5 = typ.GetProperty("COMPLETION_OTHER_COUNT");
                    var count6 = typ.GetProperty("REMOVED_COUNT");
                    var count7 = typ.GetProperty("REMOVED_LAW_COUNT");
                    var count8 = typ.GetProperty("REMOVED_INVALID_COUNT");
                    var count9 = typ.GetProperty("ACT_C2_COUNT");
                    var count10 = typ.GetProperty("ACT_NONEXPIRED_COUNT");
                    var count11 = typ.GetProperty("ACT_EXPIRED_COUNT");
                    var count12 = typ.GetProperty("BENEFIT_FIN");
                    var count13 = typ.GetProperty("BENEFIT_NONFIN");


                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;
                    Decimal AMOUNT5 = 0;
                    Decimal AMOUNT6 = 0;
                    Decimal AMOUNT7 = 0;
                    Decimal AMOUNT8 = 0;
                    Decimal AMOUNT9 = 0;
                    Decimal AMOUNT10 = 0;
                    Decimal AMOUNT11 = 0;
                    Decimal AMOUNT12 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;
                    int NUMBER5 = 0;
                    int NUMBER6 = 0;
                    int NUMBER7 = 0;
                    int NUMBER8 = 0;
                    int NUMBER9 = 0;
                    int NUMBER10 = 0;
                    int NUMBER11 = 0;
                    int NUMBER12 = 0;
                    int NUMBER13 = 0;

                    foreach (NM1 data in AllData)
                    {
                        if (data.ACT_AMOUNT != null)
                        {
                            string strNii = data.ACT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT += Amount;

                        }
                        if (data.COMPLETION_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount;

                        }
                        if (data.COMPLETION_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount;

                        }
                        if (data.COMPLETION_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount;

                        }
                        if (data.COMPLETION_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount;

                        }
                        if (data.COMPLETION_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT5 += Amount;

                        }
                        if (data.REMOVED_AMOUNT != null)
                        {
                            string strNii = data.REMOVED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT6 += Amount;

                        }
                        if (data.REMOVED_LAW_AMOUNT != null)
                        {
                            string strNii = data.REMOVED_LAW_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT7 += Amount;

                        }
                        if (data.REMOVED_INVALID_AMOUNT != null)
                        {
                            string strNii = data.REMOVED_INVALID_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT8 += Amount;

                        }
                        if (data.ACT_C2_AMOUNT != null)
                        {
                            string strNii = data.ACT_C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT9 += Amount;

                        }
                        if (data.ACT_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.ACT_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT10 += Amount;

                        }
                        if (data.ACT_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.ACT_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT11 += Amount;

                        }
                        if (data.BENEFIT_FIN_AMOUNT != null)
                        {
                            string strNii = data.BENEFIT_FIN_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT12 += Amount;

                        }

                        if (data.ACT_COUNT != 0)
                        {
                            NUMBER += Convert.ToInt32(data.ACT_COUNT);
                        }
                        if (data.COMPLETION_COUNT != 0)
                        {
                            NUMBER1 += Convert.ToInt32(data.COMPLETION_COUNT);
                        }
                        if (data.COMPLETION_STATE_COUNT != 0)
                        {
                            NUMBER2 += Convert.ToInt32(data.COMPLETION_STATE_COUNT);
                        }
                        if (data.COMPLETION_LOCAL_COUNT != 0)
                        {
                            NUMBER3 += Convert.ToInt32(data.COMPLETION_LOCAL_COUNT);
                        }
                        if (data.COMPLETION_ORG_COUNT != 0)
                        {
                            NUMBER4 += Convert.ToInt32(data.COMPLETION_ORG_COUNT);
                        }
                        if (data.COMPLETION_OTHER_COUNT != 0)
                        {
                            NUMBER5 += Convert.ToInt32(data.COMPLETION_OTHER_COUNT);
                        }
                        if (data.REMOVED_COUNT != 0)
                        {
                            NUMBER6 += Convert.ToInt32(data.REMOVED_COUNT);
                        }
                        if (data.REMOVED_LAW_COUNT != 0)
                        {
                            NUMBER7 += Convert.ToInt32(data.REMOVED_LAW_COUNT);
                        }
                        if (data.REMOVED_INVALID_COUNT != 0)
                        {
                            NUMBER8 += Convert.ToInt32(data.REMOVED_INVALID_COUNT);
                        }
                        if (data.ACT_C2_COUNT != 0)
                        {
                            NUMBER9 += Convert.ToInt32(data.ACT_C2_COUNT);
                        }
                        if (data.ACT_NONEXPIRED_COUNT != 0)
                        {
                            NUMBER10 += Convert.ToInt32(data.ACT_NONEXPIRED_COUNT);
                        }
                        if (data.ACT_EXPIRED_COUNT != 0)
                        {
                            NUMBER11 += Convert.ToInt32(data.ACT_EXPIRED_COUNT);
                        }
                        if (data.BENEFIT_FIN != 0)
                        {
                            NUMBER12 += Convert.ToInt32(data.BENEFIT_FIN);
                        }
                        if (data.BENEFIT_NONFIN != 0)
                        {
                            NUMBER13 += Convert.ToInt32(data.BENEFIT_NONFIN);
                        }
                    }


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.00"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.00"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.00"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.00"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.00"));
                    pay11.SetValue(Niit, AMOUNT11.ToString("#,0.00"));
                    pay12.SetValue(Niit, AMOUNT12.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);
                    count6.SetValue(Niit, NUMBER6);
                    count7.SetValue(Niit, NUMBER7);
                    count8.SetValue(Niit, NUMBER8);
                    count9.SetValue(Niit, NUMBER9);
                    count10.SetValue(Niit, NUMBER10);
                    count11.SetValue(Niit, NUMBER11);
                    count12.SetValue(Niit, NUMBER12);
                    count13.SetValue(Niit, NUMBER13);

                    depname.SetValue(Niit, "Нийт дүн");


                    total = body;
                    total.Add(Niit);

                    response.data = total;
                }
                else
                {
                    response.data = body;
                }
                
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public NM2ListResponse NM2List(BM2ListRequest request)
        {
            NM2ListResponse response = new NM2ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.NM2(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());

                List<NM2> body = new List<NM2>();
                List<NM2> AllData = new List<NM2>();
                List<NM2> total = new List<NM2>();
                NM2 Niit = new NM2();
                if (res != null && res.Elements("NM2") != null)
                {
                    body = (from item in res.Elements("NM2") select new NM2().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new NM2().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                    
                if (body.Count > 0)
                {
                    var typ = typeof(NM2);
                    var depname = typ.GetProperty("TOPIC_CODE");
                    var pay = typ.GetProperty("CLAIM_VIOLATION_AMOUNT");
                    var pay1 = typ.GetProperty("COMPLETION_AMOUNT");
                    var pay2 = typ.GetProperty("COMPLETION_STATE_AMOUNT");
                    var pay3 = typ.GetProperty("COMPLETION_LOCAL_AMOUNT");
                    var pay4 = typ.GetProperty("COMPLETION_ORG_AMOUNT");
                    var pay5 = typ.GetProperty("COMPLETION_OTHER_AMOUNT");
                    var pay6 = typ.GetProperty("REMOVED_AMOUNT");
                    var pay7 = typ.GetProperty("REMOVED_LAW_AMOUNT");
                    var pay8 = typ.GetProperty("REMOVED_INVALID_AMOUNT");
                    var pay9 = typ.GetProperty("CLAIM_C2_AMOUNT");
                    var pay10 = typ.GetProperty("CLAIM_C2_NONEXPIRED_AMOUNT");
                    var pay11 = typ.GetProperty("CLAIM_C2_EXPIRED_AMOUNT");
                    var pay12 = typ.GetProperty("BENEFIT_FIN_AMOUNT");

                    var count = typ.GetProperty("CLAIM_VIOLATION_COUNT");
                    var count1 = typ.GetProperty("COMPLETION_COUNT");
                    var count2 = typ.GetProperty("COMPLETION_STATE_COUNT");
                    var count3 = typ.GetProperty("COMPLETION_LOCAL_COUNT");
                    var count4 = typ.GetProperty("COMPLETION_ORG_COUNT");
                    var count5 = typ.GetProperty("COMPLETION_OTHER_COUNT");
                    var count6 = typ.GetProperty("REMOVED_COUNT");
                    var count7 = typ.GetProperty("REMOVED_LAW_COUNT");
                    var count8 = typ.GetProperty("REMOVED_INVALID_COUNT");
                    var count9 = typ.GetProperty("CLAIM_C2_COUNT");
                    var count10 = typ.GetProperty("CLAIM_C2_NONEXPIRED_COUNT");
                    var count11 = typ.GetProperty("CLAIM_C2_EXPIRED_COUNT");
                    var count12 = typ.GetProperty("BENEFIT_FIN");
                    var count13 = typ.GetProperty("BENEFIT_NONFIN");


                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;
                    Decimal AMOUNT5 = 0;
                    Decimal AMOUNT6 = 0;
                    Decimal AMOUNT7 = 0;
                    Decimal AMOUNT8 = 0;
                    Decimal AMOUNT9 = 0;
                    Decimal AMOUNT10 = 0;
                    Decimal AMOUNT11 = 0;
                    Decimal AMOUNT12 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;
                    int NUMBER5 = 0;
                    int NUMBER6 = 0;
                    int NUMBER7 = 0;
                    int NUMBER8 = 0;
                    int NUMBER9 = 0;
                    int NUMBER10 = 0;
                    int NUMBER11 = 0;
                    int NUMBER12 = 0;
                    int NUMBER13 = 0;

                    foreach (NM2 data in AllData)
                    {
                        if (data.CLAIM_VIOLATION_AMOUNT != null)
                        {
                            string strNii = data.CLAIM_VIOLATION_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT += Amount;

                        }
                        if (data.COMPLETION_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount;

                        }
                        if (data.COMPLETION_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount;

                        }
                        if (data.COMPLETION_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount;

                        }
                        if (data.COMPLETION_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount;

                        }
                        if (data.COMPLETION_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT5 += Amount;

                        }
                        if (data.REMOVED_AMOUNT != null)
                        {
                            string strNii = data.REMOVED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT6 += Amount;

                        }
                        if (data.REMOVED_LAW_AMOUNT != null)
                        {
                            string strNii = data.REMOVED_LAW_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT7 += Amount;

                        }
                        if (data.REMOVED_INVALID_AMOUNT != null)
                        {
                            string strNii = data.REMOVED_INVALID_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT8 += Amount;

                        }
                        if (data.CLAIM_C2_AMOUNT != null)
                        {
                            string strNii = data.CLAIM_C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT9 += Amount;

                        }
                        if (data.CLAIM_C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.CLAIM_C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT10 += Amount;

                        }
                        if (data.CLAIM_C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.CLAIM_C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT11 += Amount;

                        }
                        if (data.BENEFIT_FIN_AMOUNT != null)
                        {
                            string strNii = data.BENEFIT_FIN_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT12 += Amount;

                        }

                        if (data.CLAIM_VIOLATION_COUNT != 0)
                        {
                            NUMBER += Convert.ToInt32(data.CLAIM_VIOLATION_COUNT);
                        }
                        if (data.COMPLETION_COUNT != 0)
                        {
                            NUMBER1 += Convert.ToInt32(data.COMPLETION_COUNT);
                        }
                        if (data.COMPLETION_STATE_COUNT != 0)
                        {
                            NUMBER2 += Convert.ToInt32(data.COMPLETION_STATE_COUNT);
                        }
                        if (data.COMPLETION_LOCAL_COUNT != 0)
                        {
                            NUMBER3 += Convert.ToInt32(data.COMPLETION_LOCAL_COUNT);
                        }
                        if (data.COMPLETION_ORG_COUNT != 0)
                        {
                            NUMBER4 += Convert.ToInt32(data.COMPLETION_ORG_COUNT);
                        }
                        if (data.COMPLETION_OTHER_COUNT != 0)
                        {
                            NUMBER5 += Convert.ToInt32(data.COMPLETION_OTHER_COUNT);
                        }
                        if (data.REMOVED_COUNT != 0)
                        {
                            NUMBER6 += Convert.ToInt32(data.REMOVED_COUNT);
                        }
                        if (data.REMOVED_LAW_COUNT != 0)
                        {
                            NUMBER7 += Convert.ToInt32(data.REMOVED_LAW_COUNT);
                        }
                        if (data.REMOVED_INVALID_COUNT != 0)
                        {
                            NUMBER8 += Convert.ToInt32(data.REMOVED_INVALID_COUNT);
                        }
                        if (data.CLAIM_C2_COUNT != 0)
                        {
                            NUMBER9 += Convert.ToInt32(data.CLAIM_C2_COUNT);
                        }
                        if (data.CLAIM_C2_NONEXPIRED_COUNT != 0)
                        {
                            NUMBER10 += Convert.ToInt32(data.CLAIM_C2_NONEXPIRED_COUNT);
                        }
                        if (data.CLAIM_C2_EXPIRED_COUNT != 0)
                        {
                            NUMBER11 += Convert.ToInt32(data.CLAIM_C2_EXPIRED_COUNT);
                        }
                        if (data.BENEFIT_FIN != 0)
                        {
                            NUMBER12 += Convert.ToInt32(data.BENEFIT_FIN);
                        }
                        if (data.BENEFIT_NONFIN != 0)
                        {
                            NUMBER13 += Convert.ToInt32(data.BENEFIT_NONFIN);
                        }
                    }


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.00"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.00"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.00"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.00"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.00"));
                    pay11.SetValue(Niit, AMOUNT11.ToString("#,0.00"));
                    pay12.SetValue(Niit, AMOUNT12.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);
                    count6.SetValue(Niit, NUMBER6);
                    count7.SetValue(Niit, NUMBER7);
                    count8.SetValue(Niit, NUMBER8);
                    count9.SetValue(Niit, NUMBER9);
                    count10.SetValue(Niit, NUMBER10);
                    count11.SetValue(Niit, NUMBER11);
                    count12.SetValue(Niit, NUMBER12);
                    count13.SetValue(Niit, NUMBER13);

                    depname.SetValue(Niit, "Нийт дүн");


                    total = body;
                    total.Add(Niit);

                    response.data = total;
                }
                else
                {
                    response.data = body;
                }
                
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public NM3ListResponse NM3List(BM3ListRequest request)
        {
            NM3ListResponse response = new NM3ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.NM3(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<NM3> body = new List<NM3>();
                List<NM3> AllData = new List<NM3>();
                List<NM3> total = new List<NM3>();
                NM3 Niit = new NM3();
                var typ = typeof(NM3);
                if (res != null && res.Elements("NM3") != null)
                {
                    body = (from item in res.Elements("NM3") select new NM3().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new NM3().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                    
                if (body.Count > 0)
                {
                    int COUNTRE = 0;
                    Decimal AMOUNTRE = 0;
                    foreach (NM3 nm3 in AllData)
                    {
                        if (nm3.REFERENCE_COUNT != null && nm3.COMPLETION_DONE_COUNT != null)
                        {
                            COUNTRE = Convert.ToInt32(nm3.REFERENCE_COUNT) - Convert.ToInt32(nm3.COMPLETION_DONE_COUNT);
                            nm3.C2_COUNT = COUNTRE;
                        }
                    }
                    foreach (NM3 nm3 in AllData)
                    {
                        if (nm3.REFERENCE_AMOUNT != null && nm3.COMPLETION_DONE_AMOUNT != null)
                        {
                            string strNii1 = nm3.REFERENCE_AMOUNT.Replace(",", "");
                            string strNii2 = nm3.COMPLETION_DONE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii1);
                            Decimal Amount2 = Convert.ToDecimal(strNii2);
                            AMOUNTRE = Amount1 - Amount2;

                            nm3.C2_AMOUNT = AMOUNTRE.ToString("#,0.00");
                        }
                    }


                    var depname = typ.GetProperty("TOPIC_CODE");

                    var pay = typ.GetProperty("REFERENCE_AMOUNT");
                    var pay1 = typ.GetProperty("COMPLETION_DONE_AMOUNT");
                    var pay2 = typ.GetProperty("COMPLETION_PROGRESS_AMOUNT");
                    var pay3 = typ.GetProperty("C2_AMOUNT");
                    var pay4 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var pay5 = typ.GetProperty("C2_EXPIRED_AMOUNT");
                    var pay6 = typ.GetProperty("BENEFIT_FIN_AMOUNT");


                    var count = typ.GetProperty("REFERENCE_COUNT");
                    var count1 = typ.GetProperty("COMPLETION_DONE_COUNT");
                    var count2 = typ.GetProperty("COMPLETION_PROGRESS_COUNT");
                    var count3 = typ.GetProperty("C2_COUNT");
                    var count4 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var count5 = typ.GetProperty("C2_EXPIRED_COUNT");
                    var count6 = typ.GetProperty("BENEFIT_FIN_COUNT");
                    var count7 = typ.GetProperty("BENEFIT_NONFIN_COUNT");
                    var count8 = typ.GetProperty("WORKING_PERSON");
                    var count9 = typ.GetProperty("WORKING_DAY");
                    var count10 = typ.GetProperty("WORKING_ADDITION_TIME");


                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;
                    Decimal AMOUNT5 = 0;
                    Decimal AMOUNT6 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;
                    int NUMBER5 = 0;
                    int NUMBER6 = 0;
                    int NUMBER7 = 0;
                    int NUMBER8 = 0;
                    int NUMBER9 = 0;
                    int NUMBER10 = 0;

                    foreach (NM3 data in AllData)
                    {
                        if (data.REFERENCE_AMOUNT != null)
                        {
                            string strNii = data.REFERENCE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT += Amount;

                        }
                        if (data.COMPLETION_DONE_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_DONE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount;

                        }
                        if (data.COMPLETION_PROGRESS_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_PROGRESS_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT5 += Amount;

                        }
                        if (data.BENEFIT_FIN_AMOUNT != null)
                        {
                            string strNii = data.BENEFIT_FIN_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT6 += Amount;

                        }


                        if (data.REFERENCE_COUNT != 0)
                        {
                            NUMBER += Convert.ToInt32(data.REFERENCE_COUNT);
                        }
                        if (data.COMPLETION_DONE_COUNT != 0)
                        {
                            NUMBER1 += Convert.ToInt32(data.COMPLETION_DONE_COUNT);
                        }
                        if (data.COMPLETION_PROGRESS_COUNT != 0)
                        {
                            NUMBER2 += Convert.ToInt32(data.COMPLETION_PROGRESS_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            NUMBER3 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            NUMBER4 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            NUMBER5 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        if (data.BENEFIT_FIN_COUNT != 0)
                        {
                            NUMBER6 += Convert.ToInt32(data.BENEFIT_FIN_COUNT);
                        }
                        if (data.BENEFIT_NONFIN_COUNT != 0)
                        {
                            NUMBER7 += Convert.ToInt32(data.BENEFIT_NONFIN_COUNT);
                        }
                        if (data.WORKING_PERSON != 0)
                        {
                            NUMBER8 += Convert.ToInt32(data.WORKING_PERSON);
                        }
                        if (data.WORKING_DAY != 0)
                        {
                            NUMBER9 += Convert.ToInt32(data.WORKING_DAY);
                        }
                        if (data.WORKING_ADDITION_TIME != 0)
                        {
                            NUMBER10 += Convert.ToInt32(data.WORKING_ADDITION_TIME);
                        }
                    }


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);
                    count6.SetValue(Niit, NUMBER6);
                    count7.SetValue(Niit, NUMBER7);
                    count8.SetValue(Niit, NUMBER8);
                    count9.SetValue(Niit, NUMBER9);
                    count10.SetValue(Niit, NUMBER10);

                    depname.SetValue(Niit, "Нийт дүн");


                    total = body;
                    total.Add(Niit);

                    response.data = total;
                }
                else
                {
                    response.data = body;
                }
                
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public NM4ListResponse NM4List(BM4ListRequest request)
        {
            NM4ListResponse response = new NM4ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.NM4(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<NM4> body = new List<NM4>();
                List<NM4> AllData = new List<NM4>();
                List<NM4> total = new List<NM4>();
                NM4 Niit = new NM4();
                var typ = typeof(NM4);
                if (res != null && res.Elements("NM4") != null)
                {
                    body = (from item in res.Elements("NM4") select new NM4().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new NM4().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                    
                if (body.Count > 0)
                {
                    var depname = typ.GetProperty("TOPIC_CODE");

                    var pay = typ.GetProperty("PROPOSAL_AMOUNT");
                    var pay1 = typ.GetProperty("COMPLETION_DONE_AMOUNT");
                    var pay2 = typ.GetProperty("COMPLETION_PROGRESS_AMOUNT");

                    var count = typ.GetProperty("PROPOSAL_COUNT");
                    var count1 = typ.GetProperty("COMPLETION_DONE_COUNT");
                    var count2 = typ.GetProperty("COMPLETION_PROGRESS_COUNT");


                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;

                    foreach (NM4 data in AllData)
                    {
                        if (data.PROPOSAL_AMOUNT != null)
                        {
                            string strNii = data.PROPOSAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT += Amount;

                        }
                        if (data.COMPLETION_DONE_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_DONE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount;

                        }
                        if (data.COMPLETION_PROGRESS_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_PROGRESS_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount;

                        }



                        if (data.PROPOSAL_COUNT != 0)
                        {
                            NUMBER += Convert.ToInt32(data.PROPOSAL_COUNT);
                        }
                        if (data.COMPLETION_DONE_COUNT != 0)
                        {
                            NUMBER1 += Convert.ToInt32(data.COMPLETION_DONE_COUNT);
                        }
                        if (data.COMPLETION_PROGRESS_COUNT != 0)
                        {
                            NUMBER2 += Convert.ToInt32(data.COMPLETION_PROGRESS_COUNT);
                        }

                    }


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);

                    depname.SetValue(Niit, "Нийт дүн");


                    total = body;
                    total.Add(Niit);

                    response.data = total;
                }
                else
                {
                    response.data = body;
                }
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public NM5ListResponse NM5List(NM5ListRequest request)
        {
            NM5ListResponse response = new NM5ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.NM5(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<NM5> body = new List<NM5>();
                List<NM5> AllData = new List<NM5>();
                List<NM5> total = new List<NM5>();
                NM5 Niit = new NM5();
                var typ = typeof(NM5);
                if (res != null && res.Elements("NM5") != null)
                {
                    body = (from item in res.Elements("NM5") select new NM5().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new NM5().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                 
                if (body.Count > 0)
                {
                    var depname = typ.GetProperty("TOPIC_CODE");

                    var pay = typ.GetProperty("LAW_AMOUNT");
                    var pay1 = typ.GetProperty("COMPLETION_DONE_AMOUNT");
                    var pay2 = typ.GetProperty("COMPLETION_PROGRESS_AMOUNT");
                    var pay3 = typ.GetProperty("COMPLETION_INVALID_AMOUNT");
                    var pay4 = typ.GetProperty("LAW_C2_AMOUNT");

                    var count = typ.GetProperty("LAW_COUNT");
                    var count1 = typ.GetProperty("COMPLETION_DONE_COUNT");
                    var count2 = typ.GetProperty("COMPLETION_PROGRESS_COUNT");
                    var count3 = typ.GetProperty("COMPLETION_INVALID_COUNT");
                    var count4 = typ.GetProperty("LAW_C2_COUNT");


                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;

                    foreach (NM5 data in AllData)
                    {
                        if (data.LAW_AMOUNT != null)
                        {
                            string strNii = data.LAW_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT += Amount;

                        }
                        if (data.COMPLETION_DONE_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_DONE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount;

                        }
                        if (data.COMPLETION_PROGRESS_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_PROGRESS_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount;

                        }
                        if (data.COMPLETION_INVALID_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_INVALID_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount;

                        }
                        if (data.LAW_C2_AMOUNT != null)
                        {
                            string strNii = data.LAW_C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount;

                        }



                        if (data.LAW_COUNT != 0)
                        {
                            NUMBER += Convert.ToInt32(data.LAW_COUNT);
                        }
                        if (data.COMPLETION_DONE_COUNT != 0)
                        {
                            NUMBER1 += Convert.ToInt32(data.COMPLETION_DONE_COUNT);
                        }
                        if (data.COMPLETION_PROGRESS_COUNT != 0)
                        {
                            NUMBER2 += Convert.ToInt32(data.COMPLETION_PROGRESS_COUNT);
                        }
                        if (data.COMPLETION_INVALID_COUNT != 0)
                        {
                            NUMBER3 += Convert.ToInt32(data.COMPLETION_INVALID_COUNT);
                        }
                        if (data.LAW_C2_COUNT != 0)
                        {
                            NUMBER4 += Convert.ToInt32(data.LAW_C2_COUNT);
                        }
                    }


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);

                    depname.SetValue(Niit, "Нийт дүн");


                    total = body;
                    total.Add(Niit);

                    response.data = total;
                }
                else
                {
                    response.data = body;
                }
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public NM6ListResponse NM6List(NM6ListRequest request)
        {
            NM6ListResponse response = new NM6ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.NM6(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<NM6> body = new List<NM6>();
                List<NM6> AllData = new List<NM6>();
                List<NM6> total = new List<NM6>();
                NM6 Niit = new NM6();
                var typ = typeof(NM6);
                if (res != null && res.Elements("NM6") != null)
                {
                    body = (from item in res.Elements("NM6") select new NM6().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new NM6().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                    
                if (body.Count > 0)
                {
                    var depname = typ.GetProperty("TOPIC_CODE");

                    var pay = typ.GetProperty("VIOLATION_AMOUNT");
                    var pay1 = typ.GetProperty("ERROR_AMOUNT");
                    var pay2 = typ.GetProperty("ALL_AMOUNT");
                    var pay3 = typ.GetProperty("CORRECTED_ERROR_AMOUNT");
                    var pay4 = typ.GetProperty("OTHER_ERROR_AMOUNT");
                    var pay5 = typ.GetProperty("ACT_AMOUNT");
                    var pay6 = typ.GetProperty("CLAIM_AMOUNT");
                    var pay7 = typ.GetProperty("REFERENCE_AMOUNT");
                    var pay8 = typ.GetProperty("PROPOSAL_AMOUNT");
                    var pay9 = typ.GetProperty("LAW_AMOUNT");
                    var pay10 = typ.GetProperty("OTHER_AMOUNT");


                    var count = typ.GetProperty("VIOLATION_COUNT");
                    var count1 = typ.GetProperty("ERROR_COUNT");
                    var count2 = typ.GetProperty("CORRECTED_ERROR_COUNT");
                    var count3 = typ.GetProperty("OTHER_ERROR_COUNT");
                    var count4 = typ.GetProperty("ACT_COUNT");
                    var count5 = typ.GetProperty("CLAIM_COUNT");
                    var count6 = typ.GetProperty("REFERENCE_COUNT");
                    var count7 = typ.GetProperty("PROPOSAL_COUNT");
                    var count8 = typ.GetProperty("LAW_COUNT");
                    var count9 = typ.GetProperty("OTHER_COUNT");
                    var count10 = typ.GetProperty("ALL_COUNT");


                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;
                    Decimal AMOUNT5 = 0;
                    Decimal AMOUNT6 = 0;
                    Decimal AMOUNT7 = 0;
                    Decimal AMOUNT8 = 0;
                    Decimal AMOUNT9 = 0;
                    Decimal AMOUNT10 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;
                    int NUMBER5 = 0;
                    int NUMBER6 = 0;
                    int NUMBER7 = 0;
                    int NUMBER8 = 0;
                    int NUMBER9 = 0;
                    int NUMBER10 = 0;

                    foreach (NM6 data in AllData)
                    {
                        if (data.VIOLATION_AMOUNT != null)
                        {
                            string strNii = data.VIOLATION_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT += Amount;

                        }
                        if (data.ERROR_AMOUNT != null)
                        {
                            string strNii = data.ERROR_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount;

                        }
                        if (data.ALL_AMOUNT != null)
                        {
                            string strNii = data.ALL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount;

                        }
                        if (data.CORRECTED_ERROR_AMOUNT != null)
                        {
                            string strNii = data.CORRECTED_ERROR_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount;

                        }
                        if (data.OTHER_ERROR_AMOUNT != null)
                        {
                            string strNii = data.OTHER_ERROR_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount;

                        }
                        if (data.ACT_AMOUNT != null)
                        {
                            string strNii = data.ACT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT5 += Amount;

                        }
                        if (data.CLAIM_AMOUNT != null)
                        {
                            string strNii = data.CLAIM_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT6 += Amount;

                        }
                        if (data.REFERENCE_AMOUNT != null)
                        {
                            string strNii = data.REFERENCE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT7 += Amount;

                        }
                        if (data.PROPOSAL_AMOUNT != null)
                        {
                            string strNii = data.PROPOSAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT8 += Amount;

                        }
                        if (data.LAW_AMOUNT != null)
                        {
                            string strNii = data.LAW_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT9 += Amount;

                        }
                        if (data.OTHER_AMOUNT != null)
                        {
                            string strNii = data.OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT10 += Amount;

                        }


                        if (data.VIOLATION_COUNT != 0)
                        {
                            NUMBER += Convert.ToInt32(data.VIOLATION_COUNT);
                        }
                        if (data.ERROR_COUNT != 0)
                        {
                            NUMBER1 += Convert.ToInt32(data.ERROR_COUNT);
                        }
                        if (data.CORRECTED_ERROR_COUNT != 0)
                        {
                            NUMBER2 += Convert.ToInt32(data.CORRECTED_ERROR_COUNT);
                        }
                        if (data.OTHER_ERROR_COUNT != 0)
                        {
                            NUMBER3 += Convert.ToInt32(data.OTHER_ERROR_COUNT);
                        }
                        if (data.ACT_COUNT != 0)
                        {
                            NUMBER4 += Convert.ToInt32(data.ACT_COUNT);
                        }
                        if (data.CLAIM_COUNT != 0)
                        {
                            NUMBER5 += Convert.ToInt32(data.CLAIM_COUNT);
                        }
                        if (data.REFERENCE_COUNT != 0)
                        {
                            NUMBER6 += Convert.ToInt32(data.REFERENCE_COUNT);
                        }
                        if (data.PROPOSAL_COUNT != 0)
                        {
                            NUMBER7 += Convert.ToInt32(data.PROPOSAL_COUNT);
                        }
                        if (data.LAW_COUNT != 0)
                        {
                            NUMBER8 += Convert.ToInt32(data.LAW_COUNT);
                        }
                        if (data.OTHER_COUNT != 0)
                        {
                            NUMBER9 += Convert.ToInt32(data.OTHER_COUNT);
                        }
                        if (data.ALL_COUNT != 0)
                        {
                            NUMBER10 += Convert.ToInt32(data.ALL_COUNT);
                        }
                    }


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.00"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.00"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.00"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.00"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);
                    count6.SetValue(Niit, NUMBER6);
                    count7.SetValue(Niit, NUMBER7);
                    count8.SetValue(Niit, NUMBER8);
                    count9.SetValue(Niit, NUMBER9);
                    count10.SetValue(Niit, NUMBER10);

                    depname.SetValue(Niit, "Нийт дүн");


                    total = body;
                    total.Add(Niit);

                    response.data = total;
                }
                else
                {
                    response.data = body;
                }
                
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public NM7ListResponse NM7List(NM7ListRequest request)
        {
            NM7ListResponse response = new NM7ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.NM7(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<NM7> body = new List<NM7>();
                List<NM7> AllData = new List<NM7>(); 
                List<NM7> total = new List<NM7>();
                NM7 Niit = new NM7();
                var typ = typeof(NM7);
                if (res != null && res.Elements("NM7") != null)
                {
                    body = (from item in res.Elements("NM7") select new NM7().SetXml(item)).ToList();
                    AllData = (from item in res.Elements("RowCount") select new NM7().SetXml(item)).ToList();
                    response.recordsTotal = AllData.Count();
                }
                  
                if (body.Count > 0)
                {
                    var depname = typ.GetProperty("TOPIC_CODE");

                    var pay = typ.GetProperty("INCOME_STATE_AMOUNT");
                    var pay1 = typ.GetProperty("INCOME_LOCAL_NUMBER");
                    var pay2 = typ.GetProperty("BUDGET_STATE_AMOUNT");
                    var pay3 = typ.GetProperty("BUDGET_LOCAL_AMOUNT");
                    var pay4 = typ.GetProperty("ACCOUNTANT_AMOUNT");
                    var pay5 = typ.GetProperty("EFFICIENCY_AMOUNT");
                    var pay6 = typ.GetProperty("LAW_AMOUNT");
                    var pay7 = typ.GetProperty("MONITORING_AMOUNT");
                    var pay8 = typ.GetProperty("PURCHASE_AMOUNT");
                    var pay9 = typ.GetProperty("COST_AMOUNT");
                    var pay10 = typ.GetProperty("OTHER_AMOUNT");
                    var pay11 = typ.GetProperty("ALL_AMOUNT");


                    var count = typ.GetProperty("INCOME_STATE_COUNT");
                    var count1 = typ.GetProperty("INCOME_LOCAL_COUNT");
                    var count2 = typ.GetProperty("BUDGET_STATE_COUNT");
                    var count3 = typ.GetProperty("BUDGET_LOCAL_COUNT");
                    var count4 = typ.GetProperty("ACCOUNTANT_COUNT");
                    var count5 = typ.GetProperty("EFFICIENCY_COUNT");
                    var count6 = typ.GetProperty("LAW_COUNT");
                    var count7 = typ.GetProperty("MONITORING_COUNT");
                    var count8 = typ.GetProperty("PURCHASE_COUNT");
                    var count9 = typ.GetProperty("OTHER_COUNT");
                    var count10 = typ.GetProperty("ALL_COUNT");
                    var count11 = typ.GetProperty("COST_COUNT");


                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;
                    Decimal AMOUNT5 = 0;
                    Decimal AMOUNT6 = 0;
                    Decimal AMOUNT7 = 0;
                    Decimal AMOUNT8 = 0;
                    Decimal AMOUNT9 = 0;
                    Decimal AMOUNT10 = 0;
                    Decimal AMOUNT11 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;
                    int NUMBER5 = 0;
                    int NUMBER6 = 0;
                    int NUMBER7 = 0;
                    int NUMBER8 = 0;
                    int NUMBER9 = 0;
                    int NUMBER10 = 0;
                    int NUMBER11 = 0;

                    foreach (NM7 data in AllData)
                    {
                        if (data.INCOME_STATE_AMOUNT != null)
                        {
                            string strNii = data.INCOME_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT += Amount;

                        }
                        if (data.INCOME_LOCAL_NUMBER != null)
                        {
                            string strNii = data.INCOME_LOCAL_NUMBER.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount;

                        }
                        if (data.BUDGET_STATE_AMOUNT != null)
                        {
                            string strNii = data.BUDGET_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount;

                        }
                        if (data.BUDGET_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.BUDGET_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount;

                        }
                        if (data.ACCOUNTANT_AMOUNT != null)
                        {
                            string strNii = data.ACCOUNTANT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount;

                        }
                        if (data.EFFICIENCY_AMOUNT != null)
                        {
                            string strNii = data.EFFICIENCY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT5 += Amount;

                        }
                        if (data.LAW_AMOUNT != null)
                        {
                            string strNii = data.LAW_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT6 += Amount;

                        }
                        if (data.MONITORING_AMOUNT != null)
                        {
                            string strNii = data.MONITORING_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT7 += Amount;

                        }
                        if (data.PURCHASE_AMOUNT != null)
                        {
                            string strNii = data.PURCHASE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT8 += Amount;

                        }
                        if (data.COST_AMOUNT != null)
                        {
                            string strNii = data.COST_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT9 += Amount;

                        }
                        if (data.OTHER_AMOUNT != null)
                        {
                            string strNii = data.OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT10 += Amount;

                        }
                        if (data.ALL_AMOUNT != null)
                        {
                            string strNii = data.ALL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT11 += Amount;

                        }

                        if (data.INCOME_STATE_COUNT != 0)
                        {
                            NUMBER += Convert.ToInt32(data.INCOME_STATE_COUNT);
                        }
                        if (data.INCOME_LOCAL_COUNT != 0)
                        {
                            NUMBER1 += Convert.ToInt32(data.INCOME_LOCAL_COUNT);
                        }
                        if (data.BUDGET_STATE_COUNT != 0)
                        {
                            NUMBER2 += Convert.ToInt32(data.BUDGET_STATE_COUNT);
                        }
                        if (data.BUDGET_LOCAL_COUNT != 0)
                        {
                            NUMBER3 += Convert.ToInt32(data.BUDGET_LOCAL_COUNT);
                        }
                        if (data.ACCOUNTANT_COUNT != 0)
                        {
                            NUMBER4 += Convert.ToInt32(data.ACCOUNTANT_COUNT);
                        }
                        if (data.EFFICIENCY_COUNT != 0)
                        {
                            NUMBER5 += Convert.ToInt32(data.EFFICIENCY_COUNT);
                        }
                        if (data.LAW_COUNT != 0)
                        {
                            NUMBER6 += Convert.ToInt32(data.LAW_COUNT);
                        }
                        if (data.MONITORING_COUNT != 0)
                        {
                            NUMBER7 += Convert.ToInt32(data.MONITORING_COUNT);
                        }
                        if (data.PURCHASE_COUNT != 0)
                        {
                            NUMBER8 += Convert.ToInt32(data.PURCHASE_COUNT);
                        }
                        if (data.OTHER_COUNT != 0)
                        {
                            NUMBER9 += Convert.ToInt32(data.OTHER_COUNT);
                        }
                        if (data.ALL_COUNT != 0)
                        {
                            NUMBER10 += Convert.ToInt32(data.ALL_COUNT);
                        }
                        if (data.COST_COUNT != 0)
                        {
                            NUMBER11 += Convert.ToInt32(data.COST_COUNT);
                        }
                    }


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.00"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.00"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.00"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.00"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.00"));
                    pay11.SetValue(Niit, AMOUNT11.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);
                    count6.SetValue(Niit, NUMBER6);
                    count7.SetValue(Niit, NUMBER7);
                    count8.SetValue(Niit, NUMBER8);
                    count9.SetValue(Niit, NUMBER9);
                    count10.SetValue(Niit, NUMBER10);
                    count11.SetValue(Niit, NUMBER11);

                    depname.SetValue(Niit, "Нийт дүн");


                    total = body;
                    total.Add(Niit);

                    response.data = total;
                }
                else
                {
                    response.data = body;
                }
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public CM1ListResponse CM1List(CM1ListRequest request)
        {
            CM1ListResponse response = new CM1ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                elem.Add(new XElement("V_TYPE", request.Type));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.CM1(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                if (res != null && res.Elements("CM1") != null)
                    response.data = (from item in res.Elements("CM1") select new CM1().SetXml(item)).ToList();

                response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public CM2ListResponse CM2List(CM2ListRequest request)
        {
            CM2ListResponse response = new CM2ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                elem.Add(new XElement("V_TYPE", request.Type));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.CM2(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                if (res != null && res.Elements("CM2") != null)
                {
                    List<CM2> body = new List<CM2>();
                    List<CM2> cm2Detial = new List<CM2>();
                    CM2 title = new CM2();
                    List<CM2> total = new List<CM2>();
                    var typ = typeof(CM2);

                    body = (from item in res.Elements("CM2") select new CM2().SetXml(item)).ToList();
                    if (body.Count > 0)
                    {
                        if (request.Type == 1)
                        {
                            
                            List<CM2> temp = new List<CM2>();
                            List<CM2> totaltemp = new List<CM2>();


                            List<CM2> UAG = new List<CM2>();
                            List<CM2> UAG1 = new List<CM2>();
                            List<CM2> UAG2 = new List<CM2>();
                            CM2ListResponse UAGdata1 = new CM2ListResponse();
                            CM2ListResponse UAGdata2 = new CM2ListResponse();
                            CM2ListResponse UAGTotal = new CM2ListResponse();
                            UAG = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Үндэсний аудитын газар"));
                            UAG1 = UAG.FindAll(a => a.IS_STATE.Equals(1));
                            UAG2 = UAG.FindAll(a => a.IS_STATE.Equals(2));
                            if(UAG1.Count > 0)
                                UAGdata1 = Cm2TypesList(UAG1, 1);
                            if (UAG2.Count > 0)
                                UAGdata2 = Cm2TypesList(UAG2, 2);
                            if (UAG.Count > 0)
                                UAGTotal = Cm2TotalList(UAG);

                            List<CM2> NT = new List<CM2>();
                            List<CM2> NT1 = new List<CM2>();
                            List<CM2> NT2 = new List<CM2>();
                            CM2ListResponse NTdata1 = new CM2ListResponse();
                            CM2ListResponse NTdata2 = new CM2ListResponse();
                            CM2ListResponse NTTotal = new CM2ListResponse();
                            NT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Нийслэл дэх ТАГ"));
                            NT1 = NT.FindAll(a => a.IS_STATE.Equals(1));
                            NT2 = NT.FindAll(a => a.IS_STATE.Equals(2));
                            if (NT1.Count > 0)
                                NTdata1 = Cm2TypesList(NT1, 1);
                            if (NT2.Count > 0)
                                NTdata2 = Cm2TypesList(NT2, 2);
                            if (NT.Count > 0)
                                NTTotal = Cm2TotalList(NT);

                            List<CM2> ART = new List<CM2>();
                            List<CM2> ART1 = new List<CM2>();
                            List<CM2> ART2 = new List<CM2>();
                            CM2ListResponse ARTdata1 = new CM2ListResponse();
                            CM2ListResponse ARTdata2 = new CM2ListResponse();
                            CM2ListResponse ARTTotal = new CM2ListResponse();
                            ART = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Архангай аймаг дахь ТАГ"));
                            ART1 = ART.FindAll(a => a.IS_STATE.Equals(1));
                            ART2 = ART.FindAll(a => a.IS_STATE.Equals(2));
                            if (ART1.Count > 0)
                                ARTdata1 = Cm2TypesList(ART1, 1);
                            if (ART2.Count > 0)
                                ARTdata2 = Cm2TypesList(ART2, 2);
                            if (ART.Count > 0)
                                ARTTotal = Cm2TotalList(ART);

                            List<CM2> BUT = new List<CM2>();
                            List<CM2> BUT1 = new List<CM2>();
                            List<CM2> BUT2 = new List<CM2>();
                            CM2ListResponse BUTdata1 = new CM2ListResponse();
                            CM2ListResponse BUTdata2 = new CM2ListResponse();
                            CM2ListResponse BUTTotal = new CM2ListResponse();
                            BUT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Баян-Өлгий аймаг дахь ТАГ"));
                            BUT1 = BUT.FindAll(a => a.IS_STATE.Equals(1));
                            BUT2 = BUT.FindAll(a => a.IS_STATE.Equals(2));
                            if (BUT1.Count > 0)
                                BUTdata1 = Cm2TypesList(BUT1, 1);
                            if (BUT2.Count > 0)
                                BUTdata2 = Cm2TypesList(BUT2, 2);
                            if (BUT.Count > 0)
                                BUTTotal = Cm2TotalList(BUT);

                            List<CM2> BHT = new List<CM2>();
                            List<CM2> BHT1 = new List<CM2>();
                            List<CM2> BHT2 = new List<CM2>();
                            CM2ListResponse BHTdata1 = new CM2ListResponse();
                            CM2ListResponse BHTdata2 = new CM2ListResponse();
                            CM2ListResponse BHTTotal = new CM2ListResponse();
                            BHT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Баянхонгор аймаг дахь ТАГ"));
                            BHT1 = BHT.FindAll(a => a.IS_STATE.Equals(1));
                            BHT2 = BHT.FindAll(a => a.IS_STATE.Equals(2));
                            if (BHT1.Count > 0)
                                BHTdata1 = Cm2TypesList(BHT1, 1);
                            if (BHT2.Count > 0)
                                BHTdata2 = Cm2TypesList(BHT2, 2);
                            if (BHT.Count > 0)
                                BHTTotal = Cm2TotalList(BHT);

                            List<CM2> BLT = new List<CM2>();
                            List<CM2> BLT1 = new List<CM2>();
                            List<CM2> BLT2 = new List<CM2>();
                            CM2ListResponse BLTdata1 = new CM2ListResponse();
                            CM2ListResponse BLTdata2 = new CM2ListResponse();
                            CM2ListResponse BLTTotal = new CM2ListResponse();
                            BLT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Булган аймаг дахь ТАГ"));
                            BLT1 = BLT.FindAll(a => a.IS_STATE.Equals(1));
                            BLT2 = BLT.FindAll(a => a.IS_STATE.Equals(2));
                            if (BLT1.Count > 0)
                                BLTdata1 = Cm2TypesList(BLT1, 1);
                            if (BLT2.Count > 0)
                                BLTdata2 = Cm2TypesList(BLT2, 2);
                            if (BLT.Count > 0)
                                BLTTotal = Cm2TotalList(BLT);

                            List<CM2> GAT = new List<CM2>();
                            List<CM2> GAT1 = new List<CM2>();
                            List<CM2> GAT2 = new List<CM2>();
                            CM2ListResponse GATdata1 = new CM2ListResponse();
                            CM2ListResponse GATdata2 = new CM2ListResponse();
                            CM2ListResponse GATTotal = new CM2ListResponse();
                            GAT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Говь-Алтай аймаг дахь ТАГ"));
                            GAT1 = GAT.FindAll(a => a.IS_STATE.Equals(1));
                            GAT2 = GAT.FindAll(a => a.IS_STATE.Equals(2));
                            if (GAT1.Count > 0)
                                GATdata1 = Cm2TypesList(GAT1, 1);
                            if (GAT2.Count > 0)
                                GATdata2 = Cm2TypesList(GAT2, 2);
                            if (GAT.Count > 0)
                                GATTotal = Cm2TotalList(GAT);

                            List<CM2> GST = new List<CM2>();
                            List<CM2> GST1 = new List<CM2>();
                            List<CM2> GST2 = new List<CM2>();
                            CM2ListResponse GSTdata1 = new CM2ListResponse();
                            CM2ListResponse GSTdata2 = new CM2ListResponse();
                            CM2ListResponse GSTTotal = new CM2ListResponse();
                            GST = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Говьсүмбэр аймаг дахь ТАГ"));
                            GST1 = GST.FindAll(a => a.IS_STATE.Equals(1));
                            GST2 = GST.FindAll(a => a.IS_STATE.Equals(2));
                            if (GST1.Count > 0)
                                GSTdata1 = Cm2TypesList(GST1, 1);
                            if (GST2.Count > 0)
                                GSTdata2 = Cm2TypesList(GST2, 2);
                            if (GST.Count > 0)
                                GSTTotal = Cm2TotalList(GST);

                            List<CM2> DAT = new List<CM2>();
                            List<CM2> DAT1 = new List<CM2>();
                            List<CM2> DAT2 = new List<CM2>();
                            CM2ListResponse DATdata1 = new CM2ListResponse();
                            CM2ListResponse DATdata2 = new CM2ListResponse();
                            CM2ListResponse DATTotal = new CM2ListResponse();
                            DAT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дархан-Уул аймаг дахь ТАГ"));
                            DAT1 = DAT.FindAll(a => a.IS_STATE.Equals(1));
                            DAT2 = DAT.FindAll(a => a.IS_STATE.Equals(2));
                            if (DAT1.Count > 0)
                                DATdata1 = Cm2TypesList(DAT1, 1);
                            if (DAT2.Count > 0)
                                DATdata2 = Cm2TypesList(DAT2, 2);
                            if (DAT.Count > 0)
                                DATTotal = Cm2TotalList(DAT);

                            List<CM2> DOT = new List<CM2>();
                            List<CM2> DOT1 = new List<CM2>();
                            List<CM2> DOT2 = new List<CM2>();
                            CM2ListResponse DOTdata1 = new CM2ListResponse();
                            CM2ListResponse DOTdata2 = new CM2ListResponse();
                            CM2ListResponse DOTTotal = new CM2ListResponse();
                            DOT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дорноговь аймаг дахь ТАГ"));
                            DOT1 = DOT.FindAll(a => a.IS_STATE.Equals(1));
                            DOT2 = DOT.FindAll(a => a.IS_STATE.Equals(2));
                            if (DOT1.Count > 0)
                                DOTdata1 = Cm2TypesList(DOT1, 1);
                            if (DOT2.Count > 0)
                                DOTdata2 = Cm2TypesList(DOT2, 2);
                            if (DOT.Count > 0)
                                DOTTotal = Cm2TotalList(DOT);

                            List<CM2> DNT = new List<CM2>();
                            List<CM2> DNT1 = new List<CM2>();
                            List<CM2> DNT2 = new List<CM2>();
                            CM2ListResponse DNTdata1 = new CM2ListResponse();
                            CM2ListResponse DNTdata2 = new CM2ListResponse();
                            CM2ListResponse DNTTotal = new CM2ListResponse();
                            DNT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дорнод аймаг дахь ТАГ"));
                            DNT1 = DNT.FindAll(a => a.IS_STATE.Equals(1));
                            DNT2 = DNT.FindAll(a => a.IS_STATE.Equals(2));
                            if (DNT1.Count > 0)
                                DNTdata1 = Cm2TypesList(DNT1, 1);
                            if (DNT2.Count > 0)
                                DNTdata2 = Cm2TypesList(DNT2, 2);
                            if (DNT.Count > 0)
                                DNTTotal = Cm2TotalList(DNT);

                            List<CM2> DGT = new List<CM2>();
                            List<CM2> DGT1 = new List<CM2>();
                            List<CM2> DGT2 = new List<CM2>();
                            CM2ListResponse DGTdata1 = new CM2ListResponse();
                            CM2ListResponse DGTdata2 = new CM2ListResponse();
                            CM2ListResponse DGTTotal = new CM2ListResponse();
                            DGT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дундговь аймаг дахь ТАГ"));
                            DGT1 = DGT.FindAll(a => a.IS_STATE.Equals(1));
                            DGT2 = DGT.FindAll(a => a.IS_STATE.Equals(2));
                            if (DGT1.Count > 0)
                                DGTdata1 = Cm2TypesList(DGT1, 1);
                            if (DGT2.Count > 0)
                                DGTdata2 = Cm2TypesList(DGT2, 2);
                            if (DGT.Count > 0)
                                DGTTotal = Cm2TotalList(DGT);

                            List<CM2> ZAT = new List<CM2>();
                            List<CM2> ZAT1 = new List<CM2>();
                            List<CM2> ZAT2 = new List<CM2>();
                            CM2ListResponse ZATdata1 = new CM2ListResponse();
                            CM2ListResponse ZATdata2 = new CM2ListResponse();
                            CM2ListResponse ZATTotal = new CM2ListResponse();
                            ZAT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Завхан аймаг дахь ТАГ"));
                            ZAT1 = ZAT.FindAll(a => a.IS_STATE.Equals(1));
                            ZAT2 = ZAT.FindAll(a => a.IS_STATE.Equals(2));
                            if (ZAT1.Count > 0)
                                ZATdata1 = Cm2TypesList(ZAT1, 1);
                            if (ZAT2.Count > 0)
                                ZATdata2 = Cm2TypesList(ZAT2, 2);
                            if (ZAT.Count > 0)
                                ZATTotal = Cm2TotalList(ZAT);

                            List<CM2> ORT = new List<CM2>();
                            List<CM2> ORT1 = new List<CM2>();
                            List<CM2> ORT2 = new List<CM2>();
                            CM2ListResponse ORTdata1 = new CM2ListResponse();
                            CM2ListResponse ORTdata2 = new CM2ListResponse();
                            CM2ListResponse ORTTotal = new CM2ListResponse();
                            ORT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Орхон аймаг дахь ТАГ"));
                            ORT1 = ORT.FindAll(a => a.IS_STATE.Equals(1));
                            ORT2 = ORT.FindAll(a => a.IS_STATE.Equals(2));
                            if (ORT1.Count > 0)
                                ORTdata1 = Cm2TypesList(ORT1, 1);
                            if (ORT2.Count > 0)
                                ORTdata2 = Cm2TypesList(ORT2, 2);
                            if (ORT.Count > 0)
                                ORTTotal = Cm2TotalList(ORT);

                            List<CM2> UVT = new List<CM2>();
                            List<CM2> UVT1 = new List<CM2>();
                            List<CM2> UVT2 = new List<CM2>();
                            CM2ListResponse UVTdata1 = new CM2ListResponse();
                            CM2ListResponse UVTdata2 = new CM2ListResponse();
                            CM2ListResponse UVTTotal = new CM2ListResponse();
                            UVT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Өвөрхангай аймаг дахь ТАГ"));
                            UVT1 = UVT.FindAll(a => a.IS_STATE.Equals(1));
                            UVT2 = UVT.FindAll(a => a.IS_STATE.Equals(2));
                            if (UVT1.Count > 0)
                                UVTdata1 = Cm2TypesList(UVT1, 1);
                            if (UVT2.Count > 0)
                                UVTdata2 = Cm2TypesList(UVT2, 2);
                            if (UVT.Count > 0)
                                UVTTotal = Cm2TotalList(UVT);

                            List<CM2> UMT = new List<CM2>();
                            List<CM2> UMT1 = new List<CM2>();
                            List<CM2> UMT2 = new List<CM2>();
                            CM2ListResponse UMTdata1 = new CM2ListResponse();
                            CM2ListResponse UMTdata2 = new CM2ListResponse();
                            CM2ListResponse UMTTotal = new CM2ListResponse();
                            UMT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Өмнөговь аймаг дахь ТАГ"));
                            UMT1 = UMT.FindAll(a => a.IS_STATE.Equals(1));
                            UMT2 = UMT.FindAll(a => a.IS_STATE.Equals(2));
                            if (UMT1.Count > 0)
                                UMTdata1 = Cm2TypesList(UMT1, 1);
                            if (UMT2.Count > 0)
                                UMTdata2 = Cm2TypesList(UMT2, 2);
                            if (UMT.Count > 0)
                                UMTTotal = Cm2TotalList(UMT);

                            List<CM2> SBT = new List<CM2>();
                            List<CM2> SBT1 = new List<CM2>();
                            List<CM2> SBT2 = new List<CM2>();
                            CM2ListResponse SBTdata1 = new CM2ListResponse();
                            CM2ListResponse SBTdata2 = new CM2ListResponse();
                            CM2ListResponse SBTTotal = new CM2ListResponse();
                            SBT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Сүхбаатар аймаг дахь ТАГ"));
                            SBT1 = SBT.FindAll(a => a.IS_STATE.Equals(1));
                            SBT2 = SBT.FindAll(a => a.IS_STATE.Equals(2));
                            if (SBT1.Count > 0)
                                SBTdata1 = Cm2TypesList(SBT1, 1);
                            if (SBT2.Count > 0)
                                SBTdata2 = Cm2TypesList(SBT2, 2);
                            if (SBT.Count > 0)
                                SBTTotal = Cm2TotalList(SBT);

                            List<CM2> SET = new List<CM2>();
                            List<CM2> SET1 = new List<CM2>();
                            List<CM2> SET2 = new List<CM2>();
                            CM2ListResponse SETdata1 = new CM2ListResponse();
                            CM2ListResponse SETdata2 = new CM2ListResponse();
                            CM2ListResponse SETTotal = new CM2ListResponse();
                            SET = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Сэлэнгэ аймаг дахь ТАГ"));
                            SET1 = SET.FindAll(a => a.IS_STATE.Equals(1));
                            SET2 = SET.FindAll(a => a.IS_STATE.Equals(2));
                            if (SET1.Count > 0)
                                SETdata1 = Cm2TypesList(SET1, 1);
                            if (SET2.Count > 0)
                                SETdata2 = Cm2TypesList(SET2, 2);
                            if (SET.Count > 0)
                                SETTotal = Cm2TotalList(SET);

                            List<CM2> TUT = new List<CM2>();
                            List<CM2> TUT1 = new List<CM2>();
                            List<CM2> TUT2 = new List<CM2>();
                            CM2ListResponse TUTdata1 = new CM2ListResponse();
                            CM2ListResponse TUTdata2 = new CM2ListResponse();
                            CM2ListResponse TUTTotal = new CM2ListResponse();
                            TUT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Төв аймаг дахь ТАГ"));
                            TUT1 = TUT.FindAll(a => a.IS_STATE.Equals(1));
                            TUT2 = TUT.FindAll(a => a.IS_STATE.Equals(2));
                            if (TUT1.Count > 0)
                                TUTdata1 = Cm2TypesList(TUT1, 1);
                            if (TUT2.Count > 0)
                                TUTdata2 = Cm2TypesList(TUT2, 2);
                            if (TUT.Count > 0)
                                TUTTotal = Cm2TotalList(TUT);

                            List<CM2> UST = new List<CM2>();
                            List<CM2> UST1 = new List<CM2>();
                            List<CM2> UST2 = new List<CM2>();
                            CM2ListResponse USTdata1 = new CM2ListResponse();
                            CM2ListResponse USTdata2 = new CM2ListResponse();
                            CM2ListResponse USTTotal = new CM2ListResponse();
                            UST = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Увс аймаг дахь ТАГ"));
                            UST1 = UST.FindAll(a => a.IS_STATE.Equals(1));
                            UST2 = UST.FindAll(a => a.IS_STATE.Equals(2));
                            if (UST1.Count > 0)
                                USTdata1 = Cm2TypesList(UST1, 1);
                            if (UST2.Count > 0)
                                USTdata2 = Cm2TypesList(UST2, 2);
                            if (UST.Count > 0)
                                USTTotal = Cm2TotalList(UST);

                            List<CM2> HOT = new List<CM2>();
                            List<CM2> HOT1 = new List<CM2>();
                            List<CM2> HOT2 = new List<CM2>();
                            CM2ListResponse HOTdata1 = new CM2ListResponse();
                            CM2ListResponse HOTdata2 = new CM2ListResponse();
                            CM2ListResponse HOTTotal = new CM2ListResponse();
                            HOT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Ховд аймаг дахь ТАГ"));
                            HOT1 = HOT.FindAll(a => a.IS_STATE.Equals(1));
                            HOT2 = HOT.FindAll(a => a.IS_STATE.Equals(2));
                            if (HOT1.Count > 0)
                                HOTdata1 = Cm2TypesList(HOT1, 1);
                            if (HOT2.Count > 0)
                                HOTdata2 = Cm2TypesList(HOT2, 2);
                            if (HOT.Count > 0)
                                HOTTotal = Cm2TotalList(HOT);

                            List<CM2> HUT = new List<CM2>();
                            List<CM2> HUT1 = new List<CM2>();
                            List<CM2> HUT2 = new List<CM2>();
                            CM2ListResponse HUTdata1 = new CM2ListResponse();
                            CM2ListResponse HUTdata2 = new CM2ListResponse();
                            CM2ListResponse HUTTotal = new CM2ListResponse();
                            HUT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Хөвсгөл аймаг дахь ТАГ"));
                            HUT1 = HUT.FindAll(a => a.IS_STATE.Equals(1));
                            HUT2 = HUT.FindAll(a => a.IS_STATE.Equals(2));
                            if (HUT1.Count > 0)
                                HUTdata1 = Cm2TypesList(HUT1, 1);
                            if (HUT2.Count > 0)
                                HUTdata2 = Cm2TypesList(HUT2, 2);
                            if (HUT.Count > 0)
                                HUTTotal = Cm2TotalList(HUT);

                            List<CM2> HET = new List<CM2>();
                            List<CM2> HET1 = new List<CM2>();
                            List<CM2> HET2 = new List<CM2>();
                            CM2ListResponse HETdata1 = new CM2ListResponse();
                            CM2ListResponse HETdata2 = new CM2ListResponse();
                            CM2ListResponse HETTotal = new CM2ListResponse();
                            HET = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Хэнтий аймаг дахь ТАГ"));
                            HET1 = HET.FindAll(a => a.IS_STATE.Equals(1));
                            HET2 = HET.FindAll(a => a.IS_STATE.Equals(2));
                            if (HET1.Count > 0)
                                HETdata1 = Cm2TypesList(HET1, 1);
                            if (HET2.Count > 0)
                                HETdata2 = Cm2TypesList(HET2, 2);
                            if (HET.Count > 0)
                                HETTotal = Cm2TotalList(HET);


                            List<CM2> types = new List<CM2>();
                            types.AddRange(UAGdata1.data);
                            types.AddRange(UAGdata2.data);
                            types.AddRange(UAGTotal.data);
                            types.AddRange(NTdata1.data);
                            types.AddRange(NTdata2.data);
                            types.AddRange(NTTotal.data);
                            types.AddRange(ARTdata1.data);
                            types.AddRange(ARTdata2.data);
                            types.AddRange(ARTTotal.data);
                            types.AddRange(BUTdata1.data);
                            types.AddRange(BUTdata2.data);
                            types.AddRange(BUTTotal.data);
                            types.AddRange(BHTdata1.data);
                            types.AddRange(BHTdata2.data);
                            types.AddRange(BHTTotal.data);
                            types.AddRange(BLTdata1.data);
                            types.AddRange(BLTdata2.data);
                            types.AddRange(BLTTotal.data);
                            types.AddRange(GATdata1.data);
                            types.AddRange(GATdata2.data);
                            types.AddRange(GATTotal.data);
                            types.AddRange(GSTdata1.data);
                            types.AddRange(GSTdata2.data);
                            types.AddRange(GSTTotal.data);
                            types.AddRange(DATdata1.data);
                            types.AddRange(DATdata2.data);
                            types.AddRange(DATTotal.data);
                            types.AddRange(DOTdata1.data);
                            types.AddRange(DOTdata2.data);
                            types.AddRange(DOTTotal.data);
                            types.AddRange(DNTdata1.data);
                            types.AddRange(DNTdata2.data);
                            types.AddRange(DNTTotal.data);
                            types.AddRange(DGTdata1.data);
                            types.AddRange(DGTdata2.data);
                            types.AddRange(DGTTotal.data);
                            types.AddRange(ZATdata1.data);
                            types.AddRange(ZATdata2.data);
                            types.AddRange(ZATTotal.data);
                            types.AddRange(ORTdata1.data);
                            types.AddRange(ORTdata2.data);
                            types.AddRange(ORTTotal.data);
                            types.AddRange(UVTdata1.data);
                            types.AddRange(UVTdata2.data);
                            types.AddRange(UVTTotal.data);
                            types.AddRange(UMTdata1.data);
                            types.AddRange(UMTdata2.data);
                            types.AddRange(UMTTotal.data);
                            types.AddRange(SBTdata1.data);
                            types.AddRange(SBTdata2.data);
                            types.AddRange(SBTTotal.data);
                            types.AddRange(SETdata1.data);
                            types.AddRange(SETdata2.data);
                            types.AddRange(SETTotal.data);
                            types.AddRange(TUTdata1.data);
                            types.AddRange(TUTdata2.data);
                            types.AddRange(TUTTotal.data);
                            types.AddRange(USTdata1.data);
                            types.AddRange(USTdata2.data);
                            types.AddRange(USTTotal.data);
                            types.AddRange(HOTdata1.data);
                            types.AddRange(HOTdata2.data);
                            types.AddRange(HOTTotal.data);
                            types.AddRange(HUTdata1.data);
                            types.AddRange(HUTdata2.data);
                            types.AddRange(HUTTotal.data);
                            types.AddRange(HETdata1.data);
                            types.AddRange(HETdata2.data);
                            types.AddRange(HETTotal.data);

                            cm2Detial = types;

                            response.data = cm2Detial;

                        }
                        else
                        {
                            List<CM2> Bodytemp = new List<CM2>();

                            List<CM2> UAG = new List<CM2>();
                            CM2ListResponse UAGTotal = new CM2ListResponse();
                            UAG = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Үндэсний аудитын газар"));
                            if (UAG.Count > 0)
                                UAGTotal = Cm2TypesList(UAG,0);

                            List<CM2> NT = new List<CM2>();
                            CM2ListResponse NTTotal = new CM2ListResponse();
                            NT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Нийслэл дэх ТАГ"));
                            if (NT.Count > 0)
                                NTTotal = Cm2TypesList(NT,0);

                            List<CM2> ART = new List<CM2>();
                            CM2ListResponse ARTTotal = new CM2ListResponse();
                            ART = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Архангай аймаг дахь ТАГ"));
                            if (ART.Count > 0)
                                ARTTotal = Cm2TypesList(ART,0);

                            List<CM2> BUT = new List<CM2>();
                            CM2ListResponse BUTTotal = new CM2ListResponse();
                            BUT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Баян-Өлгий аймаг дахь ТАГ"));
                            if (BUT.Count > 0)
                                BUTTotal = Cm2TypesList(BUT,0);

                            List<CM2> BHT = new List<CM2>();
                            CM2ListResponse BHTTotal = new CM2ListResponse();
                            BHT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Баянхонгор аймаг дахь ТАГ"));
                            if (BHT.Count > 0)
                                BHTTotal = Cm2TypesList(BHT,0);

                            List<CM2> BLT = new List<CM2>();
                            CM2ListResponse BLTTotal = new CM2ListResponse();
                            BLT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Булган аймаг дахь ТАГ"));
                            if (BLT.Count > 0)
                                BLTTotal = Cm2TypesList(BLT,0);

                            List<CM2> GAT = new List<CM2>();
                            CM2ListResponse GATTotal = new CM2ListResponse();
                            GAT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Говь-Алтай аймаг дахь ТАГ"));
                            if (GAT.Count > 0)
                                GATTotal = Cm2TypesList(GAT,0);

                            List<CM2> GST = new List<CM2>();
                            CM2ListResponse GSTTotal = new CM2ListResponse();
                            GST = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Говьсүмбэр аймаг дахь ТАГ"));
                            if (GST.Count > 0)
                                GSTTotal = Cm2TypesList(GST,0);

                            List<CM2> DAT = new List<CM2>();
                            CM2ListResponse DATTotal = new CM2ListResponse();
                            DAT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дархан-Уул аймаг дахь ТАГ"));
                            if (DAT.Count > 0)
                                DATTotal = Cm2TypesList(DAT,0);

                            List<CM2> DOT = new List<CM2>();
                            CM2ListResponse DOTTotal = new CM2ListResponse();
                            DOT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дорноговь аймаг дахь ТАГ"));
                            if (DOT.Count > 0)
                                DOTTotal = Cm2TypesList(DOT,0);

                            List<CM2> DNT = new List<CM2>();
                            CM2ListResponse DNTTotal = new CM2ListResponse();
                            DNT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дорнод аймаг дахь ТАГ"));
                            if (DNT.Count > 0)
                                DNTTotal = Cm2TypesList(DNT,0);

                            List<CM2> DGT = new List<CM2>();
                            CM2ListResponse DGTTotal = new CM2ListResponse();
                            DGT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дундговь аймаг дахь ТАГ"));
                            if (DGT.Count > 0)
                                DGTTotal = Cm2TypesList(DGT,0);

                            List<CM2> ZAT = new List<CM2>();
                            CM2ListResponse ZATTotal = new CM2ListResponse();
                            ZAT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Завхан аймаг дахь ТАГ"));
                            if (ZAT.Count > 0)
                                ZATTotal = Cm2TypesList(ZAT,0);

                            List<CM2> ORT = new List<CM2>();
                            CM2ListResponse ORTTotal = new CM2ListResponse();
                            ORT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Орхон аймаг дахь ТАГ"));
                            if (ORT.Count > 0)
                                ORTTotal = Cm2TypesList(ORT,0);

                            List<CM2> UVT = new List<CM2>();
                            CM2ListResponse UVTTotal = new CM2ListResponse();
                            UVT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Өвөрхангай аймаг дахь ТАГ"));
                            if (UVT.Count > 0)
                                UVTTotal = Cm2TypesList(UVT,0);

                            List<CM2> UMT = new List<CM2>();
                            CM2ListResponse UMTTotal = new CM2ListResponse();
                            UMT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Өмнөговь аймаг дахь ТАГ"));
                            if (UMT.Count > 0)
                                UMTTotal = Cm2TypesList(UMT,0);

                            List<CM2> SBT = new List<CM2>();
                            CM2ListResponse SBTTotal = new CM2ListResponse();
                            SBT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Сүхбаатар аймаг дахь ТАГ"));
                            if (SBT.Count > 0)
                                SBTTotal = Cm2TypesList(SBT,0);

                            List<CM2> SET = new List<CM2>();
                            CM2ListResponse SETTotal = new CM2ListResponse();
                            SET = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Сэлэнгэ аймаг дахь ТАГ"));
                            if (SET.Count > 0)
                                SETTotal = Cm2TypesList(SET,0);

                            List<CM2> TUT = new List<CM2>();
                            CM2ListResponse TUTTotal = new CM2ListResponse();
                            TUT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Төв аймаг дахь ТАГ"));
                            if (TUT.Count > 0)
                                TUTTotal = Cm2TypesList(TUT,0);

                            List<CM2> UST = new List<CM2>();
                            CM2ListResponse USTTotal = new CM2ListResponse();
                            UST = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Увс аймаг дахь ТАГ"));
                            if (UST.Count > 0)
                                USTTotal = Cm2TypesList(UST,0);

                            List<CM2> HOT = new List<CM2>();
                            CM2ListResponse HOTTotal = new CM2ListResponse();
                            HOT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Ховд аймаг дахь ТАГ"));
                            if (HOT.Count > 0)
                                HOTTotal = Cm2TypesList(HOT,0);

                            List<CM2> HUT = new List<CM2>();
                            CM2ListResponse HUTTotal = new CM2ListResponse();
                            HUT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Хөвсгөл аймаг дахь ТАГ"));
                            if (HUT.Count > 0)
                                HUTTotal = Cm2TypesList(HUT, 0);

                            List<CM2> HET = new List<CM2>();
                            CM2ListResponse HETTotal = new CM2ListResponse();
                            HET = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Хэнтий аймаг дахь ТАГ"));
                            if (HET.Count > 0)
                                HETTotal = Cm2TypesList(HET, 0);

                            Bodytemp.AddRange(UAGTotal.data);
                            Bodytemp.AddRange(NTTotal.data);
                            Bodytemp.AddRange(ARTTotal.data);
                            Bodytemp.AddRange(BUTTotal.data);  
                            Bodytemp.AddRange(BHTTotal.data);
                            Bodytemp.AddRange(BLTTotal.data);
                            Bodytemp.AddRange(GATTotal.data);
                            Bodytemp.AddRange(GSTTotal.data);
                            Bodytemp.AddRange(DATTotal.data);
                            Bodytemp.AddRange(DOTTotal.data);
                            Bodytemp.AddRange(DNTTotal.data);
                            Bodytemp.AddRange(DGTTotal.data);
                            Bodytemp.AddRange(ZATTotal.data);
                            Bodytemp.AddRange(ORTTotal.data);
                            Bodytemp.AddRange(UVTTotal.data);
                            Bodytemp.AddRange(UMTTotal.data);
                            Bodytemp.AddRange(SBTTotal.data);
                            Bodytemp.AddRange(SETTotal.data);
                            Bodytemp.AddRange(TUTTotal.data);
                            Bodytemp.AddRange(USTTotal.data);
                            Bodytemp.AddRange(HOTTotal.data);
                            Bodytemp.AddRange(HUTTotal.data);
                            Bodytemp.AddRange(HETTotal.data);

                            total.AddRange(Bodytemp);

                            response.data = total;
                        }

                    }

                    else
                    {
                        response.data = body;
                    }
                }

                response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }

        public CM2ListResponse Cm2TypesList(List<CM2> request, int dataType)
        {
            CM2ListResponse response = new CM2ListResponse();
            var typ = typeof(CM2);
            List<CM2> requestData = new List<CM2>();
            requestData = request;

            if (requestData.Count > 0)
            {
                List<CM2> type1 = new List<CM2>();
                List<CM2> type2 = new List<CM2>();
                List<CM2> type3 = new List<CM2>();

                List<CM2> templist1 = new List<CM2>();
                List<CM2> templist2 = new List<CM2>();
                List<CM2> templist3 = new List<CM2>();

                type1 = requestData.FindAll(a => a.DECISION_TYPE.Equals("ТӨЛБӨРИЙН АКТ"));
                type2 = requestData.FindAll(a => a.DECISION_TYPE.Equals("АЛБАН ШААРДЛАГА"));
                type3 = requestData.FindAll(a => a.DECISION_TYPE.Equals("ЗӨВЛӨМЖ"));

                List<CM2> temp = new List<CM2>();

                CM2 title = new CM2();

                
                if (dataType == 1)
                    title.DECISION_TYPE = "Нэг. Төрийн аудитын байгууллага";
                if (dataType == 2)
                    title.DECISION_TYPE = "Хоёр. Хараат бус аудитын компани";
                title.DEPARTMENT_NAME = requestData.FirstOrDefault().DEPARTMENT_NAME;

                if (type1.Count > 0)
                {

                    CM2 DECNiit = new CM2();
                    var DECdepname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var DEPdepname = typ.GetProperty("DEPARTMENT_NAME");
                    var DECISION_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var DECpay = typ.GetProperty("C1_AMOUNT");
                    var DECpay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var DECpay2 = typ.GetProperty("PREV_AMOUNT");
                    var DECpay3 = typ.GetProperty("CY_AMOUNT");
                    var DECpay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var DECpay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var DECpay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var DECpay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var DECpay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var DECpay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var DECpay10 = typ.GetProperty("C2_AMOUNT");
                    var DECpay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var DECpay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var DECcount = typ.GetProperty("C1_COUNT");
                    var DECcount1 = typ.GetProperty("CURRENT_COUNT");
                    var DECcount2 = typ.GetProperty("PREV_COUNT");
                    var DECcount3 = typ.GetProperty("CY_COUNT");
                    var DECcount4 = typ.GetProperty("TOTAL_COUNT");
                    var DECcount5 = typ.GetProperty("COMP_STATE_COUNT");
                    var DECcount6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var DECcount7 = typ.GetProperty("COMP_ORG_COUNT");
                    var DECcount8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var DECcount9 = typ.GetProperty("STATISTIC_COUNT");
                    var DECcount10 = typ.GetProperty("C2_COUNT");
                    var DECcount11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var DECcount12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal DECAMOUNT = 0;
                    decimal DECAMOUNT1 = 0;
                    decimal DECAMOUNT2 = 0;
                    decimal DECAMOUNT3 = 0;
                    decimal DECAMOUNT4 = 0;
                    decimal DECAMOUNT5 = 0;
                    decimal DECAMOUNT6 = 0;
                    decimal DECAMOUNT7 = 0;
                    decimal DECAMOUNT8 = 0;
                    decimal DECAMOUNT9 = 0;
                    decimal DECAMOUNT10 = 0;
                    decimal DECAMOUNT11 = 0;
                    decimal DECAMOUNT12 = 0;

                    int DECNUMBER = 0;
                    int DECNUMBER1 = 0;
                    int DECNUMBER2 = 0;
                    int DECNUMBER3 = 0;
                    int DECNUMBER4 = 0;
                    int DECNUMBER5 = 0;
                    int DECNUMBER6 = 0;
                    int DECNUMBER7 = 0;
                    int DECNUMBER8 = 0;
                    int DECNUMBER9 = 0;
                    int DECNUMBER10 = 0;
                    int DECNUMBER11 = 0;
                    int DECNUMBER12 = 0;


                    foreach (CM2 data in type1)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            DECNUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            DECNUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            DECNUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            DECNUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            DECNUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            DECNUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            DECNUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            DECNUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            DECNUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            DECNUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            DECNUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            DECNUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            DECNUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        DEPdepname.SetValue(DECNiit, data.DEPARTMENT_NAME);
                    }
                    DECpay.SetValue(DECNiit, DECAMOUNT.ToString("#,0.00"));
                    DECpay1.SetValue(DECNiit, DECAMOUNT1.ToString("#,0.00"));
                    DECpay2.SetValue(DECNiit, DECAMOUNT2.ToString("#,0.00"));
                    DECpay3.SetValue(DECNiit, DECAMOUNT3.ToString("#,0.00"));
                    DECpay4.SetValue(DECNiit, DECAMOUNT4.ToString("#,0.00"));
                    DECpay5.SetValue(DECNiit, DECAMOUNT5.ToString("#,0.00"));
                    DECpay6.SetValue(DECNiit, DECAMOUNT6.ToString("#,0.00"));
                    DECpay7.SetValue(DECNiit, DECAMOUNT7.ToString("#,0.00"));
                    DECpay8.SetValue(DECNiit, DECAMOUNT8.ToString("#,0.00"));
                    DECpay9.SetValue(DECNiit, DECAMOUNT9.ToString("#,0.00"));
                    DECpay10.SetValue(DECNiit, DECAMOUNT10.ToString("#,0.00"));
                    DECpay11.SetValue(DECNiit, DECAMOUNT11.ToString("#,0.00"));
                    DECpay12.SetValue(DECNiit, DECAMOUNT12.ToString("#,0.00"));

                    DECcount.SetValue(DECNiit, DECNUMBER);
                    DECcount1.SetValue(DECNiit, DECNUMBER1);
                    DECcount2.SetValue(DECNiit, DECNUMBER2);
                    DECcount3.SetValue(DECNiit, DECNUMBER3);
                    DECcount4.SetValue(DECNiit, DECNUMBER4);
                    DECcount5.SetValue(DECNiit, DECNUMBER5);
                    DECcount6.SetValue(DECNiit, DECNUMBER6);
                    DECcount7.SetValue(DECNiit, DECNUMBER7);
                    DECcount8.SetValue(DECNiit, DECNUMBER8);
                    DECcount9.SetValue(DECNiit, DECNUMBER9);
                    DECcount10.SetValue(DECNiit, DECNUMBER10);
                    DECcount11.SetValue(DECNiit, DECNUMBER11);
                    DECcount12.SetValue(DECNiit, DECNUMBER12);

                    DECdepname.SetValue(DECNiit, "Дүн");

                    DECISION_TYPEdepname.SetValue(DECNiit, "ТӨЛБӨРИЙН АКТ");

                    templist1.AddRange(type1.OrderBy(m => m.DECISION_TYPE));
                    templist1.Add(DECNiit);
                    type1 = templist1;
                }
                if (type2.Count > 0)
                {

                    CM2 DECNiit2 = new CM2();
                    var DECdepname2 = typ.GetProperty("BUDGET_TYPE_NAME");
                    var DEPdepname2 = typ.GetProperty("DEPARTMENT_NAME");
                    var DECISION_TYPEdepname2 = typ.GetProperty("DECISION_TYPE");

                    var DECType2pay = typ.GetProperty("C1_AMOUNT");
                    var DECType2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var DECType2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var DECType2pay3 = typ.GetProperty("CY_AMOUNT");
                    var DECType2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var DECType2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var DECType2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var DECType2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var DECType2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var DECType2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var DECType2pay10 = typ.GetProperty("C2_AMOUNT");
                    var DECType2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var DECType2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var DECType2count = typ.GetProperty("C1_COUNT");
                    var DECType2count1 = typ.GetProperty("CURRENT_COUNT");
                    var DECType2count2 = typ.GetProperty("PREV_COUNT");
                    var DECType2count3 = typ.GetProperty("CY_COUNT");
                    var DECType2count4 = typ.GetProperty("TOTAL_COUNT");
                    var DECType2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var DECType2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var DECType2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var DECType2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var DECType2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var DECType2count10 = typ.GetProperty("C2_COUNT");
                    var DECType2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var DECType2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal DECTYPE2AMOUNT = 0;
                    decimal DECTYPE2AMOUNT1 = 0;
                    decimal DECTYPE2AMOUNT2 = 0;
                    decimal DECTYPE2AMOUNT3 = 0;
                    decimal DECTYPE2AMOUNT4 = 0;
                    decimal DECTYPE2AMOUNT5 = 0;
                    decimal DECTYPE2AMOUNT6 = 0;
                    decimal DECTYPE2AMOUNT7 = 0;
                    decimal DECTYPE2AMOUNT8 = 0;
                    decimal DECTYPE2AMOUNT9 = 0;
                    decimal DECTYPE2AMOUNT10 = 0;
                    decimal DECTYPE2AMOUNT11 = 0;
                    decimal DECTYPE2AMOUNT12 = 0;

                    int DECTYPE2NUMBER = 0;
                    int DECTYPE2NUMBER1 = 0;
                    int DECTYPE2NUMBER2 = 0;
                    int DECTYPE2NUMBER3 = 0;
                    int DECTYPE2NUMBER4 = 0;
                    int DECTYPE2NUMBER5 = 0;
                    int DECTYPE2NUMBER6 = 0;
                    int DECTYPE2NUMBER7 = 0;
                    int DECTYPE2NUMBER8 = 0;
                    int DECTYPE2NUMBER9 = 0;
                    int DECTYPE2NUMBER10 = 0;
                    int DECTYPE2NUMBER11 = 0;
                    int DECTYPE2NUMBER12 = 0;


                    foreach (CM2 data in type2)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            DECTYPE2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            DECTYPE2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            DECTYPE2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            DECTYPE2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            DECTYPE2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            DECTYPE2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            DECTYPE2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            DECTYPE2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            DECTYPE2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            DECTYPE2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            DECTYPE2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            DECTYPE2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            DECTYPE2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        DEPdepname2.SetValue(DECNiit2, data.DEPARTMENT_NAME);
                    }
                    DECType2pay.SetValue(DECNiit2, DECTYPE2AMOUNT.ToString("#,0.00"));
                    DECType2pay1.SetValue(DECNiit2, DECTYPE2AMOUNT1.ToString("#,0.00"));
                    DECType2pay2.SetValue(DECNiit2, DECTYPE2AMOUNT2.ToString("#,0.00"));
                    DECType2pay3.SetValue(DECNiit2, DECTYPE2AMOUNT3.ToString("#,0.00"));
                    DECType2pay4.SetValue(DECNiit2, DECTYPE2AMOUNT4.ToString("#,0.00"));
                    DECType2pay5.SetValue(DECNiit2, DECTYPE2AMOUNT5.ToString("#,0.00"));
                    DECType2pay6.SetValue(DECNiit2, DECTYPE2AMOUNT6.ToString("#,0.00"));
                    DECType2pay7.SetValue(DECNiit2, DECTYPE2AMOUNT7.ToString("#,0.00"));
                    DECType2pay8.SetValue(DECNiit2, DECTYPE2AMOUNT8.ToString("#,0.00"));
                    DECType2pay9.SetValue(DECNiit2, DECTYPE2AMOUNT9.ToString("#,0.00"));
                    DECType2pay10.SetValue(DECNiit2, DECTYPE2AMOUNT10.ToString("#,0.00"));
                    DECType2pay11.SetValue(DECNiit2, DECTYPE2AMOUNT11.ToString("#,0.00"));
                    DECType2pay12.SetValue(DECNiit2, DECTYPE2AMOUNT12.ToString("#,0.00" +
                        ""));

                    DECType2count.SetValue(DECNiit2, DECTYPE2NUMBER);
                    DECType2count1.SetValue(DECNiit2, DECTYPE2NUMBER1);
                    DECType2count2.SetValue(DECNiit2, DECTYPE2NUMBER2);
                    DECType2count3.SetValue(DECNiit2, DECTYPE2NUMBER3);
                    DECType2count4.SetValue(DECNiit2, DECTYPE2NUMBER4);
                    DECType2count5.SetValue(DECNiit2, DECTYPE2NUMBER5);
                    DECType2count6.SetValue(DECNiit2, DECTYPE2NUMBER6);
                    DECType2count7.SetValue(DECNiit2, DECTYPE2NUMBER7);
                    DECType2count8.SetValue(DECNiit2, DECTYPE2NUMBER8);
                    DECType2count9.SetValue(DECNiit2, DECTYPE2NUMBER9);
                    DECType2count10.SetValue(DECNiit2, DECTYPE2NUMBER10);
                    DECType2count11.SetValue(DECNiit2, DECTYPE2NUMBER11);
                    DECType2count12.SetValue(DECNiit2, DECTYPE2NUMBER12);

                    DECdepname2.SetValue(DECNiit2, "Дүн");
                    DECISION_TYPEdepname2.SetValue(DECNiit2, "АЛБАН ШААРДЛАГА");

                    templist2.AddRange(type2.OrderBy(m => m.DECISION_TYPE));
                    templist2.Add(DECNiit2);
                    type2 = templist2;
                }
                if (type3.Count > 0)
                {

                    CM2 DECNiit3 = new CM2();
                    var DECdepname3 = typ.GetProperty("BUDGET_TYPE_NAME");
                    var DEPdepname3 = typ.GetProperty("DEPARTMENT_NAME");
                    var DECISION_TYPEdepname3 = typ.GetProperty("DECISION_TYPE");

                    var DECType3pay = typ.GetProperty("C1_AMOUNT");
                    var DECType3pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var DECType3pay2 = typ.GetProperty("PREV_AMOUNT");
                    var DECType3pay3 = typ.GetProperty("CY_AMOUNT");
                    var DECType3pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var DECType3pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var DECType3pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var DECType3pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var DECType3pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var DECType3pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var DECType3pay10 = typ.GetProperty("C2_AMOUNT");
                    var DECType3pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var DECType3pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var DECType3count = typ.GetProperty("C1_COUNT");
                    var DECType3count1 = typ.GetProperty("CURRENT_COUNT");
                    var DECType3count2 = typ.GetProperty("PREV_COUNT");
                    var DECType3count3 = typ.GetProperty("CY_COUNT");
                    var DECType3count4 = typ.GetProperty("TOTAL_COUNT");
                    var DECType3count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var DECType3count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var DECType3count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var DECType3count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var DECType3count9 = typ.GetProperty("STATISTIC_COUNT");
                    var DECType3count10 = typ.GetProperty("C2_COUNT");
                    var DECType3count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var DECType3count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal DECTYPE3AMOUNT = 0;
                    decimal DECTYPE3AMOUNT1 = 0;
                    decimal DECTYPE3AMOUNT2 = 0;
                    decimal DECTYPE3AMOUNT3 = 0;
                    decimal DECTYPE3AMOUNT4 = 0;
                    decimal DECTYPE3AMOUNT5 = 0;
                    decimal DECTYPE3AMOUNT6 = 0;
                    decimal DECTYPE3AMOUNT7 = 0;
                    decimal DECTYPE3AMOUNT8 = 0;
                    decimal DECTYPE3AMOUNT9 = 0;
                    decimal DECTYPE3AMOUNT10 = 0;
                    decimal DECTYPE3AMOUNT11 = 0;
                    decimal DECTYPE3AMOUNT12 = 0;

                    int DECTYPE3NUMBER = 0;
                    int DECTYPE3NUMBER1 = 0;
                    int DECTYPE3NUMBER2 = 0;
                    int DECTYPE3NUMBER3 = 0;
                    int DECTYPE3NUMBER4 = 0;
                    int DECTYPE3NUMBER5 = 0;
                    int DECTYPE3NUMBER6 = 0;
                    int DECTYPE3NUMBER7 = 0;
                    int DECTYPE3NUMBER8 = 0;
                    int DECTYPE3NUMBER9 = 0;
                    int DECTYPE3NUMBER10 = 0;
                    int DECTYPE3NUMBER11 = 0;
                    int DECTYPE3NUMBER12 = 0;


                    foreach (CM2 data in type3)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECTYPE3AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            DECTYPE3NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            DECTYPE3NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            DECTYPE3NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            DECTYPE3NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            DECTYPE3NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            DECTYPE3NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            DECTYPE3NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            DECTYPE3NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            DECTYPE3NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            DECTYPE3NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            DECTYPE3NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            DECTYPE3NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            DECTYPE3NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        DEPdepname3.SetValue(DECNiit3, data.DEPARTMENT_NAME);
                    }
                    DECType3pay.SetValue(DECNiit3, DECTYPE3AMOUNT.ToString("#,0.00"));
                    DECType3pay1.SetValue(DECNiit3, DECTYPE3AMOUNT1.ToString("#,0.00"));
                    DECType3pay2.SetValue(DECNiit3, DECTYPE3AMOUNT2.ToString("#,0.00"));
                    DECType3pay3.SetValue(DECNiit3, DECTYPE3AMOUNT3.ToString("#,0.00"));
                    DECType3pay4.SetValue(DECNiit3, DECTYPE3AMOUNT4.ToString("#,0.00"));
                    DECType3pay5.SetValue(DECNiit3, DECTYPE3AMOUNT5.ToString("#,0.00"));
                    DECType3pay6.SetValue(DECNiit3, DECTYPE3AMOUNT6.ToString("#,0.00"));
                    DECType3pay7.SetValue(DECNiit3, DECTYPE3AMOUNT7.ToString("#,0.00"));
                    DECType3pay8.SetValue(DECNiit3, DECTYPE3AMOUNT8.ToString("#,0.00"));
                    DECType3pay9.SetValue(DECNiit3, DECTYPE3AMOUNT9.ToString("#,0.00"));
                    DECType3pay10.SetValue(DECNiit3, DECTYPE3AMOUNT10.ToString("#,0.00"));
                    DECType3pay11.SetValue(DECNiit3, DECTYPE3AMOUNT11.ToString("#,0.00"));
                    DECType3pay12.SetValue(DECNiit3, DECTYPE3AMOUNT12.ToString("#,0.00"));

                    DECType3count.SetValue(DECNiit3, DECTYPE3NUMBER);
                    DECType3count1.SetValue(DECNiit3, DECTYPE3NUMBER1);
                    DECType3count2.SetValue(DECNiit3, DECTYPE3NUMBER2);
                    DECType3count3.SetValue(DECNiit3, DECTYPE3NUMBER3);
                    DECType3count4.SetValue(DECNiit3, DECTYPE3NUMBER4);
                    DECType3count5.SetValue(DECNiit3, DECTYPE3NUMBER5);
                    DECType3count6.SetValue(DECNiit3, DECTYPE3NUMBER6);
                    DECType3count7.SetValue(DECNiit3, DECTYPE3NUMBER7);
                    DECType3count8.SetValue(DECNiit3, DECTYPE3NUMBER8);
                    DECType3count9.SetValue(DECNiit3, DECTYPE3NUMBER9);
                    DECType3count10.SetValue(DECNiit3, DECTYPE3NUMBER10);
                    DECType3count11.SetValue(DECNiit3, DECTYPE3NUMBER11);
                    DECType3count12.SetValue(DECNiit3, DECTYPE3NUMBER12);

                    DECdepname3.SetValue(DECNiit3, "Дүн");
                    DECISION_TYPEdepname3.SetValue(DECNiit3, "ЗӨВЛӨМЖ");

                    templist3.AddRange(type3.OrderBy(m => m.DECISION_TYPE));
                    templist3.Add(DECNiit3);
                    type3 = templist3;
                }

                if (dataType != 0)
                    temp.Add(title);

                temp.AddRange(type1);
                temp.AddRange(type2);
                temp.AddRange(type3);
                requestData = temp;
            }

            response.data = requestData;
            return response;
        }
        public CM2ListResponse Cm2TotalList(List<CM2> request)
        {
            CM2ListResponse response = new CM2ListResponse();
            var typ = typeof(CM2);
            List<CM2> requestData = new List<CM2>();
            requestData = request;
            if (requestData.Count > 0)
            {

                List<CM2> totalCompanytype1 = new List<CM2>();
                List<CM2> totalCompanytype2 = new List<CM2>();
                List<CM2> totalCompanytype3 = new List<CM2>();

                List<CM2> totalCompanytemplist1 = new List<CM2>();
                List<CM2> totalCompanytemplist2 = new List<CM2>();
                List<CM2> totalCompanytemplist3 = new List<CM2>();

                List<CM2> BudgetTypes1 = new List<CM2>();
                List<CM2> BudgetTypes2 = new List<CM2>();
                List<CM2> BudgetTypes3 = new List<CM2>();


                List<CM2> BUDGET1totalCompanytype1 = new List<CM2>();
                List<CM2> BUDGET2totalCompanytype1 = new List<CM2>();
                List<CM2> BUDGET3totalCompanytype1 = new List<CM2>();
                List<CM2> BUDGET4totalCompanytype1 = new List<CM2>();
                List<CM2> BUDGET5totalCompanytype1 = new List<CM2>();

                List<CM2> BUDGET1totalCompanytype2 = new List<CM2>();
                List<CM2> BUDGET2totalCompanytype2 = new List<CM2>();
                List<CM2> BUDGET3totalCompanytype2 = new List<CM2>();
                List<CM2> BUDGET4totalCompanytype2 = new List<CM2>();
                List<CM2> BUDGET5totalCompanytype2 = new List<CM2>();

                List<CM2> BUDGET1totalCompanytype3 = new List<CM2>();
                List<CM2> BUDGET2totalCompanytype3 = new List<CM2>();
                List<CM2> BUDGET3totalCompanytype3 = new List<CM2>();
                List<CM2> BUDGET4totalCompanytype3 = new List<CM2>();
                List<CM2> BUDGET5totalCompanytype3 = new List<CM2>();

                List<CM2> BudgetTypes1totaltemp = new List<CM2>();
                List<CM2> BudgetTypes2totaltemp = new List<CM2>();
                List<CM2> BudgetTypes3totaltemp = new List<CM2>();

                List<CM2> totaltemp = new List<CM2>();
                CM2 title = new CM2();

                totalCompanytype1 = requestData.FindAll(a => a.DECISION_TYPE.Equals("ТӨЛБӨРИЙН АКТ"));

                BUDGET1totalCompanytype1 = totalCompanytype1.FindAll(a => a.BUDGET_TYPE.Equals("1"));
                BUDGET2totalCompanytype1 = totalCompanytype1.FindAll(a => a.BUDGET_TYPE.Equals("2"));
                BUDGET3totalCompanytype1 = totalCompanytype1.FindAll(a => a.BUDGET_TYPE.Equals("3"));
                BUDGET4totalCompanytype1 = totalCompanytype1.FindAll(a => a.BUDGET_TYPE.Equals("4"));
                BUDGET5totalCompanytype1 = totalCompanytype1.FindAll(a => a.BUDGET_TYPE.Equals("5"));

                totalCompanytype2 = requestData.FindAll(a => a.DECISION_TYPE.Equals("АЛБАН ШААРДЛАГА"));

                BUDGET1totalCompanytype2 = totalCompanytype2.FindAll(a => a.BUDGET_TYPE.Equals("1"));
                BUDGET2totalCompanytype2 = totalCompanytype2.FindAll(a => a.BUDGET_TYPE.Equals("2"));
                BUDGET3totalCompanytype2 = totalCompanytype2.FindAll(a => a.BUDGET_TYPE.Equals("3"));
                BUDGET4totalCompanytype2 = totalCompanytype2.FindAll(a => a.BUDGET_TYPE.Equals("4"));
                BUDGET5totalCompanytype2 = totalCompanytype2.FindAll(a => a.BUDGET_TYPE.Equals("5"));

                totalCompanytype3 = requestData.FindAll(a => a.DECISION_TYPE.Equals("ЗӨВЛӨМЖ"));

                BUDGET1totalCompanytype3 = totalCompanytype3.FindAll(a => a.BUDGET_TYPE.Equals("1"));
                BUDGET2totalCompanytype3 = totalCompanytype3.FindAll(a => a.BUDGET_TYPE.Equals("2"));
                BUDGET3totalCompanytype3 = totalCompanytype3.FindAll(a => a.BUDGET_TYPE.Equals("3"));
                BUDGET4totalCompanytype3 = totalCompanytype3.FindAll(a => a.BUDGET_TYPE.Equals("4"));
                BUDGET5totalCompanytype3 = totalCompanytype3.FindAll(a => a.BUDGET_TYPE.Equals("5"));

                
                title.DECISION_TYPE = "Нийт дүн";
                title.DEPARTMENT_NAME = requestData.FirstOrDefault().DEPARTMENT_NAME;

                if (BUDGET1totalCompanytype1.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET1totalCompanytype1)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00" +
                        "" +
                        ""));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ТШЗ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "ТӨЛБӨРИЙН АКТ");
                    BUDGET1totalCompanytype1 = new List<CM2>();

                    BUDGET1totalCompanytype1.Add(totalDEC2Niit);
                }
                if (BUDGET2totalCompanytype1.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET2totalCompanytype1)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ТTЗ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "ТӨЛБӨРИЙН АКТ");
                    BUDGET2totalCompanytype1 = new List<CM2>();

                    BUDGET2totalCompanytype1.Add(totalDEC2Niit);
                }
                if (BUDGET3totalCompanytype1.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET3totalCompanytype1)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ТЕЗ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "ТӨЛБӨРИЙН АКТ");
                    BUDGET3totalCompanytype1 = new List<CM2>();

                    BUDGET3totalCompanytype1.Add(totalDEC2Niit);
                }
                if (BUDGET4totalCompanytype1.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET4totalCompanytype1)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ТБОНӨХЭ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "ТӨЛБӨРИЙН АКТ");
                    BUDGET4totalCompanytype1 = new List<CM2>();

                    BUDGET4totalCompanytype1.Add(totalDEC2Niit);
                }
                if (BUDGET5totalCompanytype1.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET5totalCompanytype1)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ЗГСНТ, НТГТ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "ТӨЛБӨРИЙН АКТ");
                    BUDGET5totalCompanytype1 = new List<CM2>();

                    BUDGET5totalCompanytype1.Add(totalDEC2Niit);
                }

                BudgetTypes1totaltemp.AddRange(BUDGET1totalCompanytype1);
                BudgetTypes1totaltemp.AddRange(BUDGET2totalCompanytype1);
                BudgetTypes1totaltemp.AddRange(BUDGET3totalCompanytype1);
                BudgetTypes1totaltemp.AddRange(BUDGET4totalCompanytype1);
                BudgetTypes1totaltemp.AddRange(BUDGET5totalCompanytype1);
                BudgetTypes1 = BudgetTypes1totaltemp;

                if (BUDGET1totalCompanytype2.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET1totalCompanytype2)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ТШЗ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "АЛБАН ШААРДЛАГА");
                    BUDGET1totalCompanytype2 = new List<CM2>();

                    BUDGET1totalCompanytype2.Add(totalDEC2Niit);
                }
                if (BUDGET2totalCompanytype2.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET2totalCompanytype2)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ТTЗ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "АЛБАН ШААРДЛАГА");
                    BUDGET2totalCompanytype2 = new List<CM2>();

                    BUDGET2totalCompanytype2.Add(totalDEC2Niit);
                }
                if (BUDGET3totalCompanytype2.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET3totalCompanytype2)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ТЕЗ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "АЛБАН ШААРДЛАГА");
                    BUDGET3totalCompanytype2 = new List<CM2>();

                    BUDGET3totalCompanytype2.Add(totalDEC2Niit);
                }
                if (BUDGET4totalCompanytype2.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET4totalCompanytype2)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ТБОНӨХЭ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "АЛБАН ШААРДЛАГА");
                    BUDGET4totalCompanytype2 = new List<CM2>();

                    BUDGET4totalCompanytype2.Add(totalDEC2Niit);
                }
                if (BUDGET5totalCompanytype2.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET5totalCompanytype2)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ЗГСНТ, НТГТ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "АЛБАН ШААРДЛАГА");
                    BUDGET5totalCompanytype2 = new List<CM2>();

                    BUDGET5totalCompanytype2.Add(totalDEC2Niit);
                }

                BudgetTypes2totaltemp.AddRange(BUDGET1totalCompanytype2);
                BudgetTypes2totaltemp.AddRange(BUDGET2totalCompanytype2);
                BudgetTypes2totaltemp.AddRange(BUDGET3totalCompanytype2);
                BudgetTypes2totaltemp.AddRange(BUDGET4totalCompanytype2);
                BudgetTypes2totaltemp.AddRange(BUDGET5totalCompanytype2);
                BudgetTypes2 = BudgetTypes2totaltemp;

                if (BUDGET1totalCompanytype3.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET1totalCompanytype3)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ТШЗ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "ЗӨВЛӨМЖ");
                    BUDGET1totalCompanytype3 = new List<CM2>();

                    BUDGET1totalCompanytype3.Add(totalDEC2Niit);
                }
                if (BUDGET2totalCompanytype3.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET2totalCompanytype3)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ТTЗ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "ЗӨВЛӨМЖ");
                    BUDGET2totalCompanytype3 = new List<CM2>();

                    BUDGET2totalCompanytype3.Add(totalDEC2Niit);
                }
                if (BUDGET3totalCompanytype3.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET3totalCompanytype3)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ТЕЗ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "ЗӨВЛӨМЖ");
                    BUDGET3totalCompanytype3 = new List<CM2>();

                    BUDGET3totalCompanytype3.Add(totalDEC2Niit);
                }
                if (BUDGET4totalCompanytype3.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET4totalCompanytype3)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ТБОНӨХЭ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "ЗӨВЛӨМЖ");
                    BUDGET4totalCompanytype3 = new List<CM2>();

                    BUDGET4totalCompanytype3.Add(totalDEC2Niit);
                }
                if (BUDGET5totalCompanytype3.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BUDGET5totalCompanytype3)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "ЗГСНТ, НТГТ");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "ЗӨВЛӨМЖ");
                    BUDGET5totalCompanytype3 = new List<CM2>();

                    BUDGET5totalCompanytype3.Add(totalDEC2Niit);
                }

                BudgetTypes3totaltemp.AddRange(BUDGET1totalCompanytype3);
                BudgetTypes3totaltemp.AddRange(BUDGET2totalCompanytype3);
                BudgetTypes3totaltemp.AddRange(BUDGET3totalCompanytype3);
                BudgetTypes3totaltemp.AddRange(BUDGET4totalCompanytype3);
                BudgetTypes3totaltemp.AddRange(BUDGET5totalCompanytype3);
                BudgetTypes3 = BudgetTypes3totaltemp;

                if (BudgetTypes1.Count > 0)
                {

                    CM2 totalDEC2Niit = new CM2();
                    var totalDEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2AMOUNT = 0;
                    decimal totalDEC2AMOUNT1 = 0;
                    decimal totalDEC2AMOUNT2 = 0;
                    decimal totalDEC2AMOUNT3 = 0;
                    decimal totalDEC2AMOUNT4 = 0;
                    decimal totalDEC2AMOUNT5 = 0;
                    decimal totalDEC2AMOUNT6 = 0;
                    decimal totalDEC2AMOUNT7 = 0;
                    decimal totalDEC2AMOUNT8 = 0;
                    decimal totalDEC2AMOUNT9 = 0;
                    decimal totalDEC2AMOUNT10 = 0;
                    decimal totalDEC2AMOUNT11 = 0;
                    decimal totalDEC2AMOUNT12 = 0;

                    int totalDEC2NUMBER = 0;
                    int totalDEC2NUMBER1 = 0;
                    int totalDEC2NUMBER2 = 0;
                    int totalDEC2NUMBER3 = 0;
                    int totalDEC2NUMBER4 = 0;
                    int totalDEC2NUMBER5 = 0;
                    int totalDEC2NUMBER6 = 0;
                    int totalDEC2NUMBER7 = 0;
                    int totalDEC2NUMBER8 = 0;
                    int totalDEC2NUMBER9 = 0;
                    int totalDEC2NUMBER10 = 0;
                    int totalDEC2NUMBER11 = 0;
                    int totalDEC2NUMBER12 = 0;


                    foreach (CM2 data in BudgetTypes1)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname.SetValue(totalDEC2Niit, data.DEPARTMENT_NAME);
                    }
                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.00"));
                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.00"));
                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.00"));
                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.00"));
                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.00"));
                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.00"));
                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.00"));
                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.00"));
                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.00"));
                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.00"));
                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.00"));
                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.00"));
                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.00"));

                    totalDEC2count.SetValue(totalDEC2Niit, totalDEC2NUMBER);
                    totalDEC2count1.SetValue(totalDEC2Niit, totalDEC2NUMBER1);
                    totalDEC2count2.SetValue(totalDEC2Niit, totalDEC2NUMBER2);
                    totalDEC2count3.SetValue(totalDEC2Niit, totalDEC2NUMBER3);
                    totalDEC2count4.SetValue(totalDEC2Niit, totalDEC2NUMBER4);
                    totalDEC2count5.SetValue(totalDEC2Niit, totalDEC2NUMBER5);
                    totalDEC2count6.SetValue(totalDEC2Niit, totalDEC2NUMBER6);
                    totalDEC2count7.SetValue(totalDEC2Niit, totalDEC2NUMBER7);
                    totalDEC2count8.SetValue(totalDEC2Niit, totalDEC2NUMBER8);
                    totalDEC2count9.SetValue(totalDEC2Niit, totalDEC2NUMBER9);
                    totalDEC2count10.SetValue(totalDEC2Niit, totalDEC2NUMBER10);
                    totalDEC2count11.SetValue(totalDEC2Niit, totalDEC2NUMBER11);
                    totalDEC2count12.SetValue(totalDEC2Niit, totalDEC2NUMBER12);

                    totalDEC2depname.SetValue(totalDEC2Niit, "Дүн");
                    totalDECISION2_TYPEdepname.SetValue(totalDEC2Niit, "ТӨЛБӨРИЙН АКТ");

                    totalCompanytemplist1.AddRange(BudgetTypes1.OrderBy(m => m.DECISION_TYPE));
                    totalCompanytemplist1.Add(totalDEC2Niit);
                    BudgetTypes1 = totalCompanytemplist1;
                }
                if (BudgetTypes2.Count > 0)
                {

                    CM2 totalDEC2Niit2 = new CM2();
                    var totalDEC2depname2 = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname2 = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname2 = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2Type2pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2Type2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2Type2pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2Type2pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2Type2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2Type2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2Type2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2Type2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2Type2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2Type2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2Type2pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2Type2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2Type2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2Type2count = typ.GetProperty("C1_COUNT");
                    var totalDEC2Type2count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2Type2count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2Type2count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2Type2count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2Type2count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2Type2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2Type2count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2Type2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2Type2count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2Type2count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2Type2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2Type2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2Type2AMOUNT = 0;
                    decimal totalDEC2Type2AMOUNT1 = 0;
                    decimal totalDEC2Type2AMOUNT2 = 0;
                    decimal totalDEC2Type2AMOUNT3 = 0;
                    decimal totalDEC2Type2AMOUNT4 = 0;
                    decimal totalDEC2Type2AMOUNT5 = 0;
                    decimal totalDEC2Type2AMOUNT6 = 0;
                    decimal totalDEC2Type2AMOUNT7 = 0;
                    decimal totalDEC2Type2AMOUNT8 = 0;
                    decimal totalDEC2Type2AMOUNT9 = 0;
                    decimal totalDEC2Type2AMOUNT10 = 0;
                    decimal totalDEC2Type2AMOUNT11 = 0;
                    decimal totalDEC2Type2AMOUNT12 = 0;

                    int totalDEC2Type2NUMBER = 0;
                    int totalDEC2Type2NUMBER1 = 0;
                    int totalDEC2Type2NUMBER2 = 0;
                    int totalDEC2Type2NUMBER3 = 0;
                    int totalDEC2Type2NUMBER4 = 0;
                    int totalDEC2Type2NUMBER5 = 0;
                    int totalDEC2Type2NUMBER6 = 0;
                    int totalDEC2Type2NUMBER7 = 0;
                    int totalDEC2Type2NUMBER8 = 0;
                    int totalDEC2Type2NUMBER9 = 0;
                    int totalDEC2Type2NUMBER10 = 0;
                    int totalDEC2Type2NUMBER11 = 0;
                    int totalDEC2Type2NUMBER12 = 0;


                    foreach (CM2 data in BudgetTypes2)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type2AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2Type2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname2.SetValue(totalDEC2Niit2, data.DEPARTMENT_NAME);
                    }
                    totalDEC2Type2pay.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT.ToString("#,0.00"));
                    totalDEC2Type2pay1.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT1.ToString("#,0.00"));
                    totalDEC2Type2pay2.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT2.ToString("#,0.00"));
                    totalDEC2Type2pay3.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT3.ToString("#,0.00"));
                    totalDEC2Type2pay4.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT4.ToString("#,0.00"));
                    totalDEC2Type2pay5.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT5.ToString("#,0.00"));
                    totalDEC2Type2pay6.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT6.ToString("#,0.00"));
                    totalDEC2Type2pay7.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT7.ToString("#,0.00"));
                    totalDEC2Type2pay8.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT8.ToString("#,0.00"));
                    totalDEC2Type2pay9.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT9.ToString("#,0.00"));
                    totalDEC2Type2pay10.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT10.ToString("#,0.00"));
                    totalDEC2Type2pay11.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT11.ToString("#,0.00"));
                    totalDEC2Type2pay12.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT12.ToString("#,0.00"));

                    totalDEC2Type2count.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER);
                    totalDEC2Type2count1.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER1);
                    totalDEC2Type2count2.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER2);
                    totalDEC2Type2count3.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER3);
                    totalDEC2Type2count4.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER4);
                    totalDEC2Type2count5.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER5);
                    totalDEC2Type2count6.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER6);
                    totalDEC2Type2count7.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER7);
                    totalDEC2Type2count8.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER8);
                    totalDEC2Type2count9.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER9);
                    totalDEC2Type2count10.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER10);
                    totalDEC2Type2count11.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER11);
                    totalDEC2Type2count12.SetValue(totalDEC2Niit2, totalDEC2Type2NUMBER12);

                    totalDEC2depname2.SetValue(totalDEC2Niit2, "Дүн");
                    totalDECISION2_TYPEdepname2.SetValue(totalDEC2Niit2, "АЛБАН ШААРДЛАГА");

                    totalCompanytemplist2.AddRange(BudgetTypes2.OrderBy(m => m.DECISION_TYPE));
                    totalCompanytemplist2.Add(totalDEC2Niit2);
                    BudgetTypes2 = totalCompanytemplist2;
                }
                if (BudgetTypes3.Count > 0)
                {

                    CM2 totalDEC2Niit3 = new CM2();
                    var totalDEC2depname3 = typ.GetProperty("BUDGET_TYPE_NAME");
                    var totalDEP2depname3 = typ.GetProperty("DEPARTMENT_NAME");
                    var totalDECISION2_TYPEdepname3 = typ.GetProperty("DECISION_TYPE");

                    var totalDEC2Type3pay = typ.GetProperty("C1_AMOUNT");
                    var totalDEC2Type3pay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var totalDEC2Type3pay2 = typ.GetProperty("PREV_AMOUNT");
                    var totalDEC2Type3pay3 = typ.GetProperty("CY_AMOUNT");
                    var totalDEC2Type3pay4 = typ.GetProperty("TOTAL_AMOUNT");
                    var totalDEC2Type3pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                    var totalDEC2Type3pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                    var totalDEC2Type3pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                    var totalDEC2Type3pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                    var totalDEC2Type3pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                    var totalDEC2Type3pay10 = typ.GetProperty("C2_AMOUNT");
                    var totalDEC2Type3pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                    var totalDEC2Type3pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                    var totalDEC2Type3count = typ.GetProperty("C1_COUNT");
                    var totalDEC2Type3count1 = typ.GetProperty("CURRENT_COUNT");
                    var totalDEC2Type3count2 = typ.GetProperty("PREV_COUNT");
                    var totalDEC2Type3count3 = typ.GetProperty("CY_COUNT");
                    var totalDEC2Type3count4 = typ.GetProperty("TOTAL_COUNT");
                    var totalDEC2Type3count5 = typ.GetProperty("COMP_STATE_COUNT");
                    var totalDEC2Type3count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                    var totalDEC2Type3count7 = typ.GetProperty("COMP_ORG_COUNT");
                    var totalDEC2Type3count8 = typ.GetProperty("COMP_OTHER_COUNT");
                    var totalDEC2Type3count9 = typ.GetProperty("STATISTIC_COUNT");
                    var totalDEC2Type3count10 = typ.GetProperty("C2_COUNT");
                    var totalDEC2Type3count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                    var totalDEC2Type3count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                    decimal totalDEC2Type3AMOUNT = 0;
                    decimal totalDEC2Type3AMOUNT1 = 0;
                    decimal totalDEC2Type3AMOUNT2 = 0;
                    decimal totalDEC2Type3AMOUNT3 = 0;
                    decimal totalDEC2Type3AMOUNT4 = 0;
                    decimal totalDEC2Type3AMOUNT5 = 0;
                    decimal totalDEC2Type3AMOUNT6 = 0;
                    decimal totalDEC2Type3AMOUNT7 = 0;
                    decimal totalDEC2Type3AMOUNT8 = 0;
                    decimal totalDEC2Type3AMOUNT9 = 0;
                    decimal totalDEC2Type3AMOUNT10 = 0;
                    decimal totalDEC2Type3AMOUNT11 = 0;
                    decimal totalDEC2Type3AMOUNT12 = 0;

                    int totalDEC2Type3NUMBER = 0;
                    int totalDEC2Type3NUMBER1 = 0;
                    int totalDEC2Type3NUMBER2 = 0;
                    int totalDEC2Type3NUMBER3 = 0;
                    int totalDEC2Type3NUMBER4 = 0;
                    int totalDEC2Type3NUMBER5 = 0;
                    int totalDEC2Type3NUMBER6 = 0;
                    int totalDEC2Type3NUMBER7 = 0;
                    int totalDEC2Type3NUMBER8 = 0;
                    int totalDEC2Type3NUMBER9 = 0;
                    int totalDEC2Type3NUMBER10 = 0;
                    int totalDEC2Type3NUMBER11 = 0;
                    int totalDEC2Type3NUMBER12 = 0;


                    foreach (CM2 data in BudgetTypes3)
                    {
                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT1 += Amount;

                        }
                        if (data.PREV_AMOUNT != null)
                        {
                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT2 += Amount;

                        }
                        if (data.CY_AMOUNT != null)
                        {
                            string strNii = data.CY_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT3 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT4 += Amount;

                        }
                        if (data.COMP_STATE_AMOUNT != null)
                        {
                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT5 += Amount;

                        }
                        if (data.COMP_LOCAL_AMOUNT != null)
                        {
                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT6 += Amount;

                        }
                        if (data.COMP_ORG_AMOUNT != null)
                        {
                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT7 += Amount;

                        }
                        if (data.COMP_OTHER_AMOUNT != null)
                        {
                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT8 += Amount;

                        }
                        if (data.STATISTIC_AMOUNT != null)
                        {
                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT9 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT10 += Amount;

                        }
                        if (data.C2_NONEXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT11 += Amount;

                        }
                        if (data.C2_EXPIRED_AMOUNT != null)
                        {
                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            totalDEC2Type3AMOUNT12 += Amount;

                        }



                        if (data.C1_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.PREV_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                        }
                        if (data.CY_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMP_STATE_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                        }
                        if (data.COMP_LOCAL_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                        }
                        if (data.COMP_ORG_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                        }
                        if (data.COMP_OTHER_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                        }
                        if (data.STATISTIC_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                        }
                        if (data.C2_NONEXPIRED_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                        }
                        if (data.C2_EXPIRED_COUNT != 0)
                        {
                            totalDEC2Type3NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                        }
                        totalDEP2depname3.SetValue(totalDEC2Niit3, data.DEPARTMENT_NAME);
                    }
                    totalDEC2Type3pay.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT.ToString("#,0.00"));
                    totalDEC2Type3pay1.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT1.ToString("#,0.00"));
                    totalDEC2Type3pay2.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT2.ToString("#,0.00"));
                    totalDEC2Type3pay3.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT3.ToString("#,0.00"));
                    totalDEC2Type3pay4.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT4.ToString("#,0.00"));
                    totalDEC2Type3pay5.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT5.ToString("#,0.00"));
                    totalDEC2Type3pay6.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT6.ToString("#,0.00"));
                    totalDEC2Type3pay7.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT7.ToString("#,0.00"));
                    totalDEC2Type3pay8.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT8.ToString("#,0.00"));
                    totalDEC2Type3pay9.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT9.ToString("#,0.00"));
                    totalDEC2Type3pay10.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT10.ToString("#,0.00"));
                    totalDEC2Type3pay11.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT11.ToString("#,0.00"));
                    totalDEC2Type3pay12.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT12.ToString("#,0.00"));

                    totalDEC2Type3count.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER);
                    totalDEC2Type3count1.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER1);
                    totalDEC2Type3count2.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER2);
                    totalDEC2Type3count3.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER3);
                    totalDEC2Type3count4.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER4);
                    totalDEC2Type3count5.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER5);
                    totalDEC2Type3count6.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER6);
                    totalDEC2Type3count7.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER7);
                    totalDEC2Type3count8.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER8);
                    totalDEC2Type3count9.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER9);
                    totalDEC2Type3count10.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER10);
                    totalDEC2Type3count11.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER11);
                    totalDEC2Type3count12.SetValue(totalDEC2Niit3, totalDEC2Type3NUMBER12);

                    totalDEC2depname3.SetValue(totalDEC2Niit3, "Дүн");
                    totalDECISION2_TYPEdepname3.SetValue(totalDEC2Niit3, "ЗӨВЛӨМЖ");

                    totalCompanytemplist3.AddRange(BudgetTypes3.OrderBy(m => m.DECISION_TYPE));
                    totalCompanytemplist3.Add(totalDEC2Niit3);
                    BudgetTypes3 = totalCompanytemplist3;



                }



                totaltemp.Add(title);
                totaltemp.AddRange(BudgetTypes1);
                totaltemp.AddRange(BudgetTypes2);
                totaltemp.AddRange(BudgetTypes3);
                requestData = totaltemp;
            }

            response.data = requestData;
            return response;
        }
        [HttpPost]
        public CM3ListResponse CM3List(CM3ListRequest request)
        {
            CM3ListResponse response = new CM3ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                elem.Add(new XElement("V_TYPE", request.Type));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.CM3(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<CM3> total = new List<CM3>();
                CM3 Niit = new CM3();
                var typ = typeof(CM3);

                if (res != null && res.Elements("CM3") != null)
                {
                    List<CM3> body = new List<CM3>();
                    List<CM3> CM3Detial = new List<CM3>();
                    CM3 title = new CM3();
                    CM3 total1 = new CM3();
                    CM3 total2 = new CM3();

                    body = (from item in res.Elements("CM3") select new CM3().SetXml(item)).ToList();
                    if (body.Count > 0)
                    {
                        var depname = typ.GetProperty("DEPARTMENT_NAME");
                       

                        var pay = typ.GetProperty("C1_AMOUNT");
                        var pay1 = typ.GetProperty("CURRENT_AMOUNT");
                        var pay2 = typ.GetProperty("TOTAL_AMOUNT");
                        var pay3 = typ.GetProperty("COMPLETION_DONE_AMOUNT");
                        var pay4 = typ.GetProperty("COMPLETION_PROGRESS_AMOUNT");
                        var pay5 = typ.GetProperty("LAW_AMOUNT");
                        var pay6 = typ.GetProperty("LAW_CURRENT_AMOUNT");
                        var pay7 = typ.GetProperty("LAW_TOTAL_AMOUNT");
                        var pay8 = typ.GetProperty("LAW_COMP_DONE_AMOUNT");
                        var pay9 = typ.GetProperty("LAW_COMP_PROG_AMOUNT");
                        var pay10 = typ.GetProperty("LAW_COMP_INVALID_AMOUNT");
                        var pay11 = typ.GetProperty("C2_AMOUNT");


                        var count = typ.GetProperty("C1_COUNT");
                        var count1 = typ.GetProperty("CURRENT_COUNT");
                        var count2 = typ.GetProperty("TOTAL_COUNT");
                        var count3 = typ.GetProperty("COMPLETION_DONE_COUNT");
                        var count4 = typ.GetProperty("COMPLETION_PROGRESS_COUNT");
                        var count5 = typ.GetProperty("LAW_COUNT");
                        var count6 = typ.GetProperty("LAW_CURRENT_COUNT");
                        var count7 = typ.GetProperty("LAW_TOTAL_COUNT");
                        var count8 = typ.GetProperty("LAW_COMP_DONE_COUNT");
                        var count9 = typ.GetProperty("LAW_COMP_PROG_COUNT");
                        var count10 = typ.GetProperty("LAW_COMP_INVALID_COUNT");
                        var count11 = typ.GetProperty("C2_COUNT");


                        decimal AMOUNT = 0;
                        decimal AMOUNT1 = 0;
                        decimal AMOUNT2 = 0;
                        decimal AMOUNT3 = 0;
                        decimal AMOUNT4 = 0;
                        decimal AMOUNT5 = 0;
                        decimal AMOUNT6 = 0;
                        decimal AMOUNT7 = 0;
                        decimal AMOUNT8 = 0;
                        decimal AMOUNT9 = 0;
                        decimal AMOUNT10 = 0;
                        decimal AMOUNT11 = 0;

                        int NUMBER = 0;
                        int NUMBER1 = 0;
                        int NUMBER2 = 0;
                        int NUMBER3 = 0;
                        int NUMBER4 = 0;
                        int NUMBER5 = 0;
                        int NUMBER6 = 0;
                        int NUMBER7 = 0;
                        int NUMBER8 = 0;
                        int NUMBER9 = 0;
                        int NUMBER10 = 0;
                        int NUMBER11 = 0;

                        foreach (CM3 data in body)
                        {
                            if (data.C1_AMOUNT != null)
                            {
                                string strNii = data.C1_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT += Amount;

                            }
                            if (data.CURRENT_AMOUNT != null)
                            {
                                string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT1 += Amount;

                            }
                            if (data.TOTAL_AMOUNT != null)
                            {
                                string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT2 += Amount;

                            }
                            if (data.COMPLETION_DONE_AMOUNT != null)
                            {
                                string strNii = data.COMPLETION_DONE_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT3 += Amount;

                            }
                            if (data.COMPLETION_PROGRESS_AMOUNT != null)
                            {
                                string strNii = data.COMPLETION_PROGRESS_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT4 += Amount;

                            }
                            if (data.LAW_AMOUNT != null)
                            {
                                string strNii = data.LAW_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT5 += Amount;

                            }
                            if (data.LAW_CURRENT_AMOUNT != null)
                            {
                                string strNii = data.LAW_CURRENT_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT6 += Amount;

                            }
                            if (data.LAW_TOTAL_AMOUNT != null)
                            {
                                string strNii = data.LAW_TOTAL_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT7 += Amount;

                            }
                            if (data.LAW_COMP_DONE_AMOUNT != null)
                            {
                                string strNii = data.LAW_COMP_DONE_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT8 += Amount;

                            }
                            if (data.LAW_COMP_PROG_AMOUNT != null)
                            {
                                string strNii = data.LAW_COMP_PROG_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT9 += Amount;

                            }
                            if (data.LAW_COMP_INVALID_AMOUNT != null)
                            {
                                string strNii = data.LAW_COMP_INVALID_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT10 += Amount;

                            }
                            if (data.C2_AMOUNT != null)
                            {
                                string strNii = data.C2_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT11 += Amount;

                            }

                            if (data.C1_COUNT != 0)
                            {
                                NUMBER += Convert.ToInt32(data.C1_COUNT);
                            }
                            if (data.CURRENT_COUNT != 0)
                            {
                                NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                            }
                            if (data.TOTAL_COUNT != 0)
                            {
                                NUMBER2 += Convert.ToInt32(data.TOTAL_COUNT);
                            }
                            if (data.COMPLETION_DONE_COUNT != 0)
                            {
                                NUMBER3 += Convert.ToInt32(data.COMPLETION_DONE_COUNT);
                            }
                            if (data.COMPLETION_PROGRESS_COUNT != 0)
                            {
                                NUMBER4 += Convert.ToInt32(data.COMPLETION_PROGRESS_COUNT);
                            }
                            if (data.LAW_COUNT != 0)
                            {
                                NUMBER5 += Convert.ToInt32(data.LAW_COUNT);
                            }
                            if (data.LAW_CURRENT_COUNT != 0)
                            {
                                NUMBER6 += Convert.ToInt32(data.LAW_CURRENT_COUNT);
                            }
                            if (data.LAW_TOTAL_COUNT != 0)
                            {
                                NUMBER7 += Convert.ToInt32(data.LAW_TOTAL_COUNT);
                            }
                            if (data.LAW_COMP_DONE_COUNT != 0)
                            {
                                NUMBER8 += Convert.ToInt32(data.LAW_COMP_DONE_COUNT);
                            }
                            if (data.LAW_COMP_PROG_COUNT != 0)
                            {
                                NUMBER9 += Convert.ToInt32(data.LAW_COMP_PROG_COUNT);
                            }
                            if (data.LAW_COMP_INVALID_COUNT != 0)
                            {
                                NUMBER10 += Convert.ToInt32(data.LAW_COMP_INVALID_COUNT);
                            }
                            if (data.C2_COUNT != 0)
                            {
                                NUMBER11 += Convert.ToInt32(data.C2_COUNT);
                            }
                           
                        }


                        pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                        pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                        pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                        pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                        pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                        pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));
                        pay6.SetValue(Niit, AMOUNT6.ToString("#,0.00"));
                        pay7.SetValue(Niit, AMOUNT7.ToString("#,0.00"));
                        pay8.SetValue(Niit, AMOUNT8.ToString("#,0.00"));
                        pay9.SetValue(Niit, AMOUNT9.ToString("#,0.00"));
                        pay10.SetValue(Niit, AMOUNT10.ToString("#,0.00"));
                        pay11.SetValue(Niit, AMOUNT11.ToString("#,0.00"));

                        count.SetValue(Niit, NUMBER);
                        count1.SetValue(Niit, NUMBER1);
                        count2.SetValue(Niit, NUMBER2);
                        count3.SetValue(Niit, NUMBER3);
                        count4.SetValue(Niit, NUMBER4);
                        count5.SetValue(Niit, NUMBER5);
                        count6.SetValue(Niit, NUMBER6);
                        count7.SetValue(Niit, NUMBER7);
                        count8.SetValue(Niit, NUMBER8);
                        count9.SetValue(Niit, NUMBER9);
                        count10.SetValue(Niit, NUMBER10);
                        count11.SetValue(Niit, NUMBER11);

                        depname.SetValue(Niit, "Нийт дүн");

                        if (request.Type == 1)
                        {

                            List<CM3> UAG = new List<CM3>();
                            List<CM3> UAG1 = new List<CM3>();
                            List<CM3> UAG2 = new List<CM3>();
                            CM3ListResponse UAGdata1 = new CM3ListResponse();
                            CM3ListResponse UAGdata2 = new CM3ListResponse();
                            UAG = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Үндэсний аудитын газар"));
                            UAG1 = UAG.FindAll(a => a.IS_STATE.Equals(1));
                            UAG2 = UAG.FindAll(a => a.IS_STATE.Equals(2));
                            if (UAG1.Count > 0)
                                UAGdata1 = CM3TypesList(UAG1, 1);
                            if (UAG2.Count > 0)
                                UAGdata2 = CM3TypesList(UAG2, 2);


                            List<CM3> NT = new List<CM3>();
                            List<CM3> NT1 = new List<CM3>();
                            List<CM3> NT2 = new List<CM3>();
                            CM3ListResponse NTdata1 = new CM3ListResponse();
                            CM3ListResponse NTdata2 = new CM3ListResponse();
                            NT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Нийслэл дэх ТАГ"));
                            NT1 = NT.FindAll(a => a.IS_STATE.Equals(1));
                            NT2 = NT.FindAll(a => a.IS_STATE.Equals(2));
                            if (NT1.Count > 0)
                                NTdata1 = CM3TypesList(NT1, 1);
                            if (NT2.Count > 0)
                                NTdata2 = CM3TypesList(NT2, 2);

                            List<CM3> ART = new List<CM3>();
                            List<CM3> ART1 = new List<CM3>();
                            List<CM3> ART2 = new List<CM3>();
                            CM3ListResponse ARTdata1 = new CM3ListResponse();
                            CM3ListResponse ARTdata2 = new CM3ListResponse();
                            ART = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Архангай аймаг дахь ТАГ"));
                            ART1 = ART.FindAll(a => a.IS_STATE.Equals(1));
                            ART2 = ART.FindAll(a => a.IS_STATE.Equals(2));
                            if (ART1.Count > 0)
                                ARTdata1 = CM3TypesList(ART1, 1);
                            if (ART2.Count > 0)
                                ARTdata2 = CM3TypesList(ART2, 2);

                            List<CM3> BUT = new List<CM3>();
                            List<CM3> BUT1 = new List<CM3>();
                            List<CM3> BUT2 = new List<CM3>();
                            CM3ListResponse BUTdata1 = new CM3ListResponse();
                            CM3ListResponse BUTdata2 = new CM3ListResponse();
                            BUT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Баян-Өлгий аймаг дахь ТАГ"));
                            BUT1 = BUT.FindAll(a => a.IS_STATE.Equals(1));
                            BUT2 = BUT.FindAll(a => a.IS_STATE.Equals(2));
                            if (BUT1.Count > 0)
                                BUTdata1 = CM3TypesList(BUT1, 1);
                            if (BUT2.Count > 0)
                                BUTdata2 = CM3TypesList(BUT2, 2);

                            List<CM3> BHT = new List<CM3>();
                            List<CM3> BHT1 = new List<CM3>();
                            List<CM3> BHT2 = new List<CM3>();
                            CM3ListResponse BHTdata1 = new CM3ListResponse();
                            CM3ListResponse BHTdata2 = new CM3ListResponse();
                            BHT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Баянхонгор аймаг дахь ТАГ"));
                            BHT1 = BHT.FindAll(a => a.IS_STATE.Equals(1));
                            BHT2 = BHT.FindAll(a => a.IS_STATE.Equals(2));
                            if (BHT1.Count > 0)
                                BHTdata1 = CM3TypesList(BHT1, 1);
                            if (BHT2.Count > 0)
                                BHTdata2 = CM3TypesList(BHT2, 2);

                            List<CM3> BLT = new List<CM3>();
                            List<CM3> BLT1 = new List<CM3>();
                            List<CM3> BLT2 = new List<CM3>();
                            CM3ListResponse BLTdata1 = new CM3ListResponse();
                            CM3ListResponse BLTdata2 = new CM3ListResponse();
                            BLT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Булган аймаг дахь ТАГ"));
                            BLT1 = BLT.FindAll(a => a.IS_STATE.Equals(1));
                            BLT2 = BLT.FindAll(a => a.IS_STATE.Equals(2));
                            if (BLT1.Count > 0)
                                BLTdata1 = CM3TypesList(BLT1, 1);
                            if (BLT2.Count > 0)
                                BLTdata2 = CM3TypesList(BLT2, 2);

                            List<CM3> GAT = new List<CM3>();
                            List<CM3> GAT1 = new List<CM3>();
                            List<CM3> GAT2 = new List<CM3>();
                            CM3ListResponse GATdata1 = new CM3ListResponse();
                            CM3ListResponse GATdata2 = new CM3ListResponse();
                            GAT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Говь-Алтай аймаг дахь ТАГ"));
                            GAT1 = GAT.FindAll(a => a.IS_STATE.Equals(1));
                            GAT2 = GAT.FindAll(a => a.IS_STATE.Equals(2));
                            if (GAT1.Count > 0)
                                GATdata1 = CM3TypesList(GAT1, 1);
                            if (GAT2.Count > 0)
                                GATdata2 = CM3TypesList(GAT2, 2);

                            List<CM3> GST = new List<CM3>();
                            List<CM3> GST1 = new List<CM3>();
                            List<CM3> GST2 = new List<CM3>();
                            CM3ListResponse GSTdata1 = new CM3ListResponse();
                            CM3ListResponse GSTdata2 = new CM3ListResponse();
                            GST = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Говьсүмбэр аймаг дахь ТАГ"));
                            GST1 = GST.FindAll(a => a.IS_STATE.Equals(1));
                            GST2 = GST.FindAll(a => a.IS_STATE.Equals(2));
                            if (GST1.Count > 0)
                                GSTdata1 = CM3TypesList(GST1, 1);
                            if (GST2.Count > 0)
                                GSTdata2 = CM3TypesList(GST2, 2);

                            List<CM3> DAT = new List<CM3>();
                            List<CM3> DAT1 = new List<CM3>();
                            List<CM3> DAT2 = new List<CM3>();
                            CM3ListResponse DATdata1 = new CM3ListResponse();
                            CM3ListResponse DATdata2 = new CM3ListResponse();
                            DAT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дархан-Уул аймаг дахь ТАГ"));
                            DAT1 = DAT.FindAll(a => a.IS_STATE.Equals(1));
                            DAT2 = DAT.FindAll(a => a.IS_STATE.Equals(2));
                            if (DAT1.Count > 0)
                                DATdata1 = CM3TypesList(DAT1, 1);
                            if (DAT2.Count > 0)
                                DATdata2 = CM3TypesList(DAT2, 2);

                            List<CM3> DOT = new List<CM3>();
                            List<CM3> DOT1 = new List<CM3>();
                            List<CM3> DOT2 = new List<CM3>();
                            CM3ListResponse DOTdata1 = new CM3ListResponse();
                            CM3ListResponse DOTdata2 = new CM3ListResponse();
                            DOT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дорноговь аймаг дахь ТАГ"));
                            DOT1 = DOT.FindAll(a => a.IS_STATE.Equals(1));
                            DOT2 = DOT.FindAll(a => a.IS_STATE.Equals(2));
                            if (DOT1.Count > 0)
                                DOTdata1 = CM3TypesList(DOT1, 1);
                            if (DOT2.Count > 0)
                                DOTdata2 = CM3TypesList(DOT2, 2);

                            List<CM3> DNT = new List<CM3>();
                            List<CM3> DNT1 = new List<CM3>();
                            List<CM3> DNT2 = new List<CM3>();
                            CM3ListResponse DNTdata1 = new CM3ListResponse();
                            CM3ListResponse DNTdata2 = new CM3ListResponse();
                            DNT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дорнод аймаг дахь ТАГ"));
                            DNT1 = DNT.FindAll(a => a.IS_STATE.Equals(1));
                            DNT2 = DNT.FindAll(a => a.IS_STATE.Equals(2));
                            if (DNT1.Count > 0)
                                DNTdata1 = CM3TypesList(DNT1, 1);
                            if (DNT2.Count > 0)
                                DNTdata2 = CM3TypesList(DNT2, 2);

                            List<CM3> DGT = new List<CM3>();
                            List<CM3> DGT1 = new List<CM3>();
                            List<CM3> DGT2 = new List<CM3>();
                            CM3ListResponse DGTdata1 = new CM3ListResponse();
                            CM3ListResponse DGTdata2 = new CM3ListResponse();
                            DGT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дундговь аймаг дахь ТАГ"));
                            DGT1 = DGT.FindAll(a => a.IS_STATE.Equals(1));
                            DGT2 = DGT.FindAll(a => a.IS_STATE.Equals(2));
                            if (DGT1.Count > 0)
                                DGTdata1 = CM3TypesList(DGT1, 1);
                            if (DGT2.Count > 0)
                                DGTdata2 = CM3TypesList(DGT2, 2);

                            List<CM3> ZAT = new List<CM3>();
                            List<CM3> ZAT1 = new List<CM3>();
                            List<CM3> ZAT2 = new List<CM3>();
                            CM3ListResponse ZATdata1 = new CM3ListResponse();
                            CM3ListResponse ZATdata2 = new CM3ListResponse();
                            ZAT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Завхан аймаг дахь ТАГ"));
                            ZAT1 = ZAT.FindAll(a => a.IS_STATE.Equals(1));
                            ZAT2 = ZAT.FindAll(a => a.IS_STATE.Equals(2));
                            if (ZAT1.Count > 0)
                                ZATdata1 = CM3TypesList(ZAT1, 1);
                            if (ZAT2.Count > 0)
                                ZATdata2 = CM3TypesList(ZAT2, 2);

                            List<CM3> ORT = new List<CM3>();
                            List<CM3> ORT1 = new List<CM3>();
                            List<CM3> ORT2 = new List<CM3>();
                            CM3ListResponse ORTdata1 = new CM3ListResponse();
                            CM3ListResponse ORTdata2 = new CM3ListResponse();
                            ORT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Орхон аймаг дахь ТАГ"));
                            ORT1 = ORT.FindAll(a => a.IS_STATE.Equals(1));
                            ORT2 = ORT.FindAll(a => a.IS_STATE.Equals(2));
                            if (ORT1.Count > 0)
                                ORTdata1 = CM3TypesList(ORT1, 1);
                            if (ORT2.Count > 0)
                                ORTdata2 = CM3TypesList(ORT2, 2);

                            List<CM3> UVT = new List<CM3>();
                            List<CM3> UVT1 = new List<CM3>();
                            List<CM3> UVT2 = new List<CM3>();
                            CM3ListResponse UVTdata1 = new CM3ListResponse();
                            CM3ListResponse UVTdata2 = new CM3ListResponse();
                            UVT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Өвөрхангай аймаг дахь ТАГ"));
                            UVT1 = UVT.FindAll(a => a.IS_STATE.Equals(1));
                            UVT2 = UVT.FindAll(a => a.IS_STATE.Equals(2));
                            if (UVT1.Count > 0)
                                UVTdata1 = CM3TypesList(UVT1, 1);
                            if (UVT2.Count > 0)
                                UVTdata2 = CM3TypesList(UVT2, 2);
                            List<CM3> UMT = new List<CM3>();
                            List<CM3> UMT1 = new List<CM3>();
                            List<CM3> UMT2 = new List<CM3>();
                            CM3ListResponse UMTdata1 = new CM3ListResponse();
                            CM3ListResponse UMTdata2 = new CM3ListResponse();
                            UMT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Өмнөговь аймаг дахь ТАГ"));
                            UMT1 = UMT.FindAll(a => a.IS_STATE.Equals(1));
                            UMT2 = UMT.FindAll(a => a.IS_STATE.Equals(2));
                            if (UMT1.Count > 0)
                                UMTdata1 = CM3TypesList(UMT1, 1);
                            if (UMT2.Count > 0)
                                UMTdata2 = CM3TypesList(UMT2, 2);

                            List<CM3> SBT = new List<CM3>();
                            List<CM3> SBT1 = new List<CM3>();
                            List<CM3> SBT2 = new List<CM3>();
                            CM3ListResponse SBTdata1 = new CM3ListResponse();
                            CM3ListResponse SBTdata2 = new CM3ListResponse();
                            SBT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Сүхбаатар аймаг дахь ТАГ"));
                            SBT1 = SBT.FindAll(a => a.IS_STATE.Equals(1));
                            SBT2 = SBT.FindAll(a => a.IS_STATE.Equals(2));
                            if (SBT1.Count > 0)
                                SBTdata1 = CM3TypesList(SBT1, 1);
                            if (SBT2.Count > 0)
                                SBTdata2 = CM3TypesList(SBT2, 2);

                            List<CM3> SET = new List<CM3>();
                            List<CM3> SET1 = new List<CM3>();
                            List<CM3> SET2 = new List<CM3>();
                            CM3ListResponse SETdata1 = new CM3ListResponse();
                            CM3ListResponse SETdata2 = new CM3ListResponse();
                            SET = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Сэлэнгэ аймаг дахь ТАГ"));
                            SET1 = SET.FindAll(a => a.IS_STATE.Equals(1));
                            SET2 = SET.FindAll(a => a.IS_STATE.Equals(2));
                            if (SET1.Count > 0)
                                SETdata1 = CM3TypesList(SET1, 1);
                            if (SET2.Count > 0)
                                SETdata2 = CM3TypesList(SET2, 2);

                            List<CM3> TUT = new List<CM3>();
                            List<CM3> TUT1 = new List<CM3>();
                            List<CM3> TUT2 = new List<CM3>();
                            CM3ListResponse TUTdata1 = new CM3ListResponse();
                            CM3ListResponse TUTdata2 = new CM3ListResponse();
                            TUT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Төв аймаг дахь ТАГ"));
                            TUT1 = TUT.FindAll(a => a.IS_STATE.Equals(1));
                            TUT2 = TUT.FindAll(a => a.IS_STATE.Equals(2));
                            if (TUT1.Count > 0)
                                TUTdata1 = CM3TypesList(TUT1, 1);
                            if (TUT2.Count > 0)
                                TUTdata2 = CM3TypesList(TUT2, 2);

                            List<CM3> UST = new List<CM3>();
                            List<CM3> UST1 = new List<CM3>();
                            List<CM3> UST2 = new List<CM3>();
                            CM3ListResponse USTdata1 = new CM3ListResponse();
                            CM3ListResponse USTdata2 = new CM3ListResponse();
                            UST = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Увс аймаг дахь ТАГ"));
                            UST1 = UST.FindAll(a => a.IS_STATE.Equals(1));
                            UST2 = UST.FindAll(a => a.IS_STATE.Equals(2));
                            if (UST1.Count > 0)
                                USTdata1 = CM3TypesList(UST1, 1);
                            if (UST2.Count > 0)
                                USTdata2 = CM3TypesList(UST2, 2);

                            List<CM3> HOT = new List<CM3>();
                            List<CM3> HOT1 = new List<CM3>();
                            List<CM3> HOT2 = new List<CM3>();
                            CM3ListResponse HOTdata1 = new CM3ListResponse();
                            CM3ListResponse HOTdata2 = new CM3ListResponse();
                            HOT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Ховд аймаг дахь ТАГ"));
                            HOT1 = HOT.FindAll(a => a.IS_STATE.Equals(1));
                            HOT2 = HOT.FindAll(a => a.IS_STATE.Equals(2));
                            if (HOT1.Count > 0)
                                HOTdata1 = CM3TypesList(HOT1, 1);
                            if (HOT2.Count > 0)
                                HOTdata2 = CM3TypesList(HOT2, 2);

                            List<CM3> HUT = new List<CM3>();
                            List<CM3> HUT1 = new List<CM3>();
                            List<CM3> HUT2 = new List<CM3>();
                            CM3ListResponse HUTdata1 = new CM3ListResponse();
                            CM3ListResponse HUTdata2 = new CM3ListResponse();
                            HUT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Хөвсгөл аймаг дахь ТАГ"));
                            HUT1 = HUT.FindAll(a => a.IS_STATE.Equals(1));
                            HUT2 = HUT.FindAll(a => a.IS_STATE.Equals(2));
                            if (HUT1.Count > 0)
                                HUTdata1 = CM3TypesList(HUT1, 1);
                            if (HUT2.Count > 0)
                                HUTdata2 = CM3TypesList(HUT2, 2);

                            List<CM3> HET = new List<CM3>();
                            List<CM3> HET1 = new List<CM3>();
                            List<CM3> HET2 = new List<CM3>();
                            CM3ListResponse HETdata1 = new CM3ListResponse();
                            CM3ListResponse HETdata2 = new CM3ListResponse();
                            HET = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Хэнтий аймаг дахь ТАГ"));
                            HET1 = HET.FindAll(a => a.IS_STATE.Equals(1));
                            HET2 = HET.FindAll(a => a.IS_STATE.Equals(2));
                            if (HET1.Count > 0)
                                HETdata1 = CM3TypesList(HET1, 1);
                            if (HET2.Count > 0)
                                HETdata2 = CM3TypesList(HET2, 2);


                            List<CM3> types = new List<CM3>();
                            types.AddRange(UAGdata1.data);
                            types.AddRange(UAGdata2.data);
                            types.AddRange(NTdata1.data);
                            types.AddRange(NTdata2.data);
                            types.AddRange(ARTdata1.data);
                            types.AddRange(ARTdata2.data);
                            types.AddRange(BUTdata1.data);
                            types.AddRange(BUTdata2.data);
                            types.AddRange(BHTdata1.data);
                            types.AddRange(BHTdata2.data);
                            types.AddRange(BLTdata1.data);
                            types.AddRange(BLTdata2.data);
                            types.AddRange(GATdata1.data);
                            types.AddRange(GATdata2.data);
                            types.AddRange(GSTdata1.data);
                            types.AddRange(GSTdata2.data);
                            types.AddRange(DATdata1.data);
                            types.AddRange(DATdata2.data);
                            types.AddRange(DOTdata1.data);
                            types.AddRange(DOTdata2.data);
                            types.AddRange(DNTdata1.data);
                            types.AddRange(DNTdata2.data);
                            types.AddRange(DGTdata1.data);
                            types.AddRange(DGTdata2.data);
                            types.AddRange(ZATdata1.data);
                            types.AddRange(ZATdata2.data);
                            types.AddRange(ORTdata1.data);
                            types.AddRange(ORTdata2.data);
                            types.AddRange(UVTdata1.data);
                            types.AddRange(UVTdata2.data);
                            types.AddRange(UMTdata1.data);
                            types.AddRange(UMTdata2.data);
                            types.AddRange(SBTdata1.data);
                            types.AddRange(SBTdata2.data);
                            types.AddRange(SETdata1.data);
                            types.AddRange(SETdata2.data);
                            types.AddRange(TUTdata1.data);
                            types.AddRange(TUTdata2.data);
                            types.AddRange(USTdata1.data);
                            types.AddRange(USTdata2.data);
                            types.AddRange(HOTdata1.data);
                            types.AddRange(HOTdata2.data);
                            types.AddRange(HUTdata1.data);
                            types.AddRange(HUTdata2.data);
                            types.AddRange(HETdata1.data);
                            types.AddRange(HETdata2.data);



                            CM3Detial = types;
                            CM3Detial.Add(Niit);

                            response.data = CM3Detial;
                        }
                        else
                        {
                            total.AddRange(body.OrderBy(m => m.DEPARTMENT_NAME));
                            total.Add(Niit);

                            response.data = total;
                        }

                    }


                    else
                    {
                        response.data = body;
                    }
                }
                response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        public CM3ListResponse CM3TypesList(List<CM3> request, int dataType)
        {
            CM3ListResponse response = new CM3ListResponse();
            var typ = typeof(CM3);
            List<CM3> requestData = new List<CM3>();
            requestData = request;

            if (requestData.Count > 0)
            {

                List<CM3> temp = new List<CM3>();

                CM3 title = new CM3();


                if (dataType == 1)
                    title.BUDGET_TYPE_NAME = "Нэг. Төрийн аудитын байгууллага";
                if (dataType == 2)
                    title.BUDGET_TYPE_NAME = "Хоёр. Хараат бус аудитын компани";

                title.DEPARTMENT_NAME = requestData.FirstOrDefault().DEPARTMENT_NAME;
               
                    CM3 DECNiit = new CM3();
                    var DECdepname = typ.GetProperty("BUDGET_TYPE_NAME");
                    var DEPdepname = typ.GetProperty("DEPARTMENT_NAME");


                    var DECpay = typ.GetProperty("C1_AMOUNT");
                    var DECpay1 = typ.GetProperty("CURRENT_AMOUNT");
                    var DECpay2 = typ.GetProperty("TOTAL_AMOUNT");
                    var DECpay3 = typ.GetProperty("COMPLETION_DONE_AMOUNT");
                    var DECpay4 = typ.GetProperty("COMPLETION_PROGRESS_AMOUNT");
                    var DECpay5 = typ.GetProperty("LAW_AMOUNT");
                    var DECpay6 = typ.GetProperty("LAW_CURRENT_AMOUNT");
                    var DECpay7 = typ.GetProperty("LAW_TOTAL_AMOUNT");
                    var DECpay8 = typ.GetProperty("LAW_COMP_DONE_AMOUNT");
                    var DECpay9 = typ.GetProperty("LAW_COMP_PROG_AMOUNT");
                    var DECpay10 = typ.GetProperty("LAW_COMP_INVALID_AMOUNT");
                    var DECpay11 = typ.GetProperty("C2_AMOUNT");


                    var DECcount = typ.GetProperty("C1_COUNT");
                    var DECcount1 = typ.GetProperty("CURRENT_COUNT");
                    var DECcount2 = typ.GetProperty("TOTAL_COUNT");
                    var DECcount3 = typ.GetProperty("COMPLETION_DONE_COUNT");
                    var DECcount4 = typ.GetProperty("COMPLETION_PROGRESS_COUNT");
                    var DECcount5 = typ.GetProperty("LAW_COUNT");
                    var DECcount6 = typ.GetProperty("LAW_CURRENT_COUNT");
                    var DECcount7 = typ.GetProperty("LAW_TOTAL_COUNT");
                    var DECcount8 = typ.GetProperty("LAW_COMP_DONE_COUNT");
                    var DECcount9 = typ.GetProperty("LAW_COMP_PROG_COUNT");
                    var DECcount10 = typ.GetProperty("LAW_COMP_INVALID_COUNT");
                    var DECcount11 = typ.GetProperty("C2_COUNT");


                    decimal DECAMOUNT = 0;
                    decimal DECAMOUNT1 = 0;
                    decimal DECAMOUNT2 = 0;
                    decimal DECAMOUNT3 = 0;
                    decimal DECAMOUNT4 = 0;
                    decimal DECAMOUNT5 = 0;
                    decimal DECAMOUNT6 = 0;
                    decimal DECAMOUNT7 = 0;
                    decimal DECAMOUNT8 = 0;
                    decimal DECAMOUNT9 = 0;
                    decimal DECAMOUNT10 = 0;
                    decimal DECAMOUNT11 = 0;

                    int DECNUMBER = 0;
                    int DECNUMBER1 = 0;
                    int DECNUMBER2 = 0;
                    int DECNUMBER3 = 0;
                    int DECNUMBER4 = 0;
                    int DECNUMBER5 = 0;
                    int DECNUMBER6 = 0;
                    int DECNUMBER7 = 0;
                    int DECNUMBER8 = 0;
                    int DECNUMBER9 = 0;
                    int DECNUMBER10 = 0;
                    int DECNUMBER11 = 0;

                    foreach (CM3 data in requestData)
                    {

                        if (data.C1_AMOUNT != null)
                        {
                            string strNii = data.C1_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT += Amount;

                        }
                        if (data.CURRENT_AMOUNT != null)
                        {
                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT1 += Amount;

                        }
                        if (data.TOTAL_AMOUNT != null)
                        {
                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT2 += Amount;

                        }
                        if (data.COMPLETION_DONE_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_DONE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT3 += Amount;

                        }
                        if (data.COMPLETION_PROGRESS_AMOUNT != null)
                        {
                            string strNii = data.COMPLETION_PROGRESS_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT4 += Amount;

                        }
                        if (data.LAW_AMOUNT != null)
                        {
                            string strNii = data.LAW_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT5 += Amount;

                        }
                        if (data.LAW_CURRENT_AMOUNT != null)
                        {
                            string strNii = data.LAW_CURRENT_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT6 += Amount;

                        }
                        if (data.LAW_TOTAL_AMOUNT != null)
                        {
                            string strNii = data.LAW_TOTAL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT7 += Amount;

                        }
                        if (data.LAW_COMP_DONE_AMOUNT != null)
                        {
                            string strNii = data.LAW_COMP_DONE_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT8 += Amount;

                        }
                        if (data.LAW_COMP_PROG_AMOUNT != null)
                        {
                            string strNii = data.LAW_COMP_PROG_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT9 += Amount;

                        }
                        if (data.LAW_COMP_INVALID_AMOUNT != null)
                        {
                            string strNii = data.LAW_COMP_INVALID_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT10 += Amount;

                        }
                        if (data.C2_AMOUNT != null)
                        {
                            string strNii = data.C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            DECAMOUNT11 += Amount;

                        }

                        if (data.C1_COUNT != 0)
                        {
                            DECNUMBER += Convert.ToInt32(data.C1_COUNT);
                        }
                        if (data.CURRENT_COUNT != 0)
                        {
                            DECNUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                        }
                        if (data.TOTAL_COUNT != 0)
                        {
                            DECNUMBER2 += Convert.ToInt32(data.TOTAL_COUNT);
                        }
                        if (data.COMPLETION_DONE_COUNT != 0)
                        {
                            DECNUMBER3 += Convert.ToInt32(data.COMPLETION_DONE_COUNT);
                        }
                        if (data.COMPLETION_PROGRESS_COUNT != 0)
                        {
                            DECNUMBER4 += Convert.ToInt32(data.COMPLETION_PROGRESS_COUNT);
                        }
                        if (data.LAW_COUNT != 0)
                        {
                            DECNUMBER5 += Convert.ToInt32(data.LAW_COUNT);
                        }
                        if (data.LAW_CURRENT_COUNT != 0)
                        {
                            DECNUMBER6 += Convert.ToInt32(data.LAW_CURRENT_COUNT);
                        }
                        if (data.LAW_TOTAL_COUNT != 0)
                        {
                            DECNUMBER7 += Convert.ToInt32(data.LAW_TOTAL_COUNT);
                        }
                        if (data.LAW_COMP_DONE_COUNT != 0)
                        {
                            DECNUMBER8 += Convert.ToInt32(data.LAW_COMP_DONE_COUNT);
                        }
                        if (data.LAW_COMP_PROG_COUNT != 0)
                        {
                            DECNUMBER9 += Convert.ToInt32(data.LAW_COMP_PROG_COUNT);
                        }
                        if (data.LAW_COMP_INVALID_COUNT != 0)
                        {
                            DECNUMBER10 += Convert.ToInt32(data.LAW_COMP_INVALID_COUNT);
                        }
                        if (data.C2_COUNT != 0)
                        {
                            DECNUMBER11 += Convert.ToInt32(data.C2_COUNT);
                        }
                        DEPdepname.SetValue(DECNiit, data.DEPARTMENT_NAME);
                    }
                    DECpay.SetValue(DECNiit, DECAMOUNT.ToString("#,0.00"));
                    DECpay1.SetValue(DECNiit, DECAMOUNT1.ToString("#,0.00"));
                    DECpay2.SetValue(DECNiit, DECAMOUNT2.ToString("#,0.00"));
                    DECpay3.SetValue(DECNiit, DECAMOUNT3.ToString("#,0.00"));
                    DECpay4.SetValue(DECNiit, DECAMOUNT4.ToString("#,0.00"));
                    DECpay5.SetValue(DECNiit, DECAMOUNT5.ToString("#,0.00"));
                    DECpay6.SetValue(DECNiit, DECAMOUNT6.ToString("#,0.00"));
                    DECpay7.SetValue(DECNiit, DECAMOUNT7.ToString("#,0.00"));
                    DECpay8.SetValue(DECNiit, DECAMOUNT8.ToString("#,0.00"));
                    DECpay9.SetValue(DECNiit, DECAMOUNT9.ToString("#,0.00"));
                    DECpay10.SetValue(DECNiit, DECAMOUNT10.ToString("#,0.00"));
                    DECpay11.SetValue(DECNiit, DECAMOUNT11.ToString("#,0.00"));

                    DECcount.SetValue(DECNiit, DECNUMBER);
                    DECcount1.SetValue(DECNiit, DECNUMBER1);
                    DECcount2.SetValue(DECNiit, DECNUMBER2);
                    DECcount3.SetValue(DECNiit, DECNUMBER3);
                    DECcount4.SetValue(DECNiit, DECNUMBER4);
                    DECcount5.SetValue(DECNiit, DECNUMBER5);
                    DECcount6.SetValue(DECNiit, DECNUMBER6);
                    DECcount7.SetValue(DECNiit, DECNUMBER7);
                    DECcount8.SetValue(DECNiit, DECNUMBER8);
                    DECcount9.SetValue(DECNiit, DECNUMBER9);
                    DECcount10.SetValue(DECNiit, DECNUMBER10);
                    DECcount11.SetValue(DECNiit, DECNUMBER11);

                    DECdepname.SetValue(DECNiit, "Дүн");

                if (dataType != 0)
                    temp.Add(title);

                temp.AddRange(requestData.OrderBy(m => m.DEPARTMENT_NAME));
                temp.Add(DECNiit);
                requestData = temp;

              
            }

            response.data = requestData;
            return response;
        }
        [HttpPost]
        public CM4ListResponse CM4List(CM4ListRequest request)
        {
            CM4ListResponse response = new CM4ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                elem.Add(new XElement("V_TYPE", request.Type));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.CM4(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<CM4> total = new List<CM4>();
                CM4 Niit = new CM4();
                var typ = typeof(CM4);

                if (res != null && res.Elements("CM4") != null)
                {
                    List<CM4> body = new List<CM4>();
                    List<CM4> CM4Detial = new List<CM4>();
                    CM4 title = new CM4();
                    CM4 total1 = new CM4();
                    CM4 total2 = new CM4();

                    body = (from item in res.Elements("CM4") select new CM4().SetXml(item)).ToList();
                    if (body.Count > 0)
                    {
                        var depname = typ.GetProperty("DEPARTMENT_NAME");


                        var pay = typ.GetProperty("VIOLATION_AMOUNT");
                        var pay1 = typ.GetProperty("ERROR_AMOUNT");
                        var pay2 = typ.GetProperty("ALL_AMOUNT");
                        var pay3 = typ.GetProperty("CORRECTED_ERROR_AMOUNT");
                        var pay4 = typ.GetProperty("OTHER_ERROR_AMOUNT");
                        var pay5 = typ.GetProperty("ACT_AMOUNT");
                        var pay6 = typ.GetProperty("CLAIM_AMOUNT");
                        var pay7 = typ.GetProperty("REFERENCE_AMOUNT");
                        var pay8 = typ.GetProperty("PROPOSAL_AMOUNT");
                        var pay9 = typ.GetProperty("LAW_AMOUNT");
                        var pay10 = typ.GetProperty("OTHER_AMOUNT");


                        var count = typ.GetProperty("VIOLATION_COUNT");
                        var count1 = typ.GetProperty("ERROR_COUNT");
                        var count2 = typ.GetProperty("ALL_COUNT");
                        var count3 = typ.GetProperty("CORRECTED_ERROR_COUNT");
                        var count4 = typ.GetProperty("OTHER_ERROR_COUNT");
                        var count5 = typ.GetProperty("ACT_COUNT");
                        var count6 = typ.GetProperty("CLAIM_COUNT");
                        var count7 = typ.GetProperty("REFERENCE_COUNT");
                        var count8 = typ.GetProperty("PROPOSAL_COUNT");
                        var count9 = typ.GetProperty("LAW_COUNT");
                        var count10 = typ.GetProperty("OTHER_COUNT");


                        decimal AMOUNT = 0;
                        decimal AMOUNT1 = 0;
                        decimal AMOUNT2 = 0;
                        decimal AMOUNT3 = 0;
                        decimal AMOUNT4 = 0;
                        decimal AMOUNT5 = 0;
                        decimal AMOUNT6 = 0;
                        decimal AMOUNT7 = 0;
                        decimal AMOUNT8 = 0;
                        decimal AMOUNT9 = 0;
                        decimal AMOUNT10 = 0;

                        int NUMBER = 0;
                        int NUMBER1 = 0;
                        int NUMBER2 = 0;
                        int NUMBER3 = 0;
                        int NUMBER4 = 0;
                        int NUMBER5 = 0;
                        int NUMBER6 = 0;
                        int NUMBER7 = 0;
                        int NUMBER8 = 0;
                        int NUMBER9 = 0;
                        int NUMBER10 = 0;

                        foreach (CM4 data in body)
                        {
                            if (data.VIOLATION_AMOUNT != null)
                            {
                                string strNii = data.VIOLATION_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT += Amount;

                            }
                            if (data.ERROR_AMOUNT != null)
                            {
                                string strNii = data.ERROR_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT1 += Amount;

                            }
                            if (data.ALL_AMOUNT != null)
                            {
                                string strNii = data.ALL_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT2 += Amount;

                            }
                            if (data.CORRECTED_ERROR_AMOUNT != null)
                            {
                                string strNii = data.CORRECTED_ERROR_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT3 += Amount;

                            }
                            if (data.OTHER_ERROR_AMOUNT != null)
                            {
                                string strNii = data.OTHER_ERROR_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT4 += Amount;

                            }
                            if (data.ACT_AMOUNT != null)
                            {
                                string strNii = data.ACT_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT5 += Amount;

                            }
                            if (data.CLAIM_AMOUNT != null)
                            {
                                string strNii = data.CLAIM_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT6 += Amount;

                            }
                            if (data.REFERENCE_AMOUNT != null)
                            {
                                string strNii = data.REFERENCE_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT7 += Amount;

                            }
                            if (data.PROPOSAL_AMOUNT != null)
                            {
                                string strNii = data.PROPOSAL_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT8 += Amount;

                            }
                            if (data.LAW_AMOUNT != null)
                            {
                                string strNii = data.LAW_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT9 += Amount;

                            }
                            if (data.OTHER_AMOUNT != null)
                            {
                                string strNii = data.OTHER_AMOUNT.Replace(",", "");
                                Decimal Amount = Convert.ToDecimal(strNii);
                                AMOUNT10 += Amount;

                            }
                           

                            if (data.VIOLATION_COUNT != 0)
                            {
                                NUMBER += Convert.ToInt32(data.VIOLATION_COUNT);
                            }
                            if (data.ERROR_COUNT != 0)
                            {
                                NUMBER1 += Convert.ToInt32(data.ERROR_COUNT);
                            }
                            if (data.ALL_COUNT != 0)
                            {
                                NUMBER2 += Convert.ToInt32(data.ALL_COUNT);
                            }
                            if (data.CORRECTED_ERROR_COUNT != 0)
                            {
                                NUMBER3 += Convert.ToInt32(data.CORRECTED_ERROR_COUNT);
                            }
                            if (data.OTHER_ERROR_COUNT != 0)
                            {
                                NUMBER4 += Convert.ToInt32(data.OTHER_ERROR_COUNT);
                            }
                            if (data.ACT_COUNT != 0)
                            {
                                NUMBER5 += Convert.ToInt32(data.ACT_COUNT);
                            }
                            if (data.CLAIM_COUNT != 0)
                            {
                                NUMBER6 += Convert.ToInt32(data.CLAIM_COUNT);
                            }
                            if (data.REFERENCE_COUNT != 0)
                            {
                                NUMBER7 += Convert.ToInt32(data.REFERENCE_COUNT);
                            }
                            if (data.PROPOSAL_COUNT != 0)
                            {
                                NUMBER8 += Convert.ToInt32(data.PROPOSAL_COUNT);
                            }
                            if (data.LAW_COUNT != 0)
                            {
                                NUMBER9 += Convert.ToInt32(data.LAW_COUNT);
                            }
                            if (data.OTHER_COUNT != 0)
                            {
                                NUMBER10 += Convert.ToInt32(data.OTHER_COUNT);
                            }

                        }


                        pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                        pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                        pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                        pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                        pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                        pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));
                        pay6.SetValue(Niit, AMOUNT6.ToString("#,0.00"));
                        pay7.SetValue(Niit, AMOUNT7.ToString("#,0.00"));
                        pay8.SetValue(Niit, AMOUNT8.ToString("#,0.00"));
                        pay9.SetValue(Niit, AMOUNT9.ToString("#,0.00"));
                        pay10.SetValue(Niit, AMOUNT10.ToString("#,0.00"));

                        count.SetValue(Niit, NUMBER);
                        count1.SetValue(Niit, NUMBER1);
                        count2.SetValue(Niit, NUMBER2);
                        count3.SetValue(Niit, NUMBER3);
                        count4.SetValue(Niit, NUMBER4);
                        count5.SetValue(Niit, NUMBER5);
                        count6.SetValue(Niit, NUMBER6);
                        count7.SetValue(Niit, NUMBER7);
                        count8.SetValue(Niit, NUMBER8);
                        count9.SetValue(Niit, NUMBER9);
                        count10.SetValue(Niit, NUMBER10);

                        depname.SetValue(Niit, "Нийт дүн");


                        if (request.Type == 1)
                        {

                            List<CM4> UAG = new List<CM4>();
                            List<CM4> UAG1 = new List<CM4>();
                            List<CM4> UAG2 = new List<CM4>();
                            CM4ListResponse UAGdata1 = new CM4ListResponse();
                            CM4ListResponse UAGdata2 = new CM4ListResponse();
                            UAG = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Үндэсний аудитын газар"));
                            UAG1 = UAG.FindAll(a => a.IS_STATE.Equals(1));
                            UAG2 = UAG.FindAll(a => a.IS_STATE.Equals(2));
                            if (UAG1.Count > 0)
                                UAGdata1 = CM4TypesList(UAG1, 1);
                            if (UAG2.Count > 0)
                                UAGdata2 = CM4TypesList(UAG2, 2);


                            List<CM4> NT = new List<CM4>();
                            List<CM4> NT1 = new List<CM4>();
                            List<CM4> NT2 = new List<CM4>();
                            CM4ListResponse NTdata1 = new CM4ListResponse();
                            CM4ListResponse NTdata2 = new CM4ListResponse();
                            NT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Нийслэл дэх ТАГ"));
                            NT1 = NT.FindAll(a => a.IS_STATE.Equals(1));
                            NT2 = NT.FindAll(a => a.IS_STATE.Equals(2));
                            if (NT1.Count > 0)
                                NTdata1 = CM4TypesList(NT1, 1);
                            if (NT2.Count > 0)
                                NTdata2 = CM4TypesList(NT2, 2);

                            List<CM4> ART = new List<CM4>();
                            List<CM4> ART1 = new List<CM4>();
                            List<CM4> ART2 = new List<CM4>();
                            CM4ListResponse ARTdata1 = new CM4ListResponse();
                            CM4ListResponse ARTdata2 = new CM4ListResponse();
                            ART = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Архангай аймаг дахь ТАГ"));
                            ART1 = ART.FindAll(a => a.IS_STATE.Equals(1));
                            ART2 = ART.FindAll(a => a.IS_STATE.Equals(2));
                            if (ART1.Count > 0)
                                ARTdata1 = CM4TypesList(ART1, 1);
                            if (ART2.Count > 0)
                                ARTdata2 = CM4TypesList(ART2, 2);

                            List<CM4> BUT = new List<CM4>();
                            List<CM4> BUT1 = new List<CM4>();
                            List<CM4> BUT2 = new List<CM4>();
                            CM4ListResponse BUTdata1 = new CM4ListResponse();
                            CM4ListResponse BUTdata2 = new CM4ListResponse();
                            BUT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Баян-Өлгий аймаг дахь ТАГ"));
                            BUT1 = BUT.FindAll(a => a.IS_STATE.Equals(1));
                            BUT2 = BUT.FindAll(a => a.IS_STATE.Equals(2));
                            if (BUT1.Count > 0)
                                BUTdata1 = CM4TypesList(BUT1, 1);
                            if (BUT2.Count > 0)
                                BUTdata2 = CM4TypesList(BUT2, 2);

                            List<CM4> BHT = new List<CM4>();
                            List<CM4> BHT1 = new List<CM4>();
                            List<CM4> BHT2 = new List<CM4>();
                            CM4ListResponse BHTdata1 = new CM4ListResponse();
                            CM4ListResponse BHTdata2 = new CM4ListResponse();
                            BHT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Баянхонгор аймаг дахь ТАГ"));
                            BHT1 = BHT.FindAll(a => a.IS_STATE.Equals(1));
                            BHT2 = BHT.FindAll(a => a.IS_STATE.Equals(2));
                            if (BHT1.Count > 0)
                                BHTdata1 = CM4TypesList(BHT1, 1);
                            if (BHT2.Count > 0)
                                BHTdata2 = CM4TypesList(BHT2, 2);

                            List<CM4> BLT = new List<CM4>();
                            List<CM4> BLT1 = new List<CM4>();
                            List<CM4> BLT2 = new List<CM4>();
                            CM4ListResponse BLTdata1 = new CM4ListResponse();
                            CM4ListResponse BLTdata2 = new CM4ListResponse();
                            BLT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Булган аймаг дахь ТАГ"));
                            BLT1 = BLT.FindAll(a => a.IS_STATE.Equals(1));
                            BLT2 = BLT.FindAll(a => a.IS_STATE.Equals(2));
                            if (BLT1.Count > 0)
                                BLTdata1 = CM4TypesList(BLT1, 1);
                            if (BLT2.Count > 0)
                                BLTdata2 = CM4TypesList(BLT2, 2);

                            List<CM4> GAT = new List<CM4>();
                            List<CM4> GAT1 = new List<CM4>();
                            List<CM4> GAT2 = new List<CM4>();
                            CM4ListResponse GATdata1 = new CM4ListResponse();
                            CM4ListResponse GATdata2 = new CM4ListResponse();
                            GAT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Говь-Алтай аймаг дахь ТАГ"));
                            GAT1 = GAT.FindAll(a => a.IS_STATE.Equals(1));
                            GAT2 = GAT.FindAll(a => a.IS_STATE.Equals(2));
                            if (GAT1.Count > 0)
                                GATdata1 = CM4TypesList(GAT1, 1);
                            if (GAT2.Count > 0)
                                GATdata2 = CM4TypesList(GAT2, 2);

                            List<CM4> GST = new List<CM4>();
                            List<CM4> GST1 = new List<CM4>();
                            List<CM4> GST2 = new List<CM4>();
                            CM4ListResponse GSTdata1 = new CM4ListResponse();
                            CM4ListResponse GSTdata2 = new CM4ListResponse();
                            GST = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Говьсүмбэр аймаг дахь ТАГ"));
                            GST1 = GST.FindAll(a => a.IS_STATE.Equals(1));
                            GST2 = GST.FindAll(a => a.IS_STATE.Equals(2));
                            if (GST1.Count > 0)
                                GSTdata1 = CM4TypesList(GST1, 1);
                            if (GST2.Count > 0)
                                GSTdata2 = CM4TypesList(GST2, 2);

                            List<CM4> DAT = new List<CM4>();
                            List<CM4> DAT1 = new List<CM4>();
                            List<CM4> DAT2 = new List<CM4>();
                            CM4ListResponse DATdata1 = new CM4ListResponse();
                            CM4ListResponse DATdata2 = new CM4ListResponse();
                            DAT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дархан-Уул аймаг дахь ТАГ"));
                            DAT1 = DAT.FindAll(a => a.IS_STATE.Equals(1));
                            DAT2 = DAT.FindAll(a => a.IS_STATE.Equals(2));
                            if (DAT1.Count > 0)
                                DATdata1 = CM4TypesList(DAT1, 1);
                            if (DAT2.Count > 0)
                                DATdata2 = CM4TypesList(DAT2, 2);

                            List<CM4> DOT = new List<CM4>();
                            List<CM4> DOT1 = new List<CM4>();
                            List<CM4> DOT2 = new List<CM4>();
                            CM4ListResponse DOTdata1 = new CM4ListResponse();
                            CM4ListResponse DOTdata2 = new CM4ListResponse();
                            DOT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дорноговь аймаг дахь ТАГ"));
                            DOT1 = DOT.FindAll(a => a.IS_STATE.Equals(1));
                            DOT2 = DOT.FindAll(a => a.IS_STATE.Equals(2));
                            if (DOT1.Count > 0)
                                DOTdata1 = CM4TypesList(DOT1, 1);
                            if (DOT2.Count > 0)
                                DOTdata2 = CM4TypesList(DOT2, 2);

                            List<CM4> DNT = new List<CM4>();
                            List<CM4> DNT1 = new List<CM4>();
                            List<CM4> DNT2 = new List<CM4>();
                            CM4ListResponse DNTdata1 = new CM4ListResponse();
                            CM4ListResponse DNTdata2 = new CM4ListResponse();
                            DNT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дорнод аймаг дахь ТАГ"));
                            DNT1 = DNT.FindAll(a => a.IS_STATE.Equals(1));
                            DNT2 = DNT.FindAll(a => a.IS_STATE.Equals(2));
                            if (DNT1.Count > 0)
                                DNTdata1 = CM4TypesList(DNT1, 1);
                            if (DNT2.Count > 0)
                                DNTdata2 = CM4TypesList(DNT2, 2);

                            List<CM4> DGT = new List<CM4>();
                            List<CM4> DGT1 = new List<CM4>();
                            List<CM4> DGT2 = new List<CM4>();
                            CM4ListResponse DGTdata1 = new CM4ListResponse();
                            CM4ListResponse DGTdata2 = new CM4ListResponse();
                            DGT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Дундговь аймаг дахь ТАГ"));
                            DGT1 = DGT.FindAll(a => a.IS_STATE.Equals(1));
                            DGT2 = DGT.FindAll(a => a.IS_STATE.Equals(2));
                            if (DGT1.Count > 0)
                                DGTdata1 = CM4TypesList(DGT1, 1);
                            if (DGT2.Count > 0)
                                DGTdata2 = CM4TypesList(DGT2, 2);

                            List<CM4> ZAT = new List<CM4>();
                            List<CM4> ZAT1 = new List<CM4>();
                            List<CM4> ZAT2 = new List<CM4>();
                            CM4ListResponse ZATdata1 = new CM4ListResponse();
                            CM4ListResponse ZATdata2 = new CM4ListResponse();
                            ZAT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Завхан аймаг дахь ТАГ"));
                            ZAT1 = ZAT.FindAll(a => a.IS_STATE.Equals(1));
                            ZAT2 = ZAT.FindAll(a => a.IS_STATE.Equals(2));
                            if (ZAT1.Count > 0)
                                ZATdata1 = CM4TypesList(ZAT1, 1);
                            if (ZAT2.Count > 0)
                                ZATdata2 = CM4TypesList(ZAT2, 2);

                            List<CM4> ORT = new List<CM4>();
                            List<CM4> ORT1 = new List<CM4>();
                            List<CM4> ORT2 = new List<CM4>();
                            CM4ListResponse ORTdata1 = new CM4ListResponse();
                            CM4ListResponse ORTdata2 = new CM4ListResponse();
                            ORT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Орхон аймаг дахь ТАГ"));
                            ORT1 = ORT.FindAll(a => a.IS_STATE.Equals(1));
                            ORT2 = ORT.FindAll(a => a.IS_STATE.Equals(2));
                            if (ORT1.Count > 0)
                                ORTdata1 = CM4TypesList(ORT1, 1);
                            if (ORT2.Count > 0)
                                ORTdata2 = CM4TypesList(ORT2, 2);

                            List<CM4> UVT = new List<CM4>();
                            List<CM4> UVT1 = new List<CM4>();
                            List<CM4> UVT2 = new List<CM4>();
                            CM4ListResponse UVTdata1 = new CM4ListResponse();
                            CM4ListResponse UVTdata2 = new CM4ListResponse();
                            UVT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Өвөрхангай аймаг дахь ТАГ"));
                            UVT1 = UVT.FindAll(a => a.IS_STATE.Equals(1));
                            UVT2 = UVT.FindAll(a => a.IS_STATE.Equals(2));
                            if (UVT1.Count > 0)
                                UVTdata1 = CM4TypesList(UVT1, 1);
                            if (UVT2.Count > 0)
                                UVTdata2 = CM4TypesList(UVT2, 2);
                            List<CM4> UMT = new List<CM4>();
                            List<CM4> UMT1 = new List<CM4>();
                            List<CM4> UMT2 = new List<CM4>();
                            CM4ListResponse UMTdata1 = new CM4ListResponse();
                            CM4ListResponse UMTdata2 = new CM4ListResponse();
                            UMT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Өмнөговь аймаг дахь ТАГ"));
                            UMT1 = UMT.FindAll(a => a.IS_STATE.Equals(1));
                            UMT2 = UMT.FindAll(a => a.IS_STATE.Equals(2));
                            if (UMT1.Count > 0)
                                UMTdata1 = CM4TypesList(UMT1, 1);
                            if (UMT2.Count > 0)
                                UMTdata2 = CM4TypesList(UMT2, 2);

                            List<CM4> SBT = new List<CM4>();
                            List<CM4> SBT1 = new List<CM4>();
                            List<CM4> SBT2 = new List<CM4>();
                            CM4ListResponse SBTdata1 = new CM4ListResponse();
                            CM4ListResponse SBTdata2 = new CM4ListResponse();
                            SBT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Сүхбаатар аймаг дахь ТАГ"));
                            SBT1 = SBT.FindAll(a => a.IS_STATE.Equals(1));
                            SBT2 = SBT.FindAll(a => a.IS_STATE.Equals(2));
                            if (SBT1.Count > 0)
                                SBTdata1 = CM4TypesList(SBT1, 1);
                            if (SBT2.Count > 0)
                                SBTdata2 = CM4TypesList(SBT2, 2);

                            List<CM4> SET = new List<CM4>();
                            List<CM4> SET1 = new List<CM4>();
                            List<CM4> SET2 = new List<CM4>();
                            CM4ListResponse SETdata1 = new CM4ListResponse();
                            CM4ListResponse SETdata2 = new CM4ListResponse();
                            SET = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Сэлэнгэ аймаг дахь ТАГ"));
                            SET1 = SET.FindAll(a => a.IS_STATE.Equals(1));
                            SET2 = SET.FindAll(a => a.IS_STATE.Equals(2));
                            if (SET1.Count > 0)
                                SETdata1 = CM4TypesList(SET1, 1);
                            if (SET2.Count > 0)
                                SETdata2 = CM4TypesList(SET2, 2);

                            List<CM4> TUT = new List<CM4>();
                            List<CM4> TUT1 = new List<CM4>();
                            List<CM4> TUT2 = new List<CM4>();
                            CM4ListResponse TUTdata1 = new CM4ListResponse();
                            CM4ListResponse TUTdata2 = new CM4ListResponse();
                            TUT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Төв аймаг дахь ТАГ"));
                            TUT1 = TUT.FindAll(a => a.IS_STATE.Equals(1));
                            TUT2 = TUT.FindAll(a => a.IS_STATE.Equals(2));
                            if (TUT1.Count > 0)
                                TUTdata1 = CM4TypesList(TUT1, 1);
                            if (TUT2.Count > 0)
                                TUTdata2 = CM4TypesList(TUT2, 2);

                            List<CM4> UST = new List<CM4>();
                            List<CM4> UST1 = new List<CM4>();
                            List<CM4> UST2 = new List<CM4>();
                            CM4ListResponse USTdata1 = new CM4ListResponse();
                            CM4ListResponse USTdata2 = new CM4ListResponse();
                            UST = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Увс аймаг дахь ТАГ"));
                            UST1 = UST.FindAll(a => a.IS_STATE.Equals(1));
                            UST2 = UST.FindAll(a => a.IS_STATE.Equals(2));
                            if (UST1.Count > 0)
                                USTdata1 = CM4TypesList(UST1, 1);
                            if (UST2.Count > 0)
                                USTdata2 = CM4TypesList(UST2, 2);

                            List<CM4> HOT = new List<CM4>();
                            List<CM4> HOT1 = new List<CM4>();
                            List<CM4> HOT2 = new List<CM4>();
                            CM4ListResponse HOTdata1 = new CM4ListResponse();
                            CM4ListResponse HOTdata2 = new CM4ListResponse();
                            HOT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Ховд аймаг дахь ТАГ"));
                            HOT1 = HOT.FindAll(a => a.IS_STATE.Equals(1));
                            HOT2 = HOT.FindAll(a => a.IS_STATE.Equals(2));
                            if (HOT1.Count > 0)
                                HOTdata1 = CM4TypesList(HOT1, 1);
                            if (HOT2.Count > 0)
                                HOTdata2 = CM4TypesList(HOT2, 2);

                            List<CM4> HUT = new List<CM4>();
                            List<CM4> HUT1 = new List<CM4>();
                            List<CM4> HUT2 = new List<CM4>();
                            CM4ListResponse HUTdata1 = new CM4ListResponse();
                            CM4ListResponse HUTdata2 = new CM4ListResponse();
                            HUT = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Хөвсгөл аймаг дахь ТАГ"));
                            HUT1 = HUT.FindAll(a => a.IS_STATE.Equals(1));
                            HUT2 = HUT.FindAll(a => a.IS_STATE.Equals(2));
                            if (HUT1.Count > 0)
                                HUTdata1 = CM4TypesList(HUT1, 1);
                            if (HUT2.Count > 0)
                                HUTdata2 = CM4TypesList(HUT2, 2);

                            List<CM4> HET = new List<CM4>();
                            List<CM4> HET1 = new List<CM4>();
                            List<CM4> HET2 = new List<CM4>();
                            CM4ListResponse HETdata1 = new CM4ListResponse();
                            CM4ListResponse HETdata2 = new CM4ListResponse();
                            HET = body.FindAll(a => a.DEPARTMENT_NAME.Equals("Хэнтий аймаг дахь ТАГ"));
                            HET1 = HET.FindAll(a => a.IS_STATE.Equals(1));
                            HET2 = HET.FindAll(a => a.IS_STATE.Equals(2));
                            if (HET1.Count > 0)
                                HETdata1 = CM4TypesList(HET1, 1);
                            if (HET2.Count > 0)
                                HETdata2 = CM4TypesList(HET2, 2);


                            List<CM4> types = new List<CM4>();
                            types.AddRange(UAGdata1.data);
                            types.AddRange(UAGdata2.data);
                            types.AddRange(NTdata1.data);
                            types.AddRange(NTdata2.data);
                            types.AddRange(ARTdata1.data);
                            types.AddRange(ARTdata2.data);
                            types.AddRange(BUTdata1.data);
                            types.AddRange(BUTdata2.data);
                            types.AddRange(BHTdata1.data);
                            types.AddRange(BHTdata2.data);
                            types.AddRange(BLTdata1.data);
                            types.AddRange(BLTdata2.data);
                            types.AddRange(GATdata1.data);
                            types.AddRange(GATdata2.data);
                            types.AddRange(GSTdata1.data);
                            types.AddRange(GSTdata2.data);
                            types.AddRange(DATdata1.data);
                            types.AddRange(DATdata2.data);
                            types.AddRange(DOTdata1.data);
                            types.AddRange(DOTdata2.data);
                            types.AddRange(DNTdata1.data);
                            types.AddRange(DNTdata2.data);
                            types.AddRange(DGTdata1.data);
                            types.AddRange(DGTdata2.data);
                            types.AddRange(ZATdata1.data);
                            types.AddRange(ZATdata2.data);
                            types.AddRange(ORTdata1.data);
                            types.AddRange(ORTdata2.data);
                            types.AddRange(UVTdata1.data);
                            types.AddRange(UVTdata2.data);
                            types.AddRange(UMTdata1.data);
                            types.AddRange(UMTdata2.data);
                            types.AddRange(SBTdata1.data);
                            types.AddRange(SBTdata2.data);
                            types.AddRange(SETdata1.data);
                            types.AddRange(SETdata2.data);
                            types.AddRange(TUTdata1.data);
                            types.AddRange(TUTdata2.data);
                            types.AddRange(USTdata1.data);
                            types.AddRange(USTdata2.data);
                            types.AddRange(HOTdata1.data);
                            types.AddRange(HOTdata2.data);
                            types.AddRange(HUTdata1.data);
                            types.AddRange(HUTdata2.data);
                            types.AddRange(HETdata1.data);
                            types.AddRange(HETdata2.data);



                            CM4Detial = types;
                            CM4Detial.Add(Niit);

                            response.data = CM4Detial;
                        }
                        else
                        {
                            total.AddRange(body.OrderBy(m => m.DEPARTMENT_NAME));
                            total.Add(Niit);

                            response.data = total;
                        }
                    }


                    else
                    {
                        response.data = body;
                    }
                }

                response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        public CM4ListResponse CM4TypesList(List<CM4> request, int dataType)
        {
            CM4ListResponse response = new CM4ListResponse();
            var typ = typeof(CM4);
            List<CM4> requestData = new List<CM4>();
            requestData = request;

            if (requestData.Count > 0)
            {

                List<CM4> temp = new List<CM4>();

                CM4 title = new CM4();


                if (dataType == 1)
                    title.BUDGET_TYPE_NAME = "Нэг. Төрийн аудитын байгууллага";
                if (dataType == 2)
                    title.BUDGET_TYPE_NAME = "Хоёр. Хараат бус аудитын компани";

                title.DEPARTMENT_NAME = requestData.FirstOrDefault().DEPARTMENT_NAME;

                CM4 Niit = new CM4();
                var depname = typ.GetProperty("BUDGET_TYPE_NAME");
                var DEPdepname = typ.GetProperty("DEPARTMENT_NAME");



                var pay = typ.GetProperty("VIOLATION_AMOUNT");
                var pay1 = typ.GetProperty("ERROR_AMOUNT");
                var pay2 = typ.GetProperty("ALL_AMOUNT");
                var pay3 = typ.GetProperty("CORRECTED_ERROR_AMOUNT");
                var pay4 = typ.GetProperty("OTHER_ERROR_AMOUNT");
                var pay5 = typ.GetProperty("ACT_AMOUNT");
                var pay6 = typ.GetProperty("CLAIM_AMOUNT");
                var pay7 = typ.GetProperty("REFERENCE_AMOUNT");
                var pay8 = typ.GetProperty("PROPOSAL_AMOUNT");
                var pay9 = typ.GetProperty("LAW_AMOUNT");
                var pay10 = typ.GetProperty("OTHER_AMOUNT");


                var count = typ.GetProperty("VIOLATION_COUNT");
                var count1 = typ.GetProperty("ERROR_COUNT");
                var count2 = typ.GetProperty("ALL_COUNT");
                var count3 = typ.GetProperty("CORRECTED_ERROR_COUNT");
                var count4 = typ.GetProperty("OTHER_ERROR_COUNT");
                var count5 = typ.GetProperty("ACT_COUNT");
                var count6 = typ.GetProperty("CLAIM_COUNT");
                var count7 = typ.GetProperty("REFERENCE_COUNT");
                var count8 = typ.GetProperty("PROPOSAL_COUNT");
                var count9 = typ.GetProperty("LAW_COUNT");
                var count10 = typ.GetProperty("OTHER_COUNT");


                decimal AMOUNT = 0;
                decimal AMOUNT1 = 0;
                decimal AMOUNT2 = 0;
                decimal AMOUNT3 = 0;
                decimal AMOUNT4 = 0;
                decimal AMOUNT5 = 0;
                decimal AMOUNT6 = 0;
                decimal AMOUNT7 = 0;
                decimal AMOUNT8 = 0;
                decimal AMOUNT9 = 0;
                decimal AMOUNT10 = 0;

                int NUMBER = 0;
                int NUMBER1 = 0;
                int NUMBER2 = 0;
                int NUMBER3 = 0;
                int NUMBER4 = 0;
                int NUMBER5 = 0;
                int NUMBER6 = 0;
                int NUMBER7 = 0;
                int NUMBER8 = 0;
                int NUMBER9 = 0;
                int NUMBER10 = 0;


                foreach (CM4 data in requestData)
                {
                    if (data.VIOLATION_AMOUNT != null)
                    {
                        string strNii = data.VIOLATION_AMOUNT.Replace(",", "");
                        Decimal Amount = Convert.ToDecimal(strNii);
                        AMOUNT += Amount;

                    }
                    if (data.ERROR_AMOUNT != null)
                    {
                        string strNii = data.ERROR_AMOUNT.Replace(",", "");
                        Decimal Amount = Convert.ToDecimal(strNii);
                        AMOUNT1 += Amount;

                    }
                    if (data.ALL_AMOUNT != null)
                    {
                        string strNii = data.ALL_AMOUNT.Replace(",", "");
                        Decimal Amount = Convert.ToDecimal(strNii);
                        AMOUNT2 += Amount;

                    }
                    if (data.CORRECTED_ERROR_AMOUNT != null)
                    {
                        string strNii = data.CORRECTED_ERROR_AMOUNT.Replace(",", "");
                        Decimal Amount = Convert.ToDecimal(strNii);
                        AMOUNT3 += Amount;

                    }
                    if (data.OTHER_ERROR_AMOUNT != null)
                    {
                        string strNii = data.OTHER_ERROR_AMOUNT.Replace(",", "");
                        Decimal Amount = Convert.ToDecimal(strNii);
                        AMOUNT4 += Amount;

                    }
                    if (data.ACT_AMOUNT != null)
                    {
                        string strNii = data.ACT_AMOUNT.Replace(",", "");
                        Decimal Amount = Convert.ToDecimal(strNii);
                        AMOUNT5 += Amount;

                    }
                    if (data.CLAIM_AMOUNT != null)
                    {
                        string strNii = data.CLAIM_AMOUNT.Replace(",", "");
                        Decimal Amount = Convert.ToDecimal(strNii);
                        AMOUNT6 += Amount;

                    }
                    if (data.REFERENCE_AMOUNT != null)
                    {
                        string strNii = data.REFERENCE_AMOUNT.Replace(",", "");
                        Decimal Amount = Convert.ToDecimal(strNii);
                        AMOUNT7 += Amount;

                    }
                    if (data.PROPOSAL_AMOUNT != null)
                    {
                        string strNii = data.PROPOSAL_AMOUNT.Replace(",", "");
                        Decimal Amount = Convert.ToDecimal(strNii);
                        AMOUNT8 += Amount;

                    }
                    if (data.LAW_AMOUNT != null)
                    {
                        string strNii = data.LAW_AMOUNT.Replace(",", "");
                        Decimal Amount = Convert.ToDecimal(strNii);
                        AMOUNT9 += Amount;

                    }
                    if (data.OTHER_AMOUNT != null)
                    {
                        string strNii = data.OTHER_AMOUNT.Replace(",", "");
                        Decimal Amount = Convert.ToDecimal(strNii);
                        AMOUNT10 += Amount;

                    }


                    if (data.VIOLATION_COUNT != 0)
                    {
                        NUMBER += Convert.ToInt32(data.VIOLATION_COUNT);
                    }
                    if (data.ERROR_COUNT != 0)
                    {
                        NUMBER1 += Convert.ToInt32(data.ERROR_COUNT);
                    }
                    if (data.ALL_COUNT != 0)
                    {
                        NUMBER2 += Convert.ToInt32(data.ALL_COUNT);
                    }
                    if (data.CORRECTED_ERROR_COUNT != 0)
                    {
                        NUMBER3 += Convert.ToInt32(data.CORRECTED_ERROR_COUNT);
                    }
                    if (data.OTHER_ERROR_COUNT != 0)
                    {
                        NUMBER4 += Convert.ToInt32(data.OTHER_ERROR_COUNT);
                    }
                    if (data.ACT_COUNT != 0)
                    {
                        NUMBER5 += Convert.ToInt32(data.ACT_COUNT);
                    }
                    if (data.CLAIM_COUNT != 0)
                    {
                        NUMBER6 += Convert.ToInt32(data.CLAIM_COUNT);
                    }
                    if (data.REFERENCE_COUNT != 0)
                    {
                        NUMBER7 += Convert.ToInt32(data.REFERENCE_COUNT);
                    }
                    if (data.PROPOSAL_COUNT != 0)
                    {
                        NUMBER8 += Convert.ToInt32(data.PROPOSAL_COUNT);
                    }
                    if (data.LAW_COUNT != 0)
                    {
                        NUMBER9 += Convert.ToInt32(data.LAW_COUNT);
                    }
                    if (data.OTHER_COUNT != 0)
                    {
                        NUMBER10 += Convert.ToInt32(data.OTHER_COUNT);
                    }

                }
                pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));
                pay6.SetValue(Niit, AMOUNT6.ToString("#,0.00"));
                pay7.SetValue(Niit, AMOUNT7.ToString("#,0.00"));
                pay8.SetValue(Niit, AMOUNT8.ToString("#,0.00"));
                pay9.SetValue(Niit, AMOUNT9.ToString("#,0.00"));
                pay10.SetValue(Niit, AMOUNT10.ToString("#,0.00"));

                count.SetValue(Niit, NUMBER);
                count1.SetValue(Niit, NUMBER1);
                count2.SetValue(Niit, NUMBER2);
                count3.SetValue(Niit, NUMBER3);
                count4.SetValue(Niit, NUMBER4);
                count5.SetValue(Niit, NUMBER5);
                count6.SetValue(Niit, NUMBER6);
                count7.SetValue(Niit, NUMBER7);
                count8.SetValue(Niit, NUMBER8);
                count9.SetValue(Niit, NUMBER9);
                count10.SetValue(Niit, NUMBER10);

                depname.SetValue(Niit, "Дүн");

                if (dataType != 0)
                    temp.Add(title);

                temp.AddRange(requestData.OrderBy(m => m.DEPARTMENT_NAME));
                temp.Add(Niit);
                requestData = temp;


            }

            response.data = requestData;
            return response;
        }
        [HttpPost]
        public CM5ListResponse CM5List(CM5ListRequest request)
        {
            CM5ListResponse response = new CM5ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else 
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.CM5(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                if (res != null && res.Elements("CM5") != null)
                    response.data = (from item in res.Elements("CM5") select new CM5().SetXml(item)).ToList();

                response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public CM6ListResponse CM6List(CM6ListRequest request)
        {
            CM6ListResponse response = new CM6ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.CM6(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<CM6List> body = new List<CM6List>();
                List<CM6List> total = new List<CM6List>();
                CM6List Niit = new CM6List();
                var typ = typeof(CM6List);
                if (res != null && res.Elements("CM6") != null)
                {
                    
                    body = (from item in res.Elements("CM6") select new CM6List().SetXml(item)).ToList();
                }
                if (body.Count > 0)
                {
                    var depname = typ.GetProperty("AUD_NAME");

                    var pay = typ.GetProperty("ALL_AMOUNT");
                    var pay1 = typ.GetProperty("PROCESSED_INCOMED_AMOUNT");
                    var pay2 = typ.GetProperty("PROCESSED_COSTS_AMOUNT");
                    var pay3 = typ.GetProperty("ALL_C2_AMOUNT");
                    var pay4 = typ.GetProperty("ACCEPTED_INCOMED_AMOUNT");
                    var pay5 = typ.GetProperty("ACCEPTED_COSTS_AMOUNT");


                    var count = typ.GetProperty("ALL_COUNT");
                    var count1 = typ.GetProperty("PROCESSED_INCOMED_COUNT");
                    var count2 = typ.GetProperty("PROCESSED_COSTS_COUNT");
                    var count3 = typ.GetProperty("ALL_C1_COUNT");
                    var count4 = typ.GetProperty("ACCEPTED_INCOMED_COUNT");
                    var count5 = typ.GetProperty("ACCEPTED_COSTS_COUNT");


                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;
                    Decimal AMOUNT2 = 0;
                    Decimal AMOUNT3 = 0;
                    Decimal AMOUNT4 = 0;
                    Decimal AMOUNT5 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;
                    int NUMBER5 = 0;

                    foreach (CM6List data in body)
                    {
                        if (data.ALL_AMOUNT != null)
                        {
                            string strNii = data.ALL_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT += Amount;

                        }
                        if (data.PROCESSED_INCOMED_AMOUNT != null)
                        {
                            string strNii = data.PROCESSED_INCOMED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount;

                        }
                        if (data.PROCESSED_COSTS_AMOUNT != null)
                        {
                            string strNii = data.PROCESSED_COSTS_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT2 += Amount;

                        }
                        if (data.ALL_C2_AMOUNT != null)
                        {
                            string strNii = data.ALL_C2_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT3 += Amount;

                        }
                        if (data.ACCEPTED_INCOMED_AMOUNT != null)
                        {
                            string strNii = data.ACCEPTED_INCOMED_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT4 += Amount;

                        }
                        if (data.ACCEPTED_COSTS_AMOUNT != null)
                        {
                            string strNii = data.ACCEPTED_COSTS_AMOUNT.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT5 += Amount;

                        }
                       

                        if (data.ALL_COUNT != 0)
                        {
                            NUMBER += Convert.ToInt32(data.ALL_COUNT);
                        }
                        if (data.PROCESSED_INCOMED_COUNT != 0)
                        {
                            NUMBER1 += Convert.ToInt32(data.PROCESSED_INCOMED_COUNT);
                        }
                        if (data.PROCESSED_COSTS_COUNT != 0)
                        {
                            NUMBER2 += Convert.ToInt32(data.PROCESSED_COSTS_COUNT);
                        }
                        if (data.ALL_C1_COUNT != 0)
                        {
                            NUMBER3 += Convert.ToInt32(data.ALL_C1_COUNT);
                        }
                        if (data.ACCEPTED_INCOMED_COUNT != 0)
                        {
                            NUMBER4 += Convert.ToInt32(data.ACCEPTED_INCOMED_COUNT);
                        }
                        if (data.ACCEPTED_COSTS_COUNT != 0)
                        {
                            NUMBER5 += Convert.ToInt32(data.ACCEPTED_COSTS_COUNT);
                        }
                        
                    }


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.00"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.00"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.00"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);

                    depname.SetValue(Niit, "Нийт дүн");


                    total = body;
                    total.Add(Niit);

                    response.data = total;
                }
                else
                {
                    response.data = body;
                }
                response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public CM7ListResponse CM7List(CM7ListRequest request)
        {
            CM7ListResponse response = new CM7ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.CM7(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<CM7> body = new List<CM7>();
                List<CM7> total = new List<CM7>();
                CM7 Niit = new CM7();
                var typ = typeof(CM7);
                if (res != null && res.Elements("CM7") != null)
                    body = (from item in res.Elements("CM7") select new CM7().SetXml(item)).ToList();
                if (body.Count > 0)
                {
                    var depname = typ.GetProperty("AUD_NAME");


                    var count = typ.GetProperty("REFERENCE_COUNT");
                    var count1 = typ.GetProperty("BUDGET_EXPENSES");
                    var count2 = typ.GetProperty("HUMAN_RESOURCES");
                    var count3 = typ.GetProperty("PLANNED_COMPLETED");
                    var count4 = typ.GetProperty("OTHER");
                    var count5 = typ.GetProperty("COMP_DONE");
                    var count6 = typ.GetProperty("COMP_PROGRESS");
                    var count7 = typ.GetProperty("RESOLVED_COMPLAINT_COUNT");



                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;
                    int NUMBER5 = 0;
                    int NUMBER6 = 0;
                    int NUMBER7 = 0;

                    foreach (CM7 data in body)
                    {
                        if (data.REFERENCE_COUNT != 0)
                        {
                            NUMBER += Convert.ToInt32(data.REFERENCE_COUNT);
                        }
                        if (data.BUDGET_EXPENSES != 0)
                        {
                            NUMBER1 += Convert.ToInt32(data.BUDGET_EXPENSES);
                        }
                        if (data.HUMAN_RESOURCES != 0)
                        {
                            NUMBER2 += Convert.ToInt32(data.HUMAN_RESOURCES);
                        }
                        if (data.PLANNED_COMPLETED != 0)
                        {
                            NUMBER3 += Convert.ToInt32(data.PLANNED_COMPLETED);
                        }
                        if (data.OTHER != 0)
                        {
                            NUMBER4 += Convert.ToInt32(data.OTHER);
                        }
                        if (data.COMP_DONE != 0)
                        {
                            NUMBER5 += Convert.ToInt32(data.COMP_DONE);
                        }
                        if (data.COMP_PROGRESS != 0)
                        {
                            NUMBER6 += Convert.ToInt32(data.COMP_PROGRESS);
                        }
                        if (data.RESOLVED_COMPLAINT_COUNT != 0)
                        {
                            NUMBER7 += Convert.ToInt32(data.RESOLVED_COMPLAINT_COUNT);
                        }
                    }


                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);
                    count6.SetValue(Niit, NUMBER6);
                    count7.SetValue(Niit, NUMBER7);

                    depname.SetValue(Niit, "Нийт дүн");


                    total = body;
                    total.Add(Niit);

                    response.data = total;
                }
                else
                {
                    response.data = body;
                }
                response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public CM8ListResponse CM8List(CM8ListRequest request)
        {
            CM8ListResponse response = new CM8ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                if (request.order.Count > 0)
                {
                    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                }

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                XElement res = AppStatic.SystemController.CM8(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"), User.Identity.GetUserId());
                List<CM8> body = new List<CM8>();
                List<CM8> total = new List<CM8>();
                CM8 Niit = new CM8();
                var typ = typeof(CM8);
                if (res != null && res.Elements("CM8") != null)
                    body = (from item in res.Elements("CM8") select new CM8().SetXml(item)).ToList();
                if (body.Count > 0)
                {
                    var depname = typ.GetProperty("AUD_NAME");

                    var pay = typ.GetProperty("APPROVED_BUDGET");
                    var pay1 = typ.GetProperty("PERFORMANCE_BUDGET");

                    var count = typ.GetProperty("APPROVED_NUMBERS");
                    var count1 = typ.GetProperty("WORKERS");
                    var count2 = typ.GetProperty("DIRECTING_STAFF");
                    var count3 = typ.GetProperty("MANAGER");
                    var count4 = typ.GetProperty("SENIOR_AUDITOR_ANALYST");
                    var count5 = typ.GetProperty("AUDITOR_ANALYST");
                    var count6 = typ.GetProperty("OTHER_OFFICE");
                    var count7 = typ.GetProperty("EDU_DOCTOR");
                    var count8 = typ.GetProperty("EDU_MAGISTR");
                    var count9 = typ.GetProperty("EDU_BAKLAVR");
                    var count10 = typ.GetProperty("EDU_AMONGST");
                    var count11 = typ.GetProperty("EDU_JUNIOR_AMONGST");

                    var count12 = typ.GetProperty("PRO_ACCOUNTANT");
                    var count13 = typ.GetProperty("ACCOUNTANT_ECONOMIST");
                    var count14 = typ.GetProperty("LAWYER");
                    var count15 = typ.GetProperty("INGENER");
                    var count16 = typ.GetProperty("OTHER_PROF");
                    var count17 = typ.GetProperty("STUDY_COUNT");
                    var count18 = typ.GetProperty("INCLUDED_MAN");
                    var count19 = typ.GetProperty("ONLINE_STUDY_COUNT");
                    var count20 = typ.GetProperty("LOCAL_STUDY_COUNT");
                    var count21 = typ.GetProperty("AUDIT_STUDY_COUNT");
                    var count22 = typ.GetProperty("FOREIGN_STUDY_COUNT");
                    var count23 = typ.GetProperty("FOREIGN_MAN_COUNT");
                    var count24 = typ.GetProperty("INSIDE_STUDY_COUNT");
                    var count25 = typ.GetProperty("INSIDE_MAN_COUNT");
                    var count26 = typ.GetProperty("ORG_STUDY_COUNT");
                    var count27 = typ.GetProperty("ORG_MAN_COUNT");
                    var count28 = typ.GetProperty("RESEARCH_ALL");
                    var count29 = typ.GetProperty("PUBLISHED_REPORT");
                    var count30 = typ.GetProperty("NEWS_ARTICLE");
                    var count31 = typ.GetProperty("TV_NEWS_BROADCAST");
                    var count32 = typ.GetProperty("ORG_NEWS");
                    var count33 = typ.GetProperty("WEB_ACCESS");
                    var count34 = typ.GetProperty("RECEIVED_ALL");
                    var count35 = typ.GetProperty("TAB_WORKERS");
                    var count36 = typ.GetProperty("TAB_SKILLS");
                    var count37 = typ.GetProperty("AUDIT_LET");
                    var count38 = typ.GetProperty("RECEIVED_OTHER");
                    var count39 = typ.GetProperty("DECIDED_TIME");
                    var count40 = typ.GetProperty("DEC_EXPIRED");
                    var count41 = typ.GetProperty("DEC_UNEXPIRED");

                    Decimal AMOUNT = 0;
                    Decimal AMOUNT1 = 0;

                    int NUMBER = 0;
                    int NUMBER1 = 0;
                    int NUMBER2 = 0;
                    int NUMBER3 = 0;
                    int NUMBER4 = 0;
                    int NUMBER5 = 0;
                    int NUMBER6 = 0;
                    int NUMBER7 = 0;
                    int NUMBER8 = 0;
                    int NUMBER9 = 0;
                    int NUMBER10 = 0;
                    int NUMBER11 = 0;
                    int NUMBER12 = 0;
                    int NUMBER13 = 0;
                    int NUMBER14 = 0;
                    int NUMBER15 = 0;
                    int NUMBER16 = 0;
                    int NUMBER17 = 0;
                    int NUMBER18 = 0;
                    int NUMBER19 = 0;
                    int NUMBER20 = 0;
                    int NUMBER21 = 0;
                    int NUMBER22 = 0;
                    int NUMBER23 = 0;
                    int NUMBER24 = 0;
                    int NUMBER25 = 0;
                    int NUMBER26 = 0;
                    int NUMBER27 = 0;
                    int NUMBER28 = 0;
                    int NUMBER29 = 0;
                    int NUMBER30 = 0;
                    int NUMBER31 = 0;
                    int NUMBER32 = 0;
                    int NUMBER33 = 0;
                    int NUMBER34 = 0;
                    int NUMBER35 = 0;
                    int NUMBER36 = 0;
                    int NUMBER37 = 0;
                    int NUMBER38 = 0;
                    int NUMBER39 = 0;
                    int NUMBER40 = 0;
                    int NUMBER41 = 0;

                    foreach (CM8 data in body)
                    {
                        if (data.APPROVED_BUDGET != null)
                        {
                            string strNii = data.APPROVED_BUDGET.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT += Amount;

                        }
                        if (data.PERFORMANCE_BUDGET != null)
                        {
                            string strNii = data.PERFORMANCE_BUDGET.Replace(",", "");
                            Decimal Amount = Convert.ToDecimal(strNii);
                            AMOUNT1 += Amount;

                        }
                        if (data.APPROVED_NUMBERS != 0)
                        {
                            NUMBER += Convert.ToInt32(data.APPROVED_NUMBERS);
                        }
                        if (data.WORKERS != 0)
                        {
                            NUMBER1 += Convert.ToInt32(data.WORKERS);
                        }
                        if (data.DIRECTING_STAFF != 0)
                        {
                            NUMBER2 += Convert.ToInt32(data.DIRECTING_STAFF);
                        }
                        if (data.MANAGER != 0)
                        {
                            NUMBER3 += Convert.ToInt32(data.MANAGER);
                        }
                        if (data.SENIOR_AUDITOR_ANALYST != 0)
                        {
                            NUMBER4 += Convert.ToInt32(data.SENIOR_AUDITOR_ANALYST);
                        }
                        if (data.AUDITOR_ANALYST != 0)
                        {
                            NUMBER5 += Convert.ToInt32(data.AUDITOR_ANALYST);
                        }
                        if (data.OTHER_OFFICE != 0)
                        {
                            NUMBER6 += Convert.ToInt32(data.OTHER_OFFICE);
                        }
                        if (data.EDU_DOCTOR != 0)
                        {
                            NUMBER7 += Convert.ToInt32(data.EDU_DOCTOR);
                        }
                        if (data.EDU_MAGISTR != 0)
                        {
                            NUMBER8 += Convert.ToInt32(data.EDU_MAGISTR);
                        }
                        if (data.EDU_BAKLAVR != 0)
                        {
                            NUMBER9 += Convert.ToInt32(data.EDU_BAKLAVR);
                        }
                        if (data.EDU_AMONGST != 0)
                        {
                            NUMBER10 += Convert.ToInt32(data.EDU_AMONGST);
                        }
                        if (data.EDU_JUNIOR_AMONGST != 0)
                        {
                            NUMBER11 += Convert.ToInt32(data.EDU_JUNIOR_AMONGST);
                        }
                        if (data.PRO_ACCOUNTANT != 0)
                        {
                            NUMBER12 += Convert.ToInt32(data.PRO_ACCOUNTANT);
                        }
                        if (data.ACCOUNTANT_ECONOMIST != 0)
                        {
                            NUMBER13 += Convert.ToInt32(data.ACCOUNTANT_ECONOMIST);
                        }
                        if (data.LAWYER != 0)
                        {
                            NUMBER14 += Convert.ToInt32(data.LAWYER);
                        }
                        if (data.INGENER != 0)
                        {
                            NUMBER15 += Convert.ToInt32(data.INGENER);
                        }
                        if (data.OTHER_PROF != 0)
                        {
                            NUMBER16 += Convert.ToInt32(data.OTHER_PROF);
                        }
                        if (data.STUDY_COUNT != 0)
                        {
                            NUMBER17 += Convert.ToInt32(data.STUDY_COUNT);
                        }
                        if (data.INCLUDED_MAN != 0)
                        {
                            NUMBER18 += Convert.ToInt32(data.INCLUDED_MAN);
                        }
                        if (data.ONLINE_STUDY_COUNT != 0)
                        {
                            NUMBER19 += Convert.ToInt32(data.ONLINE_STUDY_COUNT);
                        }
                        if (data.LOCAL_STUDY_COUNT != 0)
                        {
                            NUMBER20 += Convert.ToInt32(data.LOCAL_STUDY_COUNT);
                        }
                        if (data.AUDIT_STUDY_COUNT != 0)
                        {
                            NUMBER21 += Convert.ToInt32(data.AUDIT_STUDY_COUNT);
                        }
                        if (data.FOREIGN_STUDY_COUNT != 0)
                        {
                            NUMBER22 += Convert.ToInt32(data.FOREIGN_STUDY_COUNT);
                        }
                        if (data.FOREIGN_MAN_COUNT != 0)
                        {
                            NUMBER23 += Convert.ToInt32(data.FOREIGN_MAN_COUNT);
                        }
                        if (data.INSIDE_STUDY_COUNT != 0)
                        {
                            NUMBER24 += Convert.ToInt32(data.INSIDE_STUDY_COUNT);
                        }
                        if (data.INSIDE_MAN_COUNT != 0)
                        {
                            NUMBER25 += Convert.ToInt32(data.INSIDE_MAN_COUNT);
                        }
                        if (data.ORG_STUDY_COUNT != 0)
                        {
                            NUMBER26 += Convert.ToInt32(data.ORG_STUDY_COUNT);
                        }
                        if (data.ORG_MAN_COUNT != 0)
                        {
                            NUMBER27 += Convert.ToInt32(data.ORG_MAN_COUNT);
                        }
                        if (data.RESEARCH_ALL != 0)
                        {
                            NUMBER28 += Convert.ToInt32(data.RESEARCH_ALL);
                        }
                        if (data.PUBLISHED_REPORT != 0)
                        {
                            NUMBER29 += Convert.ToInt32(data.PUBLISHED_REPORT);
                        }
                        if (data.NEWS_ARTICLE != 0)
                        {
                            NUMBER30 += Convert.ToInt32(data.NEWS_ARTICLE);
                        }
                        if (data.TV_NEWS_BROADCAST != 0)
                        {
                            NUMBER31 += Convert.ToInt32(data.TV_NEWS_BROADCAST);
                        }
                        if (data.ORG_NEWS != 0)
                        {
                            NUMBER32 += Convert.ToInt32(data.ORG_NEWS);
                        }
                        if (data.WEB_ACCESS != 0)
                        {
                            NUMBER33 += Convert.ToInt32(data.WEB_ACCESS);
                        }
                        if (data.RECEIVED_ALL != 0)
                        {
                            NUMBER34 += Convert.ToInt32(data.RECEIVED_ALL);
                        }
                        if (data.TAB_WORKERS != 0)
                        {
                            NUMBER35 += Convert.ToInt32(data.TAB_WORKERS);
                        }
                        if (data.TAB_SKILLS != 0)
                        {
                            NUMBER36 += Convert.ToInt32(data.TAB_SKILLS);
                        }
                        if (data.AUDIT_LET != 0)
                        {
                            NUMBER37 += Convert.ToInt32(data.AUDIT_LET);
                        }
                        if (data.RECEIVED_OTHER != 0)
                        {
                            NUMBER38 += Convert.ToInt32(data.RECEIVED_OTHER);
                        }
                        if (data.DECIDED_TIME != 0)
                        {
                            NUMBER39 += Convert.ToInt32(data.DECIDED_TIME);
                        }
                        if (data.DEC_EXPIRED != 0)
                        {
                            NUMBER40 += Convert.ToInt32(data.DEC_EXPIRED);
                        }
                        if (data.DEC_UNEXPIRED != 0)
                        {
                            NUMBER41 += Convert.ToInt32(data.DEC_UNEXPIRED);
                        }
                    }

                    pay.SetValue(Niit, AMOUNT.ToString("#,0.00"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.00"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);
                    count6.SetValue(Niit, NUMBER6);
                    count7.SetValue(Niit, NUMBER7);
                    count8.SetValue(Niit, NUMBER8);
                    count9.SetValue(Niit, NUMBER9);
                    count10.SetValue(Niit, NUMBER10);
                    count11.SetValue(Niit, NUMBER11);
                    count12.SetValue(Niit, NUMBER12);
                    count13.SetValue(Niit, NUMBER13);
                    count14.SetValue(Niit, NUMBER14);
                    count15.SetValue(Niit, NUMBER15);
                    count16.SetValue(Niit, NUMBER16);
                    count17.SetValue(Niit, NUMBER17);
                    count18.SetValue(Niit, NUMBER18);
                    count19.SetValue(Niit, NUMBER19);
                    count20.SetValue(Niit, NUMBER20);
                    count21.SetValue(Niit, NUMBER21);
                    count22.SetValue(Niit, NUMBER22);
                    count23.SetValue(Niit, NUMBER23);
                    count24.SetValue(Niit, NUMBER24);
                    count25.SetValue(Niit, NUMBER25);
                    count26.SetValue(Niit, NUMBER26);
                    count27.SetValue(Niit, NUMBER27);
                    count28.SetValue(Niit, NUMBER28);
                    count29.SetValue(Niit, NUMBER29);
                    count30.SetValue(Niit, NUMBER30);
                    count31.SetValue(Niit, NUMBER31);
                    count32.SetValue(Niit, NUMBER32);
                    count33.SetValue(Niit, NUMBER33);
                    count34.SetValue(Niit, NUMBER34);
                    count35.SetValue(Niit, NUMBER35);
                    count36.SetValue(Niit, NUMBER36);
                    count37.SetValue(Niit, NUMBER37);
                    count38.SetValue(Niit, NUMBER38);
                    count39.SetValue(Niit, NUMBER39);
                    count40.SetValue(Niit, NUMBER40);
                    count41.SetValue(Niit, NUMBER41);

                    depname.SetValue(Niit, "Нийт дүн");


                    total = body;
                    total.Add(Niit);

                    response.data = total;
                }
                else
                {
                    response.data = body;
                }
                response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public N1ListResponse N1List(N1ListRequest request)
        {
            N1ListResponse response = new N1ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                //if (request.order.Count > 0)
                //{
                //    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                //    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                //}
               if (request.Mayagt != null)
                {
                    if (request.Mayagt == "1")
                        elem.Add(new XElement("V_Mayagt", request.Mayagt = "1"));
                    if (request.Mayagt == "2")
                        elem.Add(new XElement("V_Mayagt", request.Mayagt = "2"));
                }
                else
                    elem.Add(new XElement("V_Mayagt", null));

                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null && request.DeparmentID != 0)
                {
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                }
                else
                {
                    if ("Branch_Auditor".Equals(User.GetClaimData("USER_TYPE").ToString()))
                    {
                        elem.Add(new XElement("V_DEPARTMENT", Convert.ToInt32(User.GetClaimData("DepartmentID"))));
                    }
                    else
                    {
                        elem.Add(new XElement("V_DEPARTMENT", null));
                    }

                }

                if (request.PARENT_BUDGET_ID != null && request.PARENT_BUDGET_ID != 0)
                    elem.Add(new XElement("V_ParentBudgetID", request.PARENT_BUDGET_ID));
                else
                    elem.Add(new XElement("V_ParentBudgetID", null));

                //if (request.parent_budget_type != null)
                //{
                //    string ss = String.Join(",", request.parent_budget_type.Select(p => p.ToString()).ToArray());
                //    elem.Add(new XElement("V_ParentBudgetID", ss));
                //}
                //else
                //    elem.Add(new XElement("V_ParentBudgetID", null));

                //if (request.parent_budget_type != null) {
                //    string[] ss = request.parent_budget_type.Select(i => i.ToString()).ToArray();
                //    elem.Add(new XElement("V_ParentBudgetID", ss));
                //}
                //else
                //    elem.Add(new XElement("V_ParentBudgetID", null));

                if (request.PeriodID != null) 
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                if (request.TypeID != null && request.TypeID != 0)
                    elem.Add(new XElement("V_TypeID", request.TypeID));
                else
                    elem.Add(new XElement("V_TypeID", null));

                XElement res = AppStatic.SystemController.N1(elem, User.GetClaimData("USER_TYPE")); 
                if (res != null && res.Elements("N1") != null)
                {
                    List<N1> n1 = new List<N1>();
                    //n1 = (from item in res.Elements("N1") select new N1().SetXml(item)).ToList();
                    List<N1> n1Detial = new List<N1>();
                    N1 title = new N1();
                  
                    
                    List<N1> n2 = new List<N1>();
                    n1 =(from item in res.Elements("N1Footer") select new N1().SetXml(item)).ToList();
                   // n2 = (from item in res.Elements("N1") select new N1().SetXml(item)).ToList();
                    N1 nFooter = new N1();

                  /*  foreach (N1 n in n2)
                    {
                        nFooter = n1.Find(a => (a.ORGID.Equals(n.ORGID)));
                        if(nFooter != null)
                        {
                            n1Detial.Add(setMd(nFooter, n));
                        }
                    }*/
                    List<N1> typeNeg = new List<N1>();
                    List<N1> typeHoyor = new List<N1>();
                    List<N1> typeGurav= new List<N1>();

                    List<N1> temp = new List<N1>();
                    List<N1> temp2 = new List<N1>();
                    List<N1> temp3 = new List<N1>();

                    typeNeg = n1.FindAll(a => a.ORGTYPE.Equals("Төсвийн ерөнхийлөн захирагч"));
                    typeHoyor = n1.FindAll(a => a.ORGTYPE.Equals("Төсвийн төвлөрүүлэн захирагч"));
                    typeGurav = n1.FindAll(a => a.ORGTYPE.Equals("Төсвийн шууд захирагч"));

                    if (typeNeg.Count > 0)
                    {
                        title.ORGNAME = "Төсвийн ерөнхийлөн захирагч";
                        temp.Add(title);
                        temp.AddRange(typeNeg);
                        typeNeg = temp;
                        
                    }

                    if (typeHoyor.Count > 0)
                    {
                        title.ORGNAME = "Төсвийн төвлөрүүлэн захирагч";
                        temp2.Add(title);
                        temp2.AddRange(typeHoyor);
                        typeHoyor = temp2;

                    }

                    if (typeGurav.Count > 0)
                    {
                        title.ORGNAME = "Төсвийн шууд захирагч";
                        temp3.Add(title);
                        temp3.AddRange(typeGurav);
                        typeGurav = temp3;

                    }

                    List<N1> types = new List<N1>();
                    types.AddRange(typeNeg);
                    types.AddRange(typeHoyor);
                    types.AddRange(typeGurav);

                    N1 Medeelsen = new N1();
                    N1 Medeeleegui = new N1();
                    N1 HugtsaaHotsorson = new N1();
                    N1 Shaardlaggui = new N1();

                    N1 Niit = new N1();
                    N1 bodolt1 = new N1();
                    N1 bodolt2 = new N1();
                   
                    //n1 = (from item in res.Elements("N1Footer") select new N1().SetXml(item)).ToList();
                    var typ = typeof(N1);
                    var orgname = typ.GetProperty("ORGNAME");
                    decimal total = 0;
                    decimal math1 = 0;
                    decimal math2 = 0;
                    decimal count = 0;
                    foreach (N1 n in n1)
                    {
                        n.OPEN_HEAD_ROLE = n.OPEN_HEAD_ROLE + " " + n.OPEN_HEAD_NAME + " " + n.OPEN_HEAD_PHONE;
                        n.OPEN_ACC_ROLE = n.OPEN_ACC_ROLE + " " + n.OPEN_ACC_NAME + " " + n.OPEN_ACC_PHONE;

                        for (int i = 1; i <= 35; i++)
                        {
                            try { 
                            var prop = typ.GetProperty("MD" + i);
                            string value = prop.GetValue(n) != null ? prop.GetValue(n).ToString() : "";
                            if(value != "")
                             
                            {
                                switch (value)
                                {
                                    case "1":
                                        count = Convert.ToInt32(prop.GetValue(Medeelsen)) + 1;
                                        prop.SetValue(Medeelsen, count.ToString());
                                        orgname.SetValue(Medeelsen, "Мэдээлсэн");
                                        break;
                                    case "2":
                                        count = Convert.ToInt32(prop.GetValue(Medeeleegui)) + 1;
                                        prop.SetValue(Medeeleegui, count.ToString());
                                        orgname.SetValue(Medeeleegui, "Мэдээлээгүй");
                                        break;
                                    case "3":
                                        count = Convert.ToInt32(prop.GetValue(HugtsaaHotsorson)) + 1;
                                        prop.SetValue(HugtsaaHotsorson, count.ToString());
                                        orgname.SetValue(HugtsaaHotsorson, "Хугацаа хоцроосон");
                                        break;
                                    case "4":
                                        count = Convert.ToInt32(prop.GetValue(Shaardlaggui)) + 1;
                                        prop.SetValue(Shaardlaggui, count.ToString());
                                        orgname.SetValue(Shaardlaggui, "Мэдээлэх шаардлагагүй, хамааралгүй");
                                        break;
                                    default:
                                        break;

                                }
                                total = Convert.ToDecimal(prop.GetValue(Medeelsen)) + Convert.ToDecimal(prop.GetValue(Medeeleegui)) + Convert.ToDecimal(prop.GetValue(HugtsaaHotsorson)) + Convert.ToDecimal(prop.GetValue(Shaardlaggui));
                                prop.SetValue(Niit, total.ToString());
                                orgname.SetValue(Niit, "Нийт дүн");

                                if (total != 0)
                                {
                                    math1 = 100 - 100 * Convert.ToDecimal(prop.GetValue(Medeeleegui)) / (total - Convert.ToDecimal(prop.GetValue(Shaardlaggui)));
                                    prop.SetValue(bodolt1, String.Format("{0:0.#}", math1));
                                    orgname.SetValue(bodolt1, "Мэдээлсэн байдлын хэрэгжилтийн хувь");
                                }

                                if (total != 0)
                                {
                                    math2 = 100 - 100 * Convert.ToDecimal(prop.GetValue(HugtsaaHotsorson)) / (total - Convert.ToDecimal(prop.GetValue(Shaardlaggui)));
                                    prop.SetValue(bodolt2, String.Format("{0:0.#}", math2));
                                    orgname.SetValue(bodolt2, "Хугацаа хоцролтын хэрэгжилтийн хувь");
                                }
                            }
                            }
                            catch (Exception ex)
                            {
                                Globals.WriteErrorLog(ex);
                            }
                        }
                    }
                    if (Niit.ORGNAME == null)
                    {
                        orgname.SetValue(Niit, "Нийт дүн");
                    }
                    if (Medeelsen.ORGNAME == null)
                    {
                        orgname.SetValue(Medeelsen, "Мэдээлсэн");
                    }
                    if (Medeeleegui.ORGNAME == null)
                    {
                        orgname.SetValue(Medeeleegui, "Мэдээлээгүй");
                    }
                    if (HugtsaaHotsorson.ORGNAME == null)
                    {
                        orgname.SetValue(HugtsaaHotsorson, "Хугацаа хоцроосон");
                    }
                    if (Shaardlaggui.ORGNAME == null)
                    {
                        orgname.SetValue(Shaardlaggui, "Мэдээлэх шаардлагагүй, хамааралгүй");
                    }
                    if (bodolt1.ORGNAME == null)
                    {
                        orgname.SetValue(bodolt1, "Мэдээлсэн байдлын хэрэгжилтийн хувь");
                    }
                    if (bodolt2.ORGNAME == null)
                    {
                        orgname.SetValue(bodolt2, "Хугацаа хоцролтын хэрэгжилтийн хувь");
                    }

                    n1Detial = types;
                    n1Detial.Add(Niit);
                    n1Detial.Add(Medeelsen);
                    n1Detial.Add(Medeeleegui);
                    n1Detial.Add(HugtsaaHotsorson);
                    n1Detial.Add(Shaardlaggui);
                    n1Detial.Add(bodolt1);
                    n1Detial.Add(bodolt2);

                    response.data = n1Detial;
                }


                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public N1ListResponse Report1N2List(N1ListRequest request)
        {
              N1ListResponse response = new N1ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));
                //if (request.order.Count > 0)
                //{
                //    elem.Add(new XElement("OrderName", request.columns[request.order[0].column].name));
                //    elem.Add(new XElement("OrderDir", request.order[0].dir.ToUpper()));
                //}

                if (request.Mayagt != null)
                {
                    if (request.Mayagt == "1") 
                         elem.Add(new XElement("V_Mayagt", request.Mayagt = "1"));
                    if (request.Mayagt == "2")
                        elem.Add(new XElement("V_Mayagt", request.Mayagt = "2"));
                }
                else
                    elem.Add(new XElement("V_Mayagt", null));


                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null && request.DeparmentID != 0)
                {
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                }
                else
                {
                    if ("Branch_Auditor".Equals(User.GetClaimData("USER_TYPE").ToString()))
                    {
                        elem.Add(new XElement("V_DEPARTMENT", Convert.ToInt32(User.GetClaimData("DepartmentID"))));
                    }
                    else
                    {
                        elem.Add(new XElement("V_DEPARTMENT", null));
                    }
                    
                }

                if (request.PARENT_BUDGET_ID != null && request.PARENT_BUDGET_ID != 0)
                    elem.Add(new XElement("V_ParentBudgetID", request.PARENT_BUDGET_ID));
                else
                    elem.Add(new XElement("V_ParentBudgetID", null));


                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                if (request.TypeID != null && request.TypeID != 0)
                    elem.Add(new XElement("V_TypeID", request.TypeID));
                else
                    elem.Add(new XElement("V_TypeID", null));


                XElement res = AppStatic.SystemController.Report1N2(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("Report1N2") != null)
                {
                    List<N1> n1 = new List<N1>();
                    //n1 = (from item in res.Elements("N1") select new N1().SetXml(item)).ToList();
                    List<N1> n1Detial = new List<N1>();
                    N1 title = new N1();


                    List<N1> n2 = new List<N1>();
                    n1 = (from item in res.Elements("Report1N2Footer") select new N1().SetXml(item)).ToList();
                   // n2 = (from item in res.Elements("Report1N2") select new N1().SetXml(item)).ToList();
                    N1 nFooter = new N1();

                 /*   foreach (N1 n in n2)
                    {
                        nFooter = n1.Find(a => (a.ORGID.Equals(n.ORGID)));
                        if (nFooter != null)
                        {
                            n1Detial.Add(setMd(nFooter, n));
                        }
                    }*/
                    List<N1> typeNeg = new List<N1>();
                    List<N1> typeHoyor = new List<N1>();
                    List<N1> typeGurav = new List<N1>();

                    List<N1> temp = new List<N1>();
                    List<N1> temp2 = new List<N1>();
                    List<N1> temp3 = new List<N1>();

                    typeNeg = n1.FindAll(a => a.ORGTYPE.Equals("Төсвийн ерөнхийлөн захирагч"));
                    typeHoyor = n1.FindAll(a => a.ORGTYPE.Equals("Төсвийн төвлөрүүлэн захирагч"));
                    typeGurav = n1.FindAll(a => a.ORGTYPE.Equals("Төсвийн шууд захирагч"));

                    if (typeNeg.Count > 0)
                    {
                        title = new N1();
                        title.ORGNAME = "Төсвийн ерөнхийлөн захирагч";
                        temp.Add(title);
                        temp.AddRange(typeNeg);
                        typeNeg = temp;

                    }

                    if (typeHoyor.Count > 0)
                    {
                        title = new N1();
                        title.ORGNAME = "Төсвийн төвлөрүүлэн захирагч";
                        temp2.Add(title);
                        temp2.AddRange(typeHoyor);
                        typeHoyor = temp2;

                    }

                    if (typeGurav.Count > 0)
                    {
                        title = new N1();
                        title.ORGNAME = "Төсвийн шууд захирагч";
                        temp3.Add(title);
                        temp3.AddRange(typeGurav);
                        typeGurav = temp3;

                    }

                    List<N1> types = new List<N1>();
                    types.AddRange(typeNeg);
                    types.AddRange(typeHoyor);
                    types.AddRange(typeGurav);

                    N1 Medeelsen = new N1();
                    N1 Medeeleegui = new N1();
                    N1 HugtsaaHotsorson = new N1();
                    N1 Shaardlaggui = new N1();

                    N1 Niit = new N1();
                    N1 bodolt1 = new N1();
                    N1 bodolt2 = new N1();

                   // n1 = (from item in res.Elements("Report1N2Footer") select new N1().SetXml(item)).ToList();
                    var typ = typeof(N1);
                    var orgname = typ.GetProperty("ORGNAME");
                    decimal total = 0;
                    decimal math1 = 0;
                    decimal math2 = 0;
                    decimal count = 0;

                    string[] key = {"33","34","37","38","39","40","41","42","43","44","46","47","48","49","50","51","60","61","62","63","64","65","53","54","55","56",
                                    "57","58","66","67","69","70","71","72","73","74","76","77","78","79","80","81","83","84","85","86","87","88","90","91","92","93","94",
                                    "95","97","98","99","100","101","102","161","105","106","165","166","167","168","169" };

                    foreach (N1 n in n1)
                    {
                        n.OPEN_HEAD_ROLE = n.OPEN_HEAD_ROLE + " " + n.OPEN_HEAD_NAME + " " + n.OPEN_HEAD_PHONE;
                        n.OPEN_ACC_ROLE = n.OPEN_ACC_ROLE + " " + n.OPEN_ACC_NAME + " " + n.OPEN_ACC_PHONE;
                        for (int i = 0; i < key.Length; i++)
                         
                        {
                            try { 
                            var prop = typ.GetProperty("MD" + key[i]);
                            string value = prop.GetValue(n) != null ? prop.GetValue(n).ToString() : "0";
                            if (key[i].Equals("33") || key[i].Equals("34") ||  key[i].Equals("66") || key[i].Equals("67"))
                            {
                                if (value != "")

                                {
                                    switch (value)
                                    {
                                        case "1":
                                            count = Convert.ToInt32(prop.GetValue(Medeelsen)) + 1;
                                            prop.SetValue(Medeelsen, count.ToString());
                                            orgname.SetValue(Medeelsen, "Мэдээлсэн");
                                            break;
                                        case "2":
                                            count = Convert.ToInt32(prop.GetValue(Medeeleegui)) + 1;
                                            prop.SetValue(Medeeleegui, count.ToString());
                                            orgname.SetValue(Medeeleegui, "Мэдээлээгүй");
                                            break;
                                        case "3":
                                            count = Convert.ToInt32(prop.GetValue(HugtsaaHotsorson)) + 1;
                                            prop.SetValue(HugtsaaHotsorson, count.ToString());
                                            orgname.SetValue(HugtsaaHotsorson, "Хугацаа хоцроосон");
                                            break;
                                        case "4":
                                            count = Convert.ToInt32(prop.GetValue(Shaardlaggui)) + 1;
                                            prop.SetValue(Shaardlaggui, count.ToString());
                                            orgname.SetValue(Shaardlaggui, "Мэдээлэх шаардлагагүй, хамааралгүй");
                                            break;
                                        default:
                                            break;

                                    }
                                    total = Convert.ToInt32(prop.GetValue(Medeelsen)) + Convert.ToInt32(prop.GetValue(Medeeleegui)) + Convert.ToInt32(prop.GetValue(HugtsaaHotsorson)) + Convert.ToInt32(prop.GetValue(Shaardlaggui));
                                    prop.SetValue(Niit, total.ToString());
                                    orgname.SetValue(Niit, "Нийт дүн");

                                    if (total != 0)
                                    {
                                        math1 = 100 - 100 * Convert.ToDecimal(prop.GetValue(Medeeleegui)) / ((total - Convert.ToDecimal(prop.GetValue(Shaardlaggui))) == 0 ?1: (total - Convert.ToDecimal(prop.GetValue(Shaardlaggui))));
                                        prop.SetValue(bodolt1, String.Format("{0:0.#}", math1));
                                        orgname.SetValue(bodolt1, "Мэдээлсэн байдлын хэрэгжилтийн хувь");
                                    }

                                    if (total != 0)
                                    {
                                        math2 = 100 - 100 * Convert.ToDecimal(prop.GetValue(HugtsaaHotsorson)) / ((total - Convert.ToDecimal(prop.GetValue(Shaardlaggui))) == 0?1: (total - Convert.ToDecimal(prop.GetValue(Shaardlaggui))));
                                        prop.SetValue(bodolt2, String.Format("{0:0.#}", math2));
                                        orgname.SetValue(bodolt2, "Хугацаа хоцролтын хэрэгжилтийн хувь");
                                    }
                                }
                            }
                            else {
                                if (prop.GetValue(n) != null && prop.GetValue(n) != "")
                                {
                                        decimal too = Convert.ToDecimal(prop.GetValue(n));
                                    prop.SetValue(n,String.Format("{0:#,###.##}", too));
                                }
                                decimal tempToo = !String.IsNullOrEmpty(value) ? Convert.ToDecimal(value) : 0;
                                String test = (string)prop.GetValue(Niit);
                                decimal tempToo2 =!String.IsNullOrEmpty(test) ? Convert.ToDecimal(prop.GetValue(Niit)) : 0;
                                total = tempToo2 + tempToo;
                                prop.SetValue(Niit, total != 0 ? String.Format("{0:#,###.##}", total) : "");
                                orgname.SetValue(Niit, "Нийт дүн");
                                //math1 = 100 - 100 * Convert.ToInt32(prop.GetValue(Medeeleegui)) / total - Convert.ToInt32(prop.GetValue(HugtsaaHotsorson));
                            }
                            }
                            catch (Exception ex)
                            {
                                Globals.WriteErrorLog(ex);
                            }

                        }
                    }
                    if (Niit.ORGNAME == null)
                    {
                        orgname.SetValue(Niit, "Нийт дүн");
                    }
                    if (Medeelsen.ORGNAME == null)
                    {
                        orgname.SetValue(Medeelsen, "Мэдээлсэн");
                    }
                    if (Medeeleegui.ORGNAME == null)
                    {
                        orgname.SetValue(Medeeleegui, "Мэдээлээгүй");
                    }
                    if (HugtsaaHotsorson.ORGNAME == null)
                    {
                        orgname.SetValue(HugtsaaHotsorson, "Хугацаа хоцроосон");
                    }
                    if (Shaardlaggui.ORGNAME == null)
                    {
                        orgname.SetValue(Shaardlaggui, "Мэдээлэх шаардлагагүй, хамааралгүй");
                    }
                    if (bodolt1.ORGNAME == null)
                    {
                        orgname.SetValue(bodolt1, "Мэдээлсэн байдлын хэрэгжилтийн хувь");
                    }
                    if (bodolt2.ORGNAME == null)
                    {
                        orgname.SetValue(bodolt2, "Хугацаа хоцролтын хэрэгжилтийн хувь");
                    }
                    if (n1.Count > 0)
                    {
                  /*  math1 = (!String.IsNullOrEmpty(Niit.MD37) ? Convert.ToDecimal(Niit.MD37) : 0) - (!String.IsNullOrEmpty(Niit.MD39) ? Convert.ToDecimal(Niit.MD39) : 0) - (!String.IsNullOrEmpty(Niit.MD41) ? Convert.ToDecimal(Niit.MD41) : 0);
                    Medeelsen.MD37 = math1 != 0 ? String.Format("{0:0.#}", math1) : "";
                    Medeeleegui.MD37 = Niit.MD39;
                    HugtsaaHotsorson.MD37 = Niit.MD41;*/
                    math1 = 100 - (!String.IsNullOrEmpty(Niit.MD39)? Convert.ToDecimal(Niit.MD39):0) * 100 /  Convert.ToDecimal(!String.IsNullOrEmpty(Niit.MD37)?Niit.MD37:"1");
                    bodolt1.MD37 = math1 != 0 ? String.Format("{0:0.#}", math1) : "";
                    math1 = 100 - (!String.IsNullOrEmpty(Niit.MD40) ? Convert.ToDecimal(Niit.MD40) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(Niit.MD38) ? Niit.MD38 : "1");
                    bodolt1.MD38 = math1 != 0 ? String.Format("{0:0.#}", math1) : "";
                    math1 = 100 - (!String.IsNullOrEmpty(Niit.MD42) ? Convert.ToDecimal(Niit.MD42) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(Niit.MD38) ? Niit.MD38 : "1");
                    bodolt2.MD42 = math1 != 0 ? String.Format("{0:0.#}", math1) : "";
                    math1 = 100 - (!String.IsNullOrEmpty(Niit.MD41) ? Convert.ToDecimal(Niit.MD41) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(Niit.MD37) ? Niit.MD37 : "1"); 
                    bodolt2.MD41 = math1 != 0 ? String.Format("{0:0.#}", math1) : "";
                  
                        List<string> list = new List<string>(key);
                    list.RemoveAt(list.IndexOf("66"));
                    list.RemoveAt(list.IndexOf("67"));
                   
                        for (int i = 10; i < list.Count - 8;)
                        {
                            try
                            {
                                var niitMedeeleegui = typ.GetProperty("MD" + list[i + 2]);
                                string niitMedeeleeguiStr = niitMedeeleegui.GetValue(Niit) != null ? niitMedeeleegui.GetValue(Niit).ToString() : "0";
                                var niitMedeelsen = typ.GetProperty("MD" + list[i]);
                                string niitMedeelsenStr = niitMedeelsen.GetValue(Niit) != null ? niitMedeelsen.GetValue(Niit).ToString() : "0";
                                var niitMedeeleeguiMungu = typ.GetProperty("MD" + list[i + 3]);
                                string niitMedeeleeguiMunguStr = niitMedeeleeguiMungu.GetValue(Niit) != null ? niitMedeeleeguiMungu.GetValue(Niit).ToString() : "0";
                                var niitMedeelsenMungu = typ.GetProperty("MD" + list[i + 1]);
                                string niitMedeelsenMunguStr = niitMedeelsenMungu.GetValue(Niit) != null ? niitMedeelsenMungu.GetValue(Niit).ToString() : "0";


                                math1 = 100 - (!String.IsNullOrEmpty(niitMedeeleeguiStr) ? Convert.ToDecimal(niitMedeeleeguiStr) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(niitMedeelsenStr) ? niitMedeelsenStr : "1");
                                niitMedeeleegui.SetValue(bodolt1, math1 != 0 ? String.Format("{0:0.#}", math1) : "");
                                math1 = 100 - (!String.IsNullOrEmpty(niitMedeeleeguiMunguStr) ? Convert.ToDecimal(niitMedeeleeguiMunguStr) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(niitMedeelsenMunguStr) ? niitMedeelsenMunguStr : "1");
                                niitMedeeleeguiMungu.SetValue(bodolt1, math1 != 0 ? String.Format("{0:0.#}", math1) : "");

                                var niitKhugtsaaToo = typ.GetProperty("MD" + list[i + 4]);
                                string niitKhugtsaaTooStr = niitKhugtsaaToo.GetValue(Niit) != null ? niitKhugtsaaToo.GetValue(Niit).ToString() : "0";

                                var niitKhugtsaaMungu = typ.GetProperty("MD" + list[i + 5]);
                                string niitKhugtsaaMunguStr = niitKhugtsaaMungu.GetValue(Niit) != null ? niitKhugtsaaMungu.GetValue(Niit).ToString() : "0";



                                math1 = 100 - (!String.IsNullOrEmpty(niitKhugtsaaTooStr) ? Convert.ToDecimal(niitKhugtsaaTooStr) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(niitMedeelsenStr) ? niitMedeelsenStr : "1");
                                niitKhugtsaaToo.SetValue(bodolt2, math1 != 0 ? String.Format("{0:0.#}", math1) : "");
                                math1 = 100 - (!String.IsNullOrEmpty(niitKhugtsaaMunguStr) ? Convert.ToDecimal(niitKhugtsaaMunguStr) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(niitMedeelsenMunguStr) ? niitMedeelsenMunguStr : "1");
                                niitKhugtsaaMungu.SetValue(bodolt2, math1 != 0 ? String.Format("{0:0.#}", math1) : "");
                                i += 6;
                            }
                            catch (Exception ex)
                            {
                                Globals.WriteErrorLog(ex);
                            }
                        }
                    }

                    n1Detial = types;
                    n1Detial.Add(Niit);
                    n1Detial.Add(Medeelsen);
                    n1Detial.Add(Medeeleegui);
                    n1Detial.Add(HugtsaaHotsorson);
                    n1Detial.Add(Shaardlaggui);
                    n1Detial.Add(bodolt1);
                    n1Detial.Add(bodolt2);

                    response.data = n1Detial;
                }


                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
        [HttpPost]
        public N1ListResponse ReportN2List(N1ListRequest request)
        {
            N1ListResponse response = new N1ListResponse();
            try
            {
                XElement elem = new XElement("Request");

                /*elem.Add(new XElement("PageSize", request.length == -1 ? int.MaxValue : request.length));
                elem.Add(new XElement("PageNumber", request.start));*/
           

                elem.Add(new XElement("V_Mayagt", request.Mayagt = "3"));
                /*elem.Add(new XElement("V_Mayagt", request.Mayagt = "3"));*/


                if (!string.IsNullOrEmpty(request.search.value))
                    elem.Add(new XElement("Search", request.search.value));
                else
                    elem.Add(new XElement("Search", null));

                if (request.DeparmentID != null && request.DeparmentID != 0)
                {
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                }
                else
                {
                    if ("Branch_Auditor".Equals(User.GetClaimData("USER_TYPE").ToString()))
                    {
                        elem.Add(new XElement("V_DEPARTMENT", Convert.ToInt32(User.GetClaimData("DepartmentID"))));
                    }
                    else
                    {
                        elem.Add(new XElement("V_DEPARTMENT", null));
                    }

                }

                if (request.PARENT_BUDGET_ID != null && request.PARENT_BUDGET_ID != 0)
                    elem.Add(new XElement("V_ParentBudgetID", request.PARENT_BUDGET_ID));
                else
                    elem.Add(new XElement("V_ParentBudgetID", null));

                if (request.PeriodID != null)
                    elem.Add(new XElement("V_PERIOD", request.PeriodID));
                else
                    elem.Add(new XElement("V_PERIOD", null));

                if (request.TypeID != null && request.TypeID != 0)
                    elem.Add(new XElement("V_TypeID", request.TypeID));
                else
                    elem.Add(new XElement("V_TypeID", null));


                XElement res = AppStatic.SystemController.ReportN2(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("ReportN2") != null)
                {
                    List<N1> n1 = new List<N1>();
                    //n1 = (from item in res.Elements("N1") select new N1().SetXml(item)).ToList();
                    List<N1> n1Detial = new List<N1>();
                    N1 title = new N1();


                    List<N1> n2 = new List<N1>();
                    n1 = (from item in res.Elements("ReportN2") select new N1().SetXml(item)).ToList();
                    
                    

                    List<N1> typeNeg = new List<N1>();
                    List<N1> typeHoyor = new List<N1>();
                    List<N1> typeGurav = new List<N1>();

                    List<N1> temp = new List<N1>();
                    List<N1> temp2 = new List<N1>();
                    List<N1> temp3 = new List<N1>();

                    typeNeg = n1.FindAll(a => a.ORGTYPE.Equals("Төсвийн ерөнхийлөн захирагч"));
                    typeHoyor = n1.FindAll(a => a.ORGTYPE.Equals("Төсвийн төвлөрүүлэн захирагч"));
                    typeGurav = n1.FindAll(a => a.ORGTYPE.Equals("Төсвийн шууд захирагч"));

                    if (typeNeg.Count > 0)
                    {
                        title = new N1();
                        title.ORGNAME = "Төсвийн ерөнхийлөн захирагч";
                        temp.Add(title);
                        temp.AddRange(typeNeg);
                        typeNeg = temp;

                    }

                    if (typeHoyor.Count > 0)
                    {
                        title = new N1();
                        title.ORGNAME = "Төсвийн төвлөрүүлэн захирагч";
                        temp2.Add(title);
                        temp2.AddRange(typeHoyor);
                        typeHoyor = temp2;

                    }

                    if (typeGurav.Count > 0)
                    {
                        title = new N1();
                        title.ORGNAME = "Төсвийн шууд захирагч";
                        temp3.Add(title);
                        temp3.AddRange(typeGurav);
                        typeGurav = temp3;

                    }

                    List<N1> types = new List<N1>();
                    types.AddRange(typeNeg);
                    types.AddRange(typeHoyor);
                    types.AddRange(typeGurav);

                    N1 Medeelsen = new N1();
                    N1 Medeeleegui = new N1();
                    N1 HugtsaaHotsorson = new N1();
                    N1 Shaardlaggui = new N1();

                    N1 Niit = new N1();
                    N1 bodolt1 = new N1();
                    N1 bodolt2 = new N1();

                   // n1 = (from item in res.Elements("Report1N2Footer") select new N1().SetXml(item)).ToList();
                    var typ = typeof(N1);
                    var orgname = typ.GetProperty("ORGNAME");
                    decimal total = 0;
                    decimal math1 = 0;
                    decimal math2 = 0;
                    decimal count = 0;

                    string[] key = {"116","117","118","119","120","121",
                                    "123","124","125","126","127","128",
                                    "130","131","132","133","134","135",
                                    "137","138","139","140","141","142",
                                    "143",
                                    "144",
                                    "146","147","148","149","150","151","152","153",
                                    "155","156","157","158","159","160",
                                    "104","105","106",
                                    "107","108","109","110","113" };

                    foreach (N1 n in n1)
                    {
                        n.OPEN_HEAD_ROLE = n.OPEN_HEAD_ROLE + " " + n.OPEN_HEAD_NAME + " " + n.OPEN_HEAD_PHONE;
                        n.OPEN_ACC_ROLE = n.OPEN_ACC_ROLE + " " + n.OPEN_ACC_NAME + " " + n.OPEN_ACC_PHONE;
                        for (int i = 0; i < key.Length; i++)

                        {
                            try
                            {

                                var prop = typ.GetProperty("MD" + key[i]);
                                string value = prop.GetValue(n) != null ? prop.GetValue(n).ToString() : "0";
                                if (key[i].Equals("143") || key[i].Equals("144"))
                                {
                                    if (value != "")

                                    {
                                        switch (value)
                                        {
                                            case "1":
                                                count = Convert.ToInt32(prop.GetValue(Medeelsen)) + 1;
                                                prop.SetValue(Medeelsen, count.ToString());
                                                orgname.SetValue(Medeelsen, "Мэдээлсэн");
                                                prop.SetValue(n, "1");
                                                break;
                                            case "2":
                                                count = Convert.ToInt32(prop.GetValue(Medeeleegui)) + 1;
                                                prop.SetValue(Medeeleegui, count.ToString());
                                                orgname.SetValue(Medeeleegui, "Мэдээлээгүй");
                                                prop.SetValue(n, "1");
                                                break;
                                            case "3":
                                                count = Convert.ToInt32(prop.GetValue(HugtsaaHotsorson)) + 1;
                                                prop.SetValue(HugtsaaHotsorson, count.ToString());
                                                orgname.SetValue(HugtsaaHotsorson, "Хугацаа хоцроосон");
                                                prop.SetValue(n, "1");
                                                break;
                                            case "4":
                                                count = Convert.ToInt32(prop.GetValue(Shaardlaggui)) + 1;
                                                prop.SetValue(Shaardlaggui, count.ToString());
                                                orgname.SetValue(Shaardlaggui, "Мэдээлэх шаардлагагүй, хамааралгүй");
                                                prop.SetValue(n, "1");
                                                break;
                                            default:
                                                break;

                                        }

                                        total = Convert.ToInt32(prop.GetValue(Medeelsen)) + Convert.ToInt32(prop.GetValue(Medeeleegui)) + Convert.ToInt32(prop.GetValue(HugtsaaHotsorson)) + Convert.ToInt32(prop.GetValue(Shaardlaggui));
                                        prop.SetValue(Niit, total.ToString());
                                        orgname.SetValue(Niit, "Нийт дүн");
                                        prop.SetValue(n, "1");

                                        if (total != 0)
                                        {
                                            math1 = 100 - 100 * Convert.ToDecimal(prop.GetValue(Medeeleegui)) / ((total - Convert.ToDecimal(prop.GetValue(Shaardlaggui))) == 0 ? 1 : (total - Convert.ToDecimal(prop.GetValue(Shaardlaggui))));
                                            prop.SetValue(bodolt1, String.Format("{0:0.#}", math1));
                                            orgname.SetValue(bodolt1, "Мэдээлсэн байдлын хэрэгжилтийн хувь");
                                        }

                                        if (total != 0)
                                        {
                                            math2 = 100 - 100 * Convert.ToDecimal(prop.GetValue(HugtsaaHotsorson)) / ((total - Convert.ToDecimal(prop.GetValue(Shaardlaggui))) == 0 ? 1 : (total - Convert.ToDecimal(prop.GetValue(Shaardlaggui))));
                                            prop.SetValue(bodolt2, String.Format("{0:0.#}", math2));
                                            orgname.SetValue(bodolt2, "Хугацаа хоцролтын хэрэгжилтийн хувь");
                                        }
                                    }

                                }
                                else
                                {
                                    if (prop.GetValue(n) != null && prop.GetValue(n) != "")
                                    {
                                        decimal too = Convert.ToDecimal(prop.GetValue(n));
                                        prop.SetValue(n, String.Format("{0:#,###.##}", too));
                                    }
                                    decimal tempToo = !String.IsNullOrEmpty(value) ? Convert.ToDecimal(value) : 0;
                                    String test = (string)prop.GetValue(Niit);
                                    decimal tempToo2 = !String.IsNullOrEmpty(test) ? Convert.ToDecimal(prop.GetValue(Niit)) : 0;
                                    total = tempToo2 + tempToo;
                                    prop.SetValue(Niit, total != 0 ? String.Format("{0:0.#}", total) : "");
                                    orgname.SetValue(Niit, "Нийт дүн");
                                    //math1 = 100 - 100 * Convert.ToInt32(prop.GetValue(Medeeleegui)) / total - Convert.ToInt32(prop.GetValue(HugtsaaHotsorson));
                                }

                            }catch (Exception ex)
                            {
                                Globals.WriteErrorLog(ex);
                            }
                        }
                    }
                    if (Niit.ORGNAME == null)
                    {
                        orgname.SetValue(Niit, "Нийт дүн");
                    }
                    if (Medeelsen.ORGNAME == null)
                    {
                        orgname.SetValue(Medeelsen, "Мэдээлсэн");
                    }
                    if (Medeeleegui.ORGNAME == null)
                    {
                        orgname.SetValue(Medeeleegui, "Мэдээлээгүй");
                    }
                    if (HugtsaaHotsorson.ORGNAME == null)
                    {
                        orgname.SetValue(HugtsaaHotsorson, "Хугацаа хоцроосон");
                    }
                    if (Shaardlaggui.ORGNAME == null)
                    {
                        orgname.SetValue(Shaardlaggui, "Мэдээлэх шаардлагагүй, хамааралгүй");
                    }
                    if (bodolt1.ORGNAME == null)
                    {
                        orgname.SetValue(bodolt1, "Мэдээлсэн байдлын хэрэгжилтийн хувь");
                    }
                    if (bodolt2.ORGNAME == null)
                    {
                        orgname.SetValue(bodolt2, "Хугацаа хоцролтын хэрэгжилтийн хувь");
                    }
                    if (n1.Count > 0)
                    {

                        string[] key2 = {"116","117","118","119","120","121",
                                    "123","124","125","126","127","128",
                                    "130","131","132","133","134","135",
                                    "137","138","139","140","141","142",
                                    "146","147","148","149","150","151",
                                    "155","156","157","158","159","160",
                                    };
                        for (int i = 0; i < key2.Length;)
                        {
                            try
                            {

                                var niitMedeeleegui = typ.GetProperty("MD" + key2[i + 2]);
                                string niitMedeeleeguiStr = niitMedeeleegui.GetValue(Niit) != null ? niitMedeeleegui.GetValue(Niit).ToString() : "0";
                                var niitMedeelsen = typ.GetProperty("MD" + key2[i]);
                                string niitMedeelsenStr = niitMedeelsen.GetValue(Niit) != null ? niitMedeelsen.GetValue(Niit).ToString() : "0";
                                var niitMedeeleeguiMungu = typ.GetProperty("MD" + key2[i + 3]);
                                string niitMedeeleeguiMunguStr = niitMedeeleeguiMungu.GetValue(Niit) != null ? niitMedeeleeguiMungu.GetValue(Niit).ToString() : "0";
                                var niitMedeelsenMungu = typ.GetProperty("MD" + key2[i + 1]);
                                string niitMedeelsenMunguStr = niitMedeelsenMungu.GetValue(Niit) != null ? niitMedeelsenMungu.GetValue(Niit).ToString() : "0";


                                math1 = 100 - (!String.IsNullOrEmpty(niitMedeeleeguiStr) ? Convert.ToDecimal(niitMedeeleeguiStr) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(niitMedeelsenStr) ? niitMedeelsenStr : "1");
                                niitMedeeleegui.SetValue(bodolt1, math1 != 0 ? String.Format("{0:0.#}", math1) : "");
                                math1 = 100 - (!String.IsNullOrEmpty(niitMedeeleeguiMunguStr) ? Convert.ToDecimal(niitMedeeleeguiMunguStr) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(niitMedeelsenMunguStr) ? niitMedeelsenMunguStr : "1");
                                niitMedeeleeguiMungu.SetValue(bodolt1, math1 != 0 ? String.Format("{0:0.#}", math1) : "");

                                var niitKhugtsaaToo = typ.GetProperty("MD" + key2[i + 4]);
                                string niitKhugtsaaTooStr = niitKhugtsaaToo.GetValue(Niit) != null ? niitKhugtsaaToo.GetValue(Niit).ToString() : "0";

                                var niitKhugtsaaMungu = typ.GetProperty("MD" + key2[i + 5]);
                                string niitKhugtsaaMunguStr = niitKhugtsaaMungu.GetValue(Niit) != null ? niitKhugtsaaMungu.GetValue(Niit).ToString() : "0";



                                math1 = 100 - (!String.IsNullOrEmpty(niitKhugtsaaTooStr) ? Convert.ToDecimal(niitKhugtsaaTooStr) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(niitMedeelsenStr) ? niitMedeelsenStr : "1");
                                niitKhugtsaaToo.SetValue(bodolt2, math1 != 0 ? String.Format("{0:0.#}", math1) : "");
                                math1 = 100 - (!String.IsNullOrEmpty(niitKhugtsaaMunguStr) ? Convert.ToDecimal(niitKhugtsaaMunguStr) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(niitMedeelsenMunguStr) ? niitMedeelsenMunguStr : "1");
                                niitKhugtsaaMungu.SetValue(bodolt2, math1 != 0 ? String.Format("{0:0.#}", math1) : "");
                                i += 6;
                            }
                            catch (Exception ex)
                            {
                                Globals.WriteErrorLog(ex);
                            }
                        }
                        math1 = 100 - (!String.IsNullOrEmpty(Niit.MD105) ? Convert.ToDecimal(Niit.MD105) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(Niit.MD104) ? Niit.MD104 : "1");
                        bodolt1.MD104 = math1 != 0 ? String.Format("{0:0.#}", math1) : "";
                        math1 = 100 - (!String.IsNullOrEmpty(Niit.MD106) ? Convert.ToDecimal(Niit.MD106) : 0) * 100 / Convert.ToDecimal(!String.IsNullOrEmpty(Niit.MD104) ? Niit.MD104 : "1");
                        bodolt2.MD106 = math1 != 0 ? String.Format("{0:0.#}", math1) : "";
                    }

                    n1Detial = types;
                    n1Detial.Add(Niit);
                    n1Detial.Add(Medeelsen);
                    n1Detial.Add(Medeeleegui);
                    n1Detial.Add(HugtsaaHotsorson);
                    n1Detial.Add(Shaardlaggui);
                    n1Detial.Add(bodolt1);
                    n1Detial.Add(bodolt2);

                    response.data = n1Detial;
                }


                response.recordsFiltered = response.recordsTotal;
                response.draw = request.draw;
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return response;
        }
      

    }
}
