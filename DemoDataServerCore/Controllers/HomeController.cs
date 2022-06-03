using Microsoft.AspNetCore.Mvc;

namespace DemoDataServerCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ReloadService reloadService;

        public HomeController(ReloadService rs)
        {
            reloadService = rs;
        }
		public IActionResult Index() 
        {   
            return View();
        }

        public IActionResult Reload()
        {
            System.Threading.Tasks.Task task = reloadService.Reload("custom-index");
            return View();
        }
    }
}