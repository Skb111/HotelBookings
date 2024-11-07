using HotelBookings.Domain.Entities;
using HotelBookings.Persistence.Data;
using HotelBookings.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelBookings.Presentation.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VillaNumberController(ApplicationDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var villaNumbers = _context.VillaNumbers.Include(x => x.Villa).ToList();
            return View(villaNumbers);
        }
        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _context.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })                      
            };
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            //ModelState.Remove("Villa");
            bool roomNumberExists = _context.VillaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
            if (ModelState.IsValid && !roomNumberExists)
            {
                _context.VillaNumbers.Add(obj.VillaNumber);
                _context.SaveChanges();
                TempData["success"] = "Villa Number has been created successfully!";
                return RedirectToAction(nameof(Index));
            }
            //TEST
            if(roomNumberExists)
            {
                TempData["error"] = "Villa Number has been created already!";

            }
            obj.VillaList = _context.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);

        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _context.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {
            if (ModelState.IsValid)
            {
                _context.VillaNumbers.Update(villaNumberVM.VillaNumber);
                _context.SaveChanges();
                TempData["success"] = "Villa Number has been updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            villaNumberVM.VillaList = _context.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(villaNumberVM);
        }

        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _context.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            VillaNumber? villaObj = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);

            if (villaObj != null)
            {
                _context.VillaNumbers.Remove(villaObj);
                _context.SaveChanges();
                TempData["success"] = "VillaNumber has been deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "VillaNumber couldn't be deleted!";

            return View();
        }
    }
}
