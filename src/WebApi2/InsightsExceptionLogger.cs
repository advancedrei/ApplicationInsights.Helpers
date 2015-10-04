using System;
using System.Web.Http.ExceptionHandling;
using Microsoft.ApplicationInsights;

namespace ApplicationInsights.Helpers.WebApi2
{

    /// <summary>
    /// Application Insights Exception Logger for WebAPI 2.2.
    /// </summary>
    /// <remarks>
    /// Code based on Microsoft recommendations from http://blogs.msdn.com/b/visualstudioalm/archive/2014/12/12/application-insights-exception-telemetry.aspx.
    /// It has been updated for DI support.
    /// </remarks>
    public class InsightsExceptionLogger : ExceptionLogger
    {

        #region Properties

        /// <summary>
        /// The instance of the <see cref="TelemetryClient"/> to use for the logger.
        /// </summary>
        public TelemetryClient Telemetry { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor for the <see cref="InsightsExceptionLogger"/>.
        /// </summary>
        /// <param name="client">The <see cref="TelemetryClient"/> instance to use for the logger. Either injected by a DI framework or instanciated manually.</param>
        /// <example>
        /// //For non-DI scenarios:
        /// config.Services.Add(typeof(IExceptionLogger), new InsightsExceptionLogger(new TelemetryClient()));
        /// //For Autofac:
        /// builder.Register(c => new InsightsHandleErrorAttribute(c.Resolve&lt;TelemetryClient&gt;())).AsExceptionFilterFor&lt;Controller&gt;().InstancePerRequest();
        /// builder.RegisterFilterProvider();
        /// </example>
        public InsightsExceptionLogger(TelemetryClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client), "The TelemetryClient was not specified. Either register an instance with your DI container" +
                                                          " or pass an instance in manually.");
            }
            Telemetry = client;
        }

        #endregion

        #region ExceptionLogger Methods

        /// <summary>
        /// Logs the exception synchronously.
        /// </summary>
        /// <param name="context">The exception logger context.</param>
        public override void Log(ExceptionLoggerContext context)
        {
            if (context != null && context.Exception != null && Telemetry != null)
            {
                Telemetry.TrackException(context.Exception);
            }
            base.Log(context);
        }

        #endregion

    }
}