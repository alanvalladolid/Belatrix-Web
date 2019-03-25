using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ebay.Automation.E2ETests
{
   [TestClass]
    public class EbaySearchProductTests
    {
        private TestContext testContext;
        private IWebDriver driver;
        private string url;

        public TestContext TestContext
        {
            get
            {
                return testContext;
            }

            set
            {
                testContext = value;
            }
        }

        [TestInitialize]
        public void SetupTest()
        {
            url = "http://www.ebay.com";
            driver = new ChromeDriver();
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void PrintNumberOfSearchResults()
        {
            //Go To Ebay.com
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(3000);

            //Search for shoes
            driver.FindElement(By.Id("gh-ac")).SendKeys("shoes");
            Thread.Sleep(3000);
            driver.FindElement(By.Id("gh-btn")).Click();
            Thread.Sleep(3000);

            //Select brand PUMA
            driver.FindElement(By.XPath("//*[@id='w3-w0-w2-w2-w6']/a")).Click();
            Thread.Sleep(3000);

            //Select size 10
            driver.FindElement(By.XPath("//*[@id='w3-w0-w2-multiselect[4]']/a")).Click();
            Thread.Sleep(3000);

            //Get number of results
            var searchResultNumber = driver.FindElement(By.XPath("//*[@id='w4']/div[2]/div/div[1]/h1")).Text;
            Thread.Sleep(3000);
            TestContext.WriteLine(searchResultNumber);
            Assert.IsTrue(searchResultNumber.Contains("resultados"));
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void PrintPriceAscendant()
        {
            //Go To Ebay.com
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(3000);


            driver.FindElement(By.Id("gh-ac")).SendKeys("shoes");
            Thread.Sleep(3000);
            driver.FindElement(By.Id("gh-btn")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//*[@id='w3-w0-w2-w2-w6']/a")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//*[@id='w3-w0-w2-multiselect[4]']/a")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//*[@id='w4-w1']/button/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//*[@id='w4-w1']/button/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//*[@id='w4-w1']/div/div/ul/li[4]/a")).Click();           
            Thread.Sleep(3000);

            var listResults = driver.FindElements(By.XPath("//*[contains(@id,'srp-river-results-listing')]/div/div[2]"));

            var products = new List<string[]>();

            for (var i=0; i < 5; i++ )
            {
                var item = listResults[i];
                var line = item.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                products.Add(line);
            }

            foreach (var item in products)
            {
                TestContext.WriteLine(item[0]);
                TestContext.WriteLine(item[1]);
            }

            
        }
    }

    
}
