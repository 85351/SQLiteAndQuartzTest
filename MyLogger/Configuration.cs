using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger
{
    public class Configuration
    {
        public static readonly string ConversionPattern = "%newline%date %-5level%newline%message%newline";

        public static readonly string SimpleConversionPattern = "%newline%message";

        private static bool simplePatternLayout = false;

        private static string _logFilePath;

        public static ILog GetLogger(string name, bool _simplePatternLayout)
        {
            simplePatternLayout = _simplePatternLayout;
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", name) + "/";
            ILoggerRepository loggerRepository = LogManager.GetAllRepositories().FirstOrDefault((ILoggerRepository r) => r.Name == name);
            if (loggerRepository == null)
            {
                CreateNewRepository(name);
            }
            return LogManager.GetLogger(name, name);
        }

        private static void CreateNewRepository(string name)
        {
            ILoggerRepository loggerRepository = LogManager.CreateRepository(name);
            Hierarchy hierarchy = (Hierarchy)loggerRepository;
            hierarchy.Name = name;
            RollingFileAppender rollingFileAppender = CreateRollingFileAppender(name);
            rollingFileAppender.DatePattern = "yyyyMMdd\".txt\"";
            rollingFileAppender.MaxSizeRollBackups = 30;
            rollingFileAppender.AddFilter(new LevelRangeFilter
            {
                LevelMax = Level.Fatal,
                LevelMin = Level.Info
            });
            rollingFileAppender.ActivateOptions();
            hierarchy.Root.AddAppender(rollingFileAppender);
            RollingFileAppender rollingFileAppender2 = CreateRollingFileAppender(name + "_Error");
            rollingFileAppender2.DatePattern = "yyyyMMdd\".error.txt\"";
            rollingFileAppender2.MaxSizeRollBackups = 60;
            rollingFileAppender2.AddFilter(new LevelRangeFilter
            {
                LevelMax = Level.Fatal,
                LevelMin = Level.Warn
            });
            rollingFileAppender2.ActivateOptions();
            hierarchy.Root.AddAppender(rollingFileAppender2);
            RollingFileAppender rollingFileAppender3 = CreateRollingFileAppender(name + "_Debug");
            rollingFileAppender3.DatePattern = "yyyyMMdd\".debug.txt\"";
            rollingFileAppender3.MaxSizeRollBackups = 60;
            rollingFileAppender3.AddFilter(new LevelRangeFilter
            {
                LevelMax = Level.Debug,
                LevelMin = Level.Debug
            });
            rollingFileAppender3.ActivateOptions();
            hierarchy.Root.AddAppender(rollingFileAppender3);
            hierarchy.Root.Level = Level.Debug;
            hierarchy.Configured = true;
        }

        private static RollingFileAppender CreateRollingFileAppender(string name)
        {
            return new RollingFileAppender
            {
                Name = name,
                Layout = GetPatternLayout(),
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Date,
                StaticLogFileName = false,
                LockingModel = new FileAppender.MinimalLock(),
                File = _logFilePath
            };
        }

        private static PatternLayout GetPatternLayout()
        {
            PatternLayout patternLayout = new PatternLayout
            {
                ConversionPattern = (simplePatternLayout ? SimpleConversionPattern : ConversionPattern)
            };
            patternLayout.ActivateOptions();
            return patternLayout;
        }
    }
}
