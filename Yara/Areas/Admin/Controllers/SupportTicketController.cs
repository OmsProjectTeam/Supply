

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SupportTicketController : Controller
    {
        IISupportTicket iSupportTicket;
        IISupportTicketStatus iSupportTicketStatus;
        IISupportTicketType iSupportTicketType;
        IIUserInformation iUserInformation;
        MasterDbcontext dbcontext;
        public SupportTicketController(IISupportTicket iSupportTicket1,IISupportTicketStatus iSupportTicketStatus1,IISupportTicketType iSupportTicketType1, IIUserInformation iUserInformation1,MasterDbcontext dbcontext1)
        {
            iSupportTicket=iSupportTicket1;
            iSupportTicketStatus=iSupportTicketStatus1;
            iSupportTicketType=iSupportTicketType1;
            iUserInformation=iUserInformation1;
            dbcontext=dbcontext1;
        }
        public IActionResult MYSupportTicket()
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewSupportTicket = iSupportTicket.GetAll();
            return View(vmodel);
        } 
        public async Task<IActionResult> AddEditSupportTicket(int? IdSupportTicket)
        {


            var maxTicketNo = await dbcontext.TBSupportTickets.MaxAsync(ticket => (int?)ticket.SupportTicketNo) ?? 0;
            var nextTicketNo = maxTicketNo + 1;
            ViewBag.SupportTicketNo = nextTicketNo;
            ViewBag.SupportTicketStatus = iSupportTicketStatus.GetAll();
            ViewBag.SupportTicketType = iSupportTicketType.GetAll();
            ViewBag.user = iUserInformation.GetAllByNameall();


      
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewSupportTicket = iSupportTicket.GetAll();
            if (IdSupportTicket != null)
            {
                vmodel.SupportTicket = iSupportTicket.GetById(Convert.ToInt32(IdSupportTicket));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }
        public async Task<IActionResult> AddEditSupportTicketImage(int? IdSupportTicket)
        {
            var maxTicketNo = await dbcontext.TBSupportTickets.MaxAsync(ticket => (int?)ticket.SupportTicketNo) ?? 0;
            var nextTicketNo = maxTicketNo + 1;
            ViewBag.SupportTicketNo = nextTicketNo;
            ViewBag.SupportTicketStatus = iSupportTicketStatus.GetAll();
            ViewBag.SupportTicketType = iSupportTicketType.GetAll();
            ViewBag.user = iUserInformation.GetAllByNameall();
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            vmodel.ListViewSupportTicket = iSupportTicket.GetAll();
            if (IdSupportTicket != null)
            {
                vmodel.SupportTicket = iSupportTicket.GetById(Convert.ToInt32(IdSupportTicket));
                return View(vmodel);
            }
            else
            {
                return View(vmodel);
            }
        }  
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(ViewmMODeElMASTER model, TBSupportTicket slider, List<IFormFile> Files, string returnUrl)
        {
            try
            {
                slider.IdSupportTicket = model.SupportTicket.IdSupportTicket;
                slider.IdSupportTicketType = model.SupportTicket.IdSupportTicketType;
                slider.IdSupportTicketStatus = model.SupportTicket.IdSupportTicketStatus;
                slider.IdUser = model.SupportTicket.IdUser;
                slider.SupportTicketNo = model.SupportTicket.SupportTicketNo;
                slider.FollowUpMail = model.SupportTicket.FollowUpMail;
                slider.TicketDate = model.SupportTicket.TicketDate;
                slider.Titel = model.SupportTicket.Titel;
                slider.Description = model.SupportTicket.Description;
                slider.Photo = model.SupportTicket.Photo;
                slider.DataEntry = model.SupportTicket.DataEntry;
                slider.DateTimeEntry = model.SupportTicket.DateTimeEntry;
                slider.CurrentState = model.SupportTicket.CurrentState;            
                var file = HttpContext.Request.Form.Files;
                if (slider.IdSupportTicket == 0 || slider.IdSupportTicket == null)
                {
                    if (file.Count() > 0)
                    {
                        string Photo = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/Images/Home", Photo), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        slider.Photo = Photo;
                        fileStream.Close();
                    }
                    var reqwest = iSupportTicket.saveData(slider);
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
                            message.From.Add(new MailboxAddress("New Ticket Support", emailSetting.MailSender));
                            message.To.Add(new MailboxAddress("saif aldin", "saifaldin_s@hotmail.com"));
                            message.Subject = "تذكرة دعم جديدة من :" + slider.DataEntry;
                            var builder = new BodyBuilder
                            {
                                TextBody = $"تذكرة دعم\n" +
                                           $"رقم التذكرة: {slider.SupportTicketNo}\n" +
                                           $"بريد المتابعة: {slider.FollowUpMail}\n" +
                                           $"تاريخ التذكرة: {slider.TicketDate}\n" +
                                           $"عنوان التذكرة: {slider.Titel}\n" +
                                           $"الوصف: {slider.Description}\n" 
                                         
                            };
                            // إضافة الصورة كملف مرفق إذا كانت موجودة
                            if (!string.IsNullOrEmpty(slider.Photo))
                            {
                                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Home", slider.Photo);
                                builder.Attachments.Add(imagePath);
                            }

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
                        return RedirectToAction("MYSupportTicket");
                    }
                    else
                    {
                        var PhotoNAme = slider.Photo;
                        var delet = iSupportTicket.DELETPHOTOWethError(PhotoNAme);
                        TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    //var reqweistDeletPoto = iSupportTicket.DELETPHOTO(slider.IdSupportTicket);

                    if (file.Count() == 0)

                    {
                        slider.Photo = model.SupportTicket.Photo;
                        //TempData["Message"] = ResourceWeb.VLimageuplode;
                        var reqestUpdate2 = iSupportTicket.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYSupportTicket");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            //var delet = iSupportTicket.DELETPHOTOWethError(PhotoNAme);
                            TempData["ErrorSave"] = ResourceWeb.VLErrorUpdate;
                            return Redirect(returnUrl);
                        }
                    }
                    else
                    {
                        var reqweistDeletPoto = iSupportTicket.DELETPHOTO(slider.IdSupportTicket);
                        var reqestUpdate2 = iSupportTicket.UpdateData(slider);
                        if (reqestUpdate2 == true)
                        {
                            TempData["Saved successfully"] = ResourceWeb.VLUpdatedSuccessfully;
                            return RedirectToAction("MYSupportTicket");
                        }
                        else
                        {
                            var PhotoNAme = slider.Photo;
                            var delet = iSupportTicket.DELETPHOTOWethError(PhotoNAme);
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
                    //var delet = iSupportTicket.DELETPHOTOWethError(PhotoNAme);
                    TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                    return Redirect(returnUrl);
                }
                else
                {
                    var PhotoNAme = slider.Photo;
                    var delet = iSupportTicket.DELETPHOTOWethError(PhotoNAme);
                    TempData["ErrorSave"] = ResourceWeb.VLErrorSave;
                    return Redirect(returnUrl);
                }

            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteData(int IdSupportTicket)
        {
            var reqwistDelete = iSupportTicket.deleteData(IdSupportTicket);
            if (reqwistDelete == true)
            {
                TempData["Saved successfully"] = ResourceWeb.VLdELETESuccessfully;
                return RedirectToAction("MYSupportTicket");
            }
            else
            {
                TempData["ErrorSave"] = ResourceWeb.VLErrorDeleteData;
                return RedirectToAction("MYSupportTicket");
            }
        }

       


    }
}
