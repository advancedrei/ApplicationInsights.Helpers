using System;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;

namespace ApplicationInsights.Helpers.Mvc5
{

    /// <summary>
    /// Application Insights Exception Logger for MVC 5.3.
    /// </summary>
    /// <remarks>
    /// Code based on Microsoft recommendations from http://blogs.msdn.com/b/visualstudioalm/archive/2014/12/12/application-insights-exception-telemetry.aspx.
    /// It has been updated for DI support.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class InsightsHandleErrorAttribute : HandleErrorAttribute
    {

        #region Properties

        /// <summary>
        /// The instance of the <see cref="TelemetryClient"/> to use for the logger.
        /// </summary>
        public TelemetryClient Telemetry { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// The default constructor for the <see cref="InsightsHandleErrorAttribute"/>.
        /// </summary>
        /// <param name="client">The <see cref="TelemetryClient"/> instance to use for the logger. Either injected by a DI framework or inctanciated manually.</param>
        /// <example>
        /// //
        /// config.Services.Add(typeof(IExceptionLogger), new InsightsExceptionLogger(new TelemetryClient())); 
        /// </example>
        public InsightsHandleErrorAttribute(TelemetryClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client", "The TelemetryClient was not specified. Either register an instance with your DI container" +
                                                          " or pass an instance in manually.");
            }
            Telemetry = client;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.HttpContext != null && filterContext.Exception != null)
            {
                //If customError is Off, then AI HTTPModule will report the exception
                if (filterContext.HttpContext.IsCustomErrorEnabled)
                {    
                    Telemetry.TrackException(filterContext.Exception);
                }
            }
            base.OnException(filterContext);
        }

        #endregion

    }
}