using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryAPIController : ControllerBase
    {
        IIProductCategory iProductCategory;
        MasterDbcontext dbcontext;
        public ProductCategoryAPIController(IIProductCategory iProductCategory1, MasterDbcontext dbcontext1)
        {
            iProductCategory = iProductCategory1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iProductCategory.GetAllAPI();
            return Ok(allData);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            var allData = await iProductCategory.GetAllvAPI(id);
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iProductCategory.GetByIdAPI(id);
            return Ok(Data);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var Data = await iProductCategory.GetByNameAPI(name);
            if (Data == null)
                return NotFound();

            return Ok(Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddData(TBProductCategory model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iProductCategory.SaveDataAPI(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBProductCategory model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iProductCategory.UpdateDataAPI(model);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await GetById(id);
            if (item == null)
                return NoContent();

            await iProductCategory.DeleteDataAPI(id);
            return Ok(item);
        }
    }
}
