using log4net;
using log4net.Appender;
using System;
using System.Collections.Generic;
//using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger
{
    public enum LogType
    {
        Info,
        Debug,
        Warn,
        Error
    }
    public class NewLogger
    {
        private readonly ILog _logger;
        public NewLogger(string name, bool _simplePatternLayout = false)
        {
            _logger = Configuration.GetLogger(name, _simplePatternLayout);
        }

        public void Info(string context)
        {
            //Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffffff} {context}");
            _logger.Info(context);
        }

        public void Debug(string context)
        {
            //Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffffff} {context}");
            _logger.Debug(context);
        }
        public void Warn(string context)
        {
            //Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffffff} {context}");
            _logger.Warn(context);
        }

        public void Error(string context)
        {
            //Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffffff} {context}");
            _logger.Error(context);
        }
    }
}
