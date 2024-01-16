using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMedsNunit.Utilities
{
    internal class CoreCodes
    {
        Dictionary<string, string>? properties;
        public IWebDriver driver;
        public ExtentReports extent;
        ExtentSparkReporter sparkReporter;
        public ExtentTest test;

        public void ReadConfigSettings()
        {
            string currDir = Directory.GetParent(@"../../../").FullName;
            string logfilepath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyyy-MM-dd_hh-mm-ss") + ".txt";
            Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day).CreateLogger();
            properties = new Dictionary<string, string>();
            string fileName = currDir + "/ConfigSettings/config.properties";
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                {
                    string[] parts = line.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    properties[key] = value;
                }
            }
        }
        [OneTimeSetUp]
        public void InitializeBrowser()
        {

            string currDir = Directory.GetParent(@"../../../").FullName;
            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currDir + "/ExtentReports/extent-report"
                + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");

            extent.AttachReporter(sparkReporter);
            ReadConfigSettings();
            if (properties["browser"].ToLower() == "chrome")
            {
                driver = new ChromeDriver();
            }
            else if (properties["browser"].ToLower() == "edge")
            {
                driver = new EdgeDriver();
            }
            driver.Url = properties["baseUrl"];
            driver.Manage().Window.Maximize();


        }


        public bool CheckLinkStatus(string url)
        {
            try
            {
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = "Head";
                using (var response = request.GetResponse())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static DefaultWait<IWebDriver> Wait(IWebDriver driver)
        {
            DefaultWait<IWebDriver> fluentWait = new(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(15);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));

            fluentWait.Message = "Element not found";
            return fluentWait;
        }


        public void TakeScreenshot()
        {
            ITakesScreenshot its = (ITakesScreenshot)driver;
            Screenshot ss = its.GetScreenshot();
            string currDir = Directory.GetParent(@"../../../").FullName;
            string filepath = currDir + "/Screenshots/ss_" + DateTime.Now.ToString("yyyyy-MM-dd_hh-mm-ss") + ".png";
            ss.SaveAsFile(filepath);
        }
        public static void ScrollIntoView(IWebDriver driver, IWebElement element)

        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        protected void LogTestResult(string testName, string result, string errorMessage = null)
        {
            Log.Information(result);
            test = extent.CreateTest(testName);
            if (errorMessage == null)
            {
                Log.Information(testName + "passed");
                test.Pass(result);
            }
            else
            {
                Log.Error($"Test failed for{testName}. \n Exception: \n {errorMessage}");

                test.Fail(result);

            }
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            driver.Quit();
            extent.Flush();

        }
    }
}