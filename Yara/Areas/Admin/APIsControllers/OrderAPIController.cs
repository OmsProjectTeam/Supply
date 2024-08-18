using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        private readonly IIOrder iOrder;
        private readonly MasterDbcontext dbcontext;
        public OrderAPIController(IIOrder iOrder1, MasterDbcontext dbcontext1)
        {
            iOrder = iOrder1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iOrder.GetAllAsync();
            if (allData == null)
                return NotFound();
            return Ok(allData);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            var allData = await iOrder.GetAllvAsync(id);
            if (allData == null)
                return NotFound();

            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iOrder.GetByIdAsync(id);
            if (Data == null)
                return NotFound();
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData(TBOrder model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iOrder.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBOrder model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iOrder.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await iOrder.GetByIdAsync(id);
            if (item == null)
                return NoContent();

            await iOrder.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
