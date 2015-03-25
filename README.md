# ApplicationInsights.Helpers
Helpers for MVC 5 and WebAPI 2, to make your AI life easier.

Application Insights from Microsoft is a great little system. The fact that it's free doesn't hurt either. But it takes quite a bit of setup to get going, and when it comes to things like Exception handling, you don't find out that it doesn't cover all exceptions unless you stumble upon a blog post with the changes you need to make to your code.

I wanted to make it easier to use Application Insights, and this does just that. Why Microsoft's didn't do these things themselves is the real question, but they are free to add this code to the SDK (which AFAIK is not open source).

# Goals: 
- Add the requisite JavaScript to the MVC _layout.cshtml without having to worry about the actual script.
- Enable ApplicationInsights to log to different web "apps" based on Environment, without dealing with config transforms.
- Make it work with Microsoft's recommendation for a single instance of the TelemetryClient.
- Implement the proper handlers to grab every exception.
- Make it work with Autofac (and other DI containers).

# Installation:

All versions:

1) Add the following line to your appSettings in web.config, or to your Azure Web App appSettings:
```csharp
<add key="APPINSIGHTS_INSTRUMENTATIONKEY" value="**InsertYourInstrumentationGuidHere**"/>
```

MVC:

2) Add the following code to DependencyConfig.cs:
```csharp
builder.RegisterType<TelemetryClient>().SingleInstance();
builder.Register(c => new InsightsHandleErrorAttribute(c.Resolve<TelemetryClient>())).AsExceptionFilterFor<Controller>().InstancePerRequest();
builder.RegisterFilterProvider();
```
3) Add the following code to the first line of either Register() in Startup.cs, or Application_Start() in Global.asax.cs:
```csharp
TelemetryConfiguration.Active.ContextInitializers.Add(new InsightsTelemetryInitializer());
```
4) Add the following to the first line of the <head> tag in Shared/_Layout.cshtml:
```csharp
@Html.AddApplicationInsightsHeader()
```

WebApi:

2) Add the following code to the first line of Register() in WebApiConfig.cs:
```csharp
TelemetryConfiguration.Active.ContextInitializers.Add(new InsightsTelemetryInitializer());
config.Services.Add(typeof(IExceptionLogger), new InsightsExceptionLogger(new TelemetryClient())); 
```
NOTE: Not sure how to register the dependency in Autofac and then resolve the instance outside of constructor injection, so if you have a better way to do this and make the TelemetryClient instance available to the whole app, I'm all ears.

That's it! Now you can have slot-specific InstrumentationKeys, all of your exceptions are captured, TelemetryClient instances are used, and life is grand.
