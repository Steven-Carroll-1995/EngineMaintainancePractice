using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Royale.Pages;

namespace Royale.test
{
    public class CardTests
    {
        IWebDriver driver;

        [SetUp]
        public void BeforeEach()
        {
            driver = new ChromeDriver(Path.GetFullPath(@"../../../../" + "_drivers"));
            driver.Url = "https://statsroyale.com";
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
            var cardsPage = new CardsPage(driver);
            var iceSpirit = cardsPage.Goto().GetCardByName("Ice Spirit");
            Assert.That(iceSpirit.Displayed);
        }

        [Test]
        public void Ice_Spirit_headers_are_correct_on_Cards_Details_Page()
        {
            // 2. accept cookies
            driver.FindElement(By.CssSelector("span[id='cmpbntyestxt'")).Click();
            new CardsPage(driver).Goto().GetCardByName("Ice Spirit").Click();
            var cardDetails = new CardDetailsPage(driver);

            var (Category, Arena) = cardDetails.GetCardCategory();
            
            // 5. Assert basic header stats
            var cardName = cardDetails.Map.CardName.Text;
            var cardRarity = cardDetails.Map.CardRarity.Text;
            Assert.AreEqual("Ice Spirit", cardName);
            Assert.AreEqual("Troop", Category);
            Assert.AreEqual("Arena 8", Arena);
            Assert.AreEqual("Common", cardRarity);
        }
    }
}