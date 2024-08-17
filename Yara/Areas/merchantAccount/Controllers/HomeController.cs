using Azure;
using Domin.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Yara.Areas.merchantAccount.Controllers
{
    [Area("merchantAccount")]
    [Authorize(Roles = "Admin,Merchant")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        IIUserInformation iUserInformation;
        private readonly IIOrder iOrder;
      
        public HomeController(UserManager<ApplicationUser> userManager, IIUserInformation iUserInformation1, IIOrder iOrder)
        {
            _userManager = userManager;
            iUserInformation = iUserInformation1;
            this.iOrder = iOrder;
         
        }
        public async Task<IActionResult> Index(string userId)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            var userd = vmodel.sUser = iUserInformation.GetById(userId);

            var user = await _userManager.FindByIdAsync(userId);
            //var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            string phoneNumber = user.PhoneNumber;
            //if (!string.IsNullOrEmpty(phoneNumber))
            //{
            //    vmodel.NewOrders = (await iOrderNew.GetOrdersByPhoneAsync(phoneNumber)).ToList();
            //    vmodel.OldOrders = (await iOrder.GetOrdersByPhoneAsync(phoneNumber)).ToList();
            //}

            return View(vmodel);
        }

       

       
    }
}
