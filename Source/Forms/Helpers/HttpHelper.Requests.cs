using System;
using System.Xml;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MCCForms
{
	public partial class HttpHelper
	{
		public async Task<List<RssFeedItem>> GetTweakersRssFeed()
		{
			var foundRssItems = new List<RssFeedItem> ();

			HttpResponseMessage response = await _httpClient.GetAsync ("https://feeds.feedburner.com/tweakers/reviews");

			if (response != null) {
				
				var xml = await response.Content.ReadAsStringAsync ();

				XmlDocument rssXmlDoc = new XmlDocument ();
				rssXmlDoc.LoadXml (xml);

				XmlNodeList rssNodes = rssXmlDoc.SelectNodes ("rss/channel/item");

				foreach (XmlNode rssNode in rssNodes) {
					XmlNode rssSubNode = rssNode.SelectSingleNode ("title");
					string title = rssSubNode != null ? rssSubNode.InnerText : "";

					rssSubNode = rssNode.SelectSingleNode ("link");
					string link = rssSubNode != null ? rssSubNode.InnerText : "";

					rssSubNode = rssNode.SelectSingleNode ("description");
					string description = rssSubNode != null ? rssSubNode.InnerText : "";

					var rssFeedItem = new RssFeedItem { Title = title, Description = description, Link = link };

					foundRssItems.Add (rssFeedItem);
				}
			}
			return foundRssItems;
		}
	}
}