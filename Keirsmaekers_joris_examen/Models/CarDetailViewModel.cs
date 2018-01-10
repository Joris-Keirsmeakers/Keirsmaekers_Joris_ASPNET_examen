using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keirsmaekers_joris_examen.Models
{
    public class CarDetailViewModel
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public DateTime Buydate { get; set; }
        public string Plate { get; set; }
        public string Owner { get; set; }
        public string Cartype { get; set; }

    }
}
