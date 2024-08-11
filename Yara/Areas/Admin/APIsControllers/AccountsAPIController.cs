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
        private readonly AccountsController _accountsController;

        public AccountsAPIController(AccountsController accountsController)
        {
            _accountsController = accountsController;
        }

        [HttpPost("AddRolesAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRolesAsync([FromBody] ViewmMODeElMASTER model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.Roles(model);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteRoleAsync/{Id}")]
        public async Task<IActionResult> DeleteRoleAsync(string Id)
        {
            var result = await _accountsController.DeleteRole(Id);
            return Ok(result);
        }

        [HttpPost("RegisterAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.Registers(model);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUserAsync/{userId}")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            var result = await _accountsController.DeleteUser(userId);
            return Ok(result);
        }

        [HttpPost("ChangePasswordAsync")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] RegisterViewModel? model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.ChangePassword1(new ViewmMODeElMASTER(), model);
            return Ok(result);
        }

        [HttpPost("LoginAsync")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.Login(model, returnUrl);
            return Ok(result);
        }

        [HttpPost("LogoutAsync")]
        public async Task<IActionResult> LogoutAsync()
        {
            var result = await _accountsController.Logout1();
            return Ok(result);
        }

        [HttpPost("RegistersAsync")]
        public async Task<IActionResult> RegistersAsync([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.Registers1(model);
            return Ok(result);
        }

        [HttpPost("RegistersCustomerAsync")]
        public async Task<IActionResult> RegistersCustomerAsync([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.RegistersCustomer(model);
            return Ok(result);
        }

        [HttpPost("RegistersMerchantAsync")]
        public async Task<IActionResult> RegistersMerchantAsync([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.RegistersMerchant(model);
            return Ok(result);
        }

        [HttpPost("RegistersAirFreightAsync")]
        public async Task<IActionResult> RegistersAirFreightAsync([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.RegistersAirFreight(model);
            return Ok(result);
        }

        [HttpPost("RegistersEdite/{Id}")]
        public async Task<IActionResult> RegistersEdite([FromBody] ViewmMODeElMASTER model, [FromHeader] List<IFormFile> Files, string returnUrl, string? Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.RegistersEdite(model, Files, returnUrl, Id);
            return Ok(result);
        }

        [HttpPost("AddEditRolesUserAsync")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEditRolesUserAsync([FromBody] ViewmMODeElMASTER model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountsController.AddEditRolesUser(model);
            return Ok(result);
        }
    }
}
