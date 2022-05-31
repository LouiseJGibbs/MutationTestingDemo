using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace RestfulBookerUITests.Steps
{
    [Binding]
    public sealed class AdminInboxControls
    {
        private readonly IWebDriver driver;

        public AdminInboxControls(IWebDriver driver)
        {
            this.driver = driver;
        }
        public IWebElement MessagesInboxLink => this.driver.FindElement(By.XPath("//i[@class='fa fa-inbox']"));
        public IWebElement Messages => this.driver.FindElement(By.XPath("//div[@class='messages']"));
        public ReadOnlyCollection<IWebElement> MessagesList_All => Messages.FindElements(By.XPath(".//div[starts-with(@class,'row detail')]"));
        public ReadOnlyCollection<IWebElement> MessagesList_Read => Messages.FindElements(By.XPath(".//div[@class='row detail read-true']"));
        public ReadOnlyCollection<IWebElement> MessagesList_Unread => Messages.FindElements(By.XPath(".//div[@class='row detail read-false']"));


        public By MessageWindowBy = By.XPath("//div[@data-testid='message']");
        public IWebElement MessageWindow => this.driver.FindElement(MessageWindowBy);
        public ReadOnlyCollection<IWebElement> MessageDetails => MessageWindow.FindElements(By.XPath("./div[@class='form-row']"));
    }
}

