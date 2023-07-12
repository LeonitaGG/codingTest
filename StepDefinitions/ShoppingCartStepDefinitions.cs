using CodeTest.Drivers;
using CodeTest.PageObjects;
using OpenQA.Selenium;

namespace CodeTest.StepDefinitions
{
    [Binding]
    public sealed class ShoppingCartStepDefinitions
    {

        private ShoppingCartPO cartPO;
        private IList<IWebElement> sortedPriceList;

        public ShoppingCartStepDefinitions(ShoppingCartPO cartPO) { this.cartPO = cartPO; }

        [Given("I access the katalon website")]
        public void AccessTheKatalonWebsite()
        {
            cartPO.navigateToHomePage();
        }

        [When("I add four random items to my cart")]
        public void AddFourItemsToCart()
        {
            for(int i = 0; i < 4; i++)
            {
                cartPO.addItemToCart();
            }
            Thread.Sleep(1000);
        }

        [When("I find total four items listed in my cart")]
        public void AssertFourItemsInCart()
        {
            cartPO.viewCartIsVisible();

            cartPO.checkQuantityInCart(4);
        }

        [When("I search for the lowest price item")]
        public void SearchForLowestPriceItem()
        {
            sortedPriceList = cartPO.sortItemPrices();
        }

        [When("I am able to remove the lowest price item from my cart")]
        public void RemoveLowestPriceItem()
        {
            var lowestPricedItem = sortedPriceList[0];
            cartPO.removeItemFromCart(lowestPricedItem);
        }

        [Then("I am able to verify three items in my cart")]
        public void AssertCorrectNumberOfItemsInCart()
        {
            Thread.Sleep(1000);
            cartPO.checkQuantityInCart(3);
            DriverFactory.cleanupDriver();
        }
    }
}
