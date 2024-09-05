using Microsoft.AspNetCore.Mvc;

namespace Yara.Areas.Admin.Controllers
{
    public class SendLogsController : Controller
    {
        public IActionResult MySendLogs()
        {
            return View();
        }
    }
}
