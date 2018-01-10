using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keirsmaekers_joris_examen.Entities
{
    public class Cartype
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FullType => $"{Brand} {Model}";
        public virtual List<Car> Cars { get; set; }
    }
}
