using Azure;
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
        ApiResponse ApiResponse;
        public ChatMessageAPIController(IIMessageChat iMessageChat1, MasterDbcontext dbcontext1)
        {
            iMessageChat = iMessageChat1;
            dbcontext = dbcontext1;
        }

        [HttpGet("GetAllBySenderId/{SenderId}")]
        public async Task<IActionResult> GetAllBySenderId(string SenderId)
        {

            try
            {
                var allData = await iMessageChat.GetBySenderIdAsync(SenderId);
                if (allData != null)
                {
                    ApiResponse.Result = allData;
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                }

                ApiResponse.StatusCode = System.Net.HttpStatusCode.NoContent;

                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.ErrorMessage = new List<string> { ex.Message }; 
            }
            return Ok(ApiResponse);
        }

        [HttpGet("GetAllByReciverId/{ReciverId}")]
        public async Task<IActionResult> GetAllByReciverId(string ReciverId)
        {
            try
            {
                var allData = await iMessageChat.GetByReciverIdAsync(ReciverId);
                if (allData == null)
                {
                    ApiResponse.Result = allData;
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                }

                ApiResponse.StatusCode = System.Net.HttpStatusCode.NoContent;

                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.ErrorMessage = new List<string> { ex.Message };

            }

            return Ok(ApiResponse);
        }

        [HttpGet("GetAllBySenderIdAndReciverId/{sId}/{rId}")]
        public async Task<IActionResult> GetAllBySenderIdAndReciverId(string sId, string rId)
        {
            try
            {
                var allData = await iMessageChat.GetBySenderIdAndReciverIdAsync(sId, rId);
                if (allData == null)
                {
                    ApiResponse.Result = allData;
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                }
                ApiResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(ApiResponse);

            }
            catch (Exception ex)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.ErrorMessage = new List<string> { ex.Message };

            }

            return Ok(ApiResponse);
        }

        [HttpGet("GetByReciverIdLast/{rId}")]
        public async Task<IActionResult> GetByReciverIdLast(string rId)
        {
            try
            {
                var allData = await iMessageChat.GetByReciverIdLastAsync(rId);
                if (allData == null)
                {
                    ApiResponse.Result = allData;
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                }
                ApiResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(ApiResponse);

            }
            catch (Exception ex)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.ErrorMessage = new List<string> { ex.Message };

            }

            return Ok(ApiResponse);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var allData = await iMessageChat.GetByIdAsync(id);
                if (allData == null)
                {
                    ApiResponse.Result = allData;
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                }
                ApiResponse.StatusCode = System.Net.HttpStatusCode.NoContent;

                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.ErrorMessage = new List<string> { ex.Message };

            }

            return Ok(ApiResponse);

        }


        [HttpPost]
        public async Task<IActionResult> AddData([FromBody] TBMessageChat model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                ApiResponse.Result = model;
                await iMessageChat.saveDataAsync(model);

                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.ErrorMessage = new List<string> { ex.Message };

            }

            return Ok(ApiResponse);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody] TBMessageChat model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                ApiResponse.Result = model;
                await iMessageChat.UpdateDataAsync(model);

                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.ErrorMessage = new List<string> { ex.Message };

            }

            return Ok(ApiResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            try
            {
                var item = await iMessageChat.GetByIdAsync(id);
                if (item == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iMessageChat.deleteDataAsync(id);
                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
            }
            return Ok(ApiResponse);
        }
    }
}
