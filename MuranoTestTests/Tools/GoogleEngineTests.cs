using Microsoft.VisualStudio.TestTools.UnitTesting;
using MuranoTest.Models;
using MuranoTest.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuranoTest.Tools.Tests
{
    [TestClass()]
    public class GoogleEngineTests
    {
        [TestMethod()]
        public void SearchAsyncTest()
        {
            //Arrange
            HttpClient testClient = new HttpClient();
            List<UrlObject> testResults = new List<UrlObject>();
            string testText = "test";
            var res = testClient.GetStringAsync("https://www.googleapis.com/customsearch/v1?key=AIzaSyBn38kOVM3fbcJ_i8_JwEUgpJf61dAqPGs&cx=017576662512468239146:omuauf_lfve&q=" + testText);
            //Act
            GoogleSearchResult testRes = JsonConvert.DeserializeObject<GoogleSearchResult>(res.Result);

            if (testRes.items != null)
            {
                foreach (var item in testRes.items)
                {
                    testResults.Add(new UrlObject { UrlText = item.link, Description = item.snippet, SiteState = SiteState.Google });
                }
            }
            //Assert
            Assert.IsNotNull(testResults);
        }
    }
}