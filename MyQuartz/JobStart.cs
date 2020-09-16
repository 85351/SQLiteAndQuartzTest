using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuartz
{
    public class Job
    {
        private readonly IScheduler _scheduler;
        public Job()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            this._scheduler = factory.GetScheduler();
        }
        public void Start()
        {
            if (this._scheduler != null && !this._scheduler.IsStarted)
            {
                this._scheduler.Start();
            }
        }
        public void StopAll()
        {
            if (this._scheduler != null && this._scheduler.IsStarted)
            {
                this._scheduler.PauseAll();
            }
        }
    }
}
