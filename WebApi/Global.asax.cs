using NLog;
using System;
using System.Web;
using System.Web.Http;

namespace WebApi
{
    public class WebApiApplication : HttpApplication
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        protected void Application_Start(object sender, EventArgs e)
        {
            Logger.Info("Application started");

            // Register Web API routes
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            // Log application end
            Logger.Info("Application ended");

            // Shutdown NLog
            LogManager.Shutdown();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            Logger.Error(exception, "Unhandled exception occurred");
        }
    }
}
