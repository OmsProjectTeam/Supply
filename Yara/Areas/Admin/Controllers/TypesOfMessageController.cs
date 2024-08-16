

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TypesOfMessageController : Controller
    {
        MasterDbcontext dbcontext;
        IITypesOfMessage iTypesOfMessage;
        public TypesOfMessageController(MasterDbcontext dbcontext1,IITypesOfMessage iTypesOfMessage1)
        {
            dbcontext=dbcontext1;
            iTypesOfMessage=iTypesOfMessage1;
        }
        public IActionResult MyTypesOfMessage()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTypesOfMessage = iTypesOfMessage.GetAll();
            return View(vmodel);
        }

      
        public IActionResult AddTypesOfMessage(int? IdTypesOfMessage)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListTypesOfMessage = iTypesOfMessage.GetAll();
            if (IdTypesOfMessage != null)
            {
                vmodel.TypesOfMessage = iTypesOfMessage.GetById(Convert.ToInt32(IdTypesOfMessage));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }    
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBTypesOfMessage slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdTypesOfMessage = model.TypesOfMessage.IdTypesOfMessage;
                slider.TypesOfMessage = model.TypesOfMessage.TypesOfMessage;
                slider.Active = model.TypesOfMessage.Active;
                slider.DataEntry = model.TypesOfMessage.DataEntry;
                slider.DateTimeEntry = model.TypesOfMessage.DateTimeEntry;
                slider.CurrentState = model.TypesOfMessage.CurrentState;
                if (slider.IdTypesOfMessage == 0 || slider.IdTypesOfMessage == null)
                {
                    if (dbcontext.TBTypesOfMessages.Where(a => a.TypesOfMessage == slider.TypesOfMessage).ToList().Count > 0)
                    {
                        TempData["TypesOfMessage"] = ResourceWeb.VLTypesOfMessageDoplceted;
                        return RedirectToAction("AddTypesOfMessage", model);
                    }

                    var reqwest = iTypesOfMessage.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyTypesOfMessage");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iTypesOfMessage.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyTypesOfMessage");
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
        public IActionResult DeleteData(int IdTypesOfMessage)
        {
            var reqwistDelete = iTypesOfMessage.deleteData(IdTypesOfMessage);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyTypesOfMessage");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyTypesOfMessage");

            }
          


        }

      

    }
}
