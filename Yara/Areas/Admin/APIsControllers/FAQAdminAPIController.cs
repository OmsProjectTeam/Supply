using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class FAQAdminAPIController : ControllerBase
    {
        private readonly IIFAQ iFAQ;
        private readonly MasterDbcontext dbcontext;
        ApiResponse response;
        public FAQAdminAPIController(IIFAQ iFAQ1, MasterDbcontext dbcontext1)
        {
            iFAQ = iFAQ1;
            dbcontext = dbcontext1;
            response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allData = await iFAQ.GetAllAsync();
                if (allData == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = allData;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }

            return Ok(response);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            try
            {
                var allData = await iFAQ.GetAllvAsync(id);
                if (allData == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = allData;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }

            return Ok(response);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var allData = await iFAQ.GetByIdAsync(id);
                if (allData == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = allData;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddData([FromBody] TBFAQ model)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iFAQ.AddDataAsync(model);

                response.Result = model;
                return Ok(response);
            }

             catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody]TBFAQ model)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iFAQ.UpdateDataAsync(model);
                response.Result = model;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }

            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            try
            {
                var item = await iFAQ.GetByIdAsync(id);
                if (item == null)
                    response.StatusCode = System.Net.HttpStatusCode.NoContent;

                await iFAQ.DeleteDataAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.Message };
            }

            return Ok(response);

        }
    }
}
