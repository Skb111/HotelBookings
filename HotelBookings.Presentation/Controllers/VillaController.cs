using HotelBookings.Application.Common.Interfaces;
using HotelBookings.Domain.Entities;
using HotelBookings.Persistence.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookings.Presentation.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostEnvironment;
        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) 
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var villas = _unitOfWork.Villa.GetAll();
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
                if(villa.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(villa.Image.FileName);
                   // string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImages");
                    using(var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                    villa.Image.CopyTo(fileStream);
                    villa.ImageUrl = @"\images\VillaImages\" + fileName;
                }
                else
                {
                    villa.ImageUrl = "https://placehold.co/600x400";
                }
				_unitOfWork.Villa.Add(villa);
				_unitOfWork.Save();
                TempData["success"] = "Villa has been created successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Villa couldn't be created!";
            return View();
        }

        public IActionResult Update(int Id)
        {
            Villa? villa = _unitOfWork.Villa.Get(u => u.Id == Id);
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
                if (villa.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(villa.Image.FileName);
                    // string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImages");

                    if (!string.IsNullOrEmpty(villa.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, villa.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                        villa.Image.CopyTo(fileStream);
                    villa.ImageUrl = @"\images\VillaImages\" + fileName;
                }
                _unitOfWork.Villa.Update(villa);
				_unitOfWork.Save();
                TempData["success"] = "Villa has been updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int Id)
        {
            Villa? villa = _unitOfWork.Villa.Get(u => u.Id == Id);
            if (villa == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            Villa? villaObj = _unitOfWork.Villa.Get(u => u.Id == villa.Id);

            if (villaObj != null)
            {
                if (!string.IsNullOrEmpty(villa.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, villa.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.Villa.Remove(villaObj);
				_unitOfWork.Save();
                TempData["success"] = "Villa has been deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Villa couldn't be deleted!";

            return View();
        }
    }
}
