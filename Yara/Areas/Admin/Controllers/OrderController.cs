using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;
using System.Drawing;
using ZXing.QrCode;
using ZXing;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Primitives;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        IIOrder iOrder;
        IIBondType iBondType;
        IIMerchants iMerchants;
        IIProductCategory iProductCategory;
        IITypesProduct iTypesProduct;
        IIProductInformation iProductInformation;
        IIWareHouse iWareHouse;
        IIWareHouseBranch iWareHouseBranch;
        MasterDbcontext dbcontext;
        IIBrandName iBrandName;
        IICompanyInformation iCompanyInformation;

        public OrderController(IIOrder iOrder1, IIBondType iBondType1, IIMerchants iMerchants1, IIProductCategory iProductCategory1, IITypesProduct iTypesProduct1,
            IIProductInformation iProductInformation1, IIWareHouse iWareHouse1, IIWareHouseBranch iWareHouseBranch1, MasterDbcontext dbcontext1, IIBrandName iBrandName1, IICompanyInformation iCompanyInformation1)
        {
            iOrder = iOrder1;
            iBondType = iBondType1;
            iMerchants = iMerchants1;
            iProductCategory = iProductCategory1;
            iTypesProduct = iTypesProduct1;
            iProductInformation = iProductInformation1;
            iWareHouse = iWareHouse1;
            iWareHouseBranch = iWareHouseBranch1;
            dbcontext = dbcontext1;
            iBrandName = iBrandName1;
            iCompanyInformation = iCompanyInformation1;
        }
        public IActionResult MyOrder()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewOrder = iOrder.GetAll();
            vmodel.ListViewProductInformation = iProductInformation.GetAll();
            return View(vmodel);
        }
        public IActionResult AddOrder(int? IdPurchaseOrder)
        {
            ViewBag.BrandName = iBrandName.GetAll();
            ViewBag.Category = iProductCategory.GetAll();
            ViewBag.TypesProduct = iTypesProduct.GetAll();

            ViewBag.BondType = iBondType.GetAll();
            ViewBag.Merchants = iMerchants.GetAll();
            ViewBag.ProductCategory = iProductCategory.GetAll();
            ViewBag.TypesProduct = iTypesProduct.GetAll();
            ViewBag.ProductInformation = iProductInformation.GetAll();
            ViewBag.WareHouse = iWareHouse.GetAll();
            ViewBag.WareHouseBranch = iWareHouseBranch.GetAll();
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewOrder = iOrder.GetAll();
            vmodel.ListViewProductInformation = iProductInformation.GetAll();
            if (IdPurchaseOrder != null)
            {
                vmodel.Order = iOrder.GetById(Convert.ToInt32(IdPurchaseOrder));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        public IActionResult PrepareText()
        {
            return View();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBOrder slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdPurchaseOrder = model.Order.IdPurchaseOrder;
                slider.IdBondType = model.Order.IdBondType;
                slider.IdMerchants = model.Order.IdMerchants;
                slider.IdProductCategory = model.Order.IdProductCategory;
                slider.IdTypesProduct = model.Order.IdTypesProduct;
                slider.IdProductInformation = model.Order.IdProductInformation;
                slider.IdBWareHouse = model.Order.IdBWareHouse;
                slider.IdBWareHouseBranch = model.Order.IdBWareHouseBranch;
                slider.PurchaseAuotNoumber = model.Order.PurchaseAuotNoumber;
                slider.PurchaseOrderNoumber = model.Order.PurchaseOrderNoumber;
                slider.PurchasePrice = model.Order.PurchasePrice;
                slider.sellingPrice = model.Order.sellingPrice;
                slider.GlobalPrice = model.Order.GlobalPrice;
                slider.SpecialSalePrice = model.Order.SpecialSalePrice;
                slider.QuantityIn = model.Order.QuantityIn;
                slider.QuantityOute = model.Order.QuantityOute;
                slider.Qrcode = model.Order.Qrcode;
                slider.DataEntry = model.Order.DataEntry;
                slider.DateTimeEntry = model.Order.DateTimeEntry;
                slider.CurrentState = model.Order.CurrentState;
                //Conditions
                var maxPurchaseAutoNumber = dbcontext.TBOrders.Max(o => (int?)o.PurchaseAuotNoumber) ?? 0;
                slider.PurchaseAuotNoumber = maxPurchaseAutoNumber + 1;
                if (slider.GlobalPrice == null)
                    slider.GlobalPrice = 0;
                if (slider.SpecialSalePrice == null)
                    slider.SpecialSalePrice = 0;
                if (slider.QuantityOute == null)
                    slider.QuantityOute = 0;

                if (slider.IdPurchaseOrder == 0 || slider.IdPurchaseOrder == null)
                {
                    var reqwest = iOrder.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyOrder");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iOrder.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyOrder");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return Redirect(returnUrl);
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return Redirect(returnUrl);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdPurchaseOrder)
        {
            var reqwistDelete = iOrder.deleteData(IdPurchaseOrder);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyOrder");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyOrder");

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




        //    [HttpGet]
        //    public IActionResult PrintWareHouseDetails(string Merchant, string WareHouse,
        //string PurchaseOrderNoumber, string ProductInformation,
        //string WareHouseBranch, string sellingPrice,
        //string QouantityIn,
        //string PurchasePrice,
        //string SpecialSalePrice,
        //string BondType,
        //string qrCodeSrc,
        //string bar)

        //    {



        //        var htmlContent = new StringBuilder();

        //        // Start HTML content with styles
        //        htmlContent.Append("<html><head><title>Print Label</title>");
        //        htmlContent.Append("<style>");
        //        htmlContent.Append("body {font-family: Arial, sans-serif; font-size: 12px;}");
        //        htmlContent.Append(".label-container { border: 1px solid #000; width: 300px; padding: 10px; box-sizing: border-box; }");
        //        htmlContent.Append(".section { margin-bottom: 10px; border: 1px solid #000; padding: 10px; }");
        //        htmlContent.Append(".header { display: flex; justify-content: space-between; align-items: center; margin-bottom: 10px; padding-bottom: 5px; border-bottom: 1px solid #000; }");
        //        htmlContent.Append(".logo { width: 60px; height: 60px; }");
        //        htmlContent.Append(".text-container { flex-grow: 1; text-align: right; font-size: 16px; }");
        //        htmlContent.Append(".address-section, .product-details { margin-top: 10px; }");
        //        htmlContent.Append(".barcode { display: block; margin: 10px auto; width: 250px; height: 40px; }");
        //        htmlContent.Append(".footer { display: flex; justify-content: space-between; align-items: center; padding-top: 5px; border-top: 1px solid #000; }");
        //        htmlContent.Append(".qr-code { width: 80px; height: 80px; margin-right: 10px; }");
        //        htmlContent.Append("</style>");
        //        htmlContent.Append("</head><body>");

        //        // Container for the label
        //        htmlContent.Append("<div class='label-container'>");

        //        // Header with Logo and Priority Mail text
        //        htmlContent.Append("<div class='header'>");
        //        htmlContent.AppendFormat("<img class='logo' src='{0}' alt='Company Logo' />", "/Images/Home/company-logo.png");
        //        htmlContent.AppendFormat("<div class='text-container'>{0}™</div>", Merchant);
        //        htmlContent.Append("</div>");

        //        // Address and Order Info
        //        htmlContent.Append("<div class='section address-section'>");
        //        htmlContent.AppendFormat("<p>{0}</p>", Merchant);
        //        htmlContent.AppendFormat("<p>{0}</p>", WareHouse);
        //        htmlContent.AppendFormat("<p>Purchase Order #: {0}</p>", PurchaseOrderNoumber);
        //        htmlContent.AppendFormat("<p>Warehouse Branch: {0}</p>", WareHouseBranch);
        //        htmlContent.Append("</div>");

        //        // Product Details Section
        //        htmlContent.Append("<div class='section product-details'>");
        //        htmlContent.AppendFormat("<p>Product: {0}</p>", ProductInformation);
        //        htmlContent.AppendFormat("<p>Selling Price: {0}</p>", sellingPrice);
        //        htmlContent.AppendFormat("<p>Quantity In: {0}</p>", QouantityIn);
        //        htmlContent.AppendFormat("<p>Purchase Price: {0}</p>", PurchasePrice);
        //        htmlContent.AppendFormat("<p>Special Sale Price: {0}</p>", SpecialSalePrice);
        //        htmlContent.AppendFormat("<p>Bond Type: {0}</p>", BondType);
        //        htmlContent.Append("</div>");

        //        // QR Code and Barcode in Footer
        //        htmlContent.Append("<div class='section footer'>");
        //        htmlContent.AppendFormat("<img class='qr-code' src='{0}' alt='QR Code' />", qrCodeSrc);
        //        htmlContent.AppendFormat("<img class='barcode' src='{0}' alt='Barcode' />", bar);
        //        htmlContent.Append("</div>");

        //        // Close container div
        //        htmlContent.Append("</div>");

        //        // End HTML content
        //        htmlContent.Append("</body></html>");

        //        // Return the formatted content as an HTML page
        //        return Content(htmlContent.ToString(), "text/html", Encoding.UTF8);
        //    }


        [HttpGet]
        public IActionResult PrintWareHouseDetails(string Merchant, string WareHouse,
    string PurchaseOrderNoumber, string ProductInformation,
    string WareHouseBranch, string sellingPrice,
    string QouantityIn,
    string PurchasePrice,
    string SpecialSalePrice,
    string BondType,
    string qrCodeSrc,
    string bar, string upc)
        {
            string photo = string.Empty;
            string PhoneNumber = string.Empty;
            string Address = string.Empty;
            string CompanyName = string.Empty;

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

            // Fetch company information and set it in the ViewModel
            vmodel.ListCompanyInformatione = iCompanyInformation.GetAll().ToList();

            if (vmodel.ListCompanyInformatione != null && vmodel.ListCompanyInformatione.Any())
            {
                var company = vmodel.ListCompanyInformatione.FirstOrDefault();
                CompanyName = company.CompanyName;
                PhoneNumber = company.PhoneNumber;
                photo = company.Photo;
                Address = company.AddressEn;
            }

            var htmlContent = new StringBuilder();

            // Start HTML content with styles
            htmlContent.Append("<html><head><title>Print Label</title>");
            htmlContent.Append("<style>");
            htmlContent.Append("body {font-family: Arial, sans-serif; font-size: 12px;}");
            htmlContent.Append(".label-container { border: 1px solid #000; width: 231px; padding: 10px; box-sizing: border-box; }");
            htmlContent.Append(".section { margin-bottom: 10px; border: 1px solid #000; padding: 5px; }");
            htmlContent.Append(".header { display: flex; justify-content: space-between; align-items: center; margin-bottom: 10px; padding-bottom: 5px; border-bottom: 1px solid #000; }");
            htmlContent.Append(".logo { width: 50px; height: 50px; }");
            htmlContent.Append(".text-container { flex-grow: 1; text-align: right; font-size: 14px; }");
            htmlContent.Append(".address-section, .product-details { margin-top: 10px; font-size: 12px; }");
            htmlContent.Append(".footer { display: flex; flex-direction: column; align-items: center; padding-top: 5px; border-top: 1px solid #000; }");
            htmlContent.Append(".qr-code { width: 80px; height: 80px; margin-bottom: 5px; }");
            htmlContent.Append(".barcode { width: 180px; height: 40px; margin-top: 5px; }");
            htmlContent.Append(".upc { text-align: center; margin-top: 5px; font-size: 14px; }");
            htmlContent.Append("</style>");
            htmlContent.Append("</head><body>");

            // Container for the label
            htmlContent.Append("<div class='label-container'>");

            // Header with Logo and Company Name text
            htmlContent.Append("<div class='header'>");
            htmlContent.AppendFormat("<img class='logo' src='/Images/Home/{0}' alt='Company Logo' />", photo);
            htmlContent.AppendFormat("<div class='text-container'>{0}™</div>", CompanyName);
            htmlContent.Append("</div>");

            // Address and Order Info
            htmlContent.Append("<div class='section address-section'>");
            htmlContent.AppendFormat("<p>Merchant#: {0}</p>", Merchant);
            htmlContent.AppendFormat("<p>WareHouse#: {0}</p>", WareHouse);
            htmlContent.AppendFormat("<p>Warehouse Branch: {0}</p>", WareHouseBranch);
            htmlContent.AppendFormat("<p>Purchase Order #: {0}</p>", PurchaseOrderNoumber);
            htmlContent.Append("</div>");

            // Product Details Section
            htmlContent.Append("<div class='section product-details'>");
            htmlContent.AppendFormat("<p>Product: {0}</p>", ProductInformation);
            htmlContent.AppendFormat("<p>Selling Price: {0}</p>", sellingPrice);
            htmlContent.Append("</div>");

            // QR Code, Barcode, and UPC in Footer
            htmlContent.Append("<div class='section footer'>");

            // QR code on top
            htmlContent.AppendFormat("<img class='qr-code' src='{0}' alt='QR Code' />", qrCodeSrc);

            // Barcode in the middle
            htmlContent.AppendFormat("<img class='barcode' src='{0}' alt='Barcode' />", bar);

            // Display UPC number directly under the barcode
            if (!string.IsNullOrEmpty(upc))
            {
                htmlContent.AppendFormat("<div class='upc'>{0}</div>", upc);
            }
            else
            {
                htmlContent.Append("<div class='upc'>UPC: N/A</div>");
            }

            htmlContent.Append("</div>"); // End of footer section

            // Close container div
            htmlContent.Append("</div>");

            // End HTML content
            htmlContent.Append("</body></html>");

            // Return the formatted content as an HTML page
            return Content(htmlContent.ToString(), "text/html", Encoding.UTF8);
        }









        //    [HttpGet]
        //    public IActionResult PrintWareHouseDetails(string Merchant, string WareHouse,
        //string PurchaseOrderNoumber, string ProductInformation,
        //string WareHouseBranch, string sellingPrice,
        //string QouantityIn,
        //string PurchasePrice,
        //string SpecialSalePrice,
        //string BondType,
        //string qrCodeSrc,
        //string bar)
        //    {
        //        var htmlContent = new StringBuilder();

        //        // Start HTML content with styles
        //        htmlContent.Append("<html><head><title>Print Label</title>");
        //        htmlContent.Append("<style>");
        //        htmlContent.Append("body {font-family: Arial, sans-serif; font-size: 10px;}"); // Adjusted font size
        //        htmlContent.Append(".label-container { border: 1px solid #000; width: 230px; height: 230px; padding: 10px; }"); // Adjusted container to 2.4 inch equivalent in pixels
        //        htmlContent.Append("h1 { font-size: 14px; margin-bottom: 5px; }"); // Adjusted font size
        //        htmlContent.Append("h2, h3 { font-size: 10px; margin-bottom: 5px; }"); // Adjusted font size
        //        htmlContent.Append(".section { margin-bottom: 5px; border-bottom: 1px solid #000; padding-bottom: 5px; }");
        //        htmlContent.Append(".header { display: flex; justify-content: space-between; align-items: center; }");
        //        htmlContent.Append(".qr-code { width: 50px; height: 50px; margin-right: 10px; }"); // Adjusted QR code size
        //        htmlContent.Append(".text-container { flex-grow: 1; text-align: left; }");
        //        htmlContent.Append(".barcode { display: block; margin: 0 auto; }"); // Centered barcode
        //        htmlContent.Append(".footer-barcode { margin-top: 5px; text-align: center; }"); // Adjusted footer layout
        //        htmlContent.Append("</style>");
        //        htmlContent.Append("</head><body>");

        //        // Container for the label
        //        htmlContent.Append("<div class='label-container'>");

        //        // Header with QR code on the right and Priority Mail title on the left
        //        htmlContent.Append("<div class='section header'>");
        //        htmlContent.Append("<div class='text-container'>");
        //        htmlContent.AppendFormat("<h1>{0}™</h1>", Merchant);
        //        htmlContent.Append("</div>");
        //        htmlContent.AppendFormat("<img class='qr-code' src='{0}' alt='QR Code' />", qrCodeSrc); // QR Code at Top Right
        //        htmlContent.Append("</div>");

        //        // Merchant and Warehouse Info
        //        htmlContent.Append("<div class='section'>");
        //        htmlContent.AppendFormat("<h2>{0}</h2>", WareHouse);
        //        htmlContent.AppendFormat("<h2>Purchase Order #: {0}</h2>", PurchaseOrderNoumber);
        //        htmlContent.AppendFormat("<h3>Warehouse Branch: {0}</h3>", WareHouseBranch);
        //        htmlContent.Append("</div>");

        //        // Product and Price Info
        //        htmlContent.Append("<div class='section'>");
        //        htmlContent.AppendFormat("<h3>Product: {0}</h3>", ProductInformation);
        //        htmlContent.AppendFormat("<h3>Selling Price: {0}</h3>", sellingPrice);
        //        htmlContent.AppendFormat("<h3>Quantity In: {0}</h3>", QouantityIn);
        //        htmlContent.AppendFormat("<h3>Purchase Price: {0}</h3>", PurchasePrice);
        //        htmlContent.AppendFormat("<h3>Special Sale Price: {0}</h3>", SpecialSalePrice);
        //        htmlContent.AppendFormat("<h3>Bond Type: {0}</h3>", BondType);
        //        htmlContent.Append("</div>");

        //        // Footer Barcode
        //        htmlContent.Append("<div class='footer-barcode'>");
        //        htmlContent.AppendFormat("<img class='barcode' src='{0}' alt='Barcode' width='180px' height='30px' />", bar); // Adjusted barcode size
        //        htmlContent.Append("</div>");

        //        // Close container div
        //        htmlContent.Append("</div>");

        //        // End HTML content
        //        htmlContent.Append("</body></html>");

        //        // Return the formatted content as an HTML page
        //        return Content(htmlContent.ToString(), "text/html", Encoding.UTF8);
        //    }















        //[HttpGet]
        //public IActionResult PrintWareHouseDetails(string Merchant, string WareHouse,
        //    string PurchaseOrderNoumber, string ProductInformation,
        //    string WareHouseBranch, string sellingPrice,
        //    string QouantityIn,
        //    string PurchasePrice,
        //    string SpecialSalePrice,
        //    string BondType,
        //    string qrCodeSrc,
        //    string bar)
        //{
        //    var htmlContent = new StringBuilder();

        //    htmlContent.Append("<html><head><title>Print QR Code</title></head><body>");
        //    htmlContent.AppendFormat("<h1>WareHouse Type: {0}</h1>", Merchant);
        //    htmlContent.AppendFormat("<h2>Warehouse: {0}</h2>", WareHouse);
        //    htmlContent.AppendFormat("<h2>PurchaseOrderNoumber: {0}</h2>", PurchaseOrderNoumber);
        //    htmlContent.AppendFormat("<h3>WareHouseBranch: {0}</h3>", WareHouseBranch);
        //    htmlContent.AppendFormat("<h3>ProductInformation: {0}</h3>", ProductInformation);
        //    htmlContent.AppendFormat("<h3>sellingPrice: {0}</h3>", sellingPrice);
        //    htmlContent.AppendFormat("<h3>QouantityIn: {0}</h3>", QouantityIn);
        //    htmlContent.AppendFormat("<h3>PurchasePrice: {0}</h3>", PurchasePrice);
        //    htmlContent.AppendFormat("<h3>SpecialSalePrice: {0}</h3>", SpecialSalePrice);
        //    htmlContent.AppendFormat("<h3>BondType: {0}</h3>", BondType);
        //    htmlContent.AppendFormat("<img src='{0}' alt='QR Code' />", qrCodeSrc);
        //    htmlContent.AppendFormat("<img src='{0}' alt='Bar Code' />", bar);
        //    htmlContent.Append("</body></html>");

        //    return Content(htmlContent.ToString(), "text/html", Encoding.UTF8);
        //}

        //public JsonResult GetProductImageUrl(int id)
        //{
        //    // Fetch the image URL from your data source based on the product ID
        //    var product = dbcontext.TBProductInformations.FirstOrDefault(p => p.IdProductInformation == id);

        //    if (product != null)
        //    {
        //        return Json(new { imageUrl = product.Photo }); // Assuming `ImageUrl` is the property holding the image URL
        //    }
        //    else
        //    {
        //        return Json(new { imageUrl = "http://placehold.it/220x180" }); // Fallback image
        //    }
        //}
        [HttpGet]
        public async Task<JsonResult> GetProductDetails(int id)
        {
            var product = dbcontext.TBProductInformations.FirstOrDefault(p => p.IdProductInformation == id);

            if (product != null)
            {
                // Fetch the global price using HtmlAgilityPack and the mode field
                decimal globalPrice = await FetchGlobalPrice(product.Model);  // Assuming `mode` is the correct property name

                return Json(new
                {
                    imageUrl = product.Photo,
                    globalPrice = globalPrice
                });
            }
            else
            {
                return Json(new
                {
                    imageUrl = "http://placehold.it/220x180", // Fallback image
                    globalPrice = "N/A"
                });
            }
        }

        [HttpGet("GetProductByName")]
        public async Task<IActionResult> GetProductByName(string qrCode)
        {
            if (string.IsNullOrWhiteSpace(qrCode))
            {
                return BadRequest("Product name cannot be empty");
            }

            try
            {
                var product = await dbcontext.TBProductInformations.FirstOrDefaultAsync(p => p.Qrcode.StartsWith(qrCode));
                if (product != null)
                {
                    return Ok(product);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }


            return NotFound("Product not found");



        }



        //private async Task<string> FetchGlobalPrice(string model)
        //{
        //    string globalPrice = "N/A";
        //    try
        //    {
        //        HtmlWeb web = new HtmlWeb();
        //        var document = await web.LoadFromWebAsync("https://www.homedepot.com/s/" + model);

        //        var priceNode = document.DocumentNode.SelectSingleNode("//div[@class='price-format__large price-format__main-price']");
        //        if (priceNode != null)
        //        {
        //            globalPrice = priceNode.InnerText.Trim();
        //        }
        //        else
        //        {
        //            var pricePartsNodes = document.DocumentNode.SelectNodes("//div[@class='price-format__main-price']//span");
        //            if (pricePartsNodes != null && pricePartsNodes.Count >= 4)
        //            {
        //                globalPrice = string.Join("", pricePartsNodes.Take(4).Select(node => node.InnerText.Trim()));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        globalPrice = "Error fetching price";
        //    }

        //    return globalPrice;
        //}
        private async Task<decimal> FetchGlobalPrice(string model)
        {
            decimal globalPrice = 0;
            try
            {
                HtmlWeb web = new HtmlWeb();
                var document = await web.LoadFromWebAsync("https://www.homedepot.com/s/" + model);

                var priceNode = document.DocumentNode.SelectSingleNode("//div[@class='price-format__large price-format__main-price']");
                string priceText = priceNode?.InnerText.Trim();

                if (string.IsNullOrEmpty(priceText))
                {
                    var pricePartsNodes = document.DocumentNode.SelectNodes("//div[@class='price-format__main-price']//span");
                    if (pricePartsNodes != null && pricePartsNodes.Count >= 4)
                    {
                        priceText = string.Join("", pricePartsNodes.Take(4).Select(node => node.InnerText.Trim()));
                    }
                }

                // Remove any non-numeric characters (e.g., currency symbols, commas)
                priceText = Regex.Replace(priceText, "[^0-9.]", "");

                // Convert the cleaned string to a decimal value
                if (!string.IsNullOrEmpty(priceText))
                {
                    globalPrice = Convert.ToDecimal(priceText);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging) and return 0 as a fallback
                globalPrice = 0;
            }

            return globalPrice;
        }

        [HttpGet]
        public IActionResult GetSubWarehouses(int IdBWareHouse)
        {
            var subWarehouses = dbcontext.TBWareHouseBranchs
                .Where(b => b.IdBWareHouse == IdBWareHouse)
                .Select(b => new
                {
                    value = b.IdBWareHouseBranch,
                    text = b.Description
                }).ToList();

            return Json(subWarehouses);
        }

        [HttpGet]
        public async Task<JsonResult> GetProductDetailsForOrder(string productId)
        {
            var product = dbcontext.ViewProductInformation
                         .Where(p => p.Qrcode == productId || p.Model == productId || p.UPC == productId || p.ProductName == productId || p.Make == productId).FirstOrDefault();


            if (product != null)
            {
                // Fetch the global price using HtmlAgilityPack and the model field
                decimal globalPrice = await FetchGlobalPrice(product.Model);

                return Json(new
                {
                    imageUrl = product.Photo,
                    globalPrice = globalPrice,
                    productCategoryId = product.IdProductCategory,
                    bondTypeId = product.IdTypesProduct,  // Assuming this field exists
                    typesProductId = product.IdTypesProduct,
                    productName = product.ProductName,
                    id = product.IdProductInformation,
                    m = product.IdProductInformation
                });
            }
            else
            {
                return Json(new
                {
                    imageUrl = "http://placehold.it/220x180",
                    globalPrice = "0.00",
                    productCategoryId = 0,
                    bondTypeId = 0,
                    typesProductId = 0,
                    id = 0,
                });
            }
        }

        [HttpGet("GetProductSuggestions")]
        public async Task<IActionResult> GetProductSuggestions(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query cannot be empty");
            }
            try
            {
                // Fetch products matching the query
                var products = await dbcontext.ViewProductInformation
                    .Where(p => p.Qrcode.StartsWith(query) ||
                                p.Model.StartsWith(query) ||
                                p.UPC.StartsWith(query) ||
                                p.ProductName.StartsWith(query) ||
                                p.Make.StartsWith(query))
                    .Select(p => new { p.Qrcode, p.ProductName, p.Model, p.Photo })
                    .ToListAsync();

                if (products.Any())
                {
                    return Ok(products); // Return list of matching products
                }
                else
                {
                    return NotFound("No products found");
                }
            }
            catch (Exception ex)
            {
                // Log exception and return error response
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> FetchImageByModel(string model)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                var document = web.Load("https://www.homedepot.com/s/" + model);


                var imageNodes = document.DocumentNode.SelectNodes("//div[@class='mediagallery']//img");

                if (imageNodes != null && imageNodes.Any())
                {
                    var imageUrl = imageNodes.Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();

                    // الحصول على اسم المنتج من h1 داخل div
                    var productNode = document.DocumentNode.SelectSingleNode("//div[@class='product-details__badge-title--wrapper--vtpd5']//h1");
                    var productName = productNode != null ? productNode.InnerText.Trim() : "Unknown Product";

                    return Json(new { success = true, imageUrl, productName });
                }

                else
                {
                    // مصدر الصورة البديل إذا كان المصدر الرئيسي غير متاح
                    var imageNodesFallback = document.DocumentNode.SelectNodes("//div[@data-testid='product-image__wrapper']//img");

                    if (imageNodesFallback != null && imageNodesFallback.Any())
                    {
                        var firstImageUrl = imageNodesFallback
                            .Select(node => node.GetAttributeValue("src", ""))
                            .FirstOrDefault();

                        // الحصول على اسم المنتج
                        // تعديل XPath ليكون أكثر عمومية
                        var productNode = document.DocumentNode.SelectSingleNode("//h3[contains(@class, 'sui-text-primary') and contains(@class, 'sui-text-ellipsis')]");
                        var productName = productNode != null ? productNode.InnerText.Trim() : "Unknown Product";

                        return Json(new { success = true, imageUrl = firstImageUrl, productName });

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



        public async Task<string> GetUPC(int value)
        {

            var product = await iProductInformation.GetByIdFromViewAsync(value);
            if (product == null)
                return "000000000000";

            return product.UPC;
        }


    }
}