using MuranoTest.Models;
using MuranoTest.Tools;
using Newtonsoft.Json;
using System;
using System.Xml;

namespace MuranoTest.Tools
{
    public class BingEngine : BaseEngine
    {
        public override async Task<List<UrlObject>> SearchAsync(string searchText)
        {

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "d4b9d8ee-4fb2-4fe1-a5e1-5a0b53933e8f");

                        var res = await client.GetStringAsync("https://api.cognitive.microsoft.com/bing/v7.0/search?q=" + searchText);

                        BingSearchResult jres = JsonConvert.DeserializeObject<BingSearchResult>(res);

                        List<UrlObject> listResults = new List<UrlObject>();

                        if (jres?.webPages?.value != null)
                        {
                            foreach (var item in jres.webPages.value)
                            {
                                listResults.Add(new UrlObject {  UrlText = item.url, Description = item.snippet, SiteState = SiteState.Bing });
                            }
                        }

                    return listResults;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            
        }
    }

    public class BingSearchResult
    {
        public string _type { get; set; }
        public Querycontext queryContext { get; set; }
        public Webpages webPages { get; set; }
        public Relatedsearches relatedSearches { get; set; }
        public Videos videos { get; set; }
        public Rankingresponse rankingResponse { get; set; }
    }

    public class Querycontext
    {
        public string originalQuery { get; set; }
    }

    public class Webpages
    {
        public string webSearchUrl { get; set; }
        public int totalEstimatedMatches { get; set; }
        public Value[] value { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public About[] about { get; set; }
        public bool isFamilyFriendly { get; set; }
        public string displayUrl { get; set; }
        public string snippet { get; set; }
        public DateTime dateLastCrawled { get; set; }
        public string language { get; set; }
        public bool isNavigational { get; set; }
    }

    public class About
    {
        public string name { get; set; }
    }

    public class Relatedsearches
    {
        public string id { get; set; }
        public Value1[] value { get; set; }
    }

    public class Value1
    {
        public string text { get; set; }
        public string displayText { get; set; }
        public string webSearchUrl { get; set; }
    }

    public class Videos
    {
        public string id { get; set; }
        public string readLink { get; set; }
        public string webSearchUrl { get; set; }
        public bool isFamilyFriendly { get; set; }
        public Value2[] value { get; set; }
        public string scenario { get; set; }
    }

    public class Value2
    {
        public string webSearchUrl { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string thumbnailUrl { get; set; }
        public DateTime datePublished { get; set; }
        public Publisher[] publisher { get; set; }
        public bool isAccessibleForFree { get; set; }
        public string contentUrl { get; set; }
        public string hostPageUrl { get; set; }
        public string encodingFormat { get; set; }
        public string hostPageDisplayUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string duration { get; set; }
        public string motionThumbnailUrl { get; set; }
        public string embedHtml { get; set; }
        public bool allowHttpsEmbed { get; set; }
        public int viewCount { get; set; }
        public Thumbnail thumbnail { get; set; }
        public bool allowMobileEmbed { get; set; }
        public bool isSuperfresh { get; set; }
    }

    public class Thumbnail
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Publisher
    {
        public string name { get; set; }
    }

    public class Rankingresponse
    {
        public Mainline mainline { get; set; }
        public Sidebar sidebar { get; set; }
    }

    public class Mainline
    {
        public Item2[] items { get; set; }
    }

    public class Item2
    {
        public string answerType { get; set; }
        public int resultIndex { get; set; }
        public Value3 value { get; set; }
    }

    public class Value3
    {
        public string id { get; set; }
    }

    public class Sidebar
    {
        public Item1[] items { get; set; }
    }

    public class Item1
    {
        public string answerType { get; set; }
        public Value4 value { get; set; }
    }

    public class Value4
    {
        public string id { get; set; }
    }
}