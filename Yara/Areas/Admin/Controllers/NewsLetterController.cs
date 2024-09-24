using Infarstuructre.BL;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsLetterController : Controller
    {
        IINewsLetters iNewsLetters;
        IINewsLettersGroup iNewsGroup;
        IITemplate iTemplate;
        MasterDbcontext dbcontext;
        public NewsLetterController(IINewsLetters iNewsLetters1, MasterDbcontext _dbcontext, IINewsLettersGroup iNewsGroup1, IITemplate iTemplate1)
        {
            iNewsLetters = iNewsLetters1;
            dbcontext = _dbcontext;
            iNewsGroup = iNewsGroup1;
            iTemplate = iTemplate1;
        }
        public IActionResult MyNewsLetters(int? userId)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ListNewsLetter = iNewsLetters.GetAll();
            return View(viewmMODeElMASTER);
        }

        public IActionResult AddNewsLetters(int? IdNewsletter)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ListNewsLetter = iNewsLetters.GetAll();

            if (IdNewsletter != null)
                viewmMODeElMASTER.Newsletter = iNewsLetters.GetById(Convert.ToInt32(IdNewsletter));
            ViewBag.NewsLetterGroups = iNewsGroup.GetAll();
            ViewBag.Templates = iTemplate.GetAll();
            return View(viewmMODeElMASTER);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBNewsletter slider, string returnUrl)
        {
            try
            { 
                slider.IdNewsletter = model.Newsletter.IdNewsletter;
                slider.IdNewsletterGroup = model.Newsletter.IdNewsletterGroup;
                slider.IdTemplate = model.Newsletter.IdTemplate;
                slider.Title = model.Newsletter.Title;
                slider.DataEntry = model.Newsletter.DataEntry;
                slider.DateTimeEntry = model.Newsletter.DateTimeEntry;
                slider.CurrentState = model.Newsletter.CurrentState;
                if (slider.IdNewsletter == 0 || slider.IdNewsletter == null)
                {
                    if (dbcontext.TBNewsletters.Where(a => a.Title == slider.Title).ToList().Count > 0)
                    {
                        TempData["Title"] = ResourceWeb.VLFAQDoplceted;
                        return RedirectToAction("AddNewsLetters", model);
                    }

                    var reqwest = iNewsLetters.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyNewsLetters");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect("AddNewsLetters");
                    }
                }
                else
                {
                    var reqestUpdate = iNewsLetters.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyNewsLetters");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return Redirect("AddNewsLetters");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return Redirect("AddNewsLetters");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdNewsletter)
        {
            var reqwistDelete = iNewsLetters.DeleteData(IdNewsletter);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyNewsLetters");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyNewsLetters");

            }
        }
    }
}
