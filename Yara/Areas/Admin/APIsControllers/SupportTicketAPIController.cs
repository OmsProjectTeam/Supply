using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTicketAPIController : ControllerBase
    {
        IISupportTicket iSupportTicket;
        MasterDbcontext dbcontext;
        public SupportTicketAPIController(IISupportTicket iSupportTicket1, MasterDbcontext dbcontext1)
        {
            iSupportTicket = iSupportTicket1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iSupportTicket.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iSupportTicket.GetByIdAsync(id);
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData(TBSupportTicket model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iSupportTicket.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBSupportTicket model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iSupportTicket.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await GetById(id);
            if (item == null)
                return NoContent();

            await iSupportTicket.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
