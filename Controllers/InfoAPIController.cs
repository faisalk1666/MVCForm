using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoAPIController : ControllerBase
    {
        private readonly DemoMVCDbContext demoMVCDbContext;
        public InfoAPIController(DemoMVCDbContext demoMVCDbContext)
        {
            this.demoMVCDbContext = demoMVCDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetInfo()
        {
           var user = await demoMVCDbContext.InfoModel.ToListAsync();
           return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> PostInfo(AddInfoViewModel request)
        {
            var user = new InfoModel()
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                StoredFrom = request.StoredFrom
            };
            await demoMVCDbContext.InfoModel.AddAsync(user);
            await demoMVCDbContext.SaveChangesAsync();
            return Ok(user);

        }
    }
}
