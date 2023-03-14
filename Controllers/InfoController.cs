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

            TempData["SuccessMsg"] = "User Added Successfully";

            return RedirectToAction("Watch");
        }

        [HttpGet]
        public async Task<IActionResult> Watch()
        {
            var user = await demoMVCDbContext.InfoModel.ToListAsync();
            return View(user);
        }
        

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
  
            var user = await demoMVCDbContext.InfoModel.FindAsync(id);

            if (user == null)
                return NotFound();

            var editRequest = new UpdateModel()
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                StoredFrom = user.StoredFrom
            };

            return View(editRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateModel editRequest)
        {
            var user = await demoMVCDbContext.InfoModel.FindAsync(editRequest.Id);

            if (user == null)
                return NotFound();

            user.Name = editRequest.Name;
            user.Email = editRequest.Email;
            user.Phone = editRequest.Phone;
            user.StoredFrom = editRequest.StoredFrom;

            await demoMVCDbContext.SaveChangesAsync();

            return RedirectToAction("Watch");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await demoMVCDbContext.InfoModel.FindAsync(id);
            demoMVCDbContext.InfoModel.Remove(user);
            await demoMVCDbContext.SaveChangesAsync();
            TempData["DeleteMsg"] = "User Deleted Successfully";
            return RedirectToAction("Watch");
        }



    }


}
