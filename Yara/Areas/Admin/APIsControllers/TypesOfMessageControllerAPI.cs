using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class TypesOfMessageControllerAPI : ControllerBase
    {
        IITypesOfMessage iTypesOfMessage;
        MasterDbcontext dbcontext;
        public TypesOfMessageControllerAPI(IITypesOfMessage iTypesOfMessage1, MasterDbcontext dbcontext1)
        {
            iTypesOfMessage = iTypesOfMessage1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iTypesOfMessage.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iTypesOfMessage.GetByIdAsync(id);
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData(TBTypesOfMessage model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iTypesOfMessage.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBTypesOfMessage model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iTypesOfMessage.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await GetById(id);
            if (item == null)
                return NoContent();

            await iTypesOfMessage.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
