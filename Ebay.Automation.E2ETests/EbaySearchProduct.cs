using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            driver.Manage().Window.Maximize();
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

            driver.Quit();
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void PrintProducts()
        {
            //Go To Ebay.com
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(3000);

            //Search for shoes
            driver.FindElement(By.Id("gh-ac")).SendKeys("shoes");
            Thread.Sleep(3000);

            //Select brand PUMA
            driver.FindElement(By.Id("gh-btn")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//*[@id='w3-w0-w2-w2-w6']/a")).Click();
            Thread.Sleep(3000);

            //Select size 10
            driver.FindElement(By.XPath("//*[@id='w3-w0-w2-multiselect[4]']/a")).Click();
            Thread.Sleep(3000);

            //Order by price ascendant
            driver.FindElement(By.XPath("//*[@id='w4-w1']/button/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//*[@id='w4-w1']/button/div")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//*[@id='w4-w1']/div/div/ul/li[4]/a")).Click();           
            Thread.Sleep(3000);

            //Take the first 5 products with their prices and print them in console.
            var listResults = driver.FindElements(By.XPath("//*[contains(@id,'srp-river-results-listing')]/div/div[2]"));

            var products = new List<string[]>();

            for (var i=0; i < 5; i++ )
            {
                var item = listResults[i];
                var line = item.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                products.Add(line);
            }

            TestContext.WriteLine("PRINT FIRST 5 PRODUCTS");
            PrintList(products);

            //Order and print the products by name (ascendant)
            var p = products.OrderBy(x => x[0]).ToList();
            TestContext.WriteLine("");
            TestContext.WriteLine("PRINT FIRST 5 PRODUCTS ORDERED BY NAME");
            PrintList(p);

            //Order and print the products by price in descendant mode
            var reg = new Regex(@"[0-9]+.\d+");

            var p2 = products.OrderByDescending(x =>
            {
                var match = reg.Match(x[1]);
                return decimal.Parse(match.Value);
            }).ToList();

            TestContext.WriteLine("");
            TestContext.WriteLine("PRINT FIRST 5 PRODUCTS ORDERED BY PRICE");
            PrintList(p2);

            driver.Quit();
        }

        public void PrintList(List<string[]> list)
        {
            foreach (var item in list)
            {
                TestContext.WriteLine(item[0]);
                TestContext.WriteLine(item[1]);
            }
        }

    }    
}
