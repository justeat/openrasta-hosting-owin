using System.ServiceProcess;

namespace OpenRasta.Owin.Service
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main()
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