﻿using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.merchantAccount.Controllers
{
	[Area("merchantAccount")]
	[Authorize(Roles = "Admin,Merchant")]
	public class FAQController : Controller
	{

		IIFAQ iFAQ;
		IIFAQDescreption iFAQDescreption;
		IIFAQList iFAQList;

		public FAQController(IIFAQ iFAQ1, IIFAQDescreption iFAQDescreption1, IIFAQList iFAQList1)
		{
			iFAQ = iFAQ1;
			iFAQDescreption = iFAQDescreption1;
			iFAQList = iFAQList1;
		}
		public IActionResult MyFAQ()
		{
			ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
			vmodel.ListFAQ = iFAQ.GetAll();
			vmodel.ListFAQDescription = iFAQDescreption.GetAll();
			vmodel.ListFAQList = iFAQList.GetAll();

			return View(vmodel);
		}


		
	}
}
