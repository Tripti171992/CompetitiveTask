using AventStack.ExtentReports;
using Mars.BaseSetUp;
using Mars.Pages;
using Mars.TestData;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mars.Utilities.CommonMethods;
using static System.Net.Mime.MediaTypeNames;

namespace Mars.Tests
{
    [TestFixture, Order(1)]
    public class HomePageTest : BaseSetUp.BaseSetUp
    {
        HomePage HomePageObj;
        public HomePageTest()
        {
            HomePageObj = new HomePage();
        }

        [SetUp]
        public override void SetActions()
        {
            Initialize();
            NavigateUrl();
        }

        [Test, Order(1)]
        public void SignIn_Test()
        {
            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<UserInformation> userList = JsonReader.GetData<UserInformation>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\UserInformation.json");

                foreach (var user in userList)
                {
                    HomePageObj.SignIn(user);
                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "SignIn"),"successful sign in");
                    //Verify if user is taken to their home page upon login in to Mars 
                    string expectedUserName = "Hi " + user.firstName;      
                    string actualUserName = HomePageObj.GetFirstName();
                    Assert.AreEqual(expectedUserName, actualUserName, "Actual and expected username do not match.User not logged in successfully !!");                    
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "SignIn"), "Exception in Signin");
                test.Fail(ex.Message);
                Assert.Fail();
                
            }
        }
        [Test, Order(2)]
        public void SignInInvalid_Test()
        {
            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<UserInformation> userList = JsonReader.GetData<UserInformation>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\InvalidUserInformation.json");

                foreach (var user in userList)
                {
                    HomePageObj.SignInInvalid(user);
                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "SignInInvalid"), "Invalid signin successful");
                    //Verify if user is not taken to their home page upon login into Mars with invalid credentials
                    string actualUserName = HomePageObj.GetFirstName();
                    
                    bool result;

                    if (actualUserName.StartsWith("Hi "))
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                    Assert.AreEqual(true,result, "Logged in!! ");                   
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "SignInInvalid"), "Exception in Invalid Signin");
                test.Fail(ex.Message);
                Assert.Fail();               
            }
        }
    }
}
