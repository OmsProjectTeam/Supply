
namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ScrapingHtmlTitleController : Controller
    {
        MasterDbcontext dbcontext;
        IIScrapingHtmlTitle iScrapingHtmlTitle;
        public ScrapingHtmlTitleController(MasterDbcontext dbcontext1,IIScrapingHtmlTitle iScrapingHtmlTitle1)
        {
            dbcontext=dbcontext1;
            iScrapingHtmlTitle =iScrapingHtmlTitle1;
        }

        public IActionResult MyScrapingHtmlTitle(string? userId)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ListScrapingHtmlTitle = iScrapingHtmlTitle.GetAll();

            return View(viewmMODeElMASTER);
        }


        public IActionResult AddScrapingHtmlTitle(int? IdScrapingHtmlTitle)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ListScrapingHtmlTitle = iScrapingHtmlTitle.GetAll();
            if (IdScrapingHtmlTitle != null)
            {
                viewmMODeElMASTER.ScrapingHtmlTitle = iScrapingHtmlTitle.GetById(Convert.ToInt32(IdScrapingHtmlTitle));
                return View(viewmMODeElMASTER);
            }
            else
            {
                return View(viewmMODeElMASTER);
            }
           
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBScrapingHtmlTitle slider, string returnUrl)
        {
            try
            {
                slider.IdScrapingHtmlTitle = model.ScrapingHtmlTitle.IdScrapingHtmlTitle;
                slider.ScrapingHtmlTitle = model.ScrapingHtmlTitle.ScrapingHtmlTitle;     
                slider.DateTimeEntry = model.ScrapingHtmlTitle.DateTimeEntry;
                slider.CurrentState = model.ScrapingHtmlTitle.CurrentState;
                slider.DataEntry = model.ScrapingHtmlTitle.DataEntry;
                slider.Active = model.ScrapingHtmlTitle.Active;
                if (slider.IdScrapingHtmlTitle == 0 || slider.IdScrapingHtmlTitle == null)
                {
                    if (dbcontext.TBScrapingHtmlTitles.Where(a => a.ScrapingHtmlTitle == slider.ScrapingHtmlTitle).ToList().Count > 0)
                    {
                        TempData["ScrapingHtmlTitle"] = ResourceWeb.VLScrapingHtmlTitleDoplceted;
                        return RedirectToAction("AddScrapingHtmlTitle", model);
                    }
                    var reqwest = iScrapingHtmlTitle.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyScrapingHtmlTitle");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddScrapingHtmlTitle");
                    }
                }
                else
                {
                    var reqestUpdate = iScrapingHtmlTitle.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyScrapingHtmlTitle");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return RedirectToAction("AddScrapingHtmlTitle");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return RedirectToAction("AddScrapingHtmlTitle");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdScrapingHtmlTitle)
        {
            var reqwistDelete = iScrapingHtmlTitle.deleteData(IdScrapingHtmlTitle);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyScrapingHtmlTitle");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("AddScrapingHtmlTitle");
            }
        }
    }
}
