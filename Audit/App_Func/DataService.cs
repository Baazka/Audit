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

            functions.Add("Library", (request) => DataAccess.Library(request));
            functions.Add("MenuList", (request) => DataAccess.MenuList(request));
            functions.Add("MenuRole", (request) => DataAccess.MenuRole(request));
            functions.Add("UserLogin", (request) => DataAccess.UserLogin(request));
            functions.Add("UserProfile", (request) => DataAccess.UserProfile(request));
            functions.Add("OrgList", (request) => DataAccess.OrgList(request));
            functions.Add("OrgDetail", (request) => DataAccess.OrgDetail(request));
            functions.Add("OrgUB", (request) => DataAccess.OrgUB(request));
            functions.Add("OrgUBsingle", (request) => DataAccess.OrgUBsingle(request));
            functions.Add("OrgMOF", (request) => DataAccess.OrgMOF(request));
            functions.Add("OrgMOFsingle", (request) => DataAccess.OrgMOFsingle(request));
            functions.Add("OrgTAX", (request) => DataAccess.OrgTAX(request));
            functions.Add("OrgTAXsingle", (request) => DataAccess.OrgTAXsingle(request));
            functions.Add("OrgSave", (request) => DataAccess.OrgSave(request));
            functions.Add("OrgInsert", (request) => DataAccess.OrgInsert(request));
            functions.Add("OrgConfirm", (request) => DataAccess.OrgConfirm(request));
            functions.Add("OrgDelete", (request) => DataAccess.OrgDelete(request));
            functions.Add("DataSearch", (request) => DataAccess.DataSearch(request));


            functions.Add("BM0", (request) => DataAccess.BM0(request));
            functions.Add("BM1", (request) => DataAccess.BM1(request));
            functions.Add("BM2", (request) => DataAccess.BM2(request));
            functions.Add("BM3", (request) => DataAccess.BM3(request));
            functions.Add("BM4", (request) => DataAccess.BM4(request));
            functions.Add("BM5", (request) => DataAccess.BM5(request));
            functions.Add("BM6", (request) => DataAccess.BM6(request));
            functions.Add("BM7", (request) => DataAccess.BM7(request));
            functions.Add("BM8", (request) => DataAccess.BM8(request));
            functions.Add("NM1", (request) => DataAccess.NM1(request));
            functions.Add("NM2", (request) => DataAccess.NM2(request));
            functions.Add("NM3", (request) => DataAccess.NM3(request));
            functions.Add("NM4", (request) => DataAccess.NM4(request));
            functions.Add("NM5", (request) => DataAccess.NM5(request));
            functions.Add("NM6", (request) => DataAccess.NM6(request));
            functions.Add("NM7", (request) => DataAccess.NM7(request));
            functions.Add("CM1A", (request) => DataAccess.CM1A(request));
            functions.Add("CM1B", (request) => DataAccess.CM1B(request));
            functions.Add("CM1C", (request) => DataAccess.CM1C(request));
            functions.Add("CM2A", (request) => DataAccess.CM2A(request));
            functions.Add("CM2B", (request) => DataAccess.CM2B(request));
            functions.Add("CM2C", (request) => DataAccess.CM2C(request));
            functions.Add("CM3A", (request) => DataAccess.CM3A(request));
            functions.Add("CM3B", (request) => DataAccess.CM3B(request));
            functions.Add("CM3C", (request) => DataAccess.CM3C(request));
            functions.Add("CM4A", (request) => DataAccess.CM4A(request));
            functions.Add("CM4B", (request) => DataAccess.CM4B(request));
            functions.Add("CM4C", (request) => DataAccess.CM4C(request));
            functions.Add("CM5", (request) => DataAccess.CM5(request));
            functions.Add("CM6", (request) => DataAccess.CM6(request));
            functions.Add("CM7", (request) => DataAccess.CM7(request));
            functions.Add("CM8", (request) => DataAccess.CM8(request));

            functions.Add("Table1List", (request) => DataAccess.Table1List(request));
            functions.Add("MirrorAccInsert", (request) => DataAccess.MirrorAccInsert(request));
            functions.Add("MirrDataList", (request) => DataAccess.MirrDataList(request));
            functions.Add("OrgProjectInsert", (request) => DataAccess.OrgProjectInsert(request));
            functions.Add("TableProjectList", (request) => DataAccess.TableProjectList(request));
            functions.Add("OrgProjectDataList", (request) => DataAccess.OrgProjectDataList(request));
        }
    }
}