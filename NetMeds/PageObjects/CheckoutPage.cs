using NetMedsNunit.Utilities;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMedsNunit.PageObjects
{
    internal class CheckoutPage
    {
        IWebDriver driver;
        public CheckoutPage(IWebDriver? driver)
        {

            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//button[text()='Proceed']")]
        public IWebElement? CheckoutButton { get; set; }

        public SignupPage CheckoutButtonClick()
        {

            CoreCodes.ScrollIntoView(driver, CheckoutButton);
            CheckoutButton.Click();
            return new SignupPage(driver);
        }
    }
}