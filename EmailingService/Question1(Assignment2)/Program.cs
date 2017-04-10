using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Question1_Assignment2_
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            Service1 myservice = new Service1();
            myservice.onDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
            ServiceBase[] ServiceToRun;
            ServiceToRun = new ServiceBase[]
            {
            new Service1()
            };
            ServiceBase.Run(ServiceToRun);
#endif 
   

        }
    }
}
