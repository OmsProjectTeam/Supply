using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;
using System.Drawing;
using ZXing.QrCode;
using ZXing;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Primitives;
using System.Globalization;
using OpenQA.Selenium.Chrome;

using OpenQA.Selenium;

using System;

using System.Threading.Tasks;

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

        public OrderController(IIOrder iOrder1, IIBondType iBondType1, IIMerchants iMerchants1, IIProductCategory iProductCategory1, IITypesProduct iTypesProduct1,
            IIProductInformation iProductInformation1, IIWareHouse iWareHouse1, IIWareHouseBranch iWareHouseBranch1, MasterDbcontext dbcontext1, IIBrandName iBrandName1, IICompanyInformation iCompanyInformation1, IIUserInformation iUserInformation1)
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
								TempData["Merchant"] = model.Order.IdMerchants;
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
								TempData["Merchant"] = model.Order.IdMerchants;
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
        private async Task<decimal> FetchGlobalPrice(string model, string Make)
        {
            decimal globalPrice = 0;
            if (Make == "RYOBI")
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


            public async Task<JsonResult> GetProductDetailsForOrder(string productId)
            {
                var product = dbcontext.ViewProductInformation
                             .Where(p => p.Qrcode == productId || p.Model == productId || p.UPC == productId || p.ProductName == productId || p.Make == productId).FirstOrDefault();


                if (product != null)
                {
                    // Fetch the global price using HtmlAgilityPack and the model field
                    decimal globalPrice = 0; //await FetchGlobalPrice(product.Model);

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

            public async Task<IActionResult> FetchImageByModelOrder(string model, string breand)
            {

                if (breand == "RYOBI")
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



            [HttpPost]
            [AutoValidateAntiforgeryToken]
            public async Task<IActionResult> SaveNewProductInfo(ViewmMODeElMASTER model, TBProductInformation slider, List<IFormFile> Files, string returnUrl, string photo)
            {
                try
                {
                    slider.IdProductInformation = model.ProductInformation.IdProductInformation;
                    slider.IdProductCategory = model.ProductInformation.IdProductCategory;
                    slider.IdTypesProduct = model.ProductInformation.IdTypesProduct;
                    slider.IdBrandName = model.ProductInformation.IdBrandName;
                    slider.ProductName = model.ProductInformation.ProductName;
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
            public async Task<IActionResult> GetUPC(int value)

            {
                string UPC = string.Empty;
                string Make = string.Empty;
                decimal GlobalPrice = 0;

                var product = await iProductInformation.GetByIdFromViewAsync(value);
                if (product == null)
                    return Ok(new
                    {
                        UPC,
                        Make,
                        GlobalPrice

                    });

                GlobalPrice = await FetchGlobalPrice(product.Model, product.Make);

                return Ok(new
                {
                    UPC = product.UPC,
                    Make = product.Make,
                    GlobalPrice = GlobalPrice
                });
            }

		[HttpGet]
		public IActionResult PrintInvoice(string Merchant, string WareHouse,
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

			// Start HTML content
			htmlContent.Append("<html><head><title>Print Invoice</title>");
			htmlContent.Append("<style>");
			htmlContent.Append("body { font-family: Arial, sans-serif; font-size: 12px; }");
			htmlContent.Append(".invoice-box { width: 100%; padding: 30px; border: 1px solid #eee; box-shadow: 0 0 10px rgba(0, 0, 0, 0.15); line-height: 24px; color: #555; }");
			htmlContent.Append(".invoice-box table { width: 100%; line-height: inherit; text-align: left; }");
			htmlContent.Append(".invoice-box table td { padding: 5px; vertical-align: top; }");
			htmlContent.Append(".invoice-box table tr td:nth-child(2) { text-align: right; }");
			htmlContent.Append(".invoice-box table tr.top table td { padding-bottom: 20px; }");
			htmlContent.Append(".invoice-box table tr.top table td.title { font-size: 45px; line-height: 45px; color: #333; }");
			htmlContent.Append(".invoice-box table tr.information table td { padding-bottom: 40px; }");
			htmlContent.Append(".invoice-box table tr.heading td { background: #eee; border-bottom: 1px solid #ddd; font-weight: bold; }");
			htmlContent.Append(".invoice-box table tr.details td { padding-bottom: 20px; }");
			htmlContent.Append(".invoice-box table tr.item td{ border-bottom: 1px solid #eee; }");
			htmlContent.Append(".invoice-box table tr.item.last td { border-bottom: none; }");
			htmlContent.Append(".invoice-box table tr.total td:nth-child(2) { border-top: 2px solid #eee; font-weight: bold; }");
			htmlContent.Append("</style>");
			htmlContent.Append("</head><body>");

			// Invoice container
			htmlContent.Append("<div class='invoice-box'>");
			htmlContent.Append("<table cellpadding='0' cellspacing='0'>");

			// Company and Invoice Information
			htmlContent.Append("<tr class='top'>");
			htmlContent.Append("<td colspan='2'>");
			htmlContent.Append("<table>");
			htmlContent.Append("<tr>");
			htmlContent.AppendFormat("<td class='title'><img src='/Images/Home/{0}' alt='Company Logo' style='width: 100px;' /></td>", photo);
			htmlContent.AppendFormat("<td>Invoice #: {0}<br>Created: {1}<br>Due: {2}</td>", PurchaseOrderNoumber, DateTime.Now.ToShortDateString(), DateTime.Now.AddDays(30).ToShortDateString());
			htmlContent.Append("</tr>");
			htmlContent.Append("</table>");
			htmlContent.Append("</td>");
			htmlContent.Append("</tr>");

			// Company Address and Merchant Information
			htmlContent.Append("<tr class='information'>");
			htmlContent.Append("<td colspan='2'>");
			htmlContent.Append("<table>");
			htmlContent.AppendFormat("<tr><td>{0}<br>{1}<br>{2}</td>", CompanyName, Address, PhoneNumber);
			htmlContent.AppendFormat("<td>Merchant#: {0}<br>Warehouse#: {1}<br>Warehouse Branch: {2}</td>", Merchant, WareHouse, WareHouseBranch);
			htmlContent.Append("</tr>");
			htmlContent.Append("</table>");
			htmlContent.Append("</td>");
			htmlContent.Append("</tr>");

			// Payment Method
			htmlContent.Append("<tr class='heading'>");
			htmlContent.Append("<td>Payment Method</td>");
			htmlContent.Append("<td>Check #</td>");
			htmlContent.Append("</tr>");
			htmlContent.Append("<tr class='details'><td>Check</td><td>1000</td></tr>");

			// Product Details
			htmlContent.Append("<tr class='heading'><td>Product</td><td>Price</td></tr>");
			htmlContent.AppendFormat("<tr class='item'><td>{0}</td><td>{1}</td></tr>", ProductInformation, sellingPrice);

			// Total
			htmlContent.Append("<tr class='total'><td></td><td>Total: " + sellingPrice + "</td></tr>");
			htmlContent.Append("</table>");

			// QR Code, Barcode, and UPC Section
			htmlContent.Append("<div class='footer'>");
			htmlContent.AppendFormat("<img src='{0}' alt='QR Code' style='width: 80px;' /><br>", qrCodeSrc);
			htmlContent.AppendFormat("<img src='{0}' alt='Barcode' style='width: 180px;' /><br>", bar);
			if (!string.IsNullOrEmpty(upc))
			{
				htmlContent.AppendFormat("<div class='upc'>UPC: {0}</div>", upc);
			}
			else
			{
				htmlContent.Append("<div class='upc'>UPC: N/A</div>");
			}
			htmlContent.Append("</div>");

			// Print and Submit Buttons
			htmlContent.Append("<div class='text-right' style='margin-top: 20px;'>");
			htmlContent.Append("<button onclick='window.print()' class='btn btn-default'><i class='fa fa-print'></i> Print</button>");
			htmlContent.Append("<button type='submit' class='btn btn-primary'>Submit</button>");
			htmlContent.Append("</div>");

			// Close Invoice container
			htmlContent.Append("</div>");

			// End HTML content
			htmlContent.Append("</body></html>");

			// Return the formatted content as an HTML page
			return Content(htmlContent.ToString(), "text/html", Encoding.UTF8);
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








	}
}
