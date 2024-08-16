using Microsoft.AspNetCore.Mvc;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WareHouseController : Controller
    {
        IIWareHouseType iWareHouseType;
        IIWareHouse iWareHouse;
        MasterDbcontext dbcontext;

        public WareHouseController(MasterDbcontext dbcontext1, IIWareHouseType iWareHouseType1, IIWareHouse iWareHouse1)
        {
            dbcontext = dbcontext1;
            iWareHouseType = iWareHouseType1;
            iWareHouse = iWareHouse1;
        }
        public IActionResult MyWareHouse(string? userId)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ViewWareHouse = iWareHouse.GetAll();

            return View(viewmMODeElMASTER);
        }

        public IActionResult AddWareHouse(int? IdBWareHouse)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

            vmodel.ViewWareHouse = iWareHouse.GetAll();
            ViewBag.WareHouseType = iWareHouseType.GetAll();

            if (IdBWareHouse != null)
            {
                vmodel.WareHouse = iWareHouse.GetById(Convert.ToInt32(IdBWareHouse));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBWareHouse slider, string returnUrl)
        {
            try
            {
                slider.IdBWareHouse = model.WareHouse.IdBWareHouse;
                slider.Description = model.WareHouse.Description;
                slider.IdWareHouseType = model.WareHouse.IdWareHouseType;
                slider.Code = model.WareHouse.Code;
                slider.Active = model.WareHouse.Active;
                slider.DataEntry = model.WareHouse.DataEntry;
                slider.DateTimeEntry = model.WareHouse.DateTimeEntry;
                slider.CurrentState = model.WareHouse.CurrentState;
                if (slider.IdBWareHouse == 0 || slider.IdBWareHouse == null)
                {
                    if (dbcontext.TBWareHouses.Where(a => a.Description == slider.Description).ToList().Count > 0)
                    {
                        TempData["Description"] = ResourceWeb.VLDescriptionWareHouseDoplceted;
                        return RedirectToAction("AddWareHouse", model);
                    }

                    if (dbcontext.TBWareHouses.Where(a => a.Code == slider.Code).ToList().Count > 0)
                    {
                        TempData["Code"] = ResourceWeb.VLDescriptionWareHouseCodeDoplceted;
                        return RedirectToAction("AddWareHouse", model);
                    }
                    var reqwest = iWareHouse.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyWareHouse");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iWareHouse.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyWareHouse");
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
        public IActionResult DeleteData(int IdBWareHouse)
        {
            var reqwistDelete = iWareHouse.deleteData(IdBWareHouse);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyWareHouse");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyWareHouse");
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
        public IActionResult PrintWareHouseDetails(string warehouseType, string description, string code, string qrCodeSrc)
        {
            var htmlContent = new StringBuilder();

            htmlContent.Append("<html><head><title>Print QR Code</title></head><body>");
            htmlContent.AppendFormat("<h1>WareHouse: {0}</h1>", warehouseType);
            htmlContent.AppendFormat("<h2>Description: {0}</h2>", description);
            htmlContent.AppendFormat("<h3>Code: {0}</h3>", code);
            htmlContent.AppendFormat("<img src='{0}' alt='QR Code' />", qrCodeSrc);

            htmlContent.Append("</body></html>");

            return Content(htmlContent.ToString(), "text/html", Encoding.UTF8);
        }


    }
}



        
        
    
