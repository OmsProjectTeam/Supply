using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInformationController : ControllerBase
    {
        IIProductInformation iProductInformation;
        MasterDbcontext dbcontext;
        public ProductInformationController(IIProductInformation iProductInfo1, MasterDbcontext dbcontext1)
        {
            iProductInformation = iProductInfo1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iProductInformation.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            var allData = await iProductInformation.GetAllvAsync(id);
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iProductInformation.GetByIdAsync(id);
            return Ok(Data);
        }

        [HttpPost("AddData")]
        public async Task<IActionResult> AddData(TBProductInformation model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iProductInformation.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBProductInformation model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iProductInformation.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await GetById(id);
            if (item == null)
                return NoContent();

            await iProductInformation.DeleteDataAsync(id);
            return Ok(item);
        }

        [HttpPost("DeletePhoto")]
        public async Task<bool> DeletePhoto(int id)
        {
            var result = await iProductInformation.DELETPHOTOAsync(id);
            return result;
        }

        [HttpPost("DeletePhotoWithError")]
        public async Task<bool> DeletePhotoWithError(string name)
        {
            var result = await iProductInformation.DELETPHOTOWITHERRORAsync(name);
            return result;
        }
    }
}
