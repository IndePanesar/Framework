using TechTalk.SpecFlow;

namespace TT_WebdriverCore.WebDriverCore
{
    [Binding]
    public static class SpecflowHooks
    {
        //[BeforeTestRun]
        //public static void BeforeTestRun()
        //{
        //    ScenarioContext.Current.Clear();

        //}

        [BeforeScenario("ListOrdering")]
        public static void BeforeScenario()
        {
            ScenarioContext.Current.Clear();
        }

        [BeforeStep]
        public static void BeforeStep()
        {

        }

        [AfterStep]
        public static void AfterStep()
        {
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            ScenarioContext.Current.Clear();
            Browser.DeleteCookies();
            Browser.Close();
        }

        [AfterTestRun]
        public static void AfterRunScenario()
        {
        }
    }
}