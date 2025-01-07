using HotelBookings.Application.Common.Interfaces;
using HotelBookings.Domain.Entities;
using HotelBookings.Persistence.Data;
using HotelBookings.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelBookings.Presentation.Controllers
{
    public class AmenityController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;
		public AmenityController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
        {
            var amenities = _unitOfWork.AmenityRep.GetAll(includeProperties: "Villa");
            return View(amenities);
        }
        public IActionResult Create()
        {
            AmenityVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })                      
            };
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.AmenityRep.Add(obj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "Amenity has been created successfully!";
                return RedirectToAction(nameof(Index));
            }
            obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);

        }

        public IActionResult Update(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.AmenityRep.Get(u => u.Id == amenityId)
            };
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM amenityVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.AmenityRep.Update(amenityVM.Amenity);
				_unitOfWork.Save();
				TempData["success"] = "Amenity has been updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            amenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(amenityVM);
        }

        public IActionResult Delete(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.AmenityRep.Get(u => u.Id == amenityId)
            };
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM amenityVM)
        {
            Amenity? amenityObj = _unitOfWork.AmenityRep.Get(u => u.Id == amenityVM.Amenity.Id);

            if (amenityObj != null)
            {
                _unitOfWork.AmenityRep.Remove(amenityObj);
				_unitOfWork.Save();
				TempData["error"] = "Amenity has been deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Amenity couldn't be deleted!";

            return View();
        }
    }
}
