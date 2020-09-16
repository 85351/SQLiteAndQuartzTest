using MyLogger;
using MySQLite;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuartz.Job1
{
    public class DeleteJob : IJob
    {
        static NewLogger Logger = new NewLogger(typeof(DeleteJob).FullName);
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
                    if (new Random().Next(100) > 30)
                        continue;

                    Console.WriteLine($"delete -job:{jobName}- info {m.Path}");
                    Logger.Info($"delete -job:{jobName}- info {m.Path}");
                    RecordTempQueryDao.Delete(m.Path);
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
