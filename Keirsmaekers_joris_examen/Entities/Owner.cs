using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keirsmaekers_joris_examen.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public virtual List<Car> Cars { get; set; }
    }
}
