using System;
using Autofac;
using Kpi.Looperman.ClientTests.Domain.Login;
using Kpi.Looperman.ClientTests.Model.Domain.AuthInfo;
using Kpi.Looperman.ClientTests.Model.Domain.Login;
using Kpi.Looperman.ClientTests.Model.Domain.SiteInfo;
using Kpi.Looperman.ClientTests.Model.Platform.Communication;
using Kpi.Looperman.ClientTests.Model.Platform.Drivers;
using Kpi.Looperman.ClientTests.Platform.Communication;
using Kpi.Looperman.ClientTests.Platform.Configuration.Environment;
using Kpi.Looperman.ClientTests.Platform.Configuration.Run;
using Kpi.Looperman.ClientTests.Platform.Driver;
using Kpi.Looperman.ClientTests.UI.Login;
using Kpi.Looperman.ClientTests.UI.Main;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Serilog;
using Serilog.Events;

namespace Kpi.Looperman.ClientTests.Bootstrap
{
    public class Bootstraper
    {
        private ContainerBuilder _builder;

        public ContainerBuilder Builder => _builder ??= new ContainerBuilder();

        public void ConfigureServices(IConfigurationBuilder configurationBuilder)
        {
            var configurationRoot = configurationBuilder.Build();
            Builder.Register<ILogger>((c, p) => new LoggerConfiguration()
                .WriteTo.File(
                    $"Logs/log_{DateTime.UtcNow:yyyy_MM_dd_hh_mm_ss}.txt",
                    LogEventLevel.Verbose,
                    "{Timestamp:dd-MM-yyyy HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger())
                .SingleInstance();

            // Configurations
            Builder.Register<IEnvironmentConfiguration>(context => configurationRoot.Get<EnvironmentConfiguration>())
                .SingleInstance();
            Builder.Register<IRunSettings>(cont => configurationRoot.Get<RunSettings>())
                .SingleInstance();

            Builder.RegisterType<Client>().As<IClient>().InstancePerDependency();
            Builder.RegisterType<RestClient>().As<IRestClient>().InstancePerDependency();

            // Logic
            Builder.RegisterType<LoginContext>().As<ILoginContext>().SingleInstance();
            Builder.RegisterType<LoginSteps>().As<ILoginSteps>().SingleInstance();
            Builder.RegisterType<AuthCheckSteps>().As<IAuthCheckSteps>().SingleInstance();
            Builder.RegisterType<SiteInfoSteps>().As<ISiteInfoSteps>().SingleInstance();

            Builder.RegisterType<WebDriver>().As<IWebDriver>().SingleInstance();
        }
    }
}
