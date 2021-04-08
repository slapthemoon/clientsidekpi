using Kpi.YourDomain.ClientTests.Model.Domain.Poduct;
using Kpi.YourDomain.ClientTests.Model.Platform.Drivers;
using Kpi.YourDomain.ClientTests.Platform.Configuration.Environment;
using Kpi.YourDomain.ClientTests.Platform.Factory;

namespace Kpi.YourDomain.ClientTests.UI.Product
{
    public class ProductTopSteps : StepsBase, IProductTopSteps
    {
        private readonly IWebDriver _webDriver;

        public ProductTopSteps (
            IWebDriver webDriver, 
            IEnvironmentConfiguration environmentConfiguration) 
            : base(webDriver, environmentConfiguration)
        {
            _webDriver = webDriver;
        }

        private ProductTopElement ProductTopElement => 
            PageFactory.Get<MainPage>(_webDriver).ProductTopElement;

        public string GetTitle()
        {
            return ProductTopElement.ProductHeaderLabel.GetText().Trim();
        }
    }
}
