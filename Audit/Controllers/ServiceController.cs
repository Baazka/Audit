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
    }
}
