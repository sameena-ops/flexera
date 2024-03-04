using System.Collections.ObjectModel;
using Flexera.Automation.Selenium.tests;
using Flexera.Automation.Selenium.tests.pageActions;
using Flexera.Automation.Selenium.tests.utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;

namespace flexera.automation.selenium;

public class BusinessValueCalculatorTests: BaseTest,IClassFixture<BrowserFactory>
{
    private readonly BusinessValueCalculatorPageHelper _businessValueCalculatorPageHelper = new();
    private readonly ITestOutputHelper output;
    public BusinessValueCalculatorTests(ITestOutputHelper output)
    {
        this.output = output;
        _businessValueCalculatorPageHelper = new BusinessValueCalculatorPageHelper();
    }
    
    public override string UrlSectionName { get; set; } = "BvcUrl";

    [Fact]
    public Task Verify_MainMenu_Items()
    {
        List<string> expectedMenuItems = ["Products", "Solutions", "Customer Success", "Resources", "About"];
        
        var menuItems = Driver?.FindElements(By.XPath("//*[@id=\"block-flexera-main-menu\"]/ul/li"));
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        List<string> actualMenuItems = menuItems.Select(menu => menu.Text).ToList();

        Assert.Equal(expectedMenuItems, actualMenuItems);
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task Verify_PageHeader_Content()
    {
        IWebElement? headerBlock = Driver?.FindElement(By.ClassName("basic-block"));
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        var paragraphs = headerBlock?.FindElements(By.TagName("p")).Select(p => p.Text).ToList();
        var headlines = headerBlock?.FindElements(By.TagName("h1")).Select(h => h.Text).ToList();
        
        Assert.Equal(4, paragraphs?.Count);
        Assert.Equal(1, headlines?.Count);
        
        Assert.Equal("Transform your IT data into actionable intelligence", headlines?.FirstOrDefault());
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task Verify_OrgBenefits_DropdownOptions()
    {
        List<string> expectedCountriesList = ["Please select","Canada", "France", "Germany", "Nordic Country", "United Kingdom", "United States","Country Not Listed"];
        List<string> expectedNoOfYearsList = ["1 year","2 years","3 years","4 years","5 years"];
       
        _businessValueCalculatorPageHelper.SwitchToRoiFrame(Driver);
        SelectElement countries = new SelectElement(Driver.FindElement(By.Id("Headquarters_Country")));
        SelectElement noOfYearsDropDown = new SelectElement(Driver.FindElement(By.Id("Number_of_Years")));
        var actualCountriesList = countries.Options.Select(option => option.Text).ToList();
        var actualNoOfYearsList = noOfYearsDropDown.Options.Select(option => option.Text).ToList();
        
        Assert.Equal(expectedCountriesList, actualCountriesList);
        Assert.Equal(expectedNoOfYearsList, actualNoOfYearsList);
        return Task.CompletedTask;
    }

    [Fact]
    public Task Verify_UserAbleToEdit_OrgBenefitsDetails()
    {
        _businessValueCalculatorPageHelper.EnterRoiValues(Driver);
        
        var projections = Driver.FindElement(By.XPath("//*[@id=\"app_user_form\"]/div[1]/div[1]/div[2]/div[1]/div[2]/div[2]/span"));
        var actualValues = projections.Text;
        
        Assert.Equal("$3.6M to $9.7M", actualValues);
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task Verify_InterestedAreas_Items()
    {
        List<string> expectedInterestsList =
        [
            "IT Visibility", "ITAM: Software Asset Management (SAM)", "ITAM: SaaS Management",
            "ITAM: Software Request and Reclamation", "Cloud Migration and Modernization", "Cloud Cost Optimization"
        ];
     _businessValueCalculatorPageHelper.SwitchToRoiFrame(Driver);
     ReadOnlyCollection<IWebElement> interests = Driver.FindElements(By.XPath("//*[@class=\"form-check col-lg-12 checkbox_div\"]"));
        
        var actualValues = interests.Select(interest => interest.Text).ToList();
        
        Assert.Equal(expectedInterestsList, actualValues);
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task Verify_InteractiveCharts_Available()
    {
        _businessValueCalculatorPageHelper.SwitchToRoiFrame(Driver);
       IWebElement charts = Driver.FindElement(By.XPath("//*[@id=\"app_interactive_chart2\"]"));
       Assert.True(charts.Displayed); 
       return Task.CompletedTask;
    }
    
}