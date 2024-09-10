using Infarstuructre.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TemplateController : Controller
    {
        IITemplate iTemplate;
        MasterDbcontext dbcontext;
        public TemplateController(IITemplate iTemplate1, MasterDbcontext dbcontext1)
        {
            iTemplate = iTemplate1;
            dbcontext = dbcontext1;
        }
        public IActionResult MyTemplates(string? userId)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ListTemplate = iTemplate.GetAll();
            return View(viewmMODeElMASTER);
        }

        public IActionResult AddTemplate(int? IdTemplate)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            viewmMODeElMASTER.ListTemplate = iTemplate.GetAll(); 
            if (IdTemplate != null)
                viewmMODeElMASTER.Template = iTemplate.GetById(Convert.ToInt32(IdTemplate));

            return View(viewmMODeElMASTER);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBTemplate slider, string returnUrl)
        {
            try
            {
                slider.IdTemplate = model.Template.IdTemplate;
                slider.TemplateName = model.Template.TemplateName;
                slider.Containt = model.Template.Containt;
                slider.DateEntry = model.Template.DateEntry;
                slider.DateTimeEntry = model.Template.DateTimeEntry;
                slider.CurrentState = model.Template.CurrentState;
                slider.Active = model.Template.Active;
                if (slider.IdTemplate == 0 || slider.IdTemplate == null)
                {
                    if (dbcontext.TBTemplates.Where(a => a.TemplateName == slider.TemplateName).ToList().Count > 0)
                    {
                        TempData["Title"] = ResourceWeb.VLFAQDoplceted;
                        return RedirectToAction("AddTemplate", model);
                    }

                    var reqwest = iTemplate.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyTemplates");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect("AddTemplate");
                    }
                }
                else
                {
                    var reqestUpdate = iTemplate.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyTemplates");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                        return Redirect("AddTemplate");
                    }
                }
            }
            catch
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                return Redirect("AddTemplate");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdTemplate)
        {
            var reqwistDelete = iTemplate.DeleteData(IdTemplate);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyTemplates");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyTemplates");

            }
        }
    }
}
