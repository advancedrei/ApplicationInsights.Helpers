using System;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;

namespace ApplicationInsights.Helpers.Mvc5
{

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class InsightsHandleErrorAttribute : HandleErrorAttribute
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