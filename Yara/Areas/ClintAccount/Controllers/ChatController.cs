using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.ClintAccount.Controllers
{
    [Area("ClintAccount")]
    [Authorize(Roles = "Admin,Customer")]
    public class ChatController : Controller
    {
        IIConnectAndDisconnect iConnectAndDisconnect;
        IIUserInformation iUserInformation;
        IIMessageChat iMessageChat;
        UserManager<ApplicationUser> iUserManager;
        IIFAQ iFAQ;
        IIFAQList iFAQList;
        IIFAQDescreption iFAQDescreption;
        MasterDbcontext dbcontext;
        public ChatController(IIConnectAndDisconnect iConnectAndDisconnect1, IIMessageChat iMessageChat1, IIUserInformation iUserInformation1, UserManager<ApplicationUser> iUserManager1, IIFAQ iFAQ1, IIFAQDescreption iFAQDescreption1, IIFAQList iFAQList1, MasterDbcontext dbcontext)
        {
            iConnectAndDisconnect = iConnectAndDisconnect1;
            iMessageChat = iMessageChat1;
            iUserInformation = iUserInformation1;
            iUserManager = iUserManager1;
            iFAQ = iFAQ1;
            iFAQDescreption = iFAQDescreption1;
            iFAQList = iFAQList1;
            this.dbcontext = dbcontext;
        }

        public async Task<IActionResult> Index(string anotherId)
        {
            var viewModel = new ViewmMODeElMASTER();
            var currentUserId = iUserManager.GetUserId(User);

            // Retrieve the messages for the selected chat
            if (!string.IsNullOrEmpty(anotherId))
            {
                var IamSender = iMessageChat.GetBySenderIdAndReciverId(currentUserId, anotherId);
                var IamReciver = iMessageChat.GetBySenderIdAndReciverId(anotherId, currentUserId);
                IamSender.AddRange(IamReciver);

                viewModel.ViewChatMessage = IamSender.OrderBy(m => m.MessageeTime).ToList();

                // Set the ViewBag properties
                ViewBag.another = iUserInformation.GetById(anotherId)?.UserName;
                ViewBag.anotherId = anotherId;
                ViewBag.img = iUserInformation.GetById(currentUserId)?.ImageUser;
                ViewBag.UserId = currentUserId;
                //ViewBag.LastSeen = iConnectAndDisconnect.GetById(anotherId)?.LastSeen;
            }

            // Fetching all messages received by the current user (for the contacts list)
            viewModel.ViewChatMessage = iMessageChat.GetByReciverId(currentUserId);

            return View(viewModel);
        }

        [HttpGet]
        [Route("/ClintAccount/Chat/OwnChat/{anotherId}")]
        public async Task<IActionResult> OwnChat(string anotherId)
        {
            var viewModel = new ViewmMODeElMASTER();
            var currentUserId = iUserManager.GetUserId(User);

            var IamSender = iMessageChat.GetBySenderIdAndReciverId(currentUserId, anotherId);
            var IamReciver = iMessageChat.GetBySenderIdAndReciverId(anotherId, currentUserId);
            IamSender.AddRange(IamReciver);

            viewModel.ViewChatMessage = IamSender.OrderBy(m => m.MessageeTime).ToList();
            ViewBag.another = iUserInformation.GetById(anotherId).UserName;
            ViewBag.anotherId = anotherId;
            ViewBag.img = iUserInformation.GetById(currentUserId).ImageUser;
            ViewBag.UserId = currentUserId;

            return RedirectToAction("Index", new { anotherId });
        }

        [HttpPost]
        [Route("ClintAccount/Chat/UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Ok("null");

            string fileName = Guid.NewGuid().ToString();
            var filePath = Path.Combine("wwwroot/Images/Home/", fileName + file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath = $"/Images/Home/{file.FileName}" });
        }

        [HttpGet]
        [Route("/ClintAccount/Chat/GetMessages")]
        public async Task<IActionResult> GetMessages(string anotherId)
        {
            var currentUserId = iUserManager.GetUserId(User);

            var IamSender = iMessageChat.GetBySenderIdAndReciverId(currentUserId, anotherId);
            var IamReciver = iMessageChat.GetBySenderIdAndReciverId(anotherId, currentUserId);
            IamSender.AddRange(IamReciver);

            var messages = IamSender.OrderBy(m => m.MessageeTime).Select(m => new
            {
                m.Message,
                m.SenderId,
                m.ReciverId,
                m.ImgMsg,
                m.MessageeTime
            }).ToList();

            return Json(messages);
        }
    }
}
