using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandNameController : Controller
    {
        MasterDbcontext dbcontext;
        IIBrandName iBrandName;
        public BrandNameController(MasterDbcontext dbcontext1,IIBrandName iBrandName1)
        {
            dbcontext=dbcontext1;
            iBrandName=iBrandName1;
        }
        public IActionResult MyBrandName()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListBrandName = iBrandName.GetAll();
            return View(vmodel);
        }


        public IActionResult AddBrandName(int? IdBrandName)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListBrandName = iBrandName.GetAll();
            if (IdBrandName != null)
            {
                vmodel.BrandName = iBrandName.GetById(Convert.ToInt32(IdBrandName));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBBrandName slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdBrandName = model.BrandName.IdBrandName;
                slider.BrandName = model.BrandName.BrandName;
                slider.Active = model.BrandName.Active;
                slider.DataEntry = model.BrandName.DataEntry;
                slider.DateTimeEntry = model.BrandName.DateTimeEntry;
                slider.CurrentState = model.BrandName.CurrentState;
                if (slider.IdBrandName == 0 || slider.IdBrandName == null)
                {
                    if (dbcontext.TBBrandNames.Where(a => a.BrandName == slider.BrandName).ToList().Count > 0)
                    {
                        TempData["BrandName"] = ResourceWeb.VLBrandNameDoplceted;
                        return RedirectToAction("AddBrandName", model);
                    }
                    var reqwest = iBrandName.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyBrandName");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iBrandName.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyBrandName");
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
        public IActionResult DeleteData(int IdBrandName)
        {
            var reqwistDelete = iBrandName.deleteData(IdBrandName);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyBrandName");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyBrandName");

            }
        }
    }
}