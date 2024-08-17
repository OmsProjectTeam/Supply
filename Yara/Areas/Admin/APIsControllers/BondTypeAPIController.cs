using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class BondTypeAPIController : ControllerBase
    {
        private readonly IIBondType iBondType;
        private readonly MasterDbcontext dbcontext;
        public BondTypeAPIController(IIBondType iBondType1, MasterDbcontext dbcontext1)
        {
            iBondType = iBondType1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iBondType.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            var allData = await iBondType.GetAllvAsync(id);
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iBondType.GetByIdAsync(id);
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData([FromBody] TBBondType model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iBondType.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody] TBBondType model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iBondType.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await iBondType.GetByIdAsync(id);
            if (item == null)
                return NoContent();

            await iBondType.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
