using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SauceHappy.Locators
{
    public class LoginPageLocators
    {
        public static By usernameField = By.XPath("//input[@id='user-name']");
        public static By passwordField = By.XPath("//input[@id='password']");
        public static By loginFormLocator = By.XPath("//input[@id='login-button']");
        public static By homepagedisplayed = By.XPath("//div[@class='app_logo']");
    }
}
