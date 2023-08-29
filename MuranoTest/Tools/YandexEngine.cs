using MuranoTest.Models;
using MuranoTest.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace MuranoTest.Tools
{
    public class YandexEngine :BaseEngine
    {
     override public async Task<List<UrlObject>> SearchAsync(string searchText)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var res = await client.GetStringAsync("https://yandex.ru/search/xml?user=ganychev92&key=03.796630938:23def06b5e51ff6e880a0ec7e66796a5&query=" + searchText);
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(res);
                    XmlNodeList xmlRes = xmlDocument.GetElementsByTagName("group");

                    List<UrlObject> listResults = new List<UrlObject>();

                    if (xmlRes != null)
                        foreach (XmlNode x in xmlRes)
                        {
                            listResults.Add(new UrlObject { UrlText = x.SelectSingleNode("doc/url")?.InnerText, Description= x.SelectSingleNode("doc/passages/passage")?.InnerText,SiteState= SiteState.Yandex });
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
}