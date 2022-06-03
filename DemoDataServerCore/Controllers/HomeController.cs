using Microsoft.AspNetCore.Mvc;

namespace DemoDataServerCore.Controllers
{
    public class HomeController : Controller
    {
		public IActionResult Index() 
        {   
            return View();
        }
    }
}