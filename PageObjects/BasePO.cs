using CodeTest.Drivers;
using CodeTest.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CodeTest.PageObjects
{
    public class BasePO
    {
        public BasePO() { }

        public IWebDriver driver = DriverFactory.GetDriver();

        public void NavigateToURL(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        public void sendKeys(By by, string textToType)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(GlobalVariables.defaultTimeout));
            wait.Until(driver => driver.FindElement(by)).SendKeys(textToType);
        }

        public void waitForWebElementAndClick(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Utils.GlobalVariables.defaultTimeout));
            wait.Until(driver => driver.FindElement(by)).Click();
        }

        public int randomItem()
        {
            Random random = new Random();
            int randomitem = Utils.GlobalVariables.randomItems[random.Next(0, Utils.GlobalVariables.randomItems.Count)];

            //note: I remove the item we just found, so there are no duplicates in the cart
            GlobalVariables.randomItems.Remove(randomitem);
            return randomitem;
        }

    }
}
