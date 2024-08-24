using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class FAQListAPIController : ControllerBase
    {
        private readonly IIFAQList iFAQList;
        private readonly MasterDbcontext dbcontext;
        ApiResponse ApiResponse;
        public FAQListAPIController(IIFAQList iFAQList1, MasterDbcontext dbcontext1)
        {
            iFAQList = iFAQList1;
            dbcontext = dbcontext1;
            ApiResponse = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allData = await iFAQList.GetAllAsync();
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
                var allData = await iFAQList.GetByIdAsync(id);
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
        public async Task<IActionResult> AddData(TBFAQList model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iFAQList.AddDataAsync(model);
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
        public async Task<IActionResult> UpdateData(TBFAQList model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iFAQList.UpdateDataAsync(model);
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
                var item = await iFAQList.GetByIdAsync(id);
                if (item == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.NoContent;

                await iFAQList.DeleteDataAsync(id);
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
