using Infarstuructre.BL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingsViewController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        MasterDbcontext dbcontext;

        IIUserInformation iUserInformation;
        public SettingsViewController(UserManager<ApplicationUser> userManager, MasterDbcontext dbcontext1, IIUserInformation iUserInformation1)
        {
            _userManager = userManager;

            iUserInformation = iUserInformation1;
        }
        public async Task<IActionResult> MySetting(string userId)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
            //vmodel.ListlicationUser = iUserInformation.GetAllByName(user.UserName).Take(1);
            var userd = vmodel.sUser = iUserInformation.GetById(userId);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            // الحصول على دور المستخدم
            var role = await _userManager.GetRolesAsync(user);

            ViewBag.UserRole = role.FirstOrDefault();

            return View(vmodel);
        }
    }
}
