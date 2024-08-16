namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MerchantsController : Controller
    {
        MasterDbcontext dbcontext;
        IIMerchants iMerchants;
        IIUserInformation iUserInformation;
        UserManager<ApplicationUser> _userManager;
        public MerchantsController(MasterDbcontext dbcontext1,IIMerchants iMerchants1, UserManager<ApplicationUser> userManager, IIUserInformation iUserInformation1)
        {
            dbcontext = dbcontext1;
            iMerchants = iMerchants1;
            iUserInformation = iUserInformation1;
            _userManager = userManager;
        }
        public IActionResult MYMerchants()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.listViewMerchants = iMerchants.GetAll();
            return View(vmodel);
        }
        public IActionResult AddEditMerchants(int? IdMerchants)
        {
            ViewBag.user = iUserInformation.GetAllByRole("Merchant,Admin");
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.listViewMerchants = iMerchants.GetAll();
            if (IdMerchants != null)
            {
                vmodel.Merchants = iMerchants.GetById(Convert.ToInt32(IdMerchants));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        public IActionResult AddEditMerchantsImage(int? IdMerchants)
        {
            ViewBag.user = iUserInformation.GetAllByRole("Merchant,Admin");
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.listViewMerchants = iMerchants.GetAll();
            if (IdMerchants != null)
            {
                vmodel.Merchants = iMerchants.GetById(Convert.ToInt32(IdMerchants));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBMerchants slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdMerchants = model.Merchants.IdMerchants;
                slider.IdUserIdentity = model.Merchants.IdUserIdentity;
                slider.MerchantPhone = model.Merchants.MerchantPhone;
                slider.MerchantEmaile = model.Merchants.MerchantEmaile;
                slider.MerchantWeb = model.Merchants.MerchantWeb;
                slider.MerchantAddres = model.Merchants.MerchantAddres;
                slider.MerchantOnerName = model.Merchants.MerchantOnerName;
                slider.MerchantOnerPhone = model.Merchants.MerchantOnerPhone;
                slider.MerchantOnerEmail = model.Merchants.MerchantOnerEmail;                       
                slider.Photo = model.Merchants.Photo;         
                slider.Active = model.Merchants.Active;
                slider.DateTimeEntry = model.Merchants.DateTimeEntry;
                slider.DataEntry = model.Merchants.DataEntry;
                slider.CurrentState = model.Merchants.CurrentState;           
                var file = HttpContext.Request.Form.Files;
                if (slider.IdMerchants == 0 || slider.IdMerchants == null)
                {
                    if (file.Count() > 0)
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                    }
                    else
                    {
                        TempData["Message"] = ResourceWeb.VLimageuplode;
                        return RedirectToAction("AddEditMerchants");
                    }
                    if (dbcontext.TBMerchantss.Where(a => a.IdUserIdentity == slider.IdUserIdentity).ToList().Count > 0)
                    {
                        var PhotoNAme = slider.Photo;
                        var delet = iMerchants.DELETPHOTOWethError(PhotoNAme);

                        TempData["MerchantName"] = ResourceWeb.VLMerchantNameDoplceted;
                        return RedirectToAction("AddEditMerchants", model);
                    }
                    var reqwest = iMerchants.saveData(slider);
                    if (reqwest == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYMerchants");
                    }
                    else
                    {
                        var PhotoNAme = slider.Photo;
                        var delet = iMerchants.DELETPHOTOWethError(PhotoNAme);
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return RedirectToAction("AddEditMerchants");
                    }
                }
                else
                {
                   
                    if (file.Count() == 0)

                    {
                        slider.Photo = model.Merchants.Photo;
                        //TempData["Message"] = ResourceWeb.VLimageuplode;
                        var reqestUpdate2 = iMerchants.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYMerchants");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            //var delet = iMerchants.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditMerchantsImage");
                        }
                    }
                    else
                    {
                        var reqweistDeletPoto = iMerchants.DELETPHOTO(slider.IdMerchants);
                        var reqestUpdate2 = iMerchants.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYMerchants");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iMerchants.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return RedirectToAction("AddEditMerchants");
                        }
                    }              
                }
            }
            catch
            {
                var file = HttpContext.Request.Form.Files;
                if (file.Count() == 0)

                {
                    //var PhotoNAme = slider.Photo;
                    //var delet = iMerchants.DELETPHOTOWethError(PhotoNAme);
                    TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                    return RedirectToAction("AddEditMerchantsImage");
                }
                else
                {
                    var PhotoNAme = slider.Photo;
                    var delet = iMerchants.DELETPHOTOWethError(PhotoNAme);
                    TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                    return RedirectToAction("AddEditMerchants");

                }
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdMerchants)
        {
            var reqwistDelete = iMerchants.deleteData(IdMerchants);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYMerchants");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYMerchants");
            }
        }





        [HttpGet]
        public JsonResult GetUserDetails(string id)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == id);
            if (user != null)
            {
                return Json(new
                {
                    MerchantPhone = user.PhoneNumber,
                    MerchantEmaile = user.Email,
                    // أضف هنا المزيد من الخصائص إذا كنت بحاجة إليها
                });
            }
            return Json(new { });
        }
    }
}