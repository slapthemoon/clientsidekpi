using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Kpi.YourDomain.ClientTests.Platform.Configuration.Environment;
using Kpi.YourDomain.ClientTests.Platform.Driver;
using OpenQA.Selenium;
using Serilog;
using TechTalk.SpecFlow;
using IWebDriver = Kpi.YourDomain.ClientTests.Model.Platform.Drivers.IWebDriver;

namespace Kpi.YourDomain.ClientTests.Tests.Hooks
{
    [Binding]
    public sealed class CommonHooks
    {
        private static ExtentTest _featureName;
        private static AventStack.ExtentReports.ExtentReports _extent;
        private static string _featureTitle;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private readonly ILogger _logger;
        private readonly IWebDriver _webDriver;
        private readonly IEnvironmentConfiguration _environmentConfiguration;
        private ExtentTest _scenario;

        public CommonHooks(
            ScenarioContext scenarioContext,
            FeatureContext featureContext,
            ILogger logger,
            IWebDriver webDriver,
            IEnvironmentConfiguration environmentConfiguration)
        {
            _webDriver = webDriver;
            _environmentConfiguration = environmentConfiguration;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _logger = logger;
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            // Initialize Extent report before test starts
            var reportPath = $"{AppDomain.CurrentDomain.BaseDirectory}ExtentReport.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);

            // Attach report to reporter
            _extent = new AventStack.ExtentReports.ExtentReports();

            _extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            Log.Information("Start TearDownReport.");
            _extent.Flush();
            Log.Information("Finished TearDownReport.");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _logger.Information("Start AfterScenario. WebDriver closing.");
            try
            {
                _webDriver.Close();
                _logger.Information("Finished AfterScenario.");
            }
            catch (Exception e)
            {
                _logger.Error($"Closing WebDriver. Exception: {e}.");
            }
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                }
                else if (stepType == "And")
                {
                    _scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
                }
            }
            else if (_scenarioContext.TestError != null)
            {
                var mediaEntity = CaptureScreenshot(_scenarioContext.ScenarioInfo.Title.Trim());
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text)
                        .Fail(_scenarioContext.TestError.Message, mediaEntity);
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text)
                        .Fail(_scenarioContext.TestError.Message, mediaEntity);
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text)
                        .Fail(_scenarioContext.TestError.Message, mediaEntity);
                }
            }
            else if (_scenarioContext.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending");
                }
            }
        }

        [BeforeScenario]
        public void Initialize()
        {
            _logger.Information($"Start Initialize BeforeScenario..");
            var ciEnvHostUri = Environment.GetEnvironmentVariable("testrun.environment.hosturi");
            if (!string.IsNullOrEmpty(ciEnvHostUri))
            {
                _environmentConfiguration.EnvironmentUri = ciEnvHostUri;
                _logger.Information($"_environmentConfiguration.EnvironmentUri: {_environmentConfiguration.EnvironmentUri}");
            }

            if (string.IsNullOrEmpty(_featureTitle) || _featureTitle != _featureContext.FeatureInfo.Title)
            {
                _featureTitle = _featureContext.FeatureInfo.Title;
                _featureName = _extent.CreateTest<Feature>(_featureTitle);
            }

            _scenario = _featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        private MediaEntityModelProvider CaptureScreenshot(string name)
        {
            var driver = ((WebDriver)_webDriver).GetNativeDriver();
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name).Build();
        }
    }
}
