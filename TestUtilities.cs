using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulBookerSpecflowUITests
{
    public static class TestUtilities
    {

        public static int Timeout = 30;

        public static void ScrollToElement(this IWebElement element, IWebDriver driver)
        {
            var yPos = element.Location.Y;
            var windowSize = driver.Manage().Window.Size.Height;
            var scrollPosition = yPos - (windowSize / 2);
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, arguments[0]);", scrollPosition);
        }

        public static bool Exists(this By elementBy, IWebDriver driver)
        {
            try
            {
                return driver.FindElement(elementBy).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool Exists(this By elementBy, IWebElement baseElement)
        {
            try
            {
                return baseElement.FindElement(elementBy).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static string GenerateRandomString(int length)
        {
            var r = new Random();
            var str = "abcdefghijklmnopqrstuvwxyz";
            var randomString = "";
            

            for(int i = 0; i < length; i++)
                randomString += str[r.Next(str.Length)];

            return randomString;
        }

        public static string GenerateRandomAlphanumericString(int length)
        {
            var r = new Random();
            var str = "abcdefghijklmnopqrstuvwxyz0123456789 ";
            var randomString = "";


            for (int i = 0; i < length; i++)
                randomString += str[r.Next(str.Length)];

            return randomString;
        }

        public static string GenerateRandomNumber(int length)
        {
            var r = new Random();
            var randomString = "";

            for (int i = 0; i < length; i++)
                randomString += r.Next(10);

            return randomString;
        }
    }
}