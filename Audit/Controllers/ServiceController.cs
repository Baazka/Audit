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


                if (request.budget_type != null)
                {
                    string ss = String.Join(",", request.budget_type.Select(p => p.ToString()).ToArray());
                    elem.Add(new XElement("V_BUDGET_TYPE", ss));
                }
                else
                    elem.Add(new XElement("V_BUDGET_TYPE", null));

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
                    elem.Add(new XElement("V_ParentBudgetID", request.PARENT_BUDGET_ID));
                else
                    elem.Add(new XElement("V_ParentBudgetID", null));

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

                XElement res = AppStatic.SystemController.BM0(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"));
                if (res != null && res.Elements("BM0") != null)
                    response.data = (from item in res.Elements("BM0") select new BM0().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.BM1(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"));
                if (res != null && res.Elements("BM1") != null)
                    response.data = (from item in res.Elements("BM1") select new BM1().SetXml(item)).ToList();

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
                XElement res = AppStatic.SystemController.BM2(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"));
                if (res != null && res.Elements("BM2") != null)
                {
                    response.data = (from item in res.Elements("BM2") select new BM2().SetXml(item)).ToList();
                    response.recordsTotal = Convert.ToInt32(res.Element("RowCount")?.Value);
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

                XElement res = AppStatic.SystemController.BM3(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"));
                if (res != null && res.Elements("BM3") != null)
                    response.data = (from item in res.Elements("BM3") select new BM3().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.BM4(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"));
                if (res != null && res.Elements("BM4") != null)
                    response.data = (from item in res.Elements("BM4") select new BM4().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.BM5(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"));
                if (res != null && res.Elements("BM5") != null)
                    response.data = (from item in res.Elements("BM5") select new BM5().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.BM6(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"));
                if (res != null && res.Elements("BM6") != null)
                    response.data = (from item in res.Elements("BM6") select new BM6().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.BM7(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"));
                if (res != null && res.Elements("BM7") != null)
                    response.data = (from item in res.Elements("BM7") select new BM7().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.BM8(elem, User.GetClaimData("USER_TYPE"), User.GetClaimData("DepartmentID"));
                if (res != null && res.Elements("BM8") != null)
                    response.data = (from item in res.Elements("BM8") select new BM8().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.NM1(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("NM1") != null)
                    response.data = (from item in res.Elements("NM1") select new NM1().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.NM2(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("NM2") != null)
                    response.data = (from item in res.Elements("NM2") select new NM2().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.NM3(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("NM3") != null)
                    response.data = (from item in res.Elements("NM3") select new NM3().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.NM4(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("NM4") != null)
                    response.data = (from item in res.Elements("NM4") select new NM4().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.NM5(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("NM5") != null)
                    response.data = (from item in res.Elements("NM5") select new NM5().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.NM6(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("NM6") != null)
                    response.data = (from item in res.Elements("NM6") select new NM6().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.NM7(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("NM7") != null)
                    response.data = (from item in res.Elements("NM7") select new NM7().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.CM1(elem, User.GetClaimData("USER_TYPE"));
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

                XElement res = AppStatic.SystemController.CM2(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("CM2") != null)
                    response.data = (from item in res.Elements("CM2") select new CM2().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.CM3(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("CM3") != null)
                    response.data = (from item in res.Elements("CM3") select new CM3().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.CM4(elem, User.GetClaimData("USER_TYPE"));
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

                XElement res = AppStatic.SystemController.CM5(elem, User.GetClaimData("USER_TYPE"));
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

                XElement res = AppStatic.SystemController.CM6(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("CM6") != null)
                    response.data = (from item in res.Elements("CM6") select new CM6().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.CM7(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("CM7") != null)
                    response.data = (from item in res.Elements("CM7") select new CM7().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.CM8(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("CM8") != null)
                    response.data = (from item in res.Elements("CM8") select new CM8().SetXml(item)).ToList();

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
