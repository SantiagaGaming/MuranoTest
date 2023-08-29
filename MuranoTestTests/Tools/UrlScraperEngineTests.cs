using HtmlAgilityPack;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MuranoTest.Models;
using MuranoTest.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MuranoTest.Tools.Tests
{
    [TestClass()]
    public class UrlScraperEngineTests
    {
        [TestMethod()]
        public void SearchAsyncTest()
        {

            //Arrange
            string GOOGLE_SEARCH_URL = "https://www.google.com/search?q=";
            string YANDEX_SEARCH_URL = "https://ya.ru/search/?text=";
            string BING_SEARCH_URL = "https://www.bing.com/search?q=";

            List<UrlObject> testUrlObjectsGoogle = new List<UrlObject>();
            List<UrlObject> testUrlObjectsYandex = new List<UrlObject>();
            List<UrlObject> testUbjectsBing = new List<UrlObject>();
            var httpClient = new HttpClient();
            string testText = "test";

            //Act
            var testhtmlGoogle = httpClient.GetStringAsync(GOOGLE_SEARCH_URL+ testText).Result;
            var testhtmlDocumentGoogle = new HtmlDocument();
            testhtmlDocumentGoogle.LoadHtml(testhtmlGoogle);
            HtmlNodeCollection nodesGoogle = testhtmlDocumentGoogle.DocumentNode.SelectNodes("//a[@href]");
            foreach (var node in nodesGoogle)
            {
                string href = node.Attributes["href"].Value;
                if (href[0] == 'h' && href.Length < 40)
                {
                    var urlObj = new UrlObject() { UrlText = href, Description = "None", SiteState = SiteState.Google};
                    if (testUrlObjectsGoogle.FirstOrDefault(o => o.UrlText == urlObj.UrlText) == null)
                        testUrlObjectsGoogle.Add(urlObj);
                }
            }

            var testhtmlYandex = httpClient.GetStringAsync(YANDEX_SEARCH_URL + testText).Result;
            var testhtmlDocumentYandex = new HtmlDocument();
            testhtmlDocumentYandex.LoadHtml(testhtmlYandex);
            HtmlNodeCollection nodesYandex = testhtmlDocumentYandex.DocumentNode.SelectNodes("//a[@href]");

            foreach (var node in nodesYandex)
            {
                string href = node.Attributes["href"].Value;
                if (href[0] == 'h' && href.Length < 40)
                {
                    var urlObj = new UrlObject() { UrlText = href, Description = "None", SiteState = SiteState.Yandex };
                    if (testUrlObjectsYandex.FirstOrDefault(o => o.UrlText == urlObj.UrlText) == null)
                        testUrlObjectsYandex.Add(urlObj);
                }
            }

            var testhtmlBing = httpClient.GetStringAsync(BING_SEARCH_URL + testText).Result;
            var testhtmlDocumentBing = new HtmlDocument();
            testhtmlDocumentBing.LoadHtml(testhtmlBing);
            HtmlNodeCollection nodesBing= testhtmlDocumentBing.DocumentNode.SelectNodes("//a[@href]");

            foreach (var node in nodesBing)
            {
                string href = node.Attributes["href"].Value;
                if (href[0] == 'h' && href.Length < 40)
                {
                    var urlObj = new UrlObject() { UrlText = href, Description = "None", SiteState = SiteState.Bing };
                    if (testUbjectsBing.FirstOrDefault(o => o.UrlText == urlObj.UrlText) == null)
                        testUbjectsBing.Add(urlObj);
                }
            }

            //Assert
            Assert.IsNotNull(testUrlObjectsGoogle);
            Assert.IsNotNull(testUrlObjectsYandex);
            Assert.IsNotNull(testUbjectsBing);
        }
    }
}
