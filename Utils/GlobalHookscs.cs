using CodeTest.Drivers;

namespace CodeTest.Utils
{
    internal class GlobalHookscs
    {
        [Before]
        public void StartWebDriver()
        {
            DriverFactory.createDriver();
        }

        [After]
        public void StopWebDriver()
        {
            DriverFactory.cleanupDriver();
        }
    }
}
