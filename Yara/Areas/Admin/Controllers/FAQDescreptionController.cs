using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FAQDescreptionController : Controller
	{

        IIFAQ iFAQ;
        IIFAQDescreption iFAQDescreption;
        MasterDbcontext dbcontext;
        public FAQDescreptionController(IIFAQ iFAQ1, MasterDbcontext dbcontext1, IIFAQDescreption iFAQDescreption1)
        {
            iFAQ = iFAQ1;
            dbcontext = dbcontext1;
            iFAQDescreption = iFAQDescreption1;
        }
        public IActionResult MyFAQDescreption()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListFAQDescription = iFAQDescreption.GetAll();
            return View(vmodel);
        }

        public IActionResult MyFAQDescreptionAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListFAQDescription = iFAQDescreption.GetAll();
            return View(vmodel);
        }
        public IActionResult AddFAQDescreption(int? IdFAQ)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

            ViewBag.FAQ = iFAQ.GetAll();

			vmodel.ListFAQDescription = iFAQDescreption.GetAll();
            if (IdFAQ != null)
            {
                vmodel.FAQDescreption = iFAQDescreption.GetByIdFAQDescreption(Convert.ToInt32(IdFAQ));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        public IActionResult AddFAQDescreptionAr(int? IdFAQ)
        {
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

			ViewBag.FAQ = iFAQ.GetAll();

			vmodel.ListFAQDescription = iFAQDescreption.GetAll();
			if (IdFAQ != null)
			{
				vmodel.FAQDescreption = iFAQDescreption.GetByIdFAQDescreption(Convert.ToInt32(IdFAQ));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBFAQDescreption slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdFAQDescreption = model.FAQDescreption.IdFAQDescreption;
                slider.IdFAQ = model.FAQDescreption.IdFAQ;
                slider.Descreption = model.FAQDescreption.Descreption;
                slider.DateEntry = model.FAQDescreption.DateEntry;
                slider.DateTimeEntry = model.FAQDescreption.DateTimeEntry;
                slider.CurrentState = model.FAQDescreption.CurrentState;
                if (slider.IdFAQDescreption == 0 || slider.IdFAQDescreption == null)
                {
                    if (dbcontext.TBFAQDescreptions.Where(a => a.Descreption == slider.Descreption).ToList().Count > 0)
                    {
                        TempData["FAQ"] = ResourceWeb.VLFAQDoplceted;
                        return RedirectToAction("MyFAQDescreption", model);
                    }

                    var reqwest = iFAQDescreption.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyFAQDescreption");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iFAQDescreption.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyFAQDescreption");
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

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SaveAr(ViewmMODeElMASTER model, TBFAQDescreption slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdFAQDescreption = model.FAQDescreption.IdFAQDescreption;
                slider.IdFAQ = model.FAQDescreption.IdFAQ;
                slider.Descreption = model.FAQDescreption.Descreption;
                slider.DateEntry = model.FAQDescreption.DateEntry;
                slider.DateTimeEntry = model.FAQDescreption.DateTimeEntry;
                slider.CurrentState = model.FAQDescreption.CurrentState;
                if (slider.IdFAQDescreption == 0 || slider.IdFAQDescreption == null)
                {
                    if (dbcontext.TBFAQDescreptions.Where(a => a.Descreption == slider.Descreption).ToList().Count > 0)
                    {
                        TempData["FAQ"] = ResourceWeb.VLFAQDoplceted;
                        return RedirectToAction("AddFAQDescreptionAr", model);
                    }

                    var reqwest = iFAQDescreption.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyFAQDescreptionAr");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iFAQDescreption.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyFAQDescreptionAt");
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
        public IActionResult DeleteData(int IdFAQDesc)
        {
            var reqwistDelete = iFAQDescreption.deleteData(IdFAQDesc);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyFAQDescreption");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyFAQDescreption");

            }
            // تمرير التاسكات  من الادارة 
            // استخدام نظام أجايا وجيرا 


        }

       
    }
}
