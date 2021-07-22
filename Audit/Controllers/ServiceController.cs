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
                List<BM0> bm0 = new List<BM0>();
                BM0 Niit = new BM0();
                var typ = typeof(BM0);
                if (res != null && res.Elements("BM0") != null)
                {
                    bm0Body = (from item in res.Elements("BM0") select new BM0().SetXml(item)).ToList();
                    response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
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

                    foreach (BM0 nm3 in bm0Body)
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
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    time.SetValue(Niit, COUNT);
                    day.SetValue(Niit, COUNT1);
                    person.SetValue(Niit, COUNT2);
                    included.SetValue(Niit, COUNT3);

                    depname.SetValue(Niit, "НИЙТ ДҮН");


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
                List<BM1List> list = new List<BM1List>();
                BM1List Niit = new BM1List();
                var typ = typeof(BM1List);
                if (res != null && res.Elements("BM1") != null)
                    Body = (from item in res.Elements("BM1") select new BM1List().SetXml(item)).ToList();

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


                    foreach (BM1List data in Body)
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
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.##"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.##"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.##"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.##"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.##"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.##"));
                    pay11.SetValue(Niit, AMOUNT11.ToString("#,0.##"));
                    pay12.SetValue(Niit, AMOUNT12.ToString("#,0.##"));
                    pay13.SetValue(Niit, AMOUNT13.ToString("#,0.##"));
                    pay14.SetValue(Niit, AMOUNT14.ToString("#,0.##"));
                    pay15.SetValue(Niit, AMOUNT15.ToString("#,0.##"));
                    pay16.SetValue(Niit, AMOUNT16.ToString("#,0.##"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);


                    depname.SetValue(Niit, "НИЙТ ДҮН");

                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
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
                List<BM2List> list = new List<BM2List>();
                BM2List Niit = new BM2List();
                var typ = typeof(BM2List);
                if (res != null && res.Elements("BM2") != null)
                {
                    Body = (from item in res.Elements("BM2") select new BM2List().SetXml(item)).ToList();
                    response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
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

                    foreach (BM2List data in Body)
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
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.##"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.##"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.##"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.##"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.##"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.##"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);


                    depname.SetValue(Niit, "НИЙТ ДҮН");


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
                List<BM3List> list = new List<BM3List>();
                BM3List Niit = new BM3List();
                var typ = typeof(BM3List);
                if (res != null && res.Elements("BM3") != null)
                    Body = (from item in res.Elements("BM3") select new BM3List().SetXml(item)).ToList();

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

                    foreach (BM3List data in Body)
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
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.##"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);
                    count6.SetValue(Niit, NUMBER6);


                    depname.SetValue(Niit, "НИЙТ ДҮН");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
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
                List<BM4List> list = new List<BM4List>();
                BM4List Niit = new BM4List();
                var typ = typeof(BM4List);
                if (res != null && res.Elements("BM4") != null)
                    Body = (from item in res.Elements("BM4") select new BM4List().SetXml(item)).ToList();


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

                    foreach (BM4List data in Body)
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
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);


                    depname.SetValue(Niit, "НИЙТ ДҮН");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
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
                List<BM5List> list = new List<BM5List>();
                BM5List Niit = new BM5List();
                var typ = typeof(BM5List);
                if (res != null && res.Elements("BM5") != null)
                    Body = (from item in res.Elements("BM5") select new BM5List().SetXml(item)).ToList();

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

                    foreach (BM5List data in Body)
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
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);


                    depname.SetValue(Niit, "НИЙТ ДҮН");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
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
                List<BM6> list = new List<BM6>();
                BM6 Niit = new BM6();
                var typ = typeof(BM6);
                if (res != null && res.Elements("BM6") != null)
                    Body = (from item in res.Elements("BM6") select new BM6().SetXml(item)).ToList();
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

                    foreach (BM6 data in Body)
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
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.##"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.##"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.##"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.##"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.##"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.##"));

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


                    depname.SetValue(Niit, "НИЙТ ДҮН");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
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
                List<BM7> list = new List<BM7>();
                BM7 Niit = new BM7();
                var typ = typeof(BM7);
                if (res != null && res.Elements("BM7") != null)
                    Body = (from item in res.Elements("BM7") select new BM7().SetXml(item)).ToList();

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

                    foreach (BM7 data in Body)
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
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.##"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.##"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.##"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.##"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.##"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.##"));

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


                    depname.SetValue(Niit, "НИЙТ ДҮН");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
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
                List<BM8List> list = new List<BM8List>();
                BM8List Niit = new BM8List();
                var typ = typeof(BM8List);
                if (res != null && res.Elements("BM8") != null)
                    Body = (from item in res.Elements("BM8") select new BM8List().SetXml(item)).ToList();
                if (Body.Count > 0)
                {
                    var depname = typ.GetProperty("DEPARTMENT_NAME");

                    var pay = typ.GetProperty("CORRECTED_AMOUNT");

                    var count = typ.GetProperty("CORRECTED_COUNT");

                    Decimal AMOUNT = 0;

                    int NUMBER = 0;

                    foreach (BM8List data in Body)
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
                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));

                    count.SetValue(Niit, NUMBER);


                    depname.SetValue(Niit, "НИЙТ ДҮН");


                    list = Body;
                    list.Add(Niit);

                    response.data = list;
                }
                else
                {
                    response.data = Body;
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

                if (res != null && res.Elements("NM1") != null)
                    body = (from item in res.Elements("NM1") select new NM1().SetXml(item)).ToList();
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

                    foreach (NM1 data in body)
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


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.##"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.##"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.##"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.##"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.##"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.##"));
                    pay11.SetValue(Niit, AMOUNT11.ToString("#,0.##"));
                    pay12.SetValue(Niit, AMOUNT12.ToString("#,0.##"));

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

                    depname.SetValue(Niit, "НИЙТ ДҮН");


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
                List<NM2> total = new List<NM2>();
                NM2 Niit = new NM2();
                if (res != null && res.Elements("NM2") != null)
                    body = (from item in res.Elements("NM2") select new NM2().SetXml(item)).ToList();
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

                    foreach (NM2 data in body)
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


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.##"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.##"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.##"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.##"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.##"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.##"));
                    pay11.SetValue(Niit, AMOUNT11.ToString("#,0.##"));
                    pay12.SetValue(Niit, AMOUNT12.ToString("#,0.##"));

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

                    depname.SetValue(Niit, "НИЙТ ДҮН");


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
                List<NM3> total = new List<NM3>();
                NM3 Niit = new NM3();
                var typ = typeof(NM3);
                if (res != null && res.Elements("NM3") != null)
                    body = (from item in res.Elements("NM3") select new NM3().SetXml(item)).ToList();
                if (body.Count > 0)
                {
                    int COUNTRE = 0;
                    Decimal AMOUNTRE = 0;
                    foreach (NM3 nm3 in body)
                    {
                        if (nm3.REFERENCE_COUNT != null && nm3.COMPLETION_DONE_COUNT != null)
                        {
                            COUNTRE = Convert.ToInt32(nm3.REFERENCE_COUNT) - Convert.ToInt32(nm3.COMPLETION_DONE_COUNT);
                            nm3.C2_COUNT = COUNTRE;
                        }
                    }
                    foreach (NM3 nm3 in body)
                    {
                        if (nm3.REFERENCE_AMOUNT != null && nm3.COMPLETION_DONE_AMOUNT != null)
                        {
                            string strNii1 = nm3.REFERENCE_AMOUNT.Replace(",", "");
                            string strNii2 = nm3.COMPLETION_DONE_AMOUNT.Replace(",", "");
                            Decimal Amount1 = Convert.ToDecimal(strNii1);
                            Decimal Amount2 = Convert.ToDecimal(strNii2);
                            AMOUNTRE = Amount1 - Amount2;

                            nm3.C2_AMOUNT = AMOUNTRE.ToString("#,0.##");
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

                    foreach (NM3 data in body)
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


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.##"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.##"));

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

                    depname.SetValue(Niit, "НИЙТ ДҮН");


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
                List<NM4> total = new List<NM4>();
                NM4 Niit = new NM4();
                var typ = typeof(NM4);
                if (res != null && res.Elements("NM4") != null)
                    body = (from item in res.Elements("NM4") select new NM4().SetXml(item)).ToList();
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

                    foreach (NM4 data in body)
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


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);

                    depname.SetValue(Niit, "НИЙТ ДҮН");


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
                List<NM5> total = new List<NM5>();
                NM5 Niit = new NM5();
                var typ = typeof(NM5);
                if (res != null && res.Elements("NM5") != null)
                    body = (from item in res.Elements("NM5") select new NM5().SetXml(item)).ToList();
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

                    foreach (NM5 data in body)
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


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);

                    depname.SetValue(Niit, "НИЙТ ДҮН");


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
                List<NM6> total = new List<NM6>();
                NM6 Niit = new NM6();
                var typ = typeof(NM6);
                if (res != null && res.Elements("NM6") != null)
                    body = (from item in res.Elements("NM6") select new NM6().SetXml(item)).ToList();
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

                    foreach (NM6 data in body)
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


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.##"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.##"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.##"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.##"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.##"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.##"));

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

                    depname.SetValue(Niit, "НИЙТ ДҮН");


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
                List<NM7> total = new List<NM7>();
                NM7 Niit = new NM7();
                var typ = typeof(NM7);
                if (res != null && res.Elements("NM7") != null)
                    body = (from item in res.Elements("NM7") select new NM7().SetXml(item)).ToList();
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

                    foreach (NM7 data in body)
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


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.##"));
                    pay6.SetValue(Niit, AMOUNT6.ToString("#,0.##"));
                    pay7.SetValue(Niit, AMOUNT7.ToString("#,0.##"));
                    pay8.SetValue(Niit, AMOUNT8.ToString("#,0.##"));
                    pay9.SetValue(Niit, AMOUNT9.ToString("#,0.##"));
                    pay10.SetValue(Niit, AMOUNT10.ToString("#,0.##"));
                    pay11.SetValue(Niit, AMOUNT11.ToString("#,0.##"));

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

                    depname.SetValue(Niit, "НИЙТ ДҮН");


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
                            List<CM2> typeNeg = new List<CM2>();
                            List<CM2> typeHoyor = new List<CM2>();
                            List<CM2> totaltype = new List<CM2>();

                            List<CM2> temp = new List<CM2>();
                            List<CM2> temp2 = new List<CM2>();
                            List<CM2> totaltemp2 = new List<CM2>();

                            List<CM2> BudgetTypes1totaltemp = new List<CM2>();
                            List<CM2> BudgetTypes2totaltemp = new List<CM2>();
                            List<CM2> BudgetTypes3totaltemp = new List<CM2>();

                            totaltype = body;
                            typeNeg = body.FindAll(a => a.IS_STATE.Equals(1));
                            typeHoyor = body.FindAll(a => a.IS_STATE.Equals(2));
                            

                            if (totaltype.Count > 0)
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

                                totalCompanytype1 = totaltype.FindAll(a => a.DECISION_TYPE.Equals("ТӨЛБӨРИЙН АКТ"));
                                
                                BUDGET1totalCompanytype1 = totalCompanytype1.FindAll(a => a.BUDGET_TYPE.Equals("1"));
                                BUDGET2totalCompanytype1 = totalCompanytype1.FindAll(a => a.BUDGET_TYPE.Equals("2"));
                                BUDGET3totalCompanytype1 = totalCompanytype1.FindAll(a => a.BUDGET_TYPE.Equals("3"));
                                BUDGET4totalCompanytype1 = totalCompanytype1.FindAll(a => a.BUDGET_TYPE.Equals("4"));
                                BUDGET5totalCompanytype1 = totalCompanytype1.FindAll(a => a.BUDGET_TYPE.Equals("5"));

                                totalCompanytype2 = totaltype.FindAll(a => a.DECISION_TYPE.Equals("АЛБАН ШААРДЛАГА"));

                                BUDGET1totalCompanytype2 = totalCompanytype2.FindAll(a => a.BUDGET_TYPE.Equals("1"));
                                BUDGET2totalCompanytype2 = totalCompanytype2.FindAll(a => a.BUDGET_TYPE.Equals("2"));
                                BUDGET3totalCompanytype2 = totalCompanytype2.FindAll(a => a.BUDGET_TYPE.Equals("3"));
                                BUDGET4totalCompanytype2 = totalCompanytype2.FindAll(a => a.BUDGET_TYPE.Equals("4"));
                                BUDGET5totalCompanytype2 = totalCompanytype2.FindAll(a => a.BUDGET_TYPE.Equals("5"));

                                totalCompanytype3 = totaltype.FindAll(a => a.DECISION_TYPE.Equals("ЗӨВЛӨМЖ"));

                                BUDGET1totalCompanytype3 = totalCompanytype3.FindAll(a => a.BUDGET_TYPE.Equals("1"));
                                BUDGET2totalCompanytype3 = totalCompanytype3.FindAll(a => a.BUDGET_TYPE.Equals("2"));
                                BUDGET3totalCompanytype3 = totalCompanytype3.FindAll(a => a.BUDGET_TYPE.Equals("3"));
                                BUDGET4totalCompanytype3 = totalCompanytype3.FindAll(a => a.BUDGET_TYPE.Equals("4"));
                                BUDGET5totalCompanytype3 = totalCompanytype3.FindAll(a => a.BUDGET_TYPE.Equals("5"));

                                totaltemp2 = new List<CM2>();
                                title = new CM2();
                                title.DECISION_TYPE = "Нийт дүн";
                                title.DEPARTMENT_NAME = "Нийт дүн";

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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

                                if (totalCompanytype1.Count > 0)
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


                                    foreach (CM2 data in totalCompanytype1)
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
                                    totalDEC2pay.SetValue(totalDEC2Niit, totalDEC2AMOUNT.ToString("#,0.##"));
                                    totalDEC2pay1.SetValue(totalDEC2Niit, totalDEC2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2pay2.SetValue(totalDEC2Niit, totalDEC2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2pay3.SetValue(totalDEC2Niit, totalDEC2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2pay4.SetValue(totalDEC2Niit, totalDEC2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2pay5.SetValue(totalDEC2Niit, totalDEC2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2pay6.SetValue(totalDEC2Niit, totalDEC2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2pay7.SetValue(totalDEC2Niit, totalDEC2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2pay8.SetValue(totalDEC2Niit, totalDEC2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2pay9.SetValue(totalDEC2Niit, totalDEC2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2pay10.SetValue(totalDEC2Niit, totalDEC2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2pay11.SetValue(totalDEC2Niit, totalDEC2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2pay12.SetValue(totalDEC2Niit, totalDEC2AMOUNT12.ToString("#,0.##"));

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
                                    totalCompanytype1 = totalCompanytemplist1;
                                }
                                if (totalCompanytype2.Count > 0)
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


                                    foreach (CM2 data in totalCompanytype2)
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
                                    totalDEC2Type2pay.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT.ToString("#,0.##"));
                                    totalDEC2Type2pay1.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT1.ToString("#,0.##"));
                                    totalDEC2Type2pay2.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT2.ToString("#,0.##"));
                                    totalDEC2Type2pay3.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT3.ToString("#,0.##"));
                                    totalDEC2Type2pay4.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT4.ToString("#,0.##"));
                                    totalDEC2Type2pay5.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT5.ToString("#,0.##"));
                                    totalDEC2Type2pay6.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT6.ToString("#,0.##"));
                                    totalDEC2Type2pay7.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT7.ToString("#,0.##"));
                                    totalDEC2Type2pay8.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT8.ToString("#,0.##"));
                                    totalDEC2Type2pay9.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT9.ToString("#,0.##"));
                                    totalDEC2Type2pay10.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT10.ToString("#,0.##"));
                                    totalDEC2Type2pay11.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT11.ToString("#,0.##"));
                                    totalDEC2Type2pay12.SetValue(totalDEC2Niit2, totalDEC2Type2AMOUNT12.ToString("#,0.##"));

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
                                    totalCompanytype2 = totalCompanytemplist2;
                                }
                                if (totalCompanytype3.Count > 0)
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


                                    foreach (CM2 data in totalCompanytype3)
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
                                    totalDEC2Type3pay.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT.ToString("#,0.##"));
                                    totalDEC2Type3pay1.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT1.ToString("#,0.##"));
                                    totalDEC2Type3pay2.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT2.ToString("#,0.##"));
                                    totalDEC2Type3pay3.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT3.ToString("#,0.##"));
                                    totalDEC2Type3pay4.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT4.ToString("#,0.##"));
                                    totalDEC2Type3pay5.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT5.ToString("#,0.##"));
                                    totalDEC2Type3pay6.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT6.ToString("#,0.##"));
                                    totalDEC2Type3pay7.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT7.ToString("#,0.##"));
                                    totalDEC2Type3pay8.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT8.ToString("#,0.##"));
                                    totalDEC2Type3pay9.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT9.ToString("#,0.##"));
                                    totalDEC2Type3pay10.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT10.ToString("#,0.##"));
                                    totalDEC2Type3pay11.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT11.ToString("#,0.##"));
                                    totalDEC2Type3pay12.SetValue(totalDEC2Niit3, totalDEC2Type3AMOUNT12.ToString("#,0.##"));

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
                                    totalCompanytype3 = totalCompanytemplist3;



                                }

                                
                                
                                totaltemp2.Add(title);
                                totaltemp2.AddRange(totalCompanytype1);
                                totaltemp2.AddRange(totalCompanytype2);
                                totaltemp2.AddRange(totalCompanytype3);
                                totaltype = totaltemp2;
                            }

                            if (typeNeg.Count > 0)
                            {
                                List<CM2> type1 = new List<CM2>();
                                List<CM2> type2 = new List<CM2>();
                                List<CM2> type3 = new List<CM2>();

                                List<CM2> templist1 = new List<CM2>();
                                List<CM2> templist2 = new List<CM2>();
                                List<CM2> templist3 = new List<CM2>();

                                type1 = typeNeg.FindAll(a => a.DECISION_TYPE.Equals("ТӨЛБӨРИЙН АКТ"));
                                type2 = typeNeg.FindAll(a => a.DECISION_TYPE.Equals("АЛБАН ШААРДЛАГА"));
                                type3 = typeNeg.FindAll(a => a.DECISION_TYPE.Equals("ЗӨВЛӨМЖ"));

                                temp = new List<CM2>();
                                title = new CM2();
                                title.DECISION_TYPE = "Нэг. Төрийн аудитын байгууллага";
                                title.DEPARTMENT_NAME = "Нэг. Төрийн аудитын байгууллага";

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
                                    DECpay.SetValue(DECNiit, DECAMOUNT.ToString("#,0.##"));
                                    DECpay1.SetValue(DECNiit, DECAMOUNT1.ToString("#,0.##"));
                                    DECpay2.SetValue(DECNiit, DECAMOUNT2.ToString("#,0.##"));
                                    DECpay3.SetValue(DECNiit, DECAMOUNT3.ToString("#,0.##"));
                                    DECpay4.SetValue(DECNiit, DECAMOUNT4.ToString("#,0.##"));
                                    DECpay5.SetValue(DECNiit, DECAMOUNT5.ToString("#,0.##"));
                                    DECpay6.SetValue(DECNiit, DECAMOUNT6.ToString("#,0.##"));
                                    DECpay7.SetValue(DECNiit, DECAMOUNT7.ToString("#,0.##"));
                                    DECpay8.SetValue(DECNiit, DECAMOUNT8.ToString("#,0.##"));
                                    DECpay9.SetValue(DECNiit, DECAMOUNT9.ToString("#,0.##"));
                                    DECpay10.SetValue(DECNiit, DECAMOUNT10.ToString("#,0.##"));
                                    DECpay11.SetValue(DECNiit, DECAMOUNT11.ToString("#,0.##"));
                                    DECpay12.SetValue(DECNiit, DECAMOUNT12.ToString("#,0.##"));

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
                                    DECType2pay.SetValue(DECNiit2, DECTYPE2AMOUNT.ToString("#,0.##"));
                                    DECType2pay1.SetValue(DECNiit2, DECTYPE2AMOUNT1.ToString("#,0.##"));
                                    DECType2pay2.SetValue(DECNiit2, DECTYPE2AMOUNT2.ToString("#,0.##"));
                                    DECType2pay3.SetValue(DECNiit2, DECTYPE2AMOUNT3.ToString("#,0.##"));
                                    DECType2pay4.SetValue(DECNiit2, DECTYPE2AMOUNT4.ToString("#,0.##"));
                                    DECType2pay5.SetValue(DECNiit2, DECTYPE2AMOUNT5.ToString("#,0.##"));
                                    DECType2pay6.SetValue(DECNiit2, DECTYPE2AMOUNT6.ToString("#,0.##"));
                                    DECType2pay7.SetValue(DECNiit2, DECTYPE2AMOUNT7.ToString("#,0.##"));
                                    DECType2pay8.SetValue(DECNiit2, DECTYPE2AMOUNT8.ToString("#,0.##"));
                                    DECType2pay9.SetValue(DECNiit2, DECTYPE2AMOUNT9.ToString("#,0.##"));
                                    DECType2pay10.SetValue(DECNiit2, DECTYPE2AMOUNT10.ToString("#,0.##"));
                                    DECType2pay11.SetValue(DECNiit2, DECTYPE2AMOUNT11.ToString("#,0.##"));
                                    DECType2pay12.SetValue(DECNiit2, DECTYPE2AMOUNT12.ToString("#,0.##"));

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
                                    DECType3pay.SetValue(DECNiit3, DECTYPE3AMOUNT.ToString("#,0.##"));
                                    DECType3pay1.SetValue(DECNiit3, DECTYPE3AMOUNT1.ToString("#,0.##"));
                                    DECType3pay2.SetValue(DECNiit3, DECTYPE3AMOUNT2.ToString("#,0.##"));
                                    DECType3pay3.SetValue(DECNiit3, DECTYPE3AMOUNT3.ToString("#,0.##"));
                                    DECType3pay4.SetValue(DECNiit3, DECTYPE3AMOUNT4.ToString("#,0.##"));
                                    DECType3pay5.SetValue(DECNiit3, DECTYPE3AMOUNT5.ToString("#,0.##"));
                                    DECType3pay6.SetValue(DECNiit3, DECTYPE3AMOUNT6.ToString("#,0.##"));
                                    DECType3pay7.SetValue(DECNiit3, DECTYPE3AMOUNT7.ToString("#,0.##"));
                                    DECType3pay8.SetValue(DECNiit3, DECTYPE3AMOUNT8.ToString("#,0.##"));
                                    DECType3pay9.SetValue(DECNiit3, DECTYPE3AMOUNT9.ToString("#,0.##"));
                                    DECType3pay10.SetValue(DECNiit3, DECTYPE3AMOUNT10.ToString("#,0.##"));
                                    DECType3pay11.SetValue(DECNiit3, DECTYPE3AMOUNT11.ToString("#,0.##"));
                                    DECType3pay12.SetValue(DECNiit3, DECTYPE3AMOUNT12.ToString("#,0.##"));

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

                                temp.Add(title);
                                temp.AddRange(type1);
                                temp.AddRange(type2);
                                temp.AddRange(type3);
                                typeNeg = temp;
                            }

                            if (typeHoyor.Count > 0)
                            {
                                List<CM2> Companytype1 = new List<CM2>();
                                List<CM2> Companytype2 = new List<CM2>();
                                List<CM2> Companytype3 = new List<CM2>();

                                List<CM2> Companytemplist1 = new List<CM2>();
                                List<CM2> Companytemplist2 = new List<CM2>();
                                List<CM2> Companytemplist3 = new List<CM2>();

                                Companytype1 = typeHoyor.FindAll(a => a.DECISION_TYPE.Equals("ТӨЛБӨРИЙН АКТ"));
                                Companytype2 = typeHoyor.FindAll(a => a.DECISION_TYPE.Equals("АЛБАН ШААРДЛАГА"));
                                Companytype3 = typeHoyor.FindAll(a => a.DECISION_TYPE.Equals("ЗӨВЛӨМЖ"));

                                temp2 = new List<CM2>();
                                title = new CM2();
                                title.DECISION_TYPE = "Хоёр. Хараат бус аудитын компани";
                                title.DEPARTMENT_NAME = "Хоёр. Хараат бус аудитын компани";

                                if(Companytype1.Count > 0)
                                {

                                    CM2 DEC2Niit = new CM2();
                                    var DEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                                    var DEP2depname = typ.GetProperty("DEPARTMENT_NAME");
                                    var DECISION2_TYPEdepname = typ.GetProperty("DECISION_TYPE");

                                    var DEC2pay = typ.GetProperty("C1_AMOUNT");
                                    var DEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                                    var DEC2pay2 = typ.GetProperty("PREV_AMOUNT");
                                    var DEC2pay3 = typ.GetProperty("CY_AMOUNT");
                                    var DEC2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                                    var DEC2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                                    var DEC2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                                    var DEC2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                                    var DEC2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                                    var DEC2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                                    var DEC2pay10 = typ.GetProperty("C2_AMOUNT");
                                    var DEC2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                                    var DEC2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                                    var DEC2count = typ.GetProperty("C1_COUNT");
                                    var DEC2count1 = typ.GetProperty("CURRENT_COUNT");
                                    var DEC2count2 = typ.GetProperty("PREV_COUNT");
                                    var DEC2count3 = typ.GetProperty("CY_COUNT");
                                    var DEC2count4 = typ.GetProperty("TOTAL_COUNT");
                                    var DEC2count5 = typ.GetProperty("COMP_STATE_COUNT");
                                    var DEC2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                                    var DEC2count7 = typ.GetProperty("COMP_ORG_COUNT");
                                    var DEC2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                                    var DEC2count9 = typ.GetProperty("STATISTIC_COUNT");
                                    var DEC2count10 = typ.GetProperty("C2_COUNT");
                                    var DEC2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                                    var DEC2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                                    decimal DEC2AMOUNT = 0;
                                    decimal DEC2AMOUNT1 = 0;
                                    decimal DEC2AMOUNT2 = 0;
                                    decimal DEC2AMOUNT3 = 0;
                                    decimal DEC2AMOUNT4 = 0;
                                    decimal DEC2AMOUNT5 = 0;
                                    decimal DEC2AMOUNT6 = 0;
                                    decimal DEC2AMOUNT7 = 0;
                                    decimal DEC2AMOUNT8 = 0;
                                    decimal DEC2AMOUNT9 = 0;
                                    decimal DEC2AMOUNT10 = 0;
                                    decimal DEC2AMOUNT11 = 0;
                                    decimal DEC2AMOUNT12 = 0;

                                    int DEC2NUMBER = 0;
                                    int DEC2NUMBER1 = 0;
                                    int DEC2NUMBER2 = 0;
                                    int DEC2NUMBER3 = 0;
                                    int DEC2NUMBER4 = 0;
                                    int DEC2NUMBER5 = 0;
                                    int DEC2NUMBER6 = 0;
                                    int DEC2NUMBER7 = 0;
                                    int DEC2NUMBER8 = 0;
                                    int DEC2NUMBER9 = 0;
                                    int DEC2NUMBER10 = 0;
                                    int DEC2NUMBER11 = 0;
                                    int DEC2NUMBER12 = 0;


                                    foreach (CM2 data in Companytype1)
                                    {
                                        if (data.C1_AMOUNT != null)
                                        {
                                            string strNii = data.C1_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT += Amount;

                                        }
                                        if (data.CURRENT_AMOUNT != null)
                                        {
                                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT1 += Amount;

                                        }
                                        if (data.PREV_AMOUNT != null)
                                        {
                                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT2 += Amount;

                                        }
                                        if (data.CY_AMOUNT != null)
                                        {
                                            string strNii = data.CY_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT3 += Amount;

                                        }
                                        if (data.TOTAL_AMOUNT != null)
                                        {
                                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT4 += Amount;

                                        }
                                        if (data.COMP_STATE_AMOUNT != null)
                                        {
                                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT5 += Amount;

                                        }
                                        if (data.COMP_LOCAL_AMOUNT != null)
                                        {
                                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT6 += Amount;

                                        }
                                        if (data.COMP_ORG_AMOUNT != null)
                                        {
                                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT7 += Amount;

                                        }
                                        if (data.COMP_OTHER_AMOUNT != null)
                                        {
                                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT8 += Amount;

                                        }
                                        if (data.STATISTIC_AMOUNT != null)
                                        {
                                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT9 += Amount;

                                        }
                                        if (data.C2_AMOUNT != null)
                                        {
                                            string strNii = data.C2_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT10 += Amount;

                                        }
                                        if (data.C2_NONEXPIRED_AMOUNT != null)
                                        {
                                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT11 += Amount;

                                        }
                                        if (data.C2_EXPIRED_AMOUNT != null)
                                        {
                                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2AMOUNT12 += Amount;

                                        }



                                        if (data.C1_COUNT != 0)
                                        {
                                            DEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                                        }
                                        if (data.CURRENT_COUNT != 0)
                                        {
                                            DEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                                        }
                                        if (data.PREV_COUNT != 0)
                                        {
                                            DEC2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                                        }
                                        if (data.CY_COUNT != 0)
                                        {
                                            DEC2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                                        }
                                        if (data.TOTAL_COUNT != 0)
                                        {
                                            DEC2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                                        }
                                        if (data.COMP_STATE_COUNT != 0)
                                        {
                                            DEC2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                                        }
                                        if (data.COMP_LOCAL_COUNT != 0)
                                        {
                                            DEC2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                                        }
                                        if (data.COMP_ORG_COUNT != 0)
                                        {
                                            DEC2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                                        }
                                        if (data.COMP_OTHER_COUNT != 0)
                                        {
                                            DEC2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                                        }
                                        if (data.STATISTIC_COUNT != 0)
                                        {
                                            DEC2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                                        }
                                        if (data.C2_COUNT != 0)
                                        {
                                            DEC2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                                        }
                                        if (data.C2_NONEXPIRED_COUNT != 0)
                                        {
                                            DEC2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                                        }
                                        if (data.C2_EXPIRED_COUNT != 0)
                                        {
                                            DEC2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                                        }
                                        DEP2depname.SetValue(DEC2Niit, data.DEPARTMENT_NAME);
                                    }
                                    DEC2pay.SetValue(DEC2Niit, DEC2AMOUNT.ToString("#,0.##"));
                                    DEC2pay1.SetValue(DEC2Niit, DEC2AMOUNT1.ToString("#,0.##"));
                                    DEC2pay2.SetValue(DEC2Niit, DEC2AMOUNT2.ToString("#,0.##"));
                                    DEC2pay3.SetValue(DEC2Niit, DEC2AMOUNT3.ToString("#,0.##"));
                                    DEC2pay4.SetValue(DEC2Niit, DEC2AMOUNT4.ToString("#,0.##"));
                                    DEC2pay5.SetValue(DEC2Niit, DEC2AMOUNT5.ToString("#,0.##"));
                                    DEC2pay6.SetValue(DEC2Niit, DEC2AMOUNT6.ToString("#,0.##"));
                                    DEC2pay7.SetValue(DEC2Niit, DEC2AMOUNT7.ToString("#,0.##"));
                                    DEC2pay8.SetValue(DEC2Niit, DEC2AMOUNT8.ToString("#,0.##"));
                                    DEC2pay9.SetValue(DEC2Niit, DEC2AMOUNT9.ToString("#,0.##"));
                                    DEC2pay10.SetValue(DEC2Niit, DEC2AMOUNT10.ToString("#,0.##"));
                                    DEC2pay11.SetValue(DEC2Niit, DEC2AMOUNT11.ToString("#,0.##"));
                                    DEC2pay12.SetValue(DEC2Niit, DEC2AMOUNT12.ToString("#,0.##"));

                                    DEC2count.SetValue(DEC2Niit, DEC2NUMBER);
                                    DEC2count1.SetValue(DEC2Niit, DEC2NUMBER1);
                                    DEC2count2.SetValue(DEC2Niit, DEC2NUMBER2);
                                    DEC2count3.SetValue(DEC2Niit, DEC2NUMBER3);
                                    DEC2count4.SetValue(DEC2Niit, DEC2NUMBER4);
                                    DEC2count5.SetValue(DEC2Niit, DEC2NUMBER5);
                                    DEC2count6.SetValue(DEC2Niit, DEC2NUMBER6);
                                    DEC2count7.SetValue(DEC2Niit, DEC2NUMBER7);
                                    DEC2count8.SetValue(DEC2Niit, DEC2NUMBER8);
                                    DEC2count9.SetValue(DEC2Niit, DEC2NUMBER9);
                                    DEC2count10.SetValue(DEC2Niit, DEC2NUMBER10);
                                    DEC2count11.SetValue(DEC2Niit, DEC2NUMBER11);
                                    DEC2count12.SetValue(DEC2Niit, DEC2NUMBER12);

                                    DEC2depname.SetValue(DEC2Niit, "Дүн");
                                    DECISION2_TYPEdepname.SetValue(DEC2Niit, "ТӨЛБӨРИЙН АКТ");

                                    Companytemplist1.AddRange(Companytype1.OrderBy(m => m.DECISION_TYPE));
                                    Companytemplist1.Add(DEC2Niit);
                                    Companytype1 = Companytemplist1;
                                }
                                if (Companytype2.Count > 0)
                                {

                                    CM2 DEC2Niit2 = new CM2();
                                    var DEC2depname2 = typ.GetProperty("BUDGET_TYPE_NAME");
                                    var DEP2depname2 = typ.GetProperty("DEPARTMENT_NAME");
                                    var DECISION2_TYPEdepname2 = typ.GetProperty("DECISION_TYPE");

                                    var DEC2Type2pay = typ.GetProperty("C1_AMOUNT");
                                    var DEC2Type2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                                    var DEC2Type2pay2 = typ.GetProperty("PREV_AMOUNT");
                                    var DEC2Type2pay3 = typ.GetProperty("CY_AMOUNT");
                                    var DEC2Type2pay4 = typ.GetProperty("TOTAL_AMOUNT");
                                    var DEC2Type2pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                                    var DEC2Type2pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                                    var DEC2Type2pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                                    var DEC2Type2pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                                    var DEC2Type2pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                                    var DEC2Type2pay10 = typ.GetProperty("C2_AMOUNT");
                                    var DEC2Type2pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                                    var DEC2Type2pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                                    var DEC2Type2count = typ.GetProperty("C1_COUNT");
                                    var DEC2Type2count1 = typ.GetProperty("CURRENT_COUNT");
                                    var DEC2Type2count2 = typ.GetProperty("PREV_COUNT");
                                    var DEC2Type2count3 = typ.GetProperty("CY_COUNT");
                                    var DEC2Type2count4 = typ.GetProperty("TOTAL_COUNT");
                                    var DEC2Type2count5 = typ.GetProperty("COMP_STATE_COUNT");
                                    var DEC2Type2count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                                    var DEC2Type2count7 = typ.GetProperty("COMP_ORG_COUNT");
                                    var DEC2Type2count8 = typ.GetProperty("COMP_OTHER_COUNT");
                                    var DEC2Type2count9 = typ.GetProperty("STATISTIC_COUNT");
                                    var DEC2Type2count10 = typ.GetProperty("C2_COUNT");
                                    var DEC2Type2count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                                    var DEC2Type2count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                                    decimal DEC2Type2AMOUNT = 0;
                                    decimal DEC2Type2AMOUNT1 = 0;
                                    decimal DEC2Type2AMOUNT2 = 0;
                                    decimal DEC2Type2AMOUNT3 = 0;
                                    decimal DEC2Type2AMOUNT4 = 0;
                                    decimal DEC2Type2AMOUNT5 = 0;
                                    decimal DEC2Type2AMOUNT6 = 0;
                                    decimal DEC2Type2AMOUNT7 = 0;
                                    decimal DEC2Type2AMOUNT8 = 0;
                                    decimal DEC2Type2AMOUNT9 = 0;
                                    decimal DEC2Type2AMOUNT10 = 0;
                                    decimal DEC2Type2AMOUNT11 = 0;
                                    decimal DEC2Type2AMOUNT12 = 0;

                                    int DEC2Type2NUMBER = 0;
                                    int DEC2Type2NUMBER1 = 0;
                                    int DEC2Type2NUMBER2 = 0;
                                    int DEC2Type2NUMBER3 = 0;
                                    int DEC2Type2NUMBER4 = 0;
                                    int DEC2Type2NUMBER5 = 0;
                                    int DEC2Type2NUMBER6 = 0;
                                    int DEC2Type2NUMBER7 = 0;
                                    int DEC2Type2NUMBER8 = 0;
                                    int DEC2Type2NUMBER9 = 0;
                                    int DEC2Type2NUMBER10 = 0;
                                    int DEC2Type2NUMBER11 = 0;
                                    int DEC2Type2NUMBER12 = 0;


                                    foreach (CM2 data in Companytype2)
                                    {
                                        if (data.C1_AMOUNT != null)
                                        {
                                            string strNii = data.C1_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT += Amount;

                                        }
                                        if (data.CURRENT_AMOUNT != null)
                                        {
                                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT1 += Amount;

                                        }
                                        if (data.PREV_AMOUNT != null)
                                        {
                                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT2 += Amount;

                                        }
                                        if (data.CY_AMOUNT != null)
                                        {
                                            string strNii = data.CY_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT3 += Amount;

                                        }
                                        if (data.TOTAL_AMOUNT != null)
                                        {
                                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT4 += Amount;

                                        }
                                        if (data.COMP_STATE_AMOUNT != null)
                                        {
                                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT5 += Amount;

                                        }
                                        if (data.COMP_LOCAL_AMOUNT != null)
                                        {
                                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT6 += Amount;

                                        }
                                        if (data.COMP_ORG_AMOUNT != null)
                                        {
                                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT7 += Amount;

                                        }
                                        if (data.COMP_OTHER_AMOUNT != null)
                                        {
                                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT8 += Amount;

                                        }
                                        if (data.STATISTIC_AMOUNT != null)
                                        {
                                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT9 += Amount;

                                        }
                                        if (data.C2_AMOUNT != null)
                                        {
                                            string strNii = data.C2_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT10 += Amount;

                                        }
                                        if (data.C2_NONEXPIRED_AMOUNT != null)
                                        {
                                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT11 += Amount;

                                        }
                                        if (data.C2_EXPIRED_AMOUNT != null)
                                        {
                                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type2AMOUNT12 += Amount;

                                        }



                                        if (data.C1_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER += Convert.ToInt32(data.C1_COUNT);
                                        }
                                        if (data.CURRENT_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                                        }
                                        if (data.PREV_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                                        }
                                        if (data.CY_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                                        }
                                        if (data.TOTAL_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                                        }
                                        if (data.COMP_STATE_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                                        }
                                        if (data.COMP_LOCAL_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                                        }
                                        if (data.COMP_ORG_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                                        }
                                        if (data.COMP_OTHER_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                                        }
                                        if (data.STATISTIC_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                                        }
                                        if (data.C2_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                                        }
                                        if (data.C2_NONEXPIRED_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                                        }
                                        if (data.C2_EXPIRED_COUNT != 0)
                                        {
                                            DEC2Type2NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                                        }
                                        DEP2depname2.SetValue(DEC2Niit2, data.DEPARTMENT_NAME);
                                    }
                                    DEC2Type2pay.SetValue(DEC2Niit2, DEC2Type2AMOUNT.ToString("#,0.##"));
                                    DEC2Type2pay1.SetValue(DEC2Niit2, DEC2Type2AMOUNT1.ToString("#,0.##"));
                                    DEC2Type2pay2.SetValue(DEC2Niit2, DEC2Type2AMOUNT2.ToString("#,0.##"));
                                    DEC2Type2pay3.SetValue(DEC2Niit2, DEC2Type2AMOUNT3.ToString("#,0.##"));
                                    DEC2Type2pay4.SetValue(DEC2Niit2, DEC2Type2AMOUNT4.ToString("#,0.##"));
                                    DEC2Type2pay5.SetValue(DEC2Niit2, DEC2Type2AMOUNT5.ToString("#,0.##"));
                                    DEC2Type2pay6.SetValue(DEC2Niit2, DEC2Type2AMOUNT6.ToString("#,0.##"));
                                    DEC2Type2pay7.SetValue(DEC2Niit2, DEC2Type2AMOUNT7.ToString("#,0.##"));
                                    DEC2Type2pay8.SetValue(DEC2Niit2, DEC2Type2AMOUNT8.ToString("#,0.##"));
                                    DEC2Type2pay9.SetValue(DEC2Niit2, DEC2Type2AMOUNT9.ToString("#,0.##"));
                                    DEC2Type2pay10.SetValue(DEC2Niit2, DEC2Type2AMOUNT10.ToString("#,0.##"));
                                    DEC2Type2pay11.SetValue(DEC2Niit2, DEC2Type2AMOUNT11.ToString("#,0.##"));
                                    DEC2Type2pay12.SetValue(DEC2Niit2, DEC2Type2AMOUNT12.ToString("#,0.##"));

                                    DEC2Type2count.SetValue(DEC2Niit2, DEC2Type2NUMBER);
                                    DEC2Type2count1.SetValue(DEC2Niit2, DEC2Type2NUMBER1);
                                    DEC2Type2count2.SetValue(DEC2Niit2, DEC2Type2NUMBER2);
                                    DEC2Type2count3.SetValue(DEC2Niit2, DEC2Type2NUMBER3);
                                    DEC2Type2count4.SetValue(DEC2Niit2, DEC2Type2NUMBER4);
                                    DEC2Type2count5.SetValue(DEC2Niit2, DEC2Type2NUMBER5);
                                    DEC2Type2count6.SetValue(DEC2Niit2, DEC2Type2NUMBER6);
                                    DEC2Type2count7.SetValue(DEC2Niit2, DEC2Type2NUMBER7);
                                    DEC2Type2count8.SetValue(DEC2Niit2, DEC2Type2NUMBER8);
                                    DEC2Type2count9.SetValue(DEC2Niit2, DEC2Type2NUMBER9);
                                    DEC2Type2count10.SetValue(DEC2Niit2, DEC2Type2NUMBER10);
                                    DEC2Type2count11.SetValue(DEC2Niit2, DEC2Type2NUMBER11);
                                    DEC2Type2count12.SetValue(DEC2Niit2, DEC2Type2NUMBER12);

                                    DEC2depname2.SetValue(DEC2Niit2, "Дүн");
                                    DECISION2_TYPEdepname2.SetValue(DEC2Niit2, "АЛБАН ШААРДЛАГА");

                                    Companytemplist2.AddRange(Companytype2.OrderBy(m => m.DECISION_TYPE));
                                    Companytemplist2.Add(DEC2Niit2);
                                    Companytype2 = Companytemplist2;
                                }
                                if (Companytype3.Count > 0)
                                {

                                    CM2 DEC2Niit3 = new CM2();
                                    var DEC2depname3 = typ.GetProperty("BUDGET_TYPE_NAME");
                                    var DEP2depname3 = typ.GetProperty("DEPARTMENT_NAME");
                                    var DECISION2_TYPEdepname3 = typ.GetProperty("DECISION_TYPE");

                                    var DEC2Type3pay = typ.GetProperty("C1_AMOUNT");
                                    var DEC2Type3pay1 = typ.GetProperty("CURRENT_AMOUNT");
                                    var DEC2Type3pay2 = typ.GetProperty("PREV_AMOUNT");
                                    var DEC2Type3pay3 = typ.GetProperty("CY_AMOUNT");
                                    var DEC2Type3pay4 = typ.GetProperty("TOTAL_AMOUNT");
                                    var DEC2Type3pay5 = typ.GetProperty("COMP_STATE_AMOUNT");
                                    var DEC2Type3pay6 = typ.GetProperty("COMP_LOCAL_AMOUNT");
                                    var DEC2Type3pay7 = typ.GetProperty("COMP_ORG_AMOUNT");
                                    var DEC2Type3pay8 = typ.GetProperty("COMP_OTHER_AMOUNT");
                                    var DEC2Type3pay9 = typ.GetProperty("STATISTIC_AMOUNT");
                                    var DEC2Type3pay10 = typ.GetProperty("C2_AMOUNT");
                                    var DEC2Type3pay11 = typ.GetProperty("C2_NONEXPIRED_AMOUNT");
                                    var DEC2Type3pay12 = typ.GetProperty("C2_EXPIRED_AMOUNT");


                                    var DEC2Type3count = typ.GetProperty("C1_COUNT");
                                    var DEC2Type3count1 = typ.GetProperty("CURRENT_COUNT");
                                    var DEC2Type3count2 = typ.GetProperty("PREV_COUNT");
                                    var DEC2Type3count3 = typ.GetProperty("CY_COUNT");
                                    var DEC2Type3count4 = typ.GetProperty("TOTAL_COUNT");
                                    var DEC2Type3count5 = typ.GetProperty("COMP_STATE_COUNT");
                                    var DEC2Type3count6 = typ.GetProperty("COMP_LOCAL_COUNT");
                                    var DEC2Type3count7 = typ.GetProperty("COMP_ORG_COUNT");
                                    var DEC2Type3count8 = typ.GetProperty("COMP_OTHER_COUNT");
                                    var DEC2Type3count9 = typ.GetProperty("STATISTIC_COUNT");
                                    var DEC2Type3count10 = typ.GetProperty("C2_COUNT");
                                    var DEC2Type3count11 = typ.GetProperty("C2_NONEXPIRED_COUNT");
                                    var DEC2Type3count12 = typ.GetProperty("C2_EXPIRED_COUNT");


                                    decimal DEC2Type3AMOUNT = 0;
                                    decimal DEC2Type3AMOUNT1 = 0;
                                    decimal DEC2Type3AMOUNT2 = 0;
                                    decimal DEC2Type3AMOUNT3 = 0;
                                    decimal DEC2Type3AMOUNT4 = 0;
                                    decimal DEC2Type3AMOUNT5 = 0;
                                    decimal DEC2Type3AMOUNT6 = 0;
                                    decimal DEC2Type3AMOUNT7 = 0;
                                    decimal DEC2Type3AMOUNT8 = 0;
                                    decimal DEC2Type3AMOUNT9 = 0;
                                    decimal DEC2Type3AMOUNT10 = 0;
                                    decimal DEC2Type3AMOUNT11 = 0;
                                    decimal DEC2Type3AMOUNT12 = 0;

                                    int DEC2Type3NUMBER = 0;
                                    int DEC2Type3NUMBER1 = 0;
                                    int DEC2Type3NUMBER2 = 0;
                                    int DEC2Type3NUMBER3 = 0;
                                    int DEC2Type3NUMBER4 = 0;
                                    int DEC2Type3NUMBER5 = 0;
                                    int DEC2Type3NUMBER6 = 0;
                                    int DEC2Type3NUMBER7 = 0;
                                    int DEC2Type3NUMBER8 = 0;
                                    int DEC2Type3NUMBER9 = 0;
                                    int DEC2Type3NUMBER10 = 0;
                                    int DEC2Type3NUMBER11 = 0;
                                    int DEC2Type3NUMBER12 = 0;


                                    foreach (CM2 data in Companytype3)
                                    {
                                        if (data.C1_AMOUNT != null)
                                        {
                                            string strNii = data.C1_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT += Amount;

                                        }
                                        if (data.CURRENT_AMOUNT != null)
                                        {
                                            string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT1 += Amount;

                                        }
                                        if (data.PREV_AMOUNT != null)
                                        {
                                            string strNii = data.PREV_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT2 += Amount;

                                        }
                                        if (data.CY_AMOUNT != null)
                                        {
                                            string strNii = data.CY_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT3 += Amount;

                                        }
                                        if (data.TOTAL_AMOUNT != null)
                                        {
                                            string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT4 += Amount;

                                        }
                                        if (data.COMP_STATE_AMOUNT != null)
                                        {
                                            string strNii = data.COMP_STATE_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT5 += Amount;

                                        }
                                        if (data.COMP_LOCAL_AMOUNT != null)
                                        {
                                            string strNii = data.COMP_LOCAL_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT6 += Amount;

                                        }
                                        if (data.COMP_ORG_AMOUNT != null)
                                        {
                                            string strNii = data.COMP_ORG_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT7 += Amount;

                                        }
                                        if (data.COMP_OTHER_AMOUNT != null)
                                        {
                                            string strNii = data.COMP_OTHER_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT8 += Amount;

                                        }
                                        if (data.STATISTIC_AMOUNT != null)
                                        {
                                            string strNii = data.STATISTIC_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT9 += Amount;

                                        }
                                        if (data.C2_AMOUNT != null)
                                        {
                                            string strNii = data.C2_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT10 += Amount;

                                        }
                                        if (data.C2_NONEXPIRED_AMOUNT != null)
                                        {
                                            string strNii = data.C2_NONEXPIRED_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT11 += Amount;

                                        }
                                        if (data.C2_EXPIRED_AMOUNT != null)
                                        {
                                            string strNii = data.C2_EXPIRED_AMOUNT.Replace(",", "");
                                            Decimal Amount = Convert.ToDecimal(strNii);
                                            DEC2Type3AMOUNT12 += Amount;

                                        }



                                        if (data.C1_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER += Convert.ToInt32(data.C1_COUNT);
                                        }
                                        if (data.CURRENT_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                                        }
                                        if (data.PREV_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER2 += Convert.ToInt32(data.PREV_COUNT);
                                        }
                                        if (data.CY_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER3 += Convert.ToInt32(data.CY_COUNT);
                                        }
                                        if (data.TOTAL_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER4 += Convert.ToInt32(data.TOTAL_COUNT);
                                        }
                                        if (data.COMP_STATE_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER5 += Convert.ToInt32(data.COMP_STATE_COUNT);
                                        }
                                        if (data.COMP_LOCAL_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER6 += Convert.ToInt32(data.COMP_LOCAL_COUNT);
                                        }
                                        if (data.COMP_ORG_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER7 += Convert.ToInt32(data.COMP_ORG_COUNT);
                                        }
                                        if (data.COMP_OTHER_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER8 += Convert.ToInt32(data.COMP_OTHER_COUNT);
                                        }
                                        if (data.STATISTIC_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER9 += Convert.ToInt32(data.STATISTIC_COUNT);
                                        }
                                        if (data.C2_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER10 += Convert.ToInt32(data.C2_COUNT);
                                        }
                                        if (data.C2_NONEXPIRED_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER11 += Convert.ToInt32(data.C2_NONEXPIRED_COUNT);
                                        }
                                        if (data.C2_EXPIRED_COUNT != 0)
                                        {
                                            DEC2Type3NUMBER12 += Convert.ToInt32(data.C2_EXPIRED_COUNT);
                                        }
                                       
                                    }
                                    DEC2Type3pay.SetValue(DEC2Niit3, DEC2Type3AMOUNT.ToString("#,0.##"));
                                    DEC2Type3pay1.SetValue(DEC2Niit3, DEC2Type3AMOUNT1.ToString("#,0.##"));
                                    DEC2Type3pay2.SetValue(DEC2Niit3, DEC2Type3AMOUNT2.ToString("#,0.##"));
                                    DEC2Type3pay3.SetValue(DEC2Niit3, DEC2Type3AMOUNT3.ToString("#,0.##"));
                                    DEC2Type3pay4.SetValue(DEC2Niit3, DEC2Type3AMOUNT4.ToString("#,0.##"));
                                    DEC2Type3pay5.SetValue(DEC2Niit3, DEC2Type3AMOUNT5.ToString("#,0.##"));
                                    DEC2Type3pay6.SetValue(DEC2Niit3, DEC2Type3AMOUNT6.ToString("#,0.##"));
                                    DEC2Type3pay7.SetValue(DEC2Niit3, DEC2Type3AMOUNT7.ToString("#,0.##"));
                                    DEC2Type3pay8.SetValue(DEC2Niit3, DEC2Type3AMOUNT8.ToString("#,0.##"));
                                    DEC2Type3pay9.SetValue(DEC2Niit3, DEC2Type3AMOUNT9.ToString("#,0.##"));
                                    DEC2Type3pay10.SetValue(DEC2Niit3, DEC2Type3AMOUNT10.ToString("#,0.##"));
                                    DEC2Type3pay11.SetValue(DEC2Niit3, DEC2Type3AMOUNT11.ToString("#,0.##"));
                                    DEC2Type3pay12.SetValue(DEC2Niit3, DEC2Type3AMOUNT12.ToString("#,0.##"));

                                    DEC2Type3count.SetValue(DEC2Niit3, DEC2Type3NUMBER);
                                    DEC2Type3count1.SetValue(DEC2Niit3, DEC2Type3NUMBER1);
                                    DEC2Type3count2.SetValue(DEC2Niit3, DEC2Type3NUMBER2);
                                    DEC2Type3count3.SetValue(DEC2Niit3, DEC2Type3NUMBER3);
                                    DEC2Type3count4.SetValue(DEC2Niit3, DEC2Type3NUMBER4);
                                    DEC2Type3count5.SetValue(DEC2Niit3, DEC2Type3NUMBER5);
                                    DEC2Type3count6.SetValue(DEC2Niit3, DEC2Type3NUMBER6);
                                    DEC2Type3count7.SetValue(DEC2Niit3, DEC2Type3NUMBER7);
                                    DEC2Type3count8.SetValue(DEC2Niit3, DEC2Type3NUMBER8);
                                    DEC2Type3count9.SetValue(DEC2Niit3, DEC2Type3NUMBER9);
                                    DEC2Type3count10.SetValue(DEC2Niit3, DEC2Type3NUMBER10);
                                    DEC2Type3count11.SetValue(DEC2Niit3, DEC2Type3NUMBER11);
                                    DEC2Type3count12.SetValue(DEC2Niit3, DEC2Type3NUMBER12);

                                    DEC2depname3.SetValue(DEC2Niit3, "Дүн");
                                    DECISION2_TYPEdepname3.SetValue(DEC2Niit3, "ЗӨВЛӨМЖ");

                                    Companytemplist3.AddRange(Companytype3.OrderBy(m => m.DECISION_TYPE));
                                    Companytemplist3.Add(DEC2Niit3);
                                    Companytype3 = Companytemplist3;
                                }

                                temp2.Add(title);
                                temp2.AddRange(Companytype1);
                                temp2.AddRange(Companytype2);
                                temp2.AddRange(Companytype3);
                                typeHoyor = temp2;
                            }

                            

                            List<CM2> types = new List<CM2>();
                            types.AddRange(typeNeg);
                            types.AddRange(typeHoyor);
                            types.AddRange(totaltype);

                            cm2Detial = types;

                            response.data = cm2Detial;

                        }
                        else
                        {
                                List<CM2> Bodytemp = new List<CM2>();
                                List<CM2> Bodytype1 = new List<CM2>();
                                List<CM2> Bodytype2 = new List<CM2>();
                                List<CM2> Bodytype3 = new List<CM2>();

                                List<CM2> Bodytemplist1 = new List<CM2>();
                                List<CM2> Bodytemplist2 = new List<CM2>();
                                List<CM2> Bodytemplist3 = new List<CM2>();

                                Bodytype1 = body.FindAll(a => a.DECISION_TYPE.Equals("ТӨЛБӨРИЙН АКТ"));
                                Bodytype2 = body.FindAll(a => a.DECISION_TYPE.Equals("АЛБАН ШААРДЛАГА"));
                                Bodytype3 = body.FindAll(a => a.DECISION_TYPE.Equals("ЗӨВЛӨМЖ"));



                                if (Bodytype1.Count > 0)
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


                                    foreach (CM2 data in Bodytype1)
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
                                    DECpay.SetValue(DECNiit, DECAMOUNT.ToString("#,0.##"));
                                    DECpay1.SetValue(DECNiit, DECAMOUNT1.ToString("#,0.##"));
                                    DECpay2.SetValue(DECNiit, DECAMOUNT2.ToString("#,0.##"));
                                    DECpay3.SetValue(DECNiit, DECAMOUNT3.ToString("#,0.##"));
                                    DECpay4.SetValue(DECNiit, DECAMOUNT4.ToString("#,0.##"));
                                    DECpay5.SetValue(DECNiit, DECAMOUNT5.ToString("#,0.##"));
                                    DECpay6.SetValue(DECNiit, DECAMOUNT6.ToString("#,0.##"));
                                    DECpay7.SetValue(DECNiit, DECAMOUNT7.ToString("#,0.##"));
                                    DECpay8.SetValue(DECNiit, DECAMOUNT8.ToString("#,0.##"));
                                    DECpay9.SetValue(DECNiit, DECAMOUNT9.ToString("#,0.##"));
                                    DECpay10.SetValue(DECNiit, DECAMOUNT10.ToString("#,0.##"));
                                    DECpay11.SetValue(DECNiit, DECAMOUNT11.ToString("#,0.##"));
                                    DECpay12.SetValue(DECNiit, DECAMOUNT12.ToString("#,0.##"));

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

                                    Bodytemplist1.AddRange(Bodytype1.OrderBy(m => m.DECISION_TYPE));
                                    Bodytemplist1.Add(DECNiit);
                                    Bodytype1 = Bodytemplist1;
                                }
                                if (Bodytype2.Count > 0)
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


                                    foreach (CM2 data in Bodytype2)
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
                                    DECType2pay.SetValue(DECNiit2, DECTYPE2AMOUNT.ToString("#,0.##"));
                                    DECType2pay1.SetValue(DECNiit2, DECTYPE2AMOUNT1.ToString("#,0.##"));
                                    DECType2pay2.SetValue(DECNiit2, DECTYPE2AMOUNT2.ToString("#,0.##"));
                                    DECType2pay3.SetValue(DECNiit2, DECTYPE2AMOUNT3.ToString("#,0.##"));
                                    DECType2pay4.SetValue(DECNiit2, DECTYPE2AMOUNT4.ToString("#,0.##"));
                                    DECType2pay5.SetValue(DECNiit2, DECTYPE2AMOUNT5.ToString("#,0.##"));
                                    DECType2pay6.SetValue(DECNiit2, DECTYPE2AMOUNT6.ToString("#,0.##"));
                                    DECType2pay7.SetValue(DECNiit2, DECTYPE2AMOUNT7.ToString("#,0.##"));
                                    DECType2pay8.SetValue(DECNiit2, DECTYPE2AMOUNT8.ToString("#,0.##"));
                                    DECType2pay9.SetValue(DECNiit2, DECTYPE2AMOUNT9.ToString("#,0.##"));
                                    DECType2pay10.SetValue(DECNiit2, DECTYPE2AMOUNT10.ToString("#,0.##"));
                                    DECType2pay11.SetValue(DECNiit2, DECTYPE2AMOUNT11.ToString("#,0.##"));
                                    DECType2pay12.SetValue(DECNiit2, DECTYPE2AMOUNT12.ToString("#,0.##"));

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

                                    Bodytemplist2.AddRange(Bodytype2.OrderBy(m => m.DECISION_TYPE));
                                    Bodytemplist2.Add(DECNiit2);
                                    Bodytype2 = Bodytemplist2;
                                }
                                if (Bodytype3.Count > 0)
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


                                    foreach (CM2 data in Bodytype3)
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
                                    DECType3pay.SetValue(DECNiit3, DECTYPE3AMOUNT.ToString("#,0.##"));
                                    DECType3pay1.SetValue(DECNiit3, DECTYPE3AMOUNT1.ToString("#,0.##"));
                                    DECType3pay2.SetValue(DECNiit3, DECTYPE3AMOUNT2.ToString("#,0.##"));
                                    DECType3pay3.SetValue(DECNiit3, DECTYPE3AMOUNT3.ToString("#,0.##"));
                                    DECType3pay4.SetValue(DECNiit3, DECTYPE3AMOUNT4.ToString("#,0.##"));
                                    DECType3pay5.SetValue(DECNiit3, DECTYPE3AMOUNT5.ToString("#,0.##"));
                                    DECType3pay6.SetValue(DECNiit3, DECTYPE3AMOUNT6.ToString("#,0.##"));
                                    DECType3pay7.SetValue(DECNiit3, DECTYPE3AMOUNT7.ToString("#,0.##"));
                                    DECType3pay8.SetValue(DECNiit3, DECTYPE3AMOUNT8.ToString("#,0.##"));
                                    DECType3pay9.SetValue(DECNiit3, DECTYPE3AMOUNT9.ToString("#,0.##"));
                                    DECType3pay10.SetValue(DECNiit3, DECTYPE3AMOUNT10.ToString("#,0.##"));
                                    DECType3pay11.SetValue(DECNiit3, DECTYPE3AMOUNT11.ToString("#,0.##"));
                                    DECType3pay12.SetValue(DECNiit3, DECTYPE3AMOUNT12.ToString("#,0.##"));

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

                                    Bodytemplist3.AddRange(Bodytype3.OrderBy(m => m.DECISION_TYPE));
                                    Bodytemplist3.Add(DECNiit3);
                                    Bodytype3 = Bodytemplist3;
                                }

                            Bodytemp.AddRange(Bodytype1);
                            Bodytemp.AddRange(Bodytype2);
                            Bodytemp.AddRange(Bodytype3);

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


                        pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                        pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                        pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                        pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                        pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));
                        pay5.SetValue(Niit, AMOUNT5.ToString("#,0.##"));
                        pay6.SetValue(Niit, AMOUNT6.ToString("#,0.##"));
                        pay7.SetValue(Niit, AMOUNT7.ToString("#,0.##"));
                        pay8.SetValue(Niit, AMOUNT8.ToString("#,0.##"));
                        pay9.SetValue(Niit, AMOUNT9.ToString("#,0.##"));
                        pay10.SetValue(Niit, AMOUNT10.ToString("#,0.##"));
                        pay11.SetValue(Niit, AMOUNT11.ToString("#,0.##"));

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

                        depname.SetValue(Niit, "НИЙТ ДҮН");

                        if (request.Type == 1)
                        {

                            List<CM3> typeNeg = new List<CM3>();
                            List<CM3> typeHoyor = new List<CM3>();

                            List<CM3> temp = new List<CM3>();
                            List<CM3> temp2 = new List<CM3>();

                            typeNeg = body.FindAll(a => a.IS_STATE.Equals(1));
                            typeHoyor = body.FindAll(a => a.IS_STATE.Equals(2));

                            if (typeNeg.Count > 0)
                            {
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

                                temp = new List<CM3>();
                                title = new CM3();
                                title.DEPARTMENT_NAME = "Нэг. Төрийн аудитын байгууллага";
                                foreach (CM3 data in typeNeg)
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
                                    //DEPdepname.SetValue(DECNiit, data.DEPARTMENT_NAME);
                                }
                                DECpay.SetValue(DECNiit, DECAMOUNT.ToString("#,0.##"));
                                DECpay1.SetValue(DECNiit, DECAMOUNT1.ToString("#,0.##"));
                                DECpay2.SetValue(DECNiit, DECAMOUNT2.ToString("#,0.##"));
                                DECpay3.SetValue(DECNiit, DECAMOUNT3.ToString("#,0.##"));
                                DECpay4.SetValue(DECNiit, DECAMOUNT4.ToString("#,0.##"));
                                DECpay5.SetValue(DECNiit, DECAMOUNT5.ToString("#,0.##"));
                                DECpay6.SetValue(DECNiit, DECAMOUNT6.ToString("#,0.##"));
                                DECpay7.SetValue(DECNiit, DECAMOUNT7.ToString("#,0.##"));
                                DECpay8.SetValue(DECNiit, DECAMOUNT8.ToString("#,0.##"));
                                DECpay9.SetValue(DECNiit, DECAMOUNT9.ToString("#,0.##"));
                                DECpay10.SetValue(DECNiit, DECAMOUNT10.ToString("#,0.##"));
                                DECpay11.SetValue(DECNiit, DECAMOUNT11.ToString("#,0.##"));

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

                                temp.Add(title);
                                temp.AddRange(typeNeg.OrderBy(m => m.DEPARTMENT_NAME));
                                temp.Add(DECNiit);
                                typeNeg = temp;
                            }

                            if (typeHoyor.Count > 0)
                            {
                                CM3 DEC2Niit = new CM3();
                                var DEC2depname = typ.GetProperty("BUDGET_TYPE_NAME");
                                var DEP22depname = typ.GetProperty("DEPARTMENT_NAME");

                                var DEC2pay = typ.GetProperty("C1_AMOUNT");
                                var DEC2pay1 = typ.GetProperty("CURRENT_AMOUNT");
                                var DEC2pay2 = typ.GetProperty("TOTAL_AMOUNT");
                                var DEC2pay3 = typ.GetProperty("COMPLETION_DONE_AMOUNT");
                                var DEC2pay4 = typ.GetProperty("COMPLETION_PROGRESS_AMOUNT");
                                var DEC2pay5 = typ.GetProperty("LAW_AMOUNT");
                                var DEC2pay6 = typ.GetProperty("LAW_CURRENT_AMOUNT");
                                var DEC2pay7 = typ.GetProperty("LAW_TOTAL_AMOUNT");
                                var DEC2pay8 = typ.GetProperty("LAW_COMP_DONE_AMOUNT");
                                var DEC2pay9 = typ.GetProperty("LAW_COMP_PROG_AMOUNT");
                                var DEC2pay10 = typ.GetProperty("LAW_COMP_INVALID_AMOUNT");
                                var DEC2pay11 = typ.GetProperty("C2_AMOUNT");


                                var DEC2count = typ.GetProperty("C1_COUNT");
                                var DEC2count1 = typ.GetProperty("CURRENT_COUNT");
                                var DEC2count2 = typ.GetProperty("TOTAL_COUNT");
                                var DEC2count3 = typ.GetProperty("COMPLETION_DONE_COUNT");
                                var DEC2count4 = typ.GetProperty("COMPLETION_PROGRESS_COUNT");
                                var DEC2count5 = typ.GetProperty("LAW_COUNT");
                                var DEC2count6 = typ.GetProperty("LAW_CURRENT_COUNT");
                                var DEC2count7 = typ.GetProperty("LAW_TOTAL_COUNT");
                                var DEC2count8 = typ.GetProperty("LAW_COMP_DONE_COUNT");
                                var DEC2count9 = typ.GetProperty("LAW_COMP_PROG_COUNT");
                                var DEC2count10 = typ.GetProperty("LAW_COMP_INVALID_COUNT");
                                var DEC2count11 = typ.GetProperty("C2_COUNT");


                                decimal DEC2AMOUNT = 0;
                                decimal DEC2AMOUNT1 = 0;
                                decimal DEC2AMOUNT2 = 0;
                                decimal DEC2AMOUNT3 = 0;
                                decimal DEC2AMOUNT4 = 0;
                                decimal DEC2AMOUNT5 = 0;
                                decimal DEC2AMOUNT6 = 0;
                                decimal DEC2AMOUNT7 = 0;
                                decimal DEC2AMOUNT8 = 0;
                                decimal DEC2AMOUNT9 = 0;
                                decimal DEC2AMOUNT10 = 0;
                                decimal DEC2AMOUNT11 = 0;

                                int DEC2NUMBER = 0;
                                int DEC2NUMBER1 = 0;
                                int DEC2NUMBER2 = 0;
                                int DEC2NUMBER3 = 0;
                                int DEC2NUMBER4 = 0;
                                int DEC2NUMBER5 = 0;
                                int DEC2NUMBER6 = 0;
                                int DEC2NUMBER7 = 0;
                                int DEC2NUMBER8 = 0;
                                int DEC2NUMBER9 = 0;
                                int DEC2NUMBER10 = 0;
                                int DEC2NUMBER11 = 0;

                                temp2 = new List<CM3>();
                                title = new CM3();
                                title.DEPARTMENT_NAME = "Хоёр. Хараат бус аудитын компани";
                                foreach (CM3 data in typeHoyor)
                                {

                                    if (data.C1_AMOUNT != null)
                                    {
                                        string strNii = data.C1_AMOUNT.Replace(",", "");
                                        Decimal Amount = Convert.ToDecimal(strNii);
                                        DEC2AMOUNT += Amount;

                                    }
                                    if (data.CURRENT_AMOUNT != null)
                                    {
                                        string strNii = data.CURRENT_AMOUNT.Replace(",", "");
                                        Decimal Amount = Convert.ToDecimal(strNii);
                                        DEC2AMOUNT1 += Amount;

                                    }
                                    if (data.TOTAL_AMOUNT != null)
                                    {
                                        string strNii = data.TOTAL_AMOUNT.Replace(",", "");
                                        Decimal Amount = Convert.ToDecimal(strNii);
                                        DEC2AMOUNT2 += Amount;

                                    }
                                    if (data.COMPLETION_DONE_AMOUNT != null)
                                    {
                                        string strNii = data.COMPLETION_DONE_AMOUNT.Replace(",", "");
                                        Decimal Amount = Convert.ToDecimal(strNii);
                                        DEC2AMOUNT3 += Amount;

                                    }
                                    if (data.COMPLETION_PROGRESS_AMOUNT != null)
                                    {
                                        string strNii = data.COMPLETION_PROGRESS_AMOUNT.Replace(",", "");
                                        Decimal Amount = Convert.ToDecimal(strNii);
                                        DEC2AMOUNT4 += Amount;

                                    }
                                    if (data.LAW_AMOUNT != null)
                                    {
                                        string strNii = data.LAW_AMOUNT.Replace(",", "");
                                        Decimal Amount = Convert.ToDecimal(strNii);
                                        DEC2AMOUNT5 += Amount;

                                    }
                                    if (data.LAW_CURRENT_AMOUNT != null)
                                    {
                                        string strNii = data.LAW_CURRENT_AMOUNT.Replace(",", "");
                                        Decimal Amount = Convert.ToDecimal(strNii);
                                        DEC2AMOUNT6 += Amount;

                                    }
                                    if (data.LAW_TOTAL_AMOUNT != null)
                                    {
                                        string strNii = data.LAW_TOTAL_AMOUNT.Replace(",", "");
                                        Decimal Amount = Convert.ToDecimal(strNii);
                                        DEC2AMOUNT7 += Amount;

                                    }
                                    if (data.LAW_COMP_DONE_AMOUNT != null)
                                    {
                                        string strNii = data.LAW_COMP_DONE_AMOUNT.Replace(",", "");
                                        Decimal Amount = Convert.ToDecimal(strNii);
                                        DEC2AMOUNT8 += Amount;

                                    }
                                    if (data.LAW_COMP_PROG_AMOUNT != null)
                                    {
                                        string strNii = data.LAW_COMP_PROG_AMOUNT.Replace(",", "");
                                        Decimal Amount = Convert.ToDecimal(strNii);
                                        DEC2AMOUNT9 += Amount;

                                    }
                                    if (data.LAW_COMP_INVALID_AMOUNT != null)
                                    {
                                        string strNii = data.LAW_COMP_INVALID_AMOUNT.Replace(",", "");
                                        Decimal Amount = Convert.ToDecimal(strNii);
                                        DEC2AMOUNT10 += Amount;

                                    }
                                    if (data.C2_AMOUNT != null)
                                    {
                                        string strNii = data.C2_AMOUNT.Replace(",", "");
                                        Decimal Amount = Convert.ToDecimal(strNii);
                                        DEC2AMOUNT11 += Amount;

                                    }

                                    if (data.C1_COUNT != 0)
                                    {
                                        DEC2NUMBER += Convert.ToInt32(data.C1_COUNT);
                                    }
                                    if (data.CURRENT_COUNT != 0)
                                    {
                                        DEC2NUMBER1 += Convert.ToInt32(data.CURRENT_COUNT);
                                    }
                                    if (data.TOTAL_COUNT != 0)
                                    {
                                        DEC2NUMBER2 += Convert.ToInt32(data.TOTAL_COUNT);
                                    }
                                    if (data.COMPLETION_DONE_COUNT != 0)
                                    {
                                        DEC2NUMBER3 += Convert.ToInt32(data.COMPLETION_DONE_COUNT);
                                    }
                                    if (data.COMPLETION_PROGRESS_COUNT != 0)
                                    {
                                        DEC2NUMBER4 += Convert.ToInt32(data.COMPLETION_PROGRESS_COUNT);
                                    }
                                    if (data.LAW_COUNT != 0)
                                    {
                                        DEC2NUMBER5 += Convert.ToInt32(data.LAW_COUNT);
                                    }
                                    if (data.LAW_CURRENT_COUNT != 0)
                                    {
                                        DEC2NUMBER6 += Convert.ToInt32(data.LAW_CURRENT_COUNT);
                                    }
                                    if (data.LAW_TOTAL_COUNT != 0)
                                    {
                                        DEC2NUMBER7 += Convert.ToInt32(data.LAW_TOTAL_COUNT);
                                    }
                                    if (data.LAW_COMP_DONE_COUNT != 0)
                                    {
                                        DEC2NUMBER8 += Convert.ToInt32(data.LAW_COMP_DONE_COUNT);
                                    }
                                    if (data.LAW_COMP_PROG_COUNT != 0)
                                    {
                                        DEC2NUMBER9 += Convert.ToInt32(data.LAW_COMP_PROG_COUNT);
                                    }
                                    if (data.LAW_COMP_INVALID_COUNT != 0)
                                    {
                                        DEC2NUMBER10 += Convert.ToInt32(data.LAW_COMP_INVALID_COUNT);
                                    }
                                    if (data.C2_COUNT != 0)
                                    {
                                        DEC2NUMBER11 += Convert.ToInt32(data.C2_COUNT);
                                    }
                                    //DEP22depname.SetValue(DEC2Niit, data.DEPARTMENT_NAME);
                                }
                                DEC2pay.SetValue(DEC2Niit, DEC2AMOUNT.ToString("#,0.##"));
                                DEC2pay1.SetValue(DEC2Niit, DEC2AMOUNT1.ToString("#,0.##"));
                                DEC2pay2.SetValue(DEC2Niit, DEC2AMOUNT2.ToString("#,0.##"));
                                DEC2pay3.SetValue(DEC2Niit, DEC2AMOUNT3.ToString("#,0.##"));
                                DEC2pay4.SetValue(DEC2Niit, DEC2AMOUNT4.ToString("#,0.##"));
                                DEC2pay5.SetValue(DEC2Niit, DEC2AMOUNT5.ToString("#,0.##"));
                                DEC2pay6.SetValue(DEC2Niit, DEC2AMOUNT6.ToString("#,0.##"));
                                DEC2pay7.SetValue(DEC2Niit, DEC2AMOUNT7.ToString("#,0.##"));
                                DEC2pay8.SetValue(DEC2Niit, DEC2AMOUNT8.ToString("#,0.##"));
                                DEC2pay9.SetValue(DEC2Niit, DEC2AMOUNT9.ToString("#,0.##"));
                                DEC2pay10.SetValue(DEC2Niit, DEC2AMOUNT10.ToString("#,0.##"));
                                DEC2pay11.SetValue(DEC2Niit, DEC2AMOUNT11.ToString("#,0.##"));

                                DEC2count.SetValue(DEC2Niit, DEC2NUMBER);
                                DEC2count1.SetValue(DEC2Niit, DEC2NUMBER1);
                                DEC2count2.SetValue(DEC2Niit, DEC2NUMBER2);
                                DEC2count3.SetValue(DEC2Niit, DEC2NUMBER3);
                                DEC2count4.SetValue(DEC2Niit, DEC2NUMBER4);
                                DEC2count5.SetValue(DEC2Niit, DEC2NUMBER5);
                                DEC2count6.SetValue(DEC2Niit, DEC2NUMBER6);
                                DEC2count7.SetValue(DEC2Niit, DEC2NUMBER7);
                                DEC2count8.SetValue(DEC2Niit, DEC2NUMBER8);
                                DEC2count9.SetValue(DEC2Niit, DEC2NUMBER9);
                                DEC2count10.SetValue(DEC2Niit, DEC2NUMBER10);
                                DEC2count11.SetValue(DEC2Niit, DEC2NUMBER11);

                                DEC2depname.SetValue(DEC2Niit, "Дүн");

                                temp2.Add(title);
                                temp2.AddRange(typeHoyor.OrderBy(m => m.DEPARTMENT_NAME));
                                temp2.Add(DEC2Niit);
                                typeHoyor = temp2;

                            }



                            List<CM3> types = new List<CM3>();
                            types.AddRange(typeNeg);
                            types.AddRange(typeHoyor);



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
                if (res != null && res.Elements("CM4A") != null)
                    response.data = (from item in res.Elements("CM4") select new CM4().SetXml(item)).ToList();

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


                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));
                    pay2.SetValue(Niit, AMOUNT2.ToString("#,0.##"));
                    pay3.SetValue(Niit, AMOUNT3.ToString("#,0.##"));
                    pay4.SetValue(Niit, AMOUNT4.ToString("#,0.##"));
                    pay5.SetValue(Niit, AMOUNT5.ToString("#,0.##"));

                    count.SetValue(Niit, NUMBER);
                    count1.SetValue(Niit, NUMBER1);
                    count2.SetValue(Niit, NUMBER2);
                    count3.SetValue(Niit, NUMBER3);
                    count4.SetValue(Niit, NUMBER4);
                    count5.SetValue(Niit, NUMBER5);

                    depname.SetValue(Niit, "НИЙТ ДҮН");


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

                    depname.SetValue(Niit, "НИЙТ ДҮН");


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

                    pay.SetValue(Niit, AMOUNT.ToString("#,0.##"));
                    pay1.SetValue(Niit, AMOUNT1.ToString("#,0.##"));

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

                    depname.SetValue(Niit, "НИЙТ ДҮН");


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
                                orgname.SetValue(Niit, "НИЙТ ДҮН");

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
                        orgname.SetValue(Niit, "НИЙТ ДҮН");
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
                                    orgname.SetValue(Niit, "НИЙТ ДҮН");

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
                                orgname.SetValue(Niit, "НИЙТ ДҮН");
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
                        orgname.SetValue(Niit, "НИЙТ ДҮН");
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
                                        orgname.SetValue(Niit, "НИЙТ ДҮН");
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
                                    orgname.SetValue(Niit, "НИЙТ ДҮН");
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
                        orgname.SetValue(Niit, "НИЙТ ДҮН");
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
