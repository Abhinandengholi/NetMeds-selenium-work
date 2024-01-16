using NetMedsNunit.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMedsNunit.PageObjects
{
    internal class ProductPage
    {
        IWebDriver driver;
        public ProductPage(IWebDriver? driver)
        {

            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }
        //span[text()='ADD TO CART']//parent::button[@type='submit']
        //[FindsBy(How = How.XPath, Using = "//button[@class='toCart cartbag addtocartbtnpdp prodbtn'][1]")]
        [FindsBy(How = How.XPath, Using = "//span[text()='ADD TO CART']//parent::button[@type='submit']")] 
        public IWebElement? AddToCartButton { get; set; }
        [FindsBy(How = How.Id, Using = "minicart_btn")]
        public IWebElement? GoToCartButton { get; set; }


        public void AddToCartButtonClick()
        {
            var fluentWait = CoreCodes.Wait(driver);
            CoreCodes.ScrollIntoView(driver, driver.FindElement(By.XPath("//span[@class='pin-code-f']")));

        

            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(AddToCartButton));

            fluentWait.Until(ExpectedConditions.ElementToBeClickable(AddToCartButton));
            AddToCartButton?.Click();
        }
        public CheckoutPage GoToCartButtonClick()
        {
            GoToCartButton?.Click();
            return new CheckoutPage(driver);
        }

    }
}