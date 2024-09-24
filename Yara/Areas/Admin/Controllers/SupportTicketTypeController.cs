

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SupportTicketTypeController : Controller
    {
        MasterDbcontext dbcontext;
        IISupportTicketType iSupportTicketType;
        public SupportTicketTypeController(MasterDbcontext dbcontext1,IISupportTicketType iSupportTicketType1)
        {
            dbcontext=dbcontext1;
            iSupportTicketType=iSupportTicketType1;
        }
        public IActionResult MySupportTicketType()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListSupportTicketType = iSupportTicketType.GetAll();
            return View(vmodel);
        }

  
        public IActionResult AddSupportTicketType(int? IdSupportTicketType)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListSupportTicketType = iSupportTicketType.GetAll();
            if (IdSupportTicketType != null)
            {
                vmodel.SupportTicketType = iSupportTicketType.GetById(Convert.ToInt32(IdSupportTicketType));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

      

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBSupportTicketType slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdSupportTicketType = model.SupportTicketType.IdSupportTicketType;
                slider.SupportTicketType = model.SupportTicketType.SupportTicketType;
              
                slider.DataEntry = model.SupportTicketType.DataEntry;
                slider.DateTimeEntry = model.SupportTicketType.DateTimeEntry;
                slider.CurrentState = model.SupportTicketType.CurrentState;
                if (slider.IdSupportTicketType == 0 || slider.IdSupportTicketType == null)
                {
                    if (dbcontext.TBSupportTicketTypes.Where(a => a.SupportTicketType == slider.SupportTicketType).ToList().Count > 0)
                    {
                        TempData["SupportTicketType"] = ResourceWeb.VLSupportTicketTypeDoplceted;
                        return RedirectToAction("AddSupportTicketType", model);
                    }

                    var reqwest = iSupportTicketType.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MySupportTicketType");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iSupportTicketType.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MySupportTicketType");
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
        public IActionResult DeleteData(int IdSupportTicketType)
        {
            var reqwistDelete = iSupportTicketType.deleteData(IdSupportTicketType);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MySupportTicketType");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MySupportTicketType");

            }



        }

      
    }
}
