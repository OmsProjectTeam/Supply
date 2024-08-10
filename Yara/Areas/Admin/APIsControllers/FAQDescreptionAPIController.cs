using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class FAQDescreptionAPIController : ControllerBase
    {
        IIFAQDescreption iFAQDscription;
        MasterDbcontext dbcontext;
        public FAQDescreptionAPIController(IIFAQDescreption iFAQDscription1, MasterDbcontext dbcontext1)
        {
            iFAQDscription = iFAQDscription1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iFAQDscription.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            var allData = await iFAQDscription.GetAllvAsync(id);
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iFAQDscription.GetByIdAsync(id);
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData(TBFAQDescreption model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iFAQDscription.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBFAQDescreption model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iFAQDscription.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await GetById(id);
            if (item == null)
                return NoContent();

            await iFAQDscription.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
