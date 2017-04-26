using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using System.Collections.Generic;
using SaafiSystems.ViewModels;
using SaafiSystems.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


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
        public async Task<IActionResult> Index(int? page)
        {
            var driverName = from dn in context.DriverNames
                                    select dn;
            int pageSize = 3;
            return View(await PaginatedList<DriverName>.CreateAsync(driverName.AsNoTracking(), page ?? 1, pageSize));

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
        public IActionResult Remove(int ID)
        {
            var drivername = context.DriverNames.Single(e => e.ID == ID);
            if (drivername != null)
                context.DriverNames.Remove(drivername);

            context.SaveChanges();

            return Redirect("/DriverName");

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