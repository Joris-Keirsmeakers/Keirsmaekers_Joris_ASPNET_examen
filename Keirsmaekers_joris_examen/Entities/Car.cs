using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keirsmaekers_joris_examen.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public DateTime Buydate { get; set; }
        public string Plate { get; set; }
        public virtual Owner Owner {get; set;}
        public virtual Cartype Cartype {get; set;}
    }
}
