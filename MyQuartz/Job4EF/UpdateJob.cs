using MyLogger;
using MySQLiteWithEF;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyQuartz.Job4EF
{
    public class UpdateJob : IJob
    {
        static NewLogger Logger = new NewLogger(typeof(UpdateJob).FullName);
        public void Execute(IJobExecutionContext context)
        {
            var jobName = context.JobDetail.Key.Name;
            try
            {
                var models = RecordQueryTempService.GetAll();
                if (models == null)
                    return;
                foreach (var m in models)
                {
                    if (new Random().Next(100) > 50)
                        continue;

                    Console.WriteLine($"update info -job:{jobName}- {m.path}");
                    Logger.Info($"update -job:{jobName}- {m.path}");
                    RecordQueryTempService.UpdateReasonAndCount(m.path, Guid.NewGuid().ToString());
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
