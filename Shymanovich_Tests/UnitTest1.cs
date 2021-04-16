using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Shymanovich_Tests
{
    /* 
     * for Booking.com:
     * Change currency / language
     * Check the moving to tiskets purchase page
     * Check the availability of personal page
     * Check the filter (choose a city, trip in a week, arrival +2 days, 2 adults & 1 child, 1 room)
     */
    public class Tests
    {        
        private IWebDriver driver;
        
        #region 2nd test
        private readonly By _aviaTicketsArea = By.XPath("//*[@id='b2indexPage']/header/nav[2]/ul/li[2]/a");
        private readonly By _actualAviaTicketsArea = By.XPath("//h1");

        private const string expectedAviaTicketsArea = "Search hundreds of flight sites at once.";
        #endregion

        #region 3d test
        private readonly By _signInButton = By.XPath("//*[@id='b2indexPage']/header/nav[1]/div[2]/div[6]/a/span");
        private readonly By _loginInput = By.XPath("//*[@id='username']");
        private readonly By _continueButton = By.XPath("//*[@id='root']/div/div[2]/div[1]/div/div/div/div/div/div/form/div[3]/button/span");
        private readonly By _passwordInput = By.XPath("//*[@id='password']");
        private readonly By _enterButton = By.XPath("//*[@id='root']/div/div[2]/div[1]/div/div/div/div/div/div/form/button/span");
        private readonly By _actualSignIn = By.XPath("//*[@id='profile-menu-trigger--title']");

        private const string userLogin = "24011989m@mail.ru";
        private const string userPassword = "/m*gq6ie55A,!H&";
        private const string expectedSignIn = "Maryia Shatsila";
        #endregion

        [SetUp]
        public void Setup()
        {            
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.booking.com"); 
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1() //Change currency / language
        {
            
        }

        [Test]
        public void Test2() //Check the moving to tiskets purchase page
        {
            var aviaTicketsArea = driver.FindElement(_aviaTicketsArea);
            aviaTicketsArea.Click();
            Thread.Sleep(300);

            var actualAviaTicketsArea = driver.FindElement(_actualAviaTicketsArea);

            Assert.AreEqual(expectedAviaTicketsArea, actualAviaTicketsArea.Text, "Could not get the Avia page!");
        }

        [Test]
        public void Test3() //Check the availability of personal page
        {
            var signIn = driver.FindElement(_signInButton);
            signIn.Click();
            Thread.Sleep(300);

            var loginInput = driver.FindElement(_loginInput);
            loginInput.SendKeys(userLogin);

            var continueButton = driver.FindElement(_continueButton);
            continueButton.Click();
            Thread.Sleep(400);

            var passwordInput = driver.FindElement(_passwordInput);
            passwordInput.SendKeys(userPassword);

            var enterButton = driver.FindElement(_enterButton);
            enterButton.Click();
            Thread.Sleep(500);

            var actualSignIn = driver.FindElement(_actualSignIn);

            Assert.AreEqual(expectedSignIn, actualSignIn.Text, "Could not sign in!");
        }

        [Test]
        public void Test4() //Check the filter (choose a city, trip in a week, arrival +2 days, 2 adults & 1 child, 1 room)
        {
            
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}