using NetMedsNunit.Helper;
using NetMedsNunit.PageObjects;
using NetMedsNunit.Utilities;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace NetMedsNunit.TestScripts
{
    internal class BookiAppointmentTest : CoreCodes
    {
        [Test, Order(1), Category("Regression Test")]

        public void LabBookingTest()
        {

            var homePage = new NetMedsHomePage(driver);
            DefaultWait<IWebDriver> fluentWait = new(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "Element not found";
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/TestData/InputData.xlsx";
            string? sheetName = "Netmeds";

            List<SearchData> searchDataList = ExcelUtils.ReadSignUpData(excelFilePath, sheetName);
            try
            {
                foreach (var searchData in searchDataList)
                {
                    string? bookingname = searchData?.BookingName;
                    string? mobile = searchData?.BookingMobNo;
                    string? getPincode = searchData?.Pincode;

                    var bookingPage = homePage.LabTestLinkClick();
                    Log.Information("Lab Test Link clicked");
                    Assert.That(driver.Url.Contains("health-packages"));

                    bookingPage.BookingDetails(bookingname, mobile, getPincode);
                    //Thread.Sleep(2000);
                    LogTestResult("Add to cart test", "Add to cart failed");
                    test = extent.CreateTest("Book Appointment test - Pass");
                    test.Pass("Book appointment test passed");
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
