using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseBranchAPIController : ControllerBase
    {
        private readonly IIWareHouseBranch iWareHouseBranch;
        private readonly MasterDbcontext dbcontext;
        public WareHouseBranchAPIController(IIWareHouseBranch iWareHouseBranch1, MasterDbcontext dbcontext1)
        {
            iWareHouseBranch = iWareHouseBranch1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iWareHouseBranch.GetAllAPI();
            return Ok(allData);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            var allData = await iWareHouseBranch.GetAllvAPI(id);
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iWareHouseBranch.GetByIdAPI(id);
            return Ok(Data);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var Data = await iWareHouseBranch.GetByNameAPI(name);
            if (Data == null)
                return NotFound();

            return Ok(Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddData(TBWareHouseBranch model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iWareHouseBranch.SaveDataAPI(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBWareHouseBranch model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iWareHouseBranch.UpdateDataAPI(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await iWareHouseBranch.GetByIdAPI(id);
            if (item == null)
                return NotFound();

            await iWareHouseBranch.DeleteDataAPI(id);
            return Ok(item);
        }
    }
}
