using Domin.Entity.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageAPIController : ControllerBase
    {
        IIMessageChat iMessageChat;
        MasterDbcontext dbcontext;
        public ChatMessageAPIController(IIMessageChat iMessageChat, MasterDbcontext dbcontext)
        {
            this.iMessageChat = iMessageChat;
            this.dbcontext = dbcontext;
        }

        [HttpGet("GetAllBySenderId/{id}")]
        public async Task<IActionResult> GetAllBySenderId(string id)
        {
            var allData = await iMessageChat.GetBySenderIdAsync(id);
            if (allData == null)
            
                return NoContent();
            
            return Ok(allData);
        }

        [HttpGet("GetAllByReciverId/{id}")]
        public async Task<IActionResult> GetAllByReciverId(string id)
        {
            var allData = await iMessageChat.GetByReciverIdAsync(id);
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
        public async Task<IActionResult> AddData(TBMessageChat model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iMessageChat.saveDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBMessageChat model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iMessageChat.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await GetById(id);
            if (item == null)
                return NoContent();

            await iMessageChat.deleteDataAsync(id);
            return Ok(item);
        }
    }
}
