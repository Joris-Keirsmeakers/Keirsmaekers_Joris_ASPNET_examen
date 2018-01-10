using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Keirsmaekers_joris_examen.Models
{
    public class CarEditViewModel
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public DateTime Buydate { get; set; }
        public string Plate { get; set; }
        public string Owner { get; set; }
        public int? OwnerId { get; set; }
        public string Cartype { get; set; }
        public int? CartypeId { get; set; }
        public List<SelectListItem> Owners { get; set; }
        public List<SelectListItem> Cartypes { get; set; }

    }
}
