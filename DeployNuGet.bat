@echo off
echo Would you like to push the packages to NuGet when finished?
set /p choice="Enter y/n: "

del *.nupkg
@echo on
".nuget/nuget.exe" pack ApplicationInsights.Helpers.WebCore.nuspec -symbols
".nuget/nuget.exe" pack ApplicationInsights.Helpers.Mvc5.nuspec -symbols
".nuget/nuget.exe" pack ApplicationInsights.Helpers.WebApi2.nuspec -symbols
".nuget/nuget.exe" pack ApplicationInsights.Helpers.Console.nuspec -symbols
".nuget/nuget.exe" pack ApplicationInsights.Helpers.WebJobs.nuspec -symbols
if /i %choice% equ y (
    ".nuget/nuget.exe" push ApplicationInsights.Helpers.WebCore.*.nupkg
    ".nuget/nuget.exe" push ApplicationInsights.Helpers.Mvc5.*.nupkg
    ".nuget/nuget.exe" push ApplicationInsights.Helpers.WebApi2.*.nupkg
    ".nuget/nuget.exe" push ApplicationInsights.Helpers.Console.*.nupkg
    ".nuget/nuget.exe" push ApplicationInsights.Helpers.WebJobs.*.nupkg
)
pause