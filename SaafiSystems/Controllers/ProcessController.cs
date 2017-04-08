using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace SaafiSystems.Controllers
{
    public class ProcessController : Controller
    {
        public IActionResult Index()
        {
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("list", "List");
            actionChoices.Add("search", "Search");
            ViewBag.title = "Process";
            ViewBag.actions = actionChoices;

            return View();
        }
    }
}
