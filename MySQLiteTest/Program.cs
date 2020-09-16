using MyLogger;
using MySQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteHelper.InitDT();
            var job = new MyQuartz.Job();
            job.Start();
        }
    }
}
