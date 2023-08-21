using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mars.Utilities;
using Mars.TestData;

namespace Mars.Pages
{
    public class HomePage : CommonDriver
    {
        private IWebElement SignInButton => driver.FindElement(By.XPath("//*[text()='Sign In']"));
        private IWebElement EmailTextbox => driver.FindElement(By.XPath("//*[@placeholder='Email address']"));
        private IWebElement PasswordTextbox => driver.FindElement(By.XPath("//*[@placeholder='Password']"));
        private IWebElement LoginButton => driver.FindElement(By.XPath("//*[text()='Login']"));
        private IWebElement UserNameLable => driver.FindElement(By.XPath("//a[text()=' Chat']/following-sibling::span"));
        public void SignIn(UserInformation user)
        {
            //------Signing in into Mars--------

            //Click on "Sign In" button
            Wait.WaitToBeClickable(driver, "XPath", "//*[text()='Sign In']", 4);
            SignInButton.Click();
            //Enter the valid email address and password.
            EmailTextbox.SendKeys(user.email);
            PasswordTextbox.SendKeys(user.password);
            //Click on "Login" button
            LoginButton.Click();
            Thread.Sleep(3000);
        }
        public void SignInInvalid(UserInformation user)
        {
            //------Signing in into Mars with invalid credentials--------

            //Click on "Sign In" button
            Wait.WaitToBeClickable(driver, "XPath", "//*[text()='Sign In']", 4);
            SignInButton.Click();
            //Enter the invalid email address or/and password.
            EmailTextbox.SendKeys(user.email);
            PasswordTextbox.SendKeys(user.password);
            //Click on "Login" button
            LoginButton.Click();
            Thread.Sleep(2000);
        }
        public string GetFirstName()
        {
            //Return username
            try
            {
                return UserNameLable.Text;
            }
            catch (Exception ex)
            {
                driver.Navigate().Refresh();
                return ex.Message;
            }
        }

    }
}
