

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BondTypeController : Controller
    {
        MasterDbcontext dbcontext;
        IIBondType iBondType;
        public BondTypeController(MasterDbcontext dbcontext1,IIBondType iBondType1)
        {
            dbcontext=dbcontext1;
            iBondType=iBondType1;
        }
        public IActionResult MyBondType(string? userId)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ListBondType = iBondType.GetAll();

            return View(viewmMODeElMASTER);
        }

        public IActionResult MyBondTypeAr(string? userId)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ListBondType = iBondType.GetAll();

            return View(viewmMODeElMASTER);
        }
        public IActionResult AddBondType(int? IdBondType)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ListBondType = iBondType.GetAll();
            if (IdBondType != null)
            {
                viewmMODeElMASTER.BondType = iBondType.GetById(Convert.ToInt32(IdBondType));
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
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBBondType slider, string returnUrl)
        {
            try
            {
                slider.IdBondType = model.BondType.IdBondType;
                slider.BondType = model.BondType.BondType;
                slider.Active = model.BondType.Active;
                slider.DataEntry = model.BondType.DataEntry;
                slider.DateTimeEntry = model.BondType.DateTimeEntry;
                slider.CurrentState = model.BondType.CurrentState;
                if (slider.IdBondType == 0 || slider.IdBondType == null)
                {
                    if (dbcontext.TBBondTypes.Where(a => a.BondType == slider.BondType).ToList().Count > 0)
                    {
                        TempData["BondType"] = ResourceWeb.VLBondTypeDoplceted;
                        return RedirectToAction("AddBondType", model);
                    }
                    var reqwest = iBondType.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyBondType");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddBondType");
                    }
                }
                else
                {
                    var reqestUpdate = iBondType.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyBondType");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddBondType");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddBondType");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdBondType)
        {
            var reqwistDelete = iBondType.deleteData(IdBondType);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyBondType");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("AddBondType");
            }
        }
    }
}