using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsControllerAPI : ControllerBase
    {
        IIMerchants iMerchants;
        MasterDbcontext dbcontext;
        public MerchantsControllerAPI(IIMerchants iMerchants1, MasterDbcontext dbcontext1)
        {
            iMerchants = iMerchants1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iMerchants.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iMerchants.GetByIdAsync(id);
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData(TBMerchants model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iMerchants.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBMerchants model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iMerchants.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await GetById(id);
            if (item == null)
                return NoContent();

            await iMerchants.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
