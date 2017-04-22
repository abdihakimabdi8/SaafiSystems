//using Microsoft.AspNetCore.Mvc;
//using SaafiSystems.Models;
//using SaafiSystems.Data;
//using SaafiSystems.ViewModels;
//using System.Linq;

//namespace SaafiSystems.Controllers
//{
//    public class SearchController : Controller
//    {

//        // Our reference to the data store
//        private static SaafiDbContext context;

//        public SearchController(SaafiDbContext dbContext)
//        {
//          context = dbContext;
//        }

//        // Display the search form
//        public IActionResult Index()
//        {
//            SearchLoadsViewModel loadsViewModel = new SearchLoadsViewModel(context.LoadFieldTypes.ToList());
//            loadsViewModel.Title = "Search";
//            return View(loadsViewModel);
//        }

//        // Process search submission and display search results
//        public IActionResult Results(SearchLoadsViewModel loadsViewModel)
//        {
//            if (LoadFieldType.Name == "Date")
//                return View(context.Loads.Where(c => c.Date.ToString() == searchTerm || searchTerm == null).ToList());
//            if (searchBy == "Reference")
//                return View(context.Loads.Where(c => c.Reference.ToString() == searchTerm || searchTerm == null).ToList());

//            if (searchBy == "Description")
//                return View(context.Loads.Where(c => c.Description.ToString() == searchTerm || searchTerm == null).ToList());

//            if (searchBy == "Owner")
//                return View(context.Loads.Where(c => c.Owner.ToString() == searchTerm || searchTerm == null).ToList());

//            if (searchBy == "Amount")
//                return View(context.Loads.Where(c => c.Amount.ToString() == searchTerm || searchTerm == null).ToList());
//            else
//                return View(context.Loads.Where(c => c.LoadCategory.ToString() == searchTerm || searchTerm == null).ToList());

//            if (loadsViewModel.LoadFieldType.Equals(LoadFieldType.All) || loadsViewModel.Value.Equals(""))
//            {
//                loadsViewModel.Loads = loadData.FindByValue(loadsViewModel.Value);
//            }
//            else
//            {
//                loadsViewModel.Loads = loadData.FindByColumnAndValue(loadsViewModel.Column, loadsViewModel.Value);
//            }

//            loadsViewModel.Title = "Search";

//            return View("Index", loadsViewModel);
//        }
//    }
//}
