using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseTypeAPIController : ControllerBase
    {
        IIWareHouseType iWareHouseType;
        MasterDbcontext dbcontext;
        public WareHouseTypeAPIController(IIWareHouseType iWareHouseType1, MasterDbcontext dbcontext1)
        {
            iWareHouseType = iWareHouseType1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iWareHouseType.GetAllAPI();
            return Ok(allData);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            var allData = await iWareHouseType.GetAllvAPI(id);
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iWareHouseType.GetByIdAPI(id);
            return Ok(Data);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var Data = await iWareHouseType.GetByNameAPI(name);
            if (Data == null)
                return NotFound();

            return Ok(Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddData(TBWareHouseType model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iWareHouseType.SaveDataAPI(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBWareHouseType model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iWareHouseType.UpdateDataAPI(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {   
            var item = await iWareHouseType.GetByIdAPI(id);
            if (item == null)
                return NotFound();

            await iWareHouseType.DeleteDataAPI(id);
            return Ok(item);
        }
    }
}
