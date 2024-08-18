using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseAPIController : ControllerBase
    {
        private readonly IIWareHouse iWareHouse;
        private readonly MasterDbcontext dbcontext;
        public WareHouseAPIController(IIWareHouse iWareHouse1, MasterDbcontext dbcontext1)
        {
            iWareHouse = iWareHouse1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iWareHouse.GetAllAPI();
            return Ok(allData);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            var allData = await iWareHouse.GetAllvAPI(id);
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iWareHouse.GetByIdAPI(id);
            return Ok(Data);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var Data = await iWareHouse.GetByNameAPI(name);
            if (Data == null)
                return NotFound();

            return Ok(Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddData(TBWareHouse model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iWareHouse.SaveDataAPI(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBWareHouse model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iWareHouse.UpdateDataAPI(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await iWareHouse.GetByIdAPI(id);
            if (item == null)
                return NoContent();

            await iWareHouse.DeleteDataAPI(id);
            return Ok(item);
        }
    }
}
