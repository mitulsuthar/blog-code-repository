using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingDemo.Services
{
    public interface IMyLogger
    {       
        void Log(string message, Exception ex);
    }
    public class MyLogger : IMyLogger
    {
        public void Log(string message, Exception ex)
        {
            //Log to database or use application insights.
        }
    }
}
