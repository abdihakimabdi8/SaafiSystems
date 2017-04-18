using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using System.Collections.Generic;
using SaafiSystems.ViewModels;
using SaafiSystems.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace SaafiSystems.Controllers
{
    public class DriverController : Controller
    {
        private readonly SaafiDbContext context;

        

        public DriverController(SaafiDbContext dbContext)
        {
            this.context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Driver> drivers = context.Drivers.Include(c => c.DriverName).ToList();

            return View(drivers);
        }

        public IActionResult Add()
        {
            IList<DriverName> driverNames = context.DriverNames.ToList();
            AddDriverLoadViewModel addDriverLoadViewModel =
                new AddDriverLoadViewModel(context.DriverNames.ToList());
            return View(addDriverLoadViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddDriverLoadViewModel addDriverLoadViewModel)
        {
            if (ModelState.IsValid)
            {
                DriverName newDriver =
                    context.DriverNames.Single(c => c.ID == addDriverLoadViewModel.DriverNameID);
                // Add the new cheese to my existing cheeses
                Driver newTrip = new Driver
                {

                    Date = addDriverLoadViewModel.Date,
                    Reference = addDriverLoadViewModel.Reference,
                    Description = addDriverLoadViewModel.Description,
                    LoadMiles = addDriverLoadViewModel.LoadMiles,
                    DeadMiles = addDriverLoadViewModel.DeadMiles,

                    Rate = addDriverLoadViewModel.Rate,
                    DriverName = newDriver
                };

                context.Drivers.Add(newTrip);
                context.SaveChanges();

                return Redirect("/Driver");
            }

            return View(addDriverLoadViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Driver Entries";
            ViewBag.drivers = context.Drivers.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] driverIds)
        {
            foreach (int driverId in driverIds)
            {
                Driver theDriverEntry = context.Drivers.Single(c => c.ID == driverId);
                context.Drivers.Remove(theDriverEntry);
            }

            context.SaveChanges();

            return Redirect("/Driver");
        }
        [HttpPost]
        public IActionResult Names(int id)
        {
            if (id == 0)
            {
                Redirect("/Name");
            }

            DriverName theDriver = context.DriverNames
                .Include(cat => cat.Drivers)
                .Single(cat => cat.ID == id);

            /* 
             * IList<Cheese> theCheese = Context.Cheeses
             * .Include(c => c.Category)
             * .Single(c => c.CategoryID ==id)
             .ToList();*/

            ViewBag.Title = "Entries for Driver" + theDriver.Name;
            return View("Index", theDriver.Drivers);
        }


        //public IActionResult Edit(int driverId)
        //{
        //     ViewBag.title = "Edit Driver Entries";
        //    IList<DriverName> driverNames = context.DriverNames.ToList();
        //    var driverEntry = context.Drivers.Where(c => c.ID == driverId).FirstOrDefault();
        //    AddDriverLoadViewModel addDriverLoadViewModel =
        //        new AddDriverLoadViewModel(context.Drivers.Where(c => c.ID == driverId).FirstOrDefault());
        //    return View(addDriverLoadViewModel);
            
           
        //}

        //[HttpPost]
        //public IActionResult Edit(AddDriverLoadViewModel addDriverLoadViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        DriverName newDriver =
        //            context.DriverNames.Single(c => c.ID == addDriverLoadViewModel.DriverNameID);
        //        // Add the new cheese to my existing cheeses
        //        Driver newTrip = new Driver
        //        {

        //            Date = addDriverLoadViewModel.Date,
        //            Reference = addDriverLoadViewModel.Reference,
        //            Description = addDriverLoadViewModel.Description,
        //            LoadMiles = addDriverLoadViewModel.LoadMiles,
        //            DeadMiles = addDriverLoadViewModel.DeadMiles,

        //            Rate = addDriverLoadViewModel.Rate,
        //            DriverName = newDriver
        //        };

        //        context.Drivers.Add(newTrip);
        //        context.SaveChanges();

        //        return Redirect("/Driver");
        //    }

        //    return View(addDriverLoadViewModel);
        //}
    }
}