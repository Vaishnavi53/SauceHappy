using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SauceHappy.Locators;

namespace SauceHappy.Pages
{
        public class Addtocart
        {
            private IWebDriver driver;

            public Addtocart(IWebDriver driver)
            {

                this.driver = driver;
            }
            //locators on addtocart page

            //By clickproduct = By.XPath("//div[normalize-space()='Sauce Labs Backpack']");
            //By addtocartbtn = By.XPath("//button[contains(@id, 'add-to-cart')]");

            //By carticon = By.XPath("//a[@class='shopping_cart_link']");


            public void clickproductwanted()
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                IWebElement product = wait.Until(drv => drv.FindElement(AddtocartLocators.clickproduct));
                product.Click();
                //Thread.Sleep(3000);
                //driver.FindElement(clickproduct).Click();

                Thread.Sleep(2000);
            }
            public void AddToCart()
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                IWebElement product = wait.Until(drv => drv.FindElement(AddtocartLocators.addtocartbtn));
                product.Click();
                //driver.FindElement(addtocartbtn).Click();
                //Thread.Sleep(2000);
            }
            public void clickcart()
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                IWebElement product = wait.Until(drv => drv.FindElement(AddtocartLocators.carticon));
                product.Click();
                //driver.FindElement(carticon).Click();
                //Thread.Sleep(2000);
            }
            public void WaitForProductPageToLoad()
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                wait.Until(driver => driver.Url.Contains("inventory.html")); // ✅ No need for ExpectedConditions
                Console.WriteLine("✅ Product page loaded successfully.");
            }


        }
    }
