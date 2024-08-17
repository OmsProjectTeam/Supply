using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailAlartSettingAPIController : ControllerBase
    {
        private readonly IIEmailAlartSetting iEmailAlartSetting;
        private readonly MasterDbcontext dbcontext;
        public EmailAlartSettingAPIController(IIEmailAlartSetting iEmailAlartSetting1, MasterDbcontext dbcontext1)
        {
            iEmailAlartSetting = iEmailAlartSetting1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iEmailAlartSetting.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            var allData = await iEmailAlartSetting.GetAllvAsync(id);
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iEmailAlartSetting.GetByIdAsync(id);
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData(TBEmailAlartSetting model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iEmailAlartSetting.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBEmailAlartSetting model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iEmailAlartSetting.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await iEmailAlartSetting.GetByIdAsync(id);
            if (item == null)
                return NoContent();

            await iEmailAlartSetting.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
