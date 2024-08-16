using Infarstuructre.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FAQListController : Controller
	{
        IIFAQ iFAQ;
        IIFAQList iFAQList;
        MasterDbcontext dbcontext;
        public FAQListController(IIFAQ iFAQ1, IIFAQList iFAQList1, MasterDbcontext dbcontext1)
        {
            iFAQ = iFAQ1;
            iFAQList = iFAQList1;
            dbcontext = dbcontext1;
        }
        public IActionResult MyFAQList()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListFAQList = iFAQList.GetAll();
            return View(vmodel);
        }

        public IActionResult MyFAQListAr()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListFAQList = iFAQList.GetAll();
            return View(vmodel);
        }
        public IActionResult AddFAQList(int? IdFAQ)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

            ViewBag.FAQ = iFAQ.GetAll();

            vmodel.ListFAQList = iFAQList.GetAll();
            if (IdFAQ != null)
            {
                vmodel.FAQList = iFAQList.GetByIdFAQList(Convert.ToInt32(IdFAQ));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        public IActionResult AddFAQListAr(int? IdFAQ)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

            ViewBag.FAQ = iFAQ.GetAll();

            vmodel.ListFAQList = iFAQList.GetAll();
            if (IdFAQ != null)
            {
                vmodel.FAQList = iFAQList.GetByIdFAQList(Convert.ToInt32(IdFAQ));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBFAQList slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdFAQ = model.FAQList.IdFAQ;
                slider.IdFAQList = model.FAQList.IdFAQList;
                slider.DateEntry = model.FAQList.DateEntry;
                slider.ListFAQ = model.FAQList.ListFAQ;
                slider.DateTimeEntry = model.FAQList.DateTimeEntry;
                slider.CurrentState = model.FAQList.CurrentState;
                if (slider.IdFAQList == 0 || slider.IdFAQList == null)
                {
                    if (dbcontext.TBFAQLists.Where(a => a.ListFAQ == slider.ListFAQ).ToList().Count > 0)
                    {
                        TempData["FAQ"] = ResourceWeb.VLFAQDoplceted;
                        return RedirectToAction("AddFAQList", model);
                    }

                    var reqwest = iFAQList.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyFAQList");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iFAQList.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyFAQList");
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
        public IActionResult DeleteData(int IdFAQ)
        {
            var reqwistDelete = iFAQList.deleteData(IdFAQ);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyFAQList");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyFAQList");

            }
            // تمرير التاسكات  من الادارة 
            // استخدام نظام أجايا وجيرا 


        }

       
    }
}
