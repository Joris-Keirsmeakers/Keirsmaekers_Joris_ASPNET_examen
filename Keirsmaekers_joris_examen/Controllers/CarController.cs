using System;
using System.Collections.Generic;
using System.Linq;
using Keirsmaekers_joris_examen.Data;
using Keirsmaekers_joris_examen.Entities;
using System.Threading.Tasks;
using Keirsmaekers_joris_examen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Keirsmaekers_joris_examen.Controllers
{
    public class CarController : Controller
    {
        public readonly EntityContext _entityContext;

        public CarController(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        [HttpGet("/car")]
        public IActionResult Index()
        {
            var model = new CarListViewModel();
            model.Cars = new List<CarDetailViewModel>();
            var allcars = _entityContext.Car.Include(x=>x.Owner).Include(x=>x.Cartype).OrderBy(x => x.Id).ToList();
            
            foreach (var car in allcars)
            {
                
                var vm = new CarDetailViewModel
                {
                    Id = car.Id,
                    Color = car.Color,
                    Buydate = car.Buydate,
                    Plate = car.Plate,
                    Cartype = car.Cartype.FullType   
                };
                if (car.Owner.FullName == null)
                {
                    vm.Owner = "Geen eigenaar";
                }
                else
                {
                    vm.Owner = car.Owner.FullName;
                }
                model.Cars.Add(vm);
            }
            return View(model);
        }
        
        [HttpGet("/car/owners")]
        public IActionResult Owners()
        {
            var model = new CarListViewModel();
            model.Owners = new List<OwnerViewModel>();
            var allOwners = _entityContext.Owner.ToList();
            model.Cars = new List<CarDetailViewModel>();
            var allcars = _entityContext.Car.Include(x => x.Owner).Include(x => x.Cartype).OrderBy(x => x.Id).ToList();

            foreach (var owner in allOwners)
            {
                var vm = new OwnerViewModel
                {
    
                    FullName = owner.FullName
                };
                model.Owners.Add(vm);
            }

            foreach (var car in allcars)
            {

                var vm = new CarDetailViewModel
                {
                    Id = car.Id,
                    Color = car.Color,
                    Buydate = car.Buydate,
                    Plate = car.Plate,
                    Owner = car.Owner.FullName,
                    Cartype = car.Cartype.FullType

                };
                model.Cars.Add(vm);
            }
            return View(model);
        }

        [HttpGet("/car/models")]
        public IActionResult Models()
        {
            var model = new CarListViewModel();
            model.Cartypes = new List<CartypeViewModel>();
            var alltypes = _entityContext.Cartype.ToList();
            model.Cars = new List<CarDetailViewModel>();
            var allcars = _entityContext.Car.Include(x => x.Owner).Include(x => x.Cartype).OrderBy(x => x.Id).ToList();

            foreach (var type in alltypes)
            {
                var vm = new CartypeViewModel
                {
                  FullType = type.FullType
                };
                model.Cartypes.Add(vm);
            }

            foreach (var car in allcars)
            {

                var vm = new CarDetailViewModel
                {
                    Id = car.Id,
                    Color = car.Color,
                    Buydate = car.Buydate,
                    Plate = car.Plate,
                    Owner = car.Owner.FullName,
                    Cartype = car.Cartype.Brand + " " + car.Cartype.Model

                };
                model.Cars.Add(vm);
            }

            return View(model);
        }

        
        [HttpGet("/car/create")]
        public IActionResult Create()
        {
            var vm = new CarEditViewModel();
            vm.Owners = _entityContext.Owner.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString(),
            }
           ).ToList();

            vm.Cartypes = _entityContext.Cartype.Select(x => new SelectListItem
            {
                Text = x.FullType,
                Value = x.Id.ToString(),
            }
            ).ToList();
            return View(vm);
          
        }

        [HttpPost("/car/create")]
        public IActionResult Save([FromForm] CarEditViewModel vm)
        {
            if (vm.Color != null && vm.Buydate != null && vm.Plate != null)
            {
                var car = new Car();
                car.Color = vm.Color;
                car.Buydate = vm.Buydate;
                car.Plate = vm.Plate;
                car.Owner = vm.OwnerId.HasValue ? _entityContext.Owner.FirstOrDefault(x => x.Id == vm.OwnerId) : null;
                car.Cartype = vm.CartypeId.HasValue ? _entityContext.Cartype.FirstOrDefault(x => x.Id == vm.CartypeId) : null;
                _entityContext.Car.Add(car);
                _entityContext.SaveChanges();

                return Redirect("/car");
            }

            return View("create");
        }

        [HttpGet("/car/{id}/edit")]
        public IActionResult Edit([FromRoute] int id)
        {
            var car = _entityContext.Car.FirstOrDefault(x => x.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            var vm = ConvertCar(car);
            vm.Owners = _entityContext.Owner.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString(),
            }
            ).ToList();

            vm.Owners.Insert(0,new SelectListItem
            {
                Text = "Geen eigenaar",
                Value = null,
            });

            vm.Cartypes = _entityContext.Cartype.Select(x => new SelectListItem
            {
                Text = x.FullType,
                Value = x.Id.ToString(),
            }
            ).ToList();
            return View(vm);
        }


        [HttpPost("/car/{id}/edit")]
        public IActionResult EditSave([FromForm] CarEditViewModel vm)
        {
            if (vm.Color != null && vm.Buydate != null && vm.Plate != null)
            {
                var car = _entityContext.Car.FirstOrDefault(x => x.Id == vm.Id);
                car.Color = vm.Color;
                car.Buydate = vm.Buydate;
                car.Plate = vm.Plate;

                car.Owner = vm.OwnerId.HasValue ? _entityContext.Owner.FirstOrDefault(x => x.Id == vm.OwnerId) : null;
                if(vm.OwnerId == null)
                {
                    car.Owner=null;
                }
                car.Cartype = vm.CartypeId.HasValue ? _entityContext.Cartype.FirstOrDefault(x => x.Id == vm.CartypeId) : null;
                
                _entityContext.Car.Update(car);
                _entityContext.SaveChanges();

                return Redirect("/car");
            }

            return View("Edit",vm);
        }
        
        private static CarEditViewModel ConvertCar(Car car)
        {
            var vm = new CarEditViewModel
            {
                Id = car.Id,
                Color=car.Color,
                Buydate=car.Buydate,
                Plate=car.Plate,
                Owner = car.Owner?.FullName,
                OwnerId = car.Owner?.Id,
                Cartype = car.Cartype?.FullType,
                CartypeId = car.Cartype?.Id,
            };
            
            return vm;
        }

        [HttpGet("/car/delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var car = _entityContext.Car.FirstOrDefault(x => x.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            var vm = ConvertCar(car);
            return View(vm);
        }

        [HttpPost("/car/delete/{id}")]
        public IActionResult DeleteConfirm([FromRoute] int id)
        {
            var toDelete = _entityContext.Car.FirstOrDefault(x => x.Id == id);
            if (toDelete != null)
            {
                _entityContext.Car.Remove(toDelete);
                _entityContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }


    
    }
}