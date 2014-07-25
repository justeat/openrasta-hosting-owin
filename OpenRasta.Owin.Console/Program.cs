using Microsoft.Owin.Hosting;
using OpenRastaAPIProject;

namespace OpenRasta.Owin.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:8080/";

            WebApp.Start<Startup>(uri);

            System.Console.ReadLine();
        }
    }
}
