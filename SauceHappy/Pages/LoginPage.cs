using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SauceHappy.Locators;
using OpenQA.Selenium.Support.UI;

namespace SauceHappy.Pages
{
        public class LoginPage
        {

            private IWebDriver driver;

            public LoginPage(IWebDriver driver)
            {

                this.driver = driver;
            }

            // locators on the login page

            //By usernameField = By.XPath("//input[@id='user-name']");
            //By passwordField = By.XPath("//input[@id='password']");
            //By loginFormLocator = By.XPath("//input[@id='login-button']");
            //By homepagedisplayed = By.XPath("//div[@class='app_logo']");



            // laucnh browser

            public void launchbrowser()
            {
                driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            }

            // enter username and password

            public void enterusernamepass(String username, String password)
            {
                
                driver.FindElement(LoginPageLocators.usernameField).SendKeys(username);
                driver.FindElement(LoginPageLocators.passwordField).SendKeys(password);

            }

            // submit method

            public void submit()
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                IWebElement product = wait.Until(drv => drv.FindElement(LoginPageLocators.loginFormLocator));
                product.Click();

                //driver.FindElement(loginFormLocator).Click();

            }

            // home page is displayed

            public void homepagedisplay()
            {


                IWebElement homepage = driver.FindElement(LoginPageLocators.homepagedisplayed);

                if (homepage.Displayed)
                {
                    Console.WriteLine("Home page is displayed");
                }
                else
                {

                    Console.WriteLine("Home page is not displayed");
                }
            }


        }
    }
