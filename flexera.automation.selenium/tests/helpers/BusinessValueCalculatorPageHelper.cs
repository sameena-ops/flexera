using Flexera.Automation.Selenium.tests.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace flexera.automation.selenium.tests.helpers;

public class BusinessValueCalculatorPageHelper
{
    public void EnterRoiValues(IWebDriver driver)
    {
        SwitchToRoiFrame(driver);
        var revenue = driver.FindElement(By.XPath("//*[@id=\"AnnualRevenue\"]"));
        revenue?.Clear();
        revenue?.SendKeys("1000");
        var country = driver.FindElement(By.Id("Headquarters_Country"));
        country?.SendKeys("Nordic Country");
        var noOfEmployees = driver.FindElement(By.Id("NumberofEmployees"));
        noOfEmployees?.Clear();
        noOfEmployees?.SendKeys("1000");
        var annualBudget = driver.FindElement(By.XPath("//*[@id=\"app_user_form\"]/div[1]/div[1]/div[1]/div/div[5]/div/input"));
        annualBudget?.Clear();
        annualBudget?.SendKeys("5000");
        SelectElement noOfYearsDropDown = new SelectElement(driver.FindElement(By.Id("Number_of_Years")));
        noOfYearsDropDown.SelectByValue("1");
    }

    public void SwitchToRoiFrame(IWebDriver driver)
    {
        try
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
            IWebElement? roiCalculator = driver.FindElement(By.XPath("//*[@id=\"block-flexera-content\"]//p[3]/a"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            roiCalculator?.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.SwitchTo().Frame("vroi");
        }
        catch (Exception e)
        {
           new Screenshots(driver).TakeScreenshot("SwitchToRoiFrame","SwitchToRoiFrame");
            throw;
        }
    }
    
}