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
            functions.Add("UserCodeChange", (request) => DataAccess.UserCodeChange(request));
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


            functions.Add("SystemUser", (request) => DataAccess.SystemUser(request));
            functions.Add("BM0", (request) => DataAccess.BM0(request));
            functions.Add("BM0Detail", (request) => DataAccess.BM0Detail(request));
            functions.Add("BM0Insert", (request) => DataAccess.BM0Insert(request));
            functions.Add("BM0Update", (request) => DataAccess.BM0Update(request));
            functions.Add("BM0Delete", (request) => DataAccess.BM0Delete(request));
            functions.Add("BM1", (request) => DataAccess.BM1(request));
            functions.Add("BM1Detail", (request) => DataAccess.BM1Detail(request));
            functions.Add("BM1Insert", (request) => DataAccess.BM1Insert(request));
            functions.Add("BM1Update", (request) => DataAccess.BM1Update(request));
            functions.Add("BM1Delete", (request) => DataAccess.BM1Delete(request));
            functions.Add("BM2", (request) => DataAccess.BM2(request));
            functions.Add("BM2Detail", (request) => DataAccess.BM2Detail(request));
            functions.Add("BM2Insert", (request) => DataAccess.BM2Insert(request));
            functions.Add("BM2Update", (request) => DataAccess.BM2Update(request));
            functions.Add("BM2Delete", (request) => DataAccess.BM2Delete(request));
            functions.Add("BM3", (request) => DataAccess.BM3(request));
            functions.Add("BM3Detail", (request) => DataAccess.BM3Detail(request));
            functions.Add("BM3Insert", (request) => DataAccess.BM3Insert(request));
            functions.Add("BM3Update", (request) => DataAccess.BM3Update(request));
            functions.Add("BM3Delete", (request) => DataAccess.BM3Delete(request));
            functions.Add("BM4", (request) => DataAccess.BM4(request));
            functions.Add("BM4Detail", (request) => DataAccess.BM4Detail(request));
            functions.Add("BM4Insert", (request) => DataAccess.BM4Insert(request));
            functions.Add("BM4Update", (request) => DataAccess.BM4Update(request));
            functions.Add("BM4Delete", (request) => DataAccess.BM4Delete(request));
            functions.Add("BM5", (request) => DataAccess.BM5(request));
            functions.Add("BM5Detail", (request) => DataAccess.BM5Detail(request));
            functions.Add("BM5Insert", (request) => DataAccess.BM5Insert(request));
            functions.Add("BM5Update", (request) => DataAccess.BM5Update(request));
            functions.Add("BM5Delete", (request) => DataAccess.BM5Delete(request));
            functions.Add("BM6", (request) => DataAccess.BM6(request));
            functions.Add("BM7", (request) => DataAccess.BM7(request));
            functions.Add("BM8", (request) => DataAccess.BM8(request));
            functions.Add("BM8Detail", (request) => DataAccess.BM8Detail(request));
            functions.Add("BM8Insert", (request) => DataAccess.BM8Insert(request));
            functions.Add("BM8Update", (request) => DataAccess.BM8Update(request));
            functions.Add("BM8Delete", (request) => DataAccess.BM8Delete(request));
            functions.Add("NM1", (request) => DataAccess.NM1(request));
            functions.Add("NM2", (request) => DataAccess.NM2(request));
            functions.Add("NM3", (request) => DataAccess.NM3(request));
            functions.Add("NM4", (request) => DataAccess.NM4(request));
            functions.Add("NM5", (request) => DataAccess.NM5(request));
            functions.Add("NM6", (request) => DataAccess.NM6(request));
            functions.Add("NM7", (request) => DataAccess.NM7(request));
            functions.Add("CM1", (request) => DataAccess.CM1(request));
            functions.Add("CM2", (request) => DataAccess.CM2(request));
            functions.Add("CM3", (request) => DataAccess.CM3(request));
            functions.Add("CM4", (request) => DataAccess.CM4(request));
            functions.Add("CM5", (request) => DataAccess.CM5(request));
            functions.Add("CM6", (request) => DataAccess.CM6(request));
            functions.Add("CM6Detail", (request) => DataAccess.CM6Detail(request));
            functions.Add("CM6Insert", (request) => DataAccess.CM6Insert(request));
            functions.Add("CM6Update", (request) => DataAccess.CM6Update(request));
            functions.Add("CM6Delete", (request) => DataAccess.CM6Delete(request));
            functions.Add("CM7", (request) => DataAccess.CM7(request));
            functions.Add("CM7Detail", (request) => DataAccess.CM7Detail(request));
            functions.Add("CM7Insert", (request) => DataAccess.CM7Insert(request));
            functions.Add("CM7Update", (request) => DataAccess.CM7Update(request));
            functions.Add("CM7Delete", (request) => DataAccess.CM7Delete(request));
            functions.Add("CM8", (request) => DataAccess.CM8(request));
            functions.Add("CM8Detail", (request) => DataAccess.CM8Detail(request));
            functions.Add("CM8Insert", (request) => DataAccess.CM8Insert(request));
            functions.Add("CM8Update", (request) => DataAccess.CM8Update(request));
            functions.Add("CM8Delete", (request) => DataAccess.CM8Delete(request));
            functions.Add("OrgSearch", (request) => DataAccess.OrgSearch(request));
            functions.Add("BM0Search", (request) => DataAccess.BM0Search(request));

            functions.Add("MirrorOrgList", (request) => DataAccess.MirrorOrgList(request));
            functions.Add("MirrorOrgDetail", (request) => DataAccess.MirrorOrgDetail(request));
            functions.Add("Table1List", (request) => DataAccess.Table1List(request));
            functions.Add("MirrorAccInsert", (request) => DataAccess.MirrorAccInsert(request));
            functions.Add("MirrDataList", (request) => DataAccess.MirrDataList(request));
            functions.Add("OrgProjectInsert", (request) => DataAccess.OrgProjectInsert(request));
            functions.Add("TableProjectList", (request) => DataAccess.TableProjectList(request));
            functions.Add("OrgProjectDataList", (request) => DataAccess.OrgProjectDataList(request));
            functions.Add("OrgProjectDelete", (request) => DataAccess.OrgProjectDelete(request));
            functions.Add("PrintDataList", (request) => DataAccess.PrintDataList(request));
            functions.Add("Print2DataList", (request) => DataAccess.Print2DataList(request));

            functions.Add("N1", (request) => DataAccess.ReportN1(request));
        }
    }
}