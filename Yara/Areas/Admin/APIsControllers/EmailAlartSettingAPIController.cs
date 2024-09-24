using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailAlartSettingAPIController : ControllerBase
    {
        private readonly IIEmailAlartSetting iEmailAlartSetting;
        private readonly MasterDbcontext dbcontext;
        private ApiResponse response;
        public EmailAlartSettingAPIController(IIEmailAlartSetting iEmailAlartSetting1, MasterDbcontext dbcontext1)
        {
            iEmailAlartSetting = iEmailAlartSetting1;
            dbcontext = dbcontext1;
            response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allData = await iEmailAlartSetting.GetAllAsync();
                if (allData == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = allData;
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
                var allData = await iEmailAlartSetting.GetAllvAsync(id);
                if (allData == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = allData;
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
                var Data = await iEmailAlartSetting.GetByIdAsync(id);
                if (Data == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = Data;
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
        public async Task<IActionResult> AddData(TBEmailAlartSetting model)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iEmailAlartSetting.AddDataAsync(model);
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
        public async Task<IActionResult> UpdateData(TBEmailAlartSetting model)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iEmailAlartSetting.UpdateDataAsync(model);
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
                var item = await iEmailAlartSetting.GetByIdAsync(id);
                if (item == null)
                    response.StatusCode = System.Net.HttpStatusCode.NoContent;

                await iEmailAlartSetting.DeleteDataAsync(id);
                response.Result = item;
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
