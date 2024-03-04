using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;

namespace Flexera.Automation.Selenium.tests.utilities;

public class BrowserFactory : IDisposable
{
    private static IWebDriver? _driver;
    public static IWebDriver GetDriverForBrowser(string type = "chrome")
    {
        if (_driver != null)
            return _driver;
        switch (type.ToLower(new CultureInfo("en-US", false)))
        {
            case "chrome":
                ChromeOptions chromeOptions1 = new ChromeOptions();
                chromeOptions1.AddArgument("start-maximized"); 
                _driver = new ChromeDriver(chromeOptions1);
                break;
            case "firefox":
                new DriverManager().SetUpDriver((IDriverConfig) new FirefoxConfig(), "MatchingBrowser");
                _driver = new FirefoxDriver();
                _driver.Manage().Window.Maximize();
                break;
            case "headless":
                ChromeOptions chromeOptions2 = new ChromeOptions();
                chromeOptions2.AddArgument("headless");
               _driver = new ChromeDriver(chromeOptions2); 
                _driver.Manage().Window.Maximize();
                break;
            default:
                throw new ArgumentException("The browser '" + type + "' is not supported. Please use 'chrome', 'firefox' or 'headless");
        }
        return _driver;
    }

    public static void WaitForPageLoad(string acceptCookies)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.Id(acceptCookies)).Displayed);
            bool acceptExists = (bool)_driver?.FindElement(By.Id(acceptCookies)).Displayed;
            if (acceptExists)
            {
                _driver.FindElement(By.Id(acceptCookies)).Click();
            }
        }
        catch(WebDriverTimeoutException e)
        {
        }
    }

    public static void QuitInstance()
    {
        if (_driver != null)
        {
            _driver.Quit();
            _driver = null;
        }
    }

    public void Dispose()
    {
        if (_driver != null)
        {
            _driver.Quit();
            _driver = null;
        }
    }
}