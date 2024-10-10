

using Infarstuructre.BL;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using ZXing;
using ZXing.QrCode;

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
        IIUserInformation iUserInformation;
        private readonly UserManager<ApplicationUser> _userManager;
        IIScrapingHtmlTitle iScrapingHtmlTitle;
        IIPurchaseDocumentation iPurchaseDocumentation;

        public OrderController(IIOrder iOrder1, IIBondType iBondType1, IIMerchants iMerchants1, IIProductCategory iProductCategory1, IITypesProduct iTypesProduct1,
            IIProductInformation iProductInformation1, IIWareHouse iWareHouse1, IIWareHouseBranch iWareHouseBranch1, MasterDbcontext dbcontext1, IIBrandName iBrandName1, IICompanyInformation iCompanyInformation1, IIUserInformation iUserInformation1, UserManager<ApplicationUser> userManager, IIScrapingHtmlTitle iScrapingHtmlTitle1,IIPurchaseDocumentation iPurchaseDocumentation1)
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
            iUserInformation = iUserInformation1;
            _userManager = userManager;
            iScrapingHtmlTitle = iScrapingHtmlTitle1;
            iPurchaseDocumentation = iPurchaseDocumentation1;
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

            ViewBag.user = iUserInformation.GetAllByRole("Customer,Admin,Basic");


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

            ViewBag.ScrapingHtmlTitle = iScrapingHtmlTitle.GetAll();
            ViewBag.PurchaseDocumentation = iPurchaseDocumentation.GetAll();


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

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            var IdBondType = model.Order.IdBondType;

            var TAskStatus = vmodel.BondType = iBondType.GetById(IdBondType);
            string BondType = TAskStatus.BondType;
            if (BondType == "Purchase order")
            {
                try
                {
                    slider.IdPurchaseOrder = model.Order.IdPurchaseOrder;
                    slider.IdBondType = model.Order.IdBondType;
                    slider.IdUser = model.Order.IdUser;


                    slider.IdProductInformation = model.Order.IdProductInformation;
                    slider.IdBWareHouse = model.Order.IdBWareHouse;
                    slider.IdBWareHouseBranch = model.Order.IdBWareHouseBranch;
                    slider.PurchaseAuotNoumber = model.Order.PurchaseAuotNoumber;
                    slider.IdPurchaseDocumentation = model.Order.IdPurchaseDocumentation;
                    slider.sellingPrice = model.Order.sellingPrice;
                    slider.GlobalPrice = model.Order.GlobalPrice;
                    slider.QuantityIn = model.Order.QuantityIn;
                    slider.QuantityOute = model.Order.QuantityOute;
                    slider.Qrcode = model.Order.Qrcode;
                    slider.DataEntry = model.Order.DataEntry;
                    slider.DateTimeEntry = model.Order.DateTimeEntry;
                    slider.CurrentState = model.Order.CurrentState;
                    slider.UbcMaster = model.Order.UbcMaster;
                    slider.UbcSacund = model.Order.UbcSacund;
                    //Conditions
                    var maxPurchaseAutoNumber = dbcontext.TBOrders.Max(o => (int?)o.PurchaseAuotNoumber) ?? 0;
                    slider.PurchaseAuotNoumber = maxPurchaseAutoNumber + 1;
                    if (slider.GlobalPrice == null)
                        slider.GlobalPrice = 0;
                    if (slider.QuantityOute == null)
                        slider.QuantityOute = 0;
                    if (slider.IdPurchaseOrder == 0 || slider.IdPurchaseOrder == null)
                    {
                        var reqwest = iOrder.saveData(slider);
                        if (reqwest == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                            TempData["Merchant"] = model.Order.IdUser;
                            return RedirectToAction("AddOrder");
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
            else if (BondType == "Sales invoice")
            {
                try
                {




                    slider.IdPurchaseOrder = model.Order.IdPurchaseOrder;
                    slider.IdBondType = model.Order.IdBondType;
                    slider.IdUser = model.Order.IdUser;


                    slider.IdProductInformation = model.Order.IdProductInformation;
                    slider.IdBWareHouse = model.Order.IdBWareHouse;
                    slider.IdBWareHouseBranch = model.Order.IdBWareHouseBranch;
                    slider.PurchaseAuotNoumber = model.Order.PurchaseAuotNoumber;

                    slider.sellingPrice = model.Order.sellingPrice;
                    slider.GlobalPrice = model.Order.GlobalPrice;

                    slider.QuantityIn = model.Order.QuantityIn;
                    slider.QuantityOute = model.Order.QuantityOute;
                    slider.Qrcode = model.Order.Qrcode;
                    slider.DataEntry = model.Order.DataEntry;
                    slider.DateTimeEntry = model.Order.DateTimeEntry;
                    slider.CurrentState = model.Order.CurrentState;
                    //Conditions
                    var maxPurchaseAutoNumber = dbcontext.TBOrders.Max(o => (int?)o.PurchaseAuotNoumber) ?? 0;
                    slider.PurchaseAuotNoumber = maxPurchaseAutoNumber + 1;

                    slider.GlobalPrice = 0;


                    slider.QuantityIn = 0;


                    if (slider.IdPurchaseOrder == 0 || slider.IdPurchaseOrder == null)
                    {
                        var reqwest = iOrder.saveData(slider);
                        if (reqwest == true)
                        {

                            //Mail Sendig

                            //                var userd = vmodel.sUser = iUserInformation.GetById(slider.IdUser);
                            //                var user = await _userManager.FindByIdAsync(slider.IdUser);
                            //                if (user == null)
                            //                    return NotFound();

                            //                string namedovlober = user.Name;
                            //                string email = user.Email;
                            //                var TAskStatus2 = vmodel.viewOrder = iOrder.GetByIdview(slider.IdPurchaseOrder);
                            //                if (TAskStatus2 == null)
                            //                    return NotFound();

                            //                string ProductName = TAskStatus2.ProductName;
                            //                string Photo = TAskStatus2.Photo;
                            //                string TypesProduct = TAskStatus2.TypesProduct;
                            //                string sellingPrice = TAskStatus2.sellingPrice.ToString();
                            //                string QuantityOute = TAskStatus2.QuantityOute.ToString();
                            //                var total = TAskStatus2.sellingPrice * TAskStatus2.QuantityOute;

                            //                // الحصول على إعدادات البريد الإلكتروني
                            //                var emailSetting = await dbcontext.TBEmailAlartSettings
                            //                   .OrderByDescending(n => n.IdEmailAlartSetting)
                            //                   .Where(a => a.CurrentState == true && a.Active == true)
                            //                   .FirstOrDefaultAsync();

                            //                if (emailSetting != null)
                            //                {
                            //                    var message = new MimeMessage();
                            //                    message.From.Add(new MailboxAddress(slider.DataEntry, emailSetting.MailSender));
                            //                    message.To.Add(new MailboxAddress(namedovlober, email));
                            //                    message.Cc.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            //                    message.Subject = "New Invoice  " + "Number:" + slider.PurchaseAuotNoumber;

                            //                    var builder = new BodyBuilder
                            //                    {
                            //                        HtmlBody = $@"
                            //<!DOCTYPE html>
                            //<html lang='en'>
                            //<head>
                            //    <meta charset='UTF-8'>
                            //    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            //    <style>
                            //        body {{ font-family: Arial, sans-serif; margin: 20px; }}
                            //        .invoice-header, .invoice-details {{ text-align: right; }}
                            //        .invoice-container {{ width: 100%; border-collapse: collapse; }}
                            //        .invoice-container th, .invoice-container td {{ border: 1px solid #ddd; padding: 8px; }}
                            //        .invoice-container th {{ background-color: #f2f2f2; text-align: center; }}
                            //        .company-logo {{ float: left; margin-right: 20px; }}
                            //        .customer-info {{ float: right; text-align: left; }}
                            //        .clear {{ clear: both; }}
                            //        .text-center {{ text-align: center; }}
                            //        .payment-method {{ margin-top: 20px; font-weight: bold; }}
                            //    </style>
                            //    <title>Invoice</title>
                            //</head>
                            //<body>
                            //    <div class='invoice-header'>
                            //        <div class='company-logo'>
                            //            <img src='https://via.placeholder.com/100' alt='Company Logo'>
                            //        </div>
                            //        <div class='customer-info'>
                            //            <p><strong>Sales invoice</strong></p>
                            //            <p>Invoice #: 2</p>
                            //            <p>Created: {DateTime.Now}</p>
                            //            <p>Due: {DateTime.Now}</p>
                            //        </div>
                            //        <div class='clear'></div>
                            //    </div>

                            //    <div class='invoice-details'>
                            //        <p>Apx</p>
                            //        <p>+19165968856</p>
                            //        <p>Info@apx.com</p>
                            //        <hr>
                            //        <p>{namedovlober} - {slider.DataEntry}</p>
                            //    </div>

                            //    <table class='invoice-container'>
                            //        <thead>
                            //            <tr>
                            //                <th>Photo</th>
                            //                <th>Product Name</th>
                            //                <th>Type Product</th>
                            //                <th>Selling Price</th>
                            //                <th>Output Quantity</th>
                            //                <th>Total</th>
                            //            </tr>
                            //        </thead>
                            //        <tbody>
                            //            <tr>
                            //                <td class='text-center'>
                            //                    <img src='{Photo}' alt='Product Image' style='height:50px;width:50px'>
                            //                </td>
                            //                <td>{ProductName}</td>
                            //                <td class='text-center'>{TypesProduct}</td>
                            //                <td class='text-center'>{sellingPrice}</td>
                            //                <td class='text-center'>{QuantityOute}</td>
                            //                <td class='text-center'>{total}</td>
                            //            </tr>
                            //        </tbody>
                            //    </table>

                            //    <div class='payment-method'>
                            //        <p>Payment Method: Check</p>
                            //        <p>Check #: {total}</p>
                            //    </div>
                            //</body>
                            //</html>"
                            //                    };

                            //                    message.Body = builder.ToMessageBody();

                            //                    // استخدام OAuth2 بدلاً من المصادقة الأساسية
                            //                    var accessToken = "axziyzjtvxmclllo"; // يجب توليد رمز الوصول (Access Token) باستخدام OAuth2
                            //                    var oauth2 = new SaslMechanismOAuth2(emailSetting.MailSender, accessToken);

                            //                    using (var client = new SmtpClient())
                            //                    {
                            //                        await client.ConnectAsync(emailSetting.SmtpServer, emailSetting.PortServer, SecureSocketOptions.StartTls);
                            //                        await client.AuthenticateAsync(oauth2); // المصادقة باستخدام OAuth2
                            //                        await client.SendAsync(message);
                            //                        await client.DisconnectAsync(true);
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    // التعامل مع الحالة التي لا توجد فيها إعدادات البريد الإلكتروني
                            //                    // يمكنك تسجيل خطأ أو تنفيذ إجراءات أخرى هنا
                            //                }






                            TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                            TempData["Merchant"] = model.Order.IdUser;
                            return RedirectToAction("AddOrder");
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




            return Redirect(returnUrl);



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





        [HttpGet]
        public async Task<JsonResult> GetProductDetails(int id)
        {
            var product = dbcontext.TBProductInformations.FirstOrDefault(p => p.IdProductInformation == id);

            if (product != null)
            {
                // Fetch the global price using HtmlAgilityPack and the mode field
                //decimal globalPrice = await FetchGlobalPrice(product.Model);  // Assuming `mode` is the correct property name

                return Json(new
                {
                    imageUrl = product.Photo,
                    globalPrice = 0
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

        [HttpGet("Order/GetProductByName")]
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

        [HttpGet("admin/Order/FetchGlobalPrice")]
        public async Task<decimal> FetchGlobalPrice(string model, string Make)
        {
            decimal globalPrice = 0;
            if (Make == "home depot")
            {
                try
                {
                    HtmlWeb web = new HtmlWeb();
                    var document = await web.LoadFromWebAsync("https://www.homedepot.com/s/" + model);

                    var priceNode = document.DocumentNode.SelectSingleNode("//span[contains(@class, 'sui-font-display sui-leading-none sui-px-[2px] sui-text-9xl sui--translate-y-[0.5rem]')]");
                    string priceText = priceNode?.InnerText.Trim();

                    //if (string.IsNullOrEmpty(priceText))
                    //{
                    //    var pricePartsNodes = document.DocumentNode.SelectNodes("//div[@class='price-format__main-price']//span");
                    //    if (pricePartsNodes != null && pricePartsNodes.Count >= 4)
                    //    {
                    //        priceText = string.Join("", pricePartsNodes.Take(4).Select(node => node.InnerText.Trim()));
                    //    }
                    //}

                    if (string.IsNullOrEmpty(priceText))
                    {
                        var priceNode1 = document.DocumentNode.SelectSingleNode("//span[contains(@class, 'sui-font-display sui-leading-none sui-px-[2px] sui-text-4xl sui--translate-y-[0.35rem]')]");
                        if (priceNode1 != null)
                        {
                            priceText = priceNode1.InnerText.Trim();
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
            }
            else if (Make == "Lows")
            {
                /// نهاد
                /// 
                IWebDriver driver = new ChromeDriver();
                try
                {
                    // تشغيل متصفح Chrome باستخدام Selenium


                    // تعيين الرابط الذي يتم البحث فيه
                    var searchUrl = "https://www.lowes.com/search?searchTerm=" + model;

                    // التنقل إلى الرابط
                    driver.Navigate().GoToUrl(searchUrl);

                    // انتظار بعض الوقت حتى يتم تحميل الصفحة بالكامل
                    await Task.Delay(5000); // يمكن تعديل الوقت بناءً على احتياج تحميل الصفحة

                    // استهداف عنصر <span class="screen-reader"> لاستخراج السعر
                    var priceDollarNode = driver.FindElement(By.XPath("//div[@class='sc-kFWlue iHYmQD pd-comp-space-8']//span[@class='screen-reader']"));

                    string priceText = "";

                    if (priceDollarNode != null)
                    {
                        priceText = priceDollarNode.Text.Trim(); // السعر بالدولار والسنتات موجود في نفس العنصر

                        // تنظيف النص لتبقي فقط الأرقام والنقاط
                        priceText = Regex.Replace(priceText, "[^0-9.]", "");

                        // تحويل النص المنظف إلى قيمة رقمية
                        if (decimal.TryParse(priceText, out decimal parsedPrice))
                        {
                            globalPrice = parsedPrice;
                            Console.WriteLine($"Price: {parsedPrice}");
                        }
                        else
                        {
                            Console.WriteLine("Failed to parse price.");
                            globalPrice = 0;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Price not found.");
                        globalPrice = 0;
                    }
                }
                catch (Exception ex)
                {
                    // التعامل مع الاستثناءات
                    Console.WriteLine($"Error: {ex.Message}");
                    globalPrice = 0;
                }

                finally
                {
                    // إغلاق المتصفح
                    driver.Quit();
                }



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


        public async Task<IActionResult> GetProductDetailsForOrder(string productId)
        
        {
            var product = dbcontext.ViewProductInformation
                         .Where(p => p.Qrcode == productId || p.Model == productId  || p.ProductName == productId).FirstOrDefault();

            if (product != null)
            {
                var result = 0;
                return Ok(new
                {
                    imageUrl = product.Photo,
                    globalPrice = result,
                    productCategoryId = product.IdProductCategory,
                    bondTypeId = product.IdTypesProduct,  // Assuming this field exists
                    typesProductId = product.IdTypesProduct,
                    productName = product.ProductName,
                    id = product.IdProductInformation,
                    m = product.IdProductInformation,
                    brand = product.brand,
                    storeSku = product.storeSku,
                    sstoreSoSku = product.storeSoSku,
                    scrapingHtmlTitle = product.ScrapingHtmlTitle,
                   
                    mmodel = product.Model,

                });
            }
            else
            {
                return Ok(new
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

        public async Task<IActionResult> GetProductSuggestions(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query cannot be empty");
            }

            try
            {

                var products = await dbcontext.ViewProductInformation
                    .Where(p => p.Qrcode.StartsWith(query) ||
                                p.Model.StartsWith(query) ||
                              
                                p.brand.StartsWith(query) ||
                                p.storeSku.StartsWith(query) ||
                                p.storeSoSku.StartsWith(query) ||
                                p.ProductName.StartsWith(query))
                    .Select(p => new
                    {
                        p.Qrcode,
                        p.ProductName,
                        p.Model,
                      
                        p.brand,
                        p.storeSku,
                        p.storeSoSku,
                        MatchingField =
                            p.Qrcode.StartsWith(query) ? "Qrcode" :
                            p.Model.StartsWith(query) ? "Model" :
                         
                            p.brand.StartsWith(query) ? "Brand" :
                            p.storeSoSku.StartsWith(query) ? "StoreSku" :
                            p.storeSoSku.StartsWith(query) ? "StoreSoSku" :
                            p.ProductName.StartsWith(query) ? "ProductName" : null
                    })
                    .ToListAsync();

                if (products.Any())
                {
                    return Ok(products);
                }
                else
                {
                    return NotFound("No products found");
                }
            }
            catch (Exception ex)
            {
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


        public async Task<IActionResult> FetchImageByModelOrder(string model, string breand)
        {

            if (breand == "home depot")
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
            else if(breand == "Lows")
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
            return null;
        }



        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveNewProductInfo(ViewmMODeElMASTER model, TBProductInformation slider, List<IFormFile> Files, string returnUrl, string photo)
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
                        return RedirectToAction("AddOrder");
                    }
                }


                // Save or update the product information
                if (slider.IdProductInformation == 0)
                {
                    if (dbcontext.TBProductInformations.Any(a => a.ProductName == slider.ProductName && a.IdTypesProduct == slider.IdTypesProduct && a.IdProductCategory == slider.IdProductCategory))
                    {
                        TempData["ProductName"] = "Product already exists.";
                        return RedirectToAction("AddOrder", model);
                    }
                    dbcontext.TBProductInformations.Add(slider);
                }
                else
                {
                    dbcontext.TBProductInformations.Update(slider);
                }

                await dbcontext.SaveChangesAsync();

                TempData["Saved successfully"] = "Saved successfully.";
                TempData["AfterSave"] = model.ProductInformation.Model;
                return RedirectToAction("AddOrder");
            }
            catch (Exception ex)
            {
                TempData["ErrorSave"] = "Error saving data: " + ex.Message;
                return RedirectToAction("AddOrder", model);
            }
        }
   



        public IActionResult printInvoce(int IdPurchaseOrder)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformatione = iCompanyInformation.GetAll().Take(1);

            // تأكد من أن CompanieTypeWork مهيأ حتى لو لم يكن هناك ID
            if (vmodel.ListCompanyInformatione == null)
            {
                vmodel.CompanyInformation = new TBCompanyInformation(); // أو النوع الصحيح
            }

            if (IdPurchaseOrder != null)
            {
                vmodel.ListViewOrder = iOrder.GetAllv(IdPurchaseOrder);
            }

            return View(vmodel);

        }

        public IActionResult printPurchase(int IdPurchaseOrder)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformatione = iCompanyInformation.GetAll().Take(1);

            // تأكد من أن CompanieTypeWork مهيأ حتى لو لم يكن هناك ID
            if (vmodel.ListCompanyInformatione == null)
            {
                vmodel.CompanyInformation = new TBCompanyInformation(); // أو النوع الصحيح
            }

            if (IdPurchaseOrder != null)
            {
                vmodel.ListViewOrder = iOrder.GetAllv(IdPurchaseOrder);
            }

            return View(vmodel);
        }


    }
}
