﻿using MySQLiteWithEF;
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
            var job = new MyQuartz.Job();
            job.Start();
        }
    }
}
