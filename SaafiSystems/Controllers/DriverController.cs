using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using System.Collections.Generic;
using SaafiSystems.ViewModels;
using SaafiSystems.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index(int? page)
        {
            
            var drivers = from d in context.Drivers.Include(c => c.DriverName)
                        select d;
            int pageSize = 3;
            return View(await PaginatedList<Driver>.CreateAsync(drivers.AsNoTracking(), page ?? 1, pageSize));
            
        }

        public IActionResult Add()
        {
            IList<DriverName> driverNames = context.DriverNames.ToList();
            AddDriverLoadViewModel addDriverLoadViewModel =
                new AddDriverLoadViewModel(context.DriverNames.ToList(), null);
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

      
        public IActionResult Remove(int ID)
        {
            var dri = context.Drivers.Single(e => e.ID == ID);
            if (dri != null)
                context.Drivers.Remove(dri);

            context.SaveChanges();

            return Redirect("/Driver");


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


            ViewBag.Title = "Entries for Driver" + theDriver.Name;
            return View("Index", theDriver.Drivers);
        }

        public IActionResult ViewDriver()
        {
            IList<DriverName> driverName = context.DriverNames.ToList();
            ViewDriverViewModel viewDriverLoadsViewModel = new ViewDriverViewModel(context.DriverNames.ToList());

            return View(viewDriverLoadsViewModel);

        }


        [HttpPost]
        public IActionResult ViewDriver(ViewDriverViewModel viewDriverLoadsViewModel)
        {
            if (ModelState.IsValid)
            {

                return Redirect(string.Format("/Driver/DriverTrips/{0}", viewDriverLoadsViewModel.DriverNameID));
            }

            return View(viewDriverLoadsViewModel);
        }

        public IActionResult DriverTrips(int id)
        {

            List<Driver> drivers = context.
                Drivers
                .Include(c => c.DriverName)
                .Where(c => c.DriverNameID == id).
                ToList();
            DriverName driverName = context.DriverNames.Single(c => c.ID == id);
            DriverLoadsViewModel driverLoads = new DriverLoadsViewModel
            {
                Drivers = drivers,
             
            };
            ViewBag.title = driverName.Name;
            return View(driverLoads);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var driver = context.Drivers.SingleOrDefault((l) => l.ID == id);
            if (driver == null)
            {
                return NotFound();
            }
            List<DriverName> newDriverName =
                    new List<DriverName>() { context.DriverNames.Single(c => c.ID == driver.DriverNameID) };
            return View(new AddDriverLoadViewModel(newDriverName, driver));
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(AddDriverLoadViewModel addDriverLoadViewModel)
        {
            var driver = context.Drivers.SingleOrDefault((l) => l.ID == addDriverLoadViewModel.ID);
            if (driver == null)
            {
                return NotFound();
            }
            var newDriverNames = context.DriverNames;

            var viewModel = new AddDriverLoadViewModel(newDriverNames, driver);

            var existingDriverName = context.DriverNames.SingleOrDefault(c => c.ID == driver.DriverNameID);

            if (existingDriverName != null)
            {
                viewModel.DriverName = existingDriverName;
                viewModel.DriverNameID = existingDriverName.ID;
            }

            return View(viewModel);
        }
    }
}