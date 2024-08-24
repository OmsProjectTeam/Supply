using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTicketStatusAPIController : ControllerBase
    {

        private readonly IISupportTicketStatus iSupportTicketStatus;
        private readonly MasterDbcontext dbcontext;
        ApiResponse ApiResponse;
        public SupportTicketStatusAPIController(IISupportTicketStatus iSupportTicketStatus1, MasterDbcontext dbcontext1)
        {
            iSupportTicketStatus = iSupportTicketStatus1;
            dbcontext = dbcontext1;
            ApiResponse = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allData = await iSupportTicketStatus.GetAllAsync();
                if (allData == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

                ApiResponse.Result = allData;
                return Ok(ApiResponse);
            }catch (Exception ex)
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
                var allData = await iSupportTicketStatus.GetByIdAsync(id);
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
        public async Task<IActionResult> AddData([FromBody] TBSupportTicketStatus model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iSupportTicketStatus.AddDataAsync(model);
                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
                ApiResponse.IsSuccess = false;
            }
            return Ok(ApiResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody] TBSupportTicketStatus model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iSupportTicketStatus.UpdateDataAsync(model);
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
                var item = await iSupportTicketStatus.GetByIdAsync(id);
                if (item == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.NoContent;

                await iSupportTicketStatus.DeleteDataAsync(id);
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
