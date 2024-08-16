using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Yara.Areas.ClintAccount.Controllers
{
    [Area("ClintAccount")]
    [Authorize(Roles = "Admin,Customer")]

    public class CustomerMessagesController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        IITypesOfMessage iTypesOfMessage;
        IICustomerMessages iCustomerMessages;
        IIUserInformation iUserInformation;
        MasterDbcontext dbcontext;
        public CustomerMessagesController(UserManager<ApplicationUser> userManager, IITypesOfMessage iTypesOfMessage1, IICustomerMessages iCustomerMessages1, IIUserInformation iUserInformation1, MasterDbcontext dbcontext1)
        {
            _userManager = userManager;
            iTypesOfMessage = iTypesOfMessage1;
            iCustomerMessages = iCustomerMessages1;
            iUserInformation = iUserInformation1;
            dbcontext = dbcontext1;
        }

        public async Task<IActionResult> MyCustomerMessages()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewCustomerMessages = iCustomerMessages.GetAllDataentry(user.UserName);
            return View(vmodel);
        }

        public async Task<IActionResult> MyCustomerMessagesAr()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewCustomerMessages = iCustomerMessages.GetAllDataentry(user.UserName);
            return View(vmodel);
        }

        public async Task<IActionResult> AddCustomerMessages(int? IdCustomerMessages)
        {

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewCustomerMessages = iCustomerMessages.GetAll();
            if (IdCustomerMessages != null)
            {
                vmodel.CustomerMessages = iCustomerMessages.GetById(Convert.ToInt32(IdCustomerMessages));
                return View(vmodel);
            }
            //return View(vmodel);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var userSelectList = new List<SelectListItem>
{
                      new SelectListItem
            {
                Value = user.Id.ToString(), // تحويل القيمة إلى نص
                Text = user.Name
            }
};

            ViewBag.User = new SelectList(userSelectList, "Value", "Text");


            ViewBag.TypesOfMessage = iTypesOfMessage.GetAll();


            //  ViewBag.OrderStatus = iOrderStatus.GetAll();

            return View(vmodel);

        }

        public async Task<IActionResult> AddCustomerMessagesAr(int? IdCustomerMessages)
        {

            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewCustomerMessages = iCustomerMessages.GetAll();
            if (IdCustomerMessages != null)
            {
                vmodel.CustomerMessages = iCustomerMessages.GetById(Convert.ToInt32(IdCustomerMessages));
                return View(vmodel);
            }
            //return View(vmodel);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            var userSelectList = new List<SelectListItem>
{
                      new SelectListItem
            {
                Value = user.Id.ToString(), // تحويل القيمة إلى نص
                Text = user.Name
            }
};
            ViewBag.User = new SelectList(userSelectList, "Value", "Text");
            ViewBag.TypesOfMessage = iTypesOfMessage.GetAll();
            //  ViewBag.OrderStatus = iOrderStatus.GetAll();

            return View(vmodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBCustomerMessages slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdCustomerMessages = model.CustomerMessages.IdCustomerMessages;
                slider.IdTypesOfMessage = model.CustomerMessages.IdTypesOfMessage;
                slider.IdUser = model.CustomerMessages.IdUser;
                slider.Title = model.CustomerMessages.Title;
                slider.MessageDescription = model.CustomerMessages.MessageDescription;
                slider.DataEntry = model.CustomerMessages.DataEntry;
                slider.DateTimeEntry = model.CustomerMessages.DateTimeEntry;
                slider.CurrentState = model.CustomerMessages.CurrentState;
                if (slider.IdCustomerMessages == 0 || slider.IdCustomerMessages == null)
                {
                    var reqwest = iCustomerMessages.saveData(slider);
                    if (reqwest == true)
                    {


                        //send email
                        var emailSetting = await dbcontext.TBEmailAlartSettings
                           .OrderByDescending(n => n.IdEmailAlartSetting)
                           .Where(a => a.CurrentState == true && a.Active == true)
                           .FirstOrDefaultAsync();

                        // التحقق من وجود إعدادات البريد الإلكتروني
                        if (emailSetting != null)
                        {
                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress("New new message", emailSetting.MailSender));
                            message.To.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            message.Subject = "رسالة جديدة  :" + slider.DataEntry;
                            var builder = new BodyBuilder
                            {
                                TextBody = $"رسالة جديدة \n" +
                                           $"العنوان: {slider.Title}\n" +
                                           $"تاريخ : {slider.DateTimeEntry}\n" +
                                           $"الرسالة: {slider.MessageDescription}\n"

                            };

                            //// إضافة الصورة كملف مرفق إذا كانت موجودة
                            //if (!string.IsNullOrEmpty(slider.Photo))
                            //{
                            //    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home", slider.Photo);
                            //    builder.Attachments.Add(imagePath);
                            //}

                            message.Body = builder.ToMessageBody();

                            using (var client = new SmtpClient())
                            {
                                await client.ConnectAsync(emailSetting.SmtpServer, emailSetting.PortServer, SecureSocketOptions.StartTls);
                                await client.AuthenticateAsync(emailSetting.MailSender, emailSetting.PasswordEmail);
                                await client.SendAsync(message);
                                await client.DisconnectAsync(true);
                            }
                        }
                        else
                        {
                            // التعامل مع الحالة التي لا توجد فيها إعدادات البريد الإلكتروني
                            // يمكنك تسجيل خطأ أو تنفيذ إجراءات أخرى هنا
                        }

                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyCustomerMessages");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iCustomerMessages.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyCustomerMessages");
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
        public async Task<IActionResult> SaveAr(ViewmMODeElMASTER model, TBCustomerMessages slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdCustomerMessages = model.CustomerMessages.IdCustomerMessages;
                slider.IdTypesOfMessage = model.CustomerMessages.IdTypesOfMessage;
                slider.IdUser = model.CustomerMessages.IdUser;
                slider.Title = model.CustomerMessages.Title;
                slider.MessageDescription = model.CustomerMessages.MessageDescription;
                slider.DataEntry = model.CustomerMessages.DataEntry;
                slider.DateTimeEntry = model.CustomerMessages.DateTimeEntry;
                slider.CurrentState = model.CustomerMessages.CurrentState;
                if (slider.IdCustomerMessages == 0 || slider.IdCustomerMessages == null)
                {

                    var reqwest = iCustomerMessages.saveData(slider);
                    if (reqwest == true)
                    {



                        //send email
                        var emailSetting = await dbcontext.TBEmailAlartSettings
                           .OrderByDescending(n => n.IdEmailAlartSetting)
                           .Where(a => a.CurrentState == true && a.Active == true)
                           .FirstOrDefaultAsync();

                        // التحقق من وجود إعدادات البريد الإلكتروني
                        if (emailSetting != null)
                        {
                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress("New new message", emailSetting.MailSender));
                            message.To.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            message.Subject = "رسالة جديدة  :" + slider.DataEntry;
                            var builder = new BodyBuilder
                            {
                                TextBody = $"رسالة جديدة \n" +
                                           $"العنوان: {slider.Title}\n" +
                                           $"تاريخ : {slider.DateTimeEntry}\n" +
                                           $"الرسالة: {slider.MessageDescription}\n"

                            };

                            //// إضافة الصورة كملف مرفق إذا كانت موجودة
                            //if (!string.IsNullOrEmpty(slider.Photo))
                            //{
                            //    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home", slider.Photo);
                            //    builder.Attachments.Add(imagePath);
                            //}

                            message.Body = builder.ToMessageBody();

                            using (var client = new SmtpClient())
                            {
                                await client.ConnectAsync(emailSetting.SmtpServer, emailSetting.PortServer, SecureSocketOptions.StartTls);
                                await client.AuthenticateAsync(emailSetting.MailSender, emailSetting.PasswordEmail);
                                await client.SendAsync(message);
                                await client.DisconnectAsync(true);
                            }
                        }
                        else
                        {
                            // التعامل مع الحالة التي لا توجد فيها إعدادات البريد الإلكتروني
                            // يمكنك تسجيل خطأ أو تنفيذ إجراءات أخرى هنا
                        }
                        TempData["Saved successfully"] = ResourceWeb.VLSavedSuccessfully;
                        return RedirectToAction("MyCustomerMessagesAr");
                    }
                    else
                    {
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    var reqestUpdate = iCustomerMessages.UpdateData(slider);
                    if (reqestUpdate == true)
                    {
                        TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                        return RedirectToAction("MyCustomerMessagesAr");
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
        public IActionResult DeleteData(int IdCustomerMessages)
        {
            var reqwistDelete = iCustomerMessages.deleteData(IdCustomerMessages);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MyCustomerMessages");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MyCustomerMessages");
            }
        }

       
    }
}
