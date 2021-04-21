using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace Shymanovich_Tests
{
    /* 
     * for Booking.com:
     * 1/Change currency / language
     * 2/Check the moving to tiskets purchase page
     * 3/Check the availability of personal page
     * 4/Check the filter (choose a city, trip in a week, arrival +2 days, 2 adults & 1 child, 1 room)
     */
    public class Tests
    {        
        private IWebDriver driver;
        #region 1st test
        private readonly By _languageButton = By.XPath("//button[@data-modal-id='language-selection']");
        private readonly By _chooseLanguageButton = By.XPath("//div[@lang='pl']");
        private readonly By _actualTranslation = By.XPath("//span[@class='bui-tab__text']"); 
        private readonly By _currencyButton = By.XPath("//*[@id='b2indexPage']/header/nav[1]/div[2]/div[1]/button/span/span[1]"); 
        private readonly By _chooseCurrencyButton = By.XPath("//a[@data-modal-header-async-url-param='changed_currency=1;selected_currency=USD;top_currency=1']");
        
        private const string expectedTranslation = "Pobyty";
        private const string expectedCurrency = "USD";
        #endregion

        #region 2nd test
        private readonly By _aviaTicketsArea = By.XPath("//a[@data-decider-header='flights']");
        private readonly By _actualAviaTicketsArea = By.XPath("//h1");

        private const string expectedAviaTicketsArea = "Search hundreds of flight sites at once.";
        #endregion

        #region 3rd test
        private readonly By _signInButton = By.XPath("//*[@id='b2indexPage']/header/nav[1]/div[2]/div[6]/a/span");
        private readonly By _loginInput = By.XPath("//*[@id='username']");
        private readonly By _continueButton = By.XPath("//button[@type='submit']");
        private readonly By _passwordInput = By.XPath("//*[@id='password']");
        private readonly By _enterButton = By.XPath("//button[@type='submit']");
        private readonly By _actualSignIn = By.XPath("//*[@id='profile-menu-trigger--title']");

        private const string userLogin = "24011989m@mail.ru";
        private const string userPassword = "/m*gq6ie55A,!H&";
        private const string expectedSignIn = "Maryia Shatsila";
        #endregion

        #region 4th test
        private readonly By _cityInput = By.XPath("//input[@name='ss']");
        private readonly By _dateInput = By.XPath("//div[@class='xp__dates-inner xp__dates__checkout']");
        private readonly By _chooseDate1 = By.XPath(dateInXPath); 
        private readonly By _chooseDate2 = By.XPath(dateOutXPath); 
        private readonly By _guestsInput = By.XPath("//label[@id='xp__guests__toggle']");
        private readonly By _childrenChoose = By.XPath("//button[@aria-label='Increase number of Children']");
        private readonly By _searchButton = By.XPath("//button[@data-sb-id='main']");
        private readonly By _actualSearch = By.XPath("//div[@class='bui-breadcrumb__text']");

        private const string userCity = "Minsk";
        private const string expectedSearch = "Home";
        
        static string dateIn = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd");
        static string dateInXPath = "//td[@data-date='" + dateIn + "']";
        static string dateOut = DateTime.Now.AddDays(16).ToString("yyyy-MM-dd");
        static string dateOutXPath = "//td[@data-date='" + dateOut + "']";
        #endregion

        [SetUp]
        public void Setup()
        {            
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.booking.com"); 
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1ChangeCurLan() //Change currency & language
        {
            var languageButton = driver.FindElement(_languageButton);
            languageButton.Click();
            Thread.Sleep(300);

            var chooseLanguageButton = driver.FindElement(_chooseLanguageButton);
            chooseLanguageButton.Click();
            Thread.Sleep(400);

            var actualTranslation = driver.FindElement(_actualTranslation);

            Assert.AreEqual(expectedTranslation, actualTranslation.Text, "Could not change the language!");
            
            var currencyButton = driver.FindElement(_currencyButton);
            currencyButton.Click();
            Thread.Sleep(300);

            var chooseCurrencyButton = driver.FindElement(_chooseCurrencyButton);
            chooseCurrencyButton.Click();
            Thread.Sleep(300);

            var actualCurrency = driver.FindElement(_currencyButton);

            Assert.AreEqual(expectedCurrency, actualCurrency.Text, "Could not change the currency!");
        }

        [Test]
        public void Test2CheckMovingToTicketsPurch() //Check the moving to tiskets purchase page
        {
            var aviaTicketsArea = driver.FindElement(_aviaTicketsArea);
            aviaTicketsArea.Click();
            Thread.Sleep(300);

            var actualAviaTicketsArea = driver.FindElement(_actualAviaTicketsArea);

            Assert.AreEqual(expectedAviaTicketsArea, actualAviaTicketsArea.Text, "Could not get the Avia page!");
        }

        [Test]
        public void Test3CheckPersPage() //Check the availability of personal page
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
        public void Test4CheckFilter() //Check the filter (choose a city, trip: in a week, arrival: +2 days, 2 adults & 1 child, 1 room)
        {
            var cityInput = driver.FindElement(_cityInput);
            cityInput.SendKeys(userCity);

            var dateInput1 = driver.FindElement(_dateInput);
            dateInput1.Click();

            var chooseDate1 = driver.FindElement(_chooseDate1);
            chooseDate1.Click();

            var dateInput2 = driver.FindElement(_dateInput);
            dateInput2.Click();
            dateInput2.Click();

            var chooseDate2 = driver.FindElement(_chooseDate2);
            chooseDate2.Click();            

            var guestsInput = driver.FindElement(_guestsInput);
            guestsInput.Click();

            var childrenChoose = driver.FindElement(_childrenChoose);
            childrenChoose.Click();

            var searchButton = driver.FindElement(_searchButton);
            searchButton.Click();
            Thread.Sleep(400);

            var actualSearch = driver.FindElement(_actualSearch);

            Assert.AreEqual(expectedSearch, actualSearch.Text, "Could not search the trip!");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}