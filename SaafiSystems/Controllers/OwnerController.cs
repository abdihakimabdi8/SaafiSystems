using System;
using System.Collections.Generic;
using System.Linq;
using SaafiSystems.Data;
using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using SaafiSystems.ViewModels;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaafiSystems.Controllers
{
    public class OwnerController : Controller
    {
        private readonly SaafiDbContext context;

        public OwnerController(SaafiDbContext dbContext)
        {
            context = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Owner> owners = context.Owners.ToList();
            return View(owners);
        }
        public IActionResult Add()
        {
            AddOwnerViewModel addOwnerViewModel = new AddOwnerViewModel();
            return View(addOwnerViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddOwnerViewModel addOwnerViewModel)
        {

            if (ModelState.IsValid)
            {
                Owner newOwner = new Owner
                   
                {
                    Name = addOwnerViewModel.Name
                };
                context.Owners.Add(newOwner);
                context.SaveChanges();
                return Redirect("/Owner");
            }
            return View(addOwnerViewModel);
        }

        public IActionResult ViewOwner()
        {
            IList<Owner> owners = context.Owners.ToList();
            ViewOwnerViewModel viewOwnerViewModel = new ViewOwnerViewModel(context.Owners.ToList());

            return View(viewOwnerViewModel);

        }


        [HttpPost]
        public IActionResult ViewOwner(ViewOwnerViewModel viewOwnerViewModel)
        {
            if(ModelState.IsValid)
            {
            
            return Redirect(string.Format("/Owner/OwnerItems/{0}", viewOwnerViewModel.OwnerID));
        }

            return View(viewOwnerViewModel);
        }

        public IActionResult OwnerItems(int id)
        {
           
            List<OwnerLoad> items = context.
                OwnerLoads.
                Include(item => item.Load).
                Where(ol => ol.LoadID == id).
                ToList();
            Owner owner = context.Owners.Single(c => c.ID == id);
            OwnerItemViewModel ownerItem = new OwnerItemViewModel
            {
                Owner = owner,
                Items = items
            };

            return View(ownerItem);
        }

        public IActionResult AddItem(int id)
        {
            List<Load> loads = context.Loads.ToList();
            Owner owner = context.Owners.Single(o => o.ID == id);

            return View(new AddOwnerItemViewModel(owner, loads));

        }
        [HttpPost]
        public IActionResult AddItem(AddOwnerItemViewModel addOwnerItemViewModel)
        {
            if (ModelState.IsValid)
            {
                
                var loadID = addOwnerItemViewModel.LoadID;
                var ownerID = addOwnerItemViewModel.OwnerID;
                IList<OwnerLoad> existingItems = context.OwnerLoads
                    .Where(ol => ol.LoadID == loadID)
                    .Where(ol => ol.OwnerID == ownerID).ToList();
                if (existingItems.Count == 0)
                {
                    
                    OwnerLoad ownerItem = new OwnerLoad
                    {
                        Load = context.Loads.Single(o => o.ID == loadID),
                        Owner = context.Owners.Single(ol => ol.ID == ownerID)
                    };
                    context.OwnerLoads.Add(ownerItem);
                    context.SaveChanges();
                }
                return Redirect(string.Format("/Owner/OwnerItems/{0}", addOwnerItemViewModel.Owner));
            }
            return View(addOwnerItemViewModel);
        }
    }
}


