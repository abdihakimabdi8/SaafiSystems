//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using SaafiSystems.Models;
//using SaafiSystems.Data;
//using SaafiSystems.ViewModels;
//using Microsoft.EntityFrameworkCore;


//// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace SaafiSystems.Controllers
//{
//    public class ListController : Controller
//    {
//        private readonly SaafiDbContext context;

//        public ListController(SaafiDbContext dbContext)
//        {
//            this.context = dbContext;
//        }

//        // Lists options for browsing, by "column"
//        public IActionResult Index()
//        {
           
//            return View();
//        }

//        // Lists the values of a given column, or all loads if selected
//        public IActionResult Values(string column)
//        {
//            if (column.Equals("All"))
//            {
//                IList<Load> loads = context.Loads.ToList();
//                ViewBag.Title = "All Loads";
//                return View("Loads", loads);
//            }
//            else
//            {
//                IList<Load> loads = context.Loads.Include.(c => c.Loads).ToList();
//                ViewBag.Title = "All Loads";
//                return View("Loads", loads);
//            }
//        }

//        // Lists Jobs with a given field matching a given value
//        public IActionResult Loads(string column, string value)
//        {
//            IList<Load> loads = context.Loads.ToList();

//            //loadsViewModel.Loads = loadData.FindByColumnAndValue(column, value);
//            //loadsViewModel.Title = "Loads with " + column + ": " + value;

//            //return View(loadsViewModel);
//        }
//    }
//}