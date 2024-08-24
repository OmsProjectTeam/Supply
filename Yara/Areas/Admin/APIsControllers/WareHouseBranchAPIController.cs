using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseBranchAPIController : ControllerBase
    {
        private readonly IIWareHouseBranch iWareHouseBranch;
        private readonly MasterDbcontext dbcontext;
        ApiResponse response;
        public WareHouseBranchAPIController(IIWareHouseBranch iWareHouseBranch1, MasterDbcontext dbcontext1)
        {
            iWareHouseBranch = iWareHouseBranch1;
            dbcontext = dbcontext1;
            response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allData = await iWareHouseBranch.GetAllAPI();
                if (allData == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = allData;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }
            return Ok(response);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            try
            {
                var allData = await iWareHouseBranch.GetAllvAPI(id);
                if (allData == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = allData;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }
            return Ok(response);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var allData = await iWareHouseBranch.GetByIdAPI(id);
                if (allData == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = allData;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }
            return Ok(response);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var allData = await iWareHouseBranch.GetByNameAPI(name);
                if (allData == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = allData;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddData(TBWareHouseBranch model)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iWareHouseBranch.SaveDataAPI(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBWareHouseBranch model)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iWareHouseBranch.UpdateDataAPI(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            try
            {
                var item = await iWareHouseBranch.GetByIdAPI(id);
                if (item == null)
                    response.StatusCode = System.Net.HttpStatusCode.NoContent;

                await iWareHouseBranch.DeleteDataAPI(id);
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
