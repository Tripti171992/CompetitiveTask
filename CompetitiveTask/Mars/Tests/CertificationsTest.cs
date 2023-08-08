using AventStack.ExtentReports;
using Mars.BaseSetUp;
using Mars.Model;
using Mars.Pages;
using Mars.TestData;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mars.Tests
{
    [TestFixture, Order(2)]
    public class CertificationsTest : BaseSetUp.BaseSetUp
    {
        Certifications CertificationsPageObj;
        public CertificationsTest()
        {
            CertificationsPageObj = new Certifications();
        }

        [Test, Order(1)]
        public void AddCertificate_Test()
        {
            //-----------test for adding certificate record---------
            try
            {

                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<CertificateModel> certificateList = JsonReader.GetData<CertificateModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\AddCertificate.json");

                //Deleting certificate if exists
                foreach (var row in certificateList)
                {
                    CertificationsPageObj.DeleteCertificate(row);
                }

                //Adding certificate and verifying the addition
                foreach (var row in certificateList)
                {
                    // CertificationsPageObj.AddCertificate(row);
                    CertificationsPageObj.AddCertificate(row);

                    //Verifying success message for addition of a certificate record
                    string expectedMessage = row.certificate + " has been added to your certification";
                    string actualMessage = CertificationsPageObj.GetMessage();
                    Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match. Certificate not added!!");

                    //Verify the new certificate record added in the table.
                    string newAddedcertificate = CertificationsPageObj.GetCertificate();
                    string newAddedCertifiedFrom = CertificationsPageObj.GetCertifiedFrom();
                    string newAddedYear = CertificationsPageObj.GetYear();

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "AddCertificate"), "Add certificate successful");

                    Assert.AreEqual(row.certificate, newAddedcertificate, "Actual and expected certificate do not match.");
                    Assert.AreEqual(row.certifiedFrom, newAddedCertifiedFrom, "Actual and certified from do not match.");
                    Assert.AreEqual(row.year, newAddedYear, "Actual and expected year do not match.");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "AddCertificate"), "Exception in Add Certificate");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }

        [Test, Order(3)]
        public void DeleteCertificate_Test()
        {
            //-----------test for deleting certificate record---------

            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<CertificateModel> certificateToDeleteList = JsonReader.GetData<CertificateModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\DeleteCertificate.json");
                //Adding certificate for deletion
                foreach (var row in certificateToDeleteList)
                {
                    CertificationsPageObj.AddCertificate(row);
                }
                //Deleting certificate record and verifying the deletion              
                foreach (var row in certificateToDeleteList)
                {
                    //CertificationsPageObj.DeleteCertificate(row);
                    CertificationsPageObj.DeleteCertificate(row);

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "DeleteCertificate"), "Delete certificate successful");

                    //Verifying success message for deletion of a certificate record
                    string expectedMessage = row.certificate + " has been deleted from your certification";
                    string actualMessage = CertificationsPageObj.GetMessage();
                    Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected certificate do not match. Certificate not deleted!!");
                    //Verify the certificate record deleted
                    string result = CertificationsPageObj.GetDeletedCertificateResult(row);
                    Assert.AreEqual("Deleted", result, "Actual and expected result do not match. Certificate not deleted!!");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "DeleteCertificate"), "Exception in Delete Certificate");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }

        [Test, Order(2)]
        public void UpdateCertificate_Test()
        {
            //-----------test for updating certificate record---------
            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<CertificateModel> certificateToUpdateLIst = JsonReader.GetData<CertificateModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\UpdateCertificate.json");
                //Updating certificate record and verifying the updation
                foreach (var row in certificateToUpdateLIst)
                {
                    CertificationsPageObj.UpdateCertificate(row);

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "UpdateCertificate"), "Update certificate successful");

                    //Verifying success message for updation of a certificate record
                    string expectedMessage = row.certificate + " has been updated to your certification";
                    string actualMessage = CertificationsPageObj.GetMessage();
                    Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected certificate do not match. Certificate not updated!!");
                    //Verify the certificate record updated
                    string result = CertificationsPageObj.GetUpdatedCertificateResult(row);
                    Assert.AreEqual("Updated", result, "Actual and expected result do not match. Certificate not updated!!");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "UpdateCertificate"), "Exception in Update Certificate");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }

        [Test, Order(4)]
        public void CancelCertificateAddition_Test()
        {
            //-----------test for cancel adding certificate record---------
            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<CertificateModel> certificateList = JsonReader.GetData<CertificateModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\CancelCertificateAddition.json");
                //Adding certificate and verifying the addition
                foreach (var row in certificateList)
                {
                    CertificationsPageObj.CancelCertificateAddition(row);
                    //Verify the new certificate record not added in the table.
                    string newAddedcertificate = CertificationsPageObj.GetCertificate();
                    string newAddedCertifiedFrom = CertificationsPageObj.GetCertifiedFrom();
                    string newAddedYear = CertificationsPageObj.GetYear();

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "CancelCertificateAddition"), "Cancel certificate addition successful");

                    Assert.AreNotEqual(row.certificate, newAddedcertificate, "Actual and expected certificate match.");
                    Assert.AreNotEqual(row.certifiedFrom, newAddedCertifiedFrom, "Actual and certified from match.");
                    Assert.AreNotEqual(row.year, newAddedYear, "Actual and expected year match.");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "CancelCertificateAddition"), "Exception in Cancel CertificateAddition");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }

        [Test, Order(5)]
        public void AddInvalidCertificate_Test()
        {
            //-----------test for adding invalid certificate record---------
            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<CertificateModel> certificateList = JsonReader.GetData<CertificateModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\AddInvalidCertificate.json");
                //Adding invalid certificate and verifying the addition
                foreach (var row in certificateList)
                {
                    // CertificationsPageObj.AddCertificate(row);
                    CertificationsPageObj.AddCertificate(row);
                    //Verifying error message for addition of invalid certificate record
                    string expectedMessage = "Please enter Certification Name, Certification From and Certification Year";
                    string actualMessage = CertificationsPageObj.GetMessage();
                    Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match.");
                    //Verify the new certificate record not added in the table.
                    string newAddedcertificate = CertificationsPageObj.GetCertificate();
                    string newAddedCertifiedFrom = CertificationsPageObj.GetCertifiedFrom();
                    string newAddedYear = CertificationsPageObj.GetYear();

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "AddInvalidCertificate"), "Add Invalid Certificate successful");

                    Assert.AreNotEqual(row.certificate, newAddedcertificate, "Actual and expected certificate match.");
                    Assert.AreNotEqual(row.certifiedFrom, newAddedCertifiedFrom, "Actual and certified from match.");
                    Assert.AreNotEqual(row.year, newAddedYear, "Actual and expected year match.");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "AddInvalidCertificate"), "Exception in Add Invalid Certificate");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }
        [Test, Order(6)]
        public void CancelCertificateUpdation_Test()
        {
            //-----------test for cancel updating certificate record---------
            try
            {
                 test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<CertificateModel> certificateToUpdateList = JsonReader.GetData<CertificateModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\CancelCertificateUpdation.json");
                //Updating certificate record and verifying the updation
                foreach (var row in certificateToUpdateList)
                {
                    CertificationsPageObj.CancelCertificateUpdation(row);
                    //Verify the new certificate record not updated in the table
                    string result = CertificationsPageObj.GetUpdatedCertificateResult(row);

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "CancelCertificateUpdation"), "Cancel certificate updation successful");

                    Assert.AreNotEqual("Updated", result, "Actual and expected result match.");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "CancelCertificateUpdation"), "Exception in Cancel Certificate Updation");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }
        [Test, Order(7)]
        public void UpdateInvalidCertificate_Test()
        {
            //-----------test for updating invalid certificate record---------
            try
            {
                 test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                List<UpdateInvalidCertificateModel> certificateToUpdateLIst = JsonReader.GetData<UpdateInvalidCertificateModel>("C:\\Users\\Tripti Mehta\\Desktop\\Industry Connect\\GitHubRepository\\CompetitiveTask\\CompetitiveTask\\CompetitiveTask\\Mars\\JsonObject\\UpdateInvalidCertificate.json");
                //Updating invalid certificate record and verifying the updation
                foreach (var row in certificateToUpdateLIst)
                {
                    CertificationsPageObj.UpdateInvalidCertificate(row);

                    //Attaching screenshot with report
                    test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "UpdateInvalidCertificate"), "Update invalid certificate successful");

                    //Verifying error message for updation of an invalid certificate record
                    string expectedMessage = "Please enter Certification Name, Certification From and Certification Year";
                    string actualMessage = CertificationsPageObj.GetMessage();
                    Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected certificate do not match. Certificate updated!!");
                    //Verify the certificate record not updated
                    string result = CertificationsPageObj.GetUpdatedInvalidCertificateResult(row);
                    Assert.AreEqual("Not updated", result, "Actual and expected result do not match. Certificate updated!!");
                }
            }
            catch (Exception ex)
            {
                test.AddScreenCaptureFromPath(CommonMethods.SaveScreenshot(driver, "UpdateInvalidCertificate"), "Exception in Update Invalid Certificate");
                test.Fail(ex.Message);
                Assert.Fail();
            }
        }
    }
}