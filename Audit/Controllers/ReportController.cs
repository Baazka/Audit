using System;
using System.Configuration;
using System.Net;
using Audit.Models;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.AspNet.Identity;
using Audit.App_Func;
using System.Xml.Linq;
using System.Linq;


namespace Audit.Controllers
{
    
    public class ReportController : Controller
    {

        ReportViewer ReportViewer1 = new ReportViewer();
        string ssrsURL = System.Configuration.ConfigurationManager.AppSettings["SSRSReportURL"].ToString();
        public string title = "";
        // local variable for network credential.



        public ActionResult Index()
        {
            //ReportViewer ReportViewer1 = new ReportViewer();
            /*viewer.ProcessingMode = ProcessingMode.Local;
            LocalReport localReport = viewer.LocalReport;
            localReport.ReportPath = System.Web.HttpContext.Current.Server.MapPath(@"\Audit.Report\\Reports\report1.rdl");
            viewer.SizeToReportContent = true;
            viewer.AsyncRendering = true;
            ViewBag.ReportViewer = viewer;*/
            /* ReportViewer reportViewer = new ReportViewer();
             reportViewer.ProcessingMode = ProcessingMode.Local;
             reportViewer.SizeToReportContent = true;
             reportViewer.Width = Unit.Percentage(900);
             reportViewer.Height = Unit.Percentage(900);
             reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportTest.rdl";
             ViewBag.ReportViewer = reportViewer;
             XElement resUB = AppStatic.SystemController.Negtgel1("123");
             if (resUB != null && resUB.Elements("V_TESTVIEW") != null)
             {
                 List<Tailan> tailan = (from item in resUB.Elements("V_TESTVIEW") select new Tailan().SetXml(item)).ToList();
             }
             //reportViewer.LocalReport.DataSources.Add(new ReportDataSource(,"DataSet1", tailan));



             //con.Close();*/
            return View();
        }
        public ActionResult N1()
        {
            N1VM res = new N1VM();
            try
            {
                if (Globals.departments.Count > 0 || Globals.periods.Count > 0)
                {
                    res.departments = Globals.departments;
                    res.periods = Globals.periods;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    res.departments = Globals.departments;

                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    res.periods = Globals.periods;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return View(res);
        }
        public ActionResult Report1N2()
        {
            N1VM res = new N1VM();
            try
            {
                if (Globals.departments.Count > 0 || Globals.periods.Count > 0)
                {
                    res.departments = Globals.departments;
                    res.periods = Globals.periods;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    res.departments = Globals.departments;

                    XElement responsePeriod = SendLibraryRequest("StatPeriod");
                    Globals.periods = (from item in responsePeriod.Elements("Library") select new Period().FromXml(item)).ToList();
                    res.periods = Globals.periods;
                }
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            return View(res);
        }
        public PartialViewResult Viewer(string name,string title)
        {
            
            Reports res = new Reports();
            try
            {
                if (Globals.departments.Count > 0)
                {
                    res.departments = Globals.departments;
                }
                else
                {
                    XElement responseDepartment = SendLibraryRequest("Department");
                    Globals.departments = (from item in responseDepartment.Elements("Library") select new Department().FromXml(item)).ToList();
                    res.departments = Globals.departments;
                
                }
               
            }
            catch (Exception ex)
            {
                Globals.WriteErrorLog(ex);
            }
            //return View(res);
            ViewBag.periods = Globals.periods;
        
            
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            ReportViewer1.SizeToReportContent = true;
            //ReportViewer1.Width = Unit.Percentage(100);
            //ReportViewer1.Height = Unit.Percentage(100);
            ReportViewer1.AsyncRendering = true;
            ReportViewer1.ShowToolBar = false;
            ReportViewer1.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(ssrsURL);
            ReportViewer1.ServerReport.ReportPath = string.IsNullOrEmpty(name)? "/Audit.Report/Report1/Audit.Report/Report1" : name;
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("UserId", User.Identity.GetUserId(), true);
            ReportViewer1.ServerReport.SetParameters(parameters);
            ReportViewer1.ServerReport.Refresh();
            ViewBag.ReportViewer = ReportViewer1;
            res.title = title;
            return PartialView(res);
        }
        protected void ExportExcel_Click(object sender, EventArgs e)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string filename;

            byte[] bytes = ReportViewer1.ServerReport.Render(
               "Excel", null, out mimeType, out encoding,
                out extension,
               out streamids, out warnings);

            filename = string.Format("{0}.{1}", "ExportToExcel", "xls");
            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = mimeType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        public static XElement SendLibraryRequest(string lib)
        {
            XElement elem = new XElement("lib");
            elem.Add(new XElement("LibraryName", lib));

            return AppStatic.SystemController.Library(elem);
        }
        [HttpPost]
        public void albaSongoh(string value)
        {
            Console.WriteLine("value=============================================>" + value);
            // your code
            // return PartialView();//your response
        }

        public ActionResult negtgel1()
        {
            string ssrsURL = System.Configuration.ConfigurationManager.AppSettings["SSRSReportURL"].ToString();
            ReportViewer reportView = new ReportViewer();
            reportView.ProcessingMode = ProcessingMode.Remote;
            reportView.SizeToReportContent = true;
            reportView.AsyncRendering = true;
            reportView.ServerReport.ReportServerUrl = new Uri(ssrsURL);
            reportView.ServerReport.ReportPath = "Report1";
            ViewBag.ReportViewer = reportView;
            return View();
        }
    }
    [Serializable]
    public sealed class MyReportServerCredentials :
    IReportServerCredentials
    {
        public WindowsIdentity ImpersonationUser
        {
            get
            {             

                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                // Read the user information from the Web.config file.  
                // By reading the information on demand instead of 
                // storing it, the credentials will not be stored in 
                // session, reducing the vulnerable surface area to the
                // Web.config file, which can be secured with an ACL.

                // User name
                string userName =
                    ConfigurationManager.AppSettings
                        ["MyReportViewerUser"];

                if (string.IsNullOrEmpty(userName))
                    throw new Exception(
                        "Missing user name from web.config file");

                // Password
                string password =
                    ConfigurationManager.AppSettings
                        ["MyReportViewerPassword"];

                if (string.IsNullOrEmpty(password))
                    throw new Exception(
                        "Missing password from web.config file");

                // Domain
                string domain = "";

                return new NetworkCredential(userName, password, domain);
            }

        }

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
             
                authCookie = null;
            userName = password = authority = null;
                return false;
            
        }

    }
    }


