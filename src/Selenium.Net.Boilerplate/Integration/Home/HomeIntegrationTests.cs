using OpenQA.Selenium;
using Selenium.Net.Boilerplate.Extensions;
using Xunit;

namespace Selenium.Net.Boilerplate.Integration.Home
{
    public class ChannelIntegrationTests : IntegrationTestBase
    {
        [Theory, MemberData("Browsers")]
        public void ShouldBeToSearchByDefinedPhrase(string browser, string version)
        {
            Setup(browser, version);
            Navigate();
            Driver.SwitchTo().Window(Driver.CurrentWindowHandle);

            var searchInput = Driver.FindElement(By.Id("masthead-search-term"));

            searchInput.SendKeys("Wojciech Tubek");
            searchInput.SendKeys(Keys.Enter);

            Assert.Equal(Driver.Url, "https://www.youtube.com/results?search_query=Wojciech+Tubek");
            Assert.True(Driver.ElementIsPresent(By.Id("results")));
        }
    }
}
