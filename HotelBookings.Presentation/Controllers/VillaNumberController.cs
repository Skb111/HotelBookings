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
                return RedirectToAction("Index");
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

        public IActionResult Update(int Id)
        {
            Villa? villa = _context.Villas.FirstOrDefault(u => u.Id == Id);
            if (villa == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Update(Villa villa)
        {
            if (ModelState.IsValid)
            {
                _context.Update(villa);
                _context.SaveChanges();
                TempData["success"] = "Villa has been updated successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int Id)
        {
            Villa? villa = _context.Villas.FirstOrDefault(u => u.Id == Id);
            if (villa == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            Villa? villaObj = _context.Villas.FirstOrDefault(u => u.Id == villa.Id);

            if (villaObj != null)
            {
                _context.Remove(villaObj);
                _context.SaveChanges();
                TempData["success"] = "Villa has been deleted successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa couldn't be deleted!";

            return View();
        }
    }
}
