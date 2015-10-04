using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ApplicationInsights.Helpers.WebJobs
{

    /// <summary>
    /// 
    /// </summary>
    public class InsightsTraceWriter : TraceWriter
    {

        #region Private Members

        /// <summary>
        /// The TelemetryClient instance to be used to report Trace messages.
        /// </summary>
        private TelemetryClient Telemetry { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance of the InsightsTraceWriter for the given <see cref="TraceLevel"/>, with a new TelemetryClient instance.
        /// </summary>
        /// <param name="level"></param>
        public InsightsTraceWriter(TraceLevel level) : base(level)
        {
            Telemetry = new TelemetryClient();
        }

        /// <summary>
        /// Constructor suitable for an IoC container.
        /// </summary>
        /// <param name="telemetry"></param>
        /// <param name="level"></param>
        public InsightsTraceWriter(TelemetryClient telemetry, TraceLevel level) : base(level)
        {
            Telemetry = telemetry;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="telemetry">The <see cref="TelemetryClient"/> to use for this InsightsTraceWriter instance.</param>
        /// <remarks>Will only change the value if not null.</remarks>
        public void ChangeTelemetryClient(TelemetryClient telemetry)
        {
            if (telemetry != null)
            {
                Telemetry = telemetry;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public override void Trace(TraceLevel level, string source, string message, Exception ex)
        {

            if (!string.IsNullOrWhiteSpace(source))
            {
                message = "[" + source + "] " + message;
            }

            var properties = new Dictionary<string, string>
            {
                { "JobName", Environment.GetEnvironmentVariable("WEBJOBS_NAME") },
                { "JobId", Environment.GetEnvironmentVariable("WEBJOBS_RUN_ID") }
            };

            switch (level)
            {
                case TraceLevel.Off:
                    break;
                case TraceLevel.Error:
                    Telemetry.TrackException(ex, properties);
                    break;
                case TraceLevel.Warning:
                    Telemetry.TrackTrace(message, SeverityLevel.Warning, properties);
                    break;
                case TraceLevel.Info:
                    Telemetry.TrackTrace(message, SeverityLevel.Information, properties);
                    break;
                case TraceLevel.Verbose:
                    Telemetry.TrackTrace(message, SeverityLevel.Verbose, properties);
                    break;
            }
        }

        #endregion

    }

}