using MuranoTest.Models;
using System.Net.NetworkInformation;

namespace MuranoTest.Tools
{
    public class RandomUrlListCreator
    {
        private UrlScraperEngine _urlScraper;
        private GoogleEngine _googleEngine;
        private List<UrlObject> _randomUrlObjects;
        public RandomUrlListCreator()
        {
            _urlScraper = new UrlScraperEngine();
            _googleEngine = new GoogleEngine();
            _randomUrlObjects = new List<UrlObject>();
        }
        public List<UrlObject> GetRandomUrlObjects(string searchingText, int count)
        {
            var allUrlObjects = new List<UrlObject>();
            allUrlObjects.AddRange(_urlScraper.SearchAsync(searchingText, SiteState.Yandex).Result);
            allUrlObjects.AddRange(_urlScraper.SearchAsync(searchingText, SiteState.Bing).Result);
            allUrlObjects.AddRange(_googleEngine.SearchAsync(searchingText).Result);
            AddObjectsToRandomList(allUrlObjects, count);
            return _randomUrlObjects;
        }
        private void AddObjectsToRandomList(List<UrlObject> objectsToAdd, int count)
        {
            for (int i = 0; i <= objectsToAdd.Count; i++)
            {
                Random random = new Random();
                var randomObj = random.Next(0, objectsToAdd.Count);
                if (_randomUrlObjects.FirstOrDefault(o => o.UrlText == objectsToAdd[randomObj].UrlText) != null)
                    continue;
                _randomUrlObjects.Add(objectsToAdd[randomObj]);
                if (_randomUrlObjects.Count >= count)
                    break;
            }
        }
    }
}
