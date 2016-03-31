using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Net.Boilerplate.Enums;
using Selenium.Net.Boilerplate.Helpers;

namespace Selenium.Net.Boilerplate.Integration
{
    public class IntegrationTestBase : IDisposable
    {
        public IWebDriver Driver;

        private readonly string _baseUrl;
        private readonly string _remoteWebDriverUrl;

        // Change test mode here for your needs
        private static readonly TestMode Mode = TestMode.LocalSeleniumServer;

        public static IEnumerable<object[]> Browsers
        {
            get
            {
                switch (Mode)
                {
                    case TestMode.BrowserstackExternal:
                        yield return new object[] { "Chrome", ""};
                        yield return new object[] { "Internet Explorer", "11.0"};
                        yield return new object[] { "Internet Explorer", "9.0"};
                        yield return new object[] { "Firefox", ""};
                        break;
                    case TestMode.BrowserstackLocal:
                        yield return new object[] { "Chrome", "" };
                        break;
                    case TestMode.LocalSeleniumServer:
                        yield return new object[] { "Chrome", ""};
                        break;
                    case TestMode.Custom:
                        // enter here your desired browsers
                        break;
                }
            }
        }

        public IntegrationTestBase()
        {
            switch (Mode)
            {
                case TestMode.BrowserstackExternal:
                    _remoteWebDriverUrl = "http://hub.browserstack.com/wd/hub/";
                    _baseUrl = "https://youtube.com";
                    break;
                case TestMode.BrowserstackLocal:
                    // In order to run your local build against BrowserStack servers:
                    // 1. Navigate to https://www.browserstack.com/local-testing and get binary for your Windows
                    // 2. Run the file from your machine with your access key (cmd => BrowserStackLocal.exe ### YOUR ACCESS KEY ###)
                    // 3. Execute test
                    _remoteWebDriverUrl = "http://hub.browserstack.com/wd/hub/";
                    _baseUrl = "https://youtube.com";
                    break;
                case TestMode.LocalSeleniumServer:
                    // Important: local/remote selenium server is needed
                    // 1. npm install selenium-standalone@latest -g
                    // 2. selenium-standalone install
                    // 3. selenium-standalone start
                    _remoteWebDriverUrl = "http://127.0.0.1:4444/wd/hub/";
                    _baseUrl = "https://youtube.com";
                    break;
                case TestMode.Custom:
                    // Enter here your desired url configuration
                    break;
            }
        }

        public void Setup(string browserName, string version)
        {
            var capabilityProvider = new CapabilityProvider();
            var capability = capabilityProvider.Configure(browserName, Mode, version);

            Driver = new RemoteWebDriver(new Uri(_remoteWebDriverUrl), capability);
        }

        public void Navigate(string url = "")
        {
            Driver.Navigate().GoToUrl($"{_baseUrl}/{url}");
        }

        public void Dispose()
        {
            Driver.Quit();
            GC.SuppressFinalize(this);
        }
    }
}
