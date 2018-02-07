using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace TT_WebdriverCore.WebDriverCore
{
    public static class Browser
    {
        private static IWebDriver _webDriver;

        // Define the interface used to search for elements
        public static ISearchContext SearchContext => Driver;

        public static IJavaScriptExecutor JavaScriptExecutor => Driver as IJavaScriptExecutor;

        public static int WaitTimeout => 5;     // default it to 5 seconds

        private static IWebDriver Driver => _webDriver ?? (_webDriver = Initialize());

        public static void GoToUrl(string p_Url)
        {
            Driver.Navigate().GoToUrl(p_Url);
        }

        public static void Navigate(string p_Url)
        {
            GoToUrl(p_Url);
        }

        public static void IntroduceWaitFor()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        public static void DeleteCookies()
        {
            try
            {
                Driver.Manage().Cookies.DeleteAllCookies();
            }
            catch
            {
                // Ignored
            }
        }

        public static void RefreshPage()
        {
            Driver.Navigate().Refresh();
        }

        public static void GoToPreviousPage()
        {
            Driver.Navigate().Back();
        }

        public static string GetBrowserType()
        {
            var capabilities = ((RemoteWebDriver)Driver).Capabilities;
            return capabilities.BrowserName;
        }
        public static string GetCurrentUrl()
        {
            return Driver.Url;
        }
        public static string GetTitle()
        {
            return Driver.Title;
        }

        public static IWebElement GetElement(By p_ElementLocalization)
        {
            return SearchContext.FindElement(p_ElementLocalization);
        }

        public static IWebElement GetElementWithWait(By p_ElementLocalization)
        {
            WaitForPageLoadComplete();
            GetWaitDriver().Until(ExpectedConditions.ElementIsVisible(p_ElementLocalization));
            return SearchContext.FindElement(p_ElementLocalization);
        }

        public static void WaitUntilElementIsNotVisible(string p_Localization)
        {
            var wait = GetWaitDriver();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id(p_Localization)));
        }

        public static void WaitUntilElementIsNotVisible(By p_ElementSpecification)
        {
            var wait = GetWaitDriver();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(p_ElementSpecification));
        }

        public static void WaitFor(By p_ElementSpecification)
        {
            WaitFor(p_ElementSpecification, WaitTimeout);
        }

        public static void WaitFor(By p_ElementSpecification, int p_Timeout)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(p_Timeout));

            try
            {
                wait.Until(p_Driver =>
                {
                    try
                    {
                        p_Driver.FindElement(p_ElementSpecification);
                        return true;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                ////continue
            }
        }

        public static void WaitForElementToAppear(By p_ElementSpecification, int p_Timeout)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(p_Timeout));
            wait.Until(ExpectedConditions.ElementIsVisible(p_ElementSpecification));
        }

        public static void WaitForPageLoadComplete(int p_Timeout = -1)
        {
            if (p_Timeout == -1)
            {
                p_Timeout = WaitTimeout;
            }

            Driver.Manage().Timeouts().PageLoad = new TimeSpan(p_Timeout * 10000000); // 1 tick = one 10 millionth of a second so multiply to resolve back

            IWait<IWebDriver> wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(p_Timeout));
            wait.Until(p_Driver => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static void MaximizeWindow()
        {
            Driver.Manage().Window.Maximize();
        }

        public static void Close()
        {
            Driver.Quit();

            if (_webDriver != null)
            {
                _webDriver.Dispose();
                _webDriver = null;
            }
        }
        public static string GetNewWindowTitle()
        {
            var currentWindowHandle = Driver.CurrentWindowHandle;

            var newWindowHandle = Driver.WindowHandles.Single(p_Wh => p_Wh != currentWindowHandle);
            Driver.SwitchTo().Window(newWindowHandle);

            var newWindowTitle = Driver.Title;

            // close newly opened window then switch back to the original window
            Driver.Close();
            Driver.SwitchTo().Window(currentWindowHandle);

            return newWindowTitle;
        }
        public static IWebElement MoveToElement(IWebElement p_Element)
        {
            Actions act = new Actions(Driver);
            act.MoveToElement(p_Element);
            act.Perform();
            return p_Element;
        }

        public static void ScrollToElement(IWebElement p_Element)
        {
            try
            {
                var position = p_Element.Location.Y.ToString();
                var script = $"('#main-container').scrollTop({position})";
                ((IJavaScriptExecutor)Driver).ExecuteScript(script);
            }
            catch
            {
                // Ignored
            }
        }

        private static WebDriverWait GetWaitDriver()
        {
            var waitDriver = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            return waitDriver;
        }

        private static IWebDriver GetWebDriver(BrowserType p_Browser)
        {
            var driver = GetDriver(p_Browser);

            // Implicit wait of up to 10 seconds
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
            return driver;
        }

        private static IWebDriver GetDriver(BrowserType p_Browser)
        {
            switch (p_Browser)
            {
                case BrowserType.Chrome:
                    var options = new ChromeOptions();
                    options.AddArgument("start-maximized");
                    //return new ChromeDriver(options);

                case BrowserType.Firefox:
                    var profile = new FirefoxProfile();
                    profile.SetPreference("acceptUntrustedCerts", true);
                    return new FirefoxDriver(profile);

                case BrowserType.InternetExplorer:
                    return new InternetExplorerDriver(
                        new InternetExplorerOptions
                        {
                            IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                            UnhandledPromptBehavior = UnhandledPromptBehavior.Dismiss,
                            EnsureCleanSession = true
                        });

                default:
                    
                    Assert.Fail($"Browser type {p_Browser} not handled.");
                    break;

            }

            return new FirefoxDriver();
        }

        private static IWebDriver Initialize()
        {
            var browser = (BrowserType)Enum.Parse(typeof(BrowserType), ConfigurationManager.AppSettings["Browser"], true);
            return GetWebDriver(browser);
        }

        public static void Goto(string p_System, string p_Path = "", string p_QueryString = "")
        {
            if (Driver == null)
            {
                return;
            }

            Driver.Url = $"{p_System}{p_Path}{p_QueryString}";
        }
    }
}
