﻿using MyLogger;
using MySQLite;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuartz.Job1
{
    public class AddJob : IJob
    {
        static NewLogger Logger = new NewLogger(typeof(AddJob).FullName);
        public void Execute(IJobExecutionContext context)
        {
            var jobName = context.JobDetail.Key.Name;
            
            var path = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffffff}-{jobName}-{Guid.NewGuid().ToString().Substring(0, 8)}";
            try
            {
                RecordTempQueryDao.Add(path);
                Console.WriteLine($"add info {path}");
                Logger.Info(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"add error {path}：{ex.Message}");
                Logger.Error($"{path},err:{ex}");
                
            }
        }
    }
}
