using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTicketTypeAPIController : ControllerBase
    {
        private readonly IISupportTicketType iSupportTicketType;
        private readonly MasterDbcontext dbcontext;
        ApiResponse ApiResponse;
        public SupportTicketTypeAPIController(IISupportTicketType iSupportTicketType1, MasterDbcontext dbcontext1)
        {
            iSupportTicketType = iSupportTicketType1;
            dbcontext = dbcontext1;
            ApiResponse = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allData = await iSupportTicketType.GetAllAsync();
                if (allData == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

                ApiResponse.Result = allData;
                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
                ApiResponse.IsSuccess = false;
            }
            return Ok(ApiResponse);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var allData = await iSupportTicketType.GetByIdAsync(id);
                if (allData == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

                ApiResponse.Result = allData;
                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
                ApiResponse.IsSuccess = false;
            }
            return Ok(ApiResponse);
        }

        [HttpPost]
        public async Task<IActionResult> AddData([FromBody] TBSupportTicketType model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iSupportTicketType.AddDataAsync(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
                ApiResponse.IsSuccess = false;
            }
            return Ok(ApiResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody] TBSupportTicketType model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iSupportTicketType.UpdateDataAsync(model);
                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
                ApiResponse.IsSuccess = false;
            }
            return Ok(ApiResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            try
            {
                var item = await iSupportTicketType.GetByIdAsync(id);
                if (item == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.NoContent;

                await iSupportTicketType.DeleteDataAsync(id);
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
