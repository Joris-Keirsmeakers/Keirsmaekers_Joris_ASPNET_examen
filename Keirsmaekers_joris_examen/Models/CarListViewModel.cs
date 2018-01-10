using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keirsmaekers_joris_examen.Models
{
    public class CarListViewModel {
        public List<CarDetailViewModel> Cars { get; set; }
        public List<OwnerViewModel> Owners { get; set; }
        public List<CartypeViewModel> Cartypes { get; set; }
    }
}
