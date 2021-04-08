using System;
using System.Collections.Generic;
using Kpi.YourDomain.ClientTests.Model.Domain.Run;
using Kpi.YourDomain.ClientTests.Model.Platform.Element;
using Kpi.YourDomain.ClientTests.Model.Platform.Locator;
using Kpi.YourDomain.ClientTests.Platform.Configuration.Run;
using Kpi.YourDomain.ClientTests.Platform.Enum;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Serilog;
using IJavaScriptExecutor = Kpi.YourDomain.ClientTests.Model.Platform.Drivers.IJavaScriptExecutor;

namespace Kpi.YourDomain.ClientTests.Platform.Driver
{
    public sealed class WebDriver : Model.Platform.Drivers.IWebDriver, IJavaScriptExecutor, INative
    {
        private readonly IRunSettings _runSettings;
        private readonly ILogger _logger;
        private readonly Guid _sessionId;
        private IWebDriver _nativeDriver;

        public WebDriver (
            IRunSettings runSettings,
            ILogger logger)
        {
            _runSettings = runSettings;
            _logger = logger;
            _sessionId = Guid.NewGuid();
        }

        private bool IsRunning => _nativeDriver != null && _nativeDriver.Alive();

        private IWebDriver NativeDriver
        {
            get
            {
                if (!IsRunning)
                {
                    _logger.Information($"Creating Driver. SessionId: {_sessionId}");
                    _nativeDriver = InitDriver();
                    _logger.Information($"Creating Driver. Status: finished.");
                }

                return _nativeDriver;
            }
        }

        IWebElement INative.FindElement (Locator locator, int index) =>
            NativeDriver.FindElement(locator.ToSeleniumLocator());

        public IWebElement GetElement (Locator locator, int index) =>
            NativeDriver.FindElement(locator.ToSeleniumLocator());

        IReadOnlyCollection<IWebElement> INative.FindElements (Locator locator) =>
            _nativeDriver.FindElements(locator.ToSeleniumLocator());

        public IReadOnlyCollection<IWebElement> GetElements (Locator locator) =>
            _nativeDriver.FindElements(locator.ToSeleniumLocator());

        public void Get (string url) =>
            NativeDriver.Navigate().GoToUrl(url);

        public void Close ()
        {
            _logger.Information($"Start WebDriver Close. SessionId: {_sessionId}");
            _nativeDriver.Quit();
            _logger.Information($"Finished WebDriver Close. SessionId: {_sessionId}");
        }

        public IWebDriver GetNativeDriver () =>
            NativeDriver;

        public void AcceptAlert () =>
            _nativeDriver.SwitchTo().Alert().Accept();

        public void TakeScreenShot (string fileName) =>
            ((ITakesScreenshot)NativeDriver).GetScreenshot().SaveAsFile(fileName, ScreenshotImageFormat.Png);

        object IJavaScriptExecutor.ExecuteScript (string script, params object[] args) =>
            ((OpenQA.Selenium.IJavaScriptExecutor)NativeDriver).ExecuteScript(script, args);

        public void Refresh () =>
            NativeDriver.Navigate().Refresh();

        public void Dispose ()
        {
            _logger.Information($"Start WebDriver Dispose. SessionId: {_sessionId}");
            _nativeDriver?.Quit();
            _logger.Information($"Finished WebDriver Dispose. SessionId: {_sessionId}");
        }

        private IWebDriver InitDriver () => CreateDriver();

        private IWebDriver CreateDriver ()
        {
            _logger.Information("Starting Create Driver.");
            var ciRunType = Environment.GetEnvironmentVariable("testrun.runtype");
            var runType = string.IsNullOrEmpty(ciRunType)
                ? _runSettings.RunType
                : ciRunType.ToEnum<RunType>();
            _logger.Information($"Run Type is {runType}.");
            return runType switch
            {
                RunType.SeleniumGrid => InitSeleniumGrid(),
                RunType.Local => InitLocalRun(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private ChromeOptions GetChromeOptions ()
        {
            _logger.Information("Starting Get Chrome Options.");
            var options = new ChromeOptions();
            options.AddArgument("--allow-running-insecure-content");
            options.AddArgument("test-type");
            options.AddArgument("--disable-extensions");
            options.AddArguments("--window-size=1920,1080");
            options.AddArgument("disable-infobars");
            options.AddArgument("no-sandbox");
            options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.186 Safari/537.36 FGE5GDWER1XZOPIMLW8OE");
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.SetLoggingPreference(LogType.Client, LogLevel.All);
            var isHeadless = Environment.GetEnvironmentVariable("testrun.chrome.headless");
            if (bool.TryParse(isHeadless, out _))
            {
                options.AddArgument("--headless");
                _logger.Information($"Headless parameter has {isHeadless} value.");
            }

            _logger.Information("Finished Get Chrome Options.");
            return options;
        }

        private IWebDriver InitSeleniumGrid ()
        {
            _logger.Information("Starting Init Selenium Grid.");
            var chromeOptions = GetChromeOptions();
            chromeOptions.AddAdditionalCapability("browser", "chrome", true);
            var remoteWebDriver = new RemoteWebDriver(
                new Uri(_runSettings.SeleniumGrid.HostUri),
                chromeOptions.ToCapabilities(),
                TimeSpan.FromMinutes(5));
            remoteWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromMinutes(2);
            _logger.Information("Finished Init Selenium Grid.");
            return remoteWebDriver;
        }

        private IWebDriver InitLocalRun ()
        {
            _logger.Information("Starting Init Local Run.");
            var chromeOptions = GetChromeOptions();
            var chromeService = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            chromeService.EnableVerboseLogging = true;
            chromeService.LogPath = $"{AppDomain.CurrentDomain.BaseDirectory}/Logs/chrome_client_logs_{DateTime.UtcNow:yyyy_MM_dd_hh_mm_ss}.txt";
            var driver = new ChromeDriver(
                chromeService,
                chromeOptions,
                TimeSpan.FromMinutes(5));
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromMinutes(6);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(6);
            _logger.Information("Finished Init Local Run.");
            return driver;
        }
    }
}
