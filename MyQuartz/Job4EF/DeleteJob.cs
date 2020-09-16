using MyLogger;
using MySQLiteWithEF;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuartz.Job4EF
{
    public class DeleteJob : IJob
    {
        static NewLogger Logger = new NewLogger(typeof(DeleteJob).FullName);
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
                    if (new Random().Next(100) > 30)
                        continue;

                    Console.WriteLine($"delete -job:{jobName}- info {m.path}");
                    Logger.Info($"delete -job:{jobName}- info {m.path}");
                    RecordQueryTempService.Delete(m.path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"delete -job:{jobName}- error:{ex.Message}");
                Logger.Error($"delete -job:{jobName}- error err:{ex}");
            }
        }
    }
}
