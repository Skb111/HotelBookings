using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookings.Domain.Entities
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }
        //[Display(Name = "Villa Number")]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("VillaId")]
        public int VillaId { get; set; }
        [ValidateNever]
        public Villa Villa { get; set; }
        //public string? SpecialDetails { get; set; }
    }
}
