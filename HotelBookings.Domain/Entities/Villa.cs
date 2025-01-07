using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookings.Domain.Entities
{
    public class Villa
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; } 
        public string? Description { get; set; }
        [Display(Name ="PRICE Per Night")]
        [Range(1,10000)]
        public double Price { get; set; }
        public int Sqft { get; set; }
        [Range(1, 10000)]
        public int Occupancy { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
