using System.ServiceProcess;
using OWINTest.Service;

namespace OpenRasta.Owin.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new APIServiceTest() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
