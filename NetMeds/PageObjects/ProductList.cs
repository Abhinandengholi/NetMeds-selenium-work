using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMedsNunit.PageObjects
{
    internal class ProductList
    {
        IWebDriver driver;
        public ProductList(IWebDriver? driver)
        {

            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//div[@class='logo']")]
        public IWebElement? LogoButton { get; set; }


        [FindsBy(How = How.XPath, Using = "//div[@Id='sort_container']//following::button[contains(text(),'Discount')]")]

        public IWebElement? Discount { get; set; }
        //input[@type='checkbox'])[2]
        [FindsBy(How = How.XPath, Using = " (//input[@type='checkbox'])[2]")]
        public IWebElement? Checkbox { get; set; }





        public ProductPage ProductSelectClick(string position)
        {
            DefaultWait<IWebDriver> fluentWait = new(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "Element not found";
            int intposition = Convert.ToInt32(position);
            intposition--;
            fluentWait.Until(d => driver.FindElement(By.XPath("//li[@class='ais-InfiniteHits-item'][" + intposition.ToString() + "]")).Displayed == true);
            driver.FindElement(By.XPath("//li[@class='ais-InfiniteHits-item'][" + intposition.ToString() + "]")).Click();
            return new ProductPage(driver);
        }
        public void LogoButtonClik()
        {
            LogoButton?.Click();
        }

        public void SortByDiscountButtonClick()
        {
            Discount?.Click();
        }
        public void CheckboxClick()
        {
            Checkbox?.Click();
        }
    }
}