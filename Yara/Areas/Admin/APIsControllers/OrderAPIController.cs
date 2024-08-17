﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Authorize(Roles = "Admin,ApiRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        IIOrder iOrder;
        MasterDbcontext dbcontext;
        public OrderAPIController(IIOrder iOrder1, MasterDbcontext dbcontext1)
        {
            iOrder = iOrder1;
            dbcontext = dbcontext1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allData = await iOrder.GetAllAsync();
            return Ok(allData);
        }

        [HttpGet("GetAllV/{id}")]
        public async Task<IActionResult> GetAllV(int id)
        {
            var allData = await iOrder.GetAllvAsync(id);
            return Ok(allData);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Data = await iOrder.GetByIdAsync(id);
            return Ok(Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddData(TBOrder model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iOrder.AddDataAsync(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(TBOrder model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await iOrder.UpdateDataAsync(model);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int id)
        {
            var item = await GetById(id);
            if (item == null)
                return NoContent();

            await iOrder.DeleteDataAsync(id);
            return Ok(item);
        }
    }
}
