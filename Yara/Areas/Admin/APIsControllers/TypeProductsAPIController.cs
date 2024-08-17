using Domin.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class TypeProductsAPIController : ControllerBase
    {
        private readonly IITypesProduct iTypeProduct;
        private readonly MasterDbcontext dbcontext;
        public TypeProductsAPIController(IITypesProduct iTypeProduct1, MasterDbcontext dbcontext1)
        {
            iTypeProduct = iTypeProduct1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iTypeProduct.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iTypeProduct.GetByIdAsync(id);
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData([FromBody] TBTypesProduct model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iTypeProduct.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody] TBTypesProduct model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iTypeProduct.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await iTypeProduct.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            await iTypeProduct.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
