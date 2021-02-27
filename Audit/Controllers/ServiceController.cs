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
                    elem.Add(new XElement("OrderDir", request.order[0].dir));
                }
                //if (!string.IsNullOrEmpty(request.columns[request.order[0].column].name))
                //{
                   
                //}
                if (!string.IsNullOrEmpty(request.search.value))
                {
                    elem.Add(new XElement("Search", request.search.value));
                }
                if(request.DeparmentID != 0)
                {
                    elem.Add(new XElement("V_DEPARTMENT", request.DeparmentID));
                }                

                if (request.status.Count != 0)
                {
                    elem.Add(new XElement("V_STATUS", request.status));
                }
                if (request.violation.Count !=0)
                {
                    elem.Add(new XElement("V_VIOLATION", request.violation));
                }

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
                    elem.Add(new XElement("OrderDir", request.order[0].dir));
                }
                if (!string.IsNullOrEmpty(request.search.value))
                {
                    elem.Add(new XElement("Search", request.search.value));
                }

                XElement res = AppStatic.SystemController.BM8(elem, User.GetClaimData("DepartmentID"));
                if (res != null && res.Elements("BM8") != null)
                    response.data = res.Elements("BM8").Select(m => new BM8().SetXml(m)).ToList();

                response.recordsTotal = Convert.ToInt32(res.Element("TotalRow")?.Value);
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
