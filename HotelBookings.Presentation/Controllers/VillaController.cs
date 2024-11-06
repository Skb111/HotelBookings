using HotelBookings.Domain.Entities;
using HotelBookings.Persistence.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookings.Presentation.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VillaController(ApplicationDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var villas = _context.Villas.ToList();
            return View(villas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if (villa.Name == villa.Description)
            {
                ModelState.AddModelError("name", "The description cannot exactly match name");
            }
            if (ModelState.IsValid)
            {
                _context.Villas.Add(villa);
                _context.SaveChanges();
                TempData["success"] = "Villa has been created successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa couldn't be created!";
            return View();
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
