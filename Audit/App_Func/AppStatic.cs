using Audit.Controllers.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Audit
{
    public class AppStatic
    {
        public static int SessionTimeOut
        {
            get { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["SessionTimeOut"]); }
        }
        public static List<SelectListItem> AddObjItem(List<SelectListItem> obj)
        {
            obj.Add(new SelectListItem { Text = "А", Value = "А" });
            obj.Add(new SelectListItem { Text = "Б", Value = "Б" });
            obj.Add(new SelectListItem { Text = "В", Value = "В" });
            obj.Add(new SelectListItem { Text = "Г", Value = "Г" });
            obj.Add(new SelectListItem { Text = "Д", Value = "Д" });
            obj.Add(new SelectListItem { Text = "Е", Value = "Е" });
            obj.Add(new SelectListItem { Text = "Ё", Value = "Ё" });
            obj.Add(new SelectListItem { Text = "Ж", Value = "Ж" });
            obj.Add(new SelectListItem { Text = "З", Value = "З" });
            obj.Add(new SelectListItem { Text = "И", Value = "И" });

            obj.Add(new SelectListItem { Text = "Й", Value = "Й" });
            obj.Add(new SelectListItem { Text = "К", Value = "К" });
            obj.Add(new SelectListItem { Text = "Л", Value = "Л" });
            obj.Add(new SelectListItem { Text = "М", Value = "М" });
            obj.Add(new SelectListItem { Text = "Н", Value = "Н" });
            obj.Add(new SelectListItem { Text = "О", Value = "О" });
            obj.Add(new SelectListItem { Text = "Ө", Value = "Ө" });
            obj.Add(new SelectListItem { Text = "П", Value = "П" });
            obj.Add(new SelectListItem { Text = "Р", Value = "Р" });
            obj.Add(new SelectListItem { Text = "С", Value = "С" });

            obj.Add(new SelectListItem { Text = "Т", Value = "Т" });
            obj.Add(new SelectListItem { Text = "У", Value = "У" });
            obj.Add(new SelectListItem { Text = "Ү", Value = "Ү" });
            obj.Add(new SelectListItem { Text = "Ф", Value = "Ф" });
            obj.Add(new SelectListItem { Text = "Х", Value = "Х" });
            obj.Add(new SelectListItem { Text = "Ц", Value = "Ц" });
            obj.Add(new SelectListItem { Text = "Ч", Value = "Ч" });
            obj.Add(new SelectListItem { Text = "Ш", Value = "Ш" });
            obj.Add(new SelectListItem { Text = "Щ", Value = "Щ" });
            obj.Add(new SelectListItem { Text = "Ъ", Value = "Ъ" });

            obj.Add(new SelectListItem { Text = "Ы", Value = "Ы" });
            obj.Add(new SelectListItem { Text = "Ь", Value = "Ь" });
            obj.Add(new SelectListItem { Text = "Э", Value = "Э" });
            obj.Add(new SelectListItem { Text = "Ю", Value = "Ю" });
            obj.Add(new SelectListItem { Text = "Я", Value = "Я" });
            return obj;
        }
        public static SystemController SystemController
        {
            get
            {
                SystemController sysController = null;
                if (HttpContext.Current.Items["_SystemController"] == null)
                {
                    sysController = new SystemController();
                    HttpContext.Current.Items["_SystemController"] = sysController;
                }
                else
                    sysController = HttpContext.Current.Items["_SystemController"] as SystemController;

                return sysController;
            }
        }
        public static void SetError(List<String> errors, string message, ModelStateDictionary modelState)
        {
            if (errors != null && errors.Count > 0)
                foreach (string err in errors)
                {
                    var msg = err.Split(':');
                    if (msg.Length == 2)
                        if (msg[0].Equals("Error") || msg[0].Equals("E001"))
                            modelState.AddModelError(string.Empty, msg[1]);
                        else
                            modelState.AddModelError(msg[0], msg[1]);
                    else
                        modelState.AddModelError(string.Empty, err);
                }
            else
                if (!string.IsNullOrEmpty(message))
                modelState.AddModelError(string.Empty, message);
            else
                modelState.AddModelError(string.Empty, "Алдаа гарлаа. Та түр хүлээгээд ахин хандана уу.");
        }
        public static string ExceptionMessage(Exception ex)
        {
            string responseMessage = "";
            switch (ex.GetType().ToString())
            {
                default:
                    responseMessage = "Системд алдаа гарлаа. Та түр хүлээгээд дахин оролдоно уу.";
                    break;
            }
            return responseMessage;
        }
    }
}