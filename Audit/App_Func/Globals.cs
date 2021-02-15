﻿using Audit.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Audit.App_Func
{
    public class Globals
    {
        public static List<Department> departments { get; set; } = new List<Department>();
        public static List<Status> statuses { get; set; } = new List<Status>();
        public static List<Violation> violations { get; set; } = new List<Violation>();
        //public static List<GenderVM> gender { get; set; } = new List<GenderVM>();
        //public static List<OriginVM> origin { get; set; } = new List<OriginVM>();
        //public static List<SystemUserLevelVM> systemuserlevel { get; set; } = new List<SystemUserLevelVM>();
        //public static List<PersonRelationVM> personrelation { get; set; } = new List<PersonRelationVM>();
        //public static List<PersonStatusVM> personstatus { get; set; } = new List<PersonStatusVM>();

        static string SecurityKey = null;
        static internal string _logFolder = null;
        static public string LogFolder
        {
            get
            {
                return _logFolder;
            }
            set { _logFolder = value; }
        }
        static internal string _currentFolder = null;
        static public string CurrentFolder
        {
            set { _currentFolder = value; }
            get
            {
                return _currentFolder;
            }
        }
        static object _loglocker = new object();
        public static void WriteTestLog(object log, string type = null)
        {
            string l;
            l = string.Format("{0}", JsonConvert.SerializeObject(log));
            if (log is Exception)
                l = string.Format("{0} - {1}", ((Exception)log).Source, ((Exception)log).Message);
            if (type == null)
                WriteLog(string.Format("TEST_{0}.log", DateTime.Now.ToString("yyyyMMdd")), string.Format("{0}: {1} [{2}]{3}", DateTime.Now.ToString("HH:mm:ss"), l, getCallerInfoString(2), Environment.NewLine));
            else
                WriteLog(string.Format("{1}_{0}.log", DateTime.Now.ToString("yyyyMMdd"), type.ToUpper()), string.Format("{0}: {1} [{2}]{3}", DateTime.Now.ToString("HH:mm:ss"), l, getCallerInfoString(2), Environment.NewLine));
        }
        public static void WriteErrorLog(object log)
        {
            string l = "";
            if (log is Exception)
                l = string.Format("{0} - {1}", ((Exception)log).Source, ((Exception)log).Message);
            WriteLog(string.Format("ERROR_{0}.log", DateTime.Now.ToString("yyyyMMdd")), string.Format("{0}: {1} [{2}]{3}", DateTime.Now.ToString("HH:mm:ss"), l, getCallerInfoString(2), Environment.NewLine));
        }
        static public StackFrame[] getCaller(int skipFrames = 1)
        {
            StringBuilder callerinfo = new StringBuilder();
            StackTrace st = new StackTrace(1 + skipFrames, true);

            return st.GetFrames();
        }
        static public string getCallerInfoString(int skipFrames = 1, int frameCount = 2)
        {
            StringBuilder callerinfo = new StringBuilder();
            StackFrame[] frames = getCaller(skipFrames);
            if (frameCount > frames.Length) frameCount = frames.Length - 1;
            for (int i = 0; i < frameCount; i++)
            {
                StackFrame frame = frames[i];
                if (i > 0)
                    callerinfo.Append(" <--- ");
                MethodBase mb = frame.GetMethod();
                if (mb.ReflectedType != null)
                {
                    callerinfo.Append(string.Format("{0}.{1}", mb.ReflectedType.FullName, mb.Name));
                }
                else
                {
                    callerinfo.Append(string.Format("ReflectedType = NULL.{0}", mb.Name));
                }
                callerinfo.Append(string.Format("(МӨР: {0})", frame.GetFileLineNumber()));
            }

            return callerinfo.ToString();
        }
        private static void WriteLog(string filename, string log)
        {
            lock (_loglocker)
            {
                try
                {
                    #region [ Файлын нэр нь фолдер агуулаагүй бол дуудагдсан фолдерыг оруулах ]
                    if (string.IsNullOrEmpty(filename))
                        filename = "log.log";
                    if (!filename.Contains("\\"))
                    {
                        if (Directory.Exists(LogFolder))
                            filename = Path.Combine(LogFolder, filename);
                        else
                            filename = Path.Combine(CurrentFolder, filename);
                    }
                    #endregion
                    #region [ Файл руу бичих ]
                    using (FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.Write))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(string.Format("{0}{1}", log, Environment.NewLine));
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                        fs.Close();
                    }
                    #endregion
                }
                catch { }
            }
        }
        public static void Init()
        {
            Globals.CurrentFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            Globals.LogFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            Globals.SecurityKey = ConfigurationManager.AppSettings["SecurityKey"];
        }
        public static string Encrypt(string value)
        {
            string EncryptionKey = Globals.SecurityKey;
            byte[] clearBytes = Encoding.Unicode.GetBytes(value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    value = Convert.ToBase64String(ms.ToArray());
                }
            }
            return value;
        }

        public static string Decrypt(string value)
        {
            string EncryptionKey = Globals.SecurityKey;
            value = value.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    value = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return value;
        }
    }
}