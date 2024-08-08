using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

public class ExternalDataService
{
    private readonly HttpClient _httpClient;

    public ExternalDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        httpClient.Timeout = TimeSpan.FromMinutes(5);
    }

    public async Task<string> GetProductInfoAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "https://www.liquidation.com/auction/view?auctionId=18878999");
        request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
        request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
        request.Headers.Add("Referer", "https://www.liquidation.com/");
        request.Headers.Add("Accept-Language", "en-US,en;q=0.9");
        request.Headers.Add("Connection", "keep-alive");

        try
        {
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            // قم بتسجيل الـ HTML للتحقق من محتوياته
            Console.WriteLine(responseBody);

            var doc = new HtmlDocument();
            doc.LoadHtml(responseBody);

            var contentNode = doc.DocumentNode.SelectSingleNode("//div[@id='lg-outer-1']");

            return contentNode?.OuterHtml ?? "No content found for the given XPath.";
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
            return null;
        }
    }

    public async Task<string> GetWebContentAsync(string url)
    {
        var response = await _httpClient.GetStringAsync(url);
        return response;
    }

    public async Task ScrapeDataAsync(string url)
    {
        var html = await GetWebContentAsync(url);
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        // مثال: استخراج العناوين من عناصر <h1>
        var headings = htmlDocument.DocumentNode.SelectNodes("//h1");
        if (headings != null)
        {
            foreach (var heading in headings)
            {
                // قم بمعالجة أو تخزين البيانات هنا
                Console.WriteLine(heading.InnerText);
            }
        }

    }
}
