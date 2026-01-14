using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationMosaicMPA101.Contexts;
using WebApplicationMosaicMPA101.Models;
using WebApplicationMosaicMPA101.ViewModels.PositionViewModels;

namespace WebApplicationMosaicMPA101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _folderPath;
        public PositionController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            
        }
        public async Task<IActionResult> Index()
        {
            var positions = await _context.Positions.Select(x=> new PositionGetVM()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return View(positions);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PositionCreateVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            Position position = new()
            {
                Name = vm.Name,
                
            };
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            if(position == null)
            {
                return NotFound();
            }
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            if(position is null)
            {
                return NotFound();
            }
            PositionUpdateVM updateVM = new()
            {
                Name = position.Name
            };
            return View(updateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(PositionUpdateVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            var position = await _context.Positions.FindAsync(vm.Id);
            if(position is null)
            {
                return BadRequest();
            }
            position.Name = vm.Name;
            _context.Positions.Update(position);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
