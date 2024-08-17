

using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;
using System.Drawing;
using ZXing.QrCode;
using ZXing;

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

        public OrderController(IIOrder iOrder1, IIBondType iBondType1, IIMerchants iMerchants1, IIProductCategory iProductCategory1, IITypesProduct iTypesProduct1, IIProductInformation iProductInformation1, IIWareHouse iWareHouse1, IIWareHouseBranch iWareHouseBranch1, MasterDbcontext dbcontext1)
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
        }
        public IActionResult MyOrder()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewOrder = iOrder.GetAll();
            return View(vmodel);
        }
        public IActionResult AddOrder(int? IdPurchaseOrder)
        {
            ViewBag.BondType = iBondType.GetAll();
            ViewBag.Merchants = iMerchants.GetAll();
            ViewBag.ProductCategory = iProductCategory.GetAll();
            ViewBag.TypesProduct = iTypesProduct.GetAll();
            ViewBag.ProductInformation = iProductInformation.GetAll();
            ViewBag.WareHouse = iWareHouse.GetAll();
            ViewBag.WareHouseBranch = iWareHouseBranch.GetAll();
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewOrder = iOrder.GetAll();
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


        [HttpGet]
        public IActionResult PrintWareHouseDetails(string Merchant, string WareHouse, string ProductInformation, string WareHouseBranch, string sellingPrice, string qrCodeSrc)
        {
            var htmlContent = new StringBuilder();

            htmlContent.Append("<html><head><title>Print QR Code</title></head><body>");
            htmlContent.AppendFormat("<h1>WareHouse Type: {0}</h1>", Merchant);
            htmlContent.AppendFormat("<h2>Warehouse: {0}</h2>", WareHouse);
            htmlContent.AppendFormat("<h3>Description: {0}</h3>", WareHouseBranch);
            htmlContent.AppendFormat("<h3>Description: {0}</h3>", ProductInformation);
            htmlContent.AppendFormat("<h3>Description: {0}</h3>", sellingPrice);
            htmlContent.AppendFormat("<img src='{0}' alt='QR Code' />", qrCodeSrc);
            htmlContent.Append("</body></html>");

            return Content(htmlContent.ToString(), "text/html", Encoding.UTF8);
        }

        public JsonResult GetProductImageUrl(int id)
        {
            // Fetch the image URL from your data source based on the product ID
            var product = dbcontext.TBProductInformations.FirstOrDefault(p => p.IdProductInformation == id);

            if (product != null)
            {
                return Json(new { imageUrl = product.Photo }); // Assuming `ImageUrl` is the property holding the image URL
            }
            else
            {
                return Json(new { imageUrl = "http://placehold.it/220x180" }); // Fallback image
            }
        }

    }
}
