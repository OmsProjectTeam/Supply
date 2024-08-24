using Domin.Entity;
using Infarstuructre.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class TypeProductsAPIController : ControllerBase
    {
        private readonly IITypesProduct iTypeProduct;
        private readonly MasterDbcontext dbcontext;
        ApiResponse response;
        public TypeProductsAPIController(IITypesProduct iTypeProduct1, MasterDbcontext dbcontext1)
        {
            iTypeProduct = iTypeProduct1;
            dbcontext = dbcontext1;
            response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allData = await iTypeProduct.GetAllAsync();
                if (allData == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = allData;
                return Ok(response);
            }catch (Exception ex)
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
                var allData = await iTypeProduct.GetByIdAsync(id);
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
        public async Task<IActionResult> AddData([FromBody] TBTypesProduct model)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iTypeProduct.AddDataAsync(model);
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
        public async Task<IActionResult> UpdateData([FromBody] TBTypesProduct model)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iTypeProduct.UpdateDataAsync(model);
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
                var item = await iTypeProduct.GetByIdAsync(id);
                if (item == null)
                    response.StatusCode = System.Net.HttpStatusCode.NoContent;

                await iTypeProduct.DeleteDataAsync(id);
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
