
namespace CodeTest.Utils
{
    internal class GlobalVariables
    {
        public static string browser = "chrome";

        public static string homePage = "https://cms.demo.katalon.com/";

        public static int defaultTimeout = 10;

        public static List<int> randomItems = new List<int> { 26, 54, 27, 25, 66 };

        public static string ProjectRootDirectory = Directory.GetCurrentDirectory().Split("bin")[0];

        public static string ReportFolder = Path.Combine(ProjectRootDirectory, "Report");
    }

}
