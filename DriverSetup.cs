using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestfulBookerUITests.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace RestfulBookerUITests
{
    [Binding]
    public sealed class DriverSetup
    {
        private IObjectContainer objectContainer;
        private IWebDriver Driver { get; set; }

        LoginControls loginControls => new LoginControls(Driver);
        public DriverSetup(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Driver = new ChromeDriver(Environment.CurrentDirectory);
            objectContainer.RegisterInstanceAs(this.Driver);

            Driver.Navigate().GoToUrl("https://automationintesting.online/#/");
            Driver.Manage().Window.Maximize();

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            this.loginControls.LetMeHackButton.Click();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Driver.Close();
        }
    }
}
