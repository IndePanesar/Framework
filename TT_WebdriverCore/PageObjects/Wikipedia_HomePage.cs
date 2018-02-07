using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;
using TT_WebdriverCore.PageLocators;
using TT_WebdriverCore.WebDriverCore;

namespace TT_WebdriverCore.PageObjects
{
    class WikipediaHomePage : WikipediaHome_PageLocators
    {
        private const string PageTitle = "Wikipedia";

        public WikipediaHomePage()
        {
            PageFactory.InitElements(Browser.SearchContext, this);
        }

        public void EnterSearchText(string p_SearchText)
        {
            //txtUsername.Click();
            //txtUsername.Clear();
            //txtUsername.SendKeys(p_SearchText);
        }


        public void SubmitSearch()
        {
            //btnSubmitSearch.Click();
        }

        public string GetPageTitle()
        {
            return ScenarioContext.Current.ContainsKey("SEARCH_TEXT") ?
                    $"{(string) ScenarioContext.Current["SEARCH_TEXT"]} - Wikipedia" :
                    PageTitle;
        }
    }
}
