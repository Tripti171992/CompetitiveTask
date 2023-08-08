using Mars.Model;
using Mars.TestData;
using Mars.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars.Pages
{
    public class Education : CommonDriver
    {
        private IWebElement EducationTab => driver.FindElement(By.XPath("//a[text()='Education']"));
        private IWebElement AddNewEducationButton => driver.FindElement(By.XPath("//*[text()='Country']/following-sibling::th[5]/div"));
        private IWebElement UniversityTextBox => driver.FindElement(By.XPath("//input[@placeholder='College/University Name']"));
        private IWebElement DegreeTextBox => driver.FindElement(By.XPath("//input[@placeholder='Degree']"));
        private IWebElement CountryDropDown => driver.FindElement(By.XPath("//select[@name='country']"));
        private IWebElement TitleDropDown => driver.FindElement(By.XPath("//select[@name='title']"));
        private IWebElement GraduationYearDropDown => driver.FindElement(By.XPath("//select[@name='yearOfGraduation']"));
        private IWebElement AddButton => driver.FindElement(By.XPath("//input[@value='Add']"));
        private IWebElement CancelButton => driver.FindElement(By.XPath("//*[@value='Cancel']"));
        private IWebElement NewCountryAddedCell => driver.FindElement(By.XPath("(//div[text()='Did you attend a college or university?']/parent::div/following-sibling::div//table/tbody)[last()]//td[1]"));
        private IWebElement NewUniversityAddedCell => driver.FindElement(By.XPath("(//div[text()='Did you attend a college or university?']/parent::div/following-sibling::div//table/tbody)[last()]//td[2]"));
        private IWebElement NewTitleAddedCell => driver.FindElement(By.XPath("(//div[text()='Did you attend a college or university?']/parent::div/following-sibling::div//table/tbody)[last()]//td[3]"));
        private IWebElement NewDegreeAddedCell => driver.FindElement(By.XPath("(//div[text()='Did you attend a college or university?']/parent::div/following-sibling::div//table/tbody)[last()]//td[4]"));
        private IWebElement NewGraduationYearAddedCell => driver.FindElement(By.XPath("(//div[text()='Did you attend a college or university?']/parent::div/following-sibling::div//table/tbody)[last()]//td[5]"));
        private IWebElement MessageWindow => driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
        private IWebElement CloseMessageIcon => driver.FindElement(By.XPath("//*[@class='ns-close']"));
        private IWebElement UpdateButton => driver.FindElement(By.XPath("//*[@value='Update']"));
        private string Message = "";
        public void AddEducation(EducationModel education)
        {
            //----Adding Education------------
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Education']", 5);
            //Click on "Education" tab.
            EducationTab.Click();
            //Click on "Add New" button.
            Wait.WaitToBeClickable(driver, "XPath", "//*[text()='Country']/following-sibling::th[5]/div", 3);
            AddNewEducationButton.Click();
            //Enter record details
            UniversityTextBox.SendKeys(education.university);
            DegreeTextBox.SendKeys(education.degree);
            SelectElement countryOption = new SelectElement(CountryDropDown);
            countryOption.SelectByText(education.country);
            SelectElement titleOption = new SelectElement(TitleDropDown);
            titleOption.SelectByText(education.title);
            SelectElement graduationYearOption = new SelectElement(GraduationYearDropDown);
            graduationYearOption.SelectByText(education.graduationYear);
            //Click on "Add" button.
            AddButton.Click();

            //Saving error or success message
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 5);
            Message = MessageWindow.Text;

            //If any message visible close it
            CloseMessageWindow();

            if (GetMessage() == "Please enter all the fields")
            {
                CancelButton.Click();
            }
        }
        public string GetCountry()
        {
            //Return new added country
            try
            {
                return NewCountryAddedCell.Text;
            }
            catch (Exception ex)
            {
                return "Record not found";
            }
        }
        public string GetUniversity()
        {
            //Return new added university
            try
            {
                return NewUniversityAddedCell.Text;
            }
            catch (Exception ex)
            {
                return "Record not found";
            }

        }
        public string GetTitle()
        {
            //Return new added title
            try
            {
                return NewTitleAddedCell.Text;
            }
            catch (Exception ex)
            {
                return "Record not found";
            }
        }
        public string GetDegree()
        {
            //Return new added degree
            try
            {
                return NewDegreeAddedCell.Text;
            }
            catch (Exception ex)
            {
                return "Record not found";
            }
        }
        public string GetGraduationYear()
        {
            //Return new added graduation year
            try
            {
                return NewGraduationYearAddedCell.Text;
            }
            catch (Exception ex)
            {
                return "Record not found";
            }
        }
        public void DeleteEducation(EducationModel education)
        {
            //---Deleting an education-----
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Education']", 5);
            //Click on "Education" tab.
            EducationTab.Click();
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead/following-sibling::tbody/tr"));
            foreach (IWebElement row in rows)
            {
                // Get the values of fields in the row
                string countryText = row.FindElement(By.XPath("./td[1]")).Text;
                string universityText = row.FindElement(By.XPath("./td[2]")).Text;
                string titleText = row.FindElement(By.XPath("./td[3]")).Text;
                string degreeText = row.FindElement(By.XPath("./td[4]")).Text;
                string graduationYearText = row.FindElement(By.XPath("./td[5]")).Text;

                // Check if the values of fields matches the provided education
                if (countryText.Equals(education.country) && universityText.Equals(education.university) && titleText.Equals(education.title))
                {
                    if (degreeText.Equals(education.degree) && graduationYearText.Equals(education.graduationYear))

                    {
                        //Click on delete icon button of desired row
                        IWebElement educationDeleteIcon = row.FindElement(By.XPath("./td[6]/span[2]/i"));
                        educationDeleteIcon.Click();

                        //Saving error or success message
                        Thread.Sleep(1000);
                        Message = MessageWindow.Text;

                        //If any message visible close it
                        CloseMessageWindow();
                        break;
                    }
                }
            }
        }
        public string GetDeletedEducationResult(EducationModel education)
        {
            //---Return education deleted result-----      
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Education']", 5);
            //Click on "Education" tab.
            EducationTab.Click();
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();
            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead/following-sibling::tbody/tr"));
            string result = "Deleted";
            foreach (IWebElement row in rows)
            {
                // Get the values of fields in the row
                string countryText = row.FindElement(By.XPath("./td[1]")).Text;
                string universityText = row.FindElement(By.XPath("./td[2]")).Text;
                string titleText = row.FindElement(By.XPath("./td[3]")).Text;
                string degreeText = row.FindElement(By.XPath("./td[4]")).Text;
                string graduationYearText = row.FindElement(By.XPath("./td[5]")).Text;

                // Check if the values of fields matches the provided education
                if (countryText.Equals(education.country) && universityText.Equals(education.university) && titleText.Equals(education.title))
                {
                    if (degreeText.Equals(education.degree) && graduationYearText.Equals(education.graduationYear))
                    {
                        result = "Not Deleted";
                    }
                }
            }
            return result;
        }
        public void UpdateEducation(EducationModel education)
        {
            //---------updating a education-----
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Education']", 5);
            //Click on "Education" tab.
            EducationTab.Click();
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();
            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead/following-sibling::tbody/tr"));
            foreach (IWebElement row in rows)
            {
                // Get the value of the education field of the row
                string titleText = row.FindElement(By.XPath("./td[3]")).Text;
                // Check if the title matches the provided education title
                if (titleText.Equals(education.title))
                {
                    //Click on update icon button of desired row
                    IWebElement educationUpdateIcon = row.FindElement(By.XPath("./td[6]/span[1]/i"));
                    educationUpdateIcon.Click();
                    //Edit values of the row
                    Wait.WaitToExist(driver, "XPath", "//input[@placeholder='College/University Name']", 5);
                    UniversityTextBox.SendKeys(Keys.Control + "A");
                    UniversityTextBox.SendKeys(Keys.Backspace);
                    UniversityTextBox.SendKeys(education.university);
                    DegreeTextBox.SendKeys(Keys.Control + "A");
                    DegreeTextBox.SendKeys(Keys.Backspace);
                    DegreeTextBox.SendKeys(education.degree);
                    SelectElement countryOption = new SelectElement(CountryDropDown);
                    countryOption.SelectByText(education.country);
                    SelectElement graduationYearOption = new SelectElement(GraduationYearDropDown);
                    graduationYearOption.SelectByText(education.graduationYear);
                    //Click on "Update" button
                    UpdateButton.Click();
                    //Saving error or success message
                    Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
                    Message = MessageWindow.Text;
                    //if (GetMessage() == "Please enter all the fields")
                    //{
                    //    CancelButton.Click();
                    //}
                    //If any message visible close it
                    CloseMessageWindow();
                    break;
                }
            }
        }
        public string GetUpdatedEducationResult(EducationModel education)
        {
            //------Return education updated result---------
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Education']", 5);
            //Click on "Education" tab.
            EducationTab.Click();
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();
            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead/following-sibling::tbody/tr"));
            string result = "Not updated";
            foreach (IWebElement row in rows)
            {
                // Get the value of the title field of the row
                string titleText = row.FindElement(By.XPath("./td[3]")).Text;
                // Check if the title matches the provided education title
                if (titleText.Equals(education.title))
                {
                    // Get the values of the existing education details in the row
                    string countryText = row.FindElement(By.XPath("./td[1]")).Text;
                    string universityText = row.FindElement(By.XPath("./td[2]")).Text;
                    string degreeText = row.FindElement(By.XPath("./td[4]")).Text;
                    string graduationYearText = row.FindElement(By.XPath("./td[5]")).Text;
                    // Check if the education matches the provided education
                    if (countryText.Equals(education.country) && universityText.Equals(education.university))
                    {
                        if (degreeText.Equals(education.degree) && graduationYearText.Equals(education.graduationYear))
                        {
                            result = "Updated";
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public void CancelEducationAddition(EducationModel education)
        {
            //----Adding Education------------
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Education']", 5);
            //Click on "Education" tab.
            EducationTab.Click();
            //Click on "Add New" button.
            Wait.WaitToBeClickable(driver, "XPath", "//*[text()='Country']/following-sibling::th[5]/div", 3);
            AddNewEducationButton.Click();
            //Enter record details
            UniversityTextBox.SendKeys(education.university);
            DegreeTextBox.SendKeys(education.degree);
            SelectElement countryOption = new SelectElement(CountryDropDown);
            countryOption.SelectByText(education.country);
            SelectElement titleOption = new SelectElement(TitleDropDown);
            titleOption.SelectByText(education.title);
            SelectElement graduationYearOption = new SelectElement(GraduationYearDropDown);
            graduationYearOption.SelectByText(education.graduationYear);
            //Click on "Cancel" button
            CancelButton.Click();
        }
        public void CancelEducationUpdation(EducationModel education)
        {
            //----Cancel updating education------------
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Education']", 5);
            //Click on "Education" tab.
            EducationTab.Click();
            //wait for rows to get populated
            Wait.WaitToBeVisible(driver, "XPath", "//th[text()='Country']//ancestor::thead/following-sibling::tbody[last()]", 2);
            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead/following-sibling::tbody/tr"));
            foreach (IWebElement row in rows)
            {
                // Get the text of the education details of the row
                string titleText = row.FindElement(By.XPath("./td[3]")).Text;
                // Check if the education title matches the provided education title
                if (titleText.Equals(education.title))
                {
                    //Click on update icon button of desired row
                    IWebElement educationUpdateIcon = row.FindElement(By.XPath("./td[6]/span[1]/i"));
                    educationUpdateIcon.Click();
                    //Edit values of the row
                    Wait.WaitToExist(driver, "XPath", "//input[@placeholder='College/University Name']", 5);
                    UniversityTextBox.SendKeys(Keys.Control + "A");
                    UniversityTextBox.SendKeys(Keys.Backspace);
                    UniversityTextBox.SendKeys(education.university);
                    DegreeTextBox.SendKeys(Keys.Control + "A");
                    DegreeTextBox.SendKeys(Keys.Backspace);
                    DegreeTextBox.SendKeys(education.degree);
                    SelectElement countryOption = new SelectElement(CountryDropDown);
                    countryOption.SelectByText(education.country);
                    SelectElement graduationYearOption = new SelectElement(GraduationYearDropDown);
                    graduationYearOption.SelectByText(education.graduationYear);
                    //Click on "Cancel" button
                    CancelButton.Click();

                    break;
                }
            }
        }
        public void UpdateInvalidEducation(UpdateInvalidEducationModel education)
        {
            //---------updating an invalid education-----
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Education']", 5);
            //Click on "Education" tab.
            EducationTab.Click();
            //Wait.WaitToExist(driver, "XPath", "//th[text()='Education']//ancestor::thead/following-sibling::tbody[last()]", 6);
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();
            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead/following-sibling::tbody/tr"));
            foreach (IWebElement row in rows)
            {
                // Get the text of the education details of the row
                string titleText = row.FindElement(By.XPath("./td[3]")).Text;
                // Check if the education title matches the provided old education title
                if (titleText.Equals(education.oldTitle))
                {
                    //Click on update icon button of desired row
                    IWebElement educationUpdateIcon = row.FindElement(By.XPath("./td[6]/span[1]/i"));
                    educationUpdateIcon.Click();
                    //Edit values of the row
                    Wait.WaitToExist(driver, "XPath", "//input[@placeholder='College/University Name']", 5);
                    UniversityTextBox.SendKeys(Keys.Control + "A");
                    UniversityTextBox.SendKeys(Keys.Backspace);
                    UniversityTextBox.SendKeys(education.university);
                    DegreeTextBox.SendKeys(Keys.Control + "A");
                    DegreeTextBox.SendKeys(Keys.Backspace);
                    DegreeTextBox.SendKeys(education.degree);
                    SelectElement titleOption = new SelectElement(TitleDropDown);
                    titleOption.SelectByText(education.title);
                    SelectElement countryOption = new SelectElement(CountryDropDown);
                    countryOption.SelectByText(education.country);
                    SelectElement graduationYearOption = new SelectElement(GraduationYearDropDown);
                    graduationYearOption.SelectByText(education.graduationYear);
                    //Click on "Update" button
                    UpdateButton.Click();
                    //Saving error or success message
                    Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
                    Message = MessageWindow.Text;
                    if (GetMessage() == "Please enter all the fields")
                    {
                        CancelButton.Click();
                    }
                    //If any message visible close it
                    CloseMessageWindow();
                    break;
                }
            }
        }
        public string GetUpdatedInvalidEducationResult(UpdateInvalidEducationModel education)
        {
            //------Return invalid education updated result---------
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Education']", 5);
            //Click on "Education" tab.
            EducationTab.Click();
            //Wait for rows to get populated
            WaitForRowsToGetPopulated();
            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead/following-sibling::tbody/tr"));
            string result = "Not updated";
            foreach (IWebElement row in rows)
            {
                // Get the text of the education title of the row
                string countryText = row.FindElement(By.XPath("./td[1]")).Text;
                string universityText = row.FindElement(By.XPath("./td[2]")).Text;
                string titleText = row.FindElement(By.XPath("./td[3]")).Text;
                string degreeText = row.FindElement(By.XPath("./td[4]")).Text;
                string graduationYearText = row.FindElement(By.XPath("./td[5]")).Text;
                // Check if the education matches the provided education
                if (titleText.Equals(education.title) && countryText.Equals(education.country) && universityText.Equals(education.university))
                {
                    if (degreeText.Equals(education.degree) && graduationYearText.Equals(education.graduationYear))
                    {
                        result = "Updated";
                        break;
                    }
                }
            }
            return result;
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
        public void WaitForRowsToGetPopulated()
        {
            try
            {
                //wait for rows to get populated
                Wait.WaitToBeVisible(driver, "XPath", "//th[text()='Country']//ancestor::thead/following-sibling::tbody[last()]", 2);
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
        }
    }
}
