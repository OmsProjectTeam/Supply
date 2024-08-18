using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class FAQControllerAPI : ControllerBase
    {
        IIFAQ iFAQ;
        MasterDbcontext dbcontext;
        public FAQControllerAPI(IIFAQ iFAQ1, MasterDbcontext dbcontext1)
        {
            iFAQ = iFAQ1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iFAQ.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            var allData = await iFAQ.GetAllvAsync(id);
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iFAQ.GetByIdAsync(id);
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData([FromBody] TBFAQ model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iFAQ.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody]TBFAQ model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iFAQ.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await iFAQ.GetByIdAsync(id);
            if (item == null)
                return NoContent();

            await iFAQ.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
