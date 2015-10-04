using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System.Diagnostics;
using System.Web.Configuration;

namespace ApplicationInsights.Helpers
{

    /// <summary>
    /// Sets the TelemetryClient Context information from AppSettings string from web.config, app.config, or Azure's App Properties.
    /// </summary>
    /// <remarks>
    /// See http://azure.microsoft.com/en-us/documentation/articles/app-insights-search-diagnostic-logs/#exceptions
    /// </remarks>
    public class KeyAndVersionContextInitializer : IContextInitializer
    {

        #region Constructor

        /// <summary>
        /// The default constructor for the KeyAndVersionContextInitializer.
        /// </summary>
        public KeyAndVersionContextInitializer()
        {
        }

        #endregion

        #region IContextInitializer Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(TelemetryContext context)
        {
            var key = WebConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"] ?? "";
            var version = WebConfigurationManager.AppSettings["APPINSIGHTS_APPVERSION"] ?? "";

            if (!string.IsNullOrWhiteSpace(key))
            {
                Trace.WriteLine($"KeyAndVersionContextInitializer: Key Found! '{key}'");
            }
            if (!string.IsNullOrWhiteSpace(version))
            {
                Trace.WriteLine($"KeyAndVersionContextInitializer: Version Found! '{version}'");
            }

            context.InstrumentationKey = key.Trim();
            context.Component.Version = version.Trim();
        }

        #endregion

    }
}