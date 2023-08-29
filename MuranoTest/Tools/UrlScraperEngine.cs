using HtmlAgilityPack;
using MuranoTest.Models;
using System;

namespace MuranoTest.Tools
{
    public class UrlScraperEngine
    {
        private const string GOOGLE_SEARCH_URL = "https://www.google.com/search?q=";
        private const string YANDEX_SEARCH_URL = "https://ya.ru/search/?text=";
        private const string BING_SEARCH_URL = "https://www.bing.com/search?q=";

        private string _currentUrl;
        private List<UrlObject> _urlObjects;

        public async Task<List<UrlObject>> SearchAsync(string searchingText, SiteState state)
        {
            _urlObjects = new List<UrlObject>();
            var httpClient = new HttpClient();
            switch (state)
            {
                case SiteState.Yandex:
                    _currentUrl = YANDEX_SEARCH_URL + searchingText;
                    break;
                case SiteState.Google:
                    _currentUrl = GOOGLE_SEARCH_URL + searchingText;
                    break;
                case SiteState.Bing:
                    _currentUrl = BING_SEARCH_URL + searchingText;
                    break;
                default:
                    break;
            }

            var html = await httpClient.GetStringAsync(_currentUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            HtmlNodeCollection nodes = htmlDocument.DocumentNode.SelectNodes("//a[@href]");

            foreach (var item in nodes)
            {
                string href = item.Attributes["href"].Value;
                if (href[0] == 'h' && href.Length < 40)
                {
                    var urlObj = new UrlObject() { UrlText = href, Description = "None", SiteState = state };
                    if (_urlObjects.FirstOrDefault(o => o.UrlText == urlObj.UrlText) == null)
                        _urlObjects.Add(urlObj);
                }
            }
            return _urlObjects;
        }
    }
}
