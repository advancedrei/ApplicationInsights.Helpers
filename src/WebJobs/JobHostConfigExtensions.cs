using ApplicationInsights.Helpers.Console;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs;
using System.Diagnostics;
using System.Reflection;

namespace ApplicationInsights.Helpers.WebJobs
{

    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationInsightsExtensions
    {

        /// <summary>
        /// Configures Application Insights to be used by a WebJob app.
        /// </summary>
        /// <param name="config">The JobHostConfiguration instance to extend.</param>
        /// <param name="instrumentationkey">The ApplicationInsights InstrumentationKey to initialize all <see cref="TelemetryClient">TelemetryClients</see> with.</param>
        /// <param name="addTraceWriter">Signals whether or not to add the InsightsTraceWriter, which logs Trace messages to Insights in addition to the standard WebJobs logs.</param>
        /// <param name="traceLevel"></param>
        public static void UseApplicationInsights(this JobHostConfiguration config, string instrumentationkey = null, bool addTraceWriter = false, TraceLevel traceLevel = TraceLevel.Info)
        {
            config.Tracing.ConsoleLevel = traceLevel;

            if (addTraceWriter)
            {
                config.Tracing.Trace = new InsightsTraceWriter(traceLevel);
            }

            TelemetryConfiguration.Active.ContextInitializers.Add(new ConsoleContextInitializer(Assembly.GetCallingAssembly()));
            if (!string.IsNullOrWhiteSpace(instrumentationkey))
            {
                TelemetryConfiguration.Active.InstrumentationKey = instrumentationkey;
            }

        }

    }

}