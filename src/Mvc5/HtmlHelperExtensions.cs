using System.IO;
using System.Web.Configuration;
using System.Web.UI;

namespace System.Web.Mvc
{

    /// <summary>
    /// 
    /// </summary>
    public static class HtmlHelperExtensions
    {
        private const string Script = "var appInsights=window.appInsights||function(config){function s(config){t[config]=function(){var i=arguments;t.queue.push(function(){t[config].apply(t,i)})}}var t={config:config},r=document,f=window,e=\"script\",o=r.createElement(e),i,u;for(o.src=config.url||\"//az416426.vo.msecnd.net/scripts/a/ai.0.js\",r.getElementsByTagName(e)[0].parentNode.appendChild(o),t.cookie=r.cookie,t.queue=[],i=[\"Event\",\"Exception\",\"Metric\",\"PageView\",\"Trace\"];i.length;)s(\"track\"+i.pop());return config.disableExceptionTracking||(i=\"onerror\",s(\"_\"+i),u=f[i],f[i]=function(config,r,f,e,o){var s=u&&u(config,r,f,e,o);return s!==!0&&t[\"_\"+i](config,r,f,e,o),s}),t}({instrumentationKey:\"{INSERTKEYHERE}\"});window.appInsights=appInsights;appInsights.trackPageView();";

        /// <summary>
        /// Adds the Application Insights header to your MVC page. 
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper instance to extend.</param>
        /// <returns></returns>
        public static MvcHtmlString AddApplicationInsightsHeader(this HtmlHelper htmlHelper)
        {
            using (var output = new StringWriter())
            {
                using (var writer = new HtmlTextWriter(output))
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Script);
                    writer.Write(Script.Replace("{INSERTKEYHERE}", WebConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"]));
                    writer.RenderEndTag();
                }
                return MvcHtmlString.Create(output.ToString());
            }
        }

    }
}
