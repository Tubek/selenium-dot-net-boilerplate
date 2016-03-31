using OpenQA.Selenium.Remote;
using Selenium.Net.Boilerplate.Enums;

namespace Selenium.Net.Boilerplate.Helpers
{
    public class CapabilityProvider
    {
        public DesiredCapabilities Configure(string browser, TestMode mode, string version = "")
        {
            DesiredCapabilities capabilities;

            switch (browser)
            {
                case "Internet Explorer":
                    capabilities = InternetExplorer(version);
                    break;
                case "Chrome":
                    capabilities = Chrome();
                    break;
                case "Firefox":
                    capabilities = Firefox();
                    break;
                default:
                    capabilities = Chrome();
                    break;
            }

            if (mode == TestMode.BrowserstackLocal)
            {
                capabilities.SetCapability("browserstack.local", "true");
            }

            if (mode == TestMode.BrowserstackExternal)
            {
                capabilities.SetCapability("browserstack.user", "### YOUR-BROWSERSTACK-USERNAME ###");
                capabilities.SetCapability("browserstack.key", "### YOUR-BROWSERSTACK-KEY ###");
            }

            return capabilities;
        }

        private DesiredCapabilities Chrome()
        {
            var capability = DesiredCapabilities.Chrome();
            capability.SetCapability("browserName", "chrome");

            return capability;
        }

        private DesiredCapabilities Firefox()
        {
            var capability = DesiredCapabilities.Firefox();
            capability.SetCapability("browserName", "Firefox");

            return capability;
        }

        private DesiredCapabilities InternetExplorer(string version = "")
        {
            var capability = DesiredCapabilities.InternetExplorer();

            capability.SetCapability("ignoreZoomSetting", true);
            capability.SetCapability("requireWindowFocus", true);
            capability.SetCapability("ignoreProtectedModeSettings", true);
            capability.SetCapability("browserName", "internet explorer");
            capability.SetCapability("version", version);

            return capability;

        }
    }
}
