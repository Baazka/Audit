using System;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;


namespace Audit.Controllers
{
    
    public class ReportController : Controller
    {
        private string _UserName = "BatOchir.n@audit.mn";
        private string _PassWord = "Audit8085*";
        private string _DomainName = "http://10.10.10.43:8787/ReportServer";

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
        public PartialViewResult Viewer(string name)
        {
            string ssrsURL = System.Configuration.ConfigurationManager.AppSettings["SSRSReportURL"].ToString();
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            ReportViewer1.SizeToReportContent = true;
            ReportViewer1.Width = Unit.Percentage(100);
            ReportViewer1.Height = Unit.Percentage(100);
            ReportViewer1.AsyncRendering = true;
            ReportViewer1.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(ssrsURL);
            ReportViewer1.ServerReport.ReportPath = name;
            ReportViewer1.ServerReport.Refresh();
            ViewBag.ReportViewer = ReportViewer1;
            return PartialView(ViewBag);
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
                // Use the default Windows user.  Credentials will be
                // provided by the NetworkCredentials property.
               

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


