using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Globalization;
using System.Reflection;

namespace ApplicationInsights.Helpers.Console
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Re-worked from https://github.com/bc3tech/DesktopApplicationInsights
    /// </remarks>
    public class ConsoleContextInitializer : IContextInitializer
    {

        #region Private Members

        private readonly Assembly _sourceAssembly;

        #endregion

        public ConsoleContextInitializer(Assembly sourceAssembly)
        {
            _sourceAssembly = sourceAssembly;
        }

        public void Initialize(TelemetryContext context)
        {
            context.Component.Version = _sourceAssembly.GetName().Version.ToString();

            context.Device.Language = CultureInfo.CurrentUICulture.IetfLanguageTag;
            context.Device.OperatingSystem = Environment.OSVersion.ToString();

            context.Properties.Add("64BitOS", Environment.Is64BitOperatingSystem.ToString());
            context.Properties.Add("64BitProcess", Environment.Is64BitProcess.ToString());
            context.Properties.Add("MachineName", Environment.MachineName);
            context.Properties.Add("ProcessorCount", Environment.ProcessorCount.ToString());
            context.Properties.Add("ClrVersion", Environment.Version.ToString());

            context.Session.Id = DateTime.UtcNow.ToString();
            context.Session.IsFirst = true;

            //context.User.AccountId = Environment.UserDomainName;
            //context.User.Id = Environment.UserName;
        }

    }

}