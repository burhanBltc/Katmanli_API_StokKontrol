using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace _04_Stok.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/"); //areasız home/index, redirectintoAction("Index", "Home"); da olur //http://localhost:5023
            //dışardaki home ındex e atacak
        }


    }
}
