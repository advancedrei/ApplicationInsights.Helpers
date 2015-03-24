using System;
using System.Web.Http.ExceptionHandling;
using Microsoft.ApplicationInsights;

namespace ApplicationInsights.Helpers.WebApi2
{
    public class InsightsExceptionLogger : ExceptionLogger
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public TelemetryClient Telemetry { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public InsightsExceptionLogger(TelemetryClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client", "The TelemetryClient was not specified. Either register an instance with your DI container" +
                                                          " or pass an instance in manually.");
            }
            Telemetry = client;
        }

        #endregion

        #region ExceptionLogger Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Log(ExceptionLoggerContext context)
        {
            if (context != null && context.Exception != null)
            {
                Telemetry.TrackException(context.Exception);
            }
            base.Log(context);
        }

        #endregion

    }
}