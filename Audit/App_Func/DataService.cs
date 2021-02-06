using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.App_Func
{
    public class DataService
    {

        public Dictionary<string, Func<XElement, DataResponse>> functions;
        public DataResponse GetResponse(XElement request)
        {
            DataResponse response = new DataResponse();

            try
            {
                string key = request.Element("Function").Value;

                if (functions.ContainsKey(key))
                {
                    response = functions[key](request);
                }
                else
                {
                    response.CreateResponse(new Exception("Invalid function name!"));
                }
            }
            catch (Exception ex)
            {
                response.CreateResponse(ex);
            }

            return response;
        }
        public DataService()
        {
            functions = new Dictionary<string, Func<XElement, DataResponse>>();

            functions.Add("LibraryList", (request) => DataAccess.LibraryList(request));
            functions.Add("UserLogin", (request) => DataAccess.UserLogin(request));
            functions.Add("UserProfile", (request) => DataAccess.UserProfile(request));
            functions.Add("Dashboard", (request) => DataAccess.Dashboard(request));
            functions.Add("OrgList", (request) => DataAccess.OrgList(request));
            functions.Add("OrgAddEdit", (request) => DataAccess.OrgAddEdit(request));
            functions.Add("OrgDetail", (request) => DataAccess.OrgDetail(request));



            functions.Add("BM1", (request) => DataAccess.BM1(request));

        }
    }
}