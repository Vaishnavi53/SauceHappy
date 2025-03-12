using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SauceHappy.Locators
{
   public class AddtocartLocators
    {
        public static By clickproduct = By.XPath("//div[normalize-space()='Sauce Labs Backpack']");
        public static By addtocartbtn = By.XPath("//button[contains(@id, 'add-to-cart')]");

        public static By carticon = By.XPath("//a[@class='shopping_cart_link']");

    }
}
