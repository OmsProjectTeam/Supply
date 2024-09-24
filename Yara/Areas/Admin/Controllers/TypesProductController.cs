namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TypesProductController : Controller
    {
        MasterDbcontext dbcontext;
        IITypesProduct iTypesProduct;
        public TypesProductController(MasterDbcontext dbcontext1,IITypesProduct iTypesProduct1)
        {
            dbcontext = dbcontext1;
            iTypesProduct = iTypesProduct1;
        }
        public IActionResult MyTypesProduct()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTypesProduct = iTypesProduct.GetAll();
            return View(vmodel);
        }  
        public IActionResult AddTypesProduct(int? IdTypesProduct)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTypesProduct = iTypesProduct.GetAll();
            if (IdTypesProduct != null)
            {
                vmodel.TypesProduct = iTypesProduct.GetById(Convert.ToInt32(IdTypesProduct));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBTypesProduct slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdTypesProduct = model.TypesProduct.IdTypesProduct;
                slider.TypesProduct = model.TypesProduct.TypesProduct;
                slider.Active = model.TypesProduct.Active;
                slider.DataEntry = model.TypesProduct.DataEntry;
                slider.DateTimeEntry = model.TypesProduct.DateTimeEntry;
                slider.CurrentState = model.TypesProduct.CurrentState;
                if (slider.IdTypesProduct == 0 || slider.IdTypesProduct == null)
                {
                    if (dbcontext.TBTypesProducts.Where(a => a.TypesProduct == slider.TypesProduct).ToList().Count > 0)
                    {
                        TempData["TypesProduct"] = ResourceWeb.VLTypesProductDoplceted;
                        return RedirectToAction("AddTypesProduct", model);
                    }
                    var reqwest = iTypesProduct.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyTypesProduct");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iTypesProduct.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyTypesProduct");
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
        public IActionResult DeleteData(int IdTypesProduct)
        {
            var reqwistDelete = iTypesProduct.deleteData(IdTypesProduct);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyTypesProduct");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyTypesProduct");

            }
        } 
    }
}