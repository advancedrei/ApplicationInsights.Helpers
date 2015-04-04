using System.Web.Configuration;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace ApplicationInsights.Helpers
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// See http://azure.microsoft.com/en-us/documentation/articles/app-insights-search-diagnostic-logs/#exceptions
    /// </remarks>
    public class InsightsTelemetryInitializer : IContextInitializer
    {

        #region Private Members

        private readonly string _version;

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor for the InsightsTelemetryInitializer.
        /// </summary>
        /// <param name="version">The current version of the app to pass to AI.</param>
        public InsightsTelemetryInitializer(string version = "")
        {
            _version = version;
        }

        #endregion

        #region IContextInitializer Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(TelemetryContext context)
        {
            context.InstrumentationKey = WebConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"];
            if (!string.IsNullOrWhiteSpace(_version.Trim()))
            {
                context.Component.Version = _version;
            }
        }

        #endregion

    }
}