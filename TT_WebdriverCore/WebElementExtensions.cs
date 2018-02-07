//using System;
//using System.Threading;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Support.UI;
//using TT_WebdriverCore.WebDriverCore;

//namespace TT_WebdriverCore
//{
//    public static class WebElementExtension
//    {

//        public static void ClickByJs(this IWebElement p_Element)
//        {
//            Browser.JavaScriptExecutor.ExecuteScript("arguments[0].click();", p_Element);
//        }

//        public static void ClearByBackspace(this IWebElement p_Element)
//        {
//            p_Element.SendKeys(Keys.End);
//            while (p_Element.GetAttribute("value").Length > 0)
//                p_Element.SendKeys(Keys.Backspace);
//        }
        
//        public static bool IsDisplayed(this IWebElement p_Element)
//        {
//            try
//            {
//                return p_Element.Displayed;
//            }
//            catch (NoSuchElementException)
//            {
//                return false;
//            }
//        }

//        public static void SelectDropDownListItemByText(this IWebElement p_Element, string p_Text)
//        {
//            var selectElement = new SelectElement(p_Element);
//            selectElement.SelectByText(p_Text);
//        }

//        public static void ChangeFocusByClickingTheInputAndSendingTheTabKey(this IWebElement p_Element)
//        {
//            p_Element.Click();
//            p_Element.SendKeys(Keys.Tab);
//        }

//        public static bool HasClass(this IWebElement p_Element, string p_ClassName)
//        {
//            return p_Element.GetAttribute("class").Contains(p_ClassName);
//        }

//        public static void ClickWithWait(this IWebElement p_Element, bool p_IsJsOff = false, bool p_IsExceedTest = false)
//        {
//            if (!p_IsExceedTest)
//            {
//                Browser.ScrollToElement(p_Element);
//            }

//            try
//            {
//                p_Element.Click();
//            }
//            catch (Exception)
//            {
//                // ignore
//            }
//        }

//        public static void SendKeysWithWait(this IWebElement p_Element, string p_Text, bool p_IsJsOff = false, bool p_IsExceedTest = false)
//        {
//            if (p_IsJsOff || p_IsExceedTest)
//            {
//                p_Element.SendKeys(p_Text);
//                return;
//            }

////            Browser.WaitForElementToAppear(p_Element);
//            Browser.ScrollToElement(p_Element);
//            p_Element.Clear();
//            p_Element.SendKeys(p_Text);
//        }

//        public static void SendKeysWithClear(this IWebElement p_Element, string p_Text)
//        {
//            p_Element.Clear();
//            p_Element.SendKeys(p_Text);
//        }
//    }
//}
