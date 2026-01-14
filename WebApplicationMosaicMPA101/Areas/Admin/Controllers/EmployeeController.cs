using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationMosaicMPA101.Contexts;
using WebApplicationMosaicMPA101.Helpers;
using WebApplicationMosaicMPA101.Models;
using WebApplicationMosaicMPA101.ViewModels.EmployeeViewModels;

namespace WebApplicationMosaicMPA101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _folderPath;
        public EmployeeController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _folderPath = Path.Combine(_environment.WebRootPath, "images");
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Select(x=> new EmployeeGetVM()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ImagePath = x.ImagePath,
                PositionName = x.Position.Name,
                Description = x.Description
            }).ToListAsync();
            return View(employees);
        }
        public async Task<IActionResult> Create()
        {
            await _SendPositionsWithViewBag();
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVM vm)
        {
            if(!ModelState.IsValid)
            {
                await _SendPositionsWithViewBag();
                return View(vm);
            }
            var existPosition = await _context.Positions.AnyAsync(x=>x.Id == vm.PositionId);
            if(!existPosition)
            {
                ModelState.AddModelError("PositionId", "Bele bir position movcud deyil!");
                await _SendPositionsWithViewBag();
                return View(vm);
            }
            if(!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("image", "Bura sadece image tipinde data daxil ede bilersiniz!");
                await _SendPositionsWithViewBag();
                return View(vm);
            }
            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("image", "Bura sadece max 2mb-liq image tipinde data daxil ede bilersiniz!");
                await _SendPositionsWithViewBag();
                return View(vm);
            }
            string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);
            Employee employee = new()
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                PositionId = vm.PositionId,
                Description = vm.Description,
                ImagePath = uniqueFileName
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee is null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            string deletedImagePath = Path.Combine(_folderPath, employee.ImagePath);
            ExtensionMethods.DeleteFile(deletedImagePath);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee is null)
            {
                return NotFound();
            }
            EmployeeUpdateVM updateVM = new()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Description = employee.Description,
                PositionId = employee.PositionId
            };
            await _SendPositionsWithViewBag();
            return View(updateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeUpdateVM vm)
        {
            if(!ModelState.IsValid)
            {
                await _SendPositionsWithViewBag();
                return View(vm);
            }
            var existPosition = await _context.Positions.AnyAsync(x => x.Id == vm.PositionId);
            if (!existPosition)
            {
                ModelState.AddModelError("PositionId", "Bele bir position movcud deyil!");
                await _SendPositionsWithViewBag();
                return View(vm);
            }
            if (!vm.Image?.CheckType("image") ?? false)
            {
                ModelState.AddModelError("image", "Bura sadece image tipinde data daxil ede bilersiniz!");
                await _SendPositionsWithViewBag();
                return View(vm);
            }
            if (!vm.Image?.CheckSize(2) ?? false)
            {
                ModelState.AddModelError("image", "Bura sadece max 2mb-liq image tipinde data daxil ede bilersiniz!");
                await _SendPositionsWithViewBag();
                return View(vm);
            }
            var isexistEmployee = await _context.Employees.FindAsync(vm.Id);
            if(isexistEmployee == null)
            {
                return NotFound();
            }
            isexistEmployee.FirstName = vm.FirstName;
            isexistEmployee.LastName = vm.LastName;
            isexistEmployee.Description = vm.Description;
            isexistEmployee.PositionId = vm.PositionId;
            if(vm.Image is { })
            {
                string newImagePath = await vm.Image.FileUploadAsync(_folderPath);
                string oldImagePath = Path.Combine(_folderPath, isexistEmployee.ImagePath);
                ExtensionMethods.DeleteFile(oldImagePath);
                isexistEmployee.ImagePath = newImagePath;
            }
            _context.Employees.Update(isexistEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private async Task _SendPositionsWithViewBag()
        {
            var positions = await _context.Positions.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToListAsync();
            ViewBag.Positions = positions;
        }

    }
}
