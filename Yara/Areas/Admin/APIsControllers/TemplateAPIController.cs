﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateAPIController : ControllerBase
    {
        IITemplate iTemplate;
        ApiResponse response;
        public TemplateAPIController(IITemplate iTemplate1)
        {
            iTemplate = iTemplate1;
            response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allData = await iTemplate.GetAllAsync();
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
                var allData = await iTemplate.GetAllvAsync(id);
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
                var Data = await iTemplate.GetByIdAsync(id);
                if (Data == null)
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Result = Data;
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
        public async Task<IActionResult> AddData([FromBody] TBTemplate model)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iTemplate.AddDataAsync(model);
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
        public async Task<IActionResult> UpdateData([FromBody] TBTemplate model)
        {
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iTemplate.UpdateDataAsync(model);
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
                var item = await iTemplate.GetByIdAsync(id);
                if (item == null)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iTemplate.DeleteDataAsync(id);
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
