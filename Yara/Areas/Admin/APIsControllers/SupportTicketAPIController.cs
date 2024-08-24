using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTicketAPIController : ControllerBase
    {
        private readonly IISupportTicket iSupportTicket;
        private readonly MasterDbcontext dbcontext;
        ApiResponse ApiResponse;
        public SupportTicketAPIController(IISupportTicket iSupportTicket1, MasterDbcontext dbcontext1)
        {
            iSupportTicket = iSupportTicket1;
            dbcontext = dbcontext1;
            ApiResponse = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allData = await iSupportTicket.GetAllAsync();
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
                var data = await iSupportTicket.GetByIdAsync(id);
                if (data == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

                ApiResponse.Result = data;
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
        public async Task<IActionResult> AddData([FromBody] TBSupportTicket model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode=System.Net.HttpStatusCode.BadRequest;

                ApiResponse.Result = model;
                await iSupportTicket.AddDataAsync(model);
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
        public async Task<IActionResult> UpdateData([FromBody] TBSupportTicket model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                ApiResponse.Result = model;
                await iSupportTicket.UpdateDataAsync(model);
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
                var item = await iSupportTicket.GetByIdAsync(id);
                if (item == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.NoContent;

                await iSupportTicket.DeleteDataAsync(id);
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
