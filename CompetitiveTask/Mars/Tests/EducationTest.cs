using Mars.Model;
using Mars.Pages;
using Mars.TestData;
using Mars.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars.Tests
{
    [TestFixture, Order(3)]
    public class EducationTest : BaseSetUp.BaseSetUp
    {
        Education EducationPageObj;
        public EducationTest()
        {
            EducationPageObj = new Education();
        }

        [Test, Order(1)]
        public void AddEducation_Test()
        {
            //-----------test for adding education record---------
            try
            {

                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<EducationModel> educationList = JsonReader.GetData<EducationModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\AddEducation.json");

                //Deleting education if exists
                foreach (var row in educationList)
                {
                    EducationPageObj.DeleteEducation(row);
                }

                //Adding certificate and verifying the addition
                foreach (var row in educationList)
                {
                    EducationPageObj.AddEducation(row);
                    //Verifying success message for addition of a education record
                    string expectedMessage = "Education has been added";
                    string actualMessage = EducationPageObj.GetMessage();
                    Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match. Education not added!!");

                    //Verify the new education record added in the table.
                    string newAddedUniversity = EducationPageObj.GetUniversity();
                    string newAddedCountry = EducationPageObj.GetCountry();
                    string newAddedGraduationYear = EducationPageObj.GetGraduationYear();
                    string newAddedDegree = EducationPageObj.GetDegree();
                    string newAddedTitle = EducationPageObj.GetTitle();

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "AddEducation"), "Add education successful");

                    Assert.AreEqual(row.university, newAddedUniversity, "Actual and expected university do not match.");
                    Assert.AreEqual(row.country, newAddedCountry, "Actual and expected country from do not match.");
                    Assert.AreEqual(row.graduationYear, newAddedGraduationYear, "Actual and expected graduation year do not match.");
                    Assert.AreEqual(row.degree, newAddedDegree, "Actual and expected degree do not match.");
                    Assert.AreEqual(row.title, newAddedTitle, "Actual and expected title do not match.");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "AddEducation"), "Exception in Add Education");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }
        [Test, Order(3)]
        public void DeleteEducation_Test()
        {
            //-----------test for deleting education record---------

            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<EducationModel> educationToDeleteList = JsonReader.GetData<EducationModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\DeleteEducation.json");
                //Adding certificate for deletion
                foreach (var row in educationToDeleteList)
                {
                    EducationPageObj.AddEducation(row);
                }
                //Deleting education record and verifying the deletion              
                foreach (var row in educationToDeleteList)
                {
                    EducationPageObj.DeleteEducation(row);

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "DeleteEducation"), "Delete education successful ");

                    //Verifying success message for deletion of a education record
                    string expectedMessage = "Education entry successfully removed";
                    string actualMessage = EducationPageObj.GetMessage();
                    Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected education do not match. Education not deleted!!");
                    //Verify the education record deleted
                    string result = EducationPageObj.GetDeletedEducationResult(row);
                    Assert.AreEqual("Deleted", result, "Actual and expected result do not match. Education not deleted!!");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "DeleteEducation"), "Exception in  Delete Education");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }

        [Test, Order(2)]
        public void UpdateEducation_Test()
        {
            //-----------test for updating education record---------
            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<EducationModel> educationToUpdateLIst = JsonReader.GetData<EducationModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\UpdateEducation.json");
                //Updating education record and verifying the updation
                foreach (var row in educationToUpdateLIst)
                {
                    EducationPageObj.UpdateEducation(row);

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "UpdateEducation"), "Update education successful");

                    //Verifying success message for updation of a education record
                    string expectedMessage = "Education as been updated";
                    string actualMessage = EducationPageObj.GetMessage();
                    Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected education do not match. Education not updated!!");
                    //Verify the education record updated
                    string result = EducationPageObj.GetUpdatedEducationResult(row);
                    Assert.AreEqual("Updated", result, "Actual and expected result do not match. Education not updated!!");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "UpdateEducation"), "Exception in Update Education");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }
        [Test, Order(4)]
        public void CancelEducationAddition_Test()
        {
            //-----------test for cancel adding education record---------
            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<EducationModel> educationList = JsonReader.GetData<EducationModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\CancelEducationAddition.json");
                //Adding certificate and verifying the addition
                foreach (var row in educationList)
                {
                    EducationPageObj.CancelEducationAddition(row);
                    //Verify the new education record not added in the table.
                    string newAddedUniversity = EducationPageObj.GetUniversity();
                    string newAddedCountry = EducationPageObj.GetCountry();
                    string newAddedGraduationYear = EducationPageObj.GetGraduationYear();
                    string newAddedDegree = EducationPageObj.GetDegree();
                    string newAddedTitle = EducationPageObj.GetTitle();

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "CancelEducationAddition_Test"), "Cancel education addition successful");

                    Assert.AreNotEqual(row.university, newAddedUniversity, "Actual and expected university match.");
                    Assert.AreNotEqual(row.country, newAddedCountry, "Actual and expected country from match.");
                    Assert.AreNotEqual(row.graduationYear, newAddedGraduationYear, "Actual and expected graduation year match.");
                    Assert.AreNotEqual(row.degree, newAddedDegree, "Actual and expected degree match.");
                    Assert.AreNotEqual(row.title, newAddedTitle, "Actual and expected title match.");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "CancelCertificateAddition"), "Exception in Cancel Certificate Addition");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }
        [Test, Order(5)]
        public void AddInvalidEducation_Test()
        {
            //-----------test for adding invalid education record---------
            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<EducationModel> educationList = JsonReader.GetData<EducationModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\AddInvalidEducation.json");
                //Adding education and verifying the addition
                foreach (var row in educationList)
                {
                    EducationPageObj.AddEducation(row);
                    //Verifying error message for addition of invalid education record
                    string expectedMessage = "Please enter all the fields";
                    string actualMessage = EducationPageObj.GetMessage();
                    Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match.");
                    //Verify the new education record not added in the table.
                    string newAddedUniversity = EducationPageObj.GetUniversity();
                    string newAddedCountry = EducationPageObj.GetCountry();
                    string newAddedGraduationYear = EducationPageObj.GetGraduationYear();
                    string newAddedDegree = EducationPageObj.GetDegree();
                    string newAddedTitle = EducationPageObj.GetTitle();

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "AddInvalidEducation"), "Add invalid education successful");

                    Assert.AreNotEqual(row.university, newAddedUniversity, "Actual and expected university match.");
                    Assert.AreNotEqual(row.country, newAddedCountry, "Actual and expected country from match.");
                    Assert.AreNotEqual(row.graduationYear, newAddedGraduationYear, "Actual and expected graduation year match.");
                    Assert.AreNotEqual(row.degree, newAddedDegree, "Actual and expected degree match.");
                    Assert.AreNotEqual(row.title, newAddedTitle, "Actual and expected title match.");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "AddInvalidEducation"), "Exception in Update Invalid Education");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }
        [Test, Order(6)]
        public void CancelEducationUpdation_Test()
        {
            //-----------test for cancel updating education record---------
            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<EducationModel> educationToUpdateList = JsonReader.GetData<EducationModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\CancelEducationUpdation.json");
                //Updating education record and verifying the updation
                foreach (var row in educationToUpdateList)
                {
                    EducationPageObj.CancelEducationUpdation(row);
                    //Verify the new education record not updated in the table
                    string result = EducationPageObj.GetUpdatedEducationResult(row);

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "CancelEducationUpdation"), "Cancel education updation successful");

                    Assert.AreNotEqual("Updated", result, "Actual and expected result match.");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "CancelEducationUpdation"), "Exception in Update Invalid Education");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }

        [Test, Order(7)]
        public void UpdateInvalidEducation_Test()
        {
            //-----------test for updating invalid education record---------
            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<UpdateInvalidEducationModel> educationToUpdateList = JsonReader.GetData<UpdateInvalidEducationModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\UpdateInvalidEducation.json");
                //Updating invalid education record and verifying the updation
                foreach (var row in educationToUpdateList)
                {
                    EducationPageObj.UpdateInvalidEducation(row);
                    //Verifying error message for updation of invalid education record
                    string expectedMessage = "Please enter all the fields";
                    string actualMessage = EducationPageObj.GetMessage();
                    Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match.");
                    //Verify the new education record not updated in the table
                    string result = EducationPageObj.GetUpdatedInvalidEducationResult(row);

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "UpdateInvalidEducation"), "Update invalid education successful");

                    Assert.AreEqual("Not updated", result, "Actual and expected result do not match. Education updated!!");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "UpdateInvalidEducation"), "Exception in update invalid education");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }
    }
}
