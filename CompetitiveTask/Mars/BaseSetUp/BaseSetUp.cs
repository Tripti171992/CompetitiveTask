using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using Mars.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;
using Mars.TestData;
using Mars.Pages;
using AventStack.ExtentReports.Reporter.Configuration;

namespace Mars.BaseSetUp
{
    [TestFixture]
    public class BaseSetUp : CommonDriver
    {

        HomePage HomePageObj;
        public ExtentReports extent;
        public ExtentTest test;
        public BaseSetUp()
        {
            HomePageObj = new HomePage();
        }
        [OneTimeSetUp]
        public void TestSuitSetUp()
        {
            ExtentHtmlReporter htmlReport = new ExtentHtmlReporter(ConstantUtils.ReportsPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReport);
            extent.AddSystemInfo("Host Name", "Local Host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Tripti");

            htmlReport.Config.DocumentTitle = "Automation Report";
            htmlReport.Config.ReportName = "Test Report";
            htmlReport.Config.Theme = Theme.Dark;
        }

        [SetUp]
        public virtual void SetActions()
        {
            Initialize();
            NavigateUrl();

            //Login into Mars
            List<UserInformation> userList = JsonReader.GetData<UserInformation>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\UserInformation.json");
            HomePageObj.SignIn(userList[0]);
        }
        [TearDown]
        public void TearDownActions()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == TestStatus.Passed)
            {
                test.Pass("Test Case Passed!!");
            }
            else if (status == TestStatus.Failed)
            {
                test.Fail("Test Case Failed!!");

            }

            Close();
        }

        [OneTimeTearDown]
        public void testsuitteardown()
        {
            extent.Flush();
        }
    }
}

