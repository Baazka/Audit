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

                if (request.budget_type != null)
                {
                    string ss = String.Join(",", request.budget_type.Select(p => p.ToString()).ToArray());
                    elem.Add(new XElement("V_BUDGET_TYPE", ss));
                }
                else
                    elem.Add(new XElement("V_BUDGET_TYPE", null));

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
                {
                    string ss = String.Join(",", request.budget_type.Select(p => p.ToString()).ToArray());
                    elem.Add(new XElement("V_BUDGET_TYPE", ss));
                }
                else
                    elem.Add(new XElement("V_BUDGET_TYPE", null));

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

                XElement res = AppStatic.SystemController.BM0(elem, User.GetClaimData("USER_TYPE"));
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

                XElement res = AppStatic.SystemController.BM1(elem, User.GetClaimData("USER_TYPE"));
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

                XElement res = AppStatic.SystemController.BM2(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("BM2") != null)
                    response.data = (from item in res.Elements("BM2") select new BM2().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.BM3(elem, User.GetClaimData("USER_TYPE"));
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

                XElement res = AppStatic.SystemController.BM4(elem, User.GetClaimData("USER_TYPE"));
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

                XElement res = AppStatic.SystemController.BM5(elem, User.GetClaimData("USER_TYPE"));
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

                XElement res = AppStatic.SystemController.BM6(elem, User.GetClaimData("USER_TYPE"));
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

                XElement res = AppStatic.SystemController.BM7(elem, User.GetClaimData("USER_TYPE"));
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

                XElement res = AppStatic.SystemController.BM8(elem, User.GetClaimData("USER_TYPE"));
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
                        elem.Add(new XElement("V_Mayagt", request.Mayagt = "1,2"));
                    if (request.Mayagt == "2")
                        elem.Add(new XElement("V_Mayagt", request.Mayagt = "3"));
                }
                else
                    elem.Add(new XElement("V_Mayagt", null));

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

                XElement res = AppStatic.SystemController.N1(elem, User.GetClaimData("USER_TYPE")); 
                if (res != null && res.Elements("N1") != null)
                {
                    List<N1> n1 = new List<N1>();
                    //n1 = (from item in res.Elements("N1") select new N1().SetXml(item)).ToList();
                    List<N1> n1Detial = new List<N1>();
                    N1 title = new N1();
                  
                    
                    List<N1> n2 = new List<N1>();
                    n1 =(from item in res.Elements("N1Footer") select new N1().SetXml(item)).ToList();
                    n2 = (from item in res.Elements("N1") select new N1().SetXml(item)).ToList();
                    N1 nFooter = new N1();

                    foreach (N1 n in n2)
                    {
                        nFooter = n1.Find(a => (a.ORGID.Equals(n.ORGID)));
                        if(nFooter != null)
                        {
                            n1Detial.Add(setMd(nFooter, n));
                        }
                    }
                    List<N1> typeNeg = new List<N1>();
                    List<N1> typeHoyor = new List<N1>();
                    List<N1> typeGurav= new List<N1>();

                    List<N1> temp = new List<N1>();
                   

                    typeNeg = n1Detial.FindAll(a => a.ORGTYPE.Equals("Төсвийн ерөнхийлөн захирагч"));
                    typeHoyor = n1Detial.FindAll(a => a.ORGTYPE.Equals("Төсвийн төвлөрүүлэн захирагч"));
                    typeGurav = n1Detial.FindAll(a => a.ORGTYPE.Equals("Төсвийн шууд захирагч"));

                    if (typeNeg.Count > 0)
                    {
                        title = new N1();
                        title.ORGNAME = "Төсвийн ерөнхийлөн захирагч";
                        temp.Add(title);
                        temp.AddRange(typeNeg);
                        typeNeg = temp;
                        temp = new List<N1>(); 
                        
                    }

                    if (typeHoyor.Count > 0)
                    {
                        title = new N1();
                        title.ORGNAME = "Төсвийн төвлөрүүлэн захирагч";
                        temp.Add(title);
                        temp.AddRange(typeHoyor);
                        typeHoyor = temp;
                        temp = new List<N1>(); 

                    }

                    if (typeGurav.Count > 0)
                    {
                        title = new N1();
                        title.ORGNAME = "Төсвийн шууд захирагч";
                        temp.Add(title);
                        temp.AddRange(typeGurav);
                        typeGurav = temp;
                        temp = new List<N1>(); 

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
                   
                    n1 = (from item in res.Elements("N1Footer") select new N1().SetXml(item)).ToList();
                    var typ = typeof(N1);
                    var orgname = typ.GetProperty("ORGNAME");
                    decimal total = 0;
                    decimal math1 = 0;
                    decimal math2 = 0;
                    decimal count = 0;
                    foreach (N1 n in n1)
                    {
                        
                        for (int i = 1; i <= 35; i++)
                        {
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
                                total = Convert.ToInt32(prop.GetValue(Medeelsen)) + Convert.ToInt32(prop.GetValue(Medeeleegui)) + Convert.ToInt32(prop.GetValue(HugtsaaHotsorson)) + Convert.ToInt32(prop.GetValue(Shaardlaggui));
                                prop.SetValue(Niit, total.ToString());
                                orgname.SetValue(Niit, "НИЙТ ДҮН");

                                if (total != 0)
                                {
                                    math1 = 100 - 100 * Convert.ToInt32(prop.GetValue(Medeeleegui)) / total - Convert.ToInt32(prop.GetValue(HugtsaaHotsorson));
                                    prop.SetValue(bodolt1, String.Format("{0:0.0}", math1));
                                    orgname.SetValue(bodolt1, "Мэдээлсэн байдлын хэрэгжилтийн хувь");
                                }

                                if (total != 0)
                                {
                                    math2 = 100 - 100 * Convert.ToInt32(prop.GetValue(Shaardlaggui)) / total - Convert.ToInt32(prop.GetValue(HugtsaaHotsorson));
                                    prop.SetValue(bodolt2, String.Format("{0:0.0}", math2));
                                    orgname.SetValue(bodolt2, "Хугацаа хоцролтын хэрэгжилтийн хувь");
                                }
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
                        elem.Add(new XElement("V_Mayagt", request.Mayagt = "1,2"));
                    if (request.Mayagt == "2")
                        elem.Add(new XElement("V_Mayagt", request.Mayagt = "3"));
                }
                else
                    elem.Add(new XElement("V_Mayagt", null));

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

                XElement res = AppStatic.SystemController.Report1N2(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("Report1N2") != null)
                {
                    List<N1> n1 = new List<N1>();
                    //n1 = (from item in res.Elements("N1") select new N1().SetXml(item)).ToList();
                    List<N1> n1Detial = new List<N1>();
                    N1 title = new N1();


                    List<N1> n2 = new List<N1>();
                    n1 = (from item in res.Elements("Report1N2Footer") select new N1().SetXml(item)).ToList();
                    n2 = (from item in res.Elements("Report1N2") select new N1().SetXml(item)).ToList();
                    N1 nFooter = new N1();

                    foreach (N1 n in n2)
                    {
                        nFooter = n1.Find(a => (a.ORGID.Equals(n.ORGID)));
                        if (nFooter != null)
                        {
                            n1Detial.Add(setMd(nFooter, n));
                        }
                    }
                    List<N1> typeNeg = new List<N1>();
                    List<N1> typeHoyor = new List<N1>();
                    List<N1> typeGurav = new List<N1>();

                    List<N1> temp = new List<N1>();
                    List<N1> temp2 = new List<N1>();
                    List<N1> temp3 = new List<N1>();

                    typeNeg = n1Detial.FindAll(a => a.ORGTYPE.Equals("Төсвийн ерөнхийлөн захирагч"));
                    typeHoyor = n1Detial.FindAll(a => a.ORGTYPE.Equals("Төсвийн төвлөрүүлэн захирагч"));
                    typeGurav = n1Detial.FindAll(a => a.ORGTYPE.Equals("Төсвийн шууд захирагч"));

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

                    n1 = (from item in res.Elements("Report1N2Footer") select new N1().SetXml(item)).ToList();
                    var typ = typeof(N1);
                    var orgname = typ.GetProperty("ORGNAME");
                    decimal total = 0;
                    decimal math1 = 0;
                    decimal math2 = 0;
                    decimal count = 0;

                    string[] key = {"MD33","MD34","MD37","MD38","MD39","MD40","MD41","MD42","MD43","MD44","MD46","MD47","MD48","MD49","MD50","MD51","MD60","MD61","MD62","MD63","MD64","MD65","MD53","MD54","MD55","MD56",
                                    "MD57","MD58","MD66","MD67","MD69","MD70","MD71","MD72","MD73","MD74","MD76","MD77","MD78","MD79","MD80","MD81","MD83","MD84","MD85","MD86","MD87","MD88","MD90","MD91","MD92","MD93","MD94",
                                    "MD95","MD97","MD98","MD99","MD100","MD101","MD102","MD161","MD105","MD106","MD165","MD166","MD167","MD168","MD169" };

                    foreach (N1 n in n1)
                    {

                        for (int i = 0; i <= key.Length; i++)
                        {
                            var prop = typ.GetProperty(key[i]);
                            string value = prop.GetValue(n) != null ? prop.GetValue(n).ToString() : "";
                            if (key[i].Equals("MD33") || key[i].Equals("MD34") || key[i].Equals("MD40") || key[i].Equals("MD44") || key[i].Equals("MD66") || key[i].Equals("MD67")) {
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
                                        math1 = 100 - 100 * Convert.ToInt32(prop.GetValue(Medeeleegui)) / total - Convert.ToInt32(prop.GetValue(HugtsaaHotsorson));
                                        prop.SetValue(bodolt1, String.Format("{0:0.0}", math1));
                                        orgname.SetValue(bodolt1, "Мэдээлсэн байдлын хэрэгжилтийн хувь");
                                    }

                                    if (total != 0)
                                    {
                                        math2 = 100 - 100 * Convert.ToInt32(prop.GetValue(Shaardlaggui)) / total - Convert.ToInt32(prop.GetValue(HugtsaaHotsorson));
                                              .SetValue(bodolt2, String.Format("{0:0.0}", math2));
                                        orgname.SetValue(bodolt2, "Хугацаа хоцролтын хэрэгжилтийн хувь");
                                    }
                                }
                            }
                            else if(key[i].Equals("MD37") || key[i].Equals("MD38") || key[i].Equals("MD39") || key[i].Equals("MD40") || key[i].Equals("MD41") || key[i].Equals("MD42") || key[i].Equals("MD43") || key[i].Equals("MD44"))
                            {
                                total = Convert.ToInt32(prop.GetValue(Medeelsen));

                            }
                            else
                            {
                                total = Convert.ToInt32(prop.GetValue(Medeelsen)) + Convert.ToInt32(prop.GetValue(Medeeleegui)) + Convert.ToInt32(prop.GetValue(HugtsaaHotsorson)) + Convert.ToInt32(prop.GetValue(Shaardlaggui));
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
        public N1 setMd(N1 param1, N1 param2)
        {
            param1.MD1 = param2.MD1;
            param1.MD2 = param2.MD2;
            param1.MD3 = param2.MD3;
            param1.MD4 = param2.MD4;
            param1.MD5 = param2.MD5;
            param1.MD6 = param2.MD6;
            param1.MD7 = param2.MD7;
            param1.MD8 = param2.MD8;
            param1.MD9 = param2.MD9;
            param1.MD10 = param2.MD10;
            param1.MD11 = param2.MD11;
            param1.MD12 = param2.MD12;
            param1.MD13 = param2.MD13;
            param1.MD14 = param2.MD14;
            param1.MD15 = param2.MD15;
            param1.MD16 = param2.MD16;
            param1.MD17 = param2.MD17;
            param1.MD18 = param2.MD18;
            param1.MD19 = param2.MD19;
            param1.MD20 = param2.MD20;
            param1.MD21 = param2.MD21;
            param1.MD22 = param2.MD22;
            param1.MD23 = param2.MD23;
            param1.MD24 = param2.MD24;
            param1.MD25 = param2.MD25;
            param1.MD26 = param2.MD26;
            param1.MD27 = param2.MD27;
            param1.MD28 = param2.MD28;
            param1.MD29 = param2.MD29;
            param1.MD30 = param2.MD30;
            param1.MD31 = param2.MD31;
            param1.MD32 = param2.MD32;
            param1.MD33 = param2.MD33;
            param1.MD34 = param2.MD34;
            param1.MD35 = param2.MD35;
            param1.MD36 = param2.MD36;
            param1.MD37 = param2.MD37;
            param1.MD38 = param2.MD38;
            param1.MD39 = param2.MD39;
            param1.MD40 = param2.MD40;
            param1.MD41 = param2.MD41;
            param1.MD42 = param2.MD42;
            param1.MD43 = param2.MD43;
            param1.MD44 = param2.MD44;
            param1.MD45 = param2.MD45;
            param1.MD46 = param2.MD46;
            param1.MD47 = param2.MD47;
            param1.MD48 = param2.MD48;
            param1.MD49 = param2.MD49;
            param1.MD50 = param2.MD50;
            param1.MD51 = param2.MD51;
            param1.MD52 = param2.MD52;
            param1.MD53 = param2.MD53;
            param1.MD54 = param2.MD54;
            param1.MD55 = param2.MD55;
            param1.MD56 = param2.MD56;
            param1.MD57 = param2.MD57;
            param1.MD58 = param2.MD58;
            param1.MD59 = param2.MD59;
            param1.MD60 = param2.MD60;
            param1.MD61 = param2.MD61;
            param1.MD62 = param2.MD62;
            param1.MD63 = param2.MD63;
            param1.MD64 = param2.MD64;
            param1.MD65 = param2.MD65;
            param1.MD66 = param2.MD66;
            param1.MD67 = param2.MD67;
            param1.MD68 = param2.MD68;
            param1.MD69 = param2.MD69;
            param1.MD70 = param2.MD70;
            param1.MD71 = param2.MD71;
            param1.MD72 = param2.MD72;
            param1.MD73 = param2.MD73;
            param1.MD74 = param2.MD74;
            param1.MD75 = param2.MD75;
            param1.MD76 = param2.MD76;
            param1.MD77 = param2.MD77;
            param1.MD78 = param2.MD78;
            param1.MD79 = param2.MD79;
            param1.MD80 = param2.MD80;
            param1.MD81 = param2.MD81;
            param1.MD82 = param2.MD82;
            param1.MD83 = param2.MD83;
            param1.MD84 = param2.MD84;
            param1.MD85 = param2.MD85;
            param1.MD86 = param2.MD86;
            param1.MD87 = param2.MD87;
            param1.MD88 = param2.MD88;
            param1.MD89 = param2.MD89;
            param1.MD90 = param2.MD90;
            param1.MD91 = param2.MD91;
            param1.MD92 = param2.MD92;
            param1.MD93 = param2.MD93;
            param1.MD94 = param2.MD94;
            param1.MD95 = param2.MD95;
            param1.MD96 = param2.MD96;
            param1.MD97 = param2.MD97;
            param1.MD98 = param2.MD98;
            param1.MD99 = param2.MD99;
            param1.MD100 = param2.MD100;
            param1.MD101 = param2.MD101;
            param1.MD102 = param2.MD102;
            param1.MD103 = param2.MD103;
            param1.MD104 = param2.MD104;
            param1.MD105 = param2.MD105;
            param1.MD106 = param2.MD106;
            param1.MD158 = param2.MD158;
            param1.MD159 = param2.MD159;
            param1.MD160 = param2.MD160;
            param1.MD161 = param2.MD161;
            param1.MD162 = param2.MD162;
            param1.MD163 = param2.MD163;
            param1.MD164 = param2.MD164;
            param1.MD165 = param2.MD165;
            param1.MD166 = param2.MD166;
            param1.MD167 = param2.MD167;
            param1.MD168 = param2.MD168;
            param1.MD169 = param2.MD169;

            param1.OPEN_HEAD_ROLE = param1.OPEN_HEAD_ROLE + " " + param1.OPEN_HEAD_NAME + " " + param1.OPEN_HEAD_PHONE;
            param1.OPEN_ACC_ROLE = param1.OPEN_ACC_ROLE + " " + param1.OPEN_ACC_NAME + " " + param1.OPEN_ACC_PHONE;


            return param1;

        }
       


    }
}
