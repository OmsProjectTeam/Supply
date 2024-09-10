

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SupportTicketStatusController : Controller
    {
        MasterDbcontext dbcontext;
        IISupportTicketStatus iSupportTicketStatus;
        public SupportTicketStatusController(MasterDbcontext dbcontext1,IISupportTicketStatus iSupportTicketStatus1)
        {
            dbcontext=dbcontext1;
            iSupportTicketStatus=iSupportTicketStatus1;
        }
        public IActionResult MySupportTicketStatus()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListSupportTicketStatus = iSupportTicketStatus.GetAll();
            return View(vmodel);
        }   
        public IActionResult AddSupportTicketStatus(int? IdSupportTicketStatus)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListSupportTicketStatus = iSupportTicketStatus.GetAll();
            if (IdSupportTicketStatus != null)
            {
                vmodel.SupportTicketStatus = iSupportTicketStatus.GetById(Convert.ToInt32(IdSupportTicketStatus));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }      
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBSupportTicketStatus slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdSupportTicketStatus = model.SupportTicketStatus.IdSupportTicketStatus;
                slider.SupportTicketStatus = model.SupportTicketStatus.SupportTicketStatus;

                slider.DataEntry = model.SupportTicketStatus.DataEntry;
                slider.DateTimeEntry = model.SupportTicketStatus.DateTimeEntry;
                slider.CurrentState = model.SupportTicketStatus.CurrentState;
                if (slider.IdSupportTicketStatus == 0 || slider.IdSupportTicketStatus == null)
                {
                    if (dbcontext.TBSupportTicketStatuss.Where(a => a.SupportTicketStatus == slider.SupportTicketStatus).ToList().Count > 0)
                    {
                        TempData["SupportTicketStatus"] = ResourceWeb.VLSupportTicketStatusDoplceted;
                        return RedirectToAction("AddSupportTicketStatus", model);
                    }

                    var reqwest = iSupportTicketStatus.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MySupportTicketStatus");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iSupportTicketStatus.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MySupportTicketStatus");
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
        public IActionResult DeleteData(int IdSupportTicketStatus)
        {
            var reqwistDelete = iSupportTicketStatus.deleteData(IdSupportTicketStatus);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MySupportTicketStatus");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MySupportTicketStatus");

            }



        } 
    }
}
