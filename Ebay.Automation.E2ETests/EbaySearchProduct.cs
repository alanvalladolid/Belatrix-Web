using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebay.Automation.E2ETests
{
   [TestClass]
    public class EbaySearchProductTests
    {
        private IWebDriver driver;
        private string url;

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
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.Id("gh-ac")).SendKeys("shoes");
            driver.FindElement(By.Id("gh-btn")).Click();
            driver.FindElement(By.XPath("//*[@id='w3-w0-w2-w2-w6']/a")).Click();
            driver.FindElement(By.XPath("//*[@id='w3-w0-w2-multiselect[4]']/a")).Click();
            var searchResultNumber = driver.FindElement(By.XPath("//*[@id='w4']/div[2]/div/div[1]/h1")).Text;
            Console.WriteLine(searchResultNumber);
            Assert.IsTrue(driver.Title.Contains("Azure Pipelines"), "Verified title of the page");


        }
    }

    
}
