using System.Configuration;
using System.Linq;
using FluentAssertions;
using TechTalk.SpecFlow;
using TT_ListOrder;
using TT_WebdriverCore.WebDriverCore;

namespace TT_AutomatedTests
{
    [Binding]
    public class TT_ListOrderingSteps
    {
        [Given(@"I have a list of unorderd integers '(.*)'")]
        public void GivenIHaveAListOfUnorderdIntegers(string p_List)
        {
            // Extract the list to an array of integers and save in ScenarioContext
            var numlist = (p_List ?? "").Split(',').Select(int.Parse).ToArray();
            if (ScenarioContext.Current.ContainsKey("LIST"))
                ScenarioContext.Current.Remove("LIST");
            ScenarioContext.Current.Set(numlist, "LIST");
        }

        [When(@"I process the list for the  largest orderd list")]
        public void WhenIProcessTheListForTheLargestOrderdList()
        {
            // Get the list saved in the Scenario context
            var intList = (int[]) ScenarioContext.Current["LIST"];

            intList.Should().NotBeNullOrEmpty();

            var ttListOrdering = new TT_ListOrdering();
            var largestOrderList = ttListOrdering.GetLargestOrderList(intList).ToArray();

            largestOrderList.Should().NotBeNullOrEmpty();
            // Extract the list to an array of integers and save in ScenarioContext
            if (ScenarioContext.Current.ContainsKey("LARGEST_LIST"))
                ScenarioContext.Current.Remove("LARGET_LIST");
            ScenarioContext.Current.Set(largestOrderList, "LARGEST_LIST");
        }

        [Then(@"I am given the first largest sublist and its size as '(.*)' '(.*)'")]
        public void ThenIAmGivenTheFirstLargestSublistAndItsSizeAs(string p_LargestSub, int p_Size)
        {
            var expectedlist = (p_LargestSub ?? "").Split(',').Select(int.Parse).ToArray();
            expectedlist.Should().NotBeNullOrEmpty();

            // Get the sublist from ScenarioContext
            var actuallist = (int[])ScenarioContext.Current["LARGEST_LIST"];

            actuallist.Should().NotBeNullOrEmpty();
            actuallist.Should().Equal(expectedlist);
            actuallist.Max().Should().Equals(p_Size);
        }

        [Given(@"I have am on the Wikipedia home page")]
        public void GivenIHaveAmOnTheWikipediaHomePage()
        {
            //Browser.GoToUrl(ConfigurationManager.AppSettings["AlexaURL"]);
        }

    }
}
