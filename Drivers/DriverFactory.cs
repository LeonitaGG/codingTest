using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;

namespace CodeTest.Drivers
{
    public class DriverFactory
    {
        public static IWebDriver CurrentDriver;

        public static IWebDriver GetDriver()
        {
            try
            {
                CurrentDriver = createDriver();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            return CurrentDriver;
        }

        public static void cleanupDriver()
        {
            CurrentDriver.Close();
            CurrentDriver.Quit();
        }

        public static IWebDriver createDriver()
        {
            string browserName = Utils.GlobalVariables.browser;
            IWebDriver? driver = null;

            switch (browserName)
            {
                case "chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;

                case "firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;
            }

            return driver;
        }
    }
}
