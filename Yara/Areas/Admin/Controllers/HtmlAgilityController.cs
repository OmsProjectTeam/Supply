
using HtmlAgilityPack;
using System.Net;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HtmlAgilityController : Controller
    {
       

       

        public IActionResult myLodeHtml()
        {
            return View();
        }

        public async Task<IActionResult> MyUrl(string url)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                var document = web.Load("https://www.homedepot.com/s/" + url);
                var imageNodes = document.DocumentNode.SelectNodes("//div[@class='mediagallery']//img");

                if (imageNodes != null)
                {
                    var imageUrls = imageNodes.Select(node => node.GetAttributeValue("src", "")).ToList();
                    ViewBag.ImageUrls = imageUrls;                 
                }
                else
                {
                    var imageNodes7 = document.DocumentNode.SelectNodes("//div[@data-testid='product-image__wrapper']//img");

                    if (imageNodes7 != null && imageNodes7.Any())
                    {
                        var firstImageUrl = imageNodes7
                            .Select(node => node.GetAttributeValue("src", ""))
                            .FirstOrDefault(); // الحصول على أول رابط فقط

                        ViewBag.ImageUrls = new List<string> { firstImageUrl }; // تخزين الرابط في قائمة جديدة
                    }

                }

                var imageNodes2 = document.DocumentNode.SelectNodes("//div[@class='price-format__large price-format__main-price']");
                if (imageNodes2 != null)
                {
                    // استخدم النص من العقدة بدلاً من العقدة نفسها
                    ViewBag.html = imageNodes2.Select(node => node.InnerText).ToArray();
                    // استخدم النص من العقدة بدلاً من العقدة نفسها

                }
                else
                {
                    var pricePartsNodes = document.DocumentNode.SelectNodes("//div[@class='price-format__main-price']//span");
                    if (pricePartsNodes != null && pricePartsNodes.Count >= 4)
                    {
                        // استخدم أول أربعة عناصر فقط لتكوين السعر الأول
                        var price = string.Join("", pricePartsNodes.Take(4).Select(node => node.InnerText.Trim()));
                        ViewBag.html1 = price;
                    }


                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ListProduct(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return View("myLodeHtml", new List<ViewmMODeElMASTER>());
            }


            string htmlContent = string.Empty;

            try
            {
                htmlContent = await LoadHtmlContent(url, 30); // محاولة أولى بمهلة 30 ثانية
            }
            catch (TaskCanceledException)
            {
                try
                {
                    htmlContent = await LoadHtmlContent(url, 120); // إعادة المحاولة بمهلة 120 ثانية
                }
                catch (TaskCanceledException ex)
                {
                    // قم بمعالجة الاستثناء هنا، مثل تسجيل الخطأ أو عرض رسالة للمستخدم
                    return View("myLodeHtml", new List<ViewmMODeElMASTER>());
                }
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            var products = new List<ViewmMODeElMASTER>();

            var rows = doc.DocumentNode.SelectNodes("//table/tbody/tr");
            if (rows != null)
            {
                foreach (var row in rows)
                {
                    var cells = row.SelectNodes("td").Select(td => td.InnerText.Trim()).ToList();
                    if (cells.Count == 8)
                    {
                        products.Add(new ViewmMODeElMASTER
                        {
                            ProductName = cells[0],
                            Make = cells[1],
                            Model = cells[2],
                            UPC = cells[3],
                            Quantity = int.Parse(cells[4]),
                            RetailPrice = cells[5],
                            TotalRetailPrice = cells[6]
                        });
                    }
                }
            }

            return View("myLodeHtml", products);
        }

        private async Task<string> LoadHtmlContent(string url, int timeoutInSeconds)
        {
            using (var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(timeoutInSeconds) })
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36");
                return await httpClient.GetStringAsync(url);
            }
        }

        public IActionResult test(string model)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            WebRequest wreq = WebRequest.Create("https://www.lowes.com/search?searchTerm=" + model);
            HttpWebResponse wresp = (HttpWebResponse)wreq.GetResponse();
            Stream dStream = wresp.GetResponseStream();
            StreamReader rdr = new StreamReader(dStream);
            string respStr = rdr.ReadToEnd();
            ViewBag.Model = respStr;
            return View();


        }

    }

}



