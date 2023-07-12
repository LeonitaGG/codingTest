using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using CodeTest.Drivers;
using OpenQA.Selenium;
using System.Reflection;

namespace CodeTest.Utils
{
    [Binding]
    class ReportManager: TechTalk.SpecFlow.Steps
    {
        private static ExtentTest? featureName;
        private static ExtentTest? scenario;
        private static ExtentReports? extent;

        [BeforeTestRun]
        public static void InitializeReport()
        {
            string reportDir = Utils.GlobalVariables.ReportFolder;

            if (!Directory.Exists(reportDir))
            {
                Directory.CreateDirectory(reportDir);
            }
            string path = Path.Combine(reportDir, "TestReport.html");
            ExtentV3HtmlReporter htmlReporter = new ExtentV3HtmlReporter(path);

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            htmlReporter.Config.DocumentTitle = "Framework Report";
            htmlReporter.Config.ReportName = "Test Automation Report";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            htmlReporter.Start();
        }


        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featurecontext)
        {
            featureName = extent.CreateTest(featurecontext.FeatureInfo.Title);
        }

        [AfterStep]
        public void InsertReportingSteps(ScenarioContext sc)
        {
            var stepType = sc.StepContext.StepInfo.Text;

            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(sc, null);

            if (sc.TestError == null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                }
            }

            if (sc.TestError != null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(sc.TestError.Message);
                    DateTime currentTime = DateTime.Now;
                    TakeScreenshot(DriverFactory.CurrentDriver, "FailedTestScreenshot" + currentTime.ToString());
                }
                if (stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(sc.TestError.Message);
                    DateTime currentTime = DateTime.Now;
                    TakeScreenshot(DriverFactory.CurrentDriver, "FailedTestScreenshot" + currentTime.ToString());
                }
                if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(sc.TestError.Message);
                    DateTime currentTime = DateTime.Now;
                    TakeScreenshot(DriverFactory.CurrentDriver, "FailedTestScreenshot" + currentTime.ToString());
                }
                if (stepType == "And")
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(sc.TestError.Message);
                    DateTime currentTime = DateTime.Now;
                    TakeScreenshot(DriverFactory.CurrentDriver, "FailedTestScreenshot" + currentTime.ToString());
                }
            }
        }

        public void TakeScreenshot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            string screenshotFilePath = Path.Combine(Utils.GlobalVariables.ReportFolder, "Screenshots", screenshotName + ".png");
            screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);
        }
    }
}
