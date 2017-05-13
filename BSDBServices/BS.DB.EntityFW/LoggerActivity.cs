using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BS.DB.EntityFW
{
  public  class LoggerActivity
    {
        public Log ErrorSetup(string app, string msg, string user, string servername, string url , string exceptionmsg)
        {
            var log = new Log();
            log.Application = app;
            log.Message = msg;
            log.UserName = user;
            log.ServerName = servername;
            log.Url = url;
            log.Exception = exceptionmsg;

            return log;
        }

        public void SaveLog(Log log)
        {
            try
            {
                BSDBEntities ef = new EntityFW.BSDBEntities();
                ef.Logs.Add(log);
                ef.SaveChanges();
            }
            catch 
            {
              var logpath=  Properties.Settings.Default.LogFilePath;
                SaveFile(log, logpath);
            }
        }

        public void SaveFile(Log log, string filepath)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Log));
            var subReq = new Log();
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, subReq);
                    xml = sww.ToString(); // Your XML
                }
            }

            using (var sw = new StreamWriter(filepath, true))
            {
                sw.WriteLine(Environment.NewLine);
                sw.WriteLine("----------------------------------- " + DateTime.Now.ToString()+" ---------------------------------------------");
                sw.WriteLine(Environment.NewLine);

                sw.WriteLine(xml);

                sw.WriteLine(Environment.NewLine);
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine(Environment.NewLine);
                sw.Close();

            }
        }
    }
}
