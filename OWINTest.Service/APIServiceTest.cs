using System;
using System.ServiceProcess;
using Microsoft.Owin.Hosting;
using OpenRastaAPIProject;

namespace OpenRasta.Owin.Service
{
    public partial class APIServiceTest : ServiceBase
    {
        private IDisposable _server;
        public string baseAddress = "http://localhost:9000/";

        public APIServiceTest()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _server = WebApp.Start<Startup>(url: baseAddress);
        }

        protected override void OnStop()
        {
            if (_server != null)
            {
                _server.Dispose();
            }
            base.OnStop();
        }
    }
}