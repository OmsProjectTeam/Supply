using Domin.Entity;
using Infarstuructre.BL;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class productCategoryController : Controller
    {
        IIProductCategory iProductCategory;
        MasterDbcontext dbcontext;
        public productCategoryController(MasterDbcontext dbcontext1, IIProductCategory iProductCategory1)
        {
            dbcontext = dbcontext1;
            iProductCategory = iProductCategory1;
        }
        public IActionResult MyproductCategory()
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ViewProductCategory = iProductCategory.GetAll();

            return View(viewmMODeElMASTER);
        }
        public IActionResult AddproductCategory(int? IdProductCategory)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ViewProductCategory = iProductCategory.GetAll();
            if (IdProductCategory != null)
            {
                viewmMODeElMASTER.ProductCategory = iProductCategory.GetById(Convert.ToInt32(IdProductCategory));
                return View(viewmMODeElMASTER);
            }
            else
            {
                return View(viewmMODeElMASTER);
            }
            return View(viewmMODeElMASTER);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBProductCategory slider, string returnUrl)
        {
            try
            {
                slider.IdProductCategory = model.ProductCategory.IdProductCategory;
                slider.ProductCategory = model.ProductCategory.ProductCategory;
                slider.Active = model.ProductCategory.Active;
                slider.DataEntry = model.ProductCategory.DataEntry;
                slider.DateTimeEntry = model.ProductCategory.DateTimeEntry;
                slider.CurrentState = model.ProductCategory.CurrentState;
                if (slider.IdProductCategory == 0 || slider.IdProductCategory == null)
                {
                    if (dbcontext.TBProductCategorys.Where(a => a.ProductCategory == slider.ProductCategory).ToList().Count > 0)
                    {
                        TempData["ProductCategory"] = ResourceWeb.VLProductCategoryDoplceted;
                        return RedirectToAction("AddproductCategory", model);
                    }
                    var reqwest = iProductCategory.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyproductCategory");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddproductCategory");
                    }
                }
                else
                {
                    var reqestUpdate = iProductCategory.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyproductCategory");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddproductCategory");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddproductCategory");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdProductCategory)
        {
            var reqwistDelete = iProductCategory.deleteData(IdProductCategory);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyproductCategory");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("AddproductCategory");
            }
        }
    }
}
