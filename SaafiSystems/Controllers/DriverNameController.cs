using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using System.Collections.Generic;
using SaafiSystems.ViewModels;
using SaafiSystems.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaafiSystems.Controllers
{
    public class DriverNameController : Controller
    {
        private readonly SaafiDbContext context;

        public DriverNameController(SaafiDbContext dbContext)
        {
            this.context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<DriverName> driverName = context.DriverNames.ToList();

            return View(driverName);
        }

        public IActionResult Add()
        {
            AddDriverNameViewModel addDriverNameViewModel =
                new AddDriverNameViewModel();
            return View(addDriverNameViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddDriverNameViewModel addDriverNameViewModel)
        {
            if (ModelState.IsValid)
            {
                DriverName newDriverName = new DriverName
                {
                    Name = addDriverNameViewModel.Name,
                };

                context.DriverNames.Add(newDriverName);
                context.SaveChanges();

                return Redirect("/DriverName");
            }

            return View(addDriverNameViewModel);
        }
        public IActionResult Remove()
        {
            ViewBag.title = "Remove Drivers";
            ViewBag.drivers = context.DriverNames.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] driverIds)
        {
            foreach (int driverId in driverIds)
            {
                DriverName theDriverEntry = context.DriverNames.Single(c => c.ID == driverId);
                context.DriverNames.Remove(theDriverEntry);
            }

            context.SaveChanges();

            return Redirect("/DriverName");
        }
    }
}