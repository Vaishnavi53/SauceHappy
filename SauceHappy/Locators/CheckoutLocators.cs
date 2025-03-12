using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SauceHappy.Locators
{
    public class CheckoutLocators
    {

        public static By checkoutbtn = By.XPath("//button[@id='checkout']");
        public static By itemincart = By.XPath("//div[@class='inventory_item_name']");
        public static By checkOutFirstName = By.XPath("//input[@id='first-name']");
        public static By checkOutLastName = By.XPath("//input[@id='last-name']");
        public static By checkOutPostalCode = By.XPath("//input[@id='postal-code']");
        public static By infotitle = By.XPath("//span[@class='title']");
        public static By continueCheckOut = By.XPath("//input[@id='continue']");
        public static By finishButton = By.XPath("//button[@id='finish']");
    }
}
