using CodeTest.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CodeTest.PageObjects
{
    public class ShoppingCartPO : BasePO
    {

        public By addToCart_button => By.CssSelector("[data-product_id='" + randomItem().ToString() + "']");

        public By viewCart_button => By.CssSelector("a[title='View cart']");

        public By remove_button => By.CssSelector("a[class='remove']");


        public void navigateToHomePage()
        {
            NavigateToURL(Utils.GlobalVariables.homePage);
        }

        public void addItemToCart()
        {
            waitForWebElementAndClick(addToCart_button);
        }

        public void viewCartIsVisible()
        {
            waitForWebElementAndClick(viewCart_button);
        }

        public IList<IWebElement> sortItemPrices()
        {
            IList<IWebElement> priceElements = DriverFactory.CurrentDriver.FindElements(By.CssSelector("td.product-price"));
            IEnumerable<IWebElement> sortPriceElements = priceElements.OrderBy(p => p.Text);
            IList<IWebElement> sortedPriceElements = sortPriceElements.ToList();

            return sortedPriceElements;  
        }

        public void removeItemFromCart(IWebElement itemToRemove)
        {
            Thread.Sleep(1000);
            Console.WriteLine("lowest item price is: " + itemToRemove.Text);

            //get the parent element from price (child) element
            IWebElement parentElement = itemToRemove.FindElement(By.XPath(".."));
            parentElement.FindElement(remove_button).Click();
        }

        public void checkQuantityInCart(int quantity)
        {
            IWebElement cartTotal = driver.FindElement(By.CssSelector("form[method='post']  tbody"));
            IReadOnlyCollection<IWebElement> numberOfItemsInCart = cartTotal.FindElements(By.CssSelector("td.product-name"));
            int count = numberOfItemsInCart.Count();
            Assert.AreEqual(quantity, count);
            Console.WriteLine("Actual Quantity: " + count);
        }
    }
}
