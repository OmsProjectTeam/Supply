using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using System.Drawing;
using ZXing.QrCode;
using ZXing;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WareHouseBranchController : Controller
    {
        IIWareHouseBranch iWareHouseBranch;
        IIWareHouseType iWareHouseType;
        IIWareHouse iWareHouse;
        MasterDbcontext dbcontext;

        public WareHouseBranchController(MasterDbcontext dbcontext1, IIWareHouseType iWareHouseType1, IIWareHouse iWareHouse1, IIWareHouseBranch iWareHouseBranch1)
        {
            dbcontext = dbcontext1;
            iWareHouseType = iWareHouseType1;
            iWareHouse = iWareHouse1;
            iWareHouseBranch = iWareHouseBranch1;
        }
        public IActionResult MyWareHouseBranch(string? userId)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ViewWareHouseBranch = iWareHouseBranch.GetAll();

            return View(viewmMODeElMASTER);
        }

        public IActionResult AddWareHouseBranch(int? IdBWareHouseBranch)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

            vmodel.ViewWareHouseBranch = iWareHouseBranch.GetAll();
            ViewBag.WareHouseType = iWareHouseType.GetAll();
            ViewBag.WareHouse = iWareHouse.GetAll();

            if (IdBWareHouseBranch != null)
            {
                vmodel.WareHouseBranch = iWareHouseBranch.GetById(Convert.ToInt32(IdBWareHouseBranch));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBWareHouseBranch slider, string returnUrl)
        {
            try
            {
                slider.IdBWareHouseBranch = model.WareHouseBranch.IdBWareHouseBranch;
                slider.Description = model.WareHouseBranch.Description;
                slider.IdWareHouseType = model.WareHouseBranch.IdWareHouseType;
                slider.IdBWareHouse = model.WareHouseBranch.IdBWareHouse;
                slider.Code = model.WareHouseBranch.Code;
                slider.Active = model.WareHouseBranch.Active;
                slider.DataEntry = model.WareHouseBranch.DataEntry;
                slider.DateTimeEntry = model.WareHouseBranch.DateTimeEntry;
                slider.CurrentState = model.WareHouseBranch.CurrentState;
                if (slider.IdBWareHouseBranch == 0 || slider.IdBWareHouseBranch == null)
                {
                    if (dbcontext.TBWareHouseBranchs.Where(a => a.Description == slider.Description).ToList().Count > 0)
                    {
                        TempData["WareHouseBranch"] = ResourceWeb.VLWareHouseBranchDoplceted;
                        return RedirectToAction("AddWareHouseBranch", model);
                    }
                    if (dbcontext.TBWareHouseBranchs.Where(a => a.Code == slider.Code).ToList().Count > 0)
                    {
                        TempData["Code"] = ResourceWeb.VLWareHouseBranchCodeDoplceted;
                        return RedirectToAction("AddWareHouseBranch", model);
                    }
                    var reqwest = iWareHouseBranch.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyWareHouseBranch");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("MyWareHouseBranch");
                    }
                }
                else
                {
                    var reqestUpdate = iWareHouseBranch.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyWareHouseBranch");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("MyWareHouseBranch");
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
        public IActionResult DeleteData(int IdBWareHouseBranch)
        {
            var reqwistDelete = iWareHouseBranch.deleteData(IdBWareHouseBranch);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyWareHouseBranch");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyWareHouseBranch");
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
        public IActionResult PrintWareHouseDetails(string warehouseType, string warehouse, string description, string qrCodeSrc)
        {
            var htmlContent = new StringBuilder();

            htmlContent.Append("<html><head><title>Print QR Code</title></head><body>");
            htmlContent.AppendFormat("<h1>WareHouse Type: {0}</h1>", warehouseType);
            htmlContent.AppendFormat("<h2>Warehouse: {0}</h2>", warehouse);
            htmlContent.AppendFormat("<h3>Description: {0}</h3>", description);
            htmlContent.AppendFormat("<img src='{0}' alt='QR Code' />", qrCodeSrc);
            htmlContent.Append("</body></html>");

            return Content(htmlContent.ToString(), "text/html", Encoding.UTF8);
        }

		//[HttpGet]
		//public JsonResult GetSubWarehouses(int IdSubWarehouse)
		//{
		//	var subWarehouses = iWareHouseBranch.GetById(IdSubWarehouse);
		//	return Json(new SelectList(subWarehouses, "IdSubWarehouse", "Description"));
		//}
		public IActionResult GetSubWarehouses(int IdSubWarehouse)
		{
			var subWarehouses = iWareHouseBranch.GetAllv(IdSubWarehouse);
			var selectList = new SelectList(subWarehouses, "IdSubWarehouse", "Description");
			return Json(selectList);
		}

	}
}
