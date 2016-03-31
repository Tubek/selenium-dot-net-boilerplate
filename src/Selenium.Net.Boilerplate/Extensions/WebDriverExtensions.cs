using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Selenium.Net.Boilerplate.Extensions
{
    public static class Config
    {
        public static readonly TimeSpan ImplicitWait = new TimeSpan(0, 0, 0, 10);
        public static readonly TimeSpan NoWait = new TimeSpan(0, 0, 0, 0);
    }

    public static class WebDriverExtensions
    {
        public static bool ElementIsPresent(this IWebDriver driver, By by)
        {
            var present = false;
            driver.Manage().Timeouts().ImplicitlyWait(Config.NoWait);

            try
            {
                present = driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
            }

            driver.Manage().Timeouts().ImplicitlyWait(Config.ImplicitWait);
            return present;
        }

        public static bool WaitUntilElementIsPresent(this IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            for (var i = 0; i < timeoutInSeconds; i++)
            {
                if (driver.ElementIsPresent(by)) return true;
                Thread.Sleep(1000);
            }
            return false;
        }

        public static bool WaitUntilElementIsNotPresent(this IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            for (var i = 0; i < timeoutInSeconds; i++)
            {
                if (!driver.ElementIsPresent(by)) return true;
                Thread.Sleep(1000);
            }
            return false;
        }

        public static void Wait(this IWebDriver driver, int ms = 1000)
        {
            Thread.Sleep(ms);
        }
    }
}
