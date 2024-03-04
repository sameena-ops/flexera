using OpenQA.Selenium;

namespace Flexera.Automation.Selenium.tests.utilities;

public class Screenshots
{
    private static IWebDriver? _driver;

    public Screenshots(IWebDriver driver)
    {
        _driver = driver;
    }
    public void TakeScreenshot(string folder, string fileName)
    {
        var rootFolder = "Screenshots";
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), rootFolder, folder);
        Directory.CreateDirectory(folderPath);
        var filePath = Path.Combine(folderPath, fileName + ".png");
        ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile(filePath);
    }
}