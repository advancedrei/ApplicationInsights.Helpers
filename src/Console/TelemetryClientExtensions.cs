using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Threading;

namespace ApplicationInsights.Helpers.Console
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Re-worked from https://github.com/bc3tech/DesktopApplicationInsights
    /// </remarks>
    public static class TelemetryClientExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public static void HandleAppDomainEvents(this TelemetryClient client)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                client.TrackException(new ExceptionTelemetry((Exception)e.ExceptionObject)
                {
                    HandledAt = ExceptionHandledAt.Unhandled,
                    SeverityLevel = SeverityLevel.Critical,
                });

                //throw e.Exception;
            };

            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                client.Flush();
                Thread.Sleep(1500);
            };

        }

    }

}