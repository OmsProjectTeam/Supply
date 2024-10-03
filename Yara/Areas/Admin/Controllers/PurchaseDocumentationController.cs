

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PurchaseDocumentationController : Controller
    {
        MasterDbcontext dbcontext;
        IIPurchaseDocumentation iPurchaseDocumentation;
        public PurchaseDocumentationController(MasterDbcontext dbcontext1,IIPurchaseDocumentation iPurchaseDocumentation1)
        {
            dbcontext = dbcontext1;
            iPurchaseDocumentation = iPurchaseDocumentation1;
        }
        public IActionResult MyPurchaseDocumentation(string? userId)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ListPurchaseDocumentation = iPurchaseDocumentation.GetAll();

            return View(viewmMODeElMASTER);
        }

    
        public IActionResult AddPurchaseDocumentation(int? IdPurchaseDocumentation)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ListPurchaseDocumentation = iPurchaseDocumentation.GetAll();
            if (IdPurchaseDocumentation != null)
            {
                viewmMODeElMASTER.PurchaseDocumentation = iPurchaseDocumentation.GetById(Convert.ToInt32(IdPurchaseDocumentation));
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
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBPurchaseDocumentation slider, string returnUrl)
        {
            try
            {
                slider.IdPurchaseDocumentation = model.PurchaseDocumentation.IdPurchaseDocumentation;
                slider.OrderNumber = model.PurchaseDocumentation.OrderNumber;
                slider.StartDate = model.PurchaseDocumentation.StartDate;
                slider.TransactionDate = model.PurchaseDocumentation.TransactionDate;
                slider.CostOrder = model.PurchaseDocumentation.CostOrder;            
                slider.DateTimeEntry = model.PurchaseDocumentation.DateTimeEntry;
                slider.CurrentState = model.PurchaseDocumentation.CurrentState;
                slider.DataEntry = model.PurchaseDocumentation.DataEntry;
                if (slider.IdPurchaseDocumentation == 0 || slider.IdPurchaseDocumentation == null)
                {
                    if (dbcontext.TBPurchaseDocumentations.Where(a => a.OrderNumber == slider.OrderNumber).ToList().Count > 0)
                    {
                        TempData["PurchaseDocumentation"] = ResourceWeb.VLPurchaseDocumentationDoplceted;
                        return RedirectToAction("AddPurchaseDocumentation", model);
                    }
                    var reqwest = iPurchaseDocumentation.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyPurchaseDocumentation");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddPurchaseDocumentation");
                    }
                }
                else
                {
                    var reqestUpdate = iPurchaseDocumentation.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyPurchaseDocumentation");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddPurchaseDocumentation");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddPurchaseDocumentation");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdPurchaseDocumentation)
        {
            var reqwistDelete = iPurchaseDocumentation.deleteData(IdPurchaseDocumentation);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyPurchaseDocumentation");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("AddPurchaseDocumentation");
            }
        }
    }
}
