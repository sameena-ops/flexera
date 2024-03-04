using Flexera.Automation.Selenium.tests.utilities;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Xunit;

namespace Flexera.Automation.Selenium.tests;

public abstract class BaseTest
{
    private IConfiguration Configuration { get; }
    
    public string? Browser { get; init; }
    
    public string? Url { get; init; }
    
    public abstract string UrlSectionName { get; set; }
    
    public IWebDriver? Driver { get; set; }
    

    protected BaseTest()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        Configuration = builder.Build();
        Browser = Configuration.GetSection("Browser").Value;
       Url = Configuration.GetSection(UrlSectionName).Value;
       Driver = BrowserFactory.GetDriverForBrowser(Browser);
       Driver?.Navigate().GoToUrl(Url);
       BrowserFactory.WaitForPageLoad("cookiescript_accept");
    }
}