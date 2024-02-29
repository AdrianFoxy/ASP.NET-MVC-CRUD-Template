using CrudManagement.Web.Data;
using CrudManagement.Web.Models.Entities;
using CrudManagement.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudManagement.Web.Controllers
{
    public class WorkerController : Controller
    {
        private readonly AppDbContext _context;

        public WorkerController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddWorkerViewModel model) 
        {
            var worker = new Worker
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Active = model.Active
            };

            await _context.Worker.AddAsync(worker);
            await _context.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var workers = await _context.Worker.ToListAsync();

            return View(workers);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var worker = await _context.Worker.FindAsync(id);
            return View(worker);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Worker model)
        {
            var worker = await _context.Worker.FindAsync(model.Id);

            if(worker != null)
            {
                worker.Name = model.Name;
                worker.Email = model.Email;
                worker.Phone = model.Phone;
                worker.Active = model.Active;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("List", "Worker");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Worker model)
        {
            var worker = await _context.Worker
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if(worker is not null)
            {
                _context.Worker.Remove(model);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("List", "Worker");
        }
    }
}
