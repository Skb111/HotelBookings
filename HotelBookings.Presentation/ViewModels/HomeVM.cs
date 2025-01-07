using HotelBookings.Domain.Entities;

namespace HotelBookings.Presentation.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Villa>? VillaList { get; set; }
        public DateOnly? CheckedInDate { get; set; }
        public DateOnly? CheckedOutDate { get; set; }
        public int Nights { get; set; }

    }
}
