using Domin.Entity;
using Infarstuructre.BL;
using Microsoft.AspNetCore.Identity;

namespace Yara.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class FAQController : Controller
	{
		IIFAQ iFAQ;
		//IIMessageChat iMessageChat;
		MasterDbcontext dbcontext;
		IIUserInformation iUserInformation;
		UserManager<ApplicationUser> _userManager;

        public FAQController(IIFAQ iFAQ1, MasterDbcontext dbcontext1, /*IIMessageChat iMessageChat1,*/ IIUserInformation iUserInformation, UserManager<ApplicationUser> _userManager1)
		{
			iFAQ = iFAQ1;
			dbcontext = dbcontext1;
            //iMessageChat = iMessageChat1;
			_userManager = _userManager1;

        }
		public async Task<IActionResult> MyFAQ()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

            var user = await _userManager.GetUserAsync(User);
            vmodel.ListFAQ = iFAQ.GetAll();
            //vmodel.ViewChatMessage = iMessageChat.GetByReciverId(user.Id);

            return View(vmodel);
		}

		public IActionResult MyFAQAr()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListFAQ = iFAQ.GetAll();
			return View(vmodel);
		}
		public IActionResult AddFAQ(int? IdFAQ)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListFAQ = iFAQ.GetAll();
			if (IdFAQ != null)
			{
				vmodel.FAQ = iFAQ.GetById(Convert.ToInt32(IdFAQ));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}

		public IActionResult AddFAQAr(int? IdFAQ)
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListFAQ = iFAQ.GetAll();
			if (IdFAQ != null)
			{
				vmodel.FAQ = iFAQ.GetById(Convert.ToInt32(IdFAQ));
				return View(vmodel);
			}
			else
			{
				return View(vmodel);
			}
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBFAQ slider, List<IFormFile> Files, string returnUrl)
		{
			try
			{
				slider.IdFAQ = model.FAQ.IdFAQ;
				slider.FAQ = model.FAQ.FAQ;
				slider.Active = model.FAQ.Active;
				slider.DateEntry = model.FAQ.DateEntry;
				slider.DateTimeEntry = model.FAQ.DateTimeEntry;
				slider.CurrentState = model.FAQ.CurrentState;
				if (slider.IdFAQ == 0 || slider.IdFAQ == null)
				{
					if (dbcontext.TBFAQs.Where(a => a.FAQ == slider.FAQ).ToList().Count > 0)
					{
						TempData["FAQ"] = ResourceWeb.VLFAQDoplceted;
						return RedirectToAction("AddFAQ", model);
					}

					var reqwest = iFAQ.saveData(slider);
					if (reqwest == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
						return RedirectToAction("MyFAQ");
					}
					else
					{
						TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
						return Redirect(returnUrl);
					}
				}
				else
				{
					var reqestUpdate = iFAQ.UpdateData(slider);
					if (reqestUpdate == true)
					{
						TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
						return RedirectToAction("MyFAQ");
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
			var reqwistDelete = iFAQ.deleteData(IdFAQ);
			if (reqwistDelete == true)
			{
				TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
				return RedirectToAction("MyFAQ");
			}
			else
			{
				TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
				return RedirectToAction("MyFAQ");

			}
			// تمرير التاسكات  من الادارة 
			// استخدام نظام أجايا وجيرا 


		}

	
	}
}
