using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class FAQListAPIController : ControllerBase
    {
        private readonly IIFAQList iFAQList;
        private readonly MasterDbcontext dbcontext;
        public FAQListAPIController(IIFAQList iFAQList1, MasterDbcontext dbcontext1)
        {
            iFAQList = iFAQList1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iFAQList.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iFAQList.GetByIdAsync(id);
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData(TBFAQList model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iFAQList.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBFAQList model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iFAQList.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await iFAQList.GetByIdAsync(id);
            if (item == null)
                return NoContent();

            await iFAQList.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
