using Audit.App_Func;
using Audit.Models;
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

                if(request.DeparmentID != null)
                    elem.Add(new XElement("V_DEPARTMENT",  request.DeparmentID));
                else
                    elem.Add(new XElement("V_DEPARTMENT", null));

                if (request.status != null)
                {
                    string ss = String.Join(",", request.status.Select(p => p.ToString()).ToArray());
                    elem.Add(new XElement("V_STATUS", ss));
                }
                else
                    elem.Add(new XElement("V_STATUS", null));

                if (request.violation !=null)
                    elem.Add(new XElement("V_VIOLATION", request.violation));
                else
                    elem.Add(new XElement("V_VIOLATION", null));

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
        public OrgListResponse MirrorOrgList(OrgListRequest request)
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

                XElement res = AppStatic.SystemController.MirrorOrgList(elem, User.GetClaimData("DepartmentID"));
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

                XElement res = AppStatic.SystemController.CM1A(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("CM1A") != null)
                    response.data = (from item in res.Elements("CM1A") select new CM1().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.CM2A(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("CM2A") != null)
                    response.data = (from item in res.Elements("CM2A") select new CM2().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.CM3A(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("CM3A") != null)
                    response.data = (from item in res.Elements("CM3A") select new CM3().SetXml(item)).ToList();

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

                XElement res = AppStatic.SystemController.CM4A(elem, User.GetClaimData("USER_TYPE"));
                if (res != null && res.Elements("CM4A") != null)
                    response.data = (from item in res.Elements("CM4A") select new CM4().SetXml(item)).ToList();

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
    }
}
