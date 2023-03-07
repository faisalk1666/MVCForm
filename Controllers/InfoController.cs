using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{

    public class InfoController : Controller
    {
        private readonly DemoMVCDbContext demoMVCDbContext;

        public InfoController(DemoMVCDbContext demoMVCDbContext)
        {
            this.demoMVCDbContext = demoMVCDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddInfoViewModel addInfoRequest)
        {
            var user = new InfoModel()
            {
                Name = addInfoRequest.Name,
                Email = addInfoRequest.Email,
                Phone = addInfoRequest.Phone,
                StoredFrom = addInfoRequest.StoredFrom 
            };

            await demoMVCDbContext.AddAsync(user);
            await demoMVCDbContext.SaveChangesAsync();

            return RedirectToAction("Watch");
        }

        [HttpGet]
        public async Task<IActionResult> Watch() 
        {
            var user = await demoMVCDbContext.InfoModel.ToListAsync();
            return View(user);
        }

    }
}
