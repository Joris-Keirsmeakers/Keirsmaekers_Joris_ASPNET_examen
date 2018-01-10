using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keirsmaekers_joris_examen.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Keirsmaekers_joris_examen.Data
{
    public class DatabaseInitializer
    {
        public static void InitializeDatabase(EntityContext entityContext)
        {
            if (((entityContext.GetService<IDatabaseCreator>() as RelationalDatabaseCreator)?.Exists()).GetValueOrDefault(false))
            {
                return;
            }

            var cartypes = new List<Cartype>
            {
                new Cartype() { Brand = "Ford", Model = "Focus" },
                new Cartype() { Brand = "Porsche", Model = "911" },
                new Cartype() { Brand = "Toyota", Model = "Prius" },
                new Cartype() { Brand = "Audi", Model = "A3" }
            };

            var owners = new List<Owner>
            {

                new Owner() { FirstName = "Adam", LastName = "Adamsen" },
                new Owner() { FirstName = "Bert", LastName = "Bertsen" },
                new Owner() { FirstName = "Chris", LastName = "Chrissen" },
                new Owner() { FirstName = "Dieter", LastName = "Dietersen" },
                new Owner() { FirstName = "Erik", LastName = "Eriksen" },
                new Owner() { FirstName = "Frank", LastName = "Franksen" }
            };

            var cars = new List<Car>();
            for (var i = 0; i < 12; i++)
            {
                Owner owner = null;
                if(i<6)
                {
                    owner = owners[i];
                }
                else
                {
                    owner = owners[i-6];
                }
                Cartype cartype = cartypes[0];
                if ( i%4 ==0 )
                {                 
                    cartype = cartypes[0];
                }
                if ( i % 4 == 1)
                {                  
                    cartype = cartypes[1];
                }
                if (i % 4 == 2)
                {               
                    cartype = cartypes[2];
                }
                if (i % 4 == 3)
                {
                     cartype = cartypes[3];
                }

                cars.Add(new Car { Color = "Zwart", Buydate = DateTime.Now, Plate = "AAA-111-1", Owner= owner, Cartype=cartype });
            }


            entityContext.Database.EnsureCreated();
            entityContext.Car.AddRange(cars);
            entityContext.Owner.AddRange(owners);
            entityContext.Cartype.AddRange(cartypes);
            entityContext.SaveChanges();
        }
    }
    
}
