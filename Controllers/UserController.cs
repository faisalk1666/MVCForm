using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult AddAPI()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAPI(AddInfoViewModel request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7007/api/InfoAPI");

                var postTask = client.PostAsJsonAsync<AddInfoViewModel>("InfoAPI", request);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("AddAPI");
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(request);
            }
        }
    }
}
