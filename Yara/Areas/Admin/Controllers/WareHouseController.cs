using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Mvc;

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
                vmodel.WareHouseType = iWareHouseType.GetById(Convert.ToInt32(IdBWareHouse));
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
                        TempData["WareHouseType"] = ResourceWeb.VLWareHouseTypeDoplceted;
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
    }
}
