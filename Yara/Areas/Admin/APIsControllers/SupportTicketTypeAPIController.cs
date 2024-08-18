using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTicketTypeAPIController : ControllerBase
    {
        IISupportTicketType iSupportTicketType;
        MasterDbcontext dbcontext;
        public SupportTicketTypeAPIController(IISupportTicketType iSupportTicketType1, MasterDbcontext dbcontext1)
        {
            iSupportTicketType = iSupportTicketType1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iSupportTicketType.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iSupportTicketType.GetByIdAsync(id);
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData([FromBody] TBSupportTicketType model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iSupportTicketType.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody] TBSupportTicketType model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iSupportTicketType.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await iSupportTicketType.GetByIdAsync(id);
            if (item == null)
                return NoContent();

            await iSupportTicketType.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
