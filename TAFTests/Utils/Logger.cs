﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAFTests.Utils
{
    public class Logger
    {
        private static NLog.Logger? nlogger;

        public Logger()
        {
            nlogger = NLog.LogManager.GetCurrentClassLogger();
        }
    }
}
