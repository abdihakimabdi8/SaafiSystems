//using Microsoft.AspNetCore.Mvc;
//using SaafiLogistics.Models;
//using SaafiLogistics.Data;
//using SaafiLogistics.ViewModels;

//namespace SaafiSystems.Controllers
//{
//    public class SearchController : Controller
//    {

//        // Our reference to the data store
//        private static LoadData loadData;

//        static SearchController()
//        {
//            loadData = LoadData.GetInstance();
//        }

//        // Display the search form
//        public IActionResult Index()
//        {
//            SearchLoadsViewModel loadsViewModel = new SearchLoadsViewModel();
//            loadsViewModel.Title = "Search";
//            return View(loadsViewModel);
//        }

//        // Process search submission and display search results
//        public IActionResult Results(SearchLoadsViewModel loadsViewModel)
//        {

//            if (loadsViewModel.Column.Equals(LoadFieldType.All) || loadsViewModel.Value.Equals(""))
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
