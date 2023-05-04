using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAFTestsMSTest.Utils
{
    public class NLogger
    {
        private static NLog.Logger? nlogger;

        public NLogger()
        {
            nlogger = NLog.LogManager.GetCurrentClassLogger();
        }
    }
}
