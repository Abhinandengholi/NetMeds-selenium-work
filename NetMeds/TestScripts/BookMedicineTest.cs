using NetMedsNunit.Helper;
using NetMedsNunit.PageObjects;
using NetMedsNunit.Utilities;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using Serilog;

namespace NetMedsNunit.TestScripts
{
    [TestFixture]
    internal class BookMedicineTest : CoreCodes
    {
        [Test, Order(1), Category("Regression Test")]

        public void AddToCartE2ETest()
        {

            var homePage = new NetMedsHomePage(driver);
            var fluentWait = Wait(driver);
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/TestData/InputData.xlsx";
            string? sheetName = "Netmeds";

            List<SearchData> searchDataList = ExcelUtils.ReadSignUpData(excelFilePath, sheetName);
            try
            {
                foreach (var searchData in searchDataList)
                {
                    string? getPincode = searchData.Pincode;
                    homePage.TypePincode(getPincode);
                    //fluentWait.Until(d => d.FindElement(By.XPath("//span[@id='delivery_details']"))).Equals(getPincode);
                    Thread.Sleep(500);
                    fluentWait.Until(ExpectedConditions.ElementToBeClickable(homePage.DelivryDropDown));
                    string? getSearchText = fluentWait.Until(d => searchData.searchText);
                    var productlist = homePage.TypeSearch(getSearchText);
                    //Log.Information(driver.Url);
                    TakeScreenshot();
                    string? productposition = searchData.SearchPosition;
                    var productpage = productlist.ProductSelectClick(productposition);
                    Log.Information("clicked a product");
                    TakeScreenshot();
                    Thread.Sleep(1000);
                    fluentWait.Until(ExpectedConditions.ElementToBeClickable(productpage.AddToCartButton));
                    productpage.AddToCartButtonClick();
                    Log.Information("Add to cart button clicked");
                    var checkoutpage = productpage.GoToCartButtonClick();
                    Thread.Sleep(2000);
                    Log.Information("Go to cart button clicked");
                    TakeScreenshot();
                    fluentWait.Until(ExpectedConditions.ElementToBeClickable(checkoutpage.CheckoutButton));
                    var signuppage = checkoutpage.CheckoutButtonClick();
                    signuppage.EnterMobileNo();
                    Log.Information("Proceed button clicked");
                    TakeScreenshot();
                    Assert.That(driver.Url.Contains("account"));
                    LogTestResult("Add to cart E2E test", "Add to cart E2E test- passed");
                    test = extent.CreateTest("Add to cart Test - Pass");
                    test.Pass("Add to cart link failed");
                }
            }
            catch (AssertionException ex)
            {
                LogTestResult("Add to cart test", "Add to cart failed", ex.Message);
                test = extent.CreateTest("Add to cart Test - Fail");
                test.Fail("Add to cart Link failed");

            }

        }

    }

}