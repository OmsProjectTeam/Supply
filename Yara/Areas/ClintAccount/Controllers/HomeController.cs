
using Domin.Entity;
using Infarstuructre.BL;
using System.Diagnostics;

namespace Yara.Areas.ClintAccount.Controllers;

[Area("ClintAccount")]
[Authorize(Roles = "Admin,Customer")]
public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
	IIUserInformation iUserInformation;


    public HomeController(UserManager<ApplicationUser> userManager,IIUserInformation iUserInformation1)
	{
		_userManager = userManager;
		iUserInformation= iUserInformation1;
     
    }
	public async Task<IActionResult> Index(string userId)
	{
        ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
        var userd = vmodel.sUser = iUserInformation.GetById(userId);

        var user = await _userManager.FindByIdAsync(userId);
        //var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

       

        return View(vmodel);
    }

	public async Task<IActionResult> IndexAr(string userId)
	{
        ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();
        var userd = vmodel.sUser = iUserInformation.GetById(userId);

        var user = await _userManager.FindByIdAsync(userId);
        //var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

       

        return View(vmodel);
    }
}
