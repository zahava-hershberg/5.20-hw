using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace Scraper.Web.Services
{
    public class News
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
        public int Comments { get; set; }

    }
    public class LakewoodScoopScraper
    {
        public List<News> Scrape ()
        {
            var html = GetLakewoodScoopHtml();
            var parse = new HtmlParser();
            var doc = parse.ParseDocument(html);
            var resultDivs = doc.QuerySelectorAll("div.td-category-pos-image");
            return resultDivs.Select(div => ParseNews(div)).Where(i => i != null).ToList();
        }
        private string GetLakewoodScoopHtml()
        {
            var url = "https://thelakewoodscoop.com/";
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
                UseCookies = true
            };
            var client = new HttpClient(handler);
            return client.GetStringAsync(url).Result;
        }
        private News ParseNews(IElement div)
        {
            var news = new News();
            var titleElement = div.QuerySelector("h3.td-module-title");
            if (titleElement == null)
            {
                return null;
            }
            news.Title = titleElement.TextContent;
            var imageElement = div.QuerySelector("span.entry-thumb");
            if (imageElement != null)
            {
                var src = imageElement.Attributes["data-img-url"].Value;
                news.Image = src;
            }
            var anchorTag = div.QuerySelector("h3.td-module-title a");
            if (anchorTag != null)
            {
                news.Url = $"{anchorTag.Attributes["href"].Value}";
            }
            var textElement = div.QuerySelector("div.td-excerpt");
            if(textElement != null)
            {
                news.Text = textElement.TextContent;
            }
            var comments = div.QuerySelector("span.td-module-comments");
            if (comments != null)
            {
                news.Comments = int.Parse(comments.TextContent);
            }
            return news;
            
        }
    }
}
