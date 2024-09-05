using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsLetterGroupController : Controller
    {
        IINewsLettersGroup iNewsLettersGroup;
        IIUserInformation iUserInformation;
        MasterDbcontext dbcontext;
        public NewsLetterGroupController(IINewsLettersGroup iNewsLettersGroup1, IIUserInformation iUserInformation1, MasterDbcontext dbcontext1)
        {
             iNewsLettersGroup = iNewsLettersGroup1;
            iUserInformation = iUserInformation1;
            dbcontext = dbcontext1;
        }

        public IActionResult MyNewsLetterGroups(int? userId)
        {
            ViewmMODeElMASTER viewm = new ViewmMODeElMASTER();
            viewm.ListNewsLetterGroup = iNewsLettersGroup.GetAll();

            return View(viewm);
        }

        public IActionResult AddNewsLetterGroup(int? IdNewsletterGroup)
        {
            ViewmMODeElMASTER viewm = new ViewmMODeElMASTER();
            viewm.ListNewsLetterGroup = iNewsLettersGroup.GetAll();

            if (IdNewsletterGroup != null) 
            {
                viewm.NewsletterGroup = iNewsLettersGroup.GetById(Convert.ToInt32(IdNewsletterGroup));
            }

            ViewBag.users = iUserInformation.GetAll();

            return View(viewm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBNewsletterGroup slider, string returnUrl)
        {
            try
            {
                slider.IdNewsletterGroup = model.NewsletterGroup.IdNewsletterGroup;
                slider.GroupName = model.NewsletterGroup.GroupName;
                slider.Active = model.NewsletterGroup.Active;
                slider.Email = model.NewsletterGroup.Email;
                slider.IdUser = model.NewsletterGroup.IdUser;
                slider.DataEntry = model.NewsletterGroup.DataEntry;
                slider.DateTimeEntry = model.NewsletterGroup.DateTimeEntry;
                slider.CurrentState = model.NewsletterGroup.CurrentState;
                if (slider.IdNewsletterGroup == 0 || slider.IdNewsletterGroup == null)
                {
                    if (dbcontext.TBNewsletterGroups.Where(a => a.GroupName == slider.GroupName).ToList().Count > 0)
                    {
                        TempData["GroupName"] = ResourceWeb.VLFAQDoplceted;
                        return RedirectToAction("AddNewsLetterGroup", model);
                    }

                    var reqwest = iNewsLettersGroup.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyNewsLetterGroups");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect("AddNewsLetterGroup");
                    }
                }
                else
                {
                    var reqestUpdate = iNewsLettersGroup.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyNewsLetterGroups");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return Redirect("AddNewsLetterGroup");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return Redirect("AddNewsLetterGroup");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdNewsletterGroup)
        {
            var reqwistDelete = iNewsLettersGroup.DeleteData(IdNewsletterGroup);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyNewsLetterGroups");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyNewsLetterGroups");

            }
        }
    }
}
