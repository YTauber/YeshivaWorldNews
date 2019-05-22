using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace YeshivaWorldNews.Data
{
    public class News
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public string Url { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Head,
        HeadSmall,
        SideHead,
        SideSmall,
        Main
    }

    public class NewsManager
    {
        public IEnumerable<News> GetNews()
        {
            string Html = GetHtml();
            return GetItems(Html);
        }

        private string GetHtml()
        {
            using (HttpClient client = new HttpClient())
            {
                return client.GetStringAsync("https://www.theyeshivaworld.com").Result;
            }
        }

        private List<News> GetItems(string html)
        {
            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(html);

            var ItemDivs = document.QuerySelectorAll(".td_module_10");//

            List<News> news = new List<News>();

            foreach (var div in ItemDivs)
            {
                News n = new News
                {
                    Image = div.QuerySelector("img.entry-thumb").Attributes["src"].Value,
                    Title = div.QuerySelector("a").Attributes["title"].Value,
                    Content = div.QuerySelector("div.td-excerpt").TextContent,
                    Url = div.QuerySelector("a").Attributes["href"].Value,
                    Date = div.QuerySelector("span.td-post-date").TextContent,
                    Status = Status.Main
                };
                news.Add(n);
            }

            ItemDivs = document.QuerySelectorAll(".td_module_mx8");
            foreach (var div in ItemDivs)
            {
                News n = new News
                {
                    Image = div.QuerySelector("img.entry-thumb").Attributes["src"].Value,
                    Title = div.QuerySelector("a").Attributes["title"].Value,
                    Url = div.QuerySelector("a").Attributes["href"].Value,
                    Date = div.QuerySelector("span.td-post-date").TextContent,
                    Status = Status.Head
                };
                news.Add(n);
            }

            ItemDivs = document.QuerySelectorAll(".td_module_mx1");
            foreach (var div in ItemDivs)
            {
                News n = new News
                {
                    Image = div.QuerySelector("img.entry-thumb").Attributes["src"].Value,
                    Title = div.QuerySelector("a").Attributes["title"].Value,
                    Url = div.QuerySelector("a").Attributes["href"].Value,
                    Date = div.QuerySelector("span.td-post-date").TextContent,
                    Status = Status.HeadSmall
                };
                news.Add(n);
            }

            ItemDivs = document.QuerySelectorAll(".td_module_4");
            foreach (var div in ItemDivs)
            {
                News n = new News
                {
                    Image = div.QuerySelector("img.entry-thumb").Attributes["src"].Value,
                    Title = div.QuerySelector("a").Attributes["title"].Value,
                    Url = div.QuerySelector("a").Attributes["href"].Value,
                    Date = div.QuerySelector("span.td-post-date").TextContent,
                    Status = Status.SideSmall
                };
                news.Add(n);
            }

            ItemDivs = document.QuerySelectorAll(".td_module_6");
            foreach (var div in ItemDivs)
            {
                News n = new News
                {
                    Image = div.QuerySelector("img.entry-thumb").Attributes["src"].Value,
                    Title = div.QuerySelector("a").Attributes["title"].Value,
                    Url = div.QuerySelector("a").Attributes["href"].Value,
                    Date = div.QuerySelector("span.td-post-date").TextContent,
                    Status = Status.SideHead
                };
                news.Add(n);
            }

            return news;
        }
    }
}
