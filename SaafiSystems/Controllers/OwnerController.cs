using System;
using System.Collections.Generic;
using System.Linq;
using SaafiSystems.Data;
using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using SaafiSystems.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
      
        public async Task<IActionResult> Index(int? page)
        {
            var owners = from o in context.Owners
                                    select o;
            int pageSize = 3;
            return View(await PaginatedList<Owner>.CreateAsync(owners.AsNoTracking(), page ?? 1, pageSize));

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
        public async Task<IActionResult> ViewOwner(int id, int? page)
        {
            //TODO: #1 implement a paginatedList for ViewOwner Action
            List<OwnerLoad> items = context
                .OwnerLoads
                .Include(item => item.Load)
                .Where(cm => cm.OwnerID == id)
                .ToList();
            Owner owner = context.Owners.Single(m => m.ID == id);
         
            ViewOwnerViewModel viewModel = new ViewOwnerViewModel
            {
                Owner = owner,
                Items = items

            };
            int pageSize = 3;
            return View( (viewModel));
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
           Owner owner = context.Owners.Single(o => o.ID == id);
            List<Load> loads = context.Loads.ToList();
            

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
                return Redirect(string.Format("/Owner/ViewOwner/{0}", addOwnerItemViewModel.OwnerID));
            }
            return View(addOwnerItemViewModel);
        }

        public async Task<IActionResult> Expense(int? page)
        {
            var owners = from o in context.Owners
                         select o;
            int pageSize = 3;
            return View(await PaginatedList<Owner>.CreateAsync(owners.AsNoTracking(), page ?? 1, pageSize));

        }

        public async Task<IActionResult> ViewOwnerExpense(int id, int? page)
        {
            //TODO: #2 implement a paginatedList for ViewOwnerExenpense Action
            List<OwnerExpense> expenseItems = context.
               OwnerExpenses.
               Include(item => item.Expense).
               Where(ol => ol.OwnerID == id).
               ToList();
            Owner owner = context.Owners.Single(c => c.ID == id);
            ViewOwnerExpenseViewModel ownerExpenseItem = new ViewOwnerExpenseViewModel
            {
                Owner = owner,
                ExpenseItems = expenseItems
            };

            
            int pageSize = 3;
            return View(ownerExpenseItem);
        }

        
        
        public IActionResult OwnerExpenseItems(int id)
        {
           
            List<OwnerExpense> expenseItems = context.
                OwnerExpenses.
                Include(item => item.Expense).
                Where(ol => ol.ExpenseID == id).
                ToList();
            Owner owner = context.Owners.Single(c => c.ID == id);
            OwnerExpenseItemViewModel ownerExpenseItem = new OwnerExpenseItemViewModel
            {
                Owner = owner,
                ExpenseItems = expenseItems
            };

            return View(ownerExpenseItem);
        }


    
        
        
        public IActionResult AddExpenseItem(int id)
        {
            Owner owner = context.Owners.Single(o => o.ID == id);
            List<Expense> expenses = context.Expenses.ToList();
          

            return View(new AddOwnerExpenseItemViewModel(owner, expenses));

        }

   
        [HttpPost]
        public IActionResult AddExpenseItem(AddOwnerExpenseItemViewModel addOwnerExpenseItemViewModel)
        {
            //TODO: Display Category Name along item rather than the just the ID

            if (ModelState.IsValid)
            {
                
                var expenseID = addOwnerExpenseItemViewModel.ExpenseID;
                var ownerID = addOwnerExpenseItemViewModel.OwnerID;
                IList<OwnerExpense> existingItems = context.OwnerExpenses
                    .Where(ol => ol.ExpenseID == expenseID)
                    .Where(ol => ol.OwnerID == ownerID).ToList();
                if (existingItems.Count == 0)
                {

                    OwnerExpense ownerExpenseItem = new OwnerExpense
                    {
                        Expense = context.Expenses.Single(o => o.ID == expenseID),
                        Owner = context.Owners.Single(ol => ol.ID == ownerID)
                    };
                    context.OwnerExpenses.Add(ownerExpenseItem);
                    context.SaveChanges();
                }

                return Redirect(string.Format("/Owner/ViewOwnerExpense/{0}", addOwnerExpenseItemViewModel.OwnerID));
            }
            return View(addOwnerExpenseItemViewModel);
        }


        public IActionResult Remove(int ID)
        {
            var o = context.Owners.Single(e => e.ID == ID);
            if (o != null)
                context.Owners.Remove(o);

            context.SaveChanges();

            return Redirect("/Owner");


        }
       
        public async Task<IActionResult> Edit(int id)
        {
            var owner = context.Owners.SingleOrDefault((l) => l.ID == id);
            if (owner == null)
            {
                return NotFound();
            }
           
            return View(new AddOwnerViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AddOwnerViewModel addOwnerViewModel)
        {
            var owner = context.Owners.SingleOrDefault((l) => l.ID == addOwnerViewModel.ID);
            if (owner == null)
            {
                return NotFound();
            }
            

            var viewModel = new AddOwnerViewModel(owner);

            

            return View(viewModel);
        }
    }
}


