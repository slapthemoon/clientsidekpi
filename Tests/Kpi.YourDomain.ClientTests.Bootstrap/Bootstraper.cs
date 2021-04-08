using System;
using Autofac;
using Kpi.YourDomain.ClientTests.Domain.Login;
using Kpi.YourDomain.ClientTests.Domain.Search;
using Kpi.YourDomain.ClientTests.Model.Domain.Login;
using Kpi.YourDomain.ClientTests.Model.Domain.Poduct;
using Kpi.YourDomain.ClientTests.Model.Domain.Search;
using Kpi.YourDomain.ClientTests.Model.Platform.Communication;
using Kpi.YourDomain.ClientTests.Model.Platform.Drivers;
using Kpi.YourDomain.ClientTests.Platform.Communication;
using Kpi.YourDomain.ClientTests.Platform.Configuration.Environment;
using Kpi.YourDomain.ClientTests.Platform.Configuration.Run;
using Kpi.YourDomain.ClientTests.Platform.Driver;
using Kpi.YourDomain.ClientTests.UI.Login;
using Kpi.YourDomain.ClientTests.UI.Product;
using Kpi.YourDomain.ClientTests.UI.Search;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Serilog;
using Serilog.Events;

namespace Kpi.YourDomain.ClientTests.Bootstrap
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
            Builder.RegisterType<SearchSteps>().As<ISearchSteps>().SingleInstance();
            Builder.RegisterType<ProductTopSteps>().As<IProductTopSteps>().SingleInstance();
            Builder.RegisterType<SearchContext>().As<ISearchContext>().SingleInstance();

            Builder.RegisterType<WebDriver>().As<IWebDriver>().SingleInstance();
        }
    }
}
