using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.ClintAccount.ControllersApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerMessagesAPIController : ControllerBase
    {
        IICustomerMessages iCustomerMessages;
        ApiResponse apiResponse;
        public CustomerMessagesAPIController(IICustomerMessages iCustomerMessages1)
        {
            iCustomerMessages = iCustomerMessages1;
            apiResponse = new ApiResponse();
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var messages = await iCustomerMessages.GetAllAsync();

                if(messages == null)
                    apiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

                apiResponse.Result = messages;
                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;


                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = new List<string> { ex.Message };
            }

            return Ok(apiResponse);
        }

        [HttpPost("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var messages = await iCustomerMessages.GetByIdAsync(id);

                if (messages == null)
                    apiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;

                apiResponse.Result = messages;
                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;


                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = new List<string> { ex.Message };
            }

            return Ok(apiResponse);
        }

        [HttpPost]
        public async Task<IActionResult> AddData(TBCustomerMessages model)
        {
            try
            {
                if (!ModelState.IsValid)
                    apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                 await iCustomerMessages.AddDataAsync(model);
                apiResponse.Result = model;

                apiResponse.StatusCode = System.Net.HttpStatusCode.OK;


                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = new List<string> { ex.Message };
            }

            return Ok(apiResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateData(int id, [FromBody] TBCustomerMessages model)
        {
            try
            {
                var oldModel = await iCustomerMessages.GetByIdAsync(id);
                if (oldModel == null)
                    apiResponse.StatusCode =System.Net.HttpStatusCode.BadRequest;
                oldModel = model;
                await iCustomerMessages.UpdateDataAsync(oldModel);

                apiResponse.Result = oldModel;
                return Ok(apiResponse);

            }
            catch (Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = new List<string> { ex.Message };
            }

            return Ok(apiResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            try
            {
                var model = await iCustomerMessages.GetByIdAsync(id);
                if (model == null)
                    apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iCustomerMessages.DeleteDataAsync(id);

                return Ok(apiResponse);
            }
            catch(Exception ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = new List<string> { ex.Message };
            }

            return Ok(apiResponse);
        }


    }
}
