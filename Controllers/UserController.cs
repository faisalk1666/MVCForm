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
                    return RedirectToAction("GetUser");
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(request);
            }
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            IEnumerable<InfoModel> user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7007/api/InfoAPI");
                var response = client.GetAsync("InfoAPI");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<InfoModel>>();
                    readTask.Wait();

                    user = readTask.Result;
                }
                else
                {
                    user = Enumerable.Empty<InfoModel>();
                    ModelState.AddModelError(string.Empty, "Server Error !");
                }
                
            }
            return View(user);
        }
    }
}
