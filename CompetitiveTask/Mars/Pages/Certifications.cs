using Mars.Model;
using Mars.TestData;
using Mars.Utilities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MongoDB.Driver.WriteConcern;

namespace Mars.Pages
{
    public class Certifications : CommonDriver
    {
        private IWebElement CertificationsTab => driver.FindElement(By.XPath("//a[text()='Certifications']"));
        private IWebElement AddNewCertificateButton => driver.FindElement(By.XPath("//*[text()='Certificate']/following-sibling::th[3]/div"));
        private IWebElement CertificateTextBox => driver.FindElement(By.XPath("//input[@placeholder='Certificate or Award']"));
        private IWebElement CertifiedFromTextBox => driver.FindElement(By.XPath("//input[@placeholder='Certified From (e.g. Adobe)']"));
        private IWebElement YearDropDown => driver.FindElement(By.XPath("//select[@class='ui fluid dropdown']"));
        private IWebElement AddButton => driver.FindElement(By.XPath("//input[@value='Add']"));
        private IWebElement CancelButton => driver.FindElement(By.XPath("//*[@value='Cancel']"));
        private IWebElement NewCertificateAddedCell => driver.FindElement(By.XPath("(//div[text()='Do you have any certifications?']/parent::div/following-sibling::div//table/tbody)[last()]//td[1]"));
        private IWebElement NewCertifiedFromAddedCell => driver.FindElement(By.XPath("(//div[text()='Do you have any certifications?']/parent::div/following-sibling::div//table/tbody)[last()]//td[2]"));
        private IWebElement NewYearAddedCell => driver.FindElement(By.XPath("(//div[text()='Do you have any certifications?']/parent::div/following-sibling::div//table/tbody)[last()]//td[3]"));
        private IWebElement MessageWindow => driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
        private IWebElement CloseMessageIcon => driver.FindElement(By.XPath("//*[@class='ns-close']"));
        private IWebElement UpdateButton => driver.FindElement(By.XPath("//*[@value='Update']"));
        private string Message = "";
        public void AddCertificate(CertificateModel certificate)
        {
            //----Adding Certificate------------
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Certifications']", 5);
            //Click on "Certifications" tab.
            CertificationsTab.Click();
            //Click on "Add New" button.
            Wait.WaitToBeClickable(driver, "XPath", "//*[text()='Certificate']/following-sibling::th[3]/div", 3);
            AddNewCertificateButton.Click();
            //Enter certificate, certified from and year details
            CertificateTextBox.SendKeys(certificate.certificate);
            CertifiedFromTextBox.SendKeys(certificate.certifiedFrom);
            SelectElement yearOption = new SelectElement(YearDropDown);
            yearOption.SelectByText(certificate.year);
            //Click on "Add" button.
            AddButton.Click();
            //Saving error or success message
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 5);
            Message = MessageWindow.Text;
            //If any message visible close it
            CloseMessageWindow();

            if (GetMessage() == "Please enter Certification Name, Certification From and Certification Year")
            {
                CancelButton.Click();
            }
        }
        public string GetCertificate()
        {
            //Return new added Certificate
            try
            {
                return NewCertificateAddedCell.Text;
            }
            catch (Exception ex)
            {
                return "Record not found";
            }
        }
        public string GetCertifiedFrom()
        {
            //Return new added Certificate from
            try
            {
                return NewCertifiedFromAddedCell.Text;
            }
            catch (Exception ex)
            {
                return "Record not found";
            }

        }
        public string GetYear()
        {
            //Return new added year
            try
            {
                return NewYearAddedCell.Text;
            }
            catch (Exception ex)
            {
                return "Record not found";
            }
        }
        public string GetMessage()
        {
            //Returning error or success message
            return Message;
        }
        public void CloseMessageWindow()
        {
            Wait.WaitToBeClickable(driver, "XPath", "//*[@class='ns-close']", 5);
            CloseMessageIcon.Click();
        }
        public void DeleteCertificate(CertificateModel certificate)
        {
            //---Deleting a Certificate-----
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Certifications']", 5);
            //Click on "Certifications" tab.
            CertificationsTab.Click();
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead/following-sibling::tbody/tr"));
            foreach (IWebElement row in rows)
            {
                // Get the values of fields in the row
                string certificateText = row.FindElement(By.XPath("./td[1]")).Text;
                string certificationFromText = row.FindElement(By.XPath("./td[2]")).Text;
                string yearText = row.FindElement(By.XPath("./td[3]")).Text;

                // Check if the values of fields matches the provided certificate
                if (certificateText.Equals(certificate.certificate, StringComparison.OrdinalIgnoreCase) && certificationFromText.Equals(certificate.certifiedFrom, StringComparison.OrdinalIgnoreCase) && yearText.Equals(certificate.year, StringComparison.OrdinalIgnoreCase))
                {
                    //Click on delete icon button of desired row
                    IWebElement certificateDeleteIcon = row.FindElement(By.XPath("./td[4]/span[2]/i"));
                    certificateDeleteIcon.Click();

                    //Saving error or success message
                    Thread.Sleep(1000);
                    Message = MessageWindow.Text;

                    //If any message visible close it
                    CloseMessageWindow();
                    break;
                }
            }
        }
        public string GetDeletedCertificateResult(CertificateModel certificate)
        {
            //---Return certificate deleted result-----      

            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Certifications']", 5);
            //Click on "Certifications" tab.
            CertificationsTab.Click();
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();
            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead/following-sibling::tbody/tr"));
            string result = "Deleted";
            foreach (IWebElement row in rows)
            {
                // Get the values of fields in the row
                string certificateText = row.FindElement(By.XPath("./td[1]")).Text;
                string certificationFromText = row.FindElement(By.XPath("./td[2]")).Text;
                string yearText = row.FindElement(By.XPath("./td[3]")).Text;

                // Check if the values of fields matches the provided certificate             
                if (certificateText.Equals(certificate.certificate, StringComparison.OrdinalIgnoreCase) && certificationFromText.Equals(certificate.certifiedFrom, StringComparison.OrdinalIgnoreCase) && yearText.Equals(certificate.year, StringComparison.OrdinalIgnoreCase))
                {
                    result = "Not Deleted";
                }
            }
            return result;
        }
        public void UpdateCertificate(CertificateModel certificate)
        {
            //---------updating a Certificate-----

            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Certifications']", 5);
            //Click on "Certifications" tab.
            CertificationsTab.Click();
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();
            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead/following-sibling::tbody/tr"));
            foreach (IWebElement row in rows)
            {
                // Get the text of the certificate field of the row
                string certificateText = row.FindElement(By.XPath("./td[1]")).Text;
                // Check if the values of certificate field matches the provided certificate
                if (certificateText.Equals(certificate.certificate))
                {
                    //Click on update icon button of desired row
                    IWebElement certificateUpdateIcon = row.FindElement(By.XPath("./td[4]/span[1]/i"));
                    certificateUpdateIcon.Click();
                    //Edit values
                    Wait.WaitToExist(driver, "XPath", "//*[@placeholder='Certificate or Award']", 5);
                    CertifiedFromTextBox.SendKeys(Keys.Control + "A");
                    CertifiedFromTextBox.SendKeys(Keys.Backspace);
                    CertifiedFromTextBox.SendKeys(certificate.certifiedFrom);
                    SelectElement certificateOption = new SelectElement(YearDropDown);
                    certificateOption.SelectByText(certificate.year);
                    //Click on "Update" button
                    UpdateButton.Click();
                    //Saving error or success message
                    Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
                    Message = MessageWindow.Text;
                    //if (GetMessage() == "Please enter Certification Name, Certification From and Certification Year")
                    //{
                    //    CancelButton.Click();
                    //}
                    //If any message visible close it
                    CloseMessageWindow();
                    break;
                }
            }
        }
        public string GetUpdatedCertificateResult(CertificateModel certificate)
        {
            //------Return certificate updated result---------
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Certifications']", 5);
            //Click on "Certifications" tab.
            CertificationsTab.Click();
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();
            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead/following-sibling::tbody/tr"));
            string result = "Not updated";
            foreach (IWebElement row in rows)
            {
                // Get the value of the certificate field of the row
                string certificateText = row.FindElement(By.XPath("./td[1]")).Text;
                // Check if the value of certificate field matches the provided certificate
                if (certificateText.Equals(certificate.certificate))
                {
                    // Get the value of the certifiedFrom and year fields in the row
                    string certificationFromText = row.FindElement(By.XPath("./td[2]")).Text;
                    string yearText = row.FindElement(By.XPath("./td[3]")).Text;
                    // Check if the value of the certifiedFrom and year fields matches the provided certificate's certifiedFrom and year
                    if (certificationFromText.Equals(certificate.certifiedFrom, StringComparison.OrdinalIgnoreCase) && yearText.Equals(certificate.year, StringComparison.OrdinalIgnoreCase))
                    {
                        result = "Updated";
                        break;
                    }
                }
            }
            return result;
        }

        public void UpdateInvalidCertificate(UpdateInvalidCertificateModel certificate)
        {
            //---------updating an invalid Certificate-----

            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Certifications']", 5);
            //Click on "Certifications" tab.
            CertificationsTab.Click();
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();
            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead/following-sibling::tbody/tr"));
            foreach (IWebElement row in rows)
            {
                // Get the value of the certificate field of the row
                string certificateText = row.FindElement(By.XPath("./td[1]")).Text;
                // Check if the value of certificate field matches wiith the provided oldCertificate
                if (certificateText == certificate.oldCertificate)
                {
                    //Click on update icon button of desired row
                    IWebElement certificateUpdateIcon = row.FindElement(By.XPath("./td[4]/span[1]/i"));
                    certificateUpdateIcon.Click();
                    //edit details 
                    Wait.WaitToExist(driver, "XPath", "//*[@placeholder='Certificate or Award']", 5);
                    CertificateTextBox.SendKeys(Keys.Control + "A");
                    CertificateTextBox.SendKeys(Keys.Backspace);
                    CertificateTextBox.SendKeys(certificate.newCertificate);
                    CertifiedFromTextBox.SendKeys(Keys.Control + "A");
                    CertifiedFromTextBox.SendKeys(Keys.Backspace);
                    CertifiedFromTextBox.SendKeys(certificate.newCertifiedFrom);
                    SelectElement certificateOption = new SelectElement(YearDropDown);
                    certificateOption.SelectByText(certificate.newYear);
                    //Click on "Update" button
                    UpdateButton.Click();
                    //Saving error or success message
                    Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
                    Message = MessageWindow.Text;

                    if (GetMessage() == "Please enter Certification Name, Certification From and Certification Year")
                    {
                        CancelButton.Click();
                    }

                    //If any message visible close it
                    CloseMessageWindow();
                    break;
                }
            }
        }
        public string GetUpdatedInvalidCertificateResult(UpdateInvalidCertificateModel certificate)
        {
            //----Return invalid certificate updated result-------
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Certifications']", 5);
            //Click on "Certifications" tab.
            CertificationsTab.Click();
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();
            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead/following-sibling::tbody/tr"));
            string result = "Not updated";
            foreach (IWebElement row in rows)
            {
                // Get the values of the certificate, certification from and year field in the row
                string certificateText = row.FindElement(By.XPath("./td[1]")).Text;
                string certificationFromText = row.FindElement(By.XPath("./td[2]")).Text;
                string yearText = row.FindElement(By.XPath("./td[3]")).Text;
                // Check if the certificate matches the provided certificate
                if (certificateText.Equals(certificate.newCertificate, StringComparison.OrdinalIgnoreCase) && certificationFromText.Equals(certificate.newCertifiedFrom, StringComparison.OrdinalIgnoreCase) && yearText.Equals(certificate.newYear, StringComparison.OrdinalIgnoreCase))
                {
                    result = "Updated";
                    break;
                }
            }
            return result;
        }

        public void CancelCertificateAddition(CertificateModel certificate)
        {
            //----Cancel adding certificate------------

            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Certifications']", 5);
            //Click on "Certifications" tab.
            CertificationsTab.Click();
            //Click on "Add New" button.
            Wait.WaitToBeClickable(driver, "XPath", "//*[text()='Certificate']/following-sibling::th[3]/div", 3);
            AddNewCertificateButton.Click();
            //Enter certificate, certified from and year details
            CertificateTextBox.SendKeys(certificate.certificate);
            CertifiedFromTextBox.SendKeys(certificate.certifiedFrom);
            SelectElement yearOption = new SelectElement(YearDropDown);
            yearOption.SelectByText(certificate.year);
            //Click on "Cancel" button
            CancelButton.Click();
        }
        public void CancelCertificateUpdation(CertificateModel certificate)
        {
            //----Cancel updating certificate------------
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Certifications']", 5);
            //Click on "Certifications" tab.
            CertificationsTab.Click();
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead/following-sibling::tbody/tr"));
            foreach (IWebElement row in rows)
            {
                // Get the value of the certificate field in the row
                IWebElement certificateElement = row.FindElement(By.XPath("./td[1]"));
                string certificateText = certificateElement.Text;
                // Check if the value of the certificate field matches the provided certificate value
                if (certificateText.Equals(certificate.certificate))
                {
                    //Click on update icon button of desired row
                    IWebElement certificateUpdateIcon = row.FindElement(By.XPath("./td[4]/span[1]/i"));
                    certificateUpdateIcon.Click();
                    //edit details
                    Wait.WaitToExist(driver, "XPath", "//*[@placeholder='Certificate or Award']", 5);
                    CertifiedFromTextBox.SendKeys(Keys.Control + "A");
                    CertifiedFromTextBox.SendKeys(Keys.Backspace);
                    CertifiedFromTextBox.SendKeys(certificate.certifiedFrom);
                    SelectElement certificateOption = new SelectElement(YearDropDown);
                    certificateOption.SelectByText(certificate.year);
                    //Click on "Cancel" button
                    CancelButton.Click();

                    break;
                }
            }
        }
        public void WaitForRowsToGetPopulated()
        {
            try
            {
                //wait for rows to get populated
                Wait.WaitToBeVisible(driver, "XPath", "//th[text()='Certificate']//ancestor::thead/following-sibling::tbody[last()]", 2);
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
        }
    }
}
