using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Royale.test
{
    public class CardTests
    {
        IWebDriver driver;

        [SetUp]
        public void BeforeEach()
        {
            driver = new ChromeDriver(Path.GetFullPath(@"../../../../" + "_drivers"));
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void AfterEach()
        {
            driver.Quit();
        }

        [Test]
        public void Ice_Spirit_is_on_Cards_Page()
        {
            // 1. go to statsroyale.com.
            driver.Url = "https://statsroyale.com";
            // 2. click "Cards" link in the header nav.
            driver.FindElement(By.CssSelector("a[href='/cards']")).Click();
            // 3. Assert "Ice Spirit" is displayed.
            var iceSpirit = driver.FindElement(By.CssSelector("a[href*='Ice+Spirit']"));
            Assert.That(iceSpirit.Displayed);
        }

        [Test]
        public void Ice_Spirit_headers_are_correct_on_Cards_Details_Page()
        {
            // 1. go to statsroyale.com.
            driver.Url = "https://statsroyale.com";
            // 2. accept cookies
            driver.FindElement(By.CssSelector("span[id='cmpbntyestxt'")).Click();
            // 3. click "Cards" link in the header nav.
            driver.FindElement(By.CssSelector("a[href='/cards']")).Click();
            // 4. Assert "Ice Spirit" is displayed.
            driver.FindElement(By.CssSelector("a[href*='Ice+Spirit']")).Click();
            // 5. Assert basic header stats
            var cardName = driver.FindElement(By.CssSelector("[class*='cardName']")).Text;
            var cardCategories = driver.FindElement(By.CssSelector(".card__rarity")).Text.Split(", ");
            var cardType = cardCategories[0];
            var cardArena = cardCategories[1];
            var cardRarity = driver.FindElement(By.CssSelector(".card__common")).Text;

            Assert.AreEqual("Ice Spirit", cardName);
            Assert.AreEqual("Troop", cardType);
            Assert.AreEqual("Arena 8", cardArena);
            Assert.AreEqual("Common", cardRarity);
        }
    }
}