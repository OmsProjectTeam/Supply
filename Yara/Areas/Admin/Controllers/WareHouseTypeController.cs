using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WareHouseTypeController : Controller
    {
        IIWareHouseType iWareHouseType;
        MasterDbcontext dbcontext;

        public WareHouseTypeController(MasterDbcontext dbcontext1, IIWareHouseType iWareHouseType1)
        {
            dbcontext = dbcontext1;
            iWareHouseType = iWareHouseType1;

        }
        public IActionResult MyWareHouseType(string? userId)
        {
           ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ViewWareHouseType = iWareHouseType.GetAll();

            return View(viewmMODeElMASTER);
        }

        public IActionResult AddWareHouseType(int? IdWareHouseType)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ViewWareHouseType = iWareHouseType.GetAll();
            if (IdWareHouseType != null)
            {
                vmodel.WareHouseType = iWareHouseType.GetById(Convert.ToInt32(IdWareHouseType));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBWareHouseType slider, string returnUrl)
        {
            try
            {
                slider.IdWareHouseType = model.WareHouseType.IdWareHouseType;
                slider.WareHouseType = model.WareHouseType.WareHouseType;
                slider.Active = model.WareHouseType.Active;
                slider.DataEntry = model.WareHouseType.DataEntry;
                slider.DateTimeEntry = model.WareHouseType.DateTimeEntry;
                slider.CurrentState = model.WareHouseType.CurrentState;
                if (slider.IdWareHouseType == 0 || slider.IdWareHouseType == null)
                {
                    if (dbcontext.TBWareHouseTypes.Where(a => a.WareHouseType == slider.WareHouseType).ToList().Count > 0)
                    {
                        TempData["WareHouseType"] = ResourceWeb.VLWareHouseTypeDoplceted;
                        return RedirectToAction("AddWareHouseType", model);
                    }

                    var reqwest = iWareHouseType.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyWareHouseType");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddWareHouseType");
                    }
                }
                else
                {
                    var reqestUpdate = iWareHouseType.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyWareHouseType");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddWareHouseType");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddWareHouseType");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdWareHouseType)
        {
            var reqwistDelete = iWareHouseType.deleteData(IdWareHouseType);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyWareHouseType");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyWareHouseType");
            }
        }

    }
}
