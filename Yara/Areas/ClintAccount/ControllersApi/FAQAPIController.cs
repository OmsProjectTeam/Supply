using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.ClintAccount.ControllersApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQAPIController : ControllerBase
    {
        IIFAQ iFAQ;
        IIFAQDescreption iIFAQDescreption;
        IIFAQList iIFAQList;
        public FAQAPIController(IIFAQ iFAQ1, IIFAQDescreption iIFAQDescreption1, IIFAQList iIFAQList1)
        {
            iFAQ = iFAQ1;
            iIFAQDescreption = iIFAQDescreption1;
            iIFAQList = iIFAQList1;
        }

        [HttpGet]
        public async Task<IActionResult> GelAllFaq()
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
             viewmMODeElMASTER.ListFAQ = await iFAQ.GetAllAsync();
             viewmMODeElMASTER.ListFAQDescription = await iIFAQDescreption.GetAllAsync();
             viewmMODeElMASTER.ListFAQList = await iIFAQList.GetAllAsync();

            return Ok(viewmMODeElMASTER);
        }
    }
}
