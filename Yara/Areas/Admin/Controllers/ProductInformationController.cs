using HtmlAgilityPack;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductInformationController : Controller
    {
        MasterDbcontext dbcontext;
        IIProductCategory iProductCategory;
        IIProductInformation iProductInformation;
        IITypesProduct iTypesProduct;
        public ProductInformationController(MasterDbcontext dbcontext1,IIProductCategory iProductCategory1,IIProductInformation iProductInformation1,IITypesProduct iTypesProduct1)
        {
            dbcontext= dbcontext1;
            iProductCategory= iProductCategory1;
            iProductInformation= iProductInformation1;
            iTypesProduct= iTypesProduct1;
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

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
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
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
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
        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBProductInformation slider, List<IFormFile> Files, string returnUrl)
        //{
        //    try
        //    {
        //        slider.IdProductInformation = model.ProductInformation.IdProductInformation;
        //        slider.IdProductCategory = model.ProductInformation.IdProductCategory;
        //        slider.IdTypesProduct = model.ProductInformation.IdTypesProduct;
        //        slider.ProductName = model.ProductInformation.ProductName;
        //        slider.Make = model.ProductInformation.Make;
        //        slider.UPC = model.ProductInformation.UPC;
        //        slider.Qrcode = model.ProductInformation.Qrcode;    
        //        slider.Photo = model.ProductInformation.Photo;
        //        slider.Active = model.ProductInformation.Active;
        //        slider.DateTimeEntry = model.ProductInformation.DateTimeEntry;
        //        slider.DataEntry = model.ProductInformation.DataEntry;
        //        slider.CurrentState = model.ProductInformation.CurrentState;
        //        slider.Model = model.ProductInformation.Model;

        //        var file = HttpContext.Request.Form.Files;
        //        if (slider.IdProductInformation == 0 || slider.IdProductInformation == null)
        //        {
        //            if (file.Count() > 0)
        //            {
        //                string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
        //                var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Product", Photo), FileMode.Create);
        //                file[0].CopyTo(fileStream);
        //                slider.Photo = Photo;
        //                fileStream.Close();
        //            }
        //            else
        //            {
        //                TempData["Message"] = ResourceWeb.VLimageuplode;
        //                return RedirectToAction("AddEditProductInformation");
        //            }
        //            if (dbcontext.TBProductInformations.Where(a => a.ProductName == slider.ProductName).Where(a => a.IdTypesProduct == slider.IdTypesProduct).Where(a => a.IdProductCategory == slider.IdProductCategory).ToList().Count > 0)
        //            {
        //                var PhotoNAme = slider.Photo;
        //                var delet = iProductInformation.DELETPHOTOWethError(PhotoNAme);

        //                TempData["ProductName"] = ResourceWeb.VLProductNameDoplceted;
        //                return RedirectToAction("AddEditProductInformation", model);
        //            }
        //            //تجهز الصور والباركود 





        //            var reqwest = iProductInformation.saveData(slider);
        //            if (reqwest == true)
        //            {
        //                TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
        //                return RedirectToAction("MYProductInformation");
        //            }
        //            else
        //            {
        //                var PhotoNAme = slider.Photo;
        //                var delet = iProductInformation.DELETPHOTOWethError(PhotoNAme);
        //                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
        //                return RedirectToAction("AddEditProductInformation");
        //            }
        //        }
        //        else
        //        {
        //            //var reqweistDeletPoto = iProductInformation.DELETPHOTO(slider.IdProductInformation);
        //            if (file.Count() == 0)

        //            {
        //                slider.Photo = model.ProductInformation.Photo;
        //                //TempData["Message"] = ResourceWeb.VLimageuplode;
        //                var reqestUpdate2 = iProductInformation.UpdateData(slider);
        //                if (reqestUpdate2 == true)
        //                {
        //                    TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
        //                    return RedirectToAction("MYProductInformation");
        //                }
        //                else
        //                {
        //                    var PhotoNAme = slider.Photo;
        //                    //var delet = iProductInformation.DELETPHOTOWethError(PhotoNAme);
        //                    TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
        //                    return RedirectToAction("AddEditProductInformationImage");
        //                }
        //            }
        //            else
        //            {
        //                var reqweistDeletPoto = iProductInformation.DELETPHOTO(slider.IdProductInformation);
        //                var reqestUpdate2 = iProductInformation.UpdateData(slider);
        //                if (reqestUpdate2 == true)
        //                {
        //                    TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
        //                    return RedirectToAction("MYProductInformation");
        //                }
        //                else
        //                {
        //                    var PhotoNAme = slider.Photo;
        //                    var delet = iProductInformation.DELETPHOTOWethError(PhotoNAme);
        //                    TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
        //                    return RedirectToAction("AddEditProductInformation");
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        var file = HttpContext.Request.Form.Files;
        //        if (file.Count() == 0)

        //        {
        //            //var PhotoNAme = slider.Photo;
        //            //var delet = iProductInformation.DELETPHOTOWethError(PhotoNAme);
        //            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
        //            return RedirectToAction("AddEditProductInformationImage");
        //        }
        //        else
        //        {
        //            var PhotoNAme = slider.Photo;
        //            var delet = iProductInformation.DELETPHOTOWethError(PhotoNAme);
        //            TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
        //            return RedirectToAction("AddEditProductInformation");
        //        }
        //    }
        //}
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBProductInformation slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdProductInformation = model.ProductInformation.IdProductInformation;
                slider.IdProductCategory = model.ProductInformation.IdProductCategory;
                slider.IdTypesProduct = model.ProductInformation.IdTypesProduct;
                slider.ProductName = model.ProductInformation.ProductName;
                slider.Make = model.ProductInformation.Make;
                slider.UPC = model.ProductInformation.UPC;
                slider.Qrcode = model.ProductInformation.Qrcode;
                slider.Active = model.ProductInformation.Active;
                slider.DateTimeEntry = model.ProductInformation.DateTimeEntry;
                slider.DataEntry = model.ProductInformation.DataEntry;
                slider.CurrentState = model.ProductInformation.CurrentState;
                slider.Model = model.ProductInformation.Model;

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

                // Save or update the product information
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
                return RedirectToAction("MYProductInformation");
            }
            catch (Exception ex)
            {
                TempData["ErrorSave"] = "Error saving data: " + ex.Message;
                return RedirectToAction("AddEditProductInformation", model);
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

        //[HttpGet]
        //public async Task<IActionResult> FetchImageByModel(string model)
        //{
        //    try
        //    {
        //        HtmlWeb web = new HtmlWeb();
        //        var document = web.Load("https://www.homedepot.com/s/" + model);
        //        var imageNodes = document.DocumentNode.SelectNodes("//div[@class='mediagallery']//img");

        //        if (imageNodes != null)
        //        {
        //            var imageUrl = imageNodes.Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
        //            return Json(new { success = true, imageUrl });
        //        }
        //        else
        //        {
        //            var imageNodesFallback = document.DocumentNode.SelectNodes("//div[@class='grid']//img");
        //            if (imageNodesFallback != null)
        //            {
        //                var imageUrl = imageNodesFallback.Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
        //                return Json(new { success = true, imageUrl });
        //            }
        //        }

        //        return Json(new { success = false, message = "Image not found." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> FetchImageByModel(string model)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                var document = web.Load("https://www.homedepot.com/s/" + model);

                // Attempt to fetch the image from the primary source
                var imageNodes = document.DocumentNode.SelectNodes("//div[@class='mediagallery']//img");

                if (imageNodes != null && imageNodes.Any())
                {
                    var imageUrl = imageNodes.Select(node => node.GetAttributeValue("src", "")).FirstOrDefault();
                    return Json(new { success = true, imageUrl });
                }
                else
                {
                    // Fallback to secondary image source if the primary is unavailable
                    var imageNodesFallback = document.DocumentNode.SelectNodes("//div[@data-testid='product-image__wrapper']//img");

                    if (imageNodesFallback != null && imageNodesFallback.Any())
                    {
                        var firstImageUrl = imageNodesFallback
                            .Select(node => node.GetAttributeValue("src", ""))
                            .FirstOrDefault();

                        return Json(new { success = true, imageUrl = firstImageUrl });
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

    }
}