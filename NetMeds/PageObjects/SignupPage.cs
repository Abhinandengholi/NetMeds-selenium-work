using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMedsNunit.PageObjects
{
    internal class SignupPage
    {
        IWebDriver driver;
        public SignupPage(IWebDriver? driver)
        {

            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.Id, Using = "loginfirst_mobileno")]
        public IWebElement? PhoneNoInput { get; set; }

        public void EnterMobileNo()
        {
            PhoneNoInput.SendKeys("1234566778");
            PhoneNoInput.SendKeys(Keys.Enter);
        }
    }
}