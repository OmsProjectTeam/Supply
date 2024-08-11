using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yara.Areas.Admin.Controllers;

namespace Yara.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsAPIController : ControllerBase
    {
        AccountsController AccountsController;
        MasterDbcontext dbcontext;
        public AccountsAPIController(AccountsController accountsController, MasterDbcontext dbcontext = null)
        {
            AccountsController = accountsController;
            this.dbcontext = dbcontext;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRolesAsync(ViewmMODeElMASTER model)
        {
            if (ModelState.IsValid)
            {
                var result = await AccountsController.Roles(model);
                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoleAsync(string Id)
        {
            var result = await AccountsController.DeleteRole(Id);
            return Ok(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            var result = await AccountsController.Registers(model);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            var result = AccountsController.DeleteUser(userId);
            return Ok(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePasswordAsync(ViewmMODeElMASTER model1, RegisterViewModel? model)
        {
            var result = await AccountsController.ChangePassword1(model1, model);
            return Ok(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginViewModel model, string returnUrl)
        {
            var result = await AccountsController.Login(model, returnUrl);
            return Ok(result);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            var result = await AccountsController.Logout1();
            return Ok(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> RegistersAsync(RegisterViewModel model)
        {
            var result = await AccountsController.Registers1(model);
            return Ok(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> RegistersCustomerAsync(RegisterViewModel model)
        {
            var result = await AccountsController.RegistersCustomer(model);
            return Ok(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> RegistersMerchantAsync(RegisterViewModel model)
        {
            var result = await AccountsController.RegistersMerchant(model);
            return Ok(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> RegistersAirFreightAsync(RegisterViewModel model)
        {
            var result = await AccountsController.RegistersAirFreight(model);
            return Ok(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> RegistersEdite(ViewmMODeElMASTER model, List<IFormFile> Files, string returnUrl, string? Id)
        {
            var result = await AccountsController.RegistersEdite(model, Files, returnUrl, Id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditRolesUserAsync(ViewmMODeElMASTER model)
        {
            var result = await AccountsController.AddEditRolesUser(model);
            return Ok(result);
        }


    }
}
