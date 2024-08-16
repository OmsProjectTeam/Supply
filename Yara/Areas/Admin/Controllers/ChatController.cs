using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ChatController : Controller
    {
        MasterDbcontext db;
        IIConnectAndDisconnect iConnectAndDisconnect;
        IIUserInformation iUserInformation;
        IIMessageChat iMessageChat;
        UserManager<ApplicationUser> iUserManager;
        public ChatController(IIConnectAndDisconnect iConnectAndDisconnect1, IIMessageChat iMessageChat1, IIUserInformation iUserInformation1, UserManager<ApplicationUser> iUserManager1, MasterDbcontext db)
        {
            iConnectAndDisconnect = iConnectAndDisconnect1;
            iMessageChat = iMessageChat1;
            iUserInformation = iUserInformation1;
            iUserManager = iUserManager1;
            this.db = db;
        }
        //public async Task<IActionResult> Index()
        //{
        //    ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
        //    var currentUserId = iUserManager.GetUserId(User);

        //    viewmMODeElMASTER.ViewChatMessage = iMessageChat.GetByReciverId(currentUserId);


        //    return View(viewmMODeElMASTER);
        //}
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
        [Route("/Admin/Chat/OwnChat/{anotherId}")]
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
        //      [HttpGet]
        //      [Route("/Admin/Chat/OwnChat/{anotherId}")]
        //      public async Task<IActionResult> OwnChat(string anotherId) 
        //{
        //          ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
        //          var users = viewmMODeElMASTER.ConnectAndDisConnect = iConnectAndDisconnect.GetAll();

        //          var currentUserId = iUserManager.GetUserId(User);

        //          var IamSender = iMessageChat.GetBySenderIdAndReciverId(currentUserId, anotherId);
        //          var IamReciver = iMessageChat.GetBySenderIdAndReciverId(anotherId, currentUserId);
        //          ///////////////////////////////////////////////////////////////////////////////////////////

        //          /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //          foreach (var item in IamReciver)
        //          {
        //              IamSender.Add(item);
        //          }

        //          viewmMODeElMASTER.ViewChatMessage = IamSender;
        //          ViewBag.another = (iUserInformation.GetById(anotherId)).UserName;
        //          ViewBag.anotherId = (iUserInformation.GetById(anotherId)).Id;
        //          //viewmMODeElMASTER.ViewChatMessage = iMessageChat.GetByReciverId(currentUserId);
        //          ViewBag.img = (iUserInformation.GetById(currentUserId)).ImageUser;
        //          ViewBag.UserId = currentUserId;

        //          return View(viewmMODeElMASTER);
        //      }

        // =============================================================================

        //[HttpPost]
        //      [Route("/Admin/chat/uploadFile")]
        //      public async Task<IActionResult> UploadFile(IFormFile file)
        //      {
        //          if (file == null || file.Length == 0)
        //              return Ok("null");

        //          string fileName = Guid.NewGuid().ToString();
        //          var filePath = Path.Combine("wwwroot/Images/Home/", fileName + file.FileName);

        //          using (var stream = new FileStream(filePath, FileMode.Create))
        //          {
        //              await file.CopyToAsync(stream);
        //          }

        //          return Ok(new { filePath = $"/Images/Home/{file.FileName}" });
        //      }
        [HttpPost]
        [Route("/Admin/chat/uploadFile")]
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
        [Route("/Admin/Chat/GetMessages")]
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
