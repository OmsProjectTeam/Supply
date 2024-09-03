using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.APIsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyInformationAPIController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        IICompanyInformation iCompanyInformation;
        ApiResponse response;
        UserManager<ApplicationUser> userManager;
        public CompanyInformationAPIController(IICompanyInformation iCompanyInformation1, IWebHostEnvironment env, UserManager<ApplicationUser> userManager)
        {
            iCompanyInformation = iCompanyInformation1;
            response = new ApiResponse();
            _env = env;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allData = await iCompanyInformation.GetAllAsync();
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
                var allData = await iCompanyInformation.GetByIdAsync(id);
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
        public async Task<IActionResult> AddData([FromForm] TBCompanyInformation model, List<IFormFile> file)
        {
            string fileName = string.Empty;
            if (file.Count == 0)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            else
            {
                fileName = $"{Guid.NewGuid()}{Path.GetExtension(file[0].FileName)}";
                string path = Path.Combine("wwwroot/Images/Home", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file[0].CopyToAsync(stream);
                    model.Photo = fileName;
                }
            }

            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                await iCompanyInformation.AddDataAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                await iCompanyInformation.DELETPHOTOWethErrorAsync(fileName);
                response.ErrorMessage = new List<string> { ex.Message };
                response.IsSuccess = false;
            }
            return Ok(response);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateData([FromForm] TBCompanyInformation model, List<IFormFile> file)
        {
            string fileName = string.Empty;
            if (file.Count == 0)
            {
                model.Photo = model.Photo;
            }
            else
            {
                fileName = $"{Guid.NewGuid()}{Path.GetExtension(file[0].FileName)}";
                string path = Path.Combine("wwwroot/Images/Home", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file[0].CopyToAsync(stream);
                }
                model.Photo = fileName;
            }
            try
            {
                if (!ModelState.IsValid)
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
               
                await iCompanyInformation.UpdateDataAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                await iCompanyInformation.DELETPHOTOWethErrorAsync(fileName);
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
                var item = await iCompanyInformation.GetByIdAsync(id);
                if (item == null)
                    response.StatusCode = System.Net.HttpStatusCode.NoContent;

                await iCompanyInformation.DeleteDataAsync(id);
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
