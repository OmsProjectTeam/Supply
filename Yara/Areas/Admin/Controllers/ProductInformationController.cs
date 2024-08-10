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
                slider.Photo = model.ProductInformation.Photo;
                slider.Active = model.ProductInformation.Active;
                slider.DateTimeEntry = model.ProductInformation.DateTimeEntry;
                slider.DataEntry = model.ProductInformation.DataEntry;
                slider.CurrentState = model.ProductInformation.CurrentState;
                slider.Model = model.ProductInformation.Model;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdProductInformation == 0 || slider.IdProductInformation == null)
                {
                    if (file.Count() > 0)
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Product", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                    }
                    else
                    {
                        TempData["Message"] = ResourceWeb.VLimageuplode;
                        return RedirectToAction("AddEditProductInformation");
                    }
                    if (dbcontext.TBProductInformations.Where(a => a.ProductName == slider.ProductName).Where(a => a.IdTypesProduct == slider.IdTypesProduct).Where(a => a.IdProductCategory == slider.IdProductCategory).ToList().Count > 0)
                    {
                        var PhotoNAme = slider.Photo;
                        var delet = iProductInformation.DELETPHOTOWethError(PhotoNAme);

                        TempData["ProductName"] = ResourceWeb.VLProductNameDoplceted;
                        return RedirectToAction("AddEditProductInformation", model);
                    }
                    //تجهز الصور والباركود 





                    var reqwest = iProductInformation.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYProductInformation");
                    }
                    else
                    {
                        var PhotoNAme = slider.Photo;
                        var delet = iProductInformation.DELETPHOTOWethError(PhotoNAme);
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddEditProductInformation");
                    }
                }
                else
                {
                    //var reqweistDeletPoto = iProductInformation.DELETPHOTO(slider.IdProductInformation);
                    if (file.Count() == 0)

                    {
                        slider.Photo = model.ProductInformation.Photo;
                        //TempData["Message"] = ResourceWeb.VLimageuplode;
                        var reqestUpdate2 = iProductInformation.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYProductInformation");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            //var delet = iProductInformation.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditProductInformationImage");
                        }
                    }
                    else
                    {
                        var reqweistDeletPoto = iProductInformation.DELETPHOTO(slider.IdProductInformation);
                        var reqestUpdate2 = iProductInformation.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYProductInformation");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iProductInformation.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditProductInformation");
                        }
                    }
                }
            }
            catch
            {
                var file = HttpContext.Request.Form.Files;
                if (file.Count() == 0)

                {
                    //var PhotoNAme = slider.Photo;
                    //var delet = iProductInformation.DELETPHOTOWethError(PhotoNAme);
                    TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                    return RedirectToAction("AddEditProductInformationImage");
                }
                else
                {
                    var PhotoNAme = slider.Photo;
                    var delet = iProductInformation.DELETPHOTOWethError(PhotoNAme);
                    TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                    return RedirectToAction("AddEditProductInformation");
                }
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
    }
}