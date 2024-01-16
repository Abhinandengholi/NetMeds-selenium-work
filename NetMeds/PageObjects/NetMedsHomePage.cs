using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMedsNunit.PageObjects
{
    internal class NetMedsHomePage
    {
        IWebDriver driver;
        public NetMedsHomePage(IWebDriver? driver)
        {

            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        //Arrange
        [FindsBy(How = How.XPath, Using = "//span[@id ='delivery_details']")]
        public IWebElement? DelivryDropDown { get; set; }

        [FindsBy(How = How.ClassName, Using = "inp_text")]
        public IWebElement? SearchPincode { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id ='search']")]
        [CacheLookup]
        public IWebElement? Searchtext { get; set; }
        [FindsBy(How = How.XPath, Using = "(//a[text()='Lab Tests'][1])")]
        public IWebElement? LabTestLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Face Makeup']")]
        public IWebElement? FaceMakeup { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[2]//div[1]//div[2]//div[4]//div[1]//ul[1]//li[6]//a[1]")]
        public IWebElement? BathAndShowerLink { get; set; }

        [FindsBy(How = How.ClassName, Using = "new_offers_icon")]
        public IWebElement? OffersLink { get; set; }

        //Act
        public void TypePincode(string searchpin)
        {
            DelivryDropDown?.Click();
            SearchPincode?.SendKeys(searchpin);

        }
        public ProductList TypeSearch(string searchTerm)
        {
            Searchtext?.SendKeys(searchTerm);

            Searchtext?.SendKeys(Keys.Enter);
            return new ProductList(driver);

        }
        public BookingPage LabTestLinkClick()
        {

            LabTestLink?.Click();
            return new BookingPage(driver);
        }

        public void FaceMakeUpClick()
        {
            FaceMakeup?.Click();
        }

        public void BathAndShowerLinkClick()
        {
            BathAndShowerLink?.Click();
        }

        public void OffersLinkClick()
        {
            OffersLink?.Click();
        }


    }
}
