using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTicketStatusAPIController : ControllerBase
    {

        IISupportTicketStatus iSupportTicketStatus;
        MasterDbcontext dbcontext;
        public SupportTicketStatusAPIController(IISupportTicketStatus iSupportTicketStatus1, MasterDbcontext dbcontext1)
        {
            iSupportTicketStatus = iSupportTicketStatus1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iSupportTicketStatus.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iSupportTicketStatus.GetByIdAsync(id);
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData(TBSupportTicketStatus model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iSupportTicketStatus.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBSupportTicketStatus model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iSupportTicketStatus.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await GetById(id);
            if (item == null)
                return NoContent();

            await iSupportTicketStatus.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
