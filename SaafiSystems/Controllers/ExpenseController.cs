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
    public class ExpenseController : Controller
    {
        private readonly SaafiDbContext context;

        public ExpenseController(SaafiDbContext dbContext)
        {
            this.context = dbContext;
        }

        // GET: /<controller>/
        public async Task< IActionResult> Index(int? page)
        {
            //.Include(c => c.Owner).ToList();
            var expenses = from e in context.Expenses.Include(c => c.ExpenseCategory)
                          select e;
            int pageSize = 3;
            return View(await PaginatedList<Expense>.CreateAsync(expenses.AsNoTracking(), page ?? 1, pageSize));
           
        }

        public IActionResult Add()
        {
            IList<ExpenseCategory> expenseCategories = context.ExpenseCategories.ToList();
            AddExpenseViewModel addExpenseViewModel =
                new AddExpenseViewModel(context.ExpenseCategories.ToList(), null);
            return View(addExpenseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddExpenseViewModel addExpenseViewModel)
        {
            if (ModelState.IsValid)
            {
                ExpenseCategory newCategory =
                    context.ExpenseCategories.Single(c => c.ID == addExpenseViewModel.ExpenseCategoryID);
                // Add the new cheese to my existing cheeses
                Expense newExpense = new Expense
                {

                    Date = addExpenseViewModel.Date,
                    Reference = addExpenseViewModel.Reference,
                    Owner = addExpenseViewModel.Owner,
                  
                    Description = addExpenseViewModel.Description,
                    Amount = addExpenseViewModel.Amount,
                    ExpenseCategory = newCategory
                  
                    
                };

                context.Expenses.Add(newExpense);
                context.SaveChanges();

                return Redirect("/Expense");
            }

            return View(addExpenseViewModel);
        }

        public IActionResult Remove(int ID)
        {
            var exp = context.Expenses.Single(e => e.ID == ID);
            if (exp != null)
                context.Expenses.Remove(exp);

            context.SaveChanges();

            return Redirect("/Expense");

       
        }

        [HttpPost]
        public IActionResult Remove(int[] expenseIds)
        {
            foreach (int expenseId in expenseIds)
            {
                Revenue theRevenue = context.Revenues.Single(c => c.ID == expenseId);
                context.Revenues.Remove(theRevenue);
            }

            context.SaveChanges();

            return Redirect("/Expense");
        }
        [HttpPost]
        public IActionResult Category(int id)
        {
            if (id == 0)
            {
                Redirect("/Category");
            }

            ExpenseCategory theCategory = context.ExpenseCategories
                .Include(cat => cat.Expenses)
                .Single(cat => cat.ID == id);

            /* 
             * IList<Cheese> theCheese = Context.Cheeses
             * .Include(c => c.Category)
             * .Single(c => c.CategoryID ==id)
             .ToList();*/

            ViewBag.Title = "Expenses in Category" + theCategory.Name;
            return View("Index", theCategory.Expenses);
        }
      
        public ViewResult Search(string searchString)
        {
           //IList<Expense> expenses = context.Expenses.Include(c => c.ExpenseCategory).Include(c => c.Owner).ToList();
            var expenses = from c in context.Expenses.Include(c => c.ExpenseCategory).Include(c => c.Owner).ToList()
            select c;

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                expenses = context.Expenses.Where(c =>
               c.Reference.Contains(searchString)
                ||

               c.Date.ToString().Contains(searchString) ||

               c.Description.Contains(searchString) ||

               c.Owner.Contains(searchString));

          
            }
            
            return View(expenses);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var expense = context.Expenses.SingleOrDefault((l) => l.ID == id);
            if (expense == null)
            {
                return NotFound();
            }
            List<ExpenseCategory> newExpenseCategory =
                    new List<ExpenseCategory>() { context.ExpenseCategories.Single(c => c.ID == expense.ExpenseCategoryID) };
            return View(new AddExpenseViewModel(newExpenseCategory, expense));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AddExpenseViewModel addExpenseViewModel)
        {
            var expense = context.Expenses.SingleOrDefault((l) => l.ID == addExpenseViewModel.ID);
            if (expense == null)
            {
                return NotFound();
            }
            var newExpenseCategories = context.ExpenseCategories;

            var viewModel = new AddExpenseViewModel(newExpenseCategories, expense);

            var existingExpenseCategory = context.ExpenseCategories.SingleOrDefault(c => c.ID == expense.ExpenseCategoryID);

            if (existingExpenseCategory != null)
            {
                viewModel.ExpenseCategory = existingExpenseCategory;
                viewModel.ExpenseCategoryID = existingExpenseCategory.ID;
            }

            return View(viewModel);
        }
    }
}