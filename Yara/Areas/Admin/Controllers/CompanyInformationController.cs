

using Infarstuructre.BL;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CompanyInformationController : Controller
    {
        MasterDbcontext dbcontext;
        IICompanyInformation iCompanyInformation;
        public CompanyInformationController(MasterDbcontext dbcontext1,IICompanyInformation iCompanyInformation1)
        {
            dbcontext=dbcontext1;
            iCompanyInformation=iCompanyInformation1;
        }
        public IActionResult MYCompanyInformation()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformatione = iCompanyInformation.GetAll();
            return View(vmodel);
        }
        public async Task<IActionResult> AddEditCompanyInformation(int? IdCompanyInformation)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformatione = iCompanyInformation.GetAll();
            if (IdCompanyInformation != null)
            {
                vmodel.CompanyInformation = iCompanyInformation.GetById(Convert.ToInt32(IdCompanyInformation));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        public async Task<IActionResult> AddEditCompanyInformationImage(int? IdCompanyInformation)
        {
          
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListCompanyInformatione = iCompanyInformation.GetAll();
            if (IdCompanyInformation != null)
            {
                vmodel.CompanyInformation = iCompanyInformation.GetById(Convert.ToInt32(IdCompanyInformation));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBCompanyInformation slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdCompanyInformation = model.CompanyInformation.IdCompanyInformation;     
                slider.Photo = model.CompanyInformation.Photo;
                slider.PhoneNumber = model.CompanyInformation.PhoneNumber;
                slider.EmailCompany = model.CompanyInformation.EmailCompany;
                slider.AddressEn = model.CompanyInformation.AddressEn;
                slider.ShortDescriptionEn = model.CompanyInformation.ShortDescriptionEn;
                slider.UrlFaceBook = model.CompanyInformation.UrlFaceBook;
                slider.UrlTwitter = model.CompanyInformation.UrlTwitter;
                slider.UrlInstgram = model.CompanyInformation.UrlInstgram;
                slider.UrlMap = model.CompanyInformation.UrlMap;
                slider.PhoneNumber2 = model.CompanyInformation.PhoneNumber2;
                slider.DataEntry = model.CompanyInformation.DataEntry;
                slider.DateTimeEntry = model.CompanyInformation.DateTimeEntry;
                slider.CurrentState = model.CompanyInformation.CurrentState;
                var file = HttpContext.Request.Form.Files;
                if (slider.IdCompanyInformation == 0 || slider.IdCompanyInformation == null)
                {

                    if (dbcontext.TBCompanyInformations.Where(a => a.CompanyName == slider.CompanyName).ToList().Count > 0)
                    {
                        TempData["CompanyName"] = ResourceWeb.VLCompanyNameDoplceted;
                        return RedirectToAction("AddEditCompanyInformation", model);
                    }

                    if (file.Count() > 0)
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                    }
                    var reqwest = iCompanyInformation.saveData(slider);
                    if (reqwest == true)
                    {
                        //send email
                       
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MYCompanyInformation");
                    }
                    else
                    {
                        var PhotoNAme = slider.Photo;
                        var delet = iCompanyInformation.DELETPHOTOWethError(PhotoNAme);
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    //var reqweistDeletPoto = iCompanyInformation.DELETPHOTO(slider.IdCompanyInformation);

                    if (file.Count() == 0)

                    {
                        slider.Photo = model.CompanyInformation.Photo;
                        //TempData["Message"] = ResourceWeb.VLimageuplode;
                        var reqestUpdate2 = iCompanyInformation.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYCompanyInformation");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            //var delet = iCompanyInformation.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return Redirect(returnUrl);
                        }
                    }
                    else
                    {
                        var reqweistDeletPoto = iCompanyInformation.DELETPHOTO(slider.IdCompanyInformation);
                        var reqestUpdate2 = iCompanyInformation.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYCompanyInformation");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iCompanyInformation.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return Redirect(returnUrl);
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
                    //var delet = iCompanyInformation.DELETPHOTOWethError(PhotoNAme);
                    TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                    return Redirect(returnUrl);
                }
                else
                {
                    var PhotoNAme = slider.Photo;
                    var delet = iCompanyInformation.DELETPHOTOWethError(PhotoNAme);
                    TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                    return Redirect(returnUrl);
                }

            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdCompanyInformation)
        {
            var reqwistDelete = iCompanyInformation.deleteData(IdCompanyInformation);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYCompanyInformation");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYCompanyInformation");
            }
        }



      
    }
}
