using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInformationController : ControllerBase
    {
        private readonly IIProductInformation iProductInformation;
        private readonly MasterDbcontext dbcontext;
        ApiResponse ApiResponse;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductInformationController(IIProductInformation iProductInfo1, MasterDbcontext dbcontext1, IHttpClientFactory httpClientFactory)
        {
            iProductInformation = iProductInfo1;
            dbcontext = dbcontext1;
            ApiResponse = new ApiResponse();
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allData = await iProductInformation.GetAllAsync();
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

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            try
            {
                var allData = await iProductInformation.GetAllvAsync(id);
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
                var allData = await iProductInformation.GetByIdAsync(id);
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

        [HttpPost("ReturnPhotoUrl/{modelName}")]
        public async Task<ActionResult<string>> ReturnPhotoUrl(string modelName)
        {

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7124");
            var url = $"/Admin/ProductInformation/FetchImageByModel?model={modelName}";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("AddData")]
        public async Task<IActionResult> AddData([FromBody] TBProductInformation model)
        {
            try
            {
                var actionResult = await ReturnPhotoUrl(model.Model);
                if (actionResult.Value != null)
                {
                    model.Photo = actionResult.Value;
                }
                else
                {
                    model.Photo = "Can`t Scrape The WebSite";
                }


                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iProductInformation.AddDataAsync(model);
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
        public async Task<IActionResult> UpdateData([FromBody] TBProductInformation model)
        {
            try
            {
                if (!ModelState.IsValid)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iProductInformation.UpdateDataAsync(model);
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
                var item = await iProductInformation.GetByIdAsync(id);
                if (item == null)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.NoContent;

                await iProductInformation.DeleteDataAsync(id);
                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
            }
            return Ok(ApiResponse);
        }

        [HttpPost("DeletePhoto/{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            try
            {
                var result = await iProductInformation.DELETPHOTOAsync(id);
                if(!result)
                    ApiResponse.StatusCode =System.Net.HttpStatusCode.BadRequest;

                ApiResponse.Result = result;
                return Ok(ApiResponse);
            }
            catch (Exception ex)
            {
                ApiResponse.IsSuccess = false;
                ApiResponse.ErrorMessage = new List<string> { ex.Message };
            }
            return Ok(ApiResponse);
        }

        [HttpPost("DeletePhotoWithError/{name}")]
        public async Task<IActionResult> DeletePhotoWithError(string name)
        {
            try
            {
                var result = await iProductInformation.DELETPHOTOWITHERRORAsync(name);
                if (!result)
                    ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                ApiResponse.Result = result;
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
