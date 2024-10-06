﻿using System.Drawing.Imaging;
using System.Drawing;
using ZXing.QrCode;
using ZXing;
using System.Net;
using System.Runtime.InteropServices;
namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductInformationlowesController : Controller
    {
        MasterDbcontext dbcontext;
        IIProductCategory iProductCategory;
        IIProductInformation iProductInformation;
        IITypesProduct iTypesProduct;
        IIBrandName iBrandName;
        IIScrapingHtmlTitle iScrapingHtmlTitle;
        public ProductInformationlowesController(MasterDbcontext dbcontext1, IIProductCategory iProductCategory1, IIProductInformation iProductInformation1, IITypesProduct iTypesProduct1, IIBrandName iBrandName1, IIScrapingHtmlTitle iScrapingHtmlTitle1)
        {
            dbcontext = dbcontext1;
            iProductCategory = iProductCategory1;
            iProductInformation = iProductInformation1;
            iTypesProduct = iTypesProduct1;
            iBrandName = iBrandName1;
            iScrapingHtmlTitle = iScrapingHtmlTitle1;
        }
        public IActionResult MYProductInformation()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewProductInformation = iProductInformation.GetAll();
            return View(vmodel);
        }
        public IActionResult AddEditProductInformation(int? IdProductInformation)
        {
            ViewBag.Category = iProductCategory.GetAll();
            ViewBag.TypesProduct = iTypesProduct.GetAll();
            ViewBag.BrandName = iBrandName.GetAll();
            ViewBag.ScrapingHtmlTitle = iScrapingHtmlTitle.GetAll();
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ProductInformation = new TBProductInformation();
            vmodel.ListViewProductInformation = iProductInformation.GetAll();
            if (IdProductInformation != null)
            {
                vmodel.ProductInformation = iProductInformation.GetById(Convert.ToInt32(IdProductInformation));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        public IActionResult AddEditProductInformationImage(int? IdProductInformation)
        {
            ViewBag.Category = iProductCategory.GetAll();
            ViewBag.TypesProduct = iTypesProduct.GetAll();
            ViewBag.BrandName = iBrandName.GetAll();
            ViewBag.ScrapingHtmlTitle = iScrapingHtmlTitle.GetAll();
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

            vmodel.ListViewProductInformation = iProductInformation.GetAll();

            if (IdProductInformation != null)
            {
                var productInfo = iProductInformation.GetById(Convert.ToInt32(IdProductInformation));
                if (productInfo != null)
                {
                    vmodel.ProductInformation = productInfo;
                }
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBProductInformation slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdProductInformation = model.ProductInformation.IdProductInformation;
                slider.IdProductCategory = model.ProductInformation.IdProductCategory;
                slider.IdTypesProduct = model.ProductInformation.IdTypesProduct;
                slider.IdScrapingHtmlTitle = model.ProductInformation.IdScrapingHtmlTitle;
                slider.ProductName = model.ProductInformation.ProductName;
                slider.storeSku = model.ProductInformation.storeSku;
                slider.storeSoSku = model.ProductInformation.storeSoSku;
                slider.brand = model.ProductInformation.brand;
                slider.UPC = model.ProductInformation.UPC;
                slider.Qrcode = model.ProductInformation.Qrcode;
                slider.Active = model.ProductInformation.Active;
                slider.DateTimeEntry = model.ProductInformation.DateTimeEntry;
                slider.DataEntry = model.ProductInformation.DataEntry;
                slider.CurrentState = model.ProductInformation.CurrentState;
                slider.Model = model.ProductInformation.Model;
                slider.Photo = model.ProductInformation.Photo;
                if (slider.Photo == null)
                {
                    var file = Files.FirstOrDefault(); // Assuming a single file upload for the image
                    if (file != null && file.Length > 0)
                    {
                        // Generate a unique filename
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                        // Save the file to the server
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Save the filename in the database
                        slider.Photo = "/Images/Product/" + fileName;
                    }
                    else if (slider.IdProductInformation == 0 || string.IsNullOrEmpty(slider.Photo))
                    {
                        TempData["Message"] = "Please upload an image."; // Message indicating that an image upload is required
                        return RedirectToAction("AddEditProductInformation");
                    }
                }
                // Save or update the product information
                if (slider.IdProductInformation == 0)
                {
                    if (dbcontext.TBProductInformations.Any(a => a.ProductName == slider.ProductName && a.IdTypesProduct == slider.IdTypesProduct && a.IdProductCategory == slider.IdProductCategory))
                    {
                        TempData["ProductName"] = "Product already exists.";
                        return RedirectToAction("AddEditProductInformation", model);
                    }
                    dbcontext.TBProductInformations.Add(slider);

                    var reqwest = iProductInformation.saveData(slider);
                    if (reqwest== true)
                    {
                        TempData["Saved successfully"] = "Saved successfully.";
                        return RedirectToAction("MYProductInformation");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddEditProductInformation");
                    }



                }
                else
                {
                    dbcontext.TBProductInformations.Update(slider);
                }
                await dbcontext.SaveChangesAsync();

                TempData["Saved successfully"] = "Saved successfully.";
                return RedirectToAction("MYProductInformation");
            }
            catch (Exception ex)
            {
                TempData["ErrorSave"] = "Error saving data: " + ex.Message;
                return RedirectToAction("AddEditProductInformation", model);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveModal(ViewmMODeElMASTER model, TBProductInformation slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdProductInformation = model.ProductInformation.IdProductInformation;
                slider.IdProductCategory = model.ProductInformation.IdProductCategory;
                slider.IdTypesProduct = model.ProductInformation.IdTypesProduct;
                slider.ProductName = model.ProductInformation.ProductName;
                slider.storeSku = model.ProductInformation.storeSku;
                slider.storeSoSku = model.ProductInformation.storeSoSku;
                slider.brand = model.ProductInformation.brand;          
                slider.UPC = model.ProductInformation.UPC;
                slider.Qrcode = model.ProductInformation.Qrcode;
                slider.Active = model.ProductInformation.Active;
                slider.DateTimeEntry = model.ProductInformation.DateTimeEntry;
                slider.DataEntry = model.ProductInformation.DataEntry;
                slider.CurrentState = model.ProductInformation.CurrentState;
                slider.Model = model.ProductInformation.Model;
                slider.Photo = "model.ProductInformation.Photo";
                if (slider.Photo == null)
                {
                    var file = Files.FirstOrDefault(); // Assuming a single file upload for the image
                    if (file != null && file.Length > 0)
                    {
                        // Generate a unique filename
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        // Save the file to the server
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        // Save the filename in the database
                        slider.Photo = "/Images/Product/" + fileName;
                    }
                    else if (slider.IdProductInformation == 0 || string.IsNullOrEmpty(slider.Photo))
                    {
                        TempData["Message"] = "Please upload an image."; // Message indicating that an image upload is required
                        return RedirectToAction("AddEditProductInformation");
                    }
                }               // Save or update the product information
                if (slider.IdProductInformation == 0)
                {
                    if (dbcontext.TBProductInformations.Any(a => a.ProductName == slider.ProductName && a.IdTypesProduct == slider.IdTypesProduct && a.IdProductCategory == slider.IdProductCategory))
                    {
                        TempData["ProductName"] = "Product already exists.";
                        return RedirectToAction("AddEditProductInformation", model);
                    }
                    dbcontext.TBProductInformations.Add(slider);
                }
                else
                {
                    dbcontext.TBProductInformations.Update(slider);
                }
                await dbcontext.SaveChangesAsync();
                TempData["Saved successfully"] = "Saved successfully.";
                return RedirectToAction("AddOrder", "Order", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                TempData["ErrorSave"] = "Error saving data: " + ex.Message;
                return RedirectToAction("AddOrder", "Order", new { area = "Admin" });
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdProductInformation)
        {
            var reqwistDelete = iProductInformation.deleteData(IdProductInformation);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYProductInformation");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYProductInformation");
            }
        }
        

        [HttpGet]
        public async Task<IActionResult> FetchImageByModel(string model, string breand)
        {
            if (breand == "home depot")
            {
                try
                {
                    //HtmlWeb web = new HtmlWeb();
                    //var document = web.Load("https://www.homedepot.com/s/" + model);


                    //var imageNodes = document.DocumentNode.SelectNodes("//div[@class='mediagallery']//img");

                    //if (imageNodes != null && imageNodes.Any())
                    //{
                    //    var imageUrl = imageNodes.Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();

                    //    // الحصول على اسم المنتج من h1 داخل div
                    //    var productNode = document.DocumentNode.SelectSingleNode("//div[@class='product-details__badge-title--wrapper--vtpd5']//h1");
                    //    var productName = productNode != null ? productNode.InnerText.Trim() : "Unknown Product";

                    //    return Json(new { success = true, imageUrl, productName });
                    //}
                    HtmlWeb web = new HtmlWeb();
                    var document = web.Load("https://www.homedepot.com/s/" + model);

                    var imageNodes = document.DocumentNode.SelectNodes("//div[@class='mediagallery']//img");

                    if (imageNodes != null && imageNodes.Any())
                    {
                        var imageUrl = imageNodes.Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                        // الحصول على اسم المنتج من h1 داخل div
                        var productNode = document.DocumentNode.SelectSingleNode("//div[@class='product-details__badge-title--wrapper--vtpd5']//h1");
                        var productName = productNode != null ? productNode.InnerText.Trim() : "Unknown Product";
                        // الحصول على Store SKU من div
                        var skuNode = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'sui-flex sui-inline-flex sui-mr-2')]//h2//span");
                        var storeSku = skuNode != null ? skuNode.InnerText.Trim() : "Unknown SKU";
                        // الحصول على Store SO SKU من h2
                        var soSkuNode = document.DocumentNode.SelectSingleNode("//h2[contains(text(), 'Store SO SKU')]/span");
                        var storeSoSku = soSkuNode != null ? soSkuNode.InnerText.Trim() : "Unknown SO SKU";
                        // الحصول على العلامة التجارية (RYOBI) من div
                        var brandNode = document.DocumentNode.SelectSingleNode("//div[@class='sui-pr-2 sui-inline-flex']//h2");
                        var brand = brandNode != null ? brandNode.InnerText.Trim() : "Unknown Brand";
                        //الحصول على المودل 
                        var modelNode = document.DocumentNode.SelectSingleNode("//div[@class='sui-flex sui-inline-flex sui-mr-2']//h2[contains(text(), 'Model #')]/span");
                        var modelNumber = modelNode != null ? modelNode.InnerText.Trim() : "Unknown Model";
                        return Json(new { success = true, imageUrl, productName, storeSku, storeSoSku, brand, modelNumber });
                    }

                    //else
                    //{
                    //    return Json(new { success = false, message = "No images found" });
                    //}

                    else
                    {
                        // مصدر الصورة البديل إذا كان المصدر الرئيسي غير متاح
                        //var imageNodesFallback = document.DocumentNode.SelectNodes("//div[@data-testid='product-image__wrapper']//img");

                        //if (imageNodesFallback != null && imageNodesFallback.Any())
                        //{
                        //    var firstImageUrl = imageNodesFallback
                        //        .Select(node => node.GetAttributeValue("src", ""))
                        //        .FirstOrDefault();

                        //    // الحصول على اسم المنتج
                        //    // تعديل XPath ليكون أكثر عمومية
                        //    var productNode = document.DocumentNode.SelectSingleNode("//h3[contains(@class, 'sui-text-primary') and contains(@class, 'sui-text-ellipsis')]");
                        //    var productName = productNode != null ? productNode.InnerText.Trim() : "Unknown Product";

                        //    return Json(new { success = true, imageUrl = firstImageUrl, productName });

                        //}

                        // مصدر الصورة البديل إذا كان المصدر الرئيسي غير متاح
                        var imageNodesFallback = document.DocumentNode.SelectNodes("//div[@data-testid='product-image__wrapper']//img");

                        if (imageNodesFallback != null && imageNodesFallback.Any())
                        {
                            var firstImageUrl = imageNodesFallback
                                .Select(node => node.GetAttributeValue("src", ""))
                                .FirstOrDefault();

                            // الحصول على اسم المنتج
                            var productNode = document.DocumentNode.SelectSingleNode("//h3[contains(@class, 'sui-text-primary') and contains(@class, 'sui-text-ellipsis')]");
                            var productName = productNode != null ? productNode.InnerText.Trim() : "Unknown Product";

                            // الحصول على الموديل
                            var modelNode = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'sui-flex sui-text-xs sui-mb-1 sui-mr-1')]");
                            var modelNumber = modelNode != null ? modelNode.InnerText.Replace("Model#", "").Trim() : "Unknown Model";



                            var brandNode = document.DocumentNode.SelectSingleNode("//p[@data-testid='attribute-brandname-above']");
                            var brand = brandNode != null ? brandNode.InnerText.Trim() : "Unknown Brand";


                            return Json(new { success = true, imageUrl = firstImageUrl, productName, modelNumber, brand });
                        }

                    }

                    // If no image was found
                    return Json(new { success = false, message = "Image not found." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.Timeout = TimeSpan.FromSeconds(30); // تعيين المهلة إلى 30 ثانية

                        // تعيين وكيل المستخدم ليبدو كأنه متصفح حقيقي
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");

                        var searchUrl = "https://www.lowes.com/search?searchTerm=" + model;
                        var response = await client.GetAsync(searchUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            // الحصول على الرابط الجديد بعد إعادة التوجيه
                            var redirectedUrl = response.RequestMessage.RequestUri.ToString();

                            // تحميل محتوى الصفحة الجديدة
                            var pageContents = await response.Content.ReadAsStringAsync();
                            var document = new HtmlDocument();
                            document.LoadHtml(pageContents);

                            // محاولة استخراج اسم المنتج
                            var productTitleNode = document.DocumentNode.SelectSingleNode("//h1[@class='styles__H1-sc-11vpuyu-0 krJSUv typography variant--h1 align--left product-brand-description']");
                            string productName = productTitleNode != null ? productTitleNode.InnerText.Trim() : "Product name not found";

                            // محاولة استخراج الصور من العنصر المطلوب
                            var imageNodes = document.DocumentNode.SelectNodes("//div[@class='ImageContainerstyles__ImageTileWrapper-sc-1l8vild-0 cfyBCn tile']//img");

                            if (imageNodes != null && imageNodes.Any())
                            {
                                var firstImageUrl = imageNodes
                                   .Select(node => node.GetAttributeValue("src", ""))
                                   .FirstOrDefault(src => !string.IsNullOrEmpty(src));

                                if (!string.IsNullOrEmpty(firstImageUrl))
                                {
                                    return Json(new { success = true, imageUrl = firstImageUrl, productName, redirectedUrl });
                                }
                                else
                                {
                                    // إذا لم يتم العثور على أي صورة
                                    return Json(new { success = false, message = "Image not found in the specified div.", productName, redirectedUrl });
                                }
                            }
                            else
                            {
                                var imageNodes2 = document.DocumentNode.SelectNodes("//img");
                                // إذا لم يتم العثور على أي صورة
                                if (imageNodes2 != null && imageNodes2.Any())
                                {
                                    var firstImageUrl = imageNodes2
                                       .Select(node => node.GetAttributeValue("src", ""))
                                       .FirstOrDefault(src => !string.IsNullOrEmpty(src));

                                    if (!string.IsNullOrEmpty(firstImageUrl))
                                    {
                                        return Json(new { success = true, imageUrl = firstImageUrl, productName, redirectedUrl });
                                    }
                                    else
                                    {
                                        // إذا لم يتم العثور على أي صورة
                                        return Json(new { success = false, message = "Image not found in the specified div.", productName, redirectedUrl });
                                    }
                                }

                                return Json(new { success = false, message = "Image not found in the specified div.", productName, redirectedUrl });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, message = "Failed to load the page." });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
        public IActionResult GenerateQRCode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Content("No text provided");

            // إعداد خيارات التشفير
            var encodingOptions = new QrCodeEncodingOptions
            {
                Width = 200,
                Height = 200,
                Margin = 1
            };
            // إنشاء كائن BarcodeWriter
            var barcodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = encodingOptions
            };
            // توليد الصورة
            var pixelData = barcodeWriter.Write(text);
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
                IntPtr ptr = bitmapData.Scan0;
                System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, ptr, pixelData.Pixels.Length);
                bitmap.UnlockBits(bitmapData);
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    return File(stream.ToArray(), "image/png");
                }
            }
        }
        [HttpGet]
        public IActionResult PrintWareHouseDetails(string Category, string TypesProduct, string Product, string Model, string UPC, string qrCodeSrc, string brandName)
        {
            var htmlContent = new StringBuilder();
            htmlContent.Append("<html><head><title>Print QR Code</title></head><body>");
            htmlContent.AppendFormat("<h1>Category: {0}</h1>", Category);
            htmlContent.AppendFormat("<h2>Product Type: {0}</h2>", TypesProduct);
            htmlContent.AppendFormat("<h2>BrandName Type: {0}</h2>", brandName);
            htmlContent.AppendFormat("<h3>Model: {0}</h3>", Model);
            htmlContent.AppendFormat("<h3>Product: {0}</h3>", Product);
            htmlContent.AppendFormat("<h3>UPC: {0}</h3>", UPC);
            htmlContent.AppendFormat("<img src='{0}' alt='UPC' />", qrCodeSrc);
            htmlContent.Append("</body></html>");
            return Content(htmlContent.ToString(), "text/html", Encoding.UTF8);
        }

        public IActionResult GenerateBarcode(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest("No text provided for barcode generation.");
            }


            var barcodeOptions = new ZXing.Common.EncodingOptions
            {
                Width = 300,
                Height = 50, // يمكنك تعديل هذه القيمة وفقًا لاحتياجاتك
                Margin = 10
            };

            var barcodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.CODE_128,
                Options = barcodeOptions
            };

            try
            {
                var pixelData = barcodeWriter.Write(text);

                // إنشاء صورة Bitmap من بيانات البكسل
                using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
                {
                    var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                    try
                    {
                        Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    using (var stream = new MemoryStream())
                    {
                        bitmap.Save(stream, ImageFormat.Png);
                        return File(stream.ToArray(), "image/png");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error generating barcode: " + ex.Message);
            }
        }


    }
}
