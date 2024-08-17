using Domin.Entity.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageAPIController : ControllerBase
    {
        private readonly IIMessageChat iMessageChat;
        private readonly MasterDbcontext dbcontext;
        public ChatMessageAPIController(IIMessageChat iMessageChat1, MasterDbcontext dbcontext1)
        {
            iMessageChat = iMessageChat1;
            dbcontext = dbcontext1;
        }

        [HttpGet("GetAllBySenderId/{SenderId}")]
        public async Task<IActionResult> GetAllBySenderId(string SenderId)
        {
            var allData = await iMessageChat.GetBySenderIdAsync(SenderId);
            if (allData == null)

                return NoContent();

            return Ok(allData);
        }

        [HttpGet("GetAllByReciverId/{ReciverId}")]
        public async Task<IActionResult> GetAllByReciverId(string ReciverId)
        {
            var allData = await iMessageChat.GetByReciverIdAsync(ReciverId);
            if (allData == null)

                return NoContent();

            return Ok(allData);
        }

        [HttpGet("GetAllBySenderIdAndReciverId/{sId}/{rId}")]
        public async Task<IActionResult> GetAllBySenderIdAndReciverId(string sId, string rId)
        {
            var allData = await iMessageChat.GetBySenderIdAndReciverIdAsync(sId, rId);
            if (allData == null)

                return NoContent();

            return Ok(allData);
        }

        [HttpGet("GetByReciverIdLast/{rId}")]
        public async Task<IActionResult> GetByReciverIdLast(string rId)
        {
            var allData = await iMessageChat.GetByReciverIdLastAsync(rId);
            if (allData == null)

                return NoContent();

            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var allData = await iMessageChat.GetByIdAsync(id);
            if (allData == null)
                return NoContent();

            return Ok(allData);
        }


        [HttpPost]
        public async Task<IActionResult> AddData([FromBody] TBMessageChat model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iMessageChat.saveDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody] TBMessageChat model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iMessageChat.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await iMessageChat.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            await iMessageChat.deleteDataAsync(id);
            return Ok(item);
        }
    }
}
