using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Mvc;

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
    }
}
