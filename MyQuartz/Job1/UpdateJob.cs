using MyLogger;
using MySQLite;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyQuartz.Job1
{
    public class UpdateJob : IJob
    {
        static NewLogger Logger = new NewLogger(typeof(UpdateJob).FullName);
        public void Execute(IJobExecutionContext context)
        {
            var jobName = context.JobDetail.Key.Name;
            try
            {
                var models = RecordTempQueryDao.GetAll();
                if (models == null)
                    return;
                foreach (var m in models)
                {
                    if (new Random().Next(100) > 50)
                        continue;

                    Console.WriteLine($"update info -job:{jobName}- {m.Path}");
                    Logger.Info($"update -job:{jobName}- {m.Path}");
                    RecordTempQueryDao.UpdateReasonAndCount(m.Path, Guid.NewGuid().ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"update -job:{jobName}- error:{ex.Message}");
                Logger.Error($"update -job:{jobName}- error :{ex}");
            }
        }
    }
}
